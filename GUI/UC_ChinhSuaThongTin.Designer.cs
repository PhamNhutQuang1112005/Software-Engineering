using System.Windows.Forms;

namespace GUI
{
    partial class UC_ChinhSuaThongTin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        private void InitializeComponent()
        {
            this.txtHoTen = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtSDT = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMatKhauCu = new Guna.UI2.WinForms.Guna2TextBox();
            this.comfirm_change = new Guna.UI2.WinForms.Guna2Button();
            this.decline_change = new Guna.UI2.WinForms.Guna2Button();
            this.display_name = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.phong_display = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.phong_label = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2CirclePictureBox2 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMatKhauMoi = new Guna.UI2.WinForms.Guna2TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtHoTen
            // 
            this.txtHoTen.BorderRadius = 10;
            this.txtHoTen.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtHoTen.DefaultText = "";
            this.txtHoTen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtHoTen.Location = new System.Drawing.Point(157, 270);
            this.txtHoTen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.PlaceholderText = "";
            this.txtHoTen.SelectedText = "";
            this.txtHoTen.Size = new System.Drawing.Size(230, 30);
            this.txtHoTen.TabIndex = 0;
            // 
            // txtSDT
            // 
            this.txtSDT.BorderRadius = 10;
            this.txtSDT.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSDT.DefaultText = "";
            this.txtSDT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSDT.Location = new System.Drawing.Point(157, 320);
            this.txtSDT.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.PlaceholderText = "";
            this.txtSDT.SelectedText = "";
            this.txtSDT.Size = new System.Drawing.Size(230, 30);
            this.txtSDT.TabIndex = 1;
            // 
            // txtEmail
            // 
            this.txtEmail.BorderRadius = 10;
            this.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEmail.DefaultText = "";
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEmail.Location = new System.Drawing.Point(677, 270);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "";
            this.txtEmail.SelectedText = "";
            this.txtEmail.Size = new System.Drawing.Size(230, 30);
            this.txtEmail.TabIndex = 3;
            // 
            // txtMatKhauCu
            // 
            this.txtMatKhauCu.BorderRadius = 10;
            this.txtMatKhauCu.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMatKhauCu.DefaultText = "";
            this.txtMatKhauCu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMatKhauCu.Location = new System.Drawing.Point(677, 320);
            this.txtMatKhauCu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMatKhauCu.Name = "txtMatKhauCu";
            this.txtMatKhauCu.PasswordChar = '*';
            this.txtMatKhauCu.PlaceholderText = "";
            this.txtMatKhauCu.SelectedText = "";
            this.txtMatKhauCu.Size = new System.Drawing.Size(230, 30);
            this.txtMatKhauCu.TabIndex = 4;
            // 
            // comfirm_change
            // 
            this.comfirm_change.BorderRadius = 15;
            this.comfirm_change.FillColor = System.Drawing.Color.SeaGreen;
            this.comfirm_change.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.comfirm_change.ForeColor = System.Drawing.Color.White;
            this.comfirm_change.Location = new System.Drawing.Point(327, 440);
            this.comfirm_change.Name = "comfirm_change";
            this.comfirm_change.Size = new System.Drawing.Size(120, 40);
            this.comfirm_change.TabIndex = 6;
            this.comfirm_change.Text = "Thay đổi";
            this.comfirm_change.Click += new System.EventHandler(this.comfirm_change_Click);
            // 
            // decline_change
            // 
            this.decline_change.BorderRadius = 15;
            this.decline_change.FillColor = System.Drawing.Color.SeaGreen;
            this.decline_change.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.decline_change.ForeColor = System.Drawing.Color.White;
            this.decline_change.Location = new System.Drawing.Point(527, 440);
            this.decline_change.Name = "decline_change";
            this.decline_change.Size = new System.Drawing.Size(120, 40);
            this.decline_change.TabIndex = 7;
            this.decline_change.Text = "Hủy";
            this.decline_change.Click += new System.EventHandler(this.decline_change_Click);
            // 
            // display_name
            // 
            this.display_name.BackColor = System.Drawing.Color.Transparent;
            this.display_name.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.display_name.ForeColor = System.Drawing.Color.White;
            this.display_name.Location = new System.Drawing.Point(407, 180);
            this.display_name.Name = "display_name";
            this.display_name.Size = new System.Drawing.Size(154, 25);
            this.display_name.TabIndex = 8;
            this.display_name.Text = "Nguyễn Văn Thành";
            // 
            // phong_display
            // 
            this.phong_display.BackColor = System.Drawing.Color.Transparent;
            this.phong_display.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.phong_display.ForeColor = System.Drawing.Color.White;
            this.phong_display.Location = new System.Drawing.Point(490, 210);
            this.phong_display.Name = "phong_display";
            this.phong_display.Size = new System.Drawing.Size(76, 22);
            this.phong_display.TabIndex = 10;
            this.phong_display.Text = "kinh doanh";
            this.phong_display.Click += new System.EventHandler(this.phong_display_Click);
            // 
            // phong_label
            // 
            this.phong_label.BackColor = System.Drawing.Color.Transparent;
            this.phong_label.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.phong_label.ForeColor = System.Drawing.Color.White;
            this.phong_label.Location = new System.Drawing.Point(357, 210);
            this.phong_label.Name = "phong_label";
            this.phong_label.Size = new System.Drawing.Size(127, 22);
            this.phong_label.TabIndex = 9;
            this.phong_label.Text = "Nhân viên phòng:";
            // 
            // guna2CirclePictureBox2
            // 
            this.guna2CirclePictureBox2.ImageRotate = 0F;
            this.guna2CirclePictureBox2.Location = new System.Drawing.Point(437, 40);
            this.guna2CirclePictureBox2.Name = "guna2CirclePictureBox2";
            this.guna2CirclePictureBox2.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CirclePictureBox2.Size = new System.Drawing.Size(90, 90);
            this.guna2CirclePictureBox2.TabIndex = 11;
            this.guna2CirclePictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(76, 278);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 22);
            this.label1.TabIndex = 12;
            this.label1.Text = "Họ Tên:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(76, 328);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 22);
            this.label2.TabIndex = 18;
            this.label2.Text = "SĐT:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(539, 278);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 22);
            this.label3.TabIndex = 19;
            this.label3.Text = "Email:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtMatKhauMoi
            // 
            this.txtMatKhauMoi.BorderRadius = 10;
            this.txtMatKhauMoi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMatKhauMoi.DefaultText = "";
            this.txtMatKhauMoi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMatKhauMoi.Location = new System.Drawing.Point(677, 370);
            this.txtMatKhauMoi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMatKhauMoi.Name = "txtMatKhauMoi";
            this.txtMatKhauMoi.PasswordChar = '*';
            this.txtMatKhauMoi.PlaceholderText = "";
            this.txtMatKhauMoi.SelectedText = "";
            this.txtMatKhauMoi.Size = new System.Drawing.Size(230, 30);
            this.txtMatKhauMoi.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(539, 328);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 22);
            this.label5.TabIndex = 20;
            this.label5.Text = "Mật khẩu cũ:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(539, 378);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 22);
            this.label4.TabIndex = 21;
            this.label4.Text = "Mật khẩu mới:";
            // 
            // UC_ChinhSuaThongTin
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.txtSDT);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtMatKhauCu);
            this.Controls.Add(this.txtMatKhauMoi);
            this.Controls.Add(this.comfirm_change);
            this.Controls.Add(this.decline_change);
            this.Controls.Add(this.display_name);
            this.Controls.Add(this.phong_label);
            this.Controls.Add(this.phong_display);
            this.Controls.Add(this.guna2CirclePictureBox2);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Name = "UC_ChinhSuaThongTin";
            this.Size = new System.Drawing.Size(983, 571);
            this.Load += new System.EventHandler(this.UC_ChinhSuaThongTin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox txtHoTen;
        private Guna.UI2.WinForms.Guna2TextBox txtSDT;
        private Guna.UI2.WinForms.Guna2TextBox txtEmail;
        private Guna.UI2.WinForms.Guna2TextBox txtMatKhauCu;
        private Guna.UI2.WinForms.Guna2Button comfirm_change;
        private Guna.UI2.WinForms.Guna2Button decline_change;
        private Guna.UI2.WinForms.Guna2HtmlLabel display_name;
        private Guna.UI2.WinForms.Guna2HtmlLabel phong_display;
        private Guna.UI2.WinForms.Guna2HtmlLabel phong_label;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Guna.UI2.WinForms.Guna2TextBox txtMatKhauMoi;
        private Label label5;
        private Label label4;
    }
}
