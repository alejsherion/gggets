//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        权限DAL
// 作成者				lh
// 改版日				2011.02.12
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using ETS.GGGETSApp.Infrastructure.Data.Core;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.UnitOfWork;
using ETS.GGGETSApp.Infrastructure.CrossCutting.Logging;
using ETS.GGGETSApp.Domain.Application.Entities;
using Domain.GGGETS;
using System.Linq;
using ETS.GGGETSApp.Infrastructure.Data.Core.Extensions;

namespace ETS.GGGETSApp.Infrastructure.Data.Persistence.Repositories
{
    ///<summary>
    /// 权限类
    ///</summary>
    public class PrivilegeRepository : Repository<Privilege>, IPrivilegeRepository
    {
        ///<summary>
        ///</summary>
        ///<param name="unitOfWork"></param>
        ///<param name="traceManager"></param>
        public PrivilegeRepository(IGGGETSAppUnitOfWork unitOfWork, ITraceManager traceManager)
            : base(unitOfWork, traceManager)
        {

        }

        /// <summary>
        /// 获取当前页面的权限
        /// </summary>
        /// <param name="privilegeIds">权限Id集合</param>
        /// <returns></returns>
        public ModulePrivilege GetCurentModulePrivilege(Guid[] privilegeIds)
        {
            if (privilegeIds == null || privilegeIds.Count() == 0) return null;
            var context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
            {
                var url = HttpContext.Current.Request.Url.ToString();
                var pos = url.LastIndexOf("/");
                var fileName = "";
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
                if (string.IsNullOrWhiteSpace(fileName)) return null;
                var privileges=context.Privilege.Where(BuildOrExpression<Privilege, Guid>(p => p.PrivilegeID, privilegeIds)).Where(p => p.UrlAdress.Contains(fileName));
                if (privileges.Count() == 0) return null;
                var currentPrivilege = privileges.First();
                var tempModulePrivilege = new ModulePrivilege
                                              {
                                                  ModuleName = currentPrivilege.AppModule.Description,
                                                  Url = currentPrivilege.UrlAdress,
                                                  DisplayName = currentPrivilege.DisplayName,
                                                  Privilege = currentPrivilege.PrivilegeDesc
                                              };
                if (String.IsNullOrWhiteSpace(tempModulePrivilege.Privilege)) tempModulePrivilege.Privilege = "000000";
                var privilegeValue = Convert.ToInt32(currentPrivilege.PrivilegeDesc, 2);
                tempModulePrivilege.QueryPrivilege = (privilegeValue &(int)PrivilegeDesc.Query) == (int)PrivilegeDesc.Query?true:false;
                tempModulePrivilege.AddPrivilege = (privilegeValue & (int)PrivilegeDesc.Add) == (int)PrivilegeDesc.Add ? true : false;
                tempModulePrivilege.UpdatePrivilege = (privilegeValue & (int)PrivilegeDesc.Update) == (int)PrivilegeDesc.Update ? true : false;
                tempModulePrivilege.DeletePrivilege = (privilegeValue & (int)PrivilegeDesc.Del) == (int)PrivilegeDesc.Del ? true : false;
                return tempModulePrivilege;
            }
            return null;
        }
        
        /// <summary>
        /// 根据权限id查到权限信息集合
        /// </summary>
        /// <param name="privilegeIds">权限ID集合</param>
        /// <returns></returns>
        public IList<Privilege> GePrivilegeByIds(Guid[] privilegeIds)
        {
            if (privilegeIds == null || privilegeIds.Count() == 0) return null;
            var context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
            {
                var privileges = context.Privilege.Include(ba => ba.AppModule)
                                                  .Where(BuildOrExpression<Privilege, Guid>(p => p.PrivilegeID
                                                                                            , privilegeIds)).ToList();
                return privileges;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="valueSelector"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Expression<Func<TElement, bool>> BuildOrExpression<TElement, TValue>(
       Expression<Func<TElement, TValue>> valueSelector,
       IEnumerable<TValue> values)
        {
            if (null == valueSelector)
                throw new ArgumentNullException("valueSelector");

            if (null == values)
                throw new ArgumentNullException("values");

            ParameterExpression p = valueSelector.Parameters.Single();

            if (!values.Any())
                return e => false;

            var equals = values.Select(value =>
                (Expression)Expression.Equal(
                     valueSelector.Body,
                     Expression.Constant(
                         value,
                         typeof(TValue)
                     )
                )
            );

            var body = equals.Aggregate(
                     (accumulate, equal) => Expression.Or(accumulate, equal)
             );

            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }

    }
}
