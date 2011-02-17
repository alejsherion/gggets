using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;

namespace GGGETSAdmin.PackageManage
{
    public partial class packageAdd : System.Web.UI.Page
    {
        private static string RRegion = @"^[A-Za-z]{3}";
        protected Package package=new Package();
        protected HAWB hawb;
        private IPackageManagementService _ipackageservice;
        protected IHAWBManagementService _hawbservice;
        protected packageAdd()
        { }
        public packageAdd(IPackageManagementService ipackageservice,IHAWBManagementService hawbservice)
        {
            _ipackageservice=ipackageservice;
            _hawbservice = hawbservice;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (gv_HAWB.Rows.Count == 0)
                {
                    btn_Close.Visible = false;
                }
                txt_CreateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                //txt_UpdateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                
            }
        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            
            if (txt_BarCode.Text.Trim() == "")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('请输入运单号！')</script>");
            }
            else
            {
                hawb = _hawbservice.FindHAWBByBarCode(txt_BarCode.Text.Trim());
                if (hawb != null)
                {
                    if (package.JudgeHAWB(hawb))
                    {
                        package.HAWBs.Add(hawb);
                        txt_Pice.Text = package.Piece.ToString();
                        Txt_TotalWeight.Text = package.TotalWeight.ToString();
                        gv_HAWB.DataSource = package.HAWBs;
                        gv_HAWB.DataBind();
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('该运单已经添加！')</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('没有该运单记录！')</script>");
                }
            }
            if (gv_HAWB.Rows.Count != 0)
            {
                btn_Close.Visible = true;
            }
            
            
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            AddP("Save");
        }

        protected void btn_SaveAndClose_Click(object sender, EventArgs e)
        {
            AddP("SaveAndClose");
        }
        protected void AddP(string type)
        {
            package.PID = Guid.NewGuid();
            package.BarCode = Txt_BagNumber.Text.Trim();
            package.CreateTime = DateTime.Parse(txt_CreateTime.Text.Trim());
            package.UpdateTime = DateTime.Now;
            package.Operator = "ceshi";
            if (txt_Pice.Text.Trim() != "")
            {
                package.Piece = int.Parse(txt_Pice.Text.Trim());
            }
            if (Txt_TotalWeight.Text.Trim() != "")
            {
                package.TotalWeight = decimal.Parse(Txt_TotalWeight.Text.Trim());
            }
            if (type == "SaveAndClose")
            {
                package.Status = 1;
            }
            if (Regex.IsMatch(txt_Destination.Text.Trim(), RRegion))
            {
                package.RegionCode = txt_Destination.Text.Trim();
                package.Status = 1;
                _ipackageservice.AddPackage(package);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('添加成功！')</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('只能输入字母并为3位！！')</script>");
                txt_Destination.Focus();
            }
            
        }

        protected void btn_Close_Click(object sender, EventArgs e)
        {
            for (int i = gv_HAWB.Rows.Count - 1; i > -1; i--)
            {
                string Bar = string.Empty;
                if (((CheckBox)gv_HAWB.Rows[i].FindControl("chkId")).Checked)
                {
                    Bar = gv_HAWB.DataKeys[i].Value.ToString();
                    foreach (HAWB ha in package.HAWBs)
                    {
                        if (ha.BarCode == Bar)
                        {
                            hawb = ha;
                        }
                    }
                    package.HAWBs.Remove(hawb);
                }
            }
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show", "<script>alert('删除成功！')</script>");
            if (package.HAWBs.Count == 0)
            {
                btn_Close.Visible = false;
            }
            gv_HAWB.DataSource = package.HAWBs;
            gv_HAWB.DataBind();
        }
    }
}