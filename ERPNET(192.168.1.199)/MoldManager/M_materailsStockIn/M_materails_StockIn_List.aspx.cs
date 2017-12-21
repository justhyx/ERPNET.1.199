using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;

namespace ERPPlugIn.MoldManager.M_materailsStockIn
{
    public partial class M_materails_StockIn_List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT m_materails_pch_order.Order_No,m_materails_pch_order.mode_no, CONVERT(varchar(11), 
                              m_materails_pch_order.Order_date, 120) AS Order_date, m_vendor.vendor_name, 
                              operator.operator_name, CONVERT(varchar(11), 
                              m_materails_pch_order.Operator_Date, 120) AS Operator_Date, 
                              m_materails_pch_order.Remark
                        FROM m_materails_pch_order INNER JOIN
                              m_vendor ON m_materails_pch_order.Vendor_id = m_vendor.vendor_id INNER JOIN
                              operator ON m_materails_pch_order.Operator_id = operator.operator_id where  m_materails_pch_order.isCheck = 'N'";
            if (!string.IsNullOrEmpty(txtStartDate.Text))
            {
                sql += " And CONVERT(varchar(11),m_materails_pch_order.Order_date, 120) = '" + txtStartDate.Text + "'";
            }
            if (!string.IsNullOrEmpty(txtMNo.Text))
            {
                sql += " And m_materails_pch_order.mode_no = '" + txtMNo.Text.Trim() + "'";
            }
            gvData.DataSource = new SelectCommandBuilder().ExecuteDataTable(sql);
            gvData.DataBind();
        }
    }
}