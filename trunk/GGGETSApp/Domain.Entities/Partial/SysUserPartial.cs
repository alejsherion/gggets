//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        运单货物
// 作成者				hong.li
// 改版日				2011.02.16
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETS.GGGETSApp.Domain.Application.Entities
{
    ///<summary>
    /// 会员状态
    ///</summary>
    public enum Status
    {
        ///<summary>
        ///</summary>
        全部=-1,
        ///<summary>
        /// 正常
        ///</summary>
        正常=1
    }
    ///<summary>
    /// 系统用户
    ///</summary>
    public partial class SysUser
    {
        ///<summary>
        /// 空的账号
        ///</summary>
        public static readonly Guid EmptyUid = new Guid("00000000-0000-0000-0000-000000000000");
    }
}
