using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace RegistroDeUsuarios.Infra.DataBase
{
    internal class ConnectDatabase
    {
        private static string server = "localhost";
        private static string user = "user";
        private static string password = "";
        private static string database = "cadastroUnico";

        private string connectionString = $"Server={server};Database={database};Uid={user};Pwd={password}";

        private MySqlConnection dbClient;

        public void DBConection()
        {
            dbClient = new MySqlConnection(connectionString);
        }

        public MySqlConnection Connect()
        {
            if (dbClient.State == System.Data.ConnectionState.Closed)
            {
                dbClient.Open();
            }
            return dbClient;
        }

        public void Desconnect()
        {
            if (dbClient.State == System.Data.ConnectionState.Open)
            {
                dbClient.Close();
            }
        }
    }
}
