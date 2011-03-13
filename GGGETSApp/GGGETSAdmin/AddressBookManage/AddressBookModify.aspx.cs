﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;

namespace GGGETSAdmin.AddressBookManage
{
    public partial class AddressBookModify : System.Web.UI.Page
    {
        private static Regex RTel = new Regex(@"^[0-9]*$");
        private static Regex RRegion = new Regex(@"^[A-Za-z]");
        private IDepartmentManagementService _departmentservice;
        private IAddressBookManagementService _addressbookservice;
        private static ICountryCodeManagementService _countryservice;
        private static IRegionCodeManagementService _regionservice;
        private IUserManagementService _userService;
        private ICompanyManagementService _companyService;
        private string AID;
        private AddressBook address;
        private Department depar;
        private ETS.GGGETSApp.Domain.Application.Entities.User user;
        protected AddressBookModify()
        { }
        public AddressBookModify(ICompanyManagementService companyservice, ICountryCodeManagementService countryservice, IRegionCodeManagementService regionservice, IDepartmentManagementService departmentservice, IAddressBookManagementService addressbookservice, IUserManagementService userservice)
        {
            _departmentservice = departmentservice;
            _addressbookservice = addressbookservice;
            _countryservice = countryservice;
            _regionservice = regionservice;
            _userService = userservice;
            _companyService = companyservice;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["AID"]))
                {
                    AID = Request.QueryString["AID"].ToString();
                    //ViewState["Url"] = Request.UrlReferrer.ToString();
                    Storage(AID);
                }
                else
                {
                    Response.Redirect("../../Navigation.aspx");
                }
            }
        }
        protected void Storage(string AID)
        {
            address = _addressbookservice.FindAddressBookByAID(AID);
            ddl_AddressBookType.SelectedValue = address.AddressType.ToString();
            if (address.DID != null)
            {
                Department depar = _departmentservice.FindDepartmentByDID(address.DID.ToString());
                if (depar != null)
                {
                    Txt_CompanyCode.Text = depar.CompanyCode;
                    Txt_Code.Text = depar.DepCode;
                }
            }
            if (address.UID != null)
            {
                ETS.GGGETSApp.Domain.Application.Entities.User user = _userService.FindUserByUID(address.UID.ToString());
                if (user != null)
                {
                    Txt_LoginName.Text = user.LoginName;
                }
            }
            ddl_AddressBookType.SelectedValue = address.AddressType.ToString();
            Txt_DeliverName.Text = address.Name;
            Txt_DeliverAddress.Text = address.Address;
            Txt_DeliverCountry.Text = CountrySwitch(address.CountryCode, "");
            Txt_DeliverRegion.Text = RegionSwitch(address.RegionCode, "");
            Txt_DeliverProvince.Text = address.Provience;
            Txt_DeliverZipCode.Text = address.PostCode;
            Txt_DeliverContactor.Text = address.ContactorName;
            Txt_DeliverTel.Text = address.Phone;
            Session["Address"] = address;

        }
        protected string CountrySwitch(string countryname,string Countype)
        {
            string country = string.Empty;
            IList<CountryCode> Countrycode = _countryservice.FindAllCountries();
            if (Countype == "store")
            {
                foreach (CountryCode countrycode in Countrycode)
                {
                    if (countrycode.CountryName == countryname)
                    {
                        country = countrycode.CountryCode1;
                        break;
                    }
                }
            }
            else
            {
                foreach (CountryCode countrycode in Countrycode)
                {
                    if (countrycode.CountryCode1 == countryname)
                    {
                        country = countrycode.CountryName;
                        break;
                    }
                }
            }
            return country.ToUpper();
        }
        protected string RegionSwitch(string regionname,string regtype)
        {
            string region = string.Empty;
            IList<RegionCode> Regioncode = _regionservice.FindAllRegionCodes();
            if (regtype == "store")
            {
                foreach (RegionCode regioncode in Regioncode)
                {
                    if (regioncode.RegionName == regionname)
                    {
                        region = regioncode.RegionCode1;
                        break;
                    }
                }
            }
            else
            {
                foreach (RegionCode regioncode in Regioncode)
                {
                    if (regioncode.RegionCode1 == regionname)
                    {
                        region = regioncode.RegionName;
                        break;
                    }
                }
            }
            return region.ToUpper();
        }

        protected void Txt_CompanyCode_TextChanged(object sender, EventArgs e)
        {
            if (Txt_CompanyCode.Text.Trim() != "")
            {
                Company company = _companyService.FindCompanyByCompanyCode(Txt_CompanyCode.Text.Trim());
                if (company == null)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('没有该公司，请重新输入！')</script>");
                    Txt_CompanyCode.Focus();
                    Txt_CompanyCode.Text = "";
                }
                else
                {
                    Txt_Code.Focus();
                }
            }
        }

        protected void Txt_Code_TextChanged(object sender, EventArgs e)
        {
            if (Txt_CompanyCode.Text.Trim() != "")
            {
                if (Txt_Code.Text.Trim() != "")
                {
                    depar = _departmentservice.FindDepartmentByDepCodeAndCompanyCode(Txt_Code.Text.Trim(), Txt_CompanyCode.Text.Trim());
                    if (depar == null)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('没有该部门账号，请重新输入！')</script>");
                        Txt_Code.Focus();
                        Txt_Code.Text = "";
                    }
                    else
                    {
                        Txt_LoginName.Focus();
                        Session["Depar"] = depar.DID;
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请重新输入部门账号！')</script>");
                    Txt_Code.Focus();
                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请重新输入公司账号！')</script>");
                Txt_CompanyCode.Focus();
            }
        }
        protected void Txt_LoginName_TextChanged(object sender, EventArgs e)
        {
            if (Txt_LoginName.Text.Trim() != "")
            {
                user = _userService.FindUserByLoginName(Txt_LoginName.Text.Trim());
                if (user == null)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('没有该用户名，请重新输入！')</script>");
                    Txt_LoginName.Focus();
                    Txt_LoginName.Text = "";
                }
                else
                {
                    Session["User"] = user.UID;
                    Txt_DeliverName.Focus();
                }
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[][] GetCountryList(string prefixText, int count)
        {
            if (count == 0)
            {
                count = 10;
            }

            List<string[]> items = new List<string[]>();

            IList<CountryCode> countrycode = _countryservice.FindCountriedByCountryName(prefixText);
            foreach (CountryCode country in countrycode)
            {
                string[] ItemArry = new string[3];
                ItemArry[0] = country.CountryName;
                ItemArry[1] = country.CountryCode1;
                items.Add(ItemArry);
            }
            return items.Take(count).ToArray();
        }


        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[][] GetRegionList(string prefixText, int count, string contextKey)
        {
            if (count == 0)
            {
                count = 10;
            }
            if (string.IsNullOrEmpty(contextKey))
            {
                return new string[0][];
            }
            List<string[]> items = new List<string[]>();

            IList<RegionCode> regioncode = _regionservice.FindRegionsByCountryCodeAndRegionName(prefixText, contextKey);
            foreach (RegionCode region in regioncode)
            {
                string[] ItemArry = new string[2];
                ItemArry[0] = region.RegionName;
                ItemArry[1] = region.RegionCode1;

                items.Add(ItemArry);
            }
            return items.Take(count).ToArray();
        }
        protected void Txt_DeliverCountry_TextChanged(object sender, EventArgs e)
        {
            Txt_DeliverRegion.Text = "";
            bool ok = false;
            IList<CountryCode> country = _countryservice.FindAllCountries();
            foreach (CountryCode countrycode in country)
            {
                if (countrycode.CountryName == Txt_DeliverCountry.Text.Trim())
                {
                    autoDeliverRegion.ContextKey = countrycode.ID.ToString();
                    ok = true;
                    break;
                }
            }
            if (autoDeliverRegion.ContextKey != "" && autoDeliverRegion.ContextKey != null && ok == true)
            {
                Txt_DeliverProvince.Focus();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入正确的国家！')</script>");
                Txt_DeliverCountry.Focus();
                Txt_DeliverCountry.Text = string.Empty;
            }
        }
        protected void autoDeliveCountry_ItemSelected(object sender, EventArgs e)
        {
        }
        protected void Txt_DeliverRegion_TextChanged(object sender, EventArgs e)
        {
            IList<RegionCode> region = _regionservice.FindAllRegionCodes();
            bool Ok = false;
            foreach (RegionCode regioncode in region)
            {
                if (regioncode.RegionName == Txt_DeliverRegion.Text.Trim())
                {
                    Ok = true;
                    break;
                }
            }
            if (Ok)
            {
                Txt_DeliverZipCode.Focus();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入正确的城市！')</script>");
                Txt_DeliverRegion.Focus();
            }
        }

        protected void But_Conel_Click(object sender, EventArgs e)
        {
            Response.Redirect((string)ViewState["Url"]);
        }

        protected void But_Update_Click(object sender, EventArgs e)
        {
            string Uid = "";
            string DID = "";
            if (Txt_CompanyCode.Text.Trim() == "" && Txt_Code.Text.Trim() == "" && Txt_LoginName.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('部门账号与用户名必须填写一个！')</script>");
                Txt_LoginName.Focus();
            }
            else
            {
                if (Session["User"] != null)
                {
                    Uid = Session["User"].ToString();
                }
                if (Session["Depar"] != null)
                {
                    DID = Session["Depar"].ToString();
                }
                AddRessBook(Uid, DID);
            }
        }
        protected void AddRessBook(string UID, string DID)
        {
            if (address == null)
            {
                address = (AddressBook)Session["Address"];
            }
            if (Txt_DeliverName.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                Txt_DeliverName.Focus();
            }
            else if (Txt_DeliverAddress.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                Txt_DeliverAddress.Focus();
            }
            else if (Txt_DeliverCountry.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                Txt_DeliverCountry.Focus();
            }
            else if (Txt_DeliverRegion.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空!')</script>");
                Txt_DeliverRegion.Focus();
            }
            else if (Txt_DeliverZipCode.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                Txt_DeliverZipCode.Focus();
            }

            else if (Txt_DeliverContactor.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                Txt_DeliverContactor.Focus();
            }
            else if (Txt_DeliverTel.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                Txt_DeliverTel.Focus();
            }
            else
            {
                if (!RTel.IsMatch(Txt_DeliverTel.Text))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('电话号码格式不正确！')</script>");
                    Txt_DeliverTel.Focus();
                }
                else
                {
                    if (UID != "")
                    {
                        address.UID = Guid.Parse(UID);
                    }
                    if (DID != "")
                    {
                        address.DID = Guid.Parse(DID);
                    }
                    address.AddressType = int.Parse(ddl_AddressBookType.SelectedValue);
                    address.Name = Txt_DeliverName.Text.Trim();
                    address.Address = Txt_DeliverAddress.Text.Trim();
                    address.CountryCode = CountrySwitch(Txt_DeliverCountry.Text.Trim(), "store");
                    address.Provience = Txt_DeliverProvince.Text.Trim();
                    address.RegionCode = RegionSwitch(Txt_DeliverRegion.Text.Trim(), "store");
                    address.PostCode = Txt_DeliverZipCode.Text.Trim();
                    address.ContactorName = Txt_DeliverContactor.Text.Trim();
                    address.Phone = Txt_DeliverTel.Text.Trim();
                    address.AddressType = int.Parse(ddl_AddressBookType.SelectedValue);
                    address.UpdateTime = DateTime.Now;
                    address.Operator = "admin";
                    _addressbookservice.ModifyAddressBook(address);
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('修改成功！');location='AddressBookManagemnet.aspx'</script>");
                    Session["Address"] = null;
                    Session["Depar"] = null;
                    Session["User"] = null;
                }
            }
        }

        
    }
}