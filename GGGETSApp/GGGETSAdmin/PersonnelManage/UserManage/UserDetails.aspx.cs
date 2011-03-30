using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;

namespace GGGETSAdmin.PersonnelManage.UserManage
{
    public partial class UserDetails : System.Web.UI.Page
    {
        private IDepartmentManagementService _deparService;
        private IUserManagementService _userService;
        private ISysUserManagementService _SysUserManagementService;
        protected UserDetails()
        { }
        public UserDetails(IDepartmentManagementService deparService, IUserManagementService userService, ISysUserManagementService SysUserManagementService)
        {
            _deparService = deparService;
            _userService = userService;
            _SysUserManagementService = SysUserManagementService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["LoginName"]))
                {
                    if (Session["UserID"] != null)
                    {
                        Guid id = (Guid)Session["UserID"];
                        ModulePrivilege Mprivilege = _SysUserManagementService.GetPrivilegeByUserid(id);
                        if (!(bool)Mprivilege[Privilege.修改.ToString()])
                        {
                            But_Update.Enabled = false;
                        }

                    }
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
        /// 页面控件赋值
        /// </summary>
        /// <param name="loginname">个人用户登录名</param>
        protected void Storage(string loginname)
        {
            ETS.GGGETSApp.Domain.Application.Entities.User user = _userService.FindUserByLoginName(loginname);//根据用户名获取信息
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
                else
                {
                    tdCompanyCode.Visible = false;
                    tdDepCode.Visible = false;
                }
                Txt_LoginName.Text = user.LoginName;
                Txt_Phone.Text = user.Phone;
                Txt_RealName.Text = user.RealName;
                Txt_Email.Text = user.Email;
                if (user.FeeDiscountType == 0)
                {
                    Txt_FeeDiscountType.Text = "灵活折扣";
                }
                else
                {
                    Txt_FeeDiscountType.Text = "固定折扣";
                }
                Txt_FeeDiscountRate.Text = user.FeeDiscountRate.ToString();
                if (user.WeightDiscountType == 0)
                {
                    Txt_WeightDiscountType.Text = "灵活折扣";
                }
                else
                {
                    Txt_WeightDiscountType.Text = "固定折扣";
                }
                Txt_WeightDiscountRate.Text = user.WeightDiscountRate.ToString();
                switch (user.SettleType)
                {
                    case 0:
                        Txt_SettleType.Text = "预付月结";
                        break;
                    case 1:
                        Txt_SettleType.Text = "预付现结";
                        break;
                    case 2:
                        Txt_SettleType.Text = "到付月结";
                        break;
                    case 3:
                        Txt_SettleType.Text = "到付现结";
                        break;
                }
                if (user.WeightCalType == 0)
                {
                    Txt_WeightCalType.Text = "按照0.5KG标准";
                }
                else
                {
                    Txt_WeightCalType.Text = "按照分段标准";
                }
                Txt_Remark.Text = user.Remark;
                if (user.Status == 0)
                {
                    Txt_Status.Text = "可用";
                }
                else
                {
                    Txt_Status.Text = "不可用";
                }
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void But_Next_Click(object sender, EventArgs e)
        {

            Response.Redirect("UserModify.aspx?LoginName=" + Txt_LoginName.Text.Trim() + "");
        }
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void But_Conel_Click(object sender, EventArgs e)
        {
            Response.Redirect((string)ViewState["Url"]);
        }
    }
}