using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using ERPPlugIn.Class;

namespace ERPPlugIn.Mold_Management_Accounting
{
    public partial class Mode_Index : System.Web.UI.Page
    {
        public string rate
        {
            get
            {
                return Convert.ToDouble(ViewState["rate"]).ToString("0.00#");
            }
            set
            {
                ViewState["rate"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getType();
                rate = getRate();
            }
        }
        protected void getType()
        {
            SelectCommandBuilder s = new SelectCommandBuilder("Mode_type");
            s.SelectColumn("Mode_type_id");
            s.SelectColumn("Mode_type_name");
            s.ConditionsColumn("1", "1");
            s.getSelectCommand();
            DataTable d = s.ExecuteDataTable();
            ddlMode_Type.Items.Clear();
            ddlMode_Type.Items.Add(new ListItem("请选择", "0"));
            for (int i = 0; i < d.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(d.Rows[i]["Mode_type_name"].ToString().Trim()))
                {
                    ddlMode_Type.Items.Add(new ListItem(d.Rows[i]["Mode_type_name"].ToString().Trim(), d.Rows[i]["Mode_type_id"].ToString().Trim()));
                }
            }
            ddlMode_Type.DataTextField = "Mode_type_name";
        }
        protected string getRate()
        {
            string rate = string.Empty;
            string sql = "SELECT rate FROM Mode_rate ";
            rate = new SelectCommandBuilder().ExecuteDataTable(sql).Rows[0][0].ToString();
            string r = (double.Parse(rate) * 100).ToString() + "%";
            lblRate.Text = "税金" + "(" + r + ")";
            lblTotal.Text = "金额(含税" + r + ")";
            return rate;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (true)
            {
                if (ddlMode_Type.SelectedIndex == 0) return;
            }
            InsertCommandBuilder ins = new InsertCommandBuilder("Mold_management_accounting");
            string id = CommadMethod.getNextId("M" + DateTime.Now.ToString("yyyyMMddHHmm"), "");
            ins.InsertColumn("id", id);
            ins.InsertColumn("type_id", ddlMode_Type.SelectedItem.Value);
            ins.InsertColumn("mode", txtMode.Text.Trim());
            ins.InsertColumn("goods_name", txtGoods_Name.Text.Trim());
            ins.InsertColumn("goods_no", txtGoods_No.Text.Trim());
            ins.InsertColumn("qty", int.Parse(txtCount.Text.Trim()));
            ins.InsertColumn("quotation_number", txtQuotation_number.Text.Trim());
            ins.InsertColumn("price", double.Parse(txtPrice.Text));
            ins.InsertColumn("rate", double.Parse(txtRate.Value));
            ins.InsertColumn("total", double.Parse(txtTotal.Value));
            ins.InsertColumn("currency", ddlCurrency.Text);
            ins.InsertColumn("order_receipt_date", txtRecive_Date.Text);
            ins.InsertColumn("delivery_date", txtDelivery_Date.Text);
            ins.InsertColumn("tax_rate", double.Parse(rate));
            if (!string.IsNullOrEmpty(txtContent_Remark.Text))
            {
                ins.InsertColumn("content_remark", txtContent_Remark.Text);
            }
            if (!string.IsNullOrEmpty(txtdelivery_no.Text))
            {
                ins.InsertColumn("delivery_no", txtdelivery_no.Text);
            }
            if (!string.IsNullOrEmpty(txtcustomer_order_no.Text))
            {
                ins.InsertColumn("customer_order_no", txtcustomer_order_no.Text);
            }
            if (!string.IsNullOrEmpty(txtrevenue_time.Text))
            {
                ins.InsertColumn("revenue_time", Convert.ToDateTime(txtrevenue_time.Text).ToString("yyyy-MM-dd"));
            }
            if (!string.IsNullOrEmpty(txtreconciliation_time.Text))
            {
                ins.InsertColumn("reconciliation_time", txtreconciliation_time.Text);
            }
            if (!string.IsNullOrEmpty(txtreceipt_date.Text))
            {
                ins.InsertColumn("receipt_date", txtreceipt_date.Text);
            }
            if (!string.IsNullOrEmpty(txtRemark.Text))
            {
                ins.InsertColumn("remarks", txtRemark.Text);
            }
            ins.getInsertCommand();
            int count = ins.ExecuteNonQuery();
            if (count != 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "<script>alert('添加成功!')</script>");
                clearTextBox();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "<script>alert('添加失败!')</script>");
            }
        }
        protected void clearTextBox()
        {
            foreach (System.Web.UI.Control stl in this.Form.Controls)
            {
                if (stl is System.Web.UI.WebControls.TextBox)
                {
                    TextBox tb = (TextBox)stl;
                    tb.Text = string.Empty;
                }
            }
            txtRate.Value = string.Empty;
            txtTotal.Value = string.Empty;
            ddlMode_Type.SelectedIndex = 0;
        }
    }
}