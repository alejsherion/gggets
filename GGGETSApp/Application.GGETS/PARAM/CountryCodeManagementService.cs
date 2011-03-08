//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        国家二字码BLL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.12
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
    public class CountryCodeManagementService:ICountryCodeManagementService
    {
        /// <summary>
        /// IOC Injecting into
        /// </summary>
        ICountryCodeRepository _countryCodeRepository;
        public CountryCodeManagementService(ICountryCodeRepository countryCodeRepository)
        {
            _countryCodeRepository = countryCodeRepository;
        }

        /// <summary>
        /// 获取所有的国家信息
        /// </summary>
        /// <returns></returns>
        public IList<CountryCode> FindAllCountries()
        {
            return _countryCodeRepository.FindAllCountries();
        }

        /// <summary>
        /// 通过国家名字模糊查询国家信息
        /// </summary>
        /// <param name="countryName">国家名称</param>
        /// <returns></returns>
        public IList<CountryCode> FindCountriedByCountryName(string countryName)
        {
            return _countryCodeRepository.FindCountriedByCountryName(countryName);
        }

        /// <summary>
        /// 新增国家信息
        /// </summary>
        /// <param name="countryCode">国家编号</param>
        public void AddCountryCode(CountryCode countryCode)
        {
            if (countryCode == null)
                throw new ArgumentNullException("Country is null");
            IUnitOfWork unitOfWork = _countryCodeRepository.UnitOfWork;
            _countryCodeRepository.Add(countryCode);
            //complete changes in this unit of work
            unitOfWork.Commit();
        }

        /// <summary>
        /// 修改国家
        /// </summary>
        /// <param name="countryCode">国家</param>
        public void ModifyCountryCode(CountryCode countryCode)
        {
            if (countryCode == null)
                throw new ArgumentNullException("Country is null");
            IUnitOfWork unitOfWork = _countryCodeRepository.UnitOfWork;
            _countryCodeRepository.Modify(countryCode);
            //complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
        }

        /// <summary>
        /// 通过国家二字码获取国家信息
        /// </summary>
        /// <param name="countryCode">国家编号</param>
        /// <returns></returns>
        public CountryCode FindCountriedByCountryCode(string countryCode)
        {
            return _countryCodeRepository.FindCountriedByCountryCode(countryCode);
        }

        /// <summary>
        /// 删除国家
        /// </summary>
        /// <param name="countryCode">国家</param>
        public void RemoveCountryCode(CountryCode countryCode)
        {
            if (countryCode == null)
                throw new ArgumentNullException("Country is null");
            IUnitOfWork unitOfWork = _countryCodeRepository.UnitOfWork;
            _countryCodeRepository.Remove(countryCode);
            //complete changes in this unit of work
            unitOfWork.Commit();
        }

        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="countryCode">国家二字码</param>
        /// <param name="countryName">国家名称</param>
        /// <returns></returns>
        public IList<CountryCode> FindCountriesByCondition(string countryCode, string countryName)
        {
            return _countryCodeRepository.FindCountriesByCondition(countryCode, countryName);
        }

        public IList<CountryCode> FindCountriesByCondition(string countrycode, string countryName, int pageIndex, int pageCount, ref int totalCount)
        {
            IList<CountryCode> countries = FindCountriesByCondition(countrycode, countryName);
            if (countries != null) totalCount = countries.Count();
            else totalCount = 0;

            return _countryCodeRepository.FindCountriesByCondition(countrycode, countryName, pageIndex, pageCount);
        }
    }
}
