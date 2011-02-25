// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        地址本单元测试
// 作成者				ZhiWei.Shen
// 改版日				2011.02.24
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
    ///这是 AddressBookManagementServiceTest 的测试类，旨在
    ///包含所有 AddressBookManagementServiceTest 单元测试
    ///</summary>
    [TestClass()]
    public class AddressBookManagementServiceTest : RepositoryTestsBase<AddressBook>
    {
        static IAddressBookManagementService _addressBookManagementService;//BLL操作类返回
        public AddressBookManagementServiceTest()
        {
            IGGGETSAppUnitOfWork context = GetUnitOfWork();//上下文
            ITraceManager traceManager = GetTraceManager();//跟踪管理器
            DepartmentRepository departmentRepository = new DepartmentRepository(context, traceManager);//创建DAL操作对象
            AddressBookRepository addressBookRepository = new AddressBookRepository(context, traceManager);

            _addressBookManagementService = new AddressBookManagementService(departmentRepository, addressBookRepository);
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

        #region 根据序号获取地址本信息
        /// <summary>
        ///FindAddressBookByAID 的测试
        ///</summary>
        [TestMethod()]
        public void FindAddressBookByAIDTest()
        {
            string AID = "a5c22b70-98bf-4aa8-b427-891bd1c7161d"; // 序号
            AddressBook actual;
            actual = _addressBookManagementService.FindAddressBookByAID(AID);
        }
        #endregion

        #region 修改地址本,不存在数据库产生僵尸地址本，所以这里只会修改地址本属性
        /// <summary>
        ///ModifyAddressBook 的测试
        ///</summary>
        [TestMethod()]
        public void ModifyAddressBookTest()
        {
            AddressBook addressBook = null; // 地址本
            addressBook = _addressBookManagementService.FindAddressBookByAID("c976f88c-565f-41e4-bca9-1d80c06291b9");
            addressBook.Name = "TEST";
            _addressBookManagementService.ModifyAddressBook(addressBook);
            //Assert.Inconclusive("无法验证不返回值的方法。");
        }
        #endregion

        public override Expression<Func<AddressBook, bool>> FilterExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override Expression<Func<AddressBook, int>> OrderByExpression
        {
            get { throw new NotImplementedException(); }
        }
    }
}
