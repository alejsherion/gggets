﻿//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        模板参数IDAL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.28
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Domain.Core;

namespace Domain.GGGETS
{
    public interface IParamRepository :IRepository<Param>
    {
        IList<Param> FindParamsByTID(string TID);
        Param FindParamByTID(string TID);
        Param FindParamByTIDAndTag(string TID, int Tag);
    }
}
