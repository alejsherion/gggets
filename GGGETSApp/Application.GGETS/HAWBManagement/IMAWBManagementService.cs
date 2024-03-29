﻿//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        总运单IBLL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.15
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace Application.GGETS
{
    public interface IMAWBManagementService
    {
        void AddMAWB(MAWB mawb);
        MAWB FindMAWBByBarcode(string barcode);
        void ModifyMAWB(MAWB mawb);
        IList<MAWB> FindMAWBByCondition(string barCode, DateTime? beginDate, DateTime? endDate);
        IList<MAWB> FindMAWBByCondition(string barCode, DateTime? beginDate, DateTime? endDate,int pageIndex, int pageCount,ref int totalCount);
        IList<MAWB> FindAllMAWBsByFlightNo(string flightNo);
        IList<MAWB> FindAllMAWBsByFlightNo(string flightNo, int pageIndex, int pageCount, ref int totalCount);
        MAWB FindMAWBByMID(string MID);
        IList<MAWB> FindMAWBByFlightCondition(string flightNo, string from, string to);
        IList<MAWB> FindMAWBByFlightCondition(string flightNo, string from, string to, int pageIndex, int pageCount,ref int totalCount);
        bool JudgeMIDIsNull(string barcode);
    }
}
