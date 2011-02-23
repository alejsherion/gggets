//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        地区三字码BLL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.23
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Core;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Domain.Core.Specification;

namespace Application.GGETS
{
    public class RegionCodeManagementService:IRegionCodeManagementService
    {
        /// <summary>
        /// IOC Injecting into
        /// </summary>
        IRegionCodeRepository _regionCodeRepository;
        public RegionCodeManagementService(IRegionCodeRepository regionCodeRepository)
        {
            _regionCodeRepository = regionCodeRepository;
        }

        /// <summary>
        /// 获取该国家下所有地区信息
        /// </summary>
        /// <param name="countryCode">国家编号</param>
        /// <returns></returns>
        public IList<RegionCode> FindRegionsByCountryCode(string countryCode)
        {
            return _regionCodeRepository.FindRegionsByCountryCode(countryCode);
        }
    }
}
