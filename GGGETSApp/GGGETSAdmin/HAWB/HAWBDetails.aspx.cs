using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace GGGETSAdmin.HAWB
{
    public partial class HAWBDetails : System.Web.UI.Page
    {
        public int n = 1;
        private ETS.GGGETSApp.Domain.Application.Entities.HAWB hawb;
        private IHAWBManagementService _IHawbService;
        public HAWBDetails()
        { }
        public HAWBDetails(IHAWBManagementService IHawbService)
        {
            _IHawbService = IHawbService; ;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["BarCode"] != "" && Request.QueryString["BarCode"] != null)
                {
                    hawb = _IHawbService.FindHAWBByBarCode(Request.QueryString["BarCode"].ToString());
                    
                    if (hawb.Item.Count > 0)
                    {
                        Gv_BaleXinXi.DataSource = hawb.Item;
                        Gv_BaleXinXi.DataBind();
                    }
                    else
                    {
                        Item item = new Item();
                        List<Item> list = new List<Item>();
                        list.Add(item);
                        Gv_BaleXinXi.DataSource = list;
                        Gv_BaleXinXi.DataBind();
                        foreach (GridViewRow gvr in Gv_BaleXinXi.Rows)
                        {
                            ((Label)gvr.FindControl("lbl_n") as Label).Visible = false;
                            ((Label)gvr.FindControl("lbl_Weight") as Label).Visible = false;
                        }
                    }
                    Evaluate();
                }
            }
        }

        protected void But_Conel_Click(object sender, EventArgs e)
        {
            Response.Redirect("HAWBManagement.aspx");
        }
        private void Evaluate()
        {
            lbl_Hid.Text = hawb.HID.ToString();
            Txt_BarCode.Text = hawb.BarCode;
            Txt_Carrier.Text = hawb.Carrier;
            //hawb.CarrierHAWBID = Guid.Empty;
            //hawb.Account = Guid.NewGuid();
            Txt_SettleType.Text = hawb.SettleType;
            Txt_ServiceType.Text = hawb.ServiceType;
            //Txt_DeadlineTime.Text = hawb.DeadlienTime.ToString();
            //hawb.Owner = Guid.NewGuid();
            //hawb.ShipperID = Guid.NewGuid();
            Txt_ShipperContactor.Text = hawb.ShipperContactor;
            Txt_ShipperCountry.Text = hawb.ShipperCountry;
            Txt_ShipperRegion.Text = hawb.ShipperRegion;
            Txt_ShipperAddress.Text = hawb.ShipperAddress;
            Txt_ShipperTel.Text = hawb.ShipperTel;
            Txt_ShipperZipCode.Text = hawb.ShipperZipCode;
            //hawb.ConsigneeID = Guid.NewGuid();
            Txt_ConsigneeContactor.Text = hawb.ConsigneeContactor;
            Txt_ConsigneeCountry.Text = hawb.ConsigneeCountry;
            Txt_ConsigneeRegion.Text = hawb.ConsigneeRegion;
            Txt_ConsigneeAddress.Text = hawb.ConsigneeAddress;
            Txt_ConsigneeTel.Text = hawb.ConsigneeTel;
            Txt_ConsigneeZipCode.Text = hawb.ConsigneeZipCode;
            //Txt_WeightType.Text = hawb1.WeightType;
            Txt_Piece.Text = (n-1).ToString();
            //if (hawb1.IsInternational==true)
            hawb.SpecialInstruction = "有";
            Txt_State.Text = hawb.State.ToString();
            Txt_Taxes.Text = hawb.Taxes;
            Txt_Description.Text = hawb.Description;
            Txt_Remark.Text = hawb.Remark;

            if (hawb.CreateTime != null && hawb.CreateTime.ToString() != "")
            {
                //DateTime dtCreate = DateTime.Parse(hawb1.CreateTime.ToString());
                Txt_CreateTime.Text = hawb.CreateTime.ToString();
            }
            if (hawb.UpdateTime != null && hawb.UpdateTime.ToString() != "")
            {
                //DateTime dtUpdate = DateTime.Parse(hawb1.UpdateTime.ToString());
                Txt_UpdateTime.Text = hawb.UpdateTime.ToString();
            }

        }

        //protected void But_Up_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("HAWBModify.aspx?BarCode=" + Txt_BarCode.Text + "");
        //}
        public int N()//显示货物编号
        {
            return n++;
        }
    }
}