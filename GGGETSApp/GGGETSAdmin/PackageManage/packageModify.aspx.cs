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

    public partial class packageModify : System.Web.UI.Page
    {
        private static string RRegion = @"^[A-Za-z]{3}";
        private IPackageManagementService _packageservice;
        private IHAWBManagementService _hawbservice;
        private static IRegionCodeManagementService _regionservice;
        private IMAWBManagementService _mawbservice;
        private ISysUserManagementService _sysUserManagementService;
        private Package package;
        private HAWB hawb = new HAWB();
        private int n = 1;
        private List<Guid> list = new List<Guid>();
        protected packageModify()
        { }
        public packageModify(IPackageManagementService packageservice, IHAWBManagementService hawbservice, IRegionCodeManagementService regionservice, IMAWBManagementService mawbservice, ISysUserManagementService sysUserManagementService)
        {
            _packageservice = packageservice;
            _hawbservice = hawbservice;
            _regionservice = regionservice;
            _mawbservice = mawbservice;
            _sysUserManagementService = sysUserManagementService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                if (Request.QueryString["BarCode"] != "" && Request.QueryString["BarCode"] != null)
                {

                    if (!bool.Parse(Request.QueryString["Privilege"]) || !bool.Parse(Request.QueryString["Privilege1"]))
                    {
                        btn_Add.Enabled = false;
                        btn_Save.Enabled = false;
                        btn_SaveAndClose.Enabled = false;
                        btn_Close.Enabled = false;
                    }
                   
                    ViewState["UrlReferrer"] = Request.UrlReferrer;
                    package = _packageservice.FindPackageByBarcode(Request.QueryString["BarCode"]);
                    if (package != null)
                    {
                        Evaluate(package);
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('没有相关记录！');location='packageManagement.aspx'</script>");
                        //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有相关记录!');", true);
                        //Response.Redirect("<script>alert('没有相关记录！');location='packageManagement.aspx'</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('没有相关记录！');location='packageManagement.aspx'</script>");
                    //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有相关记录!');", true);
                    //Response.Redirect("<script>alert('没有相关记录！');location='packageManagement.aspx'</script>");
                }
            }
        }
        /// <summary>
        /// 页面控件赋值
        /// </summary>
        /// <param name="package"></param>
        protected void Evaluate(Package package)
        {
            lbtn_BagBarCode.Text = package.BarCode;
            txt_CreateTime.Text = package.CreateTime.ToString("yyyy-MM-dd HH:mm");
            txt_UpdateTime.Text = package.UpdateTime.ToString("yyyy-MM-dd HH:mm");
            if (package.MID != null)
            {
                MAWB mawb = _mawbservice.FindMAWBByMID(package.MID.ToString());
                if (mawb != null)
                {
                    Txt_MAWBCode.Text = mawb.BarCode;
                    txt_FLTNo.Text = mawb.FlightNo;
                }
            }
            txt_Destination.Text = package.OriginalRegionCode;
            txt_Pice.Text = package.Piece.ToString();
            Txt_TotalWeight.Text = package.TotalWeight.ToString();
            
            gv_HAWB.DataSource = package.HAWBs;
            gv_HAWB.DataBind();
            if (gv_HAWB.Rows.Count == 0)
            {
                btn_Close.Visible = false;
            }
        }
        /// <summary>
        /// 删除包里的运单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Close_Click(object sender, EventArgs e)
        {
            if (package == null)
            {
                package = _packageservice.FindPackageByBarcode(lbtn_BagBarCode.Text);
            }
            bool Ok = false;
            
            for (int i = gv_HAWB.Rows.Count - 1; i > -1; i--)
            {
                string Bar = string.Empty;
                if (((CheckBox)gv_HAWB.Rows[i].FindControl("chkId")).Checked)
                {
                    Ok = true;
                    Bar = (((LinkButton)gv_HAWB.Rows[i].FindControl("lbtn_BarCoder") as LinkButton).Text);
                    HAWB hawb = _hawbservice.FindHAWBByBarCode(Bar);
                    package.HAWBs.Remove(hawb);
                    txt_Pice.Text = package.Piece.ToString();
                    Txt_TotalWeight.Text = package.TotalWeight.ToString();
                   
                }
            }
            if (Ok)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('删除成功!');", true);
                _packageservice.ModifyPackage(package);
                if (package.HAWBs.Count == 0)
                {
                    btn_Close.Visible = false;
                }
                gv_HAWB.DataSource = package.HAWBs;
                gv_HAWB.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请选择要删除的记录!');", true);
            }
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
        /// 包添加运单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            if (txt_BarCode.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请输入运单号!');", true);
            }
            else
            {
                hawb = _hawbservice.FindHAWBByBarCode(txt_BarCode.Text.Trim());
                if (hawb != null)
                {

                    package = _packageservice.FindPackageByBarcode(lbtn_BagBarCode.Text);
                    if (package == null)
                    {
                        package = new Package();
                    }
                    if (_packageservice.JudgePIDIsNull(hawb.BarCode))//判断运单是否已存在包号
                    {
                        if (package.JudgeHAWB(hawb))//判断该包是否存在该条运单
                        {
                            //if (_packageservice.JudgeRegionCodeIsRepeat(hawb.BarCode, txt_Destination.Text.Trim().ToUpper(), package.IsMixed))
                            //{
                            package.HAWBs.Add(hawb);
                            _packageservice.ModifyPackage(package);
                            txt_Pice.Text = package.Piece.ToString();
                            Txt_TotalWeight.Text = package.TotalWeight.ToString();
                            gv_HAWB.DataSource = package.HAWBs;
                            gv_HAWB.DataBind();
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
            
            ModifyPackage(0);
        }
        /// <summary>
        /// 保存并关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_SaveAndClose_Click(object sender, EventArgs e)
        {
            ModifyPackage(1);
        }
        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="type">0:保存，1：保存并关闭</param>
        protected void ModifyPackage(int type)
        {
            bool ok = false;
            if (package == null)
            {
                package = _packageservice.FindPackageByBarcode(lbtn_BagBarCode.Text);
            }

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
            else
            {
                package.Status = 0;
            }
            if (Regex.IsMatch(txt_Destination.Text.Trim(), RRegion))
            {
                IList<RegionCode> Regioncode = _regionservice.FindAllRegionCodes();
                foreach (RegionCode regioncode in Regioncode)
                {
                    if (regioncode.RegionCode1 == txt_Destination.Text.Trim().ToUpper())
                    {
                        ok = true;
                        break;
                    }
                }
                if (!ok)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有该三字码!')", true);
                    txt_Destination.Focus();
                }
                else
                {
                    if (Txt_MAWBCode.Text != "")
                    {
                        if (txt_FLTNo.Text != "")
                        {
                            MAWB mawb = _mawbservice.FindMAWBByBarcode(Txt_MAWBCode.Text.Trim());
                            if (mawb != null)
                            {
                                if (mawb.FlightNo == txt_FLTNo.Text.Trim())
                                {
                                    package.MID = mawb.MID;
                                    package.CreateTime = DateTime.Parse(txt_CreateTime.Text.Trim());
                                    package.UpdateTime = DateTime.Now;
                                    package.DestinationRegionCode = txt_Destination.Text.Trim().ToUpper();
                                    _packageservice.ModifyPackage(package);
                                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "Url()", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('航班号不正确!');", true);
                                    txt_FLTNo.Focus();
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有该总运单号!');", true);
                                Txt_MAWBCode.Focus();
                                Txt_MAWBCode.Text = "";
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请输入航班号!');", true);
                            txt_FLTNo.Focus();
                        }
                    }
                    else
                    {
                        package.MID = null;
                        package.CreateTime = DateTime.Parse(txt_CreateTime.Text.Trim());
                        package.UpdateTime = DateTime.Now;
                        package.DestinationRegionCode = txt_Destination.Text.Trim().ToUpper();
                        _packageservice.ModifyPackage(package);
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "Url()", true);

                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('目的地三字码只能输入字母!');", true);
                txt_Destination.Focus();
            }
            
        }
        /// <summary>
        /// 查看包信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtn_BagBarCode_Click(object sender, EventArgs e)
        {
            Response.Redirect("PackageDetails.aspx?BarCode=" + lbtn_BagBarCode.Text + "");
        }
        /// <summary>
        /// 地区三字码填充
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
                string[] ItemArry = new string[2];
                ItemArry[0] = region.RegionName;
                ItemArry[1] = region.RegionCode1;
                items.Add(ItemArry);
            }
            return items.Take(count).ToArray();
        }

        protected void autocomplete_ItemSelected(object sender, EventArgs e)
        {
            txt_Destination.Text = ((AutoCompleteExtra.AutoCompleteExtraExtender)sender).SelectedValue;
        }


    }
}