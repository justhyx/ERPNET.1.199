using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;

namespace ERPPlugIn.CrushedMaterialManager
{
    public partial class delete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            if (id == 0)
            {
                Response.Redirect("index.aspx");
            }
        }
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (TextBoxPwd.Text.Equals("123456"))
            {
                string id = Request.QueryString["id"];
                DeleteCommandBuilder dcb = new DeleteCommandBuilder();
                string sql = "delete  from shatter_Parts where id = " + id + "";
                dcb.ExecuteNonQuery(sql);
                Response.Redirect("index.aspx");
            }
            else
            {
                Response.Write("<script>alert('密码不正确')</script>");
            }
        }
    }
}