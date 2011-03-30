using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;

namespace GGGETSAdmin.PersonnelManage.DepartmentManage
{
    public partial class DepartmentManagemnet : System.Web.UI.Page
    {
        private IDepartmentManagementService _deparService;
        private ISysUserManagementService _sysUserManagementService;
        protected DepartmentManagemnet()
        { }
        public DepartmentManagemnet(IDepartmentManagementService deparservice, ISysUserManagementService sysUserManagementService)
        {
            _deparService = deparservice;
            _sysUserManagementService = sysUserManagementService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
        }
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Demand_Click(object sender, EventArgs e)
        {
            string CompanyCode = string.Empty;
            string DepCode = string.Empty;
            string DepName = string.Empty;
            if (Txt_CompanyCode.Text.Trim() != "")
            {
                CompanyCode = Txt_CompanyCode.Text.Trim().ToUpper();
            }
            if (Txt_DepCode.Text.Trim() != "")
            {
                DepCode = Txt_DepCode.Text.Trim().ToUpper();
            }
            if (Txt_DepName.Text.Trim() != "")
            {
                DepName = Txt_DepName.Text.Trim().ToUpper();
            }
            gv_Depar.DataSource = _deparService.FindDepartmentsByCondition(CompanyCode, DepCode, DepName);
            gv_Depar.DataBind();
        }
        /// <summary>
        /// 数据操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gv_Depar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = int.Parse(e.CommandArgument.ToString());
            GridViewRow row = gv_Depar.Rows[index];
            string CompanyCode = ((Label)row.FindControl("lbl_CompanyCode") as Label).Text;
            string DepCode = ((Label)row.FindControl("lbl_DepCode") as Label).Text;
            if (e.CommandName == "Eidt")
            {
                Response.Redirect("DepartmentDetails.aspx?CompanyCode=" + CompanyCode + "&DeparCode=" + DepCode + "");
            }
            else if (e.CommandName == "Updata")
            {
                Response.Redirect("DepartmentModify.aspx?CompanyCode=" + CompanyCode + "&DeparCode=" + DepCode + "");
            }
            else if (e.CommandName == "Del")
            {
                Department depar = _deparService.FindDepartmentByDepCodeAndCompanyCode(DepCode, CompanyCode);
                if (depar != null)
                {
                    
                }
            }
        }

        protected void gv_Depar_DataBound(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                Guid id = (Guid)Session["UserID"];
                ModulePrivilege Authority = _sysUserManagementService.GetPrivilegeByUserid(id);
                foreach (GridViewRow row in gv_Depar.Rows)
                {
                    if (!(bool)Authority[Privilege.修改.ToString()])
                    {
                        ((LinkButton)row.FindControl("lbtn_Updata") as LinkButton).Enabled = false;
                    }
                    if (!(bool)Authority[Privilege.删除.ToString()])
                    {
                        ((LinkButton)row.FindControl("lbtn_Delete") as LinkButton).Enabled = false;
                    }

                }
            }
        }
    }
}