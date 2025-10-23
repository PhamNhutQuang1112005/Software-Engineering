using System.Windows.Forms;

namespace GUI
{
    partial class UC_QuanLyKhachHang
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
            this.ThanhTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnSua = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.themkhachhang = new Guna.UI2.WinForms.Guna2Button();
            this.microphone = new Guna.UI2.WinForms.Guna2PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            ((System.ComponentModel.ISupportInitialize)(this.microphone)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.ThanhTimKiem.Location = new System.Drawing.Point(531, 33);
            this.ThanhTimKiem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ThanhTimKiem.Name = "ThanhTimKiem";
            this.ThanhTimKiem.PlaceholderForeColor = System.Drawing.Color.Black;
            this.ThanhTimKiem.PlaceholderText = "Thanh tìm kiếm...";
            this.ThanhTimKiem.SelectedText = "";
            this.ThanhTimKiem.Size = new System.Drawing.Size(327, 36);
            this.ThanhTimKiem.TabIndex = 54;
            this.ThanhTimKiem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ThanhTimKiem.TextChanged += new System.EventHandler(this.ThanhTimKiem_TextChanged);
            // 
            // btnSua
            // 
            this.btnSua.BorderColor = System.Drawing.Color.White;
            this.btnSua.BorderRadius = 15;
            this.btnSua.BorderThickness = 2;
            this.btnSua.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSua.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSua.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSua.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSua.FillColor = System.Drawing.Color.SeaGreen;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(376, 33);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(113, 36);
            this.btnSua.TabIndex = 53;
            this.btnSua.Text = "Sửa";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BorderColor = System.Drawing.Color.White;
            this.btnXoa.BorderRadius = 15;
            this.btnXoa.BorderThickness = 2;
            this.btnXoa.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXoa.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXoa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXoa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXoa.FillColor = System.Drawing.Color.SeaGreen;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(230, 33);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(113, 36);
            this.btnXoa.TabIndex = 52;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // themkhachhang
            // 
            this.themkhachhang.BorderColor = System.Drawing.Color.White;
            this.themkhachhang.BorderRadius = 15;
            this.themkhachhang.BorderThickness = 2;
            this.themkhachhang.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.themkhachhang.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.themkhachhang.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.themkhachhang.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.themkhachhang.FillColor = System.Drawing.Color.SeaGreen;
            this.themkhachhang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.themkhachhang.ForeColor = System.Drawing.Color.White;
            this.themkhachhang.Location = new System.Drawing.Point(91, 33);
            this.themkhachhang.Name = "themkhachhang";
            this.themkhachhang.Size = new System.Drawing.Size(113, 36);
            this.themkhachhang.TabIndex = 51;
            this.themkhachhang.Text = "Thêm";
            this.themkhachhang.Click += new System.EventHandler(this.themkhachhang_Click);
            // 
            // microphone
            // 
            this.microphone.FillColor = System.Drawing.Color.Transparent;
            this.microphone.Image = global::GUI.Properties.Resources.mic;
            this.microphone.ImageRotate = 0F;
            this.microphone.Location = new System.Drawing.Point(817, 34);
            this.microphone.Name = "microphone";
            this.microphone.Size = new System.Drawing.Size(28, 35);
            this.microphone.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.microphone.TabIndex = 55;
            this.microphone.TabStop = false;
            this.microphone.UseTransparentBackground = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(10, 116);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(972, 451);
            this.flowLayoutPanel1.TabIndex = 56;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.ThanhTimKiem);
            this.guna2Panel1.Controls.Add(this.themkhachhang);
            this.guna2Panel1.Controls.Add(this.microphone);
            this.guna2Panel1.Controls.Add(this.btnXoa);
            this.guna2Panel1.Controls.Add(this.btnSua);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(10, 10);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(972, 100);
            this.guna2Panel1.TabIndex = 57;
            // 
            // UC_QuanLyKhachHang
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UC_QuanLyKhachHang";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(992, 577);
            this.Load += new System.EventHandler(this.UC_QuanLyKhachHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.microphone)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2TextBox ThanhTimKiem;
        private Guna.UI2.WinForms.Guna2Button btnSua;
        private Guna.UI2.WinForms.Guna2Button btnXoa;
        private Guna.UI2.WinForms.Guna2Button themkhachhang;
        private Guna.UI2.WinForms.Guna2PictureBox microphone;
        private FlowLayoutPanel flowLayoutPanel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
    }
}
