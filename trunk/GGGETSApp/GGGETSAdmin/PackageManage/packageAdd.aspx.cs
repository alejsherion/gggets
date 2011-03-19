using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;

namespace GGGETSAdmin.PackageManage
{
    public partial class packageAdd : System.Web.UI.Page
    {
        private static string RRegion = @"^[A-Za-z]";
        private int n = 1;
        private Package package;
        private DateTime time = DateTime.Now;
        private HAWB hawb;
        private IPackageManagementService _packageservice;
        private IHAWBManagementService _hawbservice;
        private static IRegionCodeManagementService _regionservice;
        protected packageAdd()
        { }
        public packageAdd(IPackageManagementService packageservice, IHAWBManagementService hawbservice, IRegionCodeManagementService regionservice)
        {
            _packageservice=packageservice;
            _hawbservice = hawbservice;
            _regionservice = regionservice;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            txt_CreateTime.Text = time.ToString("yyyy-MM-dd HH:mm");
            Txt_BagBarCode.Focus();
            if (!IsPostBack)
            {
                package = new Package();
                Session["package"] = package;
                if (gv_HAWB.Rows.Count == 0)
                {
                    btn_Close.Visible = false;
                }
            }
        }
        /// <summary>
        /// 包添加运单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            string type = rbtn_PackageType.SelectedValue;
            if (txt_BarCode.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请输入运单号!')", true);
                txt_BarCode.Focus();
            }
            else
            {
                if (Txt_BagBarCode.Text.Trim() != "" && Txt_Region.Text.Trim() != "")
                {
                    hawb = _hawbservice.FindHAWBByBarCode(txt_BarCode.Text.Trim());
                    if (hawb != null)
                    {

                        package = (Package)Session["package"];
                        if (package == null)
                        {
                            package = new Package();
                        }

                        if (package.JudgeHAWB(hawb))//判断运单是否在该包里面
                        {
                            bool isMix;
                            if (rbtn_PackageType.SelectedValue == "0")//判断是否混包
                            {
                                isMix = false;
                            }
                            else
                            {
                                isMix = true;
                            }
                            if (_packageservice.JudgePIDIsNull(hawb.BarCode))//判断该运单是否已经打包
                            {
                                //if (_packageservice.JudgeRegionCodeIsRepeat(hawb.BarCode, Txt_Region.Text.Trim().ToUpper(), isMix))
                                //{
                                package.HAWBs.Add(hawb);
                                txt_Pice.Text = package.Piece.ToString();
                                Txt_TotalWeight.Text = package.TotalWeight.ToString();
                                gv_HAWB.DataSource = package.HAWBs;
                                gv_HAWB.DataBind();
                                Session["package"] = package;
                                //}
                                //else
                                //{
                                //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('包目的地和运单目的地不一直!')", true);
                                //}
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('该运单已经添加，不能再次进行添加!')", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('该运单已经添加，不能再次进行添加!')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有该运单记录!')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请先输入包的信息!')", true);
                }
            }
            if (gv_HAWB.Rows.Count != 0)
            {
                btn_Close.Visible = true;
            }
            txt_BarCode.Text = string.Empty;
            txt_BarCode.Focus();
            
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            AddPackage(0);
        }
        /// <summary>
        /// 保存并关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_SaveAndClose_Click(object sender, EventArgs e)
        {
            AddPackage(1);
        }
        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="type">保存类型0:保存,1：保存并关闭</param>
        protected void AddPackage(int type)
        {
            package = (Package)Session["package"];
            bool ok = false;
            if (Txt_BagBarCode.Text.Trim() != "")
            {
                if (Txt_Region.Text.Trim() != "")
                {
                    package.RegionCode = Txt_Region.Text.Trim().ToUpper();
                    if (txt_Pice.Text.Trim() != "")
                    {
                        package.Piece = int.Parse(txt_Pice.Text.Trim());
                    }
                    if (Txt_TotalWeight.Text.Trim() != "")
                    {
                        package.TotalWeight = decimal.Parse(Txt_TotalWeight.Text.Trim());
                    }
                    if (type == 1)
                    {
                        package.Status = 1;
                    }
                    if (Regex.IsMatch(Txt_Region.Text.Trim(), RRegion))
                    {
                        IList<RegionCode> Regioncode = _regionservice.FindAllRegionCodes();
                        foreach (RegionCode regioncode in Regioncode)
                        {
                            if (regioncode.RegionCode1 == Txt_Region.Text.Trim().ToUpper())
                            {
                                ok = true;
                                break;
                            }
                        }
                        if (!ok)
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有该三字码!')", true);
                            Txt_Region.Focus();
                        }
                        else
                        {
                            package.PID = Guid.NewGuid();
                            package.BarCode = Txt_BagBarCode.Text.Trim().ToUpper();
                            package.CreateTime = DateTime.Parse(txt_CreateTime.Text.Trim());
                            package.UpdateTime = DateTime.Now;
                            package.RegionCode = Txt_Region.Text.Trim().ToUpper();
                            package.Operator = "ceshi";
                            _packageservice.AddPackage(package);
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('添加成功!')", true);
                            Session["package"] = null;
                            Txt_BagBarCode.Text = string.Empty;
                            txt_CreateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                            Txt_Region.Text = string.Empty;
                            txt_Pice.Text = string.Empty;
                            Txt_TotalWeight.Text = string.Empty;
                            gv_HAWB.DataSource = null;
                            gv_HAWB.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('目的地三字码只能输入字母!')", true);
                        Txt_Region.Focus();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('目的地三字码不能为空!')", true);
                    Txt_Region.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('包号不能为空!')", true);
                Txt_BagBarCode.Focus();
            }
        }
        /// <summary>
        /// 删除包里面的运单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Close_Click(object sender, EventArgs e)
        {
            if (package == null)
            {
                package = (Package)Session["package"];
            }
            bool Ok = false;
            for (int i = gv_HAWB.Rows.Count - 1; i > -1; i--)
            {
                string Bar = string.Empty;
                if (((CheckBox)gv_HAWB.Rows[i].FindControl("chkId")).Checked)
                {
                    Ok = true;
                    Bar = gv_HAWB.DataKeys[i].Value.ToString();
                    foreach (HAWB ha in package.HAWBs)
                    {
                        if (ha.BarCode == Bar)
                        {
                            hawb = ha;
                        }
                    }
                    package.HAWBs.Remove(hawb);
                    txt_Pice.Text = package.Piece.ToString();
                    Txt_TotalWeight.Text = package.TotalWeight.ToString();
                }
            }
            if (Ok == true)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('删除成功!')", true);
                if (package.HAWBs.Count == 0)
                {
                    btn_Close.Visible = false;
                }
                gv_HAWB.DataSource = package.HAWBs;
                gv_HAWB.DataBind();
                txt_BarCode.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请选择要移除的记录!')", true);
            }
            
        }
        /// <summary>
        /// 清空页面控件值
        /// </summary>
        /// <param name="objControlCollection"></param>
        private void InitialControl(ControlCollection objControlCollection)
        {
            foreach (System.Web.UI.Control objControl in objControlCollection)
            {
                if (objControl.HasControls())
                {
                    InitialControl(objControl.Controls);
                }
                else
                {
                    if (objControl is System.Web.UI.WebControls.TextBox)
                    {
                        ((TextBox)objControl).Text = String.Empty;
                    }
                }
            }
            txt_Pice.Text = string.Empty;
            Txt_TotalWeight.Text = string.Empty;
        }
        /// <summary>
        /// 前台行号显示方法
        /// </summary>
        /// <returns></returns>
        public int N()
        {
            return n++;
        }
        /// <summary>
        /// 判断包是否已存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Txt_BagBarCode_TextChanged(object sender, EventArgs e)
        {
            Package pack = _packageservice.FindPackageByBarcode(Txt_BagBarCode.Text.Trim());
            if (pack != null)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('该包号已存在!')", true);
                Txt_BagBarCode.Focus();
            }
            else
            {
                Txt_Region.Focus();
            }
        }
        /// <summary>
        /// 三字码填充
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[][] GetCountryList(string prefixText, int count)
        {
            if (count == 0)
            {
                count = 10;
            }

            List<string[]> items = new List<string[]>();

            IList<RegionCode> regioncode = _regionservice.FindAllRegionCodes();
            foreach (RegionCode region in regioncode)
            {
                string[] ItemArry = new string[3];
                ItemArry[0] = region.RegionName;
                ItemArry[1] = region.RegionCode1;
                items.Add(ItemArry);
            }
            return items.Take(count).ToArray();
        }

        protected void autocomplete_ItemSelected(object sender, EventArgs e)
        {
            Txt_Region.Text = ((AutoCompleteExtra.AutoCompleteExtraExtender)sender).SelectedValue;
            txt_BarCode.Focus();
        }
        /// <summary>
        /// 判断是否正确的三字码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Txt_Region_TextChanged(object sender, EventArgs e)
        {
            Region();
        }
        private void Region()
        {
            bool ok = false;
            if (!string.IsNullOrEmpty(Txt_Region.Text.Trim()))
            {
                if (Regex.IsMatch(Txt_Region.Text.Trim(), RRegion))
                {
                    IList<RegionCode> Regioncode = _regionservice.FindAllRegionCodes();
                    foreach (RegionCode regioncode in Regioncode)
                    {
                        if (regioncode.RegionCode1 == Txt_Region.Text.Trim().ToUpper())
                        {
                            ok = true;
                            break;
                        }
                    }
                    if (!ok)
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有该三字码!')", true);
                        Txt_Region.Focus();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('目的地三字码只能为字母!')", true);
                    Txt_Region.Focus();
                }
            }
        }
    }
}