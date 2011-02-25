//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        部门DAL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.24
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Globalization;
using ETS.GGGETSApp.Infrastructure.Data.Core;
using ETS.GGGETSApp.Infrastructure.Data.Core.Extensions;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.Resources;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.UnitOfWork;
using ETS.GGGETSApp.Infrastructure.CrossCutting.Logging;
using ETS.GGGETSApp.Domain.Application.Entities;
using Domain.GGGETS;
using System.Linq;


namespace ETS.GGGETSApp.Infrastructure.Data.Persistence.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IGGGETSAppUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager) { }
        
        /// <summary>
        /// 通过部门账号获取部门信息
        /// </summary>
        /// <param name="depCode">部门账号</param>
        /// <returns></returns>
        public Department FindDepartmentByDepCode(string depCode)
        {
            if (string.IsNullOrEmpty(depCode)) throw new ArgumentException("DepCode is null!");
            //Get Assemble's Context
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            //don't forget open package's load:HAWBs
            return
                context.Department.Where(d => d.DepCode == depCode).Include(d => d.HAWBs).Include(d => d.Users).Include(
                    d => d.Company).Include(d => d.AddressBooks).SingleOrDefault();
        }

        #region 公司操作
        public Company FindCompanyByCompanyCode(string companyCode)
        {
            if (string.IsNullOrEmpty(companyCode)) throw new ArgumentException("Company is null!");
            //Get Assemble's Context
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            //don't forget open package's load:HAWBs
            return
                context.Company.Where(it => it.CompanyCode == companyCode).Include(it => it.Departments).SingleOrDefault();
        }

        /// <summary>
        /// 通过用户账号获取用户信息
        /// </summary>
        /// <param name="loginName">用户账号</param>
        /// <returns></returns>
        public User FindUserByLoginName(string loginName)
        {
            if (string.IsNullOrEmpty(loginName)) throw new ArgumentException("User is null!");
            //Get Assemble's Context
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            //don't forget open package's load:HAWBs
            return
                context.User.Where(it => it.LoginName == loginName).Include(it => it.Department).SingleOrDefault();
        }

        /// <summary>
        /// 通过序号获取地址本
        /// </summary>
        /// <param name="AID">序号</param>
        /// <returns></returns>
        public AddressBook FindAddressBookByAID(string AID)
        {
            if (string.IsNullOrEmpty(AID)) throw new ArgumentException("AID is null!");
            //Get Assemble's Context
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            Guid guidObj = new Guid(AID);
            //don't forget open package's load:HAWBs
            return
                context.AddressBook.Where(it => it.AID == guidObj).Include(it => it.User).Include(it=>it.Department).SingleOrDefault();
        }

        /// <summary>
        /// 通过公司账号获取部门信息
        /// </summary>
        /// <param name="companyCode">公司账号</param>
        /// <returns></returns>
        public IList<Department> FindDepartmentsByCompanyCode(string companyCode)
        {
            if (string.IsNullOrEmpty(companyCode)) throw new ArgumentException("CompanyCode is null!");
            //Get Assemble's Context
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            //don't forget open package's load:HAWBs
            return
                context.Department.Where(it => it.CompanyCode == companyCode).ToList();
        }

        #endregion
    }
}
