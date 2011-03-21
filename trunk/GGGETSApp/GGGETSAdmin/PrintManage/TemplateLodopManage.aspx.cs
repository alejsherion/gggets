//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        打印维护界面
// 作成者				ZhiWei.Shen
// 改版日				2011.02.28
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;

namespace GGGETSAdmin.PrintManage
{
    public partial class TemplateLodopManage : System.Web.UI.Page
    {
        private IParamManagementService _paramService;
        private ITemplateManagementService _templateService;
        protected TemplateLodopManage()
        { }
        public TemplateLodopManage(IParamManagementService paramService, ITemplateManagementService templateService)
        {
            _paramService = paramService;
            _templateService = templateService;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMaintain_Click(object sender, EventArgs e)
        {
            //获取参数TID
            string TID = Request["TID"];
            //获取模板对象
            Template template = _templateService.FindTemplateByTID(TID);
            if (template != null)
                _paramService.PrintHAWB(Convert.ToInt32(template.PrintDirection), Convert.ToInt32(template.PagerWidth),
                                        Convert.ToInt32(template.PagerHeight), template.PaperType, "",
                                        template.TemplateCode, Convert.ToInt32(template.BatchHeight), 0, Page);
        }

        /// <summary>
        /// 更新打印状态
        /// </summary>
        /// <param name="ItemTop">上边距</param>
        /// <param name="ItemLeft">左边距</param>
        /// <param name="ItemWidth">宽度</param>
        /// <param name="ItemHeight">高度</param>
        /// <param name="ItemFontSize">字体大小</param>
        /// <param name="ItemFontName">字体名</param>
        /// <param name="ItemAlignment">对齐方式</param>
        /// <param name="Itembold">是否粗体</param>
        /// <param name="ItemItalic">是否斜体</param>
        /// <param name="ItemUnderline">是否下划线</param>
        /// <param name="Tag">参数队列位置</param>
        /// <param name="RequestParam">前面的页面传递过来的参数</param>
        /// <returns></returns>
        [WebMethod]
        public static string GetItemClassName(string ItemTop, string ItemLeft, string ItemWidth, string ItemHeight, string ItemFontName, string ItemFontSize, string ItemAlignment, string Itembold, string ItemItalic, string ItemUnderline, int Tag, string RequestParam)
        {
            string sqlStr =
                string.Format(
                    "update Param set [Top]={0},[Left]={1},Width={2},Height={3},FontName='{4}',FontSize={5},Alignment={6},Bold={7},Italic={8},Underline={9} where TID='{10}' and Tag={11}",
                    ItemTop, ItemLeft, ItemWidth, ItemHeight, ItemFontName, ItemFontSize, ItemAlignment, Itembold,
                    ItemItalic, ItemUnderline, RequestParam, Tag);
            //执行update语句
            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sqlStr);
            return Convert.ToString(result);
        }
    }
}