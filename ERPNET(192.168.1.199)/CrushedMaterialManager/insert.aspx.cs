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
using System.Net.Mail;
using System.Drawing;
using ERPPlugIn.Class;
using System.Text;
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
            string inputArea = RadioButtonList1.SelectedValue;




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

            string sqlExchange = @"select wb_hl from prd_dictate_wb inner join  goods
                                       on prd_dictate_wb.wb_name = goods.wb_name
                                       where goods.new_price=" + price + "";
            SqlDataReader dhl = new SelectCommandBuilder().ExecuteReader(sqlExchange);
            double exchang = 1;
            //计算汇率默认为人民币，如果价格表没有查询到默认为人民币
            while (dhl.Read())
            {
                exchang = double.Parse(dhl[0].ToString());
            }
            dhl.Close();
            price = price * exchang;
            double moneySum = price * double.Parse(count);
            HttpCookie Cook = Request.Cookies["cookie"];
            string userName = Cook["name"].ToString();


            //插入数据           

            string sqlinsert = @"insert into 
                            shatter_Parts(goodsName,Shattercount,price,moneySum,badContent,produceTime,employeeName
                            ,produceArea,inputArea,inputTime,inputName)
                            values('" + goodsName + "'," + count + "," + price + "," + moneySum + ",'" + badcontent
                                           + "'," + produceTime + ",'" + employeeName + "','" + produceArea
                                           + "', '" + inputArea
                                          + "',getDate(),'" + userName + "')";
            Debug.WriteLine(sqlinsert);
            new InsertCommandBuilder().ExecuteNonQuery(sqlinsert);
            Response.Redirect("index.aspx");
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }

      
    }
}