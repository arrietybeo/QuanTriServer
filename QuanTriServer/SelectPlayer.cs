using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanTriServer
{
    public class SelectPlayer
    {
        public byte typeFind;
        public Player player;
        private DashBoard dashboard;
        private Database database;

        public SelectPlayer(DashBoard dashboard, Database database)
        {
            this.dashboard = dashboard;
            this.database = database;
        }

        public void findPlayerByName(int typeFind, string name)
        {
            //MessageBox.Show("Find player by name: " + name + " type: " + typeFind);
            switch (typeFind)
            {
                case 0:
                    this.findPlayerByIndex(name);
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }

        private void findPlayerByIndex(string name)
        {
            string name_player = name;
            try
            {
                string query = @"SELECT player.account_id, player.power, player.head, player.gender,
                         head_avatar.avatar_id, player.data_point, player.data_task, player.data_inventory,
                         player.items_body, player.items_bag, player.items_box
                         FROM player LEFT JOIN head_avatar ON player.head = head_avatar.head_id 
                         WHERE player.name LIKE @name_player";

                using (MySqlCommand cmd = new MySqlCommand(query, database.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@name_player", "%" + name_player + "%");
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        bool hasRows = false;
                        int taskId = -1;
                        string taskName = string.Empty;
                        string taskDetail = string.Empty;
                        List<short> itemIds = new List<short>();
                        List<short> itemListBag = new List<short>();
                        List<short> itemListBox = new List<short>();

                        while (reader.Read())
                        {
                            hasRows = true;
                            int account_id = reader.GetInt32("account_id");
                            long power = reader.GetInt64("power");
                            int head = reader.GetInt32("head");
                            int gender = reader.GetInt32("gender");

                            string jsonItemBody = reader.GetString("items_body");
                            string jsonItemBag = reader.GetString("items_bag");
                            string jsonItemBox = reader.GetString("items_box");

                            JArray itemsArray = JArray.Parse(jsonItemBody);
                            JArray itemBag = JArray.Parse(jsonItemBag);
                            JArray itemBox = JArray.Parse(jsonItemBox);

                            foreach (JObject dataObject in itemsArray)
                            {
                                short tempId = short.Parse(dataObject["temp_id"].ToString());
                                if (tempId != -1)
                                {
                                    itemIds.Add(tempId);
                                }
                            }

                            foreach (JObject dataObject in itemBag)
                            {
                                short tempId = short.Parse(dataObject["temp_id"].ToString());
                                if (tempId != -1)
                                {
                                    itemListBag.Add(tempId);
                                }
                            }

                            foreach (JObject dataObject in itemBox)
                            {
                                short tempId = short.Parse(dataObject["temp_id"].ToString());
                                if (tempId != -1)
                                {
                                    itemListBox.Add(tempId);
                                }
                            }

                            int avatar_id = reader.IsDBNull(reader.GetOrdinal("avatar_id")) ? 544 : reader.GetInt32("avatar_id");
                            if (avatar_id < 0)
                            {
                                avatar_id = 544;
                            }
                            string iconFileName = avatar_id + ".png";
                            string iconPath = $"image/icon/{iconFileName}";

                            dashboard.setPictureBoxImageHead(iconPath);

                            string jsonData = reader.GetString("data_point");
                            JArray dataPointArray = JArray.Parse(jsonData);

                            long nangdong = (long)dataPointArray[0];
                            int plMp = (int)dataPointArray[1];
                            int mpg = (int)dataPointArray[2];
                            int critg = (int)dataPointArray[3];
                            long limitPower = (long)dataPointArray[4];
                            long stamina = (long)dataPointArray[5];
                            int plHp = (int)dataPointArray[6];
                            int defg = (int)dataPointArray[7];
                            long tiemNang = (long)dataPointArray[8];
                            long maxStamina = (long)dataPointArray[9];
                            int dameg = (int)dataPointArray[10];
                            long power_old = (long)dataPointArray[11];
                            int hpg = (int)dataPointArray[12];

                            dashboard.setTextPointPlayer(
                                name_player,
                                power,
                                hpg,
                                plHp,
                                plMp,
                                mpg,
                                dameg,
                                defg,
                                critg
                            );

                            string jsonInventory = reader.GetString("data_inventory");
                            long[] dataJsonInventory = JsonConvert.DeserializeObject<long[]>(jsonInventory);
                            long gold = dataJsonInventory[0];
                            long gem = dataJsonInventory[1];
                            long ruby = dataJsonInventory[2];

                            dashboard.setTextInventory(gold, gem, ruby);

                            string data_task = reader.GetString("data_task");
                            int[] taskArray = JsonConvert.DeserializeObject<int[]>(data_task);
                            taskId = taskArray.Length > 1 ? taskArray[1] : -1;
                        }

                        if (!hasRows)
                        {
                            MessageBox.Show("Không tìm thấy kết quả phù hợp.");
                        }

                        reader.Close(); // Đóng `reader` trước khi gọi `addDataGridViewItemBody`

                        if (taskId != -1)
                        {
                            string taskQuery = "SELECT name, detail FROM task_main_template WHERE id = @taskId";
                            using (MySqlCommand avatarCmd = new MySqlCommand(taskQuery, database.GetConnection()))
                            {
                                avatarCmd.Parameters.AddWithValue("@taskId", taskId);

                                using (MySqlDataReader taskReader = avatarCmd.ExecuteReader())
                                {
                                    if (taskReader.Read())
                                    {
                                        taskName = taskReader["name"].ToString();
                                        taskDetail = taskReader["detail"].ToString();
                                        dashboard.setTabPageTask(taskName, taskDetail);
                                    }
                                }
                            }
                        }

                        dashboard.clearDataGridViewItemBody();

                        foreach (short tempId in itemIds)
                        {
                            addDataGridViewItemBody(tempId);
                        }

                        foreach (short tempId in itemListBag)
                        {
                            addDataGridViewItemBody(tempId);
                        }

                        dashboard.clearDataGridViewItemBox();

                        foreach (short tempId in itemListBox)
                        {
                            addDataGridViewItemBox(tempId);
                        }

                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi truy vấn: " + e.Message);
            }
        }

        private void addDataGridViewItemBox(int id_item)
        {
            try
            {
                string query = "SELECT * FROM item_template WHERE id = @id_item";

                using (MySqlConnection connection = new MySqlConnection(database.GetConnection().ConnectionString))
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id_item", id_item);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = reader["name"].ToString();
                                string iconFileName = reader.GetInt32("icon_id") + ".png";
                                string iconPath = $"image/icon/{iconFileName}";

                                Image iconImage = null;
                                if (File.Exists(iconPath))
                                {
                                    iconImage = Image.FromFile(iconPath);
                                }
                                //MessageBox.Show("Add item: " + name + " id: " + id_item);
                                dashboard.addDataGridViewItemBox(id_item, name, iconImage);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi truy vấn: " + e.Message);
            }

        }

        private void addDataGridViewItemBody(int id_item)
        {
            try
            {
                string query = "SELECT * FROM item_template WHERE id = @id_item";

                using (MySqlConnection connection = new MySqlConnection(database.GetConnection().ConnectionString))
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id_item", id_item);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = reader["name"].ToString();
                                string iconFileName = reader.GetInt32("icon_id") + ".png";
                                string iconPath = $"image/icon/{iconFileName}";

                                Image iconImage = null;
                                if (File.Exists(iconPath))
                                {
                                    iconImage = Image.FromFile(iconPath);
                                }
                                //MessageBox.Show("Add item: " + name + " id: " + id_item);
                                dashboard.addDataGridViewItemBody(id_item, name, iconImage);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi truy vấn: " + e.Message);
            }
        }
    }
}
