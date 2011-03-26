//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        添加角色权限用户
// 作成者				hong.li
// 改版日				2011.02.25
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using GGGETSAdmin.Common;
using Telerik.Web.UI;

namespace GGGETSAdmin.SysManager
{
    public partial class AddRolePrivilege : Page
    {
        #region 构造函数以及字段
        private readonly IAppModuleManagementService _appModuleManagementService;//模块IBLL
        private readonly IRoleManagementService _roleManagementService;//角色IBLL
        protected AddRolePrivilege()
        {
            
        }
        public AddRolePrivilege(IAppModuleManagementService appModuleManagementService
                             ,IRoleManagementService roleManagementService)
        {
            _appModuleManagementService = appModuleManagementService;
            _roleManagementService = roleManagementService;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = Request["Id"];
                //var id = "00000000-0000-0000-0000-000000000001";
                try
                {
                    if (string.IsNullOrEmpty(id))
                        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('请传入参数!')", true);
                    else
                    {
                        var guidRoleId = new Guid(id);
                        ViewState["Id"] = guidRoleId;
                        BindAppModule();
                        BindAppModuleById(guidRoleId);
                    }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('加载数据失败!')", true);
                    //.RegisterStartupScript(this, GetType(), "", "alert('模板保存成功!')", true);
                }

            }
        }

        #region 公用方法
        #endregion

        #region 私有方法
        /// <summary>
        /// 绑定树型菜单
        /// </summary>
        private void BindAppModule()
        {
            var dataSource = _appModuleManagementService.GetAll();
            Session["AppModel"] = dataSource;
            trvwAppModule.DataSource = dataSource;
            trvwAppModule.DataBind();

        }

        /// <summary>
        /// 根据ID绑定树
        /// </summary>
        private void BindAppModuleById(Guid id)
        {
            if (_roleManagementService == null) return;
            var currentRole = _roleManagementService.GetRoleByRoleId(id);
            if (currentRole == null) return;
            var rolePrivilege = currentRole.Role_Privilege;
            if (rolePrivilege == null || rolePrivilege.Count == 0) return;
            foreach (var item in rolePrivilege)
            {
                if (item.ModuleID != null)
                {
                    var mId = item.ModuleID.Value;
                    var node = trvwAppModule.FindNodeByValue(mId.ToString());
                    JudgeNode(node, item.PrivilegeDesc);
                }
            }
        }

        /// <summary>
        /// 绑定数据到树型
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="privilegeDesc">权限描述</param>
        private static void JudgeNode(RadTreeNode node, int privilegeDesc)
        {
            if (node == null || node.Nodes.Count == 0) return;
            foreach (RadTreeNode childNode in node.Nodes)
            {
                var vlaue = Convert.ToInt32(childNode.Value) & privilegeDesc;
                childNode.Checked = Convert.ToInt32(childNode.Value) == vlaue;
            }
        }

        /// <summary>
        /// 读取页面数据
        /// </summary>
        /// <param name="role"></param>
        private void GetDataSourceFormTree(Role role)
        {
            if (role == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('加载数据失败!')", true);
                return;
            }
            var nodes = trvwAppModule.GetAllNodes();
            if (nodes != null && nodes.Count > 0)
            {
                foreach (RadTreeNode node in nodes)
                {
                    if (node.Nodes.Count == 0)
                    {
                        var parentNode = node.ParentNode;
                        var modelId = new Guid(parentNode.Value);
                        var rolePrivilege = role.Role_Privilege.Where(it => it.ModuleID == modelId).SingleOrDefault();
                        if(!node.Checked)
                        {
                            if(rolePrivilege!=null)
                            {
                                role.Role_Privilege.Remove(rolePrivilege);
                            }
                            continue;
                        }
                        if (rolePrivilege!=null)
                        {
                            rolePrivilege.PrivilegeDesc = rolePrivilege.PrivilegeDesc | Convert.ToInt32(node.Value);
                        }
                        else
                        {
                            rolePrivilege = new Role_Privilege
                            {
                                Role_PrivilegeID = Guid.NewGuid(),
                                RoleID = role.RoleID,
                                ModuleID = modelId
                            };
                            rolePrivilege.PrivilegeDesc = rolePrivilege.PrivilegeDesc | Convert.ToInt32(node.Value);
                            rolePrivilege.CreateTime = DateTime.Now;
                            role.Role_Privilege.Add(rolePrivilege);
                        }
                      
                    }
                }
            }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 模块树的绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trvwAppModule_NodeDataBound(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Node.Value)) return;
            var id = new Guid(e.Node.Value);
            var dataSource = (IList<AppModule>)Session["AppModel"];
            if (dataSource == null || dataSource.Count() == 0) return;
            var currentNode = dataSource.Where(p => p.ModuleID == id).SingleOrDefault();
            if (currentNode == null) return;
            if (currentNode.IsLeft)
            {
                var currentModulePrivilege = _appModuleManagementService.GetPrivilegeById(id);
                var names = Enum.GetNames(typeof(Privilege));
                foreach (var name in names)
                {
                    var tempreult = currentModulePrivilege[name];
                    if (tempreult != null && tempreult.Value)
                    {
                        var value = DataConversion.GetValue(typeof(Privilege), name);
                        if (value == null) continue;
                        var tempNode = new RadTreeNode(name, value.Value.ToString());
                        e.Node.Nodes.Add(tempNode);
                        e.Node.Expanded = true;
                    }
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var guidRoleId = new Guid(Convert.ToString(ViewState["Id"]));
            var role = _roleManagementService.GetRoleByRoleId(guidRoleId);
            if (role == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('加载数据失败!');", true);
            }
            else
            {
                GetDataSourceFormTree(role);
                try
                {
                    _roleManagementService.Modify(role);
                    ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('修改成功!');", true);
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('修改失败!');", true);
                }

            }
        }
        #endregion
    }
}