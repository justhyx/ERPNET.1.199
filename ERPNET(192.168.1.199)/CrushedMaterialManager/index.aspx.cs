using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using System.Diagnostics;
namespace ERPPlugIn.CrushedMaterialManager
{
    public partial class index : System.Web.UI.Page
    {

        SelectCommandBuilder cmd = new SelectCommandBuilder();
        UpdateCommandBuilder ucd = new UpdateCommandBuilder();
        string sqlquery = @"select ID,goodsName,count ,price , moneySum ,ISNULL(CONVERT(nvarchar(10),  inputTime,126),'') as inputTime,
                            badContent ,ISNULL( CONVERT(nvarchar(10),  produceTime,126),'') as produceTime,employeeName ,spec,produceArea,                                                       
                            CONVERT(nvarchar(10),  purchaseConfirm,126) as purchaseConfirm,
                            CONVERT(nvarchar(10),  PMCConfirm,126) as PMCConfirm,
                            CONVERT(nvarchar(10), directorConfirm,126) as directorConfirm
                            from shatter_Parts 
                            left join goods on goods.goods_name = shatter_Parts.goodsName
                            where topManagerConfirm is null and managerConfirm is null
                            and ({0})";
        string sqlmoney = @"select sum(moneySum) from shatter_Parts where topManagerConfirm is null
                        and managerConfirm is null and ({0}) ";
        string sqlbutton = @"select goodsName from shatter_Parts where (purchaseConfirm is  null
                                              or PMCConfirm is  null 
                                              or directorConfirm is  null) 
                          and (topManagerConfirm is null and managerConfirm is null) 
                            and ({0})";
        string Area1 = "produceArea='客户退货区' or produceArea ='全检不良品' or produceArea='选别不良品'";
        string Area2 = "produceArea='G1' or produceArea ='G2' or produceArea='G3' or produceArea='丝印区'";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Area = @"produceArea='客户退货区' or produceArea ='全检不良品'
                                            or produceArea='选别不良品'";
                //绑定粉碎部品数据表
                string sqlselect = string.Format(sqlquery, Area);
                Debug.WriteLine(sqlselect);
                DataTable dt = new DataTable();
                dt = cmd.ExecuteDataTable(sqlselect);
                GridView1.DataSource = dt;
                GridView1.DataBind();

                string[] produceArea = new string[] { "客户退货不良品", "工程内不良品" };
                DropDownListArea.DataSource = produceArea;
                DropDownListArea.DataBind();
                //根据金额大小判断由谁来审核
                string sqlmoney1 = string.Format(sqlmoney, Area);
                Debug.WriteLine(sqlmoney1);
                double moneySum = 0;

                if (!(DBNull.Value.Equals(cmd.ExecuteScalar(sqlmoney1))))
                {
                    moneySum = Convert.ToDouble(cmd.ExecuteScalar(sqlmoney1));
                }
                LabelMoney.Text = string.Format("总金额:{0}", moneySum);

