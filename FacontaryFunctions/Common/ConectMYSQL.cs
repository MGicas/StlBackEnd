using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace FacontaryFunctions.Common
{
    public class ConectMYSQL
    {
        public static async Task<MySqlConnection> ConnAsync()
        {
            try
            {

                string conStr = System.Environment.GetEnvironmentVariable("ConnectionStrings:MySqlConnection");

                if (string.IsNullOrEmpty(conStr)) // Azure Functions App Service naming convention
                {
                    //conStr = System.Environment.GetEnvironmentVariable($"MYSQLAZURECONNSTR_MySqlConnection", EnvironmentVariableTarget.Process);
                    conStr = "Server=facontary.mysql.database.azure.com; Port=3306; Database=facontary; Uid=zebax; Pwd=Ernalituyd_#45%?2suq3.xsa; SslMode=Preferred;";
                }
                MySqlConnection connection = new MySqlConnection(conStr);
                await connection.OpenAsync();

                return connection;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void PrepareTextCmd(MySqlCommand cmd)
        {
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Clear();
        }
        public static void PrepareSPCmd(MySqlCommand cmd)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
        }
    }
}
