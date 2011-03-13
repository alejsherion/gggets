//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        HS品名管理IDAL
// 作成者				ZhiWei.Shen
// 改版日				2011.03.13
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Domain.Core;

namespace Domain.GGGETS
{
    public interface IHSPropertyRepository : IRepository<HSProperty>
    {
        IList<HSProperty> GetAll();
        IList<HSProperty> GetHSPropertiesByCondition(string propertyName);
        HSProperty FindHSPropertyByHSPID(string HSPID);
    }
}
