using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using GGGETSAdmin.Common;

namespace GGGETSAdmin.WebService
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private IHAWBManagementService _hawbservice;
        private IPackageManagementService _packageservice;
        private IMAWBManagementService _mawbservice;
        protected WebForm1()
        { }
        public WebForm1(IHAWBManagementService hawbService, IPackageManagementService packService, IMAWBManagementService mawbservice)
        {
            _hawbservice = hawbService;
            _packageservice = packService;
            _mawbservice = mawbservice;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //HAWB hawb = _hawbservice.FindHAWBByBarCode("HAWB001");
            //string jsonStr = UtilityJson.ToJson(hawb);

            //Package p = _packageservice.FindPackageByBarcode("PPPP");
            //string jsonStr2 = UtilityJson.ToJson(p);

            MAWB mawb = _mawbservice.FindMAWBByBarcode("MAWB007");
            string jsonStr3 = UtilityJson.ToJson(mawb);
        }
    }
}