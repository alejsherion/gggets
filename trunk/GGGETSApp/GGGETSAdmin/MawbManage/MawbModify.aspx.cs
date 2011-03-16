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
        private static string RRegion = @"^[A-Za-z]{3}";
        private MAWB mawb;
        private Package package;
        private DateTime time = DateTime.Now;
        public int n = 1;
        protected MawbModify()
        { }
        public MawbModify(IMAWBManagementService mawbservice, IPackageManagementService packageservice)
        {
            _mawbservice = mawbservice;
            _packageservice = packageservice;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Txt_MAWBBarCode.Focus();
                if (Request.QueryString["BarCode"] != "" && Request.QueryString["BarCode"] != null)
                {
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
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            if (Txt_BagBarCode.Text.Trim() == "")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('请输入总运单号！')</script>");
            }
            else
            {

                if (_packageservice.FindPackageByBarcode(Txt_BagBarCode.Text.Trim()) != null)
                {
                    package = _packageservice.FindPackageByBarcode(Txt_BagBarCode.Text.Trim());
                    //mawb = (MAWB)Session["mawb"];
                    if (mawb == null)
                    {
                        mawb = _mawbservice.FindMAWBByBarcode(Request.QueryString["BarCode"]);
                    }
                    mawb.Packages.Add(package);
                    _mawbservice.ModifyMAWB(mawb);
                    Txt_TotalWeight.Text = mawb.TotalWeight.ToString();
                    txt_TotalVolume.Text = mawb.TotalVolume.ToString();
                    gv_Bag.DataSource = mawb.Packages;
                    gv_Bag.DataBind();
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('没有该包记录！')</script>");
                }

            }
            if (gv_Bag.Rows.Count != 0)
            {
                btn_Close.Visible = true;
            }
            Txt_BagBarCode.Text = string.Empty;
            Txt_BagBarCode.Focus();
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            Addmawb(0);
        }

        protected void btn_SaveAndClose_Click(object sender, EventArgs e)
        {
            Addmawb(1);
        }
        protected void Addmawb(int type)
        {
            if (mawb == null)
            {
                mawb = _mawbservice.FindMAWBByBarcode(Txt_MAWBBarCode.Text.Trim().ToUpper());
            }
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
                Response.Write("<script>alert('修改成功！');location='MawbManagement.aspx'</script>");
                
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('机场三字码只能输入字母并为3位！！')</script>");
            }
        }
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
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('删除成功！')</script>");
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
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('请选择要删除的记录！')</script>");
            }
        }
        public int N()
        {
            return n++;
        }
    }
}