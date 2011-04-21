//************************************************************************
// 用户名				GETS国际综合快递
// 系统名				管理后台
// 子系统名		        操作COOKIE
// 作成者				ZhiWei.Shen
// 改版日				2011.04.18
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GGGETSAdmin.Common
{
    public class CookiesHelp
    {
        /// <summary>
        /// 添加cookies值
        /// </summary>
        /// <param name="cookiesName">名字</param>
        /// <param name="value">值</param>
        public static void Add(string cookiesName, string value)
        {
            if (String.IsNullOrEmpty(cookiesName) || String.IsNullOrEmpty(value)) return;
            var oCookie = HttpContext.Current.Request.Cookies[cookiesName];
            if (oCookie != null)
            {
                var CurrentValue = oCookie.Value;
                CurrentValue = HttpUtility.UrlDecode(CurrentValue);
                if (CurrentValue.IndexOf(value) == -1)
                {
                    var Temp = HttpUtility.UrlEncode(value);
                    if (!String.IsNullOrEmpty(Temp))
                    {
                        Temp = Temp.Replace("+", "%20");
                        oCookie.Value = oCookie.Value + "," + Temp;
                    }

                }

            }
            else
            {
                var Temp = HttpUtility.UrlEncode(value);
                if (!String.IsNullOrEmpty(Temp))
                {
                    Temp = Temp.Replace("+", "%20");
                    oCookie = new HttpCookie(cookiesName) { Value = Temp };
                }

            }
            if (oCookie != null)
                HttpContext.Current.Response.Cookies.Add(oCookie);
        }

        /// <summary>
        /// 读取Cookies值
        /// </summary>
        /// <param name="cookiesName">名字</param>
        /// <returns></returns>
        public static string Read(string cookiesName)
        {
            if (String.IsNullOrEmpty(cookiesName)) return "";
            var oCookie = HttpContext.Current.Request.Cookies[cookiesName];
            if (oCookie != null)
            {
                var CurrentValue = HttpUtility.HtmlEncode(oCookie.Value);
                return CurrentValue;
            }
            return "";
        }
    }
}