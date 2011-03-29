//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        运单用户DAL
// 作成者				hong.li
// 改版日				2011.02.12
// 改版内容				创建并且修改
//************************************************************************
using System;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using Resources;

namespace GGGETSAdmin.Login
{
    /// <summary>
    /// 登录页面
    /// </summary>
    public partial class Login : System.Web.UI.Page
    {
        #region 构造函数以及字段

        private readonly ISysUserManagementService _sysUserManagementService;
        

        /// <summary>
        /// 构造函数
        /// </summary>
        protected Login()
        {
            
        }

        public Login(ISysUserManagementService sysUserManagementService)
        {
           
            _sysUserManagementService = sysUserManagementService;
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //RadCaptcha1.CaptchaImage.RenderImageOnly = true;
                //RadCaptcha1.ValidatedTextBoxID = "txtRadCaptcha";
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnLoginClick(object sender, EventArgs e)
        {
            if(_sysUserManagementService!=null)
            {
                var password = txtpwd.Text.Trim();
                var lgoinName = txtuser.Text.Trim();
                var result = _sysUserManagementService.IsLgoin(lgoinName, password);
                if (result == SysUser.EmptyUid) labError.Text = Resource1.LoginError;
                else if (result == null) labError.Text = Resource1.SysError;
                else
                {
                    labError.Text = "";
                    Session["UserID"] = result;
                    Response.Redirect("../Navigation.aspx");
                    //Response.Redirect("../HOME.aspx");
                }
               
            }
        }
    }
}