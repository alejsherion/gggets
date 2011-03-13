//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        HS分配DAL
// 作成者				ZhiWei.Shen
// 改版日				2011.03.13
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Globalization;
using EFCachingProvider.Caching;
using EFCachingProvider.Web;
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
    public class HSRelationRepository : Repository<HSRelation>, IHSRelationRepository
    {
        public HSRelationRepository(IGGGETSAppUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager) { }
        /// <summary>
        /// 通过商品编码获取对应的品名集合
        /// </summary>
        /// <param name="HSID">商品编码</param>
        /// <returns></returns>
        public IList<HSRelation> FindHSRelationsByHSID(string HSID)
        {
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
            {
                context.Cache = new AspNetCache();
                context.CachingPolicy = CachingPolicy.CacheAll;
                return context.HSRelation.Where(it=>it.HSID==new Guid(HSID)).ToList();
            }

            else
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name)); 
        }
    }
}
