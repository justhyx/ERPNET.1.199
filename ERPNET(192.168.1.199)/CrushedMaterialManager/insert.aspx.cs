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
            double moneySum = price * double.Parse(count) ;
            HttpCookie Cook = Request.Cookies["cookie"];
            string userName = Cook["name"].ToString();


            //插入数据           

            string sqlinsert = @"insert into 
                            shatter_Parts(goodsName,count,price,moneySum,badContent,produceTime,employeeName
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

        protected void Button3_Click(object sender, EventArgs e)
        {
            string MailUser = "systemmail@sz-hudson.com";//邮箱
            string MailPwd = "Hh123456h";//邮箱密码
            string MailName = "系统通知";
            string MailHost = "smtp.exmail.qq.com";//smtp的地址

            MailAddress from = new MailAddress(MailUser, MailName); //邮件的发件人  
            MailMessage mail = new MailMessage();

            //设置邮件的标题  
            mail.Subject = "粉碎部品记录审核";

            //设置邮件的发件人  
            
            mail.From = from;

         //获取所需要发送邮件的邮箱
            SelectCommandBuilder cmd = new SelectCommandBuilder("Data Source=192.168.1.7;Initial Catalog=hudsonwwwroot;User ID=sa");
            string sql = @"select email from HUDSON_User where noticeLever % 2 = 1";
            
            SqlDataReader dr = cmd.ExecuteReader(sql);
            while (dr.Read())
            {
                string email = dr["email"].ToString();
                mail.To.Add(email);
            }
     

            //设置邮件的内容  
            mail.Body = "需要粉碎的部品已经录入完成，请尽快确认。";
            //设置邮件的格式  
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            //设置邮件的发送级别  
            mail.Priority = MailPriority.Normal;

            
            SmtpClient client = new SmtpClient();
            //设置用于 SMTP 事务的主机的名称，填IP地址也可以了  
            client.Host = MailHost;
            //设置用于 SMTP 事务的端口，默认的是 25  
            client.Port = 25;
            client.UseDefaultCredentials = false;
            //邮箱登陆名和密码 
            client.Credentials = new System.Net.NetworkCredential(MailUser, MailPwd);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            ////如果发送失败，SMTP 服务器将发送 失败邮件告诉我  
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


            try
            {
                client.Send(mail);
                Label1.Text = "发送邮件成功";
                Label1.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                Label1.Text = "发送邮件失败";
                Label1.ForeColor = Color.Red;
            }
        }
    }
}