using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERPPlugIn.InventoryManager;
using System.Data;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using ERPPlugIn.Class;

namespace ERPPlugIn.MaterailsManager
{
    public partial class MaterailsStock_KeyIn : PageBase
    {
        protected List<AddData> lList
        {
            set { ViewState["lList"] = value; }
            get { return ViewState["lList"] as List<AddData>; }
        }
        protected List<AddData> aList
        {
            set { ViewState["aList"] = value; }
            get { return ViewState["aList"] as List<AddData>; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                tr.Visible = false;
                trgoods.Visible = false;
                tredit.Visible = false;
                lList = new List<AddData>();
                aList = new List<AddData>();
                bindDDL();
                gvAddData.Columns[12].Visible = false;
            }
        }

        protected void ddlMaterialStock_SelectedIndexChanged(object sender, EventArgs e)
        {

            string sql = "SELECT (SELECT s." + Resources.Resource.store + " FROM store s  WHERE a.store_id = s.store_id) AS '" + Resources.Resource.clck + "', prd_pk_id '" + Resources.Resource.pddh + "', prd_pk_date '" + Resources.Resource.pdrq + "','" + Resources.Resource.czry + "'=(select operator_name from operator o where o.operator_id =a.operator_id),remark as '" + Resources.Resource.bz + "' FROM pre_prd_pk a where a.store_id = '" + ddlMaterialStock.SelectedItem.Value + "' ";
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            tr.Visible = true;
            gvData.DataSource = dt;
            gvData.DataBind();

        }
        protected void bindDDL()
        {

            string sql = "SELECT store_id, " + Resources.Resource.store + " FROM store";
            SelectCommandBuilder s = new SelectCommandBuilder();
            DataTable dt = s.ExecuteDataTable(sql);
            ddlMaterialStock.Items.Clear();
            ddlMaterialStock.Items.Add(new ListItem(Resources.Resource.qxz, "0"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddlMaterialStock.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i]["store_id"].ToString()));
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
                String evt = Page.ClientScript.GetPostBackClientHyperlink(sender as GridView, "Select$" + e.Row.RowIndex.ToString());
                e.Row.Attributes.Add("onclick", evt);
            }
        }

        protected void gvData_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Response.Write("<script>alert('" + gvData.SelectedRow.Cells[1].Text.Trim() + "')</script>");
            txtMaterialStock.Text = gvData.SelectedRow.Cells[0].Text.Trim();
            txtPK_Id.Text = gvData.SelectedRow.Cells[1].Text.Trim();
            txtPK_Date.Text = Convert.ToDateTime(gvData.SelectedRow.Cells[2].Text.Trim()).ToString("yyyy/MM/dd");
            txtRemark.Text = gvData.SelectedRow.Cells[4].Text.Trim() == "&nbsp;" ? "" : gvData.SelectedRow.Cells[4].Text.Trim();
            gvData.DataSource = null;
            gvData.DataBind();
            tr.Visible = false;
            txtGoodsName.Enabled = true;
            ddlMaterialStock.Enabled = false;
            txtAdd.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "focus", "<script>document.all.txtGoodsName.focus()</script>");
        }
        protected DataTable getGoodsData(string goodsname, string hwh)
        {
            string sql = @"SELECT materials.name, materials.name, materials.spec, materials.texture, materials.color, 
                                  vendor.vendor_name, materials.unit, prd_batch.pch AS pch, prd_batch.hwh AS hwh, 
                                  prd_Stock.qty AS Qty, materials.new_price, materials.crt_date, 0 AS total,pre_prd_pk_detail.stock_remain_id, pre_prd_pk_detail.prd_batch_id
                            FROM materials INNER JOIN
                              prd_batch ON materials.id = prd_batch.materials_id INNER JOIN
                              prd_Stock ON prd_batch.prd_batch_id = prd_Stock.prd_batch_id INNER JOIN
                              pre_prd_pk_detail ON 
                              prd_batch.materials_id = pre_prd_pk_detail.materials_id LEFT OUTER JOIN
                              vendor ON materials.sccj_id = vendor.vendor_id 
                           WHERE (materials.name = '" + goodsname + "') And (prd_Stock.store_id = '" + ddlMaterialStock.SelectedItem.Value.ToString() + "') ";
            if (hwh != "")
            {
                sql += "And (prd_batch.hwh = '" + hwh + "')";
            }
            return new SelectCommandBuilder().ExecuteDataTable(sql);
        }

        protected void gvGoodsData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvEditData.Rows.Count != 0)
            {
                AddData dataa = new AddData()
                {
                    id = "",
                    pm = this.gvGoodsData.SelectedRow.Cells[0].Text.Trim(),
                    spec = this.gvGoodsData.SelectedRow.Cells[2].Text.Trim(),
                    cz = this.gvGoodsData.SelectedRow.Cells[3].Text.Trim(),
                    ys = this.gvGoodsData.SelectedRow.Cells[4].Text.Trim(),
                    goods_unit = this.gvGoodsData.SelectedRow.Cells[5].Text.Trim(),
                    pch = this.gvGoodsData.SelectedRow.Cells[6].Text == "&nbsp;" ? "" : this.gvGoodsData.SelectedRow.Cells[6].Text.Trim(),
                    hwh = this.gvGoodsData.SelectedRow.Cells[7].Text == "&nbsp;" ? "" : this.gvGoodsData.SelectedRow.Cells[7].Text.Trim(),
                    pdsl = this.gvGoodsData.SelectedRow.Cells[8].Text == "&nbsp;" ? "0" : Convert.ToDecimal(this.gvGoodsData.SelectedRow.Cells[8].Text).ToString("0.##"),
                    Price = this.gvGoodsData.SelectedRow.Cells[9].Text == "&nbsp;" ? "0" : Convert.ToDecimal(this.gvGoodsData.SelectedRow.Cells[9].Text).ToString("0.##"),
                    total = "0",
                    customer_name = this.gvGoodsData.SelectedRow.Cells[10].Text.Trim(),
                };
                List<string> sList = getBatch_id(dataa.pm, dataa.pch);
                InsertCommandBuilder ins = new InsertCommandBuilder("pd_detail");
                string Bill_id = ViewState["billId"].ToString();
                ins.InsertColumn("Bill_id", Bill_id);
                ins.InsertColumn("prd_Batch_id", sList[1].Trim());
                ins.InsertColumn("materials_id", sList[0].Trim());
                ins.InsertColumn("Qty", dataa.pdsl);
                ins.InsertColumn("Pch", dataa.pch);
                string detail_id = new SelectCommandBuilder().ExecuteDataTable("SELECT MAX(Detail_id) AS maxId FROM pd_detail where bill_id = '" + Bill_id + "'").Rows[0][0].ToString();
                int i = Convert.ToInt32(detail_id.Substring(detail_id.Length - 4, 4));
                ins.InsertColumn("Detail_id", Bill_id + (i + 1).ToString().PadLeft(4, '0'));
                ins.InsertColumn("Price", dataa.Price);
                ins.InsertColumn("is_can_sale", "Y");
                ins.InsertColumn("hwh", dataa.hwh);
                ins.InsertColumn("is_new", "Y");
                ins.getInsertCommand();
                ins.ExecuteNonQuery();
                //if (aList.Count==0)
                //{
                //    btnSelect_Click(sender, e);
                //}                
                trgoods.Visible = false;
                AddData data1 = new AddData()
                {
                    id = Bill_id + (i + 1).ToString().PadLeft(4, '0'),
                    pm = this.gvGoodsData.SelectedRow.Cells[0].Text.Trim(),
                    spec = this.gvGoodsData.SelectedRow.Cells[2].Text.Trim(),
                    cz = this.gvGoodsData.SelectedRow.Cells[3].Text.Trim(),
                    ys = this.gvGoodsData.SelectedRow.Cells[4].Text.Trim(),
                    goods_unit = this.gvGoodsData.SelectedRow.Cells[5].Text.Trim(),
                    pch = this.gvGoodsData.SelectedRow.Cells[6].Text == "&nbsp;" ? "" : this.gvGoodsData.SelectedRow.Cells[6].Text.Trim(),
                    hwh = this.gvGoodsData.SelectedRow.Cells[7].Text == "&nbsp;" ? "" : this.gvGoodsData.SelectedRow.Cells[7].Text.Trim(),
                    pdsl = this.gvGoodsData.SelectedRow.Cells[8].Text == "&nbsp;" ? "0" : Convert.ToDecimal(this.gvGoodsData.SelectedRow.Cells[8].Text).ToString("0.##"),
                    Price = this.gvGoodsData.SelectedRow.Cells[9].Text == "&nbsp;" ? "0" : Convert.ToDecimal(this.gvGoodsData.SelectedRow.Cells[9].Text).ToString("0.##"),
                    total = "0",
                    customer_name = this.gvGoodsData.SelectedRow.Cells[10].Text.Trim(),
                };
                aList.Insert(0, data1);
                gvAddData.DataSource = aList;
                gvAddData.DataBind();
                return;
            }
            AddData data = new AddData()
            {
                id = "",
                pm = this.gvGoodsData.SelectedRow.Cells[0].Text.Trim(),
                spec = this.gvGoodsData.SelectedRow.Cells[2].Text.Trim(),
                cz = this.gvGoodsData.SelectedRow.Cells[3].Text.Trim(),
                ys = this.gvGoodsData.SelectedRow.Cells[4].Text.Trim(),
                goods_unit = this.gvGoodsData.SelectedRow.Cells[5].Text.Trim(),
                pch = this.gvGoodsData.SelectedRow.Cells[6].Text == "&nbsp;" ? "" : this.gvGoodsData.SelectedRow.Cells[6].Text.Trim(),
                hwh = this.gvGoodsData.SelectedRow.Cells[7].Text == "&nbsp;" ? "" : this.gvGoodsData.SelectedRow.Cells[7].Text.Trim(),
                pdsl = this.gvGoodsData.SelectedRow.Cells[8].Text == "&nbsp;" ? "0" : Convert.ToDecimal(this.gvGoodsData.SelectedRow.Cells[8].Text).ToString("0.##"),
                Price = this.gvGoodsData.SelectedRow.Cells[9].Text == "&nbsp;" ? "0" : Convert.ToDecimal(this.gvGoodsData.SelectedRow.Cells[9].Text).ToString("0.##"),
                total = "0",
                customer_name = this.gvGoodsData.SelectedRow.Cells[10].Text.Trim(),
                s_id = this.gvGoodsData.SelectedRow.Cells[11].Text.Trim(),
                b_id = this.gvGoodsData.SelectedRow.Cells[12].Text.Trim()
            };
            lList.Insert(0, data);
            gvAddData.DataSource = lList;
            gvAddData.DataBind();
            (gvAddData.Rows[0].Cells[7].FindControl("txtpdsl") as TextBox).Attributes.Add("onfocus", "this.select();");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "focus", "<script>document.all.gvAddData_ctl02_txtpdsl.focus()</script>");
            gvGoodsData.DataSource = null;
            gvGoodsData.DataBind();
            gvAddData.Columns[11].Visible = true;
            gvAddData.Columns[12].Visible = false;
            trgoods.Visible = false;
        }

        protected void gvGoodsData_RowDataBound(object sender, GridViewRowEventArgs e)
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
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            string[] s = tb.ClientID.Split('_');
            string[] ss = s[1].Split('l');
            int row = Convert.ToInt32(ss[1]) - 2;
            decimal pdsl = Convert.ToDecimal((gvAddData.Rows[row].Cells[7].FindControl("txtpdsl") as TextBox).Text);
            decimal price = Convert.ToDecimal((gvAddData.Rows[row].Cells[8].FindControl("txtprice") as TextBox).Text);
            decimal total = pdsl * price;
            if (gvAddData.Columns[11].Visible != false)
            {
                lList[row].pdsl = pdsl.ToString("0.##");
                lList[row].Price = price.ToString("0.##");
                lList[row].total = total.ToString("0.##");
                gvAddData.DataSource = lList;
                gvAddData.DataBind();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "focus", "<script>document.all.txtGoodsName.focus()</script>");
            }
            else
            {
                aList[row].pdsl = pdsl.ToString("0.##");
                aList[row].Price = price.ToString("0.##");
                aList[row].total = total.ToString("0.##");
                gvAddData.DataSource = aList;
                gvAddData.DataBind();
            }

            //(gvAddData.Rows[row].Cells[9].FindControl("Price") as TextBox).Text = total.ToString("0.##");
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            DataTable dt = getGoodsData(txtGoodsName.Text.Trim().ToUpper(), txthwh.Text.Trim().ToUpper());
            if (dt != null && dt.Rows.Count != 0)
            {
                trgoods.Visible = true;
                gvGoodsData.DataSource = dt;
                txtGoodsName.Text = string.Empty;
                txthwh.Text = string.Empty;
                gvGoodsData.DataBind();
            }
            else
            {
                trgoods.Visible = false;
                gvGoodsData.DataSource = null;
                txtGoodsName.Text = string.Empty;
                txthwh.Text = string.Empty;
                gvGoodsData.DataBind();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "focus", "<script>document.all.txtGoodsName.select()</script>");
            }

        }
        protected List<string> getBatch_id(string goodsName, string pch)
        {
            List<string> sList = new List<string>();
            string sql = "select id from materials where name  = '" + goodsName + "'";
            string id = new SelectCommandBuilder().ExecuteDataTable(sql).Rows[0][0].ToString();
            sql = "select prd_batch_id from prd_batch where materials_id='" + id + "' and pch ='" + pch + "'";
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            string batchId = "盘盈入库";
            if (dt != null && dt.Rows.Count != 0)
            {
                batchId = dt.Rows[0][0].ToString();
            }
            sList.Add(id);
            sList.Add(batchId);
            return sList;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (gvAddData.Rows.Count == 0)
            {
                return;
            }
            List<string> sqlList = new List<string>();
            InsertCommandBuilder insert = new InsertCommandBuilder();
            if (gvAddData.Columns[11].Visible == false)
            {
                for (int i = 0; i < aList.Count; i++)
                {
                    UpdateCommandBuilder up = new UpdateCommandBuilder("pd_detail");
                    up.UpdateColumn("Qty", aList[i].pdsl);
                    up.UpdateColumn("pch", aList[i].pch);
                    up.UpdateColumn("hwh", aList[i].hwh);
                    up.ConditionsColumn("Detail_id", aList[i].id);
                    sqlList.Add(up.getUpdateCommand());
                    //UpdateCommandBuilder up1 = new UpdateCommandBuilder("pre_prd_pk_detail");
                    //up1.UpdateColumn("pdsl", aList[i].pdsl);
                    //up1.ConditionsColumn("Detail_id", aList[i].id);                    
                    //sqlList.Add(up1.getUpdateCommand());
                }

            }
            else
            {
                InsertCommandBuilder ins = new InsertCommandBuilder("pd_detail");
                string Bill_id = "PD" + DateTime.Now.ToString("ddHHmmssfff");
                for (int i = 0; i < lList.Count; i++)
                {
                    List<string> sList = getBatch_id(lList[i].pm, lList[i].pch);
                    string detail_id = Bill_id + (i + 1).ToString().PadLeft(4, '0');
                    //string sql = "INSERT INTO tmp_pk_detail ( Bill_id, Batch_id, Goods_id, Qty, Pch, Detail_id, Price, is_can_sale, hwh ) VALUES ( '" + Bill_id + "', '盘盈入库', '3034', 500.000000, '无库存', 'PB2012072109381211401010938257800002', 1.225000, 'Y', '' )";
                    ins.CommandClear();
                    ins.InsertColumn("Bill_id", Bill_id);
                    ins.InsertColumn("prd_Batch_id", sList[1].Trim());
                    ins.InsertColumn("materials_id", sList[0].Trim());
                    ins.InsertColumn("Qty", lList[i].pdsl);
                    ins.InsertColumn("Pch", lList[i].pch);
                    ins.InsertColumn("Detail_id", detail_id);
                    ins.InsertColumn("Price", lList[i].Price);
                    ins.InsertColumn("is_can_sale", "Y");
                    ins.InsertColumn("hwh", lList[i].hwh);
                    ins.InsertColumn("is_new", "Y");
                    sqlList.Add(ins.getInsertCommand());
                    int countPD = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar("select count(*) from pre_prd_pk_detail WHERE (prd_pk_id = '" + txtPK_Id.Text.Trim().ToUpper() + "') AND (prd_batch_id = '" + lList[i].b_id + "') AND (stock_remain_id = '" + lList[i].s_id + "') "));
                    if (countPD != 0)
                    {
                        string updateSQL = "update pre_prd_pk_detail set pdsl =" + lList[i].pdsl + ",detail_id = '" + detail_id + "' WHERE (prd_pk_id = '" + txtPK_Id.Text.Trim().ToUpper() + "') AND (prd_batch_id = '" + lList[i].b_id + "') AND (stock_remain_id = '" + lList[i].s_id + "') ";
                        sqlList.Add(updateSQL);
                    }
                }
                ins.CommandClear();
                ins = new InsertCommandBuilder("pd");
                ins.InsertColumn("Bill_Id", Bill_id);
                ins.InsertColumn("Bill_no", txtPK_No.Text.Trim());
                ins.InsertColumn("Pk_id", txtPK_Id.Text.Trim().ToUpper());
                ins.InsertColumn("store_id", ddlMaterialStock.SelectedItem.Value.Trim());
                ins.InsertColumn("Crt_emp", HttpContext.Current.Request.Cookies["cookie"].Values["id"]);
                ins.InsertColumn("Crt_Date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                ins.InsertColumn("Status", "N");
                ins.InsertColumn("Remark", txtRemark.Text.Trim().ToUpper());
                sqlList.Add(ins.getInsertCommand());
            }
            int count = insert.ExcutTransaction(sqlList);
            if (count != 0)
            {
                Response.Write("<script>alert('" + Resources.Resource.alterOk + "')</script>");
                lList.Clear();
                aList.Clear();
                gvAddData.DataSource = lList;
                gvAddData.DataBind();
                ClearTextBox();
                ddlMaterialStock.Enabled = true;
                ddlMaterialStock.SelectedIndex = 0;
                gvDetailData.DataSource = null;
                gvDetailData.DataBind();
            }
        }

        protected void ClearTextBox()
        {
            foreach (Control cl in this.Page.FindControl("Form1").Controls)
            {
                if (cl.GetType().ToString() == "System.Web.UI.WebControls.TextBox")
                {
                    ((TextBox)cl).Text = "";
                }
            }
        }

        protected void gvAddData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //----------鼠标移动到每项时颜色交替效果  
                e.Row.Attributes["onmouseover"] = "e=this.style.backgroundColor;this.style.backgroundColor='#FFFF99'";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor=e";
                //----------设置悬浮鼠标指针形状为小手"  
                e.Row.Attributes["style"] = "Cursor:hand";
                //e.Row.Attributes.Add("ondblclick", "return confirm('是否确认删除?');");
            }
            if (gvAddData.FooterRow != null)
            {
                gvAddData.FooterRow.Visible = true;
            }
            if (e.Row.RowType == DataControlRowType.Footer)// 判断当前项是否为页脚  
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[0].Text = Resources.Resource.hj + "：";
                e.Row.Cells[8].Text = getTextBoxSum(gvAddData, "txtpdsl").ToString("0.##");
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[10].Text = getLabelSum(gvAddData, "Label10").ToString("0.##");
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Center;
            }
        }
        public decimal getTextBoxSum(GridView gv, string LableId)
        {
            decimal sum = 0;
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                sum += Convert.ToDecimal((gv.Rows[i].FindControl(LableId) as TextBox).Text);
            }
            return sum;
        }
        public decimal getLabelSum(GridView gv, string LableId)
        {
            decimal sum = 0;
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                sum += Convert.ToDecimal((gv.Rows[i].FindControl(LableId) as Label).Text);
            }
            return sum;
        }

        protected void gvAddData_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvAddData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lList.RemoveAt(e.RowIndex);
            gvAddData.DataSource = lList;
            gvAddData.DataBind();
        }

        protected void btninput_Click(object sender, EventArgs e)
        {

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtPK_Id.Text != "")
            {

                List<AddData> aList = new List<AddData>();
                DataTable dt = getEditData(txtPK_Id.Text.Trim());
                if (dt != null && dt.Rows.Count != 0)
                {
                    gvEditData.DataSource = dt;
                    gvEditData.DataBind();
                    gvAddData.DataSource = null;
                    gvAddData.DataBind();
                    gvDetailData.DataSource = null;
                    gvDetailData.DataBind();
                    tredit.Visible = true;
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    AddData a = new AddData()
                    //    {
                    //        pm = dt.Rows[i][1].ToString(),
                    //        spec = dt.Rows[i][2].ToString(),
                    //        cz = dt.Rows[i][3].ToString(),
                    //        ys = dt.Rows[i][4].ToString(),
                    //        goods_unit = dt.Rows[i][5].ToString(),
                    //        pch = dt.Rows[i][6].ToString(),
                    //        hwh = dt.Rows[i][7].ToString(),
                    //        pdsl = dt.Rows[i][12].ToString(),
                    //        Price = dt.Rows[i][9].ToString(),
                    //        total = (Convert.ToDecimal(dt.Rows[i][12].ToString()) * Convert.ToDecimal(dt.Rows[i][9].ToString())).ToString("0.##"),
                    //        customer_name = dt.Rows[i][13].ToString()
                    //    };
                    //    aList.Add(a);
                    //}

                }
                //gvAddData.DataSource = aList;
                //gvAddData.DataBind();
            }
        }

        private DataTable getEditData(string p)
        {
            string sql = @"SELECT DISTINCT a.Bill_Id, a.Bill_no, a.Pk_id, ISNULL
                                      ((SELECT " + Resources.Resource.store + @"
                                      FROM store
                                      WHERE store_id = a.store_id), a.store_id) AS store_id, 
                                  pre_prd_pk.prd_pk_date AS pk_date, ISNULL
                                      ((SELECT operator_name
                                      FROM operator
                                      WHERE operator_id = a.Crt_emp), a.Crt_emp) AS Crt_emp, a.Crt_Date, a.Remark, 
                                 Status= (case when a.status ='Y' then 'true' else 'false' end) 
                            FROM pd a INNER JOIN
                                  pre_prd_pk ON a.Pk_id = pre_prd_pk.prd_pk_id INNER JOIN
                                  pd_detail b ON a.Bill_Id = b.Bill_id where a.PK_Id = '" + p + "' and a.status ='N'";
            return new SelectCommandBuilder().ExecuteDataTable(sql);
        }

        protected void gvEditData_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = @"  SELECT a.Bill_id,   
                             a.prd_Batch_id,   
                             a.materials_id,   
                             a.Qty,   
                             a.Pch,   
                             a.Detail_id,   
                             a.yxq,   
                             a.PDate,   
                             a.Price,   
                             a.is_can_sale,   
                             materials.name,   
                             materials.spec,   
                             vendor.vendor_name,   
                             materials.unit,   
                             materials.name,
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
			                    a.bill_id ='" + gvEditData.SelectedRow.Cells[0].Text.Trim() + "'";
            ViewState["billId"] = gvEditData.SelectedRow.Cells[0].Text.Trim();
            gvDetailData.DataSource = new SelectCommandBuilder().ExecuteDataTable(sql);
            gvDetailData.DataBind();
        }
        protected void gvEditData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Attributes["onmouseover"] = "e=this.style.backgroundColor;this.style.backgroundColor='#FFFF99'";
                //e.Row.Attributes["onmouseout"] = "this.style.backgroundColor=e";
                //----------设置悬浮鼠标指针形状为小手"  
                e.Row.Attributes["style"] = "Cursor:hand";
                String evt = Page.ClientScript.GetPostBackClientHyperlink(sender as GridView, "Select$" + e.Row.RowIndex.ToString()); e.Row.Attributes.Add("onclick", evt);
            }
        }

        protected void gvDetailData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)          // 判断当前项是否为页脚  
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[0].Text = Resources.Resource.hj + "：";
                e.Row.Cells[8].Text = getLabelSum(gvDetailData, 8).ToString("0.##");
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[10].Text = getLabelSum(gvDetailData, 10).ToString("0.##");
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Left;
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
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            if (gvEditData.SelectedIndex != -1)
            {
                string sql = @"SELECT a.Bill_id,   
                             a.prd_Batch_id,   
                             a.materials_id,   
                             a.Qty,   
                             a.Pch,   
                             a.Detail_id,   
                             a.yxq,   
                             a.PDate,   
                             a.Price,   
                             a.is_can_sale,   
                             materials.name,   
                             materials.spec,   
                             vendor.vendor_name,   
                             materials.unit,   
                             materials.name,
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
			                    a.bill_id ='" + ViewState["billId"].ToString().Trim() + "'";
                DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
                aList.Clear();
                if (dt != null && dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AddData a = new AddData()
                        {
                            id = dt.Rows[i]["Detail_id"].ToString(),
                            pm = dt.Rows[i]["name"].ToString(),
                            spec = dt.Rows[i]["spec"].ToString(),
                            cz = dt.Rows[i]["texture"].ToString(),
                            ys = dt.Rows[i]["color"].ToString(),
                            goods_unit = dt.Rows[i]["unit"].ToString(),
                            //Detail_id = dt.Rows[i]["Detail_id"].ToString(),
                            pch = dt.Rows[i]["pch"].ToString(),
                            hwh = dt.Rows[i]["hwh"].ToString(),
                            pdsl = Convert.ToDecimal(dt.Rows[i]["qty"]).ToString("0.##"),
                            Price = dt.Rows[i]["price"].ToString() == "" ? "0.00" : Convert.ToDecimal(dt.Rows[i]["price"]).ToString("0.##"),
                            total = dt.Rows[i]["price"].ToString() == "" ? "0.00" : (Convert.ToDecimal(dt.Rows[i]["qty"]) * Convert.ToDecimal(dt.Rows[i]["price"])).ToString("0.##"),
                            customer_name = dt.Rows[i]["vendor_name"].ToString()
                        };
                        aList.Add(a);
                    }
                }
                gvAddData.DataSource = aList;
                gvAddData.DataBind();
                txtPK_No.Text = gvEditData.SelectedRow.Cells[4].Text.Trim() == "&nbsp;" ? "" : gvEditData.SelectedRow.Cells[4].Text.Trim();
                gvAddData.Columns[11].Visible = false;
                gvAddData.Columns[12].Visible = true;
                tredit.Visible = false;
                gvEditData.DataSource = null;
                gvEditData.DataBind();
            }
        }

        protected void btnCannel_Click(object sender, EventArgs e)
        {
            gvEditData.DataSource = null;
            gvEditData.DataBind();
            aList.Clear();
            gvAddData.DataSource = aList;
            gvAddData.DataBind();
            tredit.Visible = false;
        }

        protected void txtAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("MaterailsStock_KeyInNoData.aspx?bill_id=" + txtPK_Id.Text + "&bill_no=" + txtPK_No.Text + "&status=" + ddlMaterialStock.SelectedItem.Value.Trim() + "");
        }

        protected void gvAddData_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "focus", "<script>alert('" + (gvAddData.Rows[e.RowIndex].FindControl("HiddenField1") as HiddenField).Value + "')</script>");
            string D_id = (gvAddData.Rows[e.RowIndex].FindControl("hfDetailId") as HiddenField).Value;
            string c = "select Bill_id from pd_detail where Detail_id ='" + D_id + "'";
            string bill_id = new SelectCommandBuilder().ExecuteDataTable(c).Rows[0][0].ToString();
            string sql1 = "delete from pd_detail where Detail_id ='" + D_id + "'";
            int count = new DeleteCommandBuilder().ExecuteNonQuery(sql1);
            string dsql = "SELECT COUNT(*) AS COUNTa FROM pd_detail where Bill_id = '" + bill_id + "'";
            int p = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar(dsql));
            if (p == 0)
            {
                string sqlc = "delete from pd where Bill_id = '" + bill_id + "'";
                count += new DeleteCommandBuilder().ExecuteNonQuery(sqlc);
            }
            if (count != 0)
            {
                string sql = @"SELECT a.Bill_id,
                             a.prd_Batch_id,
                             a.materials_id,
                             a.Qty,
                             a.Pch,
                             a.Detail_id,
                             a.yxq,
                             a.PDate,
                             a.Price,
                             a.is_can_sale,
                             materials.name,
                             materials.spec,
                             vendor.vendor_name,
                             materials.unit,
                             materials.name,
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
			                    a.bill_id ='" + ViewState["billId"].ToString().Trim() + "'";
                DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
                aList.Clear();
                if (dt != null && dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AddData a = new AddData()
                        {
                            id = dt.Rows[i]["Detail_id"].ToString(),
                            pm = dt.Rows[i]["name"].ToString(),
                            spec = dt.Rows[i]["spec"].ToString(),
                            cz = dt.Rows[i]["texture"].ToString(),
                            ys = dt.Rows[i]["color"].ToString(),
                            goods_unit = dt.Rows[i]["unit"].ToString(),
                            pch = dt.Rows[i]["pch"].ToString(),
                            hwh = dt.Rows[i]["hwh"].ToString(),
                            pdsl = Convert.ToDecimal(dt.Rows[i]["qty"]).ToString("0.##"),
                            Price = dt.Rows[i]["price"].ToString() == "" ? "0.00" : Convert.ToDecimal(dt.Rows[i]["price"]).ToString("0.##"),
                            total = dt.Rows[i]["price"].ToString() == "" ? "0.00" : (Convert.ToDecimal(dt.Rows[i]["qty"]) * Convert.ToDecimal(dt.Rows[i]["price"])).ToString("0.##"),
                            customer_name = dt.Rows[i]["vendor_name"].ToString()
                        };
                        aList.Add(a);
                    }
                }
                gvAddData.DataSource = aList;
                gvAddData.DataBind();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "focus", "<script>alert('删除失败!')</script>");
            }
        }

    }
}