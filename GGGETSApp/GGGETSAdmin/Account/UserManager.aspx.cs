//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        用户管理界面
// 作成者				hong.li
// 改版日				2011.02.25
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using GGGETSAdmin.Common;

namespace GGGETSAdmin.Account
{
    public partial class UserManager : System.Web.UI.Page
    {
        #region 构造函数以及字段

        private readonly ISysUserManagementService _sysUserManagementService;
        protected UserManager()
        {
            
        }
        public UserManager(ISysUserManagementService sysUserManagementService)
        {
            _sysUserManagementService = sysUserManagementService;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindStatus();
                
                var condtion=(SeachCondtion)Session["Condtion"];
                if(condtion!=null)
                {
                    DataBindHelper<SeachCondtion>.Bind(tbCondtion, condtion);
                    btnQueryClick(null, null);
                }
            }
        }

        #region 事件
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQueryClick(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var tel = txtTel.Text.Trim();
            var loginName = txtLoginName.Text.Trim();
            var status = (Status)Convert.ToInt32(ddlStatus.SelectedValue);
            DateTime? startCreateTime = null;
            if (!String.IsNullOrEmpty(txtStartCreateTime.Text))
                startCreateTime = DateTime.Parse(txtStartCreateTime.Text);
            DateTime? endCreateTime = null;
            if (!String.IsNullOrEmpty(txtEndCreateTime.Text))
                endCreateTime = DateTime.Parse(txtEndCreateTime.Text);
            var dataSource = _sysUserManagementService.QueryByCondtion(email, tel, loginName, startCreateTime, endCreateTime
                                                      , status);
            dgrdUsers.DataSource = dataSource;
            dgrdUsers.DataBind();
            var condtion = new SeachCondtion();
            DataBindHelper<SeachCondtion>.GetDaTa(tbCondtion, condtion);
            Session["Condtion"] = condtion;
        }
       

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddUser.aspx");
        }

        /// <summary>
        /// 修改以及查看事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dgrdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var commandName = e.CommandName;
            switch (commandName)
            {
                case "Update":
                    var id = Convert.ToString(e.CommandArgument);
                    try
                    {
                        var userId = new Guid(id);
                        var url = "AddUser.aspx?Id=" + id;
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
                        _sysUserManagementService.Remove(guidUserId);
                        btnQueryClick(null, null);
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
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dgrdUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgrdUsers.PageIndex = e.NewPageIndex;
            dgrdUsers.DataBind();
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 绑定会员状态
        /// </summary>
        private void BindStatus()
        {
            ddlStatus.DataSource = DataConversion.ListTypeForEnum(typeof(Status));
            ddlStatus.DataTextField = "text";
            ddlStatus.DataValueField = "value";
            ddlStatus.DataBind();
        }
        #endregion

        #region 公用方法
        /// <summary>
        /// 得到会员状态描述
        /// </summary>
        /// <param name="value">状态值</param>
        /// <returns></returns>
        public string GetStatusByCode(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            try
            {
                var tempValue = Enum.GetName(typeof(Status),Convert.ToInt32(value));
                return tempValue;
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
            /// 邮件
            /// </summary>
            public string Email
            {
                get;
                set;
            }

            /// <summary>
            /// 电话
            /// </summary>
            public string Tel
            {
                get;
                set;
            }

            /// <summary>
            /// 登录名
            /// </summary>
            public string LoginName
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