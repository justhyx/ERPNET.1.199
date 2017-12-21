using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data;
using ERPPlugIn.Class;
using System.Data.SqlClient;

namespace ERPPlugIn.MaterialForecastManager
{
    public partial class ForecastInput : PageBase
    {
        public string ip { get { return ViewState["ip"].ToString(); } set { ViewState["ip"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                getCustomer();
            }
        }

        protected void Unnamed2_Click(object sender, EventArgs e)
        {
            if (btnUpload.Text == Resources.Resource.yl)
            {
                #region  将上传的Excel数据显示在GridView中
                if (FileUpload1.HasFile == false)
                {
                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "error", "<script>alert('" + Resources.Resource.alertxzwj + "!')</script>");
                    return;
                }
                string IsXls = System.IO.Path.GetExtension(FileUpload1.FileName).ToString().ToLower();
                if (IsXls != ".xls")
                {
                    Response.Write("<script>alert('" + Resources.Resource.alertxzexcel + "')</script>");
                    return;
                }
                btnUpload.Text = Resources.Resource.qr;
                Button1.Text = Resources.Resource.qx;
                string path = Server.MapPath("~/UploadExcel/");
                string strpath = FileUpload1.PostedFile.FileName.ToString();
                //string filename = FileUpload1.FileName;
                FileUpload1.PostedFile.SaveAs(path + FileUpload1.FileName);
                string Filename = path + FileUpload1.FileName;

                FileStream file = new FileStream(Filename, FileMode.Open, FileAccess.Read);
                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                Sheet sheet = hssfworkbook.GetSheetAt(0);
                DataTable table = new DataTable();
                Row headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;
                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                    table.Columns.Add(column);
                }
                int rowCount = sheet.LastRowNum;
                for (int i = (sheet.FirstRowNum + 1); i < sheet.LastRowNum + 1; i++)
                {
                    Row row = sheet.GetRow(i);
                    DataRow dataRow = table.NewRow();

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow[j] = row.GetCell(j).ToString();
                    }
                    table.Rows.Add(dataRow);
                }
                hssfworkbook = null;
                sheet = null;
                this.gvExcel.DataSource = table;
                this.gvExcel.DataBind();
                File.Delete(Filename);
                #endregion
            }
            else
            {
                #region 确认上传数据
                if (gvExcel.Rows.Count == 0)
                {
                    Response.Write("<script>alert('" + Resources.Resource.alertksj + "')</script>");
                    return;
                }
                string Text10 = "";
                List<string> sList = getSQLList(this.gvExcel);
                UpdateCommandBuilder up = new UpdateCommandBuilder();
                up.ExecuteNonQuery("update Client_FC set status='Y' where len(no_id)>1");
                up.ExecuteNonQuery("update Client_FC set delivery_no='" + Text10 + "'+convert(varchar(6),delivery_date,112) where ( len(delivery_no) =0 and status='N')");
                up.ExecuteNonQuery("update Client_FC set operation_flag='N' where (operation_flag is null and status='N' )");
                SqlParameter[] parm = new SqlParameter[] 
                { 
                    new SqlParameter("@Kh_id",txtCName.SelectedItem.Value.Split('|')[0].Trim()),
                    new SqlParameter("@Dly_no",txtCName.SelectedItem.Value.Split('|')[1].Trim()+Convert.ToDateTime(txtDate.Text).ToString("yyyyMM")),
                    new SqlParameter("@dat",Convert.ToDateTime(txtDate.Text).ToString("yyyyMM"))
                };
                SqlHelper.ExecuteNonQuery(base.ConnectionString, CommandType.StoredProcedure, "Client_FC_Ver", parm);
                up.ExecuteNonQuery("Update Client_FC Set lb1_id = goods.lb1_id FROM Client_FC INNER JOIN goods ON goods.goods_name = Client_FC.goods_name WHERE status = 'N'");
                parm = new SqlParameter[] { new SqlParameter("@Op_id", ip.Trim() + HttpContext.Current.Request.Cookies["cookie"].Values["id"].Trim()) };
                SqlHelper.ExecuteNonQuery(base.ConnectionString, CommandType.StoredProcedure, "Demands_goods_expand", parm);
                parm = new SqlParameter[] { new SqlParameter("@Kh_id", txtCName.SelectedItem.Value.Split('|')[0].Trim()), new SqlParameter("@Op_id", ip.Trim() + HttpContext.Current.Request.Cookies["cookie"].Values["id"].Trim()), new SqlParameter("@dat", Convert.ToDateTime(txtDate.Text).ToString("yyyyMM")) };
                SqlHelper.ExecuteNonQuery(base.ConnectionString, CommandType.StoredProcedure, "Client_FC_ID", parm);
                up.ExecuteNonQuery("update Client_FC set status='Y' where len(no_id)>1");
                InsertCommandBuilder insert = new InsertCommandBuilder();
                int count = insert.ExcutTransaction(sList);
                if (count != 0)
                {
                    Response.Write("<script>alert('" + Resources.Resource.alterOk + "')</script>");
                    this.gvExcel.DataSource = null;
                    this.gvExcel.DataBind();
                    btnUpload.Text = Resources.Resource.yl;
                }
                else
                {
                    Response.Write("<script>alert('" + Resources.Resource.alterfiald + "')</script>");
                }
                #endregion

            }

        }
        protected List<string> getSQLList(GridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Rows[i].Cells.Count; j++)
                {
                    if (dgv.Rows[i].Cells[j].Text == "&nbsp;")
                    {
                        dgv.Rows[i].Cells[j].Text = "";
                    }
                }
            }
            List<string> sList = new List<string>();
            InsertCommandBuilder ins = new InsertCommandBuilder("Client_FC");
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                ins.InsertColumn("NO_id", "1");
                ins.InsertColumn("goods_name", dgv.Rows[i].Cells[0].Text.ToString());
                //ins.InsertColumn("lb1_id", dgv.Rows[i].Cells[1].Text.ToString());
                ins.InsertColumn("client_id", dgv.Rows[i].Cells[2].Text.ToString());
                ins.InsertColumn("FC_qty", dgv.Rows[i].Cells[3].Text.ToString());
                ins.InsertColumn("delivery_date", Convert.ToDateTime(dgv.Rows[i].Cells[4].Text).ToString("yyyy-MM-dd HH:mm:ss"));
                ins.InsertColumn("delivery_no", dgv.Rows[i].Cells[5].Text.ToString());
                ins.InsertColumn("remark", txtRemark.Text.Trim());
                ins.InsertColumn("version_no", txtVer_No.Text.Trim());
                ins.InsertColumn("status", "N");
                ins.InsertColumn("operation_date", "getDate()");
                ins.InsertColumn("operator_id", HttpContext.Current.Request.Cookies["cookie"].Values["id"]);
                sList.Add(ins.getInsertCommand());
                ins.CommandClear();
            }
            return sList;
        }
        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (Button1.Text == Resources.Resource.qx)
            {
                btnUpload.Text = Resources.Resource.yl;
                this.gvExcel.DataSource = null;
                this.gvExcel.DataBind();
                Button1.Text = Resources.Resource.fh;
            }
            else
            {
                Button1.Text = Resources.Resource.qx;
                Response.Redirect("#");
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string sql = "select * from Client_FC where 1=1";
            if (!string.IsNullOrEmpty(txtDate.Text))
            {
                sql += " and ((CONVERT(VARCHAR(6), delivery_date, 112) = '" + DateTime.Parse(txtDate.Text.Trim()).ToString("yyyyMM") + "'))";
            }
            else
            {
                txtDate.Focus();
                return;
            }
            if (txtCName.Text.Trim() != "请选择")
            {
                if (txtCName.SelectedItem.Value.Split('|')[0] == "020000")
                {
                    sql += " and client_id = '020000'";
                }
                else
                {
                    sql += " and (client_id = '010009' or client_id = '010010' or client_id = '010011')";
                }
            }
            else
            {
                txtCName.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(txtVer_No.Text))
            {
                sql += " and version_no = '" + txtVer_No.Text.Trim() + "'";
            }
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            gvExcel.DataSource = dt;
            gvExcel.DataBind();
        }
        private void getCustomer()
        {
            string sql = "SELECT * From customer Where (Len(customer_id) > 2)";
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            txtCName.Items.Clear();
            txtCName.Items.Add("请选择");
            if (dt != null && dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    txtCName.Items.Add(new ListItem(dt.Rows[i]["customer_aname"].ToString().Trim(), dt.Rows[i]["customer_id"].ToString().Trim() + "|" + dt.Rows[i]["dm"].ToString().Trim() == "0" ? dt.Rows[i]["customer_id"].ToString().Trim().Substring(dt.Rows[i]["customer_id"].ToString().Trim().Length - 2, 2) : dt.Rows[i]["dm"].ToString().Trim()));
                }
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvExcel.Rows.Count == 0)
            {
                return;
            }
            Export(gvExcel);
        }
        public void Export(GridView dt)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            Sheet sheet = hssfworkbook.CreateSheet();

            Row HeaderRow = sheet.CreateRow(0);
            HeaderRow.CreateCell(0).SetCellValue("No");
            for (int j = 0; j < dt.HeaderRow.Cells.Count; j++)
            {
                HeaderRow.CreateCell(j + 1).SetCellValue(dt.HeaderRow.Cells[j].Text);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Row r = sheet.CreateRow(i + 1);
                r.CreateCell(0).SetCellValue(i + 1);
                for (int j = 0; j < dt.Rows[i].Cells.Count; j++)
                {
                    if (dt.Rows[i].Cells[j].GetType() == typeof(decimal) || dt.Rows[i].Cells[j].GetType() == typeof(int) || dt.Rows[i].Cells[j].GetType() == typeof(double) || dt.Rows[i].Cells[j].GetType() == typeof(Int64))
                    {
                        r.CreateCell(j + 1).SetCellValue(Convert.ToDouble(dt.Rows[i].Cells[j].Text == "&nbsp;" ? "" : dt.Rows[i].Cells[j].Text));
                    }
                    else
                    {
                        r.CreateCell(j + 1).SetCellValue(dt.Rows[i].Cells[j].Text == "&nbsp;" ? "" : dt.Rows[i].Cells[j].Text.ToString());
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
                Response.AddHeader("Content-Disposition:", "attachment; filename= ForecastInput" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls");
                Response.BinaryWrite(stream.ToArray());
            }
        }
    }
}