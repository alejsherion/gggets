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
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            //using (IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork)
            //{
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
            //}
        }

        /// <summary>
        /// 多条件查询总运单(支持分页)
        /// </summary>
        /// <param name="barCode">总运单编号</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageCount">一页显示个数</param>
        /// <returns></returns>
        public IList<MAWB> FindMAWBByCondition(string barCode, DateTime? beginDate, DateTime? endDate, int pageIndex, int pageCount)
        {
            IEnumerable<MAWB> mawb = null;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;

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
            return mawb.OrderByDescending(m => m.CreateTime).Skip(pageIndex*pageCount).Take(pageCount).ToList();
        }

        /// <summary>
        /// 通过航班号获取所有总运单
        /// </summary>
        /// <param name="flightNo">航班号</param>
        /// <returns></returns>
        public IList<MAWB> FindAllMAWBsByFlightNo(string flightNo)
        {
            if (string.IsNullOrEmpty(flightNo)) throw new ArgumentException("FlightNo is null!");
            //Get Assemble's Context
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            //don't forget open package's load:HAWBs
            return context.MAWB.Where(m => m.FlightNo == flightNo).ToList();
        }

        /// <summary>
        /// 通过航班号获取所有总运单(支持分页)
        /// </summary>
        /// <param name="flightNo">航班号</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageCount">一页显示个数</param>
        /// <returns></returns>
        public IList<MAWB> FindAllMAWBsByFlightNo(string flightNo, int pageIndex, int pageCount)
        {
            if (string.IsNullOrEmpty(flightNo)) throw new ArgumentException("FlightNo is null!");
            //Get Assemble's Context
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            //don't forget open package's load:HAWBs
            return context.MAWB.Where(m => m.FlightNo == flightNo).Skip(pageIndex*pageCount).Take(pageCount).ToList();
        }

        /// <summary>
        /// 通过MID获取MAWB
        /// </summary>
        /// <param name="MID">总运单编号</param>
        /// <returns></returns>
        public MAWB FindMAWBByMID(string MID)
        {
            if (string.IsNullOrEmpty(MID)) throw new ArgumentException("MID is null!");
            //Get Assemble's Context
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            //don't forget open package's load:HAWBs
            return context.MAWB.Where(m => m.MID == new Guid(MID)).SingleOrDefault();
        }

        /// <summary>
        /// 通过航班信息查询下面所有的总运单信息
        /// </summary>
        /// <param name="flightNo">航班编号</param>
        /// <param name="from">起始地字码</param>
        /// <param name="to">目的地字码</param>
        /// <returns></returns>
        public IList<MAWB> FindMAWBByFlightCondition(string flightNo, string from, string to)
        {
            IEnumerable<MAWB> mawbs = null;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
            {
                mawbs = context.MAWB.Select(m => m);
                if (!string.IsNullOrEmpty(flightNo)) mawbs = mawbs.Where(m => m.FlightNo == flightNo);
                if (!string.IsNullOrEmpty(from)) mawbs = mawbs.Where(m => m.From == from);
                if (!string.IsNullOrEmpty(to)) mawbs = mawbs.Where(m => m.To == to);
            }
            else
            {
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name));
            }
            return mawbs.OrderByDescending(m => m.CreateTime).ToList();
        }

        /// <summary>
        /// 通过航班信息查询下面所有的总运单信息(支持分页)
        /// </summary>
        /// <param name="flightNo">航班编号</param>
        /// <param name="from">起始地字码</param>
        /// <param name="to">目的地字码</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageCount">一页显示个数</param>
        /// <returns></returns>
        public IList<MAWB> FindMAWBByFlightCondition(string flightNo, string from, string to, int pageIndex, int pageCount)
        {
            IEnumerable<MAWB> mawbs = null;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
            {
                mawbs = context.MAWB.Select(m => m);
                if (!string.IsNullOrEmpty(flightNo)) mawbs = mawbs.Where(m => m.FlightNo == flightNo);
                if (!string.IsNullOrEmpty(from)) mawbs = mawbs.Where(m => m.From == from);
                if (!string.IsNullOrEmpty(to)) mawbs = mawbs.Where(m => m.To == to);
            }
            else
            {
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name));
            }
            return mawbs.OrderByDescending(m => m.CreateTime).Skip(pageIndex*pageCount).Take(pageCount).ToList();
        }
    }
}
