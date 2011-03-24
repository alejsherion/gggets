//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        运单用户IDAL
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
    public interface ISysUserRepository : IRepository<SysUser>
    {
        /// <summary>
        /// 获取当前用户的权限Id集合
        /// </summary>
        /// <param name="userId">当前用户编号</param>
        /// <returns>key是他的ModuleID value是他的权限描述</returns>
        Dictionary<Guid, int> GetPrivilegeIdsByUers(Guid userId);

        /// <summary>
        /// 根据用户名密码是否能够登录
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="password">密码</param>
        /// <returns>返回账号</returns>
        Guid? IsLgoin(string loginName, string password);

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
        IList<SysUser> QueryByCondtion(string email, string tel, string loginName
                                    , DateTime? startCreateTime, DateTime? endCreateTime
                                    , Status status = Status.全部);

        /// <summary>
        /// 获取当前用户的角色id集合
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        Guid[] GetRoleIdsByUers(Guid userid);

        /// <summary>
        /// 获取当前用户的角色集合
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        IList<Role> GetRolesByUers(Guid userid);

        /// <summary>
        /// 读取单个用户信息
        /// </summary>
        /// <param name="id"></param>
        SysUser GetUserById(Guid id);


    }
}
