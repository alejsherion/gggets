using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Core;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Domain.Core.Specification;

namespace Application.GGETS
{
    public class HAWBManagementService:IHAWBManagementService
    {
        IHAWBRepository _hawbRepository;

        public HAWBManagementService(IHAWBRepository hawbRepository)
        {
            _hawbRepository = hawbRepository;
        }

        public void AddHAWB(HAWB newHAWB)
        {
            if (newHAWB ==null)
                throw new ArgumentNullException("HAWB is null");
            IUnitOfWork unitOfWork = _hawbRepository.UnitOfWork as IUnitOfWork;
            _hawbRepository.Add(newHAWB);
            //complete changes in this unit of work
            unitOfWork.Commit();
        }

        public void ChangeHAWB(HAWB hawb)
        {
            if (hawb == null)
                throw new ArgumentNullException("HAWB is null");
            IUnitOfWork unitOfWork = _hawbRepository.UnitOfWork as IUnitOfWork;
            _hawbRepository.Modify(hawb);
            //complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
        }

        public HAWB FindHAWBByBarCode(string barCode)
        {
           // HAWBBarCodeSpecification specification = new HAWBBarCodeSpecification(barCode);
           ////query repository
            return this._hawbRepository.FindHAWBByBarCode(barCode); ;

        }

        public List<HAWB> FindPagedHAWBs(int pageIndex, int pageCount)
        {
            return _hawbRepository.GetPagedElements(pageIndex, pageCount, b => b.BarCode, true).ToList();
        }
    }
}
