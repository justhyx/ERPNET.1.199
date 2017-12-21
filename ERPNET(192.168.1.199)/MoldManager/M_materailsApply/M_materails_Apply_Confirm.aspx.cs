using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;

namespace ERPPlugIn.MoldManager.M_materailsApply
{
    public partial class M_materails_Apply_Confirm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvDetailData.DataSource = null;
            gvDetailData.DataBind();
            string sql = "select apply_no, mode_no, internal_no, Convert(varchar(11),apply_date,120) apply_date, apply_by, remark FROM m_materails_apply where is_confirm = 'N'";
            if (!string.IsNullOrEmpty(txtStartDate.Text))
            {
                sql += "  And Convert(varchar(11),apply_date,120) = '" + txtStartDate.Text + "'";
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
            string sql = "update m_materails_apply set is_confirm = 'Y' where apply_no = '" + gvData.SelectedRow.Cells[0].Text.Trim() + "'";
            int count = new UpdateCommandBuilder().ExecuteNonQuery(sql);
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
            string sql = "SELECT apply_no, name, texture, spec, qty FROM m_materails_apply_detail where apply_no = '" + gvData.SelectedRow.Cells[0].Text.Trim() + "'";
            gvDetailData.DataSource = new SelectCommandBuilder().ExecuteDataTable(sql);
            gvDetailData.DataBind();
        }
    }
}