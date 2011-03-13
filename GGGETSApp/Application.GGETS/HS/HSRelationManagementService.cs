//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        HS分配BLL
// 作成者				ZhiWei.Shen
// 改版日				2011.03.13
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
    public class HSRelationManagementService:IHSRelationManagementService
    {
        /// <summary>
        /// IOC Injecting into
        /// </summary>
        IHSRelationRepository _HSRelationRepository;
        public HSRelationManagementService(IHSRelationRepository HSRelationRepository)
        {
            _HSRelationRepository = HSRelationRepository;
        }

        public IList<HSRelation> FindHSRelationsByHSID(string HSID)
        {
            return _HSRelationRepository.FindHSRelationsByHSID(HSID);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="relation">关系表</param>
        public void ModifyHSRelation(HSRelation relation)
        {
            if (relation == null)
                throw new ArgumentNullException("HSRelation is null");
            IUnitOfWork unitOfWork = _HSRelationRepository.UnitOfWork;
            _HSRelationRepository.Modify(relation);
            //complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="relation"></param>
        public void AddHSRelation(HSRelation relation)
        {
            if (relation == null)
                throw new ArgumentNullException("HSRelation is null");
            IUnitOfWork unitOfWork = _HSRelationRepository.UnitOfWork;
            _HSRelationRepository.Add(relation);
            //complete changes in this unit of work
            try
            {
                unitOfWork.Commit();
            }
            catch(Exception ex){}
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="relation"></param>
        public void RemoveHSRelation(HSRelation relation)
        {
            if (relation == null)
                throw new ArgumentNullException("HSRelation is null");
            IUnitOfWork unitOfWork = _HSRelationRepository.UnitOfWork;
            _HSRelationRepository.Remove(relation);
            try
            {
                unitOfWork.Commit();
            }
            catch (Exception ex) { }
        }
    }
}
