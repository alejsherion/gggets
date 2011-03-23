//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        HS品名管理BLL
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
    public class HSPropertyManagementService:IHSPropertyManagementService
    {
        /// <summary>
        /// IOC Injecting into
        /// </summary>
        IHSPropertyRepository _HSPropertyRepository;
        public HSPropertyManagementService(IHSPropertyRepository HSPropertyRepository)
        {
            _HSPropertyRepository = HSPropertyRepository;
        }

        /// <summary>
        /// 获取所有品名信息
        /// </summary>
        /// <returns></returns>
        public IList<HSProperty> GetAll()
        {
            return _HSPropertyRepository.GetAll();
        }

        /// <summary>
        /// 新增品名
        /// </summary>
        /// <param name="hsproperty">品名</param>
        public void AddHSProperty(HSProperty hsproperty)
        {
            if (hsproperty == null)
                throw new ArgumentNullException("HSProperty is null");
            IUnitOfWork unitOfWork = _HSPropertyRepository.UnitOfWork;
            _HSPropertyRepository.Add(hsproperty);
            //complete changes in this unit of work
            unitOfWork.Commit();
        }

        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="propertyName">品名</param>
        /// <returns></returns>
        public IList<HSProperty> GetHSPropertiesByCondition(string propertyName)
        {
            return _HSPropertyRepository.GetHSPropertiesByCondition(propertyName);
        }

        /// <summary>
        /// 修改品名
        /// </summary>
        /// <param name="hsproperty">品名</param>
        public void ModifyProperty(HSProperty hsproperty)
        {
            if (hsproperty == null)
                throw new ArgumentNullException("HSProperty is null");
            IUnitOfWork unitOfWork = _HSPropertyRepository.UnitOfWork;
            _HSPropertyRepository.Modify(hsproperty);
            //complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
        }

        /// <summary>
        /// 获取单个品名
        /// </summary>
        /// <param name="HSPID">序号</param>
        /// <returns></returns>
        public HSProperty FindHSPropertyByHSPID(string HSPID)
        {
            return _HSPropertyRepository.FindHSPropertyByHSPID(HSPID);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="hsproperty"></param>
        public void RemoveHSProperty(HSProperty hsproperty)
        {
            if (hsproperty == null)
                throw new ArgumentNullException("HSProperty is null");
            IUnitOfWork unitOfWork = _HSPropertyRepository.UnitOfWork;
            _HSPropertyRepository.Remove(hsproperty);
            unitOfWork.Commit();
        }

        /// <summary>
        /// 判断品名是否重复
        /// </summary>
        /// <param name="propertyName">品名</param>
        /// <returns></returns>
        public bool JudgeHSPropertyIsExist(string propertyName)
        {
            bool judge = true;
            IList<HSProperty> properties = GetAll();//获取所有信息
            if (properties.Count != 0)
            {
                foreach (HSProperty property in properties)
                {
                    if (property.PropertyName.Equals(propertyName))
                    {
                        judge = false;
                    }
                }
            }
            return judge;
        }
    }
}
