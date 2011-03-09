using System;
using System.Collections.Generic;
using System.Linq;
using ETS.GGGETSApp.Infrastructure.Data.Core;
using ETS.GGGETSApp.Infrastructure.Data.Core.Extensions;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.UnitOfWork;
using ETS.GGGETSApp.Infrastructure.CrossCutting.Logging;
using ETS.GGGETSApp.Domain.Application.Entities;
using Domain.GGGETS;
using System.Globalization;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.Resources;

namespace ETS.GGGETSApp.Infrastructure.Data.Persistence.Repositories
{
    public class HAWBRepository : Repository<HAWB>, IHAWBRepository
    {
        public HAWBRepository(IGGGETSAppUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager) { }

        public IEnumerable<HAWB> FindPagedHAWBs(int pageIndex, int pageCount)
        {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");

            IGGGETSAppUnitOfWork context = this.UnitOfWork as IGGGETSAppUnitOfWork;

            if (context != null)
            {
                return context.HAWB.AsEnumerable();
            }
            else
                throw new InvalidOperationException(string.Format(
                                                                CultureInfo.InvariantCulture,
                                                                Messages.exception_InvalidStoreContext,
                                                                this.GetType().Name));
        }

        public HAWB FindHAWBByBarCode(string barCode)
        {

            IGGGETSAppUnitOfWork context = this.UnitOfWork as IGGGETSAppUnitOfWork;

            if (context != null)
            {
                if(context.HAWBItem.Count()!=0)
                    return context.HAWB.Include(ba => ba.HAWBItems).Where(it => it.BarCode == barCode).SingleOrDefault();
                else
                    return context.HAWB.Where(it => it.BarCode == barCode).SingleOrDefault();
            }
            else
                throw new InvalidOperationException(string.Format(
                                                                CultureInfo.InvariantCulture,
                                                                Messages.exception_InvalidStoreContext,
                                                                this.GetType().Name));
        }

        /// <summary>
        /// 加载运单
        /// </summary>
        /// <param name="barCode">条形码</param>
        /// <returns></returns>
        public HAWB LoadHAWBByBarCode(string barCode)
        {
            IGGGETSAppUnitOfWork context = this.UnitOfWork as IGGGETSAppUnitOfWork;

            if (context != null)
            {
                HAWB hawb = context.HAWB.Include(ba => ba.HAWBItems).Include(ba => ba.HAWBBoxes).Include(it => it.Department).Include(it=>it.User).Where(it => it.BarCode == barCode).SingleOrDefault();
                if (hawb == null) return null;
                hawb.ConsigneeCountryDesc =
                    (context.CountryCode.Where(it => it.CountryCode1 == hawb.ShipperCountry).SingleOrDefault()).
                        CountryName;
                return hawb;
            }
            else
                throw new InvalidOperationException(string.Format(
                                                                CultureInfo.InvariantCulture,
                                                                Messages.exception_InvalidStoreContext,
                                                                this.GetType().Name));
        }

