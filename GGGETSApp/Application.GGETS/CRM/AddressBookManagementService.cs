//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        地址本BLL
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
    public class AddressBookManagementService:IAddressBookManagementService
    {
        /// <summary>
        /// IOC Injecting into
        /// </summary>
        private IDepartmentRepository _departmentRepository;

        private IAddressBookRepository _addressBookRepository;

        public AddressBookManagementService(IDepartmentRepository departmentRepository, IAddressBookRepository addressBookRepository)
        {
            _departmentRepository = departmentRepository;
            _addressBookRepository = addressBookRepository;
        }

        /// <summary>
        /// 通过序号获取地址本
        /// </summary>
        /// <param name="AID">序号</param>
        /// <returns></returns>
        public AddressBook FindAddressBookByAID(string AID)
        {
            return _departmentRepository.FindAddressBookByAID(AID);
        }

        /// <summary>
        /// 移除地址本
        /// </summary>
        /// <param name="addressBook">地址本</param>
        public void RemoveAddressBook(AddressBook addressBook)
        {
            if (addressBook == null)
                throw new ArgumentNullException("AddressBook is null");
            IUnitOfWork unitOfWork = _addressBookRepository.UnitOfWork;
            _addressBookRepository.Add(addressBook);
            //complete changes in this unit of work
            unitOfWork.Commit();
        }

        /// <summary>
        /// 移除所有NULL地址本
        /// </summary>
        public void RemoveBadAddressBook()
        {
            IUnitOfWork unitOfWork = _addressBookRepository.UnitOfWork;
            IList<AddressBook> list = _addressBookRepository.GetAllBadAddressBook();
            foreach(AddressBook addressbook in list)
            {
                _addressBookRepository.Remove(addressbook);
            }
            //complete changes in this unit of work
            unitOfWork.Commit();
        }

        /// <summary>
        /// 修改地址本
        /// </summary>
        /// <param name="addressBook">地址本</param>
        public void ModifyAddressBook(AddressBook addressBook)
        {
            if (addressBook == null)
                throw new ArgumentNullException("AddressBook is null");
            IUnitOfWork unitOfWork = _addressBookRepository.UnitOfWork;
            _addressBookRepository.Modify(addressBook);
            //complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
        }

        /// <summary>
        /// 新增地址本
        /// </summary>
        /// <param name="addressBook">地址本</param>
        public void AddAddressBook(AddressBook addressBook)
        {
            if (addressBook == null)
                throw new ArgumentNullException("AddressBook is null");
            IUnitOfWork unitOfWork = _addressBookRepository.UnitOfWork;
            _addressBookRepository.Add(addressBook);
            //complete changes in this unit of work
            unitOfWork.Commit();
        }
    }
}
