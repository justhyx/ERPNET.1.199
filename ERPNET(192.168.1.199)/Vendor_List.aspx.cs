using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using System.Data.SqlClient;
using ERPPlugIn.Class;

namespace ERPPlugIn
{
    public partial class _Default : PageBase
    {
        public int c = 0;
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
            string sql = "select vendor_id,vendor_name ,vendor_aname from Vendor where CheckFirst = 'Y'";
            if (!string.IsNullOrEmpty(VendorName))
            {
                sql += " and vendor_name like '%" + VendorName + "%'";
            }
            SelectCommandBuilder select = new SelectCommandBuilder();
            SqlDataReader reader = select.ExecuteReader(sql);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Vendor v = new Vendor() { VendorID = reader.GetString(0), VenderName = reader.IsDBNull(1) ? "null" : reader.GetString(1), VenderShortName = reader.IsDBNull(2) ? "null" : reader.GetString(2) };
                    vList.Add(v);
                }
            }
            return vList;

        }

        protected void dgvVenderList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgvVenderList.PageIndex = e.NewPageIndex;
            dgvVenderList.DataSource = getAllVendor("");
            dgvVenderList.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            dgvVenderList.DataSource = getAllVendor(txtName.Value.Trim());
            dgvVenderList.DataBind();
        }

        protected void dgvVenderList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sql = "update Vendor set CheckFirst = 'N' where vendor_id = '" + ((Label)dgvVenderList.Rows[e.RowIndex].Cells[0].FindControl("Label1")).Text + "'";
            UpdateCommandBuilder u = new UpdateCommandBuilder();
            u.ExecuteNonQuery(sql);
            dgvVenderList.DataSource = getAllVendor("");
            dgvVenderList.DataBind();
        }

        protected void btnDeleteManager_Click(object sender, EventArgs e)
        {
            Response.Redirect("Vender_DeletedItems.aspx");
        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Vender_add.aspx");
        }

        protected void dgvVenderList_DataBound(object sender, EventArgs e)
        {
            string sql = "select count(*) from Vendor where CheckFirst = 'N'";
            int count = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar(sql));
            if (count != 0 && btnDeleteManager.Text.IndexOf('(') == -1)
            {
                btnDeleteManager.Text += "(" + count.ToString() + ")";
            }
            else if (count == 0)
            {
                btnDeleteManager.Text = Resources.Resource.ysc;
            }
            else
            {
                btnDeleteManager.Text = Resources.Resource.ysc + "(" + count.ToString() + ")";
            }
        }

    }
    public class Vendor
    {
        public string VendorID { get; set; }
        public string VenderName { get; set; }
        public string VenderShortName { get; set; }
    }
}