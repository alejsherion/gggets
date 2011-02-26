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
    }
}
