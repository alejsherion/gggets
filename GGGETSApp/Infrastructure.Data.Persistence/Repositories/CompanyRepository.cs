//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        公司DAL
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
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(IGGGETSAppUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager) { }
        /// <summary>
        /// 公司多条件查询
        /// </summary>
        /// <param name="companyCode">公司编号</param>
        /// <param name="fullName">公司全称</param>
        /// <param name="shortName">公司简称</param>
        /// <param name="contactor">联系人</param>
        /// <param name="contactorPhone">联系人电话</param>
        /// <returns></returns>
        public IList<Company> FindCompaniesByCondition(string companyCode, string fullName, string shortName, string contactor, string contactorPhone)
        {
            IEnumerable<Company> companies = null;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;

            if (context != null)
            {
                companies = context.Company.Select(it => it);
                if (!string.IsNullOrEmpty(companyCode)) companies = companies.Where(it => it.CompanyCode == companyCode);
                if (!string.IsNullOrEmpty(fullName)) companies = companies.Where(it => it.FullName.StartsWith(fullName));
                if (!string.IsNullOrEmpty(shortName)) companies = companies.Where(it => it.ShortName.StartsWith(shortName));
                if (!string.IsNullOrEmpty(contactor)) companies = companies.Where(it => it.Contactor.StartsWith(contactor));
                if (!string.IsNullOrEmpty(contactorPhone))
                    companies = companies.Where(it => it.ContactorPhone == contactorPhone);
            }
            else
            {
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name));
            }
            return companies.OrderBy(it => it.Status).ToList();
        }
    }
}
