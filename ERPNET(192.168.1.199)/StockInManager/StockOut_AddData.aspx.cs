using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using System.Configuration;

namespace ERPPlugIn.StockInManager
{
    public partial class StockOut_AddData : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConXG_ZhuSu"].ConnectionString;
        public static List<inputData> insList = new List<inputData>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                StockOutCreate a = (StockOutCreate)Context.Handler;
                lblNo.Text = a.No;
                ViewState["sql"] = a.sql;
                ViewState["str_out_bill_id"] = a.bill_id;
                insList.Clear();
                txtGoodsName.Focus();
                qty.Attributes.Add("onkeypress", "EnterTextBox('btnAdd')");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string msg = inputValidate();
            if (msg != "")
            {
                Response.Write("<script>alert('" + msg + "')</script>");
                return;
            }
            SelectCommandBuilder select = new SelectCommandBuilder(constr, "goods_tmp");
            select.SelectColumn("goods_id");
            select.ConditionsColumn("goods_name", txtGoodsName.Text.Trim().ToUpper());
            select.getSelectCommand();
            DataTable dt = select.ExecuteDataTable();
            string goods_id = "";
            if (dt == null || dt.Rows.Count == 0)
            {
                Response.Write("<script>alert('部番不存在')</script>");
                return;
            }
            else
            {
                goods_id = dt.Rows[0][0].ToString();
            }
            string sql = "SELECT sum( stock_remain.qty) as qty  FROM batch INNER JOIN stock_remain ON batch.batch_id = stock_remain.batch_id where (batch.hwh = '" + hwh.Value.Trim().ToUpper() + "') AND (stock_remain.goods_id = '" + goods_id + "') group by  stock_remain.goods_id,batch.hwh";
            DataTable ddt = new SelectCommandBuilder(constr, "").ExecuteDataTable(sql);
            if (ddt == null || ddt.Rows.Count == 0)
            {
                Response.Write("<script>alert('仓位错误')</script>");
                hwh.Focus();
                return;
            }
            else
            {
                if (Convert.ToInt32(qty.Value) > Convert.ToInt32(ddt.Rows[0]["qty"]))
                {
                    Response.Write("<script>alert('仓位数量不足')</script>");
                    qty.Focus();
                    return;
                }
            }
            inputData indata = new inputData()
            {
                goods_name = txtGoodsName.Text.Trim().ToUpper(),
                Qty = int.Parse(qty.Value.Trim()),
                unit = "pcs",
                goodsPost = hwh.Value.Trim().ToUpper(),
            };
            insList.Add(indata);
            gvData.DataSource = insList;
            gvData.DataBind();
            clearText();
        }

        protected void gvData_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvData.EditIndex = -1;
            gvData.DataSource = insList;
            gvData.DataBind();
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    ((LinkButton)e.Row.Cells[5].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除：\"" + (e.Row.Cells[1].FindControl("Label2") as Label).Text + "\"吗?')");
                }
            }
        }

        protected void gvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            insList.RemoveAt(e.RowIndex);
            gvData.DataSource = insList;
            gvData.DataBind();
        }

        protected void gvData_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvData.EditIndex = e.NewEditIndex;
            gvData.DataSource = insList;
            gvData.DataBind();
        }

        protected void gvData_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            insList[e.RowIndex].Qty = int.Parse((gvData.Rows[e.RowIndex].Cells[2].FindControl("qty") as TextBox).Text);
            insList[e.RowIndex].goodsPost = (gvData.Rows[e.RowIndex].Cells[5].FindControl("goodsPost") as TextBox).Text;
            gvData.EditIndex = -1;
            gvData.DataSource = insList;
            gvData.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (insList.Count == 0)
            {
                Response.Write("<script>alert('无数据保存,请先添加数据')</script>");
                return;
            }
            List<string> sqlList = new List<string>();
            sqlList.Add(ViewState["sql"].ToString());
            DateTime date = DateTime.Now;
            for (int i = 0; i < insList.Count; i++)
            {
                string goodsId = new SelectCommandBuilder(constr, "").ExecuteDataTable("select goods_id from goods_tmp where goods_name = '" + insList[i].goods_name + "'").Rows[0][0].ToString().Trim();
                string sql = @"SELECT stock_remain.goods_id, stock_remain.batch_id, stock_remain.store_id, 
                                      stock_remain.qty, batch.hwh
                                FROM batch INNER JOIN
                                      stock_remain ON batch.batch_id = stock_remain.batch_id
                                WHERE (batch.hwh = '" + insList[i].goodsPost + @"') AND (stock_remain.goods_id ='" + goodsId + "')";
                DataTable dt = new SelectCommandBuilder(constr, "").ExecuteDataTable(sql);
                string batchId = dt.Rows[0]["batch_id"].ToString();
                InsertCommandBuilder ins = new InsertCommandBuilder(constr,"pre_str_out_bill_Detail");
                ins.InsertColumn("str_out_bill_id", ViewState["str_out_bill_id"].ToString());
                ins.InsertColumn("goods_id", goodsId);
                ins.InsertColumn("batch_id", batchId);
                ins.InsertColumn("qty", insList[i].Qty);
                ins.InsertColumn("Can_sale", "Y");
                ins.InsertColumn("hwh", insList[i].goodsPost);
                sqlList.Add(ins.getInsertCommand());
            }
            int count = new InsertCommandBuilder(constr,"").ExcutTransaction(sqlList);
            if (count != 0)
            {
                Response.Write("<script>alert('保存成功')</script>");
                insList.Clear();
                Response.Redirect("StockIn_SaveOk.aspx");
            }
        }
        public string inputValidate()
        {
            string msg = "";
            if (string.IsNullOrEmpty(txtGoodsName.Text))
            {
                txtGoodsName.Focus();
                msg = "请输入部番";
            }
            else if (string.IsNullOrEmpty(qty.Value))
            {
                qty.Focus();
                msg = "请输入数量";
            }
            else if (string.IsNullOrEmpty(hwh.Value))
            {
                hwh.Focus();
                msg = "请输入货位号";
            }
            return msg;
        }
        public void clearText()
        {
            txtGoodsName.Text = string.Empty;
            qty.Value = string.Empty;
            hwh.Value = string.Empty;
            txtGoodsName.Focus();
        }

        protected void btnStock_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT goods_tmp.goods_name,
                                 (SELECT store_name FROM store WHERE store_id = stock_remain.store_id) AS store, 
                                  stock_remain.qty,batch.hwh
                            FROM batch INNER JOIN
                                  stock_remain ON batch.batch_id = stock_remain.batch_id INNER JOIN
                                  goods_tmp ON batch.goods_id = goods_tmp.goods_id ";
            if (txtGoodsName.Text != string.Empty)
            {
                sql += "where goods_name = '" + txtGoodsName.Text.Trim().ToUpper() + "'";
            }
            gvList.DataSource = new SelectCommandBuilder(constr, "").ExecuteDataTable(sql);
            gvList.DataBind();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["style"] = "Cursor:hand";
                String evt = Page.ClientScript.GetPostBackClientHyperlink(sender as GridView, "Select$" + e.Row.RowIndex.ToString()); e.Row.Attributes.Add("onclick", evt);
            }
        }

        protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvList.SelectedIndex != -1)
            {
                txtGoodsName.Text = gvList.SelectedRow.Cells[0].Text.Trim().ToUpper(); ;
                hwh.Value = gvList.SelectedRow.Cells[2].Text.Trim().ToUpper();
                qty.Value = gvList.SelectedRow.Cells[3].Text.Trim();
                txtGoodsName.Focus();
            }
        }
    }
}