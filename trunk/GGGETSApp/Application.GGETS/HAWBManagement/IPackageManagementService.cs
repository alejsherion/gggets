//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        运单包裹IBLL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.12
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace Application.GGETS
{
    public interface IPackageManagementService
    {
        void AddPackage(Package package);
        Package FindPackageByBarcode(string barcode);
        void ModifyPackage(Package package);

        IList<Package> FindPackageByCondition(string barCode, DateTime? beginDate, DateTime? endDate,
                                              string destinationCode);
        bool JudgePIDIsNull(string barcode);
        bool JudgeRegionCodeIsRepeat(string barcode, string packageRegionCode, bool isMix);
    }
}
