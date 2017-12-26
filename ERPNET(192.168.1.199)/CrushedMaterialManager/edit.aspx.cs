using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
namespace ERPPlugIn.CrushedMaterialManager
{
    public partial class edit : System.Web.UI.Page
    {

        SelectCommandBuilder cmd = new SelectCommandBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                if (id == 0)
                {
                    Response.Redirect("index.aspx");
                }
                string sql = @"select goodsName,count,badContent,produceTime,employeeName,produceArea
                      from shatter_Parts where id= " + id + " and produceArea is not null";
                DataTable dt = cmd.ExecuteDataTable(sql);

                TextgoodsName.Text = (string)dt.Rows[0]["goodsName"];
                Textcount.Text = Convert.ToString(dt.Rows[0]["count"]);
                Textbadcontent.Text = (string)dt.Rows[0]["badContent"];
                TextemployeeName.Text = (string)dt.Rows[0]["employeeName"];
            }
        }
        protected void ButtonOk_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            string goodsName = (TextgoodsName.Text).Trim();
            string count = Textcount.Text;
            string badcontent = Textbadcontent.Text;
            string produceTime = string.IsNullOrWhiteSpace(TextproduceTime.Text.Trim()) ? "null" : "'" + TextproduceTime.Text.Trim() + "'";
            string employeeName = TextemployeeName.Text;
            string produceArea = DropDownListproduceArea.Value;
           

            UpdateCommandBuilder ucd = new UpdateCommandBuilder();
           
                string sqlgetPrice = @" select new_price from goods where goods_name='" + goodsName + "'";
                //string sqlspec = @" select spec from goods where goods_name='" + goodsName + "'";
                Debug.WriteLine(sqlgetPrice);
                double price = 0;
                using (SqlDataReader dr = cmd.ExecuteReader(sqlgetPrice))
                {
                    while (dr.Read())
                    {
                        price = double.Parse(dr[0].ToString());
                    }
                    dr.Close();
                }
                double moneySum = price * double.Parse(count);
                //string spec = (string)cmd.ExecuteScalar(sqlspec);


                string sql = @"update shatter_Parts set
                              goodsName='" + goodsName + "',count='" + count + "',price='" +
                                           price + "',moneySum='" + moneySum + "',badContent='"
                                           + badcontent + "',produceTime=" + produceTime + ",employeeName='" +
                                           employeeName + "',produceArea='" +
                                           produceArea + "' where id=" + id + "";
                Debug.WriteLine(sql);

                ucd.ExecuteNonQuery(sql);

                Response.Redirect("index.aspx");
           
        }
        protected void ButtonBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}