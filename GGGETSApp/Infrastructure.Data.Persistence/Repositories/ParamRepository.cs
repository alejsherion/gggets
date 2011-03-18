//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        模板参数DAL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.28
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
    public class ParamRepository : Repository<Param>, IParamRepository
    {
        public ParamRepository(IGGGETSAppUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager) { }
        /// <summary>
        /// 获取对应模板的所有参数
        /// </summary>
        /// <param name="TID">模板编号</param>
        /// <returns></returns>
        public IList<Param> FindParamsByTID(string TID)
        {
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
                return context.Param.Where(it => it.TID == new Guid(TID)).ToList();
            else
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name));
        }

        /// <summary>
        /// 通过TID获取参数对象
        /// </summary>
        /// <param name="TID">TID</param>
        /// <returns></returns>
        public Param FindParamByTID(string TID)
        {
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
                return context.Param.Where(it => it.TID == new Guid(TID)).SingleOrDefault();
            else
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name));
        }

        public Param FindParamByTIDAndTag(string TID, int Tag)
        {
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
                return context.Param.Where(it => it.TID == new Guid(TID)).Where(it => it.Tag == Tag).SingleOrDefault();
            else
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name));
        }
    }
}
