using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace GGGETSWeb.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }

        protected Login()
        {
        }


        //public Login(ITransManagementService transManagementService)
        //{
        //    this.transManagementService = transManagementService;
        //}

        protected void LoginButton_Click(object sender, EventArgs e)
        {
           
        }
    }
}
