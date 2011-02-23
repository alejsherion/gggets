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
    }
}
