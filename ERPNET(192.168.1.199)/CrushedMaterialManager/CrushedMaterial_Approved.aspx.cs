using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data.SqlClient;
using System.Configuration;
using ERPPlugIn.Class;

namespace ERPPlugIn.CrushedMaterialManager
{
    public partial class CrushedMaterial_Approved : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                ViewState["status"] = Request.QueryString["status"];
                ViewState["area"] = Request.QueryString["area"];
                if (string.IsNullOrEmpty(Request.QueryString["status"]))
                {
                    ViewState["status"] = "";
                }
                else
                {
                    ddlDemp.SelectedIndex = Convert.ToInt32(ViewState["area"]);
                    ddlDemp.Enabled = false;
                }
                if (string.IsNullOrEmpty(Request.QueryString["status"]))
                {
                    dgvList.DataSource = getAllCrushedMaterial();
                }
                else
                {
                    dgvList.DataSource = getAllCrushedMaterial(ViewState["area"].ToString(), ViewState["status"].ToString());
                }

                dgvList.DataBind();
            }
        }
        public List<CrushedMaterialDetail> getAllCrushedMaterial()
        {
            List<CrushedMaterialDetail> cList = new List<CrushedMaterialDetail>();
            SelectCommandBuilder s = new SelectCommandBuilder(ConnectionFactory.ConnectionString_hudsonwwwroot, "trymolde");
            string sql = "SELECT id, 品番, 材料, 材料编号, 模具担当, addtime, area1style, area2style, area3style, area4style FROM trymolde WHERE (PO处理 = '粉碎') AND (isApprove = 'OK') AND (area1style <> 'Done')OR (area2style <> 'Done')OR (area3style <> 'Done')OR (area4style <> 'Done') ORDER BY addtime";
            SqlDataReader dr = s.ExecuteReader(sql);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    CrushedMaterialDetail c = new CrushedMaterialDetail()
                    {
                        id = dr.GetInt32(0),
                        Name = dr.GetString(1).Trim(),
                        Material = dr.GetString(2).Trim(),
                        MaterialNo = dr.GetString(3).Trim(),
                        JigLeader = dr.GetString(4).Trim(),
                        addtime = dr.GetString(5).Trim(),
                        area1style = dr.IsDBNull(6) ? "待粉碎" : getStatusdeng(dr.GetString(6)),
                        area2style = dr.IsDBNull(7) ? "待粉碎" : getStatusdeng(dr.GetString(7)),
                        area3style = dr.IsDBNull(8) ? "待粉碎" : getStatusdeng(dr.GetString(8)),
                        area4style = dr.IsDBNull(9) ? "待粉碎" : getStatusdeng(dr.GetString(9))
                    };
                    cList.Add(c);
                }
            }
            return cList;
        }
        public List<CrushedMaterialDetail> getAllCrushedMaterial(string area, string status)
        {
            List<CrushedMaterialDetail> cList = new List<CrushedMaterialDetail>();
            SelectCommandBuilder s = new SelectCommandBuilder(ConnectionFactory.ConnectionString_hudsonwwwroot, "trymolde");
            string sql = "SELECT id, 品番, 材料, 材料编号, 模具担当, addtime, area1style, area2style, area3style, area4style FROM trymolde WHERE (PO处理 = '粉碎') AND (isApprove = 'OK') AND (area" + area + "style = '" + status + "') ORDER BY addtime";
            SqlDataReader dr = s.ExecuteReader(sql);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    CrushedMaterialDetail c = new CrushedMaterialDetail()
                    {
                        id = dr.GetInt32(0),
                        Name = dr.GetString(1),
                        Material = dr.GetString(2),
                        MaterialNo = dr.GetString(3),
                        JigLeader = dr.GetString(4),
                        addtime = dr.GetString(5),
                        area1style = dr.IsDBNull(6) ? "待粉碎" : getStatusdeng(dr.GetString(6)),
                        area2style = dr.IsDBNull(7) ? "待粉碎" : getStatusdeng(dr.GetString(7)),
                        area3style = dr.IsDBNull(8) ? "待粉碎" : getStatusdeng(dr.GetString(8)),
                        area4style = dr.IsDBNull(9) ? "待粉碎" : getStatusdeng(dr.GetString(9))
                    };
                    cList.Add(c);
                }
            }
            return cList;
        }
        protected string getStatusdeng(string status)
        {
            string s = "";
            if (status == "Wait" || string.IsNullOrEmpty(status))
            {
                s = "待粉碎";
            }
            else if (status == "Running")
            {
                s = "粉碎中";
            }
            else
            {
                string[] a = status.Split('(');
                s = a.Length > 1 ? "已完成" + "(" + a[1] : "已完成";
            }
            return s;
        }
        protected void dgvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvList.PageIndex = e.NewPageIndex;
            dgvList.DataSource = getAllCrushedMaterial();
            dgvList.DataBind();
        }
        protected void dgvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //----------鼠标移动到每项时颜色交替效果  
                e.Row.Attributes["onmouseover"] = "e=this.style.backgroundColor;this.style.backgroundColor='#FFFF99'";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor=e";
                //----------设置悬浮鼠标指针形状为小手"  
                e.Row.Attributes["style"] = "Cursor:hand";
                //String evt = Page.ClientScript.GetPostBackClientHyperlink(sender as GridView, "Select$" + e.Row.RowIndex.ToString()); e.Row.Attributes.Add("onclick", evt);
            }
        }
        protected void dgvList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CheckBox cb = (dgvList.SelectedRow.Cells[0].FindControl("cboCheckItem") as CheckBox);
            //if (cb.Checked == true)
            //{
            //    cb.Checked = false;
            //}
            //else
            //{
            //    cb.Checked = true;
            //}
        }

        protected void btnBegin_Click(object sender, EventArgs e)
        {
            if (ddlDemp.SelectedItem.Value == "0")
            {
                Response.Write("<script>alert('请选择处理区域!')</script>");
                return;
            }
            else
            {
                ViewState["area"] = ddlDemp.SelectedItem.Value;
            }
            List<CrushedMaterialDetail> cmList = new List<CrushedMaterialDetail>();
            List<string> sqlList = new List<string>();
            if (dgvList.Rows.Count != 0)
            {
                for (int i = 0; i < dgvList.Rows.Count; i++)
                {
                    if ((dgvList.Rows[i].Cells[0].FindControl("cboCheckItem") as CheckBox).Checked == true)
                    {
                        CrushedMaterialDetail c = new CrushedMaterialDetail()
                        {
                            id = int.Parse((dgvList.Rows[i].Cells[0].FindControl("Hfid") as HiddenField).Value),
                            Name = (dgvList.Rows[i].Cells[1].FindControl("Label1") as Label).Text,
                            MaterialNo = (dgvList.Rows[i].Cells[2].FindControl("Label2") as Label).Text,
                            Material = (dgvList.Rows[i].Cells[3].FindControl("Label3") as Label).Text,
                            JigLeader = (dgvList.Rows[i].Cells[4].FindControl("Label4") as Label).Text,
                            addtime = (dgvList.Rows[i].Cells[5].FindControl("Label5") as Label).Text,
                            area1style = (dgvList.Rows[i].Cells[6].FindControl("Label6") as Label).Text,
                            area2style = (dgvList.Rows[i].Cells[7].FindControl("Label7") as Label).Text,
                            area3style = (dgvList.Rows[i].Cells[8].FindControl("Label8") as Label).Text,
                            area4style = (dgvList.Rows[i].Cells[9].FindControl("Label9") as Label).Text
                        };
                        cmList.Add(c);
                    }
                }
                if (cmList.Count == 0)
                {
                    Response.Write("<script>alert('没有选择任何行!')</script>");
                    return;
                }
                for (int i = 0; i < cmList.Count; i++)
                {
                    UpdateCommandBuilder up = new UpdateCommandBuilder(ConnectionFactory.ConnectionString_hudsonwwwroot, "trymolde");
                    string status = "";
                    switch (ViewState["area"].ToString())
                    {
                        case "1"://
                            up.UpdateColumn("area1style", "Running");
                            status = cmList[i].area1style;
                            break;
                        case "2":
                            up.UpdateColumn("area2style", "Running");
                            status = cmList[i].area2style;
                            break;
                        case "3":
                            up.UpdateColumn("area3style", "Running");
                            status = cmList[i].area3style;
                            break;
                        case "4":
                            up.UpdateColumn("area4style", "Running");
                            status = cmList[i].area4style;
                            break;
                        default:
                            break;
                    }
                    up.ConditionsColumn("id", cmList[i].id);
                    sqlList.Add(up.getUpdateCommand());
                    if (status != "待粉碎")
                    {
                        Response.Write("<script>alert('选择行中数据有误，请选择<待粉碎>的部番!')</script>");
                        return;
                    }
                }
                InsertCommandBuilder ins = new InsertCommandBuilder(ConnectionFactory.ConnectionString_hudsonwwwroot, "trymolde");
                int count = ins.ExcutTransaction(sqlList);
                if (count != 0)
                {
                    dgvList.DataSource = getAllCrushedMaterial();
                    dgvList.DataBind();
                }

            }
        }

        protected void btnDone_Click(object sender, EventArgs e)
        {
            if (ddlDemp.SelectedItem.Value == "0")
            {
                Response.Write("<script>alert('请选择处理区域!')</script>");
                return;
            }
            else
            {
                ViewState["area"] = ddlDemp.SelectedItem.Value;
            }
            List<CrushedMaterialDetail> cmList = new List<CrushedMaterialDetail>();
            List<string> sqlList = new List<string>();
            if (dgvList.Rows.Count != 0)
            {
                for (int i = 0; i < dgvList.Rows.Count; i++)
                {
                    if ((dgvList.Rows[i].Cells[0].FindControl("cboCheckItem") as CheckBox).Checked == true)
                    {
                        CrushedMaterialDetail c = new CrushedMaterialDetail()
                        {
                            id = int.Parse((dgvList.Rows[i].Cells[0].FindControl("Hfid") as HiddenField).Value),
                            Name = (dgvList.Rows[i].Cells[1].FindControl("Label1") as Label).Text,
                            MaterialNo = (dgvList.Rows[i].Cells[2].FindControl("Label2") as Label).Text,
                            Material = (dgvList.Rows[i].Cells[3].FindControl("Label3") as Label).Text,
                            JigLeader = (dgvList.Rows[i].Cells[4].FindControl("Label4") as Label).Text,
                            addtime = (dgvList.Rows[i].Cells[5].FindControl("Label5") as Label).Text,
                            area1style = (dgvList.Rows[i].Cells[6].FindControl("Label6") as Label).Text,
                            area2style = (dgvList.Rows[i].Cells[7].FindControl("Label7") as Label).Text,
                            area3style = (dgvList.Rows[i].Cells[8].FindControl("Label8") as Label).Text,
                            area4style = (dgvList.Rows[i].Cells[9].FindControl("Label9") as Label).Text,
                            Qty = int.Parse((dgvList.Rows[i].Cells[10].FindControl("TextBox1") as TextBox).Text == string.Empty ? "0" : (dgvList.Rows[i].Cells[10].FindControl("TextBox1") as TextBox).Text)
                        };
                        cmList.Add(c);
                    }
                }
                if (cmList.Count == 0)
                {
                    Response.Write("<script>alert('没有选择任何行!')</script>");
                    return;
                }
                for (int i = 0; i < cmList.Count; i++)
                {
                    UpdateCommandBuilder up = new UpdateCommandBuilder(ConnectionFactory.ConnectionString_hudsonwwwroot, "trymolde");
                    string status = "";
                    switch (ViewState["area"].ToString())
                    {
                        case "1":
                            up.UpdateColumn("area1style", "Done(" + cmList[i].Qty + ")");
                            up.UpdateColumn("area1CrushedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            status = cmList[i].area1style;
                            break;
                        case "2":
                            up.UpdateColumn("area2style", "Done(" + cmList[i].Qty + ")");
                            up.UpdateColumn("area2CrushedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            status = cmList[i].area2style;
                            break;
                        case "3":
                            up.UpdateColumn("area3style", "Done(" + cmList[i].Qty + ")");
                            up.UpdateColumn("area3CrushedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            status = cmList[i].area3style;
                            break;
                        case "4":
                            up.UpdateColumn("area4style", "Done(" + cmList[i].Qty + ")");
                            up.UpdateColumn("area4CrushedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            status = cmList[i].area4style;
                            break;
                        default:
                            break;
                    }
                    up.ConditionsColumn("id", cmList[i].id);
                    sqlList.Add(up.getUpdateCommand());
                    if (status != "粉碎中")
                    {
                        Response.Write("<script>alert('选择行中数据有误，请选择<粉碎中>的部番!')</script>");
                        return;
                    }
                }
                InsertCommandBuilder ins = new InsertCommandBuilder(ConnectionFactory.ConnectionString_hudsonwwwroot, "trymolde");
                int count = ins.ExcutTransaction(sqlList);
                if (count != 0)
                {
                    dgvList.DataSource = getAllCrushedMaterial();
                    dgvList.DataBind();
                }

            }
        }
    }
    public class CrushedMaterialDetail : CrushedMaterial
    {
        public string status { get; set; }
        public string crushedStartDate { get; set; }
        public string crushedDate { get; set; }
        public string area1style { get; set; }
        public string area2style { get; set; }
        public string area3style { get; set; }
        public string area4style { get; set; }
        public int Qty { get; set; }
    }
}