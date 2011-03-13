using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.IO;
namespace GGGETSAdmin.Common
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected IHAWBManagementService _hawbService;
        private IMAWBManagementService _mawbService;
        protected WebForm1()
        { }
         public WebForm1(IHAWBManagementService hawbService, IMAWBManagementService mawbService)
        {
            _hawbService = hawbService;
            _mawbService = mawbService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //HAWB hawbObj = _hawbService.LoadHAWBByBarCode("2014");
            ////var NpoiHelper = new NpoiHelper(hawbObj);
            //NpoiHelper.ExportInvoice();
            //var str = (MemoryStream)NpoiHelper.RenderToExcel();
            //if (str == null) return;
            //var data = str.ToArray();
            //var resp = Page.Response;
            //resp.Buffer = true;
            //resp.Clear();
            //resp.Charset = "utf-8";
            //resp.ContentEncoding = System.Text.Encoding.UTF8;
            //resp.ContentType = "application/ms-excel";
            //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(String.Format("{0}.xls", "Invoice"), System.Text.Encoding.UTF8));
            //HttpContext.Current.Response.BinaryWrite(data);
            //HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.End();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //MAWB mawbObj = _mawbService.FindMAWBByBarcode("0001");
            //IList<HAWB> hawbs = _hawbService.FindHAWBsByMID(mawbObj.MID.ToString());
            //var NpoiHelper = new NpoiHelper(mawbObj, hawbs);
            //NpoiHelper.ExportMAWB();
            //var str = (MemoryStream)NpoiHelper.RenderToExcel();
            //if (str == null) return;
            //var data = str.ToArray();
            //var resp = Page.Response;
            //resp.Buffer = true;
            //resp.Clear();
            //resp.Charset = "utf-8";
            //resp.ContentEncoding = System.Text.Encoding.UTF8;
            //resp.ContentType = "application/ms-excel";
            //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(String.Format("{0}.xls", "MAWB"), System.Text.Encoding.UTF8));
            //HttpContext.Current.Response.BinaryWrite(data);
            //HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.End();
        }
    }
}