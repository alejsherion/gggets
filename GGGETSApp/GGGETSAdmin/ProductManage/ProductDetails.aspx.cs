using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace GGGETSAdmin.ProductManage
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        private IHSProductManagementService _HSProduct;
        private ISysUserManagementService _SysUserManagementService;
        protected ProductDetails()
        { }
        public ProductDetails(IHSProductManagementService HSProduct, ISysUserManagementService SysUserManagementService)
        {
            _HSProduct = HSProduct;
            _SysUserManagementService = SysUserManagementService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["HSCode"]))
                {
                    string HSCode = Request.QueryString["HSCode"];
                    Evaluate(HSCode);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('没有相关记录！');location='ProductManagemnet.aspx'</script>");
                }
            }
        }
        /// <summary>
        /// 页面控件赋值
        /// </summary>
        /// <param name="Code"></param>
        protected void Evaluate(string Code)
        {
            if (!string.IsNullOrEmpty(Code))
            {
                HSProduct product = _HSProduct.LoadHSProductByHSCode(Code);
                if (product != null)
                {
                    Txt_HSCode.Text = product.HSCode;
                    Txt_HSName.Text = product.HSName;
                    Txt_DiscountTax.Text = product.DiscountTax.ToString();
                    Txt_GeneralTax.Text = product.GeneralTax.ToString();
                    Txt_ExportTax.Text = product.ExportTax.ToString();
                    Txt_ConsumeTax.Text = product.ConsumeTax.ToString();
                    Txt_RiseTax.Text = product.RiseTax.ToString();
                    Txt_CertificateSign.Text = product.CertificateSign;
                    Txt_PricingSign.Text = product.PricingSign;
                    Txt_TaxDemandSign.Text = product.TaxDemandSign;
                    Txt_Remark.Text = product.Remark;
                    Gv_Productiy.DataSource = product.HSProperty;
                    Gv_Productiy.DataBind();
                }
            }
        }
        /// <summary>
        /// 修改按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Update_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductModify.aspx?HSCode=" + Txt_HSCode.Text + "&Privilege=" + Request.QueryString["Privilege"] + "");
        }
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void But_Conel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductManagemnet.aspx");
        }
    }
}