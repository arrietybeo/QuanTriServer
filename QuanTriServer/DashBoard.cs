using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Newtonsoft.Json;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Xml.Linq;

namespace QuanTriServer
{
    public partial class DashBoard : Form
    {

        private string databaseName;
        private Database database;
        private SelectPlayer selectPlayer;

        public DashBoard(Database database)
        {
            this.database = database;
            this.databaseName = this.database.GetDatabaseName();
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(DashBoard_KeyDown);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void ReleaseCapture();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private void DashBoard_Load(object sender, EventArgs e)
        {
            panel1.MouseDown += new MouseEventHandler(panel1_MouseDown);
            label1.Text = $"Database Name: {databaseName}";
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (database != null && database.IsConnected())
            {
                try
                {
                    string query = "SELECT id, type, gender, name, description, icon_id, part FROM item_template";

                    MySqlCommand cmd = new MySqlCommand(query, database.GetConnection());
                    MySqlDataReader reader = cmd.ExecuteReader();

                    dataGridViewItems.Rows.Clear();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        int type = reader.GetInt32("type");
                        int gender = reader.GetInt32("gender");

                        string name = reader["name"].ToString();
                        string caption = reader["description"].ToString();

                        string iconFileName = reader.GetInt32("icon_id") + ".png";
                        string iconPath = $"image/icon/{iconFileName}";

                        Image iconImage = null;
                        if (File.Exists(iconPath))
                        {
                            iconImage = Image.FromFile(iconPath);
                        }
                        int part = reader.GetInt32("part");

                        dataGridViewItems.Rows.Add(id, type, gender, name, caption, iconImage, part);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Chưa kết nối tới cơ sở dữ liệu.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id = textId.Text;
            if (!int.TryParse(id, out int id_item))
            {
                MessageBox.Show("id không hợp lệ :v");
                return;
            }
            if (database != null && database.IsConnected())
            {
                dataGridViewItems.Rows.Clear();
                try
                {
                    string query = "SELECT * FROM item_template WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, database.GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("@id", id_item);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int con_cac = reader.GetInt32("id");
                                int type = reader.GetInt32("type");
                                int gender = reader.GetInt32("gender");

                                string name = reader["name"].ToString();
                                string caption = reader["description"].ToString();

                                string iconFileName = reader.GetInt32("icon_id") + ".png";
                                string iconPath = $"image/icon/{iconFileName}";

                                Image iconImage = null;
                                if (File.Exists(iconPath))
                                {
                                    iconImage = Image.FromFile(iconPath);
                                }
                                int part = reader.GetInt32("part");

                                dataGridViewItems.Rows.Add(con_cac, type, gender, name, caption, iconImage, part);
                            }
                        }

                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void DashBoard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                ShowSearchDialog();
            }
        }

        private void ShowSearchDialog()
        {
            string searchText = Microsoft.VisualBasic.Interaction.InputBox("Nhập từ khóa tìm kiếm:", "Tìm kiếm", "");

            if (!string.IsNullOrEmpty(searchText))
            {
                SearchInDataGridView(searchText);
            }
        }


        private void SearchInDataGridView(string searchText)
        {
            // create list data grid view 
            List<DataGridViewRow> matchingRows = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in dataGridViewItems.Rows)
            {
                bool rowMatches = false;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    {
                        rowMatches = true;
                        break; // stop khi khong tim thấy
                    }
                }

                if (rowMatches)
                {
                    // copy bảng hiện tại cho zô danh sách
                    matchingRows.Add((DataGridViewRow)row.Clone());
                    int cellIndex = 0;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        matchingRows[matchingRows.Count - 1].Cells[cellIndex].Value = cell.Value;
                        cellIndex++;
                    }
                }
            }

            // clear sạch dữ liệu trong bảng
            dataGridViewItems.Rows.Clear();


            if (matchingRows.Count > 0)
            {
                foreach (DataGridViewRow row in matchingRows)
                {
                    dataGridViewItems.Rows.Add(row);
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy kết quả phù hợp.", "Kết quả tìm kiếm");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (comboTypeFind.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn loại type để tìm tên nhân vật!");
                return;
            }

            string name_player = textNamePlayer.Text;

            if (string.IsNullOrEmpty(name_player))
            {
                MessageBox.Show("Vui lòng nhập ít nhất 2 ký tự để tìm kiếm.");
                return;
            }

            if (database != null && database.IsConnected())
            {
                selectPlayer = new SelectPlayer(this, database);
                selectPlayer.findPlayerByName(comboTypeFind.SelectedIndex, name_player);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = comboTypeFind.SelectedItem.ToString();
        }

        public void setPictureBoxImageHead(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                pictureBox4.Image = Image.FromFile(imagePath);
                pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        public void setTabPageTask(string name, string detail)
        {
            nameTask.Text = $"Name Task: {name}";
            detailTask.Text = $"Detail Task: {detail}";
        }

        public void setTextPointPlayer(string name, long power, int hp, int hpmax, int mp, int mpMax, int dame, int def, int crif)
        {
            groupBox1.Text = $"Name Player: {name}";
            //LabelNamePlayer.Text = $"Name Player: {name}";
            labelPowerPlayer.Text = $"Sức Mạnh: {power}";
            lablePointHP.Text = $"Hp - Hp max: {hpmax} - {hp}";
            lablePointMP.Text = $"Mp - Mp max: {mpMax} - {mp}";
            lablePointDame.Text = $"Dame: {dame}";
            lablePointDefense.Text = $"Giáp: {def}";
            labelPointCrit.Text = $"Chí mạng: {crif}";
        }

        public void setTextInventory(long gold, long gem, long ruby)
        {
            labelGold.Text = $"Vàng: {gold}";
            labelGem.Text = $"Ngọc xanh: {gem}";
            labelRuby.Text = $"Ngọc hồng: {ruby}";
        }

        public void addDataGridViewItemBody(int id, string name, Image iconImage)
        {
            dataGridViewItemBody.Rows.Add(id, name, iconImage);
        }

        public void clearDataGridViewItemBody()
        {
            dataGridViewItemBody.Rows.Clear();
        }

        public void addDataGridViewItemBox(int id, string name, Image iconImage)
        {
            dataGridViewBoxPlayer.Rows.Add(id, name, iconImage);
        }

        public void clearDataGridViewItemBox()
        {
            dataGridViewBoxPlayer.Rows.Clear();
        }
    }
}
