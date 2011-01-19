using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace GGGETSAdmin.HAWB1
{
    public partial class AddItem1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
            }
        }

        protected void But_AddItme_Click(object sender, EventArgs e)
        {
            Item item = new Item();
            item.Id = Guid.NewGuid();
            item.Height = decimal.Parse(Txt_ItemHeight.Text);
            item.Length = decimal.Parse(Txt_ItemLength.Text);
            item.TransCurrency = int.Parse(Txt_ItemTransCurrency.Text);
            item.TransPays = decimal.Parse(Txt_ItemTransPays.Text);
            item.Weight = decimal.Parse(Txt_ItemWeight.Text);
            item.Width = decimal.Parse(Txt_ItemWidth.Text);
            Session["jilu"] = "1";
            Session["item"] = item;
            string url = (string)ViewState["UrlReferrer"];
            Response.Redirect(url);
        }
    }
}