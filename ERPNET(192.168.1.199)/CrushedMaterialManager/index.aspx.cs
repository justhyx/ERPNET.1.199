using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Text;
using ERPPlugIn.Class;
using System.Drawing;
namespace ERPPlugIn.CrushedMaterialManager
{
    public partial class index : System.Web.UI.Page
    {


        SelectCommandBuilder cmd = new SelectCommandBuilder();
        UpdateCommandBuilder ucd = new UpdateCommandBuilder();

        string sqlquery = @"select ID,goodsName,Shattercount ,(Shattercount * cpdz) as cpdz,price , moneySum ,
                            ISNULL(CONVERT(nvarchar(10),  inputTime,126),'') as inputTime,
                            badContent ,ISNULL( CONVERT(nvarchar(10),  produceTime,126),'') as produceTime,employeeName ,cz,produceArea,                                                       
                            CONVERT(nvarchar(20),  purchaseConfirm,126) as purchaseConfirm,
                            CONVERT(nvarchar(20),  PMCConfirm,126) as PMCConfirm,
                            CONVERT(nvarchar(20), directorConfirm,126) as directorConfirm,
                            CONVERT(nvarchar(20), managerConfirm,126) as managerConfirm
                            from shatter_Parts 
                            left join goods on goods.goods_name = shatter_Parts.goodsName
                            where tableSign is null 
                            and ({0}) order by id desc";
        string sqlmoney = @"select sum(moneySum) from shatter_Parts where tableSign is null and ({0}) ";

        string sqlconfirm = @"select count(goodsName) from shatter_Parts
                                         where (PMCConfirm is not null 
                                        and  purchaseConfirm is not null
                                        and directorConfirm is not null {0}) 
                                        and tableSign is null and {1} ";



