//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        用户DAL
// 作成者				hong.li
// 改版日				2011.02.12
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Web;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Infrastructure.CrossCutting.Logging;
using ETS.GGGETSApp.Infrastructure.Data.Core;
using ETS.GGGETSApp.Infrastructure.Data.Core.Extensions;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.UnitOfWork;
using System.Linq;

namespace ETS.GGGETSApp.Infrastructure.Data.Persistence.Repositories
{
    ///<summary>
    /// 系统用户DAL
    ///</summary>
    public class SysUserRepository : Repository<SysUser>, ISysUserRepository
    {
        #region 构造函数以及字段

        private readonly Repository<SysUser_Role> _sysUserRole;//角色和用户关联关系表操作

        ///<summary>
        ///</summary>
        ///<param name="unitOfWork"></param>
        ///<param name="traceManager"></param>
        public SysUserRepository(IGGGETSAppUnitOfWork unitOfWork, ITraceManager traceManager)
            : base(unitOfWork, traceManager)
        {
            _sysUserRole = new Repository<SysUser_Role>(unitOfWork, traceManager);
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取当前用户的权限Id集合
        /// </summary>
        /// <param name="userId">当前用户编号</param>
        /// <returns>key是他的ModuleID value是他的权限描述</returns>
        public Dictionary<Guid, int> GetPrivilegeIdsByUers(Guid userId)
        {
            var context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
            {
                var currentSysUser = context.SysUser.Where(it => it.UID == userId).SingleOrDefault();
                if(currentSysUser!=null)
                {
                    var roleIds = GetRoleIdsByUers(userId);
                    if (roleIds == null || roleIds.Count() == 0) return null;
                    var tempPrivilegeArray = context.Role_Privilege.Where(it => it.RoleID != null);
                    if (tempPrivilegeArray.Count() == 0) return null;
                   var privilegeArray= tempPrivilegeArray.Where(
                        BuildOrExpression<Role_Privilege, Guid>(p => p.RoleID.Value, roleIds)
                        ).ToList();
                    var tempArray = new Dictionary<Guid, int>();
                    foreach(var item in privilegeArray)
                    {
                        if (item.ModuleID == null) continue;
                        if(!tempArray.ContainsKey(item.ModuleID.Value))
                        {
                            tempArray.Add(item.ModuleID.Value, item.PrivilegeDesc);
                        }
                        else
                        {
                            var privilegeDesc = Convert.ToInt32(tempArray[item.ModuleID.Value]);
                            privilegeDesc = privilegeDesc | item.PrivilegeDesc;
                            tempArray[item.ModuleID.Value] = privilegeDesc;
                        }
                    }
                    return tempArray;
                }
                return null;
            }
            return null;
        }

        /// <summary>
        /// 根据用户名密码是否能够登录
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="password">密码</param>
        /// <returns>返回账号</returns>
        public Guid? IsLgoin(string loginName, string password)
        {
            if (String.IsNullOrEmpty(loginName) || String.IsNullOrEmpty(password)) return SysUser.EmptyUid;
           
            try
            {
                var context = UnitOfWork as IGGGETSAppUnitOfWork;
                if (context != null)
                {
                    var sysUser = context.SysUser.Where(it => it.LoginName == loginName)
                        .Where(it => it.Password == password).SingleOrDefault();
                    if (sysUser == null) return SysUser.EmptyUid;
                    return sysUser.UID;
                }
                return SysUser.EmptyUid;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="tel">电话</param>
        /// <param name="loginName">登录名</param>
        /// <param name="endCreateTime">创建开始时间</param>
        /// /// <param name="startCreateTime">创建结束时间</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public IList<SysUser> QueryByCondtion(string email, string tel, string loginName
                                              , DateTime? startCreateTime, DateTime? endCreateTime 
                                              , Status status = Status.全部)
        {
            try
            {
                var context = UnitOfWork as IGGGETSAppUnitOfWork;
                if (context != null)
                {
                    var tempSysUser = context.SysUser.Include(it=>it.SysUser_Role).Select(it => it);
                    if (!string.IsNullOrEmpty(email)) tempSysUser = tempSysUser.Where(a => a.Email == email.ToUpper());
                    if (!string.IsNullOrEmpty(tel)) tempSysUser = tempSysUser.Where(a => a.Phone == tel.ToUpper());
                    if (!string.IsNullOrEmpty(loginName)) tempSysUser = tempSysUser.Where(a => a.LoginName.Contains(loginName.ToUpper()));
                    if (status != Status.全部)
                    {
                        var value = Convert.ToInt32(status);
                        tempSysUser = tempSysUser.Where(a => a.Status == value);
                    }
                    if (startCreateTime != null) tempSysUser = tempSysUser.Where(a => a.CreateTime >= startCreateTime.Value);
                    if (endCreateTime != null) tempSysUser = tempSysUser.Where(a => a.CreateTime <= endCreateTime.Value);
                    return tempSysUser.ToList();
                }
                 return null;
            }
            catch
            {
                return null;
            }
           
        }

        /// <summary>
        /// 获取当前用户的角色id集合
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public Guid[] GetRoleIdsByUers(Guid userid)
        {
            var context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
            {
                var sysUserRoles = context.SysUser_Role.Where(it => it.UID == userid).ToList();
                if (sysUserRoles.Count()==0) return null;
                var rolesIds = new Guid[sysUserRoles.Count()];
                var k = 0;
                foreach(var sysRoleItem in sysUserRoles)
                {
                    if (sysRoleItem.RoleID == null) continue;
                    rolesIds[k] = sysRoleItem.RoleID.Value;
                    k++;
                }
                return rolesIds;
            }
            return null;
        }

        /// <summary>
        /// 获取当前用户的角色集合
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public IList<Role> GetRolesByUers(Guid userid)
        {
            var context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
            {
                var tempSysUserRoleIds = context.SysUser_Role.Where(it => it.UID == userid).Where(it=>it.RoleID!=null);
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
        /// 删除用户
        /// </summary>
        /// <param name="item">系统用户</param>
        public override void Remove(SysUser item)
        {
            if (item == null) throw new ArgumentNullException();
            base.Remove(item);
            UnitOfWork.Commit();
            DelteSysUserRole();
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="item"></param>
        public override void Modify(SysUser item)
        {
            if (item == null) throw new ArgumentNullException();
            if (item.SysUser_Role == null || item.SysUser_Role.Count == 0)
            {
                var context = UnitOfWork as IGGGETSAppUnitOfWork;
                if (context != null)
                {
                    var tempSysUser = context.SysUser_Role.Where(it => it.UID == item.UID).ToList();
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
        }

        /// <summary>
        /// 读取单个用户信息
        /// </summary>
        /// <param name="id"></param>
        public SysUser GetUserById(Guid id)
        {
            try
            {
                var context = UnitOfWork as IGGGETSAppUnitOfWork;
                if (context != null)
                {
                    var tempSysUser = context.SysUser.Include(it => it.SysUser_Role)
                                      .Where(it => it.UID == id).Select(it => it).SingleOrDefault();
                    
                    return tempSysUser;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取权限集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<AppModule> GetAppModuleByUserid(Guid userId)
        {
            var array = GetPrivilegeIdsByUers(userId);
            if (array == null || array.Count == 0) return null;
            var appMduleArry = new List<AppModule>();
             var context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
            {
               foreach(var key in array.Keys)
               {
                   Guid tempKey = key;
                   var singleappModuel = context.AppModule.Where(it => it.ModuleID == tempKey).SingleOrDefault();
                   if (singleappModuel != null)
                   {
                       appMduleArry.Add(singleappModuel);
                       var parentId = singleappModuel.ParentId;
                       if(parentId!=null)
                       {
                            var reuslt = appMduleArry.Where(it => it.ModuleID
                                                        == parentId.Value).SingleOrDefault();
                            if(reuslt==null)
                            {
                                var tempAppModuel = context.AppModule.Where(it => it.ModuleID
                                                                            == parentId).SingleOrDefault();
                                if (tempAppModuel!=null)
                                appMduleArry.Add(tempAppModuel);
                            }
                       }
                      
                   }

               }
                return appMduleArry;
            }
            return null;
        }

        /// <summary>
        /// 获取当前页面权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ModulePrivilege GetPrivilegeByUserid(Guid userId)
        {
            var url = HttpContext.Current.Request.Url.ToString();
            var pos = url.LastIndexOf("/");
            string fileName;
            if (url.LastIndexOf("?") != -1)
            {
                var endPos = url.IndexOf("?");
                var length = endPos - pos;
                fileName = url.Substring(pos + 1, length);
            }
            else
            {
                fileName = url.Substring(pos + 1);
            }
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new Exception("获取当前页面地址失败");
            }
            var privilege = GetPrivilegeIdsByUers(userId);
            if(privilege==null)
            {
                throw new Exception("获取权限失败");
            }
            var tempAppModule = GetModelByName(fileName);
            if(tempAppModule==null)
            {
                throw new Exception("获取权限失败");
            }
            var modulePrivilege = new ModulePrivilege
                                      {
                                          Url = tempAppModule.URL,
                                          ModuleName = tempAppModule.Description
                                      };

            var modelId = tempAppModule.ModuleID;
            var result = privilege.ContainsKey(modelId);
            if (!result) return modulePrivilege;
            var privilegeValue = privilege[modelId];
            modulePrivilege.QueryPrivilege =
                (privilegeValue & (int)Privilege.查询) == (int)Privilege.查询 ? true : false;
            modulePrivilege.AddPrivilege =
                (privilegeValue & (int)Privilege.添加) == (int)Privilege.添加 ? true : false;
            modulePrivilege.UpdatePrivilege =
                (privilegeValue & (int)Privilege.修改) == (int)Privilege.修改 ? true : false;
            modulePrivilege.DeletePrivilege =
                (privilegeValue & (int)Privilege.删除) == (int)Privilege.删除 ? true : false;
            modulePrivilege.ExportPrivilege =
                (privilegeValue & (int)Privilege.导出) == (int)Privilege.导出 ? true : false;
            modulePrivilege.PrintPrivilege =
                (privilegeValue & (int)Privilege.打印) == (int)Privilege.打印 ? true : false;
            return modulePrivilege;
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 删除该用户下用户id为空的用户角色关联表
        /// </summary>
        private void DelteSysUserRole()
        {
            var context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context == null) return;
            var sysUserRole = context.SysUser_Role.Where(it => it.UID == null).ToList();
            if (sysUserRole.Count == 0) return;
            foreach (var item in sysUserRole)
            {
                _sysUserRole.Remove(item);
            }
            _sysUserRole.UnitOfWork.Commit();
        }

        /// <summary>
        /// 根据地址名获取模块Id
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private AppModule GetModelByName(string fileName)
        {
            if (String.IsNullOrEmpty(fileName)) return null;
             var context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
            {
                var tempValue = context.AppModule.Where(it => it.URL.Contains(fileName)).SingleOrDefault();
                return tempValue;
            }
            return null;
        }
        #endregion
    }
}
