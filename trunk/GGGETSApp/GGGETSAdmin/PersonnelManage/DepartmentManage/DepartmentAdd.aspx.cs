using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;

namespace GGGETSAdmin.PersonnelManage.DepartmentManage
{
    public partial class DepartmentAdd : System.Web.UI.Page
    {
        private ICompanyManagementService _companyService;
        private IDepartmentManagementService _deparService;
        private static Regex Rnubel = new Regex(@"^([1])$|^([1].[0]{2})$|^([0].[1-9][0-9])$|^([0].[0-9][1-9])$");
        protected DepartmentAdd()
        { }
        public DepartmentAdd(ICompanyManagementService companyService, IDepartmentManagementService deparservice)
        {
            _companyService = companyService;
            _deparService = deparservice;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownList();
            }
        }
        private void DropDownList()
        {
            EnumType enumtype = new EnumType();
            ddl_WeightCalType.DataSource = enumtype.GetName("WeightCalType");
            ddl_WeightCalType.DataTextField = "Text";
            ddl_WeightCalType.DataValueField = "Value";
            ddl_WeightCalType.DataBind();

            ddl_SettleType.DataSource = enumtype.GetName("SettleType");
            ddl_SettleType.DataTextField = "Text";
            ddl_SettleType.DataValueField = "Value";
            ddl_SettleType.DataBind();

            ddl_WeightDiscountType.DataSource = enumtype.GetName("DiscountType");
            ddl_WeightDiscountType.DataTextField = "Text";
            ddl_WeightDiscountType.DataValueField = "Value";
            ddl_WeightDiscountType.DataBind();

            ddl_FeeDiscountType.DataSource = enumtype.GetName("DiscountType");
            ddl_FeeDiscountType.DataTextField = "Text";
            ddl_FeeDiscountType.DataValueField = "Value";
            ddl_FeeDiscountType.DataBind();
        }
        /// <summary>
        /// 验证公司账号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Txt_CompanyCode_TextChanged(object sender, EventArgs e)
        {
            Company company = _companyService.FindCompanyByCompanyCode(Txt_CompanyCode.Text.Trim());
            if (company == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('没有该公司账号，请重新输入！')</script>");
                Txt_CompanyCode.Focus();
                Txt_CompanyCode.Text = "";
            }
            else
            {
                Session["Compar"] = company;
                Txt_DepCode.Focus();
            }
        }

        
        /// <summary>
        /// 验证部门账号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Txt_DepCode_TextChanged(object sender, EventArgs e)
        {
            bool tabIndex = true;
            IList<Department> ldepar = _deparService.FindDepartmentsByCompanyCode(Txt_CompanyCode.Text.Trim());
            if (ldepar != null)
            {
                foreach (Department depar in ldepar)
                {
                    if (depar.DepCode == Txt_DepCode.Text.Trim())
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('部门账号已存在，请重新输入！')</script>");
                        Txt_DepCode.Focus();
                        Txt_DepCode.Text = "";
                        tabIndex = false;
                        break;
                    }
                }
                Session["Department"] = ldepar;
            }
            if (tabIndex)
            {
                Txt_DepName.Focus();
            }
            
        }
        /// <summary>
        /// 验证该部门是否已经存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Txt_DepName_TextChanged(object sender, EventArgs e)
        {
            bool tabindex = true;
            IList<Department> ldepar;
            if (Session["Department"] != null)
            {
                ldepar = (IList<Department>)Session["Department"];
            }
            else
            {
                ldepar = _deparService.FindDepartmentsByCompanyCode(Txt_CompanyCode.Text.Trim().ToUpper());
            }
            foreach (Department depar in ldepar)
            {
                if (depar.DepName == Txt_DepName.Text.Trim())
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('该部门名称已存在，请重新输入！')</script>");
                    Txt_DepName.Focus();
                    Txt_DepName.Text = "";
                    tabindex = false;
                    break;
                }
                
            }
            if (tabindex)
            {
                ddl_FeeDiscountType.Focus();
            }
            
        }
        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_AddCompany_Click(object sender, EventArgs e)
        {
            Department depar = new Department();
            if (Txt_CompanyCode.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('公司账号不能为空！')</script>");
                Txt_CompanyCode.Focus();
            }
            else if (Txt_DepCode.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('部门账号不能为空！')</script>");
                Txt_DepCode.Focus();
            }
            else if (Txt_FeeDiscountRate.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('费用折扣率不能为空！')</script>");
                Txt_FeeDiscountRate.Focus();
            }
            else if (Txt_WeightDiscountRate.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('重量折扣率不能为空！')</script>");
                Txt_WeightDiscountRate.Focus();
            }
            else
            {
                if(!Rnubel.IsMatch(Txt_FeeDiscountRate.Text.Trim()))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入0.01-1的数字！')</script>");
                    Txt_FeeDiscountRate.Focus();
                }
                else if (!Rnubel.IsMatch(Txt_WeightDiscountRate.Text.Trim()))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入0.01-1的数字！')</script>");
                    Txt_WeightDiscountRate.Focus();
                }
                else
                {
                    
                    depar.DID = Guid.NewGuid();
                    Company company;
                    if (Session["Compar"] != null)
                    {
                        company = (Company)Session["Compar"];
                    }
                    else
                    {
                        company = _companyService.FindCompanyByCompanyCode(Txt_CompanyCode.Text.Trim().ToUpper());
                    }
                    IList<Department> ldepar;
                    if (Session["Department"] != null)
                    {
                        ldepar = (IList<Department>)Session["Department"];
                    }
                    else
                    {
                        ldepar = _deparService.FindDepartmentsByCompanyCode(Txt_CompanyCode.Text.Trim());
                    }
                    bool tabindex=true;
                    foreach (Department depar1 in ldepar)
                    {
                        if (depar1.DepName == Txt_DepName.Text.Trim())
                        {
                            
                            tabindex = false;
                            break;
                        }
                
                    }
                    if (company != null)
                    {
                        if (tabindex)
                        {
                            depar.CID = company.CID;
                            depar.CompanyCode = Txt_CompanyCode.Text.Trim().ToUpper();
                            depar.DepCode = Txt_DepCode.Text.Trim().ToUpper();
                            depar.DepName = Txt_DepName.Text.Trim().ToUpper();
                            depar.FeeDiscountType = int.Parse(ddl_FeeDiscountType.SelectedValue);
                            depar.FeeDiscountRate = decimal.Parse(Txt_FeeDiscountRate.Text.Trim());
                            depar.WeightDiscountType = int.Parse(ddl_WeightDiscountType.SelectedValue);
                            depar.WeightDiscountRate = decimal.Parse(Txt_WeightDiscountRate.Text.Trim());
                            depar.WeightCalType = int.Parse(ddl_WeightCalType.SelectedValue);
                            depar.SettleType = int.Parse(ddl_SettleType.SelectedValue);
                            _deparService.AddDepartment(depar);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('添加成功！')</script>");
                            InitialControl(this.Controls);
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('该部门名称已存在，请重新输入！')</script>");
                            Txt_DepName.Focus();
                            Txt_DepName.Text = "";
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('没有该公司账号，请重新输入！')</script>");
                        Txt_CompanyCode.Focus();
                    }
                }
            }
        }
        /// <summary>
        /// 页面输入框清空
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
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../Navigation.aspx");
        }
    }
}