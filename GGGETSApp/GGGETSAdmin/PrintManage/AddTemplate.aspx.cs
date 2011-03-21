//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        模板维护
// 作成者				hong.li
// 改版日				2011.02.25
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace GGGETSAdmin.PrintManage
{
    public partial class AddTemplate : System.Web.UI.Page
    {
        #region 构造函数
         private ITemplateManagementService _templateService;
        protected AddTemplate()
        { }
        public AddTemplate(ITemplateManagementService templateService)
        {
            _templateService = templateService;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindPaperType();
                BindCorrespondingTable();
            }
        }

        #region 事件
        protected void rbltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            var rbltype = (RadioButtonList) sender;
            if (rbltype == null) return;
            var selectValue = rbltype.SelectedValue;
            switch(selectValue)
            {
                case "0":
                    trPagerWidth.Visible = false;
                    trPagerHeight.Visible = false;
                    trPaperType.Visible = true;
                    txtPagerWidth.Text = @"0";
                    txtPagerHeight.Text = @"0";
                    break;
                case "1":
                    trPagerWidth.Visible = true;
                    trPagerHeight.Visible = true;
                    trPaperType.Visible = false;
                    ddlPaperType.SelectedValue = "";
                    break;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var result = ValPage();
            if(result)
            {
                var tableSouce = (DataTable)ViewState["CorrespondingTable"];
                if (tableSouce == null) return;
                var template = new Template();
                template.TID = Guid.NewGuid();
                template.TemplateCode = txtTemplateCode.Text;
                template.Name = txtName.Text;
                template.PrintDirection = Convert.ToInt32(ddlPrintDirection.SelectedValue);
                template.PagerWidth = Convert.ToInt32(txtPagerWidth.Text);
                template.PagerHeight = Convert.ToInt32(txtPagerHeight.Text);
                template.PaperType = ddlPaperType.SelectedValue;
                template.CreateDate = DateTime.Now;
                template.ModifyDate = DateTime.Now;
                template.Operator ="Admin";
                var nameArry = tableSouce.Select("SVALUE='" + ddlCorrespondingTable.SelectedValue + "'");
                if (nameArry!=null&&nameArry.Count()!=0)
                {
                    template.CorrespondingTable = ddlCorrespondingTable.SelectedValue.ToLower();
                    template.CorrespondingCN = Convert.ToString(nameArry[0].ItemArray[3]).ToLower();
                    template.IdentifyKey = Convert.ToString(nameArry[0].ItemArray[4]).ToLower();
                }
                template.TemplateCode = txtTemplateCode.Text;
                try
                {
                    _templateService.AddTemplate(template);
                    ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('模板保存成功!')", true);
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('模板保存失败!')", true);
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PrintManage/TemplateMaintain.aspx");
        }
        #endregion 

        #region 公有方法

        #endregion

        #region 私有方法
        /// <summary>
        /// 把枚举转换为数组
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        private static IList ListTypeForEnum(Type enumType)
        {
            var list = new ArrayList();
            foreach (int i in Enum.GetValues(enumType))
            {
                var listitem = new ListItem(Enum.GetName(enumType, i), i.ToString());
                list.Add(listitem);
            }
            return list;
        }

        /// <summary>
        /// 绑定纸张
        /// </summary>
        private void BindPaperType()
        {
            var dataSource = ListTypeForEnum(typeof (PrintPage));
            ddlPaperType.DataSource = dataSource;
            ddlPaperType.DataTextField = "text";
            ddlPaperType.DataValueField = "text";
            ddlPaperType.DataBind();
            ddlPaperType.Items.Insert(0,new ListItem("请选择", ""));
        }

        /// <summary>
        /// 绑定主表
        /// </summary>
        private void BindCorrespondingTable()
        {
            if (_templateService == null) return;
            var source = _templateService.GetAllPrimaryTable();
            ddlCorrespondingTable.DataSource = source;
            ddlCorrespondingTable.DataTextField = "NameCN";
            ddlCorrespondingTable.DataValueField = "SVALUE";
            ddlCorrespondingTable.DataBind();
            ViewState["CorrespondingTable"] = source;
        }

        /// <summary>
        /// 验证页面
        /// </summary>
        private bool ValPage()
        {
            if (_templateService == null) return false;
            var value = rbltype.SelectedValue;
            var result = true;
            if(value=="0")
            {
                var selectValue = ddlPaperType.SelectedValue;
                if(string.IsNullOrEmpty(selectValue))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('请选择纸张!')", true);
                    result = false;
                }
            }
            var code = txtTemplateCode.Text.Trim();
            var tempEneity = _templateService.FindTemplateByTemplateCode(code);
            if (tempEneity!=null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('模板编号已经存在!')", true);
                result = false;
            }
            return result;
        }
        #endregion
    }
}