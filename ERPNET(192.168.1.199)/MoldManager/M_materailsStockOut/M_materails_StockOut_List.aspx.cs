using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;

namespace ERPPlugIn.MoldManager.M_materailsStockOut
{
    public partial class M_materails_StockOut_List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = "SELECT str_in_no,order_no, str_in_no, mode_no, CONVERT(varchar(11),str_in_date, 120)str_in_date,CONVERT(varchar(11),operate_date, 120) operate_date,(select operator_name from operator where operator_id=operator) operator,remark FROM m_materails_str_in where is_confirm = 'N'";
            if (!string.IsNullOrEmpty(txtStartDate.Text))
            {
                sql += " And CONVERT(varchar(11),str_in_date, 120) = '" + txtStartDate.Text + "'";
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