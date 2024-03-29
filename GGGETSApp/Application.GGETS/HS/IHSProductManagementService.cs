﻿//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        HS海关商品编码IBLL
// 作成者				ZhiWei.Shen
// 改版日				2011.03.12
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace Application.GGETS
{
    public interface IHSProductManagementService
    {
        IList<HSProduct> GetAll();
        IList<HSProduct> GetPagedAll(int pageIndex, int pageCount);
        HSProduct FindHSProductByHSCode(string HSCode);
        HSProduct LoadHSProductByHSCode(string HSCode);
        IList<HSProduct> FindHSProductByCondition(string HSCode, string HSName, int pageIndex, int pageCount, ref int totalCount);
        IList<HSProduct> FindHSProductByCondition(string HSCode, string HSName);
        void ModifyHSProduct(HSProduct product);
        void AddHSProduct(HSProduct product);
        bool JudgeHSCodeIsExist(string HSCode);
        bool JudgeHSNameIsExist(string HSName);
    }
}
