//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        模板参数IBLL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.28
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using ETS.GGGETSApp.Domain.Application.Entities;
using System;
using System.Web;
using System.Web.UI;

namespace Application.GGETS
{
    public interface IParamManagementService
    {
        #region database
        IList<Param> FindParamsByTID(string TID);
        Param FindParamByTID(string TID);
        void ModifyParam(Param param);
        void RemoveParam(Param param);
        Param FindParamByTIDAndTag(string TID, int Tag);
        #endregion

        #region logic
        void PrintHAWB(int intOrient, int intPageWidth, int intPageHeight, string strPageName, string identifyKey, string templateCode, int batchHeight, int operateType, Page page);
        void AddParam(Param param);
        #endregion
    }
}
