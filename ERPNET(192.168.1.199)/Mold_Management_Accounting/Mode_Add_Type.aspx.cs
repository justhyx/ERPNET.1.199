using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;

namespace ERPPlugIn.Mold_Management_Accounting
{
    public partial class Mode_Add_Type : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtTypeName.Focus();
                getType();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTypeName.Text))
            {
                return;
            }
            InsertCommandBuilder ins = new InsertCommandBuilder("Mode_type");
            ins.InsertColumn("Mode_type_name", txtTypeName.Text.Trim());
            ins.getInsertCommand();
            int count = ins.ExecuteNonQuery();
            if (count != 0)
            {
                getType();
                txtTypeName.Text = string.Empty;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "<script>alert('添加成功!')</script>");
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "<script>alert('添加失败!')</script>");
            }
        }
        protected void getType()
        {
            SelectCommandBuilder s = new SelectCommandBuilder("Mode_type");
            s.SelectColumn("Mode_type_id");
            s.SelectColumn("Mode_type_name");
            s.ConditionsColumn("1", "1");
            s.getSelectCommand();
            DataTable d = s.ExecuteDataTable();
            ddlMode_Type.Items.Clear();
            ddlMode_Type.Items.Add(new ListItem("请选择", "0"));
            for (int i = 0; i < d.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(d.Rows[i]["Mode_type_name"].ToString().Trim()))
                {
                    ddlMode_Type.Items.Add(new ListItem(d.Rows[i]["Mode_type_name"].ToString().Trim(), d.Rows[i]["Mode_type_id"].ToString().Trim()));
                }
            }
            ddlMode_Type.DataTextField = "Mode_type_name";
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (ddlMode_Type.SelectedItem.Value == "0")
            {
                return;
            }
            string sql = "delete from Mode_type where Mode_type_id = " + ddlMode_Type.SelectedItem.Value + "";
            int count = new DeleteCommandBuilder().ExecuteNonQuery(sql);
            if (count != 0)
            {
                getType();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "<script>alert('删除成功!')</script>");
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "<script>alert('删除失败!')</script>");
            }
        }
    }
}