using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using ERPPlugIn.Class;
using System.Configuration;

namespace ERPPlugIn.StockInManager
{
    public partial class StockIn_AddData : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConXG_ZhuSu"].ConnectionString;
        public static List<inputData> insList = new List<inputData>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                StockIn_Create a = (StockIn_Create)Context.Handler;
                lblNo.Text = a.No;
                ViewState["sql"] = a.sql;
                ViewState["str_in_bill_id"] = a.bill_id;
                insList.Clear();
                goods_name.Focus();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string msg = inputValidate();
            if (msg != "")
            {
                Response.Write("<script>alert('" + msg + "')</script>");
                return;
            }
            SelectCommandBuilder select = new SelectCommandBuilder(constr, "goods_tmp");
            select.SelectColumn("Count(*)");
            select.ConditionsColumn("goods_name", goods_name.Value.Trim().ToUpper());
            select.getSelectCommand();
            int count = Convert.ToInt32(select.ExecuteScalar());
            if (count == 0)
            {
                Response.Write("<script>alert('部番不存在')</script>");
                return;
            }
            inputData indata = new inputData()
            {
                goods_name = goods_name.Value.Trim().ToUpper(),
                Qty = int.Parse(qty.Value.Trim()),
                unit = "pcs",
                goodsPost = hwh.Value.Trim().ToUpper(),
                batch = pch.Value.Trim().ToUpper()
            };
            insList.Add(indata);
            gvData.DataSource = insList;
            gvData.DataBind();
            clearText();

        }
        public void clearText()
        {
            goods_name.Value = string.Empty;
            qty.Value = string.Empty;
            hwh.Value = string.Empty;
            pch.Value = string.Empty;
            goods_name.Focus();
        }

        protected void gvData_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvData.EditIndex = e.NewEditIndex;
            gvData.DataSource = insList;
            gvData.DataBind();
        }

        protected void gvData_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvData.EditIndex = -1;
            gvData.DataSource = insList;
            gvData.DataBind();
        }

        protected void gvData_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            insList[e.RowIndex].Qty = int.Parse((gvData.Rows[e.RowIndex].Cells[2].FindControl("qty") as TextBox).Text);
            insList[e.RowIndex].batch = (gvData.Rows[e.RowIndex].Cells[4].FindControl("batch") as TextBox).Text;
            insList[e.RowIndex].goodsPost = (gvData.Rows[e.RowIndex].Cells[5].FindControl("goodsPost") as TextBox).Text;
            gvData.EditIndex = -1;
            gvData.DataSource = insList;
            gvData.DataBind();
        }

        protected void gvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            insList.RemoveAt(e.RowIndex);
            gvData.DataSource = insList;
            gvData.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (insList.Count == 0)
            {
                Response.Write("<script>alert('无数据保存,请先添加数据')</script>");
                return;
            }
            List<string> sqlList = new List<string>();
            sqlList.Add(ViewState["sql"].ToString());
            DateTime date = DateTime.Now;
            string IdLeftPart = "PH" + date.Year.ToString().Substring(2, 2) + date.Month.ToString().PadLeft(2, '0') + date.Day.ToString().PadLeft(2, '0');
            for (int i = 0; i < insList.Count; i++)
            {
                string Id = CommadMethod.getNextId(IdLeftPart, "");
                InsertCommandBuilder ins = new InsertCommandBuilder(constr, "pre_str_in_bill_detail");
                ins.InsertColumn("str_in_bill_id", ViewState["str_in_bill_id"].ToString());
                ins.InsertColumn("batch_id", Id);
                ins.InsertColumn("goods_id", new SelectCommandBuilder(constr, "").ExecuteDataTable("select goods_id from goods_tmp where goods_name = '" + insList[i].goods_name + "'").Rows[0][0].ToString().Trim());
                ins.InsertColumn("qty", insList[i].Qty.ToString());
                ins.InsertColumn("pch", insList[i].batch);
                ins.InsertColumn("can_sale", "Y");
                ins.InsertColumn("hwh", insList[i].goodsPost);
                sqlList.Add(ins.getInsertCommand());
            }
            int count = new InsertCommandBuilder(constr, "").ExcutTransaction(sqlList);
            if (count != 0)
            {
                Response.Write("<script>alert('保存成功')</script>");
                insList.Clear();
                Response.Redirect("StockIn_SaveOk.aspx");
            }
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    ((LinkButton)e.Row.Cells[7].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除：\"" + (e.Row.Cells[1].FindControl("Label2") as Label).Text + "\"吗?')");
                }
            }

        }
        public string inputValidate()
        {
            string msg = "";
            if (string.IsNullOrEmpty(goods_name.Value))
            {
                goods_name.Focus();
                msg = "请输入部番";
            }
            else if (string.IsNullOrEmpty(qty.Value))
            {
                qty.Focus();
                msg = "请输入数量";
            }
            else if (string.IsNullOrEmpty(hwh.Value))
            {
                hwh.Focus();
                msg = "请输入货位号";
            }
            else if (string.IsNullOrEmpty(pch.Value))
            {
                pch.Focus();
                msg = "请输入批次号";
            }
            return msg;

        }
    }
    public class inputData
    {
        public string goods_name { get; set; }
        public int Qty { get; set; }
        public string unit { get; set; }
        public string batch { get; set; }
        public string goodsPost { get; set; }
    }
}