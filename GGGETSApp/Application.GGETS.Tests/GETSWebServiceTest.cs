using System;
using System.Linq.Expressions;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Infrastructure.CrossCutting.Logging;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.Repositories;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.UnitOfWork;
using GGGETSAdmin.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.GGETS.Tests
{
    /// <summary>
    /// GETSWebServiceTest 的摘要说明
    /// </summary>
    [TestClass]
    public class GETSWebServiceTest : RepositoryTestsBase<Package>
    {
        static IPackageManagementService _packageManagementService;//BLL操作类返回
        static IMAWBManagementService _MAWBManagementService;//BLL操作类返回
        private readonly string WSURL = "http://localhost/GETSB/WebService/GETSWebService.asmx";
        public GETSWebServiceTest()
        {
            IGGGETSAppUnitOfWork context = GetUnitOfWork();//上下文
            ITraceManager traceManager = GetTraceManager();//跟踪管理器
            HAWBRepository HAWBRepository = new HAWBRepository(context, traceManager);//创建DAL操作对象
            PackageRepository packageRepository = new PackageRepository(context, traceManager);
            MAWBRepository MAWBRepository = new MAWBRepository(context, traceManager);

            _packageManagementService = new PackageManagementService(packageRepository, HAWBRepository);
            _MAWBManagementService = new MAWBManagementService(MAWBRepository, HAWBRepository);
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
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
        // 编写测试时，可以使用以下附加特性:
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        #region WebServiceMethod
        [TestMethod]
        public void AddPACKAGE()
        {
            Package package = _packageManagementService.FindPackageByBarcode("TESTPACKAGEPACKAGE");
            if (package.IsSubmit.Equals("0"))
            {
                string jsonStr = UtilityJson.ToJson(package);
                string url = WSURL;
                string[] args = new string[1];
                args[0] = jsonStr;
                object result = WebServiceHelperOperation.InvokeWebService(url, "AddPACKAGE", args);
            }
            else
            {
                //todo 已经实现过推送
            }
        }

        [TestMethod]
        public void AddMAWB()
        {
            MAWB mawb = _MAWBManagementService.FindMAWBByBarcode("TESTMAWBMAWB");
            if (mawb.IsSubmit.Equals("0"))
            {
                string jsonStr = UtilityJson.ToJson(mawb);
                string url = WSURL;
                string[] args = new string[1];
                args[0] = jsonStr;
                object result = WebServiceHelperOperation.InvokeWebService(url, "AddMAWB", args);
            }
            else
            {
                //todo 已经实现过推送
            }
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
