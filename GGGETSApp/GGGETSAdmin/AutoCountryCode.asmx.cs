using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;

namespace GGGETSAdmin
{
    /// <summary>
    /// AutoCountryCode 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class AutoCountryCode : System.Web.Services.WebService
    {
        private IList<CountryCode> countrycode;
        private ICountryCodeManagementService _countryservice;
        protected AutoCountryCode()
        { }
        public AutoCountryCode(ICountryCodeManagementService countryservice)
        {
            _countryservice = countryservice;
        }
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string[] GetCountryList(string prefixText, int count)
        {
            if (count == 0)
            {
                count = 10;
            }

            List<string> items = new List<string>(count);

            countrycode = _countryservice.FindAllCountries();
            foreach (CountryCode country in countrycode)
            {
                items.Add(country.CountryName);
            }
            return items.ToArray();
        }
    }
}
