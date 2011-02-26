//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        用户BLL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.24
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Core;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Domain.Core.Specification;

namespace Application.GGETS
{
    public class UserManagementService:IUserManagementService
    {
        /// <summary>
        /// IOC Injecting into
        /// </summary>
        IDepartmentRepository _departmentRepository;

        private IUserRepository _userRepository;

        public UserManagementService(IDepartmentRepository departmentRepository, IUserRepository userRepository)
        {
            _departmentRepository = departmentRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 通过用户账号查询用户信息
        /// </summary>
        /// <param name="loginName">用户账号</param>
        /// <returns></returns>
        public User FindUserByLoginName(string loginName)
        {
            return _departmentRepository.FindUserByLoginName(loginName);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user">用户</param>
        public void AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("User is null");
            IUnitOfWork unitOfWork = _userRepository.UnitOfWork;
            _userRepository.Add(user);
            //complete changes in this unit of work
            unitOfWork.Commit();
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user"></param>
        public void ModifyUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
