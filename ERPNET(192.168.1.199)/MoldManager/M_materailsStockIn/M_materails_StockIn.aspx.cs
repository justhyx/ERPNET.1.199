using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using ERPPlugIn.MoldManager.M_materailsOrder;
using ERPPlugIn.Class;

namespace ERPPlugIn.MoldManager.M_materailsStockIn
{
    public partial class M_materails_StockIn : System.Web.UI.Page
    {
        public string orderNo { get { return ViewState["orderNo"].ToString(); } set { ViewState["orderNo"] = value; } }
        public string Mno { get { return ViewState["Mno"].ToString(); } set { ViewState["Mno"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                orderNo = Request.QueryString["id"];
                Mno = Request.QueryString["Mno"];
                string sql = "SELECT id, order_no, name, texture, spec, qty FROM m_materails_pch_order_detail where is_end='N' and order_no='" + orderNo + "'";
                gvDetailData.DataSource = new SelectCommandBuilder().ExecuteDataTable(sql);
                gvDetailData.DataBind();
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            List<Details> DList = new List<Details>();
            List<string> SList = new List<string>();
            for (int i = 0; i < gvDetailData.Rows.Count; i++)
            {
                if ((gvDetailData.Rows[i].Cells[0].FindControl("checkbox1") as CheckBox).Checked == true)
                {
                    if (string.IsNullOrEmpty((gvDetailData.Rows[i].Cells[5].FindControl("txtPrice") as TextBox).Text.Trim()))
                    {
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('请输入数量')</script>", false);
                        (gvDetailData.Rows[i].Cells[6].FindControl("txtPrice") as TextBox).Focus();
                        return;
                    }
                    Details dItem = new Details()
                    {
                        Apply_No = orderNo,
                        MNo = Mno,
                        Name = gvDetailData.Rows[i].Cells[2].Text.Trim(),
                        Texture = gvDetailData.Rows[i].Cells[3].Text.Trim(),
                        Spec = gvDetailData.Rows[i].Cells[4].Text.Trim(),
                        Qty = Convert.ToInt32((gvDetailData.Rows[i].Cells[6].FindControl("txtPrice") as TextBox).Text.Trim()),
                        Hwh = (gvDetailData.Rows[i].Cells[7].FindControl("txtHwh") as TextBox).Text.Trim(),
                        Id = Convert.ToInt64(gvDetailData.Rows[i].Cells[8].Text.Trim())
                    };
                    DList.Add(dItem);
                }
            }
            if (DList.Count == 0)
            {
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('无选择数据')</script>", false);
                return;
            }
            string Id = CommadMethod.getNextId("SI" + DateTime.Now.ToString("yyyyMMdd"), "").Trim();
            for (int i = 0; i < DList.Count; i++)
            {
                InsertCommandBuilder insert = new InsertCommandBuilder("m_materails_pre_str_in_detail");
                UpdateCommandBuilder up = new UpdateCommandBuilder("m_materails_pch_order_detail");
                up.UpdateColumn("is_end", "Y");
                up.ConditionsColumn("Id", DList[i].Id);
                insert.InsertColumn("str_in_no", Id);
                insert.InsertColumn("name", DList[i].Name);
                insert.InsertColumn("texture", DList[i].Texture);
                insert.InsertColumn("spec", DList[i].Spec);
                insert.InsertColumn("qty", DList[i].Qty);
                insert.InsertColumn("hwh", DList[i].Hwh);
                insert.InsertColumn("is_check", "N");
                SList.Add(insert.getInsertCommand());
                SList.Add(up.getUpdateCommand());
            }
            InsertCommandBuilder ins = new InsertCommandBuilder("m_materails_pre_str_in");
            ins.InsertColumn("str_in_no", Id);
            if (!string.IsNullOrEmpty(Mno))
            {
                ins.InsertColumn("mode_no", Mno);
            }
            ins.InsertColumn("order_no", orderNo);
            ins.InsertColumn("Operator", "0000");
            ins.InsertColumn("str_in_date", "getDate()");
            ins.InsertColumn("Operate_Date", "getDate()");
            ins.InsertColumn("is_confirm", "N");
            SList.Add(ins.getInsertCommand());
            int count = ins.ExcutTransaction(SList);
            if (count != 0)
            {
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('保存成功')</script>", false);
                gvDetailData.DataSource = null;
                gvDetailData.DataBind();
                string updateSql = "update m_materails_pch_order set isCheck = 'Y' where Order_No='" + orderNo.Trim() + "'";
                int countY = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar("select count(*) from m_materails_pch_order_detail where Order_No='" + orderNo.Trim() + "' and is_end = 'N' "));
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

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("M_materails_StockIn_List.aspx");
        }
    }
}