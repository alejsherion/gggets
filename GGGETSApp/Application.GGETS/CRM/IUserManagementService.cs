﻿//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        用户IBLL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.24
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace Application.GGETS
{
    public interface IUserManagementService
    {
        User FindUserByLoginName(string loginName);
    }
}
