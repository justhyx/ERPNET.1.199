using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ERPPlugIn.Goods_Ito
{
    public partial class goods_ito : System.Web.UI.Page
    {
        public string constr { get { return ViewState["constr"].ToString(); } set { ViewState["constr"] = value; } }
        public DataTable myTable { get { return ViewState["tb"] as DataTable; } set { ViewState["tb"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                myTable = new DataTable();
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStartDate.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Alert", "<script>alert('请输入开始日期')</script>");
                return;
            }
            if (string.IsNullOrEmpty(txtEndDate.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Alert", "<script>alert('请输入结束日期')</script>");
                return;
            }
            string startDate = Convert.ToDateTime(txtStartDate.Text).ToString("yyyyMMdd");
            string endDate = Convert.ToDateTime(txtEndDate.Text).ToString("yyyyMMdd");
            SelectCommandBuilder select = new SelectCommandBuilder();
            SqlParameter[] parm = new SqlParameter[]
            {
                new SqlParameter("@BeginDate",startDate),
                new SqlParameter("@EndDate",endDate)
            };
            DataSet ds = SqlHelper.ExecuteDataset(constr, CommandType.StoredProcedure, "goods_itos", parm);
            gvShowData.DataSource = ds.Tables[0];
            gvShowData.DataBind();
            if (ds.Tables.Count != 0)
            {
                myTable = ds.Tables[0];
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (btnQuery.Text != "计算")
            {
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Alert", "<script>alert('系统数据处理中,请稍后。')</script>");
                return;
            }
            if (gvShowData.Rows.Count == 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Alert", "<script>alert('无数据导出。')</script>");
                return;
            }
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
                Response.AddHeader("Content-Disposition:", "attachment; filename= Goods_Ito" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls");
                Response.BinaryWrite(stream.ToArray());
            }
        }
    }
}