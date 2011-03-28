using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;

namespace GGGETSAdmin.ProductManage
{
    public partial class ProductModify : System.Web.UI.Page
    { private IHSProductManagementService _HSProduct;
        private IHSPropertyManagementService _HSProperty;
        private HSProduct product;
        private HSProperty property;
        private static string RTax = @"^([1])$|^([1].[0]{2})$|^([0].[1-9][0-9])?$|^([0].[0-9][1-9])?$";
        protected ProductModify()
        { }
        public ProductModify(IHSProductManagementService HSProduct, IHSPropertyManagementService HSProperty)
        {
            _HSProduct = HSProduct;
            _HSProperty = HSProperty;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["HSCode"]))
                {
                    string HSCode = Request.QueryString["HSCode"];
                    ViewState["HSCode"] = HSCode;
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
                product = _HSProduct.LoadHSProductByHSCode(Code);
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
                    Session["product"] = product;
                }
            }
        }
        /// <summary>
        /// 验证编码和名称
        /// </summary>
        /// <param name="name">验证字段</param>
        /// <param name="type">类型：0,Product编码，1,Product名称,2 Property名称 </param>
        private bool Checking(string name, int type)
        {
            bool ok = false;
            if (type == 0)
            {
                ok = _HSProduct.JudgeHSCodeIsExist(name);
            }
            else if (type == 1)
            {
                ok = _HSProduct.JudgeHSNameIsExist(name);
            }
            else if (type == 2)
            {
                ok = _HSProperty.JudgeHSPropertyIsExist(name);
            }
            return ok;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Addparoduct_Click(object sender, EventArgs e)
        {
            if (product == null)
            {
                product = (HSProduct)Session["product"];
            }
            if (Txt_HSCode.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('HS编码不能为空!')", true);
                Txt_HSCode.Focus();
            }
            else if (Txt_HSName.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('商品名称不能为空!')", true);
                Txt_HSName.Focus();
            }
            else if (Txt_DiscountTax.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('优惠税率不能为空!')", true);
                Txt_DiscountTax.Focus();
            }
            else if (Txt_GeneralTax.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('普通税率不能为空!')", true);
                Txt_GeneralTax.Focus();
            }
            else if (Txt_ExportTax.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('出口税率不能为空!')", true);
                Txt_ExportTax.Focus();
            }
            else if (Txt_ConsumeTax.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('消费税率不能为空!')", true);
                Txt_ConsumeTax.Focus();
            }
            else if (Txt_RiseTax.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('增值税率不能为空!')", true);
                Txt_RiseTax.Focus();
            }
            else
            {
                if (!Regex.IsMatch(Txt_DiscountTax.Text.Trim(), RTax))
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('优惠税率只能输入0.01-1的数字且小数点后保留2位!')", true);
                    Txt_DiscountTax.Focus();
                }
                else if (!Regex.IsMatch(Txt_GeneralTax.Text.Trim(), RTax))
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('普通税率只能输入0.01-1的数字且小数点后保留2位!')", true);
                    Txt_GeneralTax.Focus();
                }
                else if (!Regex.IsMatch(Txt_ExportTax.Text.Trim(), RTax))
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('出口税率只能输入0.01-1的数字且小数点后保留2位!')", true);
                    Txt_ExportTax.Focus();
                }
                else if (!Regex.IsMatch(Txt_ConsumeTax.Text.Trim(), RTax))
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('消费税率只能输入0.01-1的数字且小数点后保留2位!')", true);
                    Txt_ConsumeTax.Focus();
                }
                else if (!Regex.IsMatch(Txt_RiseTax.Text.Trim(), RTax))
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('增值税率只能输入0.01-1的数字且小数点后保留2位!')", true);
                    Txt_RiseTax.Focus();
                }
                else
                {
                    //if (Checking(Txt_HSCode.Text.Trim().ToUpper(), 0))
                    //{
                    //    if (Checking(Txt_HSName.Text.Trim().ToUpper(), 1))
                    //    {
                            product.HSCode = Txt_HSCode.Text.Trim().ToUpper();
                            product.HSName = Txt_HSName.Text.Trim().ToUpper();
                            product.DiscountTax = decimal.Parse(Txt_DiscountTax.Text.Trim());
                            product.GeneralTax = decimal.Parse(Txt_GeneralTax.Text.Trim());
                            product.ExportTax = decimal.Parse(Txt_ExportTax.Text.Trim());
                            product.ConsumeTax = decimal.Parse(Txt_ConsumeTax.Text.Trim());
                            product.RiseTax = decimal.Parse(Txt_RiseTax.Text.Trim());
                            product.CertificateSign = Txt_CertificateSign.Text.Trim();
                            product.PricingSign = Txt_PricingSign.Text.Trim();
                            product.TaxDemandSign = Txt_TaxDemandSign.Text.Trim();
                            product.Remark = Txt_Remark.Text.Trim();
                            _HSProduct.ModifyHSProduct(product);
                            //foreach (HSProperty pt in product.HSProperty)
                            //{
                            //    _HSProperty.AddHSProperty(pt);
                            //}
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "Url()", true);
                            InitialControl(this.Controls);
                    //    }
                    //    else
                    //    {
                    //        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('该名称已经存请重新输入!')", true);
                    //        Txt_HSName.Focus();
                    //    }
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('该编码已经存在请重新输入!')", true);
                    //    Txt_HSCode.Focus();
                    //}
                }
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void But_Conel_Click(object sender, EventArgs e)
        {
            if (Gv_Productiy.Rows.Count == 0)
            {
                Response.Redirect("../Navigation.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请先删除该编码里的数据!')", true);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Close_Click(object sender, EventArgs e)
        {
        
            bool Ok = false;
            for (int i = Gv_Productiy.Rows.Count - 1; i > -1; i--)
            {
                string Bar = string.Empty;
                if (((CheckBox)Gv_Productiy.Rows[i].FindControl("chkId")).Checked)
                {
                    Ok = true;
                    
                    Bar = Gv_Productiy.DataKeys[i].Value.ToString();
                    HSProperty hsproperty = _HSProperty.FindHSPropertyByHSPID(Bar);
                    _HSProperty.RemoveHSProperty(hsproperty);
                    
                }
            }
            if (Ok == true)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('删除成功!')", true);
                product = _HSProduct.LoadHSProductByHSCode(Request.QueryString["HSCode"]);
                Session.Remove("product");
                Session["product"] = product;
                if (product.HSProperty.Count == 0)
                {
                    btn_Close.Visible = false;
                }
                Gv_Productiy.DataSource = product.HSProperty;
                Gv_Productiy.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请选择要移除的记录!')", true);
            }
        }
        /// <summary>
        /// 属性名添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            if (Txt_Property.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('属性名不能为空!')", true);
                Txt_Property.Focus();
            }
            else if (Checking(Txt_Property.Text.Trim(), 2))
            {
                if (product == null)
                {
                    product = (HSProduct)Session["product"];
                }
                HSProperty property = new HSProperty();
                property.HSPID = Guid.NewGuid();
                property.HSID = product.HSID;
                property.PropertyName = Txt_Property.Text.Trim();
                property.ChineseRemark = Txt_ChineseRemark.Text.Trim();
                _HSProperty.AddHSProperty(property);
                product.HSProperty.Add(property);
                Session["product"] = product;
                Gv_Productiy.DataSource = product.HSProperty;
                Gv_Productiy.DataBind();
                if (Gv_Productiy.Rows.Count > 0)
                {
                    DVProperty.Visible = true;
                }
                else
                {
                    DVProperty.Visible = false;
                }
                Txt_ChineseRemark.Text = string.Empty;
                Txt_Property.Text = string.Empty;
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('属性名已经存在，请重新输入!')", true);
                Txt_Property.Focus();
            }
        }
        /// <summary>
        /// 清空页面控件值
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
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Gv_Productiy_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (product == null)
            {
                product = (HSProduct)Session["product"];
            }
            Gv_Productiy.EditIndex = e.NewEditIndex;
            Gv_Productiy.DataSource = product.HSProperty;
            Gv_Productiy.DataBind();
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Gv_Productiy_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (product == null)
            {
                product = (HSProduct)Session["product"];
            }
            Gv_Productiy.EditIndex = -1;
            Gv_Productiy.DataSource = product.HSProperty;
            Gv_Productiy.DataBind();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Gv_Productiy_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string HScode=ViewState["HSCode"].ToString();
            if (HScode == Txt_HSCode.Text.Trim().ToUpper())
            {
                property = _HSProperty.FindHSPropertyByHSPID(Gv_Productiy.DataKeys[e.RowIndex].Value.ToString());
                property.PropertyName = ((TextBox)Gv_Productiy.Rows[e.RowIndex].FindControl("Txt_PropertyName")).Text.Trim();
                property.ChineseRemark = ((TextBox)Gv_Productiy.Rows[e.RowIndex].FindControl("Txt_ChineseRemark")).Text.Trim();
                _HSProperty.ModifyProperty(property);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改成功！')</script>");
                product = _HSProduct.FindHSProductByHSCode(HScode);
                Gv_Productiy.EditIndex = -1;
                Gv_Productiy.DataSource = product.HSProperty;
                Gv_Productiy.DataBind();
                Session.Remove("product");
                Session["product"] = product;
            }
        }
    }
}