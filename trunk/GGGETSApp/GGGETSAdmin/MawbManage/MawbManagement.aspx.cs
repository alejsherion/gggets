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
using System.IO;

namespace GGGETSAdmin.MawbManage
{
    public partial class MawbManagement : System.Web.UI.Page
    {
        private IMAWBManagementService _mawbService;
        private IHAWBManagementService _hawbService;
        private ISysUserManagementService _sysUserManagementService;
        private IDataBusService _dataBusService;
        private IList<MAWB> listmawb;
        private readonly int PageCount = 35;//页面显示个数，固定不变。需要配置请修改此属性
        public int PageIndex //当期页码，会随着点击下一页，上一页进行动态变化
        {
            get { return (int)ViewState["pageIndex"]; }
            set { ViewState["pageIndex"] = value; }
        }
        private DateTime beginTime = new DateTime();
        private DateTime endTime = new DateTime();
        private string BarCode = string.Empty;
        public int n = 1;
        private static string Rtime = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29))$";
        protected MawbManagement()
        { }
        public MawbManagement(IMAWBManagementService mawbservice, IHAWBManagementService hawbservice, ISysUserManagementService sysUserManagementService, IDataBusService dataBusService)
        {
            _mawbService = mawbservice;
            _hawbService = hawbservice;
            _sysUserManagementService = sysUserManagementService;
            _dataBusService = dataBusService;
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
        /// 数据源绑定
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        protected void Band(int pageIndex, int pageCount)
        {
            bool Ok = true;
            int totalCount = 0;
            if (Txt_MAWBBarCode.Text != "")
            {
                BarCode = Txt_MAWBBarCode.Text.Trim().ToUpper();
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
            if (Ok == true)
            {
                listmawb = _mawbService.FindMAWBByCondition(BarCode, beginTime, endTime, pageIndex, pageCount, ref totalCount);
                ViewState["totalCount"] = totalCount;
                if (listmawb.Count > 0)
                {
                    gv_HAWB.DataSource = listmawb;
                    gv_HAWB.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有相关记录!')", true);
                    gv_HAWB.DataSource = null;
                    gv_HAWB.DataBind();
                    InitialControl(this.Controls);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请按提示操作!')", true);
            }
        }
        /// <summary>
        /// 查询按钮事件
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
        /// <summary>
        /// 前台行号显示方法
        /// </summary>
        /// <returns></returns>
        public int N()
        {
            return n++;
        }
        /// <summary>
        /// 数据源导出总运单和承运单清单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gv_HAWB_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eidt")
            {
                Guid id = (Guid)Session["UserID"];
                ModulePrivilege Authority = _sysUserManagementService.GetPrivilegeByUserid(id);
                bool aPrivilege = (bool)Authority[Privilege.修改.ToString()];
                bool aPrivilege2 = (bool)Authority[Privilege.导出.ToString()];
                bool aPrivilege1 = (bool)Authority[Privilege.解锁.ToString()];
                Response.Redirect("MawbDetails.aspx?Privilege=" + aPrivilege + "&BarCode=" + e.CommandArgument + "&aPrivilege1=" + aPrivilege1 + "&aPrivilege2=" + aPrivilege2 + "");
            }
            if (e.CommandName == "Derive")
            {
                string barcode = e.CommandArgument.ToString();
                MAWB mawb = _mawbService.FindMAWBByBarcode(barcode);
                IList<HAWB> hawbs = _hawbService.FindHAWBsByMID(mawb.MID.ToString());
                if (hawbs.Count != 0)
                {
                    var NpoiHelper = new NpoiHelper(mawb, hawbs, ExportType.运单号);
                    NpoiHelper.ExportMAWB();
                    var str = (MemoryStream)NpoiHelper.RenderToExcel();
                    if (str == null) return;
                    var data = str.ToArray();
                    var resp = Page.Response;
                    resp.Buffer = true;
                    resp.Clear();
                    resp.Charset = "utf-8";
                    resp.ContentEncoding = System.Text.Encoding.UTF8;
                    resp.ContentType = "application/ms-excel";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(String.Format("{0}.xls", barcode + "总运单清单"), System.Text.Encoding.UTF8));
                    HttpContext.Current.Response.BinaryWrite(data);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('该总运单下没有运单，不能导出！')", true);
                }
            }
            else if (e.CommandName == "DeriveAccept")
            {
                string barcode = e.CommandArgument.ToString();
                MAWB mawb = _mawbService.FindMAWBByBarcode(barcode);
                IList<HAWB> hawbs = _hawbService.FindHAWBsByMID(mawb.MID.ToString());
                if (hawbs.Count != 0)
                {
                    var NpoiHelper = new NpoiHelper(mawb, hawbs, ExportType.承运单号);
                    NpoiHelper.ExportMAWB();
                    var str = (MemoryStream)NpoiHelper.RenderToExcel();
                    if (str == null) return;
                    var data = str.ToArray();
                    var resp = Page.Response;
                    resp.Buffer = true;
                    resp.Clear();
                    resp.Charset = "utf-8";
                    resp.ContentEncoding = System.Text.Encoding.UTF8;
                    resp.ContentType = "application/ms-excel";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(String.Format("{0}.xls", barcode + "承运清单"), System.Text.Encoding.UTF8));
                    HttpContext.Current.Response.BinaryWrite(data);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('该总运单下没有运单，不能导出！')", true);
                }
            }
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
        /// 上下页按钮控制
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

        protected void gv_HAWB_DataBound(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                Guid id = (Guid)Session["UserID"];
                ModulePrivilege Authority = _sysUserManagementService.GetPrivilegeByUserid(id);
                foreach (GridViewRow row in gv_HAWB.Rows)
                {
                    if (!(bool)Authority[Privilege.导出.ToString()])
                    {
                        ((LinkButton)row.FindControl("lbtn_Derive") as LinkButton).Enabled = false;
                        ((LinkButton)row.FindControl("lbtn_DeriveAccept") as LinkButton).Enabled = false;
                    }

                }
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
            string barcode = ((Button)sender).CommandArgument;
            if (string.IsNullOrEmpty(barcode))
            {
                throw new Exception("该总运单没有总运单编号!");
            }
            else
            {
                MAWB mawb = _mawbService.FindMAWBByBarcode(barcode);
                if (mawb.IsSubmit.Equals("0"))
                {
                    string jsonStr = UtilityJson.ToJson(mawb);
                    //var appID = new Guid("48240b6b-1c67-4587-a091-e198b2e2449e");
                    var appID = Guid.Parse(ConfigurationManager.AppSettings["AppID"]);
                    var app = _dataBusService.GetNextDeliverApp(appID, "TYO");
                    string url = app.URL + "WebService/GETSWebService.asmx";

                    //string url = "http://localhost/GETSB/WebService/GETSWebService.asmx";
                    string[] args = new string[1];
                    args[0] = jsonStr;
                    try
                    {
                        object result = WebServiceHelperOperation.InvokeWebService(url, "AddMAWB", args);
                        if (result.Equals("SUCCESS:   操作已成功!"))
                        {
                            //改变包裹提交状态
                            mawb.IsSubmit = "1";
                            _mawbService.ModifyMAWB(mawb);
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

        protected void gv_HAWB_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //首先获取需要绑定JS的控件
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //((Button)e.Row.FindControl("btnSubmit")).Attributes.Add("onclick", "return confirm('是否要提交?注：提交后的包裹信息将传输到下一级站点,请慎重!');");
            }
        }

        /// <summary>
        /// 提 交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click1(object sender, EventArgs e)
        {
            ArrayList barcodeList = new ArrayList();
            int rowCount = gv_HAWB.Rows.Count;
            if (rowCount == 0)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有总运单可以提交!')", true);
            }
            else
            {
                foreach (GridViewRow gvRow in gv_HAWB.Rows)
                {
                    CheckBox selectCheckBox = ((CheckBox)gvRow.FindControl("ckSelect"));
                    if (selectCheckBox.Checked)
                    {
                        barcodeList.Add(((LinkButton)gvRow.FindControl("lbtn_FLTNo")).CommandArgument);
                    }
                }

                if (barcodeList == null || barcodeList.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请选择要提交的总运单!')", true);
                }
                else
                {
                    //实现批量提交
                    foreach (string barcode in barcodeList)
                    {
                        MAWB mawb = _mawbService.FindMAWBByBarcode(barcode);
                        if (mawb.IsSubmit.Equals("0"))
                        {
                            string jsonStr = UtilityJson.ToJson(mawb);
                            //var appID = new Guid("48240b6b-1c67-4587-a091-e198b2e2449e");
                            var appID = Guid.Parse(ConfigurationManager.AppSettings["AppID"]);
                            if (string.IsNullOrEmpty(appID.ToString()))
                            {
                                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('未配置实例AppID')", true);
                                return;
                            }
                            var app = _dataBusService.GetNextDeliverApp(appID, mawb.To);
                            string url = app.URL + "WebService/GETSWebService.asmx";

                            //string url = "http://localhost/GETSB/WebService/GETSWebService.asmx";
                            string[] args = new string[1];
                            args[0] = jsonStr;
                            try
                            {
                                object result = WebServiceHelperOperation.InvokeWebService(url, "AddMAWB", args);
                                if (result.Equals("0"))
                                {
                                    //改变包裹提交状态
                                    mawb.IsSubmit = "1";
                                    _mawbService.ModifyMAWB(mawb);
                                    //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('" + result + "')", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('" + ReturnError(result.ToString()) + "')", true);
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

        private string ReturnError(string error)
        {
            string strResult = "";
            switch (error.ToString())
            {
                case "1":
                    strResult = Resources.ErrorResult._1;
                    break;
                case "2":
                    strResult = Resources.ErrorResult._2;
                    break;
                case "3":
                    strResult = Resources.ErrorResult._3;
                    break;
                case "4":
                    strResult = Resources.ErrorResult._4;
                    break;
                default:
                    strResult = Resources.ErrorResult._0;
                    break;
            }
            return strResult;
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