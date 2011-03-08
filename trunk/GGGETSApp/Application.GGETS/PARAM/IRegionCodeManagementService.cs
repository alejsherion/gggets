//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        地区三字码IBLL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.23
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace Application.GGETS
{
    public interface IRegionCodeManagementService
    {
        IList<RegionCode> FindRegionsByCountryCode(string countryCode);
        IList<RegionCode> FindRegionsByCountryCodeAndRegionName(string regionName, string countryCode);
        void AddRegionCode(RegionCode regionCode);
        void ModifyRegionCode(RegionCode regionCode);
        RegionCode FindRegionByRegionCode(string regionCode);
        void RemoveRegionCode(RegionCode regionCode);
        IList<RegionCode> FindRegionCodesByCondition(string countryCode, string regioncode, string regionName);
        IList<RegionCode> FindRegionCodesByCondition(string countryCode, string regioncode, string regionName, int pageIndex, int pageCount,ref int totalCount);
        IList<RegionCode> FindAllRegionCodes();
    }
}
