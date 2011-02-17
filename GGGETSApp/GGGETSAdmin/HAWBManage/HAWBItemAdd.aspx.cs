using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;
using System.Data;

namespace GGGETSAdmin.HAWBManage
{
    public partial class HAWBItemAdd : System.Web.UI.Page
    {
        private static HAWB hawb;
        private static HAWBItem item;
        private static HAWBBox box;
        private string type = string.Empty;
        private static string intPattern = @"^[1-9]*$";
        private static string decimalPattern = @"^[0]{1}\.?[0-9]{0,2}|[1-9]+\.?[0-9]{0,2}$";
        private IHAWBManagementService _hawbService;
        protected HAWBItemAdd()
        { }
        public HAWBItemAdd(IHAWBManagementService hawbservice)
        {
            _hawbService = hawbservice;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["HAWB"] != null)
                {
                    hawb = (HAWB)Session["HAWB"];
                    if (Request.QueryString["type"] == "Amend")
                    {
                        type = Request.QueryString["type"];
                        ViewState["type"] = type;
                        But_AddHAWB.Text = "保 存";
                        gv_Box.DataSource = hawb.HAWBBoxes;
                        gv_Box.DataBind();
                        GV_item.DataSource = hawb.HAWBItems;
                        GV_item.DataBind();
                    }
                    else
                    {
                        item = new HAWBItem();
                        box = new HAWBBox();
                        gv_item();
                        gv_box();
                        But_AddHAWB.Text = "创 建";
                    }
                    Evaluate();
                    BoxType();
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
            item = new HAWBItem();
            item.ItemID = Guid.NewGuid();
            AddItem();
        }
        protected void AddItem()
        {
            item.HID = hawb.HID;
            if (Txt_ItemPiece.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                Txt_ItemPiece.Focus();
            }
            else if (Txt_ItemName.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                Txt_ItemName.Focus();
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
                    item.Name = Txt_ItemName.Text;
                    item.Piece = int.Parse(Txt_ItemPiece.Text);
                    item.Remark = Txt_ItemType.Text;
                    item.UnitAmount = decimal.Parse(Txt_ItemPice.Text);
                    item.TotalAmount += item.UnitAmount;
                    
                    hawb.HAWBItems.Add(item);
                    GV_item.DataSource = hawb.HAWBItems;
                    GV_item.DataBind();
                }
            }
        }
        protected void but_AddBox_Click(object sender, EventArgs e)
        {
            box = new HAWBBox();
            box.BoxID = Guid.NewGuid();
            AddBox();
        }
        protected void AddBox()
        {
            
            box.BoxType = int.Parse(rbt_BoxType.SelectedValue);
            if (Txt_BoxWeight.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                Txt_BoxWeight.Focus();
            }
            else if (txt_BoxPiece.Text == "" && txt_BoxPiece.Visible==false)
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
                else if (!Regex.IsMatch(Txt_BoxHeight.Text, decimalPattern) && Txt_BoxHeight.Visible==false)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能小数且小数点后保留2位！')</script>");
                    Txt_BoxHeight.Focus();
                }
                else if (!Regex.IsMatch(Txt_BoxLength.Text, decimalPattern) && Txt_BoxLength.Visible == false)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能小数且小数点后保留2位！')</script>");
                    Txt_BoxLength.Focus();
                }
                else if (!Regex.IsMatch(Txt_BoxWidth.Text, decimalPattern) && Txt_BoxWidth.Visible==false)
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
                    hawb.HAWBBoxes.Add(box);
                    txt_TotalWeight.Text = hawb.TotalWeight.ToString();
                    lbl_TotalVolume.Text = hawb.TotalVolume.ToString();
                    lbl_Piece.Text = hawb.Piece.ToString();
                   
                    gv_Box.DataSource = hawb.HAWBBoxes;
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
            lbl_ConsigneeName.Text = hawb.ConsigneeName.ToString();
            if (ViewState["type"] != null)
            {
                type = ViewState["type"].ToString(); ;
            }
            if (type == "Amend")
            {
                foreach (HAWBBox box in hawb.HAWBBoxes)
                {
                    rbt_BoxType.SelectedValue = box.BoxType.ToString();
                }
                BoxType();
                rbt_BillTax.SelectedValue = hawb.BillTax.ToString();
                Rbl_SpecialInstruction.SelectedValue = hawb.SpecialInstruction.ToString();
                ddl_WeightType.SelectedValue = hawb.WeightType.ToString();
                txt_TotalWeight.Text = hawb.TotalWeight.ToString();
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
            if (txt_TotalWeight.Text == "")
            {
                hawb.TotalVolume = 0;
            }
            else
            {
                hawb.TotalVolume = decimal.Parse(txt_TotalWeight.Text);
            }
            hawb.IsInternational = true;
            hawb.BillTax = int.Parse(rbt_BillTax.SelectedValue);
            hawb.SpecialInstruction = Rbl_SpecialInstruction.Text;
            hawb.Remark = txt_Remark.Text;
            hawb.Carrier = Txt_Carrier.Text;
            hawb.ServiceType = int.Parse(rbt_BoxType.SelectedValue);
            //hawb.CarrierHAWBID = Guid.Parse(Txt_CarrierHAWBID.Text);
            if (hawb.HAWBBoxes.Count != 0 && hawb.HAWBItems.Count != 0)
            {
                if (ViewState["type"] != null)
                {
                    type = ViewState["type"].ToString(); ;
                }
                if (type == "Amend")
                {
                    ViewState["type"] = null;
                    _hawbService.ChangeHAWB(hawb);
                    Response.Write("<script>alert('修改成功！');location='HAWBManagement.aspx'</script>");
                    Session.Clear();
                }
                else
                { 
                    _hawbService.AddHAWB(hawb);
                    Response.Write("<script>alert('添加成功！');location='HAWBManagement.aspx'</script>");
                    Session.Clear();
                }
                
                
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请添加包裹和物品！')</script>");
            }
        }

        protected void rbt_BoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BoxType();
        }
        protected void BoxType()
        {
            if (rbt_BoxType.SelectedValue == "0")
            {
                Txt_ItemName.Enabled = false;
                Txt_ItemType.Enabled = false;
                Txt_ItemPice.Enabled = false;
                Txt_ItemPiece.Enabled = false;
                but_AddItem.Enabled = false;
                GV_item.Enabled = false;
                Txt_BoxHeight.Enabled = false;
                Txt_BoxLength.Enabled = false;
                Txt_BoxWidth.Enabled = false;
            }
            else
            {
                Txt_ItemName.Enabled = true;
                Txt_ItemType.Enabled = true;
                Txt_ItemPice.Enabled = true;
                Txt_ItemPiece.Enabled = true;
                but_AddItem.Enabled = true;
                GV_item.Enabled = true;
                Txt_BoxHeight.Enabled = true;
                Txt_BoxLength.Enabled = true;
                Txt_BoxWidth.Enabled = true;
            }
        }
        protected void gv_Box_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_Box.EditIndex = e.NewEditIndex;
            gv_Box.DataSource = hawb.HAWBBoxes;
            gv_Box.DataBind();
        }

        protected void gv_Box_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int inex = Convert.ToInt16(e.RowIndex);
            Guid id = Guid.Parse(gv_Box.DataKeys[inex].Value.ToString());
            foreach (HAWBBox bx in hawb.HAWBBoxes)
            {
                if (bx.BoxID == id)
                {
                    box = bx;
                }
            }
            foreach (GridViewRow itemrow in gv_Box.Rows)
            {
                if (((TextBox)itemrow.FindControl("txt_BoxPiece") as TextBox).Text == "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                    ((TextBox)itemrow.FindControl("txt_BoxPiece") as TextBox).Focus();
                }
                else if (((TextBox)itemrow.FindControl("txt_BoxWeight") as TextBox).Text == "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                    ((TextBox)itemrow.FindControl("txt_BoxWeight") as TextBox).Focus();
                }
                else
                {
                    if (!Regex.IsMatch(((TextBox)itemrow.FindControl("txt_BoxPiece") as TextBox).Text, intPattern))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入整数！')</script>");
                        ((TextBox)itemrow.FindControl("txt_BoxPiece") as TextBox).Focus();
                    }
                    else if (!Regex.IsMatch(((TextBox)itemrow.FindControl("txt_BoxWeight") as TextBox).Text, decimalPattern))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能小数且小数点后保留2位！')</script>");
                        ((TextBox)itemrow.FindControl("txt_BoxWeight") as TextBox).Focus();
                    }
                    else if (!Regex.IsMatch(((TextBox)itemrow.FindControl("txt_BoxHeight") as TextBox).Text, decimalPattern) && ((TextBox)gv_Box.FindControl("txt_BoxHeight") as TextBox).Text != "")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能小数且小数点后保留2位！')</script>");
                        ((TextBox)itemrow.FindControl("txt_BoxHeight") as TextBox).Focus();
                    }
                    else if (!Regex.IsMatch(((TextBox)itemrow.FindControl("txt_BoxLength") as TextBox).Text, decimalPattern) && ((TextBox)gv_Box.FindControl("txt_BoxLength") as TextBox).Text != "")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能小数且小数点后保留2位！')</script>");
                        ((TextBox)itemrow.FindControl("txt_BoxLength") as TextBox).Focus();
                    }
                    else if (!Regex.IsMatch(((TextBox)itemrow.FindControl("txt_BoxWidth") as TextBox).Text, decimalPattern) && ((TextBox)gv_Box.FindControl("txt_BoxWidth") as TextBox).Text != "")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能小数且小数点后保留2位！')</script>");
                        ((TextBox)itemrow.FindControl("txt_BoxWidth") as TextBox).Focus();
                    }
                    else
                    {
                        box.Height = decimal.Parse(((TextBox)itemrow.FindControl("txt_BoxHeight") as TextBox).Text.Trim());
                        box.Length = decimal.Parse(((TextBox)itemrow.FindControl("txt_BoxLength") as TextBox).Text);
                        box.Piece = int.Parse(((TextBox)itemrow.FindControl("txt_BoxPiece") as TextBox).Text);
                        box.Weight = decimal.Parse(((TextBox)itemrow.FindControl("txt_BoxWeight") as TextBox).Text);
                        box.Width = decimal.Parse(((TextBox)itemrow.FindControl("txt_BoxWidth") as TextBox).Text);
                        hawb.HAWBBoxes.Add(box);
                        lbl_TotalVolume.Text = hawb.TotalVolume.ToString();
                        lbl_Piece.Text = hawb.Piece.ToString();
                        gv_Box.EditIndex = -1;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('更新成功！')</script>");
                        gv_Box.DataSource = hawb.HAWBBoxes;
                        gv_Box.DataBind();
                    }
                }
            }
        }

        protected void gv_Box_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int inex=Convert.ToInt16(e.CommandArgument);
                Guid id=Guid.Parse(gv_Box.DataKeys[inex].Value.ToString());
                foreach (HAWBBox bx in hawb.HAWBBoxes)
                {
                    if (bx.BoxID == id)
                    {
                        box = bx;
                        
                    }
                }
                hawb.HAWBBoxes.Remove(box);
                txt_TotalWeight.Text = hawb.TotalWeight.ToString();
                lbl_TotalVolume.Text = hawb.TotalVolume.ToString();
                lbl_Piece.Text = hawb.Piece.ToString();
                gv_Box.DataSource = hawb.HAWBBoxes;
                gv_Box.DataBind();
            }
        }

        protected void gv_Box_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gv_Box_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_Box.EditIndex = -1;
            gv_Box.DataSource = hawb.HAWBBoxes;
            gv_Box.DataBind();
        }

        protected void GV_item_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GV_item.EditIndex = e.NewEditIndex;
            GV_item.DataSource = hawb.HAWBItems;
            GV_item.DataBind();
        }

        protected void GV_item_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void GV_item_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int inex = Convert.ToInt16(e.RowIndex);
            Guid id = Guid.Parse(GV_item.DataKeys[inex].Value.ToString());
            foreach (HAWBItem it in hawb.HAWBItems)
            {
                if (it.ItemID == id)
                {
                    item = it;
                }
            }
            foreach (GridViewRow itemrow in GV_item.Rows)
            {
                if (((TextBox)itemrow.FindControl("txt_ItemPiece") as TextBox).Text == "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                    ((TextBox)itemrow.FindControl("txt_ItemPiece") as TextBox).Focus();
                }
                else if (((TextBox)itemrow.FindControl("txt_ItemName") as TextBox).Text == "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                    ((TextBox)itemrow.FindControl("txt_ItemName") as TextBox).Focus();
                }
                else if (((TextBox)itemrow.FindControl("txt_ItemPice") as TextBox).Text == "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                    ((TextBox)itemrow.FindControl("txt_ItemPice") as TextBox).Focus();
                }
                else
                {
                    if (!Regex.IsMatch(((TextBox)itemrow.FindControl("txt_ItemPiece") as TextBox).Text, intPattern))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入整数！')</script>");
                        ((TextBox)itemrow.FindControl("txt_ItemPiece") as TextBox).Focus();
                    }
                    else if (!Regex.IsMatch(((TextBox)itemrow.FindControl("txt_ItemName") as TextBox).Text, decimalPattern))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能小数且小数点后保留2位！')</script>");
                        ((TextBox)itemrow.FindControl("txt_ItemName") as TextBox).Focus();
                    }
                    else if (!Regex.IsMatch(((TextBox)itemrow.FindControl("txt_ItemPice") as TextBox).Text, decimalPattern))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能小数且小数点后保留2位！')</script>");
                        ((TextBox)itemrow.FindControl("txt_ItemPice") as TextBox).Focus();
                    }
                    else
                    {
                        item.Name = ((TextBox)itemrow.FindControl("txt_ItemName") as TextBox).Text.Trim();
                        item.Piece = int.Parse(((TextBox)itemrow.FindControl("txt_ItemPiece") as TextBox).Text);
                        item.Remark = ((TextBox)itemrow.FindControl("txt_ItemType") as TextBox).Text;
                        item.UnitAmount = decimal.Parse(((TextBox)itemrow.FindControl("txt_ItemPice") as TextBox).Text);
                        item.TotalAmount += item.UnitAmount;
                        hawb.HAWBItems.Add(item);
                        GV_item.EditIndex = -1;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('更新成功！')</script>");
                        GV_item.DataSource = hawb.HAWBItems;
                        GV_item.DataBind();
                    }
                }
            }
        }

        protected void GV_item_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int inex = Convert.ToInt16(e.CommandArgument);
                Guid id = Guid.Parse(GV_item.DataKeys[inex].Value.ToString());
                foreach (HAWBItem it in hawb.HAWBItems)
                {
                    if (it.ItemID == id)
                    {
                        item = it;
                    }
                }
                hawb.HAWBItems.Remove(item);
                GV_item.DataSource = hawb.HAWBItems;
                GV_item.DataBind();
            }
        }

        protected void GV_item_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GV_item.EditIndex = -1;
            GV_item.DataSource = hawb.HAWBItems;
            GV_item.DataBind();
        }

        
    }
}