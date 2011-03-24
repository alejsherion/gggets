//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        角色IDAL
// 作成者				hong.li
// 改版日				2011.02.12
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Domain.Core;

namespace Domain.GGGETS
{
    public interface IRoleRepository : IRepository<Role>
    {
        /// <summary>
        /// 根据角色ID获取用户
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        IList<SysUser> GetUserByRoleId(Guid roleId);

        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="name">名字</param>
        /// <param name="description">描述</param>
        /// <param name="startCreateTime">结束时间</param>
        /// <param name="endCreateTime">开始时间</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        IList<Role> QueryByCondtion(string name, string description, DateTime? startCreateTime
                                    , DateTime? endCreateTime, RoleStatus status = RoleStatus.正常);

        /// <summary>
        /// 根据角色读取权限
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        IList<AppModule> GetAppModuleByRoleId(Guid roleId);

        /// <summary>
        /// 根据角色ID读取角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        Role GetRoleByRoleId(Guid roleId);

        
    }
}
