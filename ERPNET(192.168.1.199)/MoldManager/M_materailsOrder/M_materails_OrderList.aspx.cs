using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;

namespace ERPPlugIn.MoldManager.M_materailsOrder
{
    public partial class M_materails_OrderList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = "select apply_no, mode_no, internal_no, Convert(varchar(11),apply_date,120) apply_date, apply_by, remark FROM m_materails_apply where is_confirm = 'Y'";
            if (!string.IsNullOrEmpty(txtStartDate.Text))
            {
                sql += "  And Convert(varchar(11),apply_date,120) = '" + txtStartDate.Text + "'";
            }
            if (!string.IsNullOrEmpty(txtMNo.Text))
            {
                sql += " And mode_no = '" + txtMNo.Text.Trim() + "'";
            }
            gvData.DataSource = new SelectCommandBuilder().ExecuteDataTable(sql);
            gvData.DataBind();
        }
    }
}