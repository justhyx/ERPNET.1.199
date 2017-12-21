using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;

namespace ERPPlugIn.Mold_Management_Accounting
{
    public partial class Mode_Recipt_Data : System.Web.UI.Page
    {
        public string Id
        {
            get
            {
                return ViewState["Id"].ToString();
            }
            set
            {
                ViewState["Id"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Id = Request.QueryString["id"];
                getReciptData(Id);
                txtRecipt_Date.Focus();
            }
        }

        protected void btnSure_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtinvoice_date.Text))
            {
                txtinvoice_date.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtRecipt_Date.Text))
            {
                txtRecipt_Date.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtAmount.Text))
            {
                txtAmount.Focus();
                return;
            }
            InsertCommandBuilder ins = new InsertCommandBuilder("Mode_receipt_accounting");
            ins.InsertColumn("receipt_date", txtRecipt_Date.Text);
            ins.InsertColumn("Amount", double.Parse(txtAmount.Text).ToString("0.00#"));
            ins.InsertColumn("M_id", Id);
            ins.InsertColumn("invoice_date", txtinvoice_date.Text);
            if (!string.IsNullOrEmpty(txtinvoice_no.Text))
            {
                ins.InsertColumn("invoice_no", txtinvoice_no.Text);
            }
            ins.getInsertCommand();
            int count = ins.ExecuteNonQuery();
            if (count != 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "<script>alert('收款成功!')</script>");
                getReciptData(Id);
                txtAmount.Text = string.Empty;
                txtRecipt_Date.Text = string.Empty;
                txtAmount.Focus();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "<script>alert('收款失败!')</script>");
            }
        }
        protected void getReciptData(string M_id)
        {
            string sql = "SELECT M_id, Amount, CONVERT(varchar(16), receipt_date, 120) AS receipt_date,CONVERT(varchar(11), invoice_date, 120) AS invoice_date,invoice_no FROM Mode_receipt_accounting WHERE (M_id = '" + M_id + "') ";
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count != 0)
            {
                div.Visible = true;
                gvReciptData.DataSource = dt;
                gvReciptData.DataBind();
            }
            else
            {
                div.Visible = false;
            }

        }
    }
}