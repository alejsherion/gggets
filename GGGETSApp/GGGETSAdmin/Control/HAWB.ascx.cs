using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace GGGETSAdmin.Control
{
    public partial class HAWB : System.Web.UI.UserControl
    {
        protected static Regex RZipCode = new Regex(@"\d{6}");
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Add_Deliver_Click(object sender, EventArgs e)
        {
            this.Deliver.Visible = true;
        }
        
        protected void But_Conel_Click(object sender, EventArgs e)
        {
            Txt_Account1.Text = string.Empty;
            Txt_Account2.Text = string.Empty;
            Txt_BarCode.Text = string.Empty;
            Txt_City.Text = string.Empty;
            Txt_ConsigneeAddress.Text = string.Empty;
            Txt_ConsigneeCity.Text = string.Empty;
            Txt_ConsigneeContactor.Text = string.Empty;
            Txt_ConsigneeCountry.Text = string.Empty;
            Txt_ConsigneeName.Text = string.Empty;
            Txt_ConsigneeRegion.Text = string.Empty;
            Txt_ConsigneeTel.Text = string.Empty;
            Txt_ConsigneeZipCode.Text = string.Empty;
            Txt_ShipperAddress.Text = string.Empty;
            Txt_ShipperContactor.Text = string.Empty;
            Txt_ShipperCountry.Text = string.Empty;
            Txt_ShipperName.Text = string.Empty;
            Txt_ShipperRegion.Text = string.Empty;
            Txt_ShipperTel.Text = string.Empty;
            Txt_ShipperZipCode.Text = string.Empty;
            Txt_DeliverAddress.Text = string.Empty;
            Txt_DeliverCity.Text = string.Empty;
            Txt_DeliverContactor.Text = string.Empty;
            Txt_DeliverCountry.Text = string.Empty;
            Txt_DeliverName.Text = string.Empty;
            Txt_DeliverRegion.Text = string.Empty;
            Txt_DeliverTel.Text = string.Empty;
            Txt_DeliverZipCode.Text = string.Empty;
        }
    }
}