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
    public partial class StockOut_Confirm : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConXG_ZhuSu"].ConnectionString;
        public string out_str_no { get; set; }
        public string do_date { get; set; }
        public string oper { get; set; }
        public string note { get; set; }
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
        public void getTitle(string Id)
        {
            string sql = @"SELECT pre_str_out_bill.str_out_bill_id, pre_str_out_bill.str_out_date, 
                                  pre_str_out_bill.remark, operator.operator_name,pre_str_out_bill.str_out_bill_no
                            FROM pre_str_out_bill INNER JOIN
                                  operator ON pre_str_out_bill.operator_id = operator.operator_id
                            WHERE (pre_str_out_bill.str_out_bill_Id = '" + Id + "')";
            DataTable dt = new SelectCommandBuilder(constr, "").ExecuteDataTable(sql);
            lblNo.Text = dt.Rows[0]["str_out_bill_no"].ToString();
            lblDate.Text = Convert.ToDateTime(dt.Rows[0]["str_out_date"]).ToString("yyyy-MM-dd");
            lblOper.Text = dt.Rows[0]["operator_name"].ToString();
            lblRemark.Text = dt.Rows[0]["remark"].ToString();
        }
        public List<inputData> getConfirmData(string str_id)
        {
            List<inputData> insList = new List<inputData>();
            string sql = @"SELECT goods_tmp.goods_name, pre_str_out_bill_detail.qty, pre_str_out_bill_detail.hwh
                            FROM pre_str_out_bill_detail INNER JOIN
                                  goods_tmp ON pre_str_out_bill_detail.goods_id = goods_tmp.goods_id INNER JOIN
                                  pre_str_out_bill ON 
                                  pre_str_out_bill_detail.str_out_bill_id = pre_str_out_bill.str_out_bill_id
                            WHERE (pre_str_out_bill.str_out_bill_id = '" + str_id + "')";
            DataTable dt = new SelectCommandBuilder(constr, "").ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inputData indata = new inputData()
                    {
                        goods_name = dt.Rows[i]["goods_name"].ToString(),
                        Qty = Convert.ToInt32(dt.Rows[i]["qty"]),
                        goodsPost = dt.Rows[i]["hwh"].ToString()
                    };
                    insList.Add(indata);
                }
            }
            return insList;
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();
            SelectCommandBuilder select = new SelectCommandBuilder(constr, "");
            string sql = "SELECT goods_id, qty, hwh FROM pre_str_out_bill_detail where str_out_bill_id='" + ViewState["Id"].ToString() + "'";
            DataTable DToutDetial = select.ExecuteDataTable(sql);
            if (DToutDetial != null && DToutDetial.Rows.Count != 0)
            {
                for (int i = 0; i < DToutDetial.Rows.Count; i++)
                {
                    string goodsId = DToutDetial.Rows[i]["goods_id"].ToString();
                    int dQty = Convert.ToInt32(DToutDetial.Rows[i]["qty"]);
                    string hwh = DToutDetial.Rows[i]["hwh"].ToString();
                    string sql1 = "SELECT stock_remain.goods_id, stock_remain.batch_id, stock_remain.qty,stock_remain.store_id, batch.hwh, batch.pch FROM stock_remain INNER JOIN batch ON stock_remain.batch_id = batch.batch_id where stock_remain.goods_id='" + goodsId + "' and batch.hwh='" + hwh + "' ORDER BY stock_remain.str_in_date";
                    DataTable DTQty = select.ExecuteDataTable(sql1);
                    int diffqty = 0;
                    for (int j = 0; j < DTQty.Rows.Count; j++)
                    {
                        diffqty = dQty - Convert.ToInt32(DTQty.Rows[j]["qty"]);
                        if (diffqty == 0)
                        {
                            InsertCommandBuilder ins = new InsertCommandBuilder(constr, "str_out_bill_detail");
                            ins.InsertColumn("str_out_bill_id", ViewState["Id"].ToString());
                            ins.InsertColumn("goods_id", DTQty.Rows[j]["goods_id"].ToString());
                            ins.InsertColumn("batch_id", DTQty.Rows[j]["batch_id"].ToString());
                            ins.InsertColumn("qty", dQty);
                            ins.InsertColumn("Can_sale", "Y");
                            ins.InsertColumn("delivery_plan_id", "");
                            sqlList.Add(ins.getInsertCommand());
                            string Rsql = "delete from stock_remain where goods_id='" + DTQty.Rows[j]["goods_id"].ToString() + "' and batch_id='" + DTQty.Rows[j]["batch_id"].ToString() + "'";
                            string Dsql = "delete from batch where goods_id='" + DTQty.Rows[j]["goods_id"].ToString() + "' and batch_id='" + DTQty.Rows[j]["batch_id"].ToString() + "'";
                            sqlList.Add(Rsql);
                            sqlList.Add(Dsql);
                            break;
                        }
                        else if (diffqty > 0)
                        {
                            InsertCommandBuilder ins = new InsertCommandBuilder(constr, "str_out_bill_detail");
                            ins.InsertColumn("str_out_bill_id", ViewState["Id"].ToString());
                            ins.InsertColumn("goods_id", DTQty.Rows[j]["goods_id"].ToString());
                            ins.InsertColumn("batch_id", DTQty.Rows[j]["batch_id"].ToString());
                            ins.InsertColumn("qty", Convert.ToInt32(DTQty.Rows[j]["qty"]));
                            ins.InsertColumn("Can_sale", "Y");
                            ins.InsertColumn("delivery_plan_id", "");
                            string Rsql = "delete from stock_remain where goods_id='" + DTQty.Rows[j]["goods_id"].ToString() + "' and batch_id='" + DTQty.Rows[j]["batch_id"].ToString() + "'";
                            string Dsql = "delete from batch where goods_id='" + DTQty.Rows[j]["goods_id"].ToString() + "' and batch_id='" + DTQty.Rows[j]["batch_id"].ToString() + "'";
                            sqlList.Add(ins.getInsertCommand());
                            sqlList.Add(Rsql);
                            sqlList.Add(Dsql);
                            dQty = diffqty;
                            continue;
                        }
                        else if (diffqty < 0)
                        {
                            InsertCommandBuilder ins = new InsertCommandBuilder(constr, "str_out_bill_detail");
                            ins.InsertColumn("str_out_bill_id", ViewState["Id"].ToString());
                            ins.InsertColumn("goods_id", DTQty.Rows[j]["goods_id"].ToString());
                            ins.InsertColumn("batch_id", DTQty.Rows[j]["batch_id"].ToString());
                            ins.InsertColumn("qty", diffqty);
                            ins.InsertColumn("Can_sale", "Y");
                            ins.InsertColumn("delivery_plan_id", "");
                            sqlList.Add(ins.getInsertCommand());
                            string Usql = "update stock_remain set qty = '" + Math.Abs(diffqty) + "' where goods_id='" + DTQty.Rows[j]["goods_id"].ToString() + "' and batch_id='" + DTQty.Rows[j]["batch_id"].ToString() + "'";
                            sqlList.Add(Usql);
                            break;
                        }
                    }
                }
            }
            sqlList.Add("insert into str_out_bill(str_out_bill_id,str_out_bill_no,str_out_type_id,str_out_date,operator_date,operator_id,come_to,remark) select str_out_bill_id,str_out_bill_no,lxrid,gatherdate,operator_date,operator_id,lxrname,remark  from  pre_str_out_bill where str_out_bill_id='" + ViewState["Id"].ToString() + "'");
            sqlList.Add("delete from pre_str_out_bill_detail where str_out_bill_id='" + ViewState["Id"].ToString() + "'");
            sqlList.Add("delete from pre_str_out_bill where str_out_bill_id='" + ViewState["Id"].ToString() + "'");
            int count = new InsertCommandBuilder(constr, "").ExcutTransaction(sqlList);
            if (count != 0)
            {
                Response.Redirect("StockIn_SaveOk.aspx", false);
            }
            else
            {
                gvData.DataSource = getConfirmData(ViewState["Id"].ToString());
                gvData.DataBind();
                Response.Write("<script>alert('操作失败')</script>");
            }

        }

        protected void btnCannel_Click(object sender, EventArgs e)
        {
            string sql1 = "delete from pre_str_out_bill where str_out_bill_id='" + ViewState["Id"].ToString() + "'";
            string sql2 = "delete from pre_str_out_bill_detail where str_out_bill_id='" + ViewState["Id"].ToString() + "'";
            List<string> sList = new List<string>();
            sList.Add(sql1);
            sList.Add(sql2);
            int count = new InsertCommandBuilder(constr, "").ExcutTransaction(sList);
            if (count != 0)
            {
                Response.Redirect("StockIn_SaveOk.aspx", false);
            }
            else
            {
                Response.Write("<script>alert('操作失败')</script>");
            }
        }
    }
}