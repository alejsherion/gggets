//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        航班单元测试
// 作成者				ZhiWei.Shen
// 改版日				2011.02.21
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
    ///这是 FlightManagementServiceTest 的测试类，旨在
    ///包含所有 FlightManagementServiceTest 单元测试
    ///</summary>
    [TestClass()]
    public class FlightManagementServiceTest : RepositoryTestsBase<Flight>
    {
        static IFlightManagementService _flightManagementService;//BLL操作类返回
        static IHAWBManagementService _HAWBManagementService;//BLL操作类返回
        public FlightManagementServiceTest()
        {
            IGGGETSAppUnitOfWork context = GetUnitOfWork();//上下文
            ITraceManager traceManager = GetTraceManager();//跟踪管理器
            HAWBRepository HAWBRepository = new HAWBRepository(context, traceManager);//创建DAL操作对象
            HAWBItemRepository HAWBItemRepository = new HAWBItemRepository(context, traceManager);
            HAWBBoxRepository HAWBBoxRepository = new HAWBBoxRepository(context, traceManager);
            UserRepository UserRepository = new UserRepository(context, traceManager);
            PackageRepository packageRepository = new PackageRepository(context, traceManager);
            FlightRepository flightRepository = new FlightRepository(context, traceManager);

            _flightManagementService = new FlightManagementService(flightRepository, HAWBRepository);
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

        #region 多条件航班查询测试
        /// <summary>
        ///FindFlightByCondition 的测试
        ///</summary>
        [TestMethod()]
        public void FindFlightByConditionTest()
        {
            string flightNo = string.Empty; // 航班号
            string from = string.Empty; // 起始地
            string to = string.Empty; // 目的地
            Nullable<DateTime> beginDate = new DateTime(2011, 2, 18); // 开始日期
            Nullable<DateTime> endDate = new DateTime(2011, 2, 19);  // 结束日期
            IList<Flight> actual;
            actual = _flightManagementService.FindFlightByCondition(flightNo, from, to, beginDate, endDate);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("验证此测试方法的正确性。");
        }
        #endregion

        #region 根据航班号获取航班测试
        /// <summary>
        ///FindFlightByFlightNo 的测试
        ///</summary>
        [TestMethod()]
        public void FindFlightByFlightNoTest()
        {
            string flightNo = "T565"; // 航班号
            Flight actual;
            actual = _flightManagementService.FindFlightByFlightNo(flightNo);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("验证此测试方法的正确性。");
        }
        #endregion

        #region 修改航班测试
        /// <summary>
        ///ModifyFlight 的测试
        ///</summary>
        [TestMethod()]
        public void ModifyFlightTest()
        {
            Flight flight = _flightManagementService.FindFlightByFlightNo("T565"); // 获取当前数据库存在的航班信息
            flight.TakeOffTime = new DateTime(2011, 2, 20, 16, 0, 0);//修改起飞日期为2011-2-20 下午4点整
            _flightManagementService.ModifyFlight(flight);
            //Assert.Inconclusive("无法验证不返回值的方法。");
        }
        #endregion

        public override Expression<Func<Flight, bool>> FilterExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override Expression<Func<Flight, int>> OrderByExpression
        {
            get { throw new NotImplementedException(); }
        }
    }
}
