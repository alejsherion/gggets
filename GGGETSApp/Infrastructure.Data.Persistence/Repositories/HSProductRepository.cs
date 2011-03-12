//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        HS海关商品编码DAL
// 作成者				ZhiWei.Shen
// 改版日				2011.03.12
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
    public class HSProductRepository : Repository<HSProduct>, IHSProductRepository
    {
        public HSProductRepository(IGGGETSAppUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager) { }
        /// <summary>
        /// 获取分页后所有海关商品编码信息
        /// </summary>
        /// <returns></returns>
        public IList<HSProduct> GetPagedAll(int pageIndex, int pageCount)
        {
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
            {
                context.Cache = new AspNetCache();
                context.CachingPolicy = CachingPolicy.CacheAll;
                return context.HSProduct.OrderBy(it=>it.HSCode).Skip(pageIndex*pageCount).Take(pageCount).ToList();
            }

            else
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name)); 
        }

        /// <summary>
        /// 通过HS编码获取商品信息
        /// </summary>
        /// <param name="HSCode">HS编码</param>
        /// <returns></returns>
        public HSProduct FindHSProductByHSCode(string HSCode)
        {
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
            {
                return context.HSProduct.Where(it => it.HSCode == HSCode).SingleOrDefault();
            }

            else
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name)); 
        }

        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="HSCode">HS编码</param>
        /// <param name="HSName">商品名称</param>
        /// <returns></returns>
        public IList<HSProduct> FindHSProductByCondition(string HSCode, string HSName, int pageIndex, int pageCount)
        {
            IEnumerable<HSProduct> products = null;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;

            if (context != null)
            {
                context.Cache = new AspNetCache();
                context.CachingPolicy = CachingPolicy.CacheAll;
                products = context.HSProduct.Select(p => p);
                if (!string.IsNullOrEmpty(HSCode)) products = products.Where(p => p.HSCode.StartsWith(HSCode));
                if (!string.IsNullOrEmpty(HSName)) products = products.Where(p => p.HSName.StartsWith(HSName));
            }
            else
            {
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name));
            }
            return products.OrderBy(p => p.HSID).Skip(pageIndex*pageCount).Take(pageCount).ToList();
        }

        public IList<HSProduct> FindHSProductByCondition(string HSCode, string HSName)
        {
            IEnumerable<HSProduct> products = null;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;

            if (context != null)
            {
                context.Cache = new AspNetCache();
                context.CachingPolicy = CachingPolicy.CacheAll;
                products = context.HSProduct.Select(p => p);
                if (!string.IsNullOrEmpty(HSCode)) products = products.Where(p => p.HSCode.StartsWith(HSCode));
                if (!string.IsNullOrEmpty(HSName)) products = products.Where(p => p.HSName.StartsWith(HSName));
            }
            else
            {
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name));
            }
            return products.OrderBy(p => p.HSID).ToList();
        }
    }
}