        protected void Page_Load(object sender, EventArgs e)
        {

            HttpCookie Cook = Request.Cookies.Get("cookie");
            if (!IsPostBack)
            {
                //如果cookie没有就需要登录
                if (Cook == null)
                {
                    string urli = Request.Path;
                    Response.Redirect("../Login.aspx?urli=" + urli + "");
                }
                else
                {


                    //string userID = Cook.Values["id"];
                    //string sqlrole = "select department from shatter_Parts_Password where userID = '" + userID + "'";
                    //string role = (string)cmd.ExecuteScalar(sqlrole);
                    getData();
                    #region
                    //switch (role)
                    //{
                    //    //总经理承认如果前面的确认没有完成，这个按钮是灰色不能点击的，点击后代表审核动作完成
                    //    //sql中inputArea 1代表工程内不良2代表品检不良
                    //    case "topManagerConfirm":
                    //        getData();
                    //        string sqlCEOconfirm = string.Format(sqlconfirm, "and managerConfirm is not null", "inputArea =  2");
                    //        int CEOconfirm = Convert.ToInt32(cmd.ExecuteScalar(sqlCEOconfirm));
                    //        if (CEOconfirm == 0)
                    //        {
                    //            buttonConfirm.Enabled = false;
                    //        }
                    //        else
                    //        {

                    //        }
                    //        buttonConfirm.Text = "总经理承认";
                    //        buttonConfirm.Visible = true;
                    //        break;

                    //    //经理确认如果金额小于500，如果前面的确认没有完成这个按钮是灰色不能点击的，点击后就完成当前审核动作完成     
                    //    //如果金额大于500这个按钮就和其他确认按钮一样
                    //    //sql中inputArea 1代表工程内不良2代表品检不良
                    //    case "managerConfirm":
                    //        getData();
                    //        string[] money = LabelMoney.Text.Split(':');
                    //        double monrySum = Convert.ToDouble(money[1]);
                    //        string sqlManagerconfirm = string.Format(sqlconfirm, "", "inputArea =  2");
                    //        Debug.WriteLine(sqlManagerconfirm);
                    //        int Managerconfirm = Convert.ToInt32(cmd.ExecuteScalar(sqlManagerconfirm));
                    //        if (monrySum < 500)
                    //        {
                    //            if (Managerconfirm == 0)
                    //            {
                    //                buttonConfirm.Enabled = false;
                    //            }
                    //            else
                    //            {

                    //            }

                    //        }
                    //        buttonConfirm.Text = "经理确认";
                    //        buttonConfirm.Visible = true;
                    //        break;
                    //    case "purchaseConfirm":
                    //        getData();
                    //        buttonConfirm.Visible = true;
                    //        buttonConfirm.Text = "采购确认";
                    //        break;
                    //    case "PMCConfirm":
                    //        getData();
                    //        buttonConfirm.Text = "生管确认";
                    //        buttonConfirm.Visible = true;
                    //        break;
                    //    case "directorConfirm":
                    //        getData();
                    //        buttonConfirm.Text = "品质确认";
                    //        buttonConfirm.Visible = true;
                    //        break;
                    //    default:
                    //        getData();
                    //        break;

                    //}
                    #endregion

                }
            }
        }
        protected void getRoot(string inputArea)
        {
            //金额
            string sqlmoney1 = string.Format(sqlmoney, inputArea);
            Debug.WriteLine(sqlmoney1);
            double moneySum = 0;

            if (!(DBNull.Value.Equals(cmd.ExecuteScalar(sqlmoney1))))
            {
                moneySum = Convert.ToDouble(cmd.ExecuteScalar(sqlmoney1));
            }
            //根据金额判断两个承认按钮的状态
            if (moneySum > 500)
            {
                Button4.Enabled = true;
                string sql = @"select goodsName from shatter_Parts where
                           (purchaseConfirm is  null or
                            PMCConfirm is  null or
                            directorConfirm is  null or
                            managerConfirm is null )
                            and  " + inputArea + "";
                string goodsName = Convert.ToString(cmd.ExecuteScalar(sql));
                if (goodsName == "")
                {
                    Button5.Enabled = true;
                }
            }
            else
            {
                string sql = @"select goodsName from shatter_Parts where
                           (purchaseConfirm is  null or
                            PMCConfirm is  null or
                            directorConfirm is  null)
                            and " + inputArea + "";
                string goodsName = Convert.ToString(cmd.ExecuteScalar(sql));
                if (goodsName == "")
                {
                    Button4.Enabled = true;
                }
            }
        }
        protected void getData()
        {
            //绑定粉碎部品数据表
            string inputArea = "inputArea = 2";
            string sqlselect = string.Format(sqlquery, inputArea);
            Debug.WriteLine(sqlselect);
            DataTable dt = new DataTable();
            dt = cmd.ExecuteDataTable(sqlselect);
            GridView1.DataSource = dt;
            GridView1.DataBind();

            string[] produceArea = new string[] { "品检不良品", "工程内不良品" };
            DropDownListArea.DataSource = produceArea;
            DropDownListArea.DataBind();

            //金额
            string sqlmoney1 = string.Format(sqlmoney, inputArea);
            Debug.WriteLine(sqlmoney1);
            double moneySum = 0;

            if (!(DBNull.Value.Equals(cmd.ExecuteScalar(sqlmoney1))))
            {
                moneySum = Convert.ToDouble(cmd.ExecuteScalar(sqlmoney1));
            }
            lblMoney.InnerText = string.Format("总金额:{0}", moneySum);

            getRoot(inputArea);

        }

        //确认行的数据绑定
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
            {
                return;
            }
            DataRowView drv = e.Row.DataItem as DataRowView;
            //编辑行
            HyperLink linkedit = e.Row.FindControl("hlinkedit") as HyperLink;
            linkedit.Text = "编辑";
            linkedit.NavigateUrl = string.Format("edit.aspx?id={0}", drv.Row["id"]);
            //删除行
            HyperLink linkdelete = e.Row.FindControl("hlinkdelete") as HyperLink;
            linkdelete.NavigateUrl = string.Format("delete.aspx?id={0}", drv.Row["id"]);
            linkdelete.Text = "删除";
            var lsTitle = new Dictionary<string, string>() {  
             {"Moding","managerConfirm"},
            {"Budget","purchaseConfirm"}, 
            {"PMC","PMCConfirm"}, 
            {"Leader","directorConfirm"} };
            foreach (var t in lsTitle)
            {
                var linktitle = "hlink" + t.Key;
                var lbltitle = "lbl" + t.Key;
                Label txtlbl = e.Row.FindControl(lbltitle) as Label;
                if (DBNull.Value.Equals(drv.Row[t.Value]))
                {

                }
                else
                {
                    txtlbl.Visible = true;
                    string confirm = drv.Row[t.Value].ToString().ToString();
                    txtlbl.Text = confirm;
                }

            }

        }
        //根据下拉列表的改变而改变所录入的表
        protected void DropDownListArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            string produceArea = DropDownListArea.SelectedValue == "品检不良品" ? "品检不良品" : "工程内不良品";
            //inputArea等于2时代表品检不良1代表工程内不良
            string inputArea1 = "inputArea = 2";
            string inputArea2 = " inputArea = 1";
            string inputArea = DropDownListArea.SelectedValue == "品检不良品" ? inputArea1 : inputArea2;
            string sqlselect = string.Format(sqlquery, inputArea);

