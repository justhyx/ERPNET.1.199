using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using System.Threading;
using System.Globalization;
using ERPPlugIn.Class;
using System.Configuration;
using System.Data.SqlClient;

namespace ERPPlugIn.MaterailsManager
{
    public partial class MaterailsStock_FinalConfirm : PageBase
    {
        public Dictionary<string, double> dicRemainQty { get { return ViewState["dic"] as Dictionary<string, double>; } set { ViewState["dic"] = value; } }
        public string constr { get { return ViewState["constr"].ToString(); } set { ViewState["constr"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                dicRemainQty = new Dictionary<string, double>();
                constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                bindDDL();
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
            string sql = "SELECT DISTINCT(SELECT s." + Resources.Resource.store + " FROM store s  WHERE a.store_id = s.store_id) AS '" + Resources.Resource.clck + @"', a.prd_pk_id as '" + Resources.Resource.pddh + @"', CONVERT(varchar(100), a.prd_pk_date, 20) as '" + Resources.Resource.pdrq + @"','" + Resources.Resource.czry + @"'=(select operator_name from operator o where o.operator_id =a.operator_id),a.remark as '" + Resources.Resource.bz + @"' FROM pre_prd_pk  a  join pd b on a.prd_pk_id =b.pk_id where a.store_id='" + store + "' and b.status ='Y'";
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
            string sql = @"SELECT materials.dm, materials.name, materials.spec, prd_batch.pch, materials.unit, 
                              pre_prd_pk_detail.zmsl, pre_prd_pk_detail.pdsl, materials.szb, prd_batch.yxq, 
                              prd_batch.str_in_date, prd_batch.price, pre_prd_pk_detail.zmsl1, 
                              pre_prd_pk_detail.pdsl1, materials.select_code, 'N' AS disobey, 
                              prd_batch.prd_batch_id, pre_prd_pk_detail.is_can_sale, materials.id, 
                              pre_prd_pk_detail.stock_remain_id, materials.texture, materials.color, prd_batch.hwh, 
                              vendor.vendor_name
                        FROM pre_prd_pk_detail INNER JOIN
                              materials ON pre_prd_pk_detail.materials_id = materials.id INNER JOIN
                              prd_batch ON pre_prd_pk_detail.prd_batch_id = prd_batch.prd_batch_id INNER JOIN
                              vendor ON materials.sccj_id = vendor.vendor_id
                        WHERE (pre_prd_pk_detail.prd_pk_id  = '" + gvData.SelectedRow.Cells[1].Text.Trim() + "') ";
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
            if (gvData.SelectedIndex == -1)
            {
                Response.Write("<script>alert('" + Resources.Resource.alterwxz + "!')</script>");
                return;
            }
            string store_id = ddlMaterialStock.SelectedItem.Value.Trim();
            string pk_id = gvData.SelectedRow.Cells[1].Text.ToString().Trim().ToUpper();
            List<string> sqlList = new List<string>();
            if (gvData.SelectedIndex == -1)
            {
                Response.Write("<script>alert('" + Resources.Resource.alterwxz + "!')</script>");
                return;
            }            
            SqlParameter[] parm = new SqlParameter[]
            {
                new SqlParameter("@store_id",store_id),
                new SqlParameter("@pk_id",pk_id),
                new SqlParameter("@operator_id",HttpContext.Current.Request.Cookies["cookie"].Values["id"])
            };
            int pCount = Convert.ToInt32(SqlHelper.ExecuteScalar(constr, CommandType.StoredProcedure, "Usp_Materails_storage_update", parm));
            if (pCount != 0)
            {
                DataTable rdt1 = new SelectCommandBuilder().ExecuteDataTable("SELECT materials_id, sum(Qty) as 'Qty' FROM prd_stock  where store_id='" + store_id + "' GROUP BY materials_id");
                for (int j = 0; j < rdt1.Rows.Count; j++)
                {
                    dicRemainQty.Add(rdt1.Rows[j]["materials_id"].ToString().Trim().Trim(), Convert.ToInt32(rdt1.Rows[j]["Qty"]));
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
                //deletePDData(pk_id, false);
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
            //            string sql = @"SELECT prd_Stock.materials_id, materials.name, prd_batch.hwh,
            //                                              SUM(prd_Stock.qty) AS Qty,
            //                                              ISNULL(pd_detail.Qty, 0.00) AS pkQty
            //                                        FROM prd_Stock LEFT OUTER JOIN
            //                                              materials ON materials.id = prd_Stock.materials_id INNER JOIN
            //                                              prd_batch ON prd_Stock.prd_batch_id = prd_batch.prd_batch_id LEFT OUTER JOIN
            //                                              pd_detail ON materials.id = pd_detail.materials_id AND
            //                                              prd_batch.hwh = pd_detail.hwh AND
            //                                              prd_batch.pch = pd_detail.Pch LEFT OUTER JOIN
            //                                              pd ON pd_detail.Bill_id = pd.Bill_Id
            //                                        WHERE (prd_Stock.store_id = '" + store_id + "') AND (pd.Pk_id IS NULL OR  pd.Pk_id = '" + pk_id + @"')
            //                                        GROUP BY prd_Stock.materials_id, materials.name, prd_batch.hwh,pd_detail.Qty
            //                                        ORDER BY prd_Stock.materials_id,  prd_batch.hwh ";
            //            string sql = @"select a.id as 'materials_id' ,a.name,a.hwh,a.qty as 'Qty',b.qty as 'pkQty' from
            //                            (SELECT materials.id, materials.name,prd_Stock.store_id, prd_batch.hwh, cast(SUM(prd_Stock.qty) as decimal(10,2)) AS qty
            //                            FROM prd_batch INNER JOIN
            //                                  prd_Stock ON prd_batch.prd_batch_id = prd_Stock.prd_batch_id INNER JOIN
            //                                  materials ON prd_Stock.materials_id = materials.id
            //                            GROUP BY materials.name, prd_batch.hwh, materials.id,prd_Stock.store_id) a
            //                            inner join 
            //                            (SELECT pd.Pk_id, pd_detail.materials_id, pd_detail.hwh, cast( SUM(pd_detail.Qty)as decimal(10,2)) AS qty
            //                            FROM pd INNER JOIN
            //                                  pd_detail ON pd.Bill_Id = pd_detail.Bill_id
            //                            where pd.store_id = '" + store_id + @"'
            //                            GROUP BY pd.Pk_id, pd_detail.materials_id, pd_detail.hwh)b on a.id = b.materials_id and a.hwh = b.hwh
            //                            where b.Pk_id='" + pk_id + @"' and a.store_id='" + store_id + @"'
            //                            ORDER BY a.id, a.hwh";
            //            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            //            if (dt != null && dt.Rows.Count != 0)
            //            {
            //                double diffQty = 0;
            //                double remainQty = 0;

            //                for (int i = 0; i < dt.Rows.Count; i++)
            //                {

            //                    string id = i == 0 ? "" : dt.Rows[i - 1]["materials_id"].ToString().Trim();
            //                    double stockQty = Convert.ToDouble(dt.Rows[i]["Qty"]);
            //                    double pkQty = Convert.ToDouble(dt.Rows[i]["pkQty"]);
            //                    diffQty = pkQty - stockQty;
            //                    //DataTable rdt = new SelectCommandBuilder().ExecuteDataTable("SELECT isnull(remain_qty, 0) FROM materials_flow WHERE materials_id = '" + dt.Rows[i]["materials_id"].ToString().Trim() + "' AND str_date =(SELECT MAX(str_Date) FROM materials_flow  WHERE materials_id = '" + dt.Rows[i]["materials_id"].ToString().Trim() + "')");
            //                    //if (rdt == null || rdt.Rows.Count == 0)
            //                    //{
            //                    //DataTable rdt = new SelectCommandBuilder().ExecuteDataTable("SELECT sum(Qty) FROM prd_Stock where materials_id ='" + dt.Rows[i]["materials_id"].ToString().Trim() + "' and store_id = '" + store_id + "'");
            //                    //}
            //                    if (diffQty > 0)//盘点数量大于库存数量，盘盈入库
            //                    {
            //                        string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //                        string inbatchId = CommadMethod.getNextId("SL", "1010A");
            //                        //UpdateCommandBuilder update = new UpdateCommandBuilder();
            //                        //string updateSql = "update prd_Stock set qty = qty+" + diffQty + " where prd_batch_id = (SELECT MIN(prd_batch.prd_batch_id) FROM prd_batch INNER JOIN prd_stock ON prd_batch.prd_batch_id = prd_stock.prd_batch_id WHERE (prd_batch.str_in_date =(SELECT MIN(prd_batch.str_in_date) FROM prd_Stock INNER JOIN prd_batch ON prd_stock.prd_batch_id = prd_batch.prd_batch_id WHERE prd_Stock.materials_id = '" + dt.Rows[i]["materials_id"].ToString().Trim().Trim() + "' AND prd_batch.hwh ='" + dt.Rows[i]["hwh"].ToString().Trim() + "')))";
            //                        //InsertCommandBuilder inst = new InsertCommandBuilder("prd_Stock");
            //                        //inst.InsertColumn("materials_id", dt.Rows[i]["materials_id"].ToString().Trim().Trim());
            //                        //inst.InsertColumn("qty", diffQty);
            //                        //inst.InsertColumn("prd_batch_id", inbatchId);
            //                        //inst.InsertColumn("store_id", store_id);
            //                        //inst.InsertColumn("is_can_sale", "Y");
            //                        //sqlList.Add(inst.getInsertCommand());
            //                        DateTime date = DateTime.Now;
            //                        string IdLeftPart = "PH" + date.Year.ToString().Substring(2, 2) + date.Month.ToString().PadLeft(2, '0') + date.Day.ToString().PadLeft(2, '0');
            //                        string Id = CommadMethod.getNextId(IdLeftPart, "");
            //                        InsertCommandBuilder ins = new InsertCommandBuilder("prd_in_bill");
            //                        ins.InsertColumn("prd_in_bill_id", Id);
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
            //                        //ins.InsertColumn("is_statics", "Y");
            //                        //ins.InsertColumn("is_mine", 1);
            //                        ins.InsertColumn("islocal", "Y");
            //                        ins.InsertColumn("FinImport", "N");
            //                        ins.InsertColumn("is_end", "N");
            //                        // ins.InsertColumn("print_cs", 1);
            //                        string str_in_bill_Sql = ins.getInsertCommand();
            //                        InsertCommandBuilder inss = new InsertCommandBuilder("prd_in_bill_detail");
            //                        inss.InsertColumn("prd_in_bill_id", Id);
            //                        inss.InsertColumn("prd_batch_id", inbatchId);
            //                        inss.InsertColumn("materials_id", dt.Rows[i]["materials_id"].ToString().Trim());
            //                        inss.InsertColumn("qty", diffQty);
            //                        inss.InsertColumn("can_sale", "Y");
            //                        inss.InsertColumn("piece", 1);
            //                        string str_in_detail_Sql = inss.getInsertCommand();
            //                        //sqlList.Add(updateSql);
            //                        sqlList.Add(str_in_bill_Sql);
            //                        sqlList.Add(str_in_detail_Sql);
            //                        InsertCommandBuilder insss = new InsertCommandBuilder("prd_batch");
            //                        insss.InsertColumn("prd_batch_id", inbatchId);
            //                        insss.InsertColumn("materials_id", dt.Rows[i]["materials_id"].ToString().Trim());
            //                        insss.InsertColumn("pch", "PY" + DateTime.Now.ToString("yyyyMMdd"));
            //                        insss.InsertColumn("hwh", dt.Rows[i]["hwh"].ToString());
            //                        insss.InsertColumn("str_in_date", time);
            //                        sqlList.Add(insss.getInsertCommand());
            //                        InsertCommandBuilder insertStock = new InsertCommandBuilder("prd_Stock");
            //                        insertStock.InsertColumn("prd_Stock_id", CommadMethod.getNextId(DateTime.Now.ToString("yyMMdd"), "").Trim());
            //                        insertStock.InsertColumn("materials_id", dt.Rows[i]["materials_id"].ToString().Trim());
            //                        insertStock.InsertColumn("prd_batch_id", inbatchId);
            //                        insertStock.InsertColumn("store_id", store_id);
            //                        insertStock.InsertColumn("qty", diffQty);
            //                        insertStock.InsertColumn("is_can_sale", "Y");
            //                        sqlList.Add(insertStock.getInsertCommand());
            //                        //InsertCommandBuilder insert = new InsertCommandBuilder("goods_detail");
            //                        //insert.InsertColumn("goods_detail_id", CommadMethod.getNextId("SM", "0101"));
            //                        //insert.InsertColumn("goods_id", dt.Rows[i]["goods_id"].ToString());
            //                        //insert.InsertColumn("str_date", time);
            //                        //insert.InsertColumn("store_id", store_id);
            //                        //insert.InsertColumn("prd_batch_id", Id);
            //                        //insert.InsertColumn("qty", diffQty);
            //                        //insert.InsertColumn("remain_qty", "");
            //                        //insert.InsertColumn("is_str_in", "Y");
            //                        //insert.InsertColumn("str_type_name", "盘盈入库");
            //                        //insert.InsertColumn("str_bill_id", pk_id);
            //                        //insert.InsertColumn("operator_name", "Admin");
            //                        //insert.InsertColumn("down", "N");
            //                        //sqlList.Add(insert.getInsertCommand());
            //                        if (id != dt.Rows[i]["materials_id"].ToString().Trim())
            //                        {
            //                            id = dt.Rows[i]["materials_id"].ToString().Trim();
            //                            remainQty = dicRemainQty[id];
            //                        }
            //                        if (dicRemainQty.ContainsKey(dt.Rows[i]["materials_id"].ToString().Trim()))
            //                        {
            //                            dicRemainQty[dt.Rows[i]["materials_id"].ToString().Trim()] = remainQty;
            //                        }
            //                        else
            //                        {
            //                            dicRemainQty.Add(dt.Rows[i]["materials_id"].ToString().Trim(), remainQty);
            //                        }
            //                        remainQty += diffQty;
            //                        string sqlDetail = "insert into materials_flow(materials_flow_id,materials_id,str_date,store_id,prd_batch_id,qty,remain_qty,is_str_in,str_type_name,str_bill_id,operator_name)";
            //                        sqlDetail += " values ('" + CommadMethod.getNextId("SM", "0101") + "','" + dt.Rows[i]["materials_id"].ToString().Trim() + "', getdate(), '" + store_id + "','" + inbatchId + "',";
            //                        sqlDetail += diffQty + ", " + dicRemainQty[dt.Rows[i]["materials_id"].ToString().Trim()] + ",'Y','盘盈入库','" + pk_id + "','Admin')";
            //                        sqlList.Add(sqlDetail);
            //                    }
            //                    else if (diffQty < 0)//盘亏出库
            //                    {
            //                        string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //                        DateTime date = DateTime.Now;
            //                        string IdLeftPart = "HC" + date.Year.ToString().Substring(2, 2) + date.Month.ToString().PadLeft(2, '0') + date.Day.ToString().PadLeft(2, '0');
            //                        string str_out_bill_id = CommadMethod.getNextId(IdLeftPart, "");
            //                        InsertCommandBuilder ins = new InsertCommandBuilder("prd_out_bill");
            //                        ins.InsertColumn("prd_out_bill_id", str_out_bill_id);
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
            //                        string CompairSql = @"SELECT prd_Stock.prd_stock_id,prd_Stock.prd_batch_id, prd_Stock.materials_id, prd_batch.hwh, prd_Stock.qty, 
            //                                                  prd_batch.pch, prd_batch.str_in_date
            //                                            FROM prd_Stock INNER JOIN
            //                                                  prd_batch ON prd_Stock.prd_batch_id = prd_batch.prd_batch_id
            //                                            WHERE (prd_Stock.materials_id = '" + dt.Rows[i]["materials_id"].ToString().Trim() + @"' AND (prd_batch.hwh = '" + dt.Rows[i]["hwh"].ToString() + @"')) AND 
            //                                            (prd_Stock.store_id = '" + store_id + @"')
            //                                            ORDER BY prd_Stock.materials_id, prd_batch.str_in_date";
            //                        DataTable dtItems = new SelectCommandBuilder().ExecuteDataTable(CompairSql);
            //                        if (id != dtItems.Rows[0]["materials_id"].ToString().Trim() || remainQty == 0)
            //                        {
            //                            id = dtItems.Rows[0]["materials_id"].ToString().Trim();
            //                            remainQty = dicRemainQty[id];
            //                        }
            //                        double diff = Math.Abs(diffQty);
            //                        if (dtItems != null && dtItems.Rows.Count != 0)
            //                        {
            //                            for (int j = 0; j < dtItems.Rows.Count; j++)
            //                            {
            //                                time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //                                double compairQty = Convert.ToInt32(dtItems.Rows[j]["qty"]);
            //                                diff -= compairQty;
            //                                if (diff == 0)
            //                                {
            //                                    remainQty -= compairQty;
            //                                    if (dicRemainQty.ContainsKey(dtItems.Rows[0]["materials_id"].ToString().Trim()))
            //                                    {
            //                                        dicRemainQty[dtItems.Rows[0]["materials_id"].ToString().Trim()] = remainQty;
            //                                    }
            //                                    else
            //                                    {
            //                                        dicRemainQty.Add(dtItems.Rows[0]["materials_id"].ToString().Trim(), remainQty);
            //                                    }
            //                                    string inbatchId = CommadMethod.getNextId("SL", "1010A");
            //                                    InsertCommandBuilder inst = new InsertCommandBuilder("prd_out_bill_detail");
            //                                    inst.InsertColumn("prd_out_bill_id", str_out_bill_id);
            //                                    inst.InsertColumn("materials_id", dtItems.Rows[j]["materials_id"].ToString().Trim());
            //                                    inst.InsertColumn("prd_batch_id", inbatchId);
            //                                    inst.InsertColumn("qty", compairQty);
            //                                    inst.InsertColumn("can_sale", "Y");
            //                                    sqlList.Add(inst.getInsertCommand());
            //                                    string sqlDetail = "insert into materials_flow(materials_flow_id,materials_id,str_date,store_id,prd_batch_id,qty,remain_qty,is_str_in,str_type_name,str_bill_id,operator_name)";
            //                                    sqlDetail += " values ('" + CommadMethod.getNextId("SM", "0101") + "','" + dtItems.Rows[j]["materials_id"].ToString().Trim() + "', getdate(), '" + store_id + "','" + inbatchId + "',";
            //                                    sqlDetail += diffQty + ", " + (dicRemainQty[dtItems.Rows[0]["materials_id"].ToString().Trim()]) + ",'N','盘亏出库','" + str_out_bill_id + "','Admin')";
            //                                    sqlList.Add(sqlDetail);
            //                                    string Rsql = "delete from prd_Stock where materials_id='" + dtItems.Rows[j]["materials_id"].ToString().Trim() + "' and prd_batch_id='" + dtItems.Rows[j]["prd_batch_id"].ToString() + "'";
            //                                    string Dsql = "delete from prd_batch where materials_id='" + dtItems.Rows[j]["materials_id"].ToString().Trim() + "' and prd_batch_id='" + dtItems.Rows[j]["prd_batch_id"].ToString() + "'";
            //                                    sqlList.Add(Rsql);
            //                                    sqlList.Add(Dsql);
            //                                    InsertCommandBuilder insss = new InsertCommandBuilder("prd_batch");
            //                                    insss.InsertColumn("prd_batch_id", inbatchId);
            //                                    insss.InsertColumn("materials_id", dtItems.Rows[j]["materials_id"].ToString().Trim());
            //                                    insss.InsertColumn("pch", dtItems.Rows[j]["pch"].ToString());
            //                                    insss.InsertColumn("hwh", dtItems.Rows[j]["hwh"].ToString());
            //                                    insss.InsertColumn("str_in_date", time);
            //                                    sqlList.Add(insss.getInsertCommand());
            //                                    break;
            //                                }
            //                                else if (diff > 0)
            //                                {
            //                                    remainQty -= compairQty;
            //                                    if (dicRemainQty.ContainsKey(dtItems.Rows[0]["materials_id"].ToString().Trim()))
            //                                    {
            //                                        dicRemainQty[dtItems.Rows[0]["materials_id"].ToString().Trim()] = remainQty;
            //                                    }
            //                                    else
            //                                    {
            //                                        dicRemainQty.Add(dtItems.Rows[0]["materials_id"].ToString().Trim(), remainQty);
            //                                    }
            //                                    string inbatchId = CommadMethod.getNextId("SL", "1010A");
            //                                    double qty = 0 - Convert.ToDouble(dtItems.Rows[j]["qty"]);
            //                                    InsertCommandBuilder inst = new InsertCommandBuilder("prd_out_bill_detail");
            //                                    inst.InsertColumn("prd_out_bill_id", str_out_bill_id);
            //                                    inst.InsertColumn("materials_id", dtItems.Rows[j]["materials_id"].ToString().Trim());
            //                                    inst.InsertColumn("prd_batch_id", dtItems.Rows[j]["prd_batch_id"].ToString());
            //                                    inst.InsertColumn("qty", Math.Abs(qty));
            //                                    inst.InsertColumn("can_sale", "Y");
            //                                    string sqlDetail = "insert into materials_flow(materials_flow_id,materials_id,str_date,store_id,prd_batch_id,qty,remain_qty,is_str_in,str_type_name,str_bill_id,operator_name)";
            //                                    sqlDetail += " values ('" + CommadMethod.getNextId("SM", "0101") + "','" + dtItems.Rows[j]["materials_id"].ToString().Trim() + "', getdate(), '" + store_id + "','" + inbatchId + "',";
            //                                    sqlDetail += 0 - compairQty + "," + (dicRemainQty[dtItems.Rows[0]["materials_id"].ToString().Trim()]) + ",'N','盘亏出库','" + str_out_bill_id + "','Admin')";
            //                                    sqlList.Add(sqlDetail);
            //                                    string Rsql = "delete from prd_Stock where materials_id='" + dtItems.Rows[j]["materials_id"].ToString().Trim() + "' and prd_batch_id='" + dtItems.Rows[j]["prd_batch_id"].ToString() + "'";
            //                                    string Dsql = "delete from prd_batch where materials_id='" + dtItems.Rows[j]["materials_id"].ToString().Trim() + "' and prd_batch_id='" + dtItems.Rows[j]["prd_batch_id"].ToString() + "'";
            //                                    sqlList.Add(inst.getInsertCommand());
            //                                    sqlList.Add(Rsql);
            //                                    sqlList.Add(Dsql);
            //                                    InsertCommandBuilder insss = new InsertCommandBuilder("prd_batch");
            //                                    insss.InsertColumn("prd_batch_id", inbatchId);
            //                                    insss.InsertColumn("materials_id", dtItems.Rows[j]["materials_id"].ToString().Trim());
            //                                    insss.InsertColumn("pch", dtItems.Rows[j]["pch"].ToString());
            //                                    insss.InsertColumn("hwh", dtItems.Rows[j]["hwh"].ToString());
            //                                    insss.InsertColumn("str_in_date", time);
            //                                    sqlList.Add(insss.getInsertCommand());
            //                                    continue;
            //                                }
            //                                else if (diff < 0)
            //                                {
            //                                    remainQty -= (compairQty + diff);
            //                                    if (dicRemainQty.ContainsKey(dtItems.Rows[0]["materials_id"].ToString().Trim()))
            //                                    {
            //                                        dicRemainQty[dtItems.Rows[0]["materials_id"].ToString().Trim()] = remainQty;
            //                                    }
            //                                    else
            //                                    {
            //                                        dicRemainQty.Add(dtItems.Rows[0]["materials_id"].ToString().Trim(), remainQty);
            //                                    }
            //                                    string inbatchId = CommadMethod.getNextId("SL", "1010A");
            //                                    InsertCommandBuilder inst = new InsertCommandBuilder("prd_out_bill_detail");
            //                                    inst.InsertColumn("prd_out_bill_id", str_out_bill_id);
            //                                    inst.InsertColumn("materials_id", dtItems.Rows[j]["materials_id"].ToString().Trim());
            //                                    inst.InsertColumn("prd_batch_id", dtItems.Rows[j]["prd_batch_id"].ToString());
            //                                    inst.InsertColumn("qty", Math.Abs(diffQty));
            //                                    inst.InsertColumn("can_sale", "Y");
            //                                    sqlList.Add(inst.getInsertCommand());
            //                                    string Usql = "update prd_Stock set qty = '" + Math.Abs(diff) + "' where materials_id='" + dtItems.Rows[j]["materials_id"].ToString().Trim() + "' and prd_batch_id='" + dtItems.Rows[j]["prd_batch_id"].ToString() + "'";
            //                                    sqlList.Add(Usql);
            //                                    string sqlDetail = "insert into materials_flow(materials_flow_id,materials_id,str_date,store_id,prd_batch_id,qty,remain_qty,is_str_in,str_type_name,str_bill_id,operator_name)";
            //                                    sqlDetail += " values ('" + CommadMethod.getNextId("SM", "0101") + "','" + dtItems.Rows[j]["materials_id"].ToString().Trim() + "', getdate(), '" + store_id + "','" + inbatchId + "',";
            //                                    sqlDetail += 0 - (compairQty + diff) + ", " + (dicRemainQty[dtItems.Rows[0]["materials_id"].ToString().Trim()]) + ",'N','盘亏出库','" + str_out_bill_id + "','Admin')";
            //                                    sqlList.Add(sqlDetail);
            //                                    InsertCommandBuilder insss = new InsertCommandBuilder("prd_batch");
            //                                    insss.InsertColumn("prd_batch_id", inbatchId);
            //                                    insss.InsertColumn("materials_id", dtItems.Rows[j]["materials_id"].ToString().Trim());
            //                                    insss.InsertColumn("pch", dtItems.Rows[j]["pch"].ToString());
            //                                    insss.InsertColumn("hwh", dtItems.Rows[j]["hwh"].ToString());
            //                                    insss.InsertColumn("str_in_date", time);
            //                                    sqlList.Add(insss.getInsertCommand());
            //                                    break;
            //                                }
            //                            }
            //                        }
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

        private List<string> doStockIn(string pk_id, string store_id)
        {
            List<string> sqlList = new List<string>();
            string detailsql = @"SELECT pd_detail.materials_id, pd_detail.Qty, pd_detail.Pch, 
                                 pd_detail.hwh FROM pd_detail INNER JOIN  pd ON pd_detail.Bill_id = pd.Bill_Id where pd.Pk_id = '" + pk_id + "' and pd.isAdd = 'Y'";

            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(detailsql);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                double remainQty = 0;
                string id = i == 0 ? "" : dt.Rows[i - 1]["materials_id"].ToString().Trim();
                string inbatchId = CommadMethod.getNextId("SL", "1010A");
                InsertCommandBuilder inst = new InsertCommandBuilder("prd_Stock");
                inst.InsertColumn("prd_Stock_id", CommadMethod.getNextId(DateTime.Now.ToString("yyMMddhh"), ""));
                inst.InsertColumn("materials_id", dt.Rows[i]["materials_id"].ToString().Trim().Trim());
                inst.InsertColumn("qty", dt.Rows[i]["Qty"]);
                inst.InsertColumn("prd_batch_id", inbatchId);
                inst.InsertColumn("store_id", store_id);
                inst.InsertColumn("is_can_sale", "Y");
                sqlList.Add(inst.getInsertCommand());
                DateTime date = DateTime.Now;
                string IdLeftPart = "PH" + date.Year.ToString().Substring(2, 2) + date.Month.ToString().PadLeft(2, '0') + date.Day.ToString().PadLeft(2, '0');
                string Id = CommadMethod.getNextId(IdLeftPart, "");
                InsertCommandBuilder ins = new InsertCommandBuilder("prd_in_bill");
                ins.InsertColumn("prd_in_bill_id", Id);
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
                ins.InsertColumn("islocal", "Y");
                ins.InsertColumn("FinImport", "N");
                ins.InsertColumn("is_end", "N");
                string str_in_bill_Sql = ins.getInsertCommand();
                InsertCommandBuilder inss = new InsertCommandBuilder("prd_in_bill_detail");
                inss.InsertColumn("prd_in_bill_id", Id);
                inss.InsertColumn("prd_batch_id", inbatchId);
                inss.InsertColumn("materials_id", dt.Rows[i]["materials_id"].ToString().Trim());
                inss.InsertColumn("qty", dt.Rows[i]["Qty"]);
                inss.InsertColumn("can_sale", "Y");
                inss.InsertColumn("piece", 1);
                string str_in_detail_Sql = inss.getInsertCommand();
                sqlList.Add(str_in_bill_Sql);
                sqlList.Add(str_in_detail_Sql);
                if (id != dt.Rows[i]["materials_id"].ToString().Trim())
                {
                    if (!dicRemainQty.ContainsKey(dt.Rows[i]["materials_id"].ToString().Trim()))
                    {
                        dicRemainQty.Add(dt.Rows[i]["materials_id"].ToString().Trim(), 0.00);
                    }
                    id = dt.Rows[i]["materials_id"].ToString().Trim();
                    remainQty = dicRemainQty[id];
                }
                remainQty += Convert.ToInt32(dt.Rows[i]["Qty"]);
                if (dicRemainQty.ContainsKey(dt.Rows[i]["materials_id"].ToString().Trim()))
                {
                    dicRemainQty[dt.Rows[i]["materials_id"].ToString().Trim()] = remainQty;
                }
                else
                {
                    dicRemainQty.Add(dt.Rows[i]["materials_id"].ToString().Trim(), remainQty);
                }
                string sqlDetail = "insert into materials_flow(materials_flow_id,materials_id,str_date,store_id,prd_batch_id,qty,remain_qty,is_str_in,str_type_name,str_bill_id,operator_name)";
                sqlDetail += " values ('" + CommadMethod.getNextId("SM", "0101") + "','" + dt.Rows[i]["materials_id"].ToString().Trim() + "', getdate(), '" + store_id + "','" + inbatchId + "',";
                sqlDetail += dt.Rows[i]["Qty"] + ", " + dicRemainQty[dt.Rows[i]["materials_id"].ToString().Trim()] + ",'Y','21','" + pk_id + "','Admin')";
                sqlList.Add(sqlDetail);
                InsertCommandBuilder insss = new InsertCommandBuilder("prd_batch");
                insss.InsertColumn("prd_batch_id", inbatchId);
                insss.InsertColumn("materials_id", dt.Rows[i]["materials_id"].ToString().Trim());
                insss.InsertColumn("pch", dt.Rows[i]["pch"].ToString());
                insss.InsertColumn("hwh", dt.Rows[i]["hwh"].ToString());
                insss.InsertColumn("str_in_date", time);
                sqlList.Add(insss.getInsertCommand());
            }
            return sqlList;

        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string pk_id = gvData.SelectedRow.Cells[1].Text.ToString().Trim().ToUpper();
            deletePDData(pk_id, true);
        }

        private void deletePDData(string pk_id, bool alert)
        {
            string mainsql = "delete from pre_prd_pk where prd_pk_id='" + pk_id + "'";
            string detailsql = "delete from pre_prd_pk_Detail where prd_pk_id='" + pk_id + "'";
            string billsql = "delete from pd where pk_id='" + pk_id + "'";
            string billdetailsql = "delete from pd_detail where exists(select bill_id from pd where bill_id=pd_detail.bill_id and pk_id='" + pk_id + "')";
            List<string> sList = new List<string>();
            sList.Add(mainsql);
            if (Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar("select count(*) from pre_prd_pk_Detail where prd_pk_id='" + pk_id + "'")) != 0)
            {
                sList.Add(detailsql);
            }
            int cc = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar("select Count(*) from pd_detail where exists(select bill_id from pd where bill_id=pd_detail.bill_id and pk_id='" + pk_id + "')"));
            if (cc != 0)
            {
                sList.Add(billdetailsql);
            }
            if (Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar("select count(*) from pd where pk_id='" + pk_id + "'")) != 0)
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
        private void PDData(string pk_id, bool alert)
        {
            string mainsql = "update pre_prd_pk set islocal = 'A' where prd_pk_id='" + pk_id + "'";
            //string detailsql = "delete from pre_prd_pk_Detail where prd_pk_id='" + pk_id + "'";
            string billsql = "update pd set status = 'A' where pk_id='" + pk_id + "'";
            //string billdetailsql = "update pd_detail where exists(select bill_id from pd where bill_id=pd_detail.bill_id and pk_id='" + pk_id + "')";
            List<string> sList = new List<string>();
            sList.Add(mainsql);
            //if (Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar("select count(*) from pre_prd_pk_Detail where prd_pk_id='" + pk_id + "'")) != 0)
            //{
            //    sList.Add(detailsql);
            //}
            //int cc = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar("select Count(*) from pd_detail where exists(select bill_id from pd where bill_id=pd_detail.bill_id and pk_id='" + pk_id + "')"));
            //if (cc != 0)
            //{
            //    sList.Add(billdetailsql);
            //}
            if (Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar("select count(*) from pd where pk_id='" + pk_id + "'")) != 0)
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

    }
}