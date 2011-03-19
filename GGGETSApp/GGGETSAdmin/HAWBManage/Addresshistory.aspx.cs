using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
namespace GGGETSAdmin.HAWBManage
{
    public partial class Addresshistory : System.Web.UI.Page
    {
        private IDepartmentManagementService _deparservice;
        private IAddressBookManagementService _AddRessBook;
        protected Addresshistory()
        { }
        public Addresshistory(IDepartmentManagementService deparservice,IAddressBookManagementService AddRessBook)
        {
            _deparservice = deparservice;
            _AddRessBook = AddRessBook;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Evaluate();
            }
        }
        /// <summary>
        /// 判断运单页面所传的值并绑定gridviw数据源
        /// </summary>
        protected void Evaluate()
        {
            if (Session["Department"] != null)//判断公司账号是否存在
            {
                string type = string.Empty;
                string CompanyCode = string.Empty;
                string DepCode = string.Empty;
                string Name = string.Empty;
                if (Session["compayCode"] != null)
                {
                    CompanyCode = Session["compayCode"].ToString();//公司账号
                }
                if (Session["DepCode"] != null)
                {
                    DepCode = Session["DepCode"].ToString();//部门账号
                }
                if (Session["name"] != null)
                {
                    Name = Session["name"].ToString();//个人账号
                }
                if (Session["historytype"] != null)
                {
                    type = Session["historytype"].ToString();//地址类型
                }
                if (!string.IsNullOrEmpty(DepCode) && !string.IsNullOrEmpty(CompanyCode))
                {
                    if (type == "Shipper")
                    {
                        IList<AddressBook> ressbook = _deparservice.FindAllShipAddressesByDepCodeAndCompanyCode(DepCode, CompanyCode);//获取发件人信息

                        gv_Shipper.DataSource = ressbook;
                        gv_Shipper.DataBind();
                    }
                    else if (type == "Consignee")
                    {
                        IList<AddressBook> ressbook = _deparservice.FindAllDeliveryAddressesByDepCodeAndCompanyCode(DepCode, CompanyCode);//获取发件人信息
                        if (Name != "")
                        {
                            ressbook = ressbook.Where(it => it.Name == Name).ToList();
                        }
                        gv_Shipper.DataSource = ressbook;
                        gv_Shipper.DataBind();
                    }
                    else if (type == "Deliver")
                    {
                        IList<AddressBook> ressbook = _deparservice.FindAllForwarderAddressesByDepCodeAndCompanyCode(DepCode, CompanyCode);//获取交付人信息
                        gv_Shipper.DataSource = ressbook;
                        gv_Shipper.DataBind();
                    }

                }
            }
        }

        protected void gv_Shipper_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string Aid = e.CommandArgument.ToString();
                string DeliverLiShi = string.Empty;
                string type = string.Empty;
                AddressBook ressbook;
                if (Session["historytype"] != null)
                {
                    type = Session["historytype"].ToString();
                }
                if (Session["DeliverLiShi"] != null)
                {
                    DeliverLiShi = Session["DeliverLiShi"].ToString();//添加交付人历史页面说传的值
                }
                if (type == "Shipper")
                {
                    ressbook = _AddRessBook.FindAddressBookByAID(Aid);
                    Session["AddressShipperBook"] = ressbook;
                }
                else if (type == "Consignee")
                {
                    ressbook = _AddRessBook.FindAddressBookByAID(Aid);
                    Session["AddressConsigneeBook"] = ressbook;
                }
                else if (type == "Deliver")
                {
                    if (!string.IsNullOrEmpty(DeliverLiShi))
                    {
                        ressbook = _AddRessBook.FindAddressBookByAID(Aid);
                        Session["DeliverBook"] = ressbook;
                    }
                    else
                    {
                        ressbook = _AddRessBook.FindAddressBookByAID(Aid);
                        Session["AddressDeliverBook"] = ressbook;
                    }
                }
                Session.Remove("ComCode");
                Session.Remove("DepCode");
                Session.Remove("name");
                Session.Remove("DeliverLiShi");
                Session.Remove("historytype");
                if (!string.IsNullOrEmpty(DeliverLiShi))
                {
                    Response.Write("<script>window.parent.location = 'DeliverAdd.aspx';</script>");
                }
                else
                {
                    Response.Write("<script>window.parent.location = 'HAWBAdd.aspx';</script>");
                }
            }
        }
    }
}