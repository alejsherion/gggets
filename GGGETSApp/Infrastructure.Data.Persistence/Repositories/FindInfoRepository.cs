//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        模板参数维护DAL
// 作成者				ZhiWei.Shen
// 改版日				2011.03.18
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
    public class FindInfoRepository : Repository<FindInfo>, IFindInfoRepository
    {
        public FindInfoRepository(IGGGETSAppUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager) { }
        /// <summary>
        /// 通过表名获取所有列名
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public IList<FindInfo> FindAllByTableName(string tableName)
        {
            IEnumerable<FindInfo> infos = null;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            infos = context.FindInfo.Select(it => it);

            if (context != null)
            {
                if (!string.IsNullOrEmpty(tableName)) infos = infos.Where(it => it.name == tableName);
            }
            else
            {
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name));
            }
            return infos.OrderByDescending(it => it.fieldname).ToList();
        }
    }
}
