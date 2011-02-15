//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        运单包裹单元测试
// 作成者				ZhiWei.Shen
// 改版日				2011.02.13
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Linq.Expressions;
using Application.GGETS.HAWBManagement;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Infrastructure.CrossCutting.Logging;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.Repositories;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Application.GGETS.Tests
{
    
    
    /// <summary>
    ///这是 PackageManagementServiceTest 的测试类，旨在
    ///包含所有 PackageManagementServiceTest 单元测试
    ///</summary>
    [TestClass()]
    public class PackageManagementServiceTest : RepositoryTestsBase<Package>
    {
        static IPackageManagementService _packageManagementService;//BLL操作类返回
        static IHAWBManagementService _HAWBManagementService;//BLL操作类返回
        public PackageManagementServiceTest()
        {
            IGGGETSAppUnitOfWork context = GetUnitOfWork();//上下文
            ITraceManager traceManager = GetTraceManager();//跟踪管理器
            HAWBRepository HAWBRepository = new HAWBRepository(context, traceManager);//创建DAL操作对象
            HAWBItemRepository HAWBItemRepository = new HAWBItemRepository(context, traceManager);
            HAWBBoxRepository HAWBBoxRepository = new HAWBBoxRepository(context, traceManager);
            UserRepository UserRepository = new UserRepository(context, traceManager);
            IPackageRepository packageRepository = new PackageRepository(context, traceManager);

            _packageManagementService = new PackageManagementService(packageRepository,HAWBRepository);
            _HAWBManagementService = new HAWBManagementService(HAWBRepository, HAWBItemRepository, HAWBBoxRepository,
                                                               UserRepository);
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

        #region 新增包裹(包括其子运单)
        /// <summary>
        ///AddPackage 的测试
        ///</summary>
        [TestMethod()]
        public void AddPackageTest()
        {
            HAWB hawbTest = _HAWBManagementService.FindHAWBByBarCode("2010");
            Package package = new Package
            {
                BarCode = "p1",//条形码
                RegionCode = "001",//地区三字码
                Piece = 10,//件数
                TotalWeight = 10,//总重量
                CreateTime = DateTime.Now,//创建日期
                UpdateTime = DateTime.Now,//更新日期
                Operator = "沈志伟",//操作人员
                Status = 0,//包状态
                IsMixed = true,//是否是混包
            };
            //授权于HAWB对象
            package.HAWB.Add(hawbTest);
            _packageManagementService.AddPackage(package);
            //Assert.Inconclusive("无法验证不返回值的方法。");
        }
        #endregion

        public override Expression<Func<Package, bool>> FilterExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override Expression<Func<Package, int>> OrderByExpression
        {
            get { throw new NotImplementedException(); }
        }
    }
}
