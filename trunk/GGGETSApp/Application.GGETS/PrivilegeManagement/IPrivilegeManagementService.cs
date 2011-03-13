//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        权限IBLL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.12
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace Application.GGETS
{
    public interface IPrivilegeManagementService
    {
        /// <summary>
        /// 根据权限ids读出权限
        /// </summary>
        /// <param name="privilegeIds">权限ids集合</param>
        /// <returns></returns>
        ModulePrivilege GetCurentModulePrivilege(Guid[] privilegeIds);

        /// <summary>
        /// 根据权限ids读出权限
        /// </summary>
        /// <param name="privilegeIds">权限ids集合</param>
        /// <returns></returns>
        IList<Privilege> GePrivilegeByIds(Guid[] privilegeIds);
    }
}
