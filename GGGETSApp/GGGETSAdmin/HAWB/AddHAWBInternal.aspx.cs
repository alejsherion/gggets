using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using System.Text.RegularExpressions;


namespace GGGETSAdmin.HAWB
{
    public partial class AddHAWBInernal : System.Web.UI.Page
    {
        private IHAWBManagementService _hawbService;
        protected AddHAWBInernal()
        {
        }
        ETS.GGGETSApp.Domain.Application.Entities.HAWB hawb = new ETS.GGGETSApp.Domain.Application.Entities.HAWB();
        ETS.GGGETSApp.Domain.Application.Entities.HAWB hawbUpdate;
        List<Item> lt = new List<Item>();
        Item itemUpdate;
        Item item = new Item();
        public int n=1;
        private int i=0;
        private int a = 0;
        public AddHAWBInernal(IHAWBManagementService hawbService)
        {
            _hawbService = hawbService;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                if (Session["jilu"] != null)
                {
                    int i = Int32.Parse(Session["jilu"].ToString());
                    if (i > 0)
                    {
                        hawbUpdate = (ETS.GGGETSApp.Domain.Application.Entities.HAWB)Session["hawb"];
                        Evaluate();

                    }
                }
                if (Session["item"] == null)
                {
                    lt.Add(item);
                    Gv_BaleXinXi.DataSource = lt;
                    Gv_BaleXinXi.DataBind();
                    foreach (GridViewRow gvr in Gv_BaleXinXi.Rows)
                    {
                        ((Label)gvr.FindControl("lbl_n") as Label).Visible = false;
                        ((Label)gvr.FindControl("lbl_Weight") as Label).Visible = false;
                        ((LinkButton)gvr.FindControl("lbt_Eit") as LinkButton).Visible = false;
                        ((LinkButton)gvr.FindControl("lbt_Delete") as LinkButton).Visible = false;
                    }
                }
                else
                {
                    lt.Add((Item)Session["item"]);
                    Gv_BaleXinXi.DataSource = lt;
                    Gv_BaleXinXi.DataBind();
                }


            }
        }
        protected void But_AddItme_Click(object sender, EventArgs e)
        {
            Storage();
            Response.Redirect("AddItem.aspx");
        }

