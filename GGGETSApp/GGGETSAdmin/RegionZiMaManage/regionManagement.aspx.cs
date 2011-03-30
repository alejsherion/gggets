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
        private ISysUserManagementService _sysUserManagementService;
        protected regionManagement()
        { }
        public regionManagement(IRegionCodeManagementService regionservice, ICountryCodeManagementService countryservice, ISysUserManagementService sysUserManagementService)
        {
            _regionservice = regionservice;
            _countryservice = countryservice;
            _sysUserManagementService = sysUserManagementService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                Guid id = (Guid)Session["UserID"];
                ModulePrivilege Mpriviege = _sysUserManagementService.GetPrivilegeByUserid(id);
                if (!(bool)Mpriviege[Privilege.查询.ToString()])
                {
                    btn_Demand.Enabled = false;
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
            listregion = _regionservice.FindRegionCodesByCondition(CountryCode, RegionCode, RegionName);//查询地区二字码
            gv_Region.DataSource = listregion;
            gv_Region.DataBind();
            Session["Region"] = listregion;
            Txt_CountryCode.Text = string.Empty;
            txt_RegionCode.Text = string.Empty;
            txt_RegionName.Text = string.Empty;
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gv_Region_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            regioncode.ID = int.Parse(gv_Region.DataKeys[e.RowIndex].Value.ToString());
            regioncode.CountryCode = ((Label)gv_Region.Rows[e.RowIndex].FindControl("Txt_CountryCode")).Text.Trim().ToUpper();
            regioncode.RegionCode1 = ((TextBox)gv_Region.Rows[e.RowIndex].FindControl("Txt_RegionCode")).Text.Trim().ToUpper();
            regioncode.RegionName = ((TextBox)gv_Region.Rows[e.RowIndex].FindControl("Txt_RegionName")).Text.Trim().ToUpper();
            _regionservice.ModifyRegionCode(regioncode);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改成功！')</script>");
            gv_Region.EditIndex = -1;
            listregion = _regionservice.FindAllRegionCodes();//获取所以地区三字码
            gv_Region.DataSource = listregion;
            gv_Region.DataBind();
            Session["Region"] = listregion;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gv_Region_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gv_Region_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string RegionCode = e.CommandArgument.ToString();
                regioncode = _regionservice.FindRegionByRegionCode(RegionCode);//根据地区二字码获取信息
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

        protected void gv_Region_DataBound(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                Guid id = (Guid)Session["UserID"];
                ModulePrivilege Authority = _sysUserManagementService.GetPrivilegeByUserid(id);
                foreach (GridViewRow row in gv_Region.Rows)
                {
                    if (!(bool)Authority[Privilege.修改.ToString()])
                    {
                        ((LinkButton)row.FindControl("lbtn_Eint") as LinkButton).Enabled = false;
                    }
                    if (!(bool)Authority[Privilege.删除.ToString()])
                    {
                        ((LinkButton)row.FindControl("btn_Delete") as LinkButton).Enabled = false;
                    }
                }
            }
        }
    }
}