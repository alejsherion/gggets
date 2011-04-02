using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;

namespace GGGETSAdmin.PersonnelManage.CompanyManage
{
    public partial class CompanyModify : System.Web.UI.Page
    {
        private ICompanyManagementService _companyService;
        private ISysUserManagementService _sysUserManagementService;
        private Company company;
        private static Regex RTel = new Regex(@"^[0-9]*$");
        protected CompanyModify()
        { }
        public CompanyModify(ICompanyManagementService companyservice, ISysUserManagementService sysUserManagementService)
        {
            _companyService = companyservice;
            _sysUserManagementService = sysUserManagementService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["CompanyCode"]))
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["Privilege"]))
                    {
                        if (!bool.Parse(Request.QueryString["Privilege"]))
                        {
                            btn_Update.Enabled = false;
                        }
                    }
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
        /// <summary>
        /// 页面控件赋值
        /// </summary>
        /// <param name="Companycode">公司账号</param>
        protected void Storage(string Companycode)
        {
            company = _companyService.FindCompanyByCompanyCode(Companycode);
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
                DDL_Status.SelectedValue = company.Status.ToString();
                Session["Company"] = company;
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_UpCompany_Click(object sender, EventArgs e)
        {
            bool ok = true;
            if (company == null)
            {
                if (Session["Company"] != null)
                {
                    company = (Company)Session["Company"];
                }
                else
                {
                    company = _companyService.FindCompanyByCompanyCode(Request.QueryString["CompanyCode"].ToString());
                }
            }
            if (Txt_CompanyCode.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入公司账号！')</script>");
                Txt_CompanyCode.Focus();
                ok = false;
            }
            else if (Txt_FullName.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入公司全称！')</script>");
                Txt_FullName.Focus();
                ok = false;
            }
            else if (Txt_ShortName.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入公司简称！')</script>");
                Txt_ShortName.Focus();
                ok = false;
            }
            else if (Txt_Contactor.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入联系人！')</script>");
                ok = false;
                Txt_Contactor.Focus();
            }
            else if (Txt_ContactorPhone.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入联系人电话！')</script>");
                ok = false;
                Txt_ContactorPhone.Focus();
            }
            else if (Txt_PostCode.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入邮政编码！')</script>");
                ok = false;
                Txt_PostCode.Focus();
            }
            else if (Txt_Address.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入地址！')</script>");
                ok = false;
                Txt_Address.Focus();
            }
            else if (Txt_Phone.Text.Trim() != "" && !RTel.IsMatch(Txt_Phone.Text.Trim()))
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入数字！')</script>");
                ok = false;
                Txt_Phone.Focus();
            }
            else if (!RTel.IsMatch(Txt_ContactorPhone.Text.Trim()))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入数字！')</script>");
                ok = false;
                Txt_ContactorPhone.Focus();
            }
            else
            {
                if (ok == true)
                {
                    company.FullName = Txt_FullName.Text.Trim().ToUpper();
                    company.ShortName = Txt_ShortName.Text.Trim().ToUpper();
                    company.Contactor = Txt_Contactor.Text.Trim().ToUpper();
                    company.ContactorPhone = Txt_ContactorPhone.Text.Trim();
                    company.Phone = Txt_Phone.Text.Trim();
                    company.Fax = Txt_Phone.Text.Trim();
                    company.Address = Txt_Address.Text.Trim();
                    company.PostCode = Txt_PostCode.Text.Trim();
                    company.OrganizationCode = Txt_OrganizationCode.Text.Trim();
                    company.Remark = Txt_Remark.Text.Trim();
                    company.Status = int.Parse(DDL_Status.SelectedValue);
                    _companyService.ModifyCompany(company);
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('修改成功！');location='CompanyManagemnet.aspx'</script>");
                }
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect((string)ViewState["Url"]);
        }
    }
}