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

        /// <summary>
        /// 运单多条件查询
        /// </summary>
        /// <param name="HID">运单编号</param>
        /// <param name="countryCode">国家二字码</param>
        /// <param name="regionCode">地区三字码</param>
        /// <param name="loginName">客户账号</param>
        /// <param name="realName">联系人姓名</param>
        /// <param name="phone">联系电话</param>
        /// <param name="settleType">结算方式</param>
        /// <param name="serviceType">包裹类型</param>
        /// <param name="isInternational">运单类型</param>
        /// <returns></returns>
        public IList<HAWB> FindHAWBsByCondition(string HID, string countryCode, string regionCode, string loginName, string realName, string phone, string settleType, string serviceType, string isInternational)
        {
            return _hawbRepository.FindHAWBsByCondition(HID, countryCode, regionCode, loginName, realName, phone,
                                                        settleType, serviceType, isInternational);
        }
    }
}
