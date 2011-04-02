using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;

namespace GGGETSAdmin.MawbManage
{
    public partial class MawbModify : System.Web.UI.Page
    {
        protected IMAWBManagementService _mawbservice;
        protected IPackageManagementService _packageservice;
        private ISysUserManagementService _sysUserManagementService;
        private static string RRegion = @"^[A-Za-z]{3}";
        private MAWB mawb;
        private Package package;
        private DateTime time = DateTime.Now;
        public int n = 1;
        protected MawbModify()
        { }
        public MawbModify(IMAWBManagementService mawbservice, IPackageManagementService packageservice, ISysUserManagementService sysUserManagementService)
        {
            _mawbservice = mawbservice;
            _packageservice = packageservice;
            _sysUserManagementService = sysUserManagementService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Txt_MAWBBarCode.Focus();
                if (Request.QueryString["BarCode"] != "" && Request.QueryString["BarCode"] != null)
                {
                    
                        if (!bool.Parse(Request.QueryString["Privilege"]))
                        {
                            btn_Add.Enabled = false;
                            btn_Save.Enabled = false;
                            btn_SaveAndClose.Enabled = false;
                            btn_Close.Enabled = false;
                        }
                        else
                        {
                            if (txt_Status.Text != "打开")
                            {
                                if (!bool.Parse(Request.QueryString["Privilege1"]))
                                {
                                    btn_Add.Enabled = false;
                                    btn_Save.Enabled = false;
                                    btn_SaveAndClose.Enabled = false;
                                    btn_Close.Enabled = false;
                                }
                            }
                        }
                    ViewState["UrlReferrer"] = Request.UrlReferrer;
                    mawb = _mawbservice.FindMAWBByBarcode(Request.QueryString["BarCode"]);
                    if (mawb != null)
                    {
                        Evaluate(mawb);
                        
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('没有相关记录！');location='MawbManagement.aspx'</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('没有相关记录！');location='MawbManagement.aspx'</script>");
                }
            }
        }
        /// <summary>
        /// 页面控件赋值
        /// </summary>
        /// <param name="mawb"></param>
        protected void Evaluate(MAWB mawb)
        {
            txt_FLTNo.Text = mawb.FlightNo;
            txt_From.Text = mawb.From;
            txt_To.Text = mawb.To;
            txt_CreateTime.Text = mawb.CreateTime.ToString("yyyy-MM-dd HH:mm");
            if (mawb.LockedTime != null)
            {
                txt_UpdateTime.Text = mawb.LockedTime.Value.ToString("yyyy-MM-dd HH:mm");
            }
            Txt_MAWBBarCode.Text = mawb.BarCode;
            txt_Status.Text = mawb.Status.ToString().Replace("0","打开").Replace("1","关闭");
            Txt_TotalWeight.Text = mawb.TotalWeight.ToString();
            txt_TotalVolume.Text = mawb.TotalVolume.ToString();
            gv_Bag.DataSource = mawb.Packages;
            gv_Bag.DataBind();
            if (gv_Bag.Rows.Count == 0)
            {
                btn_Close.Visible = false;
            }
                

        }
        /// <summary>
        /// 总运单添加包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            bool ok = true;
            if (Txt_BagBarCode.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请输入总运单号!')", true);
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('请输入总运单号！')</script>");
            }
            else
            {
                package = _packageservice.FindPackageByBarcode(Txt_BagBarCode.Text.Trim());
                if (package != null)
                {
                    if (_mawbservice.JudgeMIDIsNull(package.BarCode))
                    {
                        //mawb = (MAWB)Session["mawb"];
                        if (mawb == null)
                        {
                            mawb = _mawbservice.FindMAWBByBarcode(Request.QueryString["BarCode"]);
                        }
                        if (mawb.Packages.Count == 0)
                        {
                            mawb.Packages.Add(package);
                        }
                        else
                        {
                            foreach (Package pack in mawb.Packages)
                            {
                                if (package.BarCode == pack.BarCode)
                                {
                                    ok = false;
                                    break;
                                }
                            }
                        }
                        if (ok)
                        {
                            mawb.Packages.Add(package);
                            _mawbservice.ModifyMAWB(mawb);
                            Txt_TotalWeight.Text = mawb.TotalWeight.ToString();
                            txt_TotalVolume.Text = mawb.TotalVolume.ToString();
                            gv_Bag.DataSource = mawb.Packages;
                            gv_Bag.DataBind();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('该包已经添加，不能再次进行添加!')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('该包已经添加，不能再次进行添加!')", true);
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('！')</script>");
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有该包记录!')", true);
                    
                }

            }
            if (gv_Bag.Rows.Count != 0)
            {
                btn_Close.Visible = true;
            }
            Txt_BagBarCode.Text = string.Empty;
            Txt_BagBarCode.Focus();
        }
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            Addmawb(0);
        }
        /// <summary>
        /// 保存并关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_SaveAndClose_Click(object sender, EventArgs e)
        {
            Addmawb(1);
        }
        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="type">保存类型0：保存,1:保存并关闭</param>
        protected void Addmawb(int type)
        {
            if (mawb == null)
            {
                mawb = _mawbservice.FindMAWBByBarcode(Txt_MAWBBarCode.Text.Trim().ToUpper());
            }
            if (Txt_MAWBBarCode.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请输入总运单号!')", true);
            }
            else if (txt_FLTNo.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请输入航班号!')", true);
            }
            else
            {
                if (Txt_TotalWeight.Text.Trim() != "")
                {
                    mawb.TotalWeight = decimal.Parse(Txt_TotalWeight.Text.Trim());
                }
                if (txt_TotalVolume.Text.Trim() != "")
                {
                    mawb.TotalVolume = decimal.Parse(txt_TotalVolume.Text.Trim());
                }
                if (type == 1)
                {
                    mawb.Status = 1;
                }
                else
                {
                    mawb.Status = 0;
                }
                if (Regex.IsMatch(txt_From.Text.Trim(), RRegion) && Regex.IsMatch(txt_To.Text.Trim(), RRegion))
                {
                    mawb.BarCode = Txt_MAWBBarCode.Text.Trim().ToUpper();
                    mawb.FlightNo = txt_FLTNo.Text.Trim().ToUpper();
                    mawb.From = txt_From.Text.Trim().ToUpper();
                    mawb.To = txt_To.Text.Trim().ToUpper();
                    mawb.CreateTime = DateTime.Parse(txt_CreateTime.Text.Trim());
                    mawb.LockedTime = DateTime.Now;
                    mawb.Operator = "ceshi";
                    _mawbservice.ModifyMAWB(mawb);
                    Txt_MAWBBarCode.Text = string.Empty;
                    txt_FLTNo.Text = string.Empty;
                    txt_CreateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    txt_From.Text = string.Empty;
                    txt_To.Text = string.Empty;
                    txt_Status.Text = string.Empty;
                    Txt_TotalWeight.Text = string.Empty;
                    txt_TotalVolume.Text = string.Empty;
                    gv_Bag.DataSource = null;
                    gv_Bag.DataBind();
                    btn_Close.Visible = false;
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "Url()", true);
                    //Response.Write("<script>alert('修改成功！');location='MawbManagement.aspx'</script>");

                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('机场三字码只能输入字母并为3位!')", true);
                }
            }
        }
        /// <summary>
        /// 删除总运单里的包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Close_Click(object sender, EventArgs e)
        {
            bool ok = false;
            if (mawb == null)
            {
                mawb = _mawbservice.FindMAWBByBarcode(Request.QueryString["BarCode"]);
            }
            for (int i = gv_Bag.Rows.Count - 1; i > -1; i--)
            {
                if (((CheckBox)gv_Bag.Rows[i].FindControl("chkId")).Checked)
                {
                    ok = true;
                    string barcode = ((Label)gv_Bag.Rows[i].FindControl("lbl_BagBarCode")).Text;

                    Package pack = _packageservice.FindPackageByBarcode(barcode);

                    mawb.Packages.Remove(pack);
                    Txt_TotalWeight.Text = mawb.TotalWeight.ToString();
                    txt_TotalVolume.Text = mawb.TotalVolume.ToString();
                }
            }
            if (ok == true)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('删除成功!')", true);
                _mawbservice.ModifyMAWB(mawb);
                if (mawb.Packages == null)
                {
                    btn_Close.Visible = false;
                }
                gv_Bag.DataSource = mawb.Packages;
                gv_Bag.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请选择要删除的记录!')", true);
               
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
    }
}