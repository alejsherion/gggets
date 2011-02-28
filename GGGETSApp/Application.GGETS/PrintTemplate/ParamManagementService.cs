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
using System.Linq;
using System.Text;
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
        IParamRepository _paramRepository;
        public ParamManagementService(IParamRepository paramRepository)
        {
            _paramRepository = paramRepository;
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
        /// <param name="page">页面对象</param>
        public void MaintainDan(int intOrient, int intPageWidth, int intPageHeight, string strPageName, Page page)
        {
            HttpContext.Current.Response.Write("<script lanuage=javascript>");
            HttpContext.Current.Response.Write("function MaintainHAWB() {");
            HttpContext.Current.Response.Write("LODOP.SET_PRINT_PAGESIZE(" + intOrient + "," + intPageWidth + "," + intPageHeight + ",'" + strPageName + "');");//控制纸张大小和打印方向

            //HttpContext.Current.Response.Write("LODOP.ADD_PRINT_TEXT(31,672, 100, 20, '" + rootList[0].InnerText + "');");//条形码
        }
    }
}
