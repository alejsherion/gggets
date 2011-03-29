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
    public partial class CompanyAdd : System.Web.UI.Page
    {
        private ICompanyManagementService _companyService;
        private IAddressBookManagementService _addressService;
        private static Regex RTel = new Regex(@"^[0-9]*$");
        private Company company;
        private ISysUserManagementService _SysUserManagementService;
        protected CompanyAdd()
        { }
        public CompanyAdd(ICompanyManagementService companyService, IAddressBookManagementService addressservice, ISysUserManagementService SysUserManagementService)
        {
            _companyService = companyService;
            _SysUserManagementService = SysUserManagementService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] != null)
                {
                    Guid id = (Guid)Session["UserID"];
                    ModulePrivilege Mprivilege = _SysUserManagementService.GetPrivilegeByUserid(id);
                    if (!(bool)Mprivilege.AddPrivilege)
                    {
                        btn_Add.Enabled = false;
                    }
                }
            }

        }
        /// <summary>
        /// 新建公司账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_AddCompany_Click(object sender, EventArgs e)
        {
            bool ok = true;
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
            else if (Txt_PostCode.Text.Trim()=="")
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
            else if (Txt_Phone.Text.Trim() != ""&&!RTel.IsMatch(Txt_Phone.Text.Trim()))
            {
                
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('公司电话只能输入数字！')</script>");
                ok = false;
                Txt_Phone.Focus();
            }
            else if (!RTel.IsMatch(Txt_ContactorPhone.Text.Trim()))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('联系人电话只能输入数字！')</script>");
                ok = false;
                Txt_ContactorPhone.Focus();
            }
            else
            {
                if (ok == true)
                {
                    Company company = _companyService.FindCompanyByCompanyCode(Txt_CompanyCode.Text.Trim());
                    if (company != null)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('该账号已存在，请重新输入！')</script>");
                        Txt_CompanyCode.Focus();
                    }
                    else
                    {
                        company = new Company();
                        //addbook = new AddressBook();
                        company.CID = Guid.NewGuid();
                        company.CompanyCode = Txt_CompanyCode.Text.Trim().ToUpper();
                        company.FullName = Txt_FullName.Text.Trim().ToUpper();
                        company.ShortName = Txt_ShortName.Text.Trim().ToUpper();
                        company.Contactor = Txt_Contactor.Text.Trim().ToUpper();
                        company.ContactorPhone = Txt_ContactorPhone.Text.Trim();
                        company.Phone = Txt_Phone.Text.Trim();
                        company.Fax = Txt_Fax.Text.Trim();
                        company.Address = Txt_Address.Text.Trim();
                        company.PostCode = Txt_PostCode.Text.Trim();
                        company.OrganizationCode = Txt_OrganizationCode.Text.Trim();
                        company.Remark = Txt_Remark.Text.Trim();
                        company.Status = 1;
                        _companyService.AddCompany(company);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('注册成功！')</script>");
                        InitialControl(this.Controls);
                    }
                }
            }
        }
        /// <summary>
        /// 返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../Navigation.aspx");

        }
        /// <summary>
        /// 清空页面输入控件值
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
        }
        /// <summary>
        /// 判断账号是否已经存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Txt_CompanyCode_TextChanged(object sender, EventArgs e)
        {
            Company company = _companyService.FindCompanyByCompanyCode(Txt_CompanyCode.Text.Trim());
            if (company != null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('该账号已存在，请重新输入！')</script>");
                Txt_CompanyCode.Focus();
            }
            else
            {
                Txt_FullName.Focus();
            }
        }
    }
}