//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        批量套打
// 作成者				ZhiWei.Shen
// 改版日				2011.02.25
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace GGGETSAdmin.PrintManage
{
    public partial class BatchingPrint : System.Web.UI.Page
    {
        private IHAWBManagementService _hawbService;
        private IParamManagementService _paramService;
        private ITemplateManagementService _templateService;
        protected BatchingPrint()
        { }
        public BatchingPrint(IHAWBManagementService hawbservice, IParamManagementService paramService, ITemplateManagementService templateService)
        {
            _hawbService = hawbservice;
            _paramService = paramService;
            _templateService = templateService;
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            // When using the ListBox with UpdatePanels, you should disable 
            // partial rendering for FF < v1.5. If you decide not to, reloads
            // (F5) can still cause client / server loss of synchronization.
            // The least you should do is disable page caching for FF < v1.5.
            if (Request.Browser.Browser.Equals("Firefox")
                && Request.Browser.MajorVersion < 2
                && Request.Browser.MinorVersion < 0.5)
            {
                ScriptManager.GetCurrent(Page).EnablePartialRendering = false;
                //Response.Cache.SetNoStore();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Bind("", null, null);
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            this.moveSelectedItems(lbLeft, lbRight, false);
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            this.moveSelectedItems(lbRight, lbLeft, false);
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            this.moveSelectedItems(lbLeft, lbRight, true);
        }
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            this.moveSelectedItems(lbRight, lbLeft, true);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Bind(txtHAWBBarcode.Text.Trim(),
                 txtBeginDate.Text == string.Empty ? (DateTime?) null : Convert.ToDateTime(txtBeginDate.Text.Trim()),
                 txtEndDate.Text == string.Empty ? (DateTime?) null : Convert.ToDateTime(txtEndDate.Text.Trim()));
        }

        /// <summary>
        /// 批量套打
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBatchPrint_Click(object sender, EventArgs e)
        {
            int count = lbRight.Items.Count;
            if (count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('请选择需要套打的运单!');", true);
                return;
            }
            //组织逗号分隔字符串
            StringBuilder sb = new StringBuilder();
            int judgeCount = 0;
            
            foreach(ListItem item in lbRight.Items)
            {
                judgeCount++;
                sb.Append(item.Value);
                if (count != judgeCount) sb.Append(",");
            }

            //执行批量套打通用方法
            Template template = _templateService.FindTemplateByTemplateCode("A2");
            _paramService.PrintHAWB(Convert.ToInt32(template.PrintDirection), Convert.ToInt32(template.PagerWidth),
                                        Convert.ToInt32(template.PagerHeight), template.PaperType, sb.ToString(), "A2",
                                        Convert.ToInt32(template.BatchHeight), 1, Page);
        }

        #region Private
        private void moveSelectedItems(ListBox source, ListBox target, bool moveAllItems)
        {
            // loop through all source items to find selected ones
            for (int i = source.Items.Count - 1; i >= 0; i--)
            {
                ListItem item = source.Items[i];

                if (moveAllItems)
                    item.Selected = true;

                if (item.Selected)
                {
                    // if the target already contains items, loop through
                    // them to place this new item in correct sorted order
                    if (target.Items.Count > 0)
                    {
                        for (int ii = 0; ii < target.Items.Count; ii++)
                        {
                            if (target.Items[ii].Text.CompareTo(item.Text) > 0)
                            {
                                target.Items.Insert(ii, item);
                                item.Selected = false;
                                break;
                            }
                        }
                    }

                    // if item is still selected, it must be appended
                    if (item.Selected)
                    {
                        target.Items.Add(item);
                        item.Selected = false;
                    }

                    // remove the item from the source list
                    source.Items.Remove(item);
                }
            }
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        private void Bind(string barcode, DateTime? beginDate, DateTime? endDate)
        {
            lbLeft.DataSource = _hawbService.FindHAWBsByCondition(barcode, beginDate, endDate);
            lbLeft.DataTextField = "BarCode";
            lbLeft.DataValueField = "HID";
            lbLeft.DataBind();
        }
        #endregion
    }
}