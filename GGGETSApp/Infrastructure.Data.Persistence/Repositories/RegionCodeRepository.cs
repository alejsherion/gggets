//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        地区三字码DAL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.23
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Globalization;
using ETS.GGGETSApp.Infrastructure.Data.Core;
using ETS.GGGETSApp.Infrastructure.Data.Core.Extensions;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.Resources;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.UnitOfWork;
using ETS.GGGETSApp.Infrastructure.CrossCutting.Logging;
using ETS.GGGETSApp.Domain.Application.Entities;
using Domain.GGGETS;
using System.Linq;
namespace ETS.GGGETSApp.Infrastructure.Data.Persistence.Repositories
{
    public class RegionCodeRepository : Repository<RegionCode>, IRegionCodeRepository
    {
        public RegionCodeRepository(IGGGETSAppUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager) { }

        /// <summary>
        /// 通过国家编号获取这个国家下的所有地区信息
        /// </summary>
        /// <param name="countryCode">国家编号</param>
        /// <returns></returns>
        public IList<RegionCode> FindRegionsByCountryCode(string countryCode)
        {
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
                return context.RegionCode.Where(it => it.CountryCode == countryCode).ToList();
            else
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name)); 
        }

        /// <summary>
        /// 通过国家编号模糊查询地区
        /// </summary>
        /// <param name="regionName">地区名称</param>
        /// <param name="countryCode">国家编号</param>
        /// <returns></returns>
        public IList<RegionCode> FindRegionsByCountryCodeAndRegionName(string regionName, string countryCode)
        {
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
                return context.RegionCode.Where(it => it.CountryCode == countryCode).Where(it => it.RegionName.StartsWith(regionName)).ToList();
            else
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name));
        }

        /// <summary>
        /// 根据地区三字码获取地区信息
        /// </summary>
        /// <param name="regionCode">地区三字码</param>
        /// <returns></returns>
        public RegionCode FindRegionByRegionCode(string regionCode)
        {
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
                return context.RegionCode.Where(it => it.RegionCode1 == regionCode).SingleOrDefault();
            else
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name));
        }

        /// <summary>
        /// 地区多条件查询
        /// </summary>
        /// <param name="countryCode">国家二字码</param>
        /// <param name="regioncode">地区三字码</param>
        /// <param name="regionName">地区全称</param>
        /// <returns></returns>
        public IList<RegionCode> FindRegionCodesByCondition(string countryCode, string regioncode, string regionName)
        {
            IEnumerable<RegionCode> regions = null;
            using (IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork)
            {
                if (context != null)
                {
                    regions = context.RegionCode.Select(r => r);
                    if (!string.IsNullOrEmpty(countryCode))
                        regions = regions.Where(r => r.CountryCode == countryCode);
                    if (!string.IsNullOrEmpty(regioncode))
                        regions = regions.Where(r => r.RegionCode1 == regioncode);
                    if (!string.IsNullOrEmpty(regionName))
                        regions = regions.Where(r => r.RegionName.StartsWith(regionName));
                }
                else
                {
                    throw new InvalidOperationException(string.Format(
                        CultureInfo.InvariantCulture,
                        Messages.exception_InvalidStoreContext,
                        GetType().Name));
                }
                return regions.ToList();
            }
        }
    }
}
