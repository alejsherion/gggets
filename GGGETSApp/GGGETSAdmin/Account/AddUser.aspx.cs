//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        添加用户
// 作成者				hong.li
// 改版日				2011.02.25
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using GGGETSAdmin.Common;

namespace GGGETSAdmin.Account
{
    public partial class AddUser : System.Web.UI.Page
    {
        #region 构造函数以及字段
        private readonly ISysUserManagementService _sysUserManagementService;
        private readonly IRoleManagementService _roleManagementService;
        private static ICountryCodeManagementService _countryservice;
        private static IRegionCodeManagementService _regionservice;
        protected AddUser()
        {
            
        }
        public AddUser(ISysUserManagementService sysUserManagementService
                       , IRoleManagementService roleManagementService
                        , ICountryCodeManagementService countryservice
                        , IRegionCodeManagementService regionservice)
        {
            _sysUserManagementService = sysUserManagementService;
            _roleManagementService = roleManagementService;
            _countryservice = countryservice;
            _regionservice = regionservice;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindRoleId();
                BindStatus();
                var id = Request["Id"];
                //var id = "ab6f8a54-be77-4bcf-a5c2-ee711c04f65b";
                if (String.IsNullOrEmpty(id))
                {
                    ViewState["OpStatus"] = "Add";
                    var user = new SysUser();
                    DataBindHelper<SysUser>.Bind(form1, user);
                    Session["SysUser"] = user;
                    trPassword.Visible = true;
                    Session.Remove("Condtion");
                }
                else
                {
                    ViewState["OpStatus"] = "Update";
                    var guidRoleId = new Guid(id);
                    BindControl(guidRoleId);
                    trPassword.Visible = false;
                }
            }
        }

        #region 事件

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_login_Click(object sender, EventArgs e)
        {

            var opStatus = Convert.ToString(ViewState["OpStatus"]);
            var user = (SysUser)Session["SysUser"];
            if (user == null)
            {
                //Page.ClientScript.RegisterStartupScript(GetType(), ""
                //                                  , "alert('操作失败!')", true);
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('操作失败!')", true);
                return;
            }
            try
            {
                DataBindHelper<SysUser>.GetDaTa(form1, user);
            }
            catch (Exception ce)
            {
                //Page.ClientScript.RegisterStartupScript(GetType(), ""
                                               //, "alert('" + ce.Message + "!')", true);
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('" + ce.Message + "!')", true);
                return;
            }
            try
            {
                if (opStatus == "Add")
                {
                    var result = Validation();
                    if (!result) return;
                    user.UID = Guid.NewGuid();
                    user.CreateTime = DateTime.Now;
                    user.UpdateTime = DateTime.Now;
                    user.Operator = "Admin";
                    GetRoles(user);
                    _sysUserManagementService.Add(user);
                }
                else
                {
                    GetRoles(user);
                    user.UpdateTime = DateTime.Now;
                    user.Operator = "Admin";
                    _sysUserManagementService.Modify(user);
                }
                //Page.ClientScript.RegisterStartupScript(GetType(), ""
                //                                      , "alert('操作成功!')", true);
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('操作成功!')", true);
            }
            catch(Exception ex)
            {
                if (!ex.Message.Contains("登录名"))
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('操作失败!')", true);
                else
                 
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('" + ex.Message + "!')", true);
            }
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserManager.aspx");
        }
        #endregion

        #region 公有方法
        #endregion

        #region 私有方法
        /// <summary>
        /// 绑定角色
        /// </summary>
        private void BindRoleId()
        {
            var dataSouce = _roleManagementService.GetAllRole();
            chkRoleID.DataSource = dataSouce;
            chkRoleID.DataTextField = "Name";
            chkRoleID.DataValueField = "RoleID";
            chkRoleID.DataBind();
        }

