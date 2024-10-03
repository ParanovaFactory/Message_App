using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp
{
    public class SQLConnection
    {
        public SqlConnection connection()
        {
            SqlConnection sql = new SqlConnection(@"Data Source=Predator;Initial Catalog=Db_Message;Integrated Security=True;TrustServerCertificate=True");
            sql.Open();
            return sql;
        }
    }
}
