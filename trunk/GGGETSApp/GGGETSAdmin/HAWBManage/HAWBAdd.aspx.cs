using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;

namespace GGGETSAdmin.HAWBManage
{
    public partial class HAWBAdd : System.Web.UI.Page
    {
        private static Regex RZipCode = new Regex(@"^\d{6}");
        private static Regex RTel = new Regex(@"^(\d{3,4}-)?\d{7,8}$");
        private static Regex RTel1 = new Regex(@"^1[35]\d{9}$");
        private static Regex RCountry = new Regex(@"^[A-Za-z]{2}");
        private static Regex RRegion = new Regex(@"^[A-Za-z]{3}");
        private HAWB hawb;
        private int Update=0;
        private static string type;
        protected IHAWBManagementService _hawbService;
        protected HAWBAdd()
        { }
        public HAWBAdd(IHAWBManagementService hawbService)
        {
            _hawbService = hawbService;
            type = string.Empty;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["BarCode"] != null&&Request.QueryString["BarCode"] !="")
                {
                    Update = Convert.ToInt32(Request.QueryString["update"]);
                    ViewState["update"] = Update;
                    hawb = _hawbService.LoadHAWBByBarCode(Request.QueryString["BarCode"].ToString());
                    Session["HAWB"] = hawb;
                    if (hawb.DeliverName != "")
                    {
                        this.Deliver.Visible = true;
                    }
                    Evaluate();                    
                    DDl_Status.Visible = true;
                    lbl_Status.Visible = true;
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
                    DDl_Status.Visible = false;
                    lbl_Status.Visible = false;
                }
            }

        }

        private void Storage()
        {
            if(ViewState["update"]!=null)
            {
                Update = (int)ViewState["update"];
            }            
            if (Update == 0)
            {
                hawb = new HAWB();
                User user = new User();
                user.UID = Guid.NewGuid();
                user.LoginName = "test001";
                user.Password = "123456";
                user.RealName = "张三";
                user.Department = "研发部";
                user.PostCode = "200435";
                user.Phone = "110";
                user.CountryCode = "01";
                user.RegionCode = "01";
                user.Address = "XXX路XXX号";
                user.UpdateTime = DateTime.Now;
                user.CreateTime = DateTime.Now;
                user.Operator = "沈志伟";
                user.FeeDiscountType = 0;
                user.FeeDiscountRate = 1;
                user.WeightDiscountType = 0;
                user.WeightDiscountRate = 1;
                user.SettleType = 0;
                user.WeightCalType = 0;
                user.Status = 0;
                hawb.User = user;


                hawb.HID = Guid.NewGuid();
                hawb.BarCode = Txt_BarCode.Text;
                hawb.UID = user.UID;
                hawb.SettleType = int.Parse(DDl_SettleType.SelectedValue);

                hawb.ShipperName = Txt_ShipperName.Text;
                hawb.ShipperContactor = Txt_ShipperContactor.Text;
                hawb.ShipperCountry = Txt_ShipperCountry.Text;
                hawb.ShipperRegion = Txt_ShipperRegion.Text;
                hawb.ShipperAddress = Txt_ShipperAddress.Text;
                hawb.ShipperTel = Txt_ShipperTel.Text;
                hawb.ShipperZipCode = Txt_ShipperZipCode.Text;

                hawb.ConsigneeName = Txt_ConsigneeName.Text;
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
                hawb = (HAWB)Session["HAWB"];
                hawb.BarCode = Txt_BarCode.Text;
                hawb.SettleType = int.Parse(DDl_SettleType.SelectedValue);
                hawb.Status = int.Parse(DDl_Status.SelectedValue);

                hawb.ShipperName = Txt_ShipperName.Text;
                hawb.ShipperContactor = Txt_ShipperContactor.Text;
                hawb.ShipperCountry = Txt_ShipperCountry.Text;
                hawb.ShipperRegion = Txt_ShipperRegion.Text;
                hawb.ShipperAddress = Txt_ShipperAddress.Text;
                hawb.ShipperTel = Txt_ShipperTel.Text;
                hawb.ShipperZipCode = Txt_ShipperZipCode.Text;

                hawb.ConsigneeName = Txt_ConsigneeName.Text;
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
                type = "Amend";
            }
        }
        protected void Evaluate()
        {
            hawb = (HAWB)Session["HAWB"];
            Txt_BarCode.Text = hawb.BarCode;
            DDl_SettleType.SelectedValue = hawb.SettleType.ToString();
            DDl_Status.SelectedValue = hawb.Status.ToString();

            Txt_ShipperName.Text = hawb.ShipperName;
            Txt_ShipperContactor.Text = hawb.ShipperContactor;
            Txt_ShipperCountry.Text = hawb.ShipperCountry;
            Txt_ShipperRegion.Text = hawb.ShipperRegion;
            Txt_ShipperAddress.Text = hawb.ShipperAddress;
            Txt_ShipperTel.Text = hawb.ShipperTel;
            Txt_ShipperZipCode.Text = hawb.ShipperZipCode;

            Txt_ConsigneeName.Text = hawb.ConsigneeName.ToString();
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>Open();</script>");
        }

        protected void But_Next_Click(object sender, EventArgs e)
        {
            if (Deliver.Visible == true)
            {
                if (Txt_BarCode.Text == "")
                {
                    judge(Txt_BarCode);
                }
                else if (Txt_Account1.Text == "")
                {
                    judge(Txt_Account1);
                }
                else if (Txt_ShipperName.Text == "")
                {
                    judge(Txt_ShipperName);
                }
                else if (Txt_ShipperCountry.Text == "")
                {
                    judge(Txt_ShipperCountry);
                }
                else if (Txt_ShipperRegion.Text == "")
                {
                    judge(Txt_ShipperRegion);
                }
                else if (Txt_ShipperZipCode.Text == "")
                {
                    judge(Txt_ShipperZipCode);
                }
                else if (Txt_ShipperAddress.Text == "")
                {
                    judge(Txt_ShipperAddress);
                }
                else if (Txt_ShipperContactor.Text == "")
                {
                    judge(Txt_ShipperContactor);
                }
                else if (Txt_ShipperTel.Text == "")
                {
                    judge(Txt_ShipperTel);
                }
                else if (Txt_ConsigneeName.Text == "")
                {
                    judge(Txt_ConsigneeName);
                }
                else if (Txt_ConsigneeCountry.Text == "")
                {
                    judge(Txt_ConsigneeCountry);
                }
                else if (Txt_ConsigneeRegion.Text == "")
                {
                    judge(Txt_ConsigneeRegion);
                }
                else if (Txt_ConsigneeZipCode.Text == "")
                {
                    judge(Txt_ConsigneeZipCode);
                }
                else if (Txt_ConsigneeAddress.Text == "")
                {
                    judge(Txt_ConsigneeAddress);
                }
                else if (Txt_ConsigneeContactor.Text == "")
                {
                    judge(Txt_ConsigneeContactor);
                }
                else if (Txt_ConsigneeTel.Text == "")
                {
                    judge(Txt_ConsigneeTel);
                }
                else if (Txt_DeliverName.Text == "")
                {
                    judge(Txt_DeliverName);
                }
                else if (Txt_DeliverCountry.Text == "")
                {
                    judge(Txt_DeliverCountry);
                }
                else if (Txt_DeliverRegion.Text == "")
                {
                    judge(Txt_DeliverRegion);
                }
                else if (Txt_DeliverZipCode.Text == "")
                {
                    judge(Txt_DeliverZipCode);
                }
                else if (Txt_DeliverAddress.Text == "")
                {
                    judge(Txt_DeliverAddress);
                }
                else if (Txt_DeliverContactor.Text == "")
                {
                    judge(Txt_DeliverContactor);
                }
                else if (Txt_DeliverTel.Text == "")
                {
                    judge(Txt_DeliverTel);
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
                    else if (!ZipCodechecking(Txt_ShipperZipCode))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('邮编格式不正确,只能输入数字且为6位！')</script>");
                        Txt_ShipperZipCode.Focus();
                    }
                    else if (!ZipCodechecking(Txt_ConsigneeZipCode))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('邮编格式不正确,只能输入数字且为6位！')</script>");
                        Txt_ConsigneeZipCode.Focus();
                    }
                    else if (!ZipCodechecking(Txt_DeliverZipCode))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('邮编格式不正确,只能输入数字且为6位！')</script>");
                        Txt_DeliverZipCode.Focus();
                    }
                    else if (!Countrychecking(Txt_ShipperCountry))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母并为2位！')</script>");
                        Txt_ShipperCountry.Focus();
                    }
                    else if (!Countrychecking(Txt_ConsigneeCountry))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母并为2位！')</script>");
                        Txt_ConsigneeCountry.Focus();
                    }
                    else if (!Countrychecking(Txt_DeliverCountry))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母并为2位！')</script>");
                        Txt_DeliverCountry.Focus();
                    }
                    else if (!Regionchecking(Txt_ShipperRegion))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母并为3位！！')</script>");
                        Txt_ShipperRegion.Focus();
                    }
                    else if (!Regionchecking(Txt_ConsigneeRegion))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母并为3位！！')</script>");
                        Txt_ConsigneeRegion.Focus();
                    }
                    else if (!Regionchecking(Txt_DeliverRegion))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母并为3位！！')</script>");
                        Txt_DeliverRegion.Focus();
                    }
                    else
                    {
                        Storage();
                        ViewState["update"] = null;
                        if (type != string.Empty)
                        {
                            Response.Redirect("HAWBItemAdd.aspx?type=" + type + "");
                        }
                        else
                        {                            
                            Response.Redirect("HAWBItemAdd.aspx");
                        }
                        
                    }
                }
            }
            else
            {
                if (Txt_BarCode.Text == "")
                {
                    judge(Txt_BarCode);
                }
                else if (Txt_Account1.Text == "")
                {
                    judge(Txt_Account1);
                }
                else if (Txt_ShipperName.Text == "")
                {
                    judge(Txt_ShipperName);
                }
                else if (Txt_ShipperCountry.Text == "")
                {
                    judge(Txt_ShipperCountry);
                }
                else if (Txt_ShipperRegion.Text == "")
                {
                    judge(Txt_ShipperRegion);
                }
                else if (Txt_ShipperZipCode.Text == "")
                {
                    judge(Txt_ShipperZipCode);
                }
                else if (Txt_ShipperAddress.Text == "")
                {
                    judge(Txt_ShipperAddress);
                }
                else if (Txt_ShipperContactor.Text == "")
                {
                    judge(Txt_ShipperContactor);
                }
                else if (Txt_ShipperTel.Text == "")
                {
                    judge(Txt_ShipperTel);
                }
                else if (Txt_ConsigneeName.Text == "")
                {
                    judge(Txt_ConsigneeName);
                }
                else if (Txt_ConsigneeCountry.Text == "")
                {
                    judge(Txt_ConsigneeCountry);
                }
                else if (Txt_ConsigneeRegion.Text == "")
                {
                    judge(Txt_ConsigneeRegion);
                }
                else if (Txt_ConsigneeZipCode.Text == "")
                {
                    judge(Txt_ConsigneeZipCode);
                }
                else if (Txt_ConsigneeAddress.Text == "")
                {
                    judge(Txt_ConsigneeAddress);
                }
                else if (Txt_ConsigneeContactor.Text == "")
                {
                    judge(Txt_ConsigneeContactor);
                }
                else if (Txt_ConsigneeTel.Text == "")
                {
                    judge(Txt_ConsigneeTel);
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
                    else if (!ZipCodechecking(Txt_ShipperZipCode))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('邮编格式不正确,只能输入数字且为6位！')</script>");
                        Txt_ShipperZipCode.Focus();
                    }
                    else if (!ZipCodechecking(Txt_ConsigneeZipCode))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('邮编格式不正确,只能输入数字且为6位！')</script>");
                        Txt_ConsigneeZipCode.Focus();
                    }
                    else if (!Countrychecking(Txt_ShipperCountry))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母并为2位！')</script>");
                        Txt_ShipperCountry.Focus();
                    }
                    else if (!Countrychecking(Txt_ConsigneeCountry))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母并为2位！')</script>");
                        Txt_ConsigneeCountry.Focus();
                    }
                    else if (!Regionchecking(Txt_ShipperRegion))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母并为3位！！')</script>");
                        Txt_ShipperRegion.Focus();
                    }
                    else if (!Regionchecking(Txt_ConsigneeRegion))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母并为3位！！')</script>");
                        Txt_ConsigneeRegion.Focus();
                    }
                    else
                    {
                        Storage();
                        ViewState["update"] = null;
                        if (type != string.Empty)
                        {
                            Response.Redirect("HAWBItemAdd.aspx?type=" + type + "");
                        }
                        else
                        {
                            Response.Redirect("HAWBItemAdd.aspx");
                        }
                        
                    }
                }
            }
        }
        protected void But_Conel_Click(object sender, EventArgs e)
        {
            InitialControl(this.Controls);
            Session["HAWB"] = null;
        }
        protected void judge(TextBox tb)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不能为空！')</script>");
            tb.Focus();
        }
        protected bool ZipCodechecking(TextBox tb)
        {
            bool num = true;
            if (!RZipCode.IsMatch(tb.Text))
            {
                num = false;
            }
            return num;
        }
        protected bool Countrychecking(TextBox tb)
        {
            bool num = true;
            if (!RCountry.IsMatch(tb.Text))
            {
                num = false;
            }
            return num;
        }
        protected bool Regionchecking(TextBox tb)
        {
            bool num = true;
            if (!RRegion.IsMatch(tb.Text))
            {
                num = false;
            }
            return num;
        }
        protected void but_cancel_Click(object sender, EventArgs e)
        {
            Txt_DeliverName.Text = string.Empty;
            Txt_DeliverAddress.Text = string.Empty;
            Txt_DeliverCountry.Text = string.Empty;
            Txt_DeliverRegion.Text = string.Empty;
            Txt_DeliverContactor.Text = string.Empty;
            Txt_DeliverZipCode.Text = string.Empty;
            Txt_DeliverTel.Text = "";
            Deliver.Visible = false;
        }
    }
}