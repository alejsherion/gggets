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
using System.Linq;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Infrastructure.CrossCutting.Logging;
using ETS.GGGETSApp.Infrastructure.Data.Core;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.UnitOfWork;

namespace ETS.GGGETSApp.Infrastructure.Data.Persistence.Repositories
{
    ///<summary>
    ///</summary>
    public class OrganizationRepository : Repository<OrganizationChart>, IOrganizationRepository
    {
       #region 构造函数以及字段
 
        ///<summary>
        ///</summary>
        ///<param name="unitOfWork"></param>
        ///<param name="traceManager"></param>
        public OrganizationRepository(IGGGETSAppUnitOfWork unitOfWork, ITraceManager traceManager)
            : base(unitOfWork, traceManager)
        {
        }
        #endregion

       #region 公共方法
        /// <summary>
        /// 根据组织ID获取其下面的组织
        /// </summary>
        /// <param name="did">组织ID</param>
        /// <returns></returns>
        public IList<OrganizationChart> GeOrganizationByDid(Guid did)
        {
            var context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
            {
                var organizationCharts = context.OrganizationChart.Where(it => it.ParentDID == did).ToList();
                return organizationCharts;
            }
            return null;
        }

        /// <summary>
        /// 根据组织ID获取其上面的组织
        /// </summary>
        /// <param name="did">父级组织ID</param>
        /// <returns></returns>
        public IList<OrganizationChart> GetParentOrganizationByDid(Guid did)
        {
            var context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context == null)
            {
                return null;
            }
            var tempArray = new List<Guid>();
            GetOrganizationDid(did, tempArray);
            var array = context.OrganizationChart.Where(it => (!tempArray.Contains(it.DID))).ToList();
            return array;
        }

        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="organizationName">组织名</param>
        /// <param name="startCreateTime">结束时间</param>
        /// <param name="endCreateTime">开始时间</param>
        /// <param name="organizationCode">组织架构编号</param>
        /// <param name="parentId">父级架构ID</param>
        /// <returns></returns>
        public IList<OrganizationChart> QueryByCondtion(string organizationName, DateTime? startCreateTime, DateTime? endCreateTime,string organizationCode, Guid? parentId=null)
        {
            try
            {
               var context = UnitOfWork as IGGGETSAppUnitOfWork;
                if (context != null)
                {
                    var tempOrganization = context.OrganizationChart.Select(it => it);
                    if (parentId != null) tempOrganization = tempOrganization.Where(a => a.ParentDID == parentId.Value);
                    if (!string.IsNullOrEmpty(organizationName)) tempOrganization = tempOrganization.Where(a => a.OrganizationName.Contains(organizationName.ToUpper()));
                    if (!string.IsNullOrEmpty(organizationCode)) tempOrganization = tempOrganization.Where(a => a.OrganizationCode == organizationCode);
                    if (startCreateTime != null) tempOrganization = tempOrganization.Where(a => a.CreateTime >= startCreateTime.Value);
                    if (endCreateTime != null) tempOrganization = tempOrganization.Where(a => a.CreateTime <= endCreateTime.Value);
                    return tempOrganization.ToList();
                }
                 return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取单个组织
        /// </summary>
        /// <param name="did">组织ID</param>
        /// <returns></returns>
        public OrganizationChart GetOrganizationByDid(Guid did)
        {
            var context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
            {
                var organizationChart = context.OrganizationChart.Where(it => it.DID == did).SingleOrDefault();
                return organizationChart;
            }
            return null;
        }

        /// <summary>
        /// 获取所有组织结构
        /// </summary>
        /// <returns></returns>
        public IList<OrganizationChart> GeAllOrganization()
        {
            var context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
            {
                var organizationCharts = context.OrganizationChart.ToList();
                return organizationCharts;
            }
            return null;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="organization"></param>
        public override void Add(OrganizationChart organization)
        {
            if(organization!=null)
            {
                var result = IsExists(organization.OrganizationCode);
                if (result != null && !result.Value)
                {
                    base.Add(organization);
                    UnitOfWork.Commit();
                }
                else
                {
                    throw new Exception("存在相同的组织编号!");
                }
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="organization"></param>
        public override void Modify(OrganizationChart organization)
        {
            if (organization != null)
            {
                var result = IsExists(organization.OrganizationCode, organization.DID);
                if (result != null && !result.Value)
                {
                    base.Modify(organization);
                    UnitOfWork.CommitAndRefreshChanges();
                }
                else
                {
                    throw new Exception("存在相同的组织编号!");
                }
            }
        }

        /// <summary>
        /// 获取该组织下的所有用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<SysUser> GetSysUserByDid(Guid id)
        {
            var array = new List<Guid>();
            GetOrganizationDid(id, array);
            var context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context == null) return null;
            var tempArray = context.SysUser.Where(it => it.DepartmentID != null);
            if (tempArray.Count()==0) return null;
            var sysUserArray = tempArray.Where(
                BuildOrExpression<SysUser, Guid>(p => p.DepartmentID.Value, array)
                                   ).ToList();
            return sysUserArray;
        }

        /// <summary>
        /// 删除所有
        /// </summary>
        /// <param name="id"></param>
        public void RemoveAll(Guid id)
        {
            var array = GeOrganizationByDid(id);
            if (array.Count == 0)
            {
                RemoveSingle(id);
            }
            else if (array.Count>0)
            {
                foreach(var itemid in array)
                {
                    RemoveAll(itemid.DID);
                }
                RemoveSingle(id);
            }
        }

        #endregion

       #region 私有方法
        /// <summary>
        /// 是否存在组织编号
        /// </summary>
        /// <param name="code"></param>
        /// <param name="did"></param>
        /// <returns></returns>
        private bool? IsExists(string code,Guid? did=null)
        {
            var context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (context != null)
            {
                try
                {
                    var organizationChart = context.OrganizationChart.Where(it => it.OrganizationCode == code).ToList();
                    if (did!=null)
                    {
                        organizationChart = organizationChart.Where(it => it.DID != did).ToList();
                    }
                    return organizationChart.Count != 0;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取该组织下的所有组织（包括自己）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        private void  GetOrganizationDid(Guid id,List<Guid> array)
        {
            if (array == null) array = new List<Guid>(); 
            var context = UnitOfWork as IGGGETSAppUnitOfWork;
            if (!array.Contains(id)) array.Add(id);
            if (context == null)
            {
                return;
            }
            var tempArray = context.OrganizationChart.Where(it => it.ParentDID == id).ToList();
            if (tempArray.Count == 0) return;
            foreach (var item in tempArray)
            {
                GetOrganizationDid(item.DID, array);
            }
        }

        /// <summary>
        /// 删除单个组织架构
        /// </summary>
        /// <param name="id"></param>
        private void RemoveSingle(Guid id)
        {
            var organization = GetOrganizationByDid(id);
            if (organization != null)
            {
                base.Remove(organization);
                UnitOfWork.Commit();
            }
        }
        #endregion

    }
}
