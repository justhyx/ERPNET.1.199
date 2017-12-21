using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Configuration;
using System.Data;
using System.Drawing;

namespace ERPPlugIn.GoodsManager
{
    public partial class Goods_Aprove : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConXG_ZhuSu"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                dgvList.DataSource = getConfirmGoodsList();
                dgvList.DataBind();
                dgvList.SelectedIndex = 0;
                //if (1 == 1)
                //{
                //    btnConfirm.Enabled = false;
                //}
            }
        }
        protected List<goods> getConfirmGoodsList()
        {
            List<goods> gList = new List<goods>();
            SelectCommandBuilder s = new SelectCommandBuilder(constr, "goods_tmp");
            string sql = "select goods_Id, (RTRIM(goods_name) + isnull(Version,'')) as goods_name,mjh,goods_ename,dw,qs,Materail_Number, Materail_Name,ys, Materail_Model, Materail_Vender_Color,Materail_Color,cpdz,skdz, Drying_Temperature, Drying_Time,sk_scale,cxzq,khdm = (select customer_name from customer where customer_id = khdm),remark,Fire_Retardant_Grade,Buyer,Toner_Model,Toner_Buyer,Rohs_Certification,Aircraft,Model_Abrasives from goods_tran where isConfirm = 'Waiting'";
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
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            List<goods> List = new List<goods>();
            List<string> sqlList = new List<string>();
            if (dgvList.Rows.Count != 0)
            {
                for (int i = 0; i < dgvList.Rows.Count; i++)
                {
                    if ((dgvList.Rows[i].Cells[0].FindControl("cboCheckItem") as CheckBox).Checked == true)
                    {
                        goods g = new goods()
                        {
                            goodsId = (dgvList.Rows[i].Cells[0].FindControl("HfId") as HiddenField).Value,
                            goods_name = (dgvList.Rows[i].Cells[2].FindControl("Label0") as Label).Text,
                            mjh = (dgvList.Rows[i].Cells[3].FindControl("Label1") as Label).Text,
                            goods_ename = (dgvList.Rows[i].Cells[4].FindControl("Label2") as Label).Text,
                            Aircraft = (dgvList.Rows[i].Cells[5].FindControl("Label3") as Label).Text,
                            Materail_Number = (dgvList.Rows[i].Cells[6].FindControl("Label4") as Label).Text,
                            Materail_Name = (dgvList.Rows[i].Cells[7].FindControl("Label5") as Label).Text,
                            Materail_Model = (dgvList.Rows[i].Cells[8].FindControl("Label6") as Label).Text,
                            ys = (dgvList.Rows[i].Cells[9].FindControl("Label7") as Label).Text,
                            Materail_Vender_Color = (dgvList.Rows[i].Cells[10].FindControl("Label8") as Label).Text,
                            Materail_Color = (dgvList.Rows[i].Cells[11].FindControl("Label9") as Label).Text,
                            cpdz = (dgvList.Rows[i].Cells[12].FindControl("Label10") as Label).Text,
                            skdz = (dgvList.Rows[i].Cells[13].FindControl("Label11") as Label).Text,
                            Drying_Temperature = (dgvList.Rows[i].Cells[14].FindControl("Label12") as Label).Text,
                            Drying_Time = (dgvList.Rows[i].Cells[15].FindControl("Label13") as Label).Text,
                            sk_scale = (dgvList.Rows[i].Cells[16].FindControl("Label14") as Label).Text,
                            Fire_Retardant_Grade = (dgvList.Rows[i].Cells[17].FindControl("Label15") as Label).Text,
                            Buyer = (dgvList.Rows[i].Cells[18].FindControl("Label16") as Label).Text,
                            cxzq = (dgvList.Rows[i].Cells[19].FindControl("Label17") as Label).Text,
                            Toner_Model = (dgvList.Rows[i].Cells[20].FindControl("Label18") as Label).Text,
                            Toner_Buyer = (dgvList.Rows[i].Cells[21].FindControl("Label19") as Label).Text,
                            qs = (dgvList.Rows[i].Cells[22].FindControl("Label20") as Label).Text,
                            dw = (dgvList.Rows[i].Cells[23].FindControl("Label21") as Label).Text,
                            khdm = (dgvList.Rows[i].Cells[24].FindControl("Label22") as Label).Text,
                            Rohs_Certification = (dgvList.Rows[i].Cells[25].FindControl("Label23") as Label).Text,
                            Model_Abrasives = (dgvList.Rows[i].Cells[26].FindControl("Label24") as Label).Text,
                            remark = (dgvList.Rows[i].Cells[27].FindControl("Label25") as Label).Text
                        };
                        List.Add(g);
                    }
                }
            }
            if (List.Count == 0)
            {
                Response.Write("<script>alert('没有选择任何行')</script>");
                return;
            }
            for (int i = 0; i < List.Count; i++)
            {
                UpdateCommandBuilder up = new UpdateCommandBuilder(constr, "goods_tran");
                up.UpdateColumn("isConfirm", "Approved");
                up.ConditionsColumn("goods_id", List[i].goodsId);
                sqlList.Add(up.getUpdateCommand());
            }
            InsertCommandBuilder ins = new InsertCommandBuilder(constr, "");
            int count = ins.ExcutTransaction(sqlList);
            //int count = new InsertCommandBuilder().ExcutTransaction(sqlList);
            if (count != 0)
            {
                Response.Write("<script>alert('审核成功!')</script>");
                dgvList.DataSource = getConfirmGoodsList();
                dgvList.DataBind();
                gvData.DataSource = null;
                gvData.DataBind();
            }
        }

        protected void dgvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //----------鼠标移动到每项时颜色交替效果  

                //e.Row.Attributes["onmouseover"] = "e=this.style.backgroundColor;this.style.backgroundColor='#FFFF99'";
                //e.Row.Attributes["onmouseout"] = "this.style.backgroundColor=e";
                //----------设置悬浮鼠标指针形状为小手"  
                e.Row.Attributes["style"] = "Cursor:hand";
                String evt = Page.ClientScript.GetPostBackClientHyperlink(sender as GridView, "Select$" + e.Row.RowIndex.ToString()); e.Row.Attributes.Add("onclick", evt);
            }
        }
        protected List<goods> getConfirmGoodsList(string goodsId)
        {
            List<goods> gList = new List<goods>();
            SelectCommandBuilder s = new SelectCommandBuilder(constr, "");
            string sql = "select goods_Id, (RTRIM(goods_name) + isnull(Version,'')) as goods_name,mjh,goods_ename,dw,qs,Materail_Number, Materail_Name,ys, Materail_Model, Materail_Vender_Color,Materail_Color,cpdz,skdz, Drying_Temperature, Drying_Time,sk_scale,cxzq,khdm = (select customer_name from customer where customer_id = khdm),remark,Fire_Retardant_Grade,Buyer,Toner_Model,Toner_Buyer,Rohs_Certification,Aircraft,Model_Abrasives from goods_chage_record where goods_name =(Select top 1 goods_name from goods_chage_record where goods_id= '" + goodsId + "')";
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

        protected void dgvList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvList.SelectedIndex != -1)
            {
                gvData.DataSource = getConfirmGoodsList((dgvList.SelectedRow.Cells[0].FindControl("HfId") as HiddenField).Value.Trim());
                gvData.DataBind();
            }
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