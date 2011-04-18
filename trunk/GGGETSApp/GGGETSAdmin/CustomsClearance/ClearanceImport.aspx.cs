//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        报关文件导入
// 作成者				ZhiWei.Shen
// 改版日				2011.04.18
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.UI;
using AjaxPro;
using GGGETSAdmin.Common;
using IMMENSITY.SWFUploadAPI;

namespace GGGETSAdmin.CustomsClearance
{
    public partial class ClearanceImport : System.Web.UI.Page
    {
        #region 属性
        private static string _saveFilePath;//文件保存路径
        static readonly object Padlock = new object();//保证线程安全
        private readonly string COLUMESTATUS = "XXXXX";//todo 状态列名
        private readonly string COLUMEHAWBBARCODE = "XXXXX";//todo 我们的运单编号列名
        /// <summary>
        /// 文件保存路径
        /// </summary>
        private string SaveFilePath
        {
            get
            {
                lock (Padlock)
                {
                    var path = UC_SWFUpload1.FilePath;
                    var fileName = UC_SWFUpload1.FileName;
                    if (String.IsNullOrEmpty(path) || String.IsNullOrEmpty(fileName)) return "";
                    var accessPath = "../" + path + "/b/" + fileName;
                    _saveFilePath = Request.MapPath(accessPath);
                }
                return _saveFilePath;
            }
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //DWR声明，不能放在ispostback中
            Utility.RegisterTypeForAjax(typeof(ClearanceImport));
            #region 导入Excel文件设置
            UC_SWFUpload1.SwfUploadInfo = new SWFUploadInfo
            {
                File_types = "*.xls",
                File_types_description = "Excel文件",
                IsSmall = false,
                IsWaterMark = false,
                Path = "Excel"
            };
            var uf = new SWFUploadFileInfo();
            var serializer = new DataContractJsonSerializer(typeof(SWFUploadFileInfo));
            var ms = new MemoryStream();
            serializer.WriteObject(ms, uf);
            RGGetData.Visible = false;
            lblHAWBNum.Text = "0 单";
            lblFlightNo.Text = Request["FlightNo"];
            #endregion
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Click(object sender, EventArgs e)
        {
            btnConfirm.Visible = !String.IsNullOrEmpty(SaveFilePath);
            //清空错误提示lable
            result.InnerText = "";
            try
            {
                OleDbAccessHelper oleDbObj = new OleDbAccessHelper();
                string sql = "select * from [Sheet0$]";
                var excelTable = oleDbObj.OleDbQuery(sql, SaveFilePath);
                if (excelTable == null)
                {
                    result.InnerHtml = "上传文件不能是空文件";
                    btnConfirm.Visible = false;
                }
                else
                {
                    var Resutl = Excelformat(excelTable);//验证
                    if (String.IsNullOrEmpty(Resutl))
                    {
                        if (excelTable.Rows.Count > 0)
                        {
                            lblHAWBNum.Text = Convert.ToString(excelTable.Rows.Count);
                            Bind(excelTable);
                        }
                    }
                    else
                    {
                        result.InnerHtml = Resutl;
                    }

                }
            }
            catch
            {
                result.InnerHtml = "系统错误";
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="dt"></param>
        private void Bind(DataTable dt)
        {
            ViewState["bind"] = dt;
            //获取参数传递过来的总运单号和航班号
            string FlightNo = Request["FlightNo"];
            string MAWBCode = Request["MAWBCode"];
            if (!string.IsNullOrEmpty(FlightNo) && !string.IsNullOrEmpty(MAWBCode))
            {
                
            }
            //bind source
            if (dt == null) return;
            RGGetData.Visible = true;
            RGGetData.DataSource = dt;
            RGGetData.DataBind();
        }

        /// <summary>
        /// 确认方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            //跳转到提货卷系统导入页面进行插入数据库
            ScriptManager.RegisterStartupScript(this, GetType(), "", "CloseRadWindow()", true);
        }

        /// <summary>
        /// 判断Excel表中数据
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        private string Excelformat(DataTable tb)
        {
            if (tb == null || tb.Rows.Count == 0) return "";
            var Reuslt = "";
            foreach (DataRow row in tb.Rows)
            {
                var Key = Convert.ToString(row["劵号"]);
                var Value = Convert.ToString(row["密码"]);
                if (string.IsNullOrEmpty(Key) && string.IsNullOrEmpty(Value))
                {
                    Reuslt = "Excel表中含有空记录,请重新导入";
                    break;
                }
                if (string.IsNullOrEmpty(Key))
                {
                    Reuslt = "Excel表中含有空劵号,请重新导入";
                    break;
                }
                if (Key.Length != 10)
                {
                    Reuslt = "劵号: " + Key + " 不是10位数字和字母组成的字符!您可以在Excel表中使用“Ctrl+F”快捷键寻找后进行修改";
                    break;
                }
                if (Value.Length != 6)
                {
                    Reuslt = "劵号: " + Key + " 所对应的密码不是6位数字和字母组成的字符!您可以在Excel表中使用“Ctrl+F”快捷键寻找后进行修改";
                    break;
                }
            }
            return Reuslt;
        }

        #region AjaxPro
        [AjaxMethod]
        public void DeleteCookie()
        {
            if (HttpContext.Current.Response.Cookies["uploadStatus"] != null)
                HttpContext.Current.Response.Cookies["uploadStatus"].Expires = DateTime.Now.AddDays(-1);
        }

        #endregion

        /// <summary>
        /// GRID绑定
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void RadGridPickupTickets_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (ViewState["bind"] != null)
            {
                RGGetData.Visible = true;
                RGGetData.DataSource = ViewState["bind"];
                RGGetData.DataBind();
            }
        }
    }
}