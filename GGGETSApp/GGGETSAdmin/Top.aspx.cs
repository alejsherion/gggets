using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace GGGETSAdmin
{
    public partial class Top : System.Web.UI.Page
    {
        private readonly ISysUserManagementService _sysUserManagementService;
        /// <summary>
        /// 构造函数
        /// </summary>
        protected Top()
        {
            
        }
        public Top(ISysUserManagementService sysUserManagementService)
        {
           
            _sysUserManagementService = sysUserManagementService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["UserID"]!=null)
            {
                string userID = Convert.ToString(Session["UserID"]);

                lblLoginInfo.Text = _sysUserManagementService.GetUserById(new Guid(userID)).LoginName;
                IList<Role> role = _sysUserManagementService.GetRolesByUers(new Guid(userID));
                StringBuilder sb = new StringBuilder();
                if(role!=null || role.Count!=0)
                {
                    foreach(Role item in role)
                    {
                        sb.Append(item.Name);
                        sb.Append(" ");
                    }
                }
                lblRoleInfo.Text = sb.ToString();
            }
        }
    }
}