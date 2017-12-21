using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Configuration;
using System.Web.UI.MobileControls;
using System.Data;
using NPOI.HSSF.Record.Formula.Functions;
using System.Data.SqlClient;
using System.Drawing;
using System.Net.NetworkInformation;

namespace ERPPlugIn.Equip
{
    public partial class IndexEquipState : System.Web.UI.Page
    {
        string Con380T05 = ConfigurationManager.ConnectionStrings["Conn380T05"].ConnectionString;
        string Con470T02 = ConfigurationManager.ConnectionStrings["Conn470T02"].ConnectionString;
        string Con450T01 = ConfigurationManager.ConnectionStrings["Conn450T01"].ConnectionString;
        string Con530T01 = ConfigurationManager.ConnectionStrings["Conn530T01"].ConnectionString;
        string Con380T04 = ConfigurationManager.ConnectionStrings["Conn380T04"].ConnectionString;
        string Con550T01 = ConfigurationManager.ConnectionStrings["Conn550T01"].ConnectionString;
        string Con320T04 = ConfigurationManager.ConnectionStrings["Conn320T04"].ConnectionString;
        string Con380T06 = ConfigurationManager.ConnectionStrings["Conn380T06"].ConnectionString;
        string Con320T03 = ConfigurationManager.ConnectionStrings["Conn320T03"].ConnectionString;
        string Con200T02 = ConfigurationManager.ConnectionStrings["Conn200T02"].ConnectionString;
        string Con280T09 = ConfigurationManager.ConnectionStrings["Conn280T09"].ConnectionString;
        string Con280T08 = ConfigurationManager.ConnectionStrings["Conn280T08"].ConnectionString;
        string Con280T07 = ConfigurationManager.ConnectionStrings["Conn280T07"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {                
                get470T02();
                get450T01();
                get380T05();
                get530T01();
                get380T04();
                get550T01();
                get320T04();
                get380T06();
                get320T03();
                get200T02();
                get280T07();
                get280T08();
                get280T09();
            }
        }
        public void get470T02()
        {
            string sql23001 = @"SELECT ISNULL(SUM(RejectsTotal.qty), 0) AS reQty, 不良计算.计数, 不良计算.部番, 
                                  equip.equip_name, equip.state_color
                            FROM equip INNER JOIN
                                  不良计算 ON equip.equip_name = 不良计算.机台 LEFT OUTER JOIN
                                  RejectsTotal ON 不良计算.部番 = RejectsTotal.goods_name
                            GROUP BY 不良计算.计数, RejectsTotal.goods_name, RejectsTotal.equip, 
                                  equip.state_color, 不良计算.部番, equip.equip_name, equip.state_color
                            HAVING (equip.equip_name = '470T-2')";
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send("192.168.3.174", 300);//第一个参数为ip地址，第二个参数为ping的时间
            if (reply.Status == IPStatus.Success)//成功
            {
                try
                {
                    DataTable dt = new SelectCommandBuilder(Con470T02, "").ExecuteDataTable(sql23001);
                    N4702.Text = dt.Rows[0][1].ToString();
                    T4702.Text = dt.Rows[0][2].ToString();
                    NR4702.Text = dt.Rows[0][0].ToString();
                    NRA4702.Text = (Convert.ToDecimal(NR4702.Text) / (Convert.ToDecimal(N4702.Text) + Convert.ToDecimal(NR4702.Text)) * 100).ToString("0.0#") + "%";
                    string cb4702 = new SelectCommandBuilder(Con470T02, "").ExecuteDataTable("select state_color from equip where equip_name='470T-2'").Rows[0][0].ToString();
                    C4702.Attributes.Add("bgcolor", "#'" + cb4702 + "'");
                }
                catch (Exception)  //异常说明数据库连接有问题，打不开连接
                {
                    N4702.Text = "机台未开机";
                }   
            }
            else
            {
                N4702.Text = "未开机";
                C4702.Attributes.Add("bgcolor", "#999999");

            }            
                    
            //Label31.Text = "<a herf=equip_detail.aspx?id=60T01>60T-1</a>";
        }
        public void get450T01()
        {
            string sql23001 = @"SELECT ISNULL(SUM(RejectsTotal.qty), 0) AS reQty, 不良计算.计数, 不良计算.部番, 
                                  equip.equip_name, equip.state_color
                            FROM equip INNER JOIN
                                  不良计算 ON equip.equip_name = 不良计算.机台 LEFT OUTER JOIN
                                  RejectsTotal ON 不良计算.部番 = RejectsTotal.goods_name
                            GROUP BY 不良计算.计数, RejectsTotal.goods_name, RejectsTotal.equip, 
                                  equip.state_color, 不良计算.部番, equip.equip_name, equip.state_color
                            HAVING (equip.equip_name = '450T-1')";
            //ping 
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send("192.168.3.175", 300);//第一个参数为ip地址，第二个参数为ping的时间            
            if (reply.Status == IPStatus.Success)//成功
            {
                try
                {                
                    DataTable dt = new SelectCommandBuilder(Con450T01, "").ExecuteDataTable(sql23001);
                    N4501.Text = dt.Rows[0][1].ToString();
                    T4501.Text = dt.Rows[0][2].ToString();
                    NR4501.Text = dt.Rows[0][0].ToString();
                    NRA4501.Text = (Convert.ToDecimal(NR4501.Text) / (Convert.ToDecimal(N4501.Text) + Convert.ToDecimal(NR4501.Text)) * 100).ToString("0.0#") + "%";
                    string cb4501 = new SelectCommandBuilder(Con450T01, "").ExecuteDataTable("select state_color from equip where equip_name='450T-1'").Rows[0][0].ToString();
                    C4501.Attributes.Add("bgcolor", "#'" + cb4501 + "'");
                }
                catch (Exception)
                {
                    N4501.Text="数据异常";
                }
            }
            else
            {
                N4501.Text = "机台未开机";
                C4501.Attributes.Add("bgcolor", "#999999");
            }           
            //Label31.Text = "<a herf=equip_detail.aspx?id=60T01>60T-1</a>";
        }
        public void get380T05()
        {
            string sql38005 = @"SELECT ISNULL(SUM(RejectsTotal.qty), 0) AS reQty, 不良计算.计数, 不良计算.部番, 
                                  equip.equip_name, equip.state_color
                            FROM equip INNER JOIN
                                  不良计算 ON equip.equip_name = 不良计算.机台 LEFT OUTER JOIN
                                  RejectsTotal ON 不良计算.部番 = RejectsTotal.goods_name
                            GROUP BY 不良计算.计数, RejectsTotal.goods_name, RejectsTotal.equip, 
                                  equip.state_color, 不良计算.部番, equip.equip_name, equip.state_color
                            HAVING (equip.equip_name = '380T-5')";
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send("192.168.3.173", 300);//第一个参数为ip地址，第二个参数为ping的时间
            if (reply.Status == IPStatus.Success)//成功
            {
                try
                {
                    DataTable dt = new SelectCommandBuilder(Con380T05, "").ExecuteDataTable(sql38005);
                    N3805.Text = dt.Rows[0][1].ToString();
                    T3805.Text = dt.Rows[0][2].ToString();
                    NR3805.Text = dt.Rows[0][0].ToString();
                    NRA3805.Text = (Convert.ToDecimal(NR3805.Text) / (Convert.ToDecimal(N3805.Text) + Convert.ToDecimal(NR3805.Text)) * 100).ToString("0.0#") + "%";
                    string cb3805 = new SelectCommandBuilder(Con380T05, "").ExecuteDataTable("select state_color from equip where equip_name='380T-5'").Rows[0][0].ToString();
                    C3805.Attributes.Add("bgcolor", "#'" + cb3805 + "'");
                }
                catch (Exception)
                {
                    N3805.Text = "数据异常";
                }
            }
            else
            {
                N3805.Text = "未开机";
                C3805.Attributes.Add("bgcolor","#999999");
            }
            //Label31.Text = "<a herf=equip_detail.aspx?id=60T01>60T-1</a>";
        }
        public void get530T01()
        {
            string sql53001 = @"SELECT ISNULL(SUM(RejectsTotal.qty), 0) AS reQty, 不良计算.计数, 不良计算.部番, 
                                  equip.equip_name, equip.state_color
                            FROM equip INNER JOIN
                                  不良计算 ON equip.equip_name = 不良计算.机台 LEFT OUTER JOIN
                                  RejectsTotal ON 不良计算.部番 = RejectsTotal.goods_name
                            GROUP BY 不良计算.计数, RejectsTotal.goods_name, RejectsTotal.equip, 
                                  equip.state_color, 不良计算.部番, equip.equip_name, equip.state_color
                            HAVING (equip.equip_name = '530T-1')";
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send("192.168.3.172", 300);//第一个参数为ip地址，第二个参数为ping的时间
            if (reply.Status == IPStatus.Success)//成功
            {
                try
                {
                    DataTable dt = new SelectCommandBuilder(Con530T01, "").ExecuteDataTable(sql53001);
                    N5301.Text = dt.Rows[0][1].ToString();
                    T5301.Text = dt.Rows[0][2].ToString();
                    NR5301.Text = dt.Rows[0][0].ToString();
                    NRA5301.Text = (Convert.ToDecimal(NR5301.Text) / (Convert.ToDecimal(N5301.Text) + Convert.ToDecimal(NR5301.Text)) * 100).ToString("0.0#") + "%";
                    string cb5301 = new SelectCommandBuilder(Con530T01, "").ExecuteDataTable("select state_color from equip where equip_name='530T-1'").Rows[0][0].ToString();
                    C5301.Attributes.Add("bgcolor", "#'" + cb5301 + "'");
                }
                catch (Exception)
                {
                    N5301.Text = "数据异常";
                }
            }
            else
            {
                N5301.Text = "未开机";
                C5301.Attributes.Add("bgcolor", "#999999");
            }
            
            //Label31.Text = "<a herf=equip_detail.aspx?id=60T01>60T-1</a>";
        }
        public void get380T04()
        {
            string sql38004 = @"SELECT ISNULL(SUM(RejectsTotal.qty), 0) AS reQty, 不良计算.计数, 不良计算.部番, 
                                  equip.equip_name, equip.state_color
                            FROM equip INNER JOIN
                                  不良计算 ON equip.equip_name = 不良计算.机台 LEFT OUTER JOIN
                                  RejectsTotal ON 不良计算.部番 = RejectsTotal.goods_name
                            GROUP BY 不良计算.计数, RejectsTotal.goods_name, RejectsTotal.equip, 
                                  equip.state_color, 不良计算.部番, equip.equip_name, equip.state_color
                            HAVING (equip.equip_name = '380T-4')";
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send("192.168.3.171", 300);//第一个参数为ip地址，第二个参数为ping的时间
            if (reply.Status == IPStatus.Success)//成功
            {
                try
                {
                    DataTable dt = new SelectCommandBuilder(Con380T04, "").ExecuteDataTable(sql38004);
                    N3804.Text = dt.Rows[0][1].ToString();
                    T3804.Text = dt.Rows[0][2].ToString();
                    NR3804.Text = dt.Rows[0][0].ToString();
                    NRA3804.Text = (Convert.ToDecimal(NR3804.Text) / (Convert.ToDecimal(N3804.Text) + Convert.ToDecimal(NR3804.Text)) * 100).ToString("0.0#") + "%";
                    string cb3804 = new SelectCommandBuilder(Con380T04, "").ExecuteDataTable("select state_color from equip where equip_name='380T-4'").Rows[0][0].ToString();
                    C3804.Attributes.Add("bgcolor", "#'" + cb3804 + "'");
                }
                catch (Exception)
                {
                    N3804.Text = "数据异常";
                }
            }
            else
            {
                N3804.Text = "未开机";
                C3804.Attributes.Add("bgcolor", "#999999");
            }           
            //Label31.Text = "<a herf=equip_detail.aspx?id=60T01>60T-1</a>";
        }
        public void get550T01()
        {
            string sql55001 = @"SELECT ISNULL(SUM(RejectsTotal.qty), 0) AS reQty, 不良计算.计数, 不良计算.部番, 
                                  equip.equip_name, equip.state_color
                            FROM equip INNER JOIN
                                  不良计算 ON equip.equip_name = 不良计算.机台 LEFT OUTER JOIN
                                  RejectsTotal ON 不良计算.部番 = RejectsTotal.goods_name
                            GROUP BY 不良计算.计数, RejectsTotal.goods_name, RejectsTotal.equip, 
                                  equip.state_color, 不良计算.部番, equip.equip_name, equip.state_color
                            HAVING (equip.equip_name = '550T-1')";
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send("192.168.3.164", 300);//第一个参数为ip地址，第二个参数为ping的时间
            if (reply.Status == IPStatus.Success)//成功
            {
                try
                {
                    DataTable dt = new SelectCommandBuilder(Con550T01, "").ExecuteDataTable(sql55001);
                    N5501.Text = dt.Rows[0][1].ToString();
                    T5501.Text = dt.Rows[0][2].ToString();
                    NR5501.Text = dt.Rows[0][0].ToString();
                    NRA5501.Text = (Convert.ToDecimal(NR5501.Text) / (Convert.ToDecimal(N5501.Text) + Convert.ToDecimal(NR5501.Text)) * 100).ToString("0.0#") + "%";
                    string cb5501 = new SelectCommandBuilder(Con550T01, "").ExecuteDataTable("select state_color from equip where equip_name='550T-1'").Rows[0][0].ToString();
                    C5501.Attributes.Add("bgcolor", "#'" + cb5501 + "'");
                }
                catch (Exception)
                {
                    N5501.Text = "数据异常";
                }
            }
            else
            {
                N5501.Text = "未开机";
                C5501.Attributes.Add("bgcolor", "#999999");
            }            
            //Label31.Text = "<a herf=equip_detail.aspx?id=60T01>60T-1</a>";
        }        
        public void get320T04()
        {
            string sql32004 = @"SELECT ISNULL(SUM(RejectsTotal.qty), 0) AS reQty, 不良计算.计数, 不良计算.部番, 
                                  equip.equip_name, equip.state_color
                            FROM equip INNER JOIN
                                  不良计算 ON equip.equip_name = 不良计算.机台 LEFT OUTER JOIN
                                  RejectsTotal ON 不良计算.部番 = RejectsTotal.goods_name
                            GROUP BY 不良计算.计数, RejectsTotal.goods_name, RejectsTotal.equip, 
                                  equip.state_color, 不良计算.部番, equip.equip_name, equip.state_color
                            HAVING (equip.equip_name = '320T-4')";
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send("192.168.3.162", 300);//第一个参数为ip地址，第二个参数为ping的时间
            if (reply.Status == IPStatus.Success)//成功
            {
                try
                {
                    DataTable dt = new SelectCommandBuilder(Con320T04, "").ExecuteDataTable(sql32004);
                    N3204.Text = dt.Rows[0][1].ToString();
                    T3204.Text = dt.Rows[0][2].ToString();
                    NR3204.Text = dt.Rows[0][0].ToString();
                    NRA3204.Text = (Convert.ToDecimal(NR3204.Text) / (Convert.ToDecimal(N3204.Text) + Convert.ToDecimal(NR3204.Text)) * 100).ToString("0.0#") + "%";
                    string cb3204 = new SelectCommandBuilder(Con320T04, "").ExecuteDataTable("select state_color from equip where equip_name='320T-4'").Rows[0][0].ToString();
                    C3204.Attributes.Add("bgcolor", "#'" + cb3204 + "'");
                }
                catch (Exception)
                {
                    N3204.Text = "数据异常";                    
                }
            }
            else
            {
                N3204.Text = "未开机";
                C3204.Attributes.Add("bgcolor", "#999999");
            }            
            //Label31.Text = "<a herf=equip_detail.aspx?id=60T01>60T-1</a>";
        }
        public void get380T06()
        {
            string sql38006 = @"SELECT ISNULL(SUM(RejectsTotal.qty), 0) AS reQty, 不良计算.计数, 不良计算.部番, 
                                  equip.equip_name, equip.state_color
                            FROM equip INNER JOIN
                                  不良计算 ON equip.equip_name = 不良计算.机台 LEFT OUTER JOIN
                                  RejectsTotal ON 不良计算.部番 = RejectsTotal.goods_name
                            GROUP BY 不良计算.计数, RejectsTotal.goods_name, RejectsTotal.equip, 
                                  equip.state_color, 不良计算.部番, equip.equip_name, equip.state_color
                            HAVING (equip.equip_name = '380T-6')";
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send("192.168.3.161", 300);//第一个参数为ip地址，第二个参数为ping的时间
            if (reply.Status == IPStatus.Success)//成功
            {
                try
                {
                    DataTable dt = new SelectCommandBuilder(Con380T06, "").ExecuteDataTable(sql38006);
                    N3806.Text = dt.Rows[0][1].ToString();
                    T3806.Text = dt.Rows[0][2].ToString();
                    NR3806.Text = dt.Rows[0][0].ToString();
                    NRA3806.Text = (Convert.ToDecimal(NR3806.Text) / (Convert.ToDecimal(N3806.Text) + Convert.ToDecimal(NR3806.Text)) * 100).ToString("0.0#") + "%";
                    string cb3806 = new SelectCommandBuilder(Con380T06, "").ExecuteDataTable("select state_color from equip where equip_name='380T-6'").Rows[0][0].ToString();
                    C3806.Attributes.Add("bgcolor", "#'" + cb3806 + "'");
                }
                catch (Exception)
                {
                    N3806.Text = "数据异常";
                }
            }
            else
            {
                N3806.Text = "未开机";
                C3806.Attributes.Add("bgcolor", "#999999");
            }            
            //Label31.Text = "<a herf=equip_detail.aspx?id=60T01>60T-1</a>";
        }
        public void get320T03()
        {
            string sql32003 = @"SELECT ISNULL(SUM(RejectsTotal.qty), 0) AS reQty, 不良计算.计数, 不良计算.部番, 
                                  equip.equip_name, equip.state_color
                            FROM equip INNER JOIN
                                  不良计算 ON equip.equip_name = 不良计算.机台 LEFT OUTER JOIN
                                  RejectsTotal ON 不良计算.部番 = RejectsTotal.goods_name
                            GROUP BY 不良计算.计数, RejectsTotal.goods_name, RejectsTotal.equip, 
                                  equip.state_color, 不良计算.部番, equip.equip_name, equip.state_color
                            HAVING (equip.equip_name = '320T-3')";
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send("192.168.3.166", 300);//第一个参数为ip地址，第二个参数为ping的时间
            if (reply.Status == IPStatus.Success)//成功
            {
                try
                {
                    DataTable dt = new SelectCommandBuilder(Con320T03, "").ExecuteDataTable(sql32003);
                    N3203.Text = dt.Rows[0][1].ToString();
                    T3203.Text = dt.Rows[0][2].ToString();
                    NR3203.Text = dt.Rows[0][0].ToString();
                    NRA3203.Text = (Convert.ToDecimal(NR3203.Text) / (Convert.ToDecimal(N3203.Text) + Convert.ToDecimal(NR3203.Text)) * 100).ToString("0.0#") + "%";
                    string cb3203 = new SelectCommandBuilder(Con320T03, "").ExecuteDataTable("select state_color from equip where equip_name='320T-3'").Rows[0][0].ToString();
                    C3203.Attributes.Add("bgcolor", "#'" + cb3203 + "'");
                }
                catch (Exception)
                {
                    N3203.Text = "数据异常";
                }
            }
            else
            {
                N3203.Text = "未开机";
                C3203.Attributes.Add("bgcolor", "#999999");
            }
            
            //Label31.Text = "<a herf=equip_detail.aspx?id=60T01>60T-1</a>";
        }
        public void get200T02()
        {
            string sql20003 = @"SELECT ISNULL(SUM(RejectsTotal.qty), 0) AS reQty, 不良计算.计数, 不良计算.部番, 
                                  equip.equip_name, equip.state_color
                            FROM equip INNER JOIN
                                  不良计算 ON equip.equip_name = 不良计算.机台 LEFT OUTER JOIN
                                  RejectsTotal ON 不良计算.部番 = RejectsTotal.goods_name
                            GROUP BY 不良计算.计数, RejectsTotal.goods_name, RejectsTotal.equip, 
                                  equip.state_color, 不良计算.部番, equip.equip_name, equip.state_color
                            HAVING (equip.equip_name = '200T-2')";
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send("192.168.3.168", 300);//第一个参数为ip地址，第二个参数为ping的时间
            if (reply.Status == IPStatus.Success)//成功
            {
                try
                {
                    DataTable dt = new SelectCommandBuilder(Con200T02, "").ExecuteDataTable(sql20003);
                    N2002.Text = dt.Rows[0][1].ToString();
                    T2002.Text = dt.Rows[0][2].ToString();
                    NR2002.Text = dt.Rows[0][0].ToString();
                    NRA2002.Text = (Convert.ToDecimal(NR2002.Text) / (Convert.ToDecimal(N2002.Text) + Convert.ToDecimal(NR2002.Text)) * 100).ToString("0.0#") + "%";
                    string cb2002 = new SelectCommandBuilder(Con200T02, "").ExecuteDataTable("select state_color from equip where equip_name='200T-2'").Rows[0][0].ToString();
                    C2002.Attributes.Add("bgcolor", "#'" + cb2002 + "'");
                }
                catch (Exception)
                {
                    N2002.Text = "数据异常";
                }
            }
            else
            {
                N2002.Text = "未开机";
                C2002.Attributes.Add("bgcolor", "#999999");
            }           
            //Label31.Text = "<a herf=equip_detail.aspx?id=60T01>60T-1</a>";
        }
        public void get280T09()
        {
            string sql28009 = @"SELECT ISNULL(SUM(RejectsTotal.qty), 0) AS reQty, 不良计算.计数, 不良计算.部番, 
                                  equip.equip_name, equip.state_color
                            FROM equip INNER JOIN
                                  不良计算 ON equip.equip_name = 不良计算.机台 LEFT OUTER JOIN
                                  RejectsTotal ON 不良计算.部番 = RejectsTotal.goods_name
                            GROUP BY 不良计算.计数, RejectsTotal.goods_name, RejectsTotal.equip, 
                                  equip.state_color, 不良计算.部番, equip.equip_name, equip.state_color
                            HAVING (equip.equip_name = '280T-9')";
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send("192.168.3.169", 300);//第一个参数为ip地址，第二个参数为ping的时间
            if (reply.Status == IPStatus.Success)//成功
            {
                try
                {
                    DataTable dt = new SelectCommandBuilder(Con280T09, "").ExecuteDataTable(sql28009);
                    N2809.Text = dt.Rows[0][1].ToString();
                    T2809.Text = dt.Rows[0][2].ToString();
                    NR2809.Text = dt.Rows[0][0].ToString();
                    NRA2809.Text = (Convert.ToDecimal(NR2809.Text) / (Convert.ToDecimal(N2809.Text) + Convert.ToDecimal(NR2809.Text)) * 100).ToString("0.0#") + "%";
                    string cb2809 = new SelectCommandBuilder(Con280T09, "").ExecuteDataTable("select state_color from equip where equip_name='280T-9'").Rows[0][0].ToString();
                    C2809.Attributes.Add("bgcolor", "#'" + cb2809 + "'");
                }
                catch (Exception)
                {
                    N2809.Text = "数据异常";
                }
            }
            else
            {
                N2809.Text = "未开机";
                C2809.Attributes.Add("bgcolor", "#999999");
            }
            
            //Label31.Text = "<a herf=equip_detail.aspx?id=60T01>60T-1</a>";
        }
        public void get280T08()
        {
            string sql28008 = @"SELECT ISNULL(SUM(RejectsTotal.qty), 0) AS reQty, 不良计算.计数, 不良计算.部番, 
                                  equip.equip_name, equip.state_color
                            FROM equip INNER JOIN
                                  不良计算 ON equip.equip_name = 不良计算.机台 LEFT OUTER JOIN
                                  RejectsTotal ON 不良计算.部番 = RejectsTotal.goods_name
                            GROUP BY 不良计算.计数, RejectsTotal.goods_name, RejectsTotal.equip, 
                                  equip.state_color, 不良计算.部番, equip.equip_name, equip.state_color
                            HAVING (equip.equip_name = '280T-8')";
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send("192.168.3.165", 300);//第一个参数为ip地址，第二个参数为ping的时间
            if (reply.Status == IPStatus.Success)//成功
            {
                try
                {
                    DataTable dt = new SelectCommandBuilder(Con280T08, "").ExecuteDataTable(sql28008);
                    N2808.Text = dt.Rows[0][1].ToString();
                    T2808.Text = dt.Rows[0][2].ToString();
                    NR2808.Text = dt.Rows[0][0].ToString();
                    NRA2808.Text = (Convert.ToDecimal(NR2808.Text) / (Convert.ToDecimal(N2808.Text) + Convert.ToDecimal(NR2808.Text)) * 100).ToString("0.0#") + "%";
                    string cb2808 = new SelectCommandBuilder(Con280T08, "").ExecuteDataTable("select state_color from equip where equip_name='280T-8'").Rows[0][0].ToString();
                    C2808.Attributes.Add("bgcolor", "#'" + cb2808 + "'");
                }
                catch (Exception)
                {
                    N2808.Text = "数据异常";
                }
            }
            else
            {
                N2808.Text = "未开机";
                C2808.Attributes.Add("bgcolor", "#999999");
            }            
            //Label31.Text = "<a herf=equip_detail.aspx?id=60T01>60T-1</a>";
        }
        public void get280T07()
        {
            string sql28007 = @"SELECT ISNULL(SUM(RejectsTotal.qty), 0) AS reQty, 不良计算.计数, 不良计算.部番, 
                                  equip.equip_name, equip.state_color
                            FROM equip INNER JOIN
                                  不良计算 ON equip.equip_name = 不良计算.机台 LEFT OUTER JOIN
                                  RejectsTotal ON 不良计算.部番 = RejectsTotal.goods_name
                            GROUP BY 不良计算.计数, RejectsTotal.goods_name, RejectsTotal.equip, 
                                  equip.state_color, 不良计算.部番, equip.equip_name, equip.state_color
                            HAVING (equip.equip_name = '280T-7')";            
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send("192.168.3.167", 100);//第一个参数为ip地址，第二个参数为ping的时间            
            if (reply.Status == IPStatus.Success)//成功
            {
                try
                {                
                    DataTable dt = new SelectCommandBuilder(Con280T07, "").ExecuteDataTable(sql28007);
                    N2807.Text = dt.Rows[0][1].ToString();
                    T2807.Text = dt.Rows[0][2].ToString();
                    NR2807.Text = dt.Rows[0][0].ToString();
                    NRA2807.Text = (Convert.ToDecimal(NR2807.Text) / (Convert.ToDecimal(N2807.Text) + Convert.ToDecimal(NR2807.Text)) * 100).ToString("0.0#") + "%";
                    string cb2807 = new SelectCommandBuilder(Con280T07, "").ExecuteDataTable("select state_color from equip where equip_name='280T-7'").Rows[0][0].ToString();
                    C2807.Attributes.Add("bgcolor", "#'" + cb2807 + "'");
                }
                catch (Exception)
                {
                    N2807.Text = "数据异常";
                }
            }
            else
            {
                N2807.Text = "未开机";
                C2807.Attributes.Add("bgcolor", "#999999");
            }           
            //Label31.Text = "<a herf=equip_detail.aspx?id=60T01>60T-1</a>";
        }
    }
}