//************************************************************************
// 用户名				GETS国际综合快递
// 系统名				管理后台
// 子系统名		        报关主界面
// 作成者				ZhiWei.Shen
// 改版日				2011.04.18
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace GGGETSAdmin.CustomsClearance
{
    public partial class ClearanceManage : System.Web.UI.Page
    {
        //ioc regester
        private IMAWBManagementService _mawbService;
        protected ClearanceManage()
        { }
        public ClearanceManage(IMAWBManagementService mawbService)
        {
            _mawbService = mawbService;
        }
        //Flight No
        private string FlightNo
        {
            set; get;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            FlightNo = this.txtFlightNo.Text.Trim();//bind Flight Property
            Bind();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Bind();
        }

        private void Bind()
        {
            RGMAWB.DataSource = GetMawbSourceByFlightNo(FlightNo);
            RGMAWB.DataBind();
        }

        #region Private Block
        /// <summary>
        /// Get MAWB BY FLIGHT NO
        /// </summary>
        /// <returns></returns>
        private IList<MAWB> GetMawbSourceByFlightNo(string flightNo)
        {
            IList<MAWB> mawbs = new List<MAWB>();
            if (string.IsNullOrEmpty(flightNo)) return mawbs;
            
            if (_mawbService != null)
            {
                mawbs = _mawbService.FindAllMAWBsByFlightNo(flightNo);
            }
            return mawbs;
        }

        /// <summary>
        /// Get Chinese Status OF MAWBs
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public string GetStatusStr(string status)
        {
            string statusStr = "未定义";
            if(typeof(MAWBStatus).IsEnum)
            {
                statusStr = typeof(MAWBStatus).GetEnumName(Convert.ToInt32(status));
            }
            return statusStr;
        }
        #endregion

        #region ServiceEvent Block
        /// <summary>
        /// 导出报关文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbExport_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}