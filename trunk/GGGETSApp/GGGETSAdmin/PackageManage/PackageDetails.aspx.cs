using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;

namespace GGGETSAdmin.PackageManage
{
    public partial class PackageDetails : System.Web.UI.Page
    {
        private IPackageManagementService _packageservice;
        private IMAWBManagementService _mawbservice;
        private Package package;

        public int n = 1;
        protected PackageDetails()
        { }
        public PackageDetails(IPackageManagementService packageservice,IMAWBManagementService mawbservice)
        {
            _packageservice = packageservice;
            _mawbservice = mawbservice;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                if (Request.QueryString["BarCode"] != "" && Request.QueryString["BarCode"] != null)
                {
                    ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
                    package = _packageservice.FindPackageByBarcode(Request.QueryString["BarCode"]);
                    if (package != null)
                    {
                        Evaluate(package);
                        
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('没有相关记录！');location='packageManagement.aspx'</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('没有相关记录！');location='packageManagement.aspx'</script>");
                }
            }
        }
        protected void Evaluate(Package package)
        {
            txt_BagBarCode.Text = package.BarCode;
            txt_CreateTime.Text = package.CreateTime.ToString("yyyy-MM-dd HH:mm");
            txt_UpdateTime.Text = package.UpdateTime.ToString("yyyy-MM-dd HH:mm");
            
            if (package.MAWB != null)
            {
                lbtn_MHAWb.Text = package.MAWB.BarCode;
                
            }
            txt_Destination.Text = package.RegionCode;
            txt_Pice.Text = package.Piece.ToString();
            Txt_TotalWeight.Text = package.TotalWeight.ToString();
            txt_Status.Text = package.Status.ToString().Replace("0","打开").Replace("1","关闭");
            gv_HAWB.DataSource = package.HAWBs;
            gv_HAWB.DataBind();
        }
        public int N()
        {
            return n++;
        }
        protected void But_Conel_Click(object sender, EventArgs e)
        {
            Response.Redirect((string)ViewState["UrlReferrer"]);
        }

        protected void But_Next_Click(object sender, EventArgs e)
        {
            //if (txt_Status.Text == "打开")
            //{
                Response.Redirect("packageModify.aspx?BarCode=" + txt_BagBarCode.Text + "");
            //}
            //else
            //{
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('该包处于关闭状态，不能进行修改！')</script>");
            //}
        }

        protected void lbtn_MHAWb_Click(object sender, EventArgs e)
        {
            Response.Redirect("../MawbManage/MawbDetails.aspx?BarCode=" + lbtn_MHAWb.Text + "");
        }
    }
}