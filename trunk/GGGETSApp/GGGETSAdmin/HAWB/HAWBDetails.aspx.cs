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
    public partial class HAWBSetAndEit : System.Web.UI.Page
    {
        private HAWB hawb1 = new HAWB();
        private IHAWBManagementService _IHawbService;
        protected HAWBSetAndEit()
        { }
        public HAWBSetAndEit(IHAWBManagementService IHawbService)
        {
            _IHawbService = IHawbService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                

                if (Request.QueryString["Type"] != "" || Request.QueryString["Type"] != null)
                {
                    string type = Request.QueryString["Type"];
                    if (type == "Select")
                    {
                        
                        if (Request.QueryString["BarCode"] != "" || Request.QueryString["BarCode"] != null)
                        {
                            hawb1 = _IHawbService.FindHAWBByBarCode(Request.QueryString["BarCode"]);
                            FuZhi();
                        }
                        for (int i = 0; i < Controls.Count; i++)
                        {
                            if (Controls[i] is TextBox)
                            {
                                ((TextBox)Controls[i]).Enabled = false;
                            }
                        }
                        this.But_AddHAWB.Text = "修 改";
                    }
                    else if (type == "Eit")
                    {
                        if (Request.QueryString["BarCode"] != "" || Request.QueryString["BarCode"] != null)
                        {
                            hawb1 = _IHawbService.FindHAWBByBarCode(Request.QueryString["BarCode"]);
                            FuZhi();
                        }
                        this.But_AddHAWB.Text = "提 交";
                    }
                }
            }
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

        protected void But_AddHAWB_Click(object sender, EventArgs e)
        {
          
        }


    }
}