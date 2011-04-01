using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace Application.GGETS
{
    public class OrganizationManagementService : IOrganizationManagementService
    {
        #region 构造函数以及字段
        /// <summary>
        /// 系统用户的接口
        /// </summary>
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationManagementService(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }
        #endregion

        /// <summary>
        /// 根据组织ID获取其下面的组织
        /// </summary>
        /// <param name="did">组织ID</param>
        /// <returns></returns>
        public IList<OrganizationChart> GeOrganizationByDid(Guid did)
        {
            return _organizationRepository.GeOrganizationByDid(did);
        }

        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="organizationName">组织名</param>
        /// <param name="startCreateTime">结束时间</param>
        /// <param name="endCreateTime">开始时间</param>
        /// <param name="organizationCode">编号</param>
        /// <param name="parentId">父级架构ID</param>
        /// <returns></returns>
        public IList<OrganizationChart> QueryByCondtion(string organizationName, DateTime? startCreateTime, DateTime? endCreateTime, string organizationCode, Guid? parentId=null)
        {
            return _organizationRepository.QueryByCondtion(organizationName, startCreateTime, endCreateTime, organizationCode,parentId);
        }

        /// <summary>
        /// 获取单个组织
        /// </summary>
        /// <param name="did">组织ID</param>
        /// <returns></returns>
        public OrganizationChart GetOrganizationByDid(Guid did)
        {
            return _organizationRepository.GetOrganizationByDid(did);
        }

        /// <summary>
        /// 获取所有组织结构
        /// </summary>
        /// <returns></returns>
        public IList<OrganizationChart> GeAllOrganization()
        {
            return _organizationRepository.GeAllOrganization();
        }

        /// <summary>
        /// 删除组织
        /// </summary>
        /// <param name="did"></param>
        public void Remove(Guid did)
        {
            var organization = _organizationRepository.GetOrganizationByDid(did);
            if(organization!=null)
            {
                _organizationRepository.Remove(organization);
                _organizationRepository.UnitOfWork.Commit();
            }
        }

        /// <summary>
        /// 添加组织
        /// </summary>
        /// <param name="organization"></param>
        public void Add(OrganizationChart organization)
        {
            if (organization == null) throw new ArgumentNullException();
            _organizationRepository.Add(organization);
        }

        /// <summary>
        /// 修改组织
        /// </summary>
        /// <param name="organization"></param>
        public void Modify(OrganizationChart organization)
        {
            if (organization == null) throw new ArgumentNullException();
            _organizationRepository.Modify(organization);
        }

        /// <summary>
        /// 获取该组织下的所有用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<SysUser> GetSysUserByDid(Guid id)
        {
            return _organizationRepository.GetSysUserByDid(id);
        }

        /// <summary>
        /// 根据组织ID获取其上面的组织
        /// </summary>
        /// <param name="did">组织ID</param>
        /// <returns></returns>
        public IList<OrganizationChart> GetParentOrganizationByDid(Guid did)
        {
            return _organizationRepository.GetParentOrganizationByDid(did);
        }

        /// <summary>
        /// 删除所有
        /// </summary>
        /// <param name="id"></param>
        public void RemoveAll(Guid id)
        {
             _organizationRepository.RemoveAll(id);
        }
    }
}
