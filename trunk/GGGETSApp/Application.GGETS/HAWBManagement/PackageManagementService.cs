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
    }
}
