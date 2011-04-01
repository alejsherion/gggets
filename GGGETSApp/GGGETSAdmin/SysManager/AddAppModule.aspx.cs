//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        添加模块
// 作成者				hong.li
// 改版日				2011.02.25
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using GGGETSAdmin.Common;

namespace GGGETSAdmin.SysManager
{
    public partial class AddAppModule : Page
    {
        #region 构造函数以及字段
        private readonly IAppModuleManagementService _appModuleManagementService;
        private readonly IRoleManagementService _roleManagementService;
        protected AddAppModule()
        {
            
        }
        public AddAppModule(IAppModuleManagementService appModuleManagementService
                             ,IRoleManagementService roleManagementService)
        {
            _appModuleManagementService = appModuleManagementService;
            _roleManagementService = roleManagementService;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                var appModelId = Request["Id"];
                BindPrivilegeDesc();
                BinddropParentId();
                //BindRoleId();
                if(String.IsNullOrEmpty(appModelId))
                {
                    ViewState["OpStatus"] = "Add";
                    rbtnIsLeft.Enabled = true;
                    var user = new SysUser();
                    DataBindHelper<SysUser>.Bind(form1, user);
                    Session["SysUser"] = user;
                    Session.Remove("AppModuleCondtion");
                }
                else
                {
                    ViewState["OpStatus"] = "UpDate";
                    try
                    {
                        var appModelGuid = new Guid(appModelId);
                        ViewState["Id"] = appModelGuid;
                        BindPage(appModelGuid);
                    }
                    catch
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                      , "alert('加载数据失败!')", true);
                    }
                }
            }
        }

        #region 事件
        /// <summary>
        /// 是否为父页面或子页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbtnIsLeft_SelectedIndexChanged(object sender, EventArgs e)
        {
            var isLeftValue = rbtnIsLeft.SelectedValue;
            if(isLeftValue.Equals("0"))
            {
                trParentDirectory.Visible = false;
                trURl.Visible = false;
                trPrivilegeDesc.Visible = false;
                //trRoleID.Visible = false;
            }
            else
            {
                trParentDirectory.Visible = true;
                trURl.Visible = true;
                trPrivilegeDesc.Visible = true;
                //trRoleID.Visible = true;
            }

        }

        /// <summary>
        /// 修改或者保存权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var appmodule = new AppModule();
            try
            {
                var opStatus = Convert.ToString(ViewState["OpStatus"]);
                if (opStatus == "Add")
                {
                    appmodule.ModuleID = Guid.NewGuid();
                    GetDataFormPage(appmodule);
                    appmodule.CreateTime = DateTime.Now;
                    _appModuleManagementService.Add(appmodule);
                }
                else
                {
                    appmodule.ModuleID = (Guid) ViewState["Id"];
                    GetDataFormPage(appmodule);
                    _appModuleManagementService.Modify(appmodule);
                }
                Cache.Remove("RoleArry");
                Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                     , "alert('添加成功!')", true);

            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                     , "alert('添加失败!')", true);
            }
            
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnComeBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AppModuleManagement.aspx");
        }
        #endregion

        #region 公共方法
        #endregion

        #region 私有方法
        /// <summary>
        /// 绑定父级页面
        /// </summary>
        private void BinddropParentId()
        {
            var dataSource = _appModuleManagementService.GeParentModule();
            dropParentId.DataSource = dataSource;
            dropParentId.DataTextField = "Description";
            dropParentId.DataValueField = "ModuleID";
            dropParentId.DataBind();
        }

        /// <summary>
        /// 绑定权限
        /// </summary>
        private void BindPrivilegeDesc()
        {
            var dataSouce = DataConversion.ListTypeForEnum(typeof(Privilege));
            chklPrivilegeDesc.DataSource = dataSouce;
            chklPrivilegeDesc.DataTextField = "text";
            chklPrivilegeDesc.DataValueField = "value";
            chklPrivilegeDesc.DataBind();
        }

        ///// <summary>
        ///// 绑定角色
        ///// </summary>
        //private void BindRoleId()
        //{
        //    var dataSouce = _roleManagementService.GetAllRole();
        //    if (dataSouce!=null)
        //    Cache["RoleArry"] = dataSouce;
        //    chkRoleID.DataSource = dataSouce;
        //    chkRoleID.DataTextField = "Name";
        //    chkRoleID.DataValueField = "RoleID";
        //    chkRoleID.DataBind();
        //}

        /// <summary>
        /// 获取页面数据
        /// </summary>
        private void GetDataFormPage(AppModule appmodule)
        {
            if (appmodule == null) appmodule = new AppModule();
            var result = Validation();
            if (!result) return; 
            appmodule.Description = txtDescription.Text;
            appmodule.Remark = txtRemark.Text;
            var pageType = rbtnIsLeft.SelectedValue;
            if (pageType == "1")
            {
                appmodule.IsLeft = true;
                appmodule.ParentId = new Guid(dropParentId.SelectedValue);
                appmodule.URL = txtURL.Text;
                var privileges = GetPrivileges();
                appmodule.PrivilegeDesc = DataConversion.GetCurrentPrivilege(privileges);
                //GetRoles(appmodule);
            } 
        }

        /// <summary>
        /// 获取权限数组
        /// </summary>
        /// <returns></returns>
        private int[] GetPrivileges()
        {
            var items = chklPrivilegeDesc.Items;
            return (from ListItem item in items where item.Selected select                                 Convert.ToInt32(item.Value)).ToArray();
        }

        ///// <summary>
        ///// 获取角色
        ///// </summary>
        ///// <param name="appmodule"></param>
        //private void GetRoles(AppModule appmodule)
        //{
        //    if (appmodule == null) return;
        //    var items = chkRoleID.Items;
        //    appmodule.Role_Privilege.Clear();
        //    foreach(ListItem item in items)
        //    {
        //        if (!item.Selected) continue;
        //        var rp = new Role_Privilege
        //                     {
        //                         Role_PrivilegeID = Guid.NewGuid(),
        //                         ModuleID = appmodule.ModuleID,
        //                         RoleID = new Guid(item.Value),
        //                         CreateTime = DateTime.Now,
        //                         PrivilegeDesc = 0
        //                     };
        //        appmodule.Role_Privilege.Add(rp);
        //    }
        //}

        /// <summary>
        /// 页面数据验证
        /// </summary>
        /// <returns></returns>
        private bool Validation()
        {
            var pageType = rbtnIsLeft.SelectedValue;
            if (pageType == "1")
            {
                var errorMessage = "";
                var url = txtURL.Text.Trim();
                if (String.IsNullOrEmpty(url))
                {
                    errorMessage += "访问地址不能为空<br/>";
                }
                var privilegeDesc = chklPrivilegeDesc.SelectedValue;
                if (String.IsNullOrEmpty(privilegeDesc))
                {
                    errorMessage += "必须选择一个权限<br/>";
                }
                if (!String.IsNullOrEmpty(errorMessage))
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                       , "alert('" + errorMessage + "')", true);
                    return false;
                }
                return true;

            }
            return true;
        }

        /// <summary>
        /// 绑定页面数据
        /// </summary>
        /// <param name="id"></param>
        private void BindPage(Guid id)
        {
            rbtnIsLeft.Enabled = false;
            var appmodule = _appModuleManagementService.GetSingleModuleByModuleId(id);
            if(appmodule==null)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                      , "alert('加载数据失败')", true);
                btnAdd.Visible = false;

            } 
            else
            {
                rbtnIsLeft.SelectedValue = appmodule.IsLeft ? "1" : "0";
                rbtnIsLeft_SelectedIndexChanged(null,null);
                txtDescription.Text = appmodule.Description;
                txtRemark.Text = appmodule.Remark;
                
                if (appmodule.IsLeft)
                {
                    dropParentId.SelectedValue = appmodule.ParentId.ToString();
                    txtURL.Text = appmodule.URL;
                    BindPrivilegeDescFormModel(appmodule);
                    //BindRoleIdFormModel(appmodule);
                }
            }
        }

        /// <summary>
        /// 读取数据绑定权限
        /// </summary>
        /// <param name="appmodule">模块实体</param>
        private void BindPrivilegeDescFormModel(AppModule appmodule)
        {
            if (appmodule == null ||appmodule.PrivilegeDesc==null|| appmodule.PrivilegeDesc == 0) return ;
            var items = chklPrivilegeDesc.Items;
            chklPrivilegeDesc.ClearSelection();
            if (items == null || items.Count == 0) return ;
            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var value = Convert.ToString(Convert.ToInt32(item.Value) & appmodule.PrivilegeDesc);
                if (value == item.Value)
                {
                    item.Selected = true;
                }
            }
        }

        ///// <summary>
        ///// 读取数据绑定权限
        ///// </summary>
        ///// <param name="appmodule">模块实体</param>
        //private void BindRoleIdFormModel(AppModule appmodule)
        //{
        //    if (appmodule == null) return;
        //    chkRoleID.ClearSelection();
        //    var items = chkRoleID.Items;
        //    if (items == null || items.Count == 0) return;
        //    for (var i = 0; i < items.Count; i++)
        //    {
        //        var item = items[i];
        //        var value = new Guid(item.Value);
        //        var temprp = appmodule.Role_Privilege.Where(it => it.RoleID == value);
        //        if(temprp!=null&&temprp.Count()>0)
        //        {
        //            item.Selected = true;
        //        }
        //    }
        //}
        #endregion

     
    }
}