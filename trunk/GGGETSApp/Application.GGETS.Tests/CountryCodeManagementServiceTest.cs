//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        国家二字码单元测试
// 作成者				ZhiWei.Shen
// 改版日				2011.02.23
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Linq.Expressions;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Infrastructure.CrossCutting.Logging;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.Repositories;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Application.GGETS;

namespace Application.GGETS.Tests
{
    
    
    /// <summary>
    ///这是 CountryCodeManagementServiceTest 的测试类，旨在
    ///包含所有 CountryCodeManagementServiceTest 单元测试
    ///</summary>
    [TestClass()]
    public class CountryCodeManagementServiceTest : RepositoryTestsBase<CountryCode>
    {
        static ICountryCodeManagementService _countryCodeManagementService;//BLL操作类返回
        public CountryCodeManagementServiceTest()
        {
            IGGGETSAppUnitOfWork context = GetUnitOfWork();//上下文
            ITraceManager traceManager = GetTraceManager();//跟踪管理器
            CountryCodeRepository countryCodeRepository = new CountryCodeRepository(context, traceManager);//创建DAL操作对象
            _countryCodeManagementService = new CountryCodeManagementService(countryCodeRepository);
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        #region 获取所有的国家二字码
        /// <summary>
        ///FindAllCountries 的测试
        ///</summary>
        [TestMethod()]
        public void FindAllCountriesTest()
        {
            IList<CountryCode> actual;
            actual = _countryCodeManagementService.FindAllCountries();
        }
        #endregion

        public override Expression<Func<CountryCode, bool>> FilterExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override Expression<Func<CountryCode, int>> OrderByExpression
        {
            get { throw new NotImplementedException(); }
        }
    }
}
