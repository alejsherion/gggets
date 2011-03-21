//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        打印管理界面
// 作成者				ZhiWei.Shen
// 改版日				2011.02.25
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;

namespace GGGETSAdmin.PrintManage
{
    public partial class HAWBPreview : System.Web.UI.Page
    {
        private IHAWBManagementService _hawbService;
        private ICountryCodeManagementService _countryService;
        private IRegionCodeManagementService _regionService;
        private IParamManagementService _paramService;
        private ITemplateManagementService _templateService;
        protected HAWBPreview()
        { }
        public HAWBPreview(IHAWBManagementService hawbservice, ICountryCodeManagementService countryService, IRegionCodeManagementService regionService, IParamManagementService paramService, ITemplateManagementService templateService)
        {
            _hawbService = hawbservice;
            _countryService = countryService;
            _regionService = regionService;
            _paramService = paramService;
            _templateService = templateService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Rebind();
            }
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        private void Rebind()
        {
            GVHAWBs.DataSource = GetConditionHAWBs();
            GVHAWBs.DataBind();
        }

        /// <summary>
        /// 多条件查询运单
        /// </summary>
        /// <returns></returns>
        private IList<HAWB> GetConditionHAWBs()
        {
            string HAWBTypeStr = ddl_HAWBType.SelectedValue;
            bool? judge = null;
            if(!HAWBTypeStr.Equals("-1"))
            {
                if(HAWBTypeStr.Equals("0")) judge = true;
                else judge = false;
            }
            IList<HAWB> HAWBs = _hawbService.FindHAWBsByCondition(Txt_BarCode.Text.Trim(), Txt_Country.Text.Trim(),
                                                                  Txt_Region.Text.Trim(), Txt_UserCode.Text.Trim(),
                                                                  Txt_corporationName.Text.Trim(),
                                                                  txt_RealName.Text.Trim(),
                                                                  Txt_GetUpTime.Text == string.Empty
                                                                      ? (DateTime?) null
                                                                      : Convert.ToDateTime(Txt_GetUpTime.Text.Trim()),
                                                                  Txt_StopTime.Text == string.Empty
                                                                      ? (DateTime?) null
                                                                      : Convert.ToDateTime(Txt_StopTime.Text.Trim()),
                                                                  Convert.ToInt16(DDl_SettleType.SelectedValue),
                                                                  Convert.ToInt16(ddl_BoxType.SelectedValue), judge);
            return HAWBs;
        }

        /// <summary>
        /// 通过国家编号获取国家信息
        /// </summary>
        /// <param name="countryCode">国家编号</param>
        /// <returns></returns>
        public string GetCountryNameByCode(string countryCode)
        {
            return _countryService.FindCountriedByCountryCode(countryCode).CountryName;
        }

        /// <summary>
        /// 通过地区编号获取地区信息
        /// </summary>
        /// <param name="regionCode">地区编号</param>
        /// <returns></returns>
        public string GetRegionNameByCode(string regionCode)
        {
            return _regionService.FindRegionByRegionCode(regionCode).RegionName;
        }

        /// <summary>
        /// 获取运单类型
        /// </summary>
        /// <param name="isInternational">运单类型</param>
        /// <returns></returns>
        public string GetHAWBType(bool isInternational)
        {
            if (isInternational) return "国外";
            return "国内";
        }

        /// <summary>
        /// 结算方式
        /// </summary>
        /// <param name="serviceType">结算方式</param>
        /// <returns></returns>
        public string GetServiceType(string serviceType)
        {
            string type = string.Empty;
            switch(serviceType)
            {
                case "0":
                    type = "预付月结";
                    break;
                case "1":
                    type = "预付现结";
                    break;
                case "2":
                    type = "到付月结";
                    break;
                default:
                    type = "到付现结";
                    break;
            }
            return type;
        }
        
        /// <summary>
        /// 获取包裹类型
        /// </summary>
        /// <param name="packageType">包裹类型</param>
        /// <returns></returns>
        public string GetPackageType(string packageType)
        {
            string type = string.Empty;
            switch (packageType)
            {
                case "0":
                    type = "文件";
                    break;
                case "1":
                    type = "小包裹";
                    break;
                default:
                    type = "普货";
                    break;
            }
            return type;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Demand_Click(object sender, EventArgs e)
        {
            Rebind();
        }

        /// <summary>
        /// 命令输出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GVHAWBs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //单打-打印预览
            if(e.CommandName=="Print")
            {
                Template template = _templateService.FindTemplateByTemplateCode("A1");
                //获取当前选中的HAWB对象编号
                string HID = e.CommandArgument.ToString();
                _paramService.PrintHAWB(Convert.ToInt32(template.PrintDirection), Convert.ToInt32(template.PagerWidth),
                                        Convert.ToInt32(template.PagerHeight), template.PaperType, HID, "A1",
                                        Convert.ToInt32(template.BatchHeight), 1, Page);
            }
            //单打-直接打印
            if(e.CommandName=="DirectPrint")
            {
                Template template = _templateService.FindTemplateByTemplateCode("A1");
                //获取当前选中的HAWB对象编号
                string HID = e.CommandArgument.ToString();
                _paramService.PrintHAWB(Convert.ToInt32(template.PrintDirection), Convert.ToInt32(template.PagerWidth),
                                        Convert.ToInt32(template.PagerHeight), template.PaperType, HID, "A1",
                                        Convert.ToInt32(template.BatchHeight), 2, Page);
            }
        }
    }
}