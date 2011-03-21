//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        打印参数维护界面
// 作成者				ZhiWei.Shen
// 改版日				2011.03.17
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;

namespace GGGETSAdmin.PrintManage
{
    public partial class TemplateParamManage : System.Web.UI.Page
    {
        private IParamManagementService _paramService;
        private ITemplateManagementService _templateService;
        private IFindInfoManagementService _findInfoService;
        protected TemplateParamManage()
        { }
        public TemplateParamManage(IParamManagementService paramService,ITemplateManagementService templateService,IFindInfoManagementService findInfoService)
        {
            _paramService = paramService;
            _templateService = templateService;
            _findInfoService = findInfoService;
        }
        //模板号 
        public string TID
        {
            get { return Convert.ToString(ViewState["TID"]); }
            set { ViewState["TID"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string TID = Request["TID"];//获取传递过来的参数
                if(!string.IsNullOrEmpty(TID))
                {
                    Bind(TID);
                    //初始化columes
                    BindColume(ddlTable.SelectedValue);
                    BindKey(ddlColume.SelectedItem.Text);
                }
            }
        }

        private void Bind(string TID)
        {
            IList<Param> templateParams = _paramService.FindParamsByTID(TID);
            //if (templateParams!=null && templateParams.Count != 0)
            //{
                gbTemplateParams.DataSource = templateParams;
                gbTemplateParams.DataBind();

                //绑定table下拉列表
                BindTable(TID);
            //}
            //else
            //    ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('暂无记录！您可以添加对应参数!');", true);
        }

        /// <summary>
        /// 绑定table 
        /// </summary>
        private void BindTable(string TID)
        {
            //获取模板对应字段CorrespondingTable和CorrespondingCN
            Template template = _templateService.FindTemplateByTID(TID);
            //获取模板并且拆分,这里的CorrespondingTable和CorrespondingCN绝对不可能为空，所以不判断了
            string[] tables = template.CorrespondingTable.Split(new char[] {','});
            string[] cns = template.CorrespondingCN.Split(new char[] {','});
            IDictionary<string, string> DTables = new Dictionary<string, string>();
            for(int i=0;i<tables.Count();i++)
            {
                DTables.Add(cns[i], tables[i]);
            }
            //绑定DDL
            ddlTable.DataSource = DTables;
            ddlTable.DataTextField = "key";
            ddlTable.DataValueField = "value";
            ddlTable.DataBind();
        }

        /// <summary>
        /// 绑定colume
        /// </summary>
        private void BindColume(string selectedTable)
        {
            //通过选择表格触发获取它所对应的所有列信息
            IList<FindInfo> infos = _findInfoService.FindAllByTableName(selectedTable);
            if(infos.Count!=0)
            {
                //组织一个局部变量用来绑定columes
                IDictionary<string, string> columeList = new Dictionary<string, string>();
                foreach(FindInfo info in infos)
                {
                    columeList.Add(info.fielddesc, info.fieldname);
                }
                if(columeList.Count!=0)
                {
                    //绑定开始
                    ddlColume.DataSource = columeList;
                    ddlColume.DataTextField = "key";
                    ddlColume.DataValueField = "value";
                    ddlColume.DataBind();
                }
            }
        }

        /// <summary>
        /// 绑定key
        /// </summary>
        private void BindKey(string selectedColume)
        {
            if(!string.IsNullOrEmpty(selectedColume))
            {
                txtKey.Text = selectedColume;
            }
        }

        /// <summary>
        /// 逻辑处理参数类型
        /// </summary>
        /// <param name="paramType"></param>
        /// <returns></returns>
        public string ReturnParamType(string paramType)
        {
            string result = string.Empty;
            switch(paramType)
            {
                case "Text":
                    result = "普通文本";
                    break;
                case "Textarea":
                    result = "长文本";
                    break;
                case "BarCode":
                    result = "条形码";
                    break;
                case "RadioBox":
                    result = "单选按钮";
                    break;
            }
            return result;
        }

        /// <summary>
        /// 分页 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gbTemplateParams_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gbTemplateParams.PageIndex = e.NewPageIndex;
            Bind(Request["TID"]);
        }

