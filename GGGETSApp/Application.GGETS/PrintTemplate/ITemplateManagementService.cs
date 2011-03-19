//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        模板IBLL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.28
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using ETS.GGGETSApp.Domain.Application.Entities;
using System;
using System.Web;
using System.Web.UI;

namespace Application.GGETS
{
    public interface ITemplateManagementService
    {
        #region database
        Template FindTemplateByTemplateCode(string templateCode);
        IList<Template> GetAll();
        Template FindTemplateByTID(string TID);
        void ModifyTemplate(Template template);
        void RemoveTemplate(Template template);
        #endregion

        #region logic
        void AddTemplate(Template template);

        /// <summary>
        /// 获取主表相关信息
        /// </summary>
        /// <returns></returns>
        DataTable GetAllPrimaryTable();
       
        #endregion
    }
}
