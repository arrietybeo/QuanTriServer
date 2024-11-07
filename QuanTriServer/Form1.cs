using System.Runtime.InteropServices;
using System.Security.Principal;


namespace QuanTriServer
{
    public partial class Form1 : Form
    {

        private Database database;

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (database != null && database.IsConnected())
            {
                database.CloseConnection();
            }
            Application.Exit();
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

        private void Form1_Load(object sender, EventArgs e)
        {
            HostBox.Text = "127.0.0.1";
            UserBox.Text = "root";
            DatabaseBox.Text = "data";
            panel1.MouseDown += new MouseEventHandler(panel1_MouseDown);

        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void ReleaseCapture();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string host = HostBox.Text;
            string user = UserBox.Text;
            string pass = PasswordBox.Text;
            string database_name = DatabaseBox.Text;

            if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(database_name))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin vào tất cả các ô.");
                return;
            }

            this.database = new Database(host, user, pass, database_name);

            if (this.database.Connect())
            {
                this.Hide();// ẩn form 1

                using (DashBoard formTool = new DashBoard(this.database))
                {
                    formTool.StartPosition = FormStartPosition.CenterParent;
                    formTool.ShowDialog();
                }

                this.Close();//đóng form 1
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoginButton_Click(this, new EventArgs()); 
                e.Handled = true; 
                e.SuppressKeyPress = true; 
            }
        }
    }
}
