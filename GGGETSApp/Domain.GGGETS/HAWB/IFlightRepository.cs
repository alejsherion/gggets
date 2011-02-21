//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        航班IDAL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.21
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Domain.Core;

namespace Domain.GGGETS
{
    public interface IFlightRepository : IRepository<Flight>
    {
        IList<Flight> FindFlightByCondition(string flightNo, string from, string to, DateTime? beginDate,
                                            DateTime? endDate);

        Flight GetSingleFlightByFlightNo(string flightNo); 
    }
}
