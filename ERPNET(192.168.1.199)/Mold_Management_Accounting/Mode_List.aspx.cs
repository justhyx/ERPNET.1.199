using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;

namespace ERPPlugIn.Mold_Management_Accounting
{
    public partial class Mode_List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                div.Visible = false;
                tags.Focus();
            }
        }
        protected void getMode_List(string mode)
        {
            SelectCommandBuilder select = new SelectCommandBuilder();
            string sql = @"SELECT Mold_management_accounting.id, Mode_type.Mode_type_name, 
                              Mold_management_accounting.mode, Mold_management_accounting.goods_name, 
                              Mold_management_accounting.goods_no, Mold_management_accounting.qty, 
                              Mold_management_accounting.quotation_number, 
                              Mold_management_accounting.price, Mold_management_accounting.rate, 
                              Mold_management_accounting.total, Mold_management_accounting.currency, 
                              CONVERT(varchar(11), Mold_management_accounting.order_receipt_date, 120) 
                              AS order_receipt_date, CONVERT(varchar(11), 
                              Mold_management_accounting.delivery_date, 120) AS delivery_date, 
                              RTRIM(Mold_management_accounting.content_remark) AS content_remark, 
                              Mold_management_accounting.tax_rate
                        FROM Mold_management_accounting INNER JOIN
                              Mode_type ON 
                              Mold_management_accounting.type_id = Mode_type.Mode_type_id
                        WHERE (1 = 1) ";
            if (!string.IsNullOrEmpty(mode))
            {
                sql += " And mode = '" + mode + @"'";
            }
            if (!string.IsNullOrEmpty(txtCyear.Text))
            {
                sql += " And (CONVERT(varchar, order_receipt_date, 112) like '" + (txtCyear.Text + txtCmoth.Text + txtCday.Text).Trim() + "%')";
            }
            if (!string.IsNullOrEmpty(txtSyear.Text))
            {
                sql += " And (CONVERT(varchar, delivery_date, 112) like '" + (txtSyear.Text + txtSmoth.Text + txtSday.Text).Trim() + "%')";
            }
            DataTable dt = select.ExecuteDataTable(sql);
            div.Visible = true;
            gvData.DataSource = dt;
            gvData.DataBind();
            //if (dt == null || dt.Rows.Count == 0)
            //{
            //    div.Visible = true;
            //    div.Style.Add("margin-left", "20px");
            //    div.InnerHtml = "无数据...";
            //    tags.Value = string.Empty;
            //}
        }
        public string BindTitle(object title, int len)
        {
            string returnValue = string.Empty;
            if (title.ToString().Length < len)
            {
                returnValue = title.ToString().Trim();
            }
            else
            {
                returnValue = title.ToString().Substring(0, len) + "…";
            }
            return returnValue;
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            getMode_List(tags.Value);
        }
    }
}