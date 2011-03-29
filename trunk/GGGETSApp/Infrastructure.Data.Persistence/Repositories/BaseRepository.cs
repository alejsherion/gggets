using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Domain.Core;
using ETS.GGGETSApp.Domain.Core.Entities;
using ETS.GGGETSApp.Infrastructure.CrossCutting.Logging;
using ETS.GGGETSApp.Infrastructure.Data.Core;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.UnitOfWork;

namespace ETS.GGGETSApp.Infrastructure.Data.Persistence.Repositories
{
    public class BaseRepository<TEntity> : Repository<TEntity> where TEntity : class, IObjectWithChangeTracker
    {
        private readonly SysUserRepository _sysUserRepository;//角色和用户关联关系表操作
        private const string KEY = "UserID";
        public BaseRepository(IGGGETSAppUnitOfWork unitOfWork, ITraceManager traceManager)
            : base(unitOfWork, traceManager)
        {
            _sysUserRepository = new SysUserRepository(unitOfWork, traceManager);
        }

        public System.Linq.Expressions.Expression<Func<TEntity, bool>> GetCustomExpression<TEntity>(string fieldName, CompareOperate operate)
        {
            ///System.Web.SessionState.HttpSessionState sessionObj = HttpContext.Current.Session;//获取SESSION作用域
            string userId = Convert.ToString(HttpContext.Current.Session[KEY]);
            if (!string.IsNullOrEmpty(userId))
            {
                //获取当前用户
                SysUser user = _sysUserRepository.GetUserById(new Guid(userId));
                if (user == null) throw new ArgumentException("获取用户信息失败!");

                ModulePrivilege privilege = _sysUserRepository.GetPrivilegeByUserid(user.UID);

                bool? query = privilege[Privilege.查询.ToString()];
                if (query == null) throw new ArgumentException("获取用户信息失败!");
                if (query == false)//无全部查询
                {
                    var result = privilege[Privilege.部分查询.ToString()];
                    if (result == null) throw new ArgumentException("获取用户信息失败!");
                    if (result.Value)//具有部分查询
                    {
                        return base.GetExpression<TEntity>(fieldName, user.RegionCode, operate);
                    }
                    else//无部分查询
                    {
                        throw new ArgumentException("该用户无部分查询权限!");
                    }
                }
                else//查询所有
                {
                    return base.GetExpression<TEntity>(fieldName, user.RegionCode, CompareOperate.GreaterThan, true);
                }
            }
            else
            {
                throw new ArgumentException("获取用户信息失败!");
            }
        }
    }
}
