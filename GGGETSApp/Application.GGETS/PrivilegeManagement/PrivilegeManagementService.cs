//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        权限BLL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.12
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using Application.GGETS;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace Application.GGETS
{
    public class PrivilegeManagementService : IPrivilegeManagementService
    {
        #region 字段
        /// <summary>
        /// IOC Injecting into
        /// </summary>
        IPrivilegeRepository _privilegeRepository;
        #endregion

        #region 构造函数
        public PrivilegeManagementService(IPrivilegeRepository privilegeRepository)
        {
            if (privilegeRepository != null) _privilegeRepository = privilegeRepository;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 根据权限ids读出权限
        /// </summary>
        /// <param name="privilegeIds">权限ids集合</param>
        /// <returns></returns>
        public ModulePrivilege GetCurentModulePrivilege(Guid[] privilegeIds)
        {
            return _privilegeRepository != null ? _privilegeRepository.GetCurentModulePrivilege(privilegeIds) : null;
        }

        /// <summary>
        /// 根据权限ids读出权限
        /// </summary>
        /// <param name="privilegeIds">权限ids集合</param>
        /// <returns></returns>
        public IList<Privilege> GePrivilegeByIds(Guid[] privilegeIds)
        {
            return _privilegeRepository != null ? _privilegeRepository.GePrivilegeByIds(privilegeIds) : null;
        }

        #endregion
    }
}
