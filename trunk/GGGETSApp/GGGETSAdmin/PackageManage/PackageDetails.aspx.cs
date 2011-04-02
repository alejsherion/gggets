using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;

namespace GGGETSAdmin.PackageManage
{
    public partial class PackageDetails : System.Web.UI.Page
    {
        private IPackageManagementService _packageservice;
        private IMAWBManagementService _mawbservice;
        private ISysUserManagementService _SysUserManagementService;
        private Package package;

        public int n = 1;
        protected PackageDetails()
        { }
        public PackageDetails(IPackageManagementService packageservice, IMAWBManagementService mawbservice, ISysUserManagementService SysUserManagementService)
        {
            _packageservice = packageservice;
            _mawbservice = mawbservice;
            _SysUserManagementService = SysUserManagementService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                if (Request.QueryString["BarCode"] != "" && Request.QueryString["BarCode"] != null)
                {
                   
                    ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
                    package = _packageservice.FindPackageByBarcode(Request.QueryString["BarCode"]);//根据包号获取包信息
                    if (package != null)
                    {
                        Evaluate(package);
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('没有相关记录！');location='packageManagement.aspx'</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('没有相关记录！');location='packageManagement.aspx'</script>");
                }
            }
        }
        /// <summary>
        /// 页面控件赋值
        /// </summary>
        /// <param name="package"></param>
        protected void Evaluate(Package package)
        {
            txt_BagBarCode.Text = package.BarCode;
            txt_CreateTime.Text = package.CreateTime.ToString("yyyy-MM-dd HH:mm");
            txt_UpdateTime.Text = package.UpdateTime.ToString("yyyy-MM-dd HH:mm");
            
            if (package.MAWB != null)
            {
                lbtn_MHAWb.Text = package.MAWB.BarCode;
                
            }
            Txt_OriginalRegionCode.Text = package.OriginalRegionCode;
            Txt_DestinationRegionCode.Text = package.DestinationRegionCode;
            txt_Pice.Text = package.Piece.ToString();
            Txt_TotalWeight.Text = package.TotalWeight.ToString();
            txt_Status.Text = package.Status.ToString().Replace("0","打开").Replace("1","关闭");
            gv_HAWB.DataSource = package.HAWBs;
            gv_HAWB.DataBind();
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
        /// 返回上一级页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void But_Conel_Click(object sender, EventArgs e)
        {
            Response.Redirect((string)ViewState["UrlReferrer"]);
        }
        /// <summary>
        /// 修改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void But_Next_Click(object sender, EventArgs e)
        {
            //if (txt_Status.Text == "打开")
            //{
            Response.Redirect("packageModify.aspx?BarCode=" + txt_BagBarCode.Text + "&Privilege=" + Request.QueryString["Privilege"] + "&Privilege1="+Request.QueryString["Privilege1"]+"");
            //}
            //else
            //{
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('该包处于关闭状态，不能进行修改！')</script>");
            //}
        }
        /// <summary>
        /// 查看该包所在的总运单信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtn_MHAWb_Click(object sender, EventArgs e)
        {
            Response.Redirect("../MawbManage/MawbDetails.aspx?BarCode=" + lbtn_MHAWb.Text + "");
        }
    }
}