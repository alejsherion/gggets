﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;

namespace GGGETSAdmin.PersonnelManage.CompanyManage
{
    public partial class CompanyManagemnet : System.Web.UI.Page
    {
        private ICompanyManagementService _companyService;
        private ISysUserManagementService _sysUserManagementService;
        private static Regex RTel = new Regex(@"^[0-9]*$");
        protected CompanyManagemnet()
        { }
        public CompanyManagemnet(ICompanyManagementService companyservice, ISysUserManagementService sysUserManagementService)
        {
            _companyService = companyservice;
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
            string FullName = string.Empty;
            string ShortName = string.Empty;
            string Contactor = string.Empty;
            string ContactorPhone = string.Empty;
            bool Ok = true;//判断是否条件有误
            if (Txt_CompanyCode.Text.Trim() != "")
            {
                CompanyCode = Txt_CompanyCode.Text.Trim().ToUpper();
            }
            if (Txt_FullName.Text.Trim() != "")
            {
                FullName = Txt_FullName.Text.Trim().ToUpper();
            }
            if (Txt_ShortName.Text.Trim() != "")
            {
                ShortName = Txt_ShortName.Text.Trim().ToUpper();
            }
            if (Txt_Contactor.Text.Trim() != "")
            {
                Contactor = Txt_Contactor.Text.Trim().ToUpper();
            }
            if (Txt_ContactorPhone.Text.Trim() != "")
            {
                ContactorPhone = Txt_ContactorPhone.Text.Trim();
            }
            else
            {
                if (!RTel.IsMatch(Txt_ContactorPhone.Text.Trim()))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入数字！')</script>");
                    Txt_ContactorPhone.Focus();
                    Ok = false;
                }
            }
            if (Ok)
            {
                gv_Company.DataSource = _companyService.FindCompaniesByCondition(CompanyCode, FullName, ShortName, Contactor, ContactorPhone);
                gv_Company.DataBind();
            }
        }
        /// <summary>
        /// 数据修改，编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gv_Company_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid id = (Guid)Session["UserID"];
            ModulePrivilege Mpriviege = _sysUserManagementService.GetPrivilegeByUserid(id);
            if (e.CommandName == "Eidt")
            {
                bool privilege = (bool)Mpriviege[Privilege.修改.ToString()];
                int index = Convert.ToInt32(e.CommandArgument);
                string CompanyCode = gv_Company.DataKeys[index].Value.ToString();
                Response.Redirect("CompanyDetails.aspx?CompanyCode=" + CompanyCode + "&Privilege=" + privilege + "");
            }
            else if (e.CommandName == "Updata")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string CompanyCode = gv_Company.DataKeys[index].Value.ToString();
                Response.Redirect("CompanyModify.aspx?CompanyCode=" + CompanyCode + "");
            }
            else
            {
                string CompanyCode = e.CommandArgument.ToString();
                Response.Redirect("");
            }
        }

        protected void gv_Company_DataBound(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                Guid id = (Guid)Session["UserID"];
                ModulePrivilege Authority = _sysUserManagementService.GetPrivilegeByUserid(id);
                foreach (GridViewRow row in gv_Company.Rows)
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