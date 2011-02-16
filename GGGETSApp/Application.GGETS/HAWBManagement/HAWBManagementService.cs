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
        //运单
        IHAWBRepository _hawbRepository;
        //运单货物
        IHAWBItemRepository _hawbItemRepository;
        //运单盒子
        IHAWBBoxRepository _hawbBoxRepository;
        //运单用户
        IUserRepository _userRepository;

        public HAWBManagementService(IHAWBRepository hawbRepository, IHAWBItemRepository hawbItemRepository, IHAWBBoxRepository hawbBoxRepository, IUserRepository userRepository)
        {
            _hawbRepository = hawbRepository;
            _hawbItemRepository = hawbItemRepository;
            _hawbBoxRepository = hawbBoxRepository;
            _userRepository = userRepository;
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
        public IList<HAWB> FindHAWBsByCondition(string barCode, string countryCode, string regionCode, string loginName, string departmentCode,
                                               string realName, string phone, DateTime? beginTime, DateTime? endTime, int settleType, int serviceType,
                                               bool isInternational)
        {
            return _hawbRepository.FindHAWBsByCondition(barCode, countryCode, regionCode, loginName, departmentCode, 
                                                realName, phone, beginTime, endTime, settleType, serviceType, isInternational);
        }

        /// <summary>
        /// 通过条形码移除运单
        /// </summary>
        /// <param name="barCode">条形码</param>
        public void RemoveHAWB(string barCode)
        {
            if (barCode == null)
                throw new ArgumentNullException("barCode is null");
            //open all hawb's load
            HAWB hawb = _hawbRepository.FindHAWBByBarCode(barCode);
            IUnitOfWork unitOfWork = _hawbRepository.UnitOfWork;
            IUnitOfWork itemUnitOfWork = _hawbItemRepository.UnitOfWork;
            IUnitOfWork boxUnitOfWork = _hawbBoxRepository.UnitOfWork;
            IUnitOfWork userUnitOfWork = _userRepository.UnitOfWork;
            if (hawb == null)
                throw new ArgumentNullException("hawb is null");
            IList<HAWBBox> HAWBBoxList = _hawbRepository.FindHAWBBoxByHID(hawb.HID.ToString());
            if (HAWBBoxList.Count != 0)//运单盒子
            {
                foreach (HAWBBox hawbBox in HAWBBoxList)
                {
                    _hawbBoxRepository.Remove(hawbBox);
                }
            }
            IList<HAWBItem> HAWBItemList = _hawbRepository.FindHAWBItemByHID(hawb.HID.ToString());
            if (HAWBItemList.Count != 0)//运单货物
            {
                foreach (HAWBItem hawbItem in HAWBItemList)
                {
                    _hawbItemRepository.Remove(hawbItem);
                }
            }
            User userObj = _hawbRepository.FindUserByUID(hawb.UID.ToString());
            if (userObj != null) _userRepository.Remove(hawb.User);//运单用户
            _hawbRepository.Remove(hawb);//整个运单
            //complete changes in this unit of work
            boxUnitOfWork.Commit();
            itemUnitOfWork.Commit();
            userUnitOfWork.Commit();
            unitOfWork.Commit();
        }

        /// <summary>
        /// 延迟加载ALL
        /// </summary>
        /// <param name="barCode">条形码</param>
        /// <returns></returns>
        public HAWB LoadHAWBByBarCode(string barCode)
        {
            return _hawbRepository.LoadHAWBByBarCode(barCode);
        }
    }
}
