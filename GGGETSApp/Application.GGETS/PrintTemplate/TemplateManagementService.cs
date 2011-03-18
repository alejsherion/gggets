//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        模板BLL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.28
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Core;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Domain.Core.Specification;

namespace Application.GGETS
{
    public class TemplateManagementService:ITemplateManagementService
    {
        /// <summary>
        /// IOC Injecting into
        /// </summary>
        ITemplateRepository _templateRepository;
        public TemplateManagementService(ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        /// <summary>
        /// 通过模板编号获取模板信息
        /// </summary>
        /// <param name="templateCode">模板编号</param>
        /// <returns></returns>
        public Template FindTemplateByTemplateCode(string templateCode)
        {
            return _templateRepository.FindTemplateByTemplateCode(templateCode);
        }

        /// <summary>
        /// 获取所有模板信息
        /// </summary>
        /// <returns></returns>
        public IList<Template> GetAll()
        {
            return _templateRepository.GetAll().ToList();
        }

        /// <summary>
        /// 通过TID获取模板
        /// </summary>
        /// <param name="TID">模板序号</param>
        /// <returns></returns>
        public Template FindTemplateByTID(string TID)
        {
            return _templateRepository.FindTemplateByTID(TID);
        }

        /// <summary>
        /// 修改模板
        /// </summary>
        /// <param name="template">模板</param>
        public void ModifyTemplate(Template template)
        {
            if (template == null)
                throw new ArgumentNullException("Template is null");
            IUnitOfWork unitOfWork = _templateRepository.UnitOfWork;
            _templateRepository.Modify(template);
            //complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
        }

        /// <summary>
        /// 新增模板
        /// </summary>
        /// <param name="template">模板</param>
        public void AddTemplate(Template template)
        {
            if (template == null)
                throw new ArgumentNullException("Template is null");
            IUnitOfWork unitOfWork = _templateRepository.UnitOfWork;
            _templateRepository.Add(template);
            //complete changes in this unit of work
            unitOfWork.Commit();
        }
    }
}
