using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERPPlugIn.StockInManager;
using System.Data;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using ERPPlugIn.Class;
using System.Configuration;

namespace ERPPlugIn.StockInManager
{
    public partial class StockIn_Confirm : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConXG_ZhuSu"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["Id"] = Request.QueryString["Id"];
                getTitle(ViewState["Id"].ToString());
                gvData.DataSource = getConfirmData(ViewState["Id"].ToString());
                gvData.DataBind();
            }
        }
        public List<inputData> getConfirmData(string str_id)
        {
            List<inputData> insList = new List<inputData>();
            string sql = @"SELECT goods_tmp.goods_name, goods_tmp.goods_unit, pre_str_in_bill_detail.qty, 
                                  pre_str_in_bill_detail.pch, pre_str_in_bill_detail.hwh
                            FROM pre_str_in_bill_detail INNER JOIN
                                  goods_tmp ON pre_str_in_bill_detail.goods_id = goods_tmp.goods_id INNER JOIN
                                  pre_str_in_bill ON pre_str_in_bill_detail.str_in_bill_id = pre_str_in_bill.str_in_bill_id where (pre_str_in_bill.str_in_bill_id = '" + str_id + "')";
            DataTable dt = new SelectCommandBuilder(constr, "").ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inputData indata = new inputData()
                    {
                        goods_name = dt.Rows[i]["goods_name"].ToString(),
                        unit = dt.Rows[i]["goods_unit"].ToString(),
                        Qty = Convert.ToInt32(dt.Rows[i]["qty"]),
                        batch = dt.Rows[i]["pch"].ToString(),
                        goodsPost = dt.Rows[i]["hwh"].ToString()
                    };
                    insList.Add(indata);
                }
            }
            return insList;
        }
        public void getTitle(string Id)
        {
            string sql = @"SELECT str_in_type.str_in_type_name, pre_str_in_bill.str_in_bill_no, 
                                      operator.operator_name, pre_str_in_bill.remark, pre_str_in_bill.str_in_bill_id, 
                                      pre_str_in_bill.operator_date
                                FROM pre_str_in_bill INNER JOIN
                                      str_in_type ON 
                                      pre_str_in_bill.str_in_type_id = str_in_type.str_in_type_id INNER JOIN
                                      operator ON pre_str_in_bill.operator_id = operator.operator_id
                                WHERE (pre_str_in_bill.is_state = 'Y') and (pre_str_in_bill.str_in_bill_id = '" + Id + "')";
            DataTable dt = new SelectCommandBuilder(constr,"").ExecuteDataTable(sql);
            sty.Text = dt.Rows[0]["str_in_type_name"].ToString();
            in_str_no.Text = dt.Rows[0]["str_in_bill_no"].ToString();
            do_date.Text = Convert.ToDateTime(dt.Rows[0]["operator_date"]).ToString("yyyy-MM-dd");
            oper.Text = dt.Rows[0]["operator_name"].ToString();
            note.Text = dt.Rows[0]["remark"].ToString();
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();

            string sql = @"INSERT INTO str_in_bill
                                  (str_in_bill_id, str_in_bill_no, str_in_type_id, operator_date, str_in_date, store_id, 
                                  operator_id, come_from, remark, islocal, ComputerName, is_end)
                            SELECT str_in_bill_id, str_in_bill_no, str_in_type_id, operator_date, str_in_date, store_id, 
                                  operator_id, come_from, remark, islocal, ComputerName, 'N' AS is_end
                            FROM pre_str_in_bill
                            WHERE (str_in_bill_id = '" + ViewState["Id"].ToString() + "')";
            string sql1 = @"INSERT INTO str_in_bill_Detail(str_in_bill_id, goods_id, batch_id, qty, Can_sale)
                            SELECT str_in_bill_id, goods_id, batch_id, qty, Can_sale
                            FROM pre_str_in_bill_detail where str_in_bill_id = '" + ViewState["Id"].ToString() + "'";
            string sql2 = @"INSERT INTO batch(batch_id, goods_id, pch,hwh,str_in_date)
                            SELECT batch_id, goods_id,pch,hwh,getdate()
                            FROM pre_str_in_bill_detail where str_in_bill_id ='" + ViewState["Id"].ToString() + "'";
            string B_Id = new SelectCommandBuilder(constr,"").ExecuteDataTable("SELECT RTRIM(goods_id) + RTRIM(hwh) + RTRIM(pch) AS SumId FROM pre_str_in_bill_detail where str_in_bill_id ='" + ViewState["Id"].ToString() + "'").Rows[0]["SumId"].ToString();
            DataTable dt = new SelectCommandBuilder(constr,"").ExecuteDataTable("SELECT stock_remain.goods_id, stock_remain.batch_id, stock_remain.store_id, stock_remain.qty, stock_remain.is_can_sale, stock_remain.stock_remain_id, batch.hwh FROM stock_remain INNER JOIN batch ON stock_remain.batch_id = batch.batch_id WHERE (RTRIM(stock_remain.goods_id) + batch.hwh + RTRIM(batch.pch)='" + B_Id + "')");
            DataTable ddt = new SelectCommandBuilder(constr,"").ExecuteDataTable("SELECT str_in_bill_id, goods_id, batch_id, qty, pch, Can_sale, hwh FROM pre_str_in_bill_detail where str_in_bill_id='" + ViewState["Id"].ToString() + "'");
            DataTable ddtt = new SelectCommandBuilder(constr,"").ExecuteDataTable("SELECT store_id,come_from FROM pre_str_in_bill where str_in_bill_id='" + ViewState["Id"].ToString() + "' ");
            if (dt != null && dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string stock_remain_id = dt.Rows[i]["stock_remain_id"].ToString();
                    int qty = Convert.ToInt32(dt.Rows[i]["qty"]);
                    sqlList.Add("update stock_remain set qty = qty +" + qty + "where stock_remain_id='" + stock_remain_id + "'");
                }
            }
            else
            {
                DateTime date = DateTime.Now;
                string IdLeftPart = "PR" + date.Year.ToString().Substring(2, 2) + date.Month.ToString().PadLeft(2, '0') + date.Day.ToString().PadLeft(2, '0');
                for (int i = 0; i < ddt.Rows.Count; i++)
                {
                    string Id = CommadMethod.getNextId(IdLeftPart, "");
                    InsertCommandBuilder insert = new InsertCommandBuilder(constr,"stock_remain");
                    insert.InsertColumn("stock_remain_id", Id);
                    insert.InsertColumn("goods_id", ddt.Rows[i]["goods_id"].ToString().Trim());
                    insert.InsertColumn("batch_id", ddt.Rows[i]["batch_id"].ToString().Trim());
                    insert.InsertColumn("store_id", ddtt.Rows[0]["store_id"].ToString().Trim());
                    insert.InsertColumn("qty", Convert.ToInt32(ddt.Rows[i]["qty"]));
                    insert.InsertColumn("is_can_sale", "Y");
                    sqlList.Add(insert.getInsertCommand());
                }
            }
            if (this.sty.Text.Trim() != "期初入库")
            {
                int InQty = Convert.ToInt32(ddt.Rows[0]["qty"]);
                DataTable df = new SelectCommandBuilder(constr,"").ExecuteDataTable("SELECT prd_qty, ISNULL(plan_get_qty, 0) AS plan_get_qty FROM prd_dictate WHERE (is_End = 'N') AND (dictate_id = '" + ddtt.Rows[0]["come_from"].ToString() + "')");
                int prd_qty = Convert.ToInt32(df.Rows[0]["prd_qty"]);
                int plan_get_qty = Convert.ToInt32(df.Rows[0]["plan_get_qty"]);
                if ((InQty + plan_get_qty) >= prd_qty)
                {
                    sqlList.Add("update prd_dictate set is_End = 'Y' where dictate_id = '" + ddtt.Rows[0]["come_from"].ToString() + "'");
                }
                string sql3 = "update prd_dictate set plan_get_qty = plan_get_qty +" + InQty + " where dictate_id = '" + ddtt.Rows[0]["come_from"].ToString() + "'";
                sqlList.Add(sql3);
            }
            string sql4 = "delete from pre_str_in_bill where str_in_bill_id='" + ViewState["Id"].ToString() + "'";
            string sql5 = "delete from pre_str_in_bill_detail where str_in_bill_id='" + ViewState["Id"].ToString() + "'";
            sqlList.Add(sql);
            sqlList.Add(sql1);
            sqlList.Add(sql2);
            sqlList.Add(sql4);
            sqlList.Add(sql5);
            int count = new InsertCommandBuilder(constr,"").ExcutTransaction(sqlList);
            if (count != 0)
            {
                Response.Redirect("StockIn_SaveOk.aspx", false);
            }
            else
            {
                getTitle(ViewState["Id"].ToString());
                gvData.DataSource = getConfirmData(ViewState["Id"].ToString());
                gvData.DataBind();
                Response.Write("<script>alert('操作失败')</script>");
            }
        }

        protected void btnCannel_Click(object sender, EventArgs e)
        {
            string sql1 = "delete from pre_str_in_bill where str_in_bill_id='" + ViewState["Id"].ToString() + "'";
            string sql2 = "delete from pre_str_in_bill_detail where str_in_bill_id='" + ViewState["Id"].ToString() + "'";
            List<string> sList = new List<string>();
            sList.Add(sql1);
            sList.Add(sql2);
            int count = new InsertCommandBuilder(constr,"").ExcutTransaction(sList);
            if (count != 0)
            {
                Response.Redirect("StockIn_SaveOk.aspx", false);
            }
            else
            {
                getTitle(ViewState["Id"].ToString());
                gvData.DataSource = getConfirmData(ViewState["Id"].ToString());
                gvData.DataBind();
                Response.Write("<script>alert('操作失败')</script>");
            }
        }
    }
}