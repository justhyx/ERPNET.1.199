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

namespace ERPPlugIn.MaterialControlManager
{
    public partial class BarcodRecordsExport : System.Web.UI.Page
    {
        public DataTable myTable { get { return ViewState["myTable"] as DataTable; } set { ViewState["myTable"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                myTable = new DataTable();
                lblZsh.Visible = false;
                txtZsh.Visible = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = "";
            if (rbtCustomer.SelectedItem.Value == "0")
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
                else if (string.IsNullOrEmpty(txtLeaderDate.Text))
                {
                    txtLeaderDate.Focus();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('请选择日期')</script>", false);
                    return;
                }
                else if (string.IsNullOrEmpty(txtsLeaderTime.Text))
                {
                    txtsLeaderTime.Focus();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('请选择使用时间')</script>", false);
                    return;
                }
                else if (string.IsNullOrEmpty(txteLeaderTime.Text))
                {
                    txteLeaderTime.Focus();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('请选择使用时间')</script>", false);
                    return;
                }

                if (rdoMode.SelectedItem.Value == "1")
                {
                    sql = @"select 备货单号,部番,计划出货数,出货检数,使用时间
                            from (SELECT 出货日期,out_id AS 备货单号,部番, SUM(计划出货数) AS 计划出货数, 使用时间
                            FROM 出货指示表
                            WHERE (使用时间 BETWEEN " + int.Parse(txtsLeaderTime.Text) * 100 + @" AND " + int.Parse(txteLeaderTime.Text) * 100 + @") AND (出货日期 = CONVERT(DATETIME, 
                                  '" + Convert.ToDateTime(txtLeaderDate.Text).ToString("yyyy-MM-dd") + @"', 102))
                            GROUP BY 出货日期,out_id,部番, 使用时间)a inner join
                            (select goods_name,sum(Qty) 出货检数 from  BarCodeRecords Where (create_date BETWEEN CONVERT(DATETIME,
                                             '" + txtStartDate.Text + @"', 102) AND CONVERT(DATETIME, '" + txtEndDate.Text + @"', 102))
                            group by goods_name)b
                            on a.部番 = b.goods_name INNER JOIN
                                              goods ON b.goods_name = goods.goods_name
                                        WHERE (goods.khdm = '010010')
                            UNION
                            SELECT a.out_id as 备货单号, BarCodeRecords.goods_name as 部番,
                                                              SUM(a.计划出货数) AS 计划出货数, SUM(BarCodeRecords.qty)
                                                              AS 出货检数, a.使用时间  FROM goods INNER JOIN
                                                              BarCodeRecords ON 
                                                              goods.goods_name = BarCodeRecords.goods_name LEFT OUTER JOIN
                            (SELECT 出货日期, out_id , 计划出货数 AS 计划出货数, 使用时间, 
                                  部番
                            FROM 出货指示表
                            WHERE (使用时间 BETWEEN " + int.Parse(txtsLeaderTime.Text) * 100 + @" AND " + int.Parse(txteLeaderTime.Text) * 100 + @") AND (出货日期 = CONVERT(DATETIME, 
                                  '" + Convert.ToDateTime(txtLeaderDate.Text).ToString("yyyy-MM-dd") + @"', 102))) a on BarCodeRecords.goods_name=a.部番
                            Where (BarCodeRecords.create_date BETWEEN CONVERT(DATETIME, 
                                                              '" + txtStartDate.Text + @"', 102) AND CONVERT(DATETIME, '" + txtEndDate.Text + @"', 102))
                            group by a.出货日期,a.out_id,BarCodeRecords.goods_name,a.使用时间,goods.khdm
                            Having (a.out_id is null) AND (goods.khdm = '010010') Order by  使用时间,部番";
                }
                else
                {
                    sql = @"select 备货单号,部番,计划出货数,出货检数,出货检数-计划出货数 as 差异数
                            from (SELECT 出货日期,out_id AS 备货单号,部番, SUM(计划出货数) AS 计划出货数
                            FROM 出货指示表
                            WHERE (使用时间 BETWEEN " + int.Parse(txtsLeaderTime.Text) * 100 + @" AND " + int.Parse(txteLeaderTime.Text) * 100 + @") AND (出货日期 = CONVERT(DATETIME, 
                                  '" + Convert.ToDateTime(txtLeaderDate.Text).ToString("yyyy-MM-dd") + @"', 102))
                            GROUP BY 出货日期,out_id,部番)a inner join
                            (select goods_name,sum(Qty) 出货检数 from  BarCodeRecords Where (create_date BETWEEN CONVERT(DATETIME, 
                                             '" + txtStartDate.Text + @"', 102) AND CONVERT(DATETIME, '" + txtEndDate.Text + @"', 102))
                            group by goods_name)b
                            on a.部番 = b.goods_name INNER JOIN
                                              goods ON b.goods_name = goods.goods_name
                                        WHERE (goods.khdm = '010010')
                            UNION 
                            SELECT a.out_id as 备货单号, BarCodeRecords.goods_name as 部番, 
                                                              SUM(a.计划出货数) AS 计划出货数, SUM(BarCodeRecords.qty) 
                                                              AS 出货检数, SUM(BarCodeRecords.qty) 
                                                              - SUM(a.计划出货数) AS 差异数 FROM goods INNER JOIN
                                                              BarCodeRecords ON 
                                                              goods.goods_name = BarCodeRecords.goods_name LEFT OUTER JOIN
                            (SELECT 出货日期, out_id , 计划出货数 AS 计划出货数, 使用时间, 
                                  部番
                            FROM 出货指示表
                            WHERE (使用时间 BETWEEN " + int.Parse(txtsLeaderTime.Text) * 100 + @" AND " + int.Parse(txteLeaderTime.Text) * 100 + @") AND (出货日期 = CONVERT(DATETIME, 
                                  '" + Convert.ToDateTime(txtLeaderDate.Text).ToString("yyyy-MM-dd") + @"', 102))) a on BarCodeRecords.goods_name=a.部番
                            Where (BarCodeRecords.create_date BETWEEN CONVERT(DATETIME, 
                                                              '" + txtStartDate.Text + @"', 102) AND CONVERT(DATETIME, '" + txtEndDate.Text + @"', 102))
                            group by a.出货日期,a.out_id,BarCodeRecords.goods_name,a.使用时间,goods.khdm
                            Having (a.out_id is null) AND (goods.khdm = '010010') Order by 差异数,部番";
                }
            }
            else
            {
                string detailSql = "";
                if (!string.IsNullOrEmpty(txtZsh.Text))
                {
                    detailSql = "jhzs_detail.zsh = '" + txtZsh.Text + "'";
                }
                sql = @"select a.zsh as '指示号',a.goods_name as '部番',a.jhsQty as 指示出货数,b.出货检数 ,b.出货检数-a.jhsQty as '差异' from
                        (SELECT jhzs_detail.zsh , goods.goods_name , SUM(jhzs_detail.qty) as jhsQty 
                                                FROM jhzs_detail INNER JOIN
                                                      goods ON jhzs_detail.goods_id = goods.goods_id where 1 = 1 " + detailSql + @"
                                                GROUP BY goods.goods_name, jhzs_detail.zsh) a inner join 
                        (SELECT goods_name, SUM(qty) AS 出货检数
                        FROM BarCodeRecords Where (create_date BETWEEN CONVERT(DATETIME,'" + txtStartDate.Text + @"', 102) AND CONVERT(DATETIME, '" + txtEndDate.Text + @"', 102))
                        GROUP BY goods_name) b on a.goods_name = b.goods_name";
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

            }
            myTable = new SelectCommandBuilder().ExecuteDataTable(sql);
            gvShowData.DataSource = myTable;
            gvShowData.DataBind();
        }

        protected void btnExpot_Click(object sender, EventArgs e)
        {
            if (myTable != null && myTable.Rows.Count != 0)
            {
                Export(myTable);
            }
        }
        public void Export(DataTable dt)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            Sheet sheet = hssfworkbook.CreateSheet();

            Row HeaderRow = sheet.CreateRow(0);
            HeaderRow.CreateCell(0).SetCellValue("No");
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                HeaderRow.CreateCell(j + 1).SetCellValue(dt.Columns[j].ToString());
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Row r = sheet.CreateRow(i + 1);
                r.CreateCell(0).SetCellValue(i + 1);
                for (int j = 0; j < dt.Rows[i].ItemArray.Length; j++)
                {
                    if (dt.Rows[i].ItemArray[j].GetType() == typeof(decimal) || dt.Rows[i].ItemArray[j].GetType() == typeof(int) || dt.Rows[i].ItemArray[j].GetType() == typeof(double) || dt.Rows[i].ItemArray[j].GetType() == typeof(Int64))
                    {
                        r.CreateCell(j + 1).SetCellValue(Convert.ToDouble(dt.Rows[i].ItemArray[j]));
                    }
                    else
                    {
                        r.CreateCell(j + 1).SetCellValue(dt.Rows[i].ItemArray[j].ToString());
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
                Response.AddHeader("Content-Disposition:", "attachment; filename= BarCodeRecords" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls");
                Response.BinaryWrite(stream.ToArray());
            }
        }

        protected void btnDetailExport_Click(object sender, EventArgs e)
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
            string sql = @"SELECT goods_name AS 部番, qty AS 数量, pdate AS 生产日期, sn AS 箱号,  create_date AS 扫描日期 FROM BarCodeRecords Where (create_date BETWEEN CONVERT(DATETIME, 
                                             '" + txtStartDate.Text + @"', 102) AND CONVERT(DATETIME, '" + txtEndDate.Text + @"', 102))";
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count != 0)
            {
                Export(dt);
            }
        }

        protected void gvShowData_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gvShowData_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "";
            if (rbtCustomer.SelectedItem.Value == "0")
            {
                sql = @"select id, 部番,计划出货数,出货检数,生产日期,箱号,扫描日期
                            from (SELECT 出货日期,out_id AS 备货单号,部番, SUM(计划出货数) AS 计划出货数
                            FROM 出货指示表
                            WHERE (使用时间 BETWEEN " + int.Parse(txtsLeaderTime.Text) * 100 + @" AND " + int.Parse(txteLeaderTime.Text) * 100 + @") AND (出货日期 = CONVERT(DATETIME, 
                                  '" + Convert.ToDateTime(txtLeaderDate.Text).ToString("yyyy-MM-dd") + @"', 102))
                            GROUP BY 出货日期,out_id,部番)a inner join
                            (select id, goods_name,Qty 出货检数,pdate AS 生产日期, sn AS 箱号, 
                            CONVERT(varchar(20), create_date, 120) AS 扫描日期 from  BarCodeRecords Where (create_date BETWEEN CONVERT(DATETIME, 
                                             '" + txtStartDate.Text + @"', 102) AND CONVERT(DATETIME, '" + txtEndDate.Text + @"', 102)))b
                            on a.部番 = b.goods_name
			    where a.部番='" + gvShowData.SelectedRow.Cells[3].Text.Trim() + "'";
            }
            else
            {
                sql = @"SELECT b.id, a.goods_name AS 部番, a.jhsQty AS 指示出货数, 
                              b.出货检数, b.生产日期, b.箱号, b.扫描日期
                        FROM (SELECT jhzs_detail.zsh, goods.goods_name, SUM(jhzs_detail.qty) AS jhsQty
                                FROM jhzs_detail INNER JOIN
                                      goods ON jhzs_detail.goods_id = goods.goods_id
                                GROUP BY goods.goods_name, jhzs_detail.zsh) a INNER JOIN
                                  (SELECT id, goods_name, qty AS 出货检数, pdate AS 生产日期, sn AS 箱号, 
                                       CONVERT(varchar(20), create_date, 120) AS 扫描日期
                                 FROM BarCodeRecords Where (create_date BETWEEN CONVERT(DATETIME, 
                                             '" + txtStartDate.Text + @"', 102) AND CONVERT(DATETIME, '" + txtEndDate.Text + @"', 102))) b ON a.goods_name = b.goods_name
			    where a.goods_name ='" + gvShowData.SelectedRow.Cells[3].Text.Trim() + "'";
            }
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            gvDetail.DataSource = dt;
            gvDetail.DataBind();
        }

        protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)// 判断当前项是否为页脚  
            {
                e.Row.Cells[0].Text = "汇总";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[5].Text = getLabelSum(gvDetail, 5).ToString("0.##");
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
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

        protected void gvShowData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sql = "delete from BarCodeRecords where goods_name = '" + gvShowData.Rows[e.RowIndex].Cells[3].Text.Trim() + "' and create_date BETWEEN CONVERT(DATETIME,'" + txtStartDate.Text + @"', 102) AND CONVERT(DATETIME, '" + txtEndDate.Text + @"', 102)";
            int o = new DeleteCommandBuilder().ExecuteNonQuery(sql);
            if (o > 0)
            {
                btnSearch_Click(sender, e as EventArgs);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('取消失败')</script>", false);
            }
        }

        protected void gvDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sql = "delete from BarCodeRecords where id = '" + gvDetail.Rows[e.RowIndex].Cells[2].Text.Trim() + "'";
            int o = new DeleteCommandBuilder().ExecuteNonQuery(sql);
            if (o > 0)
            {
                gvShowData_SelectedIndexChanged(sender, e as EventArgs);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('取消失败')</script>", false);
            }
        }

        protected void rbtCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtCustomer.SelectedItem.Value == "0")
            {
                lblZsh.Visible = false;
                txtZsh.Visible = false;
            }
            else
            {
                lblZsh.Visible = true;
                txtZsh.Visible = true;
            }
        }
    }
}