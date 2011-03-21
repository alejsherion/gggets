//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        打印维护界面
// 作成者				ZhiWei.Shen
// 改版日				2011.02.28
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;


namespace GGGETSAdmin.PrintManage
{
    //[ScriptService]
    public partial class TemplateMaintain : System.Web.UI.Page
    {
        private IParamManagementService _paramService;
        private ITemplateManagementService _templateService;
        protected TemplateMaintain()
        { }
        public TemplateMaintain(IParamManagementService paramService, ITemplateManagementService templateService)
        {
            _paramService = paramService;
            _templateService = templateService;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                IList<Template> templates = _templateService.GetAll();
                if(templates.Count==0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('请联系管理员添加模板基础数据！');", true);
                    return;
                }
                Bind();
            }
        }

        private void Bind()
        {
            gvTemplate.DataSource = _templateService.GetAll();
            gvTemplate.DataBind();
        }

        /// <summary>
        /// 打印方向确认
        /// </summary>
        /// <returns></returns>
        public string PrintDirection(int printDirection)
        {
            string result = string.Empty;
            switch(printDirection)
            {
                case 0:
                    result = "由操作人员自行选择";
                    break;
                case 1:
                    result = "纵向打印,固定纸张";
                    break;
                case 2:
                    result = "横向打印,固定纸张";
                    break;
                case 3:
                    result = "纵向,宽度固定,高度自由";
                    break;
            }
            return result;
        }

        /// <summary>
        /// 修改模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTemplate_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string name = ((TextBox)this.gvTemplate.Rows[e.RowIndex].Cells[1].FindControl("txtName")).Text;
            string desc = ((TextBox)this.gvTemplate.Rows[e.RowIndex].Cells[2].FindControl("txtDesc")).Text;
            int printDirection = Convert.ToInt32(((DropDownList)this.gvTemplate.Rows[e.RowIndex].Cells[3].FindControl("ddlPrintDirection")).SelectedValue);
            string width = ((TextBox)this.gvTemplate.Rows[e.RowIndex].Cells[4].FindControl("txtWidth")).Text;
            string height = ((TextBox)this.gvTemplate.Rows[e.RowIndex].Cells[5].FindControl("txtHeight")).Text;
            string batchHeight = ((TextBox)this.gvTemplate.Rows[e.RowIndex].Cells[6].FindControl("txtBatchHeight")).Text; 

            //验证开始
            if(string.IsNullOrEmpty(width))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('纸张宽度不能为空!');", true);
                return;
            }
            if (string.IsNullOrEmpty(height))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('纸张高度不能为空!');", true);
                return;
            }
            if (string.IsNullOrEmpty(batchHeight))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('批量套打高度不能为空!');", true);
                return;
            }

            //转化INT
            int pageWidth = Convert.ToInt32(width);
            int pageHeight = Convert.ToInt32(height);
            int pageBatchHeight = Convert.ToInt32(batchHeight);
            //转化成功..............................................................................................

            //获取当前对象
            string templateCode = gvTemplate.DataKeys[e.RowIndex].Value.ToString();
            Template templateObj = _templateService.FindTemplateByTemplateCode(templateCode);

            //开始修改赋值
            templateObj.Name = name;
            templateObj.Desc = desc;
            templateObj.PrintDirection = printDirection;
            templateObj.PagerWidth = pageWidth;
            templateObj.PagerHeight = pageHeight;
            templateObj.BatchHeight = pageBatchHeight;

            //最后保存该对象
            _templateService.ModifyTemplate(templateObj);
            gvTemplate.EditIndex = -1;
            Bind();

            ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('更新成功!');", true);
        }

        /// <summary>
        /// 编辑模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTemplate_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTemplate.EditIndex = e.NewEditIndex;
            Bind();

            var ddlObj = ((DropDownList)this.gvTemplate.Rows[e.NewEditIndex].Cells[3].FindControl("ddlPrintDirection"));
            if (ViewState["CommandArgument"] != null)
                ddlObj.SelectedIndex = Convert.ToInt32(ViewState["CommandArgument"]);
        }

        /// <summary>
        /// 取消模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTemplate_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTemplate.EditIndex = -1;
            Bind();
        }

        protected void gvTemplate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName=="Edit")
            {
                if(e.CommandArgument!=null)
                    ViewState["CommandArgument"] = e.CommandArgument;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PrintManage/AddTemplate.aspx");
        }

        /// <summary>
        /// 删除模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTemplate_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string templateCode = gvTemplate.DataKeys[e.RowIndex].Value.ToString();
            //获取该模板
            Template template = _templateService.FindTemplateByTemplateCode(templateCode);
            _templateService.RemoveTemplate(template);
            ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('删除成功！');", true);
            Bind();
        }

        /// <summary>
        /// 绑定删除提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTemplate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                if (e.Row.Cells[7].FindControl("lbDelete")!=null)
                    ((LinkButton)e.Row.Cells[7].FindControl("lbDelete")).Attributes.Add("onclick", "javascript:return confirm('你确认要删除吗?删除将删除所有已经布局好的参数信息！')");
        }
    }
}