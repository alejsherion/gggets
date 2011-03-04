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
        public Department FindDepartmentByDepCodeAndCompanyCode(string depCode, string companyCode)
        {
            return _departmentRepository.FindDepartmentByDepcodeAndCompanyCode(depCode, companyCode);
        }

        /// <summary>
        /// 通过公司账号获取部门信息
        /// </summary>
        /// <param name="companyCode">公司账号</param>
        /// <returns></returns>
        public IList<Department> FindDepartmentsByCompanyCode(string companyCode)
        {
            return _departmentRepository.FindDepartmentsByCompanyCode(companyCode);
        }

        /// <summary>
        /// 获取所有的发货人地址本通过部门账号
        /// </summary>
        /// <param name="depCode">部门账号</param>
        /// <param name="companyCode">公司账号</param>
        /// <returns></returns>
        public IList<AddressBook> FindAllShipAddressesByDepCodeAndCompanyCode(string depCode, string companyCode)
        {
            return _departmentRepository.FindAllAddressBooksByCondition(depCode,companyCode, 0);
        }

        /// <summary>
        /// 获取所有的收货人地址本通过部门账号
        /// </summary>
        /// <param name="depCode">部门账号</param>
        /// <param name="companyCode">公司账号</param>
        /// <returns></returns>
        public IList<AddressBook> FindAllDeliveryAddressesByDepCodeAndCompanyCode(string depCode, string companyCode)
        {
            return _departmentRepository.FindAllAddressBooksByCondition(depCode,companyCode, 1);
        }

        /// <summary>
        /// 获取所有的交付人地址本通过部门账号
        /// </summary>
        /// <param name="depCode">部门账号</param>
        /// <param name="companyCode">公司账号</param>
        /// <returns></returns>
        public IList<AddressBook> FindAllForwarderAddressesByDepCodeAndCompanyCode(string depCode, string companyCode)
        {
            return _departmentRepository.FindAllAddressBooksByCondition(depCode,companyCode, 2);
        }

        /// <summary>
        /// 判断地址本重复问题 返回ture-是重复；false-不重复
        /// </summary>
        /// <param name="AID">地址本序号</param>
        /// <param name="contactorName">联系人姓名</param>
        /// <returns></returns>
        public bool JudgeAddressBookWhetherRepeat(string AID,string contactorName)
        {
            bool judge = false;
            AddressBook addressBook = _departmentRepository.FindAddressBookByAID(AID);
            if(addressBook!=null)
            {
                if(!string.IsNullOrEmpty(contactorName))
                {
                    if (addressBook.ContactorName.Equals(contactorName)) judge = true;
                }
            }
            return judge;
        }

        /// <summary>
        /// 判断当前类型下地址重复
        /// </summary>
        /// <param name="AID">地址序号</param>
        /// <param name="tempAddress">地址</param>
        /// <param name="tempCountryCode">国家二字码</param>
        /// <param name="tempProvience">省</param>
        /// <param name="tempRegionCode">地区三字码</param>
        /// <param name="tempPostCode">邮政编码</param>
        /// <param name="tempPhone">电话</param>
        /// <param name="tempName">公司名称</param>
        /// <param name="tempContactorName">联系人</param>
        /// <returns></returns>
        public bool JudgeRepeat(string AID, string tempName, string tempAddress, string tempCountryCode, string tempProvience, string tempRegionCode, string tempPostCode, string tempContactorName, string tempPhone)
        {
            //验证变量
            bool judge = true;
            //先和自己比
            AddressBook addressBookSelf = _departmentRepository.FindAddressBookByAID(Convert.ToString(AID));
            if (addressBookSelf == null) throw new ArgumentNullException("AddressBook is null!");
            if (addressBookSelf.CountryCode.Equals(tempCountryCode) && addressBookSelf.Provience.Equals(tempProvience) && addressBookSelf.RegionCode.Equals(tempRegionCode) && addressBookSelf.PostCode.Equals(tempPostCode) && addressBookSelf.ContactorName.Equals(tempContactorName) && addressBookSelf.Phone.Equals(tempPhone) && addressBookSelf.Name.Equals(tempName) && addressBookSelf.Address.Equals(tempAddress))
                judge = true;
            else
            {
                //再获取其他类型地址集合比，排除自己
                IList<AddressBook> addressBooks =
                    _departmentRepository.FindAddressBooksByDIDAndType(Convert.ToString(addressBookSelf.DID),
                                                                       addressBookSelf.AddressType).Where(it => it.AID != addressBookSelf.AID).ToList();
                int count = 0;//计数器
                foreach (AddressBook temp in addressBooks)
                {
                    if (!temp.Address.Equals(tempAddress) || !temp.CountryCode.Equals(tempCountryCode) || !temp.Provience.Equals(tempProvience) || !temp.RegionCode.Equals(tempRegionCode) || !temp.PostCode.Equals(tempPostCode) || !temp.ContactorName.Equals(tempContactorName) || !temp.Phone.Equals(tempPhone) || !temp.Name.Equals(tempName))
                    {
                        count++;
                    }
                }
                if (count != addressBooks.Count) judge = false;
            }
            return judge;
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
            return _departmentRepository.FindDepartmentsByCondition(companyCode, depCode, depName);
        }
    }
}
