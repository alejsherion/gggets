//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        组织管理界面
// 作成者				hong.li
// 改版日				2011.02.25
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Web.UI.WebControls;
using Application.GGETS;
using GGGETSAdmin.Common;
using Telerik.Web.UI;

namespace GGGETSAdmin.SysManager
{
    public partial class OrganizationManagement : System.Web.UI.Page
    {
         #region 构造函数以及字段

        private readonly IOrganizationManagementService _organizationManagementService;
        protected OrganizationManagement()
        {
            
        }
        public OrganizationManagement(IOrganizationManagementService organizationManagementService)
        {
            _organizationManagementService = organizationManagementService;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindStatus();

                var condtion = (SeachCondtion)Session["OrgCondtion"];
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
            var organizationName = txtOrganizationName.Text.Trim();
            var organizationCode = txtOrganizationCode.Text.Trim();
            var parentId = dropParentDID.SelectedValue;
            Guid? guidParentId=null;
            if(!String.IsNullOrEmpty(parentId))
            {
                guidParentId = new Guid(parentId);
            }
            DateTime? startCreateTime = null;
            if (!String.IsNullOrEmpty(txtStartCreateTime.Text))
                startCreateTime = DateTime.Parse(txtStartCreateTime.Text);
            DateTime? endCreateTime = null;
            if (!String.IsNullOrEmpty(txtEndCreateTime.Text))
                endCreateTime = DateTime.Parse(txtEndCreateTime.Text);
            var dataSource = _organizationManagementService.QueryByCondtion(organizationName, startCreateTime, endCreateTime, organizationCode, guidParentId);
            dgrdOrganization.DataSource = dataSource;
            dgrdOrganization.DataBind();
            var condtion = new SeachCondtion();
            DataBindHelper<SeachCondtion>.GetDaTa(tbCondtion, condtion);
            Session["OrgCondtion"] = condtion;
        }

        protected void dgrdOrganization_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var commandName = e.CommandName;
            switch (commandName)
            {
                case "Update":
                    var id = Convert.ToString(e.CommandArgument);
                    try
                    {
                        var userId = new Guid(id);
                        var url = "AddOrganization.aspx?Id=" + id;
                        Response.Redirect(url);

                    }
                    catch
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                   , "alert('修改失败!')", true);
                    }
                    break;
                case "Del":
                    var userid = Convert.ToString(e.CommandArgument);
                    try
                    {
                        var guidUserId = new Guid(userid);
                        _organizationManagementService.RemoveAll(guidUserId);
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

        protected void dgrdOrganization_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgrdOrganization.PageIndex = e.NewPageIndex;
            dgrdOrganization.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddOrganization.aspx");
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 绑定父级节点
        /// </summary>
        private void BindStatus()
        {
            var tree = (RadTreeView)dropParentDID.Items[0].FindControl("trvwOrganization");
            var dataSource =_organizationManagementService.GeAllOrganization();
            tree.DataSource = dataSource;
            tree.DataBind();
        }
        #endregion

        #region 公用方法
        /// <summary>
        /// 得到组织父级描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetStatusByCode(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            try
            {
                var currentGuid = new Guid(value);
                if (_organizationManagementService == null) return "";
                var organization = _organizationManagementService.GetOrganizationByDid(currentGuid);
                return organization == null ? "" : organization.OrganizationName;
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region 查询类
        private class SeachCondtion
        {
            /// <summary>
            /// 组织名称
            /// </summary>
            public string OrganizationName
            {
                get;
                set;
            }

            /// <summary>
            /// 父级节点
            /// </summary>
            public Guid ParentDid
            {
                get;
                set;
            }


            /// <summary>
            /// 状态
            /// </summary>
            public string OrganizationCode
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
        }
        #endregion

      

        

    }
}