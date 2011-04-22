//************************************************************************
// 用户名				GETS国际综合快递
// 系统名				管理后台
// 子系统名		        重新打包
// 作成者				ZhiWei.Shen
// 改版日				2011.04.19
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;

namespace GGGETSAdmin.PackageGetOffManage
{
    public partial class PackageAttribute : System.Web.UI.Page
    {
        private static string RRegion = @"^[A-Za-z]";//判断区字码的规范
        private Package package;
        private HAWB hawb;
        private IPackageManagementService _packageservice;
        private IHAWBManagementService _hawbservice;
        private static IRegionCodeManagementService _regionservice;
        private ISysUserManagementService _SysUserManagementService;
        public string userid
        {
            get { return (string) ViewState["user"]; }
            set { ViewState["user"] = value; }
        }
        public int status//全局状态，控制是新增还是修改;0-新增 1-修改
        {
            get { return (int)ViewState["status"]; }
            set { ViewState["status"] = value; }
        }
        protected PackageAttribute()
        { }
        public PackageAttribute(IPackageManagementService packageservice, IHAWBManagementService hawbservice, IRegionCodeManagementService regionservice, ISysUserManagementService SysUserManagementService)
        {
            _packageservice=packageservice;
            _hawbservice = hawbservice;
            _regionservice = regionservice;
            _SysUserManagementService = SysUserManagementService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                status = 0;
                Txt_BagBarCode.Focus();//初始化聚焦
                if (Session["UserID"] != null)
                {
                    Guid id = (Guid)Session["UserID"];
                    userid = _SysUserManagementService.GetUserById(id).LoginName;
                    ModulePrivilege Mprivilege = _SysUserManagementService.GetPrivilegeByUserid(id);
                    if (!(bool)Mprivilege[Privilege.添加.ToString()])
                    {
                        btn_Add.Enabled = false;
                        btn_Save.Enabled = false;
                    }
                }
                package = new Package();
                Session["package"] = package;
                if (gv_HAWB.Rows.Count == 0)
                {
                    btn_Close.Visible = false;
                }
                
            }
        }
        /// <summary>
        /// 包添加运单操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_BarCode.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请输入运单号!')", true);
                txt_BarCode.Focus();
            }
            else
            {
                if (!string.IsNullOrEmpty(Txt_BagBarCode.Text.Trim()) && !string.IsNullOrEmpty(Txt_DestinationRegionCode.Text.Trim()) && !string.IsNullOrEmpty(Txt_OriginalRegionCode.Text.Trim()))
                {
                    hawb = _hawbservice.FindHAWBByBarCode(txt_BarCode.Text.Trim());
                    if (hawb != null)
                    {
                        //再分配运单前首先确保包存在
                        if (string.IsNullOrEmpty(Txt_BagBarCode.Text.Trim()))
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请先输入包号!')", true);
                            txt_BarCode.Focus();
                            return;
                        }
                        else
                        {
                            package = (Package)Session["package"];
                            if (package == null)
                                package = new Package();
                        }
                        if (package.JudgeHAWB(hawb))//判断运单是否在该包里面
                        {
                            bool isMix;
                            if (rbtn_PackageType.SelectedValue == "0")//判断是否混包
                            {
                                isMix = false;
                            }
                            else
                            {
                                isMix = true;
                            }
                            if (_packageservice.JudgePIDIsNull(hawb.BarCode))//判断该运单是否已经打包
                            {
                                hawb.Status = 1;//已打包
                                package.HAWBs.Add(hawb);
                                txt_Pice.Text = package.Piece.ToString();
                                Txt_TotalWeight.Text = package.TotalWeight.ToString();
                                gv_HAWB.DataSource = package.HAWBs;
                                gv_HAWB.DataBind();
                                Session["package"] = package;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('该运单已经添加，不能再次进行添加!')", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('该运单已经添加，不能再次进行添加!')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有该运单记录!')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请先输入包的信息!')", true);
                }
            }
            if (gv_HAWB.Rows.Count != 0)
            {
                btn_Close.Visible = true;
            }
            txt_BarCode.Text = string.Empty;
            txt_BarCode.Focus();
            
        }
        
        /// <summary>
        /// 删除包里面的运单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Close_Click(object sender, EventArgs e)
        {
            if (package == null)
            {
                package = (Package)Session["package"];
            }
            bool Ok = false;
            for (int i = gv_HAWB.Rows.Count - 1; i > -1; i--)
            {
                string Bar = string.Empty;
                if (((CheckBox)gv_HAWB.Rows[i].FindControl("chkId")).Checked)
                {
                    Ok = true;
                    Bar = gv_HAWB.DataKeys[i].Value.ToString();
                    foreach (HAWB ha in package.HAWBs)
                    {
                        if (ha.BarCode == Bar)
                        {
                            hawb = ha;
                        }
                    }
                    package.HAWBs.Remove(hawb);

                    //特殊操作，SESSION里面加成员保存没有问题，但是如果是减成员就会没有任何效果
                    hawb.PID = null;
                    hawb.Status = 0;
                    _hawbservice.ChangeHAWB(hawb);

                    txt_Pice.Text = package.Piece.ToString();
                    Txt_TotalWeight.Text = package.TotalWeight.ToString();
                }
            }
            if (Ok == true)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('删除成功!')", true);
                if (package.HAWBs.Count == 0)
                {
                    btn_Close.Visible = false;
                }
                gv_HAWB.DataSource = package.HAWBs;
                gv_HAWB.DataBind();
                txt_BarCode.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请选择要移除的记录!')", true);
            }
            
        }

        #region ServiceEvent Block
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            AddPackage();
        }

        /// <summary>
        /// 判断是否正确的起始地三字码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Txt_Region_TextChanged(object sender, EventArgs e)
        {
            Judge(Txt_OriginalRegionCode.Text.Trim().ToUpper());
        }

        /// <summary>
        /// 判断是否正确的目的地三字码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Txt_DestinationRegionCode_TextChanged(object sender, EventArgs e)
        {
            Judge2(Txt_DestinationRegionCode.Text.Trim().ToUpper());
        }

        /// <summary>
        /// 判断包是否已存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Txt_BagBarCode_TextChanged(object sender, EventArgs e)
        {
            Package pack = _packageservice.FindPackageByBarcode(Txt_BagBarCode.Text.ToUpper().Trim());
            if (pack != null)
            {
                int isMixValue = pack.IsMixed == true ? 1 : 0;
                rbtn_PackageType.SelectedIndex = isMixValue;//是否是混包
                Txt_OriginalRegionCode.Text = pack.OriginalRegionCode;//起始地
                Txt_DestinationRegionCode.Text = pack.DestinationRegionCode;//目的地
                txt_Pice.Text = pack.Piece.ToString();//件数
                Txt_TotalWeight.Text = pack.TotalWeight.ToString();//总重量
                gv_HAWB.DataSource = pack.HAWBs.ToList();//包下运单数据源绑定
                gv_HAWB.DataBind();
                if (pack.HAWBs.ToList().Count>0)
                {
                    btn_Close.Visible = true;
                }
                status = 1;

                Session["package"] = pack;
            }
            else
            {
                Txt_OriginalRegionCode.Focus();
                status = 0;

                Session["package"] = null;
                Txt_OriginalRegionCode.Text = string.Empty;
                Txt_DestinationRegionCode.Text = string.Empty;
                txt_Pice.Text = string.Empty;
                Txt_TotalWeight.Text = string.Empty;
                rbtn_PackageType.SelectedValue = "0";
                gv_HAWB.DataSource = null;
                gv_HAWB.DataBind();
            }
        }
        #endregion

        #region Private Block
        /// <summary>
        /// 判断区码重复问题
        /// </summary>
        private void Judge(string regionStr)
        {
            bool ok = false;
            if (!string.IsNullOrEmpty(regionStr))
            {
                if (Regex.IsMatch(Txt_OriginalRegionCode.Text.Trim(), RRegion))
                {
                    IList<RegionCode> Regioncode = _regionservice.FindAllRegionCodes();
                    foreach (RegionCode regioncode in Regioncode)
                    {
                        if (regioncode.RegionCode1 == regionStr)
                        {
                            ok = true;
                            break;
                        }
                    }
                    if (!ok)
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有该三字码!')",
                                                            true);
                        Txt_OriginalRegionCode.Text = "";
                        Txt_OriginalRegionCode.Focus();
                    }
                    else
                    {
                        Txt_DestinationRegionCode.Focus();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('起始地三字码只能为字母!')", true);
                    Txt_OriginalRegionCode.Focus();
                }
            }
        }

        /// <summary>
        /// 判断区码重复问题
        /// </summary>
        private void Judge2(string regionStr)
        {
            bool ok = false;
            if (!string.IsNullOrEmpty(regionStr))
            {
                if (Regex.IsMatch(Txt_DestinationRegionCode.Text.Trim(), RRegion))
                {
                    IList<RegionCode> Regioncode = _regionservice.FindAllRegionCodes();
                    foreach (RegionCode regioncode in Regioncode)
                    {
                        if (regioncode.RegionCode1 == regionStr)
                        {
                            ok = true;
                            break;
                        }
                    }
                    if (!ok)
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有该三字码!')",
                                                            true);
                        Txt_DestinationRegionCode.Text = "";
                        Txt_DestinationRegionCode.Focus();
                    }
                    else
                    {
                        txt_BarCode.Focus();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('目的地三字码只能为字母!')", true);
                    Txt_OriginalRegionCode.Focus();
                }
            }
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="type">保存类型0:保存,1：保存并关闭</param>
        private void AddPackage()
        {
            package = (Package)Session["package"];
            if(package==null)
            {
                package = new Package();
            }
            if(!string.IsNullOrEmpty(package.BarCode))
            {
                if(status==1)
                {
                    package.IsMixed = rbtn_PackageType.SelectedValue == "0" ? false : true;
                    package.OriginalRegionCode = Txt_OriginalRegionCode.Text.Trim().ToUpper();
                    package.CreateTime = DateTime.Now;
                    package.UpdateTime = DateTime.Now;
                    package.DestinationRegionCode = Txt_DestinationRegionCode.Text.Trim().ToUpper();

                    _packageservice.ModifyPackage(package);
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('修改成功!')", true);

                    Session["package"] = null;
                    Txt_BagBarCode.Text = string.Empty;
                    Txt_OriginalRegionCode.Text = string.Empty;
                    Txt_DestinationRegionCode.Text = string.Empty;
                    txt_Pice.Text = string.Empty;
                    Txt_TotalWeight.Text = string.Empty;
                    gv_HAWB.DataSource = null;
                    gv_HAWB.DataBind();
                    return;
                }
                
            }
            bool ok = false;
            if (!string.IsNullOrEmpty(Txt_BagBarCode.Text.Trim()))
            {
                if (!string.IsNullOrEmpty(Txt_OriginalRegionCode.Text.Trim()) && !string.IsNullOrEmpty(Txt_DestinationRegionCode.Text.Trim()))
                {
                    package.OriginalRegionCode = Txt_OriginalRegionCode.Text.Trim().ToUpper();
                    if (!string.IsNullOrEmpty(txt_Pice.Text.Trim()))
                    {
                        package.Piece = int.Parse(txt_Pice.Text.Trim());//件数
                    }
                    if (!string.IsNullOrEmpty(Txt_TotalWeight.Text.Trim()))
                    {
                        package.TotalWeight = decimal.Parse(Txt_TotalWeight.Text.Trim());//总重量
                    }
                    package.Status = 1;//由于是下飞机流程，状态为1,0-代表是上飞机
                    package.IsMixed = rbtn_PackageType.SelectedValue == "0" ? false : true;//是否是混包

                    #region Add Block
                    IList<RegionCode> Regioncode = _regionservice.FindAllRegionCodes();//获取所有的地区信息
                    foreach (RegionCode regioncode in Regioncode)
                    {
                        if (regioncode.RegionCode1 == Txt_OriginalRegionCode.Text.Trim().ToUpper())
                        {
                            ok = true;
                            break;
                        }
                    }

                    if (!ok)
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有该三字码!')", true);
                        Txt_OriginalRegionCode.Text = "";
                        Txt_OriginalRegionCode.Focus();
                    }
                    else
                    {
                        package.PID = Guid.NewGuid();
                        package.BarCode = Txt_BagBarCode.Text.Trim().ToUpper();
                        package.CreateTime = DateTime.Now;
                        package.UpdateTime = DateTime.Now;
                        package.DestinationRegionCode = Txt_DestinationRegionCode.Text.Trim().ToUpper();
                        package.OriginalRegionCode = Txt_OriginalRegionCode.Text.Trim().ToUpper();
                        package.Operator = userid;
                        package.IsMixed = this.rbtn_PackageType.SelectedValue == "0" ? false : true;//这个小王遗漏了，混包问题
                        _packageservice.AddPackage(package);
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('添加成功!')", true);

                        Session["package"] = null;
                        Txt_BagBarCode.Text = string.Empty;
                        Txt_OriginalRegionCode.Text = string.Empty;
                        Txt_DestinationRegionCode.Text = string.Empty;
                        txt_Pice.Text = string.Empty;
                        Txt_TotalWeight.Text = string.Empty;
                        btn_Close.Visible = false;
                        gv_HAWB.DataSource = null;
                        gv_HAWB.DataBind();
                    }
                    #endregion
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('起/终地三字码不能为空!')", true);
                    Txt_OriginalRegionCode.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('包号不能为空!')", true);
                Txt_BagBarCode.Focus();
            }
        }
        #endregion
    }
}