            Debug.WriteLine(sqlselect);
            DataTable dt = new DataTable();
            dt = cmd.ExecuteDataTable(sqlselect);
            GridView1.DataSource = dt;
            GridView1.DataBind();


            string sqlmoney1 = string.Format(sqlmoney, inputArea);
            Debug.WriteLine(sqlmoney1);
            double moneySum = 0;

            if (!(DBNull.Value.Equals(cmd.ExecuteScalar(sqlmoney1))))
            {
                moneySum = Convert.ToDouble(cmd.ExecuteScalar(sqlmoney1));
            }
            lblMoney.InnerText = string.Format("总金额:{0}", moneySum);

            getRoot(inputArea);
            //在切换不同录入来源的表的时经理和总经理按钮权限的控制
            #region
            //HttpCookie Cook = Request.Cookies.Get("cookie");            
            //string userID = Cook.Values["id"];
            //string sqlrole = "select department from shatter_Parts_Password where userID = '" + userID + "'";
            //string role = (string)cmd.ExecuteScalar(sqlrole);
            //switch (role)
            //{
            //    case "topManagerConfirm":                   
            //        string sqlCEOconfirm = string.Format(sqlconfirm, "and managerConfirm is not null",inputArea);
            //        int CEOconfirm = Convert.ToInt32(cmd.ExecuteScalar(sqlCEOconfirm));
            //        if (CEOconfirm == 0)
            //        {
            //            buttonConfirm.Enabled = false;
            //        }
            //        else
            //        {
            //            buttonConfirm.Enabled = true;
            //            buttonConfirm.Text = "总经理承认";
            //            buttonConfirm.Visible = true;

            //        }
            //        break;

            //    case "managerConfirm":


            //        string[] money = LabelMoney.Text.Split(':');
            //        double moneySum = Convert.ToDouble(money[1]);
            //        string sqlManagerconfirm = string.Format(sqlconfirm, "", inputArea);
            //        Debug.WriteLine(sqlManagerconfirm);
            //        int Managerconfirm = Convert.ToInt32(cmd.ExecuteScalar(sqlManagerconfirm));

            //        if (moneySum < 500)
            //        {
            //            if (Managerconfirm == 0)
            //            {
            //                buttonConfirm.Enabled = false;
            //                buttonConfirm.Text = "经理确认";
            //                buttonConfirm.Visible = true;
            //            }
            //            else
            //            {

            //            }

            //        }
            //        else
            //        {
            //            buttonConfirm.Enabled = true;
            //            buttonConfirm.Text = "经理确认";
            //            buttonConfirm.Visible = true;
            //        }
            //        break;

            //    default:
            //        break;

