using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.UI;
using AjaxPro;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using GGGETSAdmin.Common;
using IMMENSITY.SWFUploadAPI;

namespace GGGETSAdmin.CustomsClearance
{
    //todo 需要提供导入文件格式
    public partial class ClearanceImport : System.Web.UI.Page
    {
        //ioc regester
        private IHAWBManagementService _hawbService;
        private IMAWBManagementService _mawbService;
        protected ClearanceImport()
        { }
        public ClearanceImport(IHAWBManagementService hawbService,IMAWBManagementService mawbService)
        {
            _hawbService = hawbService;
            _mawbService = mawbService;
        }
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
            lblMAWBCode.Text = Request["MAWBCode"];
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
                string sql = "select * from [Sheet1$]";
                var excelTable = oleDbObj.OleDbQuery(sql, SaveFilePath);
                if (excelTable == null)
                {
                    result.InnerHtml = "上传文件不能是空文件";
                    btnConfirm.Visible = false;
                }
                else
                {
                    lblHAWBNum.Text = Convert.ToString(excelTable.Rows.Count);//获取运单总数量
                    Bind(excelTable);
                    //todo --判断导入文件是不是该总运单的数据
                    //bool judge=JudgeImportData(excelTable);
                    bool judge = true;
                    //todo 一一批量更新运单状态,以及总运单中ImportStatus状态，表示是否已经导入过文件，防止重复导入，性能影响
                    if (judge)
                        BatchUpdateStatus(excelTable);
                    else
                        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('获取数据错误，请获取指定总运单的数据文件');", true);
                }
            }
            catch(Exception ex)
            {
                if (ex.Message.Contains("Sheet1$"))
                {
                    result.InnerHtml = "请修改EXCEL的sheet为sheet1";
                    return;
                }
                if (ex.Message.Contains("不属于表"))
                {
                    result.InnerHtml = string.Format("该EXCEL中没有{0}列", COLUMEHAWBBARCODE);
                    return;
                }
                result.InnerHtml = "系统错误,请联系管理员!";
            }
        }

        /// <summary>
        /// 判断导入文件是不是该总运单的数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private bool JudgeImportData(DataTable dt)
        {
            bool judge = true;
            //获取该总运单下所有运单集合
            MAWB mawb = _mawbService.FindMAWBByBarcode(Request["MAWBCode"]);
            var HAWBLists = _hawbService.FindHAWBsByMID(Convert.ToString(mawb.MID));
            foreach (var hawb in HAWBLists)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (!row[COLUMEHAWBBARCODE].Equals(hawb.BarCode))
                    {
                        return false;
                    }
                }
            }
            return judge;
        }

        /// <summary>
        /// 批量更新运单状态
        /// </summary>
        private void BatchUpdateStatus(DataTable dt)
        {
            //todo 将DataTable处理为XML解析字符串
        
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="dt"></param>
        private void Bind(DataTable dt)
        {
            ViewState["bind"] = dt;
            //bind source
            if (dt == null) return;
            RGGetData.Visible = true;
            RGGetData.DataSource = dt;
            RGGetData.DataBind();
        }

        /// <summary>
        /// 返回方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CustomsClearance/ClearanceManage.aspx");
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