//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        运单包裹BLL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.12
// 改版内容				创建并且修改
//************************************************************************
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
    public class PackageManagementService : IPackageManagementService
    {
        /// <summary>
        /// IOC Injecting into
        /// </summary>
        IPackageRepository _packageRepository;

        private IHAWBRepository _hawbRepository;

        public PackageManagementService(IPackageRepository packageRepository,IHAWBRepository hawbRepository)
        {
            _packageRepository = packageRepository;
            _hawbRepository = hawbRepository;
        }

        /// <summary>
        /// 新增包裹
        /// </summary>
        /// <param name="package">包裹(是一个运单的集合体)</param>
        public void AddPackage(Package package)
        {
            if (package == null)
                throw new ArgumentNullException("Package is null");
            IUnitOfWork unitOfWork = _packageRepository.UnitOfWork;
            _packageRepository.Add(package);
            //complete changes in this unit of work
            unitOfWork.Commit();
        }

        /// <summary>
        /// 通过条形码获取包裹
        /// </summary>
        /// <param name="barcode">条形码</param>
        /// <returns></returns>
        public Package FindPackageByBarcode(string barcode)
        {
            return _hawbRepository.FindPackageByBarcode(barcode);
        }

        /// <summary>
        /// 修改包裹
        /// </summary>
        /// <param name="package">包裹</param>
        public void ModifyPackage(Package package)
        {
            if (package == null)
                throw new ArgumentNullException("Package is null");
            IUnitOfWork unitOfWork = _packageRepository.UnitOfWork;
            _packageRepository.Modify(package);
            //complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
        }

        /// <summary>
        /// 包裹多条件查询
        /// </summary>
        /// <param name="barCode">包裹编号</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="destinationCode">目标地三字码</param>
        /// <returns></returns>
        public IList<Package> FindPackageByCondition(string barCode, DateTime? beginDate, DateTime? endDate, string destinationCode)
        {
            return _packageRepository.FindPackageByCondition(barCode, beginDate, endDate, destinationCode);
        }

        /// <summary>
        /// 判断加入的运单的PID是否为NULL，如果是NULL才可以加入包裹，否则等于重复添加运单
        /// </summary>
        /// <param name="barcode">运单编号</param>
        /// <returns></returns>
        public bool JudgePIDIsNull(string barcode)
        {
            bool judge = false;
            HAWB hawb = _hawbRepository.FindHAWBByBarCode(barcode);
            if(hawb!=null)
            {
                if (string.IsNullOrEmpty(Convert.ToString(hawb.PID))) judge = true;
            }
            return judge;
        }

        /// <summary>
        /// 判断包裹和运单的三字码是否重复
        /// </summary>
        /// <param name="barcode">运单编号</param>
        /// <param name="packageRegionCode">包裹三字码</param>
        /// <param name="isMix">是否为混包</param>
        /// <returns></returns>
        public bool JudgeRegionCodeIsRepeat(string barcode, string packageRegionCode, bool isMix)
        {
            bool judge = false;
            if (isMix) return true;
            else
            {
                //首先获取运单对象
                HAWB hawbObj = _hawbRepository.FindHAWBByBarCode(barcode);
                if (hawbObj != null)
                {
                    if(!string.IsNullOrEmpty(hawbObj.DeliverRegion))
                    {
                        if (hawbObj.DeliverRegion.Equals(packageRegionCode)) return true;
                    }
                    else
                    {
                        if(!string.IsNullOrEmpty(hawbObj.ConsigneeRegion))
                        {
                            if (hawbObj.ConsigneeRegion.Equals(packageRegionCode)) return true;
                        }
                    }
                }
            }
            return judge;
        }
    }
}
