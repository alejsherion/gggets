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
        private ISPManagementService _spService;//存储过程BLL
        protected WayBillGenerate()
        {
        }
        public WayBillGenerate(IHAWBManagementService hawbService,ISPManagementService spService)
        {
            _hawbService = hawbService;
            _spService = spService;
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
            if(ds!=null)
            {
                //注意dataset1必须和你报表所引用的table 一致
                RVWayBills.Visible = true;
                ReportDataSource rds = new ReportDataSource("DataSet1", ds.Tables[0]);//注意这里的name和报表中的一致
                RVWayBills.LocalReport.DataSources.Clear();
                RVWayBills.LocalReport.DataSources.Add(rds);
                RVWayBills.LocalReport.Refresh();
            }
            else
            {
                RVWayBills.LocalReport.DataSources.Clear();
                RVWayBills.LocalReport.Refresh();
                RVWayBills.Visible = false;
            }
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
        /// 验证规范化
        /// </summary>
        /// <returns></returns>
        private bool JudgeRegex2()
        {
            string str = txtWayBill.Text.Trim();
            if (string.IsNullOrEmpty(str))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('路单编号不能为空!');", true);
                return false;
            }
            if (!regexRegular.IsMatch(str))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('路单编号只能是数字和字母组成!');", true);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证规范化
        /// </summary>
        /// <returns></returns>
        private bool JudgeRegex3()
        {
            string str = txtWayBill.Text.Trim();
            if (!regexRegular.IsMatch(str))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('路单编号只能是数字和字母组成!');", true);
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
                    //路单是否已经分配
                    if (!string.IsNullOrEmpty(hawb.BillWayCode))
                    {
                        string message = string.Format("该运单已经被分配过,路单编号为{0}", hawb.BillWayCode);
                        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + message + "!');", true);
                        return;
                    }

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

        /// <summary>
        /// 保存的同时批量分配运单的路单编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbSave_Click(object sender, EventArgs e)
        {
            //首先验证路单编号的合法性
            bool judge = JudgeRegex3();
            if(judge)
            {
                if(HAWBList==null || HAWBList.Count==0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('请先分配运单!');", true);
                    return;
                }
                //批处理更新
                //首先获取XML连接字符串
                var hawbCodes = new XElement("Root",
                                             from hawb in HAWBList
                                             select new XElement("Hawb",
                                                 new XElement("barcode", hawb.BarCode))).ToString();
                //批量更新操作
                int count = _spService.UseBatchUpdateWayBillCode(hawbCodes, txtWayBill.Text.Trim());
                if(count==1)
                {
                    txtBarCode.Text = "";
                    txtWayBill.Text = "";
                    HAWBList = new List<TrimedHAWB>();
                    Bind();

                    ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('操作成功!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('操作失败!');", true);
                }
            }
        }

        /// <summary>
        /// 动态获取路单下的运单信息，如果不存在，就认为是一个新的路单编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtWayBill_TextChanged(object sender, EventArgs e)
        {
            string wayBillCode = txtWayBill.Text.Trim();//获取路单信息
            //验证路单编号的合法性
            bool judge = JudgeRegex2();
            if(judge)//验证通过
            {
                IList<HAWB> hawbs = _hawbService.FindHAWBsByBillWayCode(wayBillCode);
                if(hawbs!=null && hawbs.Count!=0)//需要赋值给瘦身后的运单
                {
                    
                    foreach(var hawb in hawbs)
                    {
                        TrimedHAWB trimedHawbModel = new TrimedHAWB();
                        TrimedHAWB trimedHAWB = (TrimedHAWB)PropertyCopy(hawb, trimedHawbModel);//对象属性复制
                        HAWBList.Add(trimedHAWB);//添加成功
                    }
                    
                }
                else
                {
                    HAWBList = new List<TrimedHAWB>();
                }
                Bind();
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