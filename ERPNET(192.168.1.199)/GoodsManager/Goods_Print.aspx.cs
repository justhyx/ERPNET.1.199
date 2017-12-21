using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using System.Configuration;

namespace ERPPlugIn.GoodsManager
{
    public partial class Goods_Print : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConXG_ZhuSu"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCustomer();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT goods_id, RTRIM(goods_name) + ISNULL(Version, '') AS goods_name,
                                  goods_ename, mjh, dw, qs, Materail_Number, ys, Materail_Model, 
                                  Materail_Vender_Color, cpdz, skdz, Drying_Temperature, Drying_Time, sk_scale, 
                                  cxzq, khdm, remark, Fire_Retardant_Grade, Buyer, Toner_Model, Toner_Buyer, 
                                  Aircraft, Rohs_Certification,Materail_Name,Materail_Color,Model_Abrasives
                            FROM goods_tran ";
            if (ddlCustomer.SelectedItem.Value != "0")
            {
                sql += "where khdm = '" + ddlCustomer.SelectedItem.Value + "'";
            }
            gvData.DataSource = new SelectCommandBuilder(constr, "").ExecuteDataTable(sql);
            gvData.DataBind();
        }
        private void BindCustomer()
        {
            SelectCommandBuilder s = new SelectCommandBuilder(constr,"customer");
            s.SelectColumn("customer_id");
            s.SelectColumn("customer_aname");
            s.ConditionsColumn("1", "1");
            s.getSelectCommand();
            DataTable d = s.ExecuteDataTable();
            ddlCustomer.Items.Clear();
            ddlCustomer.Items.Add(new ListItem("请选择", "0"));
            for (int i = 0; i < d.Rows.Count; i++)
            {
                ddlCustomer.Items.Add(new ListItem(d.Rows[i]["customer_aname"].ToString(), d.Rows[i]["customer_id"].ToString()));
            }
            ddlCustomer.DataTextField = "customer_aname";
        }
    }
}