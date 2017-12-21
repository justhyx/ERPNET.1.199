using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using ERPPlugIn.Class;
using System.Data.SqlClient;
using System.Configuration;

namespace ERPPlugIn.InventoryManager
{
    public partial class Inventory_FinalConfirm : PageBase
    {
        public Dictionary<string, int> dicRemainQty { get { return ViewState["dic"] as Dictionary<string, int>; } set { ViewState["dic"] = value; } }
        public string constr { get { return ViewState["constr"].ToString(); } set { ViewState["constr"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindDDL();
                constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                dicRemainQty = new Dictionary<string, int>();
            }
        }
        protected void bindDDL()
        {
            string sql = "SELECT store_id, " + Resources.Resource.store + " FROM store";
            SelectCommandBuilder s = new SelectCommandBuilder();
            DataTable dt = s.ExecuteDataTable(sql);
            ddlMaterialStock.Items.Clear();
            ddlMaterialStock.Items.Add(new ListItem(Resources.Resource.qxz, "0"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddlMaterialStock.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i]["store_id"].ToString()));
            }
        }
        protected void ddlMaterialStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvData.DataSource = getData(ddlMaterialStock.SelectedItem.Value.Trim());
            gvData.DataBind();
            gvDetailData.DataSource = null;
            gvDetailData.DataBind();
        }
        public DataTable getData(string store)
        {
            string sql = "SELECT DISTINCT(SELECT s." + Resources.Resource.store + " FROM store s  WHERE a.store_id = s.store_id) AS '" + Resources.Resource.clck + @"', a.pk_id as '" + Resources.Resource.pddh + @"', CONVERT(varchar(100), a.pk_date, 20) as '" + Resources.Resource.pdrq + @"','" + Resources.Resource.czry + @"'=(select operator_name from operator o where o.operator_id =a.operator_id),a.remark as '" + Resources.Resource.bz + @"' FROM pre_pk a join tmp_pk_bill b on a.pk_id=b.pk_id where a.store_id='" + store + "' and b.status = 'Y'";
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            return dt;
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //----------鼠标移动到每项时颜色交替效果
                //e.Row.Attributes["onmouseover"] = "e=this.style.backgroundColor;this.style.backgroundColor='#FFFF99'";
                //e.Row.Attributes["onmouseout"] = "this.style.backgroundColor=e";
                //----------设置悬浮鼠标指针形状为小手"  
                e.Row.Attributes["style"] = "Cursor:hand";
                String evt = Page.ClientScript.GetPostBackClientHyperlink(sender as GridView, "Select$" + e.Row.RowIndex.ToString()); e.Row.Attributes.Add("onclick", evt);
            }
        }

        protected void gvData_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = @"SELECT goods.dm,goods.goods_name, goods.spec,sccj.sccj_name, batch.pch,goods.goods_unit,pre_pk_detail.zmsl,pre_pk_detail.pdsl,goods.szb,batch.yxq, batch.str_in_date,batch.price, pre_pk_detail.zmsl1, pre_pk_detail.pdsl1,goods.select_code,sprice = price.price,
			                            'N' as disobey, batch.batch_id, pre_pk_detail.is_can_sale,goods.goods_id,pre_pk_detail.stock_remain_id ,prd_dictate.style, prd_dictate.dictate_name ,customer_name = (select bb.customer_name 
									    from customer bb where  bb.customer_id = goods.khdm ),goods.cz,goods.ys,batch.hwh FROM batch,goods,sccj,price,pre_pk_detail,prd_dictate WHERE pre_pk_detail.goods_id = goods.goods_id and  
			                            pre_pk_detail.goods_id *= price.goods_id and
			                            batch.dictate_id *= prd_dictate.dictate_id and 
			                            goods.sccj_id *= sccj.sccj_id and 
			                            price.pos_id = '' and
			                            price.price_type_id = '' and 
			                            price.price <> -2 and 
			                            pre_pk_detail.batch_id = batch.batch_id and  
			                            pre_pk_detail.pk_id = '" + gvData.SelectedRow.Cells[1].Text.Trim() + "' ";
            gvDetailData.DataSource = new SelectCommandBuilder().ExecuteDataTable(sql);
            gvDetailData.DataBind();
        }

        protected void gvDetailData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)// 判断当前项是否为页脚
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[0].Text = "Count:" + gvDetailData.Rows.Count.ToString();
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[1].Text = Resources.Resource.hj + "：";
                e.Row.Cells[7].Text = getLabelSum(gvDetailData, 7).ToString("0.##");
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[8].Text = getLabelSum(gvDetailData, 8).ToString("0.##");
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[10].Text = getLabelSum(gvDetailData, 10).ToString("0.##");
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Left;
            }
        }
        public decimal getLabelSum(GridView gv, int colIndex)
        {
            decimal sum = 0;
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                sum += Convert.ToDecimal(gv.Rows[i].Cells[colIndex].Text);
            }
            return sum;
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {

            List<string> sqlList = new List<string>();
            if (gvData.SelectedIndex == -1)
            {
                Response.Write("<script>alert('" + Resources.Resource.alterwxz + "!')</script>");
                return;
            }
            string store_id = ddlMaterialStock.SelectedItem.Value.Trim();
            string pk_id = gvData.SelectedRow.Cells[1].Text.ToString().Trim().ToUpper();
            SqlParameter[] parm = new SqlParameter[]
            {
                new SqlParameter("@store_id",store_id),
                new SqlParameter("@pk_id",pk_id),
                new SqlParameter("@operator_id",HttpContext.Current.Request.Cookies["cookie"].Values["id"])
            };
            int pCount = Convert.ToInt32(SqlHelper.ExecuteScalar(constr, CommandType.StoredProcedure, "Usp_Goods_storage_update", parm));
            if (pCount != 0)
            {
                DataTable rdt1 = new SelectCommandBuilder().ExecuteDataTable("SELECT goods_id, sum(Qty) as 'Qty' FROM stock_remain  where store_id='" + store_id + "' GROUP BY goods_id");
                for (int j = 0; j < rdt1.Rows.Count; j++)
                {
                    dicRemainQty.Add(rdt1.Rows[j]["goods_id"].ToString().Trim().Trim(), Convert.ToInt32(rdt1.Rows[j]["Qty"]));
                }  
                List<string> s = doStockIn(pk_id, store_id);
                if (s != null)
                {
                    for (int i = 0; i < s.Count; i++)
                    {
                        sqlList.Add(s[i]);
                    }
                }
                new InsertCommandBuilder().ExcutTransaction(sqlList);
                Response.Write("<script>alert('" + Resources.Resource.alterOk + "!')</script>");
                //PDData(pk_id, false);
                dicRemainQty.Clear();
                gvData.DataSource = getData(ddlMaterialStock.SelectedItem.Value.Trim());
                gvData.DataBind();
                gvDetailData.DataSource = null;
                gvDetailData.DataBind();
                return;
            }
            else
            {
                Response.Write("<script>alert('" + Resources.Resource.alterfiald + "!')</script>");
                return;
            }
            #region
            //            //            string sql = @"SELECT stock_remain.goods_id, goods.goods_name, batch.hwh,
            //            //                                  CAST(SUM(stock_remain.qty) AS int) AS Qty,
            //            //                                  ISNULL(CAST(SUM(tmp_pk_detail.Qty) AS int), 0) AS pkQty
            //            //                            FROM stock_remain LEFT OUTER JOIN
            //            //                                  goods ON goods.goods_id = stock_remain.goods_id INNER JOIN
            //            //                                  batch ON stock_remain.batch_id = batch.batch_id LEFT OUTER JOIN
            //            //                                  tmp_pk_detail ON goods.goods_id = tmp_pk_detail.Goods_id AND
            //            //                                  batch.hwh = tmp_pk_detail.hwh AND
            //            //                                  batch.pch = tmp_pk_detail.Pch LEFT OUTER JOIN
            //            //                                  tmp_pk_bill ON tmp_pk_detail.Bill_id = tmp_pk_bill.Bill_Id
            //            //                            WHERE (stock_remain.store_id = '" + store_id + "') AND (tmp_pk_bill.Pk_id IS NULL OR tmp_pk_bill.Pk_id = '" + pk_id + @"')
            //            //                            GROUP BY stock_remain.goods_id, goods.goods_name, batch.hwh
            //            //                            ORDER BY stock_remain.goods_id ,batch.hwh";
            //            string sql = @"select a.goods_id ,a.goods_name,a.hwh,a.qty as 'Qty',b.qty as 'pkQty' from
            //                            (SELECT goods.goods_id, goods.goods_name,stock_remain.store_id, batch.hwh, SUM(stock_remain.qty) AS qty
            //                            FROM batch INNER JOIN
            //                                  stock_remain ON batch.batch_id = stock_remain.batch_id INNER JOIN
            //                                  goods ON stock_remain.goods_id = goods.goods_id
            //                            GROUP BY goods.goods_name, batch.hwh, goods.goods_id,stock_remain.store_id) a
            //                            inner join 
            //                            (SELECT tmp_pk_bill.Pk_id, tmp_pk_detail.goods_id, tmp_pk_detail.hwh, SUM(tmp_pk_detail.Qty) AS qty
            //                            FROM tmp_pk_bill INNER JOIN
            //                                  tmp_pk_detail ON tmp_pk_bill.Bill_Id = tmp_pk_detail.Bill_id
            //                            where tmp_pk_bill.store_id = '" + store_id + @"'
            //                            GROUP BY tmp_pk_bill.Pk_id, tmp_pk_detail.goods_id, tmp_pk_detail.hwh)b on a.goods_id = b.goods_id and a.hwh = b.hwh
            //                            where b.Pk_id ='" + pk_id + "'  and a.store_id='" + store_id + @"'
            //                            ORDER BY a.goods_id, a.hwh";
            //            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            //            if (dt != null && dt.Rows.Count != 0)
            //            {
            //                int diffQty = 0;
            //                int remainQty = 0;
            //                for (int i = 0; i < dt.Rows.Count; i++)
            //                {
            //                    string id = i == 0 ? "" : dt.Rows[i - 1]["goods_id"].ToString().Trim();
            //                    int stockQty = Convert.ToInt32(dt.Rows[i]["Qty"]);
            //                    int pkQty = Convert.ToInt32(dt.Rows[i]["pkQty"]);
            //                    diffQty = pkQty - stockQty;
            //                    //DataTable rdt = new SelectCommandBuilder().ExecuteDataTable("SELECT isnull(remain_qty, 0) FROM goods_detail WHERE goods_id = '" + dt.Rows[i]["goods_id"].ToString().Trim() + "' AND str_date =(SELECT MAX(str_Date) FROM goods_detail  WHERE goods_id = '" + dt.Rows[i]["goods_id"].ToString().Trim() + "')");
            //                    //if (rdt == null || rdt.Rows.Count == 0)
            //                    //{                    
            //                    //DataTable rdt = new SelectCommandBuilder().ExecuteDataTable("SELECT sum(Qty) FROM stock_remain where goods_id ='" + dt.Rows[i]["goods_id"].ToString().Trim() + "' and store_id='" + store_id + "'");
            //                    //}
            //                    if (diffQty > 0)//盘点数量大于库存数量，盘盈入库
            //                    {
            //                        string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //                        string inbatchId = CommadMethod.getNextId("SL", "1010A");
            //                        UpdateCommandBuilder update = new UpdateCommandBuilder();
            //                        //string updateSql = "update stock_remain set qty = qty+" + diffQty + " where batch_id = (SELECT MIN(batch.batch_id) FROM batch INNER JOIN stock_remain ON batch.batch_id = stock_remain.batch_id WHERE (batch.str_in_date =(SELECT MIN(batch.str_in_date) FROM stock_remain INNER JOIN batch ON stock_remain.batch_id = batch.batch_id WHERE stock_remain.goods_id = '" + dt.Rows[i]["goods_id"].ToString().Trim().Trim() + "' AND batch.hwh ='" + dt.Rows[i]["hwh"].ToString().Trim() + "')))";

            //                        DateTime date = DateTime.Now;
            //                        string IdLeftPart = "PH" + date.Year.ToString().Substring(2, 2) + date.Month.ToString().PadLeft(2, '0') + date.Day.ToString().PadLeft(2, '0');
            //                        string Id = CommadMethod.getNextId(IdLeftPart, "");
            //                        //str_in_bill
            //                        InsertCommandBuilder ins = new InsertCommandBuilder("str_in_bill");
            //                        ins.InsertColumn("str_in_bill_id", Id);
            //                        ins.InsertColumn("str_in_type_id", 2);
            //                        ins.InsertColumn("str_in_date", time);
            //                        ins.InsertColumn("operator_date", time);
            //                        ins.InsertColumn("store_id", store_id);
            //                        ins.InsertColumn("operator_id", HttpContext.Current.Request.Cookies["cookie"].Values["id"]);
            //                        ins.InsertColumn("modifier_id", HttpContext.Current.Request.Cookies["cookie"].Values["id"]);
            //                        ins.InsertColumn("modify_date", time);
            //                        ins.InsertColumn("auditer_id", HttpContext.Current.Request.Cookies["cookie"].Values["id"]);
            //                        ins.InsertColumn("audit_date", time);
            //                        ins.InsertColumn("bill_num", 1);
            //                        ins.InsertColumn("is_statics", "Y");
            //                        ins.InsertColumn("is_mine", 1);
            //                        ins.InsertColumn("islocal", "Y");
            //                        ins.InsertColumn("FinImport", "N");
            //                        ins.InsertColumn("is_end", "N");
            //                        ins.InsertColumn("print_cs", 1);
            //                        //str_in_bill_detail
            //                        string str_in_bill_Sql = ins.getInsertCommand();
            //                        InsertCommandBuilder inss = new InsertCommandBuilder("str_in_bill_detail");
            //                        inss.InsertColumn("str_in_bill_id", Id);
            //                        inss.InsertColumn("batch_id", inbatchId);
            //                        inss.InsertColumn("goods_id", dt.Rows[i]["goods_id"].ToString().Trim());
            //                        inss.InsertColumn("qty", diffQty);
            //                        inss.InsertColumn("can_sale", "Y");
            //                        inss.InsertColumn("piece", 1);
            //                        string str_in_detail_Sql = inss.getInsertCommand();
            //                        //batch
            //                        InsertCommandBuilder insss = new InsertCommandBuilder("batch");
            //                        insss.InsertColumn("batch_id", inbatchId);
            //                        insss.InsertColumn("goods_id", dt.Rows[i]["goods_id"].ToString().Trim());
            //                        insss.InsertColumn("pch", "PY" + DateTime.Now.ToString("yyyyMMdd"));
            //                        insss.InsertColumn("hwh", dt.Rows[i]["hwh"].ToString());
            //                        insss.InsertColumn("str_in_date", time);
            //                        sqlList.Add(insss.getInsertCommand());
            //                        //sqlList.Add(updateSql);
            //                        sqlList.Add(str_in_bill_Sql);
            //                        sqlList.Add(str_in_detail_Sql);
            //                        InsertCommandBuilder insertStock = new InsertCommandBuilder("stock_remain");
            //                        insertStock.InsertColumn("stock_remain_id", CommadMethod.getNextId(DateTime.Now.ToString("yyMMdd"), "").Trim());
            //                        insertStock.InsertColumn("goods_id", dt.Rows[i]["goods_id"].ToString().Trim());
            //                        insertStock.InsertColumn("batch_id", inbatchId);
            //                        insertStock.InsertColumn("store_id", store_id);
            //                        insertStock.InsertColumn("qty", diffQty);
            //                        insertStock.InsertColumn("is_can_sale", "Y");
            //                        sqlList.Add(insertStock.getInsertCommand());
            //                        //InsertCommandBuilder insert = new InsertCommandBuilder("goods_detail");
            //                        //insert.InsertColumn("goods_detail_id", CommadMethod.getNextId("SM", "0101"));
            //                        //insert.InsertColumn("goods_id", dt.Rows[i]["goods_id"].ToString().Trim());
            //                        //insert.InsertColumn("str_date", time);
            //                        //insert.InsertColumn("store_id", store_id);
            //                        //insert.InsertColumn("batch_id", Id);
            //                        //insert.InsertColumn("qty", diffQty);
            //                        //insert.InsertColumn("remain_qty", "");
            //                        //insert.InsertColumn("is_str_in", "Y");
            //                        //insert.InsertColumn("str_type_name", "盘盈入库");
            //                        //insert.InsertColumn("str_bill_id", pk_id);
            //                        //insert.InsertColumn("operator_name", "Admin");
            //                        //insert.InsertColumn("down", "N");+
            //                        //sqlList.Add(insert.getInsertCommand());
            //                        if (id != dt.Rows[i]["goods_id"].ToString().Trim())
            //                        {
            //                            id = dt.Rows[i]["goods_id"].ToString().Trim();
            //                            remainQty = dicRemainQty[id];
            //                        }
            //                        remainQty += diffQty;
            //                        if (dicRemainQty.ContainsKey(dt.Rows[i]["goods_id"].ToString().Trim()))
            //                        {
            //                            dicRemainQty[dt.Rows[i]["goods_id"].ToString().Trim()] = remainQty;
            //                        }
            //                        else
            //                        {
            //                            dicRemainQty.Add(dt.Rows[i]["goods_id"].ToString().Trim(), remainQty);
            //                        }
            //                        string sqlDetail = "insert into goods_detail(goods_detail_id,goods_id,str_date,store_id,batch_id,qty,remain_qty,is_str_in,str_type_name,str_bill_id,operator_name,down)";
            //                        sqlDetail += " values ('" + CommadMethod.getNextId("SM", "0101") + "','" + dt.Rows[i]["goods_id"].ToString().Trim() + "', getdate(), '" + store_id + "','" + inbatchId + "',";
            //                        sqlDetail += diffQty + ", " + dicRemainQty[dt.Rows[i]["goods_id"].ToString().Trim()] + ",'Y','盘盈入库','" + pk_id + "','Admin','N')";
            //                        sqlList.Add(sqlDetail);
            //                    }
            //                    else if (diffQty < 0)//盘亏出库
            //                    {
            //                        string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //                        DateTime date = DateTime.Now;
            //                        string IdLeftPart = "HC" + date.Year.ToString().Substring(2, 2) + date.Month.ToString().PadLeft(2, '0') + date.Day.ToString().PadLeft(2, '0');
            //                        string str_out_bill_id = CommadMethod.getNextId(IdLeftPart, "");
            //                        InsertCommandBuilder ins = new InsertCommandBuilder("str_out_bill");
            //                        ins.InsertColumn("str_out_bill_id", str_out_bill_id);
            //                        ins.InsertColumn("dfdh", pk_id);
            //                        ins.InsertColumn("str_out_type_id", 1);
            //                        ins.InsertColumn("str_out_date", time);
            //                        ins.InsertColumn("store_id", store_id);
            //                        ins.InsertColumn("operator_id", HttpContext.Current.Request.Cookies["cookie"].Values["id"]);//HttpContext.Current.Request.Cookies["cookie"].Values["id"].Trim());
            //                        ins.InsertColumn("operator_date", time);
            //                        ins.InsertColumn("come_to", "盘亏出库");
            //                        ins.InsertColumn("islocal", "Y");
            //                        string Outsql = ins.getInsertCommand();
            //                        sqlList.Add(Outsql);
            //                        string CompairSql = @"SELECT stock_remain.stock_remain_id,stock_remain.batch_id, stock_remain.goods_id, batch.hwh, stock_remain.qty, 
            //                                                  batch.pch, stock_remain.str_in_date
            //                                            FROM stock_remain INNER JOIN
            //                                                  batch ON stock_remain.batch_id = batch.batch_id
            //                                            WHERE (stock_remain.goods_id = '" + dt.Rows[i]["goods_id"].ToString().Trim() + @"' AND (batch.hwh = '" + dt.Rows[i]["hwh"].ToString() + @"'))
            //                                                   And (stock_remain.store_id = '" + store_id + @"')
            //                                            ORDER BY stock_remain.goods_id, stock_remain.str_in_date";
            //                        DataTable dtItems = new SelectCommandBuilder().ExecuteDataTable(CompairSql);
            //                        if (id != dtItems.Rows[0]["goods_id"].ToString().Trim() || remainQty == 0)
            //                        {
            //                            id = dtItems.Rows[0]["goods_id"].ToString().Trim();
            //                            remainQty = dicRemainQty[id];
            //                        }
            //                        int diff = Math.Abs(diffQty);
            //                        if (dtItems != null && dtItems.Rows.Count != 0)
            //                        {
            //                            for (int j = 0; j < dtItems.Rows.Count; j++)
            //                            {
            //                                time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //                                int compairQty = Convert.ToInt32(dtItems.Rows[j]["qty"]);
            //                                diff -= compairQty;
            //                                if (diff == 0)
            //                                {
            //                                    remainQty -= compairQty;
            //                                    if (dicRemainQty.ContainsKey(dtItems.Rows[0]["goods_id"].ToString().Trim()))
            //                                    {
            //                                        dicRemainQty[dtItems.Rows[0]["goods_id"].ToString().Trim()] = remainQty;
            //                                    }
            //                                    else
            //                                    {
            //                                        dicRemainQty.Add(dtItems.Rows[0]["goods_id"].ToString().Trim(), remainQty);
            //                                    }
            //                                    string inbatchId = CommadMethod.getNextId("SL", "1010A");
            //                                    InsertCommandBuilder inst = new InsertCommandBuilder("str_out_bill_detail");
            //                                    inst.InsertColumn("str_out_bill_id", str_out_bill_id);
            //                                    inst.InsertColumn("goods_id", dtItems.Rows[j]["goods_id"].ToString().Trim());
            //                                    inst.InsertColumn("batch_id", inbatchId);
            //                                    inst.InsertColumn("qty", compairQty);
            //                                    sqlList.Add(inst.getInsertCommand());
            //                                    InsertCommandBuilder insss = new InsertCommandBuilder("batch");
            //                                    insss.InsertColumn("batch_id", inbatchId);
            //                                    insss.InsertColumn("goods_id", dtItems.Rows[j]["goods_id"].ToString().Trim());
            //                                    insss.InsertColumn("pch", dtItems.Rows[j]["pch"].ToString());
            //                                    insss.InsertColumn("hwh", dtItems.Rows[j]["hwh"].ToString());
            //                                    insss.InsertColumn("str_in_date", time);
            //                                    sqlList.Add(insss.getInsertCommand());
            //                                    string sqlDetail = "insert into goods_detail(goods_detail_id,goods_id,str_date,store_id,batch_id,qty,remain_qty,is_str_in,str_type_name,str_bill_id,operator_name,down)";
            //                                    sqlDetail += " values ('" + CommadMethod.getNextId("SM", "0101") + "','" + dt.Rows[i]["goods_id"].ToString().Trim() + "', getdate(), '" + store_id + "','" + inbatchId + "',";
            //                                    sqlDetail += diffQty + ", " + (dicRemainQty[dtItems.Rows[0]["goods_id"].ToString().Trim()]) + ",'N','盘亏出库','" + str_out_bill_id + "','Admin','N')";
            //                                    sqlList.Add(sqlDetail);
            //                                    string Rsql = "delete from stock_remain where goods_id='" + dtItems.Rows[j]["goods_id"].ToString().Trim() + "' and batch_id='" + dtItems.Rows[j]["batch_id"].ToString() + "'";
            //                                    string Dsql = "delete from batch where goods_id='" + dtItems.Rows[j]["goods_id"].ToString().Trim() + "' and batch_id='" + dtItems.Rows[j]["batch_id"].ToString() + "'";
            //                                    sqlList.Add(Rsql);
            //                                    sqlList.Add(Dsql);
            //                                    break;
            //                                }
            //                                else if (diff > 0)
            //                                {
            //                                    remainQty -= compairQty;
            //                                    if (dicRemainQty.ContainsKey(dtItems.Rows[0]["goods_id"].ToString().Trim()))
            //                                    {
            //                                        dicRemainQty[dtItems.Rows[0]["goods_id"].ToString().Trim()] = remainQty;
            //                                    }
            //                                    else
            //                                    {
            //                                        dicRemainQty.Add(dtItems.Rows[0]["goods_id"].ToString().Trim(), remainQty);
            //                                    }
            //                                    string inbatchId = CommadMethod.getNextId("SL", "1010A");
            //                                    int qty = 0 - Convert.ToInt32(dtItems.Rows[j]["qty"]);
            //                                    InsertCommandBuilder inst = new InsertCommandBuilder("str_out_bill_detail");
            //                                    inst.InsertColumn("str_out_bill_id", str_out_bill_id);
            //                                    inst.InsertColumn("goods_id", dtItems.Rows[j]["goods_id"].ToString().Trim());
            //                                    inst.InsertColumn("batch_id", inbatchId);
            //                                    inst.InsertColumn("qty", Math.Abs(qty));
            //                                    InsertCommandBuilder insss = new InsertCommandBuilder("batch");
            //                                    insss.InsertColumn("batch_id", inbatchId);
            //                                    insss.InsertColumn("goods_id", dtItems.Rows[j]["goods_id"].ToString().Trim());
            //                                    insss.InsertColumn("pch", dtItems.Rows[j]["pch"].ToString());
            //                                    insss.InsertColumn("hwh", dtItems.Rows[j]["hwh"].ToString());
            //                                    insss.InsertColumn("str_in_date", time);
            //                                    sqlList.Add(insss.getInsertCommand());
            //                                    string sqlDetail = "insert into goods_detail(goods_detail_id,goods_id,str_date,store_id,batch_id,qty,remain_qty,is_str_in,str_type_name,str_bill_id,operator_name,down)";
            //                                    sqlDetail += " values ('" + CommadMethod.getNextId("SM", "0101") + "','" + dt.Rows[i]["goods_id"].ToString().Trim() + "', getdate(), '" + store_id + "','" + inbatchId + "',";
            //                                    sqlDetail += 0 - compairQty + "," + (dicRemainQty[dtItems.Rows[0]["goods_id"].ToString().Trim()]) + ",'N','盘亏出库','" + str_out_bill_id + "','Admin','N')";
            //                                    sqlList.Add(sqlDetail);
            //                                    string Rsql = "delete from stock_remain where goods_id='" + dtItems.Rows[j]["goods_id"].ToString().Trim() + "' and batch_id='" + dtItems.Rows[j]["batch_id"].ToString() + "'";
            //                                    string Dsql = "delete from batch where goods_id='" + dtItems.Rows[j]["goods_id"].ToString().Trim() + "' and batch_id='" + dtItems.Rows[j]["batch_id"].ToString() + "'";
            //                                    sqlList.Add(inst.getInsertCommand());
            //                                    sqlList.Add(Rsql);
            //                                    sqlList.Add(Dsql);
            //                                    continue;
            //                                }
            //                                else if (diff < 0)
            //                                {
            //                                    remainQty -= (compairQty + diff);
            //                                    if (dicRemainQty.ContainsKey(dtItems.Rows[0]["goods_id"].ToString().Trim()))
            //                                    {
            //                                        dicRemainQty[dtItems.Rows[0]["goods_id"].ToString().Trim()] = remainQty;
            //                                    }
            //                                    else
            //                                    {
            //                                        dicRemainQty.Add(dtItems.Rows[0]["goods_id"].ToString().Trim(), remainQty);
            //                                    }
            //                                    string inbatchId = CommadMethod.getNextId("SL", "1010A");
            //                                    InsertCommandBuilder inst = new InsertCommandBuilder("str_out_bill_detail");
            //                                    inst.InsertColumn("str_out_bill_id", str_out_bill_id);
            //                                    inst.InsertColumn("goods_id", dtItems.Rows[j]["goods_id"].ToString().Trim());
            //                                    inst.InsertColumn("batch_id", inbatchId);
            //                                    inst.InsertColumn("qty", Math.Abs(diffQty));
            //                                    sqlList.Add(inst.getInsertCommand());
            //                                    InsertCommandBuilder insss = new InsertCommandBuilder("batch");
            //                                    insss.InsertColumn("batch_id", inbatchId);
            //                                    insss.InsertColumn("goods_id", dtItems.Rows[j]["goods_id"].ToString().Trim());
            //                                    insss.InsertColumn("pch", dtItems.Rows[j]["pch"].ToString());
            //                                    insss.InsertColumn("hwh", dtItems.Rows[j]["hwh"].ToString());
            //                                    insss.InsertColumn("str_in_date", time);
            //                                    sqlList.Add(insss.getInsertCommand());
            //                                    string Usql = "update stock_remain set qty = '" + Math.Abs(diff) + "' where goods_id='" + dtItems.Rows[j]["goods_id"].ToString().Trim() + "' and batch_id='" + dtItems.Rows[j]["batch_id"].ToString() + "'";
            //                                    sqlList.Add(Usql);
            //                                    string sqlDetail = "insert into goods_detail(goods_detail_id,goods_id,str_date,store_id,batch_id,qty,remain_qty,is_str_in,str_type_name,str_bill_id,operator_name,down)";
            //                                    sqlDetail += " values ('" + CommadMethod.getNextId("SM", "0101") + "','" + dt.Rows[i]["goods_id"].ToString().Trim() + "', getdate(), '" + store_id + "','" + inbatchId + "',";
            //                                    sqlDetail += 0 - (compairQty + diff) + ", " + (dicRemainQty[dtItems.Rows[0]["goods_id"].ToString().Trim()]) + ",'N','盘亏出库','" + str_out_bill_id + "','Admin','N')";
            //                                    sqlList.Add(sqlDetail);
            //                                    break;
            //                                }
            //                            }
            //                        }
            //                    }
            //                    else//相等情况下，保留一个仓位数量，其他仓位作盘库出库
            //                    {

            //                    }
            //                }
            //            }
            //            if (sqlList.Count != 0)
            //            {
            //                int count = new InsertCommandBuilder().ExcutTransaction(sqlList);
            //                if (count != 0)
            //                {
            //                    Response.Write("<script>alert('" + Resources.Resource.alterOk + "!')</script>");
            //                    deletePDData(pk_id, false);
            //                    dicRemainQty.Clear();
            //                    return;
            //                }
            //                else
            //                {
            //                    Response.Write("<script>alert('" + Resources.Resource.alterfiald + "!')</script>");
            //                    return;
            //                }
            //            }
            #endregion
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (gvData.SelectedIndex == -1)
            {
                Response.Write("<script>alert('" + Resources.Resource.alterwxz + "!')</script>");
                return;
            }
            string pk_id = gvData.SelectedRow.Cells[1].Text.ToString().Trim().ToUpper();
            deletePDData(pk_id, true);
        }
        private void PDData(string pk_id, bool alert)
        {
            string mainsql = "delete from pre_pk where pk_id='" + pk_id + "'";
            string detailsql = "delete from pre_pk_Detail where pk_id='" + pk_id + "'";
            string billsql = "update tmp_pk_bill set status = 'A' where pk_id='" + pk_id + "'";
            //string billdetailsql = "delete from tmp_pk_detail where exists(select bill_id from tmp_pk_bill where bill_id=tmp_pk_detail.bill_id and pk_id='" + pk_id + "')";
            List<string> sList = new List<string>();
            sList.Add(mainsql);
            if (Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar("select count(*) from pre_pk_Detail where pk_id='" + pk_id + "'")) != 0)
            {
                sList.Add(detailsql);
            }
            //int cc = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar("select Count(*) from tmp_pk_detail where exists(select bill_id from tmp_pk_bill where bill_id=tmp_pk_detail.bill_id and pk_id='" + pk_id + "')"));
            //if (cc != 0)
            //{
            //    sList.Add(billdetailsql);
            //}
            if (Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar("select count(*) from tmp_pk_bill where pk_id='" + pk_id + "'")) != 0)
            {
                sList.Add(billsql);
            }
            int count = new InsertCommandBuilder().ExcutTransaction(sList);
            if (count != 0)
            {
                gvData.DataSource = getData(ddlMaterialStock.SelectedItem.Value.Trim());
                gvData.DataBind();
                gvDetailData.DataSource = null;
                gvDetailData.DataBind();
                if (alert == true)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('" + Resources.Resource.alterOk + "!')</script>");
                }
            }
        }
        private void deletePDData(string pk_id, bool alert)
        {
            string mainsql = "delete from pre_pk where pk_id='" + pk_id + "'";
            string detailsql = "delete from pre_pk_Detail where pk_id='" + pk_id + "'";
            string billsql = "delete from tmp_pk_bill where pk_id='" + pk_id + "'";
            string billdetailsql = "delete from tmp_pk_detail where exists(select bill_id from tmp_pk_bill where bill_id=tmp_pk_detail.bill_id and pk_id='" + pk_id + "')";
            List<string> sList = new List<string>();
            sList.Add(mainsql);
            if (Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar("select count(*) from pre_pk_Detail where pk_id='" + pk_id + "'")) != 0)
            {
                sList.Add(detailsql);
            }
            int cc = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar("select Count(*) from tmp_pk_detail where exists(select bill_id from tmp_pk_bill where bill_id=tmp_pk_detail.bill_id and pk_id='" + pk_id + "')"));
            if (cc != 0)
            {
                sList.Add(billdetailsql);
            }
            if (Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar("select count(*) from tmp_pk_bill where pk_id='" + pk_id + "'")) != 0)
            {
                sList.Add(billsql);
            }
            int count = new InsertCommandBuilder().ExcutTransaction(sList);
            if (count != 0)
            {
                gvData.DataSource = getData(ddlMaterialStock.SelectedItem.Value.Trim());
                gvData.DataBind();
                gvDetailData.DataSource = null;
                gvDetailData.DataBind();
                if (alert == true)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('" + Resources.Resource.alterOk + "!')</script>");
                }
            }
        }
        private List<string> doStockIn(string pk_id, string store_id)
        {
            int remainQty = 0;
            List<string> sqlList = new List<string>();
            string detailsql = @"select tmp_pk_detail.Goods_id, tmp_pk_detail.Qty, tmp_pk_detail.Pch,tmp_pk_detail.hwh
                                 FROM tmp_pk_detail INNER JOIN tmp_pk_bill ON tmp_pk_detail.Bill_id = tmp_pk_bill.Bill_Id where tmp_pk_bill.Pk_id = '" + pk_id + "' and tmp_pk_bill.isAdd = 'Y'";
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(detailsql);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string id = i == 0 ? "" : dt.Rows[i - 1]["goods_id"].ToString().Trim();
                //DataTable rdt = new SelectCommandBuilder().ExecuteDataTable("SELECT sum(Qty) FROM stock_remain where goods_id ='" + dt.Rows[i]["goods_id"].ToString().Trim() + "' and store_id='" + store_id + "'");
                string inbatchId = CommadMethod.getNextId("SL", "1010A");
                InsertCommandBuilder inst = new InsertCommandBuilder("stock_remain");
                inst.InsertColumn("stock_remain_id", CommadMethod.getNextId("", "").Trim().PadLeft(14, '0'));
                inst.InsertColumn("goods_id", dt.Rows[i]["goods_id"].ToString().Trim().Trim());
                inst.InsertColumn("qty", dt.Rows[i]["Qty"]);
                inst.InsertColumn("batch_id", inbatchId);
                inst.InsertColumn("store_id", store_id);
                inst.InsertColumn("is_can_sale", "Y");
                sqlList.Add(inst.getInsertCommand());
                DateTime date = DateTime.Now;
                string IdLeftPart = "PH" + date.Year.ToString().Substring(2, 2) + date.Month.ToString().PadLeft(2, '0') + date.Day.ToString().PadLeft(2, '0');
                string Id = CommadMethod.getNextId(IdLeftPart, "");
                InsertCommandBuilder ins = new InsertCommandBuilder("str_in_bill");
                ins.InsertColumn("str_in_bill_id", Id);
                ins.InsertColumn("str_in_type_id", 2);
                ins.InsertColumn("str_in_date", time);
                ins.InsertColumn("operator_date", time);
                ins.InsertColumn("store_id", store_id);
                ins.InsertColumn("operator_id", HttpContext.Current.Request.Cookies["cookie"].Values["id"]);
                ins.InsertColumn("modifier_id", HttpContext.Current.Request.Cookies["cookie"].Values["id"]);
                ins.InsertColumn("modify_date", time);
                ins.InsertColumn("auditer_id", HttpContext.Current.Request.Cookies["cookie"].Values["id"]);
                ins.InsertColumn("audit_date", time);
                ins.InsertColumn("bill_num", 1);
                ins.InsertColumn("is_statics", "Y");
                ins.InsertColumn("is_mine", 1);
                ins.InsertColumn("islocal", "Y");
                ins.InsertColumn("FinImport", "N");
                ins.InsertColumn("is_end", "N");
                ins.InsertColumn("print_cs", 1);
                string str_in_bill_Sql = ins.getInsertCommand();
                InsertCommandBuilder inss = new InsertCommandBuilder("str_in_bill_detail");
                inss.InsertColumn("str_in_bill_id", Id);
                inss.InsertColumn("batch_id", inbatchId);
                inss.InsertColumn("goods_id", dt.Rows[i]["goods_id"].ToString().Trim());
                inss.InsertColumn("qty", dt.Rows[i]["Qty"]);
                inss.InsertColumn("can_sale", "Y");
                inss.InsertColumn("piece", 1);
                string str_in_detail_Sql = inss.getInsertCommand();
                InsertCommandBuilder insss = new InsertCommandBuilder("batch");
                insss.InsertColumn("batch_id", inbatchId);
                insss.InsertColumn("goods_id", dt.Rows[i]["goods_id"].ToString().Trim());
                insss.InsertColumn("pch", dt.Rows[i]["pch"].ToString());
                insss.InsertColumn("hwh", dt.Rows[i]["hwh"].ToString());
                insss.InsertColumn("str_in_date", time);
                sqlList.Add(insss.getInsertCommand());
                sqlList.Add(str_in_bill_Sql);
                sqlList.Add(str_in_detail_Sql);
                if (id != dt.Rows[i]["goods_id"].ToString().Trim())
                {
                    if (!dicRemainQty.ContainsKey(dt.Rows[i]["goods_id"].ToString().Trim()))
                    {
                        dicRemainQty.Add(dt.Rows[i]["goods_id"].ToString().Trim(), 0);
                    }
                    id = dt.Rows[i]["goods_id"].ToString().Trim();
                    remainQty = dicRemainQty[id];
                }
                remainQty += Convert.ToInt32(dt.Rows[i]["Qty"]);
                if (dicRemainQty.ContainsKey(dt.Rows[i]["goods_id"].ToString().Trim()))
                {
                    dicRemainQty[dt.Rows[i]["goods_id"].ToString().Trim()] = remainQty;
                }
                else
                {
                    dicRemainQty.Add(dt.Rows[i]["goods_id"].ToString().Trim(), remainQty);
                }
                string sqlDetail = "insert into goods_detail(goods_detail_id,goods_id,str_date,store_id,batch_id,qty,remain_qty,is_str_in,str_type_name,str_bill_id,operator_name,down)";
                sqlDetail += " values ('" + CommadMethod.getNextId("SM", "0101") + "','" + dt.Rows[i]["goods_id"].ToString().Trim() + "', getdate(), '" + store_id + "','" + inbatchId + "',";
                sqlDetail += dt.Rows[i]["Qty"] + ", " + dicRemainQty[dt.Rows[i]["goods_id"].ToString().Trim()] + ",'Y','21','" + pk_id + "','Admin','N')";
                sqlList.Add(sqlDetail);
            }
            return sqlList;

        }

    }
}