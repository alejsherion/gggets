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
        IList<HAWB> FindHAWBsByCondition(string barCode, string countryCode, string regionCode, string loginName, string departmentCode, string companyName,
                                               string realName, string phone, DateTime? beginTime, DateTime? endTime, int settleType, int serviceType,
                                               bool? isInternational);

        void RemoveHAWB(string barCode);
        HAWB LoadHAWBByBarCode(string barCode);
        HAWBBox FindHAWBBoxByHID(string HID);
        //Flight FindFlightByFID(string FID);
        IList<HAWB> FindHAWBsOfPackageByCondition(string barCode, DateTime? beginDate, DateTime? endDate,
                                                  string destinationCode);
    }
}
