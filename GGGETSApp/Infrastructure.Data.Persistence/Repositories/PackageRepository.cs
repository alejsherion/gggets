//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        运单包裹DAL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.12
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using ETS.GGGETSApp.Infrastructure.Data.Core;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.UnitOfWork;
using ETS.GGGETSApp.Infrastructure.CrossCutting.Logging;
using ETS.GGGETSApp.Domain.Application.Entities;
using Domain.GGGETS;
using System.Linq;

namespace ETS.GGGETSApp.Infrastructure.Data.Persistence.Repositories
{
    public class PackageRepository : Repository<Package>, IPackageRepository
    {
        public PackageRepository(IGGGETSAppUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager) { }
        /// <summary>
        /// 通过包裹编号获取对应包裹
        /// </summary>
        /// <param name="PID">包裹编号</param>
        /// <returns></returns>
        public Package GetSinglePackageByPid(string PID)
        {
            if (string.IsNullOrEmpty(PID)) throw new ArgumentException("PID is null!");
            //Get Assemble's Context
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            //transfer GUID
            Guid guidObj = new Guid(PID);
            //don't forget open package's load:HAWBs
            return context.Package.Where(p => p.PID == guidObj).Single();
        }
    }
}
