//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        添加组织架构
// 作成者				hong.li
// 改版日				2011.02.25
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using GGGETSAdmin.Common;
using Telerik.Web.UI;

namespace GGGETSAdmin.SysManager
{
    public partial class AddOrganization : System.Web.UI.Page
    {
         #region 构造函数以及字段
        private readonly IOrganizationManagementService _organizationManagementService;
        protected AddOrganization()
        {
            
        }
        public AddOrganization(IOrganizationManagementService organizationManagementService)
        {
            _organizationManagementService = organizationManagementService;
        }
        #endregion

         protected void Page_Load(object sender, EventArgs e)
        {
             if(!IsPostBack)
             {
                
                 var id = Request["Id"];
                 //var id = "d04a82be-5e52-43d2-a126-408de4fa1024";
                 errorDesc.Text = "";
                 if (String.IsNullOrEmpty(id))
                 {
                     ViewState["OpStatus"] = "Add";
                     var organization = new OrganizationChart();
                     DataBindHelper<OrganizationChart>.Bind(form1, organization);
                     Session.Remove("OrgCondtion");
                    
                 }
                 else
                 {
                     ViewState["OpStatus"] = "Update";
                     var guidRoleId = new Guid(id);
                     ViewState["Id"] = guidRoleId;
                     BindControl(guidRoleId);
                     rbtnOrganizationType.Enabled = false;
                 }
                 BindAppModule();
             }
        }

        

         #region 公共方法
         #endregion

         #region 私有方法
         /// <summary>
         /// 绑定树型菜单
         /// </summary>
         private void BindAppModule()
         {
             var tree = (RadTreeView)dropParentDID.Items[0].FindControl("trvwOrganization");
             var opStatus = Convert.ToString(ViewState["OpStatus"]);
             IList<OrganizationChart> dataSource = new List<OrganizationChart>(); 
             if(opStatus == "Add")
             {
                 dataSource = _organizationManagementService.GeAllOrganization();
             }
             else
             {
                 var guidId = (Guid)ViewState["Id"];
                 var user = _organizationManagementService.GetOrganizationByDid(guidId);
                 if (user == null)
                 {
                     errorDesc.Text = @"加载父级节点失败";
                     return;
                 }
                 if (user.ParentDID != null)
                     dataSource=_organizationManagementService.GetParentOrganizationByDid(user.DID);
             }
              
             tree.DataSource = dataSource;
             tree.DataBind();

         }
         
        /// <summary>
        /// 绑定
        /// </summary>
         private void BindOrganizationType(Guid? id)
        {
            rbtnOrganizationType.SelectedValue = id == null ? "0" : "1";
            trParentDirectory.Visible = id != null;
            if (id == null) return;
            var organization = _organizationManagementService.GetOrganizationByDid(id.Value);
            var item=dropParentDID.Items.FindItemByValue(Convert.ToString(id.Value));
            if (item==null)
            {
                dropParentDID.Items.Add
               (new RadComboBoxItem(organization.OrganizationName, Convert.ToString(id.Value)));
                dropParentDID.SelectedValue = Convert.ToString(id.Value);
            }
           
        }

        /// <summary>
         /// 绑定页面数据
         /// </summary>
         /// <param name="id"></param>
         private void BindControl(Guid id)
         {
             if (_organizationManagementService == null) return;
             try
             {
                 var organization = _organizationManagementService.GetOrganizationByDid(id);
                 DataBindHelper<OrganizationChart>.Bind(form1, organization);
                 BindOrganizationType(organization.ParentDID);
               
                 errorDesc.Text = "";
                 
             }
             catch
             {
                 errorDesc.Text = @"加载数据失败!";
             }

         }

        #endregion

         #region 事件
         /// <summary>
        /// 组织类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         protected void rbtnOrganizationType_SelectedIndexChanged(object sender, EventArgs e)
         {
            var value = rbtnOrganizationType.SelectedValue;
             switch(value)
             {
                 case "0":
                     trParentDirectory.Visible = false;
                     dropParentDID.SelectedIndex = -1;
                     break;
                 case "1":
                     trParentDirectory.Visible = true;
                     break;
             }
         }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         protected void btnAdd_Click(object sender, EventArgs e)
         {
             var opStatus = Convert.ToString(ViewState["OpStatus"]);
             OrganizationChart user;
             try
             {
                 var result = Validation();
                 if (!result) return;
                 if (opStatus == "Add")
                 {
                     user = new OrganizationChart();
                 }
                 else
                 {
                     var guidId =  (Guid)ViewState["Id"];
                     user = _organizationManagementService.GetOrganizationByDid(guidId);
                 }
                 DataBindHelper<OrganizationChart>.GetDaTa(form1, user);
                 if (rbtnOrganizationType.SelectedValue == "1")
                 {
                     var parentDid = dropParentDID.SelectedValue;
                     user.ParentDID = new Guid(parentDid);
                 }
                 else
                 {
                     user.ParentDID = null;
                 }
             }
             catch (Exception ce)
             {
                 errorDesc.Text = ce.Message;
                 return;
             }
             try
             {
                 
                 if (opStatus == "Add")
                 {
                     user.DID= Guid.NewGuid();
                     user.CreateTime = DateTime.Now;
                     if (rbtnOrganizationType.SelectedValue=="1")
                     {
                         user.ParentDID = new Guid(dropParentDID.SelectedValue);
                     }
                     else
                     {
                         user.ParentDID = null;
                     }
                     _organizationManagementService.Add(user);
                 }
                 else
                 {
                     _organizationManagementService.Modify(user);
                 }
                 errorDesc.Text =@"操作成功!";
             }
             catch(Exception ex)
             {

                 errorDesc.Text = ex.Message.Contains("组织编号") ? ex.Message : @"操作失败!";
             }
         }
         
         /// <summary>
         /// 返回
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
         protected void btnComeBack_Click(object sender, EventArgs e)
         {
             Response.Redirect("OrganizationManagement.aspx");
         }

         /// <summary>
         /// 页面数据验证
         /// </summary>
         /// <returns></returns>
         private bool Validation()
         {
             var result = true;
             if (rbtnOrganizationType.SelectedValue=="1")
             {
                 var parentDid = dropParentDID.SelectedValue;
                 if (String.IsNullOrEmpty(parentDid))
                 {
                     Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                       , "alert('请选择上级组织!')", true);
                     result = false;
                 }
             }
             return result;

         }
        #endregion


       
    }
}