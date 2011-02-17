using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;

namespace GGGETSAdmin.HAWBManage
{
    public partial class HAWBDetails : System.Web.UI.Page
    {
        protected static string BarCode = string.Empty;
        protected static HAWB hawb;
        protected IHAWBManagementService _hawbService;
        protected HAWBDetails()
        { }
        public HAWBDetails(IHAWBManagementService hawbService)
        {
            _hawbService = hawbService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["BarCode"] != null)
                {
                    BarCode = Request.QueryString["BarCode"];
                    hawb = _hawbService.LoadHAWBByBarCode(BarCode);
                    ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
                    Storage();
                }
                else
                {
                    Response.Redirect((string)ViewState["UrlReferrer"]);
                }
            }
        }
        protected void Storage()
        {
            Txt_BarCode.Text = hawb.BarCode;
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
            Txt_ShipperCountry.Text = hawb.ShipperCountry;
            Txt_ShipperRegion.Text = hawb.ShipperRegion;
            Txt_ShipperAddress.Text = hawb.ShipperAddress;
            Txt_ShipperTel.Text = hawb.ShipperTel;
            Txt_ShipperZipCode.Text = hawb.ShipperZipCode;

            Txt_ConsigneeName.Text = hawb.ConsigneeName.ToString();
            Txt_ConsigneeContactor.Text = hawb.ConsigneeContactor;
            Txt_ConsigneeCountry.Text = hawb.ConsigneeCountry;
            Txt_ConsigneeRegion.Text = hawb.ConsigneeRegion;
            Txt_ConsigneeAddress.Text = hawb.ConsigneeAddress;
            Txt_ConsigneeTel.Text = hawb.ConsigneeTel;
            Txt_ConsigneeZipCode.Text = hawb.ConsigneeZipCode;

            if (hawb.DeliverName != "")
            {
                Deliver.Visible = true;
                Txt_DeliverName.Text = hawb.DeliverName;
                Txt_DeliverAddress.Text = hawb.DeliverAddress;
                Txt_DeliverCountry.Text = hawb.DeliverCountry;
                Txt_DeliverRegion.Text = hawb.DeliverRegion;
                Txt_DeliverContactor.Text = hawb.DeliverContactor;
                Txt_DeliverZipCode.Text = hawb.DeliverZipCode;
                Txt_DeliverTel.Text = hawb.DeliverTel;
            }
            else
            {
                Deliver.Visible = false;
            }
            //foreach(HAWBBox box in hawb.HAWBBoxes)
            //{
            //    rbt_BoxType.SelectedValue = box.BoxType.ToString();
            //}
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
            Txt_CarrierHAWBID.Text = hawb.CarrierHAWBID.ToString();
            gv_Box.DataSource = hawb.HAWBBoxes;
            gv_Box.DataBind();
            GV_item.DataSource = hawb.HAWBItems;
            GV_item.DataBind();
        }
        protected void But_Conel_Click(object sender, EventArgs e)
        {
            Response.Redirect((string)ViewState["UrlReferrer"]);
        }

        protected void But_Next_Click(object sender, EventArgs e)
        {
            int Update = 1;
            Response.Redirect("HAWBAdd.aspx?BarCode=" + BarCode + "&update="+Update+"");
        }
    }
}