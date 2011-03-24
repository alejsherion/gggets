//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        添加角色用户
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

namespace GGGETSAdmin.SysManager
{
    public partial class AddRole : System.Web.UI.Page
    {
        #region 构造函数以及字段
        private readonly IRoleManagementService _roleManagementService;//角色IBLL
        protected AddRole()
        {
            
        }
        public AddRole(IRoleManagementService roleManagementService)
        {
            _roleManagementService = roleManagementService;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                var id = Request["Id"];
                try
                {
                   
                    BindStatus();
                    if(String.IsNullOrEmpty(id))
                    {
                        hfId.Value = "";
                        ViewState["OpStatues"] = "Add";
                        var role = new Role();
                        DataBindHelper<Role>.Bind(form1, role);
                        Session["Role"] = role;
                        btnPrivilege.Visible = false;
                        btnUser.Visible = false;
                        Session.Remove("RoleCondtion");
                    }
                    else
                    {
                        hfId.Value = id;
                        ViewState["OpStatues"] = "Update";
                        var guidRoleId = new Guid(id);
                        ViewState["guidRoleId"] = id;
                        BindControl(guidRoleId);
                        btnPrivilege.Visible = true;
                        btnUser.Visible = true;
                    }
                   
                   
                }
                catch
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                     , "alert('加载数据失败!')", true);
                }
                
            }
        }

        #region 事件
        /// <summary>
        ///  设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_login_Click(object sender, EventArgs e)
        {
            var opStatus = Convert.ToString(ViewState["OpStatues"]);
            var role = (Role) Session["Role"];
            if (role==null)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                  , "alert('操作失败!')", true);
                return;
            }
            try
            {
                DataBindHelper<Role>.GetDaTa(form1, role);
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
                    role.RoleID = Guid.NewGuid();
                    role.CreateTime = DateTime.Now;
                    _roleManagementService.Add(role);
                }
                else
                {
                    role.LastUpdateTime = DateTime.Now;
                    _roleManagementService.Modify(role);
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
        /// 添加用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUser_Click(object sender, EventArgs e)
        {
            //var url = "AddRoleUser.aspx?Id=" + Convert.ToString(ViewState["guidRoleId"]);
            //Response.Redirect(url);
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrivilege_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("RoleManagement.aspx");
        }
        #endregion

        #region 公用方法
        #endregion

        #region 私有方法
        /// <summary>
        /// 绑定状态
        /// </summary>
        private void BindStatus()
        {
            var dataSouce = DataConversion.ListTypeForEnum(typeof(RoleStatus));
            rbtnStatus.DataSource = dataSouce;
            rbtnStatus.DataTextField = "text";
            rbtnStatus.DataValueField = "value";
            rbtnStatus.DataBind();
            rbtnStatus.SelectedIndex = 1;
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
                var role = _roleManagementService.GetRoleByRoleId(id);
                DataBindHelper<Role>.Bind(form1, role);
                Session["Role"] = role;
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                    , "alert('加载数据失败!')", true);
            }
         
        }
        #endregion

       

      
    }
}