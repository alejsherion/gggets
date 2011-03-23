//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        HS品名管理IBLL
// 作成者				ZhiWei.Shen
// 改版日				2011.03.13
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace Application.GGETS
{
    public interface IHSPropertyManagementService
    {
        IList<HSProperty> GetAll();
        void AddHSProperty(HSProperty hsproperty);
        IList<HSProperty> GetHSPropertiesByCondition(string propertyName);
        void ModifyProperty(HSProperty hsproperty);
        HSProperty FindHSPropertyByHSPID(string HSPID);
        void RemoveHSProperty(HSProperty hsproperty);
        bool JudgeHSPropertyIsExist(string propertyName);
    }
}
