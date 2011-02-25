//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        部门BLL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.24
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
    public class DepartmentManagementService:IDepartmentManagementService
    {
         /// <summary>
        /// IOC Injecting into
        /// </summary>
        private IDepartmentRepository _departmentRepository;

        private IHAWBRepository _hawbRepository;

        private IUserRepository _userRepository;

        private ICompanyRepository _companyRepository;

        private IAddressBookRepository _addressBookRepository;

        public DepartmentManagementService(IDepartmentRepository departmentRepository, IHAWBRepository hawbRepository,IUserRepository userRepository,ICompanyRepository companyRepository,IAddressBookRepository addressBookRepository)
        {
            _departmentRepository = departmentRepository;
            _hawbRepository = hawbRepository;
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _addressBookRepository = addressBookRepository;
        }

        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="department">部门</param>
        public void AddDepartment(Department department)
        {
            if (department == null)
                throw new ArgumentNullException("Department is null");
            IUnitOfWork unitOfWork = _departmentRepository.UnitOfWork;
            _departmentRepository.Add(department);
            //complete changes in this unit of work
            unitOfWork.Commit();
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="department">部门</param>
        public void ModifyDepartment(Department department)
        {
            if (department == null)
                throw new ArgumentNullException("Department is null");
            IUnitOfWork unitOfWork = _departmentRepository.UnitOfWork;
            _departmentRepository.Modify(department);
            //这里需要额外进行一次判断
            //当地址本取出了外键约束之后要将NULL值进行删除
            IAddressBookManagementService addressBookBll = new AddressBookManagementService(_departmentRepository,_addressBookRepository);
            addressBookBll.RemoveBadAddressBook();

            //complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
        }

        /// <summary>
        /// 通过部门账号获取部门信息
        /// </summary>
        /// <param name="depCode">部门账号</param>
        /// <returns></returns>
        public Department FindDepartmentByDepCode(string depCode)
        {
            return _departmentRepository.FindDepartmentByDepCode(depCode);
        }
    }
}
