using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace KNCore.DapperDAL
{
    public class DapperConfig
    {
        private static string DefaultSqlConnectString= "server=192.168.99.100;database=kamdb;uid=kim;pwd=147852;pooling=true;CharSet=utf8;port=3308;sslmode=none";

        public static IDbConnection GetSqlConnection(string sqlConnectionString = null)
        {
            if (string.IsNullOrWhiteSpace(sqlConnectionString))
            {
                sqlConnectionString = DefaultSqlConnectString;
            }
            IDbConnection conn = new MySqlConnection(sqlConnectionString);
            conn.Open();
            
            return conn;
        }
    }
}
