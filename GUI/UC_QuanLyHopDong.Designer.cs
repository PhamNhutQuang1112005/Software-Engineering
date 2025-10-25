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
            this.lockhachhang = new System.Windows.Forms.Label();
            this.loctrangthai = new System.Windows.Forms.Label();
            this.loctheokhachhang = new Guna.UI2.WinForms.Guna2ComboBox();
            this.loctheotrangthai = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2TextBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Button3 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.microphone = new Guna.UI2.WinForms.Guna2PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            ((System.ComponentModel.ISupportInitialize)(this.microphone)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lockhachhang
            // 
            this.lockhachhang.BackColor = System.Drawing.Color.SeaGreen;
            this.lockhachhang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lockhachhang.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lockhachhang.Location = new System.Drawing.Point(354, 86);
            this.lockhachhang.Name = "lockhachhang";
            this.lockhachhang.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.lockhachhang.Size = new System.Drawing.Size(104, 28);
            this.lockhachhang.TabIndex = 50;
            this.lockhachhang.Text = "Khách hàng";
            this.lockhachhang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lockhachhang.Click += new System.EventHandler(this.lockhachhang_Click);
            // 
            // loctrangthai
            // 
            this.loctrangthai.BackColor = System.Drawing.Color.SeaGreen;
            this.loctrangthai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.loctrangthai.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.loctrangthai.Location = new System.Drawing.Point(642, 86);
            this.loctrangthai.Name = "loctrangthai";
            this.loctrangthai.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.loctrangthai.Size = new System.Drawing.Size(93, 28);
            this.loctrangthai.TabIndex = 48;
            this.loctrangthai.Text = "Trạng thái";
            this.loctrangthai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.loctheokhachhang.Location = new System.Drawing.Point(262, 82);
            this.loctheokhachhang.Name = "loctheokhachhang";
            this.loctheokhachhang.Size = new System.Drawing.Size(223, 36);
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
            this.loctheotrangthai.Location = new System.Drawing.Point(516, 82);
            this.loctheotrangthai.Name = "loctheotrangthai";
            this.loctheotrangthai.Size = new System.Drawing.Size(247, 36);
            this.loctheotrangthai.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(164, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 23);
            this.label1.TabIndex = 46;
            this.label1.Text = "Lọc theo:";
            // 
            // guna2TextBox1
            // 
            this.guna2TextBox1.BorderColor = System.Drawing.Color.Black;
            this.guna2TextBox1.BorderRadius = 15;
            this.guna2TextBox1.BorderThickness = 2;
            this.guna2TextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox1.DefaultText = "";
            this.guna2TextBox1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2TextBox1.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox1.Location = new System.Drawing.Point(546, 36);
            this.guna2TextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2TextBox1.Name = "guna2TextBox1";
            this.guna2TextBox1.PlaceholderForeColor = System.Drawing.Color.Black;
            this.guna2TextBox1.PlaceholderText = "Tìm kiếm theo mã hợp đồng...";
            this.guna2TextBox1.SelectedText = "";
            this.guna2TextBox1.Size = new System.Drawing.Size(327, 36);
            this.guna2TextBox1.TabIndex = 45;
            this.guna2TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.guna2TextBox1.TextChanged += new System.EventHandler(this.guna2TextBox1_TextChanged);
            // 
            // guna2Button3
            // 
            this.guna2Button3.BorderColor = System.Drawing.Color.White;
            this.guna2Button3.BorderRadius = 15;
            this.guna2Button3.BorderThickness = 2;
            this.guna2Button3.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button3.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button3.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button3.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button3.FillColor = System.Drawing.Color.SeaGreen;
            this.guna2Button3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.guna2Button3.ForeColor = System.Drawing.Color.White;
            this.guna2Button3.Location = new System.Drawing.Point(391, 36);
            this.guna2Button3.Name = "guna2Button3";
            this.guna2Button3.Size = new System.Drawing.Size(113, 36);
            this.guna2Button3.TabIndex = 44;
            this.guna2Button3.Text = "Sửa";
            // 
            // guna2Button2
            // 
            this.guna2Button2.BorderColor = System.Drawing.Color.White;
            this.guna2Button2.BorderRadius = 15;
            this.guna2Button2.BorderThickness = 2;
            this.guna2Button2.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button2.FillColor = System.Drawing.Color.SeaGreen;
            this.guna2Button2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.guna2Button2.ForeColor = System.Drawing.Color.White;
            this.guna2Button2.Location = new System.Drawing.Point(245, 36);
            this.guna2Button2.Name = "guna2Button2";
            this.guna2Button2.Size = new System.Drawing.Size(113, 36);
            this.guna2Button2.TabIndex = 43;
            this.guna2Button2.Text = "Xóa";
            // 
            // guna2Button1
            // 
            this.guna2Button1.BorderColor = System.Drawing.Color.White;
            this.guna2Button1.BorderRadius = 15;
            this.guna2Button1.BorderThickness = 2;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.SeaGreen;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Location = new System.Drawing.Point(106, 36);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(113, 36);
            this.guna2Button1.TabIndex = 42;
            this.guna2Button1.Text = "Thêm";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
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
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.label1);
            this.guna2Panel1.Controls.Add(this.lockhachhang);
            this.guna2Panel1.Controls.Add(this.loctrangthai);
            this.guna2Panel1.Controls.Add(this.loctheotrangthai);
            this.guna2Panel1.Controls.Add(this.loctheokhachhang);
            this.guna2Panel1.Location = new System.Drawing.Point(10, 14);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(972, 149);
            this.guna2Panel1.TabIndex = 58;
            // 
            // UC_QuanLyHopDong
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.microphone);
            this.Controls.Add(this.guna2TextBox1);
            this.Controls.Add(this.guna2Button3);
            this.Controls.Add(this.guna2Button2);
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.guna2Panel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UC_QuanLyHopDong";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(992, 703);
            ((System.ComponentModel.ISupportInitialize)(this.microphone)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lockhachhang;
        private System.Windows.Forms.Label loctrangthai;
        private Guna.UI2.WinForms.Guna2ComboBox loctheokhachhang;
        private Guna.UI2.WinForms.Guna2ComboBox loctheotrangthai;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox1;
        private Guna.UI2.WinForms.Guna2Button guna2Button3;
        private Guna.UI2.WinForms.Guna2Button guna2Button2;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2PictureBox microphone;
        private FlowLayoutPanel flowLayoutPanel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
    }
}
