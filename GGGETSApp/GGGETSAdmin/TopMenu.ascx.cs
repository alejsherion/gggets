using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.GGETS;
using GGGETSWeb;
using SAFF.Client;

namespace GGGETSAdmin
{
    public partial class TopMenu : System.Web.UI.UserControl
    {
        #region 构造函数

        private static ISysUserManagementService _sysUserManagementService;//权限操作类

        /// <summary>
        /// 权限操作类
        /// </summary>
        public static ISysUserManagementService SysUserManagementService
        {
            set
            {
                _sysUserManagementService = value;
            }
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["Id"] = "00000000-0000-0000-0000-000000000001";
                var userCode = (Guid)Session["UserID"];
                //var userCode = "234";
                var directoryPath = Server.MapPath("~/UserMenu");
                var absfilePath = string.Format("{0}\\{1}Menu.xml", directoryPath, userCode);
                //var ids = new Guid[]
                //              {
                //                  new Guid("9db6164c-de63-474f-aba9-49ad7e5b8c90"),
                //                  new Guid("69e88993-2979-45d2-bad3-64c6f8b0322d"),
                //                  new Guid("c379be1e-0bed-4366-9046-77caf245790b"),
                //                  new Guid("c9cc4a09-a9ba-410d-9cae-892e15b41666"),
                //                  new Guid("51cec8c8-e29d-4019-99f0-bf679a11871c"),
                //                  new Guid("77f71775-8748-4e1a-910e-cf3777712abc")
                //              };
                string menuStr;
                if (!File.Exists(absfilePath))
                {
                    var xmlOp = new XmlHelp(absfilePath);
                    CreateMenu(xmlOp);
                    menuStr = xmlOp.ReadXML();
                }
                else
                {
                    var xmlOp = new XmlHelp(absfilePath);
                    var sysUser = _sysUserManagementService.GetUserById(userCode);
                    if (sysUser == null) return;
                    var lastUpdateDate = sysUser.UpdateTime;
                    var result = xmlOp.JudgeXml(lastUpdateDate);
                    if (result)
                    {
                        CreateMenu(xmlOp);
                    }
                    menuStr = xmlOp.ReadXML();
                }
                Menu.InnerHtml = menuStr;
            }
        }

        /// <summary>
        /// 创建菜单数据
        /// </summary>
        /// <param name="xmlOp"></param>
        private void CreateMenu(XmlHelp xmlOp)
        {
            if (xmlOp == null) return;
            var userCode = (Guid)Session["UserID"];
            //var ids = new Guid[]
            //                  {
            //                      new Guid("9db6164c-de63-474f-aba9-49ad7e5b8c90"),
            //                      new Guid("69e88993-2979-45d2-bad3-64c6f8b0322d"),
            //                      new Guid("c379be1e-0bed-4366-9046-77caf245790b"),
            //                      new Guid("c9cc4a09-a9ba-410d-9cae-892e15b41666"),
            //                      new Guid("51cec8c8-e29d-4019-99f0-bf679a11871c"),
            //                      new Guid("77f71775-8748-4e1a-910e-cf3777712abc")
            //                  };
            var curentePrivileges = _sysUserManagementService.GetAppModuleByUserid(userCode);
            xmlOp.CreateUserMenuForXml(curentePrivileges);
        }
    }
}