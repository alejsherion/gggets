//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        模板参数维护IBLL
// 作成者				ZhiWei.Shen
// 改版日				2011.03.18
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
    public interface IFindInfoManagementService
    {
        IList<FindInfo> FindAllByTableName(string tableName);
        FindInfo FindInfoByTableAndFieldName(string fieldName, string tableName);
    }
}
