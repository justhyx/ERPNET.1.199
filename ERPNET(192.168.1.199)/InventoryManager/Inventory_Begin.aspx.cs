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

namespace ERPPlugIn.InventoryManager
{
    public partial class Inventory_Begin : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindDDL();
                gvData.DataSource = getData();
                gvData.DataBind();
                txtRemark.Text = HttpContext.Current.Request.Cookies["cookie"].Values["l"];
            }
        }

        protected void btnBegin_Click(object sender, EventArgs e)
        {
            //string sql = "INSERT INTO pre_pk ( pk_id, pk_no, pk_date, store_id, verifier, operator_id, remark, str_in_bill_id, str_out_bill_id, islocal, pkms ) VALUES ( 'PK000009420101', '', '2012-7-17 8:49:33.056', '03', '', '0000', '', 'RK001406270101', 'CK000548050101', 'Y', '正常按批次盘库' )";
            string ss = "select count(*) from pre_pk where store_id='" + ddlMaterialStock.SelectedItem.Value.Trim() + "' and CONVERT(varchar(100), pk_Date, 112) = '" + DateTime.Now.ToString("yyyyMMdd") + "'";
            int count = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar(ss));
            if (count != 0)
            {
                Response.Write("<script>alert('" + Resources.Resource.alertWxqd + "')</script>");
                gvData.DataSource = getData();
                gvData.DataBind();
                return;
            }
            string sql = @" SELECT goods.dm,
			goods.goods_name,    
			goods.spec,    
			sccj.sccj_name as sccjname,    
			goods.goods_unit,  
			batch.pch as pch,  
			price =  isnull(batch.price,0.0),    
			stock_remain.qty as zmsl,    
			stock_remain.qty as pdsl,
			batch.yxq,
			' ' as stock_type_id,  
			stock_remain.is_can_sale, 
			batch.str_in_date,
			stock_remain.batch_id ,
			stock_remain.stock_remain_id,
			stock_remain.goods_id,
			'ddd' as pk_id,
			'N' as disobey,
			stock_remain.qty as zmsl1, 
			stock_remain.qty as pdsl1,
			price.price as saleprice,
			batch.mjpch,
			c.location as location,
			isnull(( select counter_name from counter where counter.counter_id = c.counter_id), '未设置') as counter ,
			d.style,
			d.dictate_name,
			customer_name = (select bb.customer_name 
									from xsht aa,customer bb
									where aa.customer_id = bb.customer_id
										and aa.xsht_id = d.order_id ),
			batch.hwh  as 'hwh',	
			goods.cz,
			goods.ys
    FROM goods,    
			stock_remain,
			batch ,
			price  ,
			sccj,
			goods_counter c ,
			prd_dictate d 
   WHERE ( stock_remain.goods_id = goods.goods_id ) and   
			stock_Remain.batch_id = batch.batch_id and 
			batch.dictate_id *= d.dictate_id and 
			goods.sccj_id *= sccj.sccj_id and 
			stock_remain.stockstatus = 'N' and 
			goods.price_id *= price.price_id and 
 			stock_remain.store_id = '" + ddlMaterialStock.SelectedItem.Value.Trim() + @"'  and 
 			stock_remain.goods_id like '%' and
			goods.goods_id *= c.goods_id and
			stock_remain.store_id *= c.store_id and 1 = 1";
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            if (dt == null || dt.Rows.Count == 0)
            {
                Response.Write("<script>alert('" + Resources.Resource.alertWsj + "')</script>");
                return;
            }
            List<string> sqlList = new List<string>();
            InsertCommandBuilder ins = new InsertCommandBuilder("pre_pk");
            string pkId = CommadMethod.getNextId("HPD", "0101");
            ins.InsertColumn("pk_id", pkId);
            ins.InsertColumn("pk_no", "");
            ins.InsertColumn("pk_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            ins.InsertColumn("store_id", ddlMaterialStock.SelectedItem.Value.Trim());
            ins.InsertColumn("verifier", "");
            ins.InsertColumn("operator_id", HttpContext.Current.Request.Cookies["cookie"].Values["id"]);
            ins.InsertColumn("remark", txtRemark.Text.Trim().ToUpper());
            //ins.InsertColumn("str_in_bill_id", getId());
            //ins.InsertColumn("str_out_bill_id", getId());
            ins.InsertColumn("islocal", "Y");
            ins.InsertColumn("pkms", "正常按批次盘库");
            sqlList.Add(ins.getInsertCommand());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ins = new InsertCommandBuilder("pre_pk_Detail");
                ins.InsertColumn("pk_id", pkId);
                ins.InsertColumn("stock_remain_id", dt.Rows[i]["stock_remain_id"]);
                ins.InsertColumn("goods_id", dt.Rows[i]["goods_id"]);
                ins.InsertColumn("pdsl", 0);
                ins.InsertColumn("zmsl", dt.Rows[i]["zmsl"]);
                ins.InsertColumn("batch_id", dt.Rows[i]["batch_id"]);
                ins.InsertColumn("zmsl1", dt.Rows[i]["zmsl1"]);
                ins.InsertColumn("pdsl1", dt.Rows[i]["pdsl1"]);
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
            string sql = "SELECT (SELECT s." + Resources.Resource.store + " FROM store s  WHERE a.store_id = s.store_id) AS '" + Resources.Resource.clck + "', pk_id  as '" + Resources.Resource.pddh + "', pk_date as '" + Resources.Resource.pdrq + "','" + Resources.Resource.czry + "'=(select operator_name from operator o where o.operator_id =a.operator_id),remark as '" + Resources.Resource.bz + "' FROM pre_pk a ";
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            return dt;
        }
        protected void Unnamed3_Click(object sender, EventArgs e)
        {
            if (gvData.SelectedIndex == -1)
            {
                Response.Write("<script>alert('" + Resources.Resource.alterwxz + "')</script>");
                return;
            }
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "<script>window.open('Inventory_PrintFrom.aspx?s_id=" + ViewState["Sid"] + "')</script>", false);

            //Response.Redirect("Inventory_PrintFrom.aspx?s_id=" + ViewState["Sid"]);
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
                Response.AddHeader("Content-Disposition:", "attachment; filename= MaterialForecast_Purchasing" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls");
                Response.BinaryWrite(stream.ToArray());
            }
        }
        protected DataTable getData(string s_id)
        {
            string sql = @"SELECT stock_remain.goods_id, goods.goods_name as '" + Resources.Resource.bpmc + @"' , goods.spec as '" + Resources.Resource.gg + @"', 
                           goods.ys as '" + Resources.Resource.ys + @"', batch.hwh as '" + Resources.Resource.hwh + @"', batch.pch as '" + Resources.Resource.pch + @"','' as '" + Resources.Resource.pdsl + @"', '' as 修正  FROM stock_remain LEFT OUTER JOIN
                           goods ON goods.goods_id = stock_remain.goods_id INNER JOIN
                           batch ON stock_remain.batch_id = batch.batch_id
                           WHERE (stock_remain.store_id = '" + s_id + "') order by batch.hwh,goods.goods_name";
            return new SelectCommandBuilder().ExecuteDataTable(sql);
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
            string sql = "delete from pre_pk where pk_id = '" + gvData.Rows[e.RowIndex].Cells[2].Text.Trim() + "'";
            string sqldetail = "delete from pre_pk_detail where pk_id = '" + gvData.Rows[e.RowIndex].Cells[2].Text.Trim() + "'";
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