//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        总运单IDAL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.15
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Domain.Core;

namespace Domain.GGGETS
{
    public interface IMAWBRepository : IRepository<MAWB>
    {
        IList<MAWB> FindMAWBByCondition(string barCode, DateTime? beginDate, DateTime? endDate);
        IList<MAWB> FindMAWBByCondition(string barCode, DateTime? beginDate, DateTime? endDate,int pageIndex,int pageCount);
        IList<MAWB> FindAllMAWBsByFlightNo(string flightNo);
        IList<MAWB> FindAllMAWBsByFlightNo(string flightNo, int pageIndex, int pageCount);
        MAWB FindMAWBByMID(string MID);
        IList<MAWB> FindMAWBByFlightCondition(string flightNo, string from, string to);
        IList<MAWB> FindMAWBByFlightCondition(string flightNo, string from, string to, int pageIndex, int pageCount);
    }
}
