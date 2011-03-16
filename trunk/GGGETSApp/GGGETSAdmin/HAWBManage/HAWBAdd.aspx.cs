using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Globalization;

namespace GGGETSAdmin.HAWBManage
{
    public partial class HAWBAdd : System.Web.UI.Page
    {
        private static Regex RTel = new Regex(@"^[0-9]*$");
        private static Regex RRegion = new Regex(@"^[A-Za-z]");
        private HAWB hawb;
        private static IList<CountryCode> listcountry;
        private static IList<RegionCode> listregion;
        private int Update = 0;
        private IHAWBManagementService _hawbService;
        private static ICountryCodeManagementService _countryservice;
        private static IRegionCodeManagementService _regionservice;
        private ICompanyManagementService _companyservice;
        private IDepartmentManagementService _departmentservice;
        private IAddressBookManagementService _addressbookservice;
        protected HAWBAdd()
        { }
        public HAWBAdd(IHAWBManagementService hawbService, ICountryCodeManagementService countryservice, IRegionCodeManagementService regionservice, ICompanyManagementService companyservice, IDepartmentManagementService departmentservice, IAddressBookManagementService addressbookservice)
        {
            _hawbService = hawbService;
            _countryservice = countryservice;
            _regionservice = regionservice;
            _companyservice = companyservice;
            _departmentservice = departmentservice;
            _addressbookservice = addressbookservice;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Txt_BarCode.Focus();
                listcountry = _countryservice.FindAllCountries();
                listregion = _regionservice.FindAllRegionCodes();
                if (!string.IsNullOrEmpty(Request.QueryString["BarCode"]) && !string.IsNullOrEmpty(Request.QueryString["update"]))
                {
                    Update = Convert.ToInt32(Request.QueryString["update"]);
                    ViewState["update"] = Update;
                    hawb = _hawbService.LoadHAWBByBarCode(Request.QueryString["BarCode"].ToString());
                    Session["HAWB"] = hawb;
                    if (hawb.DeliverName != "" && hawb.DeliverName != null)
                    {
                        this.Deliver.Visible = true;
                        //this.delivertitle.Visible = true;
                        this.lbtn_AddConsignee.Enabled = true;
                    }
                    Evaluate();
                    DDl_Status.Visible = true;
                    lbl_Status.Visible = true;
                }
                else
                {
                    if (Session["HAWB"] != null)
                    {
                        hawb = (HAWB)Session["HAWB"];
                        if (hawb.DeliverName != null && hawb.DeliverName != "")
                        {
                            this.Deliver.Visible = true;
                            //this.delivertitle.Visible = true;
                            this.lbtn_AddConsignee.Enabled = false;
                            Txt_ConsigneeName.Focus();
                        }

                    }
                    Evaluate();
                    DDl_Status.Visible = false;
                    lbl_Status.Visible = false;
                }
            }

        }

