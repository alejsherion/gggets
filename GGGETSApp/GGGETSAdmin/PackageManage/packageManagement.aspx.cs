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
        public int n = 1;
        private static DateTime beginTime = new DateTime();
        private static DateTime endTime = new DateTime();
        private string BarCode = string.Empty;
        private string regionCode = string.Empty;
        private IList<HAWB> listHawb;
        private readonly int PageCount = 35;//页面显示个数，固定不变。需要配置请修改此属性
        public int PageIndex //当期页码，会随着点击下一页，上一页进行动态变化
        {
            get { return (int)ViewState["pageIndex"]; }
            set { ViewState["pageIndex"] = value; }
        }
        private IPackageManagementService _packageservice;
        private static Regex RRegion = new Regex(@"^[A-Za-z]");
        private static string Rtime = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29))$";
        private IList<Package> listpackage;
        private static IRegionCodeManagementService _regionservice;
        protected packageManagement()
        { }
        public packageManagement(IPackageManagementService packageservice, IRegionCodeManagementService regionservice)
        {
            _packageservice = packageservice;
            _regionservice = regionservice;
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
            if (Txt_BagBarCode.Text.Trim() != "")
            {
                BarCode = Txt_BagBarCode.Text.Trim().ToUpper();
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
            if (txt_Destination.Text.Trim() != "")
            {
                if (!RRegion.IsMatch(txt_Destination.Text))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母并为3位！')</script>");
                    Ok = false;
                    txt_Destination.Focus();
                }
                else
                {
                    regionCode = txt_Destination.Text.Trim().ToUpper();
                    Ok = true;
                }
            }
            if (Ok == true)
            {
                listpackage = _packageservice.FindPackageByCondition(BarCode, beginTime, endTime, regionCode,pageIndex,pageCount,ref totalCount);
                ViewState["totalCount"] = totalCount;//返回总条数
                if (listpackage.Count > 0)
                {

                    gv_HAWB.DataSource = listpackage;
                    gv_HAWB.DataBind();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('没有相关记录！')</script>");
                    gv_HAWB.DataSource = null;
                    gv_HAWB.DataBind();
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请按提示操作！')</script>");
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
            if (ViewState["totalCount"]!=null)
            {
                lbl_nuber.Text = "1";
                lbl_sumnuber.Text = (((int)ViewState["totalCount"] + PageCount - 1) / PageCount).ToString();
                DataBound();
                if (gv_HAWB.Rows.Count < PageCount)
                {
                    FenYe.Visible = false;
                }
                else
                {
                    FenYe.Visible = true;
                }
            }
        }

        protected void btn_Close_Click(object sender, EventArgs e)
        {
            //if (package == null)
            //{
            //    package = (Package)Session["package"];
            //}
            //bool Ok = false;
            //for (int i = gv_HAWB.Rows.Count - 1; i > -1; i--)
            //{
            //    string Bar = string.Empty;
            //    if (((CheckBox)gv_HAWB.Rows[i].FindControl("chkId")).Checked)
            //    {
            //        Ok = true;
            //        Bar = gv_HAWB.DataKeys[i].Value.ToString();
            //        foreach (HAWB ha in package.HAWBs)
            //        {
            //            if (ha.BarCode == Bar)
            //            {
            //                hawb = ha;
            //            }
            //        }
            //        package.HAWBs.Remove(hawb);
            //        txt_Pice.Text = package.Piece.ToString();
            //        Txt_TotalWeight.Text = package.TotalWeight.ToString();
            //    }
            //}
            //if (Ok == true)
            //{
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('删除成功！')</script>");
            //    if (package.HAWBs.Count == 0)
            //    {
            //        btn_Close.Visible = false;
            //    }
            //    gv_HAWB.DataSource = package.HAWBs;
            //    gv_HAWB.DataBind();
            //}
            //else
            //{
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('请选择要删除的记录！')</script>");
            //}
        }
        /// <summary>
        /// 前台行号显示
        /// </summary>
        /// <returns></returns>
        public int N()
        {
           return n++;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[][] GetCountryList(string prefixText, int count)
        {
            if (count == 0)
            {
                count = 10;
            }

            List<string[]> items = new List<string[]>();

            //IList<RegionCode> regioncode = _regionservice
            //foreach (RegionCode region in regioncode)
            //{
            //    string[] ItemArry = new string[3];
            //    ItemArry[0] = region.RegionName;
            //    ItemArry[1] = region.RegionCode1;
            //    items.Add(ItemArry);
            //}
            return items.Take(count).ToArray();
        }

        protected void autocomplete_ItemSelected(object sender, EventArgs e)
        {
            txt_Destination.Text = ((AutoCompleteExtra.AutoCompleteExtraExtender)sender).SelectedValue;
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
    }
}