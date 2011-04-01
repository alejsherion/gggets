//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        组织架构
// 作成者				hong.li
// 改版日				2011.02.23
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Domain.Core;

namespace Domain.GGGETS
{
    public interface IOrganizationRepository : IRepository<OrganizationChart>
    {
        /// <summary>
        /// 根据组织ID获取其下面的组织
        /// </summary>
        /// <param name="did">组织ID</param>
        /// <returns></returns>
        IList<OrganizationChart> GeOrganizationByDid(Guid did);

        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="organizationName">组织名</param>
        /// <param name="startCreateTime">结束时间</param>
        /// <param name="endCreateTime">开始时间</param>
        /// <param name="organizationCode">组织编号</param>
        /// <param name="parentId">父级架构ID</param>
        /// <returns></returns>
        IList<OrganizationChart> QueryByCondtion(string organizationName, DateTime? startCreateTime
                                    , DateTime? endCreateTime, string organizationCode, Guid? parentId = null);

        /// <summary>
        /// 获取单个组织
        /// </summary>
        /// <param name="did">组织ID</param>
        /// <returns></returns>
        OrganizationChart GetOrganizationByDid(Guid did);

        /// <summary>
        /// 获取所有组织结构
        /// </summary>
        /// <returns></returns>
        IList<OrganizationChart> GeAllOrganization();


        /// <summary>
        /// 获取该组织下的所有用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<SysUser> GetSysUserByDid(Guid id);

        /// <summary>
        /// 根据组织ID获取其上面的组织
        /// </summary>
        /// <param name="did">父级组织ID</param>
        /// <returns></returns>
        IList<OrganizationChart> GetParentOrganizationByDid(Guid did);

        /// <summary>
        /// 删除所有
        /// </summary>
        /// <param name="id"></param>
        void RemoveAll(Guid id);
    }
}
