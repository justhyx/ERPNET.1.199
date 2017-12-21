using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;

namespace ERPPlugIn.Mold_Management_Accounting
{
    public partial class Mode_Modify : System.Web.UI.Page
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
        public string C
        {
            get
            {
                return ViewState["C"].ToString();
            }
            set
            {
                ViewState["C"] = value;
            }
        }
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
                getType();
                Id = Request.QueryString["id"];
                getItemData(Id);
                getReceiptData(Id);
                getRate();
            }
        }
        protected void getRate()
        {
            string r = (double.Parse(rate) * 100).ToString() + "%";
            lblRate.Text = "税金" + "(" + r + ")";
            lblTotal.Text = "金额(含税" + r + ")";
        }
        protected void getItemData(string Id)
        {
            string sql = @"SELECT id, type_id, mode, goods_name, goods_no, qty, quotation_number, price, rate, 
                          total, currency, CONVERT(varchar(11), order_receipt_date, 120) 
                          AS order_receipt_date, CONVERT(varchar(11), delivery_date, 120) AS delivery_date, 
                          content_remark, delivery_no, customer_order_no, CONVERT(varchar(7), revenue_time, 120) AS revenue_time, 
                          CONVERT(varchar(11), reconciliation_time, 120) AS reconciliation_time, CONVERT(varchar(11), receipt_date, 120) AS receipt_date, remarks,tax_rate
                    FROM Mold_management_accounting where Id = '" + Id + "'";
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count != 0)
            {
                ddlMode_Type.SelectedIndex = getTypeName(dt.Rows[0]["type_id"].ToString());
                txtMode.Text = dt.Rows[0]["mode"].ToString();
                txtGoods_Name.Text = dt.Rows[0]["goods_name"].ToString();
                txtGoods_No.Text = dt.Rows[0]["goods_no"].ToString();
                txtCount.Text = dt.Rows[0]["qty"].ToString();
                txtQuotation_number.Text = dt.Rows[0]["quotation_number"].ToString();
                txtPrice.Text = dt.Rows[0]["price"].ToString();
                txtRate.Value = dt.Rows[0]["rate"].ToString();
                txtTotal.Value = dt.Rows[0]["total"].ToString();
                ddlCurrency.SelectedIndex = getCurrency(dt.Rows[0]["currency"].ToString());
                txtRecive_Date.Text = dt.Rows[0]["order_receipt_date"].ToString();
                txtDelivery_Date.Text = dt.Rows[0]["delivery_date"].ToString();
                txtContent_Remark.Text = dt.Rows[0]["content_remark"].ToString();
                txtdelivery_no.Text = dt.Rows[0]["delivery_no"].ToString();
                txtcustomer_order_no.Text = dt.Rows[0]["customer_order_no"].ToString();
                if (dt.Rows[0]["revenue_time"].ToString() != "")
                {
                    DateTime d = Convert.ToDateTime(dt.Rows[0]["revenue_time"].ToString());
                    txtrevenue_time.Text = d.Year + "年" + d.Month + "月";
                }
                txtreconciliation_time.Text = dt.Rows[0]["reconciliation_time"].ToString();
                txtreceipt_date.Text = dt.Rows[0]["receipt_date"].ToString();
                txtRemark.Text = dt.Rows[0]["remarks"].ToString();
                rate = dt.Rows[0]["tax_rate"].ToString();
            }
        }
        protected int getTypeName(string id)
        {
            int index = 0;
            for (int i = 0; i < ddlMode_Type.Items.Count; i++)
            {
                if (id == ddlMode_Type.Items[i].Value)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        protected int getCurrency(string currency)
        {
            int index = 0;
            for (int i = 0; i < ddlCurrency.Items.Count; i++)
            {
                if (currency.Trim() == ddlCurrency.Items[i].Text)
                {
                    index = i;
                    C = ddlCurrency.Items[i].Text;
                    break;
                }
            }
            return index;
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
        protected void getReceiptData(string M_id)
        {
            string sql = "SELECT isnull(SUM(Amount),0) AS Amount FROM Mode_receipt_accounting WHERE (M_id = '" + M_id + "')";
            double Sum = Convert.ToDouble(new SelectCommandBuilder().ExecuteScalar(sql));
            string c = "";
            if (C == "RMB")
            {
                c = "￥";
            }
            else if (C == "USD")
            {
                c = "$";
            }
            else if (C == "HKD")
            {
                c = "HK$";
            }
            lblYiShou.Text = c + Sum.ToString("0.00#");
            double Total = double.Parse(txtTotal.Value);
            lblWeiShou.Text = c + (Total - Sum).ToString("0.00#");
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            UpdateCommandBuilder ins = new UpdateCommandBuilder("Mold_management_accounting");
            ins.UpdateColumn("type_id", ddlMode_Type.SelectedItem.Value);
            ins.UpdateColumn("mode", txtMode.Text.Trim());
            ins.UpdateColumn("goods_name", txtGoods_Name.Text.Trim());
            ins.UpdateColumn("goods_no", txtGoods_No.Text.Trim());
            ins.UpdateColumn("qty", int.Parse(txtCount.Text.Trim()));
            ins.UpdateColumn("quotation_number", txtQuotation_number.Text.Trim());
            ins.UpdateColumn("price", double.Parse(txtPrice.Text));
            ins.UpdateColumn("rate", double.Parse(txtRate.Value));
            ins.UpdateColumn("total", double.Parse(txtTotal.Value));
            ins.UpdateColumn("currency", ddlCurrency.Text);
            ins.UpdateColumn("order_receipt_date", txtRecive_Date.Text);
            ins.UpdateColumn("delivery_date", txtDelivery_Date.Text);
            //ins.UpdateColumn("tax_rate", double.Parse(rate));
            if (!string.IsNullOrEmpty(txtContent_Remark.Text))
            {
                ins.UpdateColumn("content_remark", txtContent_Remark.Text);
            }
            if (!string.IsNullOrEmpty(txtdelivery_no.Text))
            {
                ins.UpdateColumn("delivery_no", txtdelivery_no.Text);
            }
            if (!string.IsNullOrEmpty(txtcustomer_order_no.Text))
            {
                ins.UpdateColumn("customer_order_no", txtcustomer_order_no.Text);
            }
            if (!string.IsNullOrEmpty(txtrevenue_time.Text))
            {
                ins.UpdateColumn("revenue_time", Convert.ToDateTime(txtrevenue_time.Text).ToString("yyyy-MM-dd"));
            }
            if (!string.IsNullOrEmpty(txtreconciliation_time.Text))
            {
                ins.UpdateColumn("reconciliation_time", txtreconciliation_time.Text);
            }
            if (!string.IsNullOrEmpty(txtreconciliation_time.Text))
            {
                ins.UpdateColumn("invoice_date", txtreconciliation_time.Text);
            }
            if (!string.IsNullOrEmpty(txtRemark.Text))
            {
                ins.UpdateColumn("remarks", txtRemark.Text);
            }
            ins.ConditionsColumn("id", Id);
            ins.getUpdateCommand();
            int count = ins.ExecuteNonQuery();
            if (count != 0)
            {
                getItemData(Id);
                getReceiptData(Id);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "<script>alert('保存成功!');</script>");
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "<script>alert('保存失败!');</script>");
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Mode_List.aspx");
        }


    }
}