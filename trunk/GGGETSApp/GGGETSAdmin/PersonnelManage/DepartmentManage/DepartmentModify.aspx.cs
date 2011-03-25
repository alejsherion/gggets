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
    public partial class DepartmentModify : System.Web.UI.Page
    {
        private IDepartmentManagementService _deparService;
        private Department depar;
        private static Regex Rnubel = new Regex(@"^([1])$|^([1].[0]{2})$|^([0].[1-9][0-9])?$");
        protected DepartmentModify()
        { }
        public DepartmentModify(IDepartmentManagementService deparservice)
        {
            _deparService = deparservice;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["DeparCode"]) && !string.IsNullOrEmpty(Request.QueryString["CompanyCode"]))
                {
                    DropDownList();
                    string CompanyCode = Request.QueryString["CompanyCode"].ToString();
                    string DeparCode = Request.QueryString["DeparCode"].ToString();
                    Storage(DeparCode, CompanyCode);
                    ViewState["Url"] = Request.UrlReferrer.ToString();

                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('没有相关记录！');location='CompanyManagemnet.aspx'</script>");

                }
            }
        }
        /// <summary>
        /// 页面DropDownList控件绑定
        /// </summary>
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
        /// 页面控件赋值
        /// </summary>
        /// <param name="deparcode">部门账号</param>
        /// <param name="CompanyCode">公司账号</param>
        protected void Storage(string deparcode, string CompanyCode)
        {
            depar = _deparService.FindDepartmentByDepCodeAndCompanyCode(deparcode, CompanyCode);
            if (depar != null)
            {
                Txt_CompanyCode.Text = depar.CompanyCode;
                Txt_DepCode.Text = depar.DepCode;
                Txt_DepName.Text = depar.DepName;
                ddl_FeeDiscountType.SelectedValue = depar.FeeDiscountType.ToString();
                Txt_FeeDiscountRate.Text = depar.FeeDiscountRate.ToString();
                ddl_WeightDiscountType.SelectedValue = depar.WeightDiscountType.ToString();
                Txt_WeightDiscountRate.Text = depar.WeightDiscountRate.ToString();
                ddl_SettleType.SelectedValue = depar.SettleType.ToString();
                ddl_WeightCalType.SelectedValue = depar.WeightCalType.ToString();
                Session["Department"] = depar;
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_UpCompany_Click(object sender, EventArgs e)
        {
            if (depar == null)
            {
                if (Session["Department"] != null)
                {
                    depar = (Department)Session["Department"];
                }
                else
                {
                    depar = _deparService.FindDepartmentByDepCodeAndCompanyCode(Txt_DepCode.Text, Txt_CompanyCode.Text);
                }
            }
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
                if (!Rnubel.IsMatch(Txt_FeeDiscountRate.Text.Trim()))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入0.10-1的数字！')</script>");
                    Txt_FeeDiscountRate.Focus();
                }
                else if (!Rnubel.IsMatch(Txt_WeightDiscountRate.Text.Trim()))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入0.10-1的数字！')</script>");
                    Txt_WeightDiscountRate.Focus();
                }
                else
                {
                    depar.DepName = Txt_DepName.Text.Trim().ToUpper();
                    depar.FeeDiscountType = int.Parse(ddl_FeeDiscountType.SelectedValue);
                    depar.FeeDiscountRate = decimal.Parse(Txt_FeeDiscountRate.Text.Trim());
                    depar.WeightDiscountType = int.Parse(ddl_WeightDiscountType.SelectedValue);
                    depar.WeightDiscountRate = decimal.Parse(Txt_WeightDiscountRate.Text.Trim());
                    depar.WeightCalType = int.Parse(ddl_WeightCalType.SelectedValue);
                    depar.SettleType = int.Parse(ddl_SettleType.SelectedValue);
                    _deparService.ModifyDepartment(depar);
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('修改成功！');location='DepartmentManagemnet.aspx'</script>");
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
            Response.Redirect((string)ViewState["Url"]);
        }
        /// <summary>
        /// 部门账号验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Txt_DepName_TextChanged(object sender, EventArgs e)
        {
            bool tabindex = true;
            IList<Department> ldepar = _deparService.FindDepartmentsByCompanyCode(Txt_CompanyCode.Text.Trim());
            foreach (Department depar in ldepar)
            {
                if (depar.DepName == Txt_DepName.Text.Trim())
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('该部门名称已存在，请重新输入！')</script>");
                    Txt_DepName.Focus();
                    tabindex = false;
                    break;
                }

            }
            if (tabindex)
            {
                ddl_FeeDiscountType.Focus();
            }
        }
    }
}