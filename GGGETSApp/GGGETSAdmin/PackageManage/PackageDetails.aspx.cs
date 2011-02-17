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
        private Package package;
        public int n = 1;
        protected PackageDetails()
        { }
        protected PackageDetails(IPackageManagementService packageservice)
        {
            _packageservice = packageservice;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["BarCode"] != "")
                {
                    package = _packageservice.FindPackageByBarcode(Request.QueryString["BarCode"]);
                    Evaluate(package);
                }
                else
                {
                    Response.Redirect(Request.UrlReferrer.ToString());
                }
            }
        }
        protected void Evaluate(Package package)
        {
            txt_BagBarCode.Text = package.BarCode;
            txt_CreateTime.Text = package.CreateTime.ToString("yyyy-MM-dd HH:mm");
            txt_UpdateTime.Text = package.UpdateTime.ToString("yyyy-MM-dd HH:mm");
            //lbtn_MHAWb.Text=
            txt_Pice.Text = package.Piece.ToString();
            Txt_TotalWeight.Text = package.TotalWeight.ToString();
            txt_Status.Text = package.Status.ToString();
            gv_HAWB.DataSource = package.HAWBs;
            gv_HAWB.DataBind();
        }
        public int N()
        {
            return n++;
        }
        protected void But_Conel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.UrlReferrer.ToString());
        }

        protected void But_Next_Click(object sender, EventArgs e)
        {
            Response.Redirect("packageModify?BarCode=" + txt_BagBarCode.Text + "");
        }
    }
}