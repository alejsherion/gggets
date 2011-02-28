//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        模板单元测试
// 作成者				ZhiWei.Shen
// 改版日				2011.02.28
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
    ///这是 TemplateManagementServiceTest 的测试类，旨在
    ///包含所有 TemplateManagementServiceTest 单元测试
    ///</summary>
    [TestClass()]
    public class TemplateManagementServiceTest : RepositoryTestsBase<Template>
    {
        static ITemplateManagementService _templateManagementService;//BLL操作类返回
        public TemplateManagementServiceTest()
        {
            IGGGETSAppUnitOfWork context = GetUnitOfWork();//上下文
            ITraceManager traceManager = GetTraceManager();//跟踪管理器
            HAWBRepository HAWBRepository = new HAWBRepository(context, traceManager);//创建DAL操作对象
            HAWBItemRepository HAWBItemRepository = new HAWBItemRepository(context, traceManager);
            HAWBBoxRepository HAWBBoxRepository = new HAWBBoxRepository(context, traceManager);
            UserRepository UserRepository = new UserRepository(context, traceManager);
            PackageRepository packageRepository = new PackageRepository(context, traceManager);
            TemplateRepository templateRepository = new TemplateRepository(context, traceManager);

            _templateManagementService = new TemplateManagementService(templateRepository);
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

        #region 新增模板
        /// <summary>
        ///AddTemplate 的测试
        ///</summary>
        [TestMethod()]
        public void AddTemplateTest()
        {
            Template template = null; // 模板信息
            template = new Template
            {
                TID=Guid.NewGuid(),//模板编号
                Name="国内外运单模板",//模板名称
                Desc="主要用于维护运单模板中各个元素的位置和布局，符合我们的运单模板打印",//模板说明
                PaperType="A4",//纸张要求
                CreateDate=DateTime.Now,//创建日期
                ModifyDate=DateTime.Now,//修改日期
                Operator="admin"//操作人账号
            };
            Param param01 = new Param
            {
                PID=Guid.NewGuid(),//参数编号
                Tag=1,//参数打印序号
                Key="条形码",//维护显示名
                Value = "select barcode from hawb where HID='#key#'",//SQL语句
                Top=31,//上边距
                Left=672,//左边距
                Height=20,//高度
                Width=100,//宽度
                //FontName = "宋体",//字体
                //FontSize=11,//字体大小
                //Alignment=1,//1-左对齐 2-居中 3-右对齐
                //Bold=0,//加粗 1-加粗 0-不加粗
                //Italic=0,//斜体 0-不 1-斜体
                //Underline=0,//下划线 0-无 1-有 
                ParamType = "BarCode",//参数类型
            };
            Param param02 = new Param
            {
                PID = Guid.NewGuid(),//参数编号
                Tag = 2,//参数打印序号
                Key = "起始地二字码",//维护显示名
                Value = "select ShipperCountry from hawb where HID='#key#'",//SQL语句
                Top = 51,//上边距
                Left = 439,//左边距
                Height = 20,//高度
                Width = 100,//宽度
                FontName = "宋体",//字体
                FontSize = 9,//字体大小
                Alignment = 1,//1-左对齐 2-居中 3-右对齐
                Bold = 0,//加粗 1-加粗 0-不加粗
                Italic = 0,//斜体 0-不 1-斜体
                Underline = 0,//下划线 0-无 1-有 
                ParamType = "Text",//参数类型
            };
            Param param03 = new Param
            {
                PID = Guid.NewGuid(),//参数编号
                Tag = 3,//参数打印序号
                Key = "目的地二字码",//维护显示名
                Value = "select DeliverCountry from hawb where HID='#key#'",//SQL语句
                Top = 76,//上边距
                Left = 438,//左边距
                Height = 20,//高度
                Width = 100,//宽度
                FontName = "宋体",//字体
                FontSize = 9,//字体大小
                Alignment = 1,//1-左对齐 2-居中 3-右对齐
                Bold = 0,//加粗 1-加粗 0-不加粗
                Italic = 0,//斜体 0-不 1-斜体
                Underline = 0,//下划线 0-无 1-有 
                ParamType = "Text",//参数类型
            };
            Param param04 = new Param
            {
                PID = Guid.NewGuid(),//参数编号
                Tag = 4,//参数打印序号
                Key = "发件人姓名",//维护显示名
                Value = "select ShipperName from hawb where HID='#key#'",//SQL语句
                Top = 97,//上边距
                Left = 78,//左边距
                Height = 20,//高度
                Width = 100,//宽度
                FontName = "宋体",//字体
                FontSize = 9,//字体大小
                Alignment = 1,//1-左对齐 2-居中 3-右对齐
                Bold = 0,//加粗 1-加粗 0-不加粗
                Italic = 0,//斜体 0-不 1-斜体
                Underline = 0,//下划线 0-无 1-有 
                ParamType = "Text",//参数类型
            };
            Param param05 = new Param
            {
                PID = Guid.NewGuid(),//参数编号
                Tag = 5,//参数打印序号
                Key = "发件人联系电话",//维护显示名
                Value = "select ShipperTel from hawb where HID='#key#'",//SQL语句
                Top = 99,//上边距
                Left = 244,//左边距
                Height = 20,//高度
                Width = 100,//宽度
                FontName = "宋体",//字体
                FontSize = 9,//字体大小
                Alignment = 1,//1-左对齐 2-居中 3-右对齐
                Bold = 0,//加粗 1-加粗 0-不加粗
                Italic = 0,//斜体 0-不 1-斜体
                Underline = 0,//下划线 0-无 1-有 
                ParamType = "Text",//参数类型
            };
            Param param06 = new Param
            {
                PID = Guid.NewGuid(),//参数编号
                Tag = 6,//参数打印序号
                Key = "发件人公司",//维护显示名
                Value = "select ShipperName from hawb where HID='#key#'",//SQL语句
                Top = 127,//上边距
                Left = 126,//左边距
                Height = 20,//高度
                Width = 100,//宽度
                FontName = "宋体",//字体
                FontSize = 9,//字体大小
                Alignment = 1,//1-左对齐 2-居中 3-右对齐
                Bold = 0,//加粗 1-加粗 0-不加粗
                Italic = 0,//斜体 0-不 1-斜体
                Underline = 0,//下划线 0-无 1-有 
                ParamType = "Text",//参数类型
            };

            template.Params.Add(param01);
            template.Params.Add(param02);
            template.Params.Add(param03);
            template.Params.Add(param04);
            template.Params.Add(param05);
            template.Params.Add(param06);
            _templateManagementService.AddTemplate(template);
            //Assert.Inconclusive("无法验证不返回值的方法。");
        }
        #endregion

        public override Expression<Func<Template, bool>> FilterExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override Expression<Func<Template, int>> OrderByExpression
        {
            get { throw new NotImplementedException(); }
        }
    }
}
