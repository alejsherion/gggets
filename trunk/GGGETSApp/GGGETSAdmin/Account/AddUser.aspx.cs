//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        添加用户
// 作成者				hong.li
// 改版日				2011.02.25
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using GGGETSAdmin.Common;

namespace GGGETSAdmin.Account
{
    public partial class AddUser : System.Web.UI.Page
    {
        #region 构造函数以及字段
        private readonly ISysUserManagementService _sysUserManagementService;
        private readonly IRoleManagementService _roleManagementService;
        protected AddUser()
        {
            
        }
        public AddUser(ISysUserManagementService sysUserManagementService
                       , IRoleManagementService roleManagementService)
        {
            _sysUserManagementService = sysUserManagementService;
            _roleManagementService = roleManagementService;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindRoleId();
                BindStatus();
                var id = Request["Id"];
                //var id = "ab6f8a54-be77-4bcf-a5c2-ee711c04f65b";
                if (String.IsNullOrEmpty(id))
                {
                    ViewState["OpStatus"] = "Add";
                    var user = new SysUser();
                    DataBindHelper<SysUser>.Bind(form1, user);
                    Session["SysUser"] = user;
                    trPassword.Visible = true;
                    Session.Remove("Condtion");
                }
                else
                {
                    ViewState["OpStatus"] = "Update";
                    var guidRoleId = new Guid(id);
                    BindControl(guidRoleId);
                    trPassword.Visible = false;
                }
            }
        }

        #region 事件

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_login_Click(object sender, EventArgs e)
        {

            var opStatus = Convert.ToString(ViewState["OpStatus"]);
            var user = (SysUser)Session["SysUser"];
            if (user == null)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                  , "alert('操作失败!')", true);
                return;
            }
            try
            {
                DataBindHelper<SysUser>.GetDaTa(form1, user);
            }
            catch (Exception ce)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), ""
                                               , "alert('" + ce.Message + "!')", true);
                return;
            }
            try
            {
                if (opStatus == "Add")
                {
                    var result = Validation();
                    if (!result) return;
                    user.UID = Guid.NewGuid();
                    user.CreateTime = DateTime.Now;
                    user.UpdateTime = DateTime.Now;
                    user.Operator = "Admin";
                    GetRoles(user);
                    _sysUserManagementService.Add(user);
                }
                else
                {
                    GetRoles(user);
                    user.UpdateTime = DateTime.Now;
                    user.Operator = "Admin";
                    _sysUserManagementService.Modify(user);
                }
                Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                      , "alert('操作成功!')", true);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                        , "alert('操作失败!')", true);
            }
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserManager.aspx");
        }
        #endregion

        #region 公有方法
        #endregion

        #region 私有方法
        /// <summary>
        /// 绑定角色
        /// </summary>
        private void BindRoleId()
        {
            var dataSouce = _roleManagementService.GetAllRole();
            chkRoleID.DataSource = dataSouce;
            chkRoleID.DataTextField = "Name";
            chkRoleID.DataValueField = "RoleID";
            chkRoleID.DataBind();
        }

        /// <summary>
        /// 绑定会员
        /// </summary>
        private void BindStatus()
        {
            var dataSouce = DataConversion.ListTypeForEnum(typeof(Status));
            ddlStatus.DataSource = dataSouce;
            ddlStatus.DataTextField = "text";
            ddlStatus.DataValueField = "value";
            ddlStatus.DataBind();
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="user"></param>
        private void GetRoles(SysUser user)
        {
            if (user == null) return;
            var items = chkRoleID.Items;
            user.SysUser_Role.Clear();
            foreach (ListItem item in items)
            {
                if (!item.Selected) continue;
                var rp = new SysUser_Role
                {
                    SysUser_RoleID = Guid.NewGuid(),
                    RoleID =  new Guid(item.Value),
                    UID = user.UID,
                    LastUpdateTime = DateTime.Now
                };
                user.SysUser_Role.Add(rp);
            }
        }

        /// <summary>
        /// 绑定页面数据
        /// </summary>
        /// <param name="id"></param>
        private void BindControl(Guid id)
        {
            if (_roleManagementService == null) return;
            try
            {
                var user = _sysUserManagementService.GetUserById(id);
                DataBindHelper<SysUser>.Bind(form1, user);
                BindRoleIdFormModel(user);
                txtPassword.Attributes["value"] = user.Password;
                Session["SysUser"] = user;
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                    , "alert('加载数据失败!')", true);
            }

        }

        /// <summary>
        /// 读取数据绑定权限
        /// </summary>
        /// <param name="user">用户实体</param>
        private void BindRoleIdFormModel(SysUser user)
        {
            if (user == null) return;
            chkRoleID.ClearSelection();
            var items = chkRoleID.Items;
            if (items == null || items.Count == 0) return;
            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var value = new Guid(item.Value);
                var temprp = user.SysUser_Role.Where(it => it.RoleID == value);
                if (temprp != null && temprp.Count() > 0)
                {
                    item.Selected = true;
                }
            }
        }

        /// <summary>
        /// 页面数据验证
        /// </summary>
        /// <returns></returns>
        private bool Validation()
        {
            var result = true;
            var opStatus = Convert.ToString(ViewState["OpStatus"]);
             if (opStatus == "Add")
              {
                  var cnfirmPassword = txtConfirmPwd.Text.Trim();
                 if(String.IsNullOrEmpty(cnfirmPassword))
                 {
                     Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                       , "alert('请填写确认密码!')", true);
                     result = false;
                 }
              }
             return result;

        }
        #endregion

     

        
    }
}