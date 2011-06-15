using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataBusDomain.Service;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;
using GGGETSAdmin.Common;

namespace GGGETSAdmin.PackageManage
{
    public partial class packageManagement : System.Web.UI.Page
    {
        public int n = 1;
        private DateTime beginTime = DateTime.MinValue;
        private DateTime endTime = DateTime.MinValue;
        private string BarCode = string.Empty;
        private string regionCode = string.Empty;
        private IList<HAWB> listHawb;
        private readonly int PageCount = 35;//页面显示个数，固定不变。需要配置请修改此属性
        public int PageIndex //当期页码，会随着点击下一页，上一页进行动态变化
        {
            get { return (int)ViewState["pageIndex"]; }
            set { ViewState["pageIndex"] = value; }
        }
        private IPackageManagementService _packageservice;
        private static Regex RRegion = new Regex(@"^[A-Za-z]");
        private static string Rtime = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29))$";
        private IList<Package> listpackage;
        private static IRegionCodeManagementService _regionservice;
        private ISysUserManagementService _sysUserManagementService;
        private IDataBusService _dataBusService;
        private ILogisticsService _logisticsService;
        protected packageManagement()
        { }
        public packageManagement(IPackageManagementService packageservice, IRegionCodeManagementService regionservice, ISysUserManagementService sysUserManagementService, IDataBusService dataBusService, ILogisticsService logisticsService)
        {
            _packageservice = packageservice;
            _regionservice = regionservice;
            _sysUserManagementService = sysUserManagementService;
            _dataBusService = dataBusService;
            _logisticsService = logisticsService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] != null)
                {
                    Guid id = (Guid)Session["UserID"];
                    ModulePrivilege Mpriviege = _sysUserManagementService.GetPrivilegeByUserid(id);
                    if (!(bool)Mpriviege[Privilege.查询.ToString()])
                    {
                        btn_Demand.Enabled = false;
                    }
                    txt_UpCreateTime.Text = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
                    txt_ToCreateTime.Text = DateTime.Today.ToString("yyyy-MM-dd");
                }
            }
        }
        /// <summary>
        /// 数据源控件绑定
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        protected void Band(int pageIndex, int pageCount)
        {
            bool Ok = true;
            int totalCount = 0;//总页数
            if (Txt_BagBarCode.Text.Trim() != "")
            {
                BarCode = Txt_BagBarCode.Text.Trim().ToUpper();
            }
            if (txt_UpCreateTime.Text.Trim() != "")
            {
                if (!Regex.IsMatch(txt_UpCreateTime.Text, Rtime))
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请输入正确的时间格式！如2010-02-16！')", true);
                    txt_UpCreateTime.Focus();
                    Ok = false;
                }
                else
                {
                    beginTime = DateTime.Parse(txt_UpCreateTime.Text.Trim());
                    Ok = true;
                }
            }
            if (txt_ToCreateTime.Text.Trim() != "")
            {
                if (!Regex.IsMatch(txt_ToCreateTime.Text, Rtime))
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请输入正确的时间格式！如2010-02-16！')", true);
                    txt_ToCreateTime.Focus();
                    Ok = false;
                }
                else
                {
                    endTime = DateTime.Parse(txt_ToCreateTime.Text.Trim());
                    if (beginTime.CompareTo(endTime) == 1)
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('起始日期不能大于结束日期！')", true);
                        Ok = false;
                        txt_ToCreateTime.Focus();
                    }
                    else
                    {
                        Ok = true;
                    }
                }
            }
            if (Txt_OriginalRegionCode.Text.Trim() != "")
            {
                if (!RRegion.IsMatch(Txt_OriginalRegionCode.Text))
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('只能输入字母并为3位!')", true);
                    Ok = false;
                    Txt_OriginalRegionCode.Focus();
                }
                else
                {
                    regionCode = Txt_OriginalRegionCode.Text.Trim().ToUpper();
                    Ok = true;
                }
            }
            if (Txt_DestinationRegionCode.Text.Trim() != "")
            {
                if (!RRegion.IsMatch(Txt_DestinationRegionCode.Text))
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('只能输入字母并为3位!')", true);
                    Ok = false;
                    Txt_DestinationRegionCode.Focus();
                }
                else
                {
                    regionCode = Txt_DestinationRegionCode.Text.Trim().ToUpper();
                    Ok = true;
                }
            }
            if (Ok == true)
            {
                listpackage = _packageservice.FindPackageByCondition(BarCode, beginTime, endTime, regionCode, pageIndex, pageCount, ref totalCount);
                ViewState["totalCount"] = totalCount;//返回总条数
                if (listpackage.Count > 0)
                {

                    gv_HAWB.DataSource = listpackage;
                    gv_HAWB.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有相关记录!')", true);
                    this.InitialControl(this.Controls);
                    gv_HAWB.DataSource = null;
                    gv_HAWB.DataBind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请按提示操作!')", true);
            }
        }
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Demand_Click(object sender, EventArgs e)
        {
            PageIndex = 0;
            Band(PageIndex, PageCount);
            FenYe.Visible = true;
            lbl_nuber.Text = "1";
            DataBound();
            if (gv_HAWB.Rows.Count < PageCount && gv_HAWB.Rows.Count != 0)//数据源总数小于总条数的时候分页不可用
            {
                lbl_sumnuber.Text = "1";
                btn_Up.Enabled = false;
                btn_down.Enabled = false;
                btn_Jumpto.Enabled = false;
                btn_lastpage.Enabled = false;
                btn_homepage.Enabled = false;
            }
            else if (gv_HAWB.Rows.Count == 0)
            {
                lbl_nuber.Text = "0";
                lbl_sumnuber.Text = "0";
            }
            else
            {
                lbl_sumnuber.Text = (((int)ViewState["totalCount"] + PageCount - 1) / PageCount).ToString();//总页数
                btn_Up.Enabled = true;
                btn_down.Enabled = true;
                btn_Jumpto.Enabled = true;
                btn_lastpage.Enabled = true;
                btn_homepage.Enabled = true;

            }
        }

        protected void btn_Close_Click(object sender, EventArgs e)
        {
            //if (package == null)
            //{
            //    package = (Package)Session["package"];
            //}
            //bool Ok = false;
            //for (int i = gv_HAWB.Rows.Count - 1; i > -1; i--)
            //{
            //    string Bar = string.Empty;
            //    if (((CheckBox)gv_HAWB.Rows[i].FindControl("chkId")).Checked)
            //    {
            //        Ok = true;
            //        Bar = gv_HAWB.DataKeys[i].Value.ToString();
            //        foreach (HAWB ha in package.HAWBs)
            //        {
            //            if (ha.BarCode == Bar)
            //            {
            //                hawb = ha;
            //            }
            //        }
            //        package.HAWBs.Remove(hawb);
            //        txt_Pice.Text = package.Piece.ToString();
            //        Txt_TotalWeight.Text = package.TotalWeight.ToString();
            //    }
            //}
            //if (Ok == true)
            //{
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('删除成功！')</script>");
            //    if (package.HAWBs.Count == 0)
            //    {
            //        btn_Close.Visible = false;
            //    }
            //    gv_HAWB.DataSource = package.HAWBs;
            //    gv_HAWB.DataBind();
            //}
            //else
            //{
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('请选择要删除的记录！')</script>");
            //}
        }
        /// <summary>
        /// 前台行号显示
        /// </summary>
        /// <returns></returns>
        public int N()
        {
            return n++;
        }
        /// <summary>
        /// 
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

            //IList<RegionCode> regioncode = _regionservice
            //foreach (RegionCode region in regioncode)
            //{
            //    string[] ItemArry = new string[3];
            //    ItemArry[0] = region.RegionName;
            //    ItemArry[1] = region.RegionCode1;
            //    items.Add(ItemArry);
            //}
            return items.Take(count).ToArray();
        }

        protected void autocomplete_ItemSelected(object sender, EventArgs e)
        {
            //txt_Destination.Text = ((AutoCompleteExtra.AutoCompleteExtraExtender)sender).SelectedValue;
        }
        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Up_Click(object sender, EventArgs e)
        {
            btn_down.Enabled = true;
            PageIndex = PageIndex - 1;
            Band(PageIndex, PageCount);
            lbl_nuber.Text = (int.Parse(lbl_nuber.Text) - 1).ToString();
            DataBound();
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_down_Click(object sender, EventArgs e)
        {
            btn_Up.Enabled = true;
            PageIndex = PageIndex + 1;
            Band(PageIndex, PageCount);
            lbl_nuber.Text = (int.Parse(lbl_nuber.Text) + 1).ToString();
            DataBound();
        }
        /// <summary>
        /// 上下页按钮控制方法
        /// </summary>
        protected void DataBound()
        {
            if (PageIndex <= 0)
            {
                btn_Up.Enabled = false;
            }
            else
            {
                btn_Up.Enabled = true;
            }
            if (lbl_sumnuber.Text == lbl_nuber.Text)
            {

                btn_down.Enabled = false;
            }
            else
            {
                btn_down.Enabled = true;
            }
        }
        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_homepage_Click(object sender, EventArgs e)
        {
            PageIndex = 0;
            Band(PageIndex, PageCount);
            lbl_nuber.Text = "1";
            DataBound();
        }
        /// <summary>
        /// 跳转到几页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Jumpto_Click(object sender, EventArgs e)
        {
            if (int.Parse(Txt_Jumpto.Text.Trim()) <= 0)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('最小页数为1,请重新输入！')", true);
                Txt_Jumpto.Focus();
            }
            else if (int.Parse(Txt_Jumpto.Text.Trim()) > int.Parse(lbl_sumnuber.Text))
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('超过最大页数请重新输入！')", true);
                Txt_Jumpto.Focus();
            }
            else
            {
                PageIndex = int.Parse(Txt_Jumpto.Text.Trim()) - 1;
                Band(PageIndex, PageCount);
                lbl_nuber.Text = Txt_Jumpto.Text.Trim();
                DataBound();
            }
            Txt_Jumpto.Text = string.Empty;
        }
        /// <summary>
        /// 末页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_lastpage_Click(object sender, EventArgs e)
        {
            PageIndex = int.Parse(lbl_sumnuber.Text) - 1;
            Band(PageIndex, PageCount);
            lbl_nuber.Text = lbl_sumnuber.Text;
            DataBound();
        }

        /// <summary>
        /// 清空条件查询条件
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

        protected void gv_HAWB_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eidt")
            {
                Guid id = (Guid)Session["UserID"];
                ModulePrivilege Mpriviege = _sysUserManagementService.GetPrivilegeByUserid(id);
                bool privilege = (bool)Mpriviege[Privilege.修改.ToString()];
                bool aprivilege = (bool)Mpriviege[Privilege.解锁.ToString()];
                Response.Redirect("PackageDetails.aspx?BarCode=" + e.CommandArgument + "&Privilege=" + privilege + "&Privilege1=" + aprivilege + "");
            }
        }

        /// <summary>
        /// 绑定JS事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gv_HAWB_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //首先获取需要绑定JS的控件
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //((Button)e.Row.FindControl("btnSubmit")).Attributes.Add("onclick", "return confirm('是否要提交?注：提交后的包裹信息将传输到下一级站点,请慎重!');");

            }
        }

        /// <summary>
        /// 提交包裹信息
        /// 调用WS服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //首先获取选中的包裹对象
            string barcode = ((Button) sender).CommandArgument;
            if(string.IsNullOrEmpty(barcode))
            {
                throw new Exception("该运单没有运单编号!");
            }
            else
            {
                Package package = _packageservice.FindPackageByBarcode(barcode);
                if(package.IsSubmit.Equals("0"))
                {
                    string jsonStr = UtilityJson.ToJson(package);
                    //var appID = new Guid("48240b6b-1c67-4587-a091-e198b2e2449e");
                    var appID = Guid.Parse(ConfigurationManager.AppSettings["AppID"]);
                    var app = _dataBusService.GetNextDeliverApp(appID, package.DestinationRegionCode);
                    //app.Type.Name.Equals(DataBusDomain.Model.AppType.
                    var curApp = _dataBusService.GetApplicationByID(appID);
                    string url = app.URL + "WebService/GETSWebService.asmx";
                    DataBusDomain.DataObjects.TrackFromToDetial tft = new DataBusDomain.DataObjects.TrackFromToDetial
                    {
                        AppID = appID,
                        Director = DataBusDomain.Model.Director.Lunach,
                        FromID = appID,
                        ToID = app.ID,
                        Time = DateTime.Now,
                        PackID = package.PID,
                        TransferWay = string.Format("{0} 发往 {1}", curApp.Name, app.Name)
                    };
                    _logisticsService.SetTrackFromTo(tft);
                    ///string url = "http://localhost/GETSB/WebService/GETSWebService.asmx";
                    string[] args = new string[1];
                    args[0] = jsonStr;
                    try
                    {
                        object result = WebServiceHelperOperation.InvokeWebService(url, "AddPACKAGE", args);
                        if (result.Equals("SUCCESS:   操作已成功!"))
                        {
                            //改变包裹提交状态
                            package.IsSubmit = "1";
                            _packageservice.ModifyPackage(package);
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('" + result + "')", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('操作失败了!')", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请不要重复提交!')", true);
                }
            }
        }

        /// <summary>
        /// 实现批量提交和单个提交效果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click1(object sender, EventArgs e)
        {
            ArrayList barcodeList = new ArrayList();
            int rowCount = gv_HAWB.Rows.Count;
            if (rowCount == 0)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有包裹可以提交!')", true);
            }
            else
            {
                foreach (GridViewRow gvRow in gv_HAWB.Rows)
                {
                    CheckBox selectCheckBox = ((CheckBox)gvRow.FindControl("ckSelect"));
                    if (selectCheckBox.Checked)
                    {
                        barcodeList.Add(((LinkButton)gvRow.FindControl("lbtn_BagBarCoder")).CommandArgument);
                    }
                }

                if (barcodeList == null || barcodeList.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请选择要提交的包裹!')", true);
                }
                else
                {
                    //实现批量提交
                    foreach (string barcode in barcodeList)
                    {
                        Package package = _packageservice.FindPackageByBarcode(barcode);
                        if (package.IsSubmit.Equals("0"))
                        {
                            string jsonStr = UtilityJson.ToJson(package);
                            //var appID = new Guid("48240b6b-1c67-4587-a091-e198b2e2449e");
                            var appID = Guid.Parse(ConfigurationManager.AppSettings["AppID"]);
                            if (string.IsNullOrEmpty(appID.ToString()))
                            {
                                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('未配置实例AppID')", true);
                                return;
                            }
                            var app = _dataBusService.GetNextDeliverApp(appID, "TYO");
                            string url = app.URL + "WebService/GETSWebService.asmx";

                            //string url = "http://localhost/GETSB/WebService/GETSWebService.asmx";
                            string[] args = new string[1];
                            args[0] = jsonStr;
                            try
                            {
                                object result = WebServiceHelperOperation.InvokeWebService(url, "AddPACKAGE", args);
                                if (result.Equals("SUCCESS:   操作已成功!"))
                                {
                                    //改变包裹提交状态
                                    package.IsSubmit = "1";
                                    _packageservice.ModifyPackage(package);
                                    //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('" + result + "')", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('" + result + "')", true);
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请不要重复提交!')", true);
                        }
                    }
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('SUCCESS:   操作已成功!')", true);
                }
            }
        }

        /// <summary>
        /// 拆包操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSplitPackage_Click(object sender, EventArgs e)
        {
            string barcode = ((Button)sender).CommandArgument;
            if (_packageservice.FindPackageByBarcode(barcode).IsSubmit == "1")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('该包裹已经推送，已不能进行拆包操作，否则将会造成数据不同步!')", true);
            }
            else
            {
                _packageservice.RemovePackage(barcode);
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('拆包成功!')", true);
                Band(PageIndex, PageCount);
            }

        }

        /// <summary>
        /// 提交状态显示文字
        /// </summary>
        /// <returns></returns>
        public string IsSubmitStr(string isSubmit)
        {
            if (isSubmit.Equals("0"))
                return "未提交";
            return "已提交";
        }

        /// <summary>
        /// 如果未提交，则显示多选框；反之亦然
        /// </summary>
        /// <returns></returns>
        public bool IsSubmitDisplay(string isSubmit)
        {
            if (isSubmit.Equals("0"))
                return true;
            return false;
        }
    }
}