using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;

namespace GGGETSAdmin.PersonnelManage.UserManage
{
    public partial class UserManagemnet : System.Web.UI.Page
    {
        private static string Rtime = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29))$";
        private IUserManagementService _userService;
        protected UserManagemnet()
        { }
        public UserManagemnet(IUserManagementService userService)
        {
            _userService = userService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Demand_Click(object sender, EventArgs e)
        {
            string loginName = string.Empty;
            DateTime beginTime=new DateTime();
            DateTime endTime=new DateTime();
            bool Ok = true;//判断条件是否有误
            if (Txt_LoginName.Text.Trim() != "")
            {
                loginName = Txt_LoginName.Text.Trim();
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
                gv_User.DataSource = _userService.FindUsersByCondition(loginName, beginTime, endTime);//根据条件获取用户信息
                gv_User.DataBind();
                InitialControl(this.Controls);
            }
        }
        /// <summary>
        /// 数据操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gv_User_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = int.Parse(e.CommandArgument.ToString());
            GridViewRow row = gv_User.Rows[index];
            string loginName = ((Label)row.FindControl("lbl_LoginName") as Label).Text;
            if (e.CommandName == "Eidt")
            {
                Response.Redirect("UserDetails.aspx?LoginName=" + loginName + "");
            }
            else if (e.CommandName == "Updata")
            {
                Response.Redirect("UserModify.aspx?LoginName=" + loginName + "");
            }
            else if (e.CommandName == "Del")
            {
                
            }
        }
        /// <summary>
        /// 清空页面控件
        /// </summary>
        /// <param name="objControlCollection"></param>
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
    }
}