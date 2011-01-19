using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using System.Data;

namespace GGGETSAdmin.HAWB1
{
    public partial class HAWBdemand : System.Web.UI.Page
    {
        private IHAWBManagementService _hawbService;
        protected HAWBdemand()
        {
        }
        private int i = 0;
        public HAWBdemand(IHAWBManagementService hawbService)
        {
            _hawbService = hawbService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GVBind();
            }
        }

        protected void GVBind()
        {
            List<HAWB> list = _hawbService.FindPagedHAWBs(0, 10);
            this.GV_Bdemand.DataSource = list;
            this.GV_Bdemand.DataBind();
        }

        protected void But_Demand_Click(object sender, EventArgs e)
        {
            HAWB hawb = _hawbService.FindHAWBByBarCode(Txt_BarCode.Text);
            List<HAWB> list = new List<HAWB>();
            list.Add(hawb);
            this.GV_Bdemand.DataSource = list;
            this.GV_Bdemand.DataBind();
        }

        protected void GV_Bdemand_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string type = "Select";        
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = GV_Bdemand.Rows[index];
                string BarCode = ((Label)row.FindControl("lbl_BarCode") as Label).Text;
                Response.Redirect("HAWBSetAndEit.aspx?Type=" + type + "&BarCode=" + BarCode + "");
            }
        }
    }
}