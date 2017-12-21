using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;

namespace ERPPlugIn.MoldManager.M_materailsManager
{
    public partial class M_materails_Edit : System.Web.UI.Page
    {
        public string id
        {
            get { return ViewState["id"].ToString(); }
            set { ViewState["id"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                id = Request.QueryString["Id"];
                getVendor();
                getData(id);
            }
        }
        public void getData(string id)
        {
            SelectCommandBuilder s = new SelectCommandBuilder("m_materials");
            s.SelectColumn("id");
            s.SelectColumn("name");
            s.SelectColumn("spec");
            s.SelectColumn("sccj_id");
            s.SelectColumn("new_price");
            s.SelectColumn("wb_name");
            s.SelectColumn("lb1_id");
            s.SelectColumn("texture");
            s.SelectColumn("color");
            s.SelectColumn("dm");
            s.ConditionsColumn("id", id);
            s.getSelectCommand();
            DataTable dt = s.ExecuteDataTable();
            if (dt != null && dt.Rows.Count != 0)
            {
                txtName.Text = dt.Rows[0]["name"].ToString().Trim();
                txtSpec.Text = dt.Rows[0]["spec"].ToString().Trim();
                ddlVendor.SelectedIndex = getIndex(dt.Rows[0]["sccj_id"].ToString().Trim());
                txtPrie.Text = dt.Rows[0]["new_price"].ToString() == string.Empty ? "0.00" : Convert.ToDouble(dt.Rows[0]["new_price"]).ToString("0.00#");
                txtmoneyType.Text = dt.Rows[0]["wb_name"].ToString().Trim();
                txttype.Text = dt.Rows[0]["lb1_id"].ToString().Trim();
                txtCz.Text = dt.Rows[0]["texture"].ToString().Trim();
                txtColor.Text = dt.Rows[0]["color"].ToString().Trim();
                txtShotName.Text = dt.Rows[0]["dm"].ToString().Trim();
            }
        }
        public int getIndex(string id)
        {
            int indx = 0;
            for (int i = 0; i < ddlVendor.Items.Count; i++)
            {
                if (ddlVendor.Items[i].Value.Trim() == id.Trim())
                {
                    indx = i;
                    break;
                }
            }
            return indx;
        }
        public void getVendor()
        {
            string sql = "select vendor_id,vendor_name  from vendor";
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            if (dt.Rows.Count != 0 && dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddlVendor.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                }
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            UpdateCommandBuilder update = new UpdateCommandBuilder("m_materials");
            update.UpdateColumn("name", txtName.Text.Trim());
            update.UpdateColumn("spec", txtSpec.Text.Trim());
            update.UpdateColumn("sccj_id", ddlVendor.SelectedItem.Value);
            update.UpdateColumn("new_price", Convert.ToDouble(txtPrie.Text));
            update.UpdateColumn("wb_name", txtmoneyType.Text);
            update.UpdateColumn("lb1_id", txttype.Text);
            update.UpdateColumn("texture", txtCz.Text);
            update.UpdateColumn("color", txtColor.Text);
            update.UpdateColumn("dm", txtShotName.Text);
            update.ConditionsColumn("id", id);
            update.getUpdateCommand();
            int count = update.ExecuteNonQuery();
            if (count != 0)
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "showwindow", "<script>alert('保存成功')</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "showwindow", "<script>alert('保存失败')</script>");
            }
        }

        protected void btnCannel_Click(object sender, EventArgs e)
        {
            Response.Redirect("M_materails_List.aspx");
        }
    }
}