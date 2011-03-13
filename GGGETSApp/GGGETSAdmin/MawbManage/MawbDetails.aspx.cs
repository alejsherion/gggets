﻿using System;
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
        private MAWB mawb;
        public int n = 1;
        protected MawbDetails()
        { }
        public MawbDetails(IMAWBManagementService mawbservice,IHAWBManagementService hawbservice)
        {
            _mawbService = mawbservice;
            _hawbService = hawbservice;
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
        public int N()
        {
            return n++;
        }
        protected void But_Conel_Click(object sender, EventArgs e)
        {
            Response.Redirect(ViewState["UrlReferrer"].ToString());
        }

        protected void But_Next_Click(object sender, EventArgs e)
        {
            //if (txt_Status.Text == "打开")
            //{
                Response.Redirect("MawbModify.aspx?BarCode=" + lbl_MAWBBarCode.Text + "");
            //}
            //else
            //{ Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('该总运单处于关闭状态，不能进行修改！')</script>"); }
        }

        protected void lbtn_FLTNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("../FlightManage/FlightManagement.aspx?FlightNo=" + lbtn_FLTNo.Text + "");
        }

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