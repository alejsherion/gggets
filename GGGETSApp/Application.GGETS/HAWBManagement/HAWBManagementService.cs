using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
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
        /// <param name="barCode">运单编号</param>
        /// <param name="countryCode">国家二字码</param>
        /// <param name="regionCode">地区三字码</param>
        /// <param name="loginName">客户账号</param>
        /// <param name="companyName">公司名称</param>
        /// <param name="realName">联系人姓名</param>
        /// <param name="phone">联系电话</param>
        /// <param name="endTime">结束日期</param>
        /// <param name="settleType">结算方式</param>
        /// <param name="serviceType">包裹类型</param>
        /// <param name="isInternational">运单类型</param>
        /// <param name="beginTime">开始日期</param>
        /// <param name="departmentCode">部门编号</param>
        /// <returns></returns>
        public IList<HAWB> FindHAWBsByCondition(string barCode, string countryCode, string regionCode, string loginName, string departmentCode, string companyName,
                                               string realName, string phone, DateTime? beginTime, DateTime? endTime, int settleType, int serviceType,
                                               bool? isInternational)
        {
            return _hawbRepository.FindHAWBsByCondition(barCode, countryCode, regionCode, loginName, departmentCode, companyName,
                                                realName, phone, beginTime, endTime, settleType, serviceType, isInternational);
        }

        /// <summary>
        /// 运单多条件查询(支持分页)
        /// </summary>
        /// <param name="barCode">运单编号</param>
        /// <param name="countryCode">国家二字码</param>
        /// <param name="regionCode">地区三字码</param>
        /// <param name="loginName">客户账号</param>
        /// <param name="companyName">公司名称</param>
        /// <param name="realName">联系人姓名</param>
        /// <param name="phone">联系电话</param>
        /// <param name="endTime">结束日期</param>
        /// <param name="settleType">结算方式</param>
        /// <param name="serviceType">包裹类型</param>
        /// <param name="isInternational">运单类型</param>
        /// <param name="beginTime">开始日期</param>
        /// <param name="departmentCode">部门编号</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageCount">一页显示个数</param>
        /// <returns></returns>
        public IList<HAWB> FindHAWBsByCondition(string barCode, string countryCode, string regionCode, string loginName, string departmentCode, string companyName, string realName, string phone, DateTime? beginTime, DateTime? endTime, int settleType, int serviceType, bool? isInternational, int pageIndex, int pageCount)
        {
            return _hawbRepository.FindHAWBsByCondition(barCode, countryCode, regionCode, loginName, departmentCode,
                                                        companyName,
                                                        realName, phone, beginTime, endTime, settleType, serviceType,
                                                        isInternational, pageIndex, pageCount);
        }

        public IList<HAWB> FindHAWBsByCondition(string barCode, string countryName, string regionName, string userCode, string companyName,
                                               string realName, DateTime? beginTime, DateTime? endTime, int settleType, int serviceType,
                                               bool? isInternational)
        {
            return _hawbRepository.FindHAWBsByCondition(barCode, countryName, regionName, userCode, companyName,
                                                        realName, beginTime, endTime, serviceType, serviceType,
                                                        isInternational);
        }

        public IList<HAWB> FindHAWBsByCondition(string barCode, string countryName, string regionName, string userCode, string companyName, string realName, DateTime? beginTime, DateTime? endTime, int settleType, int serviceType, bool? isInternational, int pageIndex, int pageCount)
        {
            return _hawbRepository.FindHAWBsByCondition(barCode, countryName, regionName, userCode, companyName,
                                                        realName, beginTime, endTime, serviceType, serviceType,
                                                        isInternational, pageIndex, pageCount);
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

        /// <summary>
        /// 通过HID获取运单盒子
        /// </summary>
        /// <param name="HID">运单编号</param>
        /// <returns></returns>
        public HAWBBox FindHAWBBoxByHID(string HID)
        {
            return _hawbRepository.FindHAWBBoxByHID(HID)[0];
        }

        ///// <summary>
        ///// 通过航班编号获取航班
        ///// 由于这里没有barcode,所以使用默认的主键作为查询条件
        ///// </summary>
        ///// <param name="FID">航班</param>
        ///// <returns></returns>
        //public Flight FindFlightByFID(string FID)
        //{
        //    return _hawbRepository.FindFlightByFID(FID);
        //}

        /// <summary>
        /// 通过运单间接查询包裹信息
        /// 主要用于方便绑定GRID
        /// </summary>
        /// <param name="barCode">包裹编号</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="destinationCode">目的地三字码</param>
        /// <returns></returns>
        public IList<HAWB> FindHAWBsOfPackageByCondition(string barCode, DateTime? beginDate, DateTime? endDate, string destinationCode)
        {
            return _hawbRepository.FindHAWBsOfPackageByCondition(barCode, beginDate, endDate, destinationCode);
        }

        /// <summary>
        /// 判断运单和包裹同三字码
        /// 只有运单的地区三字码和包裹相符合，才能装入这个包裹
        /// 否则则不行
        /// 但是如果包裹的三字码是JPX，那么运单就被认为是混包，不用判断可以直接装入任何运单
        /// </summary>
        /// <param name="HAWBBarcode">运单号</param>
        /// <param name="packageBarcode">包裹号</param>
        /// <returns></returns>
        public bool JudgeHAWBOfPackageRepeat(string HAWBBarcode, string packageBarcode)
        {
            bool judge = false;
            HAWB hawb = _hawbRepository.FindHAWBByBarCode(HAWBBarcode);
            Package package = _hawbRepository.FindPackageByBarcode(packageBarcode);
            if(package!=null)
            {
                if (package.RegionCode.Equals("JPX")) return true;//混包不用判断
                if(hawb!=null)
                {
                    if (!string.IsNullOrEmpty(hawb.DeliverRegion))//交付人三字码不为空
                    {
                        if (package.RegionCode.Equals(hawb.DeliverRegion)) 
                            judge = true;//交付人字码和包裹字码相同，可以添加该包裹
                    }
                    else //交付人如果是空的，那么就依据收件人字码来计算
                        if (package.RegionCode.Equals(hawb.ConsigneeRegion)) judge = true;
                }
            }
            return judge;
        }

        /// <summary>
        /// 通过总运单号获取运单信息
        /// </summary>
        /// <param name="MID">总运单号</param>
        /// <returns></returns>
        public IList<HAWB> FindHAWBsByMID(string MID)
        {
            return _hawbRepository.FindHAWBsByMID(MID);
        }
    }
}
