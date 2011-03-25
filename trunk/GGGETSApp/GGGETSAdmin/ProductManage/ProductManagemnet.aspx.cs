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
    public partial class ProductManagemnet : System.Web.UI.Page
    {
        private IHSProductManagementService _HSProduct;
        private IList<HSProduct> listProduct;
        private string HSCode = string.Empty;
        private string HSName = string.Empty;
        private readonly int PageCount = 35;//页面显示个数，固定不变。需要配置请修改此属性
        public int PageIndex //当期页码，会随着点击下一页，上一页进行动态变化
        {
            get { return (int)ViewState["pageIndex"]; }
            set { ViewState["pageIndex"] = value; }
        }
        protected ProductManagemnet()
        { }
        public ProductManagemnet(IHSProductManagementService HSProduct)
        {
            _HSProduct = HSProduct;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 数据源控件绑定
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        protected void Band(int pageIndex, int pageCount)
        {
            bool Ok = true;
            int totalCount = 0;//总页数
            if (Txt_HSCode.Text.Trim() != "")
            {
                HSCode = Txt_HSCode.Text.Trim().ToUpper();
            }
            if (Txt_HSName.Text.Trim() != "")
            {
                HSName = Txt_HSName.Text.Trim().ToUpper();
            }
            listProduct = _HSProduct.FindHSProductByCondition(HSCode, HSName, pageIndex, pageCount,ref totalCount);
            ViewState["totalCount"] = totalCount;//返回总条数
            if (listProduct.Count > 0)
            {

                gv_HS.DataSource = listProduct;
                gv_HS.DataBind();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('没有相关记录！')</script>");
                gv_HS.DataSource = null;
                gv_HS.DataBind();
            }

        }
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Demand_Click(object sender, EventArgs e)
        {
            PageIndex = 0;
            Band(PageIndex, PageCount);
            if (ViewState["totalCount"] != null)
            {
                lbl_nuber.Text = "1";
                lbl_sumnuber.Text = (((int)ViewState["totalCount"] + PageCount - 1) / PageCount).ToString();
                DataBound();
                if (gv_HS.Rows.Count < PageCount)
                {
                    FenYe.Visible = false;
                }
                else
                {
                    FenYe.Visible = true;
                }
            }
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Up_Click(object sender, EventArgs e)
        {
            btn_down.Enabled = true;
            PageIndex = PageIndex - 1;
            Band(PageIndex, PageCount);
            lbl_nuber.Text = (int.Parse(lbl_nuber.Text) - 1).ToString();
            DataBound();
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_down_Click(object sender, EventArgs e)
        {
            btn_Up.Enabled = true;
            PageIndex = PageIndex + 1;
            Band(PageIndex, PageCount);
            lbl_nuber.Text = (int.Parse(lbl_nuber.Text) + 1).ToString();
            DataBound();
        }
        /// <summary>
        /// 上下页按钮控制方法
        /// </summary>
        protected void DataBound()
        {
            if (PageIndex <= 0)
            {
                btn_Up.Enabled = false;
            }
            else
            {
                btn_Up.Enabled = true;
            }
            if (lbl_sumnuber.Text == lbl_nuber.Text)
            {

                btn_down.Enabled = false;
            }
            else
            {
                btn_down.Enabled = true;
            }
        }
        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_homepage_Click(object sender, EventArgs e)
        {
            PageIndex = 0;
            Band(PageIndex, PageCount);
            lbl_nuber.Text = "1";
            DataBound();
        }
        /// <summary>
        /// 跳转到几页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Jumpto_Click(object sender, EventArgs e)
        {
            if (int.Parse(Txt_Jumpto.Text.Trim()) <= 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('最小页数为1,请重新输入！')</script>");
                Txt_Jumpto.Focus();
            }
            else if (int.Parse(Txt_Jumpto.Text.Trim()) > int.Parse(lbl_sumnuber.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('超过最大页数请重新输入！')</script>");
                Txt_Jumpto.Focus();
            }
            else
            {
                PageIndex = int.Parse(Txt_Jumpto.Text.Trim()) - 1;
                Band(PageIndex, PageCount);
                lbl_nuber.Text = Txt_Jumpto.Text.Trim();
                DataBound();
            }
            Txt_Jumpto.Text = string.Empty;
        }
        /// <summary>
        /// 末页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_lastpage_Click(object sender, EventArgs e)
        {
            PageIndex = int.Parse(lbl_sumnuber.Text) - 1;
            Band(PageIndex, PageCount);
            lbl_nuber.Text = lbl_sumnuber.Text;
            DataBound();
        }

        protected void gv_HS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                
            }
        }

        protected void gv_HS_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}