        /// <summary>
        /// 选择对应表名触发表下的对应列信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tableName = ddlTable.SelectedItem.Value;
            //清除平均数
            rblStatistic.ClearSelection();
            
            //触发方法
            BindColume(tableName);
            ChangeStatus();
        }

        /// <summary>
        /// 选择列显示在下方文本中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlColume_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindKey(ddlColume.SelectedItem.Text);//绑定下方文本
            //确定选择的参数是否是整数类型
            FindInfo info = _findInfoService.FindInfoByTableAndFieldName(ddlColume.SelectedValue, ddlTable.SelectedValue);
            if(info!=null)
            {
                //将单选框默认全不选
                rblStatistic.ClearSelection();
                if (info.type.Equals("int") || info.type.Equals("decimal")) trStatistic.Visible = true;
                else trStatistic.Visible = false;

                //ChangeStatus();
            }
            //清除平均数
            rblStatistic.ClearSelection();
        }

        /// <summary>
        /// 拼接组织SQL语句
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Template template = _templateService.FindTemplateByTID(Request["TID"]);
            string sqlStr = GenerateSQL(ddlColume.SelectedValue, ddlTable.SelectedValue, template.IdentifyKey);
            //类型如果是radio，单独处理
            if(ddlParamType.SelectedValue=="RadioBox")
            {
                SaveRadio(template, sqlStr);
                //重新绑定
                Bind(Request["TID"]);
                BindColume(ddlTable.SelectedValue);
                BindKey(ddlColume.SelectedItem.Text); 
                //清除平均数
                rblStatistic.ClearSelection();
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('新增成功！');", true);
                return;
            }
            else
            {
                //开始保存参数
                Param paramObj = new Param
                {
                    PID = Guid.NewGuid(),
                    Tag = template.Params.Count + 1,
                    Key = txtKey.Text.Trim(),
                    Value = sqlStr,
                    Top = 0,
                    Left = 0,
                    Width = 100,
                    Height = 20,
                    FontName = "WST_Swed",
                    FontSize = 9,
                    Alignment = 0,
                    Bold = 0,
                    Italic = 0,
                    Underline = 0,
                    ParamType = ddlParamType.SelectedValue
                };
                paramObj.Template = template;
                _paramService.AddParam(paramObj);
                //重新绑定
                Bind(Request["TID"]);
                //清除平均数
                rblStatistic.ClearSelection();
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('新增成功！');", true);
            }
            
        }

        /// <summary>
        /// 独立保存radio
        /// </summary>
        /// <param name="template"></param>
        private void SaveRadio(Template template,string sqlStr)
        {
            //处理radioBox
            Dictionary<string, Type> dt = ResolveRadioBoxType();
            if (dt.ContainsKey(ddlColume.SelectedValue))
            {
                var enumObj = HashtableForEnum(dt[ddlColume.SelectedValue]);
                if (enumObj != null)
                {
                    foreach (var key in enumObj.Keys)
                    {
                        Param param = new Param
                        {
                            PID = Guid.NewGuid(),
                            Tag = template.Params.Count + 1,
                            Key = Convert.ToString(key),
                            Value = sqlStr,
                            Top = 0,
                            Left = 0,
                            Width = 100,
                            Height = 20,
                            FontName = "WST_Swed",
                            FontSize = 9,
                            Alignment = 0,
                            Bold = 0,
                            Italic = 0,
                            Underline = 0,
                            ParamType = ddlParamType.SelectedValue,
                            DefaultValue = Convert.ToString(enumObj[key])
                        };
                        param.Template = template;
                        _paramService.AddParam(param);//保存
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('不能选择此字段，请重新选择！');", true);

                //置索引0
                BindColume(ddlTable.SelectedValue);
                BindKey(ddlColume.SelectedItem.Text); 
                //return;
            }
        }

        /// <summary>
        /// 把枚举转换为数组
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        private Hashtable HashtableForEnum(Type enumType)
        {
            var ht = new Hashtable();
            foreach (int i in Enum.GetValues(enumType))
            {
                //var listitem = new ListItem(Enum.GetName(enumType, i), i.ToString());
                if(ht.ContainsKey(Enum.GetName(enumType, i))) continue;
                ht.Add(Enum.GetName(enumType, i), i);
                //list.Add(listitem);
            }
            return ht;
        }

        /// <summary>
        /// 单独处理类型为radiobox
        /// </summary>
        private Dictionary<string ,Type> ResolveRadioBoxType()
        {
            var ht = new Dictionary<string, Type>();

            Assembly ass = Assembly.LoadFrom(Server.MapPath("~/bin/GGGETSApp.Domain.Application.Entities.dll"));
            //获取程序集中定义的类型       
            Type[] types = ass.GetTypes();
            if (types == null || types.Count()==0) return null;
            foreach(Type type in types)
            {
                if(type.IsEnum)
                {
                    var name = type.Name;
                    ht.Add(name, type);
                }
            }
            return ht;
        }

        /// <summary>
        /// 组织SQL语句
        /// </summary>
        /// <param name="columeName">列名</param>
        /// <param name="tableName">表名</param>
        /// <param name="identifyKey">条件</param>
        /// <returns></returns>
        private string GenerateSQL(string columeName,string tableName,string identifyKey)
        {
            string sqlStr = string.Empty;
            //没有选中radio
            if (rblStatistic.SelectedIndex == -1) sqlStr = string.Format("select {0} from {1} where {2}='#key#'", columeName, tableName, identifyKey);
            else sqlStr = string.Format("select {0}({1}) from {2} where {3}='#key#'",rblStatistic.SelectedValue, columeName, tableName, identifyKey);
            return sqlStr;
        }

        /// <summary>
        /// 命令模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gbTemplateParams_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteNew")
            {
                int tag = Convert.ToInt32(e.CommandArgument);//获取tag
                //删除该对象
                _paramService.RemoveParam(_paramService.FindParamByTIDAndTag(Request["TID"], tag));
                //获取升序后的当前所有的参数集合
                IList<Param> paramsList = _paramService.FindParamsByTID(Request["TID"]).OrderBy(it => it.Tag).ToList();
                if(paramsList.Count!=0)
                {
                    foreach(Param paramObj in paramsList)
                    {
                        if(paramObj.Tag > tag)
                        {
                            //执行修改
                            paramObj.Tag = paramObj.Tag - 1;
                            _paramService.ModifyParam(paramObj);
                        }
                    }
                }
                //最后绑定
                Bind(Request["TID"]);
                //提示
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('删除成功!');", true);
            }
        }

        /// <summary>
        /// 加入删除判断 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gbTemplateParams_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                ((LinkButton)e.Row.Cells[3].FindControl("lbDelete")).Attributes.Add("onclick", "javascript:return confirm('你确认要删除吗?')");
        }

        /// <summary>
        /// 主要用于处理radioBOX
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlParamType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //清除平均数
            rblStatistic.ClearSelection();

            ChangeStatus();
        }

        private void ChangeStatus()
        {
            if (ddlParamType.SelectedIndex != 0) trStatistic.Visible = false;
            else
            {
                FindInfo info = _findInfoService.FindInfoByTableAndFieldName(ddlColume.SelectedValue, ddlTable.SelectedValue);
                if (info.type.Equals("int") || info.type.Equals("decimal")) trStatistic.Visible = true;
                else trStatistic.Visible = false;
            }
        }

        /// <summary>
        /// 返回上一目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PrintManage/TemplateMaintain.aspx");
        }
    }
}