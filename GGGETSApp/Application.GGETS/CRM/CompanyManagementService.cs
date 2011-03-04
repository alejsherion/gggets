//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        公司BLL
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
    public class CompanyManagementService:ICompanyManagementService
    {
        /// <summary>
        /// IOC Injecting into
        /// </summary>
        IDepartmentRepository _departmentRepository;

        private ICompanyRepository _companyRepository;

        public CompanyManagementService(IDepartmentRepository departmentRepository,ICompanyRepository companyRepository)
        {
            _departmentRepository = departmentRepository;
            _companyRepository = companyRepository;
        }

        /// <summary>
        /// 通过公司账号获取公司信息
        /// </summary>
        /// <param name="companyCode">公司账号</param>
        /// <returns></returns>
        public Company FindCompanyByCompanyCode(string companyCode)
        {
            return _departmentRepository.FindCompanyByCompanyCode(companyCode);
        }

        /// <summary>
        /// 新增公司信息
        /// </summary>
        /// <param name="company">公司</param>
        public void AddCompany(Company company)
        {
            if (company == null)
                throw new ArgumentNullException("Company is null");
            IUnitOfWork unitOfWork = _companyRepository.UnitOfWork;
            _companyRepository.Add(company);
            //complete changes in this unit of work
            unitOfWork.Commit();
        }

        /// <summary>
        /// 公司多条件查询
        /// </summary>
        /// <param name="companyCode">公司编号</param>
        /// <param name="fullName">公司全称</param>
        /// <param name="shortName">公司简称</param>
        /// <param name="contactor">联系人</param>
        /// <param name="contactorPhone">联系人电话</param>
        /// <returns></returns>
        public IList<Company> FindCompaniesByCondition(string companyCode, string fullName, string shortName, string contactor, string contactorPhone)
        {
            return _companyRepository.FindCompaniesByCondition(companyCode, fullName, shortName, contactor,
                                                               contactorPhone);
        }
    }
}
