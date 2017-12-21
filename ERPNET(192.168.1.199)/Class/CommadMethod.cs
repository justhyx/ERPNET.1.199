using System;
using System.Collections.Generic;
using System.Web;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;

namespace ERPPlugIn.Class
{
    public static class CommadMethod
    {
        public static string getNextId(string front_fix,string end_fix)
        {
            string Id = "";
            SelectCommandBuilder select = new SelectCommandBuilder("tmp_id");
            select.SelectColumn("str_bill_id");
            select.ConditionsColumn("1", 1);
            select.getSelectCommand();
            DataTable dt = select.ExecuteDataTable();
            if (dt == null || dt.Rows.Count == 0)
            {
                new InsertCommandBuilder().ExecuteNonQuery("insert into tmp_id values ('0')");
            }
            string currId = (dt != null && dt.Rows.Count != 0) ? dt.Rows[0][0].ToString() : "0";
            if (currId == "999999")
            {
                currId = "0";
            }
            int sNumber = int.Parse(currId);
            sNumber += 1;
            Id = front_fix + sNumber.ToString() + end_fix.ToString();
            new UpdateCommandBuilder().ExecuteNonQuery("update tmp_id set str_bill_id = '" + sNumber.ToString() + "'");
            return Id;
        }  
        public static string getNextId(string front_fix)
        {
            string Id = "";
            SelectCommandBuilder select = new SelectCommandBuilder("tmp_id");
            select.SelectColumn("str_bill_id");
            select.ConditionsColumn("1", 1);
            select.getSelectCommand();
            DataTable dt = select.ExecuteDataTable();
            if (dt == null || dt.Rows.Count == 0)
            {
                new InsertCommandBuilder().ExecuteNonQuery("insert into tmp_id values ('0')");
            }
            string currId = (dt != null && dt.Rows.Count != 0) ? dt.Rows[0][0].ToString() : "0";
            if (currId == "99999")
            {
                currId = "0";
            }
            int sNumber = int.Parse(currId);
            sNumber += 1;
            Id = front_fix + DateTime.Now.ToString("yyyyMMdd") + sNumber.ToString();
            new UpdateCommandBuilder().ExecuteNonQuery("update tmp_id set str_bill_id = '" + sNumber.ToString() + "'");
            return Id;
        }
        public static string getNextId(string front_fix, string end_fix, int count)
        {
            string Id = "";
            SelectCommandBuilder select = new SelectCommandBuilder("tmp_id");
            select.SelectColumn("str_bill_id");
            select.ConditionsColumn("1", 1);
            select.getSelectCommand();
            string currId = select.ExecuteDataTable().Rows[0][0].ToString();
            currId = currId == "" ? "0" : currId;
            if (currId == "999999")
            {
                currId = "0";
            }
            int sNumber = int.Parse(currId);
            sNumber += 1;
            Id = front_fix + sNumber.ToString().PadLeft(6, '0') + end_fix;
            new UpdateCommandBuilder().ExecuteNonQuery("update tmp_id set str_bill_id = '" + sNumber.ToString() + "'");
            return Id;
        }
        public static int getCurrentId()
        {
            SelectCommandBuilder select = new SelectCommandBuilder("tmp_id");
            select.SelectColumn("str_bill_id");
            select.ConditionsColumn("1", 1);
            select.getSelectCommand();
            int currId = int.Parse(select.ExecuteDataTable().Rows[0][0].ToString());
            return currId;
        }
        public static int count = 0;
    }
}