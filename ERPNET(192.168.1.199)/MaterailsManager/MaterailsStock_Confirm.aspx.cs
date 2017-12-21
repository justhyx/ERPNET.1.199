using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using ERPPlugIn.Class;

namespace ERPPlugIn.MaterailsManager
{
    public partial class MaterailsStock_Confirm : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT a.Bill_Id, a.Bill_no, a.Pk_id, ISNULL
                                      ((SELECT " + Resources.Resource.store + @"
                                      FROM store
                                      WHERE store_id = a.store_id), a.store_id) AS store_id, 
                                  pre_prd_pk.prd_pk_date AS pk_date, ISNULL
                                      ((SELECT operator_name
                                      FROM operator
                                      WHERE operator_id = a.Crt_emp), a.Crt_emp) AS Crt_emp, a.Crt_Date, a.Remark, 
                                  Status =(case when a.Status ='Y' then 'true' else 'false' end)
                            FROM pd a INNER JOIN
                                  pre_prd_pk ON a.Pk_id = pre_prd_pk.prd_pk_id WHERE a.status <> 'A'";
            if (!string.IsNullOrEmpty(txtStartDate.Text))
            {
                sql += " And CONVERT(varchar(100),pre_prd_pk.prd_pk_date,23) = '" + txtStartDate.Text + "'";
            }
            SelectCommandBuilder s = new SelectCommandBuilder();
            gvData.DataSource = s.ExecuteDataTable(sql);
            gvData.DataBind();
            gvDetailData.DataSource = null;
            gvDetailData.DataBind();
            gvData.SelectedIndex = 0;
            gvData_SelectedIndexChanged(sender, e);
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
            if (gvData.Rows.Count == 0)
            {
                return;
            }
            string sql = @"  SELECT a.Bill_id,   
                             a.prd_Batch_id,   
                             a.materials_id,   
                             a.Qty,   
                             a.Pch,   
                             a.Detail_id,   
                             a.yxq,   
                             a.PDate,   
                             a.Price,   
                             is_can_sale=(case when a.is_can_sale ='Y' then 'true' else 'false' end),
                             materials.name,   
                             materials.spec,   
                             vendor.vendor_name,   
                             materials.unit,   
                             materials.dm,
		                     materials.select_code,
			                 materials.texture,
			                 materials.color,
			                 a.hwh,
			                 a.is_new,
                             total = Isnull((a.Price*a.Qty),0)
                        FROM pd_detail a,   
                             materials,   
                             vendor  
                       WHERE ( materials.sccj_id *= vendor.vendor_id) and  
                             ( a.materials_id = materials.id ) and
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
            string sql = "update pd set status ='Y' where bill_id ='" + ViewState["billId"].ToString() + "' ";
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
            string sql = "update pd set status ='N' where bill_id ='" + ViewState["billId"].ToString() + "' ";
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
                string sql = "update pd set status ='Y' where bill_id ='" + gvData.Rows[i].Cells[0].Text.Trim() + "' ";
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
                string sql = "update pd set status ='N' where bill_id ='" + gvData.Rows[i].Cells[0].Text.Trim() + "' ";
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
            string sql = "delete from pd where bill_id='" + ViewState["billId"].ToString() + "'";
            string detailsql = "delete from pd_detail where bill_id='" + ViewState["billId"].ToString() + "'";
            int cunt = new InsertCommandBuilder().ExecuteNonQuery(sql);
            cunt += new InsertCommandBuilder().ExecuteNonQuery(detailsql);
            if (cunt != 0)
            {
                Response.Write("<script>alert('" + Resources.Resource.alterOk + "!')</script>");
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
            //            string sql = @"insert into pk_diff (materials_id, goods_name, hwh, diff)
            //                                  SELECT prd_stock.materials_id, materials.name AS 部番, prd_batch.hwh AS 货位号, 
            //                                  SUM((CASE status WHEN 'Y' THEN (pd_detail.Qty - prd_Stock.qty) 
            //                                  ELSE 0 - prd_Stock.qty END)) AS 差异
            //                                  FROM prd_Stock LEFT OUTER JOIN
            //                                  materials ON id = prd_stock.materials_id INNER JOIN
            //                                  prd_batch ON prd_Stock.prd_batch_id = prd_batch.prd_batch_id LEFT OUTER JOIN
            //                                  pd_detail ON id = pd_detail.materials_id AND 
            //                                  prd_batch.hwh = pd_detail.hwh AND prd_batch.pch = pd_detail.Pch LEFT JOIN
            //                                  pd ON pd_detail.bill_id = pd.bill_id
            //                                  where prd_stock.store_id = '" + store_id.Trim() + @"'
            //                                  GROUP BY prd_stock.materials_id, materials.name, prd_batch.hwh
            //                                  ORDER BY prd_batch.hwh,materials.name";
            //            new InsertCommandBuilder().ExecuteNonQuery(sql);
            //            string sql1 = @"SELECT prd_stock.materials_id, materials.name AS '" + Resources.Resource.bf + @"', prd_batch.hwh AS '" + Resources.Resource.hwh + @"', 
            //                                  prd_batch.pch AS '" + Resources.Resource.pch + @"', prd_Stock.qty AS '" + Resources.Resource.xtsl + @"', 
            //                                  (CASE status WHEN 'Y' THEN (pd_detail.Qty) ELSE 0 END) AS '" + Resources.Resource.pdsl + @"', 
            //                                  (CASE status WHEN 'Y' THEN (pd_detail.Qty - prd_Stock.qty) 
            //                                  ELSE 0 - prd_Stock.qty END) AS  '" + Resources.Resource.cy + @"', pk_diff.diff as  '" + Resources.Resource.cyhz + @"'
            //                            FROM prd_Stock LEFT OUTER JOIN
            //                                  materials ON id = prd_stock.materials_id INNER JOIN
            //                                  prd_batch ON prd_Stock.prd_batch_id = prd_batch.prd_batch_id LEFT OUTER JOIN
            //                                  pd_detail ON id = pd_detail.materials_id AND prd_batch.hwh = pd_detail.hwh AND 
            //                                  prd_batch.pch = pd_detail.Pch LEFT JOIN
            //                                  pd ON pd_detail.bill_id = pd.bill_id LEFT JOIN
            //                                  pk_diff ON prd_stock.materials_id = pk_diff.materials_id AND 
            //                                  prd_batch.hwh = pk_diff.hwh
            //                                  where prd_stock.store_id = '" + store_id.Trim() + @"'
            //                            ORDER BY prd_batch.hwh, materials.name, prd_batch.pch";
            string sql = @"select a.id,a.name  AS '" + Resources.Resource.bf + @"' , a.hwh AS '" + Resources.Resource.hwh + @"',a.qty AS '" + Resources.Resource.xtsl + @"', (CASE b.status WHEN 'Y' THEN (b.Qty) ELSE 0 END) AS '" + Resources.Resource.pdsl + @"',
                             '' AS '" + Resources.Resource.pch + @"',
                            (CASE b.status WHEN 'Y' THEN (b.Qty - a.qty) 
                                                              ELSE 0 - a.qty END) AS '" + Resources.Resource.cy + @"',
                            (CASE b.status WHEN 'Y' THEN (b.Qty - a.qty) 
                                                              ELSE 0 - a.qty END) AS '" + Resources.Resource.cyhz + @"',b.Bill_No as '" + Resources.Resource.pddh + @"',a.lb1_id as '属性' 
                            from(                            
                            SELECT materials.id,materials.name, prd_batch.hwh, SUM(prd_Stock.qty) AS qty,materials.lb1_id
                            FROM prd_Stock INNER JOIN
                                  prd_batch ON prd_Stock.prd_batch_id = prd_batch.prd_batch_id INNER JOIN
                                  materials ON prd_Stock.materials_id = materials.id
                                  where prd_stock.store_id = '" + store_id.Trim() + @"'
                            GROUP BY materials.id,materials.name, prd_batch.hwh,materials.lb1_id) a left join (SELECT pd_detail.materials_id, pd_detail.hwh, SUM(pd_detail.Qty) AS qty, pd.Status, 
                                  pd.Bill_no
                            FROM pd INNER JOIN
                                  pd_detail ON pd.Bill_Id = pd_detail.Bill_id
                            GROUP BY pd_detail.materials_id, pd_detail.hwh, pd.Status, pd.Bill_no, pd.Pk_id
                            HAVING (pd.Pk_id = '" + pk_id + @"')) b on a.id=b.materials_id and a.hwh=b.hwh
                            order by a.name,a.hwh";
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
                    else if (dt.Rows[i].ItemArray[j].GetType() == typeof(double))
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