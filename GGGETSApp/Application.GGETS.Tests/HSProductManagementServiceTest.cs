//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        HS商品编码单元测试
// 作成者				ZhiWei.Shen
// 改版日				2011.03.12
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
    ///这是 HSProductManagementServiceTest 的测试类，旨在
    ///包含所有 HSProductManagementServiceTest 单元测试
    ///</summary>
    [TestClass()]
    public class HSProductManagementServiceTest : RepositoryTestsBase<HSProduct>
    {
        static IHSProductManagementService _HSProductManagementService;//BLL操作类返回
        static IHSPropertyManagementService _HSPropertyManagementService;
        public HSProductManagementServiceTest()
        {
            IGGGETSAppUnitOfWork context = GetUnitOfWork();//上下文
            ITraceManager traceManager = GetTraceManager();//跟踪管理器
            HSProductRepository HSProductRepository = new HSProductRepository(context, traceManager);//创建DAL操作对象
            HSPropertyRepository HSPropertyRepository = new HSPropertyRepository(context, traceManager);
            _HSProductManagementService = new HSProductManagementService(HSProductRepository);
            _HSPropertyManagementService = new HSPropertyManagementService(HSPropertyRepository);
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

        public override Expression<Func<HSProduct, bool>> FilterExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override Expression<Func<HSProduct, int>> OrderByExpression
        {
            get { throw new NotImplementedException(); }
        }

        #region 获取所有编码信息
        /// <summary>
        ///GetAll 的测试
        ///</summary>
        [TestMethod()]
        public void GetAllTest1()
        {
            int actual;
            actual = _HSProductManagementService.GetAll().Count;
        }
        #endregion

        #region 更新关系
        /// <summary>
        ///ModifyHSProduct 的测试
        ///</summary>
        [TestMethod()]
        public void ModifyHSProductTest()
        {
            HSProduct product = null; // 获取当前数据库中需要测试的海关商品
            product = _HSProductManagementService.LoadHSProductByHSCode("6304939000");
            HSProperty property = _HSPropertyManagementService.FindHSPropertyByHSPID("da448190-2c7d-4062-8f2b-d808aae731c6");//获取cloths的品名
            _HSProductManagementService.ModifyHSProduct(product);
            //Assert.Inconclusive("无法验证不返回值的方法。");
        }
        #endregion

        /// <summary>
        ///AddHSProduct 的测试
        ///</summary>
        [TestMethod()]
        public void AddHSProductTest()
        {
            HSProduct product = null;
            product = new HSProduct
            {
                HSID=Guid.NewGuid(),
                HSCode="test008",
                HSName="test008",
                DiscountTax=1,
                GeneralTax=1,
                RiseTax=1
            };
            HSProperty property01 = new HSProperty
            {
                HSPID=Guid.NewGuid(),
                PropertyName="property09"
            };
            HSProperty property02 = new HSProperty
            {
                HSPID = Guid.NewGuid(),
                PropertyName = "property10"
            };
            product.HSProperty.Add(property01);
            product.HSProperty.Add(property02);
            _HSProductManagementService.AddHSProduct(product);
        }
    }
}
