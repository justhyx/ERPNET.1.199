using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using ERPPlugIn.Class;

namespace ERPPlugIn.InventoryManager
{
    public partial class Inventory_KeyInNoData : PageBase
    {
        protected List<AddData> iList
        {
            set { ViewState["iList"] = value; }
            get { return ViewState["iList"] as List<AddData>; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["bill_id"] = Request.QueryString["bill_id"];
                ViewState["bill_no"] = Request.QueryString["bill_no"];
                ViewState["s"] = Request.QueryString["status"];
                iList = new List<AddData>();
                iList.Clear();
                txtbpmc.Focus();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtbpmc.Text))
                {
                    txtbpmc.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txthwh.Text))
                {
                    txthwh.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtpch.Text))
                {
                    txtpch.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtpdsl.Text))
                {
                    txtpdsl.Focus();
                    return;
                }
                //getBatch_id(txtbpmc.Text, txtpch.Text);
                string sql = "select count(goods_id) from goods where goods_name = '" + txtbpmc.Text + "'";
                int a = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar(sql));
                if (a == 0)
                {
                    Response.Write("<script>alert('" + Resources.Resource.alertNodata + "')</script>");
                    return;
                }
                if (isCanAdd(txtbpmc.Text, txthwh.Text, ViewState["s"].ToString()))
                {
                    iList.Add(new AddData() { pm = txtbpmc.Text.Trim(), pch = txtpch.Text.Trim(), hwh = txthwh.Text.Trim(), pdsl = txtpdsl.Text.Trim() });
                }
                else
                {
                    Response.Write("<script>alert('货位号:" + txthwh.Text.ToUpper() + " 不应追加数据,因为它已经存在!')</script>");
                    return;
                }
                gvAddData.DataSource = iList;
                gvAddData.DataBind();
                txtbpmc.Text = string.Empty;
                txthwh.Text = string.Empty;
                txtpch.Text = string.Empty;
                txtpdsl.Text = string.Empty;
                txtbpmc.Focus();
            }
            catch (Exception ex)
            {
                //Response.Write("<script>alert('" + ex.Message + "')</script>");
                txtAlert.Text = ex.Message;
            }


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            txtAlert.Text = string.Empty;
            try
            {
                List<string> sqlList = new List<string>();
                InsertCommandBuilder ins = new InsertCommandBuilder("tmp_pk_detail");
                //InsertCommandBuilder insert = new InsertCommandBuilder("pre_prd_pk_detail");
                string Bill_id = CommadMethod.getNextId("PB", "0101");
                for (int i = 0; i < iList.Count; i++)
                {
                    List<string> sList = getBatch_id(iList[i].pm, iList[i].pch);
                    string detail_id = Bill_id + (i + 1).ToString().PadLeft(4, '0');
                    //string sql = "INSERT INTO tmp_pk_detail ( Bill_id, Batch_id, Goods_id, Qty, Pch, Detail_id, Price, is_can_sale, hwh ) VALUES ( '" + Bill_id + "', '盘盈入库', '3034', 500.000000, '无库存', 'PB2012072109381211401010938257800002', 1.225000, 'Y', '' )";
                    ins.CommandClear();
                    ins.InsertColumn("Bill_id", Bill_id);
                    ins.InsertColumn("Batch_id", sList[1].Trim());
                    ins.InsertColumn("Goods_id", sList[0].Trim());
                    ins.InsertColumn("Qty", iList[i].pdsl);
                    ins.InsertColumn("Pch", iList[i].pch);
                    ins.InsertColumn("Detail_id", detail_id);
                    ins.InsertColumn("is_can_sale", "Y");
                    ins.InsertColumn("hwh", iList[i].hwh);
                    sqlList.Add(ins.getInsertCommand());
                    //insert.CommandClear();                    
                    //insert.InsertColumn("prd_pk_id", ViewState["bill_id"]);
                    //insert.InsertColumn("stock_remain_id", ViewState["bill_no"]);
                    //insert.InsertColumn("materials_id", sList[0].Trim());
                    //insert.InsertColumn("pdsl", iList[i].pdsl);
                    //insert.InsertColumn("zmsl", 0);
                    //insert.InsertColumn("prd_batch_id", sList[1].Trim());
                    //insert.InsertColumn("zmsl1", 0);
                    //insert.InsertColumn("pdsl1", iList[i].pdsl);
                    //insert.InsertColumn("is_can_sale", "Y");
                    //insert.InsertColumn("detail_id", detail_id);
                    //sqlList.Add(insert.getInsertCommand());
                }
                ins.CommandClear();
                ins = new InsertCommandBuilder("tmp_pk_bill");
                ins.InsertColumn("Bill_Id", Bill_id);
                ins.InsertColumn("Bill_no", ViewState["bill_no"]);
                ins.InsertColumn("Pk_id", ViewState["bill_id"]);
                ins.InsertColumn("store_id", ViewState["s"]);
                ins.InsertColumn("Crt_emp", HttpContext.Current.Request.Cookies["cookie"].Values["id"]);
                ins.InsertColumn("Crt_Date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                ins.InsertColumn("Status", "N");
                ins.InsertColumn("isAdd", "Y");
                //ins.InsertColumn("Remark", txtRemark.Text.Trim().ToUpper());
                sqlList.Add(ins.getInsertCommand());
                int c = ins.ExcutTransaction(sqlList);
                if (c != 0)
                {
                    //Response.Write("<script>alert('" + Resources.Resource.alterOk + "')</script>");
                    txtAlert.Text = Resources.Resource.alterOk;
                    gvAddData.DataSource = null;
                    gvAddData.DataBind();
                    iList.Clear();
                }
                else
                {
                    //Response.Write("<script>alert('" + Resources.Resource.alterfiald + "')</script>");
                    txtAlert.Text = Resources.Resource.alterfiald;
                }
            }
            catch (Exception ex)
            {
                txtAlert.Text = ex.Message;
                //Response.Write("<script>alert('" + ex.Message + "')</script>");
            }

        }
        protected List<string> getBatch_id(string goodsName, string pch)
        {
            List<string> sList = new List<string>();
            string sql = "select goods_id from goods where goods_name = '" + goodsName + "'";
            string id = new SelectCommandBuilder().ExecuteDataTable(sql).Rows[0][0].ToString();
            sql = "select batch_Id from batch where goods_id='" + id + "' and pch = '" + pch + "'";
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            string batchId = "追加数据";
            if (dt != null && dt.Rows.Count != 0)
            {
                batchId = dt.Rows[0][0].ToString();
            }
            sList.Add(id);
            sList.Add(batchId);
            return sList;
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            btnAdd_Click(sender, e);
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inventory_KeyIn.aspx");
        }
        public bool isCanAdd(string goodsName, string hwh, string store_id)
        {
            bool flag = false;
            string sql = @"SELECT COUNT(*) AS COUNTA
                            FROM  goods INNER JOIN
                             stock_remain ON goods.goods_id = stock_remain.goods_id INNER JOIN
                             batch ON stock_remain.batch_id = batch.batch_id
                            WHERE (batch.hwh = '" + hwh + "') AND (goods.goods_name ='" + goodsName.Trim().ToUpper() + "') AND (stock_remain.store_id = '" + store_id + "')";
            int i = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar(sql));
            if (i == 0)
            {
                flag = true;
            }
            return flag;
        }
    }
}