using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ERPPlugIn.MaterialControlManager
{

    public partial class BarcodeChk : System.Web.UI.Page
    {
        public DataTable MyDT { get { return ViewState["MyDT"] as DataTable; } set { ViewState["MyDT"] = value; } }
        private static string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MyDT = new DataTable();
                BindCustomer();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStartDate.Text))
            {
                txtStartDate.Focus();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('请选择开始扫描时间')</script>", false);
                return;
            }
            else if (string.IsNullOrEmpty(txtEndDate.Text))
            {
                txtEndDate.Focus();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('请选择扫描完成时间')</script>", false);
                return;
            }
            string sql = @"SELECT BarCodeRecords.goods_name, BarCodeRecords.qty, BarCodeRecords.pdate, 
                                      BarCodeRecords.sn, BarCodeRecords.equip, '' AS Record
                                FROM BarCodeRecords INNER JOIN
                                      goods ON BarCodeRecords.goods_name = goods.goods_name
                                WHERE (BarCodeRecords.create_date BETWEEN CONVERT(DATETIME, 
                                      '" + Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd hh:mm") + "', 102) AND CONVERT(DATETIME, '" + Convert.ToDateTime(txtEndDate.Text).ToString("yyyy-MM-dd hh:mm") + @"', 102)) AND 
                                      (goods.khdm = '" + ddlCustomers.SelectedItem.Value + @"')
                                ORDER BY BarCodeRecords.pdate";
            MyDT = new SelectCommandBuilder().ExecuteDataTable(sql);
            gvShowData.DataSource = MyDT;
            gvShowData.DataBind();
        }
        private void BindCustomer()
        {
            DataTable d = getCustomers();
            ddlCustomers.Items.Clear();
            ddlCustomers.Items.Add(new ListItem("请选择", "0"));
            for (int i = 0; i < d.Rows.Count; i++)
            {
                ddlCustomers.Items.Add(new ListItem(d.Rows[i]["customer_aname"].ToString(), d.Rows[i]["customer_id"].ToString()));
            }
            ddlCustomers.DataTextField = "customer_aname";
        }
        protected DataTable getCustomers()
        {
            SelectCommandBuilder s = new SelectCommandBuilder("customer");
            s.SelectColumn("customer_id");
            s.SelectColumn("customer_aname");
            s.ConditionsColumn("1", "1");
            s.getSelectCommand();
            return s.ExecuteDataTable();
        }
        protected void btnChk_Click(object sender, EventArgs e)
        {
            if (gvShowData.Rows.Count != 0)
            {
                SelectCommandBuilder select = new SelectCommandBuilder();
                for (int i = 0; i < gvShowData.Rows.Count; i++)
                {
                    SqlParameter[] parm = new SqlParameter[] {
                    new SqlParameter("@goods_name",gvShowData.Rows[i].Cells[0].Text.ToString().Trim()),
                    new SqlParameter("@stock_qty",gvShowData.Rows[i].Cells[1].Text.ToString().Trim()),
                    new SqlParameter("@produce_date",DateTime.Now.Year.ToString().Substring(0,2)+ gvShowData.Rows[i].Cells[2].Text.ToString().Trim())};
                    object j = SqlHelper.ExecuteScalar(constr, CommandType.StoredProcedure, "Usp_Chk_Stock_Out", parm);
                    //if (j.ToString() == "OK")
                    //{
                    //}
                    //else
                    //{
                    //}
                }
                for (int i = 0; i < MyDT.Rows.Count; i++)
                {
                    SelectCommandBuilder s = new SelectCommandBuilder();
                    string dsql = @"SELECT DISTINCT RIGHT(RTRIM(batch.pch), 6) AS pch
                                    FROM stock_remain INNER JOIN
                                          batch ON stock_remain.batch_id = batch.batch_id INNER JOIN
                                          goods ON stock_remain.goods_id = goods.goods_id
                                    WHERE (stock_remain.store_id = '03') AND (goods.goods_name = '" + MyDT.Rows[i][0].ToString() + @"') AND 
                                          (batch.Out_Flag = 'N')AND (batch.pch < '" + MyDT.Rows[i][2].ToString().Trim() + "')ORDER BY right(rtrim(batch.pch),6)";
                    DataTable dd = s.ExecuteDataTable(dsql);
                    if (dd != null && dd.Rows.Count != 0)
                    {
                        for (int j = 0; j < dd.Rows.Count; j++)
                        {
                            MyDT.Rows[i][5] += dd.Rows[j][0].ToString() + ",";
                        }
                    }
                }
                gvShowData.DataSource = MyDT;
                gvShowData.DataBind();

            }
        }
        protected string getpch(string goods_name, string pch)
        {
            string result = "";
            string sql = @"SELECT batch.pch 
                            FROM stock_remain INNER JOIN
                                  batch ON stock_remain.batch_id = batch.batch_id INNER JOIN
                                  goods ON stock_remain.goods_id = goods.goods_id
                            WHERE (stock_remain.store_id = '03') AND (goods.goods_name = '" + goods_name + "') AND (batch.pch < '" + pch + "') ORDER BY batch.pch";
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    result += dt.Rows[i][0].ToString() + ",";
                }
            }
            return result;
        }
    }
}