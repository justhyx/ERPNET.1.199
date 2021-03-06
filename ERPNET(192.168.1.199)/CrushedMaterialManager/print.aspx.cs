﻿using System;
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
    public partial class print : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string produceArea = Request.QueryString["Area"];
            string tableSign = Request.QueryString["tableSign"];
            if (produceArea == null || tableSign == null)
            {
                Response.Redirect("query.aspx");
            }
            string inputArea1 = "inputArea = 2";
            string inputArea2 = " inputArea = 1";
            string Area = produceArea == "品检不良品" ? inputArea1 : inputArea2;
            string sqlMoney = string.Format(@"select sum(moneySum)  from shatter_Parts
                                where tableSign= " + tableSign
                                   + " and ({0}) and (topManagerConfirm is not null or managerConfirm is not null)", Area);
            string sql = string.Format(@"select goodsName as 部番,cz as 材料编号,spec as 材料材质,ys as 材料色番,
                                count as 不良数量,price as 单价, moneySum as 金额,produceArea as 不良发生区,
                                badContent as 不良内容,ISNULL( CONVERT(nvarchar(10),  produceTime,126),'') as 生产日期,employeeName as 作业员,
                                ISNULL( CONVERT(nvarchar(10),  inputTime,126),'') as 录入时间                                                               
                                from shatter_Parts 
left join goods on goods.goods_name = shatter_Parts.goodsName
                                where tableSign= " + tableSign
                                    + " and ({0}) and (topManagerConfirm is not null or managerConfirm is not null)", Area);
            string sqlCEOconfirm = string.Format(@"select goodsName from shatter_Parts where topManagerConfirm is not null and ({0})", Area);

            string goodsName = Convert.ToString(new SelectCommandBuilder().ExecuteScalar(sqlCEOconfirm));

            if (goodsName != "")
            {
                LabelAdmin.Text = "承认人：总经理";
            }
            else
            {
                LabelAdmin.Text = "承认人：经理";
            }
            var comm = new SelectCommandBuilder();
            var moneySum = comm.ExecuteScalar(sqlMoney);
            
            LabelTime.Text = DateTime.Now.ToString("yyyy-MM-dd"); 

            //double moneySum = moneySum = Convert.ToDouble();
            Debug.Write(sqlMoney);
            Debug.Write(sql);
            LabelMoney.Text = string.Format("总金额:{0}", DBNull.Value.Equals(moneySum) ? 0 : Convert.ToDouble(moneySum));
            DataTable dt = comm.ExecuteDataTable(sql);
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

        protected void ButtonBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("query.aspx");
        }
    }
}