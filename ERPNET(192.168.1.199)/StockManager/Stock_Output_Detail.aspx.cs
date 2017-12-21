using System;
using System.Collections.Generic;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Web.UI.HtmlControls;

namespace ERPPlugIn.StockManager
{
    public partial class Stock_Output_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string Pid = Request.QueryString["Pid"];
                string khdm = Request.QueryString["khdm"];
                lblPid.Text = Pid;
                lblkhdm.Text = getCustomerName(khdm);
                txtDate.Text = getDate(lblPid.Text, khdm).ToString("yyyy-MM-dd");
                lblprintDate.Text = "打印日期:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                gvExcel.DataSource = getStockData(lblPid.Text, khdm);
                gvExcel.DataBind();
            }
        }
        public List<StockDetail> getStockData(string Pid, string khdm)
        {
            List<StockDetail> sdList = new List<StockDetail>();
            SelectCommandBuilder s = new SelectCommandBuilder();
            string sql = @"SELECT goods.goods_name, goods.bzxx, goods.jzl, goods.classify_area, goods_up.qty, 
                              ISNULL(delivery_plan.plan_qty, 0) AS plan_qty, SUM(goods_up.qty) 
                              AS xsht_detailqty, CEILING(goods_up.qty / goods.jzl) AS jzlQty, 
                              ISNULL(stock_remain.sumQty, 0) AS sumQty, goods_up.khdm, 
                              goods_up.prepare_goods_id, ISNULL(stock_remain.sumQty2, 0) AS sumQty2,goods.分类区域
                            FROM goods_up LEFT OUTER JOIN
                                  goods ON goods.goods_name = goods_up.goods_name LEFT OUTER JOIN
                                  xsht_detail ON goods.goods_id = xsht_detail.goods_id LEFT OUTER JOIN
                                  xsht ON xsht_detail.xsht_id = xsht.xsht_id LEFT OUTER JOIN
                                      (SELECT delivery_plan.goods_id,
                                           SUM(delivery_plan.plan_qty - ISNULL(delivery_plan.delivery_qty, 0))
                                           AS plan_qty
                                     FROM delivery_plan
                                     WHERE is_end = 'N'
                                     GROUP BY delivery_plan.goods_id) delivery_plan ON 
                                  goods.goods_id = delivery_plan.goods_id LEFT OUTER JOIN
                                      (SELECT a.goods_id, a.sumQty, isnull(b.sumQty2, 0) sumQty2
                                     FROM (SELECT goods_id, SUM(qty) sumQty
                                             FROM stock_remain
                                             WHERE store_id = '03'
                                             GROUP BY goods_id) a LEFT JOIN
                                               (SELECT goods_id, SUM(qty) sumQty2
                                              FROM stock_remain
                                              WHERE store_id = '12'
                                              GROUP BY goods_id) b ON a.goods_id = b.goods_id) stock_remain ON 
                                  goods.goods_id = stock_remain.goods_id
                    WHERE (Goods_Up.Prepare_goods_Id ='" + Pid + @"') AND (Goods_Up.khdm ='" + khdm + @"') AND (xsht.is_end = 'M')
                    GROUP BY goods.goods_name, goods.bzxx, goods.jzl, goods.classify_area, 
                    goods_up.qty, ISNULL(delivery_plan.plan_qty, 0), CEILING(goods_up.qty / goods.jzl),
                    stock_remain.sumQty,stock_remain.sumQty2,goods_up.khdm, goods_up.prepare_goods_id,goods.分类区域 ORDER BY goods.分类区域 DESC, goods.goods_name";
            //s.SelectColumn("goods_name");
            //s.SelectColumn("qty");
            //s.ConditionsColumn("khdm", khdm);
            //s.ConditionsColumn("Prepare_goods_Id", Pid);
            //s.getSelectCommand();
            SqlDataReader dr = s.ExecuteReader(sql);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    StockDetail sd = new StockDetail();
                    sd.goodsName = dr.GetString(0);
                    sd.bzxx = dr.IsDBNull(1) ? "" : dr.GetString(1);
                    sd.jzl = Convert.ToInt32(dr.GetDecimal(2));
                    sd.area = dr.IsDBNull(3) ? "" : dr.GetString(3);
                    sd.Qty = dr.GetDecimal(4);
                    sd.plan_qty = int.Parse(dr.GetSqlDecimal(5).ToDouble().ToString());
                    sd.xsht_detailqty = Convert.ToInt32(dr.GetDecimal(6));
                    sd.jzlqty = dr.GetDecimal(7);
                    sd.stockSumQty = int.Parse(dr.GetSqlDecimal(8).ToDouble().ToString());
                    sd.isOk = getIsOK(Convert.ToInt32(dr.GetDecimal(4)), int.Parse(dr.GetSqlDecimal(5).ToDouble().ToString()), Convert.ToInt32(dr.GetDecimal(6)), int.Parse(dr.GetSqlDecimal(8).ToDouble().ToString()));
                    sd.stockQty = getStockData(Pid, khdm, dr.GetString(0), Convert.ToInt32(dr.GetDecimal(6)), Convert.ToInt32(dr.GetDecimal(4)));
                    sd.area = dr.GetString(12);
                    sd.NgQty = int.Parse(dr.GetSqlDecimal(11).ToDouble().ToString());
                    sdList.Add(sd);
                }

            }
            return sdList;
        }
        public string getIsOK(int a, int b, int c, int d)
        {
            string isOk = "";
            if (b >= a && c >= a && d >= a)
            {
                isOk = "√";
            }
            return isOk;
        }
        public string getStockData(string Pid, string khdm, string goodsname, int SumQty, int pqty)
        {
            string str = "";
            List<string> slist = new List<string>();
            SelectCommandBuilder s = new SelectCommandBuilder();
            string sql = @"SELECT d.hwh, SUM(u.qty) AS p_qty,sum(d.qty) as s_Qty, right(rtrim( d.pch),6) as pch
                            FROM goods_up u INNER JOIN
                                      (SELECT goods.goods_id, goods.goods_name, c.batch_id, c.hwh, c.qty, 
                                           c.str_in_date, c.pch
                                     FROM goods INNER JOIN
                                               (SELECT a.goods_id, a.qty, b.str_in_date, a.batch_id, b.hwh, b.pch
                                              FROM stock_remain a INNER JOIN
                                                    batch b ON a.batch_id = b.batch_id
                                              WHERE a.store_id = '03') c ON goods.goods_id = c.goods_id) d ON 
                                  u.goods_name = d.goods_name
                            WHERE (u.prepare_goods_id = '" + Pid + "') AND (u.khdm = '" + khdm + @"')
                            GROUP BY d.hwh, d.pch, u.goods_name,d.str_in_date
                            HAVING (u.goods_name = '" + goodsname + "') ORDER BY u.goods_name,right(rtrim( d.pch),6),d.str_in_date";
            SqlDataReader dr = s.ExecuteReader(sql);
            if (dr.HasRows)
            {
                int sum = 0;
                while (dr.Read())
                {
                    //str += dr.GetString(0) + "(" + dr.GetInt32(1) + "),";
                    sum += int.Parse(dr.GetSqlDecimal(2).ToDouble().ToString());
                    slist.Add(dr.GetString(0) + "[" + dr.GetString(3) + "]" + "(" + int.Parse(dr.GetSqlDecimal(2).ToDouble().ToString()) + ")" + "(" + "_____" + ")");
                    if (SumQty <= sum || pqty <= sum) break;
                }

            }
            if (slist.Count != 0)
            {
                for (int i = 0; i < slist.Count; i++)
                {
                    if (i != slist.Count - 1)
                    {
                        str += slist[i] + ",";
                    }
                    else
                    {
                        str += slist[i];
                    }
                }
            }
            return str;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Export(this.tb);
        }

        protected void gvExcel_PreRender(object sender, EventArgs e)
        {
            //Merge(gvExcel);
        }

        public void Merge(GridView PressCrackData)
        {
            int i, j, k, intSpan;//i开始行,j比较行,k列数,intSpan相同行数
            string strTemp;//保存开始行的值
            if (PressCrackData.Rows.Count > 0)
            {
                for (k = 0; k < 2; k++)
                {
                    for (i = 0; i <= PressCrackData.Rows.Count - 1; i++)
                    {
                        intSpan = 1;
                        string lable = "Label" + (k + 1);
                        strTemp = ((Label)PressCrackData.Rows[i].Cells[k].FindControl(lable)).Text;
                        for (j = i + 1; j <= PressCrackData.Rows.Count - 1; j++)
                        {
                            if (string.Compare(strTemp, ((Label)PressCrackData.Rows[j].Cells[k].FindControl(lable)).Text) == 0)
                            {
                                intSpan += 1;
                                PressCrackData.Rows[i].Cells[k].RowSpan = intSpan;
                                PressCrackData.Rows[j].Cells[k].Visible = false;
                            }
                            else
                            {
                                j = PressCrackData.Rows.Count - 1;
                            }

                        }
                        i += intSpan - 1;
                    }
                }
            }
        }

        public void Export(GridView gv, int style)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            Sheet sheet = hssfworkbook.CreateSheet();
            Row HeaderRow = sheet.CreateRow(0);
            for (int j = 0; j < gv.Columns.Count; j++)
            {
                HeaderRow.CreateCell(j).SetCellValue(gv.Columns[j].HeaderText);
            }
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                Row r = sheet.CreateRow(i + 1);
                for (int j = 0; j < gv.Rows[i].Cells.Count; j++)
                {
                    string lable = "Label" + (j + 1);
                    r.CreateCell(j).SetCellValue(((Label)gv.Rows[i].Cells[j].FindControl(lable)).Text);
                }

            }
            for (int i = 0; i < gv.Rows.Count + 1; i++)
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
                Response.AddHeader("Content-Disposition:", "attachment; filename=" + lblPid.Text + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls");
                Response.BinaryWrite(stream.ToArray());
            }
        }

        public string getCustomerName(string c_id)
        {
            string sql = "select Customer_name from customer where customer_id='" + c_id + "'";
            SelectCommandBuilder s = new SelectCommandBuilder();
            return s.ExecuteDataTable(sql).Rows[0][0].ToString();
        }

        void Export(System.Web.UI.Control ctl)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page page = new Page();
            HtmlForm form = new HtmlForm();
            this.gvExcel.EnableViewState = false;
            page.EnableEventValidation = false;
            page.DesignerInitialize();
            page.Controls.Add(form);
            form.Controls.Add(ctl);
            page.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + lblPid.Text + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.UTF8;
            Response.Write(sb.ToString());
            Response.End();
        }

        public DateTime getDate(string Pid, string khdm)
        {
            SelectCommandBuilder s = new SelectCommandBuilder("goods_up");
            s.SelectColumn("delivery_date");
            s.ConditionsColumn("khdm", khdm);
            s.ConditionsColumn("Prepare_goods_Id", Pid);
            s.getSelectCommand();
            return Convert.ToDateTime(s.ExecuteDataTable().Rows[0][0].ToString());
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

                PostBackOptions myPostBackOptions = new PostBackOptions(this);
                myPostBackOptions.AutoPostBack = false;
                myPostBackOptions.RequiresJavaScriptProtocol = true;
                myPostBackOptions.PerformValidation = false;
            }
            if (gvExcel.FooterRow != null)
            {
                gvExcel.FooterRow.Visible = true;
            }
            if (e.Row.RowType == DataControlRowType.Footer)          // 判断当前项是否为页脚  
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;  //因为示例中该GridView为7列  
                e.Row.Cells[0].Text = "合计：";
                e.Row.Cells[1].Text = getSum(gvExcel, "Label2").ToString();
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].Text = getSum(gvExcel, "Label10").ToString();
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[8].Text = getSum(gvExcel, "Label9").ToString();
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
            }

        }
        public int getSum(GridView gv, string LableId)
        {
            int sum = 0;
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                sum += Convert.ToInt32((gv.Rows[i].FindControl(LableId) as Label).Text);
            }
            return sum;
        }

    }
    public class StockDetail
    {
        public string goodsName { get; set; }
        public decimal Qty { get; set; }
        public string stockQty { get; set; }
        public string bzxx { get; set; }
        public int jzl { get; set; }
        public string area { get; set; }
        public int plan_qty { get; set; }
        public int xsht_detailqty { get; set; }
        public decimal jzlqty { get; set; }
        public long stockSumQty { get; set; }
        public string isOk { get; set; }
        public int NgQty { get; set; }
    }
}