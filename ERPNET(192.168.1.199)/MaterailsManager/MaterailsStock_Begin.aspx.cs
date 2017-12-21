using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using ERPPlugIn.Class;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

namespace ERPPlugIn.MaterailsManager
{
    public partial class MaterailsStock_Begin : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindDDL();
                gvData.DataSource = getData();
                gvData.DataBind();
            }
        }

        protected void btnBegin_Click(object sender, EventArgs e)
        {
            //string sql = "INSERT INTO pre_pk ( pk_id, pk_no, pk_date, store_id, verifier, operator_id, remark, str_in_bill_id, str_out_bill_id, islocal, pkms ) VALUES ( 'PK000009420101', '', '2012-7-17 8:49:33.056', '03', '', '0000', '', 'RK001406270101', 'CK000548050101', 'Y', '正常按批次盘库' )";
            string ss = "select count(*) from pre_prd_pk where store_id='" + ddlMaterialStock.SelectedItem.Value.Trim() + "' and CONVERT(varchar(100), prd_pk_Date, 112) = '" + DateTime.Now.ToString("yyyyMMdd") + "'";
            int count = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar(ss));
            if (count != 0)
            {
                Response.Write("<script>alert('" + Resources.Resource.alertWxqd + "')</script>");
                gvData.DataSource = getData();
                gvData.DataBind();
                return;
            }
            string sql = @" SELECT materials.dm,
                            materials.name,    
                            materials.spec,    
                            sccj.sccj_name as sccjname,    
                            materials.unit,  
                            prd_batch.pch as pch,  
                            price =  prd_batch.price,    
                            prd_stock.qty as zmsl,    
                            prd_stock.qty as pdsl,
                            prd_batch.yxq,
                            ' ' as stock_type_id,  
                            prd_stock.is_can_sale, 
                            prd_batch.str_in_date ,
                            prd_stock.prd_batch_id ,
                            prd_stock.prd_stock_id  ,
                            prd_stock.materials_id,
                            'ddd' as pk_id,
                            'N' as disobey,
                            prd_stock.qty as zmsl1, 
                            prd_stock.qty as pdsl1,
                            prd_batch.mjpch,
                            materials.texture,
                            materials.color,
                            prd_batch.hwh
                                FROM materials,    
                            prd_stock,
                            prd_batch,
                            sccj
                               WHERE ( prd_stock.materials_id = materials.id ) and
                            prd_stock.prd_batch_id = prd_batch.prd_batch_id and 
                            materials.sccj_id *= sccj.sccj_id and 
                              prd_stock.store_id = '" + ddlMaterialStock.SelectedItem.Value.Trim() + @"'  and 
                              prd_stock.materials_id like '%' and 1 = 1 ";
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            if (dt == null || dt.Rows.Count == 0)
            {
                Response.Write("<script>alert('" + Resources.Resource.alertWsj + "')</script>");
                return;
            }
            List<string> sqlList = new List<string>();
            InsertCommandBuilder ins = new InsertCommandBuilder("pre_prd_pk");
            string pkId = CommadMethod.getNextId("HPR", "0101"); ;
            ins.InsertColumn("prd_pk_id", pkId);
            ins.InsertColumn("prd_pk_no", "");
            ins.InsertColumn("prd_pk_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            ins.InsertColumn("store_id", ddlMaterialStock.SelectedItem.Value.Trim());
            ins.InsertColumn("verifier", "");
            ins.InsertColumn("operator_id", HttpContext.Current.Request.Cookies["cookie"].Values["id"]);
            ins.InsertColumn("remark", txtRemark.Text.Trim().ToUpper());
            //ins.InsertColumn("str_in_bill_id", getId());
            //ins.InsertColumn("str_out_bill_id", getId());
            ins.InsertColumn("islocal", "Y");
            ins.InsertColumn("prd_pkms", "正常按批次盘库");
            sqlList.Add(ins.getInsertCommand());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ins = new InsertCommandBuilder("pre_prd_pk_detail");
                ins.InsertColumn("prd_pk_id", pkId);
                ins.InsertColumn("stock_remain_id", dt.Rows[i]["prd_stock_id"]);
                ins.InsertColumn("materials_id", dt.Rows[i]["materials_id"]);
                ins.InsertColumn("pdsl", 0);
                ins.InsertColumn("zmsl", dt.Rows[i]["zmsl"]);
                ins.InsertColumn("prd_batch_id", dt.Rows[i]["prd_batch_id"]);
                ins.InsertColumn("zmsl1", dt.Rows[i]["zmsl1"]);
                ins.InsertColumn("pdsl1", 0);
                ins.InsertColumn("is_can_sale", dt.Rows[i]["is_can_sale"]);
                sqlList.Add(ins.getInsertCommand());
            }
            ins.ExcutTransaction(sqlList);
            gvData.DataSource = getData();
            gvData.DataBind();
        }
        protected void bindDDL()
        {
            string sql = "SELECT store_id, " + Resources.Resource.store + " FROM store";
            SelectCommandBuilder s = new SelectCommandBuilder();
            DataTable dt = s.ExecuteDataTable(sql);
            ddlMaterialStock.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddlMaterialStock.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i]["store_id"].ToString()));
            }
        }
        public DataTable getData()
        {
            string sql = "SELECT (SELECT s." + Resources.Resource.store + " FROM store s  WHERE a.store_id = s.store_id) AS '" + Resources.Resource.clck + "', prd_pk_id  as '" + Resources.Resource.pddh + "', prd_pk_date as '" + Resources.Resource.pdrq + "','" + Resources.Resource.czry + "'=(select operator_name from operator o where o.operator_id =a.operator_id),remark as '" + Resources.Resource.bz + "' FROM pre_prd_pk a ";
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            return dt;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvData.SelectedIndex == -1)
            {
                Response.Write("<script>alert('" + Resources.Resource.alterwxz + "')</script>");
                return;
            }
            Export(getData(ViewState["Sid"].ToString()));
        }

        private DataTable getData(string p)
        {
            string sql = @"SELECT prd_Stock.materials_id, materials.name as '" + Resources.Resource.bpmc + @"' , materials.spec as '" + Resources.Resource.gg + @"', 
                           prd_batch.hwh as '" + Resources.Resource.hwh + @"', prd_batch.pch as '" + Resources.Resource.pch + @"',materials.lb1_id as lb1,'' as '" + Resources.Resource.pdsl + @"', '' as 修正 FROM prd_Stock INNER JOIN
                           prd_batch ON prd_Stock.prd_batch_id = prd_batch.prd_batch_id INNER JOIN
                           materials ON prd_Stock.materials_id = materials.id
                           WHERE (prd_Stock.store_id = '" + p + "') order by materials.lb1_id , prd_batch.hwh,materials.name";
            return new SelectCommandBuilder().ExecuteDataTable(sql);
        }
        public void Export(DataTable dt)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            Sheet sheet = hssfworkbook.CreateSheet();
            Row HeaderRow = sheet.CreateRow(0);
            HeaderRow.CreateCell(0).SetCellValue("序号");
            for (int j = 2; j < dt.Columns.Count; j++)
            {
                HeaderRow.CreateCell(j - 1).SetCellValue(dt.Columns[j - 1].ToString());
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Row r = sheet.CreateRow(i + 1);
                r.CreateCell(0).SetCellValue(i + 1);
                for (int j = 2; j < dt.Rows[i].ItemArray.Length; j++)
                {
                    r.CreateCell(j - 1).SetCellValue(dt.Rows[i].ItemArray[j - 1].ToString());
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
                Response.AddHeader("Content-Disposition:", "attachment; filename= MaterialPKForm" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls");
                Response.BinaryWrite(stream.ToArray());
            }
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gvData_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["Sid"] = getSid(gvData.SelectedRow.Cells[1].Text);
        }
        protected string getSid(string txt)
        {
            string id = null;
            for (int i = 0; i < ddlMaterialStock.Items.Count; i++)
            {
                if (txt.Trim().Equals(ddlMaterialStock.Items[i].Text.Trim()))
                {
                    id = ddlMaterialStock.Items[i].Value;
                    break;
                }
            }
            return id;
        }

        protected void gvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sql = "delete from pre_prd_pk where prd_pk_id = '" + gvData.Rows[e.RowIndex].Cells[2].Text.Trim() + "'";
            string sqldetail = "delete from pre_prd_pk_detail where prd_pk_id = '" + gvData.Rows[e.RowIndex].Cells[2].Text.Trim() + "'";
            List<string> sList = new List<string>();
            sList.Add(sql);
            sList.Add(sqldetail);
            InsertCommandBuilder ins = new InsertCommandBuilder();
            int count = ins.ExecuteNonQuery(sql);
            count += ins.ExecuteNonQuery(sqldetail);
            if (count != 0)
            {
                gvData.DataSource = getData();
                gvData.DataBind();
            }
            else
            {
                Response.Write("<script>alert('" + Resources.Resource.alterfiald + "')</script>");
                return;
            }
        }
    }
}