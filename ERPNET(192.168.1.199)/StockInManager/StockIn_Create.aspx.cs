using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using ERPPlugIn.Class;
using System.Configuration;

namespace ERPPlugIn.StockInManager
{
    public partial class StockIn_Create : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConXG_ZhuSu"].ConnectionString;
        public string sql { get; set; }
        public string No { get; set; }
        public string bill_id { get; set; }
        public string DId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                trGoodsName.Visible = false;
                trgvList.Visible = false;
                txtgoodsName.Attributes.Add("onkeypress", "EnterTextBox('btnSearch')");
            }
        }
        protected void bindDDL()
        {
            string sql = "SELECT store_id, store_name FROM store";
            SelectCommandBuilder s = new SelectCommandBuilder(constr, "");
            DataTable dt = s.ExecuteDataTable(sql);
            ddlMaterialStock.Items.Clear();
            ddlMaterialStock.Items.Add(new ListItem("请选择", "0"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddlMaterialStock.Items.Add(new ListItem(dt.Rows[i]["store_name"].ToString(), dt.Rows[i]["store_id"].ToString()));
            }
        }

        protected void rbtType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMaterialStock.SelectedItem.Text == "无")
            {
                bindDDL();
                txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ddlMaterialStock.Focus();
            }
            if (rbtType.SelectedIndex != 0)
            {
                trGoodsName.Visible = true;
            }
            else
            {
                trGoodsName.Visible = false;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (ddlMaterialStock.SelectedItem.Value == "0")
            {
                Response.Write("<script>alert('请选择仓位点')</script>");
                return;
            }
            if (txtNo.Text.Trim().Equals(string.Empty))
            {
                Response.Write("<script>alert('请输入单号')</script>");
                txtNo.Focus();
                return;
            }
            if (rbtType.SelectedIndex != 0)
            {
                if (txtgoodsName.Text == string.Empty)
                {
                    Response.Write("<script>alert('请输入部番')</script>");
                    txtgoodsName.Focus();
                    return;
                }
                if (gvList.SelectedIndex == -1)
                {
                    Response.Write("<script>alert('没有选择计划')</script>");
                    return;
                }

            }
            DateTime date = DateTime.Now;
            string IdLeftPart = "HC" + date.Year.ToString().Substring(2, 2) + date.Month.ToString().PadLeft(2, '0') + date.Day.ToString().PadLeft(2, '0');
            string str_in_bill_id = CommadMethod.getNextId(IdLeftPart, "");
            InsertCommandBuilder ins = new InsertCommandBuilder(constr,"pre_str_in_bill");
            ins.InsertColumn("str_in_bill_id", str_in_bill_id);
            ins.InsertColumn("str_in_bill_no", txtNo.Text);
            ins.InsertColumn("operator_id", "0000");
            ins.InsertColumn("operator_date", txtDate.Text);
            ins.InsertColumn("store_id", ddlMaterialStock.SelectedItem.Value.Trim());
            ins.InsertColumn("str_in_type_id", rbtType.SelectedItem.Value);
            ins.InsertColumn("come_from", rbtType.SelectedIndex == 0 ? rbtType.SelectedItem.Text.Trim() : gvList.SelectedRow.Cells[1].Text.Trim());
            ins.InsertColumn("is_state", "Y");
            sql = ins.getInsertCommand();
            No = txtNo.Text.Trim();
            bill_id = str_in_bill_id;
            if (rbtType.SelectedIndex == 0)
            {
                Server.Transfer("StockIn_AddData.aspx");
            }
            else if (rbtType.SelectedIndex == 1)
            {
                DId = gvList.SelectedRow.Cells[1].Text.Trim();
                Server.Transfer("StockIn_AddDictateData.aspx");
            }

        }

        protected void ddlMaterialStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNo.Focus();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            trgvList.Visible = false;
            string sql = @"SELECT goods_tmp.goods_name, prd_dictate.dictate_date, prd_dictate.dictate_id, 
                                  prd_dictate.prd_qty, equip.equip_name
                           FROM equip INNER JOIN
                                  prd_dictate ON equip.equip_id = prd_dictate.equip_id INNER JOIN
                                  goods_tmp ON prd_dictate.goods_id = goods_tmp.goods_id WHERE (prd_dictate.is_End = 'N') AND (goods_tmp.goods_name = '" + txtgoodsName.Text.Trim().ToUpper() + "')";
            DataTable dt = new SelectCommandBuilder(constr, "").ExecuteDataTable(sql);
            gvList.DataSource = dt;
            gvList.DataBind();
            if (dt != null && dt.Rows.Count != 0)
            {
                trgvList.Visible = true;
            }
            else
            {
                Response.Write("<script>alert('此部番无任何计划')</script>");
            }
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["style"] = "Cursor:hand";
                String evt = Page.ClientScript.GetPostBackClientHyperlink(sender as GridView, "Select$" + e.Row.RowIndex.ToString()); e.Row.Attributes.Add("onclick", evt);
            }
        }
    }
}