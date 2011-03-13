using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;

namespace GGGETSAdmin.RegionZiMaManage
{
    public partial class regionManagement : System.Web.UI.Page
    {
        private RegionCode regioncode=new RegionCode();
        private IList<RegionCode> listregion;
        private static ICountryCodeManagementService _countryservice;
        private IRegionCodeManagementService _regionservice;
        protected regionManagement()
        { }
        public regionManagement(IRegionCodeManagementService regionservice, ICountryCodeManagementService countryservice)
        {
            _regionservice = regionservice;
            _countryservice = countryservice;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Demand_Click(object sender, EventArgs e)
        {
            string CountryCode = string.Empty;
            string RegionCode = string.Empty;
            string RegionName = string.Empty;
            if (Txt_CountryCode.Text.Trim() != "")
            {
                CountryCode = Txt_CountryCode.Text.Trim().ToUpper();
            }
            if (txt_RegionCode.Text.Trim() != "")
            {
                RegionCode = txt_RegionCode.Text.Trim().ToUpper();
            }
            if (txt_RegionName.Text.Trim() != "")
            {
                RegionName = txt_RegionName.Text.Trim().ToUpper();
            }
            listregion = _regionservice.FindRegionCodesByCondition(CountryCode, RegionCode, RegionName);
            gv_Region.DataSource = listregion;
            gv_Region.DataBind();
            Session["Region"] = listregion;
            Txt_CountryCode.Text = string.Empty;
            txt_RegionCode.Text = string.Empty;
            txt_RegionName.Text = string.Empty;
        }

        protected void gv_Region_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (listregion == null)
            {
                listregion = (IList<RegionCode>)Session["Region"];
            }
            gv_Region.EditIndex = e.NewEditIndex;
            gv_Region.DataSource = listregion;
            gv_Region.DataBind();
        }

        protected void gv_Region_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            regioncode.ID = int.Parse(gv_Region.DataKeys[e.RowIndex].Value.ToString());
            regioncode.CountryCode = ((TextBox)gv_Region.Rows[e.RowIndex].FindControl("Txt_CountryCode")).Text.Trim().ToUpper();
            regioncode.RegionCode1 = ((TextBox)gv_Region.Rows[e.RowIndex].FindControl("Txt_RegionCode")).Text.Trim().ToUpper();
            regioncode.RegionName = ((TextBox)gv_Region.Rows[e.RowIndex].FindControl("Txt_RegionName")).Text.Trim().ToUpper();
            _regionservice.ModifyRegionCode(regioncode);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改成功！')</script>");
            gv_Region.EditIndex = -1;
            listregion = _regionservice.FindAllRegionCodes();
            gv_Region.DataSource = listregion;
            gv_Region.DataBind();
            Session["Region"] = listregion;
        }

        protected void gv_Region_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
        }

        protected void gv_Region_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (listregion == null)
            {
                listregion = (IList<RegionCode>)Session["Region"];
            }
            gv_Region.EditIndex = -1;
            gv_Region.DataSource = listregion;
            gv_Region.DataBind();
        }

        protected void gv_Region_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string RegionCode = e.CommandArgument.ToString();
                regioncode = _regionservice.FindRegionByRegionCode(RegionCode);
                _regionservice.RemoveRegionCode(regioncode);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('删除成功！')</script>");
                if (listregion == null)
                {
                    listregion = (IList<RegionCode>)Session["Region"];
                }
                listregion.Remove(regioncode);
                gv_Region.DataSource = _regionservice.FindAllRegionCodes();
                gv_Region.DataBind();
            }
        }
    }
}