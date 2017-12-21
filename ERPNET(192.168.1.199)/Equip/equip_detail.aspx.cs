using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Configuration;
using System.Data;

namespace ERPPlugIn.Equip
{
    public partial class equip_detail : System.Web.UI.Page
    {
        public static string Constr { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string T_equip_name = Request.QueryString["t_ename"];
               Constr = ConfigurationManager.ConnectionStrings[T_equip_name].ConnectionString;

            }
        } 
        protected void Button1_Click(object sender, EventArgs e)
        {
            string eTime = T_eTime.Text.ToString();
            string sTime = T_sTime.Text.ToString();
            string G_name = goods_name.Text.ToString();
            string T_ng = Ng.Text.ToString();
            string sql=@"SELECT goods_name, equip, causation, qty, operate_date, operator, dictate_date, BC
                     FROM RejectsDetails WHERE (1 = 1)";
            if (eTime !="" && sTime!="")
            {
               sql=sql+" and (convert(varchar(12),operate_date,112) between '"+ sTime +"' and '"+ eTime +"')"; 
            }
            if (G_name!="")
            {
                sql = sql + " and goods_name='" + G_name + "'";
            }
            if (T_ng!="")
            {
                sql = sql + " and causation='"+ T_ng +"'";
            }
            sql = sql + " order by operate_date";
            DataTable d = new SelectCommandBuilder(Constr, "").ExecuteDataTable(sql);
            GridView1.DataSource=d;
            GridView1.DataBind();
            string T_sql = "SELECT 生产部番,起始时间, 终止时间, 合计时间,停机原因, 作业日期, 班别 FROM 停机时间 where 1=1";
            if (eTime != "" && sTime != "")
            {
                T_sql = T_sql + " and (convert(varchar(12),终止时间,112) between '" + sTime + "' and '" + eTime + "')";
            }
            if (G_name!="")
            {
                T_sql = T_sql + " and 生产部番='" + G_name + "'";
            }
            T_sql = T_sql + " order by 起始时间";
            DataTable Td = new SelectCommandBuilder(Constr, "").ExecuteDataTable(T_sql);
            GridView2.DataSource = Td;
            GridView2.DataBind();
        }
        
         

    }
}