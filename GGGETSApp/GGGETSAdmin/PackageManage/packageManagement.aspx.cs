using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;

namespace GGGETSAdmin.PackageManage
{
    public partial class packageManagement : System.Web.UI.Page
    {
        protected IPackageManagementService _packageservice;
        private static Regex RRegion = new Regex(@"^[A-Za-z]{3}");
        private static string Rtime = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29))$";
        protected packageManagement()
        { }
        public packageManagement(IPackageManagementService packageservice)
        {
            _packageservice = packageservice;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Demand_Click(object sender, EventArgs e)
        {
            DateTime beginTime = new DateTime();
            DateTime endTime = new DateTime();
            string BarCode = string.Empty;
            string regionCode = string.Empty;
            bool Ok = true;
            if (Txt_BagBarCode.Text != "")
            {
                BarCode = Txt_BagBarCode.Text;
            }
            if (txt_UpCreateTime.Text.Trim() != "")
            {
                if (!Regex.IsMatch(txt_UpCreateTime.Text, Rtime))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入正确的日期！如：2010-02-16！')</script>");
                    txt_UpCreateTime.Focus();
                    Ok = false;
                }
                else
                {
                    beginTime = DateTime.Parse(txt_UpCreateTime.Text.Trim());
                    Ok = true;
                }
            }
            if (txt_ToCreateTime.Text.Trim() != "")
            {
                if (!Regex.IsMatch(txt_ToCreateTime.Text, Rtime))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入正确的日期！如：2010-02-16！')</script>");
                    txt_ToCreateTime.Focus();
                    Ok = false;
                }
                else
                {
                    endTime = DateTime.Parse(txt_ToCreateTime.Text.Trim());
                    if (beginTime.CompareTo(endTime) == 1)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('起始日期不能大于结束日期！')</script>");
                        Ok = false;
                        txt_ToCreateTime.Focus();
                    }
                    else
                    {
                        Ok = true;
                    }
                }
            }
            if (Txt_Region.Text.Trim() != "")
            {
                if (!RRegion.IsMatch(Txt_Region.Text))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母并为3位！')</script>");
                    Ok = false;
                    Txt_Region.Focus();
                }
                else
                {
                    regionCode = Txt_Region.Text.Trim();
                    Ok = true;
                }
            }
            if (Ok == true)
            {
                
                //Package FindPackage = _packageservice.FindPackageByBarcode(
                //if (Package.Count > 0)
                //{

                //    gv_HAWB DataSource = Package;
                //    gv_HAWB.DataBind();
                //}
                //else
                //{
                //    gv_HAWB.DataSource = Package;
                //    gv_HAWB.DataBind();
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('没有相关记录！')</script>");
                //}
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请按提示操作！')</script>");
            }
        }

        protected void btn_Close_Click(object sender, EventArgs e)
        {
            for (int i = gv_HAWB.Rows.Count - 1; i > -1; i--)
            {
                string Bar = string.Empty;
                if (((CheckBox)gv_HAWB.Rows[i].FindControl("chkId")).Checked)
                {
                    Bar = gv_HAWB.DataKeys[i].Value.ToString();
                   
                }
            }
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('删除成功！')</script>");
            if (gv_HAWB.Rows.Count == 0)
            {
                btn_Close.Visible = false;
            }
            //gv_HAWB.DataSource = package.HAWBs;
            //gv_HAWB.DataBind();
        }
    }
}