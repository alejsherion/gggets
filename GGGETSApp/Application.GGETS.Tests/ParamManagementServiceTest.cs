//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        模板参数单元测试
// 作成者				ZhiWei.Shen
// 改版日				2011.02.28
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Linq.Expressions;
using System.Web.UI;
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
    ///这是 ParamManagementServiceTest 的测试类，旨在
    ///包含所有 ParamManagementServiceTest 单元测试
    ///</summary>
    [TestClass()]
    public class ParamManagementServiceTest : RepositoryTestsBase<Param>
    {
        static IParamManagementService _paramManagementService;//BLL操作类返回
        public ParamManagementServiceTest()
        {
            IGGGETSAppUnitOfWork context = GetUnitOfWork();//上下文
            ITraceManager traceManager = GetTraceManager();//跟踪管理器
            HAWBRepository HAWBRepository = new HAWBRepository(context, traceManager);//创建DAL操作对象
            HAWBItemRepository HAWBItemRepository = new HAWBItemRepository(context, traceManager);
            HAWBBoxRepository HAWBBoxRepository = new HAWBBoxRepository(context, traceManager);
            UserRepository UserRepository = new UserRepository(context, traceManager);
            PackageRepository packageRepository = new PackageRepository(context, traceManager);
            TemplateRepository templateRepository = new TemplateRepository(context, traceManager);
            ParamRepository paramRepository = new ParamRepository(context, traceManager);
            MAWBRepository mawbRepository = new MAWBRepository(context, traceManager);

            _paramManagementService = new ParamManagementService(paramRepository, mawbRepository, HAWBRepository);
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

        public override Expression<Func<Param, bool>> FilterExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override Expression<Func<Param, int>> OrderByExpression
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        ///MaintainDan 的测试
        ///</summary>
        [TestMethod()]
        public void MaintainDanTest()
        {
            int intOrient = 0; 
            int intPageWidth = 0; 
            int intPageHeight = 0;
            string strPageName = string.Empty; 
            string identifyKey = string.Empty;
            string templateKey = string.Empty;
            int operateType = 0; 
            Page page = null;
            _paramManagementService.MaintainDan(intOrient, intPageWidth, intPageHeight, strPageName, identifyKey, templateKey, operateType, page);
            //Assert.Inconclusive("无法验证不返回值的方法。");
        }
    }
}
