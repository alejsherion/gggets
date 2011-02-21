//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        航班DAL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.21
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
    public class FlightRepository : Repository<Flight>, IFlightRepository
    {
        public FlightRepository(IQueryableUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
        }

        /// <summary>
        /// 航班多条件查询
        /// </summary>
        /// <param name="flightNo">航班号</param>
        /// <param name="from">出发地三字码</param>
        /// <param name="to">目的地三字码</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public IList<Flight> FindFlightByCondition(string flightNo, string from, string to, DateTime? beginDate, DateTime? endDate)
        {
            IEnumerable<Flight> flights = null;
            using (IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork)
            {
                if (context != null)
                {
                    flights = context.Flight.Select(f => f);
                    if (!string.IsNullOrEmpty(flightNo)) flights = flights.Where(f => f.FlightNo == flightNo);
                    if (!string.IsNullOrEmpty(from)) flights = flights.Where(f => f.From == from);
                    if (!string.IsNullOrEmpty(to)) flights = flights.Where(f => f.To == to);
                    if (beginDate.HasValue)
                    {
                        if (beginDate.Value != DateTime.MinValue)
                            flights =
                                flights.Where(
                                    f =>
                                    f.TakeOffTime >=
                                    new DateTime(beginDate.Value.Year, beginDate.Value.Month, beginDate.Value.Day, 0, 0,
                                                 0));
                    }
                    if (endDate.HasValue)
                    {
                        if (endDate.Value != DateTime.MinValue)
                            flights =
                                flights.Where(
                                    f =>
                                    f.TakeOffTime <=
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
                return flights.OrderByDescending(f => f.TakeOffTime).ToList();
            }
        }

        public Flight GetSingleFlightByFlightNo(string flightNo)
        {
            throw new NotImplementedException();
        }
    }
}
