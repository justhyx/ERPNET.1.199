using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data.SqlClient;
using System.Drawing;

namespace ERPPlugIn.StockManager
{
    public partial class Stock_Output : System.Web.UI.Page
    {
        public static DataTable ddt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCustomer();
                btnCheckVisible.Attributes["onclick"] = this.GetPostBackEventReference(this.btnCheckVisible) + ";this.disabled=true;";
            }
        }

        protected void btnCommit_Click(object sender, EventArgs e)
        {
            if (ddlCustomers.SelectedItem.Value.ToString() != "0" || !string.IsNullOrEmpty(txtPrapareID.Text))
            {
                List<Stock> sList = new List<Stock>();
                SelectCommandBuilder s = new SelectCommandBuilder();
                gvData.DataSource = null;
                gvData.DataBind();
                string sql = "select DISTINCT Prepare_goods_Id ,khdm,delivery_date from Goods_Up where 1=1";
                if (ddlCustomers.SelectedItem.Value.ToString() != "0")
                {
                    sql += " and khdm = '" + ddlCustomers.SelectedItem.Value.Trim() + "'";
                }
                if (!string.IsNullOrEmpty(txtPrapareID.Text))
                {
                    sql += " and Prepare_goods_Id ='" + txtPrapareID.Text.Trim().ToUpper() + "'";
                }
                SqlDataReader dr = s.ExecuteReader(sql + "order by delivery_date desc");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Stock stock = new Stock()
                        {
                            pId = dr.GetString(0),
                            khdm = dr.GetString(1),
                            Date = dr.GetDateTime(2).ToString("yyyy-MM-dd"),
                            Visible = getVisible(dr.GetString(0), dr.GetString(1)) == true ? "是/Y" : "否/N"
                        };
                        sList.Add(stock);
                    }
                }
                gvExcel.DataSource = sList;
                gvExcel.DataBind();
            }
            else
            {
                Response.Write("<script>alert('请输入查询条件!')</script>");
                ddlCustomers.Focus();
            }
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
        public DataTable getStockInput()
        {
            SelectCommandBuilder s = new SelectCommandBuilder("Goods_Up");
            s.SelectColumn("goods_name");
            s.SelectColumn("qty");
            s.SelectColumn("qty");
            s.ConditionsColumn("1", "1");
            s.getSelectCommand();
            return s.ExecuteDataTable();
        }

        protected void btnUpLoad_Click(object sender, EventArgs e)
        {
            Response.Redirect("Stock_Input.aspx");
        }
        public bool getVisible(string Pid, string khdm)
        {
            bool flag = false;

            string sql = @"SELECT goods.goods_name AS 部番, goods_up.qty AS 交货数,
                        ISNULL(delivery_plan.plan_qty, 0) AS 计划余数, SUM(xsht_detail.qty)
                        AS 订单余数
                        FROM goods INNER JOIN
                              goods_up ON goods.goods_name = goods_up.goods_name LEFT OUTER JOIN
                              xsht_detail ON goods.goods_id = xsht_detail.goods_id INNER JOIN
                              xsht ON xsht_detail.xsht_id = xsht.xsht_id LEFT OUTER JOIN
                                  (SELECT delivery_plan.goods_id,
                                       SUM(delivery_plan.plan_qty - ISNULL(delivery_plan.delivery_qty, 0))
                                       AS plan_qty
                                 FROM delivery_plan
                                 GROUP BY delivery_plan.goods_id) delivery_plan ON
                              goods.goods_id = delivery_plan.goods_id
                        WHERE (Goods_Up.Prepare_goods_Id ='" + Pid + @"') AND (Goods_Up.khdm ='" + khdm + @"') AND (xsht.is_end = 'M') 
                        GROUP BY goods.goods_name, goods_up.qty, delivery_plan.plan_qty ";
            SelectCommandBuilder s = new SelectCommandBuilder();
            ddt = s.ExecuteDataTable(sql);
            List<bool> blist = new List<bool>();
            if (ddt != null && ddt.Rows.Count != 0)
            {
                for (int i = 0; i < ddt.Rows.Count; i++)
                {

                    if (Convert.ToInt32(ddt.Rows[i][2]) >= Convert.ToInt32(ddt.Rows[i][1]) && Convert.ToInt32(ddt.Rows[i][3]) >= Convert.ToInt32(ddt.Rows[i][1]))
                    {
                        blist.Add(true);
                        ListRemove(ddt);
                        flag = true;
                    }
                    else
                    {
                        blist.Add(false);
                    }
                }
            }
            for (int i = 0; i < blist.Count; i++)
            {
                if (blist[i] == false)
                {
                    flag = false;
                }
            }
            return flag;
        }

        private static void ListRemove(DataTable list)
        {
            for (int i = 0; i < list.Rows.Count; i++) 
            {
                if (Convert.ToInt32(ddt.Rows[i][2]) >= Convert.ToInt32(ddt.Rows[i][1]) && Convert.ToInt32(ddt.Rows[i][3]) >= Convert.ToInt32(ddt.Rows[i][1]))
                {
                    list.Rows.RemoveAt(i);
                    ListRemove(list);
                }
            }
        }

        protected void gvExcel_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < gvExcel.Rows.Count; i++)
            {
                if (((Label)gvExcel.Rows[i].Cells[3].FindControl("Label4")).Text == "否/N")
                {
                    ((HyperLink)gvExcel.Rows[i].Cells[4].FindControl("linkId")).Enabled = false;
                    ((Label)gvExcel.Rows[i].Cells[3].FindControl("Label4")).ForeColor = Color.Red;
                }
                else
                {
                    ((HyperLink)gvExcel.Rows[i].Cells[4].FindControl("linkId")).Enabled = true;
                }
            }             
        }

        protected void btnCheckVisible_Click(object sender, EventArgs e)
        {
            btnCheckVisible.Attributes["onclick"] = this.GetPostBackEventReference(this.btnCheckVisible) + ";this.disabled=true;";
            btnCommit_Click(sender, e);
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            gvData.DataSource = ddt;
            gvData.DataBind();
        }

        protected void gvExcel_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            getVisible(((Label)gvExcel.Rows[e.RowIndex].Cells[0].FindControl("Label2")).Text, ((Label)gvExcel.Rows[e.RowIndex].Cells[0].FindControl("Label1")).Text);
            gvData.DataSource = ddt;
            gvData.DataBind();
        }
    }
    public class Stock
    {
        public string pId { get; set; }
        public string khdm { get; set; }
        public string Date { get; set; }
        public string Visible { get; set; }
    }
}