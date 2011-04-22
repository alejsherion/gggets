//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        存储过程调用接口
// 作成者				zhiwei.shen
// 改版日				2011.04.22
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.GGETS
{
    public interface ISPManagementService
    {
        int UseBatchUpdateCustomsClearanceState(string xmlStr, string mawbCode);
        int UseUseBatchUpdateHAWBPackageState(string xmlStr);
        int UseBatchUpdateWayBillCode(string xmlStr, string waybill);
    }
}
