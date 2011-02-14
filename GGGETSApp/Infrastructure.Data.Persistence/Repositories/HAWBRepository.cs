using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                return context.HAWB.Include(ba => ba.HAWBItem).Where(it=>it.BarCode==barCode).SingleOrDefault();                                         
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
        public IList<HAWB> FindHAWBsByCondition(string HID, string countryCode, string regionCode, string loginName, string realName, string phone, string settleType, string serviceType, string isInternational)
        {
            IEnumerable<HAWB> HAWBs=null;
            using(IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork)
            {
                if (context != null)
                {
                    HAWBs = context.HAWB.Include(h => h.User).Select(h => h);
                    if (!string.IsNullOrEmpty(HID)) HAWBs = HAWBs.Where(a => a.HID == new Guid(HID));
                    if (!string.IsNullOrEmpty(countryCode)) HAWBs = HAWBs.Where(a => a.User.CountryCode == countryCode);
                    if (!string.IsNullOrEmpty(regionCode)) HAWBs = HAWBs.Where(a => a.User.RegionCode == regionCode);
                    if (!string.IsNullOrEmpty(loginName)) HAWBs = HAWBs.Where(a => a.User.LoginName == loginName);
                    if (!string.IsNullOrEmpty(realName)) HAWBs = HAWBs.Where(a => a.User.RealName == realName);
                    if (!string.IsNullOrEmpty(phone)) HAWBs = HAWBs.Where(a => a.User.Phone == phone);
                    if (!string.IsNullOrEmpty(settleType)) HAWBs = HAWBs.Where(a => a.SettleType == Convert.ToInt16(settleType));
                    if (!string.IsNullOrEmpty(serviceType)) HAWBs = HAWBs.Where(a => a.ServiceType == Convert.ToInt16(serviceType));
                    if (!string.IsNullOrEmpty(isInternational))
                    {
                        bool judge = true;
                        if (isInternational == "0") judge = false;
                        if (isInternational == "1") judge = true;
                        HAWBs = HAWBs.Where(a => a.IsInternational == judge);
                    }
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
    }
}