            //}
            #endregion

        }
        private string getInputArea()
        {
            string inputArea1 = "inputArea = 2";
            string inputArea2 = " inputArea = 1";
            string produceArea = DropDownListArea.SelectedValue == "品检不良品" ? "品检不良品" : "工程内不良品";
            string inputArea = DropDownListArea.SelectedValue == "品检不良品" ? inputArea1 : inputArea2;
            string timeNow = DateTime.Now.ToString("d");
            return inputArea;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string directorConfirm = "directorConfirm";
            string inputArea = getInputArea();
            string timeNow = DateTime.Now.ToString("d");
            string sql_Perpdate = @"update shatter_Parts set {0} = '" + timeNow
                                   + "' where ( {1}) and (tableSign is null)";
            string sql = string.Format(sql_Perpdate, directorConfirm, inputArea);
            ucd.ExecuteNonQuery(sql);
            getData();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string PMCConfirm = "PMCConfirm";
            string inputArea = getInputArea();
            string timeNow = DateTime.Now.ToString("d");
            string sql_Perpdate = @"update shatter_Parts set {0} = '" + timeNow
                                   + "' where ( {1}) and (tableSign is null)";
            string sql = string.Format(sql_Perpdate, PMCConfirm, inputArea);
            ucd.ExecuteNonQuery(sql);
            getData();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string purchaseConfirm = "purchaseConfirm";
            string inputArea = getInputArea();
            string timeNow = DateTime.Now.ToString("d");
            string sql_Perpdate = @"update shatter_Parts set {0} = '" + timeNow
                                   + "' where ( {1}) and (tableSign is null)";
            string sql = string.Format(sql_Perpdate, purchaseConfirm, inputArea);
            ucd.ExecuteNonQuery(sql);
            getData();

        }
        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            string MailUser = "systemmail@sz-hudson.com";//邮箱
            string MailPwd = "Hh123456h";//邮箱密码
            string MailName = "系统通知";
            string MailHost = "smtp.exmail.qq.com";//smtp的地址

            string titlePart = string.Empty;

            var toList = new List<string>();

            //获取所需要发送邮件的邮箱
            string sql = @"select email from HUDSON_User where noticeLever % 2 = 1";
            string sql_unfinish = "select goodsName, [Shattercount] ,[moneySum] from [shatter_Parts] where tableSign is null";
            try
            {
                using (var dr = SqlHelper.ExecuteReader(ConnectionFactory.ConnectionString_hudsonwwwroot, CommandType.Text, sql))
                {
                    while (dr.Read())
                    {
                        string email = dr["email"].ToString();
                        toList.Add(email);
                    }
                    dr.Close();
                }

                int index = 0;
                var itemList = new StringBuilder("<p>需要粉碎的部品如下，请各位尽快完成审核。<p>");
                itemList.AppendLine("<table>");
                using (var dr = SqlHelper.ExecuteReader(SqlHelper.conStr, CommandType.Text, sql_unfinish))
                {
                    while (dr.Read())
                    {
                        itemList.AppendLine("<tr>");
                        if (string.IsNullOrWhiteSpace(titlePart)) { titlePart = dr.GetString(0); }
                        itemList.AppendLine(string.Format("<td>[{0}]</td><td>{1:G} PCS</td><td>{2:C3}</td>", dr["goodsName"], dr["Shattercount"], dr["moneySum"]));
                        index++;
                        itemList.AppendLine("</tr>");
                    }
                    dr.Close();
                }
                itemList.AppendLine("</table>");
                if (index == 0) { throw new Exception("无部品需要审核!"); }

                if (toList.Count == 0) { throw new Exception("无收件人"); }

                MailAddress from = new MailAddress(MailUser, MailName); //邮件的发件人  
                MailMessage mail = new MailMessage();
                mail.From = from;

                foreach (var m in toList)
                {
                    mail.To.Add(m);
                }

                //设置邮件的标题  
                mail.Subject = string.Format("[粉碎部品审核通知]部品{1}等 共{2}项", DateTime.Now.ToLongDateString(), titlePart, index);
                //设置邮件的内容  
                mail.Body = itemList.ToString();
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

                client.Send(mail);
                LabelMsg.Text = "发送邮件成功";
                LabelMsg.CssClass = "btn btn-success";
            }
            catch (Exception ex)
            {
                LabelMsg.Text = "发送邮件失败，失败原因:" + ex.Message;
                LabelMsg.CssClass = "btn btn-danger";
            }
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            string managerConfirm = "managerConfirm";
            //获取页面所传过来的金额
            string[] money = (lblMoney.InnerText).Split(':');
            double moneySum = Convert.ToDouble(money[1]);
            string inputArea = getInputArea();
            string timeNow = DateTime.Now.ToString("d");
            if (moneySum < 500)
            {
                string sql_CEOUpdate = @"declare @maxSignid int;                   
                                    select @maxSignid= ISNULL(max(tableSign)+1,1) from shatter_Parts where tableSign>0  ;
                                    update shatter_Parts
                                     set {0} =  '" + timeNow
                                   + "',tableSign = @maxSignid where {1} is null and ({2}) and tableSign is null";
                string sql = string.Format(sql_CEOUpdate, managerConfirm, managerConfirm, inputArea);
                ucd.ExecuteNonQuery(sql);
                getData();
            }
            else
            {
                string sql_Perpdate = @"update shatter_Parts set {0} = '" + timeNow
                                   + "' where ( {1}) and (tableSign is null)";
                string sql = string.Format(sql_Perpdate, managerConfirm, inputArea);
                ucd.ExecuteNonQuery(sql);
                getData();

            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            string topManagerConfirm = "topManagerConfirm";
            string inputArea = getInputArea();
            string timeNow = DateTime.Now.ToString("d");
            string sql_CEOUpdate = @"declare @maxSignid int;                   
                                    select @maxSignid= ISNULL(max(tableSign)+1,1) from shatter_Parts where tableSign>0  ;
                                    update shatter_Parts
                                     set {0} =  '" + timeNow
                                   + "',tableSign = @maxSignid where {1} is null and ({2}) and tableSign is null";
            string sql = string.Format(sql_CEOUpdate, topManagerConfirm, topManagerConfirm, inputArea);
            ucd.ExecuteNonQuery(sql);
            getData();
        }

        //        protected void buttonConfirm_Click(object sender, EventArgs e)
        //        {
        //            //如果没有获取到cookie就跳到登录页面
        //            HttpCookie Cook = Request.Cookies["cookie"];
        //            if (Cook == null)
        //            {
        //                Response.Redirect("../Index.aspx");
        //            }
        //            //获取cookie的工号
        //            string userID = Cook["id"].ToString();
        //            //获取页面所传过来的金额
        //            string[] money = (LabelMoney.Text).Split(':');
        //            double moneySum = Convert.ToDouble(money[1]);
        //            //获取当前表的录入来源
        //            string inputArea1 = "inputArea = 2";
        //            string inputArea2 = " inputArea = 1";
        //            string produceArea = DropDownListArea.SelectedValue == "品检不良品" ? "品检不良品" : "工程内不良品";
        //            string inputArea = DropDownListArea.SelectedValue == "品检不良品" ? inputArea1 : inputArea2;
        //            //获取审核和确认时插入表中的名字和时间
        //            string sqlName = "select name from shatter_Parts_Password where userid= '" + userID + "'";
        //            string name =  Convert.ToString(cmd.ExecuteScalar(sqlName));
        //            string timeNow = DateTime.Now.ToString("d");
        //            name += timeNow;

        //            string sql_CEOUpdate = @"declare @maxSignid int;                   
        //                             select @maxSignid= ISNULL(max(tableSign)+1,1) from shatter_Parts where tableSign>0  ;
        //                             update shatter_Parts
        //                             set {0} =  '" + name
        //                                   + "',tableSign = @maxSignid where {1} is null and ({2}) and tableSign is null";

        //            string sql_Perpdate = @"update shatter_Parts set {0} = '" + name
        //                                   + "' where ( {1}) and (tableSign is null)";



        //                    string sqlrole = "select department from shatter_Parts_Password where userID = '" + userID + "'";
        //                    string role = (string)cmd.ExecuteScalar(sqlrole);
        //                    string sql = string.Empty;                   

        //                    switch (role)
        //                    {
        //                        case "topManagerConfirm":
        //                            sql = string.Format(sql_CEOUpdate, role, role, inputArea);
        //                            break;
        //                        case "managerConfirm":

        //                            if(moneySum>500)
        //                            {
        //                                sql = string.Format(sql_Perpdate, role, inputArea);
        //                            }
        //                            else
        //                            {

        //                                sql = string.Format(sql_CEOUpdate, role,role, inputArea);
        //                            }
        //                            break;

        //                        case "purchaseConfirm":
        //                        case "PMCConfirm":
        //                        case "directorConfirm":
        //                             sql = string.Format(sql_Perpdate, role, inputArea);
        //                            break;

        //                        default:
        //                            break;
        //                    }
        //                    if (sql != null)
        //                    {
        //                        SqlHelper.ExecuteNonQuery(SqlHelper.conStr, System.Data.CommandType.Text, sql);
        //                        Response.Redirect("index.aspx");

        //                    }
        //                    else
        //                    {
        //                        Response.Redirect("index.aspx");
        //                    }

        //        }


    }
}