//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        角色用户DAL
// 作成者				hong.li
// 改版日				2011.02.12
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Infrastructure.CrossCutting.Logging;
using ETS.GGGETSApp.Infrastructure.Data.Core;
using ETS.GGGETSApp.Infrastructure.Data.Core.Extensions;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.UnitOfWork;

namespace ETS.GGGETSApp.Infrastructure.Data.Persistence.Repositories
{
   ///<summary>
   /// 角色DAL
   ///</summary>
    public class RoleRepository : Repository<Role>, IRoleRepository
   {
       #region 构造函数以及字段

       private readonly Repository<SysUser_Role> _sysUserRole;//角色和用户关联关系表操作
       private readonly Repository<Role_Privilege> _privilegeRole;//角色和权限关联关系表操作
       ///<summary>
       ///</summary>
       ///<param name="unitOfWork"></param>
       ///<param name="traceManager"></param>
       public RoleRepository(IGGGETSAppUnitOfWork unitOfWork, ITraceManager traceManager)
           : base(unitOfWork, traceManager)
       {
           _sysUserRole = new Repository<SysUser_Role>(unitOfWork, traceManager);
           _privilegeRole = new Repository<Role_Privilege>(unitOfWork, traceManager);
       }
       #endregion
       #region 公共方法

       /// <summary>
       /// 根据角色ID获取用户
       /// </summary>
       /// <param name="roleId">角色ID</param>
       /// <returns></returns>
       public IList<SysUser> GetUserByRoleId(Guid roleId)
       {
           var context = UnitOfWork as IGGGETSAppUnitOfWork;
           if (context == null) return null;
           try
           {
               var userIds = context.SysUser_Role.Where(it => it.RoleID == roleId).Where(it => it.UID != null).Select(it => it.UID.Value).ToArray();
               if (userIds == null || userIds.Count() == 0) return null;
               var ursers = context.SysUser.Where(
                         BuildOrExpression<SysUser, Guid>(p => p.UID, userIds)
                         ).ToList();
               return ursers;
           }
           catch
           {
               return null;
           }
       }

       /// <summary>
       /// 多条件查询
       /// </summary>
       /// <param name="name">名字</param>
       /// <param name="description">描述</param>
       /// <param name="startCreateTime">结束时间</param>
       /// <param name="endCreateTime">开始时间</param>
       /// <param name="status">状态</param>
       /// <returns></returns>
       public IList<Role> QueryByCondtion(string name, string description, DateTime? startCreateTime, DateTime? endCreateTime, RoleStatus status = RoleStatus.正常)
       {
           try
           {
               var context = UnitOfWork as IGGGETSAppUnitOfWork;
               if (context != null)
               {
                   var tempRole = context.Role.Include(it => it.Role_Privilege)
                                               .Include(it => it.SysUser_Role).Select(it => it);
                   if (!string.IsNullOrEmpty(name)) tempRole = tempRole.Where(a => a.Name.Contains(name));
                   if (!string.IsNullOrEmpty(description))
                       tempRole = tempRole.Where(a => a.Description.Contains(description));
                   if (startCreateTime != null) tempRole = tempRole.Where(a => a.CreateTime >= startCreateTime);
                   if (endCreateTime != null) tempRole = tempRole.Where(a => a.CreateTime <= endCreateTime);
                   var tempValue = Convert.ToInt32(status);
                   tempRole = tempRole.Where(a => a.Status ==tempValue);
                   return tempRole.ToList();
               }
               return null;
           }
           catch
           {
               return null;
           }
       }

       /// <summary>
       /// 根据角色读取权限
       /// </summary>
       /// <param name="roleId">角色Id</param>
       /// <returns></returns>
       public IList<AppModule> GetAppModuleByRoleId(Guid roleId)
       {
           var context = UnitOfWork as IGGGETSAppUnitOfWork;
           if (context == null) return null;
           try
           {
               var moduleIds = context.Role_Privilege.Where(it => it.RoleID == roleId).Where(it => it.ModuleID != null).Select(it => it.ModuleID.Value).ToArray();
               if (moduleIds == null || moduleIds.Count() == 0) return null;
               var ursers = context.AppModule.Where(
                         BuildOrExpression<AppModule, Guid>(p => p.ModuleID, moduleIds)
                         ).ToList();
               return ursers;
           }
           catch
           {
               return null;
           }
       }

