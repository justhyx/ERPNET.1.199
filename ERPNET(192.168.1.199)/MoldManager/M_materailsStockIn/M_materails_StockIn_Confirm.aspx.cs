using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;

namespace ERPPlugIn.MoldManager.M_materailsStockIn
{
    public partial class M_materails_StockIn_Confirm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
            string sql = "SELECT str_in_no, name, texture, spec, qty, hwh FROM m_materails_pre_str_in_detail where str_in_no = '" + gvData.SelectedRow.Cells[0].Text.Trim() + "'";
            gvDetailData.DataSource = new SelectCommandBuilder().ExecuteDataTable(sql);
            gvDetailData.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvDetailData.DataSource = null;
            gvDetailData.DataBind();
            string sql = "SELECT str_in_no, order_no, mode_no, Convert(varchar(11),str_in_date,120)str_in_date, Convert(varchar(11),operate_date,120)operate_date,(SELECT operator_name FROM operator WHERE operator_id = operator) AS operator, remark FROM m_materails_pre_str_in where is_confirm = 'N'";
            if (!string.IsNullOrEmpty(txtStartDate.Text))
            {
                sql += "  And Convert(varchar(11),str_in_date,120) = '" + txtStartDate.Text + "'";
            }
            if (!string.IsNullOrEmpty(txtMNo.Text))
            {
                sql += " And mode_no = '" + txtMNo.Text.Trim() + "'";
            }
            gvData.DataSource = new SelectCommandBuilder().ExecuteDataTable(sql);
            gvData.DataBind();
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (gvData.SelectedIndex == -1)
            {
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('请选择审核项目')</script>", false);
                return;
            }
            string str_in_no = gvData.SelectedRow.Cells[0].Text.Trim();
            string sql = "insert into m_materails_str_in_detail(str_in_no, name, texture, spec, qty,hwh, is_check) SELECT str_in_no, name, texture, spec, qty,hwh, is_check FROM m_materails_pre_str_in_detail where str_in_no='" + gvData.SelectedRow.Cells[0].Text.Trim() + "'";
            string MainSql = "insert into m_materails_str_in(str_in_no, order_no, mode_no, str_in_date, operate_date, operator, remark, is_confirm) select str_in_no, order_no, mode_no, str_in_date, operate_date, operator, remark, 'N' from m_materails_pre_str_in where str_in_no='" + gvData.SelectedRow.Cells[0].Text.Trim() + "'";
            string updateSql = "update m_materails_pre_str_in_detail set is_check = 'Y' where str_in_no='" + gvData.SelectedRow.Cells[0].Text.Trim() + "'";
            string updateMainSql = "update m_materails_pre_str_in set is_confirm = 'Y' where str_in_no='" + gvData.SelectedRow.Cells[0].Text.Trim() + "'";
            string insertStockSql = "insert into m_stock(name,refrence_id,qty,is_can_sale) select name,id,qty,'Y' from m_materails_str_in_detail where str_in_no='" + gvData.SelectedRow.Cells[0].Text.Trim() + "'";
            List<string> SList = new List<string>();
            SList.Add(sql);
            SList.Add(MainSql);
            SList.Add(updateSql);
            SList.Add(insertStockSql);
            SList.Add(updateMainSql);
            SList.Add(updateSql);
            int count = new InsertCommandBuilder().ExcutTransaction(SList);
            if (count != 0)
            {
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('审核成功')</script>", false);
                //int countN = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar("select count(*) from m_materails_str_in_detail where str_in_no='" + gvData.SelectedRow.Cells[0].Text.Trim() + "' and is_check ='N' "));
                //if (countN == 0)
                //{
                //    string updateMainSql = "update m_materails_pre_str_in set is_confirm = 'Y' where str_in_no='" + gvData.SelectedRow.Cells[0].Text.Trim() + "'";
                //    new UpdateCommandBuilder().ExecuteNonQuery(updateMainSql);
                //}
                btnSearch_Click(sender, e);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('审核失败')</script>", false);
            }
            //if (gvData.SelectedIndex == -1)
            //{
            //    ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('请选择审核项目')</script>", false);
            //    return;
            //}
            //string str_in_no = gvData.SelectedRow.Cells[0].Text.Trim();
            //List<string> SList = new List<string>();
            //for (int i = 0; i < gvDetailData.Rows.Count; i++)
            //{
            //    InsertCommandBuilder ins = new InsertCommandBuilder("m_materails_str_in_detail");
            //    string updateSql = "update m_materails_pre_str_in_detail set is_check = 'Y' where id='" + gvDetailData.Rows[i].Cells[6].Text.Trim() + "'";
            //    ins.InsertColumn("str_in_no", str_in_no);
            //    ins.InsertColumn("name", gvDetailData.Rows[i].Cells[1].Text.Trim());
            //    ins.InsertColumn("texture", gvDetailData.Rows[i].Cells[2].Text.Trim());
            //    ins.InsertColumn("spec", gvDetailData.Rows[i].Cells[3].Text.Trim());
            //    ins.InsertColumn("hwh", gvDetailData.Rows[i].Cells[4].Text.Trim());
            //    ins.InsertColumn("qty", Convert.ToInt32(gvDetailData.Rows[i].Cells[5].Text.Trim()));
            //    ins.InsertColumn("is_check", "N");
            //    SList.Add(ins.getInsertCommand());
            //    SList.Add(updateSql);
            //}
            ////string sql = "insert into m_materails_str_in_detail(str_in_no, name, texture, spec, qty,hwh, is_check) SELECT str_in_no, name, texture, spec, qty,hwh, is_check FROM m_materails_pre_str_in_detail where str_in_no='" + gvData.SelectedRow.Cells[0].Text.Trim() + "'";
            //string MainSql = "insert into m_materails_str_in(str_in_no, order_no, mode_no, str_in_date, operate_date, operator, remark, is_confirm) select str_in_no, order_no, mode_no, str_in_date, operate_date, operator, remark, 'N' from m_materails_pre_str_in where str_in_no='" + gvData.SelectedRow.Cells[0].Text.Trim() + "'";
            ////string updateSql = "update m_materails_pre_str_in_detail set is_check = 'Y' where str_in_no='" + gvData.SelectedRow.Cells[0].Text.Trim() + "'";
            //string insertStockSql = "insert into m_stock(name,refrence_id,qty,is_can_sale) select name,id,qty,'Y' from m_materails_pre_str_in_detail where str_in_no='" + gvData.SelectedRow.Cells[0].Text.Trim() + "'";
            //SList.Add(MainSql);
            ////SList.Add(updateSql);
            //SList.Add(insertStockSql);
            //int count = new InsertCommandBuilder().ExcutTransaction(SList);
            //if (count != 0)
            //{
            //    ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('审核成功')</script>", false);
            //    int countN = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar("select count(*) from m_materails_str_in_detail where str_in_no='" + gvData.SelectedRow.Cells[0].Text.Trim() + "' and is_check ='N' "));
            //    if (countN == 0)
            //    {
            //        string updateMainSql = "update m_materails_pre_str_in set is_confirm = 'Y' where str_in_no='" + gvData.SelectedRow.Cells[0].Text.Trim() + "'";
            //        new UpdateCommandBuilder().ExecuteNonQuery(updateMainSql);
            //    }
            //    btnSearch_Click(sender, e);
            //}
            //else
            //{
            //    ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('审核失败')</script>", false);
            //}
        }
    }
}