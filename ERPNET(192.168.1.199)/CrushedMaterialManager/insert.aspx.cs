using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
namespace ERPPlugIn.CrushedMaterialManager
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sqlgetproduce = @"select distinct equip.Area  from goods inner join equip on  goods.sbdm=equip.equip_id 
                                     where equip.Area is not null ";
                DataTable prodouce = new SelectCommandBuilder().ExecuteDataTable(sqlgetproduce);

            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //获取需要的值
            string goodsName = (TextBoxgoodsName.Text).Trim();
            string count = TextBoxcount.Text;
            string badcontent = TextBoxbadcontent.Text;
            string produceTime = string.IsNullOrWhiteSpace(TextBoxproduceTime.Text.Trim()) ? "null" : "'" + TextBoxproduceTime.Text.Trim() + "'";

            string employeeName = TextBoxemployeeName.Text;
            string produceArea = DropDownListproduceArea.Value;


          

                //根据输入的部番，查询出其他需要的数据
                string sqlgetPrice = @" select new_price from goods where goods_name='" + goodsName + "'";
                //string sqlspec = @" select spec from goods where goods_name='" + goodsName + "'";
                //string sqlcz = @" select cz from goods where goods_name='" + goodsName + "'";
                //string sqlys = @" select ys from goods where goods_name='" + goodsName + "'";
                Debug.WriteLine(sqlgetPrice);
                double price = 0;
                
                SqlDataReader dr = new SelectCommandBuilder().ExecuteReader(sqlgetPrice);
                while (dr.Read())
                {
                    price = double.Parse(dr[0].ToString());
                }
                dr.Close();
                double moneySum = price * double.Parse(count);
          

                //插入数据           

                string sqlinsert = @"insert into 
                            shatter_Parts(goodsName,count,price,moneySum,badContent,produceTime,employeeName,produceArea,inputTime)
                            values('" + goodsName + "'," + count + "," + price + "," + moneySum + ",'" + badcontent
                                               + "'," + produceTime + ",'" + employeeName + "','" + produceArea
                                              + "',getDate())";
                Debug.WriteLine(sqlinsert);
                new InsertCommandBuilder().ExecuteNonQuery(sqlinsert);
                //把数据库自动生成的时间改为""
                Response.Redirect("index.aspx");
            }
        

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}