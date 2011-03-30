using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;

namespace GGGETSAdmin.RegionZiMaManage
{
    public partial class regionAdd : System.Web.UI.Page
    {
        private RegionCode regioncode=new RegionCode();
        protected IRegionCodeManagementService _regionservice;
        private static ICountryCodeManagementService _countryservice;
        private ISysUserManagementService _sysUserManagementService;
        private static string RRegion = @"^[A-Za-z]";
        protected regionAdd()
        { }
        public regionAdd(IRegionCodeManagementService regionservice, ICountryCodeManagementService countryservice, ISysUserManagementService sysUserManagementService)
        {
            _regionservice = regionservice;
            _countryservice = countryservice;
            _sysUserManagementService = sysUserManagementService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] != null)
                {
                    Guid id = (Guid)Session["UserID"];
                    ModulePrivilege Mprivilege = _sysUserManagementService.GetPrivilegeByUserid(id);
                    if (!(bool)Mprivilege[Privilege.添加.ToString()])
                    {
                        btn_Add.Enabled = false;
                    }
                }
            }
        }
        /// <summary>
        /// 国家码添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Add_Click(object sender, EventArgs e)
        {
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
            else if (txt_RegionCode.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('地区三字码不能为空！')</script>");
                txt_RegionCode.Focus();
            }
            else if (!Regex.IsMatch(txt_RegionCode.Text.Trim(), RRegion))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母！')</script>");
                txt_RegionCode.Focus();
            }
            else if (txt_RegionName.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('地区名称不能为空！')</script>");
                txt_RegionName.Focus();
            }
            else if (!Regex.IsMatch(txt_RegionName.Text.Trim(), RRegion))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母！')</script>");
                txt_RegionName.Focus();
            }
            else
            {
                regioncode.CountryCode = Txt_CountryCode.Text.Trim().ToUpper();
                regioncode.RegionCode1 = txt_RegionCode.Text.Trim().ToUpper();
                regioncode.RegionName = txt_RegionName.Text.Trim().ToUpper();
                regioncode.ID = _regionservice.GetIdentifyID();
                _regionservice.AddRegionCode(regioncode);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('添加成功！')</script>");
                Txt_CountryCode.Text = string.Empty;
                txt_RegionCode.Text = string.Empty;
                txt_RegionName.Text = string.Empty;
            }
        }
        /// <summary>
        /// 国家码填充
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[][] GetCountryList(string prefixText, int count)
        {
            if (count == 0)
            {
                count = 10;
            }

            List<string[]> items = new List<string[]>();

            IList<CountryCode> countrycode = _countryservice.FindCountriedByCountryName(prefixText);//获取国家二字码
            foreach (CountryCode country in countrycode)
            {
                string[] ItemArry = new string[3];
                ItemArry[0] = country.CountryName;
                ItemArry[1] = country.CountryCode1;
                items.Add(ItemArry);
            }
            return items.Take(count).ToArray();
        }

        protected void autocomplete_ItemSelected(object sender, EventArgs e)
        {
            Txt_CountryCode.Text = autocomplete.SelectedValue;
        }

        protected void But_Conel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Navigation.aspx");
        }

    }
}