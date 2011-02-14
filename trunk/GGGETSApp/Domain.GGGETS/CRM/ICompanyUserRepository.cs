using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETS.GGGETSApp.Domain.Core;
using ETS.GGGETSApp.Domain.Application.Entities;


namespace Domain.GGGETS
{
    public interface ICompanyUserRepository:IRepository<User>
    {
        User FindCompanyUserByLoginName(string loginName);
        
    }
}
