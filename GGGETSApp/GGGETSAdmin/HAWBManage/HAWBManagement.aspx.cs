using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;
using System.IO;
using GGGETSAdmin.Common;
namespace GGGETSAdmin.HAWBManage
{
    public partial class HAWBManagement : System.Web.UI.Page
    {
        public int n = 1;
        private IHAWBManagementService _hawbService;
        private ISysUserManagementService _SysUserManagementService;
        protected static Regex RCountry = new Regex(@"^[A-Za-z]{2}");
        private static Regex RRegion = new Regex(@"^[A-Za-z]{3}");
        private static string Rtime = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29))$";
        private IList<HAWB> listHawb;
        private readonly int PageCount = 35;//页面显示个数，固定不变。需要配置请修改此属性
        public int PageIndex //当期页码，会随着点击下一页，上一页进行动态变化
        {
            get { return (int)ViewState["pageIndex"]; }
            set { ViewState["pageIndex"] = value; }
        }
        private string BarCode = string.Empty;//运单号
        private string countryCode = string.Empty;//国家二字码
        private string regionCode = string.Empty;//地区三字码
        private string CompanyName = string.Empty;//公司名称
        private string departmentCode = string.Empty;//部门编号
        private string carrier = string.Empty;//承运公司
        private string contactor = string.Empty;//联系人
        private string HAWBOperator = string.Empty;//操作人
        private DateTime beginTime = new DateTime();
        private DateTime endTime = new DateTime();
        private int settleType = -1;
        private int serviceType = -1;
        private bool isInternational;
        protected HAWBManagement()
        { }
        public HAWBManagement(IHAWBManagementService hawbservice, ISysUserManagementService SysUserManagementService)
        {
            _hawbService = hawbservice;
            _SysUserManagementService = SysUserManagementService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                Txt_GetUpTime.Text = DateTime.Today.Date.AddDays(-1).ToString("yyyy-MM-dd");
                Txt_StopTime.Text = DateTime.Today.ToString("yyyy-MM-dd");
                DropDownList();
                if (Session["UserID"] != null)
                {
                    Guid id = (Guid)Session["UserID"];
                    ModulePrivilege Authority = _SysUserManagementService.GetPrivilegeByUserid(id);
                    if ((bool)Authority[Privilege.查询.ToString()])
                    {
                        btn_Demand.Enabled = true;
                    }
                    else
                    {
                        btn_Demand.Enabled = false;
                    }
                }
                else
                {
                    Response.Write("<script>alert('没有访问权限!');location='../HOME.aspx'</script>");
                }
            }
            
        }
        protected void DropDownList()
        {
            EnumType type = new EnumType();//枚举类
            List<EnumType> SettleTypeItem = type.GetName("SettleType");
            List<EnumType> BoxTypeItem = type.GetName("BoxType");
            EnumType item = new EnumType();
            item.Text = "所有";
            item.Value = -1;
            SettleTypeItem.Insert(0, item);
            BoxTypeItem.Insert(0, item);


            DDl_SettleType.DataSource = SettleTypeItem;
            DDl_SettleType.DataTextField = "Text";
            DDl_SettleType.DataValueField = "Value";
            DDl_SettleType.DataBind();

            ddl_BoxType.DataSource = BoxTypeItem;
            ddl_BoxType.DataTextField = "Text";
            ddl_BoxType.DataValueField = "Value";
            ddl_BoxType.DataBind();
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
            if (Txt_Account2.Text.Trim() != "")
            {
                if (Txt_Account1.Text.Trim() == "")
                {
                    
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(),"", "alert('请输入客户账号!')", true);
                    Txt_Account1.Focus();
                }
                else
                {
                    departmentCode = Txt_Account2.Text.ToUpper();
                    Ok = OK(Ok);
                }
            }
            else
            {
                Ok = OK(Ok);
            }
            
            if (Ok == true)
            {
                listHawb = _hawbService.FindHAWBsByCondition(BarCode, countryCode, regionCode,departmentCode,CompanyName,carrier,HAWBOperator,contactor,beginTime,endTime,settleType,serviceType,isInternational,PageIndex,PageCount,ref totalCount);
                ViewState["totalCount"] = totalCount;
                if (listHawb.Count > 0)
                {

                    Gv_HAWB.DataSource = listHawb;
                    Gv_HAWB.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有相关记录!')", true);                    
                    Gv_HAWB.DataSource = null;
                    Gv_HAWB.DataBind();
                    InitialControl(this.Controls);
                }
            }
            else
            {
               
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请按提示操作!')", true);
            }
        }
        #region
        /// <summary>
        /// 判断条件是否正确
        /// </summary>
        /// <param name="ok"></param>
        /// <returns></returns>
        protected bool OK(bool ok)
        {
            if (Txt_BarCode.Text.Trim() != "")
            {
                BarCode = Txt_BarCode.Text.Trim().ToUpper();
            }
            if (Txt_Country.Text.Trim() != "")
            {
                if (!RCountry.IsMatch(Txt_Country.Text))
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('只能输入字母并为2位!')", true);
                    ok = false;
                    Txt_Country.Focus();
                }
                else
                {
                    countryCode = Txt_Country.Text.Trim().ToUpper();
                    ok = true;
                }
            }
            if (Txt_Region.Text.Trim() != "")
            {
                if (!RRegion.IsMatch(Txt_Region.Text))
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('只能输入字母并为3位!')", true);
                    ok = false;
                    Txt_Region.Focus();
                }
                else
                {
                    regionCode = Txt_Region.Text.Trim().ToUpper();
                    ok = true;
                }
            }
            if (Txt_Account1.Text.Trim() != "")
            {
                CompanyName = Txt_Account1.Text.Trim().ToUpper();
            }
            if (Txt_corporationName.Text.Trim() != "")
            {
                carrier = Txt_corporationName.Text.Trim().ToUpper();
            }
            if (Txt_LoginName.Text.Trim() != "")
            {
                HAWBOperator = Txt_LoginName.Text.ToUpper();
            }
            if (Txt_Contactor.Text.Trim() != "")
            {
                contactor = Txt_Contactor.Text.Trim().ToUpper();
            }
            if (DDl_SettleType.SelectedValue != "-1")
            {
                settleType = int.Parse(DDl_SettleType.SelectedValue);
            }
            if (ddl_BoxType.SelectedValue != "-1")
            {
                serviceType = int.Parse(ddl_BoxType.SelectedValue.ToString());
            }
           
            if (ddl_HAWBType.SelectedValue != "1")
            {
                isInternational = true;
            }
            else
            {
                isInternational = false;
            }
            if (Txt_GetUpTime.Text.Trim() != "")
            {
                if (!Regex.IsMatch(Txt_GetUpTime.Text.Trim(), Rtime))
                {

                    Txt_GetUpTime.Focus();
                    ok = false;
                }
                else
                {
                    beginTime = DateTime.Parse(Txt_GetUpTime.Text.Trim().Trim());
                    ok = true;
                }
            }
            if (Txt_StopTime.Text.Trim() != "")
            {
                if (!Regex.IsMatch(Txt_StopTime.Text.Trim(), Rtime))
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请输入正确的时间格式！如2010-02-16！')", true);
                    Txt_StopTime.Focus();
                    ok = false;
                }
                else
                {
                    endTime = DateTime.Parse(Txt_StopTime.Text.Trim().Trim());
                    if (beginTime.CompareTo(endTime) == 1)
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('起始日期不能大于结束日期！')", true);
                        ok = false;
                        Txt_StopTime.Focus();
                    }
                    else
                    {
                        ok = true;
                    }
                }
            }
            return ok;
        }
        #endregion

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

            if (Gv_HAWB.Rows.Count < PageCount && Gv_HAWB.Rows.Count != 0)//数据源总数小于总条数的时候分页不可用
            {
                lbl_nuber.Text = "1";
                lbl_sumnuber.Text = "1";
                btn_Up.Enabled = false;
                btn_down.Enabled = false;
                btn_Jumpto.Enabled = false;
                btn_lastpage.Enabled = false;
                btn_homepage.Enabled = false;
            }
            else if (Gv_HAWB.Rows.Count == 0)
            {
                lbl_nuber.Text = "0";
                lbl_sumnuber.Text = "0";
            }
            else
            {
                lbl_nuber.Text = "1";
                lbl_sumnuber.Text = (((int)ViewState["totalCount"] + PageCount - 1) / PageCount).ToString();//总页数
                btn_Up.Enabled = true;
                btn_down.Enabled = true;
                btn_Jumpto.Enabled = true;
                btn_lastpage.Enabled = true;
                btn_homepage.Enabled = true;

            }
            
            DataBound();
        }
        /// <summary>
        /// 数据源操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Gv_HAWB_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid id = (Guid)Session["UserID"];
            ModulePrivilege Authority = _SysUserManagementService.GetPrivilegeByUserid(id);           
            if (e.CommandName == "Eidt")
            {
                bool aPrivilege = (bool)Authority[Privilege.修改.ToString()];
                bool aPrivilege1 = (bool)Authority[Privilege.导出.ToString()];
                int index = Convert.ToInt32(e.CommandArgument);
                string barCode = Gv_HAWB.DataKeys[index].Value.ToString();
                Response.Redirect("HAWBDetails.aspx?BarCode=" + barCode + "&Privilege=" + aPrivilege + "&Privilege1=" + aPrivilege1 + "");
            }
            else if (e.CommandName == "Updata")
            {
                string barCode = e.CommandArgument.ToString();
                int Update = 1;
                Response.Redirect("HAWBAdd.aspx?BarCode=" + barCode + "&update=" + Update + "");
            }
            else if (e.CommandName == "Del")
            {
                string barCode = e.CommandArgument.ToString();
                //_hawbService.RemoveHAWB(barCode);
            }
            else if (e.CommandName == "Derive")
            {
                string barCode = e.CommandArgument.ToString();
                HAWB hawb = _hawbService.FindHAWBByBarCode(barCode);
                var NpoiHelper = new NpoiHelper(hawb, barCode);
                NpoiHelper.ExportInvoice();
                var str = (MemoryStream)NpoiHelper.RenderToExcel();
                if (str == null) return;
                var data = str.ToArray();
                var resp = Page.Response;
                resp.Buffer = true;
                resp.Clear();
                resp.Charset = "utf-8";
                resp.ContentEncoding = System.Text.Encoding.UTF8;
                resp.ContentType = "application/ms-excel";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(String.Format("{0}.xls", barCode+"运单发票"), System.Text.Encoding.UTF8));
                HttpContext.Current.Response.BinaryWrite(data);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            else if (e.CommandName == "DeriveAccept")
            {
                int index = ((GridViewRow)((LinkButton)(e.CommandSource)).Parent.Parent).RowIndex;
                string CarrierHAWBBarCode = e.CommandArgument.ToString();
                string barCode = Gv_HAWB.DataKeys[index].Value.ToString();
                HAWB hawb = _hawbService.FindHAWBByBarCode(barCode);
                if (!string.IsNullOrEmpty(CarrierHAWBBarCode))
                {
                    var NpoiHelper = new NpoiHelper(hawb, CarrierHAWBBarCode);
                    NpoiHelper.ExportInvoice();
                    var str = (MemoryStream)NpoiHelper.RenderToExcel();
                    if (str == null) return;
                    var data = str.ToArray();
                    var resp = Page.Response;
                    resp.Buffer = true;
                    resp.Clear();
                    resp.Charset = "utf-8";
                    resp.ContentEncoding = System.Text.Encoding.UTF8;
                    resp.ContentType = "application/ms-excel";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(String.Format("{0}.xls", barCode+"承运发票"), System.Text.Encoding.UTF8));
                    HttpContext.Current.Response.BinaryWrite(data);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有承运公司编号，不能导出！')", true);
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
        /// 上下页控件控制
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
            ddl_BoxType.SelectedValue = "-1";
            ddl_HAWBType.SelectedValue = "0";
            DDl_SettleType.SelectedValue = "-1";
        }
        /// <summary>
        /// 行号方法
        /// </summary>
        /// <returns></returns>
        public int N()
        {
            return n++;
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

        protected void Gv_HAWB_DataBound(object sender, EventArgs e)
        {
             if (Session["UserID"] != null)
             {
                 Guid id = (Guid)Session["UserID"];
                 ModulePrivilege Authority = _SysUserManagementService.GetPrivilegeByUserid(id);
                 foreach (GridViewRow row in Gv_HAWB.Rows)
                 {
                     if (!(bool)Authority[Privilege.修改.ToString()])
                     {
                          ((LinkButton)row.FindControl("lbtn_Update") as LinkButton).Enabled = false;
                     }
                     if(!(bool)Authority[Privilege.删除.ToString()])
                     {
                         ((LinkButton)row.FindControl("lbtn_Delete") as LinkButton).Enabled = false;
                     }
                     if (!(bool)Authority[Privilege.导出.ToString()])
                     {
                         ((LinkButton)row.FindControl("lbtn_Derive") as LinkButton).Enabled = false;
                         ((LinkButton)row.FindControl("lbtn_DeriveAccept") as LinkButton).Enabled = false;
                     }

                 }
             }
        }

        //protected void Gv_HAWB_DataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (Session["UserID"] != null)
        //    {
        //        Guid id = (Guid)Session["UserID"];
        //        ModulePrivilege Authority = _SysUserManagementService.GetPrivilegeByUserid(id);
        //        if ((bool)Authority.DeletePrivilege)
        //        {
        //            Label lbtn = (Label)e.Row.FindControl("lbl_BarCode");
        //            lbtn.Enabled = true;
        //        }
        //        else
        //        {
        //            string lbtn = ((Label)e.Row.FindControl("lbl_BarCode") as Label).Text;
        //            //lbtn.Enabled = false;
        //        }

        //    }
        //}
    }
}