       /// <summary>
       /// 根据角色ID读取角色
       /// </summary>
       /// <param name="roleId">角色Id</param>
       /// <returns></returns>
       public Role GetRoleByRoleId(Guid roleId)
       {
           try
           {
               var context = UnitOfWork as IGGGETSAppUnitOfWork;
               if (context != null)
               {
                   var tempRole = context.Role.Include(it=>it.Role_Privilege).Include(it=>it.SysUser_Role)
                                   .Where(it => it.RoleID == roleId).SingleOrDefault();
                   return tempRole;

               }
               return null;
           }
           catch
           {
               return null;
           }
       }

       /// <summary>
       /// 删除角色
       /// </summary>
       /// <param name="item">系统角色</param>
       public override void Remove(Role item)
       {
           if (item == null) throw new ArgumentNullException();
           if (item.SysUser_Role != null && item.SysUser_Role.Count != 0)
           {
               for (var i = 0; i < item.SysUser_Role.Count; i++)
               {
                   var sysitem = item.SysUser_Role[i];
                   _sysUserRole.Remove(sysitem);
               }
               item.SysUser_Role.Clear();
           }
           if (item.Role_Privilege != null && item.Role_Privilege.Count != 0)
           {
               for (var i = 0; i < item.Role_Privilege.Count; i++)
               {
                   var rolePri = item.Role_Privilege[i];
                   _privilegeRole.Remove(rolePri);
               }
               item.Role_Privilege.Clear();
           }
           base.Remove(item);
           _sysUserRole.UnitOfWork.Commit();
           _privilegeRole.UnitOfWork.Commit();
           UnitOfWork.Commit();
       }

       /// <summary>
       /// 修改用户
       /// </summary>
       /// <param name="item"></param>
       public override void Modify(Role item)
       {
           if (item == null) throw new ArgumentNullException();
           if (item.Role_Privilege == null || item.Role_Privilege.Count == 0)
           {
               var context = UnitOfWork as IGGGETSAppUnitOfWork;
               if (context != null)
               {
                   var tempRolePrivilege = context.Role_Privilege.Where(it => it.RoleID == item.RoleID).ToList();
                   if (tempRolePrivilege.Count != 0)
                   {
                       foreach (var appModuleItem in tempRolePrivilege)
                       {
                           _privilegeRole.Remove(appModuleItem);
                       }
                       _privilegeRole.UnitOfWork.Commit();
                   }

               }
           }
           if (item.SysUser_Role == null || item.SysUser_Role.Count == 0)
           {
               var context = UnitOfWork as IGGGETSAppUnitOfWork;
               if (context != null)
               {
                   var tempSysUser = context.SysUser_Role.Where(it => it.RoleID == item.RoleID).ToList();
                   if (tempSysUser.Count != 0)
                   {
                       foreach (var sysUser in tempSysUser)
                       {
                           _sysUserRole.Remove(sysUser);
                       }
                       _sysUserRole.UnitOfWork.Commit();
                   }

               }
           }
           base.Modify(item);
           UnitOfWork.CommitAndRefreshChanges();
           DelteSysUserRole();
           DeltePrivilegeRole();
       }
       #endregion

       #region 私有方法
       /// <summary>
       /// 删除该角色下角色id为空的用户角色关联表
       /// </summary>
       private void DelteSysUserRole()
       {
           var context = UnitOfWork as IGGGETSAppUnitOfWork;
           if (context == null) return;
           var sysUserRole = context.SysUser_Role.Where(it => it.RoleID == null).ToList();
           if (sysUserRole.Count == 0) return;
           foreach (var item in sysUserRole)
           {
               _sysUserRole.Remove(item);
           }
           _sysUserRole.UnitOfWork.Commit();
       }

       /// <summary>
       /// 删除该角色下角色id为空的用户权限关联表
       /// </summary>
       private void DeltePrivilegeRole()
       {
           var context = UnitOfWork as IGGGETSAppUnitOfWork;
           if (context == null) return;
           var privilegeRole = context.Role_Privilege.Where(it => it.RoleID == null).ToList();
           if (privilegeRole.Count == 0) return;
           foreach (var item in privilegeRole)
           {
               _privilegeRole.Remove(item);
           }
           _privilegeRole.UnitOfWork.Commit();
       }

       /// <summary>
       /// 删除该角色下角色id的用户权限关联表
       /// </summary>
       private void DeltePrivilegeRole(Guid id)
       {
           var context = UnitOfWork as IGGGETSAppUnitOfWork;
           if (context == null) return;
           var privilegeRole = context.Role_Privilege.Where(it => it.RoleID == id).ToList();
           if (privilegeRole.Count == 0) return;
           foreach (var item in privilegeRole)
           {
               _privilegeRole.Remove(item);
           }
           _privilegeRole.UnitOfWork.Commit();
       }
       #endregion
   }
}
