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
        protected Addresshistory()
        { }
        public Addresshistory(IDepartmentManagementService deparservice)
        {
            _deparservice = deparservice;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Evaluate();
            }
        }
        protected void Evaluate()
        {
            if (Session["Department"] != null)
            {
                string type = string.Empty;
                string CompanyCode = string.Empty;
                string DepCode = string.Empty;
                string Name = string.Empty;
                if (Session["compayCode"] != null)
                {
                    CompanyCode = Session["compayCode"].ToString();
                }
                if (Session["DepCode"] != null)
                {
                    DepCode = Session["DepCode"].ToString();
                }
                if (Session["name"] != null)
                {
                    Name = Session["name"].ToString();
                }
                if (Session["historytype"] != null)
                {
                    type = Session["historytype"].ToString();
                }
                if (!string.IsNullOrEmpty(DepCode) && !string.IsNullOrEmpty(CompanyCode))
                {
                    if (type == "Shipper")
                    {
                        IList<AddressBook> ressbook = _deparservice.FindAllShipAddressesByDepCodeAndCompanyCode(DepCode, CompanyCode);

                        gv_Shipper.DataSource = ressbook;
                        gv_Shipper.DataBind();
                    }
                    else if (type == "Consignee")
                    {
                        IList<AddressBook> ressbook = _deparservice.FindAllDeliveryAddressesByDepCodeAndCompanyCode(DepCode, CompanyCode);
                        if (Name != "")
                        {
                            ressbook = ressbook.Where(it => it.Name == Name).ToList();
                        }
                        gv_Shipper.DataSource = ressbook;
                        gv_Shipper.DataBind();
                    }
                    else if (type == "Deliver")
                    {
                        IList<AddressBook> ressbook = _deparservice.FindAllForwarderAddressesByDepCodeAndCompanyCode(DepCode, CompanyCode);
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
                string CompanyCode = string.Empty;
                string DepCode = string.Empty;
                IList<AddressBook> ressbook = null;
                Guid Aid = Guid.Parse(e.CommandArgument.ToString());
                string DeliverLiShi = string.Empty;
                string type = string.Empty;
                if (Session["compayCode"] != null)
                {
                    CompanyCode = Session["compayCode"].ToString();
                }
                if (Session["DepCode"] != null)
                {
                    DepCode = Session["DepCode"].ToString();
                }
                if (Session["historytype"] != null)
                {
                    type = Session["historytype"].ToString();
                }
                if (Session["DeliverLiShi"] != null)
                {
                    DeliverLiShi = Session["DeliverLiShi"].ToString();
                }
                if (type == "Shipper")
                {
                    ressbook = _deparservice.FindAllShipAddressesByDepCodeAndCompanyCode(DepCode, CompanyCode);
                    if (ressbook != null)
                    {
                        foreach (AddressBook address in ressbook)
                        {
                            if (address.AID == Aid)
                            {
                                Session["AddressShipperBook"] = address;
                                break;
                            }

                        }
                    }
                }
                else if (type == "Consignee")
                {
                    ressbook = _deparservice.FindAllDeliveryAddressesByDepCodeAndCompanyCode(DepCode, CompanyCode);
                    if (ressbook != null)
                    {
                        foreach (AddressBook address in ressbook)
                        {
                            if (address.AID == Aid)
                            {
                                Session["AddressConsigneeBook"] = address;
                                break;
                            }

                        }
                    }
                }
                else if (type == "Deliver")
                {
                    if (!string.IsNullOrEmpty(DeliverLiShi))
                    {
                        ressbook = _deparservice.FindAllForwarderAddressesByDepCodeAndCompanyCode(DepCode, CompanyCode);
                        if (ressbook != null)
                        {
                            foreach (AddressBook address in ressbook)
                            {
                                if (address.AID == Aid)
                                {
                                    Session["DeliverBook"] = address;
                                    break;
                                }

                            }
                        }
                    }
                    else
                    {
                        ressbook = _deparservice.FindAllForwarderAddressesByDepCodeAndCompanyCode(DepCode, CompanyCode);
                        if (ressbook != null)
                        {
                            foreach (AddressBook address in ressbook)
                            {
                                if (address.AID == Aid)
                                {
                                    Session["AddressDeliverBook"] = address;
                                    break;
                                }

                            }
                        }
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