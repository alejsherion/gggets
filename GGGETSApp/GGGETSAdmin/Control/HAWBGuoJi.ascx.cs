using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;

namespace GGGETSAdmin.Control
{
    public partial class HAWBGuoJi : System.Web.UI.UserControl
    {
        protected static Regex RZipCode = new Regex(@"^\d{6}$");
        protected static Regex RTel = new Regex(@"^(\d{3,4}-)?\d{7,8}$");
        protected static Regex RTel1 = new Regex(@"^1[35]\d{9}$");
        protected static Regex RCountry = new Regex(@"^[A-Za-z]{2}$");
        protected static Regex RRegion = new Regex(@"^[A-Za-z]{3}$");
        protected static HAWB hawb;
        protected static string BarCode=string.Empty;
        protected IHAWBManagementService _hawbService;
        protected HAWBGuoJi()
        { }
        public HAWBGuoJi(IHAWBManagementService hawbService)
        {
            _hawbService = hawbService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["BarCode"] != null)
                {
                    BarCode = Request.QueryString["BarCode"].ToString();
                    hawb = _hawbService.FindHAWBByBarCode(BarCode);
                    if (hawb.DeliverName != "")
                    {
                        this.Deliver.Visible = true;
                    }
                    Evaluate();
                    Session["HAWB"] = hawb;
                }
                else
                {
                    if (Session["HAWB"] != null)
                    {
                        hawb = (HAWB)Session["HAWB"];
                        if (hawb.DeliverName != "")
                        {
                            this.Deliver.Visible = true;
                        }
                        Evaluate();
                    }
                    else
                    {
                        hawb = new HAWB();
                    }
                }
            }

        }
                
        private void Storage()
        {
            if (BarCode == string.Empty)
            {
                hawb.HID = Guid.NewGuid();
                hawb.BarCode = Txt_BarCode.Text;
                hawb.UID = Guid.NewGuid();
                hawb.SettleType = int.Parse(DDl_SettleType.SelectedValue);

                hawb.ShipperName = Txt_ShipperName.Text;
                hawb.ShipperContactor = Txt_ShipperContactor.Text;
                hawb.ShipperCountry = Txt_ShipperCountry.Text;
                hawb.ShipperRegion = Txt_ShipperRegion.Text;
                hawb.ShipperAddress = Txt_ShipperAddress.Text;
                hawb.ShipperTel = Txt_ShipperTel.Text;
                hawb.ShipperZipCode = Txt_ShipperZipCode.Text;

                //hawb.ConsigneeName = Txt_ConsigneeName.Text;
                hawb.ConsigneeContactor = Txt_ConsigneeContactor.Text;
                hawb.ConsigneeCountry = Txt_ConsigneeCountry.Text;
                hawb.ConsigneeRegion = Txt_ConsigneeRegion.Text;
                hawb.ConsigneeAddress = Txt_ConsigneeAddress.Text;
                hawb.ConsigneeTel = Txt_ConsigneeTel.Text;
                hawb.ConsigneeZipCode = Txt_ConsigneeZipCode.Text;
                hawb.CreateTime = DateTime.Now;

                hawb.DeliverName = Txt_DeliverName.Text;
                hawb.DeliverAddress = Txt_DeliverAddress.Text;
                hawb.DeliverCountry = Txt_DeliverCountry.Text;
                hawb.DeliverRegion = Txt_DeliverRegion.Text;
                hawb.DeliverContactor = Txt_DeliverContactor.Text;
                hawb.DeliverZipCode = Txt_DeliverZipCode.Text;
                hawb.DeliverTel = Txt_DeliverTel.Text;
                Session["HAWB"] = hawb;
            }
            else
            {
                hawb.BarCode = Txt_BarCode.Text;
                hawb.SettleType = int.Parse(DDl_SettleType.SelectedValue);

                hawb.ShipperName = Txt_ShipperName.Text;
                hawb.ShipperContactor = Txt_ShipperContactor.Text;
                hawb.ShipperCountry = Txt_ShipperCountry.Text;
                hawb.ShipperRegion = Txt_ShipperRegion.Text;
                hawb.ShipperAddress = Txt_ShipperAddress.Text;
                hawb.ShipperTel = Txt_ShipperTel.Text;
                hawb.ShipperZipCode = Txt_ShipperZipCode.Text;

                //hawb.ConsigneeName = Txt_ConsigneeName.Text;
                hawb.ConsigneeContactor = Txt_ConsigneeContactor.Text;
                hawb.ConsigneeCountry = Txt_ConsigneeCountry.Text;
                hawb.ConsigneeRegion = Txt_ConsigneeRegion.Text;
                hawb.ConsigneeAddress = Txt_ConsigneeAddress.Text;
                hawb.ConsigneeTel = Txt_ConsigneeTel.Text;
                hawb.ConsigneeZipCode = Txt_ConsigneeZipCode.Text;
                hawb.UpdateTime = DateTime.Now;

                hawb.DeliverName = Txt_DeliverName.Text;
                hawb.DeliverAddress = Txt_DeliverAddress.Text;
                hawb.DeliverCountry = Txt_DeliverCountry.Text;
                hawb.DeliverRegion = Txt_DeliverRegion.Text;
                hawb.DeliverContactor = Txt_DeliverContactor.Text;
                hawb.DeliverZipCode = Txt_DeliverZipCode.Text;
                hawb.DeliverTel = Txt_DeliverTel.Text;
                Session["HAWB"] = hawb;
            }
        }
        protected void Evaluate()
        {
            Txt_BarCode.Text = hawb.BarCode;
            DDl_SettleType.SelectedValue = hawb.SettleType.ToString(); 

            Txt_ShipperName.Text = hawb.ShipperName;
            Txt_ShipperContactor.Text = hawb.ShipperContactor;
            Txt_ShipperCountry.Text = hawb.ShipperCountry;
            Txt_ShipperRegion.Text = hawb.ShipperRegion;
            Txt_ShipperAddress.Text = hawb.ShipperAddress;
            Txt_ShipperTel.Text = hawb.ShipperTel;
            Txt_ShipperZipCode.Text = hawb.ShipperZipCode;

            //Txt_ConsigneeName.Text = hawb.ConsigneeName;
            Txt_ConsigneeContactor.Text = hawb.ConsigneeContactor;
            Txt_ConsigneeCountry.Text = hawb.ConsigneeCountry;
            Txt_ConsigneeRegion.Text = hawb.ConsigneeRegion;
            Txt_ConsigneeAddress.Text = hawb.ConsigneeAddress;
            Txt_ConsigneeTel.Text = hawb.ConsigneeTel;
            Txt_ConsigneeZipCode.Text = hawb.ConsigneeZipCode;

            Txt_DeliverName.Text = hawb.DeliverName;
            Txt_DeliverAddress.Text = hawb.DeliverAddress;
            Txt_DeliverCountry.Text = hawb.DeliverCountry;
            Txt_DeliverRegion.Text = hawb.DeliverRegion;
            Txt_DeliverContactor.Text = hawb.DeliverContactor;
            Txt_DeliverZipCode.Text = hawb.DeliverZipCode;
            Txt_DeliverTel.Text = hawb.DeliverTel;
        }
        private void InitialControl(ControlCollection objControlCollection)
        {
            foreach (System.Web.UI.Control objControl in objControlCollection)
            {
                if (objControl.HasControls())
                {
                    InitialControl(objControl.Controls);
                }
                else
                {
                    if (objControl is System.Web.UI.WebControls.TextBox)
                    {
                        ((TextBox)objControl).Text = String.Empty;
                    }
                }
            }
        }

        protected void lbtn_AddConsignee_Click(object sender, EventArgs e)
        {
            Storage();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript","<script>Open();</script>");
        }

        protected void But_Next_Click(object sender, EventArgs e)
        {
            if (Deliver.Visible == true)
            {
                if (Txt_BarCode.Text == "")
                {
                    Script(Txt_BarCode.Text);
                    Txt_BarCode.Focus();
                }
                else if (Txt_Account1.Text == "")
                {
                    Script(Txt_Account1.Text);
                    Txt_Account1.Focus();
                }
                else if (Txt_ShipperName.Text == "")
                {
                    Namejudge(Txt_ShipperName);
                }
                else if (Txt_ShipperAddress.Text == "")
                {
                    Addressjudge(Txt_ShipperAddress);
                }
                else if (Txt_ShipperContactor.Text == "")
                {
                    Contactorjudge(Txt_ShipperContactor);
                }
                else if (Txt_ShipperTel.Text == "")
                {
                    Teljudge(Txt_ShipperTel);
                }
                //else if (Txt_ShipperName.Text == "")
                //{
                //    Script(Txt_ShipperName.Text);
                //    Txt_ConsigneeName.Focus();
                //}
                else if (Txt_ConsigneeAddress.Text == "")
                {
                    Addressjudge(Txt_ConsigneeAddress);
                }
                else if (Txt_ConsigneeContactor.Text == "")
                {
                    Contactorjudge(Txt_ConsigneeContactor);
                }
                else if (Txt_ConsigneeTel.Text == "")
                {
                    Teljudge(Txt_ConsigneeTel);
                }
                else if (Txt_DeliverName.Text == "")
                {
                    Namejudge(Txt_DeliverName);
                }
                else if (Txt_DeliverAddress.Text == "")
                {
                    Addressjudge(Txt_DeliverAddress);
                }
                else if (Txt_DeliverContactor.Text == "")
                {
                    Contactorjudge(Txt_DeliverContactor);
                }
                else if (Txt_DeliverTel.Text == "")
                {
                    Teljudge(Txt_DeliverTel);
                }
                else
                {

                    if (!RTel.IsMatch(Txt_ShipperTel.Text) && !RTel1.IsMatch(Txt_ShipperTel.Text))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('电话号码格式不正确！')</script>");
                        Txt_ShipperTel.Focus();
                    }
                    else if (!RTel.IsMatch(Txt_ConsigneeTel.Text) && !RTel1.IsMatch(Txt_ConsigneeTel.Text))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('电话号码格式不正确！')</script>");
                        Txt_ConsigneeTel.Focus();
                    }
                    else if (!RTel.IsMatch(Txt_DeliverTel.Text) && !RTel1.IsMatch(Txt_DeliverTel.Text))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('电话号码格式不正确！')</script>");
                        Txt_DeliverTel.Focus();
                    }
                    else if (Txt_ShipperZipCode.Text != "")
                    {
                        ZipCodechecking(Txt_ShipperZipCode);
                    }
                    else if (Txt_ConsigneeZipCode.Text != "")
                    {
                        ZipCodechecking(Txt_ConsigneeZipCode);
                    }
                    else if (Txt_DeliverZipCode.Text != "")
                    {
                        ZipCodechecking(Txt_DeliverZipCode);
                    }
                    else if (Txt_ShipperCountry.Text != "")
                    {
                        Countrychecking(Txt_ShipperCountry);
                    }
                    else if (Txt_ConsigneeCountry.Text != "")
                    {
                        Countrychecking(Txt_ConsigneeCountry);
                    }
                    else if (Txt_DeliverCountry.Text != "")
                    {
                        Countrychecking(Txt_DeliverCountry);
                    }
                    else if (Txt_ShipperRegion.Text != "")
                    {
                        Regionchecking(Txt_ShipperRegion);
                    }
                    else if (Txt_ConsigneeRegion.Text != "")
                    {
                        Regionchecking(Txt_ConsigneeCountry);
                    }
                    else if (Txt_DeliverRegion.Text != "")
                    {
                        Regionchecking(Txt_DeliverCountry);
                    }
                    else
                    {
                        Storage();
                        Response.Write("<script>location='HAWBItemAdd.aspx'</script>");
                    }
                }
            }
            else
            {
                if (Txt_BarCode.Text == "")
                {
                    Script(Txt_BarCode.Text);
                    Txt_BarCode.Focus();
                }
                else if (Txt_Account1.Text == "")
                {
                    Script(Txt_Account1.Text);
                    Txt_Account1.Focus();
                }
                else if (Txt_ShipperName.Text == "")
                {
                    Namejudge(Txt_ShipperName);
                }
                else if (Txt_ShipperAddress.Text == "")
                {
                    Addressjudge(Txt_ShipperAddress);
                }
                else if (Txt_ShipperContactor.Text == "")
                {
                    Contactorjudge(Txt_ShipperContactor);
                }
                else if (Txt_ShipperTel.Text == "")
                {
                    Teljudge(Txt_ShipperTel);
                }
                //else if (Txt_ShipperName.Text == "")
                //{
                //    Script(Txt_ShipperName.Text);
                //    Txt_ConsigneeName.Focus();
                //}
                else if (Txt_ConsigneeAddress.Text == "")
                {
                    Addressjudge(Txt_ConsigneeAddress);
                }
                else if (Txt_ConsigneeContactor.Text == "")
                {
                    Contactorjudge(Txt_ConsigneeContactor);
                }
                else if (Txt_ConsigneeTel.Text == "")
                {
                    Teljudge(Txt_ConsigneeTel);
                }                
                else
                {

                    if (!RTel.IsMatch(Txt_ShipperTel.Text) && !RTel1.IsMatch(Txt_ShipperTel.Text))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('电话号码格式不正确！')</script>");
                        Txt_ShipperTel.Focus();
                    }
                    else if (!RTel.IsMatch(Txt_ConsigneeTel.Text) && !RTel1.IsMatch(Txt_ConsigneeTel.Text))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('电话号码格式不正确！')</script>");
                        Txt_ConsigneeTel.Focus();
                    }
                    else if (Txt_ShipperZipCode.Text != "")
                    {
                        ZipCodechecking(Txt_ShipperZipCode);
                    }
                    else if (Txt_ConsigneeZipCode.Text != "")
                    {
                        ZipCodechecking(Txt_ConsigneeZipCode);
                    }
                    else if (Txt_ShipperCountry.Text != "")
                    {
                        Countrychecking(Txt_ShipperCountry);
                    }
                    else if (Txt_ConsigneeCountry.Text != "")
                    {
                        Countrychecking(Txt_ConsigneeCountry);
                    }
                    else if (Txt_ShipperRegion.Text != "")
                    {
                        Regionchecking(Txt_ShipperRegion);
                    }
                    else if (Txt_ConsigneeRegion.Text != "")
                    {
                        Regionchecking(Txt_ConsigneeCountry);
                    }
                    else
                    {
                        Storage();
                        Response.Write("<script>location='HAWBItemAdd.aspx'</script>");
                    }
                }
            }
        }
        protected void But_Conel_Click(object sender, EventArgs e)
        {
            InitialControl(this.Controls);
            Session["HAWB"] = null;
        }
        protected void Namejudge(TextBox tb)
        {
            Script(tb.Text);
            tb.Focus(); 
        }
        protected void Addressjudge(TextBox tb)
        {
            Script(tb.Text);
            tb.Focus();
        }
        protected void Contactorjudge(TextBox tb)
        {
            Script(tb.Text);
            tb.Focus();
        }
        protected void Teljudge(TextBox tb)
        {
            Script(tb.Text);
            tb.Focus();
        }
        protected void Script(string text)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('"+text+"不能为空！')</script>");
        }
        protected void ZipCodechecking(TextBox tb)
        {
            if (!RZipCode.IsMatch(tb.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('邮编格式不正确,只能输入数字且为6位！')</script>");
                tb.Focus();
            }
        }
        protected void Countrychecking(TextBox tb)
        {
            if (!RCountry.IsMatch(tb.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母并为2位！！')</script>");
                tb.Focus();
            }
        }
        protected void Regionchecking(TextBox tb)
        {
            if (!RRegion.IsMatch(tb.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母并为3位！！')</script>");
                tb.Focus();
            }
        }
    }
}