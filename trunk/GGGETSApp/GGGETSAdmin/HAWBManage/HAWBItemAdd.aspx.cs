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
        private HAWB hawb;
        private HAWBItem item;
        private HAWBBox box;
        private string type = string.Empty;
        private static string intPattern = @"^[1-9][0-9]*$";
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
            hawb = (HAWB)Session["HAWB"];

            if (!IsPostBack)
            {
                if (Session["HAWB"] != null)
                {

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
            if (Txt_ItemPiece.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('件数不能为空！')</script>");
                Txt_ItemPiece.Focus();
            }
            else if (Txt_ItemName.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('物品名称不能为空！')</script>");
                Txt_ItemName.Focus();
            }
            else if (Txt_ItemPice.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('物品价值不能为空！')</script>");
                Txt_ItemPice.Focus();
            }
            else
            {
                if (!Regex.IsMatch(Txt_ItemPiece.Text.Trim(), intPattern))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('件数只能输入非零的整数！')</script>");
                    Txt_ItemPiece.Focus();
                }
                else if (!Regex.IsMatch(Txt_ItemPice.Text.Trim(), decimalPattern))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('物品价值只能小数且小数点后保留2位！')</script>");
                    Txt_ItemPice.Focus();
                }
                else
                {
                    item.Name = Txt_ItemName.Text.Trim();
                    item.Piece = int.Parse(Txt_ItemPiece.Text.Trim());
                    item.Remark = Txt_ItemType.Text.Trim();
                    item.UnitAmount = decimal.Parse(Txt_ItemPice.Text.Trim());
                    if (rbt_BoxType.SelectedValue == "2")
                    {
                        hawb.HAWBItems.Add(item);
                        GV_item.DataSource = hawb.HAWBItems;
                        GV_item.DataBind();
                        txt_BoxPiece.Focus();
                    }
                    else
                    {
                        if (item.Piece * item.UnitAmount <= 100)
                        {
                            if (ReturnTotal(4) + item.Piece * item.UnitAmount <= 100)
                            {
                                hawb.HAWBItems.Add(item);
                                GV_item.DataSource = hawb.HAWBItems;
                                GV_item.DataBind();
                                Txt_ItemName.Text = string.Empty;
                                Txt_ItemPiece.Text = string.Empty;
                                Txt_ItemType.Text = string.Empty;
                                Txt_ItemPice.Text = string.Empty;
                                txt_BoxPiece.Focus();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('总计价值超过100美金不能添加！')</script>");
                                Txt_ItemPiece.Focus();
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('总计价值超过100美金不能添加！')</script>");
                            Txt_ItemPiece.Focus();
                        }
                    }
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
            if (Txt_BoxWeight.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('包裹重量不能为空！')</script>");
                Txt_BoxWeight.Focus();
            }
            else if (txt_BoxPiece.Text.Trim() == "" && txt_BoxPiece.Visible == false)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('包裹件数不能为空！')</script>");
                txt_BoxPiece.Focus();
            }
            else
            {
                if (!Regex.IsMatch(txt_BoxPiece.Text.Trim(), intPattern))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('包裹件数只能输入整数！')</script>");
                    txt_BoxPiece.Focus();
                }
                else if (!Regex.IsMatch(Txt_BoxWeight.Text.Trim(), decimalPattern))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('包裹重量只能输入整数或小数且小数点后保留2位！')</script>");
                    Txt_BoxWeight.Focus();
                }
                else if (!Regex.IsMatch(Txt_BoxHeight.Text.Trim(), decimalPattern) && Txt_BoxHeight.Visible != false && Txt_BoxHeight.Text.Trim() != "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('包裹高度只能输入整数或小数且小数点后保留2位！')</script>");
                    Txt_BoxHeight.Focus();
                }
                else if (!Regex.IsMatch(Txt_BoxLength.Text.Trim(), decimalPattern) && Txt_BoxLength.Visible != false && Txt_BoxHeight.Text.Trim() != "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('包裹长度只能输入整数或小数且小数点后保留2位！')</script>");
                    Txt_BoxLength.Focus();
                }
                else if (!Regex.IsMatch(Txt_BoxWidth.Text.Trim(), decimalPattern) && Txt_BoxWidth.Visible != false && Txt_BoxWidth.Text.Trim() != "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('包裹宽度只能输入整数或小数且小数点后保留2位！')</script>");
                    Txt_BoxWidth.Focus();
                }
                else
                {
                    if (Txt_BoxHeight.Text.Trim() != "")
                    {
                        box.Height = decimal.Parse(Txt_BoxHeight.Text.Trim());
                    }
                    if (Txt_BoxLength.Text.Trim() != "")
                    {
                        box.Length = decimal.Parse(Txt_BoxLength.Text.Trim());
                    }
                    if (Txt_BoxWidth.Text.Trim() != "")
                    {
                        box.Width = decimal.Parse(Txt_BoxWidth.Text.Trim());
                    }
                    box.Piece = int.Parse(txt_BoxPiece.Text.Trim());
                    box.Weight = decimal.Parse(Txt_BoxWeight.Text.Trim());

                    hawb.HAWBBoxes.Add(box);
                    txt_TotalWeight.Text = hawb.TotalWeight.ToString();
                    Txt_VolumeWeight.Text = hawb.VolumeWeight.Value.ToString();
                    lbl_Piece.Text = hawb.Piece.ToString();

                    gv_Box.DataSource = hawb.HAWBBoxes;
                    gv_Box.DataBind();
                    txt_BoxPiece.Focus();
                    Txt_BoxHeight.Text = string.Empty;
                    Txt_BoxLength.Text = string.Empty;
                    txt_BoxPiece.Text = string.Empty;
                    Txt_BoxWeight.Text = string.Empty;
                    Txt_BoxWidth.Text = string.Empty;
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
            lbl_ShipperRegion.Text = hawb.ShipperRegion;
            lbl_ConsigneeRegion.Text = hawb.ConsigneeRegion;
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
                if (hawb.SpecialInstruction != null)
                {
                    Rbl_SpecialInstruction.SelectedValue = hawb.SpecialInstruction;
                }
                ddl_WeightType.SelectedValue = hawb.WeightType.ToString();
                txt_TotalWeight.Text = hawb.TotalWeight.ToString();
                lbl_Piece.Text = hawb.Piece.ToString();
                Txt_VolumeWeight.Text = hawb.VolumeWeight.Value.ToString();
                txt_Remark.Text = hawb.Remark;
                Txt_Carrier.Text = hawb.Carrier;
                Txt_CarrierHAWBBarCode.Text = hawb.CarrierHAWBBarCode;
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
                hawb.TotalVolume = decimal.Parse(txt_TotalWeight.Text.Trim());
            }
            hawb.IsInternational = true;
            if (Txt_CarrierHAWBBarCode.Text.Trim() != null)
            {
                hawb.CarrierHAWBID = Guid.NewGuid();
                hawb.CarrierHAWBBarCode = Txt_CarrierHAWBBarCode.Text.Trim().ToUpper();
            }
            hawb.BillTax = int.Parse(rbt_BillTax.SelectedValue);
            hawb.SpecialInstruction = Rbl_SpecialInstruction.Text.Trim();
            hawb.Remark = txt_Remark.Text.Trim().ToUpper();
            hawb.Carrier = Txt_Carrier.Text.Trim().ToUpper();
            hawb.ServiceType = int.Parse(rbt_BoxType.SelectedValue);
            if (hawb.HAWBBoxes.Count != 0)
            {
                if (hawb.HAWBItems.Count == 0)
                {
                    item = new HAWBItem();
                    item.ItemID = Guid.NewGuid();
                    item.HID = hawb.HID;
                    item.Name = Txt_ItemName.Text.Trim();
                    item.Piece = 0;
                    item.Remark = string.Empty;
                    item.UnitAmount = decimal.Parse("0.00");
                    hawb.HAWBItems.Add(item);
                }
                if (ViewState["type"] != null)
                {
                    type = ViewState["type"].ToString(); ;
                }
                if (type == "Amend")
                {
                    _hawbService.ChangeHAWB(hawb);
                    Response.Write("<script>alert('修改成功！');location='HAWBManagement.aspx'</script>");
                    Session.Clear();
                }
                else
                {
                    _hawbService.AddHAWB(hawb);
                    Response.Write("<script>alert('添加成功！');location='HAWBAdd.aspx'</script>");
                    //Session["HAWB"] = null;
                    Session.Remove("HAWB");
                }


            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请添加包裹！')</script>");
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
                Txt_ItemName.Text = "Documents";
                Txt_ItemType.Text = string.Empty;
                Txt_ItemPice.Text = string.Empty;
                Txt_ItemPiece.Text = string.Empty;
                Txt_BoxHeight.Text = string.Empty;
                Txt_BoxLength.Text = string.Empty;
                Txt_BoxWidth.Text = string.Empty;

                GV_item.DataSource = null;
                GV_item.DataBind();

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
                Txt_ItemName.Text = string.Empty;
                Txt_ItemType.Text = string.Empty;
                Txt_ItemPice.Text = string.Empty;
                Txt_ItemPiece.Text = string.Empty;
                Txt_BoxHeight.Text = string.Empty;
                Txt_BoxLength.Text = string.Empty;
                Txt_BoxWidth.Text = string.Empty;
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

            if (((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxPiece") as TextBox).Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('包裹件数不能为空！')</script>");
                ((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxPiece") as TextBox).Focus();
            }
            else if (((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxWeight") as TextBox).Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('包裹重量不能为空！')</script>");
                ((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxWeight") as TextBox).Focus();
            }
            else
            {
                if (!Regex.IsMatch(((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxPiece") as TextBox).Text.Trim(), intPattern))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入整数！')</script>");
                    ((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxPiece") as TextBox).Focus();
                }
                else if (!Regex.IsMatch(((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxWeight") as TextBox).Text.Trim(), decimalPattern))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能小数且小数点后保留2位！')</script>");
                    ((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxWeight") as TextBox).Focus();
                }
                else if (!Regex.IsMatch(((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxHeight") as TextBox).Text.Trim(), decimalPattern) && ((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxHeight") as TextBox).Text.Trim() != "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能小数且小数点后保留2位！')</script>");
                    ((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxHeight") as TextBox).Focus();
                }
                else if (!Regex.IsMatch(((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxLength") as TextBox).Text.Trim(), decimalPattern) && ((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxLength") as TextBox).Text.Trim() != "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能小数且小数点后保留2位！')</script>");
                    ((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxLength") as TextBox).Focus();
                }
                else if (!Regex.IsMatch(((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxWidth") as TextBox).Text.Trim(), decimalPattern) && ((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxWidth") as TextBox).Text.Trim() != "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能小数且小数点后保留2位！')</script>");
                    ((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxWidth") as TextBox).Focus();
                }
                else
                {
                    if (((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxHeight") as TextBox).Text.Trim() != "")
                    {
                        box.Height = decimal.Parse(((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxHeight") as TextBox).Text.Trim());
                    }
                    if (((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxLength") as TextBox).Text.Trim() != "")
                    {
                        box.Length = decimal.Parse(((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxLength") as TextBox).Text.Trim());

                    }
                    box.Piece = int.Parse(((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxPiece") as TextBox).Text.Trim());
                    if (((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxWeight") as TextBox).Text.Trim() != "")
                    {
                        box.Weight = decimal.Parse(((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxWeight") as TextBox).Text.Trim());
                    }
                    if (((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxWidth") as TextBox).Text.Trim() != "")
                    {
                        box.Width = decimal.Parse(((TextBox)gv_Box.Rows[e.RowIndex].FindControl("txt_BoxWidth") as TextBox).Text.Trim());
                    }
                    hawb.HAWBBoxes.Add(box);
                    Txt_VolumeWeight.Text = hawb.VolumeWeight.Value.ToString();
                    txt_TotalWeight.Text = hawb.TotalWeight.ToString();
                    lbl_Piece.Text = hawb.Piece.ToString();
                    gv_Box.EditIndex = -1;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('更新成功！')</script>");
                    gv_Box.DataSource = hawb.HAWBBoxes;
                    gv_Box.DataBind();
                }
            }

        }

        protected void gv_Box_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int inex = Convert.ToInt16(e.CommandArgument);
                Guid id = Guid.Parse(gv_Box.DataKeys[inex].Value.ToString());
                foreach (HAWBBox bx in hawb.HAWBBoxes)
                {
                    if (bx.BoxID == id)
                    {
                        box = bx;

                    }
                }
                hawb.HAWBBoxes.Remove(box);
                txt_TotalWeight.Text = hawb.TotalWeight.ToString();
                Txt_VolumeWeight.Text = hawb.VolumeWeight.Value.ToString();
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
            if (((TextBox)GV_item.Rows[e.RowIndex].FindControl("txt_ItemPiece") as TextBox).Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                ((TextBox)GV_item.Rows[e.RowIndex].FindControl("txt_ItemPiece") as TextBox).Focus();
            }
            else if (((TextBox)GV_item.Rows[e.RowIndex].FindControl("txt_ItemName") as TextBox).Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                ((TextBox)GV_item.Rows[e.RowIndex].FindControl("txt_ItemName") as TextBox).Focus();
            }
            else if (((TextBox)GV_item.Rows[e.RowIndex].FindControl("txt_ItemPice") as TextBox).Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
                ((TextBox)GV_item.Rows[e.RowIndex].FindControl("txt_ItemPice") as TextBox).Focus();
            }
            else
            {
                if (!Regex.IsMatch(((TextBox)GV_item.Rows[e.RowIndex].FindControl("txt_ItemPiece") as TextBox).Text.Trim(), intPattern))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入整数！')</script>");
                    ((TextBox)GV_item.Rows[e.RowIndex].FindControl("txt_ItemPiece") as TextBox).Focus();
                }
                else if (!Regex.IsMatch(((TextBox)GV_item.Rows[e.RowIndex].FindControl("txt_ItemPice") as TextBox).Text.Trim(), decimalPattern))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能小数且小数点后保留2位！')</script>");
                    ((TextBox)GV_item.Rows[e.RowIndex].FindControl("txt_ItemPice") as TextBox).Focus();
                }
                else if (int.Parse(((TextBox)GV_item.Rows[e.RowIndex].FindControl("txt_ItemPiece") as TextBox).Text.Trim()) * decimal.Parse(((TextBox)GV_item.Rows[e.RowIndex].FindControl("txt_ItemPice") as TextBox).Text.Trim()) > 100)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('总计价值超过100美金不能添加！')</script>");
                    ((TextBox)GV_item.Rows[e.RowIndex].FindControl("txt_ItemPiece") as TextBox).Focus();
                }
                else if (int.Parse(((TextBox)GV_item.Rows[e.RowIndex].FindControl("txt_ItemPiece") as TextBox).Text) * decimal.Parse(((TextBox)GV_item.Rows[e.RowIndex].FindControl("txt_ItemPice") as TextBox).Text) + ReturnTotal(4) > 100 && GV_item.Rows.Count > 1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('总计价值超过100美金不能添加！')</script>");
                    ((TextBox)GV_item.Rows[e.RowIndex].FindControl("txt_ItemPiece") as TextBox).Focus();
                }
                else
                {
                    item.Name = ((TextBox)GV_item.Rows[e.RowIndex].FindControl("txt_ItemName") as TextBox).Text.Trim();
                    item.Piece = int.Parse(((TextBox)GV_item.Rows[e.RowIndex].FindControl("txt_ItemPiece") as TextBox).Text.Trim());
                    item.Remark = ((TextBox)GV_item.Rows[e.RowIndex].FindControl("txt_ItemType") as TextBox).Text.Trim();
                    item.UnitAmount = decimal.Parse(((TextBox)GV_item.Rows[e.RowIndex].FindControl("txt_ItemPice") as TextBox).Text.Trim());
                    hawb.HAWBItems.Add(item);
                    GV_item.EditIndex = -1;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('更新成功！')</script>");
                    GV_item.DataSource = hawb.HAWBItems;
                    GV_item.DataBind();
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

        protected void GV_item_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (GV_item.Rows.Count != 0)
            {
                if (e.Row.RowType == DataControlRowType.Footer)//判断此行是否是页尾，如果是则开始统计数据
                {

                    e.Row.Cells[0].Text = "合计";//每一列的数

                    e.Row.Cells[4].Text = ReturnTotal(4).ToString();//第六列合计值
                }

            }

        }
        public decimal ReturnTotal(int col)   //根据col变量（col值代表某一列，值由GridView1_RowDataBound传入）计算某列的合计值
        {

            decimal char_total = 0;

            foreach (GridViewRow gvr in GV_item.Rows)
            {

                if (((Label)gvr.FindControl("lbl_SumPice") as Label).Text != "")
                {

                    char_total += Convert.ToDecimal(((Label)gvr.FindControl("lbl_SumPice") as Label).Text.Trim());

                }

            }

            return decimal.Round(char_total, 2);

        }


    }
}