//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				首页
// 子系统名		        首页
// 作成者				ZhiWei.Shen
// 改版日				2011.04.13
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GGGETSWeb
{
    public partial class GETSIndex : System.Web.UI.Page
    {
        /// <summary>
        /// define a internation encode;
        /// </summary>
        public string Language
        {
            get { return Convert.ToString(Session["language"]); }
            set { Session["language"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Overriad International
        /// This Method will execute onInit And be execute again after click event 
        /// </summary>
        protected override void InitializeCulture()
        {
            if (!string.IsNullOrEmpty(Language))
            {
                CultureInfo culture = new CultureInfo(Language);
                Thread.CurrentThread.CurrentUICulture = culture;
            }
            
            base.InitializeCulture();
            //ScriptManager.RegisterStartupScript(this, GetType(), "", "BindUrl();", true);//根据国际化绑定对应地址
        }

        /// <summary>
        /// Bind Selected lanuage in the webpage
        /// </summary>
        private void DynamicLanguage(string tempLanguage)
        {
            Language = tempLanguage;
            CultureInfo culture = new CultureInfo(Language);
            Thread.CurrentThread.CurrentUICulture = culture;
            Server.Transfer(Request.Path);
        }

        #region International Block
        /// <summary>
        /// China International
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbCN_Click(object sender, EventArgs e)
        {
            DynamicLanguage("zh-cn");//china
        }

        /// <summary>
        /// Japan International
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbJP_Click(object sender, EventArgs e)
        {
            DynamicLanguage("ja-jp");//japan
        }

        /// <summary>
        /// USA International
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbUSA_Click(object sender, EventArgs e)
        {
            DynamicLanguage("en-us");//USA
        }
        #endregion
    }
}