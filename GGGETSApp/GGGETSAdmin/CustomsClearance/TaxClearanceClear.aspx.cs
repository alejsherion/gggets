//************************************************************************
// 用户名				GETS国际综合快递
// 系统名				管理后台
// 子系统名		        清关操作-处理税率
// 作成者				ZhiWei.Shen
// 改版日				2011.04.18
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace GGGETSAdmin.CustomsClearance
{
    public partial class TaxClearanceClear : System.Web.UI.Page
    {
        //ioc register
        private IHAWBManagementService _hawbService;
        protected TaxClearanceClear()
        {
        }
        public TaxClearanceClear(IHAWBManagementService hawbService)
        {
            _hawbService = hawbService;
        }
        #region Param Block
        private IList<ProjectDetail> ProjectList//项目
        {
            get { return (List<ProjectDetail>)ViewState["flightNo"]; }
            set { ViewState["flightNo"] = value; }
        }
        private string XMLStr//XML字符串
        {
            get { return (string)ViewState["XMLStr"]; }
            set { ViewState["XMLStr"] = value; }
        }

        private Regex moneyRegex = new Regex(@"[1-9]\d{0,9}(\.\d{1,2})?|0\.[1-9]\d?|0\.0[1-9]");
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //todo 获取参数传递过来的运单号
            if (!IsPostBack)
            {
                string barcode = "2015";
                HAWB hawb = _hawbService.FindHAWBByBarCode(barcode);//获取运单信息
                if (hawb != null)
                {
                    BindController(hawb);
                    RGProjects.Visible = false;

                    //处理二进制数组
                    if(hawb.ProjectResolve!=null)
                    {
                        XMLStr = GetString(hawb.ProjectResolve);
                        XMLToObject();//将XML字符串转化为List集合
                        RGProjects.Visible = true;
                        XMLBind(XMLStr);//绑定数据源
                    }
                }
            }
            if (RGProjects.MasterTableView.Items.Count > 0)
            {
                btnSave.Visible = true;
            }
        }

        #region Private Block
        /// <summary>
        /// 将XML字符串转化为对象或集合
        /// </summary>
        private void XMLToObject()
        {
            StringReader strReader = new StringReader(XMLStr);
            DataSet ds = new DataSet();
            ds.ReadXml(strReader);

            //解析DataSet
            DataTable dt = ds.Tables[0];
            ProjectList = ToList<ProjectDetail>(dt);
        }

        /// <summary>
        /// DataTable 转换为List 集合
        /// </summary
        /// <typeparam name="TResult">类型</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static List<TResult> ToList<TResult>(DataTable dt) where TResult : class,new()
        {
            //创建一个属性的列表
            List<PropertyInfo> prlist = new List<PropertyInfo>();

            //获取TResult的类型实例  反射的入口
            Type t = typeof(TResult);

            //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表
            Array.ForEach<PropertyInfo>(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });

            //创建返回的集合
            List<TResult> oblist = new List<TResult>();

            foreach (DataRow row in dt.Rows)
            {
                //创建TResult的实例
                TResult ob = new TResult();

                //找到对应的数据  并赋值
                prlist.ForEach(p => { if (p.Name != "Id") p.SetValue(ob, row[p.Name], null); else { p.SetValue(ob, new Guid(row[p.Name].ToString()), null); } });
                //放入到返回的集合中.
                oblist.Add(ob);
            }
            return oblist;
        }

        /// <summary>
        /// 绑定页面端控件值
        /// </summary>
        private void BindController(HAWB hawb)
        {
            lblHAWBBindCode.Text = hawb.BarCode.Trim();//运单编号
            lblBindWeight.Text = hawb.TotalWeight.ToString();//运单重量
            lblBindStatus.Text = (Enum.GetValues(typeof(HAWBStatus))).GetValue(Convert.ToInt16(hawb.Status)).ToString();
            lblBindClearanceStatus.Text = hawb.CustomsClearanceState;//海关状态 C R H T
            BindProject();//项目下拉绑定
            BindCurrency();//币种下拉绑定            
        }

        /// <summary>
        /// bind dropdownlist for Project
        /// </summary>
        private void BindProject()
        {
            ddlProjects.Items.Clear();
            ddlProjects.DataSource = ListTypeForEnumProject();
            ddlProjects.DataValueField = "value";
            ddlProjects.DataTextField = "text";
            ddlProjects.DataBind();

            ddlProjects.Items.Insert(0, new ListItem("-请选择-", "-1"));
        }

        /// <summary>
        /// bind dropdownlist for Currency
        /// </summary>
        private void BindCurrency()
        {
            ddlCurrency.Items.Clear();
            ddlCurrency.DataSource = ListTypeForEnumCurrency();
            ddlCurrency.DataValueField = "value";
            ddlCurrency.DataTextField = "text";
            ddlCurrency.DataBind();

            ddlCurrency.Items.Insert(0, new ListItem("-请选择-", "-1"));
        }

        /// <summary>
        /// Project Enum Data Resolve
        /// </summary>
        /// <returns></returns>
        private IList ListTypeForEnumProject()
        {
            ArrayList list = new ArrayList();
            foreach (int i in Enum.GetValues(typeof(Project)))
            {
                ListItem listitem = new ListItem(Enum.GetName(typeof(Project), i), i.ToString());
                list.Add(listitem);
            }
            return list;
        }

        /// <summary>
        /// Currency Enum Data Resolve
        /// </summary>
        /// <returns></returns>
        private IList ListTypeForEnumCurrency()
        {
            ArrayList list = new ArrayList();
            foreach (int i in Enum.GetValues(typeof(Currency)))
            {
                ListItem listitem = new ListItem(Enum.GetName(typeof(Currency), i), i.ToString());
                list.Add(listitem);
            }
            return list;
        }

        /// <summary>
        /// 正则表达式判断
        /// </summary>
        private bool RegexJudge()
        {
            //首先判断项目下拉
            if (ddlProjects.SelectedValue == "-1")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('请选择项目选项!');", true);
                return false;
            }
            //再确认币种下拉
            if (ddlCurrency.SelectedValue == "-1")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('请选择币种选项!');", true);
                return false;
            }
            //最后确定金额
            if (string.IsNullOrEmpty(txtMoney.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('金额不能为空!');", true);
                return false;
            }
            if (!moneyRegex.IsMatch(txtMoney.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('请填写正确的金额!');", true);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 将XML字符串解析为二进制数组
        /// </summary>
        /// <param name="message">XML字符串</param>
        /// <returns></returns>
        private Byte[] GetByte(string message)
        {
            return Encoding.Default.GetBytes(message);
        }

        /// <summary>
        /// 将二进制数组转化为XML字符串
        /// </summary>
        /// <param name="byteObj">二进制数组</param>
        /// <returns></returns>
        private string GetString(Byte[] byteObj)
        {
            return Encoding.Default.GetString(byteObj);
        }

        /// <summary>
        /// 通过集合绑定数据源
        /// </summary>
        private void Bind(IList<ProjectDetail> list)
        {
            RGProjects.DataSource = list;
            RGProjects.DataBind();
        }

        /// <summary>
        /// 通过XML字符串绑定数据源
        /// </summary>
        private void XMLBind(string xmlStr)
        {
            var source = XMLBindSource(xmlStr);
            if(source==null)
            {
                RGProjects.DataSource = new List<ProjectDetail>();
                RGProjects.DataBind();
            }
            RGProjects.DataSource = source;
            RGProjects.DataBind();
        }

        /// <summary>
        /// 通过XML字符串绑定数据源
        /// </summary>
        /// <param name="xmlStr"></param>
        private DataSet XMLBindSource(string xmlStr)
        {
            if (string.IsNullOrEmpty(xmlStr)) return null;
            DataSet ds = new DataSet();
            StringReader reader = new StringReader(xmlStr);
            ds.ReadXml(reader);
            return ds;
        }

        /// <summary>
        /// 清空所有的控件值，恢复默认值
        /// </summary>
        private void Clear()
        {
            //所有都清空
            ddlProjects.SelectedValue = "-1";
            ddlCurrency.SelectedValue = "-1";
            txtMoney.Text = "";
        }
        #endregion

        #region ServiceEvent Block
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            bool judge = RegexJudge();//验证
            btnSave.Visible = true;
            if(judge)
            {
                if (ProjectList == null)
                {
                    ProjectList = new List<ProjectDetail>();//实例化
                }
                ProjectList.Add(new ProjectDetail
                {
                    Id = Guid.NewGuid(),
                    ProjectName = ddlProjects.SelectedItem.Text,
                    CurrencyType = ddlCurrency.SelectedItem.Text,
                    Tax = txtMoney.Text.Trim()
                });
                //所有都清空
                Clear();

                XMLStr = new XElement("Root",
                    from projectDetail in ProjectList
                    //let x = String.Format("{0},{1},{2}",projectDetail.ProjectName,projectDetail.CurrencyType,projectDetail.Tax)
                    select new XElement("Project",
                        new XElement("Id", projectDetail.Id),
                        new XElement("ProjectName", projectDetail.ProjectName),
                        new XElement("CurrencyType", projectDetail.CurrencyType),
                        new XElement("Tax", projectDetail.Tax))).ToString();

                //显示GRID
                if (ProjectList != null || ProjectList.Count != 0) RGProjects.Visible = true;

                //绑定数据源
                XMLBind(XMLStr);
            }
            
        }

        /// <summary>
        /// 生成XML，更新HAWB对应字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //待处理运单信息
            HAWB hawb = _hawbService.FindHAWBByBarCode("2015");
            if (!string.IsNullOrEmpty(XMLStr))
                hawb.ProjectResolve = GetByte(XMLStr);
            else
                hawb.ProjectResolve = null;
            _hawbService.ChangeHAWB(hawb);

            //所有都清空
            Clear();
            ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('操作成功');", true);
        }

        /// <summary>
        /// 命令事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void RGProjects_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if(e.CommandName=="Delete")
            {
                string id = e.CommandArgument.ToString();
                Guid guidId = new Guid(id);
                //获取当前集合进行删除
                ProjectList.Remove(ProjectList.Where(p => p.Id == guidId).Single());

                if (ProjectList.Count!=0)
                {
                    XMLStr = new XElement("Root",
                from projectDetail in ProjectList
                select new XElement("Project",
                    new XElement("Id", projectDetail.Id),
                    new XElement("ProjectName", projectDetail.ProjectName),
                    new XElement("CurrencyType", projectDetail.CurrencyType),
                    new XElement("Tax", projectDetail.Tax))).ToString();
                }
                else
                {
                    XMLStr = "";
                }

                //重新绑定
                XMLBind(XMLStr);
            }
        }
        #endregion

        /// <summary>
        /// 项目明细
        /// </summary>
        [Serializable]
        public class ProjectDetail
        { 
            public Guid Id
            {
                get;
                set;
            }

            public string ProjectName
            {
                get;
                set;
            }

            public string CurrencyType
            {
                get;
                set;
            }

            public string Tax
            {
                get;
                set;
            }
        }
    }
}