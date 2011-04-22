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

        IList<HAWB> FindHAWBsByCondition(string barCode, string countryCode, string regionCode, string departmentCode, string companyCode, string carrier,
                                               string HAWBOperator, string contactor, DateTime? beginTime, DateTime? endTime, int settleType, int serviceType,
                                               bool? isInternational);
        IList<HAWB> FindHAWBsByCondition(string barCode, string countryCode, string regionCode, string departmentCode, string companyCode, string carrier,
                                               string HAWBOperator, string contactor, DateTime? beginTime, DateTime? endTime, int settleType, int serviceType,
                                               bool? isInternational,int pageIndex,int pageCount);
        HAWB LoadHAWBByBarCode(string barCode);
        IList<HAWBItem> FindHAWBItemByHID(string HID);
        IList<HAWBBox> FindHAWBBoxByHID(string HID);
        User FindUserByUID(string UID);
        Package FindPackageByBarcode(string barcode);
        MAWB FindMAWBByBarcode(string barcode);
        //Flight FindFlightByFlightNo(string flightNo);
        //IList<Flight> FindAllFlights();
        IList<HAWB> FindHAWBsOfPackageByCondition(string barCode, DateTime? beginDate, DateTime? endDate,
                                                  string destinationCode);
        IList<HAWB> FindHAWBsByCondition(string barCode, string countryName, string regionName, string userCode, string companyName,
                                               string realName, DateTime? beginTime, DateTime? endTime, int settleType, int serviceType,
                                               bool? isInternational);
        IList<HAWB> FindHAWBsByCondition(string barCode, string countryName, string regionName, string userCode, string companyName,
                                               string realName, DateTime? beginTime, DateTime? endTime, int settleType, int serviceType,
                                               bool? isInternational, int pageIndex, int pageCount);
        IList<HAWB> FindHAWBsByMID(string MID);
        IList<HAWB> FindHAWBsByCondition(string barCode, DateTime? beginDate, DateTime? endDate);
        IList<HAWB> FindHAWBsByBillWayCode(string billWayCode);
    }
}
