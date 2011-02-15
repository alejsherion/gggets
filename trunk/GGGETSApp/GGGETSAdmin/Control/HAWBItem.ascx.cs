using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;

namespace GGGETSAdmin.Control
{
    public partial class HawbItem : System.Web.UI.UserControl
    {
        private static HAWB hawb = new HAWB();
        private static HAWBItem item;
        private static HAWBBox box;
        private static string type = string.Empty;
        private static string intPattern = @"^[1-9]*$";
        private static string decimalPattern = @"^[0]{1}\.?[0-9]{0,2}|[1-9]+\.?[0-9]{0,2}$";
        private IHAWBManagementService _hawbService;
        protected HawbItem()
        { }
        public HawbItem(IHAWBManagementService hawbService)
        {
            _hawbService = hawbService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["HAWB"] != null)
                {
                    hawb = (HAWB)Session["HAWB"];
                    if (Request.QueryString["type"] != "" && Request.QueryString["type"]!=null)
                    {
                        type = Request.QueryString["type"];
                        gv_Box.DataSource = hawb.HAWBBox;
                        gv_Box.DataBind();
                        GV_item.DataSource = hawb.HAWBItem;
                        GV_item.DataBind();
                    }
                    else
                    {
                        item = new HAWBItem();
                        box = new HAWBBox();
                        gv_item();
                        gv_box();
                    }
                    Evaluate();
                    ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
                }
                
            }
        }
        protected void gv_item()
        {
            List<HAWBItem> lt = new List<HAWBItem>();
            lt.Add(item);
            GV_item.DataSource = lt;
            GV_item.DataBind();
        }
        protected void gv_box()
        {
            List<HAWBBox> lt = new List<HAWBBox>();
            lt.Add(box);
            gv_Box.DataSource = lt;
            gv_Box.DataBind();
        }
        protected void but_AddItem_Click(object sender, EventArgs e)
        {
            if (type != "")
            {
                foreach (HAWBItem it in hawb.HAWBItem)
                {
                    item.ItemID = it.ItemID;
                    AddItem();
                }
            }
            else
            {
                item = new HAWBItem();
                item.ItemID = Guid.NewGuid();
                AddItem();
            }
        }
        protected void AddItem()
        {
            item.HID = hawb.HID;
            if (Txt_ItemPiece.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                Txt_ItemPiece.Focus();
            }
            else if (txt_ItemName.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                txt_ItemName.Focus();
            }
            else if (Txt_ItemPice.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                Txt_ItemPice.Focus();
            }
            else
            {
                if (!Regex.IsMatch(Txt_ItemPiece.Text, intPattern))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入整数！')</script>");
                    Txt_ItemPiece.Focus();
                }
                else if (!Regex.IsMatch(Txt_ItemPice.Text, decimalPattern))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能小数且小数点后保留2位！')</script>");
                    Txt_ItemPice.Focus();
                }
                else
                {
                    item.Name = txt_ItemName.Text;
                    item.Piece = int.Parse(Txt_ItemPiece.Text);
                    item.Remark = txt_ItemType.Text;
                    item.UnitAmount = decimal.Parse(Txt_ItemPice.Text);
                    item.TotalAmount += item.UnitAmount;
                    lbl_Piece.Text = hawb.Piece.ToString();
                    hawb.HAWBItem.Add(item);
                    GV_item.DataSource = hawb.HAWBItem;
                    GV_item.DataBind();
                }
            }
        }
        protected void but_AddBox_Click(object sender, EventArgs e)
        {
            if (type != "")
            {
                foreach (HAWBBox bx in hawb.HAWBBox)
                {
                    box.BoxID = bx.BoxID;
                    AddBox();
                }
            }
            else
            {
                box = new HAWBBox();
                box.BoxID = Guid.NewGuid();
                AddBox();
            }
        }
        protected void AddBox()
        {
            box.HID = hawb.HID;
            box.BoxType = int.Parse(rbt_BoxType.SelectedValue);
            if (Txt_BoxWeight.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                Txt_BoxWeight.Focus();
            }
            else if (txt_BoxPiece.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                txt_BoxPiece.Focus();
            }
            else
            {
                if (!Regex.IsMatch(txt_BoxPiece.Text, intPattern))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入整数！')</script>");
                    txt_BoxPiece.Focus();
                }
                else if (!Regex.IsMatch(Txt_BoxWeight.Text, decimalPattern))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能小数且小数点后保留2位！')</script>");
                    Txt_BoxWeight.Focus();
                }
                else if (!Regex.IsMatch(Txt_BoxHeight.Text, decimalPattern))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能小数且小数点后保留2位！')</script>");
                    Txt_BoxHeight.Focus();
                }
                else if (!Regex.IsMatch(Txt_BoxLength.Text, decimalPattern))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能小数且小数点后保留2位！')</script>");
                    Txt_BoxLength.Focus();
                }
                else if (!Regex.IsMatch(Txt_BoxWidth.Text, decimalPattern))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能小数且小数点后保留2位！')</script>");
                    Txt_BoxWidth.Focus();
                }
                else
                {
                    box.Height = decimal.Parse(Txt_BoxHeight.Text);
                    box.Length = decimal.Parse(Txt_BoxLength.Text);
                    box.Piece = int.Parse(txt_BoxPiece.Text);
                    box.Weight = decimal.Parse(Txt_BoxWeight.Text);
                    box.Width = decimal.Parse(Txt_BoxWidth.Text);
                    hawb.TotalWeight += box.Weight;
                    txt_VolumeWeight.Text = hawb.TotalWeight.ToString();
                    lbl_TotalVolume.Text = decimal.ToDouble(hawb.TotalWeight/166).ToString();
                    hawb.Piece += 1;
                    lbl_Piece.Text = hawb.Piece.ToString();
                    hawb.HAWBBox.Add(box);
                    gv_Box.DataSource = hawb.HAWBBox;
                    gv_Box.DataBind();
                }
            }
        }
        protected void But_Rurnet_Click(object sender, EventArgs e)
        {
            Response.Redirect((string)ViewState["UrlReferrer"]);
            
        }
        protected void Evaluate()
        {
            lbl_BarCode.Text = hawb.BarCode;
            lbl_ShipperCountry.Text = hawb.ShipperCountry;
            lbl_ShipperRegion.Text = hawb.ShipperRegion;
            lbl_ShipperName.Text = hawb.ShipperName;
            lbl_ConsigneeName.Text = hawb.ConsigneeName.ToString(); ;
            if (type != "")
            {
                rbt_BoxType.SelectedValue = box.BoxType.ToString();
                BoxType();
                rbt_BillTax.SelectedValue = hawb.BillTax.ToString();
                Rbl_SpecialInstruction.SelectedValue = hawb.SpecialInstruction.ToString();
                ddl_WeightType.SelectedValue = hawb.WeightType.ToString();
                txt_VolumeWeight.Text = hawb.VolumeWeight.ToString();
                lbl_Piece.Text = hawb.Piece.ToString();
                lbl_TotalVolume.Text = hawb.TotalVolume.ToString();
                txt_Remark.Text = hawb.Remark;
                Txt_Carrier.Text = hawb.Carrier;
                Txt_CarrierHAWBID.Text = hawb.CarrierHAWBID.ToString();
            }
        }

        protected void But_AddHAWB_Click(object sender, EventArgs e)
        {
            hawb.WeightType = int.Parse(ddl_WeightType.SelectedValue);
            if (hawb.TotalWeight == 0)
            {
                hawb.TotalVolume = 0;
            }
            else
            { 
                hawb.TotalVolume = hawb.TotalWeight / 166;
            }
            hawb.IsInternational = true;
            hawb.BillTax = int.Parse(rbt_BillTax.SelectedValue);
            hawb.SpecialInstruction = Rbl_SpecialInstruction.Text;
            hawb.Remark = txt_Remark.Text;
            hawb.Carrier = Txt_Carrier.Text;
            //hawb.CarrierHAWBID = Txt_CarrierHAWBID.Text;
            hawb.CarrierHAWBID = Guid.NewGuid();
            _hawbService.AddHAWB(hawb);
        }

        protected void rbt_BoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BoxType();
        }
        protected void BoxType()
        {
            if (rbt_BoxType.SelectedValue == "0")
            {
                txt_ItemName.Visible = false;
                txt_ItemType.Visible = false;
                Txt_ItemPice.Visible = false;
                Txt_BoxHeight.Visible = false;
                Txt_BoxLength.Visible = false;
                Txt_BoxWidth.Visible = false;
            }
            else
            {
                txt_ItemName.Visible = true;
                txt_ItemType.Visible = true;
                Txt_ItemPice.Visible = true;
                Txt_BoxHeight.Visible = true;
                Txt_BoxLength.Visible = true;
                Txt_BoxWidth.Visible = true;
            }
        }
    }
}