using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.GGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using System.Data;

namespace GGGETSAdmin.HAWB
{
    public partial class HAWBManagement : System.Web.UI.Page
    {
        private IHAWBManagementService _hawbService;
        public int pageIndex = 0;
        public int pageCount = 10;
        protected HAWBManagement()
        {
        }
        public HAWBManagement(IHAWBManagementService hawbService)
        {
            _hawbService = hawbService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GVBind();
            }
        }

        protected void GVBind()
        {

            List<ETS.GGGETSApp.Domain.Application.Entities.HAWB> list = _hawbService.FindPagedHAWBs(pageIndex, pageCount);
            if (list.Count > 0)
            {
                this.GV_Bdemand.DataSource = list;
                this.GV_Bdemand.DataBind();
            }
            else
            {
                ETS.GGGETSApp.Domain.Application.Entities.HAWB Hawb = new ETS.GGGETSApp.Domain.Application.Entities.HAWB();
                list.Add(Hawb);
                GV_Bdemand.DataSource = list;
                GV_Bdemand.DataBind();
                foreach (GridViewRow gvr in GV_Bdemand.Rows)
                {
                    ((LinkButton)gvr.FindControl("lb_Select") as LinkButton).Visible = false;
                    ((LinkButton)gvr.FindControl("lb_Eit") as LinkButton).Visible = false;
                    ((LinkButton)gvr.FindControl("lb_Colse") as LinkButton).Visible = false;
                }
            }
            if (list.Count < pageCount)
            {
                But_Down.Enabled = false;
                But_Goup.Enabled = false;
            }
            else
            {
                But_Goup.Enabled = false;
            }
            Session["pageIndex"] = pageIndex;
            Session["pageCount"] = pageCount;
        }

        protected void But_Demand_Click(object sender, EventArgs e)
        {
            if (Txt_BarCode.Text != "")
            {
                ETS.GGGETSApp.Domain.Application.Entities.HAWB hawb = _hawbService.FindHAWBByBarCode(Txt_BarCode.Text);
                if (hawb != null)
                {
                    List<ETS.GGGETSApp.Domain.Application.Entities.HAWB> list = new List<ETS.GGGETSApp.Domain.Application.Entities.HAWB>();
                    list.Add(hawb);
                    this.GV_Bdemand.DataSource = list;
                    this.GV_Bdemand.DataBind();

                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('没有相关记录！')</script>");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入条形码后再进行查询！')</script>");
            }
        }

        protected void GV_Bdemand_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = int.Parse(e.CommandArgument.ToString());
            GridViewRow row = GV_Bdemand.Rows[index];
            string BarCode = ((Label)row.FindControl("lbl_BarCode") as Label).Text;
            if (e.CommandName == "Eit")
            {
                Guid hid = Guid.Parse(((Label)row.FindControl("lbl_Guid") as Label).Text);
                Response.Redirect("HAWBModify.aspx?BarCode=" + BarCode + "");
            }
            else if (e.CommandName == "Select")
            {
                Response.Redirect("HAWBDetails.aspx?BarCode=" + BarCode + "");
            }
        }

        protected void GV_Bdemand_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string BarCode = GV_Bdemand.DataKeys[e.RowIndex].Value.ToString();
        }
        protected void But_Down_Click(object sender, EventArgs e)
        {
            if (But_Goup.Enabled == false)
            {
                But_Goup.Enabled = true;
            }
            //pageIndex = int.Parse(Session["pageCount"].ToString()) + int.Parse(Session["pageIndex"].ToString());
            pageIndex = int.Parse(Session["pageIndex"].ToString()) + 1;
            pageCount = int.Parse(Session["pageCount"].ToString());
            List<ETS.GGGETSApp.Domain.Application.Entities.HAWB> list = _hawbService.FindPagedHAWBs(pageIndex, pageCount);
            if (list.Count > 0)
            {
                this.GV_Bdemand.DataSource = list;
                this.GV_Bdemand.DataBind();
                if ((list = _hawbService.FindPagedHAWBs(pageIndex + pageCount, pageCount)).Count == 0)
                {
                    But_Down.Enabled = false;
                }
                Session["pageIndex"] = pageIndex;
                Session["pageCount"] = pageCount;
            }
            //else
            //{
            //    But_Down.Enabled = false;
            //    Session["pageIndex"] = pageIndex - 1;
            //    Session["pageCount"] = pageCount;
            //    //this.GV_Bdemand.DataSource = _hawbService.FindPagedHAWBs(pageIndex, pageCount);
            //    //this.GV_Bdemand.DataBind();
            //    //Session["pageIndex"] = 0;
            //    //Session["pageCount"] = 1;
            //}

        }

        protected void But_Goup_Click(object sender, EventArgs e)
        {
            if (But_Down.Enabled == false)
            {
                But_Down.Enabled = true;
            }
            pageIndex = int.Parse(Session["pageIndex"].ToString()) - 1;
            pageCount = (int)Session["pageCount"];
            if (pageIndex >= 0)
            {
                this.GV_Bdemand.DataSource = _hawbService.FindPagedHAWBs(pageIndex, pageCount);
                this.GV_Bdemand.DataBind();
                if (pageIndex - 1 < 0)
                {
                    But_Goup.Enabled = false; ;
                }
                Session["pageIndex"] = pageIndex;
                Session["pageCount"] = pageCount;
            }
        }
    }
}