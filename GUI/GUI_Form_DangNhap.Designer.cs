namespace GUI


{
    partial class GUI_Form_DangNhap
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.hoten_email = new System.Windows.Forms.Label();
            this.matkhau = new System.Windows.Forms.Label();
            this.phienban = new System.Windows.Forms.Label();
            this.btn_dang_nhap = new Guna.UI2.WinForms.Guna2Button();
            this.nhap_hoten_email = new Guna.UI2.WinForms.Guna2TextBox();
            this.text_chaomung = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.nhap_matkhau = new Guna.UI2.WinForms.Guna2TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.captcha = new System.Windows.Forms.WebBrowser();
            this.guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.view_password = new Guna.UI2.WinForms.Guna2PictureBox();
            this.hide_password = new Guna.UI2.WinForms.Guna2PictureBox();
            this.anh_minhhoa = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btn_config = new Guna.UI2.WinForms.Guna2CircleButton();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.view_password)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hide_password)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.anh_minhhoa)).BeginInit();
            this.SuspendLayout();
            // 
            // hoten_email
            // 
            this.hoten_email.AutoSize = true;
            this.hoten_email.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hoten_email.Location = new System.Drawing.Point(141, 271);
            this.hoten_email.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.hoten_email.Name = "hoten_email";
            this.hoten_email.Size = new System.Drawing.Size(105, 17);
            this.hoten_email.TabIndex = 1;
            this.hoten_email.Text = "Họ tên/Email:";
            this.hoten_email.Click += new System.EventHandler(this.hoten_email_Click);
            // 
            // matkhau
            // 
            this.matkhau.AutoSize = true;
            this.matkhau.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.matkhau.Location = new System.Drawing.Point(141, 362);
            this.matkhau.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.matkhau.Name = "matkhau";
            this.matkhau.Size = new System.Drawing.Size(79, 17);
            this.matkhau.TabIndex = 2;
            this.matkhau.Text = "Mật khẩu:";
            // 
            // phienban
            // 
            this.phienban.AutoSize = true;
            this.phienban.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phienban.Location = new System.Drawing.Point(13, 18);
            this.phienban.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.phienban.Name = "phienban";
            this.phienban.Size = new System.Drawing.Size(119, 17);
            this.phienban.TabIndex = 3;
            this.phienban.Text = "Version 1.1.0.2";
            // 
            // btn_dang_nhap
            // 
            this.btn_dang_nhap.BorderRadius = 10;
            this.btn_dang_nhap.BorderThickness = 1;
            this.btn_dang_nhap.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_dang_nhap.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_dang_nhap.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_dang_nhap.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_dang_nhap.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(220)))), ((int)(((byte)(89)))));
            this.btn_dang_nhap.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_dang_nhap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btn_dang_nhap.ForeColor = System.Drawing.Color.Black;
            this.btn_dang_nhap.Location = new System.Drawing.Point(232, 537);
            this.btn_dang_nhap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_dang_nhap.Name = "btn_dang_nhap";
            this.btn_dang_nhap.Size = new System.Drawing.Size(180, 46);
            this.btn_dang_nhap.TabIndex = 5;
            this.btn_dang_nhap.Text = "Đăng nhập";
            // 
            // nhap_hoten_email
            // 
            this.nhap_hoten_email.BorderColor = System.Drawing.Color.Black;
            this.nhap_hoten_email.BorderRadius = 10;
            this.nhap_hoten_email.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.nhap_hoten_email.DefaultText = "";
            this.nhap_hoten_email.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.nhap_hoten_email.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.nhap_hoten_email.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.nhap_hoten_email.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.nhap_hoten_email.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.nhap_hoten_email.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nhap_hoten_email.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.nhap_hoten_email.Location = new System.Drawing.Point(145, 302);
            this.nhap_hoten_email.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nhap_hoten_email.Name = "nhap_hoten_email";
            this.nhap_hoten_email.PlaceholderText = "";
            this.nhap_hoten_email.SelectedText = "";
            this.nhap_hoten_email.Size = new System.Drawing.Size(361, 44);
            this.nhap_hoten_email.TabIndex = 6;
            // 
            // text_chaomung
            // 
            this.text_chaomung.BackColor = System.Drawing.Color.Transparent;
            this.text_chaomung.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_chaomung.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(124)))), ((int)(((byte)(23)))));
            this.text_chaomung.Location = new System.Drawing.Point(169, 207);
            this.text_chaomung.Margin = new System.Windows.Forms.Padding(4);
            this.text_chaomung.Name = "text_chaomung";
            this.text_chaomung.Size = new System.Drawing.Size(295, 40);
            this.text_chaomung.TabIndex = 8;
            this.text_chaomung.Text = "CHÀO MỪNG BẠN!";
            // 
            // nhap_matkhau
            // 
            this.nhap_matkhau.BorderColor = System.Drawing.Color.Black;
            this.nhap_matkhau.BorderRadius = 10;
            this.nhap_matkhau.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.nhap_matkhau.DefaultText = "";
            this.nhap_matkhau.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.nhap_matkhau.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.nhap_matkhau.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.nhap_matkhau.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.nhap_matkhau.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.nhap_matkhau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nhap_matkhau.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.nhap_matkhau.Location = new System.Drawing.Point(145, 393);
            this.nhap_matkhau.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nhap_matkhau.Name = "nhap_matkhau";
            this.nhap_matkhau.PlaceholderText = "";
            this.nhap_matkhau.SelectedText = "";
            this.nhap_matkhau.Size = new System.Drawing.Size(361, 44);
            this.nhap_matkhau.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(393, 441);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "Quên mật khẩu?";
            // 
            // captcha
            // 
            this.captcha.Location = new System.Drawing.Point(202, 472);
            this.captcha.Margin = new System.Windows.Forms.Padding(4);
            this.captcha.MinimumSize = new System.Drawing.Size(27, 25);
            this.captcha.Name = "captcha";
            this.captcha.Size = new System.Drawing.Size(237, 44);
            this.captcha.TabIndex = 14;
            // 
            // guna2CirclePictureBox1
            // 
            this.guna2CirclePictureBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2CirclePictureBox1.Image = global::GUI.Properties.Resources.Green_Sol;
            this.guna2CirclePictureBox1.ImageRotate = 0F;
            this.guna2CirclePictureBox1.Location = new System.Drawing.Point(244, 70);
            this.guna2CirclePictureBox1.Name = "guna2CirclePictureBox1";
            this.guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CirclePictureBox1.Size = new System.Drawing.Size(130, 130);
            this.guna2CirclePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.guna2CirclePictureBox1.TabIndex = 17;
            this.guna2CirclePictureBox1.TabStop = false;
            this.guna2CirclePictureBox1.Click += new System.EventHandler(this.guna2CirclePictureBox1_Click_2);
            // 
            // view_password
            // 
            this.view_password.BackColor = System.Drawing.Color.White;
            this.view_password.FillColor = System.Drawing.Color.Transparent;
            this.view_password.Image = global::GUI.Properties.Resources.view;
            this.view_password.ImageRotate = 0F;
            this.view_password.Location = new System.Drawing.Point(469, 403);
            this.view_password.Margin = new System.Windows.Forms.Padding(4);
            this.view_password.Name = "view_password";
            this.view_password.Size = new System.Drawing.Size(27, 26);
            this.view_password.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.view_password.TabIndex = 16;
            this.view_password.TabStop = false;
            // 
            // hide_password
            // 
            this.hide_password.BackColor = System.Drawing.Color.White;
            this.hide_password.FillColor = System.Drawing.Color.Transparent;
            this.hide_password.Image = global::GUI.Properties.Resources.hide;
            this.hide_password.ImageRotate = 0F;
            this.hide_password.Location = new System.Drawing.Point(469, 403);
            this.hide_password.Margin = new System.Windows.Forms.Padding(4);
            this.hide_password.Name = "hide_password";
            this.hide_password.Size = new System.Drawing.Size(27, 26);
            this.hide_password.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.hide_password.TabIndex = 15;
            this.hide_password.TabStop = false;
            // 
            // anh_minhhoa
            // 
            this.anh_minhhoa.BackColor = System.Drawing.Color.Transparent;
            this.anh_minhhoa.Dock = System.Windows.Forms.DockStyle.Right;
            this.anh_minhhoa.FillColor = System.Drawing.Color.Transparent;
            this.anh_minhhoa.Image = global::GUI.Properties.Resources.Untitled_image;
            this.anh_minhhoa.ImageRotate = 0F;
            this.anh_minhhoa.Location = new System.Drawing.Point(536, 0);
            this.anh_minhhoa.Margin = new System.Windows.Forms.Padding(4);
            this.anh_minhhoa.Name = "anh_minhhoa";
            this.anh_minhhoa.Size = new System.Drawing.Size(502, 588);
            this.anh_minhhoa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.anh_minhhoa.TabIndex = 11;
            this.anh_minhhoa.TabStop = false;
            // 
            // btn_config
            // 
            this.btn_config.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_config.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_config.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_config.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_config.FillColor = System.Drawing.Color.Transparent;
            this.btn_config.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_config.ForeColor = System.Drawing.Color.White;
            this.btn_config.Image = global::GUI.Properties.Resources.cog_wheel;
            this.btn_config.Location = new System.Drawing.Point(145, 7);
            this.btn_config.Margin = new System.Windows.Forms.Padding(4);
            this.btn_config.Name = "btn_config";
            this.btn_config.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btn_config.Size = new System.Drawing.Size(51, 38);
            this.btn_config.TabIndex = 4;
            this.btn_config.Click += new System.EventHandler(this.guna2CircleButton1_Click);
            // 
            // GUI_Form_DangNhap
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1038, 588);
            this.Controls.Add(this.guna2CirclePictureBox1);
            this.Controls.Add(this.view_password);
            this.Controls.Add(this.hide_password);
            this.Controls.Add(this.captcha);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.anh_minhhoa);
            this.Controls.Add(this.nhap_matkhau);
            this.Controls.Add(this.text_chaomung);
            this.Controls.Add(this.nhap_hoten_email);
            this.Controls.Add(this.btn_dang_nhap);
            this.Controls.Add(this.btn_config);
            this.Controls.Add(this.phienban);
            this.Controls.Add(this.matkhau);
            this.Controls.Add(this.hoten_email);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "GUI_Form_DangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.view_password)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hide_password)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.anh_minhhoa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label hoten_email;
        private System.Windows.Forms.Label matkhau;
        private System.Windows.Forms.Label phienban;
        private Guna.UI2.WinForms.Guna2CircleButton btn_config;
        private Guna.UI2.WinForms.Guna2Button btn_dang_nhap;
        private Guna.UI2.WinForms.Guna2TextBox nhap_hoten_email;
        private Guna.UI2.WinForms.Guna2HtmlLabel text_chaomung;
        private Guna.UI2.WinForms.Guna2TextBox nhap_matkhau;
        private Guna.UI2.WinForms.Guna2PictureBox anh_minhhoa;
        
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.WebBrowser captcha;
        private Guna.UI2.WinForms.Guna2PictureBox hide_password;
        private Guna.UI2.WinForms.Guna2PictureBox view_password;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
    }
}

