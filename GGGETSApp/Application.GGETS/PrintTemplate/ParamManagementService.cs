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
using System.Reflection;
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

        private ITemplateRepository _templateRepository;

        public ParamManagementService(IParamRepository paramRepository, ITemplateRepository templateRepository)
        {
            _paramRepository = paramRepository;
            _templateRepository = templateRepository;
        }

        #region database
        /// <summary>
        /// 通过TID获取参数集合
        /// </summary>
        /// <param name="TID">参数序号</param>
        /// <returns></returns>
        public IList<Param> FindParamsByTID(string TID)
        {
            return _paramRepository.FindParamsByTID(TID);
        }

        /// <summary>
        /// 通过TID获取参数对象
        /// </summary>
        /// <param name="TID">参数序号</param>
        /// <returns></returns>
        public Param FindParamByTID(string TID)
        {
            return _paramRepository.FindParamByTID(TID);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="param"></param>
        public void ModifyParam(Param param)
        {
            if (param == null)
                throw new ArgumentNullException("Param is null");
            IUnitOfWork unitOfWork = _paramRepository.UnitOfWork;
            _paramRepository.Modify(param);
            //complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="param"></param>
        public void RemoveParam(Param param)
        {
            if (param == null)
                throw new ArgumentNullException("Param is null");
            IUnitOfWork unitOfWork = _paramRepository.UnitOfWork;
            _paramRepository.Remove(param);
            unitOfWork.Commit();
        }

        public Param FindParamByTIDAndTag(string TID, int Tag)
        {
            return _paramRepository.FindParamByTIDAndTag(TID, Tag);
        }

        #endregion

        /// <summary>
        /// 套打通用方法
        /// </summary>
        /// <param name="intOrient">intOrient：1-纵(正)向打印 2-横向打印 3-纵(正)向打印，宽度固定，高度按打印内容的高度自适应 0或其它-默认打印方向</param>
        /// <param name="intPageWidth">纸张宽度：毫米,0.1mm</param>
        /// <param name="intPageHeight">纸张高度：毫米,0.1mm</param>
        /// <param name="strPageName">纸张名：Letter, LetterSmall, Tabloid, Ledger, Legal,Statement, Executive, A3, A4, A4Small, A5, B4, B5, Folio, Quarto, qr10X14, qr11X17, Env9, Env10, Env11, Env12,Env14, Sheet, DSheet, ESheet</param>
        /// <param name="identifyKey">标识列，唯一区分对象的序号，目前为运单序号HID,如果是批处理时，将会是一串逗号分隔字符串</param>
        /// <param name="templateCode">模板编号，唯一区分对象的序号</param>
        /// <param name="batchHeight">用于批量套打时使用，控制页面循环高度</param>
        /// <param name="operateType">操作类型，是维护还是非维护 0-维护 1-非维护</param>
        /// <param name="page">页面对象</param>
        public void PrintHAWB(int intOrient, int intPageWidth, int intPageHeight, string strPageName, string identifyKey, string templateCode, int batchHeight, int operateType, Page page)
        {
            if (string.IsNullOrEmpty(strPageName)) strPageName = "";
            Template template = _templateRepository.FindTemplateByTemplateCode(templateCode);
            string SQLStr = string.Format("select * from param where TID='{0}' order by Tag", template.TID);

            StringBuilder sb = new StringBuilder();
            sb.Append("<script lanuage=javascript>");
            sb.Append("function Print() {");
            sb.Append("CreatePage(" + intOrient + "," + intPageWidth + "," + intPageHeight + ",'" + strPageName + "');");

            //读取模板参数
            SqlDataReader reader = Execution(SQLStr);
            if (reader.HasRows)
                while (reader.Read())
                {
                    //搜集参数信息....................................................................................
                    string key = Convert.ToString(reader["Key"]);//用于维护时，显示字段说明
                    string value = Convert.ToString(reader["Value"]);//用于非维护时，绑定打印值,需要解析这个SQL语句
                    string paramType = Convert.ToString(reader["ParamType"]);//参数类型：文本，条形码以及单选框
                    int top = Convert.ToInt32(reader["top"]);//上边距
                    int left = Convert.ToInt32(reader["left"]);//左边距
                    int width = Convert.ToInt32(reader["width"]);//打印控件宽度
                    int height = Convert.ToInt32(reader["height"]);//打印控件高度
                    string fontName = Convert.ToString(reader["FontName"]);//字体名称
                    int fontSize = Convert.ToInt32(reader["FontSize"]);//字体大小
                    int aligment = Convert.ToInt32(reader["Alignment"]);//对齐方式
                    int bold = Convert.ToInt32(reader["Bold"]);//是否加粗
                    int italic = Convert.ToInt32(reader["Italic"]);//是否斜体
                    int underLine = Convert.ToInt32(reader["Underline"]);//是否下划线
                    int horizontalRange = Convert.ToInt32(reader["HorizontalRange"]);//用于特殊类型，主要用于单选框的定位
                    int verticalRange = Convert.ToInt32(reader["VerticalRange"]);//同上
                    //搜集完成..........................................................................................

                    //处理上边距和左边距(默认都是0，只有当类型为单选框时，需要取当前对象的value与其的乘积)...................
                    if(paramType=="RadioBox" || paramType=="CheckBox")
                    {
                        top = top + Convert.ToInt32(value)*verticalRange;
                        left = left + Convert.ToInt32(value)*horizontalRange;
                    }
                    //这里逻辑太独立了，只能这么来处理
                    if (paramType == "SType" && operateType==1)
                    {
                        string output = ResolveResult(key, value, paramType, identifyKey, 1);//获取KEY对应的VALUE值
                        if(output!="0")
                        {
                            left = left + horizontalRange;
                        }
                    }
                    if(paramType=="STType" && operateType==1)
                    {
                        string output = ResolveResult(key, value, paramType, identifyKey, 1);//获取KEY对应的VALUE值
                        if(output=="2")
                        {
                            left = left + horizontalRange;
                        }
                        if(output=="1" || output=="3")
                        {
                            left = left + horizontalRange*2;
                        }
                    }
                    //处理完毕..........................................................................................

                    //开始处理对象，可能是一个，也可能是多个(批处理)......................................................
                    if(identifyKey.Contains(",") && operateType==1)
                    {
                        string[] identifykeys = identifyKey.Split(new char[] { ',' });
                        int count = 0;//计数器
                        foreach(string identifykeyTemp in identifykeys)
                        {
                            int newTop = top + count*batchHeight;//批处理高度的解决方案

                            //获取打印输出值
                            string output = ResolveResult(key, value, paramType, identifykeyTemp, operateType);
                            //控制打印
                            if (paramType.Equals("Text") || paramType.Equals("SType") || paramType.Equals("STType"))
                            {
                                if (paramType.Equals("SType") || paramType.Equals("STType")) 
                                    output = "√";
                                sb.Append("LODOP.ADD_PRINT_TEXT(" + newTop + "," + left + ", " + width + ", " + height + ", '" + output + "');");//定义内容
                                sb.Append("LODOP.SET_PRINT_STYLEA(0,\"FontName\",'" + fontName + "');");//定义字体
                                sb.Append("LODOP.SET_PRINT_STYLEA(0,\"FontSize\"," + fontSize + ");");//定义字体大小
                                sb.Append("LODOP.SET_PRINT_STYLEA(0,\"Alignment\"," + aligment + ");");//定义对齐方式
                                sb.Append("LODOP.SET_PRINT_STYLEA(0,\"Bold\"," + bold + ");");//定义是否粗体
                                sb.Append("LODOP.SET_PRINT_STYLEA(0,\"Italic\"," + italic + ");");//定义是否斜体
                                sb.Append("LODOP.SET_PRINT_STYLEA(0,\"Underline\"," + underLine + ");");//定义是否下划线
                            }
                            if (paramType.Equals("BarCode"))
                                sb.Append("LODOP.ADD_PRINT_BARCODE(" + newTop + "," + left + ", " + width + ", " + height + ", \"Code39\", '" + output + "');");//定义内容
                            if (paramType.Equals("Textarea"))
                            {
                                string strDivStyle = "<style>span {word-break:keep-all;word-wrap:break-word;white-space:normal}</style>";
                                var strDivHtml = strDivStyle + "<span>" + output + "</span>";
                                sb.Append("LODOP.ADD_PRINT_HTM(" + newTop + "," + left + ", " + width + ", " + height + ", '" + strDivHtml + "');");//定义内容
                                sb.Append("LODOP.SET_PRINT_STYLEA(0,\"FontName\",'" + fontName + "');");//定义字体
                                sb.Append("LODOP.SET_PRINT_STYLEA(0,\"FontSize\"," + fontSize + ");");//定义字体大小
                                sb.Append("LODOP.SET_PRINT_STYLEA(0,\"Alignment\"," + aligment + ");");//定义对齐方式
                                sb.Append("LODOP.SET_PRINT_STYLEA(0,\"Bold\"," + bold + ");");//定义是否粗体
                                sb.Append("LODOP.SET_PRINT_STYLEA(0,\"Italic\"," + italic + ");");//定义是否斜体
                                sb.Append("LODOP.SET_PRINT_STYLEA(0,\"Underline\"," + underLine + ");");//定义是否下划线
                            }
                               
                            count++;
                        }
                        
                    }
                    else
                    {
                        //获取打印输出值
                        string output = ResolveResult(key, value, paramType, identifyKey, operateType);
                        //控制打印
                        if (paramType.Equals("Text") || paramType.Equals("SType") || paramType.Equals("STType"))
                        {
                            if (paramType.Equals("SType") || paramType.Equals("STType")) 
                                output = "√";
                            sb.Append("LODOP.ADD_PRINT_TEXT(" + top + "," + left + ", " + width + ", " + height + ", '" + output + "');");//定义内容
                            sb.Append("LODOP.SET_PRINT_STYLEA(0,\"FontName\",'" + fontName + "');");//定义字体
                            sb.Append("LODOP.SET_PRINT_STYLEA(0,\"FontSize\"," + fontSize + ");");//定义字体大小
                            sb.Append("LODOP.SET_PRINT_STYLEA(0,\"Alignment\"," + aligment + ");");//定义对齐方式
                            sb.Append("LODOP.SET_PRINT_STYLEA(0,\"Bold\"," + bold + ");");//定义是否粗体
                            sb.Append("LODOP.SET_PRINT_STYLEA(0,\"Italic\"," + italic + ");");//定义是否斜体
                            sb.Append("LODOP.SET_PRINT_STYLEA(0,\"Underline\"," + underLine + ");");//定义是否下划线
                        }
                        if (paramType.Equals("BarCode"))
                            sb.Append("LODOP.ADD_PRINT_BARCODE(" + top + "," + left + ", " + width + ", " + height + ", \"Code39\", '" + output + "');");//定义内容
                        if (paramType.Equals("Textarea"))
                        {
                            string strDivStyle = "<style>span {word-break:keep-all;word-wrap:break-word;white-space:normal}</style>";
                            var strDivHtml = strDivStyle + "<span>" + output + "</span>";
                            sb.Append("LODOP.ADD_PRINT_HTM(" + top + "," + left + ", " + width + ", " + height + ", '" + strDivHtml + "');");//定义内容
                            sb.Append("LODOP.SET_PRINT_STYLEA(0,\"FontName\",'" + fontName + "');");//定义字体
                            sb.Append("LODOP.SET_PRINT_STYLEA(0,\"FontSize\"," + fontSize + ");");//定义字体大小
                            sb.Append("LODOP.SET_PRINT_STYLEA(0,\"Alignment\"," + aligment + ");");//定义对齐方式
                            sb.Append("LODOP.SET_PRINT_STYLEA(0,\"Bold\"," + bold + ");");//定义是否粗体
                            sb.Append("LODOP.SET_PRINT_STYLEA(0,\"Italic\"," + italic + ");");//定义是否斜体
                            sb.Append("LODOP.SET_PRINT_STYLEA(0,\"Underline\"," + underLine + ");");//定义是否下划线
                        }
                    }
                }
            else
                page.RegisterStartupScript("", "<script>alert('模板还没有创建，请联系管理员添加^_^');</script>");
           
            sb.Append("DisplayDesign();");
            sb.Append("}; ");
            sb.Append("</script>");
            page.RegisterClientScriptBlock("", sb.ToString());
            page.RegisterStartupScript("", "<script>Print();</script>");
        }

        /// <summary>
        /// 新增参数
        /// </summary>
        /// <param name="param">参数</param>
        public void AddParam(Param param)
        {
            if (param == null)
                throw new ArgumentNullException("Param is null");
            IUnitOfWork unitOfWork = _paramRepository.UnitOfWork;
            _paramRepository.Add(param);
            //complete changes in this unit of work
            unitOfWork.Commit();
        }

        /// <summary>
        /// 返回结果
        /// 维护界面返回KEY
        /// 非维护界面返回数据库处理后的value,也有可能是一个值
        /// </summary>
        /// <param name="key">用于维护显示</param>
        /// <param name="value">用于非维护显示，可能需要特殊处理</param>
        /// <param name="paramType">参数类型</param>
        /// <param name="identifyKey">标识符</param>
        /// <param name="operateType">操作类型 0-维护 1-非维护</param>
        /// <returns></returns>
        private string ResolveResult(string key, string value, string paramType, string identifyKey, int operateType)
        {
            if (operateType != 0 && operateType != 1) throw new ArgumentException("OperateType Is Invalid!");
            //维护
            if (operateType == 0) return key;
            //非维护
            else
            {
                //文本类型/条形码类型，此时value就是SQL语句，需要进行执行
                if (paramType.Equals("Text") || paramType.Equals("BarCode") || paramType.Equals("Textarea") || paramType.Equals("SType") || paramType.Equals("STType"))
                {
                    //将SQL语句进行正则转化
                    value = Regex.Replace(value, "#key#", identifyKey);
                    SqlDataReader reader = Execution(value);
                    if (reader.FieldCount > 1)
                        return ReadExecutions(reader);
                    else
                        return ReadExecution(reader);
                }
                //日期或CheckBox和RadioBox类型的数据,值可能就是一个特殊圆圈
                else
                    return key;
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
            StringBuilder result = new StringBuilder();
            while (reader.Read())
            {
                result.Append(reader[0].ToString());
            }
            return result.ToString();
        }

        /// <summary>
        /// 解析SqlDataReader多条结果集
        /// </summary>
        /// <param name="reader">SQLreader</param>
        /// <returns></returns>
        private string ReadExecutions(SqlDataReader reader)
        {
            StringBuilder result = new StringBuilder();
            while (reader.Read())
            {
                result.Append(reader[0].ToString());
                result.Append(" ");
                if (reader[1] != null)
                {
                    result.Append(reader[1].ToString());
                    result.Append("件");
                }
                result.Append(@"\n");
            }
            return result.ToString().Substring(0, result.Length - 2);
        }
    }
}
