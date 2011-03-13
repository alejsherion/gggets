//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        HS分配单元测试
// 作成者				ZhiWei.Shen
// 改版日				2011.03.13
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
    ///这是 HSRelationManagementServiceTest 的测试类，旨在
    ///包含所有 HSRelationManagementServiceTest 单元测试
    ///</summary>
    [TestClass()]
    public class HSRelationManagementServiceTest : RepositoryTestsBase<HSRelation>
    {
        static IHSRelationManagementService _HSRelationManagementService;//BLL操作类返回
        static IHSProductManagementService _HSProductManagementService;
        static IHSPropertyManagementService _HSPropertyManagementService;
        public HSRelationManagementServiceTest()
        {
            IGGGETSAppUnitOfWork context = GetUnitOfWork();//上下文
            ITraceManager traceManager = GetTraceManager();//跟踪管理器
            HSRelationRepository HSRelationRepository = new HSRelationRepository(context, traceManager);//创建DAL操作对象
            HSProductRepository HSProductRepository = new HSProductRepository(context, traceManager);
            HSPropertyRepository HSPropertyRepository = new HSPropertyRepository(context, traceManager);
            _HSRelationManagementService = new HSRelationManagementService(HSRelationRepository);
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


        /// <summary>
        ///ModifyHSRelation 的测试
        ///</summary>
        [TestMethod()]
        public void ModifyHSRelationTest()
        {
            HSRelation relation = null;
            _HSRelationManagementService.ModifyHSRelation(relation);
        }

        public override Expression<Func<HSRelation, bool>> FilterExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override Expression<Func<HSRelation, int>> OrderByExpression
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        ///AddHSRelation 的测试
        ///</summary>
        [TestMethod()]
        public void AddHSRelationTest()
        {
            HSRelation relation = null;
            HSProduct product = _HSProductManagementService.LoadHSProductByHSCode("6304939000");
            HSProperty property = _HSPropertyManagementService.FindHSPropertyByHSPID("da448190-2c7d-4062-8f2b-d808aae731c6");//获取cloths的品名

            relation = new HSRelation
                           {
                               RID=Guid.NewGuid(),
                               HSID=product.HSID,
                               HSPID=property.HSPID
                           };
            _HSRelationManagementService.AddHSRelation(relation);
            //Assert.Inconclusive("无法验证不返回值的方法。");
        }
    }
}
