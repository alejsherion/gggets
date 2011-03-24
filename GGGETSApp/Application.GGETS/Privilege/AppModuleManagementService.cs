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
using System.Linq;
using System.Text;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace Application.GGETS
{
    public class AppModuleManagementService : IAppModuleManagementService
    {
        #region 构造函数以及字段
        /// <summary>
        /// 系统模块的接口
        /// </summary>
        private readonly IAppModuleRepository _appModuleRepository;

        public AppModuleManagementService(IAppModuleRepository appModuleRepository)
        {
            _appModuleRepository = appModuleRepository;
        }
        #endregion

        #region 公共方法

        /// <summary>
        /// 根据模块获取角色ID
        /// </summary>
        /// <param name="moduleId">模块ID</param>
        /// <returns></returns>
        public IList<Role> GetRoleByModuleId(Guid moduleId)
        {
            return _appModuleRepository.GetRoleByModuleId(moduleId);
        }

        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="description">描述</param>
        /// <param name="startCreateTime">结束时间</param>
        /// <param name="endCreateTime">开始时间</param>
        /// <param name="nodetype">类型</param>
        /// <returns></returns>
        public IList<AppModule> QueryByCondtion(string description, DateTime? startCreateTime, DateTime? endCreateTime, NodeType nodetype=NodeType.所有)
        {
            return _appModuleRepository.QueryByCondtion(description, startCreateTime, endCreateTime
                                                        , nodetype);
        }

        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="id"></param>
        public void Remove(Guid id)
        {
            var appModule = GetSingleModuleByModuleId(id);
            if (appModule!=null)
            _appModuleRepository.Remove(appModule);
        }

        /// <summary>
        /// 修改模块
        /// </summary>
        /// <param name="module"></param>
        public void Modify(AppModule module)
        {
            _appModuleRepository.Modify(module);
        }

        /// <summary>
        /// 获取所有父级节点
        /// </summary>
        /// <returns></returns>
        public IList<AppModule> GeParentModule()
        {
            return _appModuleRepository.GeParentModule();
        }

        /// <summary>
        /// 添加模块
        /// </summary>
        /// <param name="appModule"></param>
        public void Add(AppModule appModule)
        {
            _appModuleRepository.Add(appModule);
            _appModuleRepository.UnitOfWork.Commit();
        }

        /// <summary>
        /// 获取单个模板
        /// </summary>
        /// <param name="moduleId">模块ID</param>
        /// <returns></returns>
        public AppModule GetSingleModuleByModuleId(Guid moduleId)
        {
            return _appModuleRepository.GetSingleModuleByModuleId(moduleId);
        }

        /// <summary>
        /// 获取所有模块
        /// </summary>
        /// <returns></returns>
        public IList<AppModule> GetAll()
        {
            var modules = _appModuleRepository.GetAll().ToList();
            return modules;
        }

        /// <summary>
        ///获取权限描述
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public ModulePrivilege GetPrivilegeById(Guid moduleId)
        {
            var module = _appModuleRepository.GetPrivilegeById(moduleId);
            return module;
        }

        #endregion
    }
}
