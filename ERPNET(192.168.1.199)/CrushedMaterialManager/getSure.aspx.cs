using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Diagnostics;

public partial class getSure : System.Web.UI.Page
{   
  
    static string Area1 = "produceArea='客户退货区' or produceArea ='全检不良品' or produceArea='选别不良品'";
    static string Area2 = "produceArea='G1' or produceArea ='G2' or produceArea='G3' or produceArea='丝印区'";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string produceArea = Request.QueryString["produceArea"];
            HiddenFieldArea.Value = produceArea;
            string manager = Request.QueryString["manager"];
            HiddenFieldPeople.Value = manager;
            if (produceArea == null || manager == null)
            {
                Response.Redirect("index.aspx");
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string manager = HiddenFieldPeople.Value == "CEO" ? "topManagerConfirm" : "managerConfirm";
        string produceArea = HiddenFieldArea.Value == "客户退货不良品" ? Area1 : Area2;
        string pwd = HiddenFieldPeople.Value == "CEO" ? "123456" : "456789";
        if (TextBoxPwd.Text == pwd)
        {
            string sqlUpdate = @"declare @maxSignid int;
                    insert into shatter_PartsSign (setDate) values(getdate());
                    select @maxSignid= max(id) from shatter_PartsSign;
                    update shatter_Parts
                    set {0} = 1,
                    tableSign = @maxSignid
                    where {1} is null 
                    and ({2}) 
                    and tableSign is null";
            string sql = string.Format(sqlUpdate, manager, manager, produceArea);
            SqlHelper.ExecuteNonQuery(SqlHelper.conStr, System.Data.CommandType.Text, sql);
            Response.Redirect("query.aspx");
        }
        else
        {
            Response.Write("<script>alert('密码不正确')</script>");
        }
    }

}