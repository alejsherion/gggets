using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using GGGETSAdmin.Common;
using System.IO;

namespace GGGETSAdmin.MawbManage
{
    public partial class MawbDetails : System.Web.UI.Page
    {
        private IMAWBManagementService _mawbService;
        private IHAWBManagementService _hawbService;
        private ISysUserManagementService _SysUserManagementService;
        private MAWB mawb;
        public int n = 1;
        protected MawbDetails()
        { }
        public MawbDetails(IMAWBManagementService mawbservice, IHAWBManagementService hawbservice, ISysUserManagementService SysUserManagementService)
        {
            _mawbService = mawbservice;
            _hawbService = hawbservice;
            _SysUserManagementService = SysUserManagementService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["BarCode"] != ""&&Request.QueryString["BarCode"] !=null)
                {
                    
                    Uri url = Request.UrlReferrer;
                    ViewState["UrlReferrer"] = Request.UrlReferrer;
                    mawb = _mawbService.FindMAWBByBarcode(Request.QueryString["BarCode"]);
                    if (mawb != null)
                    {
                        Evaluate(mawb);
                        
                        if (!bool.Parse(Request.QueryString["Privilege"]))
                        {
                            But_Update.Enabled = false;
                        }
                        else
                        {
                            if (txt_Status.Text != "打开")
                            {
                                if (!bool.Parse(Request.QueryString["aPrivilege1"]))
                                {
                                    But_Update.Enabled = false;
                                }
                            }
                                
                        }
                        if (!bool.Parse(Request.QueryString["aPrivilege2"]))
                        {
                            btn_DeriveAccept.Enabled = false;
                            btn_DeriveSince.Enabled = false;
                        }
                    }
                    else
                    {
                        //Response.Redirect("<script>alert('！');</script>");
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('没有相关记录！');location="+url+"</script>"); 
                        
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('没有相关记录！');location='MawbManagement.aspx'</script>");
                   
                }
            }
        }
        /// <summary>
        /// 页面控件赋值
        /// </summary>
        /// <param name="mawb"></param>
        protected void Evaluate(MAWB mawb)
        {
            lbl_MAWBBarCode.Text = mawb.BarCode;
            txt_CreateTime.Text = mawb.CreateTime.ToString("yyyy-MM-dd HH:mm");
            if (mawb.LockedTime != null)
            {
                txt_LockTime.Text = mawb.LockedTime.Value.ToString("yyyy-MM-dd HH:mm");
            }
            lbtn_FLTNo.Text = mawb.FlightNo;
            txt_TotalVolume.Text = mawb.TotalVolume.ToString();
            Txt_TotalWeight.Text = mawb.TotalWeight.ToString();
            txt_Status.Text = mawb.Status.ToString().Replace("0","打开").Replace("1","关闭");
            gv_MAWB.DataSource = mawb.Packages;
            gv_MAWB.DataBind();
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
        /// 返回按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void But_Conel_Click(object sender, EventArgs e)
        {
            Response.Redirect(ViewState["UrlReferrer"].ToString());
        }
        /// <summary>
        /// 修改按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void But_Next_Click(object sender, EventArgs e)
        {
            Response.Redirect("MawbModify.aspx?BarCode=" + lbl_MAWBBarCode.Text + "&Privilege=" + Request.QueryString["Privilege"] + "&aPrivilege=" + Request.QueryString["aPrivilege1"] + "");
          
        }
        /// <summary>
        /// 查看航班信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtn_FLTNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("../FlightManage/FlightManagement.aspx?FlightNo=" + lbtn_FLTNo.Text + "");
        }
        /// <summary>
        /// 导出总运单清单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_DeriveSince_Click(object sender, EventArgs e)
        {
            MAWB mawb = _mawbService.FindMAWBByBarcode(lbl_MAWBBarCode.Text);
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
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(String.Format("{0}.xls", lbl_MAWBBarCode.Text + "总运单清单"), System.Text.Encoding.UTF8));
                HttpContext.Current.Response.BinaryWrite(data);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('该总运单下没有运单，不能导出!')</script>");
            }
        }
        /// <summary>
        /// 导出承运公司清单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_DeriveAccept_Click(object sender, EventArgs e)
        {
            MAWB mawb = _mawbService.FindMAWBByBarcode(lbl_MAWBBarCode.Text);
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
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(String.Format("{0}.xls", lbl_MAWBBarCode.Text + "承运清单"), System.Text.Encoding.UTF8));
                HttpContext.Current.Response.BinaryWrite(data);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('该总运单下没有运单，不能导出!')</script>");
            }
        }

    }
}