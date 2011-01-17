using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace Application.GGETS.TransManagement
{
    public interface ITransManagementService
    {
        void AddTrans(Trans newTrans);
    }
}
