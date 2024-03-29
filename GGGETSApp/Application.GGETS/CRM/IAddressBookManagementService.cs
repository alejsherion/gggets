﻿//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        地址本IBLL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.24
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace Application.GGETS
{
    public interface IAddressBookManagementService
    {
        AddressBook FindAddressBookByAID(string AID);
        void RemoveAddressBook(AddressBook addressBook);
        void RemoveBadAddressBook();
        void ModifyAddressBook(AddressBook addressBook);
        void AddAddressBook(AddressBook addressBook);
        IList<AddressBook> FindAddressBookByCondition(string companyCode, string depCode, string loginName,
                                                      DateTime? beginDate, DateTime? endDate);
    }
}
