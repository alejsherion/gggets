using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.GGETS.TransManagement;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace GGGETSWeb.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }

        ITransManagementService transManagementService;

        protected Login()
        {
        }

        public Login(ITransManagementService transManagementService)
        {
            this.transManagementService = transManagementService;
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            Trans trans = new Trans();
            trans.TransId = Guid.NewGuid();
            trans.IsCustom = 1;
            trans.ItemsType = 1;
            trans.ItemsVolumn = 30;
            trans.ItemsWeight = 5;
            trans.Receiver = "wuyan";
            trans.ReceiverAddress = "北京东路201号408室";
            trans.ReceiverAreaCode = 2;
            trans.ReceiverPhone = "13818095080";
            trans.Remark = "这是测试代码";
            trans.Sender = "wuyan";
            trans.SenderAddress = "南京东路201号1008室";
            trans.SenderPostCode = "201203";
            trans.Status = 0;
            this.transManagementService.AddTrans(trans);
        }
    }
}
