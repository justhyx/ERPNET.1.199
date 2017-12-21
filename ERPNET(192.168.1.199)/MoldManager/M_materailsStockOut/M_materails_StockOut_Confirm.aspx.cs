using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;

namespace ERPPlugIn.MoldManager.M_materailsStockOut
{
    public partial class M_materails_StockOut_Confirm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvDetailData.DataSource = null;
            gvDetailData.DataBind();
            string sql = "SELECT str_out_no, str_in_no, mode_no, Convert(varchar(11),str_out_date,120)str_out_date, Convert(varchar(11),operate_date,120)operate_date,(SELECT operator_name FROM operator WHERE operator_id = operator) AS operator, remark FROM m_materails_pre_str_out where is_confirm = 'N'";
            if (!string.IsNullOrEmpty(txtStartDate.Text))
            {
                sql += "  And Convert(varchar(11),str_in_date,120) = '" + txtStartDate.Text + "'";
            }
            if (!string.IsNullOrEmpty(txtMNo.Text))
            {
                sql += " And mode_no = '" + txtMNo.Text.Trim() + "'";
            }
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            gvData.DataSource = dt;
            gvData.DataBind();
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (gvData.SelectedIndex == -1)
            {
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('请选择审核项目')</script>", false);
                return;
            }
            List<string> SList = new List<string>();
            string str_out_no = gvData.SelectedRow.Cells[0].Text.Trim();
            string str_in_no = gvData.SelectedRow.Cells[1].Text.Trim();
            string sql1 = @"SELECT m_materails_str_in.str_in_no, m_materails_str_in_detail.name, 
                                  m_materails_str_in_detail.texture, m_materails_str_in_detail.spec, 
                                  m_materails_str_in_detail.hwh, m_stock.qty, 
                                  m_materails_pre_str_out_detail.qty AS SoQty, m_stock.m_id
                            FROM m_materails_pre_str_out_detail INNER JOIN
                                  m_materails_str_in INNER JOIN
                                  m_materails_str_in_detail ON 
                                  m_materails_str_in.str_in_no = m_materails_str_in_detail.str_in_no INNER JOIN
                                  m_stock ON m_materails_str_in_detail.id = m_stock.refrence_id ON 
                                  m_materails_pre_str_out_detail.name = m_stock.name WHERE (m_materails_str_in.str_in_no = '" + str_in_no + "')";
            DataTable tb = new SelectCommandBuilder().ExecuteDataTable(sql1);
            if (tb != null && tb.Rows.Count != 0)
            {
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (Convert.ToInt32(tb.Rows[i]["qty"]) == Convert.ToInt32(tb.Rows[i]["SoQty"]))
                    {
                        string s = "delete from m_stock where m_id = '" + tb.Rows[i]["m_id"] + "'";
                        SList.Add(s);
                    }
                    else if (Convert.ToInt32(tb.Rows[i]["qty"]) > Convert.ToInt32(tb.Rows[i]["SoQty"]))
                    {
                        string s = "update m_stock set qty = qty-'" + Convert.ToInt32(tb.Rows[i]["SoQty"]) + "' where id = '" + tb.Rows[i]["m_id"] + "'";
                        SList.Add(s);
                    }
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('无库存')</script>", false);
                return;
            }
            string sql = "insert into m_materails_str_out_detail(str_out_no, name, texture, spec, qty,hwh, is_check) SELECT str_out_no, name, texture, spec, qty,hwh, is_check FROM m_materails_pre_str_out_detail where str_out_no='" + gvData.SelectedRow.Cells[0].Text.Trim() + "'";
            string MainSql = "insert into m_materails_str_out(str_out_no, str_in_no, mode_no, str_out_date, operate_date, operator, remark, is_confirm) SELECT str_out_no, str_in_no, mode_no, str_out_date, operate_date, operator, remark,'N' FROM m_materails_pre_str_out where str_out_no='" + gvData.SelectedRow.Cells[0].Text.Trim() + "'";
            string updateSql = "update m_materails_pre_str_out_detail set is_check = 'Y' where str_out_no='" + gvData.SelectedRow.Cells[0].Text.Trim() + "'";
            string updateMainSql = "update m_materails_pre_str_out set is_confirm = 'Y' where str_out_no='" + gvData.SelectedRow.Cells[0].Text.Trim() + "'";
            SList.Add(sql);
            SList.Add(MainSql);
            SList.Add(updateSql);
            SList.Add(updateMainSql);
            int count = new InsertCommandBuilder().ExcutTransaction(SList);
            if (count != 0)
            {
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('审核成功')</script>", false);
                btnSearch_Click(sender, e);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('审核失败')</script>", false);
            }
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //----------鼠标移动到每项时颜色交替效果
                //e.Row.Attributes["onmouseover"] = "e=this.style.backgroundColor;this.style.backgroundColor='#FFFF99'";
                //e.Row.Attributes["onmouseout"] = "this.style.backgroundColor=e";
                //----------设置悬浮鼠标指针形状为小手"
                e.Row.Attributes["style"] = "Cursor:hand";
                String evt = Page.ClientScript.GetPostBackClientHyperlink(sender as GridView, "Select$" + e.Row.RowIndex.ToString()); e.Row.Attributes.Add("onclick", evt);
            }
        }

        protected void gvData_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "SELECT str_out_no, name, texture, spec, qty, hwh FROM m_materails_pre_str_out_detail where str_out_no = '" + gvData.SelectedRow.Cells[0].Text.Trim() + "'";
            gvDetailData.DataSource = new SelectCommandBuilder().ExecuteDataTable(sql);
            gvDetailData.DataBind();
        }
    }
}