        private void Storage()
        {
            if (ViewState["update"] != null)
            {
                Update = (int)ViewState["update"];
            }
            hawb = (HAWB)Session["HAWB"];
            if (Update == 0)
            {
                if (hawb == null)
                {
                    hawb = new HAWB();
                    Department depar = (Department)Session["Department"];
                    hawb.Department = depar;

                    hawb.HID = Guid.NewGuid();
                    hawb.CreateTime = DateTime.Now;
                    hawb.DID = depar.DID;
                }
            }
            else
            {
                hawb.Status = int.Parse(DDl_Status.SelectedValue);
            }

            hawb.SettleType = int.Parse(DDl_SettleType.SelectedValue);
            hawb.BarCode = Txt_BarCode.Text.Trim().ToUpper();

            hawb.ShipperName = Txt_ShipperName.Text.Trim().ToUpper();
            hawb.ShipperAddress = Txt_ShipperAddress.Text.Trim().ToUpper();
            hawb.ShipperCountry = CountrySwitch(Txt_ShipperCountry.Text.Trim().ToUpper());
            hawb.ShipperRegion = RegionSwitch(Txt_ShipperRegion.Text.Trim().ToUpper());
            hawb.ShipperContactor = Txt_ShipperContactor.Text.Trim().ToUpper();
            hawb.ShipperTel = Txt_ShipperTel.Text.Trim().ToUpper();
            hawb.ShipperZipCode = Txt_ShipperZipCode.Text.Trim().ToUpper();

            hawb.ConsigneeName = Txt_ConsigneeName.Text.Trim().ToUpper();
            hawb.ConsigneeContactor = Txt_ConsigneeContactor.Text.Trim().ToUpper();
            hawb.ConsigneeAddress = Txt_ConsigneeAddress.Text.Trim().ToUpper();
            hawb.ConsigneeCountry = CountrySwitch(Txt_ConsigneeCountry.Text.Trim().ToUpper());
            hawb.ConsigneeRegion = RegionSwitch(Txt_ConsigneeRegion.Text.Trim().ToUpper());
            hawb.ConsigneeTel = Txt_ConsigneeTel.Text.Trim().ToUpper();
            hawb.ConsigneeZipCode = Txt_ConsigneeZipCode.Text.Trim().ToUpper();
            hawb.UpdateTime = DateTime.Now;
            if (Deliver.Visible)
            {
                hawb.DeliverName = Txt_DeliverName.Text.Trim().ToUpper();
                hawb.DeliverAddress = Txt_DeliverAddress.Text.Trim().ToUpper();
                hawb.DeliverCountry = CountrySwitch(Txt_DeliverCountry.Text.Trim().ToUpper());
                hawb.DeliverRegion = RegionSwitch(Txt_DeliverRegion.Text.Trim().ToUpper());
                hawb.DeliverContactor = Txt_DeliverContactor.Text.Trim().ToUpper();
                hawb.DeliverZipCode = Txt_DeliverZipCode.Text.Trim().ToUpper();
                hawb.DeliverTel = Txt_DeliverTel.Text.Trim().ToUpper();
            }
            Session.Remove("HAWB");
            Session["HAWB"] = hawb;
        }
        protected void Evaluate()
        {
            hawb = (HAWB)Session["HAWB"];
            if (hawb != null)
            {
                Txt_BarCode.Text = hawb.BarCode;
                Department depar = hawb.Department;
                Txt_Account1.Text = depar.CompanyCode;
                Txt_Account2.Text = depar.DepCode;
                DDl_SettleType.SelectedValue = depar.SettleType.ToString();
                DDl_Status.SelectedValue = hawb.Status.ToString();
            }
            if (Session["AddressShipperBook"] != null)
            {

                AddressBook AddderssShipper = (AddressBook)Session["AddressShipperBook"];
                foreach (CountryCode code in listcountry)
                {
                    if (code.CountryCode1 == AddderssShipper.CountryCode)
                    {
                        Txt_ShipperCountry.Text = code.CountryName;
                        break;
                    }
                }
                foreach (RegionCode code in listregion)
                {
                    if (code.RegionCode1 == AddderssShipper.RegionCode)
                    {
                        Txt_ShipperRegion.Text = code.RegionName;
                        break;
                    }
                }
                lbl_ShipperAddressAid.Text = AddderssShipper.AID.ToString();
                Txt_ShipperName.Text = AddderssShipper.Name;
                Txt_ShipperContactor.Text = AddderssShipper.ContactorName;
                Txt_ShipperAddress.Text = AddderssShipper.Address;
                Txt_ShipperTel.Text = AddderssShipper.Phone;
                Txt_ShipperZipCode.Text = AddderssShipper.PostCode;
            }
            else
            {
                if (hawb != null)
                {
                    Department depar = hawb.Department;
                    lbl_ShipperAddressAid.Text = depar.DID.ToString();
                    Txt_ShipperName.Text = hawb.ShipperName;
                    Txt_ShipperContactor.Text = hawb.ShipperContactor;
                    foreach (CountryCode code in listcountry)
                    {
                        if (code.CountryCode1 == hawb.ShipperCountry)
                        {
                            Txt_ShipperCountry.Text = code.CountryName;
                            break;
                        }
                    }
                    foreach (RegionCode code in listregion)
                    {
                        if (code.RegionCode1 == hawb.ShipperRegion)
                        {
                            Txt_ShipperRegion.Text = code.RegionName;
                            break;
                        }
                    }
                    Txt_ShipperAddress.Text = hawb.ShipperAddress;
                    Txt_ShipperTel.Text = hawb.ShipperTel;
                    Txt_ShipperZipCode.Text = hawb.ShipperZipCode;
                }
            }
            if (Session["AddressConsigneeBook"] != null)
            {
                AddressBook AddderssConsignee = (AddressBook)Session["AddressConsigneeBook"];
                Txt_ConsigneeName.Text = AddderssConsignee.Name;
                Txt_ConsigneeContactor.Text = AddderssConsignee.ContactorName;
                foreach (CountryCode code in listcountry)
                {
                    if (code.CountryCode1 == AddderssConsignee.CountryCode)
                    {
                        Txt_ConsigneeCountry.Text = code.CountryName;
                        break;
                    }
                }
                foreach (RegionCode code in listregion)
                {
                    if (code.RegionCode1 == AddderssConsignee.RegionCode)
                    {
                        Txt_ConsigneeRegion.Text = code.RegionName;
                        break;
                    }
                }
                Txt_ConsigneeAddress.Text = AddderssConsignee.Address;
                Txt_ConsigneeTel.Text = AddderssConsignee.Phone;
                Txt_ConsigneeZipCode.Text = AddderssConsignee.PostCode;
            }
            else
            {
                if (hawb != null)
                {
                    if (hawb.ConsigneeName != "")
                    {
                        foreach (CountryCode code in listcountry)
                        {
                            if (code.CountryCode1 == hawb.ConsigneeCountry)
                            {
                                Txt_ConsigneeCountry.Text = code.CountryName;
                                break;
                            }
                        }
                        foreach (RegionCode code in listregion)
                        {
                            if (code.RegionCode1 == hawb.ConsigneeRegion)
                            {
                                Txt_ConsigneeRegion.Text = code.RegionName;
                                break;
                            }
                        }
                        Txt_ConsigneeName.Text = hawb.ConsigneeName;
                        Txt_ConsigneeContactor.Text = hawb.ConsigneeContactor;
                        Txt_ConsigneeAddress.Text = hawb.ConsigneeAddress;
                        Txt_ConsigneeTel.Text = hawb.ConsigneeTel;
                        Txt_ConsigneeZipCode.Text = hawb.ConsigneeZipCode;
                    }
                    else
                    {
                        ConsigneeEvaluate(Txt_Account1.Text.Trim().ToUpper(),Txt_Account2.Text.Trim().ToUpper());
                    }
                }
            }
            if (Session["AddressDeliverBook"] != null)
            {
                AddressBook AddderssDeliver = (AddressBook)Session["AddressDeliverBook"];
                Txt_DeliverName.Text = AddderssDeliver.Name;
                Txt_DeliverAddress.Text = AddderssDeliver.ContactorName;
                foreach (CountryCode code in listcountry)
                {
                    if (code.CountryCode1 == AddderssDeliver.CountryCode)
                    {
                        Txt_DeliverCountry.Text = code.CountryName;
                        break;
                    }
                }
                foreach (RegionCode code in listregion)
                {
                    if (code.RegionCode1 == AddderssDeliver.RegionCode)
                    {
                        Txt_DeliverRegion.Text = code.RegionName;
                        break;
                    }
                }
                Txt_DeliverContactor.Text = AddderssDeliver.Address;
                Txt_DeliverZipCode.Text = AddderssDeliver.PostCode;
                Txt_DeliverTel.Text = AddderssDeliver.Phone;
            }
            else
            {
                if (hawb != null)
                {
                    if (hawb.DeliverName != "" && hawb.DeliverName != null)
                    {
                        Txt_DeliverName.Text = hawb.DeliverName;
                        Txt_DeliverAddress.Text = hawb.DeliverAddress;
                        foreach (CountryCode code in listcountry)
                        {
                            if (code.CountryCode1 == hawb.DeliverCountry)
                            {
                                Txt_DeliverCountry.Text = code.CountryName;
                                break;
                            }
                        }
                        foreach (RegionCode code in listregion)
                        {
                            if (code.RegionCode1 == hawb.DeliverRegion)
                            {
                                Txt_DeliverRegion.Text = code.RegionName;
                                break;
                            }
                        }
                        Txt_DeliverContactor.Text = hawb.DeliverContactor;
                        Txt_DeliverZipCode.Text = hawb.DeliverZipCode;
                        Txt_DeliverTel.Text = hawb.DeliverTel;
                    }
                }
            }
        }
        /// <summary>
        /// 清空所以TextBox
        /// </summary>
        /// <param name="objControlCollection"></param>
        /// 
        #region
        private void InitialControl(ControlCollection objControlCollection)
        {
            foreach (System.Web.UI.Control objControl in objControlCollection)
            {
                if (objControl.HasControls())
                {
                    InitialControl(objControl.Controls);
                }
                else
                {
                    if (objControl is System.Web.UI.WebControls.TextBox)
                    {
                        ((TextBox)objControl).Text = String.Empty;
                    }
                }
            }
        }
        #endregion
        protected void lbtn_AddConsignee_Click(object sender, EventArgs e)
        {
            if (Txt_Account2.Text.Trim().ToUpper() != "")
            {
                Storage();
                Session["compayCode"] = Txt_Account1.Text.Trim().ToUpper();
                Session["DepCode"] = Txt_Account2.Text.Trim().ToUpper();
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "changeScope()", true);
            }
            else
            {
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('！')</script>");
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请先输入客户账号!')", true);
                Txt_Account1.Focus();
                //
            }
        }

        protected void But_Next_Click(object sender, EventArgs e)
        {
            if (Txt_BarCode.Text.Trim().ToUpper() == "")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('运单号不能为空！')</script>");
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('运单号不能为空!')", true);
                Txt_BarCode.Focus();
            }
            else if (TextEmpty(3))
            {
                Storage();
                if (ViewState["update"] != null)
                {
                    Update = (int)ViewState["update"];
                }
                ViewState["update"] = null;
                Session["AddressShipperBook"] = null;
                Session["AddressConsigneeBook"] = null;
                Session["AddressDeliverBook"] = null;
                if (Update != 0)
                {
                    string type = "Amend";
                    Response.Redirect("HAWBItemAdd.aspx?type=" + type + "");
                }
                else
                {
                    Response.Redirect("HAWBItemAdd.aspx");
                }
            }
        }
 
        protected void But_Conel_Click(object sender, EventArgs e)
        {
            InitialControl(this.Controls);
            Session["HAWB"] = null;
        }
        protected void but_cancel_Click(object sender, EventArgs e)
        {
            Txt_DeliverName.Text = string.Empty;
            Txt_DeliverAddress.Text = string.Empty;
            Txt_DeliverCountry.Text = string.Empty;
            Txt_DeliverRegion.Text = string.Empty;
            Txt_DeliverContactor.Text = string.Empty;
            Txt_DeliverZipCode.Text = string.Empty;
            Txt_DeliverTel.Text = string.Empty;
            Txt_ConsigneeName.Text = string.Empty;
            Txt_ConsigneeAddress.Text = string.Empty;
            Txt_ConsigneeCountry.Text = string.Empty;
            Txt_ConsigneeRegion.Text = string.Empty;
            Txt_ConsigneeContactor.Text = string.Empty;
            Txt_ConsigneeZipCode.Text = string.Empty;
            Txt_ConsigneeTel.Text = string.Empty;
            Deliver.Visible = false;
            //delivertitle.Visible = false;
            lbtn_AddConsignee.Enabled = true;
            if (hawb == null)
            {
                hawb = (HAWB)Session["HAWB"];
            }
            hawb.DeliverAddress = null;
            hawb.DeliverContactor = null;
            hawb.DeliverCountry = null;
            hawb.DeliverName = null;
            hawb.DeliverRegion = null;
            hawb.DeliverTel = null;
            hawb.DeliverZipCode = null;
            Session.Remove("HAWB");
            Session["HAWB"] = hawb;
            Session.Remove("AddressDeliverBook");
        }

        protected void Txt_BarCode_TextChanged(object sender, EventArgs e)
        {
            HAWB ha = _hawbService.FindHAWBByBarCode(Txt_BarCode.Text.Trim().ToUpper());
            if (ha != null)
            {

                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('该运单号已存在!')", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('该运单号已存在！')</script>");
                Txt_BarCode.Text = string.Empty;
                Txt_BarCode.Focus();

            }
            else
            { Txt_Account1.Focus(); }
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


        protected void Txt_ShipperCountry_TextChanged(object sender, EventArgs e)
        {
            Txt_ShipperRegion.Text = "";
            bool ok = false;
            IList<CountryCode> country = _countryservice.FindAllCountries();
            foreach (CountryCode countrycode in country)
            {
                if (countrycode.CountryName == Txt_ShipperCountry.Text.Trim().ToUpper())
                {
                    autoRegion.ContextKey = countrycode.ID.ToString();
                    ok = true;
                    break;
                }
            }
            if (!string.IsNullOrEmpty(autoRegion.ContextKey) && ok == true)
            {
                
                Txt_ShipperProvince.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请输入正确的国家!')", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入正确的国家！')</script>");

                Txt_ShipperCountry.Focus();
                Txt_ShipperCountry.Text = "";
            }
            if (!AddressCountry(Txt_ShipperCountry.Text.Trim().ToUpper(), 0))
            {
                btn_Addressbox.Visible = true;
            }
            else
            {
                if (btn_Addressbox.Visible != true)
                {
                    btn_Addressbox.Visible = false;
                }
            }
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
                //Txt_ShipperCountry.Text = autocomplete.SelectedValue;
                Txt_DeliverProvince.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请输入正确的国家!')", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入正确的国家！')</script>");
                Txt_DeliverCountry.Focus();
                Txt_DeliverCountry.Text = "";
            }
            if (!AddressCountry(Txt_DeliverCountry.Text.Trim().ToUpper(), 2))
            {
                btn_DeliverAddress.Visible = true;
            }
            else
            {
                if (btn_DeliverAddress.Visible != true)
                {
                    btn_DeliverAddress.Visible = false;
                }
            }

        }
        protected void Txt_ConsigneeCountry_TextChanged(object sender, EventArgs e)
        {
            Txt_ConsigneeRegion.Text = "";
            bool ok = false;
            IList<CountryCode> country = _countryservice.FindAllCountries();
            foreach (CountryCode countrycode in country)
            {
                if (countrycode.CountryName == Txt_ConsigneeCountry.Text.Trim().ToUpper())
                {
                    autoConsigneeRegion.ContextKey = countrycode.ID.ToString();
                    ok = true;
                    break;
                }
            }
            if (!string.IsNullOrEmpty(autoConsigneeRegion.ContextKey) &&ok==true)
            {
                //Txt_ShipperCountry.Text = autocomplete.SelectedValue;
                Txt_ConsigneeProvince.Focus();
            }
            else
            {

                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请输入正确的国家!')", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入正确的国家！')</script>");
                Txt_ConsigneeCountry.Focus();
                Txt_ConsigneeCountry.Text = "";
            }
            if (!AddressCountry(Txt_ConsigneeCountry.Text.Trim().ToUpper(), 1))
            {
                btn_ConsigneeAddress.Visible = true;
            }
            else
            {
                if (btn_ConsigneeAddress.Visible != true)
                {
                    btn_ConsigneeAddress.Visible = false;
                }
            }

        }


        protected void autocomplete_ItemSelected(object sender, EventArgs e)
        {
            //Txt_ShipperCountry.Text = ((AutoCompleteExtra.AutoCompleteExtraExtender)sender).SelectedValue;
            Txt_ConsigneeProvince.Focus();
        }
        protected void autoDeliveCountry_ItemSelected(object sender, EventArgs e)
        {
            //Txt_DeliverCountry.Text = ((AutoCompleteExtra.AutoCompleteExtraExtender)sender).SelectedValue;
            Txt_DeliverProvince.Focus();
        }
        protected void autoConsigneeCountry_ItemSelected(object sender, EventArgs e)
        {
            //Txt_ConsigneeCountry.Text = ((AutoCompleteExtra.AutoCompleteExtraExtender)sender).SelectedValue;
            Txt_ConsigneeProvince.Focus();
        }
        protected void Txt_Account1_TextChanged(object sender, EventArgs e)
        {
            if (Txt_Account1.Text.Trim() != "")
            {
                CompanyEvaluate(Txt_Account1.Text.Trim());
                Txt_ConsigneeName.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('用户账号不能为空!')", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('用户账号不能为空！')</script>");
                Txt_Account1.Focus();
                //InitialControl(this.Controls);
            }
        }
        protected void CompanyEvaluate(string code)
        {
            Company compay = _companyservice.FindCompanyByCompanyCode(code);
            if (compay != null)
            {
                
                Txt_Account2.Text = "00";
                ShipAddressEvaluate(Txt_Account1.Text.Trim(),Txt_Account2.Text.Trim());
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有该公司账号!')", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('没有该公司账号！')</script>");
                //InitialControl(this.Controls);
                Txt_Account1.Text = string.Empty;
                Txt_Account1.Focus();
            }
        }

        protected void Txt_Account2_TextChanged(object sender, EventArgs e)
        {
            if (Txt_Account2.Text.Trim()!= "" && Txt_Account1.Text.Trim()!="")
            {
                ShipAddressEvaluate(Txt_Account1.Text.Trim(),Txt_Account2.Text.Trim());
                Txt_ShipperName.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('公司名称和部门不能为空!')", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('公司名称和部门不能为空！')</script>");
                Txt_Account2.Focus();
            }
        }
        protected void ShipAddressEvaluate(string companycode,string deparcode)
        {
            Department departmant = _departmentservice.FindDepartmentByDepCodeAndCompanyCode(deparcode,companycode);
            if (departmant != null)
            {
                
                Session["Department"] = departmant;
                IList<AddressBook> ressbook = _departmentservice.FindAllShipAddressesByDepCodeAndCompanyCode(deparcode,companycode);
                if (ressbook != null)
                {
                    foreach (AddressBook address in ressbook)
                    {
                        if (address.AddressType == 0)
                        {
                            Session["Shipperaddress"] = address;
                            DDl_SettleType.SelectedValue = departmant.SettleType.ToString();
                            lbl_ShipperAddressAid.Text = address.AID.ToString();
                            Txt_ShipperName.Text = address.Name;
                            Txt_ShipperAddress.Text = address.Address;
                            foreach (CountryCode countrycode in listcountry)
                            {
                                if (countrycode.CountryCode1 == address.CountryCode)
                                {
                                    Txt_ShipperCountry.Text = countrycode.CountryName;
                                    break;
                                }
                            }
                            foreach (RegionCode regioncode in listregion)
                            {
                                if (regioncode.RegionCode1 == address.RegionCode)
                                {
                                    Txt_ShipperRegion.Text = regioncode.RegionName;
                                    break;
                                }
                            }
                            Txt_ShipperProvince.Text = address.Provience;
                            Txt_ShipperZipCode.Text = address.PostCode;
                            Txt_ShipperContactor.Text = address.ContactorName;
                            Txt_ShipperTel.Text = address.Phone;
                            break;
                        }

                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有该部门!')", true);
                Txt_Account2.Focus();
                Txt_Account2.Text = string.Empty;
                Txt_ShipperName.Text = string.Empty;
                Txt_ShipperAddress.Text = string.Empty;
                Txt_ShipperCountry.Text = string.Empty;
                Txt_ShipperRegion.Text = string.Empty;
                Txt_ShipperProvince.Text = string.Empty;
                Txt_ShipperZipCode.Text = string.Empty;
                Txt_ShipperContactor.Text = string.Empty;
                Txt_ShipperTel.Text = string.Empty;
            }
        }

        protected void ConsigneeEvaluate(string companycode,string deparcode)
        {
            Department departmant = _departmentservice.FindDepartmentByDepCodeAndCompanyCode(deparcode, companycode);
            if (departmant != null)
            {
                Session["Department"] = departmant;
                IList<AddressBook> ressbook = _departmentservice.FindAllDeliveryAddressesByDepCodeAndCompanyCode(deparcode, companycode);
                if (ressbook != null)
                {
                    foreach (AddressBook address in ressbook)
                    {
                        if (address.AddressType == 1 && address.Name != "")
                        {
                            Txt_ConsigneeName.Text = address.Name;
                            Txt_ConsigneeAddress.Text = address.Address;
                            foreach (CountryCode countrycode in listcountry)
                            {
                                if (countrycode.CountryCode1 == address.CountryCode)
                                {
                                    Txt_ConsigneeCountry.Text = countrycode.CountryName;
                                    break;
                                }
                            }
                            foreach (RegionCode regioncode in listregion)
                            {
                                if (regioncode.RegionCode1 == address.RegionCode)
                                {
                                    Txt_ConsigneeRegion.Text = regioncode.RegionName;
                                    break;
                                }
                            }
                            
                            Txt_ConsigneeProvince.Text = address.Provience;
                            Txt_ConsigneeZipCode.Text = address.PostCode;
                            Txt_ConsigneeContactor.Text = address.ContactorName;
                            Txt_ConsigneeTel.Text = address.Phone;
                            break;
                        }

                    }
                }
            }
        }

        protected void gv_Shipper_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Guid Aid = Guid.Parse(e.CommandArgument.ToString());
                IList<AddressBook> ressbook = _departmentservice.FindAllShipAddressesByDepCodeAndCompanyCode(Txt_Account2.Text.Trim().ToUpper(),Txt_Account1.Text.Trim().ToUpper());
                if (ressbook != null)
                {
                    foreach (AddressBook address in ressbook)
                    {
                        if (address.AID == Aid)
                        {
                            Txt_ShipperName.Text = address.Name;
                            Txt_ShipperAddress.Text = address.Address;
                            Txt_ShipperCountry.Text = address.CountryCode;
                            Txt_ShipperRegion.Text = address.RegionCode;
                            Txt_ShipperProvince.Text = address.Provience;
                            Txt_ShipperZipCode.Text = address.PostCode;
                            Txt_ShipperContactor.Text = address.ContactorName;
                            Txt_ShipperTel.Text = address.Phone;
                            break;
                        }

                    }
                }
            }
        }

        protected void lbtn_Shipperhistory_Click(object sender, EventArgs e)
        {
            if (Txt_Account2.Text.Trim().ToUpper() != "" && Txt_Account1.Text.Trim().ToUpper()!="")
            {
                //Department depar = _departmentservice.FindDepartmentByDepCodeAndCompanyCode(Txt_Account2.Text.Trim().ToUpper(),Txt_Account1.Text.Trim().ToUpper());
                //if (Session["Department"] == null)
                //{
                //    Session["Department"] = depar;
                //}
                Storage();
                Session["compayCode"] = Txt_Account1.Text.Trim().ToUpper();
                Session["DepCode"] = Txt_Account2.Text.Trim().ToUpper();
                Session["historytype"] = "Shipper";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "OpenShipperhistory()", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>OpenShipperhistory();</script>");
            }
            else
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请先输入账号！')</script>");
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请先输入账号！')", true);
            }
        }

        protected void lbtn_Consigneehistory_Click(object sender, EventArgs e)
        {
            if (Txt_Account2.Text.Trim().ToUpper() != "" && Txt_Account1.Text.Trim().ToUpper()!="")
            {
                //Department depar = _departmentservice.FindDepartmentByDepCodeAndCompanyCode(Txt_Account2.Text.Trim().ToUpper(),Txt_Account1.Text.Trim().ToUpper());
                //if (Session["Department"] == null)
                //{
                //    Session["Department"] = depar;
                //}
                Storage();
                Session["compayCode"] = Txt_Account1.Text.Trim().ToUpper();
                Session["DepCode"] = Txt_Account2.Text.Trim().ToUpper();
                Session["historytype"] = "Consignee";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "OpenShipperhistory()", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请先输入账号！')", true);
            }
        }

        protected void lbtn_Deliverhistory_Click(object sender, EventArgs e)
        {
            if (Txt_Account2.Text.Trim().ToUpper() != "" && Txt_Account1.Text.Trim().ToUpper()!="")
            {
            //    Department depar = _departmentservice.FindDepartmentByDepCodeAndCompanyCode(Txt_Account2.Text.Trim().ToUpper(),Txt_Account1.Text.Trim().ToUpper());
            //    if (Session["Department"] == null)
            //    {
            //        Session["Department"] = depar;
            //    }
                Storage();
                Session["compayCode"] = Txt_Account1.Text.Trim().ToUpper();
                Session["DepCode"] = Txt_Account2.Text.Trim().ToUpper();
                Session["historytype"] = "Deliver";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>OpenShipperhistory();</script>");
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "OpenShipperhistory()", true);
            }
            else
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请先输入账号！')</script>");
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请先输入账号！')", true);
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
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请输入正确的城市!')", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入正确的城市！')</script>");
                Txt_DeliverRegion.Focus();
                Txt_DeliverRegion.Text = string.Empty;
            }
            if (!AddressRegion(Txt_DeliverRegion.Text.Trim().ToUpper(), 2))
            {
                btn_DeliverAddress.Visible = true;
            }
            else
            {
                if (btn_DeliverAddress.Visible != true)
                {
                    btn_DeliverAddress.Visible = false;
                }
            }
        }

        protected void Txt_ConsigneeRegion_TextChanged(object sender, EventArgs e)
        {
            IList<RegionCode> region = _regionservice.FindAllRegionCodes();
            bool Ok = false;
            foreach (RegionCode regioncode in region)
            {
                if (regioncode.RegionName == Txt_ConsigneeRegion.Text.Trim().ToUpper())
                {
                    Ok = true;
                    break;
                }
            }
            if (Ok)
            {
                Txt_ConsigneeZipCode.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请输入正确的城市!')", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入正确的城市！')</script>");
                Txt_ConsigneeRegion.Focus();
                Txt_ConsigneeRegion.Text = string.Empty;
            }
            if (!AddressRegion(Txt_ConsigneeRegion.Text.Trim().ToUpper(), 1))
            {
                btn_ConsigneeAddress.Visible = true;
            }
            else
            {
                if (btn_ConsigneeAddress.Visible != true)
                {
                    btn_ConsigneeAddress.Visible = false;
                }
            }
        }

        protected void Txt_ShipperRegion_TextChanged(object sender, EventArgs e)
        {
            IList<RegionCode> region = _regionservice.FindAllRegionCodes();
            bool Ok = false;
            foreach (RegionCode regioncode in region)
            {
                if (regioncode.RegionName == Txt_ShipperRegion.Text.Trim().ToUpper())
                {
                    Ok = true;
                    break;
                }
            }
            if (Ok)
            {
                Txt_ShipperZipCode.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请输入正确的城市!')", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入正确的地区三字码！')</script>");
                Txt_ShipperRegion.Focus();
                Txt_ShipperRegion.Text = string.Empty;
            }
            if (!AddressRegion(Txt_ShipperRegion.Text.Trim().ToUpper(), 0))
            {
                btn_Addressbox.Visible = true;
            }
            else
            {
                if (btn_Addressbox.Visible != true)
                {
                    btn_Addressbox.Visible = false;
                }
            }
        }

        protected void Txt_ShipperTel_TextChanged(object sender, EventArgs e)
        {
            if (!AddressTel(Txt_ShipperTel.Text.Trim().ToUpper(), 0))
            {
                btn_Addressbox.Visible = true;
                
            }
            else
            {
                if (btn_Addressbox.Visible != true)
                {
                    btn_Addressbox.Visible = false;
                }
            }
            if (Deliver.Visible == true)
            {
                Txt_DeliverName.Focus();
            }
            else
            {
                Txt_ConsigneeName.Focus();
            }
        }

        protected string CountrySwitch(string countryname)
        {
            string country = string.Empty;
            IList<CountryCode> Countrycode = _countryservice.FindAllCountries();
            foreach (CountryCode countrycode in Countrycode)
            {
                if (countrycode.CountryName == countryname)
                {
                    country = countrycode.CountryCode1;
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
                if (regioncode.RegionName == regionname)
                {
                    region = regioncode.RegionCode1;
                    break;
                }
            }
            return region.ToUpper();
        }

        protected void Txt_ShipperName_TextChanged(object sender, EventArgs e)
        {
            
            if (!AddressName(Txt_ShipperName.Text.Trim().ToUpper(), 0))
            {
                btn_Addressbox.Visible = true;
                
            }
            else
            {
                if (btn_Addressbox.Visible != true)
                {
                    btn_Addressbox.Visible = false;
                }
            }
            Txt_ShipperAddress.Focus();
        }

        protected void Txt_ShipperAddress_TextChanged(object sender, EventArgs e)
        {
            if (!Address(Txt_ShipperAddress.Text.Trim().ToUpper(), 0))
            {
                btn_Addressbox.Visible = true;
            }
            else
            {
                if (btn_Addressbox.Visible != true)
                {
                    btn_Addressbox.Visible = false;
                }
            }
            Txt_ShipperCountry.Focus();
        }

        protected void Txt_ShipperProvince_TextChanged(object sender, EventArgs e)
        {
            if (!AddressProvince(Txt_ShipperProvince.Text.Trim().ToUpper(), 0))
            {
                btn_Addressbox.Visible = true;
            }
            else
            {
                if (btn_Addressbox.Visible != true)
                {
                    btn_Addressbox.Visible = false;
                }
            }
            Txt_ShipperRegion.Focus();
        }

        protected void Txt_ShipperZipCode_TextChanged(object sender, EventArgs e)
        {
            if (!AddressZip(Txt_ShipperZipCode.Text.Trim().ToUpper(), 0))
            {
                btn_Addressbox.Visible = true;
            }
            else
            {
                if (btn_Addressbox.Visible != true)
                {
                    btn_Addressbox.Visible = false;
                }
            }
            Txt_ShipperContactor.Focus();
        }

        protected void Txt_ShipperContactor_TextChanged(object sender, EventArgs e)
        {
            if (!AddressContactorName(Txt_ShipperContactor.Text.Trim().ToUpper(), 0))
            {
                btn_Addressbox.Visible = true;
            }
            else
            {
                if (btn_Addressbox.Visible != true)
                {
                    btn_Addressbox.Visible = false;
                }
            }
            Txt_ShipperTel.Focus();
        }

        protected void Txt_DeliverName_TextChanged(object sender, EventArgs e)
        {
            if (!AddressName(Txt_DeliverName.Text.Trim().ToUpper(), 2))
            {
                btn_DeliverAddress.Visible = true;
            }
            else
            {
                if (btn_DeliverAddress.Visible != true)
                {
                    btn_DeliverAddress.Visible = false;
                }
            }
            Txt_DeliverAddress.Focus();
        }

        protected void Txt_DeliverAddress_TextChanged(object sender, EventArgs e)
        {
            if (!Address(Txt_DeliverAddress.Text.Trim().ToUpper(), 2))
            {
                btn_DeliverAddress.Visible = true;
            }
            else
            {
                if (btn_DeliverAddress.Visible != true)
                {
                    btn_DeliverAddress.Visible = false;
                }
            }
            Txt_DeliverCountry.Focus();
        }

        protected void Txt_DeliverProvince_TextChanged(object sender, EventArgs e)
        {
            if (!AddressProvince(Txt_DeliverProvince.Text.Trim().ToUpper(), 2))
            {
                btn_DeliverAddress.Visible = true;
            }
            else
            {
                if (btn_DeliverAddress.Visible != true)
                {
                    btn_DeliverAddress.Visible = false;
                }
            }
            Txt_DeliverRegion.Focus();
        }

        protected void Txt_DeliverZipCode_TextChanged(object sender, EventArgs e)
        {
            if (!AddressZip(Txt_DeliverZipCode.Text.Trim().ToUpper(), 2))
            {
                btn_DeliverAddress.Visible = true;
            }
            else
            {
                if (btn_DeliverAddress.Visible != true)
                {
                    btn_DeliverAddress.Visible = false;
                }
            }
            Txt_DeliverContactor.Focus();
        }

        protected void Txt_DeliverContactor_TextChanged(object sender, EventArgs e)
        {
            if (!AddressContactorName(Txt_DeliverContactor.Text.Trim().ToUpper(), 2))
            {
                btn_DeliverAddress.Visible = true;
            }
            else
            {
                if (btn_DeliverAddress.Visible != true)
                {
                    btn_DeliverAddress.Visible = false;
                }
            }
            Txt_DeliverTel.Focus();
        }

        protected void Txt_DeliverTel_TextChanged(object sender, EventArgs e)
        {
            if (!AddressTel(Txt_DeliverTel.Text.Trim().ToUpper(), 2))
            {
                btn_DeliverAddress.Visible = true;
            }
            else
            {
                if (btn_DeliverAddress.Visible != true)
                {
                    btn_DeliverAddress.Visible = false;
                }
            }
            Txt_ConsigneeName.Focus();
        }

        protected void Txt_ConsigneeName_TextChanged(object sender, EventArgs e)
        {
            Storage();
            Session["compayCode"] = Txt_Account1.Text.Trim().ToUpper();
            Session["DepCode"] = Txt_Account2.Text.Trim().ToUpper();
            Session["historytype"] = "Consignee";
            Session["name"] = Txt_ConsigneeName.Text.Trim().ToUpper();
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>OpenShipperhistory()</script>");
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "OpenShipperhistory()", true);
            if (!AddressName(Txt_ConsigneeName.Text.Trim().ToUpper(), 1))
            {
                btn_ConsigneeAddress.Visible = true;
            }
            else
            {
                if (btn_ConsigneeAddress.Visible != true)
                {
                    btn_ConsigneeAddress.Visible = false;
                }
            }
            Txt_ConsigneeAddress.Focus();
        }

        protected void Txt_ConsigneeAddress_TextChanged(object sender, EventArgs e)
        {
            if (!Address(Txt_ConsigneeAddress.Text.Trim().ToUpper(), 1))
            {
                btn_ConsigneeAddress.Visible = true;
            }
            else
            {
                if (btn_ConsigneeAddress.Visible != true)
                {
                    btn_ConsigneeAddress.Visible = false;
                }
            }
            Txt_ConsigneeCountry.Focus();
        }

        protected void Txt_ConsigneeProvince_TextChanged(object sender, EventArgs e)
        {
            if (!AddressProvince(Txt_ConsigneeProvince.Text.Trim().ToUpper(), 1))
            {
                btn_ConsigneeAddress.Visible = true;
            }
            else
            {
                if (btn_ConsigneeAddress.Visible != true)
                {
                    btn_ConsigneeAddress.Visible = false;
                }
            }
            Txt_ConsigneeRegion.Focus();
        }

        protected void Txt_ConsigneeZipCode_TextChanged(object sender, EventArgs e)
        {
            if (!AddressZip(Txt_ConsigneeZipCode.Text.Trim().ToUpper(), 1))
            {
                btn_ConsigneeAddress.Visible = true;
            }
            else
            {
                if (btn_ConsigneeAddress.Visible != true)
                {
                    btn_ConsigneeAddress.Visible = false;
                }
            }
            Txt_ConsigneeContactor.Focus();
        }

        protected void Txt_ConsigneeContactor_TextChanged(object sender, EventArgs e)
        {
            if (!AddressContactorName(Txt_ConsigneeContactor.Text.Trim().ToUpper(), 1))
            {
                btn_ConsigneeAddress.Visible = true;
            }
            else
            {
                if (btn_ConsigneeAddress.Visible != true)
                {
                    btn_ConsigneeAddress.Visible = false;
                }
            }
            Txt_ConsigneeTel.Focus();
        }

        protected void Txt_ConsigneeTel_TextChanged(object sender, EventArgs e)
        {
            if (!AddressTel(Txt_ConsigneeTel.Text.Trim().ToUpper(), 1))
            {
                btn_ConsigneeAddress.Visible = true;
            }
            else
            {
                if (btn_ConsigneeAddress.Visible != true)
                {
                    btn_ConsigneeAddress.Visible = false;
                }
            }
            But_Next.Focus();
        }

        protected void btn_ConsigneeAddress_Click(object sender, EventArgs e)
        {
            Address(1);
        }

        protected void btn_Addressbox_Click(object sender, EventArgs e)
        {
            Address(0);
        }

        protected void btn_DeliverAddress_Click(object sender, EventArgs e)
        {
            Address(2);
        }
        protected void Address(int type)
        {
            AddressBook address = new AddressBook();
            if (Txt_Account2.Text.Trim().ToUpper() != "" && Txt_Account1.Text.Trim().ToUpper() != "")
            {
                Department deparment = _departmentservice.FindDepartmentByDepCodeAndCompanyCode(Txt_Account2.Text.Trim().ToUpper(), Txt_Account1.Text.Trim().ToUpper());
                address.AID = Guid.NewGuid();
                address.DID = deparment.DID;
                if (TextEmpty(type))
                {
                    if (type == 0)
                    {

                        address.Name = Txt_ShipperName.Text.Trim().ToUpper();
                        address.Address = Txt_ShipperAddress.Text.Trim().ToUpper();
                        address.CountryCode = CountrySwitch(Txt_ShipperCountry.Text.Trim().ToUpper());
                        address.RegionCode = RegionSwitch(Txt_ShipperRegion.Text.Trim().ToUpper());
                        address.Provience = Txt_ShipperProvince.Text.Trim().ToUpper();
                        address.PostCode = Txt_ShipperZipCode.Text.Trim().ToUpper();
                        address.ContactorName = Txt_ShipperContactor.Text.Trim().ToUpper();
                        address.Phone = Txt_ShipperTel.Text.Trim().ToUpper();
                        address.CreateTime = DateTime.Now;
                        address.UpdateTime = DateTime.Now;
                        address.AddressType = 0;
                        address.Operator = "Admin";
                        _addressbookservice.AddAddressBook(address);
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('保存成功!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('保存成功！')</script>");
                        btn_Addressbox.Visible = false;

                    }
                    else if (type == 1)
                    {
                        address.Name = Txt_ConsigneeName.Text.Trim().ToUpper();
                        address.Address = Txt_ConsigneeAddress.Text.Trim().ToUpper();
                        address.CountryCode = CountrySwitch(Txt_ConsigneeCountry.Text.Trim().ToUpper());
                        address.RegionCode = RegionSwitch(Txt_ConsigneeRegion.Text.Trim().ToUpper());
                        address.Provience = Txt_ConsigneeProvince.Text.Trim().ToUpper();
                        address.PostCode = Txt_ConsigneeZipCode.Text.Trim().ToUpper();
                        address.ContactorName = Txt_ConsigneeContactor.Text.Trim().ToUpper();
                        address.Phone = Txt_ConsigneeTel.Text.Trim().ToUpper();
                        address.CreateTime = DateTime.Now;
                        address.UpdateTime = DateTime.Now;
                        address.AddressType = 1;
                        address.Operator = "Admin";
                        _addressbookservice.AddAddressBook(address);
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('保存成功!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('保存成功！')</script>");
                        btn_ConsigneeAddress.Visible = false;
                    }
                    else
                    {
                        address.Name = Txt_DeliverName.Text.Trim().ToUpper();
                        address.Address = Txt_DeliverAddress.Text.Trim().ToUpper();
                        address.CountryCode = CountrySwitch(Txt_DeliverCountry.Text.Trim().ToUpper());
                        address.RegionCode = RegionSwitch(Txt_DeliverRegion.Text.Trim().ToUpper());
                        address.Provience = Txt_DeliverProvince.Text.Trim().ToUpper();
                        address.PostCode = Txt_DeliverZipCode.Text.Trim().ToUpper();
                        address.ContactorName = Txt_DeliverContactor.Text.Trim().ToUpper();
                        address.Phone = Txt_DeliverTel.Text.Trim().ToUpper();
                        address.CreateTime = DateTime.Now;
                        address.UpdateTime = DateTime.Now;
                        address.AddressType = 2;
                        address.Operator = "Admin";
                        _addressbookservice.AddAddressBook(address);
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('保存成功!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('保存成功！')</script>");
                        btn_DeliverAddress.Visible = false;
                    }
                }
            }
        }
        protected bool AddressName(string name, int type)
        {
            bool ok=false;
            IList<AddressBook> ressbook = null;
            if (Txt_Account1.Text.Trim().ToUpper() != "" && Txt_Account2.Text.Trim().ToUpper() != "")
            {
                if (type == 0)
                {
                    ressbook = _departmentservice.FindAllShipAddressesByDepCodeAndCompanyCode(Txt_Account2.Text.Trim().ToUpper(), Txt_Account1.Text.Trim().ToUpper());
                    
                }
                else if (type == 1)
                {
                    ressbook = _departmentservice.FindAllDeliveryAddressesByDepCodeAndCompanyCode(Txt_Account2.Text.Trim().ToUpper(), Txt_Account1.Text.Trim().ToUpper());
                }
                else
                {
                    ressbook = _departmentservice.FindAllForwarderAddressesByDepCodeAndCompanyCode(Txt_Account1.Text.Trim().ToUpper(), Txt_Account2.Text.Trim().ToUpper());
                }
                if (ressbook != null)
                {
                    foreach (AddressBook ressname in ressbook)
                    {
                        if (ressname.Name == name)
                        {
                            ok = true;
                            break;
                        }
                    }
                }
            }
            return ok;
        }
        protected bool Address(string name, int type)
        {
            bool ok = false;
            IList<AddressBook> ressbook = null;
            if (Txt_Account1.Text.Trim().ToUpper() != "" && Txt_Account2.Text.Trim().ToUpper() != "")
            {
                if (type == 0)
                {
                    ressbook = _departmentservice.FindAllShipAddressesByDepCodeAndCompanyCode(Txt_Account2.Text.Trim().ToUpper(), Txt_Account1.Text.Trim().ToUpper());

                }
                else if (type == 1)
                {
                    ressbook = _departmentservice.FindAllDeliveryAddressesByDepCodeAndCompanyCode(Txt_Account2.Text.Trim().ToUpper(), Txt_Account1.Text.Trim().ToUpper());
                }
                else
                {
                    ressbook = _departmentservice.FindAllForwarderAddressesByDepCodeAndCompanyCode(Txt_Account1.Text.Trim().ToUpper(), Txt_Account2.Text.Trim().ToUpper());
                }
                if (ressbook != null)
                {
                    foreach (AddressBook ressname in ressbook)
                    {
                        if (ressname.Address == name)
                        {
                            ok = true;
                            break;
                        }
                    }
                }
            }
            return ok;
        }
        protected bool AddressCountry(string name, int type)
        {
            bool ok = false;
            IList<AddressBook> ressbook = null;
            if (Txt_Account1.Text.Trim().ToUpper() != "" && Txt_Account2.Text.Trim().ToUpper() != "")
            {
                if (type == 0)
                {
                    ressbook = _departmentservice.FindAllShipAddressesByDepCodeAndCompanyCode(Txt_Account2.Text.Trim().ToUpper(), Txt_Account1.Text.Trim().ToUpper());

                }
                else if (type == 1)
                {
                    ressbook = _departmentservice.FindAllDeliveryAddressesByDepCodeAndCompanyCode(Txt_Account2.Text.Trim().ToUpper(), Txt_Account1.Text.Trim().ToUpper());
                }
                else
                {
                    ressbook = _departmentservice.FindAllForwarderAddressesByDepCodeAndCompanyCode(Txt_Account1.Text.Trim().ToUpper(), Txt_Account2.Text.Trim().ToUpper());
                }
                if (ressbook != null)
                {
                    foreach (AddressBook ressname in ressbook)
                    {
                        if (ressname.CountryCode == CountrySwitch(name))
                        {
                            ok = true;
                            break;
                        }
                    }
                }
            }
            return ok;
        }
        protected bool AddressRegion(string name, int type)
        {
            bool ok = false;
            IList<AddressBook> ressbook = null;
            if (Txt_Account1.Text.Trim().ToUpper() != "" && Txt_Account2.Text.Trim().ToUpper() != "")
            {
                if (type == 0)
                {
                    ressbook = _departmentservice.FindAllShipAddressesByDepCodeAndCompanyCode(Txt_Account2.Text.Trim().ToUpper(), Txt_Account1.Text.Trim().ToUpper());

                }
                else if (type == 1)
                {
                    ressbook = _departmentservice.FindAllDeliveryAddressesByDepCodeAndCompanyCode(Txt_Account2.Text.Trim().ToUpper(), Txt_Account1.Text.Trim().ToUpper());
                }
                else
                {
                    ressbook = _departmentservice.FindAllForwarderAddressesByDepCodeAndCompanyCode(Txt_Account1.Text.Trim().ToUpper(), Txt_Account2.Text.Trim().ToUpper());
                }
                if (ressbook != null)
                {
                    foreach (AddressBook ressname in ressbook)
                    {
                        if (ressname.RegionCode == RegionSwitch(name))
                        {
                            ok = true;
                            break;
                        }
                    }
                }
            }
            return ok;
        }
        protected bool AddressProvince(string name, int type)
        {
            bool ok = false;
            IList<AddressBook> ressbook = null;
            if (Txt_Account1.Text.Trim().ToUpper() != "" && Txt_Account2.Text.Trim().ToUpper() != "")
            {
                if (type == 0)
                {
                    ressbook = _departmentservice.FindAllShipAddressesByDepCodeAndCompanyCode(Txt_Account2.Text.Trim().ToUpper(), Txt_Account1.Text.Trim().ToUpper());

                }
                else if (type == 1)
                {
                    ressbook = _departmentservice.FindAllDeliveryAddressesByDepCodeAndCompanyCode(Txt_Account2.Text.Trim().ToUpper(), Txt_Account1.Text.Trim().ToUpper());
                }
                else
                {
                    ressbook = _departmentservice.FindAllForwarderAddressesByDepCodeAndCompanyCode(Txt_Account1.Text.Trim().ToUpper(), Txt_Account2.Text.Trim().ToUpper());
                }
                if (ressbook != null)
                {
                    foreach (AddressBook ressname in ressbook)
                    {
                        if (ressname.Provience == name)
                        {
                            ok = true;
                            break;
                        }
                    }
                }
            }
            return ok;
        }
        protected bool AddressZip(string name, int type)
        {
            bool ok = false;
            IList<AddressBook> ressbook = null;
            if (Txt_Account1.Text.Trim().ToUpper() != "" && Txt_Account2.Text.Trim().ToUpper() != "")
            {
                if (type == 0)
                {
                    ressbook = _departmentservice.FindAllShipAddressesByDepCodeAndCompanyCode(Txt_Account2.Text.Trim().ToUpper(), Txt_Account1.Text.Trim().ToUpper());

                }
                else if (type == 1)
                {
                    ressbook = _departmentservice.FindAllDeliveryAddressesByDepCodeAndCompanyCode(Txt_Account2.Text.Trim().ToUpper(), Txt_Account1.Text.Trim().ToUpper());
                }
                else
                {
                    ressbook = _departmentservice.FindAllForwarderAddressesByDepCodeAndCompanyCode(Txt_Account1.Text.Trim().ToUpper(), Txt_Account2.Text.Trim().ToUpper());
                }
                if (ressbook != null)
                {
                    foreach (AddressBook ressname in ressbook)
                    {
                        if (ressname.PostCode == name)
                        {
                            ok = true;
                            break;
                        }
                    }
                }
            }
            return ok;
        }
        protected bool AddressContactorName(string name, int type)
        {
            bool ok = false;
            IList<AddressBook> ressbook = null;
            if (Txt_Account1.Text.Trim().ToUpper() != "" && Txt_Account2.Text.Trim().ToUpper() != "")
            {
                if (type == 0)
                {
                    ressbook = _departmentservice.FindAllShipAddressesByDepCodeAndCompanyCode(Txt_Account2.Text.Trim().ToUpper(), Txt_Account1.Text.Trim().ToUpper());

                }
                else if (type == 1)
                {
                    ressbook = _departmentservice.FindAllDeliveryAddressesByDepCodeAndCompanyCode(Txt_Account2.Text.Trim().ToUpper(), Txt_Account1.Text.Trim().ToUpper());
                }
                else
                {
                    ressbook = _departmentservice.FindAllForwarderAddressesByDepCodeAndCompanyCode(Txt_Account1.Text.Trim().ToUpper(), Txt_Account2.Text.Trim().ToUpper());
                }
                if (ressbook != null)
                {
                    foreach (AddressBook ressname in ressbook)
                    {
                        if (ressname.ContactorName == name)
                        {
                            ok = true;
                            break;
                        }
                    }
                }
            }
            return ok;
        }
        protected bool AddressTel(string name, int type)
        {
            bool ok = false;
            IList<AddressBook> ressbook = null;
            if (Txt_Account1.Text.Trim().ToUpper() != "" && Txt_Account2.Text.Trim().ToUpper() != "")
            {
                if (type == 0)
                {
                    ressbook = _departmentservice.FindAllShipAddressesByDepCodeAndCompanyCode(Txt_Account2.Text.Trim().ToUpper(), Txt_Account1.Text.Trim().ToUpper());

                }
                else if (type == 1)
                {
                    ressbook = _departmentservice.FindAllDeliveryAddressesByDepCodeAndCompanyCode(Txt_Account2.Text.Trim().ToUpper(), Txt_Account1.Text.Trim().ToUpper());
                }
                else
                {
                    ressbook = _departmentservice.FindAllForwarderAddressesByDepCodeAndCompanyCode(Txt_Account1.Text.Trim().ToUpper(), Txt_Account2.Text.Trim().ToUpper());
                }
                if (ressbook != null)
                {
                    foreach (AddressBook ressname in ressbook)
                    {
                        if (ressname.Phone == name)
                        {
                            ok = true;
                            break;
                        }
                    }
                }
            }
            return ok;
        }

        private bool TextEmpty(int type)
        {
            bool ok = true;
            if (type == 3)
            {
                if (Txt_Account1.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('客户账号不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('客户账号不能为空！')</script>");
                    Txt_Account1.Focus();
                    ok = false;
                }
                else if (Txt_ShipperName.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('发件人公司名称不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发件人公司名称不能为空！')</script>");
                    Txt_ShipperName.Focus();
                    ok = false;
                }
                else if (Txt_ShipperCountry.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('发件人国家不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发件人国家不能为空！')</script>");
                    Txt_ShipperCountry.Focus();
                }
                else if (Txt_ShipperRegion.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('发件人地区不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发件人地区不能为空！')</script>");
                    Txt_ShipperRegion.Focus();
                    ok = false;
                }
                else if (Txt_ShipperZipCode.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('发件人邮编不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发件人邮编不能为空！')</script>");
                    Txt_ShipperZipCode.Focus();
                    ok = false;
                }
                else if (Txt_ShipperAddress.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('发件人地址不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发件人地址不能为空！')</script>");
                    Txt_ShipperAddress.Focus();
                    ok = false;
                }
                else if (Txt_ShipperContactor.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('发件人姓名不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发件人姓名不能为空！')</script>");
                    Txt_ShipperContactor.Focus();
                    ok = false;
                }
                else if (Txt_ShipperTel.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('发件人电话不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发件人电话不能为空！')</script>");
                    Txt_ShipperTel.Focus();
                    ok = false;
                }
                else if (Txt_ConsigneeName.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('收件人公司名称不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人公司名称不能为空！')</script>");
                    Txt_ConsigneeName.Focus();
                    ok = false;
                }
                else if (Txt_ConsigneeCountry.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('收件人国家不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人国家不能为空！')</script>");
                    Txt_ConsigneeCountry.Focus();
                    ok = false;
                }
                else if (Txt_ConsigneeRegion.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('收件人地区不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人地区不能为空！')</script>");
                    Txt_ConsigneeRegion.Focus();
                    ok = false;
                }
                else if (Txt_ConsigneeZipCode.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('收件人邮编不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人地区不能为空！')</script>");
                    Txt_ConsigneeZipCode.Focus();
                    ok = false;
                }
                else if (Txt_ConsigneeAddress.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('收件人地址不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人地区不能为空！')</script>");
                    Txt_ConsigneeAddress.Focus();
                    ok = false;
                }
                else if (Txt_ConsigneeContactor.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('收件人姓名不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人姓名不能为空！')</script>");
                    Txt_ConsigneeContactor.Focus();
                    ok = false;
                }
                else if (Txt_ConsigneeTel.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('收件人电话不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人电话不能为空！')</script>");
                    Txt_ConsigneeTel.Focus();
                    ok = false;
                }
                else if (Deliver.Visible == true)
                {
                    if (Txt_DeliverName.Text.Trim().ToUpper() == "")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('交付人公司名称不能为空!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('交付人公司名称不能为空！')</script>");
                        Txt_DeliverName.Focus();
                        ok = false;
                    }
                    else if (Txt_DeliverCountry.Text.Trim().ToUpper() == "")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('交付人国家不能为空!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('交付人国家不能为空！')</script>");
                        Txt_DeliverCountry.Focus();
                        ok = false;
                    }
                    else if (Txt_DeliverRegion.Text.Trim().ToUpper() == "")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('交付人地区不能为空!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('交付人地区不能为空！')</script>");
                        Txt_DeliverRegion.Focus();
                        ok = false;
                    }
                    else if (Txt_DeliverZipCode.Text.Trim().ToUpper() == "")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('交付人邮编不能为空!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('交付人邮编不能为空！')</script>");
                        Txt_DeliverZipCode.Focus();
                        ok = false;
                    }
                    else if (Txt_DeliverAddress.Text.Trim().ToUpper() == "")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('交付人地址不能为空!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('交付人地址不能为空！')</script>");
                        Txt_DeliverAddress.Focus();
                        ok = false;
                    }
                    else if (Txt_DeliverContactor.Text.Trim().ToUpper() == "")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('交付人姓名不能为空!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('交付人姓名不能为空！')</script>");
                        Txt_DeliverContactor.Focus();
                        ok = false;
                    }
                    else if (Txt_DeliverTel.Text.Trim().ToUpper() == "")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('交付人电话不能为空!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('交付人电话不能为空！')</script>");
                        Txt_DeliverTel.Focus();
                    }
                    else
                    {
                        if (!RTel.IsMatch(Txt_ShipperTel.Text.Trim().ToUpper()))
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('发件人电话只能输入数字!')", true);
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发件人电话只能输入数字！')</script>");
                            Txt_ShipperTel.Focus();
                            ok = false;
                        }
                        else if (!RTel.IsMatch(Txt_ConsigneeTel.Text.Trim().ToUpper()))
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('收件人电话只能输入数字!')", true);
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人电话只能输入数字！')</script>");
                            Txt_ConsigneeTel.Focus();
                            ok = false;
                        }
                        else if (!RTel.IsMatch(Txt_DeliverTel.Text.Trim().ToUpper()))
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('交付人电话只能输入数字!')", true);
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人电话只能输入数字！')</script>");
                            Txt_DeliverTel.Focus();
                            ok = false;
                        }
                        else if (!RRegion.IsMatch(Txt_ShipperCountry.Text.Trim().ToUpper()) && !string.IsNullOrEmpty(CountrySwitch(Txt_ShipperCountry.Text.Trim().ToUpper())))
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('发件人国家只能输入字母!')", true);
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发件人国家只能输入字母！')</script>");
                            Txt_ShipperCountry.Focus();
                            ok = false;
                        }
                        else if (!RRegion.IsMatch(Txt_ShipperRegion.Text.Trim().ToUpper()) && !string.IsNullOrEmpty(RegionSwitch(Txt_ShipperRegion.Text.Trim().ToUpper())))
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('发件人地区只能输入字母!')", true);
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发件人地区只能输入字母！')</script>");
                            Txt_ShipperRegion.Focus();
                            ok = false;
                        }
                        else if (!RRegion.IsMatch(Txt_ConsigneeCountry.Text.Trim().ToUpper()) && !string.IsNullOrEmpty(CountrySwitch(Txt_ConsigneeCountry.Text.Trim().ToUpper())))
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('收件人国家只能输入字母!')", true);
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人国家只能输入字母！')</script>");
                            Txt_ConsigneeCountry.Focus();
                            ok = false;
                        }
                        else if (!RRegion.IsMatch(Txt_ConsigneeRegion.Text.Trim().ToUpper()) && !string.IsNullOrEmpty(RegionSwitch(Txt_ConsigneeRegion.Text.Trim().ToUpper())))
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('收件人地区只能输入字母!')", true);
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人地区只能输入字母！')</script>");
                            Txt_ConsigneeRegion.Focus();
                            ok = false;
                        }
                        else if (!RRegion.IsMatch(Txt_DeliverCountry.Text.Trim().ToUpper()) && !string.IsNullOrEmpty(CountrySwitch(Txt_DeliverCountry.Text.Trim().ToUpper())))
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('交付人国家只能输入字母!')", true);
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('交付人国家只能输入字母！')</script>");
                            Txt_DeliverCountry.Focus();
                            ok = false;
                        }
                        else if (!RRegion.IsMatch(Txt_DeliverRegion.Text.Trim().ToUpper()) && !string.IsNullOrEmpty(RegionSwitch(Txt_DeliverRegion.Text.Trim().ToUpper())))
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('交付件人地区只能输入字母!')", true);
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('交付件人地区只能输入字母！')</script>");
                            Txt_DeliverRegion.Focus();
                            ok = false;
                        }
                    }
                }
                else
                {

                    if (!RTel.IsMatch(Txt_ShipperTel.Text.Trim().ToUpper()))
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('发件人电话只能输入数字!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发件人电话只能输入数字！')</script>");
                        Txt_ShipperTel.Focus();
                        ok = false;
                    }
                    else if (!RTel.IsMatch(Txt_ConsigneeTel.Text.Trim().ToUpper()))
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('收件人电话只能输入数字!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人电话只能输入数字！')</script>");
                        Txt_ConsigneeTel.Focus();
                        ok = false;
                    }
                    //else if (!RTel.IsMatch(Txt_DeliverTel.Text.Trim().ToUpper()))
                    //{
                    //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('交付件人电话只能输入数字!')", true);
                    //    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人电话只能输入数字！')</script>");
                    //    Txt_DeliverTel.Focus();
                    //    ok = false;
                    //}
                    else if (!RRegion.IsMatch(Txt_ShipperCountry.Text.Trim().ToUpper()) && !string.IsNullOrEmpty(CountrySwitch(Txt_ShipperCountry.Text.Trim().ToUpper())))
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('发件人国家只能输入字母!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发件人国家只能输入字母！')</script>");
                        Txt_ShipperCountry.Focus();
                        ok = false;
                    }
                    else if (!RRegion.IsMatch(Txt_ShipperRegion.Text.Trim().ToUpper()) && !string.IsNullOrEmpty(RegionSwitch(Txt_ShipperRegion.Text.Trim().ToUpper())))
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('发件人地区只能输入字母!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发件人地区只能输入字母！')</script>");
                        Txt_ShipperRegion.Focus();
                        ok = false;
                    }
                    else if (!RRegion.IsMatch(Txt_ConsigneeCountry.Text.Trim().ToUpper()) && !string.IsNullOrEmpty(CountrySwitch(Txt_ConsigneeCountry.Text.Trim().ToUpper())))
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('收件人国家只能输入字母!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人国家只能输入字母！')</script>");
                        Txt_ConsigneeCountry.Focus();
                        ok = false;
                    }
                    else if (!RRegion.IsMatch(Txt_ConsigneeRegion.Text.Trim().ToUpper()) && !string.IsNullOrEmpty(RegionSwitch(Txt_ConsigneeRegion.Text.Trim().ToUpper())))
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('收件人地区只能输入字母!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人地区只能输入字母！')</script>");
                        Txt_ConsigneeRegion.Focus();
                        ok = false;
                    }
                }
            }
            else if (type == 0)
            {
                if (Txt_Account1.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('客户账号不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('客户账号不能为空！')</script>");
                    Txt_Account1.Focus();
                    ok = false;
                }
                else if (Txt_ShipperName.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('发件人公司名称不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发件人公司名称不能为空！')</script>");
                    Txt_ShipperName.Focus();
                    ok = false;
                }
                else if (Txt_ShipperCountry.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('发件人国家不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发件人国家不能为空！')</script>");
                    Txt_ShipperCountry.Focus();
                }
                else if (Txt_ShipperRegion.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('发件人地区不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发件人地区不能为空！')</script>");
                    Txt_ShipperRegion.Focus();
                    ok = false;
                }
                else if (Txt_ShipperZipCode.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('发件人邮编不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发件人邮编不能为空！')</script>");
                    Txt_ShipperZipCode.Focus();
                    ok = false;
                }
                else if (Txt_ShipperAddress.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('发件人地址不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发件人地址不能为空！')</script>");
                    Txt_ShipperAddress.Focus();
                    ok = false;
                }
                else if (Txt_ShipperContactor.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('发件人姓名不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发件人姓名不能为空！')</script>");
                    Txt_ShipperContactor.Focus();
                    ok = false;
                }
                else if (Txt_ShipperTel.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('发件人电话不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发件人电话不能为空！')</script>");
                    Txt_ShipperTel.Focus();
                    ok = false;
                }
                else
                {
                    if (!RTel.IsMatch(Txt_ShipperTel.Text.Trim().ToUpper()))
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('发件人电话只能输入数字!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发件人电话只能输入数字！')</script>");
                        Txt_ShipperTel.Focus();
                        ok = false;
                    }
                    else if (!RRegion.IsMatch(Txt_ShipperCountry.Text.Trim().ToUpper()) && !string.IsNullOrEmpty(CountrySwitch(Txt_ShipperCountry.Text.Trim().ToUpper())))
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('发件人国家只能输入字母!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发件人国家只能输入字母！')</script>");
                        Txt_ShipperCountry.Focus();
                        ok = false;
                    }
                    else if (!RRegion.IsMatch(Txt_ShipperRegion.Text.Trim().ToUpper()) && !string.IsNullOrEmpty(RegionSwitch(Txt_ShipperRegion.Text.Trim().ToUpper())))
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('发件人地区只能输入字母!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发件人地区只能输入字母！')</script>");
                        Txt_ShipperRegion.Focus();
                        ok = false;
                    }
                }
            }
            else if(type==1)
            {
                if (Txt_Account1.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('客户账号不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('客户账号不能为空！')</script>");
                    Txt_Account1.Focus();
                    ok = false;
                }
                else if (Txt_ConsigneeName.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('收件人公司名称不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人公司名称不能为空！')</script>");
                    Txt_ConsigneeName.Focus();
                    ok = false;
                }
                else if (Txt_ConsigneeCountry.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('收件人国家不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人国家不能为空！')</script>");
                    Txt_ConsigneeCountry.Focus();
                    ok = false;
                }
                else if (Txt_ConsigneeRegion.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('收件人地区不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人地区不能为空！')</script>");
                    Txt_ConsigneeRegion.Focus();
                    ok = false;
                }
                else if (Txt_ConsigneeZipCode.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('收件人邮编不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人地区不能为空！')</script>");
                    Txt_ConsigneeZipCode.Focus();
                    ok = false;
                }
                else if (Txt_ConsigneeAddress.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('收件人地址不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人地区不能为空！')</script>");
                    Txt_ConsigneeAddress.Focus();
                    ok = false;
                }
                else if (Txt_ConsigneeContactor.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('收件人姓名不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人姓名不能为空！')</script>");
                    Txt_ConsigneeContactor.Focus();
                    ok = false;
                }
                else if (Txt_ConsigneeTel.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('收件人电话不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人电话不能为空！')</script>");
                    Txt_ConsigneeTel.Focus();
                    ok = false;
                }
                else
                {
                    if (!RTel.IsMatch(Txt_ConsigneeTel.Text.Trim().ToUpper()))
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('收件人电话只能输入数字!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人电话只能输入数字！')</script>");
                        Txt_ConsigneeTel.Focus();
                        ok = false;
                    }
                    else if (!RRegion.IsMatch(Txt_ConsigneeCountry.Text.Trim().ToUpper()) && !string.IsNullOrEmpty(CountrySwitch(Txt_ConsigneeCountry.Text.Trim().ToUpper())))
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('收件人国家只能输入字母!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人国家只能输入字母！')</script>");
                        Txt_ConsigneeCountry.Focus();
                        ok = false;
                    }
                    else if (!RRegion.IsMatch(Txt_ConsigneeRegion.Text.Trim().ToUpper()) && !string.IsNullOrEmpty(RegionSwitch(Txt_ConsigneeRegion.Text.Trim().ToUpper())))
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('收件人地区只能输入字母!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人地区只能输入字母！')</script>");
                        Txt_ConsigneeRegion.Focus();
                        ok = false;
                    }
                }
            }
            else if (type == 2)
            {
                if (Txt_Account1.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('客户账号不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('客户账号不能为空！')</script>");
                    Txt_Account1.Focus();
                    ok = false;
                }
                else if (Txt_DeliverName.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('交付人公司名称不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('交付人公司名称不能为空！')</script>");
                    Txt_DeliverName.Focus();
                    ok = false;
                }
                else if (Txt_DeliverCountry.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('交付人国家不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('交付人国家不能为空！')</script>");
                    Txt_DeliverCountry.Focus();
                    ok = false;
                }
                else if (Txt_DeliverRegion.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('交付人地区不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('交付人地区不能为空！')</script>");
                    Txt_DeliverRegion.Focus();
                    ok = false;
                }
                else if (Txt_DeliverZipCode.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('交付人邮编不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('交付人邮编不能为空！')</script>");
                    Txt_DeliverZipCode.Focus();
                    ok = false;
                }
                else if (Txt_DeliverAddress.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('交付人地址不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('交付人地址不能为空！')</script>");
                    Txt_DeliverAddress.Focus();
                    ok = false;
                }
                else if (Txt_DeliverContactor.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('交付人姓名不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('交付人姓名不能为空！')</script>");
                    Txt_DeliverContactor.Focus();
                    ok = false;
                }
                else if (Txt_DeliverTel.Text.Trim().ToUpper() == "")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('交付人电话不能为空!')", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('交付人电话不能为空！')</script>");
                    Txt_DeliverTel.Focus();
                }
                else
                {
                    if (!RTel.IsMatch(Txt_DeliverTel.Text.Trim().ToUpper()))
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('交付人电话只能输入数字!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('收件人电话只能输入数字！')</script>");
                        Txt_DeliverTel.Focus();
                        ok = false;
                    }
                    else if (!RRegion.IsMatch(Txt_DeliverCountry.Text.Trim().ToUpper()) && !string.IsNullOrEmpty(CountrySwitch(Txt_DeliverCountry.Text.Trim().ToUpper())))
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('交付人国家只能输入字母!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('交付人国家只能输入字母！')</script>");
                        Txt_DeliverCountry.Focus();
                        ok = false;
                    }
                    else if (!RRegion.IsMatch(Txt_DeliverRegion.Text.Trim().ToUpper()) && !string.IsNullOrEmpty(RegionSwitch(Txt_DeliverRegion.Text.Trim().ToUpper())))
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('交付件人地区只能输入字母!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('交付件人地区只能输入字母！')</script>");
                        Txt_DeliverRegion.Focus();
                        ok = false;
                    }
                }
            }
            return ok;
        }


        public void SetLanguage(string langageType)
        {
            //Response.Cookies["LanType"].Value = langageType;
            Session["LanType"] = langageType;

            CultureInfo culture = new CultureInfo(langageType);
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            Server.Transfer(Request.Path);
        }
        protected void btn_ConsigneeName_Click(object sender, EventArgs e)
        {
            Storage();
            Session["compayCode"] = Txt_Account1.Text.Trim().ToUpper();
            Session["DepCode"] = Txt_Account2.Text.Trim().ToUpper();
            Session["historytype"] = "Consignee";
            Session["name"] = Txt_ConsigneeName.Text.Trim().ToUpper();
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>OpenShipperhistory()</script>");
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "OpenShipperhistory()", true);
            if (!AddressName(Txt_ConsigneeName.Text.Trim().ToUpper(), 1))
            {
                btn_ConsigneeAddress.Visible = true;
            }
            else
            {
                if (btn_ConsigneeAddress.Visible != true)
                {
                    btn_ConsigneeAddress.Visible = false;
                }
            }
            Txt_ConsigneeAddress.Focus();
        }
        protected override void InitializeCulture()
        {
            if (Session["LanType"] != null)
            {
                string langageType = Session["LanType"].ToString();
                CultureInfo culture = new CultureInfo(langageType);
                System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            }
            base.InitializeCulture();
        }

    }
}