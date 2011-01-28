using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;


namespace GGGETSWeb.HAWB
{
    public partial class AddHAWB : System.Web.UI.Page
    {
        private IHAWBManagementService _hawbService;
        protected AddHAWB()
        {
        }
        ETS.GGGETSApp.Domain.Application.Entities.HAWB hawb = new ETS.GGGETSApp.Domain.Application.Entities.HAWB();
        public AddHAWB(IHAWBManagementService hawbService)
        {
            _hawbService = hawbService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void But_AddHAWB_Click(object sender, EventArgs e)
        {
            Storage();
            _hawbService.AddHAWB(hawb);
            Response.Write("<script>alert('添加成功！')</script>");
        }
        private void Storage()
        {

            hawb.HID = Guid.NewGuid();
            hawb.BarCode = Txt_BarCode.Text;
            hawb.CarrierHAWBID = Guid.NewGuid();
            hawb.Account = Guid.NewGuid();
            hawb.SettleType = DDl_SettleType.Text;
            hawb.ServiceType = DDl_ServiceType.Text;
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
            if (Txt_Piece.Text != "" && Txt_Piece.Text != null)
                hawb.Piece = int.Parse(Txt_Piece.Text);
            //if (DDL_IsInternational.SelectedValue == "false")
            hawb.IsInternational = false;
            //else
            //hawb.IsInternational = true;
            hawb.Description = Txt_Description.Text;
            hawb.Remark = Txt_Remark.Text;
            hawb.CreateTime = DateTime.Now;
        }

        protected void But_But_Runrt_Click(object sender, EventArgs e)
        {

        }
    }
}