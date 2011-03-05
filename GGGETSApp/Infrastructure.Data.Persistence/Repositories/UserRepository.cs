//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        运单用户DAL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.12
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Infrastructure.CrossCutting.Logging;
using ETS.GGGETSApp.Infrastructure.Data.Core;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.Resources;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.UnitOfWork;

namespace ETS.GGGETSApp.Infrastructure.Data.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IGGGETSAppUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager) { }
        
        /// <summary>
        /// 通过用户账号，地址类型获取地址信息
        /// </summary>
        /// <param name="loginName">用户账号</param>
        /// <param name="addressType">地址类型</param>
        /// <returns></returns>
        public IList<AddressBook> FindAllAddressBooksByCondition(string loginName, int addressType)
        {
            IEnumerable<AddressBook> addressBooks = null;
            string UID = string.Empty;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            //using (IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork)
            //{
                if (context != null)
                {
                    //首先获取该用户通过用户账号
                    //if(!string.IsNullOrEmpty(loginName))
                    User user = context.User.Where(it => it.LoginName == loginName).SingleOrDefault();
                    if (user != null) UID = Convert.ToString(user.UID);

                    addressBooks = context.AddressBook.Select(a => a);
                    if (!string.IsNullOrEmpty(loginName)) addressBooks = addressBooks.Where(a => a.UID == new Guid(UID));
                    if (addressType == 0 || addressType == 1 || addressType == 2) addressBooks = addressBooks.Where(a => a.AddressType == addressType);
                }
                else
                {
                    throw new InvalidOperationException(string.Format(
                                                                CultureInfo.InvariantCulture,
                                                                Messages.exception_InvalidStoreContext,
                                                                GetType().Name));
                }
                return addressBooks.OrderByDescending(p => p.CreateTime).ToList();
            //}
        }

        /// <summary>
        /// 用户多条件查询
        /// </summary>
        /// <param name="loginName">用户名</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public IList<User> FindUsersByCondition(string loginName, DateTime? beginDate, DateTime? endDate)
        {
            IEnumerable<User> users = null;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;

            if (context != null)
            {
                users = context.User.Select(it => it);
                if (!string.IsNullOrEmpty(loginName)) users = users.Where(it => it.LoginName == loginName);
                if (beginDate.HasValue)
                {
                    if (beginDate.Value != DateTime.MinValue)
                        users =
                            users.Where(
                                p =>
                                p.CreateTime >=
                                new DateTime(beginDate.Value.Year, beginDate.Value.Month, beginDate.Value.Day, 0, 0,
                                             0));
                }
                if (endDate.HasValue)
                {
                    if (endDate.Value != DateTime.MinValue)
                        users =
                            users.Where(
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
            return users.OrderByDescending(p => p.CreateTime).ToList();
        }
    }
}
