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
            PackageRepository packageRepository = new PackageRepository(context, traceManager);

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
            package.HAWBs.Add(hawbTest);
            _packageManagementService.AddPackage(package);
            //Assert.Inconclusive("无法验证不返回值的方法。");
        }
        #endregion

        #region 通过条形码查询包裹
        /// <summary>
        ///FindPackageByBarcode 的测试
        ///</summary>
        [TestMethod()]
        public void FindPackageByBarcodeTest()
        {
            string barcode = "p1";
            //Package expected = null; 
            Package actual;
            actual = _packageManagementService.FindPackageByBarcode(barcode);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("验证此测试方法的正确性。");
        }
        #endregion

        #region 修改包裹
        /// <summary>
        ///ModifyPackage 的测试
        ///</summary>
        [TestMethod()]
        public void ModifyPackageTest()
        {
            Package package = _packageManagementService.FindPackageByBarcode("p1");
            HAWB hawb = _HAWBManagementService.FindHAWBByBarCode("2011");
            package.HAWBs.Add(hawb);
            _packageManagementService.ModifyPackage(package);
            //Assert.Inconclusive("无法验证不返回值的方法。");
        }
        #endregion

        #region 测试包裹总重量和总件数动态变化情况
        /// <summary>
        ///TestPackageTotalWeightAndPiece 的测试
        ///</summary>
        [TestMethod()]
        public void TestPackageTotalWeightAndPiece()
        {
            string barcode = "p1";
            //Package expected = null; 
            Package actual;
            actual = _packageManagementService.FindPackageByBarcode(barcode);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("验证此测试方法的正确性。");
        }
        #endregion

        #region 判断运单重复的测试
        /// <summary>
        ///TestHAWBInPackage 的测试
        ///</summary>
        [TestMethod()]
        public void TestHAWBInPackage()
        {
            //实力化一个虚假运单对象
            HAWB HAWBObj = new HAWB
            {
                //ID
                HID=new Guid("00000000-0000-0000-0000-000000000002"),
                //条形码
                BarCode = "2013",
                //条形码
                Carrier = "中国航空8",
                //承运单位
                SettleType = 0,
                //预算方式
                ServiceType = 0,
                //快件或包裹
                CreateTime = DateTime.Now,
                //创建日期
                Status = 0,
                //运单状态
                ShipperName = "沈志伟",
                //发件人姓名或者公司
                ShipperContactor = "沈志伟",
                //发件人姓名
                ShipperCountry = "01",
                //发件人国家
                ShipperRegion = "021",
                //发件人区号
                ShipperAddress = "test address",
                //发件人地址
                ShipperZipCode = "200435",
                //发件人邮编
                ShipperTel = "13817011234",
                //发件人联系电话
                ConsigneeContactor = "李宏",
                //收件人姓名
                ConsigneeCountry = "02",
                //收件人国家
                ConsigneeRegion = "022",
                //收件人区号
                ConsigneeAddress = "Japan address",
                //收件人地址
                ConsigneeZipCode = "201011",
                //收件人邮编
                ConsigneeTel = "120120",
                //收件人联系电话
                WeightType = 2,
                //计重方式
                TotalVolume = 10,
                //总体积
                TotalWeight = 10,
                //总重量
                Piece = 10,
                //件数
                IsInternational = true //是否是国际运单
            };
            //获取P1包裹
            Package package = _packageManagementService.FindPackageByBarcode("p1");
            package.JudgeHAWB(HAWBObj);
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
