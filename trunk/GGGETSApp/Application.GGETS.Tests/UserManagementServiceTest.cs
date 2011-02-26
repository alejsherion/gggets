// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        用户单元测试
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
    ///这是 UserManagementServiceTest 的测试类，旨在
    ///包含所有 UserManagementServiceTest 单元测试
    ///</summary>
    [TestClass()]
    public class UserManagementServiceTest : RepositoryTestsBase<User>
    {
        private static IDepartmentManagementService _departmentManagementService;//BLL操作类返回
        private static IHAWBManagementService _HAWBManagementService;//BLL操作类返回
        private static IUserManagementService _userManagementService;
        private static ICompanyManagementService _companyManagementService;
        private static IAddressBookManagementService _addressBookManagementService;
         public UserManagementServiceTest()
        {
            IGGGETSAppUnitOfWork context = GetUnitOfWork();//上下文
            ITraceManager traceManager = GetTraceManager();//跟踪管理器
            HAWBRepository HAWBRepository = new HAWBRepository(context, traceManager);//创建DAL操作对象
            DepartmentRepository departmentRepository = new DepartmentRepository(context, traceManager);
            HAWBItemRepository HAWBItemRepository = new HAWBItemRepository(context, traceManager);
            HAWBBoxRepository HAWBBoxRepository = new HAWBBoxRepository(context, traceManager);
            UserRepository UserRepository = new UserRepository(context, traceManager);
            IUserRepository userRepository = new UserRepository(context, traceManager);
            CompanyRepository companyRepository = new CompanyRepository(context, traceManager);
            AddressBookRepository addressBookRepository = new AddressBookRepository(context, traceManager);

            _departmentManagementService = new DepartmentManagementService(departmentRepository, HAWBRepository, userRepository, companyRepository, addressBookRepository);
            _HAWBManagementService = new HAWBManagementService(HAWBRepository, HAWBItemRepository, HAWBBoxRepository,
                                                               UserRepository);
            _userManagementService = new UserManagementService(departmentRepository, userRepository, addressBookRepository);
            _companyManagementService = new CompanyManagementService(departmentRepository, companyRepository);
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

        #region 通过用户账号获取用户信息
        /// <summary>
        ///FindUserByLoginName 的测试
        ///</summary>
        [TestMethod()]
        public void FindUserByLoginNameTest()
        {
            string loginName = "U1"; // 用户账号
            User actual;
            actual = _userManagementService.FindUserByLoginName(loginName);
        }
        #endregion

        #region 新增用户
        /// <summary>
        ///AddUser 的测试
        ///</summary>
        [TestMethod()]
        public void AddUserTest()
        {
            User user = null; // 用户
            user = new User
            {
                UID = Guid.NewGuid(),//用户序号
                LoginName = "TEST",//用户账号
                Password = "TEST",//用户密码
                RealName = "TEST",//用户真实姓名
                UpdateTime = DateTime.Now,//更新日期
                CreateTime = DateTime.Now,//创建日期
                Operator = "TEST",//操作人
                FeeDiscountRate = 1,//费用折扣率
                FeeDiscountType = 0,//费用折扣类型
                WeightDiscountRate = 1,//重量折扣率
                WeightDiscountType = 0,//重量折扣类型
                SettleType = 0,//结算方式
                WeightCalType = 0,//计重方式
                Status = 0//可用与不可用
            };
            //获取现由部门
            Department department = _departmentManagementService.FindDepartmentByDepCode("TEST");
            //获取运单
            HAWB hawb = _HAWBManagementService.FindHAWBByBarCode("2012");
            //创建地址本
            AddressBook addressBook = new AddressBook
            {
                AID = Guid.NewGuid(),//地址本序号
                Name = department.CompanyCode,//公司名称
                ContactorName = "TEST",//联系人真名
                Provience = "TEST",//省份
                CountryCode = "TT",//国家二字码
                RegionCode = "TTT",//地区三字码
                Address = "TEST",//地址
                PostCode = "TEST",//邮政编码
                AddressType = 0,//地址类型-发件人地址
                CreateTime = DateTime.Now,//创建日期
                UpdateTime = DateTime.Now,//修改日期
                Operator = "TEST"//操作人姓名
            };
            user.HAWBs.Add(hawb);
            user.Department = department;
            user.AddressBooks.Add(addressBook);
            _userManagementService.AddUser(user);
            //Assert.Inconclusive("无法验证不返回值的方法。");
        }
        #endregion

        #region 修改用户
        /// <summary>
        ///ModifyUser 的测试
        ///</summary>
        [TestMethod()]
        public void ModifyUserTest()
        {
            User user = null; // 用户
            //获取当前用户
            user = _userManagementService.FindUserByLoginName("TEST");
            //删除里面的地址本
            AddressBook addressBook = _addressBookManagementService.FindAddressBookByAID("44834b89-fe2c-47dd-b6e7-3dbea94c25f6");
            user.AddressBooks.Remove(addressBook);
            _userManagementService.ModifyUser(user);
            //Assert.Inconclusive("无法验证不返回值的方法。");
        }
        #endregion

        public override Expression<Func<User, bool>> FilterExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override Expression<Func<User, int>> OrderByExpression
        {
            get { throw new NotImplementedException(); }
        }
    }
}
