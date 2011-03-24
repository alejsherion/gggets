//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        角色IBLL
// 作成者				hong.li
// 改版日				2011.02.28
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace Application.GGETS
{
    public class RoleManagementService : IRoleManagementService
    {
        #region 构造函数以及字段
        /// <summary>
        /// 系统用户的接口
        /// </summary>
        private readonly IRoleRepository _roleRepository;

        public RoleManagementService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
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
            return _roleRepository.GetUserByRoleId(roleId);
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
            return _roleRepository.QueryByCondtion(name, description, startCreateTime, endCreateTime, status);
        }

        /// <summary>
        /// 根据角色读取权限
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public IList<AppModule> GetAppModuleByRoleId(Guid roleId)
        {
            return _roleRepository.GetAppModuleByRoleId(roleId);
        }

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        public IList<Role> GetAllRole()
        {
            var roleArry= _roleRepository.GetAll();
            if (roleArry == null || roleArry.Count()==0) return null;
            var value = Convert.ToInt32(RoleStatus.正常);
            var allRole = roleArry.Where(it => it.Status == value).ToList();
            return allRole;
        }

        /// <summary>
        /// 根据角色ID读取角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public Role GetRoleByRoleId(Guid roleId)
        {
            return _roleRepository.GetRoleByRoleId(roleId);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">系统角色编号</param>
        public void Remove(Guid id)
        {
            var role = GetRoleByRoleId(id);
            if (role!=null)
                _roleRepository.Remove(role);
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="item"></param>
        public void Modify(Role item)
        {
            _roleRepository.Modify(item);
        }

        /// <summary>
        /// 添加角色 
        /// </summary>
        /// <param name="item"></param>
        public void Add(Role item)
        {
            _roleRepository.Add(item);
            _roleRepository.UnitOfWork.Commit();
        }

        #endregion
    }
}
