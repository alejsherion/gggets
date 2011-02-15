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

namespace Application.GGETS.HAWBManagement
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
        /// 通过包裹编号获取该包裹下所有运单
        /// </summary>
        /// <param name="PID">包裹编号</param>
        /// <returns></returns>
        public IList<Package> GetAllHAWBsByPID(string PID)
        {
            if (string.IsNullOrEmpty(PID)) throw new ArgumentNullException("PID is null");
            //open HAWB's load
            Package package = _packageRepository.GetSinglePackageByPid(PID);
            return null;
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
    }
}
