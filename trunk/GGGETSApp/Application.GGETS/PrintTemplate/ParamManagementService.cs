//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        模板参数BLL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.28
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Core;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Domain.Core.Specification;

namespace Application.GGETS
{
    public class ParamManagementService:IParamManagementService
    {
        /// <summary>
        /// IOC Injecting into
        /// </summary>
        private IParamRepository _paramRepository;

        private IMAWBRepository _mawbRepository;

        private IHAWBRepository _hawbRepository;
        public ParamManagementService(IParamRepository paramRepository, IMAWBRepository mawbRepository, IHAWBRepository hawbRepository)
        {
            _paramRepository = paramRepository;
            _mawbRepository = mawbRepository;
            _hawbRepository = hawbRepository;
        }

        #region database
        public IList<Param> FindParamsByTID(string TID)
        {
            return _paramRepository.FindParamsByTID(TID);
        }

        #endregion

        /// <summary>
        /// 套打国内外配送订单
        /// </summary>
        /// <param name="intOrient">intOrient：1-纵(正)向打印 2-横向打印 0或其它-默认打印方向</param>
        /// <param name="intPageWidth">宽度：毫米,0.1mm</param>
        /// <param name="intPageHeight">高度：毫米,0.1mm</param>
        /// <param name="strPageName">纸张名：Letter, LetterSmall, Tabloid, Ledger, Legal,Statement, Executive, A3, A4, A4Small, A5, B4, B5, Folio, Quarto, qr10X14, qr11X17, Env9, Env10, Env11, Env12,Env14, Sheet, DSheet, ESheet</param>
        /// <param name="identifyKey">标识列，唯一区分对象的序号</param>
        /// <param name="templateKey">模板序号，唯一区分对象的序号</param>
        /// <param name="operateType">操作类型，是维护还是非维护</param>
        /// <param name="page">页面对象</param>
        public void MaintainDan(int intOrient, int intPageWidth, int intPageHeight, string strPageName, string identifyKey, string templateKey, int operateType, Page page)
        {
            //string SQLStr = string.Format("select * from param where TID={0}", HttpContext.Current.Request["TID"]);
            string SQLStr = string.Format("select * from param where TID='{0}' order by Tag", templateKey);

            StringBuilder sb = new StringBuilder();
            sb.Append("<script lanuage=javascript>");
            sb.Append("function MaintainHAWB() {");
            sb.Append("CreatePage();");
            SqlDataReader reader = Execution(SQLStr);
            //读取数据
            if(reader.HasRows)
            while(reader.Read())
            {
                string result = ResolveResult(reader["Key"].ToString(), reader["Value"].ToString(),
                                        reader["ParamType"].ToString(), reader["GroupName"].ToString(),
                                        identifyKey, operateType);
                if (reader["ParamType"].ToString().Equals("Text"))
                {
                    sb.Append("LODOP.ADD_PRINT_TEXT(" + reader["top"] + "," + reader["left"] + ", " + reader["width"] + ", " + reader["height"] + ", '" + result + "');");//定义内容
                    sb.Append("LODOP.SET_PRINT_STYLEA(0,\"FontName\",'" + reader["FontName"] + "');");//定义字体
                    sb.Append("LODOP.SET_PRINT_STYLEA(0,\"FontSize\"," + reader["FontSize"] + ");");//定义字体大小
                    sb.Append("LODOP.SET_PRINT_STYLEA(0,\"Alignment\"," + reader["Alignment"] + ");");//定义对齐方式
                    sb.Append("LODOP.SET_PRINT_STYLEA(0,\"Bold\"," + reader["Bold"] + ");");//定义是否粗体
                    sb.Append("LODOP.SET_PRINT_STYLEA(0,\"Italic\"," + reader["Italic"] + ");");//定义是否斜体
                    sb.Append("LODOP.SET_PRINT_STYLEA(0,\"Underline\"," + reader["Underline"] + ");");//定义是否下划线
                }   
                if (reader["ParamType"].ToString().Equals("BarCode"))
                    sb.Append("LODOP.ADD_PRINT_BARCODE(" + reader["top"] + "," + reader["left"] + ", " + reader["width"] + ", " + reader["height"] + ", \"Code39\", '" + result + "');");//定义内容
            }
            else
                page.RegisterStartupScript("Print", "<script>alert('模板还没有创建，请联系管理员添加^_^');</script>");
            sb.Append("DisplayDesign();");
            sb.Append("}; ");
            sb.Append("</script>");
            page.RegisterClientScriptBlock("Print1", sb.ToString());
            page.RegisterStartupScript("Print2", "<script>MaintainHAWB();</script>");
        }

        /// <summary>
        /// 返回结果
        /// 维护界面返回KEY
        /// 非维护界面返回数据库处理后的value,也有可能是一个值
        /// </summary>
        /// <param name="key">用于维护显示</param>
        /// <param name="value">用于非维护显示，可能需要特殊处理</param>
        /// <param name="paramType">参数类型</param>
        /// <param name="groupName">组(主要区分文本和非文本)</param>
        /// <param name="identifyKey">标识符</param>
        /// <param name="operateType">操作类型 0-维护 1-非维护</param>
        /// <returns></returns>
        private string ResolveResult(string key, string value, string paramType,string groupName, string identifyKey, int operateType)
        {
            if (operateType != 0 && operateType != 1) throw new ArgumentException("OperateType Is Invalid!");
            //维护
            if (operateType == 0) return key;
            //非维护
            else
            {
                //文本类型/条形码类型，此时value就是SQL语句，需要进行执行
                if (paramType.Equals("Text") || paramType.Equals("BarCode"))
                {
                    //将SQL语句进行正则转化
                    value = Regex.Replace(value, "#key#", identifyKey);
                    SqlDataReader reader = Execution(value);
                    return ReadExecution(reader);
                }
                //日期或checkbox类型的数据
                else
                    return value;
            }
        }

        /// <summary>
        /// 操作数据库
        /// </summary>
        /// <param name="SQLStr">SQL语句</param>
        /// <returns></returns>
        private SqlDataReader Execution(string SQLStr)
        {
            return SqlHelper.ExecuteReader(CommandType.Text, SQLStr);
        }

        /// <summary>
        /// 解析SqlDataReader
        /// </summary>
        /// <param name="reader">SQLreader</param>
        /// <returns></returns>
        private string ReadExecution(SqlDataReader reader)
        {
            string result = string.Empty;
            while (reader.Read())
            {
                result = reader[0].ToString();
            }
            return result;
        }
    }
}
