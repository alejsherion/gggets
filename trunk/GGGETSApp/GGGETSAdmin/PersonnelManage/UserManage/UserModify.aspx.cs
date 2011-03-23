using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;

namespace GGGETSAdmin.PersonnelManage.UserManage
{
    public partial class UserModify : System.Web.UI.Page
    {
        private IUserManagementService _userService;
        private IDepartmentManagementService _deparService;
        private ICompanyManagementService _companyService;
        private ETS.GGGETSApp.Domain.Application.Entities.User user;
        private static Regex Rnubel = new Regex(@"^[0]{1}\.?[0-9]{0,2}|[1-9]+\.?[0-9]{0,2}$");
        private static Regex REmail = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        protected UserModify()
        { }
        public UserModify(IUserManagementService userservice, IDepartmentManagementService deparservice, ICompanyManagementService companyservice)
        {
            _userService = userservice;
            _deparService = deparservice;
            _companyService = companyservice;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["LoginName"]))
                {
                    DropDownList();
                    string LoginName = Request.QueryString["LoginName"].ToString();
                    Storage(LoginName);
                    ViewState["Url"] = Request.UrlReferrer.ToString();

                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('没有相关记录！');location='UserManagemnet.aspx'</script>");

                }
            }
        }
        /// <summary>
        /// 页面DorpDownList控件绑定
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
        /// <param name="loginname">登录名</param>
        protected void Storage(string loginname)
        {
            user = _userService.FindUserByLoginName(loginname);
            if (user != null)
            {
                if (user.DID != null)
                {
                    Department depar = _deparService.FindDepartmentByDID(user.DID.ToString());
                    if (depar != null)
                    {
                        Txt_CompanyCode.Text = depar.CompanyCode;
                        Txt_DepCode.Text = depar.DepCode;
                    }
                }
                Txt_LoginName.Text = user.LoginName;
                Txt_Phone.Text = user.Phone;
                Txt_RealName.Text = user.RealName;
                Txt_Email.Text = user.Email;
                ddl_FeeDiscountType.SelectedValue = user.FeeDiscountType.ToString();
                Txt_FeeDiscountRate.Text = user.FeeDiscountRate.ToString();
                ddl_WeightDiscountType.SelectedValue = user.WeightDiscountType.ToString();
                Txt_WeightDiscountRate.Text = user.WeightDiscountRate.ToString();
                ddl_SettleType.SelectedValue = user.SettleType.ToString();
                ddl_WeightCalType.SelectedValue = user.WeightCalType.ToString();
                Txt_Remark.Text = user.Remark;
                DDL_Status.SelectedValue = user.Status.ToString();
                Session["User"] = user;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_UpCompany_Click(object sender, EventArgs e)
        {
            if (user == null)
            {
                if (Session["User"] != null)
                {
                    user = (ETS.GGGETSApp.Domain.Application.Entities.User)Session["User"];
                }
                else
                {
                    user = _userService.FindUserByLoginName(Request.QueryString["LoginName"].ToString());
                }
            }
            if (Txt_LoginName.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('用户名不能为空！')</script>");
                Txt_LoginName.Focus();
            }
            else if (Txt_RealName.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('真实姓名不能为空！')</script>");
                Txt_RealName.Focus();
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
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入整数或小数，且小数点保留2位！')</script>");
                    Txt_FeeDiscountRate.Focus();
                }
                else if (!Rnubel.IsMatch(Txt_WeightDiscountRate.Text.Trim()))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入整数或小数，且小数点保留2位！')</script>");
                    Txt_WeightDiscountRate.Focus();
                }
                else if (Txt_Email.Text.Trim() != "" && !REmail.IsMatch(Txt_Email.Text.Trim()))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('邮箱格式不正确!')</script>");
                    Txt_Email.Focus();
                }
                else
                {
                    if (Txt_CompanyCode.Text.Trim() != "" && Txt_DepCode.Text.Trim() != "")
                    {
                        Department depar = _deparService.FindDepartmentByDepCodeAndCompanyCode(Txt_DepCode.Text.Trim(), Txt_CompanyCode.Text.Trim());
                        user.DID = depar.DID;
                    }
                    user.LoginName = Txt_LoginName.Text.Trim();
                    user.RealName = Txt_RealName.Text.Trim();
                    user.Phone = Txt_Phone.Text.Trim();
                    user.Email = Txt_Email.Text.Trim();
                    user.UpdateTime = DateTime.Now;
                    user.Remark = Txt_Remark.Text.Trim();
                    user.FeeDiscountType = int.Parse(ddl_FeeDiscountType.SelectedValue);
                    user.FeeDiscountRate = decimal.Parse(Txt_FeeDiscountRate.Text.Trim());
                    user.WeightDiscountType = int.Parse(ddl_WeightDiscountType.SelectedValue);
                    user.WeightDiscountRate = decimal.Parse(Txt_WeightDiscountRate.Text.Trim());
                    user.SettleType = int.Parse(ddl_SettleType.SelectedValue);
                    user.WeightCalType = int.Parse(ddl_WeightCalType.SelectedValue);
                    user.Status = int.Parse(DDL_Status.SelectedValue);
                    _userService.ModifyUser(user);
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('修改成功！');location='UserManagemnet.aspx'</script>");
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
        /// 公司账号验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Txt_CompanyCode_TextChanged(object sender, EventArgs e)
        {
            Company company = _companyService.FindCompanyByCompanyCode(Txt_CompanyCode.Text.Trim());//根据公司账号获取信息
            if (company == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('没有该公司账号，请重新输入！')</script>");
                Txt_CompanyCode.Focus();
                Txt_CompanyCode.Text = "";
            }
            else
            {
                Txt_DepCode.Focus();
            }
        }
        /// <summary>
        /// 部门账号验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Txt_DepCode_TextChanged(object sender, EventArgs e)
        {
            if (Txt_CompanyCode.Text.Trim() != "")
            {
                IList<Department> ldepar = _deparService.FindDepartmentsByCompanyCode(Txt_CompanyCode.Text.Trim());//根据部门账号获取信息
                bool Ok = false;
                if (ldepar != null)
                {
                    foreach (Department depar in ldepar)
                    {
                        if (depar.DepCode == Txt_DepCode.Text.Trim())
                        {
                            Ok = true;
                            ViewState["Did"] = depar.DID;
                            ddl_FeeDiscountType.SelectedValue = depar.FeeDiscountType.ToString();
                            ddl_WeightDiscountType.SelectedValue = depar.WeightDiscountType.ToString();
                            ddl_WeightCalType.SelectedValue = depar.WeightCalType.ToString();
                            ddl_SettleType.SelectedValue = depar.SettleType.ToString();
                            break;
                        }
                    }
                }
                if (!Ok)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('没有该部门账号！')</script>");
                    Txt_DepCode.Focus();
                    Txt_DepCode.Text = "";
                }
                else
                {
                    Txt_LoginName.Focus();
                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请先输入公司账号！')</script>");
                Txt_CompanyCode.Focus();
            }
        }
        /// <summary>
        /// 登录名验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Txt_LoginName_TextChanged(object sender, EventArgs e)
        {
            if (Txt_LoginName.Text.Trim() != "")
            {
                ETS.GGGETSApp.Domain.Application.Entities.User user = _userService.FindUserByLoginName(Txt_LoginName.Text.Trim());//根据登录名获取信息
                if (user != null)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('用户名已存在请重新输入！')</script>");
                    Txt_LoginName.Text = "";
                    Txt_LoginName.Focus();
                }
                else
                {
                    Txt_Email.Focus();
                }
            }
        }
    }
}