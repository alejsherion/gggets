using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;

namespace GGGETSAdmin.CountryZiMaManage
{
    public partial class TwoZiMaAdd : System.Web.UI.Page
    {
        private CountryCode countrycode;
        private ICountryCodeManagementService _countryservice;
        private static string RRegion = @"^[A-Za-z]";
        protected TwoZiMaAdd()
        { }
        public TwoZiMaAdd(ICountryCodeManagementService countryservice)
        {
            _countryservice = countryservice;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            countrycode = new CountryCode();
            if (Txt_CountryCode.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('国家二字码不能为空！')</script>");
                Txt_CountryCode.Focus();
            }
            else if (!Regex.IsMatch(Txt_CountryCode.Text.Trim(), RRegion))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母！')</script>");
                Txt_CountryCode.Focus();
            }
            else if (txt_CountryName.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('国家名称不能为空！')</script>");
                txt_CountryName.Focus();
            }
            else if (!Regex.IsMatch(txt_CountryName.Text.Trim(), RRegion))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母！')</script>");
                txt_CountryName.Focus();
            }
            else
            {
                countrycode.CountryCode1 = Txt_CountryCode.Text.Trim().ToUpper();
                countrycode.CountryName = txt_CountryName.Text.Trim().ToUpper();
                _countryservice.AddCountryCode(countrycode);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('添加成功！')</script>");
                Txt_CountryCode.Text = string.Empty;
                txt_CountryName.Text = string.Empty;
            }

        }

        protected void But_Conel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Navigation.aspx");
        }
    }
}