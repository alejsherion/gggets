//************************************************************************
// 用户名				GETS国际综合快递
// 系统名				管理后台
// 子系统名		        路单生成
// 作成者				ZhiWei.Shen
// 改版日				2011.04.21
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using Microsoft.Reporting.WebForms;

namespace GGGETSAdmin.CustomsClearance
{
    public partial class WayBillGenerate : System.Web.UI.Page
    {
        #region Param Block
        private Regex regexRegular = new Regex(@"[a-zA-Z0-9]{1,}");//只能输入字母和数字

        //运单集合信息
        public IList<TrimedHAWB> HAWBList
        {
            get { return (List<TrimedHAWB>)ViewState["HAWBList"]; }
            set { ViewState["HAWBList"] = value; }
        }
        #endregion

        #region IOC Register

        private IHAWBManagementService _hawbService;
        protected WayBillGenerate()
        {
        }
        public WayBillGenerate(IHAWBManagementService hawbService)
        {
            _hawbService = hawbService;
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (HAWBList == null)//实例化
                {
                    HAWBList = new List<TrimedHAWB>();
                    
                }
                else
                {
                    Bind();
                }
            }
        }

        #region Private Block
        /// <summary>
        /// 绑定报表数据源
        /// </summary>
        private void Bind()
        {
            //绑定报表
            RVWayBills.LocalReport.ReportPath = MapPath("Source/WayBillSource.rdlc");
            //绑定数据源
            DataSet ds = ConvertToDataSet<TrimedHAWB>(HAWBList);
            //注意dataset1必须和你报表所引用的table 一致
            ReportDataSource rds = new ReportDataSource("DataSet1", ds.Tables[0]);//注意这里的name和报表中的一致
            RVWayBills.LocalReport.DataSources.Clear();
            RVWayBills.LocalReport.DataSources.Add(rds);
            RVWayBills.LocalReport.Refresh();
        }

        /// <summary>
        /// 验证规范化
        /// </summary>
        /// <returns></returns>
        private bool JudgeRegex()
        {
            string str = txtBarCode.Text.Trim();
            if(string.IsNullOrEmpty(str))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('运单编号不能为空!');", true);
                return false;
            }
            if (!regexRegular.IsMatch(str))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('运单编号只能是数字和字母组成!');", true);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 对象属性拷贝(全匹配拷贝)
        /// </summary>
        /// <param name="obj1">源对象</param>
        /// <param name="obj2">目标对象</param>
        /// <returns>目标对象</returns>
        private object PropertyCopy(object obj1, object obj2)
        {
            Type souType = obj1.GetType();//源
            Type tarType = obj2.GetType();//目标
            PropertyInfo[] pis = souType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (null != pis)
            {
                foreach (PropertyInfo pi in pis)
                {
                    string propertyName = pi.Name;
                    PropertyInfo pit = tarType.GetProperty(propertyName);
                    if (pit != null)
                    {
                        switch(pit.PropertyType.Name)
                        {
                            case "String":
                                pit.SetValue(obj2, pi.GetValue(obj1, null), null);
                                break;
                            case "Decimal":
                                pit.SetValue(obj2, Convert.ToDecimal(pi.GetValue(obj1, null)), null);
                                break;
                            case "Guid":
                                pit.SetValue(obj2, new Guid(pi.GetValue(obj1, null).ToString()), null);
                                break;
                        }
                    }
                }
            }
            return obj2;

        }

        /// <summary>
        /// Ilist<T> 转换成 DataSet
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public DataSet ConvertToDataSet<T>(IList<T> list)
        {
            if (list == null || list.Count <= 0)
            {
                return null;
            }
            DataSet ds = new DataSet();
            DataTable dt = new DataTable(typeof(T).Name);
            DataColumn column;
            DataRow row;
            PropertyInfo[] myPropertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (T t in list)
            {
                if (t == null)
                {
                    continue;
                }
                row = dt.NewRow();
                for (int i = 0, j = myPropertyInfo.Length; i < j; i++)
                {
                    PropertyInfo pi = myPropertyInfo[i];
                    string name = pi.Name;
                    if (dt.Columns[name] == null)
                    {
                        column = new DataColumn(name, pi.PropertyType);
                        dt.Columns.Add(column);
                    }
                    row[name] = pi.GetValue(t, null);
                }
                dt.Rows.Add(row);
            }
            ds.Tables.Add(dt);
            return ds;
        }
        #endregion

        #region Server Block
        /// <summary>
        /// 运单扫描确认事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCertain_Click(object sender, EventArgs e)
        {
            bool judge = JudgeRegex();
            if(judge)//验证通过
            {
                //读取扫描后的运单编号
                HAWB hawb = _hawbService.FindHAWBByBarCode(txtBarCode.Text.Trim());
                TrimedHAWB trimedHawb = new TrimedHAWB();
                if(hawb!=null)
                {
                    bool judge2 = true;
                    TrimedHAWB trimedHAWB = (TrimedHAWB)PropertyCopy(hawb, trimedHawb);//对象属性复制
                    //判断重复
                    foreach(var item in HAWBList)
                    {
                        if (item.BarCode.Equals(trimedHAWB.BarCode))
                            judge2 = false;
                    }
                    if(judge2)
                        //添加进HAWB集合中
                        HAWBList.Add(trimedHAWB);
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('请不要重复添加运单!');", true);
                        return;
                    }

                    Bind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('没有该运单信息，请重新录入或扫描!');", true);
                }
            }
        }
        #endregion
    }

    #region Object Block
    /// <summary>
    /// 瘦身后的HAWB，作为快递人员输出信息依据
    /// </summary>
    [Serializable]
    public class TrimedHAWB
    {
        public string BarCode { get; set; }//运单号
        public string ConsigneeContactor { get; set; }//联系人姓名
        public string ConsigneeAddress { get; set; }//地址
        public string ConsigneeZipCode { get; set; }//邮政编码
        public string ConsigneeTel { get; set; }//电话
        public decimal TotalWeight { get; set; }//总重量
    }
    #endregion
}