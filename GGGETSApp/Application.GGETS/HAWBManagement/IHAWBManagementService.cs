using System;
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
        IList<HAWB> FindHAWBsByCondition(string HID, string countryCode, string regionCode, string loginName,
                                               string realName, string phone, string settleType, string serviceType,
                                               string isInternational);

        void RemoveHAWB(string barCode);
        HAWB LoadHAWBByBarCode(string barCode);
    }
}
