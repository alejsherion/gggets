using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.GGETS;

namespace GGGETSAdmin
{
    public partial class HOME : System.Web.UI.Page
    {
        private ISysUserManagementService _sysUserManagementService;
        protected HOME()
        { }
        public HOME(ISysUserManagementService sysUserManagementService)
        {
            _sysUserManagementService = sysUserManagementService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            TopMenu.SysUserManagementService = _sysUserManagementService;
        }
    }
}