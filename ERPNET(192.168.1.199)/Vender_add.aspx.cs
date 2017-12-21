using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using ERPPlugIn.Class;

namespace ERPPlugIn
{
    public partial class Vender_add : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getMaxId();
                //ViewState["u_id"] = Request.QueryString["uid"];
                //Response.Write("<script>alert('" + HttpContext.Current.Request.Cookies["cookie"].Values["c"] + "')</script>");
            }
        }

        private void getMaxId()
        {
            SelectCommandBuilder s = new SelectCommandBuilder();
            DataTable dt = s.ExecuteDataTable("SELECT MAX(vendor_id) FROM vendor");
            if (dt != null && dt.Rows.Count != 0)
            {
                long Id = 0;
                if (!string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                {
                    Id = Convert.ToInt64(dt.Rows[0][0].ToString());
                }
                Id += 1;
                string FId = Id.ToString().PadLeft(6, '0');
                vendor_id.Text = FId;

            }

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (inputCheck() == true)
            {
                InsertCommandBuilder insert = new InsertCommandBuilder("vendor");
                insert.InsertColumn("vendor_id", vendor_id.Text);
                insert.InsertColumn("vendor_aname", vendor_aname.Text.Trim());
                insert.InsertColumn("vendor_name", vendor_name.Text.Trim());
                insert.InsertColumn("vendor_address", vendor_address.Text.Trim());
                insert.InsertColumn("city", city.Text.Trim());
                insert.InsertColumn("tel", tel.Text.Trim());
                insert.InsertColumn("fax", fax.Text.Trim());
                insert.InsertColumn("contact", contact.Text.Trim());
                insert.InsertColumn("crt_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                insert.InsertColumn("operator_name", HttpContext.Current.Request.Cookies["cookie"].Values["id"]);
                insert.InsertColumn("is_sccj", cboIsSCCJ.Checked == true ? "是" : "");
                insert.getInsertCommand();
                int count = insert.ExecuteNonQuery();
                if (count != 0)
                {
                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "showwindow", "<script>alert('" + Resources.Resource.alterOk + "!')</script>");
                    getMaxId();
                    vendor_aname.Text = "";
                    vendor_name.Text = "";
                    vendor_address.Text = "";
                    city.Text = "";
                    tel.Text = "";
                    fax.Text = "";
                    contact.Text = "";
                    cboIsSCCJ.Checked = false;
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "showwindow", "<script>alert('" + Resources.Resource.alerttx + "')</script>");
            }

        }

        protected void btnCannel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Vendor_List.aspx");
        }
        protected bool inputCheck()
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(vendor_aname.Text))
            {
                flag = true;
            }
            return flag;
        }
    }
}