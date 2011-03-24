using System;
using System.Collections.Generic;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace Application.GGETS
{
    public class SysUserManagementService : ISysUserManagementService
    {
        #region 构造函数以及字段
        /// <summary>
        /// 系统用户的接口
        /// </summary>
        private readonly ISysUserRepository _sysUserRepository;

        public SysUserManagementService(ISysUserRepository sysUserRepository)
        {
            _sysUserRepository = sysUserRepository;
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
            return _sysUserRepository.GetPrivilegeIdsByUers(userId);
        }

        /// <summary>
        /// 根据用户名密码是否能够登录
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="password">密码</param>
        /// <returns>返回账号</returns>
        public Guid? IsLgoin(string loginName, string password)
        {
            return _sysUserRepository.IsLgoin(loginName, password);
        }

        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="tel">电话</param>
        /// <param name="loginName">登录名</param>
        /// <param name="startCreateTime">开始时间</param>
        /// <param name="endCreateTime">结束时间</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public IList<SysUser> QueryByCondtion(string email, string tel, string loginName
                                               , DateTime? startCreateTime, DateTime? endCreateTime
                                               , Status status = Status.全部)
        {
            return _sysUserRepository.QueryByCondtion(email, tel, loginName, startCreateTime
                                                      , endCreateTime,status);
        }

        /// <summary>
        /// 获取当前用户的角色id集合
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public Guid[] GetRoleIdsByUers(Guid userid)
        {
            return _sysUserRepository.GetRoleIdsByUers(userid);
        }

        /// <summary>
        /// 获取当前用户的角色集合
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public IList<Role> GetRolesByUers(Guid userid)
        {
            return _sysUserRepository.GetRolesByUers(userid);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">系统用户编号</param>
        public void Remove(Guid id)
        {
            var user = GetUserById(id);
            if (user!=null)
                _sysUserRepository.Remove(user);
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="item">系统用户</param>
        public void Modify(SysUser item)
        {
            _sysUserRepository.Modify(item);
        }

        /// <summary>
        /// 读取单个用户信息
        /// </summary>
        /// <param name="id"></param>
        public SysUser GetUserById(Guid id)
        {
            return _sysUserRepository.GetUserById(id);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="item">系统用户</param>
       public void Add(SysUser item)
        {
            _sysUserRepository.Add(item);
            _sysUserRepository.UnitOfWork.Commit();
        }
        #endregion
    }
}
