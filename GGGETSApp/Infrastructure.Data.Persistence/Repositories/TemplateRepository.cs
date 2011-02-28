//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        模板DAL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.28
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
    public class TemplateRepository : Repository<Template>, ITemplateRepository
    {
        public TemplateRepository(IGGGETSAppUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager) { }

    }
}
