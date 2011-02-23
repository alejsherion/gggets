//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        国家二字码DAL
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
    public class CountryCodeRepository : Repository<CountryCode>, ICountryCodeRepository
    {
        public CountryCodeRepository(IGGGETSAppUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager) { }
        
        /// <summary>
        /// 获取所有的国家信息
        /// </summary>
        /// <returns></returns>
        public IList<CountryCode> FindAllCountries()
        {
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
                return context.CountryCode.ToList();
            else
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name));
        }

        /// <summary>
        /// 通过国家名字模糊查询国家信息
        /// </summary>
        /// <param name="countryName">国家名称</param>
        /// <returns></returns>
        public IList<CountryCode> FindCountriedByCountryName(string countryName)
        {
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
                return context.CountryCode.Where(c=>c.CountryName.StartsWith(countryName)).ToList();
            else
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name));
        }
    }
}
