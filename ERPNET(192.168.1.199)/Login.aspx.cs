﻿using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Drawing;
using ERPPlugIn.Class;
using System.Data;
using System.Collections;
using System.Web.SessionState;
using System.Configuration;
using System.Diagnostics;

namespace ERPPlugIn
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {
                HiddenField1.Value = Request.QueryString["urli"];
                //if (Request.UrlReferrer != null)
                //    ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
                txtUserName.Focus();
                txtUserName.Attributes.Add("onfocus", "this.select();");
                txtPassWords.Attributes.Add("onfocus", "this.select();");
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                lblResult.ForeColor = Color.White;
                if (string.IsNullOrEmpty(txtUserName.Text))
                {
                    lblResult.Text = "请输入账号";
                    lblResult.ForeColor = Color.Red;
                    //Response.Write("<script>alert('账号不存在')</script>");
                    txtUserName.Focus();
                    txtUserName.Attributes.Add("onfocus", "this.select();");
                    txtPassWords.Attributes.Add("onfocus", "this.select();");
                    return;
                }
                if (string.IsNullOrEmpty(txtPassWords.Text))
                {
                    lblResult.Text = "请输入密码";
                    lblResult.ForeColor = Color.Red;
                    //Response.Write("<script>alert('账号不存在')</script>");
                    txtPassWords.Focus();
                    return;
                }
                SelectCommandBuilder s = new SelectCommandBuilder(ConnectionFactory.ConnectionString_hudsonwwwroot, "HUDSON_User");
                string sqluser = "select count(UserID) from HUDSON_User where UserID = '" + txtUserName.Text.Trim() + "'";
                Debug.WriteLine(sqluser);
                int count = Convert.ToInt32(s.ExecuteScalar(sqluser));
                if (count == 0)
                {
                    lblResult.Text = "输入的账号不存在";
                    lblResult.ForeColor = Color.Red;
                    //Response.Write("<script>alert('账号不存在')</script>");
                    txtUserName.Focus();
                    txtUserName.Attributes.Add("onfocus", "this.select();");
                    txtPassWords.Attributes.Add("onfocus", "this.select();");
                    return;
                }
                string sqlpwd = @"select count(UserID) from HUDSON_User where UserPassWord = '" + txtPassWords.Text
                                + "' and UserID = '" + txtUserName.Text + "'";
                int countc = Convert.ToInt32(s.ExecuteScalar(sqlpwd));
                if (countc == 0)
                {
                    lblResult.Text = "密码错误,请重试...";
                    lblResult.ForeColor = Color.Red;
                    //Response.Write("<script>alert('密码错误')</script>");
                    txtPassWords.Focus();
                    return;
                }


                //Hashtable h = (Hashtable)Application["online"];
                //string oldsessionid = null;
                //if (h != null)
                //{
                //    IDictionaryEnumerator e1 = h.GetEnumerator();
                //    bool flag = false;
                //    while (e1.MoveNext())
                //    {

                //        //判断当前登录用户时候存在于application对象中
                //        if (((string)((ArrayList)e1.Value)[0]).Equals(txtUserName.Text))
                //        {
                //            flag = true;
                //            oldsessionid = e1.Key.ToString();
                //            break;
                //        }
                //    }
                //    if (flag)
                //    {

                //        //获取用户成功登录时间到目前现在时间差
                //        TimeSpan ts = System.DateTime.Now.Subtract(Convert.ToDateTime(((ArrayList)e1.Value)[1]));
                //        if (ts.TotalSeconds < 30)
                //        {
                //            //ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script> alert('对不起，你输入的账户正在被使用中，如果你是这个账户的真正主人，请在下次登陆时及时的更改你的密码，因为你的密码极有可能被盗窃了!');</script>");
                //            HttpSessionState sessionstate = (HttpSessionState)((ArrayList)e1.Value)[2];
                //            //sessionstate.Clear();
                //            sessionstate.Abandon();
                //            lblResult.Text = "该账号已登录...";
                //            lblResult.ForeColor = Color.Red;
                //            //Response.Write("<script>alert('密码错误')</script>");
                //            txtPassWords.Focus();
                //        }
                //        h.Remove(e1.Key);
                //        return;
                //    }

                //}
                //else
                //{
                //    h = new Hashtable();
                //}
                //ArrayList al = new ArrayList();
                //al.Add(txtUserName.Text);    //当前登录的用户名
                //al.Add(System.DateTime.Now);//登录的时间
                //al.Add(HttpContext.Current.Session);//当前会话
                //h[Session.SessionID] = al;
                //Application["online"] = h;//将当前登录用户信息存入application中
                ////Response.Redirect("~/LoginSuccess.aspx");
                HttpCookie cook = new HttpCookie("cookie");
                cook.Values.Add("id", txtUserName.Text.Trim());
                cook.Values.Add("pwd", txtPassWords.Text.Trim());
                cook.Values.Add("langue", DropDownList1.SelectedItem.Value.Trim().ToLower());
                cook.Values.Add("name", Convert.ToString(s.ExecuteScalar("select UserName from HUDSON_User where userID='" + txtUserName.Text.Trim() + "'")));
                HttpContext.Current.Response.Cookies.Add(cook);
                Session["UserName"] = txtUserName.Text;
                //if (ViewState["UrlReferrer"] == null)
                if (HiddenField1.Value == "")
                { }
                else
                {
                    Response.Redirect(HiddenField1.Value);
                }
                Response.Redirect("index.aspx");// 默认
                //else
                //    Response.Redirect(ViewState["UrlReferrer"].ToString());
            }
            catch (Exception ex)
            {
                lblResult.Text = ex.Message;
                lblResult.ForeColor = Color.Red;
                throw;


            }

        }

        protected bool CheckLogin(string UserName, string PassWord)
        {
            bool result = false;
            SelectCommandBuilder select = new SelectCommandBuilder("UserInfo");
            select.SelectColumn("password");
            select.SelectColumn("PassWordKey");
            select.ConditionsColumn("operator_name", UserName);
            select.getSelectCommand();
            DataTable dt = select.ExecuteDataTable();
            string _passWord = dt.Rows[0]["password"].ToString().Trim();
            string _key = dt.Rows[0]["PassWordKey"].ToString().Trim();
            if (PassWord == ClassDES.Decrypt(_passWord, _key))
            {
                result = true;
            }
            return result;
        }

        protected void btnColse_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.opener=null;window.open('','_self');window.close();</script>");
        }
    }
}