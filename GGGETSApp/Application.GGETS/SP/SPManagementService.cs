//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        存储过程调用
// 作成者				zhiwei.shen
// 改版日				2011.04.22
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace Application.GGETS
{
    /// <summary>
    /// 状态定义：
    /// 1-批量成功 -1-批量失败，会自动回滚
    /// </summary>
    public class SPManagementService:ISPManagementService
    {
        /// <summary>
        /// IOC Injecting into
        /// </summary>
        ISPRepository _spRepository;
        public SPManagementService(ISPRepository spRepository)
        {
            _spRepository = spRepository;
        }
        /// <summary>
        /// 批量更新运单报关状态
        /// 1-批量成功 -1-批量失败，会自动回滚
        /// </summary>
        /// <param name="xmlStr">XML解析字符串</param>
        /// <param name="mawbCode">总运单编号</param>
        /// <returns></returns>
        public int UseBatchUpdateCustomsClearanceState(string xmlStr, string mawbCode)
        {
            return _spRepository.UseBatchUpdateCustomsClearanceState(xmlStr, mawbCode);
        }

        /// <summary>
        /// 批量更新拆包后的运单状态和包编号
        /// </summary>
        /// <param name="xmlStr">XML解析字符串</param>
        public int UseUseBatchUpdateHAWBPackageState(string xmlStr)
        {
            return _spRepository.UseBatchUpdateHAWBPackageState(xmlStr);
        }

        /// <summary>
        /// 批量更新运单路单编号
        /// </summary>
        /// <param name="xmlStr">XML运单编号字符串</param>
        /// <param name="waybill">路单编号</param>
        /// <returns></returns>
        public int UseBatchUpdateWayBillCode(string xmlStr, string waybill)
        {
            return _spRepository.UseBatchUpdateWayBillCode(xmlStr, waybill);
        }
    }
}
