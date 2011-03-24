//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        模块管理
// 作成者				hong.li
// 改版日				2011.02.25
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Web.UI.WebControls;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using GGGETSAdmin.Common;

namespace GGGETSAdmin.SysManager
{
    public partial class AppModuleManagement : System.Web.UI.Page
    {
        #region 构造函数以及字段
        private readonly IAppModuleManagementService _appModuleManagementService;//模块IBLL
        protected AppModuleManagement()
        {
            
        }
        public AppModuleManagement(IAppModuleManagementService appModuleManagementService)
        {
            _appModuleManagementService = appModuleManagementService;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindStatus();
                var condtion = (SeachCondtion)Session["AppModuleCondtion"];
                if (condtion != null)
                {
                    DataBindHelper<SeachCondtion>.Bind(tbCondtion, condtion);
                    btnQuery_Click(null, null);
                }
            }
        }

        #region 事件
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindAppModule();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddAppModule.aspx");
        }

        /// <summary>
        /// 命令行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dgrdRole_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var commandName = e.CommandName;
            switch (commandName)
            {
                case "Update":
                    var roleId = Convert.ToString(e.CommandArgument);
                    try
                    {
                        //var guidRoleId = new Guid(roleId);
                        var url = "AddAppModule.aspx?Id=" + roleId;
                        Response.Redirect(url);

                    }
                    catch
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                   , "alert('修改失败!')", true);
                    }
                    break;
                case "Del":
                    var id = Convert.ToString(e.CommandArgument);
                    try
                    {
                        var guidRoleId = new Guid(id);
                        _appModuleManagementService.Remove(guidRoleId);
                        btnQuery_Click(null, null);
                    }
                    catch
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                   , "alert('删除失败!')", true);
                    }
                    break;
            }

        }

        /// <summary>
        /// 角色分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dgrdRole_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgrdRole.PageIndex = e.NewPageIndex;
            dgrdRole.DataBind();
        }
        #endregion

        #region 公有方法
        #endregion

        #region 私有方法
        /// <summary>
        /// 绑定状态
        /// </summary>
        private void BindStatus()
        {
            var dataSouce = DataConversion.ListTypeForEnum(typeof(NodeType));
            rbtnStatus.DataSource = dataSouce;
            rbtnStatus.DataTextField = "text";
            rbtnStatus.DataValueField = "value";
            rbtnStatus.DataBind();
            rbtnStatus.SelectedIndex = 1;
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindAppModule()
        {
            var description = txtDescription.Text.Trim();
            DateTime? startCreateTime = null;
            if (!String.IsNullOrEmpty(txtStartCreateTime.Text.Trim()))
            {
                startCreateTime = DateTime.Parse(txtStartCreateTime.Text);
            }
            DateTime? endCreateTime = null;
            if (!String.IsNullOrEmpty(txtEndCreateTime.Text.Trim()))
            {
                endCreateTime = DateTime.Parse(txtEndCreateTime.Text);
            }
            var status = (NodeType)Convert.ToInt32(rbtnStatus.SelectedValue);
            var dataSource = _appModuleManagementService.QueryByCondtion(description, startCreateTime, endCreateTime, status);
            dgrdRole.DataSource = dataSource;
            Session["Role"] = dataSource;
            dgrdRole.DataBind();
            var condtion = new SeachCondtion();
            DataBindHelper<SeachCondtion>.GetDaTa(tbCondtion, condtion);
            Session["AppModuleCondtion"] = condtion;
        }
        #endregion

        #region 查询类
        private class SeachCondtion
        {
            /// <summary>
            /// 名字
            /// </summary>
            public string Description
            {
                get;
                set;
            }

            /// <summary>
            /// 开始时间
            /// </summary>
            public string StartCreateTime
            {
                get;
                set;
            }

            /// <summary>
            /// 结束时间
            /// </summary>
            public string EndCreateTime
            {
                get;
                set;
            }

            /// <summary>
            /// 状态
            /// </summary>
            public string Status
            {
                get;
                set;
            }
        }
        #endregion
    }
}