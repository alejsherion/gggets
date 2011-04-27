//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				首页
// 子系统名		        实现国家化通用模板
// 作成者				ZhiWei.Shen
// 改版日				2011.04.27
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Globalization;
using System.Threading;

namespace GGGETSWeb
{
    public class CommonCulture : System.Web.UI.Page
    {
        /// <summary>
        /// Overriad International
        /// This Method will execute onInit And be execute again after click event 
        /// </summary>
        protected override void InitializeCulture()
        {
            var languageObj = Session["language"];
            if (languageObj != null)
            {
                CultureInfo culture = new CultureInfo(Convert.ToString(languageObj));
                Thread.CurrentThread.CurrentUICulture = culture;
            }

            base.InitializeCulture();
        }
    }
}