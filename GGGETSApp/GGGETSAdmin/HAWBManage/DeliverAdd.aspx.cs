using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
namespace GGGETSAdmin.HAWBManage
{
    public partial class DeliverAdd : System.Web.UI.Page
    {
        
        protected static Regex RZipCode = new Regex(@"^\d{6}$");
        protected static Regex RTel = new Regex(@"^(\d{3,4}-)?\d{7,8}$");
        protected static Regex RTel1 = new Regex(@"^1[35]\d{9}$");
        protected static Regex RCountry = new Regex(@"^[A-Za-z]{2}$");
        protected static Regex RRegion = new Regex(@"^[A-Za-z]{3}$");
        protected static HAWB hawb;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["HAWB"] != null)
                {
                    hawb = (HAWB)Session["HAWB"];
                }
                else
                {
                    hawb = new HAWB();
                }
            }
        }

        protected void btn_AddDeliver_Click1(object sender, EventArgs e)
        {
            if (Txt_DeliverName.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('公司名称不能为空！')</script>");
                Txt_DeliverName.Focus();
            }
            else if (Txt_DeliverAddress.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('公司地址不能为空！')</script>");
                Txt_DeliverAddress.Focus();
            }
            else if (Txt_DeliverContactor.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('姓名不能为空！')</script>");
                Txt_DeliverContactor.Focus();
            }
            else if (Txt_DeliverTel.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('电话不能为空！')</script>");
                Txt_DeliverTel.Focus();
            }
            else
            {
                if (!RTel.IsMatch(Txt_DeliverTel.Text) && !RTel1.IsMatch(Txt_DeliverTel.Text))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('电话号码格式不正确！')</script>");
                    Txt_DeliverTel.Focus();
                }
                else if (Txt_DeliverZipCode.Text != "")
                {
                    if (!RZipCode.IsMatch(Txt_DeliverZipCode.Text))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('邮编格式不正确！')</script>");
                        Txt_DeliverZipCode.Focus();
                    }
                }
                else if (Txt_DeliverCountry.Text != "")
                {
                    if (!RCountry.IsMatch(Txt_DeliverContactor.Text))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母并为2位！')</script>");
                        Txt_DeliverCountry.Focus();
                    }
                }
                else if (Txt_DeliverRegion.Text != "")
                {
                    if (!RRegion.IsMatch(Txt_DeliverRegion.Text))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母并为3位！')</script>");
                        Txt_DeliverRegion.Focus();
                    }
                }
                else
                {
                    hawb.DeliverName = Txt_DeliverName.Text;
                    hawb.DeliverAddress = Txt_DeliverAddress.Text;
                    hawb.DeliverCountry = Txt_DeliverCountry.Text;
                    hawb.DeliverRegion = Txt_DeliverRegion.Text;
                    hawb.DeliverContactor = Txt_DeliverContactor.Text;
                    hawb.DeliverZipCode = Txt_DeliverZipCode.Text;
                    hawb.DeliverTel = Txt_DeliverTel.Text;
                    Session["HAWB"] = hawb;
                    Response.Write("<script>opener.location.href=opener.location.href;window.close();</script>");
                }
                
            }
            
        }

    }
}