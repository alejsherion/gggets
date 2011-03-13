//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        HS分配BLL
// 作成者				ZhiWei.Shen
// 改版日				2011.03.13
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
    public interface IHSRelationManagementService
    {
        IList<HSRelation> FindHSRelationsByHSID(string HSID);
        void ModifyHSRelation(HSRelation relation);
        void AddHSRelation(HSRelation relation);
        void RemoveHSRelation(HSRelation relation);
    }
}
