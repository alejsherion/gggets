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
       
    }
}
