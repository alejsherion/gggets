using System;
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
    public partial class AddressBookAdd : System.Web.UI.Page
    {
        private static Regex RTel = new Regex(@"^[0-9]*$");
        private static Regex RRegion = new Regex(@"^[A-Za-z]");
        private static IList<CountryCode> listcountry;
        private static IList<RegionCode> listregion;
        private IDepartmentManagementService _departmentservice;
        private IAddressBookManagementService _addressbookservice;
        private static ICountryCodeManagementService _countryservice;
        private static IRegionCodeManagementService _regionservice;
        private IUserManagementService _userService;
        private ICompanyManagementService _companyService;
        private ISysUserManagementService _sysUserManagementService;
        private string type;
        private AddressBook address;
        private Department depar;
        private ETS.GGGETSApp.Domain.Application.Entities.User user;
        protected AddressBookAdd()
        { }
        public AddressBookAdd(ICompanyManagementService companyservice, ICountryCodeManagementService countryservice, IRegionCodeManagementService regionservice, IDepartmentManagementService departmentservice, IAddressBookManagementService addressbookservice, IUserManagementService userservice, ISysUserManagementService sysUserManagementService)
        {
            _departmentservice = departmentservice;
            _addressbookservice = addressbookservice;
            _countryservice = countryservice;
            _regionservice = regionservice;
            _userService = userservice;
            _companyService = companyservice;
            _sysUserManagementService = sysUserManagementService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] != null)
                {

                    listcountry = _countryservice.FindAllCountries();//获取国家二字码
                    listregion = _regionservice.FindAllRegionCodes();//获取地区三字码
                    Guid id = (Guid)Session["UserID"];
                    ModulePrivilege Mprivlege = _sysUserManagementService.GetPrivilegeByUserid(id);
                    if (!(bool)Mprivlege.AddPrivilege)
                    {
                        btn_AddDeliver.Enabled = false;
                    }
                }
            }
        }
        /// <summary>
        /// 验证是否是已注册的公司账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Txt_CompanyCode_TextChanged(object sender, EventArgs e)
        {
            if (!CompCode())
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有该公司，请重新输入!')", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('没有该公司，请重新输入！')</script>");
                Txt_CompanyCode.Focus();
                Txt_CompanyCode.Text = "";
            }
        }
        private bool CompCode()
        {
            bool ok = false;
            if (Txt_CompanyCode.Text.Trim() != "")
            {
                Company company = _companyService.FindCompanyByCompanyCode(Txt_CompanyCode.Text.Trim());
                if (company != null)
                {
                    ok = true;
                }
            }
            return ok;
        }
        /// <summary>
        /// 验证是否已注册的部门账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Txt_Code_TextChanged(object sender, EventArgs e)
        {

            DepCode();
        }

        private void DepCode()
        {
            if (Txt_CompanyCode.Text.Trim() != "")
            {
                if (Txt_Code.Text.Trim() != "")
                {
                    depar = _departmentservice.FindDepartmentByDepCodeAndCompanyCode(Txt_Code.Text.Trim(), Txt_CompanyCode.Text.Trim());
                    if (depar == null)
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有该部门账号，请重新输入!')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('没有该部门账号，请重新输入！')</script>");
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
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请重新输入部门账号！')</script>");
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请重新输入部门账号!')", true);
                    Txt_Code.Focus();
                }

            }
            else
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请重新输入公司账号！')</script>");
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请重新输入公司账号!')", true);
                Txt_CompanyCode.Focus();
            }
        }
        /// <summary>
        /// 验证是否是已注册的个人账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Txt_LoginName_TextChanged(object sender, EventArgs e)
        {
            if (!LoginName())
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有该用户名，请重新输入!')", true);
                Txt_LoginName.Focus();
                Txt_LoginName.Text = "";
            }
        }
        private bool LoginName()
        {
            bool ok = false;
            if (Txt_LoginName.Text.Trim() != "")
            {
                user = _userService.FindUserByLoginName(Txt_LoginName.Text.Trim());
                if (user != null)
                {
                    ok = true;
                    Session["User"] = user.UID;
                    Txt_DeliverName.Focus();
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('没有该用户名，请重新输入！')</script>");
                    
                }
            }
            return ok;
        }
        /// <summary>
        /// 自动填充国家二字码
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

        /// <summary>
        /// 自动填充地区三字码
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <param name="contextKey"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 验证是否正确的国家码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            if (autoDeliverRegion.ContextKey != "" && autoDeliverRegion.ContextKey != null && ok == true)
            {
                Txt_DeliverProvince.Focus();
            }
            else
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入正确的国家！')</script>");
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请输入正确的国家!')", true);
                Txt_DeliverCountry.Focus();
                Txt_DeliverCountry.Text = string.Empty;
            }
        }
        protected void autoDeliveCountry_ItemSelected(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 验证是否正确的地区码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入正确的城市！')</script>");
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请输入正确的城市!')", true);
                Txt_DeliverRegion.Focus();
            }
        }

        /// <summary>
        /// 新建地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_AddDeliver_Click(object sender, EventArgs e)
        {
            string Uid = "";
            string Did = "";
            if (Txt_CompanyCode.Text.Trim() == "" && Txt_Code.Text.Trim() == "" && Txt_LoginName.Text.Trim() == "")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('部门账号与用户名必须填写一个！')</script>");
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('部门账号与用户名必须填写一个!')", true);
                Txt_LoginName.Focus();
            }
            else if (Txt_CompanyCode.Text.Trim() != "" && Txt_Code.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('部门账号必须填写!')", true);
                Txt_Code.Focus();
            }
            else if (!CompCode())
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有该公司，请重新输入!')", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('没有该公司，请重新输入！')</script>");
                Txt_CompanyCode.Focus();
                Txt_CompanyCode.Text = "";
            }
            else if (!LoginName())
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有该用户名，请重新输入!')", true);
                Txt_LoginName.Focus();
                Txt_LoginName.Text = "";
            }
            else
            {
                if (Session["Depar"] != null)
                {
                    Did = Session["Depar"].ToString();
                }
                else
                {
                    DepCode();
                    Did = Session["Depar"].ToString();
                }
                Uid = Session["User"].ToString();
                AddRessBook(Uid, Did);
            }
                           
        }
        /// <summary>
        /// 用户输入验证
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="DID"></param>
        protected void AddRessBook(string UID,string DID)
        {
            if (Txt_DeliverName.Text.Trim() == "")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('公司名称不能为空!')", true);
                Txt_DeliverName.Focus();
            }
            else if (Txt_DeliverAddress.Text.Trim() == "")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('地址不能为空!')", true);
                Txt_DeliverAddress.Focus();
            }
            else if (Txt_DeliverCountry.Text.Trim() == "")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('国家不能为空!')", true);
                Txt_DeliverCountry.Focus();
            }
            else if (Txt_DeliverRegion.Text.Trim() == "")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空!')</script>");
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('地区不能为空!')", true);
                Txt_DeliverRegion.Focus();
            }
            else if (Txt_DeliverZipCode.Text.Trim() == "")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('邮编不能为空!')", true);
                Txt_DeliverZipCode.Focus();
            }

            else if (Txt_DeliverContactor.Text.Trim() == "")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('联系人不能为空!')", true);
                Txt_DeliverContactor.Focus();
            }
            else if (Txt_DeliverTel.Text.Trim() == "")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('电话号码不能为空!')", true);
                Txt_DeliverTel.Focus();
            }
            else
            {
                if (!RTel.IsMatch(Txt_DeliverTel.Text.Trim()))
                {
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('电话号码格式不正确！')</script>");
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('电话号码格式不正确!')", true);
                    Txt_DeliverTel.Focus();
                }
                else
                {
                    address = new AddressBook();
                    address.AID = Guid.NewGuid();
                    if (UID != "")
                    {
                        address.UID = Guid.Parse(UID);
                    }
                    if (DID != "")
                    {
                        address.DID = Guid.Parse(DID);
                    }
                    address.Name = Txt_DeliverName.Text.Trim().ToUpper();
                    address.Address = Txt_DeliverAddress.Text.Trim().ToUpper();
                    address.CountryCode = CountrySwitch(Txt_DeliverCountry.Text.Trim().ToUpper());
                    address.Provience = Txt_DeliverProvince.Text.Trim().ToUpper();
                    address.RegionCode = RegionSwitch(Txt_DeliverRegion.Text.Trim().ToUpper());
                    address.PostCode = Txt_DeliverZipCode.Text.Trim().ToUpper();
                    address.ContactorName = Txt_DeliverContactor.Text.Trim().ToUpper();
                    address.Phone = Txt_DeliverTel.Text.Trim().ToUpper();
                    address.AddressType = int.Parse(ddl_AddressBookType.SelectedValue);
                    address.CreateTime = DateTime.Now;
                    address.UpdateTime = DateTime.Now;
                    address.Operator = "admin";
                    _addressbookservice.AddAddressBook(address);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('添加成功！')</script>");
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('添加成功!')", true);
                    InitialControl(this.Controls);
                    Session.Remove("User");
                    Session.Remove("Depar");
                }
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
        /// <summary>
        /// 清空页面数据
        /// </summary>
        /// <param name="objControlCollection"></param>
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

        
    }
}