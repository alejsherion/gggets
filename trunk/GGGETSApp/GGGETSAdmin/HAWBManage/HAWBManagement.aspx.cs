using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;
namespace GGGETSAdmin.HAWBManage
{
    public partial class HAWBManagement : System.Web.UI.Page
    {
        private IHAWBManagementService _hawbService;
        protected static Regex RCountry = new Regex(@"^[A-Za-z]{2}");
        private static Regex RRegion = new Regex(@"^[A-Za-z]{3}");
        private static string Rtime = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29))$";
        private IList<HAWB> listHawb;

        protected HAWBManagement()
        { }
        public HAWBManagement(IHAWBManagementService hawbservice)
        {
            _hawbService = hawbservice;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btn_Demand_Click(object sender, EventArgs e)
        {
            string countryCode = string.Empty;
            string regionCode = string.Empty;
            string BarCode = string.Empty;
            string loginName = string.Empty;
            string departmentCode = string.Empty;
            string corporationName = string.Empty;
            string realName = string.Empty;
            string phone = string.Empty;
            DateTime beginTime=new DateTime();
            DateTime endTime=new DateTime();
            int settleType = -1;
            int serviceType = -1;
            bool isInternational;
            bool Ok = true;
            if (Txt_BarCode.Text.Trim() != "")
            {
                BarCode = Txt_BarCode.Text.Trim();
            }
            if (Txt_Country.Text.Trim() != "")
            {
                if (!RCountry.IsMatch(Txt_Country.Text))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母并为2位！')</script>");
                    Ok = false;
                    Txt_Country.Focus();
                }
                else
                { 
                    countryCode = Txt_Country.Text.Trim();
                    Ok = true;
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
            if (Txt_Account1.Text.Trim() != "")
            {
                loginName = Txt_Account1.Text.Trim();
            }
            if (Txt_Account2.Text.Trim() != "")
            {
                departmentCode = Txt_Account2.Text;
            }
            if (Txt_corporationName.Text.Trim() != "")
            {
                corporationName = Txt_corporationName.Text.Trim();
            }
            if (Txt_Contactor.Text.Trim() != "")
            {
                realName = Txt_Contactor.Text.Trim();
            }
            if (Txt_phone.Text.Trim() != "")
            {
                phone = Txt_phone.Text.Trim();
            }
            if (DDl_SettleType.SelectedValue != "-1")
            {
                settleType = int.Parse(DDl_SettleType.SelectedValue);
            }
            if (ddl_BoxType.SelectedValue != "-1")
            {
                serviceType = int.Parse(ddl_BoxType.SelectedValue.ToString());
            }
            if (ddl_HAWBType.SelectedValue != "1")
            {
                isInternational = true;
            }
            else
            {
                isInternational = false;
            }
            if (Txt_GetUpTime.Text.Trim() != "")
            {
                if (!Regex.IsMatch(Txt_GetUpTime.Text, Rtime))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入正确的日期！如：2010-02-16！')</script>");
                    Txt_GetUpTime.Focus();
                    Ok = false;
                }
                else
                {
                    beginTime = DateTime.Parse(Txt_GetUpTime.Text.Trim());
                    Ok = true;
                }
            }
            if (Txt_StopTime.Text.Trim() != "")
            {
                if (!Regex.IsMatch(Txt_StopTime.Text, Rtime))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入正确的日期！如：2010-02-16！')</script>");
                    Txt_StopTime.Focus();
                    Ok = false;
                }
                else
                {
                    endTime = DateTime.Parse(Txt_StopTime.Text.Trim());
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
            if (Ok == true)
            {
                listHawb = _hawbService.FindHAWBsByCondition(BarCode, countryCode, regionCode, loginName, departmentCode, corporationName, realName, phone, beginTime, endTime, settleType, serviceType, isInternational);
                if (listHawb.Count > 0)
                {

                    Gv_HAWB.DataSource = listHawb;
                    Gv_HAWB.DataBind();
                }
                else
                {
                    Gv_HAWB.DataSource = listHawb;
                    Gv_HAWB.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('没有相关记录！')</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请按提示操作！')</script>");
            }
        }

        protected void Gv_HAWB_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            if (e.CommandName == "Eidt")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string barCode = Gv_HAWB.DataKeys[index].Value.ToString();
                Response.Redirect("HAWBDetails.aspx?BarCode=" + barCode + "");
            }
            else if (e.CommandName == "Updata")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string barCode = Gv_HAWB.DataKeys[index].Value.ToString();
                int Update = 1;
                Response.Redirect("HAWBAdd.aspx?BarCode=" + barCode + "&update=" + Update + "");
            }
            else if (e.CommandName == "Del")
            {
                string barCode = e.CommandArgument.ToString();
                _hawbService.RemoveHAWB(barCode);
            }

        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("HAWBAdd.aspx");
        }
    }
}