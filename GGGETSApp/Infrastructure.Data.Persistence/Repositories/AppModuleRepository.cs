//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        模块DAL
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
using ETS.GGGETSApp.Infrastructure.CrossCutting.Logging;
using ETS.GGGETSApp.Infrastructure.Data.Core;
using ETS.GGGETSApp.Infrastructure.Data.Core.Extensions;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.UnitOfWork;

namespace ETS.GGGETSApp.Infrastructure.Data.Persistence.Repositories
{
    ///<summary>
    /// 模块
    ///</summary>
    public class AppModuleRepository : Repository<AppModule>, IAppModuleRepository
    {
        #region 构造函数以及字段
        private readonly Repository<Role_Privilege> _privilegeRole;//角色和权限关联关系表操作

        ///<summary>
        ///</summary>
        ///<param name="unitOfWork"></param>
        ///<param name="traceManager"></param>
        public AppModuleRepository(IGGGETSAppUnitOfWork unitOfWork, ITraceManager traceManager)
            : base(unitOfWork, traceManager)
        {
            _privilegeRole = new Repository<Role_Privilege>(unitOfWork, traceManager);
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
            var context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
            {
                var tempSysUserRoleIds = context.Role_Privilege.Where(it => it.ModuleID == moduleId).Where(it => it.RoleID != null);
                if (tempSysUserRoleIds.Count() == 0) return null;
                var sysUserRoleIds = tempSysUserRoleIds.Select(it => it.RoleID.Value).ToArray();
                if (sysUserRoleIds.Count() == 0) return null;
                var roleArray = context.Role.Where(
                      BuildOrExpression<Role, Guid>(p => p.RoleID, sysUserRoleIds)
                      ).ToList();
                return roleArray;

            }
            return null;
        }

        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="description">描述</param>
        /// <param name="startCreateTime">结束时间</param>
        /// <param name="endCreateTime">开始时间</param>
        /// <param name="nodetype">类型</param>
        /// <returns></returns>
        public IList<AppModule> QueryByCondtion(string description, DateTime? startCreateTime, DateTime? endCreateTime, NodeType nodetype = NodeType.所有)
        {
            try
            {
                var context = UnitOfWork as IGGGETSAppUnitOfWork;
                if (context != null)
                {
                    var tempAppModule = context.AppModule.Include(it=>it.Role_Privilege).Select(it => it);
                    if (!string.IsNullOrEmpty(description))
                        tempAppModule = tempAppModule.Where(a => a.Description.Contains(description));
                    if (startCreateTime != null) tempAppModule = tempAppModule.Where(a => a.CreateTime >= startCreateTime);
                    if (endCreateTime != null) tempAppModule = tempAppModule.Where(a => a.CreateTime <= endCreateTime);
                    if (nodetype != NodeType.所有)
                    {
                       switch(nodetype)
                       {
                           case NodeType.子节点:
                               tempAppModule = tempAppModule.Where(a => a.IsLeft);
                               break;
                           case NodeType.父节点:
                                tempAppModule = tempAppModule.Where(a => a.IsLeft==false);
                               break;
                       }
                    }
                    return tempAppModule.ToList();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取所有父级节点
        /// </summary>
        /// <returns></returns>
        public IList<AppModule> GeParentModule()
        {
            try
            {
                var context = UnitOfWork as IGGGETSAppUnitOfWork;
                if (context != null)
                {
                    var tempAppModule = context.AppModule.Include(it => it.Role_Privilege).Select(it => it);
                    tempAppModule = tempAppModule.Where(a => a.IsLeft==false);
                    return tempAppModule.ToList();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取单个模板
        /// </summary>
        /// <param name="moduleId">模块ID</param>
        /// <returns></returns>
        public AppModule GetSingleModuleByModuleId(Guid moduleId)
        {
            try
            {
                var context = UnitOfWork as IGGGETSAppUnitOfWork;
                if (context != null)
                {
                    var tempAppModule =
                        context.AppModule.Include(it => it.Role_Privilege).Where(it =>                                                       it.ModuleID == moduleId).SingleOrDefault();
                    return tempAppModule;

                
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="item">系统角色</param>
        public override void Remove(AppModule item)
        {
            if (item == null) throw new ArgumentNullException();
            if (item.Role_Privilege != null && item.Role_Privilege.Count != 0)
            {
                foreach (var appModuleItem in item.Role_Privilege)
                {
                    _privilegeRole.Remove(appModuleItem);
                }
                item.Role_Privilege.Clear();
            }
            _privilegeRole.UnitOfWork.Commit();
            base.Remove(item);
            UnitOfWork.Commit();
        }

        /// <summary>
        /// 修改模块
        /// </summary>
        /// <param name="item"></param>
        public override void Modify(AppModule item)
        {
            if (item == null) throw new ArgumentNullException();
            if (item.Role_Privilege == null || item.Role_Privilege.Count==0)
            {
                var context = UnitOfWork as IGGGETSAppUnitOfWork;
                if (context != null)
                {
                    var tempRolePrivilege = context.Role_Privilege.Where(it => it.ModuleID == item.ModuleID).ToList();
                    if(tempRolePrivilege.Count!=0)
                    {
                        foreach (var appModuleItem in tempRolePrivilege)
                        {
                            _privilegeRole.Remove(appModuleItem);
                        }
                        _privilegeRole.UnitOfWork.Commit();
                    }

                }
            }
            base.Modify(item);
            UnitOfWork.CommitAndRefreshChanges();
            DeltePrivilegeRole();
        }

        /// <summary>
        ///获取权限描述
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public ModulePrivilege GetPrivilegeById(Guid moduleId)
        {
            var appmodel = GetSingleModuleByModuleId(moduleId);
            if (appmodel == null) return null;
            var appModelPrivilege = new ModulePrivilege { ModuleName = appmodel.Description };
            if (appmodel.IsLeft)
            {
                if (appmodel.PrivilegeDesc == null) appmodel.PrivilegeDesc = 0;
                appModelPrivilege.Url = appmodel.URL;
                appModelPrivilege.PrivilegeDesc = appmodel.PrivilegeDesc;
                var names = Enum.GetNames(typeof(Privilege));
                foreach (var name in names)
                {
                    var value = (int)Enum.Parse(typeof(Privilege), name);
                    var result = GetPrivilegeByPrivilege(appmodel.PrivilegeDesc, value);
                    appModelPrivilege.SetPrivilege(result, name);
                }
            }
            else
            {
                appModelPrivilege.PrivilegeDesc = 0;
            }
            return appModelPrivilege;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 删除该模块下模块id为空的用户权限关联表
        /// </summary>
        private void DeltePrivilegeRole()
        {
            var context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context == null) return;
            var privilegeRole = context.Role_Privilege.Where(it => it.ModuleID == null).ToList();
            if (privilegeRole.Count == 0) return;
            foreach (var item in privilegeRole)
            {
                _privilegeRole.Remove(item);
            }
            _privilegeRole.UnitOfWork.Commit();
        }

        /// <summary>
        /// 获取当前是否具有某个权限
        /// </summary>
        /// <param name="currentPrivilege"></param>
        /// <param name="privilegeType"></param>
        /// <returns></returns>
        private static bool? GetPrivilegeByPrivilege(int? currentPrivilege, int privilegeType)
        {
            if (currentPrivilege == null) return false;
            var tempPrivilege = currentPrivilege & privilegeType;
            return tempPrivilege == privilegeType;
        }

        #endregion
    }
}
