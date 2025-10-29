using System.Windows.Forms;

namespace GUI
{
    partial class UC_QuanLyHopDong
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.loctheokhachhang = new Guna.UI2.WinForms.Guna2ComboBox();
            this.loctheotrangthai = new Guna.UI2.WinForms.Guna2ComboBox();
            this.ThanhTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.suahopdong = new Guna.UI2.WinForms.Guna2Button();
            this.xoahopdong = new Guna.UI2.WinForms.Guna2Button();
            this.themhopdong = new Guna.UI2.WinForms.Guna2Button();
            this.microphone = new Guna.UI2.WinForms.Guna2PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.loctrangthai = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.microphone)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // loctheokhachhang
            // 
            this.loctheokhachhang.BackColor = System.Drawing.Color.Transparent;
            this.loctheokhachhang.BorderColor = System.Drawing.Color.White;
            this.loctheokhachhang.BorderRadius = 15;
            this.loctheokhachhang.BorderThickness = 2;
            this.loctheokhachhang.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.loctheokhachhang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.loctheokhachhang.FillColor = System.Drawing.Color.SeaGreen;
            this.loctheokhachhang.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.loctheokhachhang.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.loctheokhachhang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.loctheokhachhang.ForeColor = System.Drawing.Color.White;
            this.loctheokhachhang.ItemHeight = 30;
            this.loctheokhachhang.Location = new System.Drawing.Point(265, 82);
            this.loctheokhachhang.Name = "loctheokhachhang";
            this.loctheokhachhang.Size = new System.Drawing.Size(229, 36);
            this.loctheokhachhang.TabIndex = 49;
            // 
            // loctheotrangthai
            // 
            this.loctheotrangthai.BackColor = System.Drawing.Color.Transparent;
            this.loctheotrangthai.BorderColor = System.Drawing.Color.White;
            this.loctheotrangthai.BorderRadius = 15;
            this.loctheotrangthai.BorderThickness = 2;
            this.loctheotrangthai.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.loctheotrangthai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.loctheotrangthai.FillColor = System.Drawing.Color.SeaGreen;
            this.loctheotrangthai.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.loctheotrangthai.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.loctheotrangthai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.loctheotrangthai.ForeColor = System.Drawing.Color.White;
            this.loctheotrangthai.ItemHeight = 30;
            this.loctheotrangthai.Location = new System.Drawing.Point(675, 82);
            this.loctheotrangthai.Name = "loctheotrangthai";
            this.loctheotrangthai.Size = new System.Drawing.Size(188, 36);
            this.loctheotrangthai.TabIndex = 47;
            // 
            // ThanhTimKiem
            // 
            this.ThanhTimKiem.BorderColor = System.Drawing.Color.Black;
            this.ThanhTimKiem.BorderRadius = 15;
            this.ThanhTimKiem.BorderThickness = 2;
            this.ThanhTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ThanhTimKiem.DefaultText = "";
            this.ThanhTimKiem.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.ThanhTimKiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ThanhTimKiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ThanhTimKiem.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ThanhTimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ThanhTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ThanhTimKiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ThanhTimKiem.Location = new System.Drawing.Point(546, 36);
            this.ThanhTimKiem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ThanhTimKiem.Name = "ThanhTimKiem";
            this.ThanhTimKiem.PlaceholderForeColor = System.Drawing.Color.Black;
            this.ThanhTimKiem.PlaceholderText = "Tìm kiếm theo tên hợp đồng...";
            this.ThanhTimKiem.SelectedText = "";
            this.ThanhTimKiem.Size = new System.Drawing.Size(327, 36);
            this.ThanhTimKiem.TabIndex = 45;
            this.ThanhTimKiem.TextChanged += new System.EventHandler(this.guna2TextBox1_TextChanged);
            // 
            // suahopdong
            // 
            this.suahopdong.BorderColor = System.Drawing.Color.White;
            this.suahopdong.BorderRadius = 15;
            this.suahopdong.BorderThickness = 2;
            this.suahopdong.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.suahopdong.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.suahopdong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.suahopdong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.suahopdong.FillColor = System.Drawing.Color.SeaGreen;
            this.suahopdong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.suahopdong.ForeColor = System.Drawing.Color.White;
            this.suahopdong.Location = new System.Drawing.Point(391, 36);
            this.suahopdong.Name = "suahopdong";
            this.suahopdong.Size = new System.Drawing.Size(113, 36);
            this.suahopdong.TabIndex = 44;
            this.suahopdong.Text = "Sửa";
            this.suahopdong.Click += new System.EventHandler(this.guna2Button3_Click_1);
            // 
            // xoahopdong
            // 
            this.xoahopdong.BorderColor = System.Drawing.Color.White;
            this.xoahopdong.BorderRadius = 15;
            this.xoahopdong.BorderThickness = 2;
            this.xoahopdong.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.xoahopdong.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.xoahopdong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.xoahopdong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.xoahopdong.FillColor = System.Drawing.Color.SeaGreen;
            this.xoahopdong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.xoahopdong.ForeColor = System.Drawing.Color.White;
            this.xoahopdong.Location = new System.Drawing.Point(245, 36);
            this.xoahopdong.Name = "xoahopdong";
            this.xoahopdong.Size = new System.Drawing.Size(113, 36);
            this.xoahopdong.TabIndex = 43;
            this.xoahopdong.Text = "Xóa";
            this.xoahopdong.Click += new System.EventHandler(this.guna2Button2_Click_1);
            // 
            // themhopdong
            // 
            this.themhopdong.BorderColor = System.Drawing.Color.White;
            this.themhopdong.BorderRadius = 15;
            this.themhopdong.BorderThickness = 2;
            this.themhopdong.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.themhopdong.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.themhopdong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.themhopdong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.themhopdong.FillColor = System.Drawing.Color.SeaGreen;
            this.themhopdong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.themhopdong.ForeColor = System.Drawing.Color.White;
            this.themhopdong.Location = new System.Drawing.Point(106, 36);
            this.themhopdong.Name = "themhopdong";
            this.themhopdong.Size = new System.Drawing.Size(113, 36);
            this.themhopdong.TabIndex = 42;
            this.themhopdong.Text = "Thêm";
            this.themhopdong.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // microphone
            // 
            this.microphone.FillColor = System.Drawing.Color.Transparent;
            this.microphone.Image = global::GUI.Properties.Resources.mic;
            this.microphone.ImageRotate = 0F;
            this.microphone.Location = new System.Drawing.Point(832, 37);
            this.microphone.Name = "microphone";
            this.microphone.Size = new System.Drawing.Size(28, 35);
            this.microphone.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.microphone.TabIndex = 49;
            this.microphone.TabStop = false;
            this.microphone.UseTransparentBackground = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(10, 169);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(972, 524);
            this.flowLayoutPanel1.TabIndex = 57;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.loctheotrangthai);
            this.guna2Panel1.Controls.Add(this.loctheokhachhang);
            this.guna2Panel1.Controls.Add(this.label1);
            this.guna2Panel1.Controls.Add(this.loctrangthai);
            this.guna2Panel1.Location = new System.Drawing.Point(10, 14);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(972, 149);
            this.guna2Panel1.TabIndex = 58;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(110, 82);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.label1.Size = new System.Drawing.Size(149, 36);
            this.label1.TabIndex = 50;
            this.label1.Text = "Khách hàng:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // loctrangthai
            // 
            this.loctrangthai.BackColor = System.Drawing.Color.Transparent;
            this.loctrangthai.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold);
            this.loctrangthai.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.loctrangthai.Location = new System.Drawing.Point(534, 81);
            this.loctrangthai.Name = "loctrangthai";
            this.loctrangthai.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.loctrangthai.Size = new System.Drawing.Size(135, 37);
            this.loctrangthai.TabIndex = 48;
            this.loctrangthai.Text = "Trạng thái:";
            this.loctrangthai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.loctrangthai.Click += new System.EventHandler(this.loctrangthai_Click);
            // 
            // UC_QuanLyHopDong
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.microphone);
            this.Controls.Add(this.ThanhTimKiem);
            this.Controls.Add(this.suahopdong);
            this.Controls.Add(this.xoahopdong);
            this.Controls.Add(this.themhopdong);
            this.Controls.Add(this.guna2Panel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UC_QuanLyHopDong";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(992, 703);
            ((System.ComponentModel.ISupportInitialize)(this.microphone)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2ComboBox loctheokhachhang;
        private Guna.UI2.WinForms.Guna2ComboBox loctheotrangthai;
        private Guna.UI2.WinForms.Guna2TextBox ThanhTimKiem;
        private Guna.UI2.WinForms.Guna2Button suahopdong;
        private Guna.UI2.WinForms.Guna2Button xoahopdong;
        private Guna.UI2.WinForms.Guna2Button themhopdong;
        private Guna.UI2.WinForms.Guna2PictureBox microphone;
        private FlowLayoutPanel flowLayoutPanel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Label label1;
        private Label loctrangthai;
    }
}
