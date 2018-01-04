using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using ERPPlugIn.Class;
using System.Data;

namespace ERPPlugIn.AuthManger
{
    public class AuthExtension
    {
        public static bool CheckAuth(int userid, int authid)
        {
            string sql = string.Format("select isnull(max([matchid]),-1) from [userauthorizeMatch] where [userid] = {0} and [authorizeid] = {1}", userid, authid);

            return 0 < (int)SqlHelper.ExecuteScalar(ConnectionFactory.ConnectionString_hudsonwwwroot, System.Data.CommandType.Text, sql);
        }

        public static bool CheckAuth(int userid, string authtitle)
        {

            string sql = string.Format(" select isnull(max([matchid]),-1) from [userauthorizeMatch] match inner join authorize auth on auth.authorizeid = match.authorizeid  where match.[userid] = {0} and Title = '{1}'", userid, authtitle);

            return 0 < (int)SqlHelper.ExecuteScalar(ConnectionFactory.ConnectionString_hudsonwwwroot, System.Data.CommandType.Text, sql);
        }

        public static Dictionary<int, string> GetUserAuth(int userid, int authid)
        {
            string sql = string.Format(" select match.authorizeid,Title from [userauthorizeMatch] match inner join authorize auth on auth.authorizeid = match.authorizeid  where match.userid = {0}", userid);
            var dic = new Dictionary<int, string>();
            using (var dr = SqlHelper.ExecuteReader(ConnectionFactory.ConnectionString_hudsonwwwroot, System.Data.CommandType.Text, sql))
            {
                while (dr.Read())
                {
                    dic.Add(dr.GetInt32(0), dr.GetString(1));
                }
                dr.Close();
            }
            return dic;
        }

        public static GenericNode<KeyValuePair<int, string>> GetAuthTree()
        {
            string sql_root = "select [authorizeid],[authorizeGroupid],[Title] from authorize where authorizeGroupid is null order by  authorizeid";
            string sql_nodetable = "select [authorizeid],[authorizeGroupid],[Title] from authorize where authorizeGroupid >0 order by  authorizeGroupid,authorizeid";
            GenericNode<KeyValuePair<int, string>> root = new GenericNode<KeyValuePair<int, string>>(new KeyValuePair<int, string>(0, "root"));
            using (var ds = SqlHelper.ExecuteDataset(ConnectionFactory.ConnectionString_hudsonwwwroot, System.Data.CommandType.Text, sql_nodetable))
            {
                var dt = ds.Tables[0];
                using (var dr = SqlHelper.ExecuteReader(ConnectionFactory.ConnectionString_hudsonwwwroot, System.Data.CommandType.Text, sql_root))
                {
                    while (dr.Read())
                    {
                        var newNode = new GenericNode<KeyValuePair<int, string>>(new KeyValuePair<int, string>(dr.GetInt32(0), dr.GetString(2)));
                        newNode.Child.AddRange(NodeBuilder(newNode.Node.Key, dt));
                        root.Child.Add(newNode);
                    }
                    dr.Close();
                }
            }

            return root;
        }

        public static IEnumerable<GenericNode<KeyValuePair<int, string>>> NodeBuilder(int Groupid, DataTable dt)
        {
            var nodes = new List<GenericNode<KeyValuePair<int, string>>>();
            var drs = dt.Select("authorizeGroupid=" + Groupid, "authorizeid asc");
            foreach (var dr in drs)
            {
                var newNode = new GenericNode<KeyValuePair<int, string>>(new KeyValuePair<int, string>((int)dr[0], (string)dr[2]));
                newNode.Child.AddRange(NodeBuilder(newNode.Node.Key, dt));

                nodes.Add(newNode);
            }

            return nodes;
        }
    }


    public class GenericNode<T>
    {
        public T Node { get; private set; }

        public List<GenericNode<T>> Child { get; private set; }

        public GenericNode(T n)
        {
            Node = n;

            Child = new List<GenericNode<T>>();
        }
    }
}