using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace GGGETSAdmin.HAWB
{
    public partial class AddItem : System.Web.UI.Page
    {
        List<Item> lt = new List<Item>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.UrlReferrer != null)
                {
                    ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
                }
                else
                {
                    Response.Redirect("HAWBManagement.aspx");
                }
            }
        }
        protected void But_AddItem_Click(object sender, EventArgs e)
        {
            Item item = new Item();
            item.Id = Guid.NewGuid();
            item.Height = decimal.Parse(Txt_ItemHeight.Text);
            item.Length = decimal.Parse(Txt_ItemLength.Text);
            if (Txt_ItemTransCurrency.Text == "")
            {
                item.TransCurrency = 0;
            }
            else
                item.TransCurrency = int.Parse(Txt_ItemTransCurrency.Text);
            if (Txt_ItemTransPays.Text == "")
            {
                item.TransPays = decimal.Parse("0");
            }
            else
            {
                item.TransPays = decimal.Parse(Txt_ItemTransPays.Text);
            }
            item.Weight = decimal.Parse(Txt_ItemWeight.Text);
            item.Width = decimal.Parse(Txt_ItemWidth.Text);
            Session["jilu"] = "1";
            Session["item"] = item; ;
            Response.Redirect((string)ViewState["UrlReferrer"]);
        }

        protected void But_Rurnet_Click(object sender, EventArgs e)
        {
            if (ViewState["UrlReferrer"] != null)
            {
                Response.Redirect((string)ViewState["UrlReferrer"]);
            }
            else
            {
                Response.Redirect("HAWBManagement.aspx");
            }
        }
    }
}