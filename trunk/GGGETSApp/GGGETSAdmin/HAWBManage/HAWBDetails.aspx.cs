using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;
using GGGETSAdmin.Common;
using System.IO;

namespace GGGETSAdmin.HAWBManage
{
    public partial class HAWBDetails : System.Web.UI.Page
    {
        private string BarCode = string.Empty;
        private HAWB hawb;
        private static IList<CountryCode> listcountry;
        private static IList<RegionCode> listregion;
        private static ICountryCodeManagementService _countryservice;
        private static IRegionCodeManagementService _regionservice;
        private IHAWBManagementService _hawbService;
        private ISysUserManagementService _sysUserManagementService;
        protected HAWBDetails()
        { }
        public HAWBDetails(IHAWBManagementService hawbService, ICountryCodeManagementService countryservice, IRegionCodeManagementService regionservice,ISysUserManagementService sysUserManagementService)
        {
            _hawbService = hawbService;
            _countryservice = countryservice;
            _regionservice = regionservice;
            _sysUserManagementService = sysUserManagementService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listcountry = _countryservice.FindAllCountries();
                listregion = _regionservice.FindAllRegionCodes();
                
                if (!string.IsNullOrWhiteSpace(Request.QueryString["BarCode"]))
                {
                    BarCode = Request.QueryString["BarCode"];
                    hawb = _hawbService.LoadHAWBByBarCode(BarCode);
                    if (Request.UrlReferrer != null)
                    {
                        ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
                    }
                    if (Session["UserID"] != null)
                    {
                        Storage(hawb);
                        Guid id = (Guid)Session["UserID"];
                        ModulePrivilege Mprivilege = _sysUserManagementService.GetPrivilegeByUserid(id);
                        if (!bool.Parse(Request.QueryString["Privilege"]))
                        {
                            But_Update.Enabled = false;
                        }
                        if (!bool.Parse(Request.QueryString["Privilege1"]))
                        {
                            btn_DeriveAccept.Enabled = false;
                            btn_DeriveSince.Enabled = false;
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('没有访问权限!');location='../HOME.aspx'</script>");
                    }
                    
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('没有该运单！')</script>");
                    if (ViewState["UrlReferrer"] != null)
                    {
                        Response.Redirect((string)ViewState["UrlReferrer"]);
                    }
                    else
                    {
                        Response.Redirect("../Navigation.aspx");
                    }
                }
            }
        }
        /// <summary>
        /// 页面控件赋值
        /// </summary>
        protected void Storage(HAWB hawb)
        {
            Txt_BarCode.Text = hawb.BarCode;
            Department depar = hawb.Department;
            Txt_Account1.Text = depar.CompanyCode;
            Txt_Account2.Text = depar.DepCode;
            DDl_SettleType.SelectedValue = hawb.SettleType.ToString();
            switch (hawb.Status)
            {
                case 0:
                    txt_Status.Text = "待审核";
                    break;
                case 1:
                    txt_Status.Text = "取货";
                    break;
                case 2:
                    txt_Status.Text = "核单";
                    break;
                case 3:
                    txt_Status.Text = "派送";
                    break;
                case 4:
                    txt_Status.Text = "in包";
                    break;
            }

            Txt_ShipperName.Text = hawb.ShipperName;
            Txt_ShipperContactor.Text = hawb.ShipperContactor;
            Txt_ShipperAddress.Text = hawb.ShipperAddress;
            Txt_ShipperTel.Text = hawb.ShipperTel;
            Txt_ShipperZipCode.Text = hawb.ShipperZipCode;

            Txt_ConsigneeName.Text = hawb.ConsigneeName;
            Txt_ConsigneeContactor.Text = hawb.ConsigneeContactor;
            Txt_ConsigneeAddress.Text = hawb.ConsigneeAddress;
            Txt_ConsigneeTel.Text = hawb.ConsigneeTel;
            Txt_ConsigneeZipCode.Text = hawb.ConsigneeZipCode;

            if (hawb.DeliverName != "" && hawb.DeliverName!=null)
            {
                foreach (CountryCode code in listcountry)
                {
                    if (code.CountryCode1 == hawb.ShipperCountry)
                    {
                        Txt_ShipperCountry.Text = code.CountryName;
                    }
                    if (code.CountryCode1 == hawb.ConsigneeCountry)
                    {
                        Txt_ConsigneeCountry.Text = code.CountryName;
                    }
                    if (code.CountryCode1 == hawb.DeliverCountry)
                    {
                        Txt_DeliverCountry.Text = code.CountryName;
                    }

                }
                foreach (RegionCode code in listregion)
                {
                    if (code.RegionCode1 == hawb.ShipperRegion)
                    {
                        Txt_ShipperRegion.Text = code.RegionName;
                    }
                    if (code.RegionCode1 == hawb.ConsigneeRegion)
                    {
                        Txt_ConsigneeRegion.Text = code.RegionName;
                    }
                    if (code.RegionCode1 == hawb.DeliverRegion)
                    {
                        Txt_DeliverRegion.Text = code.RegionName;
                    }
                }
                Deliver.Visible = true;
                Txt_DeliverName.Text = hawb.DeliverName;
                Txt_DeliverAddress.Text = hawb.DeliverAddress;
                Txt_DeliverContactor.Text = hawb.DeliverContactor;
                Txt_DeliverZipCode.Text = hawb.DeliverZipCode;
                Txt_DeliverTel.Text = hawb.DeliverTel;
                Txt_CarrierHAWBBarCode.Text = hawb.CarrierHAWBBarCode;
                Txt_Carrier.Text = hawb.Carrier;
            }
            else
            {
                Deliver.Visible = false;
                foreach (CountryCode code in listcountry)
                {
                    if (code.CountryCode1 == hawb.ShipperCountry)
                    {
                        Txt_ShipperCountry.Text = code.CountryName;
                    }
                    if (code.CountryCode1 == hawb.ConsigneeCountry)
                    {
                        Txt_ConsigneeCountry.Text = code.CountryName;
                    }
                }
                foreach (RegionCode code in listregion)
                {
                    if (code.RegionCode1 == hawb.ShipperRegion)
                    {
                        Txt_ShipperRegion.Text = code.RegionName;;
                    }
                    if (code.RegionCode1 == hawb.ConsigneeRegion)
                    {
                        Txt_ConsigneeRegion.Text = code.RegionName;
                    }
                    
                }
                rbt_BoxType.SelectedValue = hawb.ServiceType.ToString();
                rbt_payer.SelectedValue = hawb.BillTax.ToString();
                Rbl_SpecialInstruction.SelectedValue = hawb.SpecialInstruction;
                ddl_WeightType.SelectedValue = hawb.WeightType.ToString();
                hawb.CalculateTotalWeight();
                txt_TotalWeight.Text = hawb.TotalWeight.ToString();
                lbl_Piece.Text = hawb.Piece.ToString();
                lbl_TotalVolume.Text = hawb.TotalVolume.ToString();
                txt_Remark.Text = hawb.Remark;
                Txt_Carrier.Text = hawb.Carrier;
                Txt_CarrierHAWBBarCode.Text = hawb.CarrierHAWBBarCode;
                gv_Box.DataSource = hawb.HAWBBoxes;
                gv_Box.DataBind();
                GV_item.DataSource = hawb.HAWBItems;
                GV_item.DataBind();
            }
        }
        /// <summary>
        /// 返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void But_Conel_Click(object sender, EventArgs e)
        {
            Response.Redirect((string)ViewState["UrlReferrer"]);
        }

