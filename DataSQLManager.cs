using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCDClient
{
    class DataSQLManager
    {
        public static void PushDataToSQL(string[] allvaluesarr)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["connStr"]))
            {
                sqlCon.Open();
                foreach (var row in allvaluesarr)
                {
                    using (SqlCommand sqlCmd1 = new SqlCommand { CommandText = "INSERT INTO <tablename> ([DataAreaID]) VALUES (@dataareaid)", Connection = sqlCon })
                    {
                        sqlCmd1.Parameters.AddWithValue("@dataareaid", row);                        
                        sqlCmd1.ExecuteNonQuery();
                    }

                }
                sqlCon.Close();
            }
        }
    }
}
