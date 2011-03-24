//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        功能节点IBLL
// 作成者				hong.li
// 改版日				2011.02.12
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace Application.GGETS
{
    public interface IAppModuleManagementService
    {
        /// <summary>
        /// 根据模块获取角色ID
        /// </summary>
        /// <param name="moduleId">模块ID</param>
        /// <returns></returns>
        IList<Role> GetRoleByModuleId(Guid moduleId);

        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="description">描述</param>
        /// <param name="startCreateTime">结束时间</param>
        /// <param name="endCreateTime">开始时间</param>
        /// <param name="nodetype">类型</param>
        /// <returns></returns>
        IList<AppModule> QueryByCondtion(string description, DateTime? startCreateTime
                                    , DateTime? endCreateTime, NodeType nodetype = NodeType.所有
            );

        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="id"></param>
        void Remove(Guid id);

        /// <summary>
        /// 修改模块
        /// </summary>
        /// <param name="module"></param>
        void Modify(AppModule module);

        /// <summary>
        /// 获取所有父级节点
        /// </summary>
        /// <returns></returns>
        IList<AppModule> GeParentModule();

        /// <summary>
        /// 添加模块
        /// </summary>
        /// <param name="appModule"></param>
        void Add(AppModule appModule);

        /// <summary>
        /// 获取单个模板
        /// </summary>
        /// <param name="moduleId">模块ID</param>
        /// <returns></returns>
        AppModule GetSingleModuleByModuleId(Guid moduleId);

        /// <summary>
        /// 获取所有模块
        /// </summary>
        /// <returns></returns>
        IList<AppModule> GetAll();

        /// <summary>
        ///获取权限描述
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        ModulePrivilege GetPrivilegeById(Guid moduleId);
    }
}
