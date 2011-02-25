//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        地区三字码BLL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.23
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
    public class RegionCodeManagementService:IRegionCodeManagementService
    {
        /// <summary>
        /// IOC Injecting into
        /// </summary>
        IRegionCodeRepository _regionCodeRepository;
        public RegionCodeManagementService(IRegionCodeRepository regionCodeRepository)
        {
            _regionCodeRepository = regionCodeRepository;
        }

        /// <summary>
        /// 获取该国家下所有地区信息
        /// </summary>
        /// <param name="countryCode">国家编号</param>
        /// <returns></returns>
        public IList<RegionCode> FindRegionsByCountryCode(string countryCode)
        {
            return _regionCodeRepository.FindRegionsByCountryCode(countryCode);
        }

        /// <summary>
        /// 通过国家编号模糊查询地区
        /// </summary>
        /// <param name="regionName">地区名称</param>
        /// <param name="countryCode">国家编号</param>
        /// <returns></returns>
        public IList<RegionCode> FindRegionsByCountryCodeAndRegionName(string regionName, string countryCode)
        {
            return _regionCodeRepository.FindRegionsByCountryCodeAndRegionName(regionName, countryCode);
        }

        /// <summary>
        /// 新增地区三字码
        /// </summary>
        /// <param name="regionCode">地区三字码</param>
        public void AddRegionCode(RegionCode regionCode)
        {
            if (regionCode == null)
                throw new ArgumentNullException("Region is null");
            IUnitOfWork unitOfWork = _regionCodeRepository.UnitOfWork;
            _regionCodeRepository.Add(regionCode);
            //complete changes in this unit of work
            unitOfWork.Commit();
        }

        /// <summary>
        /// 修改地区三字码
        /// </summary>
        /// <param name="regionCode">地区三字码</param>
        public void ModifyRegionCode(RegionCode regionCode)
        {
            if (regionCode == null)
                throw new ArgumentNullException("Region is null");
            IUnitOfWork unitOfWork = _regionCodeRepository.UnitOfWork;
            _regionCodeRepository.Modify(regionCode);
            //complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
        }

        /// <summary>
        /// 根据地区三字码获取地区信息
        /// </summary>
        /// <param name="regionCode">地区三字码</param>
        /// <returns></returns>
        public RegionCode FindRegionByRegionCode(string regionCode)
        {
            return _regionCodeRepository.FindRegionByRegionCode(regionCode);
        }

        /// <summary>
        /// 删除地区三字码
        /// </summary>
        /// <param name="regionCode">地区三字码</param>
        public void RemoveRegionCode(RegionCode regionCode)
        {
            if (regionCode == null)
                throw new ArgumentNullException("Region is null");
            IUnitOfWork unitOfWork = _regionCodeRepository.UnitOfWork;
            _regionCodeRepository.Remove(regionCode);
            //complete changes in this unit of work
            unitOfWork.Commit();
        }

        /// <summary>
        /// 地区多条件查询
        /// </summary>
        /// <param name="countryCode">国家二字码</param>
        /// <param name="regioncode">地区三字码</param>
        /// <param name="regionName">地区名称</param>
        /// <returns></returns>
        public IList<RegionCode> FindRegionCodesByCondition(string countryCode, string regioncode, string regionName)
        {
            return _regionCodeRepository.FindRegionCodesByCondition(countryCode, regioncode, regionName);
        }

        /// <summary>
        /// 获取所有的地区
        /// </summary>
        /// <returns></returns>
        public IList<RegionCode> FindAllRegionCodes()
        {
            return _regionCodeRepository.GetAll().ToList();
        }
    }
}
