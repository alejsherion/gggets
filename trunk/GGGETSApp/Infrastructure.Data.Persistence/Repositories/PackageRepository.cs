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
using System.Globalization;
using System.Linq.Expressions;
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
    public class PackageRepository : BaseRepository<Package>, IPackageRepository
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
            return context.Package.Where(p => p.PID == guidObj).SingleOrDefault();
        }

        /// <summary>
        /// 包裹条件查询
        /// </summary>
        /// <param name="barCode">包号</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="destinationCode">目标三字码</param>
        /// <returns></returns>
        public IList<Package> FindPackageByCondition(string barCode, DateTime? beginDate, DateTime? endDate, string destinationCode)
        {
            IEnumerable<Package> packages = null;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            //using (IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork)
            //{
                if (context != null)
                {
                    //packages = context.Package.Include(p => p.HAWBs).Include(p => p.MAWB).Select(p => p);
                    packages = context.Package.Select(p => p);
                    //权限引入
                    Expression<Func<Package, bool>> predicate = GetCustomExpression<Package>("OriginalRegionCode", Domain.Core.CompareOperate.Equal);
                    packages = packages.Where(predicate.Compile());

                    if (!string.IsNullOrEmpty(barCode)) packages = packages.Where(p => p.BarCode == barCode);
                    if (!string.IsNullOrEmpty(destinationCode)) packages = packages.Where(p => p.DestinationRegionCode == destinationCode);
                    if (beginDate.HasValue)
                    {
                        if (beginDate.Value != DateTime.MinValue)
                            packages =
                                packages.Where(
                                    p =>
                                    p.CreateTime >=
                                    new DateTime(beginDate.Value.Year, beginDate.Value.Month, beginDate.Value.Day, 0, 0,
                                                 0));
                    }
                    if (endDate.HasValue)
                    {
                        if (endDate.Value != DateTime.MinValue)
                            packages =
                                packages.Where(
                                    p =>
                                    p.CreateTime <=
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
                return packages.OrderByDescending(p => p.CreateTime).ToList();
            //}
        }

        /// <summary>
        /// 包裹条件查询(支持分页)
        /// </summary>
        /// <param name="barCode">包号</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="destinationCode">目标三字码</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageCount">一页显示个数</param>
        /// <returns></returns>
        public IList<Package> FindPackageByCondition(string barCode, DateTime? beginDate, DateTime? endDate, string destinationCode, int pageIndex, int pageCount)
        {
            IEnumerable<Package> packages = null;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;

            if (context != null)
            {
                packages = context.Package.Select(p => p);
                //权限引入
                Expression<Func<Package, bool>> predicate = GetCustomExpression<Package>("OriginalRegionCode", Domain.Core.CompareOperate.Equal);
                packages = packages.Where(predicate.Compile());

                if (!string.IsNullOrEmpty(barCode)) packages = packages.Where(p => p.BarCode == barCode);
                if (!string.IsNullOrEmpty(destinationCode)) packages = packages.Where(p => p.DestinationRegionCode == destinationCode);
                if (beginDate.HasValue)
                {
                    if (beginDate.Value != DateTime.MinValue)
                        packages =
                            packages.Where(
                                p =>
                                p.CreateTime >=
                                new DateTime(beginDate.Value.Year, beginDate.Value.Month, beginDate.Value.Day, 0, 0,
                                             0));
                }
                if (endDate.HasValue)
                {
                    if (endDate.Value != DateTime.MinValue)
                        packages =
                            packages.Where(
                                p =>
                                p.CreateTime <=
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
            return packages.OrderByDescending(p => p.CreateTime).Skip(pageIndex*pageCount).Take(pageCount).ToList();
        }

        /// <summary>
        /// 通过MID获取包裹
        /// </summary>
        /// <param name="MID">总运单序号</param>
        /// <returns></returns>
        public IList<Package> FindPackagesByMID(string MID)
        {
            if (string.IsNullOrEmpty(MID)) throw new ArgumentException("MID is null!");
            //Get Assemble's Context
            IEnumerable<Package> packages = null;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            //don't forget open package's load:HAWBs
            packages = context.Package.Include(it => it.HAWBs);
            //权限引入
            Expression<Func<Package, bool>> predicate = GetCustomExpression<Package>("OriginalRegionCode", Domain.Core.CompareOperate.Equal);
            packages = packages.Where(predicate.Compile());

            return packages.Where(p => p.MID == new Guid(MID)).ToList();
        }

        /// <summary>
        /// 通过MID获取包裹(支持分页)
        /// </summary>
        /// <param name="MID">总运单序号</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageCount">一页显示个数</param>
        /// <returns></returns>
        public IList<Package> FindPackagesByMID(string MID, int pageIndex, int pageCount)
        {
            if (string.IsNullOrEmpty(MID)) throw new ArgumentException("MID is null!");
            //Get Assemble's Context
            IEnumerable<Package> packages = null;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            packages = context.Package.Include(it => it.HAWBs);
            //don't forget open package's load:HAWBs
            //权限引入
            Expression<Func<Package, bool>> predicate = GetCustomExpression<Package>("OriginalRegionCode", Domain.Core.CompareOperate.Equal);
            packages = packages.Where(predicate.Compile());

            return packages.Where(p => p.MID == new Guid(MID)).Skip(pageIndex * pageCount).Take(pageCount).ToList();
        }
    }
}