        /// <summary>
        /// 绑定会员
        /// </summary>
        private void BindStatus()
        {
            var dataSouce = DataConversion.ListTypeForEnum(typeof(Status));
            ddlStatus.DataSource = dataSouce;
            ddlStatus.DataTextField = "text";
            ddlStatus.DataValueField = "value";
            ddlStatus.DataBind();
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="user"></param>
        private void GetRoles(SysUser user)
        {
            if (user == null) return;
            var items = chkRoleID.Items;
            user.SysUser_Role.Clear();
            foreach (ListItem item in items)
            {
                if (!item.Selected) continue;
                var rp = new SysUser_Role
                {
                    SysUser_RoleID = Guid.NewGuid(),
                    RoleID =  new Guid(item.Value),
                    UID = user.UID,
                    LastUpdateTime = DateTime.Now
                };
                user.SysUser_Role.Add(rp);
            }
        }

        /// <summary>
        /// 绑定页面数据
        /// </summary>
        /// <param name="id"></param>
        private void BindControl(Guid id)
        {
            if (_roleManagementService == null) return;
            try
            {
                var user = _sysUserManagementService.GetUserById(id);
                DataBindHelper<SysUser>.Bind(form1, user);
                BindRoleIdFormModel(user);
                txtPassword.Attributes["value"] = user.Password;
                Session["SysUser"] = user;
            }
            catch
            {
                //Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                    //, "alert('!')", true);
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('加载数据失败!')", true);
            }

        }

        /// <summary>
        /// 读取数据绑定权限
        /// </summary>
        /// <param name="user">用户实体</param>
        private void BindRoleIdFormModel(SysUser user)
        {
            if (user == null) return;
            chkRoleID.ClearSelection();
            var items = chkRoleID.Items;
            if (items == null || items.Count == 0) return;
            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var value = new Guid(item.Value);
                var temprp = user.SysUser_Role.Where(it => it.RoleID == value);
                if (temprp != null && temprp.Count() > 0)
                {
                    item.Selected = true;
                }
            }
        }

        /// <summary>
        /// 页面数据验证
        /// </summary>
        /// <returns></returns>
        private bool Validation()
        {
            var result = true;
            var opStatus = Convert.ToString(ViewState["OpStatus"]);
             if (opStatus == "Add")
              {
                  var cnfirmPassword = txtConfirmPwd.Text.Trim();
                 if(String.IsNullOrEmpty(cnfirmPassword))
                 {
                     //Page.ClientScript.RegisterStartupScript(GetType(), ""
                                                       //, "alert('!')", true);
                     ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请填写确认密码!')", true);
                     result = false;
                 }
              }
             return result;

        }

        /// <summary>
        /// 国家二字码填充
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
        /// 地区三字码填充
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
        /// 验证是否正确的国家
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void txtCountryCode_TextChanged(object sender, EventArgs e)
        {
            txtRegionCode.Text = "";
            bool ok = false;
            IList<CountryCode> country = _countryservice.FindAllCountries();
            foreach (CountryCode countrycode in country)
            {
                if (countrycode.CountryName == txtCountryCode.Text.Trim().ToUpper())
                {
                    autoRegion.ContextKey = countrycode.ID.ToString();
                    ok = true;
                    break;
                }
            }
            if (string.IsNullOrEmpty(autoRegion.ContextKey) && ok != true)
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入正确的国家！')</script>");
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请输入正确的国家!')", true);
                txtCountryCode.Focus();
                txtCountryCode.Text = string.Empty;
            }
            else
            {
                txtCountryCode.Text = CountrySwitch(txtCountryCode.Text.Trim().ToUpper(), 0);
            }


        }
        protected void autoCountry_ItemSelected(object sender, EventArgs e)
        {
            //Txt_DeliverCountry.Text = ((AutoCompleteExtra.AutoCompleteExtraExtender)sender).SelectedValue;

        }
        /// <summary>
        /// 验证地区三字码是否正确
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtRegionCode_TextChanged(object sender, EventArgs e)
        {
            IList<RegionCode> region = _regionservice.FindAllRegionCodes();
            bool Ok = false;
            foreach (RegionCode regioncode in region)
            {
                if (regioncode.RegionName == txtRegionCode.Text.Trim().ToUpper())
                {
                    Ok = true;
                    break;
                }
            }
            if (!Ok)
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入正确的城市！')</script>");
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请输入正确的地区!')", true);
                txtRegionCode.Focus();
            }
            else
            {
                txtRegionCode.Text = RegionSwitch(txtRegionCode.Text.Trim().ToUpper(),0);
            }
        }
        /// <summary>
        /// 国家地区三字码转换
        /// </summary>
        /// <param name="countryname"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected string CountrySwitch(string countryname, int type)
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
        protected string RegionSwitch(string regionname, int type)
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
        #endregion

     

        
    }
}