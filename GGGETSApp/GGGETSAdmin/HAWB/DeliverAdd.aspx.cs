using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
namespace GGGETSAdmin.HAWB
{
    public partial class DeliverAdd : System.Web.UI.Page
    {
        //ETS.GGGETSApp.Domain.Application.Entities HAWB hawb = new HAWB();
        protected static Regex RZipCode = new Regex(@"\d{6}$");
        protected static Regex RTel = new Regex(@"\d{3,4}-\d{7,8}-[0-9]*$|^[0-9]*$|\d{3,4}-\d{7,8}$|\d{13}$");
        protected static Regex RCountry = new Regex(@"^[a-zA-Z]{2}$");
        protected static Regex RRegion = new Regex(@"^[a-zA-Z]{3}$");
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        void btn_AddDeliver_Click(object sender, EventArgs e)
        {
            if (Txt_DeliverName.Text == "")
            {
                Response.Write("<script>alert('公司名称不能为空！')</script>");
            }
            else if (Txt_DeliverAddress.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('公司地址不能为空！')</script>");
            }
            else if (Txt_DeliverContactor.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('姓名不能为空！')</script>");
            }
            else if (Txt_DeliverTel.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('电话不能为空！')</script>");
            }
            else if (Txt_DeliverTel.Text != "")
            {
                if (!RTel.Equals(Txt_DeliverTel.Text))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('电话号码格式不正确！')</script>");
                }
            }
            else if (Txt_DeliverZipCode.Text != "")
            {
                if (!RZipCode.Equals(Txt_DeliverZipCode.Text))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('邮编格式不正确！')</script>");
                }
            }
            else if (Txt_DeliverCountry.Text != "")
            {
                if (!RCountry.Equals(Txt_DeliverCountry.Text))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母！')</script>");
                }
            }
            else if (Txt_DeliverRegion.Text != "")
            {
                if (!RRegion.Equals(Txt_DeliverRegion.Text))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母！')</script>");
                }
            }
            else
            {
 
            }
        }

    }
}