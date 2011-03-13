﻿using System;
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
        
        private static Regex RTel = new Regex(@"^[0-9]*$");
        private HAWB hawb;
        private AddressBook Addbook;
        private static IList<CountryCode> listcountry;
        private static IList<RegionCode> listregion;
        private static ICountryCodeManagementService _countryservice;
        private static IRegionCodeManagementService _regionservice;
        private IDepartmentManagementService _deparservice;
        protected DeliverAdd()
        {}
        public DeliverAdd(ICountryCodeManagementService countryservice, IRegionCodeManagementService regionservice,IDepartmentManagementService deparservice)
        {
            _countryservice = countryservice;
            _regionservice = regionservice;
            _deparservice = deparservice;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Txt_DeliverName.Focus();
                listcountry = _countryservice.FindAllCountries();
                listregion = _regionservice.FindAllRegionCodes();
                Evaluate();
            }
        }
        protected void Evaluate()
        {
            if (Session["HAWB"] != null)
            {
                hawb = (HAWB)Session["HAWB"];
            }
            if (Session["DeliverBook"] != null)
            {
                Addbook = (AddressBook)Session["DeliverBook"];
            }
            if (Addbook != null)
            {
                Txt_DeliverName.Text = Addbook.Name;
                Txt_DeliverAddress.Text = Addbook.Address;
                foreach (CountryCode countrycode in listcountry)
                {
                    if (countrycode.CountryCode1 == Addbook.CountryCode)
                    {
                        Txt_DeliverCountry.Text = countrycode.CountryName;
                        break;
                    }
                }
                foreach (RegionCode regioncode in listregion)
                {
                    if (regioncode.RegionCode1 == Addbook.RegionCode)
                    {
                        Txt_DeliverRegion.Text = regioncode.RegionName;
                        break;
                    }
                }
                Txt_DeliverProvince.Text = Addbook.Provience;
                Txt_DeliverZipCode.Text = Addbook.PostCode;
                Txt_DeliverContactor.Text = Addbook.ContactorName;
                Txt_DeliverTel.Text = Addbook.Phone;
            }
            else
            {
                string CompanyCode = string.Empty;
                string DepCode = string.Empty;
                if (Session["compayCode"] != null)
                {
                    CompanyCode = Session["compayCode"].ToString();
                }
                if (Session["DepCode"] != null)
                {
                    DepCode = Session["DepCode"].ToString();
                }
                if (!string.IsNullOrEmpty(DepCode) && !string.IsNullOrEmpty(CompanyCode))
                {
                    IList<AddressBook> ressbook = _deparservice.FindAllForwarderAddressesByDepCodeAndCompanyCode(DepCode, CompanyCode);

                    if (ressbook != null)
                    {
                        foreach (AddressBook address in ressbook)
                        {
                            if (address.AddressType == 2)
                            {
                                Txt_DeliverName.Text = address.Name;
                                Txt_DeliverAddress.Text = address.Address;
                                Txt_DeliverCountry.Text = CountrySwitch(address.CountryCode,1);
                                Txt_DeliverRegion.Text = RegionSwitch(address.RegionCode, 1);
                                Txt_DeliverProvince.Text = address.Provience;
                                Txt_DeliverZipCode.Text = address.PostCode;
                                Txt_DeliverContactor.Text = address.ContactorName;
                                Txt_DeliverTel.Text = address.Phone;
                                break;
                            }

                        }
                    }
                }
            }
        }
        protected void btn_AddDeliver_Click1(object sender, EventArgs e)
        {
            if (hawb == null)
            {
                hawb = (HAWB)Session["HAWB"];
            }
            if (Txt_DeliverName.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('名称不能为空！')</script>");
                Txt_DeliverName.Focus();
            }
            else if (Txt_DeliverAddress.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('地址不能为空！')</script>");
                Txt_DeliverAddress.Focus();
            }
            else if (Txt_DeliverCountry.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('国家不能为空！')</script>");
                Txt_DeliverCountry.Focus();
            }
            else if (Txt_DeliverRegion.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('地区不能为空!')</script>");
                Txt_DeliverRegion.Focus();
            }
            else if (Txt_DeliverZipCode.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('邮编不能为空！')</script>");
                Txt_DeliverZipCode.Focus();
            }

            else if (Txt_DeliverContactor.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('联系人不能为空！')</script>");
                Txt_DeliverContactor.Focus();
            }
            else if (Txt_DeliverTel.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('电话不能为空！')</script>");
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
                    if (!string.IsNullOrEmpty(CountrySwitch(Txt_DeliverCountry.Text.Trim(), 0)) && !string.IsNullOrEmpty(RegionSwitch(Txt_DeliverRegion.Text.Trim(), 0)))
                    {
                        hawb.DeliverName = Txt_DeliverName.Text.Trim();
                        hawb.DeliverAddress = Txt_DeliverAddress.Text.Trim();
                        hawb.DeliverCountry = CountrySwitch(Txt_DeliverCountry.Text.Trim(), 0);
                        hawb.DeliverRegion = RegionSwitch(Txt_DeliverRegion.Text.Trim(), 0);
                        hawb.DeliverContactor = Txt_DeliverContactor.Text.Trim();
                        hawb.DeliverZipCode = Txt_DeliverZipCode.Text.Trim();
                        hawb.DeliverTel = Txt_DeliverTel.Text.Trim();
                        Session["HAWB"] = hawb;
                        Session.Remove("ComCode");
                        Session.Remove("DepCode");
                        Response.Write("<script>window.parent.location = 'HAWBAdd.aspx';</script>");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>closeList2();</script>");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alert('请输入正确的国家和地区！')</script>");
                    }
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
                if (countrycode.CountryName == Txt_DeliverCountry.Text.Trim().ToUpper())
                {
                    autoDeliverRegion.ContextKey = countrycode.ID.ToString();
                    ok = true;
                    break;
                }
            }
            if (!string.IsNullOrEmpty(autoDeliverRegion.ContextKey) && ok == true)
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
            //Txt_DeliverCountry.Text = ((AutoCompleteExtra.AutoCompleteExtraExtender)sender).SelectedValue;
            
        }
        protected void gv_Deliver_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (Session["HAWB"] != null)
            {
                hawb = (HAWB)Session["HAWB"];
            }
                if (hawb != null)
                {
                    Department depar = hawb.Department;
                    if (e.CommandName == "Select")
                    {
                        Guid Aid = Guid.Parse(e.CommandArgument.ToString());
                        IList<AddressBook> ressbook = _deparservice.FindAllForwarderAddressesByDepCodeAndCompanyCode(depar.DepCode, depar.CompanyCode);
                        if (ressbook != null)
                        {
                            foreach (AddressBook address in ressbook)
                            {
                                if (address.AID == Aid)
                                {
                                    Txt_DeliverName.Text = address.Name;
                                    Txt_DeliverAddress.Text = address.Address;
                                    foreach (CountryCode countrycode in listcountry)
                                    {
                                        if (countrycode.CountryCode1 == address.CountryCode)
                                        {
                                            Txt_DeliverCountry.Text = countrycode.CountryName;
                                            break;
                                        }
                                    }
                                    foreach (RegionCode regioncode in listregion)
                                    {
                                        if (regioncode.RegionCode1 == address.RegionCode)
                                        {
                                            Txt_DeliverRegion.Text = regioncode.RegionName;
                                            break;
                                        }
                                    }
                                    Txt_DeliverProvince.Text = address.Provience;
                                    Txt_DeliverZipCode.Text = address.PostCode;
                                    Txt_DeliverContactor.Text = address.ContactorName;
                                    Txt_DeliverTel.Text = address.Phone;
                                    break;
                                }

                            }
                        }
                    }
                }
            
        }

        protected void Txt_DeliverRegion_TextChanged(object sender, EventArgs e)
        {
            IList<RegionCode> region = _regionservice.FindAllRegionCodes();
            bool Ok = false;
            foreach (RegionCode regioncode in region)
            {
                if (regioncode.RegionName == Txt_DeliverRegion.Text.Trim().ToUpper())
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
        protected string CountrySwitch(string countryname,int type)
        {
            string country = string.Empty;
            IList<CountryCode> Countrycode = _countryservice.FindAllCountries();
            if (type == 0)
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
            return country;
        }
        protected string RegionSwitch(string regionname,int type)
        {
            string region = string.Empty;
            IList<RegionCode> Regioncode = _regionservice.FindAllRegionCodes();
            if (type == 0)
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
            return region;
        }


        protected void lbtn_Deliverhistory_Click(object sender, EventArgs e)
        {   
            
        }

        protected void lbtn_Deliverhistory_Click1(object sender, EventArgs e)
        {
            Session["historytype"] = "Deliver";
            Session["DeliverLiShi"] = "DeliverLiShi";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>OpenShipperhistory();</script>");
        }

    }

}