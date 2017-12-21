using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;

namespace ERPPlugIn.Mold_Management_Accounting
{
    public partial class Mode_SetRate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtRate.Focus();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string sql = "update Mode_rate set rate =" + (double.Parse(txtRate.Text.Trim()) / 100) + "";
            int count = new UpdateCommandBuilder().ExecuteNonQuery(sql);
            if (count != 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "<script>alert('修改成功!')</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "refreshParent", "<script>refreshParent()</script>");
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "<script>alert('修改失败!')</script>");
            }
        }

    }
}