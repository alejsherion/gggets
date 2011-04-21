using System.Data;
using System.Data.OleDb;

namespace GGGETSAdmin.Common
{
    public class OleDbAccessHelper
    {
        DataTable dt = new DataTable();
        public DataTable OleDbQuery(string sql, string database)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + database + ";Extended Properties='Excel 12.0 Xml;HDR=YES'";
            using (OleDbConnection cn = new OleDbConnection(cs))
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                    using (OleDbCommand cmd = new OleDbCommand(sql, cn))
                    {
                        using (OleDbDataReader dr = cmd.ExecuteReader())
                        {
                            dt.Load(dr);
                        }
                    }
                }
            }
            return dt;
        }
    }
}
