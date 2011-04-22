﻿//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        存储过程调用接口
// 作成者				zhiwei.shen
// 改版日				2011.04.22
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Domain.Core;

namespace Domain.GGGETS
{
    public interface ISPRepository : IRepository<HAWBItem>
    {
        int UseBatchUpdateCustomsClearanceState(string xmlStr, string mawbCode);
        int UseBatchUpdateHAWBPackageState(string xmlStr);
        int UseBatchUpdateWayBillCode(string xmlStr,string waybill);
    }
}
