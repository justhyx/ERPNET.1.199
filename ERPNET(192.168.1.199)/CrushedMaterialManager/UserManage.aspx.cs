using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using System.Diagnostics;

namespace ERPPlugIn.CrushedMaterialManager
{
    public partial class UserManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink1.NavigateUrl = "UserManage.aspx?action=add";
            string action = Request.QueryString["action"];
            switch (action)
            { 
                default :
                        getData();
                        table1.Visible = false;
                        break;
                case "add":
                        HyperLink1.Visible = false;
                        break;
            }
            
        }
        private void getData()
        {
            SelectCommandBuilder cmd = new SelectCommandBuilder();
            string sql = "select userid,name,department from shatter_Parts_Password";
            DataTable dt = cmd.ExecuteDataTable(sql);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        //if (e.Row.RowType != DataControlRowType.DataRow)
        //    {
        //        return;
        //    }
        //    DataRowView drv = e.Row.DataItem as DataRowView;

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = e.RowIndex;
            string userId = GridView1.Rows[id].Cells[1].Text;
            string sqldel = "delete from shatter_Parts_Password where userid = '" + userId + "'";
            DeleteCommandBuilder dcb = new DeleteCommandBuilder();
            dcb.ExecuteNonQuery(sqldel);
            Response.Redirect("UserManage.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string userID = TextBox1.Text;
            string department = TextBox2.Text;
            string name = TextBox3.Text;
            InsertCommandBuilder icd = new InsertCommandBuilder();
            string sqlinsert = @"insert into shatter_Parts_Password (userid,department,name)
                               values('" + userID + "','" + department + "','" + name + "')";
            Debug.WriteLine(sqlinsert);

            icd.ExecuteNonQuery(sqlinsert);
            Response.Redirect("UserManage.aspx");
        }
    }
}