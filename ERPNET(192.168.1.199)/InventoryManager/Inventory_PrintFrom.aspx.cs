using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;

namespace ERPPlugIn.InventoryManager
{
    public partial class Inventory_PrintFrom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string Id = Request.QueryString["s_id"];
                dgvData.DataSource = getData(Id);
                dgvData.DataBind();
            }
        }
        protected DataTable getData(string s_id)
        {
            string sql = @"SELECT stock_remain.goods_id, goods.goods_name , goods.spec , 
                           goods.ys , batch.hwh, batch.pch  FROM stock_remain LEFT OUTER JOIN
                           goods ON goods.goods_id = stock_remain.goods_id INNER JOIN
                           batch ON stock_remain.batch_id = batch.batch_id
                           WHERE (stock_remain.store_id = '" + s_id + "')";
            return new SelectCommandBuilder().ExecuteDataTable(sql);
        }
    }
}