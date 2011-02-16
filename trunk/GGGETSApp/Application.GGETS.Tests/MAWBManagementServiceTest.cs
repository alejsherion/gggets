﻿//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        总运单单元测试
// 作成者				ZhiWei.Shen
// 改版日				2011.02.15
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
    ///这是 MAWBManagementServiceTest 的测试类，旨在
    ///包含所有 MAWBManagementServiceTest 单元测试
    ///</summary>
    [TestClass()]
    public class MAWBManagementServiceTest: RepositoryTestsBase<MAWB>
    {
        static IPackageManagementService _packageManagementService;//BLL操作类返回
        static IMAWBManagementService _MAWBManagementService;//BLL操作类返回
        static IHAWBManagementService _HAWBManagementService;//BLL操作类返回
        public MAWBManagementServiceTest()
        {
            IGGGETSAppUnitOfWork context = GetUnitOfWork();//上下文
            ITraceManager traceManager = GetTraceManager();//跟踪管理器
            HAWBRepository HAWBRepository = new HAWBRepository(context, traceManager);//创建DAL操作对象
            HAWBItemRepository HAWBItemRepository = new HAWBItemRepository(context, traceManager);
            HAWBBoxRepository HAWBBoxRepository = new HAWBBoxRepository(context, traceManager);
            UserRepository UserRepository = new UserRepository(context, traceManager);
            MAWBRepository MAWBRepository = new MAWBRepository(context, traceManager);
            PackageRepository packageRepository = new PackageRepository(context, traceManager);

            _packageManagementService = new PackageManagementService(packageRepository, HAWBRepository);
            _MAWBManagementService = new MAWBManagementService(MAWBRepository, HAWBRepository);
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

        #region 通过条形码查询MAWB测试 
        /// <summary>
        ///FindMAWBByBarcode 的测试
        ///</summary>
        [TestMethod()]
        public void FindMAWBByBarcodeTest()
        {
            string barcode = "2012"; 
            MAWB actual;
            actual = _MAWBManagementService.FindMAWBByBarcode(barcode);
        }
        #endregion

        #region 新增总运单测试
        /// <summary>
        ///AddMAWB 的测试
        ///</summary>
        [TestMethod()]
        public void AddMAWBTest()
        {
            //new a MAWB
            MAWB mawb = new MAWB
            {
                MID = new Guid("00000000-0000-0000-0000-000000000001"),
                BarCode="2013",
                CreateTime=DateTime.Now,
                Operator="tester",
                TotalWeight=20,
                TotalVolume=20,
                Status=0
            };
            Package package = _packageManagementService.FindPackageByBarcode("p2");
            mawb.Package.Add(package);
            _MAWBManagementService.AddMAWB(mawb);
        }
        #endregion

        public override Expression<Func<MAWB, bool>> FilterExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override Expression<Func<MAWB, int>> OrderByExpression
        {
            get { throw new NotImplementedException(); }
        }
    }
}
