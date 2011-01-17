using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Core;

namespace Application.GGETS.TransManagement
{
    public class TransManagementService:ITransManagementService
    {
        ITransRepository _transRepository;

        public TransManagementService(ITransRepository transRepository)
        {
            _transRepository = transRepository;
        }

        public void AddTrans(ETS.GGGETSApp.Domain.Application.Entities.Trans newTrans)
        {
            if (newTrans ==null)
                throw new ArgumentNullException("bankAccount");
            IUnitOfWork unitOfWork = _transRepository.UnitOfWork as IUnitOfWork;
            _transRepository.Add(newTrans);
            //complete changes in this unit of work
            unitOfWork.Commit();
        }
    }
}
