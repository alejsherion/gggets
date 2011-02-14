using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace Application.GGETS
{
    public interface ICompanyUserManagementService
    {
        void CreateNewCompanyUser(User companyUser);
        void AddCompanyUserToExistingCompany(User companyUser);
        void ChangeCompanyUser(User companyUser);
        void RemoveCompanyUser(User companyUser);
    }
}
