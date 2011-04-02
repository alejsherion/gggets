using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;

namespace GGGETSAdmin.AddressBookManage
{
    public partial class AddressBookDetails : System.Web.UI.Page
    {
        private string AID;
        private IDepartmentManagementService _departmentservice;
        private ICountryCodeManagementService _countryservice;
        private IRegionCodeManagementService _regionservice;
        private IUserManagementService _userService;
        private IAddressBookManagementService _AddressBookService;
        private ISysUserManagementService _sysUserManagementService;
        protected AddressBookDetails()
        { }
        public AddressBookDetails(IAddressBookManagementService addressservice, IDepartmentManagementService departmentservice, ICountryCodeManagementService countryservice, IRegionCodeManagementService regionservice, IUserManagementService userService, ISysUserManagementService sysUserManagementService)
        {
            _countryservice = countryservice;
            _regionservice = regionservice;
            _departmentservice = departmentservice;
            _userService = userService;
            _AddressBookService = addressservice;
            _sysUserManagementService = sysUserManagementService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["AID"]) && !string.IsNullOrEmpty(Request.QueryString["Privilege"]))
                {
                    string Aid = Request.QueryString["AID"];
                    Storage(Aid);
                    bool Privilege = bool.Parse(Request.QueryString["Privilege"].ToString());
                    if (Privilege)
                    {
                        But_Update.Enabled = true;
                    }
                    else
                    {
                        But_Update.Enabled = false;
                    }
                    ViewState["Url"] = Request.UrlReferrer.ToString();
                    
                }
                else
                {
                    Response.Redirect("../../Navigation.aspx");
                }
            }
        }
        /// <summary>
        /// 根据地址ID进行页面赋值
        /// </summary>
        /// <param name="Aid"></param>
        protected void Storage(string Aid)
        {
            AddressBook address = _AddressBookService.FindAddressBookByAID(Aid);
            if (address!=null)
            {
                if (address.AddressType == 0)
                {
                    Txt_AddressType.Text = "发件地址";
                }
                else if (address.AddressType == 1)
                {
                    Txt_AddressType.Text = "收件地址";
                }
                else
                {
                    Txt_AddressType.Text = "交付人地址";
                }
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
                Txt_DeliverName.Text = address.Name;
                Txt_DeliverAddress.Text = address.Address;
                Txt_DeliverCountry.Text = CountrySwitch(address.CountryCode);
                Txt_DeliverRegion.Text = RegionSwitch(address.RegionCode);
                Txt_DeliverProvince.Text = address.Provience;
                Txt_DeliverZipCode.Text = address.PostCode;
                Txt_DeliverContactor.Text = address.ContactorName;
                Txt_DeliverTel.Text = address.Phone;
            }
            
        }
        /// <summary>
        /// 国家地区码转换
        /// </summary>
        /// <param name="countryname"></param>
        /// <returns></returns>
        protected string CountrySwitch(string countryname)
        {
            string country = string.Empty;
            IList<CountryCode> Countrycode = _countryservice.FindAllCountries();
            foreach (CountryCode countrycode in Countrycode)
            {
                if (countrycode.CountryCode1 == countryname)
                {
                    country = countrycode.CountryName;
                    break;
                }
            }
            
            return country.ToUpper();
        }
        protected string RegionSwitch(string regionname)
        {
            string region = string.Empty;
            IList<RegionCode> Regioncode = _regionservice.FindAllRegionCodes();
            foreach (RegionCode regioncode in Regioncode)
            {
                if (regioncode.RegionCode1 == regionname)
                {
                    region = regioncode.RegionName;
                    break;
                }
            }
            return region.ToUpper();
        }
        /// <summary>
        /// 返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void But_Conel_Click(object sender, EventArgs e)
        {
            Response.Redirect((string)ViewState["Url"]);
        }
        /// <summary>
        /// 修改按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void But_Update_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AID))
            {
                AID = Request.QueryString["AID"];
            }
            bool aPrivilege = bool.Parse(Request.QueryString["Privilege"]);
            Response.Redirect("AddressBookModify.aspx?AID=" + AID + "&Privilege=" + aPrivilege + "");
        }
    }
}