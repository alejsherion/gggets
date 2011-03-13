using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
namespace GGGETSAdmin.PersonnelManage.CompanyManage
{
    public partial class CompanyDetails : System.Web.UI.Page
    {
        private ICompanyManagementService _companyService;
        protected CompanyDetails()
        { }
        public CompanyDetails(ICompanyManagementService companyservice)
        {
            _companyService = companyservice;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["CompanyCode"]))
                {
                    string CompanyCode = Request.QueryString["CompanyCode"].ToString();
                    Storage(CompanyCode);
                    ViewState["Url"] = Request.UrlReferrer.ToString();

                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('没有相关记录！');location='CompanyManagemnet.aspx'</script>");

                }
            }
        }
        protected void Storage(string Companycode)
        {
            Company company = _companyService.FindCompanyByCompanyCode(Companycode);
            if (company != null)
            {
                Txt_CompanyCode.Text = company.CompanyCode;
                Txt_FullName.Text = company.FullName;
                Txt_ShortName.Text = company.ShortName;
                Txt_Contactor.Text = company.Contactor;
                Txt_ContactorPhone.Text = company.ContactorPhone;
                Txt_Phone.Text = company.Phone;
                Txt_Fax.Text = company.Fax;
                Txt_Address.Text = company.Address;
                Txt_PostCode.Text = company.PostCode;
                Txt_OrganizationCode.Text = company.OrganizationCode;
                Txt_Remark.Text = company.Remark;
                if (company.Status == 0)
                {
                    Txt_Status.Text = "可用";
                }
                else
                {
                    Txt_Status.Text = "不可用";
                }
            }
        }

        protected void But_Next_Click(object sender, EventArgs e)
        {
            Response.Redirect("CompanyModify.aspx?CompanyCode=" + Txt_CompanyCode.Text.Trim() + "");
        }

        protected void But_Conel_Click(object sender, EventArgs e)
        {
            Response.Redirect((string)ViewState["Url"]);
        }
    }
}