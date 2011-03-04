//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        部门单元测试
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
    ///这是 DepartmentManagementServiceTest 的测试类，旨在
    ///包含所有 DepartmentManagementServiceTest 单元测试
    ///</summary>
    [TestClass()]
    public class DepartmentManagementServiceTest : RepositoryTestsBase<Department>
    {
        private static IDepartmentManagementService _departmentManagementService;//BLL操作类返回
        private static IHAWBManagementService _HAWBManagementService;//BLL操作类返回
        private static IUserManagementService _userManagementService;
        private static ICompanyManagementService _companyManagementService;
        private static IAddressBookManagementService _addressBookManagementService;
        public DepartmentManagementServiceTest()
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

            _departmentManagementService = new DepartmentManagementService(departmentRepository, HAWBRepository,userRepository,companyRepository,addressBookRepository);
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

        #region 新增部门
        /// <summary>
        ///AddDepartment 的测试
        ///</summary>
        [TestMethod()]
        public void AddDepartmentTest()
        {
            #region 首先创建2个测试运单
            //运单
            //实力化一个虚假运单对象
            HAWB HAWB01 = new HAWB
            {
                HID = Guid.NewGuid(),
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
                ConsigneeName="李宏",
                //收件人名称或者公司
                ConsigneeCountry = "AF",
                //收件人国家
                ConsigneeRegion = "ABX",
                //收件人区号
                ConsigneeAddress = "Japan address",
                //收件人地址
                ConsigneeZipCode = "201011",
                //收件人邮编
                ConsigneeTel = "120120",
                //收件人联系电话
                DeliverContactor="李宏",
                DeliverName="李宏",
                DeliverCountry="AF",
                DeliverRegion="ABX",
                DeliverAddress="Japan address",
                DeliverZipCode="201011",
                DeliverTel="120120",
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
            HAWB HAWB02 = new HAWB
            {
                HID = Guid.NewGuid(),
                BarCode = "2011",
                //条形码
                Carrier = "吉祥航空",
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
                ShipperCountry = "AL",
                //发件人国家
                ShipperRegion = "HRE",
                //发件人区号
                ShipperAddress = "test address",
                //发件人地址
                ShipperZipCode = "200435",
                //发件人邮编
                ShipperTel = "13817011234",
                //发件人联系电话
                ConsigneeContactor = "李宏",
                //收件人姓名
                ConsigneeName="李宏",
                //收件人名称或公司
                ConsigneeCountry = "JP",
                //收件人国家
                ConsigneeRegion = "TKO",
                //收件人区号
                ConsigneeAddress = "Japan address2",
                //收件人地址
                ConsigneeZipCode = "111111",
                //收件人邮编
                ConsigneeTel = "222222",
                DeliverContactor = "李宏",
                DeliverName = "李宏",
                DeliverCountry = "JP",
                DeliverRegion = "TKO",
                DeliverAddress = "Japan address2",
                DeliverZipCode = "111111",
                DeliverTel = "222222",
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


            _HAWBManagementService.AddHAWB(HAWB01);
            _HAWBManagementService.AddHAWB(HAWB02);
            #endregion
            Department departmentA = null; // 部门A
            Department departmentB = null; // 部门B
            #region 部门
            departmentA = new Department
            {
                DID=Guid.NewGuid(),//部门序号
                DepCode="00",//部门账号
                DepName="开发部",//部门名称
                FeeDiscountRate=1,//折扣率，与旗下的用户同步
                FeeDiscountType=0,//折扣类型
                WeightDiscountRate=1,//重量折扣率
                WeightDiscountType=0,//折扣类型
                SettleType=0,//结算方式
                WeightCalType=0//计量方式
            };
            departmentB = new Department
            {
                DID = Guid.NewGuid(),//部门序号
                DepCode = "01",//部门账号
                DepName = "市场部",//部门名称
                FeeDiscountRate = 1,//折扣率，与旗下的用户同步
                FeeDiscountType = 0,//折扣类型
                WeightDiscountRate = 1,//重量折扣率
                WeightDiscountType = 0,//折扣类型
                SettleType = 0,//结算方式
                WeightCalType = 0//计量方式
            };
            #endregion
            #region 公司
            //公司实体实例化
            Company company = new Company
            {
                CID=Guid.NewGuid(),//公司序号
                CompanyCode="M18",//公司账号
                FullName = "麦考林",//公司全称
                ShortName = "麦考林",//公司简称
                PostCode="200435",//邮政编码
                Address = "上海市普陀区长寿路1118号",//地址
                Contactor="沈志伟",//联系人
                ContactorPhone="13817011234",//联系电话
                Phone="56881234",//公司电话
                Fax = "64950500-8999",//传真
                OrganizationCode="SH000001"//组织代码
            };
            #endregion 
            #region 用户
            //用户实例化
            User user01 = new User
            {
                UID=Guid.NewGuid(),//用户序号
                LoginName="U1",//用户账号
                Password="123456",//用户密码
                RealName="小伟",//用户真实姓名
                UpdateTime=DateTime.Now,//更新日期
                CreateTime=DateTime.Now,//创建日期
                Operator = "小伟",//操作人
                FeeDiscountRate=1,//费用折扣率
                FeeDiscountType=0,//费用折扣类型
                WeightDiscountRate=1,//重量折扣率
                WeightDiscountType=0,//重量折扣类型
                SettleType=0,//结算方式
                WeightCalType=0,//计重方式
                Status=0//可用与不可用
            };
            User user02 = new User
            {
                UID = Guid.NewGuid(),//用户序号
                LoginName = "U2",//用户账号
                Password = "123456",//用户密码
                RealName = "小美",//用户真实姓名
                UpdateTime = DateTime.Now,//更新日期
                CreateTime = DateTime.Now,//创建日期
                Operator = "小美",//操作人
                FeeDiscountRate = 1,//费用折扣率
                FeeDiscountType = 0,//费用折扣类型
                WeightDiscountRate = 1,//重量折扣率
                WeightDiscountType = 0,//重量折扣类型
                SettleType = 0,//结算方式
                WeightCalType = 0,//计重方式
                Status = 0//可用与不可用
            };
            #endregion
            #region 发货地址本
            //发货地址本
            AddressBook addressBookA1 = new AddressBook
            {
                AID=Guid.NewGuid(),//地址本序号
                Name = company.FullName,//公司名称
                ContactorName="沈从文",//联系人真名
                Provience="上海",//省份
                CountryCode="CN",//国家二字码
                RegionCode="SHA",//地区三字码
                Address = "上海市田林路487号宝石园大厦20号楼22层，近莲花路",//地址
                PostCode="200435",//邮政编码
                AddressType=0,//地址类型-发件人地址
                CreateTime=DateTime.Now,//创建日期
                UpdateTime=DateTime.Now,//修改日期
                Operator="沈志伟",//操作人姓名
                Phone="56881111"
            };
            AddressBook addressBookA2 = new AddressBook
            {
                AID = Guid.NewGuid(),//地址本序号
                Name = company.FullName,//公司名称
                ContactorName = "沈从文",//联系人真名
                Provience = "上海",//省份
                CountryCode = "CN",//国家二字码
                RegionCode = "SHA",//地区三字码
                Address = "上海市松江区车墩镇虬长路63号",//地址
                PostCode = "200123",//邮政编码
                AddressType = 0,//地址类型-发件人地址
                CreateTime = DateTime.Now,//创建日期
                UpdateTime = DateTime.Now,//修改日期
                Operator = "沈志伟",//操作人姓名
                Phone = "56882222"
            };
            AddressBook addressBookA3 = new AddressBook
            {
                AID = Guid.NewGuid(),//地址本序号
                Name = company.FullName,//公司名称
                ContactorName = "沈从文",//联系人真名
                Provience = "上海",//省份
                CountryCode = "CN",//国家二字码
                RegionCode = "SHA",//地区三字码
                Address = "上海市浦东新区银城路139号(近浦东南路)",//地址
                PostCode = "200132",//邮政编码
                AddressType = 0,//地址类型-发件人地址
                CreateTime = DateTime.Now,//创建日期
                UpdateTime = DateTime.Now,//修改日期
                Operator = "沈志伟",//操作人姓名
                Phone = "56883333"
            };
            AddressBook addressBookB1 = new AddressBook
            {
                AID = Guid.NewGuid(),//地址本序号
                Name = company.FullName,//公司名称
                ContactorName = "沈杰",//联系人真名
                Provience = "上海",//省份
                CountryCode = "CN",//国家二字码
                RegionCode = "SHA",//地区三字码
                Address = "上海市银城中路488号",//地址
                PostCode = "200435",//邮政编码
                AddressType = 0,//地址类型-发件人地址
                CreateTime = DateTime.Now,//创建日期
                UpdateTime = DateTime.Now,//修改日期
                Operator = "沈志伟",//操作人姓名
                Phone = "56884444"
            };
            AddressBook addressBookB2 = new AddressBook
            {
                AID = Guid.NewGuid(),//地址本序号
                Name = company.FullName,//公司名称
                ContactorName = "沈杰",//联系人真名
                Provience = "上海",//省份
                CountryCode = "CN",//国家二字码
                RegionCode = "SHA",//地区三字码
                Address = "上海市峨山路91弄98号",//地址
                PostCode = "200435",//邮政编码
                AddressType = 0,//地址类型-发件人地址
                CreateTime = DateTime.Now,//创建日期
                UpdateTime = DateTime.Now,//修改日期
                Operator = "沈志伟",//操作人姓名
                Phone = "56885555"
            };
            AddressBook addressBookB3 = new AddressBook
            {
                AID = Guid.NewGuid(),//地址本序号
                Name = company.FullName,//公司名称
                ContactorName = "沈杰",//联系人真名
                Provience = "上海",//省份
                CountryCode = "CN",//国家二字码
                RegionCode = "SHA",//地区三字码
                Address = "上海市杨浦区长阳路2467号铭大创意园内5楼",//地址
                PostCode = "200435",//邮政编码
                AddressType = 0,//地址类型-发件人地址
                CreateTime = DateTime.Now,//创建日期
                UpdateTime = DateTime.Now,//修改日期
                Operator = "沈志伟",//操作人姓名
                Phone = "56886666"
            };
            #endregion
            #region 收货人地址本
            //收货人地址本
            AddressBook addressBookA4 = new AddressBook
            {
                AID = Guid.NewGuid(),//地址本序号
                Name = "日本株式会社",//公司名称
                ContactorName = "土肥原贤二",//联系人真名
                Provience = "日本",//省份
                CountryCode = "JP",//国家二字码
                RegionCode = "HIJ",//地区三字码
                Address = "日本爱知县名古屋市西区幅下2－3－2 sunlife(サンライフ)城南5B",//地址
                PostCode = "200412",//邮政编码
                AddressType = 1,//地址类型-收件人地址
                CreateTime = DateTime.Now,//创建日期
                UpdateTime = DateTime.Now,//修改日期
                Operator = "沈志伟",//操作人姓名
                Phone = "888888"
            };
            AddressBook addressBookA5 = new AddressBook
            {
                AID = Guid.NewGuid(),//地址本序号
                Name = "日本株式会社",//公司名称
                ContactorName = "土肥原贤二",//联系人真名
                Provience = "日本",//省份
                CountryCode = "JP",//国家二字码
                RegionCode = "KOB",//地区三字码
                Address = "日本埼玉市太田窪",//地址
                PostCode = "200413",//邮政编码
                AddressType = 1,//地址类型-收件人地址
                CreateTime = DateTime.Now,//创建日期
                UpdateTime = DateTime.Now,//修改日期
                Operator = "沈志伟",//操作人姓名
                Phone = "888889"
            };
            AddressBook addressBookA6 = new AddressBook
            {
                AID = Guid.NewGuid(),//地址本序号
                Name = "日本株式会社",//公司名称
                ContactorName = "土肥原贤二",//联系人真名
                Provience = "日本",//省份
                CountryCode = "JP",//国家二字码
                RegionCode = "KYO",//地区三字码
                Address = "日本茨城県那珂市北酒出２００",//地址
                PostCode = "200414",//邮政编码
                AddressType = 1,//地址类型-收件人地址
                CreateTime = DateTime.Now,//创建日期
                UpdateTime = DateTime.Now,//修改日期
                Operator = "沈志伟",//操作人姓名
                Phone = "888880"
            };
            AddressBook addressBookB4 = new AddressBook
            {
                AID = Guid.NewGuid(),//地址本序号
                Name = "索尼",//公司名称
                ContactorName = "三池崇史",//联系人真名
                Provience = "日本",//省份
                CountryCode = "JP",//国家二字码
                RegionCode = "NGS",//地区三字码
                Address = "日本崎玉県入间郡毛吕山町大字毛吕本郷223番地1",//地址
                PostCode = "200412",//邮政编码
                AddressType = 1,//地址类型-收件人地址
                CreateTime = DateTime.Now,//创建日期
                UpdateTime = DateTime.Now,//修改日期
                Operator = "沈志伟",//操作人姓名
                Phone = "888887"
            };
            AddressBook addressBookB5 = new AddressBook
            {
                AID = Guid.NewGuid(),//地址本序号
                Name = "索尼",//公司名称
                ContactorName = "三池崇史",//联系人真名
                Provience = "日本",//省份
                CountryCode = "JP",//国家二字码
                RegionCode = "YOH",//地区三字码
                Address = "日本横须贺888番地2",//地址
                PostCode = "200413",//邮政编码
                AddressType = 1,//地址类型-收件人地址
                CreateTime = DateTime.Now,//创建日期
                UpdateTime = DateTime.Now,//修改日期
                Operator = "沈志伟",//操作人姓名
                Phone = "888886"
            };
            AddressBook addressBookB6 = new AddressBook
            {
                AID = Guid.NewGuid(),//地址本序号
                Name = "索尼",//公司名称
                ContactorName = "三池崇史",//联系人真名
                Provience = "日本",//省份
                CountryCode = "JP",//国家二字码
                RegionCode = "JXX",//地区三字码
                Address = "日本北海道神宫520番地4",//地址
                PostCode = "200413",//邮政编码
                AddressType = 1,//地址类型-收件人地址
                CreateTime = DateTime.Now,//创建日期
                UpdateTime = DateTime.Now,//修改日期
                Operator = "沈志伟",
                Phone = "888884"
            };
            #endregion
            #region 交付人地址本
            //交付人地址本
             AddressBook addressBookA7 = new AddressBook
            {
                AID = Guid.NewGuid(),//地址本序号
                ReceiveAID = addressBookA4.AID,//收件人地址ID
                Name = "KONAMA",//公司名称
                ContactorName = "本多忠胜",//联系人真名
                Provience = "日本",//省份
                CountryCode = "SH",//国家二字码
                RegionCode = "JXO",//地区三字码
                Address = "日本海中公园  24米海底观览隧道",//地址
                PostCode = "100435",//邮政编码
                AddressType = 2,//地址类型-交付人地址
                CreateTime = DateTime.Now,//创建日期
                UpdateTime = DateTime.Now,//修改日期
                Operator = "沈志伟",//操作人姓名
                Phone = "123456"
            };
             AddressBook addressBookA8 = new AddressBook
             {
                 AID = Guid.NewGuid(),//地址本序号
                 ReceiveAID = addressBookA5.AID,//收件人地址ID
                 Name = "KONAMA",//公司名称
                 ContactorName = "本多忠胜",//联系人真名
                 Provience = "日本",//省份
                 CountryCode = "SH",//国家二字码
                 RegionCode = "NRT",//地区三字码
                 Address = "日本名古屋",//地址
                 PostCode = "100439",//邮政编码
                 AddressType = 2,//地址类型-交付人地址
                 CreateTime = DateTime.Now,//创建日期
                 UpdateTime = DateTime.Now,//修改日期
                 Operator = "沈志伟",//操作人姓名
                 Phone = "123458"
             };
             AddressBook addressBookA9 = new AddressBook
             {
                 AID = Guid.NewGuid(),//地址本序号
                 ReceiveAID = addressBookA5.AID,//收件人地址ID
                 Name = "KONAMA",//公司名称
                 ContactorName = "本多忠胜",//联系人真名
                 Provience = "日本",//省份
                 CountryCode = "SH",//国家二字码
                 RegionCode = "OSA",//地区三字码
                 Address = "日本爱知县中国事务所",//地址
                 PostCode = "100445",//邮政编码
                 AddressType = 2,//地址类型-交付人地址
                 CreateTime = DateTime.Now,//创建日期
                 UpdateTime = DateTime.Now,//修改日期
                 Operator = "沈志伟",//操作人姓名
                 Phone = "123459"
             };
             AddressBook addressBookB7 = new AddressBook
             {
                 AID = Guid.NewGuid(),//地址本序号
                 ReceiveAID = addressBookA5.AID,//收件人地址ID
                 Name = "KONAMA",//公司名称
                 ContactorName = "明智光秀",//联系人真名
                 Provience = "日本",//省份
                 CountryCode = "SH",//国家二字码
                 RegionCode = "OSA",//地区三字码
                 Address = "日本白木屋遵义路10号",//地址
                 PostCode = "100444",//邮政编码
                 AddressType = 2,//地址类型-交付人地址
                 CreateTime = DateTime.Now,//创建日期
                 UpdateTime = DateTime.Now,//修改日期
                 Operator = "沈志伟",//操作人姓名
                 Phone = "123450"
             };
             AddressBook addressBookB8 = new AddressBook
             {
                 AID = Guid.NewGuid(),//地址本序号
                 ReceiveAID = addressBookA5.AID,//收件人地址ID
                 Name = "KONAMA",//公司名称
                 ContactorName = "明智光秀",//联系人真名
                 Provience = "日本",//省份
                 CountryCode = "SH",//国家二字码
                 RegionCode = "KOB",//地区三字码
                 Address = "日本九州大学",//地址
                 PostCode = "100333",//邮政编码
                 AddressType = 2,//地址类型-交付人地址
                 CreateTime = DateTime.Now,//创建日期
                 UpdateTime = DateTime.Now,//修改日期
                 Operator = "沈志伟",//操作人姓名
                 Phone = "123452"
             };
             AddressBook addressBookB9 = new AddressBook
             {
                 AID = Guid.NewGuid(),//地址本序号
                 ReceiveAID = addressBookA5.AID,//收件人地址ID
                 Name = "KONAMA",//公司名称
                 ContactorName = "明智光秀",//联系人真名
                 Provience = "日本",//省份
                 CountryCode = "SH",//国家二字码
                 RegionCode = "KOB",//地区三字码
                 Address = "日本西岗区不老街231号",//地址
                 PostCode = "100378",//邮政编码
                 AddressType = 2,//地址类型-交付人地址
                 CreateTime = DateTime.Now,//创建日期
                 UpdateTime = DateTime.Now,//修改日期
                 Operator = "沈志伟",//操作人姓名
                 Phone = "123451"
             };
            #endregion
            #region 新增外国地址本
             AddressBook addressBookA10 = new AddressBook
             {
                 AID = Guid.NewGuid(),//地址本序号
                 Name = "Microsoft",//公司名称
                 ContactorName = "John",//联系人真名
                 Provience = "AUSTRALIA",//省份
                 CountryCode = "AU",//国家二字码
                 RegionCode = "CBR",//地区三字码
                 Address = "46 DADASKAYA STREET RYAZAN CITY RUSSIA",//地址
                 PostCode = "00001",//邮政编码
                 AddressType = 0,//地址类型-发件人地址
                 CreateTime = DateTime.Now,//创建日期
                 UpdateTime = DateTime.Now,//修改日期
                 Operator = "沈志伟",//操作人姓名
                 Phone = "118812"
             };
             AddressBook addressBookA11 = new AddressBook
             {
                 AID = Guid.NewGuid(),//地址本序号
                 Name = "Microsoft",//公司名称
                 ContactorName = "John",//联系人真名
                 Provience = "AUSTRALIA",//省份
                 CountryCode = "AU",//国家二字码
                 RegionCode = "CBR",//地区三字码
                 Address = "Room 4-201，Yongshengxiaoqu,XX Road, Putuo District,Shanghai,PRC",//地址
                 PostCode = "00002",//邮政编码
                 AddressType = 0,//地址类型-发件人地址
                 CreateTime = DateTime.Now,//创建日期
                 UpdateTime = DateTime.Now,//修改日期
                 Operator = "沈志伟",//操作人姓名
                 Phone = "118823"
             };
             AddressBook addressBookA12 = new AddressBook
             {
                 AID = Guid.NewGuid(),//地址本序号
                 Name = "Microsoft",//公司名称
                 ContactorName = "John",//联系人真名
                 Provience = "AUSTRALIA",//省份
                 CountryCode = "AU",//国家二字码
                 RegionCode = "CBR",//地区三字码
                 Address = "Room 403,No.37,ShiFan Residential Quarter,FengTai District",//地址
                 PostCode = "00003",//邮政编码
                 AddressType = 0,//地址类型-发件人地址
                 CreateTime = DateTime.Now,//创建日期
                 UpdateTime = DateTime.Now,//修改日期
                 Operator = "沈志伟",//操作人姓名
                 Phone = "118827"
             };
             AddressBook addressBookB10 = new AddressBook
             {
                 AID = Guid.NewGuid(),//地址本序号
                 Name = "Google",//公司名称
                 ContactorName = "Peter",//联系人真名
                 Provience = "BRAZIL",//省份
                 CountryCode = "BR",//国家二字码
                 RegionCode = "REC",//地区三字码
                 Address = "BMW Kundenbetreuung.Wenden Sie sich bei Fragen aller Art an unsere Kundenbetreuung.",//地址
                 PostCode = "00004",//邮政编码
                 AddressType = 0,//地址类型-发件人地址
                 CreateTime = DateTime.Now,//创建日期
                 UpdateTime = DateTime.Now,//修改日期
                 Operator = "沈志伟",//操作人姓名
                 Phone = "118829"
             };
             AddressBook addressBookB11 = new AddressBook
             {
                 AID = Guid.NewGuid(),//地址本序号
                 Name = "Google",//公司名称
                 ContactorName = "Peter",//联系人真名
                 Provience = "BRAZIL",//省份
                 CountryCode = "BR",//国家二字码
                 RegionCode = "REC",//地区三字码
                 Address = "3900 west century boulevard inglewood",//地址
                 PostCode = "00005",//邮政编码
                 AddressType = 0,//地址类型-发件人地址
                 CreateTime = DateTime.Now,//创建日期
                 UpdateTime = DateTime.Now,//修改日期
                 Operator = "沈志伟",//操作人姓名
                 Phone = "118833"
             };
             AddressBook addressBookB12 = new AddressBook
             {
                 AID = Guid.NewGuid(),//地址本序号
                 Name = "Google",//公司名称
                 ContactorName = "Peter",//联系人真名
                 Provience = "BRAZIL",//省份
                 CountryCode = "BR",//国家二字码
                 RegionCode = "REC",//地区三字码
                 Address = "mONTEREY PARK500 N. GAFIELD AVE",//地址
                 PostCode = "00006",//邮政编码
                 AddressType = 0,//地址类型-发件人地址
                 CreateTime = DateTime.Now,//创建日期
                 UpdateTime = DateTime.Now,//修改日期
                 Operator = "沈志伟",//操作人姓名
                 Phone = "118838"
             };
            #endregion
            #region 新增外国地址本
             AddressBook addressBookA13 = new AddressBook
             {
                 AID = Guid.NewGuid(),//地址本序号
                 Name = "Baidu",//公司名称
                 ContactorName = "Jack",//联系人真名
                 Provience = "CANADA",//省份
                 CountryCode = "CA",//国家二字码
                 RegionCode = "YQR",//地区三字码
                 Address = "SAN FRANCISCO 806 MONTGOMERY STREET",//地址
                 PostCode = "02001",//邮政编码
                 AddressType = 1,//地址类型-发件人地址
                 CreateTime = DateTime.Now,//创建日期
                 UpdateTime = DateTime.Now,//修改日期
                 Operator = "沈志伟",//操作人姓名
                 Phone = "205060"
             };

             AddressBook addressBookA14 = new AddressBook
             {
                 AID = Guid.NewGuid(),//地址本序号
                 Name = "Baidu",//公司名称
                 ContactorName = "Jack",//联系人真名
                 Provience = "CANADA",//省份
                 CountryCode = "CA",//国家二字码
                 RegionCode = "YQR",//地区三字码
                 Address = "Branson Rd. Kansas City",//地址
                 PostCode = "02003",//邮政编码
                 AddressType = 1,//地址类型-发件人地址
                 CreateTime = DateTime.Now,//创建日期
                 UpdateTime = DateTime.Now,//修改日期
                 Operator = "沈志伟",//操作人姓名
                 Phone = "205062"
             };

             AddressBook addressBookA15 = new AddressBook
             {
                 AID = Guid.NewGuid(),//地址本序号
                 Name = "Baidu",//公司名称
                 ContactorName = "Jack",//联系人真名
                 Provience = "CANADA",//省份
                 CountryCode = "CA",//国家二字码
                 RegionCode = "YQR",//地区三字码
                 Address = "TRANS SECTION GRIDLINES 1014E,1/F.ALT LOGISTIC CENTRE B,BERTH3 KAWAI",//地址
                 PostCode = "02002",//邮政编码
                 AddressType = 1,//地址类型-发件人地址
                 CreateTime = DateTime.Now,//创建日期
                 UpdateTime = DateTime.Now,//修改日期
                 Operator = "沈志伟",//操作人姓名
                 Phone = "205061"
             };

             AddressBook addressBookB13 = new AddressBook
             {
                 AID = Guid.NewGuid(),//地址本序号
                 Name = "MOTO",//公司名称
                 ContactorName = "Ken",//联系人真名
                 Provience = "CZECH REPUBLIC",//省份
                 CountryCode = "CZ",//国家二字码
                 RegionCode = "OSR",//地区三字码
                 Address = "Sherwood St. Boston, MA",//地址
                 PostCode = "02003",//邮政编码
                 AddressType = 1,//地址类型-发件人地址
                 CreateTime = DateTime.Now,//创建日期
                 UpdateTime = DateTime.Now,//修改日期
                 Operator = "沈志伟",//操作人姓名
                 Phone = "205081"
             };

             AddressBook addressBookB14 = new AddressBook
             {
                 AID = Guid.NewGuid(),//地址本序号
                 Name = "MOTO",//公司名称
                 ContactorName = "Ken",//联系人真名
                 Provience = "CZECH REPUBLIC",//省份
                 CountryCode = "CZ",//国家二字码
                 RegionCode = "OSR",//地区三字码
                 Address = "Lindley Ave. Juneau, AK",//地址
                 PostCode = "02303",//邮政编码
                 AddressType = 1,//地址类型-发件人地址
                 CreateTime = DateTime.Now,//创建日期
                 UpdateTime = DateTime.Now,//修改日期
                 Operator = "沈志伟",//操作人姓名
                 Phone = "205091"
             };

             AddressBook addressBookB15 = new AddressBook
             {
                 AID = Guid.NewGuid(),//地址本序号
                 Name = "MOTO",//公司名称
                 ContactorName = "Ken",//联系人真名
                 Provience = "CZECH REPUBLIC",//省份
                 CountryCode = "CZ",//国家二字码
                 RegionCode = "OSR",//地区三字码
                 Address = "leavesden studios po box 3000 leavesden, herts, wd25 7lt",//地址
                 PostCode = "02353",//邮政编码
                 AddressType = 1,//地址类型-发件人地址
                 CreateTime = DateTime.Now,//创建日期
                 UpdateTime = DateTime.Now,//修改日期
                 Operator = "沈志伟",//操作人姓名
                 Phone = "785091"
             };
            #endregion
            #region 新增外国地址本
             AddressBook addressBookA16 = new AddressBook
             {
                 AID = Guid.NewGuid(),//地址本序号
                 ReceiveAID=addressBookA13.AID,
                 Name = "Nokia",//公司名称
                 ContactorName = "Josn",//联系人真名
                 Provience = "GREENLAND",//省份
                 CountryCode = "GL",//国家二字码
                 RegionCode = "GOH",//地区三字码
                 Address = "82 MARCHINGTON CIRCLE,SCARBOROUGH,ONM1R 3M7",//地址
                 PostCode = "02031",//邮政编码
                 AddressType = 2,//地址类型-发件人地址
                 CreateTime = DateTime.Now,//创建日期
                 UpdateTime = DateTime.Now,//修改日期
                 Operator = "沈志伟",//操作人姓名
                 Phone = "206460"
             };

             AddressBook addressBookA17 = new AddressBook
             {
                 AID = Guid.NewGuid(),//地址本序号
                 ReceiveAID = addressBookA14.AID,
                 Name = "Nokia",//公司名称
                 ContactorName = "Josn",//联系人真名
                 Provience = "GREENLAND",//省份
                 CountryCode = "GL",//国家二字码
                 RegionCode = "GOH",//地区三字码
                 Address = "xinlung education,122 marylebone high street,london, w1u 5qx",//地址
                 PostCode = "02034",//邮政编码
                 AddressType = 2,//地址类型-发件人地址
                 CreateTime = DateTime.Now,//创建日期
                 UpdateTime = DateTime.Now,//修改日期
                 Operator = "沈志伟",//操作人姓名
                 Phone = "206464"
             };

             AddressBook addressBookA18 = new AddressBook
             {
                 AID = Guid.NewGuid(),//地址本序号
                 ReceiveAID = addressBookA15.AID,
                 Name = "Nokia",//公司名称
                 ContactorName = "Josn",//联系人真名
                 Provience = "GREENLAND",//省份
                 CountryCode = "GL",//国家二字码
                 RegionCode = "GOH",//地区三字码
                 Address = "xinlung education,sino business uk ltd.cedar house, 8 fairfield st.",//地址
                 PostCode = "02039",//邮政编码
                 AddressType = 2,//地址类型-发件人地址
                 CreateTime = DateTime.Now,//创建日期
                 UpdateTime = DateTime.Now,//修改日期
                 Operator = "沈志伟",//操作人姓名
                 Phone = "206469"
             };

             AddressBook addressBookB16 = new AddressBook
             {
                 AID = Guid.NewGuid(),//地址本序号
                 ReceiveAID = addressBookB13.AID,
                 Name = "HTC",//公司名称
                 ContactorName = "Mary",//联系人真名
                 Provience = "HONDURAS",//省份
                 CountryCode = "HN",//国家二字码
                 RegionCode = "SAP",//地区三字码
                 Address = "1919B-4th Street S.W.,Box 227,Calgary, AlbertaCanadaT2S 1W4",//地址
                 PostCode = "04039",//邮政编码
                 AddressType = 2,//地址类型-发件人地址
                 CreateTime = DateTime.Now,//创建日期
                 UpdateTime = DateTime.Now,//修改日期
                 Operator = "沈志伟",//操作人姓名
                 Phone = "888469"
             };

             AddressBook addressBookB17 = new AddressBook
             {
                 AID = Guid.NewGuid(),//地址本序号
                 ReceiveAID = addressBookB14.AID,
                 Name = "HTC",//公司名称
                 ContactorName = "Mary",//联系人真名
                 Provience = "HONDURAS",//省份
                 CountryCode = "HN",//国家二字码
                 RegionCode = "SAP",//地区三字码
                 Address = "Alberta’Edmonton，Alberta Canada T6G 2E6",//地址
                 PostCode = "04039",//邮政编码
                 AddressType = 2,//地址类型-发件人地址
                 CreateTime = DateTime.Now,//创建日期
                 UpdateTime = DateTime.Now,//修改日期
                 Operator = "沈志伟",//操作人姓名
                 Phone = "888469"
             };

             AddressBook addressBookB18 = new AddressBook
             {
                 AID = Guid.NewGuid(),//地址本序号
                 ReceiveAID = addressBookB15.AID,
                 Name = "HTC",//公司名称
                 ContactorName = "Mary",//联系人真名
                 Provience = "HONDURAS",//省份
                 CountryCode = "HN",//国家二字码
                 RegionCode = "SAP",//地区三字码
                 Address = "900 Broadway SeattIe University SeattIe, WA98122",//地址
                 PostCode = "04039",//邮政编码
                 AddressType = 2,//地址类型-发件人地址
                 CreateTime = DateTime.Now,//创建日期
                 UpdateTime = DateTime.Now,//修改日期
                 Operator = "沈志伟",//操作人姓名
                 Phone = "888469"
             };
            #endregion

             //relation
            departmentA.HAWBs.Add(_HAWBManagementService.FindHAWBByBarCode("2010"));//运单关联
            departmentA.CompanyCode = company.CompanyCode.ToString();//冗余数据
            departmentA.Company = company;//公司关联
            departmentA.Users.Add(user01);//用户关联
            departmentA.AddressBooks.Add(addressBookA1);//地址本关联
            departmentA.AddressBooks.Add(addressBookA2);
            departmentA.AddressBooks.Add(addressBookA3);
            departmentA.AddressBooks.Add(addressBookA4);
            departmentA.AddressBooks.Add(addressBookA5);
            departmentA.AddressBooks.Add(addressBookA6);
            departmentA.AddressBooks.Add(addressBookA7);
            departmentA.AddressBooks.Add(addressBookA8);
            departmentA.AddressBooks.Add(addressBookA9);
            departmentA.AddressBooks.Add(addressBookA10);
            departmentA.AddressBooks.Add(addressBookA11);
            departmentA.AddressBooks.Add(addressBookA12);
            departmentA.AddressBooks.Add(addressBookA13);
            departmentA.AddressBooks.Add(addressBookA14);
            departmentA.AddressBooks.Add(addressBookA15);
            departmentA.AddressBooks.Add(addressBookA16);
            departmentA.AddressBooks.Add(addressBookA17);
            departmentA.AddressBooks.Add(addressBookA18);

            departmentB.HAWBs.Add(_HAWBManagementService.FindHAWBByBarCode("2011"));//运单关联
            departmentB.CompanyCode = company.CompanyCode.ToString();//冗余数据
            departmentB.Company = company;//公司关联
            departmentB.Users.Add(user02);//用户关联
            departmentB.AddressBooks.Add(addressBookB1);//地址本关联
            departmentB.AddressBooks.Add(addressBookB2);
            departmentB.AddressBooks.Add(addressBookB3);
            departmentB.AddressBooks.Add(addressBookB4);
            departmentB.AddressBooks.Add(addressBookB5);
            departmentB.AddressBooks.Add(addressBookB6);
            departmentB.AddressBooks.Add(addressBookB7);
            departmentB.AddressBooks.Add(addressBookB8);
            departmentB.AddressBooks.Add(addressBookB9);
            departmentB.AddressBooks.Add(addressBookB10);
            departmentB.AddressBooks.Add(addressBookB11);
            departmentB.AddressBooks.Add(addressBookB12);
            departmentB.AddressBooks.Add(addressBookB13);
            departmentB.AddressBooks.Add(addressBookB14);
            departmentB.AddressBooks.Add(addressBookB15);
            departmentB.AddressBooks.Add(addressBookB16);
            departmentB.AddressBooks.Add(addressBookB17);
            departmentB.AddressBooks.Add(addressBookB18);

            _departmentManagementService.AddDepartment(departmentA);
            _departmentManagementService.AddDepartment(departmentB);
            //_departmentManagementService.AddDepartment(department);
            //Assert.Inconclusive("无法验证不返回值的方法。");
        }
        #endregion

        #region 修改部门
        /// <summary>
        ///ModifyDepartment 的测试
        ///</summary>
        [TestMethod()]
        public void ModifyDepartmentTest()
        {
            Department department = null; // 获取当前数据库现有部门
            department = _departmentManagementService.FindDepartmentByDepCodeAndCompanyCode("D1","M18");
            #region 新增移除混合测试
            //首先删除一个运单，在增加另外一个运单
            HAWB oldHAWB = _HAWBManagementService.FindHAWBByBarCode("2010");
            HAWB newHAWB = _HAWBManagementService.FindHAWBByBarCode("2011");
            department.HAWBs.Remove(oldHAWB);
            department.HAWBs.Add(newHAWB);
            //在删除一个用户
            User oleUser = _userManagementService.FindUserByLoginName("U2");
            department.Users.Remove(oleUser);
            //删除一个地址本
            AddressBook oldAddressBook = _addressBookManagementService.FindAddressBookByAID("af7aec5e-2323-4649-88da-e1bd6a9dd177");
            department.AddressBooks.Remove(oldAddressBook);

            _departmentManagementService.ModifyDepartment(department);
            #endregion
        }
        #endregion
        
        #region 通过部门账号和公司账号获取部门
        /// <summary>
        ///FindDepartmentByDepCodeAndCompanyCodeTest 的测试
        ///</summary>
        [TestMethod()]
        public void FindDepartmentByDepCodeAndCompanyCodeTest()
        {
            string depCode = "00"; // 部门账号
            string companyCode = "M18"; // 公司账号
            Department actual;
            actual = _departmentManagementService.FindDepartmentByDepCodeAndCompanyCode(depCode, companyCode);
        }
        #endregion

        #region 通过公司账号获取部门信息测试
        /// <summary>
        ///FindDepartmentsByCompanyCode 的测试
        ///</summary>
        [TestMethod()]
        public void FindDepartmentsByCompanyCodeTest()
        {
            string companyCode = "M18"; // 公司账号
            IList<Department> actual;
            actual = _departmentManagementService.FindDepartmentsByCompanyCode(companyCode);
        }
        #endregion

        #region 通过部门账号获取发件人地址本
        /// <summary>
        ///FindAllShipAddressesByDepCode 的测试
        ///</summary>
        [TestMethod()]
        public void FindAllShipAddressesByDepCodeTest()
        {
            string depCode = "00"; // 部门账号
            string companyCode = "M18";
            IList<AddressBook> actual;
            actual = _departmentManagementService.FindAllShipAddressesByDepCodeAndCompanyCode(depCode, companyCode);
        }
        #endregion

        #region 通过部门账号获取收件人地址本
        /// <summary>
        ///FindAllShipAddressesByDepCode 的测试
        ///</summary>
        [TestMethod()]
        public void FindAllDeliveryAddressesByDepCodeTest()
        {
            string depCode = "01"; // 部门账号
            string companyCode = "M18";
            IList<AddressBook> actual;
            actual = _departmentManagementService.FindAllDeliveryAddressesByDepCodeAndCompanyCode(depCode, companyCode);
        }
        #endregion

        #region 通过部门账号获取交付人地址本
        /// <summary>
        ///FindAllShipAddressesByDepCode 的测试
        ///</summary>
        [TestMethod()]
        public void FindAllForwarderAddressesByDepCodeTest()
        {
            string depCode = "00"; // 部门账号
            string companyCode = "M18";
            IList<AddressBook> actual;
            actual = _departmentManagementService.FindAllForwarderAddressesByDepCodeAndCompanyCode(depCode, companyCode);
        }
        #endregion

        #region 判断地址本重复问题 返回ture-是重复；false-不重复
        /// <summary>
        ///JudgeAddressBookWhetherRepeat 的测试
        ///</summary>
        [TestMethod()]
        public void JudgeAddressBookWhetherRepeatTest()
        {
            string AID = "c976f88c-565f-41e4-bca9-1d80c06291b9"; // 地址本序号
            string contactorName = "明智光秀"; // 联系人姓名
            bool expected = true; 
            bool actual;
            actual = _departmentManagementService.JudgeAddressBookWhetherRepeat(AID, contactorName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
        #endregion

        #region 地址本判断重复
        /// <summary>
        ///JudgeRepeat 的测试
        ///</summary>
        [TestMethod()]
        public void JudgeRepeatTest()
        {
            AddressBook addressBook = _addressBookManagementService.FindAddressBookByAID("7a50fd64-01e1-4b10-b9cd-03f21913ff18");
            string ContactorName = "Peter";
            string Address = "3900 west century boulevard inglewood";
            string CountryCode = "BR";
            string Provience = "BRAZIL";
            string RegionCode = "REC";
            string PostCode = "00005";
            string Phone = "118833";
            string Name = "Google";
            bool actual;
            actual = _departmentManagementService.JudgeRepeat("7a50fd64-01e1-4b10-b9cd-03f21913ff18", Name, Address,
                                                              CountryCode, Provience, RegionCode, PostCode,
                                                              ContactorName, Phone);
        }
        #endregion

        #region 部门多条件查询
        /// <summary>
        ///FindDepartmentsByCondition 的测试
        ///</summary>
        [TestMethod()]
        public void FindDepartmentsByConditionTest()
        {
            string companyCode = "M18"; // 公司账号
            string depCode = string.Empty; // 部门账号
            string depName = string.Empty; // 部门名称
            IList<Department> actual;
            actual = _departmentManagementService.FindDepartmentsByCondition(companyCode, depCode, depName);
        }
        #endregion

        public override Expression<Func<Department, bool>> FilterExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override Expression<Func<Department, int>> OrderByExpression
        {
            get { throw new NotImplementedException(); }
        }
    }
}
