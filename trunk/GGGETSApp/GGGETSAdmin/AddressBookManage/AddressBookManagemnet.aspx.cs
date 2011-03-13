﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;

namespace GGGETSAdmin.AddressBookManage
{
    public partial class AddressBookManagemnet : System.Web.UI.Page
    {
        public int i = 1;
        private static string Rtime = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29))$";
        private IAddressBookManagementService _AddressService;
        protected AddressBookManagemnet()
        { }
        public AddressBookManagemnet(IAddressBookManagementService AddressService)
        {
            _AddressService = AddressService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Band()
        {
            string CompanyCode = string.Empty;
            string DeparCode = string.Empty;
            string LoginName = string.Empty;
            DateTime beginTime = new DateTime();
            DateTime endTime = new DateTime();
            bool Ok = true;
            if (!string.IsNullOrEmpty(Txt_CompanyCode.Text.Trim()) && !string.IsNullOrEmpty(Txt_DepCode.Text.Trim()))
            {
                CompanyCode = Txt_CompanyCode.Text.Trim();
                DeparCode = Txt_DepCode.Text.Trim();
            }
            if (string.IsNullOrEmpty(Txt_LoginName.Text.Trim()))
            {
                LoginName = Txt_LoginName.Text.Trim();
            }
            if (Txt_GetUpTime.Text.Trim() != "")
            {
                if (!Regex.IsMatch(Txt_GetUpTime.Text.Trim(), Rtime))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入正确的日期！如：2010-02-16！')</script>");
                    Txt_GetUpTime.Focus();
                    Ok = false;
                }
                else
                {
                    beginTime = DateTime.Parse(Txt_GetUpTime.Text.Trim().Trim());
                    Ok = true;
                }
            }
            if (Txt_StopTime.Text.Trim() != "")
            {
                if (!Regex.IsMatch(Txt_StopTime.Text.Trim(), Rtime))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入正确的日期！如：2010-02-16！')</script>");
                    Txt_StopTime.Focus();
                    Ok = false;
                }
                else
                {
                    endTime = DateTime.Parse(Txt_StopTime.Text.Trim().Trim());
                    if (beginTime.CompareTo(endTime) == 1)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('起始日期不能大于结束日期！')</script>");
                        Ok = false;
                        Txt_StopTime.Focus();
                    }
                    else
                    {
                        Ok = true;
                    }
                }
            }
            if (Ok)
            {
                gv_AddressBook.DataSource = _AddressService.FindAddressBookByCondition(CompanyCode, DeparCode, LoginName, beginTime, endTime);
                gv_AddressBook.DataBind();
                InitialControl(this.Controls);
            }
        }
        protected void btn_Demand_Click(object sender, EventArgs e)
        {
            Band();
            
        }

        protected void gv_AddressBook_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eidt")
            {
                Response.Redirect("AddressBookDetails.aspx?AID=" + e.CommandArgument + "");
            }
            else if (e.CommandName == "Updata")
            {
                Response.Redirect("AddressBookModify.aspx?AID=" + e.CommandArgument + "");
            }
            else if (e.CommandName == "Del")
            {
                AddressBook address = _AddressService.FindAddressBookByAID(e.CommandArgument.ToString());
                if (address != null)
                {
                    //_AddressService.RemoveAddressBook(address);                    
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('删除成功！')</script>");
                    //Band();
                }
            }
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
        public int N()
        {
            return i++;
        }
    }
}