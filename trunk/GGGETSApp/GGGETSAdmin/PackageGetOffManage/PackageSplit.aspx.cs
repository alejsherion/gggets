//************************************************************************
// 用户名				GETS国际综合快递
// 系统名				管理后台
// 子系统名		        包拆分
// 作成者				ZhiWei.Shen
// 改版日				2011.04.19
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Application.GGETS;
using DataBusDomain.Service;
using ETS.GGGETSApp.Domain.Application.Entities;
using Infrastructure;

namespace GGGETSAdmin.PackageGetOffManage
{
    public partial class PackageSplit : System.Web.UI.Page
    {
        #region 睿策 IOC BLOCK
        public ILogisticsService LogisticsService
        {
            get
            {
                if (_logisticsService == null)
                {
                    _logisticsService = ObjectFactory.NewIocInstance<ILogisticsService>();
                }
                return _logisticsService;
            }
        }
        ILogisticsService _logisticsService;
        #endregion

        //保存包裹中运单编号
        public IDictionary BarCodeList
        {
            get { return (IDictionary)ViewState["barcode"]; }
            set { ViewState["barcode"] = value; }
        }

        /// <summary>
        /// 用来全局记录改变背景色的次数，如果最后和GRID行数匹配，说明可以进行批量更新包裹为NULL
        /// </summary>
        public int Index
        {
            get { return (int)ViewState["index"]; }
            set { ViewState["index"] = value; }
        }
        //ioc register
        private IPackageManagementService _packageService;
        private ISPManagementService _spService;//批量存储过程执行BLL
        protected PackageSplit()
        {
        }
        public PackageSplit(IPackageManagementService packageService, ISPManagementService spService)
        {
            _packageService = packageService;
            _spService = spService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (BarCodeList == null)
                    BarCodeList = new Dictionary<string, string>();//实例化

                Index = 0;//初始值

                //初始化绑定数据源
                IList<HAWB> hawbs = new List<HAWB>();
                RGHAWBs.DataSource = hawbs;
                RGHAWBs.DataBind();
            }
        }

        #region ServiceEvent Block
        /// <summary>
        /// 查询并且锁定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbSearch_Click(object sender, EventArgs e)
        {
            if(!txtPackageCode.ReadOnly)
            {
                txtPackageCode.ReadOnly = true;
                lbSearch.Text = "解锁";
                btnCertain.Visible = true;
                //查询
                IList<HAWB> hawbs = GetHAWBsByPID(this.txtPackageCode.Text.Trim());
                RGHAWBs.DataSource = hawbs;
                RGHAWBs.DataBind();

                //绑定运单编号
                foreach(HAWB item in hawbs)
                {
                    BarCodeList.Add(item.BarCode, item.CustomsClearanceState);
                }

                txtScanner.Focus();
            }
            else
            {
                btnCertain.Visible = false;
                Index = 0;//这一步容易疏忽
                BarCodeList.Clear();//这一步容易疏忽
                txtPackageCode.ReadOnly = false;
                lbSearch.Text = "查询并锁定";
                txtPackageCode.Text = "";
                txtPackageCode.Focus();

                IList<HAWB> list = new List<HAWB>();
                RGHAWBs.DataSource = list;
                RGHAWBs.DataBind();
            }
        }

        /// <summary>
        /// 扫描自动回车时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCertain_Click(object sender, EventArgs e)
        {
            if (BarCodeList.Count==0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('该运单不存在或者运单编号不能为空');", true);
                return;
            }
            if(BarCodeList[this.txtScanner.Text.Trim()]==null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('请检查该运单是否经过报关审核');", true);
                return;
            }
            //遍历字典型集合
            string value = BarCodeList[this.txtScanner.Text.Trim()].ToString().ToLower();
            if(string.IsNullOrEmpty(value))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('该运单报关状态为空');", true);
                return;
            }else
            {
                //改变行色
                ChangeRowColor(this.txtScanner.Text.Trim(), value);
            }

