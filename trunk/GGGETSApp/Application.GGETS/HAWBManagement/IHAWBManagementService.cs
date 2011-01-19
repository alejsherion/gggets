﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace Application.GGETS
{
    public interface IHAWBManagementService
    {
        void AddHAWB(HAWB hawb);
        void ChangeHAWB(HAWB hawb);
        HAWB FindHAWBByBarCode(string barCode);
        List<HAWB> FindPagedHAWBs(int pageIndex, int pageCount);
    }
}