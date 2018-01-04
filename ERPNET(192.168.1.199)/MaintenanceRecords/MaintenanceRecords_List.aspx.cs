using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using System.Configuration;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using ERPPlugIn.Class;

namespace ERPPlugIn.MaintenanceRecords
{
    public partial class MaintenanceRecords_List : System.Web.UI.Page
    {

        public DataTable myTable { get { return ViewState["tb"] as DataTable; } set { ViewState["tb"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getPerson();
                myTable = new DataTable();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = "SELECT type as 内容, eqp_no as 设备编号, CONVERT(varchar(16), rely_date, 120) as 依赖日期, rely_dept as 依赖部门, repair_record as 处置结果, repair_time as 维修时间, repairman as 维修人员, doc_url AS imgId FROM Repair_records where 1 = 1";
            if (ddlType.SelectedItem.Value != "0")
            {
                sql += " and type = '" + ddlType.SelectedItem.Text.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(txtEQp.Text))
            {
                sql += " and eqp_no = '" + txtEQp.Text.Trim() + "'";
            }
            if (ddlPerson.SelectedItem.Value != "0")
            {
                sql += " and repairman like '%" + ddlPerson.SelectedItem.Text.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(txtResult.Text))
            {
                sql += " and repair_record like '%" + txtResult.Text.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(txtYear.Text))
            {
                sql += " and YEAR(rely_date) = '" + txtYear.Text.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(txtMoth.Text))
            {
                sql += " and MONTH(rely_date) = '" + int.Parse(txtMoth.Text.Trim()) + "'";
            }
            if (!string.IsNullOrEmpty(txtDay.Text))
            {
                sql += " and DAY(rely_date) = '" + int.Parse(txtDay.Text.Trim()) + "'";
            }
            if (ddlDept.SelectedItem.Value != "0")
            {
                sql += " and rely_dept = '" + ddlDept.SelectedItem.Text + "'";
            }
            myTable = new SelectCommandBuilder().ExecuteDataTable(sql);
            gvShowData.DataSource = myTable;
            gvShowData.DataBind();
        }
        public void getPerson()
        {
            SelectCommandBuilder s = new SelectCommandBuilder(ConnectionFactory.ConnectionString_hudsonwwwroot, "HUDSON_User");
            s.SelectColumn("UserName");
            s.ConditionsColumn("user_l", "19");
            s.getSelectCommand();
            DataTable dt = s.ExecuteDataTable();
            ddlPerson.Items.Clear();
            ddlPerson.Items.Add(new ListItem("请选择", "0"));
            if (dt != null && dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddlPerson.Items.Add(new ListItem(dt.Rows[i]["UserName"].ToString()));
                }
            }
        }

        protected void gvShowData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[0].Text = "汇总";
                e.Row.Cells[5].Text = getTotal().ToString();
            }
        }
        public decimal getTotal()
        {
            decimal total = 0;
            for (int i = 0; i < gvShowData.Rows.Count; i++)
            {
                total += Convert.ToDecimal(gvShowData.Rows[i].Cells[5].Text);
            }
            return total;
        }

        protected void btnExpot_Click(object sender, EventArgs e)
        {
            Export(myTable);
        }
        public void Export(DataTable dt)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            Sheet sheet = hssfworkbook.CreateSheet();
            Row HeaderRow = sheet.CreateRow(0);
            for (int j = 1; j < dt.Columns.Count - 1; j++)
            {
                HeaderRow.CreateCell(j - 1).SetCellValue(dt.Columns[j].ToString());
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Row r = sheet.CreateRow(i + 1);
                for (int j = 1; j < dt.Rows[i].ItemArray.Length - 1; j++)
                {
                    if (dt.Rows[i].ItemArray[j].GetType() == typeof(decimal))
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
                Response.AddHeader("Content-Disposition:", "attachment; filename= MaintenanceRecords" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls");
                Response.BinaryWrite(stream.ToArray());
            }
        }
    }
}