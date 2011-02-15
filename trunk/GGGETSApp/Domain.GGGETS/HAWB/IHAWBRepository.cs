using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETS.GGGETSApp.Domain.Core;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace Domain.GGGETS
{
    public interface IHAWBRepository : IRepository<HAWB>
    {
        IEnumerable<HAWB> FindPagedHAWBs(int pageIndex, int pageCount);
        HAWB FindHAWBByBarCode(string barCode);

        IList<HAWB> FindHAWBsByCondition(string HID, string countryCode, string regionCode, string loginName,
                                               string realName, string phone, string settleType, string serviceType,
                                               string isInternational);

        HAWB LoadHAWBByBarCode(string barCode);
        IList<HAWBItem> FindHAWBItemByHID(string HID);
        IList<HAWBBox> FindHAWBBoxByHID(string HID);
        User FindUserByUID(string UID);
        Package FindPackageByBarcode(string barcode);
    }
}
