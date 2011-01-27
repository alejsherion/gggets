using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using System.Text.RegularExpressions;

namespace GGGETSAdmin.HAWB
{
    public partial class HAWBModify : System.Web.UI.Page
    {
        public int n=1;
        private ETS.GGGETSApp.Domain.Application.Entities.HAWB hawb;
        //private HAWB hawb = new HAWB();
        private IHAWBManagementService _IHawbService;
        protected HAWBModify()
        { }
        public HAWBModify(IHAWBManagementService IHawbService)
        {
            _IHawbService = IHawbService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Gv_BaleXinXi.DataSource = null;
                this.Gv_BaleXinXi.DataBind();
                if (Request.QueryString["BarCode"] != "" && Request.QueryString["BarCode"] != null)
                {
                    hawb = _IHawbService.FindHAWBByBarCode(Request.QueryString["BarCode"].ToString());
                    Session["HAWB"] = hawb;
                    
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
                            ((LinkButton)gvr.FindControl("lbt_Eit") as LinkButton).Visible = false;
                            ((LinkButton)gvr.FindControl("lbt_Delete") as LinkButton).Visible = false;
                        }
                    }
                    Evaluate();
                }
            }
        }

        protected void But_AddHAWB_Click(object sender, EventArgs e)
        {
            Storage();
            ETS.GGGETSApp.Domain.Application.Entities.HAWB ha = (ETS.GGGETSApp.Domain.Application.Entities.HAWB)Session["HAWB"];
            _IHawbService.ChangeHAWB((ETS.GGGETSApp.Domain.Application.Entities.HAWB)Session["HAWB"]);
            Response.Write("<script>alert('修改成功！');location='HAWBManagement.aspx'</script>");
        }
        private void Storage()
        {
            if (Session["HAWB"] != null)
            {
                ETS.GGGETSApp.Domain.Application.Entities.HAWB ha = (ETS.GGGETSApp.Domain.Application.Entities.HAWB)Session["HAWB"];

                //hawb1.HID = ha.HID;
                ha.BarCode = Txt_BarCode.Text;
                ha.Carrier = Txt_Carrier.Text;
                //ha.CarrierHAWBID = hawb1.CarrierHAWBID;
                //hawb1.Account = ha.Account;
                ha.SettleType = DDl_SettleType.Text;
                ha.ServiceType = DDl_ServiceType.Text;
                //hawb1.CreateTime = ha.CreateTime;
                if (Txt_DeadlineTime.Text != "")
                {
                    ha.DeadlienTime = DateTime.Parse(Txt_DeadlineTime.Text);
                }
                // hawb1.Owner = ha.Owner;
                // hawb1.ShipperID = ha.ShipperID;
                ha.ShipperContactor = Txt_ShipperContactor.Text;
                ha.ShipperCountry = Txt_ShipperCountry.Text;
                ha.ShipperRegion = Txt_ShipperRegion.Text;
                ha.ShipperAddress = Txt_ShipperAddress.Text;
                ha.ShipperTel = Txt_ShipperTel.Text;
                ha.ShipperZipCode = Txt_ShipperZipCode.Text;
                // hawb1.ConsigneeID = ha.ConsigneeID;
                ha.ConsigneeContactor = Txt_ConsigneeContactor.Text;
                ha.ConsigneeCountry = Txt_ConsigneeCountry.Text;
                ha.ConsigneeRegion = Txt_ConsigneeRegion.Text;
                ha.ConsigneeAddress = Txt_ConsigneeAddress.Text;
                ha.ConsigneeTel = Txt_ConsigneeTel.Text;
                ha.ConsigneeZipCode = Txt_ConsigneeZipCode.Text;
                //hawb1.WeightType =  DDl_WeightType.Text;
                ha.Piece = int.Parse(Txt_Piece.Text);
                ha.IsInternational = true;
                ha.SpecialInstruction = "有";
                ha.Taxes = Txt_Taxes.Text;
                ha.Description = Txt_Description.Text;
                ha.Remark = Txt_Remark.Text;
                ha.UpdateTime = DateTime.Now;
                Session["HAWB"] = ha;
            }
        }
        private void Evaluate()
        {
            lbl_Hid.Text = hawb.HID.ToString();
            Txt_BarCode.Text = hawb.BarCode;
            Txt_Carrier.Text = hawb.Carrier;
            //hawb.CarrierHAWBID = Guid.Empty;
            //hawb.Account = Guid.NewGuid();
            DDl_SettleType.Text = hawb.SettleType;
            DDl_ServiceType.Text = hawb.ServiceType;
            if (hawb.DeadlienTime != null && hawb.DeadlienTime.ToString() != "")
            {
                DateTime dt = DateTime.Parse(hawb.DeadlienTime.ToString());
                Txt_DeadlineTime.Text = dt.ToString("yyyy-MM-dd");
            }
            //hawb.Owner = Guid.NewGuid();
            //hawb.ShipperID = Guid.NewGuid();
            Txt_ShipperID.Text = hawb.ShipperID.ToString();
            Txt_ShipperContactor.Text = hawb.ShipperContactor;
            Txt_ShipperCountry.Text = hawb.ShipperCountry;
            Txt_ShipperRegion.Text = hawb.ShipperRegion;
            Txt_ShipperAddress.Text = hawb.ShipperAddress;
            Txt_ShipperTel.Text = hawb.ShipperTel;
            Txt_ShipperZipCode.Text = hawb.ShipperZipCode;
            Txt_ConsigneeID.Text = hawb.ConsigneeID.ToString();
            Txt_ConsigneeContactor.Text = hawb.ConsigneeContactor;
            Txt_ConsigneeCountry.Text = hawb.ConsigneeCountry;
            Txt_ConsigneeRegion.Text = hawb.ConsigneeRegion;
            Txt_ConsigneeAddress.Text = hawb.ConsigneeAddress;
            Txt_ConsigneeTel.Text = hawb.ConsigneeTel;
            Txt_ConsigneeZipCode.Text = hawb.ConsigneeZipCode;
            //hawb1.WeightType = DDl_WeightType.Text;
            Txt_Piece.Text = (n-1).ToString();
            //if (hawb1.IsInternational==true)

            //if (hawb1.SpecialInstruction == "有")
            //    Ck_You.Checked = true;
            //else if (hawb1.SpecialInstruction == "无")
            //    Ck_Wu.Checked = true;
            //else
            //    Ck_QiTa.Checked = true;

            Txt_Taxes.Text = hawb.Taxes;
            Txt_Description.Text = hawb.Description;
            Txt_Remark.Text = hawb.Remark;

        }

        protected void But_Retun_Click(object sender, EventArgs e)
        {
            Response.Redirect("HAWBManagement.aspx");
        }

        protected void Gv_BaleXinXi_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


            ETS.GGGETSApp.Domain.Application.Entities.HAWB hawb = (ETS.GGGETSApp.Domain.Application.Entities.HAWB)Session["HAWB"];
            List<Item> items = hawb.Item.ToList();
            int rowIndex = e.RowIndex;

            Item updatedItem = items[rowIndex];
            string decimalPattern = @"^[0]{1}\.?[0-9]{0,2}|[1-9]+\.?[0-9]{0,2}$";
            //string intPattern = @"^[1-9]+";
            bool Ok = true;
            if (!Regex.IsMatch(((TextBox)Gv_BaleXinXi.Rows[e.RowIndex].Cells[2].Controls[0]).Text.Trim(), decimalPattern))
            {
                Ok = false;
            }
            if (!Regex.IsMatch(((TextBox)Gv_BaleXinXi.Rows[e.RowIndex].Cells[4].Controls[0]).Text.Trim(), decimalPattern))
            {

                Ok = false;
            }
            if (!Regex.IsMatch((Gv_BaleXinXi.Rows[e.RowIndex].FindControl("Txt_Weight") as TextBox).Text.Trim(), decimalPattern))
            {
                Ok = false;
            }
            if (!Regex.IsMatch(((TextBox)Gv_BaleXinXi.Rows[e.RowIndex].Cells[3].Controls[0]).Text.Trim(), decimalPattern))
            {
                Ok = false;
            }
            //if (!Regex.IsMatch(((TextBox)Gv_BaleXinXi.Rows[e.RowIndex].Cells[5].Controls[0]).Text.Trim(), decimalPattern))
            //{
            //    Ok = false;
            //}
            //if (!Regex.IsMatch(((TextBox)Gv_BaleXinXi.Rows[e.RowIndex].Cells[6].Controls[0]).Text.Trim(), intPattern))
            //{
            //    Ok = false;
            //}

            if (Ok != false)
            {
                foreach (GridViewRow gvrow in Gv_BaleXinXi.Rows)
                {
                    updatedItem.Weight = decimal.Parse(((TextBox)gvrow.FindControl("Txt_Weight") as TextBox).Text.Trim());
                }
                updatedItem.Width = decimal.Parse(((TextBox)Gv_BaleXinXi.Rows[e.RowIndex].Cells[3].Controls[0]).Text.Trim());
                updatedItem.Height = decimal.Parse(((TextBox)Gv_BaleXinXi.Rows[e.RowIndex].Cells[2].Controls[0]).Text.Trim());
               
                updatedItem.Length = decimal.Parse(((TextBox)Gv_BaleXinXi.Rows[e.RowIndex].Cells[4].Controls[0]).Text.Trim());
                //updatedItem.TransPays = decimal.Parse(((TextBox)Gv_BaleXinXi.Rows[e.RowIndex].Cells[5].Controls[0]).Text.Trim());
                //updatedItem.TransCurrency = int.Parse(((TextBox)Gv_BaleXinXi.Rows[e.RowIndex].Cells[6].Controls[0]).Text.Trim());
                Gv_BaleXinXi.EditIndex = -1;
                Gv_BaleXinXi.DataSource = items;
                Gv_BaleXinXi.DataBind();
                Session["HAWB"] = hawb;
                this.Gv_BaleXinXi.Rows[e.RowIndex].FindControl("lbt_Eit").Visible = true;
                this.Gv_BaleXinXi.Rows[e.RowIndex].FindControl("lbt_Up").Visible = false;
                this.Gv_BaleXinXi.Rows[e.RowIndex].FindControl("lbt_cancel").Visible = false;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入整数或小数，且小数点后最多保留两位！支付货币只能为整数！')</script>");

            }
        }

        protected void Gv_BaleXinXi_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Gv_BaleXinXi.EditIndex = -1;
            ETS.GGGETSApp.Domain.Application.Entities.HAWB hawb = (ETS.GGGETSApp.Domain.Application.Entities.HAWB)Session["HAWB"];
            Gv_BaleXinXi.DataSource = hawb.Item;
            Gv_BaleXinXi.DataBind();
            this.Gv_BaleXinXi.Rows[e.RowIndex].FindControl("lbt_Eit").Visible = true;
            this.Gv_BaleXinXi.Rows[e.RowIndex].FindControl("lbt_Up").Visible = false;
            this.Gv_BaleXinXi.Rows[e.RowIndex].FindControl("lbt_cancel").Visible = false;
        }

        protected void Gv_BaleXinXi_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void Gv_BaleXinXi_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Gv_BaleXinXi.EditIndex = e.NewEditIndex;
            ETS.GGGETSApp.Domain.Application.Entities.HAWB hawb = (ETS.GGGETSApp.Domain.Application.Entities.HAWB)Session["HAWB"];
            Gv_BaleXinXi.DataSource = hawb.Item;
            Gv_BaleXinXi.DataBind();
            this.Gv_BaleXinXi.Rows[e.NewEditIndex].FindControl("lbt_Eit").Visible = false;
            this.Gv_BaleXinXi.Rows[e.NewEditIndex].FindControl("lbt_Up").Visible = true;
            this.Gv_BaleXinXi.Rows[e.NewEditIndex].FindControl("lbt_cancel").Visible = true;
        }

        protected void But_ItemAdd_Click(object sender, EventArgs e)
        {
            Item item = new Item();
            ETS.GGGETSApp.Domain.Application.Entities.HAWB hawb = (ETS.GGGETSApp.Domain.Application.Entities.HAWB)Session["HAWB"];
            item.Id = Guid.NewGuid();
            //item.Height = decimal.Parse(Txt_ItemHeight.Text);
            //item.Length = decimal.Parse(Txt_ItemLength.Text);
            //if (Txt_ItemTransCurrency.Text == "")
            //{
            //    item.TransCurrency = 0;
            //}
            //else
            //    item.TransCurrency = int.Parse(Txt_ItemTransCurrency.Text);
            //if (Txt_ItemTransPays.Text == "")
            //{
            //    item.TransPays = decimal.Parse("0");
            //}
            //else
            //{
            //    item.TransPays = decimal.Parse(Txt_ItemTransPays.Text);
            //}
            //item.Weight = decimal.Parse(Txt_ItemWeight.Text);
            //item.Width = decimal.Parse(Txt_ItemWidth.Text);
            //hawb.Item.Add(item);
            //Gv_BaleXinXi.DataSource = hawb.Item;
            //Gv_BaleXinXi.DataBind();
            //AddItem.Visible = false;
            //gvXinxi.Visible = true;
        }
        protected void lbt_AddItem_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddItem.aspx");
        }
        public int N()
        {
            return n++;
        }
    }
}