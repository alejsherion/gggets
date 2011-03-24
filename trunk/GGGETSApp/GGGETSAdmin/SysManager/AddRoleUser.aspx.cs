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
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace GGGETSAdmin.SysManager
{
    public partial class AddRoleUser : System.Web.UI.Page
    {
        #region 构造函数以及字段
        private readonly ISysUserManagementService _sysUserManagementService;//用户IBLL
        private readonly IRoleManagementService _roleManagementService;//角色IBLL
        protected AddRoleUser()
        {
            
        }
        public AddRoleUser(ISysUserManagementService sysUserManagementService
                             ,IRoleManagementService roleManagementService)
        {
            _sysUserManagementService = sysUserManagementService;
            _roleManagementService = roleManagementService;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //Session.Remove("lvwRoleUserSource");
                //Session.Remove("lvwUserSource");
                var roleId = Request["Id"];
                if (String.IsNullOrEmpty(roleId))
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                       , "alert('传入参数不能为空!')", true);
                    return;
                }
                ViewState["OpStatus"] = "UpDate";
                try
                {
                    var roleGuid = new Guid(roleId);
                    ViewState["Id"] = roleGuid;
                    BindlvwUser(roleGuid);
                }
                catch
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                            , "alert('加载数据失败!')", true);
                }
                BindlvwRoleUser();

            }
        }

        #region 事件
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindlvwRoleUser();
        }

        /// <summary>
        /// 用户命令行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvwUser_ItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            if (e.CommandName != "Select") return;
            var dataSource = (IList<SysUser>)Session["lvwUserSource"];
            if (dataSource == null || dataSource.Count == 0) return;

            var code = Convert.ToString(e.CommandArgument);
            if (String.IsNullOrEmpty(code)) return;

            try
            {
                var guidCode = new Guid(code);
                var item = dataSource.Where(it => it.UID == guidCode).SingleOrDefault();
                dataSource = dataSource.Where(it => it.UID != guidCode).ToList();
                SelectlvwUserItem(dataSource, item);

            }
            catch
            {

                Page.ClientScript.RegisterStartupScript(GetType(), ""
                                              , "alert('加载数据失败!')", true);
            }
        }

        

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnsave_Click(object sender, EventArgs e)
        {
           
            try
            {
                var guidRole=(Guid)ViewState["Id"];
                var role = _roleManagementService.GetRoleByRoleId(guidRole);
            //    var opStatus = Convert.ToString(ViewState["OpStatus"]);
                
            //    if (opStatus == "Add")
            //    {
            //        role.RoleID = Guid.NewGuid();
            //        GetDataFromPage(role);
            //        role.CreateTime = DateTime.Now;
            //        _roleManagementService.Add(role);
                    
            //    }
            //    else
            //    {
                     role.RoleID = (Guid)ViewState["Id"];
                     GetDataFromPage(role);
                     role.LastUpdateTime = DateTime.Now;
                     _roleManagementService.Modify(role);
                //}
                Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                     , "alert('操作成功!')", true);
               

            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                     , "alert('操作失败!')", true);
            }
            Bind();
        }
        #endregion

        #region 公有方法
        #endregion

        #region 私有方法
        /// <summary>
        /// 绑定
        /// </summary>
        private void BindlvwRoleUser()
        {
            var email = txtEmail.Text.Trim();
            var tel = txtTel.Text.Trim();
            var loginName = txtLoginName.Text.Trim();
            DateTime? startCreateTime = null;
            if (!String.IsNullOrEmpty(txtStartCreateTime.Text))
                startCreateTime = DateTime.Parse(txtStartCreateTime.Text);
            DateTime? endCreateTime = null;
            if (!String.IsNullOrEmpty(txtEndCreateTime.Text))
                endCreateTime = DateTime.Parse(txtEndCreateTime.Text);
            var dataSource = _sysUserManagementService.QueryByCondtion(email, tel, loginName, startCreateTime, endCreateTime);
            var lvwUserSource = (IList<SysUser>) Session["lvwUserSource"];
            if (lvwUserSource != null && lvwUserSource.Count>0)
            {
                foreach(var item in lvwUserSource)
                {
                    var item1 = item;
                    dataSource = dataSource.Where(it => it.UID != item1.UID).ToList();
                }
                lvwUser.DataSource = lvwUserSource;
                lvwUser.DataBind();
            }
            lvwRoleUser.DataSource = dataSource;
            lvwRoleUser.DataBind();
            Session["lvwRoleUserSource"] = dataSource;
            

        }

        /// <summary>
        /// 绑定已选择用户
        /// </summary>
        /// <param name="roleId">角色Id</param>
        private void BindlvwUser(Guid roleId)
        {
            var dataSource = _roleManagementService.GetUserByRoleId(roleId);
            lvwUser.DataSource = dataSource;
            lvwUser.DataBind();
            Session["lvwUserSource"] = dataSource;
        }

        /// <summary>
        /// 选择角色用户
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="user"></param>
        private void SelectlvwRoleUserItem(IList<SysUser> dataSource,SysUser user)
        {
            if(user==null||dataSource==null) return;
            lvwRoleUser.DataSource = dataSource;
            lvwRoleUser.DataBind();
            var otehrSource = (IList<SysUser>)Session["lvwUserSource"] ?? new List<SysUser>();
            var array = otehrSource.Where(it => it.UID == user.UID);
            if (array != null && array.Count() > 0) return;
            otehrSource.Add(user);
            Session["lvwUserSource"] = otehrSource;
            Session["lvwRoleUserSource"] = dataSource;
            lvwUser.DataSource = otehrSource;
            lvwUser.DataBind();
        }


        /// <summary>
        /// 选择用户
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="user"></param>
        private void SelectlvwUserItem(IList<SysUser> dataSource, SysUser user)
        {
            if (user == null || dataSource == null) return;
            lvwUser.DataSource = dataSource;
            lvwUser.DataBind();
            var otehrSource = (IList<SysUser>)Session["lvwRoleUserSource"] ?? new List<SysUser>();
            var array = otehrSource.Where(it => it.UID == user.UID);
            if (array != null && array.Count() > 0) return;
            otehrSource.Add(user);
            lvwRoleUser.DataSource = otehrSource;
            lvwRoleUser.DataBind();
            Session["lvwUserSource"] = dataSource;
            Session["lvwRoleUserSource"] = otehrSource;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        private void GetDataFromPage(Role role)
        {
            if (role == null) return;
            var dataSouce = (IList<SysUser>) Session["lvwUserSource"];
            role.SysUser_Role.Clear();
            if (dataSouce == null || dataSouce.Count == 0)
            {
                return;
            }
            foreach (var item in dataSouce)
            {
                var rp = new SysUser_Role
                             {
                                 SysUser_RoleID = Guid.NewGuid(),
                                 RoleID = role.RoleID,
                                 UID = item.UID
                             };
                role.SysUser_Role.Add(rp);
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void Bind()
        {
            var dataSource = (IList<SysUser>)Session["lvwRoleUserSource"];
            lvwRoleUser.DataSource = dataSource;
            lvwRoleUser.DataBind();
            dataSource = (IList<SysUser>)Session["lvwUserSource"];
            lvwUser.DataSource = dataSource;
            lvwUser.DataBind();
        }
        #endregion

        protected void lvwRoleUser_ItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                var dataSource = (IList<SysUser>)Session["lvwRoleUserSource"];
                if (dataSource == null || dataSource.Count == 0) return;

                var code = Convert.ToString(e.CommandArgument);
                if (String.IsNullOrEmpty(code)) return;
                try
                {
                    var guidCode = new Guid(code);
                    var item = dataSource.Where(it => it.UID == guidCode).SingleOrDefault();
                    dataSource = dataSource.Where(it => it.UID != guidCode).ToList();
                    SelectlvwRoleUserItem(dataSource, item);

                }
                catch
                {

                    Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                  , "alert('加载数据失败!')", true);
                }
            }
            else
            {
                BindlvwRoleUser();
            }
        }

        

    }
}