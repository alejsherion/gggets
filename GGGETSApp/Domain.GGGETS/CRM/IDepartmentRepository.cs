//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        部门IDAL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.24
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Domain.Core;

namespace Domain.GGGETS
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Department FindDepartmentByDepCode(string depCode);
        Company FindCompanyByCompanyCode(string companyCode);
        User FindUserByLoginName(string loginName);
        AddressBook FindAddressBookByAID(string AID);
        IList<Department> FindDepartmentsByCompanyCode(string companyCode);
        IList<AddressBook> FindAllAddressBooksByCondition(string depCode,string companyCode, int addressType);
        IList<AddressBook> FindAddressBooksByDIDAndType(string DID, int type);
        Department FindDepartmentByDepcodeAndCompanyCode(string depcode, string companyCode);
    }
}