            //最后的操作，需要清空和聚焦，方便用户体验
            txtScanner.Text = "";
            txtScanner.Focus();
        }

        /// <summary>
        /// 扫描完成，检查行色是否还有不是对应的2种颜色
        /// 如果颜色全部通过，才进行批量处理包号为NULL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbComplete_Click(object sender, EventArgs e)
        {
            if(Index==0 && RGHAWBs.MasterTableView.Items.Count==0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('无记录!');", true);
                return;
            }
            bool judge = JudgeIsBatchUpdate();
            if(judge)
            {
                //将IDIRECTORY封装成ILIST集合
                IList<string> list = new List<string>();
                foreach(var item in BarCodeList.Keys)
                {
                    list.Add(item.ToString());
                }
                //获取运单XML解析字符串
                var xmlStr = new XElement("Root",
                                          from barcode in list
                                          select new XElement("Hawb",
                                                              new XElement("barcode", barcode))).ToString();
                //批量更新运单的PID=NULL 以及状态为未打包
                int count = _spService.UseUseBatchUpdateHAWBPackageState(xmlStr);
                if(count==1)
                {
                    Guid pid = _packageService.FindPackageByBarcode(txtPackageCode.Text.Trim()).PID;
                    //todo 调用睿策，待参数确定后还需要修改
                    LogisticsService.UnpackPackageToHAWB(pid, (Guid)Session["UserID"], "undefine", DateTime.Now);

                    ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('操作成功!');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('操作失败!');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('请扫描为标记的运单，以免发生遗漏和损失');", true);
                txtScanner.Focus();
                return;
            }
        }
        #endregion

        #region Private Block
        /// <summary>
        /// 判断是否可以批量更新
        /// </summary>
        /// <returns></returns>
        private bool JudgeIsBatchUpdate()
        {
            if(Index==RGHAWBs.MasterTableView.Items.Count)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 通过包裹编号获取所有运单
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        private IList<HAWB> GetHAWBsByPID(string barcode)
        {
            if (_packageService.FindPackageByBarcode(barcode)==null)
            {
                IList<HAWB> hawbList = new List<HAWB>();
                return hawbList;
            }
            return _packageService.FindPackageByBarcode(barcode).HAWBs.ToList();
        }

        /// <summary>
        /// 改变当前行色
        /// </summary>
        private void ChangeRowColor(string barcode,string customsClearanceState)
        {
            int count = 0;
            for(int i=0;i<RGHAWBs.MasterTableView.Items.Count;i++)
            {
                if (barcode.Equals(RGHAWBs.MasterTableView.DataKeyValues[i]["BarCode"].ToString()))
                {
                    var tempColor = Convert.ToString(RGHAWBs.MasterTableView.Items[i].Style["background-color"]);//定义背景色
                    if (customsClearanceState.Equals("c") && string.IsNullOrEmpty(tempColor))
                    {
                        RGHAWBs.MasterTableView.Items[i].Style.Add(HtmlTextWriterStyle.BackgroundColor, "#B9D7FF");
                        Index++;
                    }
                    if (!customsClearanceState.Equals("c") && string.IsNullOrEmpty(tempColor))
                    {
                        RGHAWBs.MasterTableView.Items[i].Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFBCA6");
                        Index++;
                    }   
                    break;
                }
                count++;//当count为GRID行数时，代表这个运单编号在这个包中不存在，这种情况很少会发生
            }
            if (count == RGHAWBs.MasterTableView.Items.Count)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('该运单不在该包中');", true);
                return;
            }
        }
        #endregion

        #region Public Block
        public string ChangeStatus(string status)
        {
            string message = "";
            if (string.IsNullOrEmpty(status))
            {
                message = "WO";
                return message;
            }
            switch(status.ToLower())
            {
                case "c":
                    message = "CR";
                    break;
                default:
                    message = "HC";
                    break;
            }
            return message;
        }
        #endregion
    }
}