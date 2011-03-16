﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETS.GGGETSApp.Domain.Application.Entities;
using Application.GGETS;
using System.Text.RegularExpressions;
namespace GGGETSAdmin.MawbManage
{
    public partial class MawbAdd : System.Web.UI.Page
    {
        private IMAWBManagementService _mawbservice;
        private IPackageManagementService _packageservice;
        private IHAWBManagementService _hawbservice;
        private static string RRegion = @"^[A-Za-z]";
        private MAWB mawb;
        private Package package;
        private DateTime time = DateTime.Now;
        public int n = 1;
        protected MawbAdd()
        { }
        public MawbAdd(IMAWBManagementService mawbservice,IPackageManagementService packageservice,IHAWBManagementService hawbservicr)
        {
            _mawbservice = mawbservice;
            _packageservice = packageservice;
            _hawbservice = hawbservicr;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            txt_CreateTime.Text = time.ToString("yyyy-MM-dd HH:mm");
            if (!IsPostBack)
            {
                Txt_MAWBBarCode.Focus();
                mawb = new MAWB();
                Session["mawb"] = mawb;
                if (gv_Bag.Rows.Count == 0)
                {
                    btn_Close.Visible = false;
                }

                //txt_UpdateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            }
        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            if (Txt_BagBarCode.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请输入运单号!')", true);
                Txt_BagBarCode.Focus();
            }
            else
            {
                package = _packageservice.FindPackageByBarcode(Txt_BagBarCode.Text.Trim());
                
                if (package != null)
                {
                    
                    if (_mawbservice.JudgeMIDIsNull(package.BarCode))
                    {
                        mawb = (MAWB)Session["mawb"];
                        mawb.Packages.Add(package);
                        Txt_TotalWeight.Text = mawb.TotalWeight.ToString();
                        txt_TotalVolume.Text = mawb.TotalVolume.ToString();
                        txt_Pice.Text = package.Piece.ToString();

                        gv_Bag.DataSource = mawb.Packages;
                        gv_Bag.DataBind();
                        Session.Remove("mawb");
                        Session["mawb"] = mawb;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('该包已经添加，不能再次进行添加!')", true);
                    }
                   
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('没有该包记录!')", true);
                }
                
            }
            if (gv_Bag.Rows.Count != 0)
            {
                btn_Close.Visible = true;
            }
            Txt_BagBarCode.Text = string.Empty;
            Txt_BagBarCode.Focus();
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            Addmawb(0);
        }

        protected void btn_SaveAndClose_Click(object sender, EventArgs e)
        {
            Addmawb(1);
        }
        protected void Addmawb(int type)
        {
            mawb = (MAWB)Session["mawb"];

            if (Txt_TotalWeight.Text.Trim() != "")
            {
                mawb.TotalWeight = decimal.Parse(Txt_TotalWeight.Text.Trim());
            }
            if (txt_TotalVolume.Text.Trim() != "")
            {
                mawb.TotalVolume = decimal.Parse(txt_TotalVolume.Text.Trim());
            }
            if (type == 1)
            {
                mawb.Status = 1;
            }
            if (Regex.IsMatch(txt_From.Text.Trim(), RRegion) && Regex.IsMatch(txt_To.Text.Trim(), RRegion))
            {
                mawb.MID = Guid.NewGuid();
                mawb.BarCode = Txt_MAWBBarCode.Text.Trim().ToUpper();
                mawb.FlightNo = txt_FLTNo.Text.Trim().ToUpper();
                mawb.From = txt_From.Text.Trim().ToUpper();
                mawb.To = txt_To.Text.Trim().ToUpper();
                mawb.CreateTime = DateTime.Parse(txt_CreateTime.Text.Trim());
                mawb.Operator = "ceshi";
                _mawbservice.AddMAWB(mawb);

                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('添加成功!')", true);
                Session["mawb"] = null;
                Txt_MAWBBarCode.Text = string.Empty;
                txt_FLTNo.Text = string.Empty;
                txt_CreateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                txt_From.Text = string.Empty;
                txt_To.Text = string.Empty;
                txt_Pice.Text = string.Empty;
                Txt_TotalWeight.Text = string.Empty;
                txt_TotalVolume.Text = string.Empty;
                gv_Bag.DataSource = null;
                gv_Bag.DataBind();
                btn_Close.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('机场三字码只能输入字母并为3位!')", true);
            }
        }
        protected void btn_Close_Click(object sender, EventArgs e)
        {
            bool ok = false;
            if (mawb == null)
            {
                mawb = (MAWB)Session["mawb"];
            }
            for (int i = gv_Bag.Rows.Count - 1; i > -1; i--)
            {
                string Bar = string.Empty;
                if (((CheckBox)gv_Bag.Rows[i].FindControl("chkId")).Checked)
                {
                    ok = true;
                    Bar = gv_Bag.DataKeys[i].Value.ToString();
                    foreach (Package pk in mawb.Packages)
                    {
                        if (pk.BarCode == Bar)
                        {
                            package = pk;
                        }
                    }
                    mawb.Packages.Remove(package);
                    Txt_TotalWeight.Text = mawb.TotalWeight.ToString();
                    txt_TotalVolume.Text = mawb.TotalVolume.ToString();
                }
            }
            if (ok == true)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('移除成功!')", true);

                gv_Bag.DataSource = mawb.Packages;
                gv_Bag.DataBind();
                if (gv_Bag.Rows.Count == 0)
                {
                    btn_Close.Visible = false;
                }
               
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请选择要移除的记录!')", true);
            }
        }
        public int N()
        {
            return n++;
        }

        protected void Txt_MAWBBarCode_TextChanged(object sender, EventArgs e)
        {
            MAWB mab = _mawbservice.FindMAWBByBarcode(Txt_MAWBBarCode.Text.Trim());
            if (mab != null)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('总运单号已经存在请重新输入!')", true);
                Txt_MAWBBarCode.Focus();
                Txt_MAWBBarCode.Text = "";
            }
            else
            {
                txt_FLTNo.Focus();
            }
        }

    }
}