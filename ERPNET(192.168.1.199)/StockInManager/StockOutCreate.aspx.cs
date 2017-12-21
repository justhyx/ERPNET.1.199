using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using ERPPlugIn.Class;
using System.Configuration;

namespace ERPPlugIn.StockInManager
{
    public partial class StockOutCreate : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConXG_ZhuSu"].ConnectionString;
        public string sql { get; set; }
        public string No { get; set; }
        public string bill_id { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void rbtType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMaterialStock.SelectedItem.Text == "无")
            {
                bindDDL();
                txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ddlMaterialStock.Focus();
            }
        }
        protected void bindDDL()
        {
            string sql = "SELECT store_id, store_name FROM store";
            SelectCommandBuilder s = new SelectCommandBuilder(constr,"");
            DataTable dt = s.ExecuteDataTable(sql);
            ddlMaterialStock.Items.Clear();
            ddlMaterialStock.Items.Add(new ListItem("请选择", "0"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddlMaterialStock.Items.Add(new ListItem(dt.Rows[i]["store_name"].ToString(), dt.Rows[i]["store_id"].ToString()));
            }
        }

        protected void ddlMaterialStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNo.Focus();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (ddlMaterialStock.SelectedItem.Value == "0")
            {
                Response.Write("<script>alert('请选择仓位点')</script>");
                return;
            }
            if (txtNo.Text.Trim().Equals(string.Empty))
            {
                Response.Write("<script>alert('请输入单号')</script>");
                return;
            }
            DateTime date = DateTime.Now;
            string IdLeftPart = "HC" + date.Year.ToString().Substring(2, 2) + date.Month.ToString().PadLeft(2, '0') + date.Day.ToString().PadLeft(2, '0');
            string str_out_bill_id = CommadMethod.getNextId(IdLeftPart, "");
            InsertCommandBuilder ins = new InsertCommandBuilder(constr,"pre_str_out_bill");
            ins.InsertColumn("str_out_bill_id", str_out_bill_id);
            ins.InsertColumn("str_out_bill_no", txtNo.Text.Trim().ToUpper());
            ins.InsertColumn("str_out_type_id", rbtType.SelectedItem.Value);
            ins.InsertColumn("str_out_date", txtDate.Text);
            ins.InsertColumn("store_id", ddlMaterialStock.SelectedItem.Value);
            ins.InsertColumn("operator_id", "0000");
            ins.InsertColumn("remark", txtRemark.Text.Trim());
            ins.InsertColumn("operator_date", txtDate.Text);
            ins.InsertColumn("come_to", rbtType.SelectedItem.Text);
            ins.InsertColumn("islocal", "Y");
            ins.InsertColumn("save_state", "Y");
            sql = ins.getInsertCommand();
            No = txtNo.Text.Trim();
            bill_id = str_out_bill_id;
            Server.Transfer("StockOut_AddData.aspx");
        }
    }
}