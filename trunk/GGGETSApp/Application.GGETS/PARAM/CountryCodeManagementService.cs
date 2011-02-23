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
    }
}
