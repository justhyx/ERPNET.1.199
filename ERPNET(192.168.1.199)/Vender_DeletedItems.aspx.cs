using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data.SqlClient;
using ERPPlugIn.Class;

namespace ERPPlugIn
{
    public partial class Vender_DeletedItems : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                dgvVenderList.DataSource = getAllVendor("");
                dgvVenderList.DataBind();
            }
        }
        public List<Vendor> getAllVendor(string VendorName)
        {
            List<Vendor> vList = new List<Vendor>();
            string sql = "select vendor_id,vendor_name ,vendor_aname from Vendor where CheckFirst = 'N'";
            SelectCommandBuilder select = new SelectCommandBuilder();
            SqlDataReader reader = select.ExecuteReader(sql);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Vendor v = new Vendor() { VendorID = reader.GetString(0), VenderName = reader.GetString(1), VenderShortName = reader.IsDBNull(2) ? "null" : reader.GetString(2) };
                    vList.Add(v);
                }
            }
            return vList;

        }

        protected void dgvVenderList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sql = "delete from Vendor where vendor_id = '" + ((Label)dgvVenderList.Rows[e.RowIndex].Cells[0].FindControl("Label1")).Text + "'";
            DeleteCommandBuilder u = new DeleteCommandBuilder();
            u.ExecuteNonQuery(sql);
            dgvVenderList.DataSource = getAllVendor("");
            dgvVenderList.DataBind();
        }

        protected void dgvVenderList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string sql = "update Vendor set CheckFirst = 'Y' where vendor_id = '" + ((Label)dgvVenderList.Rows[e.RowIndex].Cells[0].FindControl("Label1")).Text + "'";
            UpdateCommandBuilder u = new UpdateCommandBuilder();
            u.ExecuteNonQuery(sql);
            dgvVenderList.DataSource = getAllVendor("");
            dgvVenderList.DataBind();
        }

        protected void btnCannel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Vendor_List.aspx");
        }
    }
}