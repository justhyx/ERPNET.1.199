using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Data;
using ERPPlugIn.Class;

namespace ERPPlugIn.InventoryManager
{
    public partial class Inventory_Confirm : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT DISTINCT a.Bill_Id, a.Bill_no, a.Pk_id, ISNULL
                                      ((SELECT " + Resources.Resource.store + @"
                                      FROM store
                                      WHERE store_id = a.store_id), a.store_id) AS store_id, 
                                  pre_pk.pk_date AS pk_date, ISNULL
                                      ((SELECT operator_name
                                      FROM operator
                                      WHERE operator_id = a.Crt_emp), a.Crt_emp) AS Crt_emp, a.Crt_Date, a.Remark, 
                                 Status= (case when a.status ='Y' then 'true' else 'false' end) 
                            FROM tmp_pk_bill a INNER JOIN
                                  pre_pk ON a.Pk_id = pre_pk.pk_id INNER JOIN
                                  tmp_pk_detail b ON a.Bill_Id = b.Bill_id WHERE a.status <> 'A'";
            if (!string.IsNullOrEmpty(txtStartDate.Text))
            {
                sql += " WHERE (CONVERT(varchar(100),pre_pk.pk_date, 23) = '" + txtStartDate.Text + @"')";
            }
            SelectCommandBuilder s = new SelectCommandBuilder();
            gvData.DataSource = s.ExecuteDataTable(sql);
            gvData.DataBind();
            if (gvData.Rows.Count != 0)
            {
                gvData.SelectedIndex = 0;
                gvData_SelectedIndexChanged(sender, e);
            }
            gvDetailData.DataSource = null;
            gvDetailData.DataBind();
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
            string sql = @"  SELECT a.Bill_id,   
                             a.Batch_id,   
                             a.Goods_id,   
                             a.Qty,   
                             a.Pch,   
                             a.Detail_id,   
                             a.yxq,   
                             a.PDate,   
                             a.Price,   
                             is_can_sale=(case when a.is_can_sale ='Y' then 'true' else 'false' end),
                             total = isnull( a.qty*a.price,0),
                             goods.goods_name,   
                             goods.spec,   
                             sccj.sccj_name,   
                             goods.goods_unit,   
                             goods.dm,
			                 goods.select_code,
			                    prd_dictate_style = (select bb.style
										                    from prd_dictate bb,batch aa 
										                    where aa.dictate_id = bb.dictate_id
										                    and aa.batch_id = a.batch_id ) ,
			                    prd_dictate_dictate_name = (select bb.dictate_name 
										                    from prd_dictate bb,batch aa 
										                    where aa.dictate_id = bb.dictate_id
										                    and aa.batch_id = a.batch_id ) ,
			                    customer_name = (select cc.customer_name  
										                    from prd_dictate bb,batch aa,customer cc,xsht dd 
										                    where aa.dictate_id = bb.dictate_id
										                    and aa.batch_id = a.batch_id 
										                    and bb.order_id = dd.xsht_id
										                    and dd.customer_id = cc.customer_id ),
			                    a.hwh as 'hwh',
			                    goods.cz,
			                    goods.ys  
                        FROM tmp_pk_detail a,   
                             goods,   
                             sccj  
                       WHERE ( goods.sccj_id *= sccj.sccj_id) and  
                             ( a.Goods_id = goods.goods_id ) and
			                    a.bill_id ='" + gvData.SelectedRow.Cells[0].Text.Trim() + "'";
            ViewState["billId"] = gvData.SelectedRow.Cells[0].Text.Trim();
            gvDetailData.DataSource = new SelectCommandBuilder().ExecuteDataTable(sql);
            gvDetailData.DataBind();

        }

        protected void gvDetailData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)          // 判断当前项是否为页脚  
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[0].Text = Resources.Resource.hj + ":"; //"合计：";
                e.Row.Cells[7].Text = getLabelSum(gvDetailData, 7).ToString("0.##");
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[9].Text = getLabelSum(gvDetailData, 9).ToString("0.##");
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Left;
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
            if (((CheckBox)gvData.SelectedRow.Cells[8].Controls[0]).Checked)
            {
                Response.Write("<script>alert('" + Resources.Resource.alterDoubbleConfirm + "!')</script>");
                return;
            }
            SelectCommandBuilder s = new SelectCommandBuilder("pre_pk_Detail");
            //s.SelectColumn("Count(*)");
            //s.ConditionsColumn("Batch_id", lList[i].pch);
            //s.getSelectCommand();
            //int ct = Convert.ToInt32(s.ExecuteScalar());
            string sql = "update tmp_pk_bill set status ='Y' where bill_id ='" + ViewState["billId"].ToString() + "' ";
            int i = new UpdateCommandBuilder().ExecuteNonQuery(sql);
            if (i != 0)
            {
                Response.Write("<script>alert('" + Resources.Resource.alterOk + "!')</script>");
                btnSearch_Click(sender, e);
            }
        }

        protected void btnReCon_Click(object sender, EventArgs e)
        {
            if (gvData.SelectedIndex == -1)
            {
                Response.Write("<script>alert('" + Resources.Resource.alterwxz + "!')</script>");
                return;
            }
            if (!((CheckBox)gvData.SelectedRow.Cells[8].Controls[0]).Checked)
            {
                Response.Write("<script>alert('" + Resources.Resource.alterReConfrim + "!')</script>");
                return;
            }
            SelectCommandBuilder s = new SelectCommandBuilder("pre_pk_Detail");
            //s.SelectColumn("Count(*)");
            //s.ConditionsColumn("Batch_id", lList[i].pch);
            //s.getSelectCommand();
            //int ct = Convert.ToInt32(s.ExecuteScalar());
            string sql = "update tmp_pk_bill set status ='N' where bill_id ='" + ViewState["billId"].ToString() + "' ";
            int i = new UpdateCommandBuilder().ExecuteNonQuery(sql);
            if (i != 0)
            {
                Response.Write("<script>alert('" + Resources.Resource.alterOk + "!')</script>");
                btnSearch_Click(sender, e);
            }
        }

        protected void btnConfirmAll_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();
            for (int i = 0; i < gvData.Rows.Count; i++)
            {
                string sql = "update tmp_pk_bill set status ='Y' where bill_id ='" + gvData.Rows[i].Cells[0].Text.Trim() + "' ";
                sqlList.Add(sql);
            }
            int cunt = new InsertCommandBuilder().ExcutTransaction(sqlList);
            if (cunt != 0)
            {
                Response.Write("<script>alert('" + Resources.Resource.alterOk + "!')</script>");
                btnSearch_Click(sender, e);
            }
        }

        protected void btnReConAll_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();
            for (int i = 0; i < gvData.Rows.Count; i++)
            {
                string sql = "update tmp_pk_bill set status ='N' where bill_id ='" + gvData.Rows[i].Cells[0].Text.Trim() + "' ";
                sqlList.Add(sql);
            }
            int cunt = new InsertCommandBuilder().ExcutTransaction(sqlList);
            if (cunt != 0)
            {
                Response.Write("<script>alert('" + Resources.Resource.alterOk + "!')</script>");
                btnSearch_Click(sender, e);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (gvData.SelectedIndex == -1)
            {
                Response.Write("<script>alert('" + Resources.Resource.alterwxz + "!')</script>");
                return;
            }
            string sql = "delete from tmp_pk_bill where bill_id='" + ViewState["billId"].ToString() + "'";
            string detailsql = "delete from tmp_pk_detail where bill_id='" + ViewState["billId"].ToString() + "'";
            int cunt = new InsertCommandBuilder().ExecuteNonQuery(sql);
            cunt += new InsertCommandBuilder().ExecuteNonQuery(detailsql);
            if (cunt != 0)
            {
                Response.Write("<script>alert('" + Resources.Resource.alterOk + "!')</script>");
                gvDetailData.DataSource = null;
                gvDetailData.DataBind();
                btnSearch_Click(sender, e);
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvData.SelectedIndex == -1)
            {
                Response.Write("<script>alert('" + Resources.Resource.alterwxz + "')</script>");
                return;
            }
            string id = new SelectCommandBuilder().ExecuteDataTable("SELECT store_id FROM store where " + Resources.Resource.store + " ='" + gvData.SelectedRow.Cells[1].Text.Trim() + "'").Rows[0][0].ToString();
            Export(getData(id, gvData.SelectedRow.Cells[3].Text.Trim()));
        }
        public DataTable getData(string store_id, string pk_id)
        {
            //            new DeleteCommandBuilder().ExecuteNonQuery("delete from pk_diff");
            //            string sql1 = @"insert into pk_diff (materials_id, goods_name, hwh, diff)
            //                              SELECT stock_remain.goods_id, 
            //                              goods.goods_name AS '" + Resources.Resource.bpmc + @"', 
            //                              batch.hwh AS '" + Resources.Resource.hwh + @"', 
            //                              SUM((CASE status WHEN 'Y' THEN (cast(tmp_pk_detail.Qty - stock_remain.qty AS int)) 
            //                              ELSE 0 - cast(stock_remain.qty AS int) END)) 
            //                              AS '" + Resources.Resource.cy + @"'
            //                        FROM stock_remain LEFT OUTER JOIN
            //                              goods ON goods.goods_id = stock_remain.goods_id INNER JOIN
            //                              batch ON stock_remain.batch_id = batch.batch_id LEFT OUTER JOIN
            //                              tmp_pk_detail ON goods.goods_id = tmp_pk_detail.Goods_id AND 
            //                              batch.hwh = tmp_pk_detail.hwh AND batch.pch = tmp_pk_detail.Pch LEFT JOIN
            //                              tmp_pk_bill ON tmp_pk_detail.bill_id = tmp_pk_bill.bill_id
            //                        WHERE stock_remain.store_id = '" + store_id.Trim() + @"'
            //                        GROUP BY stock_remain.goods_id, goods.goods_name, batch.hwh
            //                        ORDER BY batch.hwh, goods.goods_name";
            //            new InsertCommandBuilder().ExecuteNonQuery(sql1);
            //            string sql = @"SELECT stock_remain.goods_id, goods.goods_name AS '" + Resources.Resource.bpmc + @"', batch.hwh AS '" + Resources.Resource.hwh + @"', 
            //                              batch.pch AS '" + Resources.Resource.pch + @"', cast(stock_remain.qty AS int) AS '" + Resources.Resource.xtsl + @"', 
            //                              (CASE status WHEN 'Y' THEN (cast(tmp_pk_detail.Qty AS int)) ELSE 0 END) 
            //                              AS '" + Resources.Resource.pdsl + @"', 
            //                              (CASE status WHEN 'Y' THEN (cast(tmp_pk_detail.Qty - stock_remain.qty AS int)) 
            //                              ELSE 0-cast(stock_remain.qty AS int) END) AS '" + Resources.Resource.cy + @"',pk_diff.diff as '" + Resources.Resource.cyhz + @"'
            //                                  FROM stock_remain LEFT OUTER JOIN
            //                                  goods ON goods.goods_id = stock_remain.goods_id INNER JOIN
            //                                  batch ON stock_remain.batch_id = batch.batch_id LEFT OUTER JOIN
            //                                  tmp_pk_detail ON goods.goods_id = tmp_pk_detail.Goods_id AND 
            //                                  batch.hwh = tmp_pk_detail.hwh AND batch.pch = tmp_pk_detail.Pch LEFT JOIN
            //                                  tmp_pk_bill ON tmp_pk_detail.bill_id = tmp_pk_bill.bill_id LEFT JOIN
            //                                  pk_diff ON stock_remain.goods_id = pk_diff.materials_id AND 
            //                                  batch.hwh = pk_diff.hwh
            //                                  where stock_remain.store_id = '" + store_id.Trim() + @"'
            //                            ORDER BY batch.hwh,  goods.goods_name,batch.pch";
            string sql = @"select a.goods_id,a.goods_name  AS '" + Resources.Resource.bf + @"' , a.hwh AS '" + Resources.Resource.hwh + @"',a.qty AS '" + Resources.Resource.xtsl + @"', (CASE b.status WHEN 'Y' THEN (b.Qty) ELSE 0 END) AS '" + Resources.Resource.pdsl + @"',
                             '' AS '" + Resources.Resource.pch + @"',
                            (CASE b.status WHEN 'Y' THEN (b.Qty - a.qty) 
                                                              ELSE 0 - a.qty END) AS '" + Resources.Resource.cy + @"',
                            (CASE b.status WHEN 'Y' THEN (b.Qty - a.qty) 
                                                              ELSE 0 - a.qty END) AS '" + Resources.Resource.cyhz + @"',b.Bill_No as '" + Resources.Resource.pddh + @"'
                            from(                            
                            SELECT goods.goods_id,goods.goods_name, batch.hwh, SUM(stock_remain.qty) AS qty
                            FROM stock_remain INNER JOIN
                                  batch ON stock_remain.batch_id = batch.batch_id INNER JOIN
                                  goods ON stock_remain.goods_id = goods.goods_id
                                  where stock_remain.store_id = '" + store_id.Trim() + @"'
                            GROUP BY goods.goods_id,goods.goods_name, batch.hwh) a left join (SELECT tmp_pk_detail.Goods_id, tmp_pk_detail.hwh, SUM(tmp_pk_detail.Qty) AS qty, 
                                  tmp_pk_bill.Status, tmp_pk_bill.Bill_no
                            FROM tmp_pk_bill INNER JOIN
                                  tmp_pk_detail ON tmp_pk_bill.Bill_Id = tmp_pk_detail.Bill_id
                            GROUP BY tmp_pk_detail.Goods_id, tmp_pk_detail.hwh, tmp_pk_bill.Status, 
                                  tmp_pk_bill.Bill_no, tmp_pk_bill.Pk_id
                            HAVING (tmp_pk_bill.Pk_id = '" + pk_id + @"')) b on a.goods_id=b.goods_id and a.hwh=b.hwh
                            order by a.goods_name,a.hwh";
            return new SelectCommandBuilder().ExecuteDataTable(sql);
        }
        public void Export(DataTable dt)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            Sheet sheet = hssfworkbook.CreateSheet();
            Row HeaderRow = sheet.CreateRow(0);
            for (int j = 1; j < dt.Columns.Count; j++)
            {
                HeaderRow.CreateCell(j - 1).SetCellValue(dt.Columns[j].ToString());
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Row r = sheet.CreateRow(i + 1);
                for (int j = 1; j < dt.Rows[i].ItemArray.Length; j++)
                {
                    if (dt.Rows[i].ItemArray[j].GetType() == typeof(Int32))
                    {
                        r.CreateCell(j - 1).SetCellValue(Convert.ToInt32(dt.Rows[i].ItemArray[j]));
                    }
                    else if (dt.Rows[i].ItemArray[j].GetType() == typeof(decimal))
                    {
                        r.CreateCell(j - 1).SetCellValue(Convert.ToDouble(dt.Rows[i].ItemArray[j]));
                    }
                    else
                    {
                        r.CreateCell(j - 1).SetCellValue(dt.Rows[i].ItemArray[j].ToString());
                    }
                }
            }
            for (int i = 0; i < dt.Rows.Count + 1; i++)
            {
                sheet.AutoSizeColumn(i);
            }

            sheet.ForceFormulaRecalculation = true;
            using (FileStream f = new FileStream(Server.MapPath("Create.xls"), FileMode.Create, FileAccess.ReadWrite))
            {
                hssfworkbook.Write(f);
            }
            // 在浏览器中直接下载    
            using (MemoryStream stream = new MemoryStream())
            {
                hssfworkbook.Write(stream);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition:", "attachment; filename= PKDiffrence" + DateTime.Now.ToString("yyyy-MM-dd HHmmss") + ".xls");
                Response.BinaryWrite(stream.ToArray());
            }
        }
    }
}