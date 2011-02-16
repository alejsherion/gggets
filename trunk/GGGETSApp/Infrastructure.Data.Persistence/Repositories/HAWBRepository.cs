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
                return context.HAWB.Include(ba => ba.HAWBItems).Where(it => it.BarCode == barCode).SingleOrDefault();
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
                return context.HAWB.Include(ba => ba.HAWBItems).Include(ba=>ba.HAWBBoxes).Where(it => it.BarCode == barCode).SingleOrDefault();
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
        /// <param name="HID">运单编号</param>
        /// <param name="countryCode">国家二字码</param>
        /// <param name="regionCode">地区三字码</param>
        /// <param name="loginName">客户账号</param>
        /// <param name="realName">联系人姓名</param>
        /// <param name="phone">联系电话</param>
        /// <param name="settleType">结算方式</param>
        /// <param name="serviceType">包裹类型</param>
        /// <param name="isInternational">运单类型</param>
        /// <returns></returns>
        public IList<HAWB> FindHAWBsByCondition(string barCode, string countryCode, string regionCode, string loginName, string departmentCode,
                                               string realName, string phone, DateTime? beginTime, DateTime? endTime, int settleType, int serviceType,
                                               bool isInternational)
        {
            IEnumerable<HAWB> HAWBs=null;
            using(IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork)
            {
                if (context != null)
                {
                    HAWBs = context.HAWB.Include(h => h.User).Select(h => h);
                    if (!string.IsNullOrEmpty(barCode)) HAWBs = HAWBs.Where(a => a.BarCode == barCode);
                    if (!string.IsNullOrEmpty(countryCode)) HAWBs = HAWBs.Where(a => a.User.CountryCode == countryCode);
                    if (!string.IsNullOrEmpty(regionCode)) HAWBs = HAWBs.Where(a => a.User.RegionCode == regionCode);
                    if (!string.IsNullOrEmpty(loginName)) HAWBs = HAWBs.Where(a => a.User.LoginName == loginName);
                    if (!string.IsNullOrEmpty(realName)) HAWBs = HAWBs.Where(a => a.User.RealName == realName);
                    if (!string.IsNullOrEmpty(phone)) HAWBs = HAWBs.Where(a => a.User.Phone == phone);
                    if(beginTime.HasValue)
                    {
                        if(beginTime.Value!=DateTime.MinValue)
                        HAWBs = HAWBs.Where(a => a.CreateTime >= beginTime.Value);
                    }
                    if (endTime.HasValue)
                    {
                        if (endTime.Value != DateTime.MinValue)
                        HAWBs = HAWBs.Where(a => a.CreateTime <= endTime.Value);
                    }
                    if (settleType != -1) HAWBs = HAWBs.Where(a => a.SettleType == Convert.ToInt16(settleType));
                    if (serviceType != -1) HAWBs = HAWBs.Where(a => a.ServiceType == Convert.ToInt16(serviceType));
                    HAWBs = HAWBs.Where(a => a.IsInternational == isInternational);
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
                return context.Package.Include(it=>it.HAWBs).Where(it => it.BarCode == barcode).Single();
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
                return context.User.Where(it => it.UID == guidHID).Single();
            }
            else
                throw new InvalidOperationException(string.Format(
                                                                CultureInfo.InvariantCulture,
                                                                Messages.exception_InvalidStoreContext,
                                                                GetType().Name));
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
                return context.MAWB.Include(it=>it.Packages).Where(it => it.BarCode == barcode).Single();
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
