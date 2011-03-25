using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.GGETS;

namespace GGGETSAdmin
{
    public partial class Menu : System.Web.UI.Page
    {
        #region 构造函数
        private ISysUserManagementService _templateService;
        protected Menu()
        { }
        public Menu(ISysUserManagementService templateService)
        {
            _templateService = templateService;
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            TopMenu.SysUserManagementService = _templateService;
        }
    }
}