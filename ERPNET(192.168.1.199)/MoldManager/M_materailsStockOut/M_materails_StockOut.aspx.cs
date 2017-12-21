using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using ERPPlugIn.MoldManager.M_materailsOrder;
using ERPPlugIn.Class;

namespace ERPPlugIn.MoldManager.M_materailsStockOut
{
    public partial class M_materails_StockOut : System.Web.UI.Page
    {
        public string Str_In_Id { get { return ViewState["id"].ToString(); } set { ViewState["id"] = value; } }
        public string Mno { get { return ViewState["Mno"].ToString(); } set { ViewState["Mno"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Str_In_Id = Request.QueryString["id"];
                Mno = Request.QueryString["Mno"];
                string sql = @"SELECT id,str_in_no, name, texture, spec, hwh, qty AS qty FROM m_materails_str_in_detail where is_check = 'N' and str_in_no='" + Str_In_Id + "'";
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
                    if (Convert.ToInt32((gvDetailData.Rows[i].Cells[7].FindControl("txtPrice") as TextBox).Text.Trim()) > Convert.ToInt32(gvDetailData.Rows[i].Cells[6].Text.Trim()))
                    {
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('不能超出库存数量')</script>", false);
                        (gvDetailData.Rows[i].Cells[6].FindControl("txtPrice") as TextBox).Focus();
                        return;
                    }
                    Details dItem = new Details()
                    {
                        Apply_No = Str_In_Id,
                        MNo = Mno,
                        Name = gvDetailData.Rows[i].Cells[2].Text.Trim(),
                        Texture = gvDetailData.Rows[i].Cells[3].Text.Trim(),
                        Spec = gvDetailData.Rows[i].Cells[4].Text.Trim(),
                        Qty = Convert.ToInt32((gvDetailData.Rows[i].Cells[7].FindControl("txtPrice") as TextBox).Text.Trim()),
                        Id = Convert.ToInt64(gvDetailData.Rows[i].Cells[8].Text.Trim()),
                        Hwh = gvDetailData.Rows[i].Cells[5].Text.Trim()
                    };
                    DList.Add(dItem);
                }
            }
            if (DList.Count == 0)
            {
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('无选择数据')</script>", false);
                return;
            }
            string Id = CommadMethod.getNextId("SO" + DateTime.Now.ToString("yyyyMMdd"), "").Trim();
            for (int i = 0; i < DList.Count; i++)
            {
                InsertCommandBuilder insert = new InsertCommandBuilder("m_materails_pre_str_out_detail");
                UpdateCommandBuilder up = new UpdateCommandBuilder("m_materails_str_in_detail");
                up.UpdateColumn("is_check", "Y");
                up.ConditionsColumn("Id", DList[i].Id);
                insert.InsertColumn("str_out_no", Id);
                insert.InsertColumn("name", DList[i].Name);
                insert.InsertColumn("texture", DList[i].Texture);
                insert.InsertColumn("spec", DList[i].Spec);
                insert.InsertColumn("qty", DList[i].Qty);
                if (!string.IsNullOrEmpty(DList[i].Hwh))
                {
                    insert.InsertColumn("hwh", DList[i].Hwh);
                }
                insert.InsertColumn("is_check", "N");
                SList.Add(insert.getInsertCommand());
                SList.Add(up.getUpdateCommand());
            }
            InsertCommandBuilder ins = new InsertCommandBuilder("m_materails_pre_str_out");
            ins.InsertColumn("str_out_no", Id);
            if (!string.IsNullOrEmpty(Mno))
            {
                ins.InsertColumn("mode_no", Mno);
            }
            ins.InsertColumn("str_in_no", Str_In_Id);
            ins.InsertColumn("Operator", "0000");
            ins.InsertColumn("str_out_date", "getDate()");
            ins.InsertColumn("Operate_Date", "getDate()");
            ins.InsertColumn("is_confirm", "N");
            SList.Add(ins.getInsertCommand());
            int count = ins.ExcutTransaction(SList);
            if (count != 0)
            {
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('保存成功')</script>", false);
                gvDetailData.DataSource = null;
                gvDetailData.DataBind();
                string updateSql = "update m_materails_str_in set is_confirm = 'Y' where str_in_no='" + Str_In_Id.Trim() + "'";
                int countY = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar("select count(*) from m_materails_str_in_detail where str_in_no='" + Str_In_Id.Trim() + "' and is_check = 'N' "));
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
            Response.Redirect("M_materails_StockOut_List.aspx");
        }
    }
}