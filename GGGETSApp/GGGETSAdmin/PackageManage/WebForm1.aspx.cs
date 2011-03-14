using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GGGETSAdmin.PackageManage
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("../MawbManage/MawbDetails.aspx?BarCode=" + TextBox1.Text + "");
            //Response.Redirect("PackageDetails.aspx?BarCode=" + TextBox1.Text + "");
        }
    }
}