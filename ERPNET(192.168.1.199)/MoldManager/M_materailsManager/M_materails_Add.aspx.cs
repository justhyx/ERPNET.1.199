using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;

namespace ERPPlugIn.MoldManager.M_materailsManager
{
    public partial class M_materails_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getVendor();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            InsertCommandBuilder insert = new InsertCommandBuilder("m_materials");
            insert.InsertColumn("name", txtName.Text);
            insert.InsertColumn("spec", txtSpec.Text);
            insert.InsertColumn("sccj_id", ddlVendor.SelectedItem.Value);
            insert.InsertColumn("new_price", txtPrie.Text);
            insert.InsertColumn("wb_name", moneyType.Text);
            insert.InsertColumn("lb1_id", type.Text);
            insert.InsertColumn("texture", txtCz.Text);
            insert.InsertColumn("color", txtColor.Text);
            insert.InsertColumn("dm", txtShotName.Text);
            insert.getInsertCommand();
            int i = insert.ExecuteNonQuery();
            if (i != 0)
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "showwindow", "<script>alert('添加成功')</script>");
            }
        }

        protected void btnCannel_Click(object sender, EventArgs e)
        {

        }
        public void getVendor()
        {
            string sql = "select vendor_id,vendor_name  from m_vendor";
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            if (dt.Rows.Count != 0 && dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddlVendor.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                }
            }
        }
    }
}