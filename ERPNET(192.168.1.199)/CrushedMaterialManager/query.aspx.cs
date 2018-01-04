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
    public partial class query : System.Web.UI.Page
    {
        string sql = @"select top 20 ID,goodsName,Shattercount ,price , moneySum ,
                                badContent ,ISNULL(CONVERT(nvarchar(10),  produceTime,126),'') as produceTime,employeeName  ,
                               ISNULL(CONVERT(nvarchar(10),  inputTime,126),'') as inputTime,  produceArea ,
                                CONVERT(nvarchar(10),  topManagerConfirm,126) as topManagerConfirm,
                                CONVERT(nvarchar(10),  managerConfirm,126) as managerConfirm   ,spec,ys,cz        
                                from shatter_Parts
left join goods on goods.goods_name = shatter_Parts.goodsName
                                where tableSign is not null order by id desc";

        string sqlquery = @"select   ID,goodsName,Shattercount ,price , moneySum ,
                                badContent ,ISNULL( CONVERT(nvarchar(10),  produceTime,126),'') as produceTime,employeeName ,
                                ISNULL(CONVERT(nvarchar(10),  inputTime,126),'') as inputTime ,produceArea ,
                                CONVERT(nvarchar(10),  topManagerConfirm,126) as topManagerConfirm,
                                CONVERT(nvarchar(10),  managerConfirm,126) as managerConfirm    ,spec,ys,cz      
                                from shatter_Parts
left join goods on goods.goods_name = shatter_Parts.goodsName
                                where (tableSign is not null)";
        const string projectBad = "工程内不良品";
        const string returnBad = "品检不良品";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string[] produceArea = new string[] { projectBad, returnBad };
                DropDownListArea.DataSource = produceArea;                
                DropDownListArea.DataBind();
               

                Debug.WriteLine(sql);
                DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
                GridView1.DataSource = dt;
                GridView1.DataBind();

                string sqltableSign = @"select  distinct top 10 tableSign  from shatter_Parts
                                where tableSign is not null 
                                and(inputArea = 1) order by tableSign desc ";
                DataTable dtSign = new SelectCommandBuilder().ExecuteDataTable(sqltableSign);
                DataColumn sign = new DataColumn();
                sign.ColumnName = "Sign";
                sign.DataType = System.Type.GetType("System.String");
                dtSign.Columns.Add(sign);
                var i = 1;
                foreach (DataRow dr in dtSign.Rows)
                {
                    dr["Sign"] = string.Format("{0}", i++);
                }
                DropDownListIndex.DataSource = dtSign;
                DropDownListIndex.DataValueField = "tableSign";
                DropDownListIndex.DataTextField = "Sign";
                DropDownListIndex.DataBind();

            }
        }
        //返回按钮
        protected void ButtonBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        //查询键
        protected void ButtonQuery_Click(object sender, EventArgs e)
        {
            SelectCommandBuilder cmd = new SelectCommandBuilder();
            string goodsName = TextBoxGoodsName.Text;
            string countLess = TextBoxcountLess.Text;
            string countMore = TextBoxcountMore.Text;
            string priceLess = TextBoxPriceLess.Text;
            string priceMore = TextBoxPriceMore.Text;
            string moneyLess = TextBoxMoneyLess.Text;
            string moneyMore = TextBoxMoneyMore.Text;
            string beginDate = beginTime.Value;
            string endDate = endTime.Value;
            string inputDateBegin = inputTimeBegign.Value;
            string inputDateEnd = inputTimeEnd.Value;
            string produceField = DropDownListArea.SelectedValue;


            if (goodsName != "")
            {
                sqlquery += " and goodsName='" + goodsName + "'";
            }
            else { }
            if (countLess != "" && countMore != "")
            {
                sqlquery += " and (count between'" + countLess + "'and'" + countMore + "' )";
            }
            else { }
            if (priceLess != "" && priceMore != "")
            {
                sqlquery += " and (price between'" + priceLess + "'and '" + priceMore + "')";
            }
            else { }
            if (moneyLess != "" && moneyMore != "")
            {
                sqlquery += " and (moneySum between'" + moneyLess + "'and '" + moneyMore + "')";
            }
            else { }
            if (beginDate != "" && endDate != "")
            {
                sqlquery += " and (produceTime between '" + beginDate + "' and '" + endDate + "')";
            }
            else { }
            if (inputDateBegin != "" && inputDateEnd != "")
            {
                sqlquery += " and (inputTime between'" + inputDateBegin + "'and '" + inputDateEnd + "')";
            }

            Debug.WriteLine(sqlquery);
            DataTable dt = cmd.ExecuteDataTable(sqlquery);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void Buttonprint_Click(object sender, EventArgs e)
        {
            string tableSign = DropDownListIndex.SelectedValue;
            string Area = DropDownListArea.SelectedValue;
            Response.Redirect("print.aspx?tableSign='" + tableSign + "'&Area=" + Area + "");
        }
        //重置查询条件
        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            TextBoxGoodsName.Text = "";
            TextBoxcountLess.Text = "";
            TextBoxcountMore.Text = "";
            TextBoxPriceLess.Text = "";
            TextBoxPriceMore.Text = "";
            TextBoxMoneyLess.Text = "";
            TextBoxMoneyMore.Text = "";
            beginTime.Value = "";
            endTime.Value = "";
            inputTimeBegign.Value = "";
            inputTimeEnd.Value = "";

            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }
        //打印查询
        protected void ButtonprintQuery_Click(object sender, EventArgs e)
        {
            string inputArea1 = "inputArea = 2";
            string inputArea2 = " inputArea = 1";
            string produceField = DropDownListArea.SelectedValue;
            string tableSign = DropDownListIndex.SelectedValue;
            if (produceField.Equals(returnBad))
            {
                sqlquery += @" and(" + inputArea1 + ")";

                sqlquery += " and tableSign = '" + tableSign + "'";

                Debug.WriteLine(sqlquery);
                DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sqlquery);
                GridView1.DataSource = dt;
                GridView1.DataBind();

            }
            else
            {
                sqlquery += @"and (" + inputArea2 + ")";
                sqlquery += " and tableSign = '" + tableSign + "'";

                Debug.WriteLine(sqlquery);
                DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sqlquery);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }

        }
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string inputArea1 = "inputArea = 2";
            string inputArea2 = " inputArea = 1";
            string Area = DropDownListArea.SelectedValue;
            if (Area.Equals(returnBad))
            {
                string sqltableSign = @"select  distinct top 10 tableSign  from shatter_Parts
                                where tableSign is not null 
                                and(" + inputArea1 + ") order by tableSign desc ";
                DataTable dtSign = new SelectCommandBuilder().ExecuteDataTable(sqltableSign);
                DataColumn sign = new DataColumn();
                sign.ColumnName = "Sign";
                sign.DataType = System.Type.GetType("System.String");
                dtSign.Columns.Add(sign);
                var i = 1;
                foreach (DataRow dr in dtSign.Rows)
                {
                    dr["Sign"] = string.Format("{0}", i++);
                }
                DropDownListIndex.DataSource = dtSign;
                DropDownListIndex.DataValueField = "tableSign";
                DropDownListIndex.DataTextField = "Sign";
                DropDownListIndex.DataBind();
            }
            else
            {
                string sqltableSign = @"select  distinct top 10 tableSign  from shatter_Parts
                                where tableSign is not null 
                                and (" + inputArea2 + ") order by tableSign desc ";
                DataTable dtSign = new SelectCommandBuilder().ExecuteDataTable(sqltableSign);
                DataColumn sign = new DataColumn();
                sign.ColumnName = "Sign";
                sign.DataType = System.Type.GetType("System.String");
                dtSign.Columns.Add(sign);
                var i = 1;
                foreach (DataRow dr in dtSign.Rows)
                {
                    dr["Sign"] = string.Format("{0}", i++);
                }
                DropDownListIndex.DataSource = dtSign;
                DropDownListIndex.DataValueField = "tableSign";
                DropDownListIndex.DataTextField = "Sign";
                DropDownListIndex.DataBind();
            }
        }
    }
}