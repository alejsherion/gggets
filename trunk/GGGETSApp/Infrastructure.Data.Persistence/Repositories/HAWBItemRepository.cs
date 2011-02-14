using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETS.GGGETSApp.Infrastructure.Data.Core;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.UnitOfWork;
using ETS.GGGETSApp.Infrastructure.CrossCutting.Logging;
using ETS.GGGETSApp.Domain.Application.Entities;
using Domain.GGGETS;
using System.Globalization;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.Resources;

namespace ETS.GGGETSApp.Infrastructure.Data.Persistence.Repositories
{
    public class HAWBItemRepository:Repository<HAWBItem>, IHAWBItemRepository
    {
        public HAWBItemRepository(IGGGETSAppUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager) { }
    }
}
