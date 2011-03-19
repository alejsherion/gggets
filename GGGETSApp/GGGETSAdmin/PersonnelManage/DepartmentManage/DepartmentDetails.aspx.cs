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
    public partial class DepartmentDetails : System.Web.UI.Page
    {
        private IDepartmentManagementService _deparService;
        protected DepartmentDetails()
        { }
        public DepartmentDetails(IDepartmentManagementService deparservice)
        {
            _deparService = deparservice;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["DeparCode"]) && !string.IsNullOrEmpty(Request.QueryString["CompanyCode"]))
                {

                    string CompanyCode = Request.QueryString["CompanyCode"].ToString();
                    string DeparCode = Request.QueryString["DeparCode"].ToString();
                    Storage(DeparCode,CompanyCode);
                    ViewState["Url"] = Request.UrlReferrer.ToString();

                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('没有相关记录！');location='DepartmentManagemnet.aspx'</script>");

                }
            }
        }
        /// <summary>
        /// 页面控件赋值
        /// </summary>
        /// <param name="deparcode">部门账号</param>
        /// <param name="CompanyCode">公司账号</param>
        protected void Storage(string deparcode, string CompanyCode)
        {
            Department depar = _deparService.FindDepartmentByDepCodeAndCompanyCode(deparcode, CompanyCode);
            if (depar != null)
            {
                Txt_CompanyCode.Text = depar.CompanyCode;
                Txt_DepCode.Text = depar.DepCode;
                Txt_DepName.Text = depar.DepName;
                if (depar.FeeDiscountType == 0)
                {
                    Txt_FeeDiscountType.Text = "灵活折扣";
                }
                else
                {
                    Txt_FeeDiscountType.Text = "固定折扣";
                }
                Txt_FeeDiscountRate.Text = depar.FeeDiscountRate.ToString();
                if (depar.WeightDiscountType == 0)
                {
                    Txt_WeightDiscountType.Text = "灵活折扣";
                }
                else
                {
                    Txt_WeightDiscountType.Text = "固定折扣";
                }
                Txt_WeightDiscountRate.Text = depar.WeightDiscountRate.ToString();
                switch (depar.SettleType)
                {
                    case 0:
                        Txt_SettleType.Text = "预付月结";
                        break;
                    case 1:
                        Txt_SettleType.Text = "预付现结";
                        break;
                    case 2:
                        Txt_SettleType.Text = "到付月结";
                        break;
                    case 3:
                        Txt_SettleType.Text = "到付现结";
                        break;
                }
                if (depar.WeightCalType == 0)
                {
                    Txt_WeightCalType.Text = "按照0.5KG标准";
                }
                else
                {
                    Txt_WeightCalType.Text = "按照分段标准";
                }
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void But_Next_Click(object sender, EventArgs e)
        {
            Response.Redirect("DepartmentModify.aspx?DeparCode=" + Txt_DepCode.Text.Trim() + "&CompanyCode=" + Txt_CompanyCode.Text + "");
        }
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void But_Conel_Click(object sender, EventArgs e)
        {
            Response.Redirect((string)ViewState["Url"]);
        }
    }
}