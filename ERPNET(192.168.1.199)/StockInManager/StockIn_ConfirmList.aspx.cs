using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using System.Configuration;

namespace ERPPlugIn.StockInManager
{
    public partial class StockIn_ConfirmList : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConXG_ZhuSu"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string sql = @"SELECT str_in_type.str_in_type_name, pre_str_in_bill.str_in_bill_no, 
                                      operator.operator_name, pre_str_in_bill.remark, pre_str_in_bill.str_in_bill_id, 
                                      pre_str_in_bill.operator_date
                                FROM pre_str_in_bill INNER JOIN
                                      str_in_type ON 
                                      pre_str_in_bill.str_in_type_id = str_in_type.str_in_type_id INNER JOIN
                                      operator ON pre_str_in_bill.operator_id = operator.operator_id
                                WHERE (pre_str_in_bill.is_state = 'Y')";
                DataTable dt = new SelectCommandBuilder(constr,"").ExecuteDataTable(sql);
                gvData.DataSource = dt;
                gvData.DataBind();
            }
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //模板列
                ((HyperLink)e.Row.Cells[1].FindControl("HyperLink1")).NavigateUrl = "StockIn_Confirm.aspx?Id=" + e.Row.Cells[1].Text.Trim();

            }
        }
    }
}