//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        地区三字码单元测试
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
    ///这是 RegionCodeManagementServiceTest 的测试类，旨在
    ///包含所有 RegionCodeManagementServiceTest 单元测试
    ///</summary>
    [TestClass()]
    public class RegionCodeManagementServiceTest : RepositoryTestsBase<RegionCode>
    {
        static IRegionCodeManagementService _regionCodeManagementService;//BLL操作类返回
        public RegionCodeManagementServiceTest()
        {
            IGGGETSAppUnitOfWork context = GetUnitOfWork();//上下文
            ITraceManager traceManager = GetTraceManager();//跟踪管理器
            RegionCodeRepository regionCodeRepository = new RegionCodeRepository(context, traceManager);//创建DAL操作对象
            _regionCodeManagementService = new RegionCodeManagementService(regionCodeRepository);
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

        #region 通过国家编号获取地区测试
        /// <summary>
        ///FindRegionsByCountryCode 的测试
        ///</summary>
        [TestMethod()]
        public void FindRegionsByCountryCodeTest()
        {
            string countryCode = "46"; // 国家编号
            IList<RegionCode> expected = null; 
            IList<RegionCode> actual;
            actual = _regionCodeManagementService.FindRegionsByCountryCode(countryCode);
        }
        #endregion

        #region 模糊查询地区通过国家编号测试
        /// <summary>
        ///FindRegionsByCountryCodeAndRegionName 的测试
        ///</summary>
        [TestMethod()]
        public void FindRegionsByCountryCodeAndRegionNameTest()
        {
            string regionName = "a"; // 地区名称
            string countryCode = "12"; // 国家编号
            IList<RegionCode> actual;
            actual = _regionCodeManagementService.FindRegionsByCountryCodeAndRegionName(regionName, countryCode);
        }
        #endregion

        #region 新增地区三字码
        /// <summary>
        ///AddRegionCode 的测试
        ///</summary>
        [TestMethod()]
        public void AddRegionCodeTest()
        {
            RegionCode regionCode = null; // 地区三字码
            regionCode = new RegionCode
            {
                ID=988,
                CountryCode="99",
                RegionCode1="TTT",
                RegionName="TEST"
            };
            _regionCodeManagementService.AddRegionCode(regionCode);
            //Assert.Inconclusive("无法验证不返回值的方法。");
        }
        #endregion

        #region 修改地区三字码
        /// <summary>
        ///ModifyRegionCode 的测试
        ///</summary>
        [TestMethod()]
        public void ModifyRegionCodeTest()
        {
            RegionCode regionCode = null; // 地区
            regionCode = _regionCodeManagementService.FindRegionsByCountryCode("99")[0];
            regionCode.RegionName = "TEST007";
            _regionCodeManagementService.ModifyRegionCode(regionCode);
            //Assert.Inconclusive("无法验证不返回值的方法。");
        }
        #endregion

        #region 通过地区三字码获取地区
        /// <summary>
        ///FindRegionByRegionCode 的测试
        ///</summary>
        [TestMethod()]
        public void FindRegionByRegionCodeTest()
        {
            string regionCode = "TTT"; // 地区三字码
            RegionCode actual;
            actual = _regionCodeManagementService.FindRegionByRegionCode(regionCode);
        }
        #endregion

        #region 删除地区三字码
        /// <summary>
        ///RemoveRegionCode 的测试
        ///</summary>
        [TestMethod()]
        public void RemoveRegionCodeTest()
        {
            RegionCode regionCode = null; // 地区三字码
            regionCode = _regionCodeManagementService.FindRegionByRegionCode("TTT");
            _regionCodeManagementService.RemoveRegionCode(regionCode);
            //Assert.Inconclusive("无法验证不返回值的方法。");
        }
        #endregion

        public override Expression<Func<RegionCode, bool>> FilterExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override Expression<Func<RegionCode, int>> OrderByExpression
        {
            get { throw new NotImplementedException(); }
        }
    }
}
