using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace Application.GGETS
{
    public interface ICompanyUserManagementService
    {
        void CreateNewCompanyUser(CompanyUser companyUser);
        void AddCompanyUserToExistingCompany(CompanyUser companyUser);
        void ChangeCompanyUser(CompanyUser companyUser);
        void RemoveCompanyUser(CompanyUser companyUser);
    }
}