        protected void But_AddHAWB_Click(object sender, EventArgs e)
        {
            Storage();
            ETS.GGGETSApp.Domain.Application.Entities.HAWB hawb = (ETS.GGGETSApp.Domain.Application.Entities.HAWB)Session["hawb"];
            Item item = (Item)Session["item"];
            hawb.Item.Add(item);
            _hawbService.AddHAWB(hawb);
            Session["jilu"] = "0";
            Session["item"] = null;
            Response.Write("<script>alert('添加成功！');location='HAWBManagement.aspx'</script>");


        }
        private void Storage()
        {

            hawb.HID = Guid.NewGuid();
            hawb.BarCode = Txt_BarCode.Text;
            hawb.Carrier = Txt_Carrier.Text;
            hawb.CarrierHAWBID = Guid.NewGuid();
            hawb.Account = Guid.NewGuid();
            hawb.SettleType = DDl_SettleType.Text;
            hawb.ServiceType = DDl_ServiceType.Text;
            if (Txt_DeadlineTime.Text != null && Txt_DeadlineTime.Text != "")
                hawb.DeadlienTime = DateTime.Parse(Txt_DeadlineTime.Text);
            hawb.Owner = Guid.NewGuid();
            hawb.ShipperID = Guid.NewGuid();
            hawb.ShipperContactor = Txt_ShipperContactor.Text;
            hawb.ShipperCountry = "国内";
            hawb.ShipperRegion = Txt_ShipperRegion.Text;
            hawb.ShipperAddress = Txt_ShipperAddress.Text;
            hawb.ShipperTel = Txt_ShipperTel.Text;
            hawb.ShipperZipCode = Txt_ShipperZipCode.Text;
            hawb.ConsigneeID = Guid.NewGuid();
            hawb.ConsigneeContactor = Txt_ConsigneeContactor.Text;
            hawb.ConsigneeCountry = "国内";
            hawb.ConsigneeRegion = Txt_ConsigneeRegion.Text;
            hawb.ConsigneeAddress = Txt_ConsigneeAddress.Text;
            hawb.ConsigneeTel = Txt_ConsigneeTel.Text;
            hawb.ConsigneeZipCode = Txt_ConsigneeZipCode.Text;
            //hawbUpdate.WeightType =  DDl_WeightType.Text;
            if (Txt_Piece.Text != "" && Txt_Piece.Text != null)
                hawb.Piece = int.Parse(Txt_Piece.Text);
            //if (DDL_IsInternational.SelectedValue == "false")
            //hawb.IsInternational = false;
            //else
            //    //hawb.IsInternational = true;
            //if (Ck_You.Checked == true)
            hawb.SpecialInstruction = "有";
            //else if (Ck_Wu.Checked == true)
            //    hawb.SpecialInstruction = Ck_Wu.Text;
            //else
            //    hawb.SpecialInstruction = Ck_QiTa.Text;
            hawb.Taxes = Txt_Taxes.Text;
            hawb.Description = Txt_Description.Text;
            hawb.Remark = Txt_Remark.Text;
            hawb.CreateTime = DateTime.Now;
            Session["hawb"] = hawb;
        }
        private void Evaluate()
        {
            if (hawbUpdate.BarCode != "")
                Txt_BarCode.Text = hawbUpdate.BarCode;
            if (hawbUpdate.Carrier != "")
                Txt_Carrier.Text = hawbUpdate.Carrier;
            //hawb.CarrierHAWBID = Guid.Empty;
            //hawb.Account = Guid.NewGuid();
            if (hawbUpdate.SettleType != "")
                DDl_SettleType.Text = hawbUpdate.SettleType;
            if (hawbUpdate.ServiceType != "")
                DDl_ServiceType.Text = hawbUpdate.ServiceType;
            if (hawbUpdate.DeadlienTime.ToString() != "")
                Txt_DeadlineTime.Text = hawbUpdate.DeadlienTime.ToString();
            //hawb.Owner = Guid.NewGuid();
            //hawb.ShipperID = Guid.NewGuid();
            if (hawbUpdate.ShipperContactor != "")
                Txt_ShipperContactor.Text = hawbUpdate.ShipperContactor;
            if (hawbUpdate.ShipperRegion != "")
                Txt_ShipperRegion.Text = hawbUpdate.ShipperRegion;
            if (hawbUpdate.ShipperAddress != "")
                Txt_ShipperAddress.Text = hawbUpdate.ShipperAddress;
            if (hawbUpdate.ShipperTel != "")
                Txt_ShipperTel.Text = hawbUpdate.ShipperTel;
            if (hawbUpdate.ShipperZipCode != "")
                Txt_ShipperZipCode.Text = hawbUpdate.ShipperZipCode;
            //hawb.ConsigneeID = Guid.NewGuid();
            if (hawbUpdate.ConsigneeContactor != "")
                Txt_ConsigneeContactor.Text = hawbUpdate.ConsigneeContactor;
            if (hawbUpdate.ConsigneeRegion != "")
                Txt_ConsigneeRegion.Text = hawbUpdate.ConsigneeRegion;
            if (hawbUpdate.ConsigneeAddress != "")
                Txt_ConsigneeAddress.Text = hawbUpdate.ConsigneeAddress;
            if (hawbUpdate.ConsigneeTel != "")
                Txt_ConsigneeTel.Text = hawbUpdate.ConsigneeTel;
            if (hawbUpdate.ConsigneeZipCode != "")
                Txt_ConsigneeZipCode.Text = hawbUpdate.ConsigneeZipCode;
            //hawbUpdate.WeightType = DDl_WeightType.Text;
            Txt_Piece.Text = n.ToString();
            //hawbUpdate.IsInternational
            //hawbUpdate.SpecialInstruction = "有";
            if (hawbUpdate.Taxes != "")
                Txt_Taxes.Text = hawbUpdate.Taxes;
            if (hawbUpdate.Description != "")
                Txt_Description.Text = hawbUpdate.Description;
            if (hawbUpdate.Remark != "")
                Txt_Remark.Text = hawbUpdate.Remark;

        }

