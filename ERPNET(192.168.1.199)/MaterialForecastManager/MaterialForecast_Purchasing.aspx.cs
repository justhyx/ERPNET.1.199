using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data.SqlClient;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using ERPPlugIn.Class;

namespace ERPPlugIn.MaterialForecastManager
{
    public partial class MaterialForecast_Purchasing : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindDDL();
                cbxRanColor.Checked = true;
            }
        }

        protected void btnRun_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStartDate.Text) || string.IsNullOrEmpty(txtEndDate.Text) || Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text))
            {
                Response.Write("<script>alert('" + Resources.Resource.alertsjcw + "')</script>");
                return;
            }
            string UserId = "0000";//HttpContext.Current.Request.Cookies["cookie"].Values["id"];
            string startDate = Convert.ToDateTime(txtStartDate.Text).ToString("yyyyMMdd");
            string endDate = Convert.ToDateTime(txtEndDate.Text).ToString("yyyyMMdd");
            btnUpdate_Click(sender, e);
            if (ddlRunType.SelectedItem.Text == Resources.Resource.wlyc)
            {
                new DeleteCommandBuilder().ExecuteNonQuery("delete from Materials_total_prediction where op_id='" + UserId + "'");
                SqlParameter[] parm = new SqlParameter[] 
                {
                     new SqlParameter("@Dat", Convert.ToDateTime(txtStartDate.Text).ToString("yyyyMMdd")),
                     new SqlParameter("@Dbt",Convert.ToDateTime(txtEndDate.Text).ToString("yyyyMMdd")),
                     new SqlParameter("@OP_id",UserId)
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.conStr, CommandType.StoredProcedure, "Purchase_update_MO", parm);
            }
            else
            {
                SqlParameter[] parm = new SqlParameter[] 
                {
                     new SqlParameter("@Dct",Convert.ToDateTime(txtEndDate.Text).ToString("yyyyMM")),
                     new SqlParameter("@Dbt",endDate),
                     new SqlParameter("@OP_id",UserId)
                };
                SqlHelper.ExecuteDataset(SqlHelper.conStr, CommandType.StoredProcedure, "Purchase_calculate", parm);
            }
            btnSearch_Click(sender, e);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvDetail.DataSource = null;
            gvDetail.DataBind();
            if (string.IsNullOrEmpty(txtEndDate.Text))
            {
                return;
            }
            if (ddlRunType.SelectedItem.Value == "0")
            {

                gvExcel.DataSource = GetData();
                gvExcel.DataBind();
            }
            else
            {
                gvExcel.DataSource = GetPurchaseData();
                gvExcel.DataBind();
            }
        }

        private DataTable GetPurchaseData()
        {
            string UserId = "0000";//HttpContext.Current.Request.Cookies["cookie"].Values["id"];
            string sql = null;
            if (cbxRanColor.Checked == true)
            {
                sql = @"SELECT item_Data, name, spec, color,round( daily_demand_qty,1) as daily_demand_qty, monthly_demand_qty, current_stock, 
                           producing_stock , gds_po_convert, gds_stock_convert, non_arrive_prd_qty, next_non_arrive_prd_qty, purchase_qty, crushing_demand_qty,
                           crushing_stock_F , crushing_stock_2016, client_id, op_id  FROM materials_purchase_list where  op_id='" + UserId + "' and Daily_demand_qty > 0";
                if (!string.IsNullOrEmpty(txtMName.Text))
                {
                    sql += " And name = '" + txtMName.Text.Trim() + "'";
                }
                if (ddlMProperties.SelectedItem.Value != "0")
                {
                    sql = @" SELECT item_Data, name, spec, color, Daily_demand_qty, monthly_demand_qty, current_stock, 
                           producing_stock , gds_po_convert, gds_stock_convert, non_arrive_prd_qty, next_non_arrive_prd_qty, purchase_qty, crushing_demand_qty,
                           crushing_stock_F , crushing_stock_2016, client_id, op_id  FROM materials_purchase_list a inner join materials on a.item_data =materials.id where  op_id='" + UserId + "' and Daily_demand_qty > 0" + " and materials.lb1_id='" + ddlMProperties.SelectedItem.Value + "'";
                }
            }
            else
            {
                sql = @"SELECT patriarchal_materials_name, SUM(p_m_demand_qty) AS total_demand_qty,  client_id, op_id From materials_purchase_list   GROUP BY op_id, patriarchal_materials_name, client_id HAVING (op_id = '" + UserId + "')";
                if (!string.IsNullOrEmpty(txtMName.Text))
                {
                    sql += " And name = '" + txtMName.Text.Trim() + "'";
                }
                if (ddlMProperties.SelectedItem.Value != "0")
                {
                    sql = @"SELECT patriarchal_materials_name, SUM(p_m_demand_qty) AS total_demand_qty,  client_id, op_id From materials_purchase_list a inner join materials on a.patriarchal_materials_name= materials.name WHERE  materials.lb1_id='" + ddlMProperties.SelectedItem.Value + "' GROUP BY op_id, patriarchal_materials_name, client_id HAVING (op_id = '" + UserId + "')";
                }
            }
            return new SelectCommandBuilder().ExecuteDataTable(sql);

        }

        private DataTable GetData()
        {
            string UserId = "0000";//HttpContext.Current.Request.Cookies["cookie"].Values["id"];
            string sql = @"SELECT  *  FROM Materials_total_prediction where op_id ='" + UserId + "'";
            if (ddlMProperties.SelectedItem.Value != "0")
            {
                sql += " And lb1_id = '" + ddlMProperties.SelectedItem.Value.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(txtMName.Text))
            {
                sql += " And name = '" + txtMName.Text.Trim() + "'";
            }
            if (cbxNegativeItem.Checked == true)
            {
                sql += " And end_stock < 0";
            }
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            return dt;
        }
        public void bindDDL()
        {
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable("SELECT lb1_id, lb1_name From lb1");
            ddlMProperties.Items.Add(new ListItem(Resources.Resource.qxz, "0"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddlMProperties.Items.Add(new ListItem(dt.Rows[i]["lb1_name"].ToString().Trim(), dt.Rows[i]["lb1_id"].ToString().Trim()));
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvExcel.Rows.Count != 0)
            {
                Export(GetData());
            }
        }

        public void Export(DataTable dt)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            Sheet sheet = hssfworkbook.CreateSheet();
            Row HeaderRow = sheet.CreateRow(0);
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                HeaderRow.CreateCell(j).SetCellValue(dt.Columns[j].ToString());
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Row r = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Rows[i].ItemArray.Length; j++)
                {
                    r.CreateCell(j).SetCellValue(dt.Rows[i].ItemArray[j].ToString());
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
                Response.AddHeader("Content-Disposition:", "attachment; filename= MaterialForecast_Purchasing" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls");
                Response.BinaryWrite(stream.ToArray());
            }
        }

        protected void gvExcel_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //----------鼠标移动到每项时颜色交替效果  

                e.Row.Attributes["onmouseover"] = "e=this.style.backgroundColor;this.style.backgroundColor='#FFFF99'";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor=e";
                //----------设置悬浮鼠标指针形状为小手"  
                e.Row.Attributes["style"] = "Cursor:hand";
                String evt = Page.ClientScript.GetPostBackClientHyperlink(sender as GridView, "Select$" + e.Row.RowIndex.ToString()); e.Row.Attributes.Add("onclick", evt);
            }
        }

        protected void gvExcel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRunType.SelectedItem.Value == "0")
            {
                txtMName.Text = gvExcel.SelectedRow.Cells[1].Text;
            }
            else
            {
                txtMName.Text = gvExcel.SelectedRow.Cells[0].Text;
            }
            gvDetail.DataSource = null;
            gvDetail.DataBind();
        }

        protected void btnCompair_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT goods.goods_name, goods_ration.item_Data, materials.Name ,
                            goods_ration.qty FROM goods_ration INNER JOIN goods ON goods_ration.goods_id = goods.goods_id INNER JOIN 
                            materials ON goods_ration.item_Data = materials.id WHERE (materials.name = '" + txtMName.Text.Trim() + "') ORDER BY goods.goods_name";
            gvDetail.DataSource = new SelectCommandBuilder().ExecuteDataTable(sql);
            gvDetail.DataBind();
        }

        protected void cbxRanColor_CheckedChanged(object sender, EventArgs e)
        {
            cbxYuanColor.Checked = !cbxRanColor.Checked;
        }

        protected void cbxYuanColor_CheckedChanged(object sender, EventArgs e)
        {
            cbxRanColor.Checked = !cbxYuanColor.Checked;
        }
        public string CheckUpdate()
        {
            string msg = "";
            msg = new SelectCommandBuilder().ExecuteDataTable("select * from goods where sk_scale is null").Rows.Count == 0 ? "" : "部品档案中的粉碎材使用比例为空值,请输入数据!";
            if (msg != "")
            {
                return msg;
            }
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable("SELECT goods_id, goods_name, qs From goods  Where (qs = 0)");
            if (dt != null && dt.Rows.Count != 0)
            {
                msg = Resources.Resource.bpmc + "：";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    msg += dt.Rows[i]["goods_name"].ToString() + ",";
                }
                msg += "的取数被设为零,请修正!";
            }
            return msg;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string msg = CheckUpdate();
            if (msg != "")
            {
                Response.Write("<script>alert('" + msg + "')</script>");
                return;
            }
            string UserId = "0000";//HttpContext.Current.Request.Cookies["cookie"].Values["id"];
            string Dct = Convert.ToDateTime(txtStartDate.Text).ToString("yyyyMMdd");
            string Dt = Convert.ToDateTime(txtStartDate.Text).AddDays(-1).ToString("yyyyMMdd");
            DateTime Dte = Convert.ToDateTime(txtStartDate.Text).AddDays(-2);
            string Dbt = Convert.ToDateTime(Dte).ToString("yyyyMM");
            string Dat = Convert.ToDateTime(txtEndDate.Text).ToString("yyyyMM");
            string Dtm = Convert.ToDateTime(txtEndDate.Text).ToString("yyyy/MM/dd");
            SqlParameter[] parm = new SqlParameter[] 
            {
                new SqlParameter("@Dat", Dat),
                new SqlParameter("@Dbt",Dbt),
                new SqlParameter("@OP_id",UserId),
                new SqlParameter("@Dtm",Dtm),
                new SqlParameter("@Dte",Dte.ToString("yyyyMMdd")),
                new SqlParameter("@Dt",Dt),
                new SqlParameter("@Dct",Dct),
                new SqlParameter("@combo1","Materials purchase"),
                new SqlParameter("@combo2",ddlArea.SelectedItem.Text)
            };
            SqlHelper.ExecuteNonQuery(SqlHelper.conStr, CommandType.StoredProcedure, "Purchase_update", parm);
        }
        // if riews of a if.excute  f
    }
}