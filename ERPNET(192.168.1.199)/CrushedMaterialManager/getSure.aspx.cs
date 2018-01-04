using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Diagnostics;
namespace ERPPlugIn.CrushedMaterialManager
{
    public partial class getSure : System.Web.UI.Page
    {
        public const string role_CEO = "CEO",
            role_manager = "manager",
            role_purchase = "purchase",
            role_PMC = "PMC",
            role_director = "director";

        static string mainUrl = "index.aspx";

        /// <summary>
        /// 密码列表
        /// </summary>
        Dictionary<string, string> pwds = new Dictionary<string, string>(){
       {role_CEO,"123456"},
       {role_manager,"456789"},
       {role_purchase,"111111"},
       {role_PMC,"222222"},
       {role_director,"333333"},
       };

        /// <summary>
        /// 回跳地址
        /// </summary>
        Dictionary<string, string> urls = new Dictionary<string, string>(){
       {role_CEO,"query.aspx"},
       {role_manager,"query.aspx"},
       {role_purchase,mainUrl},
       {role_PMC,mainUrl},
       {role_director,mainUrl},
       };

        Dictionary<string, string> dbfields = new Dictionary<string, string>(){
           {role_CEO,"topManagerConfirm"},
       {role_manager,"managerConfirm"},
       {role_purchase,"purchaseConfirm"},
       {role_PMC,"PMCConfirm"},
       {role_director,"directorConfirm"},
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string produceArea = Request.QueryString["produceArea"];
                string userkey = Request.QueryString["userkey"];
                if (userkey == null)
                {
                    Response.Redirect(mainUrl);
                }
                if (!pwds.ContainsKey(userkey))
                {
                    Response.Redirect(mainUrl);
                }
                else
                {
                    HiddenFieldPeople.Value = userkey;
                    HiddenFieldPwd.Value = pwds[userkey];
                    HiddenFieldurl.Value = urls[userkey];
                }
                HiddenFieldArea.Value = produceArea;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string Area1 = "produceArea='客户退货区' or produceArea ='全检不良品' or produceArea='选别不良品'";
            string Area2 = "produceArea='G1' or produceArea ='G2' or produceArea='G3' or produceArea='丝印区'";
            string sql_CEOUpdate = @"declare @maxSignid int;                   
                             select @maxSignid= ISNULL(max(tableSign)+1,1) from shatter_Parts where tableSign>0  ;
                             update shatter_Parts
                             set {0} = 1,
                             tableSign = @maxSignid
                             where {1} is null 
                             and ({2}) 
                             and tableSign is null";
            string sql_Perpdate = @"update shatter_Parts set {0} = getDate()
                          where ({1}) and (topManagerConfirm is null and managerConfirm is null)";
            string produceArea = HiddenFieldArea.Value == "客户退货不良品" ? Area1 : Area2;
            if (HiddenFieldPwd.Value.Equals(TextBoxPwd.Text.Trim()))
            {
                string userkey = Request.QueryString["userkey"];
                string sql = string.Empty;
                string rol = string.Empty;
                if (dbfields.TryGetValue(userkey, out rol))
                {
                   
                    switch (userkey)
                    {
                        case role_CEO:
                        case role_manager:
                            sql = string.Format(sql_CEOUpdate, rol, rol, produceArea);
                            break;

                        case role_purchase:
                        case role_PMC:
                        case role_director:
                            sql = string.Format(sql_Perpdate, rol, produceArea);
                            break;
                        default:
                            break;
                    }
                }
                if (string.IsNullOrWhiteSpace(sql))
                {
                    Response.Redirect(mainUrl);
                }
                else
                {
                    SqlHelper.ExecuteNonQuery(SqlHelper.conStr, System.Data.CommandType.Text, sql);
                    Response.Redirect(HiddenFieldurl.Value);
                }
            }
            else
            {
                Response.Write("<script>alert('密码不正确')</script>");
            }
        }


    }
}