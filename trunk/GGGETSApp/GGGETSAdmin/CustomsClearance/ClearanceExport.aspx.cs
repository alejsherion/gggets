//************************************************************************
// 用户名				GETS国际综合快递
// 系统名				管理后台
// 子系统名		        导出报关文件，有固定格式
// 作成者				ZhiWei.Shen
// 改版日				2011.04.19
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using GGGETSAdmin.Common;
using Telerik.Web.UI;

namespace GGGETSAdmin.CustomsClearance
{
    public partial class ClearanceExport : System.Web.UI.Page
    {
        //ioc register
        private IMAWBManagementService _mawbService;
        private IHAWBManagementService _hawbService;
        protected ClearanceExport()
        {
        }
        public ClearanceExport(IMAWBManagementService mawbService, IHAWBManagementService hawbService)
        {
            _mawbService = mawbService;
            _hawbService = hawbService;
        }

        #region property
        private string FlightNo//航班号
        {
            get { return (string) ViewState["flightNo"]; }
            set { ViewState["flightNo"] = value; }
        }
        private string MAWBNo//总运单号
        {
            get { return (string)ViewState["MAWBNo"]; }
            set { ViewState["MAWBNo"] = value; }
        }
        private string MID//总运单序号
        {
            get { return (string)ViewState["MID"]; }
            set { ViewState["MID"] = value; }
        }
        //记录多选框选中状态
        private IList WriteIndex
        {
            get { return (ArrayList) ViewState["index"]; }
            set { ViewState["index"] = value; }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                FlightNo = Request["FlightNo"];
                MAWBNo = Request["MAWBCode"];

                BindController();
            }
        }

        #region Private Block
        private void BindController()
        {
            lblFlightNoData.Text = FlightNo;
            //通过总运单编号获取总运单
            MAWB mawb = _mawbService.FindMAWBByBarcode(MAWBNo);
            MID = Convert.ToString(mawb.MID);
            lblPackageNumData.Text = mawb.Packages.Count.ToString();//绑定包总数

            ReBind();
        }

        /// <summary>
        /// 绑定GRID
        /// </summary>
        private void ReBind()
        {
            RGMAWB.DataSource = _hawbService.FindHAWBsByMID(MID);
            RGMAWB.DataBind();
        }
        #endregion

        #region ServiceEvent Block
        protected void RGMAWB_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            ReBind();
        }

        /// <summary>
        /// 导出报关文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            IList<HAWB> hawbs = new List<HAWB>();//实例化
            for (int i = 0; i < RGMAWB.MasterTableView.Items.Count; i++)
            {
                GridDataItem row = RGMAWB.MasterTableView.Items[i];
                if (((CheckBox)row.FindControl("CBSelect")).Checked)
                {
                    //获取选中的运单编号
                    string hawbBarcode =
                        RGMAWB.MasterTableView.DataKeyValues[row.ItemIndex]["BarCode"].ToString();
                    //获取运单信息
                    HAWB hawb = _hawbService.FindHAWBByBarCode(hawbBarcode);
                    hawbs.Add(hawb);
                }

            }

            //todo 进行EXCEL导出
            MAWB mawb = _mawbService.FindMAWBByBarcode(MAWBNo);
            var NpoiHelper = new NpoiHelper(mawb, hawbs);
            NpoiHelper.ExportClearance();
            var str = (MemoryStream)NpoiHelper.RenderToExcel();
            if (str == null) return;
            var data = str.ToArray();
            var resp = Page.Response;
            resp.Buffer = true;
            resp.Clear();
            resp.Charset = "utf-8";
            resp.ContentEncoding = System.Text.Encoding.UTF8;
            resp.ContentType = "application/ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(String.Format("{0}.xls", "电子出口清单"), System.Text.Encoding.UTF8));
            HttpContext.Current.Response.BinaryWrite(data);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 返回上一目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CustomsClearance/ClearanceManage.aspx");
        }
        #endregion
    }
}