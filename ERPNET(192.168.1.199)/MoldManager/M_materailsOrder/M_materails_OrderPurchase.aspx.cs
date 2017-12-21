using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using ERPPlugIn.Class;

namespace ERPPlugIn.MoldManager.M_materailsOrder
{
    public partial class M_materails_OrderPurchase : System.Web.UI.Page
    {
        public string Mno
        {
            get { return ViewState["Mno"].ToString(); }
            set { ViewState["Mno"] = value; }
        }
        public string Apply_No
        {
            get { return ViewState["Apply_No"].ToString(); }
            set { ViewState["Apply_No"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getVendor();
                Apply_No = Request.QueryString["id"];
                Mno = Request.QueryString["MNo"];
                string sql = " ";
                if (!string.IsNullOrEmpty(Mno))
                {
                    sql = @"SELECT m_materails_apply_detail.Id, m_materails_apply.apply_no, m_materails_apply.mode_no, 
                                  m_materails_apply_detail.name, m_materails_apply_detail.texture, 
                                  m_materails_apply_detail.spec, m_materails_apply_detail.qty
                            FROM m_materails_apply INNER JOIN
                                  m_materails_apply_detail ON 
                                  m_materails_apply.apply_no = m_materails_apply_detail.apply_no where m_materails_apply_detail.is_check = 'N' and m_materails_apply.mode_no = '" + Mno + "'";
                }
                else
                {
                    sql = " SELECT apply_no, name, texture, spec, qty FROM m_materails_apply_detail where is_check = 'N' and apply_no = '" + Apply_No + "'";
                }
                gvDetailData.DataSource = new SelectCommandBuilder().ExecuteDataTable(sql);
                gvDetailData.DataBind();
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (ddlVendor.SelectedIndex == 0)
            {
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('请选择供应商')</script>", false);
                return;
            }
            if (string.IsNullOrEmpty(txtDate.Text))
            {
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('请选择采购日期')</script>", false);
                return;
            }
            List<Details> DList = new List<Details>();
            List<string> SList = new List<string>();
            for (int i = 0; i < gvDetailData.Rows.Count; i++)
            {
                if ((gvDetailData.Rows[i].Cells[0].FindControl("checkbox1") as CheckBox).Checked == true)
                {
                    if (string.IsNullOrEmpty((gvDetailData.Rows[i].Cells[6].FindControl("txtPrice") as TextBox).Text.Trim()))
                    {
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('请输入价格')</script>", false);
                        (gvDetailData.Rows[i].Cells[6].FindControl("txtPrice") as TextBox).Focus();
                        return;
                    }
                    Details dItem = new Details()
                    {
                        Apply_No = Apply_No,
                        MNo = Mno,
                        Name = gvDetailData.Rows[i].Cells[2].Text.Trim(),
                        Texture = gvDetailData.Rows[i].Cells[3].Text.Trim(),
                        Spec = gvDetailData.Rows[i].Cells[4].Text.Trim(),
                        Qty = Convert.ToInt32(gvDetailData.Rows[i].Cells[5].Text.Trim()),
                        Price = Convert.ToDouble((gvDetailData.Rows[i].Cells[6].FindControl("txtPrice") as TextBox).Text.Trim()),
                        Id = Convert.ToInt64(gvDetailData.Rows[i].Cells[7].Text.Trim())
                    };
                    DList.Add(dItem);
                }
            }
            if (DList.Count == 0)
            {
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('无选择数据')</script>", false);
                return;
            }
            string Id = CommadMethod.getNextId("PCH" + DateTime.Now.ToString("yyyyMMdd"), "").Trim();
            InsertCommandBuilder insert = new InsertCommandBuilder("m_materails_pch_order_detail");
            for (int i = 0; i < DList.Count; i++)
            {
                UpdateCommandBuilder up = new UpdateCommandBuilder("m_materails_apply_detail");
                up.UpdateColumn("is_check", "Y");
                up.ConditionsColumn("Id", DList[i].Id);
                insert.InsertColumn("Order_No", Id);
                insert.InsertColumn("name", DList[i].Name);
                insert.InsertColumn("texture", DList[i].Texture);
                insert.InsertColumn("spec", DList[i].Spec);
                insert.InsertColumn("qty", DList[i].Qty);
                insert.InsertColumn("price", DList[i].Price);
                insert.InsertColumn("is_end", "N");
                insert.InsertColumn("wb_id", ddlwb.SelectedItem.Value);
                SList.Add(insert.getInsertCommand());
                SList.Add(up.getUpdateCommand());
                insert.CommandClear();
            }
            InsertCommandBuilder ins = new InsertCommandBuilder("m_materails_pch_order");
            ins.InsertColumn("Order_date", txtDate.Text);
            ins.InsertColumn("Order_No", Id);
            if (!string.IsNullOrEmpty(Mno))
            {
                ins.InsertColumn("mode_no", Mno);
            }
            ins.InsertColumn("Vendor_id", ddlVendor.SelectedItem.Value);
            ins.InsertColumn("Remark", txtRemark.Text.Trim());
            ins.InsertColumn("Operator_id", "0000");
            ins.InsertColumn("Operator_Date", "getDate()");
            ins.InsertColumn("isCheck", "N");
            SList.Add(ins.getInsertCommand());
            int count = ins.ExcutTransaction(SList);
            if (count != 0)
            {
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('保存成功')</script>", false);
                gvDetailData.DataSource = null;
                gvDetailData.DataBind();
                txtDate.Text = string.Empty;
                txtRemark.Text = string.Empty;
                ddlVendor.SelectedIndex = 0;
                ddlwb.SelectedIndex = 0;
                string updateSql = "";
                int countY = 0;
                if (string.IsNullOrEmpty(Mno))
                {
                    updateSql = "update m_materails_apply set is_confirm = 'E' where apply_no='" + Apply_No.Trim() + "'";
                    countY = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar("select count(*) from m_materails_apply_detail where apply_no='" + Id.Trim() + "' and is_check = 'N' "));
                }
                else
                {
                    updateSql = "update m_materails_apply set is_confirm = 'E' where mode_no = '" + Mno.Trim() + "'";
                    countY = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar("select count(*) from m_materails_apply_detail where apply_no='" + Id.Trim() + "' and is_check = 'N' "));
                }
                if (countY == 0)
                {
                    new UpdateCommandBuilder().ExecuteNonQuery(updateSql);
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('保存失败')</script>", false);
            }
        }
        public void getVendor()
        {
            string sql = "SELECT vendor_name, vendor_id FROM m_vendor";
            DataTable data = new SelectCommandBuilder().ExecuteDataTable(sql);
            if (data != null && data.Rows.Count != 0)
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    ddlVendor.Items.Add(new ListItem(data.Rows[i]["vendor_name"].ToString(), data.Rows[i]["vendor_id"].ToString()));
                }
            }
            sql = "SELECT wb_id, wb_name FROM prd_dictate_wb ";
            data = new SelectCommandBuilder().ExecuteDataTable(sql);
            if (data != null && data.Rows.Count != 0)
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    ddlwb.Items.Add(new ListItem(data.Rows[i]["wb_name"].ToString(), data.Rows[i]["wb_id"].ToString()));
                }
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("M_materails_OrderList.aspx");
        }
    }
    public class Details
    {
        public long Id { get; set; }
        public string MNo { get; set; }
        public string Apply_No { get; set; }
        public string Name { get; set; }
        public string Texture { get; set; }
        public string Spec { get; set; }
        public int Qty { get; set; }
        public Double Price { get; set; }
        public string Hwh { get; set; }
    }
}