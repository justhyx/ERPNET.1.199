using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using System.Configuration;
using ERPPlugIn.Class;

namespace ERPPlugIn
{
    public partial class Vender_Edit : PageBase
    {
        public bool is_sccj;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string Id = Request.QueryString["Id"];
                ViewState["Id"] = Id;
                ViewState["style"] = Request.QueryString["style"];
                getVendor_byId(ViewState["Id"].ToString());
            }
        }
        public void getVendor_byId(string Id)
        {
            string sql = "select vendor_id,vendor_aname,vendor_name,vendor_address,city,tel,fax,contact, CONVERT(varchar(100), crt_date, 20) as 'crt_date',operator_name,is_sccj,CheckFirst from Vendor where vendor_id = '" + Id + "'";
            SelectCommandBuilder select = new SelectCommandBuilder();
            DataTable dt = select.ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count != 0)
            {
                vendor_id.Text = dt.Rows[0]["vendor_id"].ToString().Trim();
                vendor_aname.Text = dt.Rows[0]["vendor_aname"].ToString().Trim();
                vendor_name.Text = dt.Rows[0]["vendor_name"].ToString().Trim();
                vendor_address.Text = dt.Rows[0]["vendor_address"].ToString().Trim();
                city.Text = dt.Rows[0]["city"].ToString().Trim();
                tel.Text = dt.Rows[0]["tel"].ToString().Trim();
                fax.Text = dt.Rows[0]["fax"].ToString().Trim();
                contact.Text = dt.Rows[0]["contact"].ToString().Trim();
                crt_date.Text = dt.Rows[0]["crt_date"].ToString().Trim();
                operator_name.Text = getOperatoName(dt.Rows[0]["operator_name"].ToString().Trim());
                is_sccj = dt.Rows[0]["is_sccj"].ToString() == "是" ? true : false;
                cboIsSCCJ.Checked = is_sccj;
            }


        }
        public void btnEdit_ServerClick()
        {
            Response.Write("Hello");
        }
        public void setTxtEnable()
        {
            vendor_aname.Enabled = !vendor_aname.Enabled;
            vendor_name.Enabled = !vendor_name.Enabled;
            vendor_address.Enabled = !vendor_address.Enabled;
            city.Enabled = !city.Enabled;
            tel.Enabled = !tel.Enabled;
            fax.Enabled = !fax.Enabled;
            contact.Enabled = !contact.Enabled;
            crt_date.Enabled = !crt_date.Enabled;
            cboIsSCCJ.Enabled = !cboIsSCCJ.Enabled;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            setTxtEnable();
            if (btnEdit.Text == Resources.Resource.xg)
            {
                btnEdit.Text = Resources.Resource.qr;
                btnCannel.Text = Resources.Resource.qx;
            }
            else
            {
                btnEdit.Text = Resources.Resource.xg;
                btnCannel.Text = Resources.Resource.fh;
                UpdateCommandBuilder update = new UpdateCommandBuilder(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString, "Vendor");
                update.UpdateColumn("vendor_aname", vendor_aname.Text.Trim());
                update.UpdateColumn("vendor_name", vendor_name.Text.Trim());
                update.UpdateColumn("vendor_address", vendor_address.Text.Trim());
                update.UpdateColumn("city", city.Text.Trim());
                update.UpdateColumn("tel", tel.Text.Trim());
                update.UpdateColumn("fax", fax.Text.Trim());
                update.UpdateColumn("contact", contact.Text.Trim());
                update.UpdateColumn("crt_date", crt_date.Text.Trim());
                update.UpdateColumn("is_sccj", cboIsSCCJ.Checked == true ? "是" : "");
                update.ConditionsColumn("vendor_id", ViewState["Id"].ToString());
                update.getUpdateCommand();
                update.ExecuteNonQuery();
            }
            getVendor_byId(ViewState["Id"].ToString());
        }

        protected void btnCannel_Click(object sender, EventArgs e)
        {
            if (btnCannel.Text == Resources.Resource.fh)
            {
                if (ViewState["style"].ToString() == "1")
                {
                    Response.Redirect("Vendor_List.aspx");
                }
                else if (ViewState["style"].ToString() == "2")
                {
                    Response.Redirect("Vender_DeletedItems.aspx");
                }

            }
            else
            {
                setTxtEnable();
                getVendor_byId(ViewState["Id"].ToString());
                btnEdit.Text = Resources.Resource.xg;
                btnCannel.Text = Resources.Resource.fh;
            }
        }

        protected string getOperatoName(string operator_Id)
        {
            string name = "";
            if (!string.IsNullOrEmpty(operator_Id))
            {
                SelectCommandBuilder s = new SelectCommandBuilder("operator");
                s.SelectColumn("operator_name");
                s.ConditionsColumn("operator_id", operator_Id);
                s.getSelectCommand();
                name = s.ExecuteDataTable().Rows[0]["operator_name"].ToString();
            }
            return name;

        }
    }
}