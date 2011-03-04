//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        部门IBLL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.24
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using ETS.GGGETSApp.Domain.Application.Entities;


namespace Application.GGETS
{
    public interface IDepartmentManagementService
    {
        void AddDepartment(Department department);
        void ModifyDepartment(Department department);
        Department FindDepartmentByDepCodeAndCompanyCode(string depCode,string companyCode);
        IList<Department> FindDepartmentsByCompanyCode(string companyCode);
        IList<AddressBook> FindAllShipAddressesByDepCodeAndCompanyCode(string depCode, string companyCode);
        IList<AddressBook> FindAllDeliveryAddressesByDepCodeAndCompanyCode(string depCode, string companyCode);
        IList<AddressBook> FindAllForwarderAddressesByDepCodeAndCompanyCode(string depCode, string companyCode);
        bool JudgeAddressBookWhetherRepeat(string AID,string contactorName);
        bool JudgeRepeat(string AID, string tempName, string tempAddress, string tempCountryCode, string tempProvience, string tempRegionCode, string tempPostCode, string tempContactorName, string tempPhone);
        IList<Department> FindDepartmentsByCondition(string companyCode, string depCode, string depName);
    }
}