        protected void But_Next_Click(object sender, EventArgs e)
        {
            int Update = 1;
            Response.Redirect("HAWBAdd.aspx?BarCode=" + Txt_BarCode.Text + "&update=" + Update + "");
        }
        /// <summary>
        /// 导出运单发票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_DeriveSince_Click(object sender, EventArgs e)
        {
            HAWB hawb = _hawbService.FindHAWBByBarCode(Txt_BarCode.Text.Trim());
            var NpoiHelper = new NpoiHelper(hawb, Txt_BarCode.Text);
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
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(String.Format("{0}.xls", Txt_BarCode.Text+"运单发票"), System.Text.Encoding.UTF8));
            HttpContext.Current.Response.BinaryWrite(data);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 导出承运单发票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_DeriveAccept_Click(object sender, EventArgs e)
        {
            HAWB hawb = _hawbService.FindHAWBByBarCode(Txt_BarCode.Text.Trim());
            if (!string.IsNullOrEmpty(Txt_CarrierHAWBBarCode.Text))
            {
                var NpoiHelper = new NpoiHelper(hawb, Txt_CarrierHAWBBarCode.Text);
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
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(String.Format("{0}.xls", Txt_BarCode.Text + "承运发票"), System.Text.Encoding.UTF8));
                HttpContext.Current.Response.BinaryWrite(data);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('没有承运公司编号,不能导出！')</script>");
            }
        }
    }
}