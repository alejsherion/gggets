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
                context.User.Where(it => it.LoginName == loginName).Include(it => it.Department).Include(it=>it.AddressBooks).SingleOrDefault();
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

        /// <summary>
        /// 通过部门编号
        /// </summary>
        /// <param name="depCode">部门账号</param>
        /// <param name="companyCode">公司账号</param>
        /// <param name="addressType">地址本类型 0=发货地址,1=送货地址,2=交付地址</param>
        /// <returns></returns>
        public IList<AddressBook> FindAllAddressBooksByCondition(string depCode,string companyCode, int addressType)
        {
            IEnumerable<AddressBook> addressBooks = null;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            //using (IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork)
            //{
                if (context != null)
                {
                    string DID = string.Empty;
                    Department department = FindDepartmentByDepcodeAndCompanyCode(depCode, companyCode);
                    if(department!=null) DID = department.DID.ToString();
                    else return null;
                    addressBooks = context.AddressBook.Select(a => a);
                    if (!string.IsNullOrEmpty(depCode)) addressBooks = addressBooks.Where(a => a.DID == new Guid(DID));
                    if (addressType == 0 || addressType == 1 || addressType == 2) addressBooks = addressBooks.Where(a => a.AddressType == addressType);
                }
                else
                {
                    throw new InvalidOperationException(string.Format(
                                                                CultureInfo.InvariantCulture,
                                                                Messages.exception_InvalidStoreContext,
                                                                GetType().Name));
                }
                return addressBooks.OrderByDescending(p => p.CreateTime).ToList();
            //}
        }

        /// <summary>
        /// 通过部门编号和地址类型获取地址信息
        /// </summary>
        /// <param name="DID">部门编号</param>
        /// <param name="type">地址类型</param>
        /// <returns></returns>
        public IList<AddressBook> FindAddressBooksByDIDAndType(string DID, int type)
        {
            IEnumerable<AddressBook> addressBooks = null;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            if(context!=null)
            {
                addressBooks = context.AddressBook.Select(a => a);
                if (!string.IsNullOrEmpty(DID)) addressBooks = addressBooks.Where(a => a.DID == new Guid(DID));
                addressBooks = addressBooks.Where(a => a.AddressType == type);
            }
            else
            {
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name));
            }
            return addressBooks.ToList();
        }

        /// <summary>
        /// 通过部门账号和公司账号获取部门信息
        /// </summary>
        /// <param name="depcode">部门账号</param>
        /// <param name="companyCode">公司账号</param>
        /// <returns></returns>
        public Department FindDepartmentByDepcodeAndCompanyCode(string depcode, string companyCode)
        {
            if (string.IsNullOrEmpty(depcode)) throw new ArgumentException("Depcode is null!");
            if (string.IsNullOrEmpty(companyCode)) throw new ArgumentException("CompanyCode is null!");
            //Get Assemble's Context
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            //don't forget open package's load:HAWBs
            return
                context.Department.Where(it => it.DepCode == depcode).Where(it => it.CompanyCode == companyCode).
                    SingleOrDefault();
        }

        /// <summary>
        /// 部门多条件查询
        /// </summary>
        /// <param name="companyCode">公司账号</param>
        /// <param name="depCode">部门账号</param>
        /// <param name="depName">部门名称</param>
        /// <returns></returns>
        public IList<Department> FindDepartmentsByCondition(string companyCode, string depCode, string depName)
        {
            IEnumerable<Department> departments = null;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;

            if (context != null)
            {
                departments = context.Department.Select(it => it);
                if (!string.IsNullOrEmpty(companyCode)) departments = departments.Where(it => it.CompanyCode == companyCode);
                if (!string.IsNullOrEmpty(depCode)) departments = departments.Where(it => it.DepCode == depCode);
                if (!string.IsNullOrEmpty(depName)) departments = departments.Where(it => it.DepName.StartsWith(depName));
            }
            else
            {
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name));
            }
            return departments.ToList();
        }

        /// <summary>
        /// 根据DID查询部门信息
        /// </summary>
        /// <param name="DID">部门序号</param>
        /// <returns></returns>
        public Department FindDepartmentByDID(string DID)
        {
            if (string.IsNullOrEmpty(DID)) throw new ArgumentException("DID is null!");
            //Get Assemble's Context
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;
            //don't forget open package's load:HAWBs
            return
                context.Department.Where(it => it.DID == new Guid(DID)).SingleOrDefault();
        }

        /// <summary>
        /// 用户多条件查询
        /// </summary>
        /// <param name="companyCode">公司账号</param>
        /// <param name="depCode">部门账号</param>
        /// <param name="loginName">用户名</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public IList<User> FindUsersByCondition(string companyCode, string depCode, string loginName, DateTime? beginDate, DateTime? endDate)
        {
            IEnumerable<User> users = null;
            IGGGETSAppUnitOfWork context = UnitOfWork as IGGGETSAppUnitOfWork;

            if (context != null)
            {
                users = context.User.Include(it => it.Department).Select(it => it);
                if (!string.IsNullOrEmpty(companyCode)) users = users.Where(it => it.Department.CompanyCode == companyCode);
                if (!string.IsNullOrEmpty(depCode)) users = users.Where(it => it.Department.DepCode == depCode);
                if (!string.IsNullOrEmpty(loginName)) users = users.Where(it => it.LoginName == loginName);
                if (beginDate.HasValue)
                {
                    if (beginDate.Value != DateTime.MinValue)
                        users =
                            users.Where(
                                p =>
                                p.CreateTime >=
                                new DateTime(beginDate.Value.Year, beginDate.Value.Month, beginDate.Value.Day, 0, 0,
                                             0));
                }
                if (endDate.HasValue)
                {
                    if (endDate.Value != DateTime.MinValue)
                        users =
                            users.Where(
                                p =>
                                p.CreateTime <=
                                new DateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day, 23, 59, 59));
                }
            }
            else
            {
                throw new InvalidOperationException(string.Format(
                                                            CultureInfo.InvariantCulture,
                                                            Messages.exception_InvalidStoreContext,
                                                            GetType().Name));
            }
            return users.OrderByDescending(p => p.CreateTime).ToList();
        }
        #endregion
    }
}
