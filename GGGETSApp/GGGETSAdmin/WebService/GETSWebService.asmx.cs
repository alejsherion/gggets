//************************************************************************
// 用户名				海安不良资产管理系统
// 系统名				管理后台
// 子系统名		        WEB服务
// 作成者				ZhiWei.Shen
// 改版日				2011.05.31
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using Application.GGETS;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Domain.Core;
using ETS.GGGETSApp.Domain.Core.Entities;
using ETS.GGGETSApp.Infrastructure.CrossCutting.IoC;
using ETS.GGGETSApp.Infrastructure.CrossCutting.Logging;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.Repositories;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.UnitOfWork;
using GGGETSAdmin.Common;

namespace GGGETSAdmin.WebService
{
    /// <summary>
    /// GETSWebService 的摘要说明
    /// </summary>
    
    //由 SoapHeader 扩展而来的 AuthHeader类   
    public class AuthHeaderCS : SoapHeader
    {
        public string Username;
        public string Password;
    }

    [WebService(Namespace = "http://tempuri.org/", Description = "组织机构之间的关联webservice")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class GETSWebService : System.Web.Services.WebService
    {
        #region Private Block

        private IHAWBRepository _hawbRepository;
        private IPackageRepository _packageRepository;
        private IMAWBRepository _mawbRepository;

        /// <summary>
        /// 验证用户合法性
        /// </summary>
        /// <param name="usr">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>true-验证成功；false-验证失败</returns>
        private bool AuthenticateUser(string usr, string pwd)
        {
            if ((usr.Equals("HELLOKELLY")) && (pwd.Equals("123456")))
            {
                return true;
            }
            return false;
        }   
        #endregion

        #region Public Block

        public AuthHeaderCS sHeader;
        #endregion

        #region Constructor

        public GETSWebService()
        {
            IGGGETSAppUnitOfWork context = GetUnitOfWork();//上下文
            ITraceManager traceManager = GetTraceManager();//跟踪管理器
            _hawbRepository = new HAWBRepository(context, traceManager);//创建DAL操作对象
            _packageRepository = new PackageRepository(context, traceManager);
            _mawbRepository = new MAWBRepository(context, traceManager);
        }
        #endregion

        #region ExecuteDB

        /// <summary>
        /// Get Active UnitOfWork for testing
        /// </summary>
        /// <param name="initializeContainer">True if unit of work is initialized</param>
        /// <returns>Active unit of work for testing</returns>
        public IGGGETSAppUnitOfWork GetUnitOfWork(bool initializeContainer = true)
        {
            // Get unit of work specified in unity configuration
            // Set active unit of work for 
            // testing with fake or real unit of work in application configuration file 
            // "defaultIoCContainer" setting

            return IoCFactory.Instance.CurrentContainer.Resolve<IGGGETSAppUnitOfWork>();
        }
        public ITraceManager GetTraceManager()
        {
            //Get configured trace manager

            return IoCFactory.Instance.CurrentContainer.Resolve<ITraceManager>();
        }
        #endregion

        #region WS Methods

        [WebMethod(Description = "添加运单")]
        public string AddHAWB(string jsonHAWB)
        {
            if (string.IsNullOrEmpty(jsonHAWB))
                return "ERROR:   运单不能为空!";
            //if (sHeader == null)
            //    return "ERROR:   您的验证失败，无法进行访问Web服务!";
            //string useID = sHeader.Username;
            //string pwd = sHeader.Password;

            try
            {
                if (true)
                {
                    //进行我们的业务逻辑操作
                    HAWB hawb = UtilityJson.ToObject<HAWB>(jsonHAWB);
                    if(hawb!=null)//判断重复
                    {
                        foreach(HAWB hawbObj in _hawbRepository.GetAll())
                        {
                            if(hawbObj.BarCode.Equals(hawb.BarCode))
                            {
                                return "ERROR:   请不要重复添加运单!";
                            }
                        }
                    }
                    if(hawb!=null)
                        hawb.ChangeTracker.State = ObjectState.Added;
                    if(hawb.HAWBItems!=null)
                    {
                        if (hawb.HAWBItems.Count != 0)
                        {
                            foreach (HAWBItem hawbItem in hawb.HAWBItems)
                            {
                                hawbItem.ChangeTracker.State = ObjectState.Added;//改变物品添加状态
                            }
                        }
                        
                    }
                    if (hawb.HAWBBoxes != null)
                    {
                        if (hawb.HAWBBoxes.Count != 0)
                        {
                            foreach (HAWBBox hawbBox in hawb.HAWBBoxes)
                            {
                                hawbBox.ChangeTracker.State = ObjectState.Added;//改变盒子添加状态
                            }
                        }
                        
                    }
                    IUnitOfWork unitOfWork = _hawbRepository.UnitOfWork as IUnitOfWork;
                    _hawbRepository.Add(hawb);
                    unitOfWork.Commit();
                    return "SUCCESS:   操作已成功!";
                }
                else
                {
                    return "ERROR:   未能通过安全身份认证!";
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [WebMethod(Description = "添加包裹")]
        public string AddPACKAGE(string jsonPackage)
        {
            if (string.IsNullOrEmpty(jsonPackage))
                return "ERROR:   包裹不能为空!";
            //if (sHeader == null)
            //    return "ERROR:   您的验证失败，无法进行访问Web服务!";
            //string useID = sHeader.Username;
            //string pwd = sHeader.Password;

            try
            {
                if (true)
                {
                    //进行我们的业务逻辑操作
                    Package package = UtilityJson.ToObject<Package>(jsonPackage);

                    if (package != null)//判断重复
                    {
                        foreach (Package packageObj in _packageRepository.GetAll())
                        {
                            if (packageObj.BarCode.Equals(package.BarCode))
                            {
                                return "ERROR:   请不要重复添加包裹!";
                            }
                        }
                    }
                    if (package != null)
                        package.ChangeTracker.State = ObjectState.Added;
                    if (package.HAWBs != null)
                    {
                        if (package.HAWBs.Count != 0)
                        {
                            foreach (HAWB hawb in package.HAWBs)
                            {
                                hawb.ChangeTracker.State = ObjectState.Modified;//改变运单添加状态
                            }
                        }
                        
                    }

                    IUnitOfWork unitOfWork = _packageRepository.UnitOfWork as IUnitOfWork;
                    _packageRepository.Add(package);
                    unitOfWork.Commit();
                    return "SUCCESS:   操作已成功!";
                }
                else
                {
                    return "ERROR:   未能通过安全身份认证!";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [WebMethod(Description = "添加总运单")]
        public string AddMAWB(string jsonMAWB)
        {
            if (string.IsNullOrEmpty(jsonMAWB))
                return "ERROR:   总运单不能为空!";
            //if (sHeader == null)
            //    return "ERROR:   您的验证失败，无法进行访问Web服务!";
            //string useID = sHeader.Username;
            //string pwd = sHeader.Password;

            try
            {
                if (true)
                {
                    //进行我们的业务逻辑操作
                    MAWB mawb = UtilityJson.ToObject<MAWB>(jsonMAWB);
                    if (mawb != null)//判断重复
                    {
                        foreach (MAWB mawbObj in _mawbRepository.GetAll())
                        {
                            if (mawbObj.BarCode.Equals(mawb.BarCode))
                            {
                                return "ERROR:   请不要重复添加总运单!";
                            }
                        }
                    }
                    if (mawb != null)
                        mawb.ChangeTracker.State = ObjectState.Added;
                    if (mawb.Packages != null)
                    {
                        if (mawb.Packages.Count != 0)
                        {
                            foreach (Package package in mawb.Packages)
                            {
                                package.ChangeTracker.State = ObjectState.Modified;//改变运单添加状态
                            }
                        }
                        
                    }

                    IUnitOfWork unitOfWork = _mawbRepository.UnitOfWork as IUnitOfWork;
                    _mawbRepository.Add(mawb);
                    unitOfWork.Commit();
                    return "SUCCESS:   操作已成功!";
                }
                else
                {
                    return "ERROR:   未能通过安全身份认证!";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
