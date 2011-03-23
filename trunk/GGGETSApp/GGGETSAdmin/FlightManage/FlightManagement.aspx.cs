using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;

namespace GGGETSAdmin.FlightManage
{
    public partial class FlightManagement : System.Web.UI.Page
    {
        private int n = 1;
        //protected IFlightManagementService _flightservice;
        private static IRegionCodeManagementService _regionservice;
        private IMAWBManagementService _mawbservice;
        private string BarCode = string.Empty;
        private string From = string.Empty;
        private string to = string.Empty;
        private static string RRegion = @"^[A-Za-z]";
        private static string Rtime = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29))$";
        protected FlightManagement()
        { }
        public FlightManagement(IRegionCodeManagementService regionservice,IMAWBManagementService mawbservice)
        {
            _regionservice = regionservice;
            _mawbservice = mawbservice;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["FlightNo"] != "" && Request.QueryString["FlightNo"] != null)
                {
                    BarCode=Request.QueryString["FlightNo"].ToString();
                    Band(BarCode, From, to);
                }
            }
        }
        /// <summary>
        /// gridviw数据源绑定
        /// </summary>
        /// <param name="barCode"></param>
        /// <param name="From"></param>
        /// <param name="to"></param>
        protected void Band(string barCode,string From,string to)
        {
            if (Txt_FlightNo.Text.Trim() != "")
            {
                BarCode = Txt_FlightNo.Text.Trim().ToUpper();
            }
            if (Txt_From.Text.Trim() != "")
            {
                From = Txt_From.Text.Trim().ToUpper();
            }
            if (Txt_To.Text.Trim() != "")
            {
                to = Txt_To.Text.Trim().ToUpper();
            }
            gv_HAWB.DataSource = _mawbservice.FindMAWBByFlightCondition(BarCode, From, to);
            gv_HAWB.DataBind();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Demand_Click(object sender, EventArgs e)
        {
            Band(BarCode, From, to);         
            
        }
        protected void gv_HAWB_RowEditing(object sender, GridViewEditEventArgs e)
        {
        //    if (listflight == null)
        //    {
        //        listflight = (IList<Flight>)Session["listflight"];
        //    }
        //    gv_HAWB.EditIndex = e.NewEditIndex;
        //    gv_HAWB.DataSource = listflight;
        //    gv_HAWB.DataBind();
        }

        protected void gv_HAWB_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        //    flight=new Flight();
        //    flight.FID=Guid.Parse(gv_HAWB.DataKeys[e.RowIndex].Value.ToString());
        //    flight.FlightNo = ((LinkButton)gv_HAWB.Rows[e.RowIndex].FindControl("lbtn_FLTNo") as LinkButton).Text.Trim();
        //    //((TextBox)gv_HAWB.Rows[e.RowIndex].FindControl("Txt_From")).Text = autoFrom.SelectedValue;
        //    flight.From = ((TextBox)gv_HAWB.Rows[e.RowIndex].FindControl("Txt_From")).Text.Trim();
        //    //((TextBox)gv_HAWB.Rows[e.RowIndex].FindControl("Txt_To") as TextBox).Text = autoTo.SelectedValue;
        //    flight.To = ((TextBox)gv_HAWB.Rows[e.RowIndex].FindControl("Txt_To") as TextBox).Text.Trim();
        //    flight.TakeOffTime = DateTime.Parse(((TextBox)gv_HAWB.Rows[e.RowIndex].FindControl("Txt_TakeOffTime") as TextBox).Text.Trim());
        //    flight.LandTime = DateTime.Parse(((TextBox)gv_HAWB.Rows[e.RowIndex].FindControl("Txt_LandTime") as TextBox).Text.Trim());
        //    _flightservice.ModifyFlight(flight);
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改成功！')</script>");
        //    gv_HAWB.EditIndex = -1;
        //    gv_HAWB.DataSource = _flightservice.FindAllFlights();
        //    gv_HAWB.DataBind();
        }

        protected void gv_HAWB_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gv_HAWB_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
        //    if (listflight == null)
        //    {
        //        listflight = (IList<Flight>)Session["listflight"];
        //    }
        //    gv_HAWB.EditIndex = -1;
        //    gv_HAWB.DataSource = listflight;
        //    gv_HAWB.DataBind();
        }

        protected void gv_HAWB_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int inex = Convert.ToInt16(e.CommandArgument);
                Guid id = Guid.Parse(gv_HAWB.DataKeys[inex].Value.ToString());
                
            }
           
        }
        public int N()
        {
            return n++;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[][] GetCountryList(string prefixText, int count)
        {
            if (count == 0)
            {
                count = 10;
            }

            List<string[]> items = new List<string[]>();

            IList<RegionCode> regioncode = _regionservice.FindAllRegionCodes();
            foreach (RegionCode region in regioncode)
            {
                string[] ItemArry = new string[3];
                ItemArry[0] = region.RegionName;
                ItemArry[1] = region.RegionCode1;
                items.Add(ItemArry);
            }
            return items.Take(count).ToArray();
        }

        protected void autoFrom_ItemSelected(object sender, EventArgs e)
        {
            //Txt_From.Text = ((AutoCompleteExtra.AutoCompleteExtraExtender)sender).SelectedValue;
        }

        protected void autoTo_ItemSelected(object sender, EventArgs e)
        {
            //Txt_To.Text = ((AutoCompleteExtra.AutoCompleteExtraExtender)sender).SelectedValue;
        }
    }
}