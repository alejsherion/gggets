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

        IList<HAWB> FindHAWBsByCondition(string barCode, string countryCode, string regionCode, string loginName, string departmentCode, string companyName,
                                               string realName, string phone, DateTime? beginTime, DateTime? endTime, int settleType, int serviceType,
                                               bool isInternational);

        HAWB LoadHAWBByBarCode(string barCode);
        IList<HAWBItem> FindHAWBItemByHID(string HID);
        IList<HAWBBox> FindHAWBBoxByHID(string HID);
        User FindUserByUID(string UID);
        Package FindPackageByBarcode(string barcode);
        MAWB FindMAWBByBarcode(string barcode);
    }
}
