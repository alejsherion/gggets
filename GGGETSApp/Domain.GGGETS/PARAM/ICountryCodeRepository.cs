//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        国家二字码IDAL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.23
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Domain.Core;

namespace Domain.GGGETS
{
    public interface ICountryCodeRepository : IRepository<CountryCode>
    {
        IList<CountryCode> FindAllCountries();
        IList<CountryCode> FindCountriedByCountryName(string countryName);
        CountryCode FindCountriedByCountryCode(string countryCode);
        IList<CountryCode> FindCountriesByCondition(string country, string countryName);
        IList<CountryCode> FindCountriesByCondition(string country, string countryName,int pageIndex, int pageCount);
    }
}
