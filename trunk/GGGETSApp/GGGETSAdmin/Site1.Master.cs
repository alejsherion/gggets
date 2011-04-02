using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace GGGETSAdmin
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void SetLanguage(string languageType)
        {
            Session["LanType"] = languageType;
            CultureInfo culture = new CultureInfo(languageType);
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            Server.Transfer(Request.Path);
        }

        protected void btn_China_Click(object sender, EventArgs e)
        {
            SetLanguage("zh-cn");
        }

        protected void btn_English_Click(object sender, EventArgs e)
        {
            SetLanguage("en-us");
        }

        protected void lbtn_Navigation_Click(object sender, EventArgs e)
        {
            Session.Remove("HAWB");
            //string url = Request.UrlReferrer.ToString();
            Response.Redirect("../Menu.aspx");
        }
    }
}