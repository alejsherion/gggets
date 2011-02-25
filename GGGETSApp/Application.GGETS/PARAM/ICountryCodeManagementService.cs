//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        国家二字码IBLL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.12
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace Application.GGETS
{
    public interface ICountryCodeManagementService
    {
        IList<CountryCode> FindAllCountries();
        IList<CountryCode> FindCountriedByCountryName(string countryName);
        void AddCountryCode(CountryCode countryCode);
        void ModifyCountryCode(CountryCode countryCode);
        CountryCode FindCountriedByCountryCode(string countryCode);
        void RemoveCountryCode(CountryCode countryCode);
        IList<CountryCode> FindCountriesByCondition(string countrycode, string countryName);
    }
}
