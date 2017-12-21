using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Configuration;

namespace ERPPlugIn.StockInManager
{
    public partial class StockOut_ConfirmList : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConXG_ZhuSu"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                gvData.DataSource = getData();
                gvData.DataBind();
            }
        }
        public DataTable getData()
        {
            string sql = @"SELECT pre_str_out_bill.str_out_bill_id, pre_str_out_bill.str_out_bill_no, 
                                  operator.operator_name, pre_str_out_bill.str_out_date, 
                                  str_out_type.str_out_type_name, pre_str_out_bill.remark
                            FROM pre_str_out_bill INNER JOIN
                                  operator ON pre_str_out_bill.operator_id = operator.operator_id INNER JOIN
                                  str_out_type ON 
                                  pre_str_out_bill.str_out_type_id = str_out_type.str_out_type_id
                            WHERE (pre_str_out_bill.save_state = 'Y')";
            return new SelectCommandBuilder(constr, "").ExecuteDataTable(sql);
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((HyperLink)e.Row.Cells[1].FindControl("HyperLink1")).NavigateUrl = "StockOut_Confirm.aspx?Id=" + e.Row.Cells[1].Text.Trim();
            }
        }
    }
}