//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        总运单DAL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.15
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
    public class MAWBRepository : Repository<MAWB>, IMAWBRepository
    {
        public MAWBRepository(IGGGETSAppUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager) { }
        /// <summary>
        /// 多条件查询总运单
        /// </summary>
        /// <param name="barCode">总运单编号</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public IList<MAWB> FindMAWBByCondition(string barCode, DateTime? beginDate, DateTime? endDate)
        {
            IEnumerable<MAWB> mawb = null;
            using (IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork)
            {
                if (context != null)
                {
                    mawb = context.MAWB.Select(m => m);
                    if (!string.IsNullOrEmpty(barCode)) mawb = mawb.Where(m => m.BarCode == barCode);
                    if (beginDate.HasValue)
                    {
                        if (beginDate.Value != DateTime.MinValue)
                            mawb =
                                mawb.Where(
                                    m =>
                                    m.CreateTime >=
                                    new DateTime(beginDate.Value.Year, beginDate.Value.Month, beginDate.Value.Day, 0, 0,
                                                 0));
                    }
                    if (endDate.HasValue)
                    {
                        if (endDate.Value != DateTime.MinValue)
                            mawb =
                                mawb.Where(
                                    m =>
                                    m.CreateTime <=
                                    new DateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day, 23, 59, 59));
                    }
                }
                else
                {
                    throw new InvalidOperationException(string.Format(
                                                                CultureInfo.InvariantCulture,
                                                                Messages.exception_InvalidStoreContext,
                                                                GetType().Name));
                }
                return mawb.OrderByDescending(m => m.CreateTime).ToList();
            }
        }
    }
}
