using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ERPPlugIn.MaterialControlManager
{
    public partial class BarcodRecords : System.Web.UI.Page
    {
        public DataTable myTable { get { return ViewState["myTable"] as DataTable; } set { ViewState["myTable"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                myTable = new DataTable();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGoods_name.Text) && (string.IsNullOrEmpty(txtStartDate.Text) || string.IsNullOrEmpty(txtEndDate.Text)))
            {
                return;
            }
            string sql = @"SELECT id,goods_name,qty,pdate ,sn ,create_date  FROM BarCodeRecords Where 1 = 1";
            if (!string.IsNullOrEmpty(txtGoods_name.Text))
            {
                sql += " AND (goods_name ='" + txtGoods_name.Text.Trim() + "' )";
            }
            if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
            {
                sql += " AND (create_date BETWEEN CONVERT(DATETIME,'" + txtStartDate.Text + @"', 102) AND CONVERT(DATETIME, '" + txtEndDate.Text + @"', 102))";
            }
            sql += " order by create_date,goods_name";
            myTable = new SelectCommandBuilder().ExecuteDataTable(sql);
            if (myTable != null && myTable.Rows.Count != 0)
            {
                gvShowData.DataSource = myTable;
                gvShowData.DataBind();
            }
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

        protected void gvShowData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = (gvShowData.Rows[e.RowIndex].Cells[0].FindControl("hfId") as HiddenField).Value;
            string sql = "delete from BarCodeRecords where id ='" + id + "'";
            int count = new DeleteCommandBuilder().ExecuteNonQuery(sql);
            if (count != 0)
            {
                btnSearch_Click(sender, e as EventArgs);
            }
        }
    }
}