        protected void Gv_BaleXinXi_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["a"] != null)
                a = int.Parse(Session["a"].ToString());
            Gv_BaleXinXi.EditIndex = e.NewEditIndex;
            if (a == 0)
            {
                itemUpdate = (Item)Session["item"];
                lt.Add(itemUpdate);
                Gv_BaleXinXi.DataSource = lt;
                Gv_BaleXinXi.DataBind();
                a++;
                Session["a"] = 1;
            }
            else
            {
                item = (Item)Session["item"];
                lt.Add(item);
                Gv_BaleXinXi.DataSource = lt;
                Gv_BaleXinXi.DataBind();
            }
            this.Gv_BaleXinXi.Rows[e.NewEditIndex].FindControl("lbt_Eit").Visible = false;
            this.Gv_BaleXinXi.Rows[e.NewEditIndex].FindControl("lbt_Up").Visible = true;
            this.Gv_BaleXinXi.Rows[e.NewEditIndex].FindControl("lbt_cancel").Visible = true;

        }

        protected void Gv_BaleXinXi_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Item item = (Item)Session["item"];
            string decimalPattern = @"^[0]{1}\.?[0-9]{0,2}|[1-9]+\.?[0-9]{0,2}$";
            string intPattern = @"^[1-9]+";
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
            if (!Regex.IsMatch(((TextBox)Gv_BaleXinXi.Rows[e.RowIndex].Cells[5].Controls[0]).Text.Trim(), decimalPattern))
            {
                Ok = false;
            }
            if (!Regex.IsMatch(((TextBox)Gv_BaleXinXi.Rows[e.RowIndex].Cells[6].Controls[0]).Text.Trim(), intPattern))
            {
                Ok = false;
            }

            if (Ok != false)
            {
                foreach (GridViewRow gvrow in Gv_BaleXinXi.Rows)
                {
                    item.Width = decimal.Parse(((TextBox)gvrow.FindControl("Txt_Weight") as TextBox).Text.Trim());
                }
                //updatedItem.Weight = decimal.Parse(((TextBox)Gv_BaleXinXi.Rows[e.RowIndex].Cells[1].Controls[0]).Text.Trim());
                item.Height = decimal.Parse(((TextBox)Gv_BaleXinXi.Rows[e.RowIndex].Cells[2].Controls[0]).Text.Trim());

                item.Length = decimal.Parse(((TextBox)Gv_BaleXinXi.Rows[e.RowIndex].Cells[4].Controls[0]).Text.Trim());
                item.TransPays = decimal.Parse(((TextBox)Gv_BaleXinXi.Rows[e.RowIndex].Cells[5].Controls[0]).Text.Trim());
                item.TransCurrency = int.Parse(((TextBox)Gv_BaleXinXi.Rows[e.RowIndex].Cells[6].Controls[0]).Text.Trim());
                Gv_BaleXinXi.EditIndex = -1;
                List<Item> lists = new List<Item>();
                lists.Add(item);
                Gv_BaleXinXi.DataSource = lists;
                Gv_BaleXinXi.DataBind();
                Session["item"] = item;
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
            Item item = (Item)Session["item"];
            lt.Add(item);
            Gv_BaleXinXi.DataSource = lt;
            Gv_BaleXinXi.DataBind();
            this.Gv_BaleXinXi.Rows[e.RowIndex].FindControl("lbt_Eit").Visible = true;
            this.Gv_BaleXinXi.Rows[e.RowIndex].FindControl("lbt_Up").Visible = false;
            this.Gv_BaleXinXi.Rows[e.RowIndex].FindControl("lbt_cancel").Visible = false;
        }

        protected void Gv_BaleXinXi_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            lt.Clear();
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('删除成功！')</script>");
            Gv_BaleXinXi.DataSource = lt;
            Gv_BaleXinXi.DataBind();
        }

        protected void But_Rurnet_Click(object sender, EventArgs e)
        {
            Response.Redirect("HAWBManagement.aspx");
        }
        public int N()
        {
            return n++;
        }
    }
}