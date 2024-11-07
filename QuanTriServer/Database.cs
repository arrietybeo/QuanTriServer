using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanTriServer
{
    public class Database
    {
        private MySqlConnection connection;
        private bool isConnected = false;
        private string host;
        private string user;
        private string pass;
        private string database_name;

        public Database(string host, string user, string pass, string database)
        {
            this.host = host;
            this.user = user;
            this.pass = pass;
            this.database_name = database;

            string connectionString = $"server={host};user={user};database={database};password={pass};";
            this.connection = new MySqlConnection(connectionString);
        }
        public bool Connect()
        {
            try
            {
                connection.Open();
                isConnected = true;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
                return false;
            }
        }
        public void CloseConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                isConnected = false;
            }
        }
        public MySqlConnection GetConnection()
        {
            return connection;
        }

        public bool IsConnected()
        {
            return isConnected;
        }

        public string GetDatabaseName()
        {
            return database_name;
        }

        public string GetHost()
        {
            return host;
        }

        public string GetUser()
        {
            return user;
        }

        public string GetPass()
        {
            return pass;
        }

        public DataTable ExecuteQuery(string query)
        {
            DataTable dataTable = new DataTable();

            try
            {
                if (!isConnected)
                {
                    Connect();
                }

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable); // Lấy dữ liệu từ query và đổ vào DataTable
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực hiện truy vấn: " + ex.Message);
            }

            return dataTable;
        }

    }
}
