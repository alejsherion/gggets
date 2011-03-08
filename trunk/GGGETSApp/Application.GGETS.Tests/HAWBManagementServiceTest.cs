//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        国内外运单单元测试
// 作成者				ZhiWei.Shen
// 改版日				2011.02.12
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Linq.Expressions;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Infrastructure.CrossCutting.Logging;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.Repositories;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Application.GGETS;
using Domain.GGGETS;

namespace Application.GGETS.Tests
{
    /// <summary>
    /// HAWBManagementServiceTest 的摘要说明
    /// </summary>
    [TestClass]
    public class HAWBManagementServiceTest:RepositoryTestsBase<HAWB>
    {
        static IHAWBManagementService _HAWBManagementService;//BLL操作类返回
        public HAWBManagementServiceTest()
        {
            IGGGETSAppUnitOfWork context = GetUnitOfWork();//上下文
            ITraceManager traceManager = GetTraceManager();//跟踪管理器
            HAWBRepository HAWBRepository = new HAWBRepository(context, traceManager);//创建DAL操作对象
            HAWBItemRepository HAWBItemRepository = new HAWBItemRepository(context, traceManager);
            HAWBBoxRepository HAWBBoxRepository = new HAWBBoxRepository(context, traceManager);
            UserRepository UserRepository = new UserRepository(context, traceManager);

            _HAWBManagementService = new HAWBManagementService(HAWBRepository, HAWBItemRepository, HAWBBoxRepository, UserRepository);
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

        #region 新增运单单元测试
        [TestMethod]
        public void TestAddHAWB()
        {
            //实力化一个虚假运单对象
            HAWB HAWBObj = new HAWB
                               {
                                   BarCode = "2010",
                                   //条形码
                                   Carrier = "中国航空",
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
            //定义盒子
            HAWBBox HAWBBox01 = new HAWBBox
            {
                BoxType = 0,
                //盒子类型
                Weight = 10,
                //重量
                TransCurrency = 0,
                //运费货币
                Piece = 10 //件数
            };
            HAWBBox HAWBBox02 = new HAWBBox
            {
                BoxType = 0,
                //盒子类型
                Weight = 20,
                //重量
                TransCurrency = 0,
                //运费货币
                Piece = 20 //件数
            };
            //定义货物
            HAWBItem HAWBItem01 = new HAWBItem
            {
                Name = "货物01",
                //货物名称
                Piece = 10,
                //货物件数
                UnitAmount = 10,
                //单价
                TotalAmount = 10 //总价
            };
            HAWBItem HAWBItem02 = new HAWBItem
            {
                Name = "货物02",
                //货物名称
                Piece = 20,
                //货物件数
                UnitAmount = 20,
                //单价
                TotalAmount = 20 //总价
            };
            //定义企业用户
            User user01 = new User
                              {
                                  LoginName = "test001",
                                  //注册名
                                  Password = "123456",
                                  //密码
                                  RealName = "张三",
                                  //真实姓名
                                  //Department = "研发部",
                                  //部门名称
                                  //PostCode = "200435",
                                  //邮政编码
                                  Phone = "110",
                                  //电话
                                  //CountryCode = "01",
                                  //国家编号
                                  //RegionCode = "01",
                                  //区域编号
                                  //Address = "XXX路XXX号",
                                  //地址
                                  UpdateTime = DateTime.Now,
                                  //更新日期
                                  CreateTime = DateTime.Now,
                                  //创建日期
                                  Operator = "沈志伟",
                                  //操作人
                                  FeeDiscountType = 0,
                                  //费用折扣类型
                                  FeeDiscountRate = 1,
                                  //费用折扣率
                                  WeightDiscountType = 0,
                                  //重量折扣类型
                                  WeightDiscountRate = 1,
                                  //重量折扣率
                                  SettleType = 0,
                                  //结算类型
                                  WeightCalType = 0,
                                  //计量方式
                                  Status = 0 //可用状态
                              };

            //in
            HAWBObj.HAWBBoxes.Add(HAWBBox01);
            //HAWBObj.HAWBBox.Add(HAWBBox02);
            //HAWBObj.HAWBItems.Add(HAWBItem01);
            //HAWBObj.HAWBItem.Add(HAWBItem02);
            HAWBObj.User = user01;

            //begin
            _HAWBManagementService.AddHAWB(HAWBObj);

        }
        #endregion

        #region 修改运单单元测试
        /// <summary>
        ///ChangeHAWB 的测试
        ///</summary>
        [TestMethod()]
        public void ChangeHAWBTest()
        {
            HAWB HAWBObj = _HAWBManagementService.FindHAWBByBarCode("2010");//根据条形码获取对应运单对象
            //HAWBObj.Carrier = "航空公司03";
            HAWBBox HAWBBox = _HAWBManagementService.FindHAWBBoxByHID(HAWBObj.HID.ToString());
            //HAWBObj.RemoveHAWBBox(HAWBBox);
            _HAWBManagementService.ChangeHAWB(HAWBObj);//修改
            //Assert.Inconclusive("无法验证不返回值的方法。");
        }
        #endregion

        #region 分页查询运单单元测试
        /// <summary>
        ///FindPagedHAWBs 的测试
        ///</summary>
        [TestMethod()]
        public void FindPagedHAWBsTest()
        {
            int pageIndex = 0; 
            int pageCount = 5;
            HAWB HAWBObj = _HAWBManagementService.FindHAWBByBarCode("2010");
            List<HAWB> expected = new List<HAWB>();
            expected.Add(HAWBObj);
            List<HAWB> actual;
            actual = _HAWBManagementService.FindPagedHAWBs(pageIndex, pageCount);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("验证此测试方法的正确性。");
        }
        #endregion

        #region 运单多条件查询
        /// <summary>
        ///FindHAWBsByCondition 的测试
        ///</summary>
        [TestMethod()]
        public void FindHAWBsByConditionTest()
        {
            string barCode = string.Empty;
            string countryCode = string.Empty; 
            string regionCode = string.Empty; 
            string departmentCode = "01"; 
            string companyCode = "M18";
            string carrier = string.Empty; 
            string HAWBOperator = string.Empty;
            string contactor = string.Empty;
            Nullable<DateTime> beginTime = new Nullable<DateTime>();
            Nullable<DateTime> endTime = new Nullable<DateTime>();
            int settleType = 0;
            int serviceType = 0; 
            Nullable<bool> isInternational = new Nullable<bool>(); 
            IList<HAWB> actual;
            actual = _HAWBManagementService.FindHAWBsByCondition(barCode, countryCode, regionCode, departmentCode, companyCode, carrier, HAWBOperator, contactor, beginTime, endTime, settleType, serviceType, isInternational);
        }
        #endregion

        #region 删除运单单元测试
        /// <summary>
        ///RemoveHAWB 的测试
        ///</summary>
        [TestMethod()]
        public void RemoveHAWBTest()
        {
            _HAWBManagementService.RemoveHAWB("2010");
            //Assert.Inconclusive("无法验证不返回值的方法。");
        }
        #endregion

        #region 测试运单总重量，总体积，总件数和体积重量
        /// <summary>
        ///RemoveHAWBTotalCal 的测试
        ///</summary>
        [TestMethod()]
        public void RemoveHAWBTotalCal()
        {
            HAWB hawb = _HAWBManagementService.LoadHAWBByBarCode("2010");
            decimal actualTotalWeight = 0;
            decimal actualVolumeWeight = 0;
            int actualTotalPiece = 0;
            decimal? actualTotalVolume = 0;
            foreach(HAWBBox box in hawb.HAWBBoxes)
            {
                actualTotalWeight += box.Weight;
                actualTotalPiece += box.Piece;
                actualTotalVolume += box.Length*box.Width*box.Height;
                
            }
            if (actualTotalWeight != 0)
                actualVolumeWeight = Math.Round(actualTotalWeight / 166, 2);
            //test
            Assert.AreEqual(actualTotalWeight, hawb.TotalWeight);
            Assert.AreEqual(actualVolumeWeight, hawb.VolumeWeight);
            Assert.AreEqual(actualTotalPiece, hawb.Piece);
            Assert.AreEqual(actualTotalVolume, hawb.TotalVolume);
            //Assert.Inconclusive("验证此测试方法的正确性。");
        }
        #endregion

        #region 测试多条件查询，通过运单间接查询包裹和总运单
        /// <summary>
        ///FindHAWBsOfPackageByCondition 的测试
        ///</summary>
        [TestMethod()]
        public void FindHAWBsOfPackageByConditionTest()
        {
            string barCode = string.Empty; // 包裹编号
            Nullable<DateTime> beginDate = new DateTime(2011,2,16); // 开始日期
            Nullable<DateTime> endDate = new DateTime(2011, 2, 17); // 结束日期
            string destinationCode = string.Empty; // 目的地三字码
            IList<HAWB> actual;
            actual = _HAWBManagementService.FindHAWBsOfPackageByCondition(barCode, beginDate, endDate, destinationCode);
        }
        #endregion

        #region 判断运单和包裹字码是否重复不能添加问题测试
        /// <summary>
        ///JudgeHAWBOfPackageRepeat 的测试
        ///</summary>
        [TestMethod()]
        public void JudgeHAWBOfPackageRepeatTest()
        {
            string HAWBBarcode = "2010"; // HAWB运单号
            string packageBarcode = "p3"; // 包裹号
            bool actual;
            actual = _HAWBManagementService.JudgeHAWBOfPackageRepeat(HAWBBarcode, packageBarcode);
        }
        #endregion

        #region 通过总运单号获取所有运单
        /// <summary>
        ///FindHAWBsByMID 的测试
        ///</summary>
        [TestMethod()]
        public void FindHAWBsByMIDTest()
        {
            string MID = "b99f909f-06ab-432f-94a4-c4689e850987"; // 总运单序号
            IList<HAWB> actual;
            actual = _HAWBManagementService.FindHAWBsByMID(MID);
        }
        #endregion

        public override Expression<Func<HAWB, bool>> FilterExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override Expression<Func<HAWB, int>> OrderByExpression
        {
            get { throw new NotImplementedException(); }
        }



        /// <summary>
        ///FindHAWBsByCondition 的测试
        ///</summary>
        [TestMethod()]
        public void FindHAWBsByConditionTest1()
        {
            IHAWBRepository hawbRepository = null; // TODO: 初始化为适当的值
            IHAWBItemRepository hawbItemRepository = null; // TODO: 初始化为适当的值
            IHAWBBoxRepository hawbBoxRepository = null; // TODO: 初始化为适当的值
            IUserRepository userRepository = null; // TODO: 初始化为适当的值
            HAWBManagementService target = new HAWBManagementService(hawbRepository, hawbItemRepository, hawbBoxRepository, userRepository); // TODO: 初始化为适当的值
            string barCode = string.Empty; // TODO: 初始化为适当的值
            string countryCode = string.Empty; // TODO: 初始化为适当的值
            string regionCode = string.Empty; // TODO: 初始化为适当的值
            string departmentCode = string.Empty; // TODO: 初始化为适当的值
            string companyCode = string.Empty; // TODO: 初始化为适当的值
            string carrier = string.Empty; // TODO: 初始化为适当的值
            string HAWBOperator = string.Empty; // TODO: 初始化为适当的值
            string contactor = string.Empty; // TODO: 初始化为适当的值
            Nullable<DateTime> beginTime = new Nullable<DateTime>(); // TODO: 初始化为适当的值
            Nullable<DateTime> endTime = new Nullable<DateTime>(); // TODO: 初始化为适当的值
            int settleType = 0; // TODO: 初始化为适当的值
            int serviceType = 0; // TODO: 初始化为适当的值
            Nullable<bool> isInternational = new Nullable<bool>(); // TODO: 初始化为适当的值
            int pageIndex = 0; // TODO: 初始化为适当的值
            int pageCount = 0; // TODO: 初始化为适当的值
            int totalCount = 0; // TODO: 初始化为适当的值
            int totalCountExpected = 0; // TODO: 初始化为适当的值
            IList<HAWB> expected = null; // TODO: 初始化为适当的值
            IList<HAWB> actual;
            actual = target.FindHAWBsByCondition(barCode, countryCode, regionCode, departmentCode, companyCode, carrier, HAWBOperator, contactor, beginTime, endTime, settleType, serviceType, isInternational, pageIndex, pageCount, ref totalCount);
            Assert.AreEqual(totalCountExpected, totalCount);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}
