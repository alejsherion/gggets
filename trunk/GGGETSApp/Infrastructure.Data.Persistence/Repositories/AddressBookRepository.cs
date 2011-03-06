//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        地址本DAL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.24
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
    public class AddressBookRepository : Repository<AddressBook>, IAddressBookRepository
    {
        public AddressBookRepository(IGGGETSAppUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager) { }
        
        /// <summary>
        /// 获取所有NULL值地址本
        /// </summary>
        public IList<AddressBook> GetAllBadAddressBook()
        {
            IList<AddressBook> list = new List<AddressBook>();
            //Get Assemble's Context
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            //don't forget open package's load:HAWBs
            IList<AddressBook> addressBooks = context.AddressBook.Select(it => it).ToList();
            if (addressBooks.Count!=0)
            {
                for(int i=0;i<addressBooks.Count();i++)
                {
                    AddressBook addressBook = addressBooks[i];
                    if (addressBook.DID == null && addressBook.UID == null) list.Add(addressBook);
                }
            }
            return list;
        }

        /// <summary>
        /// 地址本多条件查询
        /// </summary>
        /// <param name="companyCode">公司账号</param>
        /// <param name="depCode">部门账号</param>
        /// <param name="loginName">用户名</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public IList<AddressBook> FindAddressBookByCondition(string companyCode, string depCode, string loginName, DateTime? beginDate, DateTime? endDate)
        {
            IEnumerable<AddressBook> addressBooks = null;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;

            if (context != null)
            {
                addressBooks = context.AddressBook.Include(it => it.User).Include(it => it.Department).ToList();
                if (!string.IsNullOrEmpty(companyCode))
                {
                    addressBooks = addressBooks.Where(it => it.Department != null);
                    addressBooks = addressBooks.Where(it => it.Department.CompanyCode == companyCode);
                }
                if (!string.IsNullOrEmpty(depCode))
                {
                    addressBooks = addressBooks.Where(it => it.Department != null);
                    addressBooks = addressBooks.Where(it => it.Department.DepCode == depCode);
                }
                if (!string.IsNullOrEmpty(loginName))
                {
                    addressBooks = addressBooks.Where(it => it.User != null);
                    addressBooks = addressBooks.Where(it => it.User.LoginName == loginName);
                }
                if (beginDate.HasValue)
                {
                    if (beginDate.Value != DateTime.MinValue)
                        addressBooks =
                            addressBooks.Where(
                                it =>
                                it.CreateTime >=
                                new DateTime(beginDate.Value.Year, beginDate.Value.Month, beginDate.Value.Day, 0, 0,
                                             0));
                }
                if (endDate.HasValue)
                {
                    if (endDate.Value != DateTime.MinValue)
                        addressBooks =
                            addressBooks.Where(
                                it =>
                                it.CreateTime <=
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
            return addressBooks.OrderByDescending(it => it.CreateTime).ToList();
        }
    }
}
