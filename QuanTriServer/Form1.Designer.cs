namespace QuanTriServer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            HostBox = new TextBox();
            pictureBox4 = new PictureBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            UserBox = new TextBox();
            PasswordBox = new TextBox();
            LoginButton = new Button();
            label6 = new Label();
            DatabaseBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(693, 8);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(23, 26);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlDark;
            panel1.Controls.Add(pictureBox3);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(730, 37);
            panel1.TabIndex = 1;
            panel1.Paint += panel1_Paint;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(664, 8);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(23, 23);
            pictureBox3.TabIndex = 3;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(0, 7);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(29, 30);
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Sitka Small", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(35, 8);
            label1.Name = "label1";
            label1.Size = new Size(128, 23);
            label1.TabIndex = 1;
            label1.Text = "Quản trị Server";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Sitka Small", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(355, 57);
            label2.Name = "label2";
            label2.Size = new Size(285, 50);
            label2.TabIndex = 4;
            label2.Text = "Login Database";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // HostBox
            // 
            HostBox.Location = new Point(478, 150);
            HostBox.Name = "HostBox";
            HostBox.Size = new Size(178, 27);
            HostBox.TabIndex = 5;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(-172, 43);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(462, 325);
            pictureBox4.TabIndex = 6;
            pictureBox4.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Sitka Small", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(364, 152);
            label3.Name = "label3";
            label3.Size = new Size(87, 25);
            label3.TabIndex = 7;
            label3.Text = "IP Mysql";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Sitka Small", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(378, 201);
            label4.Name = "label4";
            label4.Size = new Size(52, 25);
            label4.TabIndex = 8;
            label4.Text = "User";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Sitka Small", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(364, 247);
            label5.Name = "label5";
            label5.Size = new Size(95, 25);
            label5.TabIndex = 9;
            label5.Text = "Password";
            // 
            // UserBox
            // 
            UserBox.Location = new Point(478, 199);
            UserBox.Name = "UserBox";
            UserBox.Size = new Size(178, 27);
            UserBox.TabIndex = 10;
            // 
            // PasswordBox
            // 
            PasswordBox.Location = new Point(478, 245);
            PasswordBox.Name = "PasswordBox";
            PasswordBox.Size = new Size(178, 27);
            PasswordBox.TabIndex = 11;
            // 
            // LoginButton
            // 
            LoginButton.Location = new Point(520, 359);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(94, 29);
            LoginButton.TabIndex = 4;
            LoginButton.Text = "Login";
            LoginButton.UseVisualStyleBackColor = true;
            LoginButton.Click += LoginButton_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Sitka Small", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(364, 298);
            label6.Name = "label6";
            label6.Size = new Size(91, 25);
            label6.TabIndex = 12;
            label6.Text = "Database";
            // 
            // DatabaseBox
            // 
            DatabaseBox.Location = new Point(478, 299);
            DatabaseBox.Name = "DatabaseBox";
            DatabaseBox.Size = new Size(178, 27);
            DatabaseBox.TabIndex = 13;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(728, 448);
            Controls.Add(DatabaseBox);
            Controls.Add(label6);
            Controls.Add(LoginButton);
            Controls.Add(PasswordBox);
            Controls.Add(UserBox);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(pictureBox4);
            Controls.Add(HostBox);
            Controls.Add(label2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản Trị Server";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Panel panel1;
        private Label label1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private Label label2;
        private ContextMenuStrip contextMenuStrip1;
        private TextBox HostBox;
        private PictureBox pictureBox4;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox UserBox;
        private TextBox PasswordBox;
        private Button LoginButton;
        private Label label6;
        private TextBox DatabaseBox;
    }
}
