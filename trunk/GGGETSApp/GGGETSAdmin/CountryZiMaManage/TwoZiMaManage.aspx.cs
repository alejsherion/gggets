using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Data;

namespace GGGETSAdmin.CountryZiMaManage
{
    public partial class TwoZiMaManage : System.Web.UI.Page
    {
        private ICountryCodeManagementService _countryservice;
        private ISysUserManagementService _sysUserManagementService;
        private CountryCode country = new CountryCode();
        private IList<CountryCode> listCountry;
        protected TwoZiMaManage()
        {}
        public TwoZiMaManage(ICountryCodeManagementService countryservice, ISysUserManagementService SysUserManagementService)
        {
            _countryservice = countryservice;
            _sysUserManagementService = SysUserManagementService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] != null)
                {

                    Guid id = (Guid)Session["UserID"];
                    ModulePrivilege Mprivlege = _sysUserManagementService.GetPrivilegeByUserid(id);
                    if (!(bool)Mprivlege.QueryPrivilege)
                    {
                       btn_Demand.Enabled = false;
                    }
                }
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Demand_Click(object sender, EventArgs e)
        {
            string CountryCode = string.Empty;
            string CountryName = string.Empty;
            if (Txt_CountryCode.Text.Trim() != "")
            {

                CountryCode = Txt_CountryCode.Text.Trim().ToUpper();
            }
            if (txt_CountryName.Text.Trim() != "")
            {
                CountryName = txt_CountryName.Text.Trim().ToUpper();
            }
            listCountry = _countryservice.FindCountriesByCondition(CountryCode, CountryName);
            gv_Country.DataSource = listCountry;
            gv_Country.DataBind();
            Session["Country"] = listCountry;
            Txt_CountryCode.Text = string.Empty;
            txt_CountryName.Text = string.Empty;
        }
        protected void gv_Country_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (listCountry == null)
            {
                listCountry = (IList<CountryCode>)Session["Country"];
            }
            gv_Country.EditIndex = e.NewEditIndex;
            gv_Country.DataSource = listCountry;
            gv_Country.DataBind();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gv_Country_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //foreach (GridViewItem row in gv_Country.Rows)
            //{
            //    country.CountryCode1 = ((TextBox)row.FindControl("Txt_CountryCode1")).Text.Trim();
            //    country.CountryName = ((TextBox)row.FindControl("Txt_CountryName")).Text.Trim();
            //}
            //country.CountryCode1 = ((TextBox)gv_Country.Rows[e.RowIndex].Cells[e.RowIndex].FindControl("Txt_CountryCode1")).Text.Trim();
            country.ID = int.Parse(gv_Country.DataKeys[e.RowIndex].Value.ToString());
            country.CountryCode1 = ((TextBox)gv_Country.Rows[e.RowIndex].FindControl("Txt_CountryCode")).Text.Trim().ToUpper();
            country.CountryName = ((TextBox)gv_Country.Rows[e.RowIndex].FindControl("Txt_CountryName")).Text.Trim().ToUpper();
            _countryservice.ModifyCountryCode(country);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改成功！')</script>");
            listCountry = _countryservice.FindAllCountries();
            gv_Country.EditIndex = -1;
            gv_Country.DataSource = listCountry;
            gv_Country.DataBind();
            Session["Country"] = listCountry;
        }

        protected void gv_Country_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gv_Country_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (listCountry == null)
            {
                listCountry = (IList<CountryCode>)Session["Country"];
            }
            gv_Country.EditIndex = -1;
            gv_Country.DataSource = listCountry;
            gv_Country.DataBind();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gv_Country_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string CountryCode = e.CommandArgument.ToString();
                country = _countryservice.FindCountriedByCountryCode(CountryCode);
                _countryservice.RemoveCountryCode(country);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('删除成功！')</script>");
                //if (listCountry == null)
                //{
                //    listCountry = (IList<CountryCode>)Session["Country"];
                //}
                //listCountry.Remove(country);
                gv_Country.DataSource = _countryservice.FindAllCountries();
                gv_Country.DataBind();
            }
        }

        protected void gv_Country_DataBound(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                Guid id = (Guid)Session["UserID"];
                ModulePrivilege Authority = _sysUserManagementService.GetPrivilegeByUserid(id);
                
                foreach (GridViewRow row in gv_Country.Rows)
                {
                    if (!(bool)Authority.UpdatePrivilege)
                    {
                        ((LinkButton)row.FindControl("btn_Eidt") as LinkButton).Enabled = false;
                    }
                    if (!(bool)Authority.DeletePrivilege)
                    {
                        ((LinkButton)row.FindControl("btn_Delete") as LinkButton).Enabled = false;
                    }

                }
            }
        }



        
    }
}