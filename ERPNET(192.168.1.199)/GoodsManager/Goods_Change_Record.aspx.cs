using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using System.Drawing;
using System.Configuration;

namespace ERPPlugIn.GoodsManager
{
    public partial class Goods_Change_Record : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConXG_ZhuSu"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Response.Write("<script>alert('" + Server.UrlDecode(Request.QueryString["ID"].ToString()) + "')</script>");
            }
        }
        protected List<goods> getConfirmGoodsList(string goodsName)
        {
            List<goods> gList = new List<goods>();
            SelectCommandBuilder s = new SelectCommandBuilder(constr, "");
            string sql = "select goods_Id, (RTRIM(goods_name) + isnull(Version,'')) as goods_name,mjh,goods_ename,dw,qs,Materail_Number, Materail_Name,ys, Materail_Model, Materail_Vender_Color,Materail_Color,cpdz,skdz, Drying_Temperature, Drying_Time,sk_scale,cxzq,khdm = (select customer_name from customer where customer_id = khdm),remark,Fire_Retardant_Grade,Buyer,Toner_Model,Toner_Buyer,Rohs_Certification,Aircraft,Model_Abrasives from goods_chage_record where goods_name = '" + goodsName + "'";
            DataTable d = s.ExecuteDataTable(sql);
            if (d != null && d.Rows.Count != 0)
            {
                for (int i = 0; i < d.Rows.Count; i++)
                {
                    goods g = new goods()
                    {
                        goodsId = d.Rows[i]["goods_Id"].ToString().Trim(),
                        goods_name = d.Rows[i]["goods_name"].ToString().Trim(),
                        mjh = d.Rows[i]["mjh"].ToString().Trim(),
                        goods_ename = d.Rows[i]["goods_ename"].ToString().Trim(),
                        Materail_Number = d.Rows[i]["Materail_Number"].ToString().Trim(),
                        Materail_Name = d.Rows[i]["Materail_Name"].ToString().Trim(),
                        Materail_Model = d.Rows[i]["Materail_Model"].ToString().Trim(),
                        ys = d.Rows[i]["ys"].ToString().Trim(),
                        Materail_Vender_Color = d.Rows[i]["Materail_Vender_Color"].ToString().Trim(),
                        Materail_Color = d.Rows[i]["Materail_Color"].ToString().Trim(),
                        cpdz = d.Rows[i]["cpdz"].ToString() == "" ? "" : Convert.ToDouble(d.Rows[i]["cpdz"]).ToString().Trim(),
                        skdz = d.Rows[i]["skdz"].ToString() == "" ? "" : Convert.ToDouble(d.Rows[i]["skdz"]).ToString().Trim(),
                        Drying_Temperature = d.Rows[i]["Drying_Temperature"].ToString().Trim(),
                        Drying_Time = d.Rows[i]["Drying_Time"].ToString().Trim(),
                        sk_scale = string.IsNullOrEmpty(d.Rows[i]["sk_scale"].ToString()) ? "" : Convert.ToDouble(d.Rows[i]["sk_scale"]).ToString("0.##%").Trim(),
                        cxzq = string.IsNullOrEmpty(d.Rows[i]["cxzq"].ToString()) ? "" : Convert.ToDouble(d.Rows[i]["cxzq"]).ToString("0.##").Trim(),
                        qs = string.IsNullOrEmpty(d.Rows[i]["qs"].ToString()) ? "" : Convert.ToDouble(d.Rows[i]["qs"]).ToString("0.##").Trim(),
                        remark = d.Rows[i]["remark"].ToString().Trim().ToString(),
                        Fire_Retardant_Grade = d.Rows[i]["Fire_Retardant_Grade"].ToString().Trim().ToString(),
                        Buyer = d.Rows[i]["Buyer"].ToString().Trim().ToString(),
                        Toner_Model = d.Rows[i]["Toner_Model"].ToString().Trim().ToString(),
                        Toner_Buyer = d.Rows[i]["Toner_Buyer"].ToString().Trim().ToString(),
                        Rohs_Certification = d.Rows[i]["Rohs_Certification"].ToString().Trim().ToString() == "有" ? "0" : "1",
                        Aircraft = d.Rows[i]["Aircraft"].ToString().Trim().ToString(),
                        Model_Abrasives = d.Rows[i]["Model_Abrasives"].ToString().Trim().ToString(),
                        dw = d.Rows[i]["dw"].ToString().Trim().ToString(),
                        khdm = d.Rows[i]["khdm"].ToString().Trim().ToString()
                    };
                    gList.Add(g);
                }
            }
            return gList;

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvData.DataSource = getConfirmGoodsList(txtgoodsName.Text.Trim());
            gvData.DataBind();
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (gvData.Rows.Count > 1)
            {
                for (int i = 0; i < gvData.Rows.Count - 1; i++)
                {
                    for (int j = 1; j < gvData.Rows[i].Cells.Count - 1; j++)
                    {
                        if ((gvData.Rows[i].Cells[j].FindControl("Label" + j) as Label).Text.Trim().ToUpper() != (gvData.Rows[i + 1].Cells[j].FindControl("Label" + j) as Label).Text.Trim().ToUpper())
                        {
                            (gvData.Rows[i + 1].Cells[j].FindControl("Label" + j) as Label).ForeColor = Color.Red;
                        }
                    }
                }
            }
        }
    }
}