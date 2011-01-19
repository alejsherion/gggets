using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace GGGETSAdmin.HAWB1
{
    public partial class NewHAWB : System.Web.UI.Page
    {
        private IHAWBManagementService _hawbService;
        protected NewHAWB()
        {
        }
        HAWB hawb1;
        private int i = 0;
        public NewHAWB(IHAWBManagementService hawbService)
        {
            _hawbService = hawbService;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["name"] != null && Session["name"] != "")
                {
                    Txt_Owner.Text = Session["name"].ToString();
                }

                if (Session["jilu"] != null)
                {
                    int i = Int32.Parse(Session["jilu"].ToString());
                    if (i > 0)
                    {
                        hawb1 = (HAWB)Session["hawb"];
                        FuZhi();

                    }
                }
                if (Session["item"] == null)
                {
                    this.But_AddHAWB.Enabled = false;
                }
            }
        }
        protected void But_AddItme_Click(object sender, EventArgs e)
        {
            CunZhi(); 
            Response.Redirect("AddItem1.aspx");
        }

        protected void But_AddHAWB_Click(object sender, EventArgs e)
        {
            CunZhi();
            HAWB hawb = (HAWB)Session["hawb"];
            Item item = (Item)Session["item"];
            hawb.Item.Add(item);
            _hawbService.AddHAWB(hawb);
            Session["jilu"] = "0";

        }
        private void CunZhi()
        {
            HAWB hawb = new HAWB();
            hawb.HID = Guid.NewGuid();
            hawb.BarCode = Txt_BarCode.Text;
            hawb.Carrier = Txt_Carrier.Text;
            hawb.CarrierHAWBID = Guid.Empty;
            hawb.Account = Guid.NewGuid();
            hawb.SettleType = DDl_SettleType.Text;
            hawb.ServiceType = DDl_ServiceType.Text;
            hawb.DeadlienTime = DateTime.Parse(Txt_DeadlineTime.Text);
            hawb.Owner = Guid.NewGuid();
            hawb.ShipperID = Guid.NewGuid();
            hawb.ShipperContactor = Txt_ShipperContactor.Text;
            hawb.ShipperCountry = Txt_ShipperCountry.Text;
            hawb.ShipperRegion = Txt_ShipperRegion.Text;
            hawb.ShipperAddress = Txt_ShipperAddress.Text;
            hawb.ShipperTel = Txt_ShipperTel.Text;
            hawb.ShipperZipCode = Txt_ShipperZipCode.Text;
            hawb.ConsigneeID = Guid.NewGuid();
            hawb.ConsigneeContactor = Txt_ConsigneeContactor.Text;
            hawb.ConsigneeCountry = Txt_ConsigneeCountry.Text;
            hawb.ConsigneeRegion = Txt_ConsigneeRegion.Text;
            hawb.ConsigneeAddress = Txt_ConsigneeAddress.Text;
            hawb.ConsigneeTel = Txt_ConsigneeTel.Text;
            hawb.ConsigneeZipCode = Txt_ConsigneeZipCode.Text;
            //hawb1.WeightType =  DDl_WeightType.Text;
            hawb.Piece = int.Parse(Txt_Piece.Text);
            if (DDL_IsInternational.SelectedValue == "false")
                hawb.IsInternational = false;
            else
                hawb.IsInternational = true;
            hawb.SpecialInstruction = Txt_SpecialInstruction.Text;
            hawb.Taxes = Txt_Taxes.Text;
            hawb.Description = Txt_Description.Text;
            hawb.Remark = Txt_Remark.Text;
            Session["hawb"] = hawb;           
        }
        private void FuZhi()
        {
            Txt_BarCode.Text = hawb1.BarCode;
            Txt_Carrier.Text = hawb1.Carrier;
            //hawb.CarrierHAWBID = Guid.Empty;
            //hawb.Account = Guid.NewGuid();
            DDl_SettleType.Text = hawb1.SettleType;
            DDl_ServiceType.Text = hawb1.ServiceType;
            Txt_DeadlineTime.Text = hawb1.DeadlienTime.ToString();
            //hawb.Owner = Guid.NewGuid();
            //hawb.ShipperID = Guid.NewGuid();
            Txt_ShipperContactor.Text = hawb1.ShipperContactor; ;
            Txt_ShipperCountry.Text = hawb1.ShipperCountry;
            Txt_ShipperRegion.Text = hawb1.ShipperRegion;
            Txt_ShipperAddress.Text = hawb1.ShipperAddress;
            Txt_ShipperTel.Text = hawb1.ShipperTel;
            Txt_ShipperZipCode.Text = hawb1.ShipperZipCode;
            //hawb.ConsigneeID = Guid.NewGuid();
            Txt_ConsigneeContactor.Text = hawb1.ConsigneeContactor;
            Txt_ConsigneeCountry.Text = hawb1.ConsigneeCountry;
            Txt_ConsigneeRegion.Text = hawb1.ConsigneeRegion;
            Txt_ConsigneeAddress.Text = hawb1.ConsigneeAddress;
            Txt_ConsigneeTel.Text = hawb1.ConsigneeTel;
            Txt_ConsigneeZipCode.Text = hawb1.ConsigneeZipCode;
            //hawb1.WeightType = DDl_WeightType.Text;
            Txt_Piece.Text = hawb1.Piece.ToString();
            //hawb1.IsInternational
            Txt_SpecialInstruction.Text = hawb1.SpecialInstruction;
            Txt_Taxes.Text = hawb1.Taxes;
            Txt_Description.Text = hawb1.Description;
            Txt_Remark.Text = hawb1.Remark;
            
        }
    }
}