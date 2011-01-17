using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETS.GGGETSApp.Domain.Core;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace Domain.GGGETS
{
    public interface ITransRepository : IRepository<Trans>
    {
        IEnumerable<Trans> FindPagedTrans(int pageIndex, int pageCount);
    }
}
