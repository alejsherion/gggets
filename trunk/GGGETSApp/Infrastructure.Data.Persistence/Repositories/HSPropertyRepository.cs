//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        HS品名管理DAL
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
    public class HSPropertyRepository : Repository<HSProperty>, IHSPropertyRepository
    {
        public HSPropertyRepository(IGGGETSAppUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager) { }
        /// <summary>
        /// 获取所有品名信息
        /// </summary>
        /// <returns></returns>
        public IList<HSProperty> GetAll()
        {
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
            {
                context.Cache = new AspNetCache();
                context.CachingPolicy = CachingPolicy.CacheAll;
                return context.HSProperty.OrderBy(it => it.PropertyName).ToList();
            }

            else
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name)); 
        }

        /// <summary>
        /// 模糊条件查询
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public IList<HSProperty> GetHSPropertiesByCondition(string propertyName)
        {
            IEnumerable<HSProperty> HSPropertyList = null;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;

            if (context != null)
            {
                HSPropertyList = context.HSProperty.Select(p => p);
                if (!string.IsNullOrEmpty(propertyName)) HSPropertyList = HSPropertyList.Where(p => p.PropertyName.StartsWith(propertyName));
            }
            else
            {
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name));
            }
            return HSPropertyList.OrderByDescending(p => p.PropertyName).ToList();
        }

        /// <summary>
        /// 通过GUID获取对应实体
        /// </summary>
        /// <param name="HSPID">GUID序号</param>
        /// <returns></returns>
        public HSProperty FindHSPropertyByHSPID(string HSPID)
        {
            if (string.IsNullOrEmpty(HSPID)) throw new ArgumentException("HSPID is null!");
            //Get Assemble's Context
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            //transfer GUID
            Guid guidObj = new Guid(HSPID);
            //don't forget open package's load:HAWBs
            return context.HSProperty.Where(p => p.HSPID == guidObj).SingleOrDefault();
        }
    }
}