        /// <summary>
        /// 运单多条件查询
        /// </summary>
        /// <param name="barCode">运单编号</param>
        /// <param name="countryCode">国家二字码</param>
        /// <param name="regionCode">地区三字码</param>
        /// <param name="companyCode">公司账号</param>
        /// <param name="carrier">承运公司名称</param>
        /// <param name="contactor">联系人姓名(可能是公司用户，也可能是个人用户)</param>
        /// <param name="HAWBOperator">用户名称-操作人</param>
        /// <param name="endTime">结束日期</param>
        /// <param name="settleType">结算方式</param>
        /// <param name="serviceType">包裹类型</param>
        /// <param name="isInternational">运单类型</param>
        /// <param name="beginTime">开始日期</param>
        /// <param name="departmentCode">部门编号</param>
        /// <returns></returns>
        public IList<HAWB> FindHAWBsByCondition(string barCode, string countryCode, string regionCode, string departmentCode, string companyCode, string carrier,
                                               string HAWBOperator, string contactor, DateTime? beginTime, DateTime? endTime, int settleType, int serviceType,
                                               bool? isInternational)
        {
            IEnumerable<HAWB> HAWBs=null;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;

            if (context != null)
            {
                HAWBs = context.HAWB.Include(h=>h.Department).Select(h => h);
                if (!string.IsNullOrEmpty(barCode)) HAWBs = HAWBs.Where(a => a.BarCode == barCode);
                if (!string.IsNullOrEmpty(countryCode)) HAWBs = HAWBs.Where(a => a.ConsigneeCountry == countryCode);
                if (!string.IsNullOrEmpty(regionCode)) HAWBs = HAWBs.Where(a => a.ConsigneeRegion == regionCode);
                if (!string.IsNullOrEmpty(departmentCode)) HAWBs = HAWBs.Where(a => a.Department.DepCode == departmentCode);
                if (!string.IsNullOrEmpty(companyCode)) HAWBs = HAWBs.Where(a => a.Department.CompanyCode == companyCode);
                if (!string.IsNullOrEmpty(carrier)) HAWBs = HAWBs.Where(a => a.Carrier.StartsWith(carrier));
                if (!string.IsNullOrEmpty(HAWBOperator)) HAWBs = HAWBs.Where(a => a.Operator == HAWBOperator);
                if (!string.IsNullOrEmpty(contactor)) HAWBs = HAWBs.Where(a => a.ConsigneeContactor.StartsWith(contactor));
                if(beginTime.HasValue)
                {
                    if (beginTime.Value != DateTime.MinValue)
                        HAWBs =
                            HAWBs.Where(
                                a =>
                                a.CreateTime >=
                                new DateTime(beginTime.Value.Year, beginTime.Value.Month, beginTime.Value.Day, 0, 0, 0));
                }
                if (endTime.HasValue)
                {
                    if (endTime.Value != DateTime.MinValue)
                        HAWBs =
                            HAWBs.Where(
                                a =>
                                a.CreateTime <=
                                new DateTime(endTime.Value.Year, endTime.Value.Month, endTime.Value.Day, 23, 59, 59));
                }
                if (settleType != -1) HAWBs = HAWBs.Where(a => a.SettleType == Convert.ToInt16(settleType));
                if (serviceType != -1) HAWBs = HAWBs.Where(a => a.ServiceType == Convert.ToInt16(serviceType));
                if (isInternational.HasValue) HAWBs = HAWBs.Where(a => a.IsInternational == isInternational);
            }
            else
            {
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name));
            }
            return HAWBs.OrderByDescending(a=>a.CreateTime).ToList();
        }

        /// <summary>
        /// 运单多条件查询(支持分页)
        /// </summary>
        /// <param name="barCode">运单编号</param>
        /// <param name="countryCode">国家二字码</param>
        /// <param name="regionCode">地区三字码</param>
        /// <param name="companyCode">公司账号</param>
        /// <param name="carrier">承运公司名称</param>
        /// <param name="contactor">联系人姓名(可能是公司用户，也可能是个人用户)</param>
        /// <param name="HAWBOperator">用户名称-操作人</param>
        /// <param name="endTime">结束日期</param>
        /// <param name="settleType">结算方式</param>
        /// <param name="serviceType">包裹类型</param>
        /// <param name="isInternational">运单类型</param>
        /// <param name="beginTime">开始日期</param>
        /// <param name="departmentCode">部门编号</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageCount">一页显示个数</param>
        /// <returns></returns>
        public IList<HAWB> FindHAWBsByCondition(string barCode, string countryCode, string regionCode, string departmentCode, string companyCode, string carrier, string HAWBOperator, string contactor, DateTime? beginTime, DateTime? endTime, int settleType, int serviceType, bool? isInternational, int pageIndex, int pageCount)
        {
            IEnumerable<HAWB> HAWBs = null;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;

            if (context != null)
            {
                HAWBs = context.HAWB.Include(h => h.Department).Select(h => h);
                if (!string.IsNullOrEmpty(barCode)) HAWBs = HAWBs.Where(a => a.BarCode == barCode);
                if (!string.IsNullOrEmpty(countryCode)) HAWBs = HAWBs.Where(a => a.ConsigneeCountry == countryCode);
                if (!string.IsNullOrEmpty(regionCode)) HAWBs = HAWBs.Where(a => a.ConsigneeRegion == regionCode);
                if (!string.IsNullOrEmpty(departmentCode)) HAWBs = HAWBs.Where(a => a.Department.DepCode == departmentCode);
                if (!string.IsNullOrEmpty(companyCode)) HAWBs = HAWBs.Where(a => a.Department.CompanyCode == companyCode);
                if (!string.IsNullOrEmpty(carrier)) HAWBs = HAWBs.Where(a => a.Carrier.StartsWith(carrier));
                if (!string.IsNullOrEmpty(HAWBOperator)) HAWBs = HAWBs.Where(a => a.Operator == HAWBOperator);
                if (!string.IsNullOrEmpty(contactor)) HAWBs = HAWBs.Where(a => a.ConsigneeContactor.StartsWith(contactor));
                if (beginTime.HasValue)
                {
                    if (beginTime.Value != DateTime.MinValue)
                        HAWBs =
                            HAWBs.Where(
                                a =>
                                a.CreateTime >=
                                new DateTime(beginTime.Value.Year, beginTime.Value.Month, beginTime.Value.Day, 0, 0, 0));
                }
                if (endTime.HasValue)
                {
                    if (endTime.Value != DateTime.MinValue)
                        HAWBs =
                            HAWBs.Where(
                                a =>
                                a.CreateTime <=
                                new DateTime(endTime.Value.Year, endTime.Value.Month, endTime.Value.Day, 23, 59, 59));
                }
                if (settleType != -1) HAWBs = HAWBs.Where(a => a.SettleType == Convert.ToInt16(settleType));
                if (serviceType != -1) HAWBs = HAWBs.Where(a => a.ServiceType == Convert.ToInt16(serviceType));
                if (isInternational.HasValue) HAWBs = HAWBs.Where(a => a.IsInternational == isInternational);
            }
            else
            {
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name));
            }
            return HAWBs.OrderByDescending(a => a.CreateTime).Skip(pageIndex*pageCount).Take(pageCount).ToList();
        }

        /// <summary>
        /// 运单多条件查询
        /// </summary>
        /// <param name="barCode">运单编号</param>
        /// <param name="countryName">国家名称(目的地)</param>
        /// <param name="regionName">地区名称(目的地)</param>
        /// <param name="userCode">客户/部门账号</param>
        /// <param name="companyName">承运公司名称</param>
        /// <param name="realName">联系人/公司姓名</param>
        /// <param name="beginTime">开始日期</param>
        /// <param name="endTime">结束日期</param>
        /// <param name="settleType">结算方式</param>
        /// <param name="serviceType">包裹类型</param>
        /// <param name="isInternational">运单类型</param>
        /// <returns></returns>
        public IList<HAWB> FindHAWBsByCondition(string barCode, string countryName, string regionName, string userCode, string companyName,
                                               string realName, DateTime? beginTime, DateTime? endTime, int settleType, int serviceType,
                                               bool? isInternational)
        {
            IEnumerable<HAWB> HAWBs = null;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            //using (IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork)
            //{
                if (context != null)
                {
                    HAWBs = context.HAWB.Include(h => h.User).Include(h => h.Department).Select(h => h);
                    if (!string.IsNullOrEmpty(barCode)) HAWBs = HAWBs.Where(a => a.BarCode == barCode);
                    if (!string.IsNullOrEmpty(countryName))
                    {
                        //国家
                        CountryCode country =
                            context.CountryCode.Where(it => it.CountryName == countryName).SingleOrDefault();
                        if(country!=null)
                            HAWBs = HAWBs.Where(a => a.ConsigneeCountry == country.CountryCode1);
                        else
                            HAWBs = HAWBs.Where(a => a.ConsigneeCountry == "00");
                    }
                    if (!string.IsNullOrEmpty(regionName))
                    {
                        //地区
                        RegionCode region =
                            context.RegionCode.Where(it => it.RegionName == regionName).SingleOrDefault();
                        if (region != null)
                            HAWBs = HAWBs.Where(a => a.ConsigneeRegion == region.RegionCode1);
                        else
                            HAWBs = HAWBs.Where(a => a.ConsigneeRegion == "000");
                    }
                    if (!string.IsNullOrEmpty(userCode))
                    {
                        User user = context.User.Where(it => it.LoginName == userCode).SingleOrDefault();
                        if (user != null)
                        {
                            userCode = user.LoginName;
                            HAWBs = HAWBs.Where(a => a.User.LoginName == userCode);
                        }
                        else
                            HAWBs = HAWBs.Where(a => a.Department.DepCode == userCode);
                    }
                    if (!string.IsNullOrEmpty(companyName)) HAWBs = HAWBs.Where(a => a.Carrier.StartsWith(companyName));
                    //这里的联系人和电话可能是公司也可能是个人的
                    if (!string.IsNullOrEmpty(realName))
                    {
                        //根据公司名称获取公司账号
                        Company companyObj = context.Company.Where(it => it.FullName.StartsWith(realName)).SingleOrDefault();
                        if (companyObj != null)
                        {
                            realName = companyObj.CompanyCode;
                            HAWBs = HAWBs.Where(a => a.Department.CompanyCode == realName);
                        }
                        else
                            HAWBs = HAWBs.Where(a => a.User.RealName == realName);
                    }
                    if (beginTime.HasValue)
                    {
                        if (beginTime.Value != DateTime.MinValue)
                            HAWBs = HAWBs.Where(a => a.CreateTime >= beginTime.Value);
                    }
                    if (endTime.HasValue)
                    {
                        if (endTime.Value != DateTime.MinValue)
                            HAWBs = HAWBs.Where(a => a.CreateTime <= endTime.Value);
                    }
                    if (settleType != -1) HAWBs = HAWBs.Where(a => a.SettleType == Convert.ToInt16(settleType));
                    if (serviceType != -1) HAWBs = HAWBs.Where(a => a.ServiceType == Convert.ToInt16(serviceType));
                    if (isInternational.HasValue) HAWBs = HAWBs.Where(a => a.IsInternational == isInternational);
                }
                else
                {
                    throw new InvalidOperationException(string.Format(
                                                                CultureInfo.InvariantCulture,
                                                                Messages.exception_InvalidStoreContext,
                                                                GetType().Name));
                }
                return HAWBs.OrderByDescending(a => a.CreateTime).ToList();
            //}
        }

        /// <summary>
        /// 运单多条件查询(支持分页)
        /// </summary>
        /// <param name="barCode">运单编号</param>
        /// <param name="countryName">国家名称(目的地)</param>
        /// <param name="regionName">地区名称(目的地)</param>
        /// <param name="userCode">客户/部门账号</param>
        /// <param name="companyName">承运公司名称</param>
        /// <param name="realName">联系人/公司姓名</param>
        /// <param name="beginTime">开始日期</param>
        /// <param name="endTime">结束日期</param>
        /// <param name="settleType">结算方式</param>
        /// <param name="serviceType">包裹类型</param>
        /// <param name="isInternational">运单类型</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageCount">一页显示个数</param>
        /// <returns></returns>
        public IList<HAWB> FindHAWBsByCondition(string barCode, string countryName, string regionName, string userCode, string companyName, string realName, DateTime? beginTime, DateTime? endTime, int settleType, int serviceType, bool? isInternational, int pageIndex, int pageCount)
        {
            IEnumerable<HAWB> HAWBs = null;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;

            if (context != null)
            {
                HAWBs = context.HAWB.Include(h => h.User).Include(h => h.Department).Select(h => h);
                if (!string.IsNullOrEmpty(barCode)) HAWBs = HAWBs.Where(a => a.BarCode == barCode);
                if (!string.IsNullOrEmpty(countryName))
                {
                    //国家
                    CountryCode country =
                        context.CountryCode.Where(it => it.CountryName == countryName).SingleOrDefault();
                    if (country != null)
                        HAWBs = HAWBs.Where(a => a.ConsigneeCountry == country.CountryCode1);
                    else
                        HAWBs = HAWBs.Where(a => a.ConsigneeCountry == "00");
                }
                if (!string.IsNullOrEmpty(regionName))
                {
                    //地区
                    RegionCode region =
                        context.RegionCode.Where(it => it.RegionName == regionName).SingleOrDefault();
                    if (region != null)
                        HAWBs = HAWBs.Where(a => a.ConsigneeRegion == region.RegionCode1);
                    else
                        HAWBs = HAWBs.Where(a => a.ConsigneeRegion == "000");
                }
                if (!string.IsNullOrEmpty(userCode))
                {
                    User user = context.User.Where(it => it.LoginName == userCode).SingleOrDefault();
                    if (user != null)
                    {
                        userCode = user.LoginName;
                        HAWBs = HAWBs.Where(a => a.User.LoginName == userCode);
                    }
                    else
                        HAWBs = HAWBs.Where(a => a.Department.DepCode == userCode);
                }
                if (!string.IsNullOrEmpty(companyName)) HAWBs = HAWBs.Where(a => a.Carrier.StartsWith(companyName));
                //这里的联系人和电话可能是公司也可能是个人的
                if (!string.IsNullOrEmpty(realName))
                {
                    //根据公司名称获取公司账号
                    Company companyObj = context.Company.Where(it => it.FullName.StartsWith(realName)).SingleOrDefault();
                    if (companyObj != null)
                    {
                        realName = companyObj.CompanyCode;
                        HAWBs = HAWBs.Where(a => a.Department.CompanyCode == realName);
                    }
                    else
                        HAWBs = HAWBs.Where(a => a.User.RealName == realName);
                }
                if (beginTime.HasValue)
                {
                    if (beginTime.Value != DateTime.MinValue)
                        HAWBs = HAWBs.Where(a => a.CreateTime >= beginTime.Value);
                }
                if (endTime.HasValue)
                {
                    if (endTime.Value != DateTime.MinValue)
                        HAWBs = HAWBs.Where(a => a.CreateTime <= endTime.Value);
                }
                if (settleType != -1) HAWBs = HAWBs.Where(a => a.SettleType == Convert.ToInt16(settleType));
                if (serviceType != -1) HAWBs = HAWBs.Where(a => a.ServiceType == Convert.ToInt16(serviceType));
                if (isInternational.HasValue) HAWBs = HAWBs.Where(a => a.IsInternational == isInternational);
            }
            else
            {
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name));
            }
            return HAWBs.OrderByDescending(a => a.CreateTime).Skip(pageIndex * pageCount).Take(pageCount).ToList();
        }

        #region 运单货物操作
        /// <summary>
        /// 通过运单编号获取运单货物
        /// </summary>
        /// <param name="HID">运单编号</param>
        /// <returns></returns>
        public IList<HAWBItem> FindHAWBItemByHID(string HID)
        {
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;

            if (context != null)
            {
                Guid guidHID = new Guid(HID);
                return context.HAWBItem.Where(it => it.HID == guidHID).Select(h => h).ToList();
            }
            else
                throw new InvalidOperationException(string.Format(
                                                                CultureInfo.InvariantCulture,
                                                                Messages.exception_InvalidStoreContext,
                                                                GetType().Name));
        }
        #endregion

        #region 运单盒子操作
        /// <summary>
        /// 通过运单编号获取运单盒子
        /// </summary>
        /// <param name="HID">运单编号</param>
        /// <returns></returns>
        public IList<HAWBBox> FindHAWBBoxByHID(string HID)
        {
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;

            if (context != null)
            {
                Guid guidHID = new Guid(HID);
                return context.HAWBBox.Where(it => it.HID == guidHID).Select(h => h).ToList();
            }
            else
                throw new InvalidOperationException(string.Format(
                                                                CultureInfo.InvariantCulture,
                                                                Messages.exception_InvalidStoreContext,
                                                                GetType().Name));
        }
        #endregion

        #region 运单包裹操作
        /// <summary>
        /// 通过条形码获取包裹
        /// </summary>
        /// <param name="barcode">包裹</param>
        /// <returns></returns>
        public Package FindPackageByBarcode(string barcode)
        {
            if (string.IsNullOrEmpty(barcode)) throw new ArgumentException("barcode is null!");
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
            {
                return context.Package.Include(it=>it.HAWBs).Include(it=>it.MAWB).Where(it => it.BarCode == barcode).SingleOrDefault();
            }
            else
                throw new InvalidOperationException(string.Format(
                                                                CultureInfo.InvariantCulture,
                                                                Messages.exception_InvalidStoreContext,
                                                                GetType().Name));
        }
        #endregion

        #region 运单用户操作
        /// <summary>
        /// 通过运单中的用户编号获取运单用户
        /// </summary>
        /// <param name="UID">用户编号</param>
        /// <returns></returns>
        public User FindUserByUID(string UID)
        {
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;

            if (context != null)
            {
                Guid guidHID = new Guid(UID);
                return context.User.Where(it => it.UID == guidHID).SingleOrDefault();
            }
            else
                throw new InvalidOperationException(string.Format(
                                                                CultureInfo.InvariantCulture,
                                                                Messages.exception_InvalidStoreContext,
                                                                GetType().Name));
        }
        #endregion

        #region 飞机航班操作
        ///// <summary>
        ///// 通过航班号获取航班
        ///// </summary>
        ///// <param name="FID">航班号</param>
        ///// <returns></returns>
        //public Flight FindFlightByFlightNo(string flightNo)
        //{
        //    if (flightNo == null) throw new ArgumentNullException("flightNo is not null!");
        //    IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;

        //    if (context != null)
        //    {
        //        return context.Flight.Include(it=>it.MAWBs).Where(it => it.FlightNo == flightNo).SingleOrDefault();
        //    }
        //    else
        //        throw new InvalidOperationException(string.Format(
        //                                                        CultureInfo.InvariantCulture,
        //                                                        Messages.exception_InvalidStoreContext,
        //                                                        GetType().Name));
        //}

        ///// <summary>
        ///// 查询所有航班
        ///// </summary>
        ///// <returns></returns>
        //public IList<Flight> FindAllFlights()
        //{
        //    IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;

        //    if (context != null)
        //    {
        //        return context.Flight.ToList();
        //    }
        //    else
        //        throw new InvalidOperationException(string.Format(
        //                                                        CultureInfo.InvariantCulture,
        //                                                        Messages.exception_InvalidStoreContext,
        //                                                        GetType().Name));
        //}

        /// <summary>
        /// 通过运单间接查询包裹信息
        /// 主要用于方便绑定GRID
        /// </summary>
        /// <param name="barCode">包裹编号</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="destinationCode">目的地三字码</param>
        /// <returns></returns>
        public IList<HAWB> FindHAWBsOfPackageByCondition(string barCode, DateTime? beginDate, DateTime? endDate, string destinationCode)
        {
            IEnumerable<HAWB> hawbs = null;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            //using (IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork)
            //{
                if (context != null)
                {
                    hawbs = (from he in context.HAWB.Include(h => h.Package) where he.Package!=null select he).ToList();
                    //hawbs = context.HAWB.Include(h => h.Package).Select(h => h).ToList().SkipWhile(h => h.Package == null);
                    if (!string.IsNullOrEmpty(barCode)) hawbs = hawbs.Where(h => h.Package.BarCode == barCode);
                    if (!string.IsNullOrEmpty(destinationCode)) hawbs = hawbs.Where(h => h.Package.RegionCode == destinationCode);
                    if (beginDate.HasValue)
                    {
                        if (beginDate.Value != DateTime.MinValue)
                            hawbs =
                                hawbs.Where(
                                    h =>
                                    h.Package.CreateTime >=
                                    new DateTime(beginDate.Value.Year, beginDate.Value.Month, beginDate.Value.Day, 0, 0,
                                                 0));
                    }
                    if (endDate.HasValue)
                    {
                        if (endDate.Value != DateTime.MinValue)
                            hawbs =
                                hawbs.Where(
                                    h =>
                                    h.Package.CreateTime <=
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
                return hawbs.ToList();
            //}
        }

        #endregion

        #region 总运单操作
        /// <summary>
        /// 通过运单中的用户编号获取运单用户
        /// </summary>
        /// <param name="UID">用户编号</param>
        /// <returns></returns>
        public MAWB FindMAWBByBarcode(string barcode)
        {
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;

            if (context != null)
            {
                return context.MAWB.Include(it=>it.Packages).Where(it => it.BarCode == barcode).SingleOrDefault();
            }
            else
                throw new InvalidOperationException(string.Format(
                                                                CultureInfo.InvariantCulture,
                                                                Messages.exception_InvalidStoreContext,
                                                                GetType().Name));
        }

        /// <summary>
        /// 通过总运单号获取运单信息
        /// </summary>
        /// <param name="MID">总运单号</param>
        /// <returns></returns>
        public IList<HAWB> FindHAWBsByMID(string MID)
        {
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            IList<HAWB> list = new List<HAWB>();
            if (context != null)
            {
                IList<Package> packages = context.Package.Include(it => it.HAWBs).Where(it=>it.MID==new Guid(MID)).ToList();
                foreach(Package package in packages)
                {
                    Guid pid = package.PID;
                    IList<HAWB> hawbs =
                        context.HAWB.Include(it => it.HAWBItems).Where(it => it.PID == pid).ToList();
                    foreach(HAWB hawb in hawbs)
                    {
                        string name =
                            (context.RegionCode.Where(it => it.RegionCode1 == hawb.ConsigneeRegion).SingleOrDefault()).
                                RegionName;
                        hawb.ConsigneeRegionDesc = name;
                        list.Add(hawb);
                    }
                }
                return list;
            }
            else
                throw new InvalidOperationException(string.Format(
                                                                CultureInfo.InvariantCulture,
                                                                Messages.exception_InvalidStoreContext,
                                                                GetType().Name));
        }
        #endregion
    }
}
