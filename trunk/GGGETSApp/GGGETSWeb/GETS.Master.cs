//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				首页
// 子系统名		        首页母版页
// 作成者				ZhiWei.Shen
// 改版日				2011.04.27
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
    public partial class GETS : System.Web.UI.MasterPage
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
        /// Bind Selected lanuage in the webpage
        /// </summary>
        private void DynamicLanguage(string tempLanguage)
        {
            Language = tempLanguage;
            CultureInfo culture = new CultureInfo(Language);
            Thread.CurrentThread.CurrentUICulture = culture;
            //组织需要的语言页面路径
            string path = TransferPath(Request.Path);
            Server.Transfer(path);
        }

        /// <summary>
        /// 转化对应的语言页面
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string TransferPath(string path)
        {
            //模糊查询
            string newPath = VaguePath(path);
            switch(Language)
            {
                case "zh-cn":
                    break;
                case "ja-jp":
                    newPath = string.Format("{0}_JP.aspx", newPath.Substring(0, newPath.IndexOf(".aspx")));
                    break;
                case "en-us":
                    newPath = string.Format("{0}_US.aspx", newPath.Substring(0, newPath.IndexOf(".aspx")));
                    break;
            }
            return newPath;
        }

        /// <summary>
        /// 通过模糊查询获取第一个中文地址进行解析
        /// </summary>
        /// <returns></returns>
        private string VaguePath(string path)
        {
            if(path.Contains("Main"))//首页
            {
                path = "GETS_Main.aspx";
            }
            else if(path.Contains("Contact"))//联系我们
            {
                path = "GETS_Contact.aspx";
            }
            else if (path.Contains("Fee"))//服务费
            {
                path = "GETS_Fee.aspx";
            }
            else if (path.Contains("CompanyInfo"))//公司概要
            {
                path = "GETS_CompanyInfo.aspx";
            }
            else if (path.Contains("Treaty"))//条约
            {
                path = "GETS_Treaty.aspx";
            }
            else if (path.Contains("NetWork"))//营业网点
            {
                path = "GETS_NetWork.aspx";
            }
            else if (path.Contains("CompanyIdea"))//公司理念
            {
                path = "GETS_CompanyIdea.aspx";
            }
            else
            {
                path = "GETS_About.aspx";//关于我们
            }
            return path;
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

        #region Service Block
        /// <summary>
        /// 公司概要
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbInfo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Language)) Language = "zh-cn";
            
            switch(Language)
            {
                case "zh-cn":
                    Server.Transfer("~/GETS_CompanyInfo.aspx");
                    break;
                case "ja-jp":
                    Server.Transfer("~/GETS_CompanyInfo_JP.aspx");
                    break;
                case "en-us":
                    Server.Transfer("~/GETS_CompanyInfo_US.aspx");
                    break;
            }
        }

        /// <summary>
        /// 营业网络
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbNetwork_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Language)) Language = "zh-cn";
            //CultureInfo culture = new CultureInfo(Language);
            //Thread.CurrentThread.CurrentUICulture = culture;
            switch (Language)
            {
                case "zh-cn":
                    Server.Transfer("~/GETS_NetWork.aspx");
                    break;
                case "ja-jp":
                    Server.Transfer("~/GETS_NetWork_JP.aspx");
                    break;
                case "en-us":
                    Server.Transfer("~/GETS_NetWork_US.aspx");
                    break;
            }
        }

        /// <summary>
        /// 联系我们
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbContact_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Language)) Language = "zh-cn";
            //CultureInfo culture = new CultureInfo(Language);
            //Thread.CurrentThread.CurrentUICulture = culture;
            switch (Language)
            {
                case "zh-cn":
                    Server.Transfer("~/GETS_Contact.aspx");
                    break;
                case "ja-jp":
                    Server.Transfer("~/GETS_Contact_JP.aspx");
                    break;
                case "en-us":
                    Server.Transfer("~/GETS_Contact_US.aspx");
                    break;
            }
        }

        /// <summary>
        /// 关于我们
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbAbout_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Language)) Language = "zh-cn";
            //CultureInfo culture = new CultureInfo(Language);
            //Thread.CurrentThread.CurrentUICulture = culture;
            switch (Language)
            {
                case "zh-cn":
                    Server.Transfer("~/GETS_About.aspx");
                    break;
                case "ja-jp":
                    Server.Transfer("~/GETS_About_JP.aspx");
                    break;
                case "en-us":
                    Server.Transfer("~/GETS_About_US.aspx");
                    break;
            }
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbService_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Language)) Language = "zh-cn";
            //CultureInfo culture = new CultureInfo(Language);
            //Thread.CurrentThread.CurrentUICulture = culture;
            switch (Language)
            {
                case "zh-cn":
                    Server.Transfer("~/GETS_Main.aspx");
                    break;
                case "ja-jp":
                    Server.Transfer("~/GETS_Main_JP.aspx");
                    break;
                case "en-us":
                    Server.Transfer("~/GETS_Main_US.aspx");
                    break;
            }
        }

        /// <summary>
        /// 费用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbFee_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Language)) Language = "zh-cn";
            //CultureInfo culture = new CultureInfo(Language);
            //Thread.CurrentThread.CurrentUICulture = culture;
            switch (Language)
            {
                case "zh-cn":
                    Server.Transfer("~/GETS_Fee.aspx");
                    break;
                case "ja-jp":
                    Server.Transfer("~/GETS_Fee_JP.aspx");
                    break;
                case "en-us":
                    Server.Transfer("~/GETS_Fee_US.aspx");
                    break;
            }
        }

        /// <summary>
        /// 条约
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lblTreaty_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Language)) Language = "zh-cn";
            //CultureInfo culture = new CultureInfo(Language);
            //Thread.CurrentThread.CurrentUICulture = culture;
            switch (Language)
            {
                case "zh-cn":
                    Server.Transfer("~/GETS_Treaty.aspx");
                    break;
                case "ja-jp":
                    Server.Transfer("~/GETS_Treaty_JP.aspx");
                    break;
                case "en-us":
                    Server.Transfer("~/GETS_Treaty_US.aspx");
                    break;
            }
        }

        /// <summary>
        /// 公司理念
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbIdea_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Language)) Language = "zh-cn";
            switch (Language)
            {
                case "zh-cn":
                    Server.Transfer("~/GETS_CompanyIdea.aspx");
                    break;
                case "ja-jp":
                    Server.Transfer("~/GETS_CompanyIdea_JP.aspx");
                    break;
                case "en-us":
                    Server.Transfer("~/GETS_CompanyIdea_US.aspx");
                    break;
            }
        }
        #endregion

    }
}