                string sqlbutton1 = string.Format(sqlbutton, Area);
                Debug.WriteLine(sqlbutton1);
                Debug.WriteLine(cmd.ExecuteScalar(sqlbutton1));
                if (moneySum < 200)
                {
                    if (Convert.ToString(cmd.ExecuteScalar(sqlbutton1)) == "")
                    {

                        ButtonManager.Visible = true;
                    }
                }
                else if (Convert.ToString(cmd.ExecuteScalar(sqlbutton1)) == "")
                {
                    ButtonCEO.Visible = true;
                }

            }
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
                    txtlbl.Text = drv.Row[t.Value].ToString();
                }

            }

        }


        //承认点击
        protected void ButtonCEO_Click(object sender, EventArgs e)
        {
            //1代表总经理，2代表经理
            string manager = "CEO";
            string produceArea = DropDownListArea.SelectedValue;

            Response.Redirect("getSure.aspx?manager=" + manager + "&produceArea=" + produceArea + "");
        }
        protected void Buttonmanager_Click(object sender, EventArgs e)
        {
            string manager = "manager";
            string produceArea = DropDownListArea.SelectedValue;
            Response.Redirect("getSure.aspx?manager=" + manager + "&produceArea=" + produceArea + "");
        }

        protected void DropDownListArea_SelectedIndexChanged(object sender, EventArgs e)
        {

            ButtonCEO.Visible = false;
            ButtonManager.Visible = false;
            string produceArea = DropDownListArea.SelectedValue == "客户退货不良品" ? "客户退货不良品" : "工程内不良品";

            string Area = DropDownListArea.SelectedValue == "客户退货不良品" ? Area1 : Area2;
            string sqlselect = string.Format(sqlquery, Area);

            Debug.WriteLine(sqlselect);
            DataTable dt = new DataTable();
            dt = cmd.ExecuteDataTable(sqlselect);
            GridView1.DataSource = dt;
            GridView1.DataBind();

            //根据金额大小判断由谁来审核
            string sqlmoney1 = string.Format(sqlmoney, Area);
            Debug.WriteLine(sqlmoney1);
            double moneySum = 0;

            if (!(DBNull.Value.Equals(cmd.ExecuteScalar(sqlmoney1))))
            {
                moneySum = Convert.ToDouble(cmd.ExecuteScalar(sqlmoney1));
            }
            LabelMoney.Text = string.Format("总金额:{0}", moneySum);
            string sqlbutton1 = string.Format(sqlbutton, Area);
            if (moneySum < 200)
            {
                if (Convert.ToString(cmd.ExecuteScalar(sqlbutton1)) == "")
                {

                    ButtonManager.Visible = true;
                }
            }
            else if (Convert.ToString(cmd.ExecuteScalar(sqlbutton1)) == "")
            {
                ButtonCEO.Visible = true;
            }



        }


        static string getAdmit(string people, string Area)
        {
            return string.Format(@"update shatter_Parts set {0} = getDate()
                          where ({1}) and (topManagerConfirm is null and managerConfirm is null)"
                            , people, Area);
        }

        //采购确认
        protected void ButtonPurchase_Click(object sender, EventArgs e)
        {
            string produceArea = DropDownListArea.SelectedValue;
            string Area = produceArea == "客户退货不良品" ? Area1 : Area2;
            string people = "purchaseConfirm";
            string sql = getAdmit(people, Area);

            ucd.ExecuteNonQuery(sql);
            string sqlselect = string.Format(sqlquery, Area);
            Debug.WriteLine(sqlselect);
            DataTable dt = new DataTable();
            dt = cmd.ExecuteDataTable(sqlselect);
            GridView1.DataSource = dt;
            GridView1.DataBind();


        }
        //生管确认
        protected void ButtonPMC_Click(object sender, EventArgs e)
        {
            string produceArea = DropDownListArea.SelectedValue;
            string Area = produceArea == "客户退货不良品" ? Area1 : Area2;
            string people = "PMCConfirm";
            string sql = getAdmit(people, Area);


            ucd.ExecuteNonQuery(sql);
            string sqlselect = string.Format(sqlquery, Area);
            Debug.WriteLine(sqlselect);
            DataTable dt = new DataTable();
            dt = cmd.ExecuteDataTable(sqlselect);
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }
        //主管确认
        protected void Buttondirector_Click(object sender, EventArgs e)
        {
            string produceArea = DropDownListArea.SelectedValue;
            string Area = produceArea == "客户退货不良品" ? Area1 : Area2;
            string people = "directorConfirm";
            string sql = getAdmit(people, Area);


            ucd.ExecuteNonQuery(sql);
            string sqlselect = string.Format(sqlquery, Area);
            Debug.WriteLine(sqlselect);
            DataTable dt = new DataTable();
            dt = cmd.ExecuteDataTable(sqlselect);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}