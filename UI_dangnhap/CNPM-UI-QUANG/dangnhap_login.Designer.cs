namespace CNPM_UI_QUANG

{
    partial class dangnhap_login
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
            this.view_password = new Guna.UI2.WinForms.Guna2PictureBox();
            this.hide_password = new Guna.UI2.WinForms.Guna2PictureBox();
            this.logo = new CNPM_UI_QUANG.CirclePictureBoxWithBorder();
            this.anh_minhhoa = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btn_config = new Guna.UI2.WinForms.Guna2CircleButton();
            ((System.ComponentModel.ISupportInitialize)(this.view_password)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hide_password)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.anh_minhhoa)).BeginInit();
            this.SuspendLayout();
            // 
            // hoten_email
            // 
            this.hoten_email.AutoSize = true;
            this.hoten_email.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hoten_email.Location = new System.Drawing.Point(189, 178);
            this.hoten_email.Name = "hoten_email";
            this.hoten_email.Size = new System.Drawing.Size(85, 13);
            this.hoten_email.TabIndex = 1;
            this.hoten_email.Text = "Họ tên/Email:";
            this.hoten_email.Click += new System.EventHandler(this.hoten_email_Click);
            // 
            // matkhau
            // 
            this.matkhau.AutoSize = true;
            this.matkhau.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.matkhau.Location = new System.Drawing.Point(189, 252);
            this.matkhau.Name = "matkhau";
            this.matkhau.Size = new System.Drawing.Size(64, 13);
            this.matkhau.TabIndex = 2;
            this.matkhau.Text = "Mật khẩu:";
            // 
            // phienban
            // 
            this.phienban.AutoSize = true;
            this.phienban.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phienban.Location = new System.Drawing.Point(33, 30);
            this.phienban.Name = "phienban";
            this.phienban.Size = new System.Drawing.Size(93, 13);
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
            this.btn_dang_nhap.Location = new System.Drawing.Point(257, 394);
            this.btn_dang_nhap.Margin = new System.Windows.Forms.Padding(2);
            this.btn_dang_nhap.Name = "btn_dang_nhap";
            this.btn_dang_nhap.Size = new System.Drawing.Size(135, 37);
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
            this.nhap_hoten_email.Location = new System.Drawing.Point(192, 203);
            this.nhap_hoten_email.Name = "nhap_hoten_email";
            this.nhap_hoten_email.PlaceholderText = "";
            this.nhap_hoten_email.SelectedText = "";
            this.nhap_hoten_email.Size = new System.Drawing.Size(271, 36);
            this.nhap_hoten_email.TabIndex = 6;
            // 
            // text_chaomung
            // 
            this.text_chaomung.BackColor = System.Drawing.Color.Transparent;
            this.text_chaomung.Font = new System.Drawing.Font("SVN-A Love Of Thunder", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_chaomung.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(124)))), ((int)(((byte)(23)))));
            this.text_chaomung.Location = new System.Drawing.Point(210, 126);
            this.text_chaomung.Name = "text_chaomung";
            this.text_chaomung.Size = new System.Drawing.Size(238, 40);
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
            this.nhap_matkhau.Location = new System.Drawing.Point(192, 277);
            this.nhap_matkhau.Name = "nhap_matkhau";
            this.nhap_matkhau.PlaceholderText = "";
            this.nhap_matkhau.SelectedText = "";
            this.nhap_matkhau.Size = new System.Drawing.Size(271, 36);
            this.nhap_matkhau.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(378, 316);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Quên mật khẩu?";
            // 
            // captcha
            // 
            this.captcha.Location = new System.Drawing.Point(235, 341);
            this.captcha.MinimumSize = new System.Drawing.Size(20, 20);
            this.captcha.Name = "captcha";
            this.captcha.Size = new System.Drawing.Size(178, 36);
            this.captcha.TabIndex = 14;
            // 
            // view_password
            // 
            this.view_password.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.view_password.BackColor = System.Drawing.Color.White;
            this.view_password.FillColor = System.Drawing.Color.Transparent;
            this.view_password.Image = global::CNPM_UI_QUANG.Properties.Resources.view;
            this.view_password.ImageRotate = 0F;
            this.view_password.Location = new System.Drawing.Point(435, 285);
            this.view_password.Name = "view_password";
            this.view_password.Size = new System.Drawing.Size(20, 21);
            this.view_password.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.view_password.TabIndex = 16;
            this.view_password.TabStop = false;
            // 
            // hide_password
            // 
            this.hide_password.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.hide_password.BackColor = System.Drawing.Color.White;
            this.hide_password.FillColor = System.Drawing.Color.Transparent;
            this.hide_password.Image = global::CNPM_UI_QUANG.Properties.Resources.hide;
            this.hide_password.ImageRotate = 0F;
            this.hide_password.Location = new System.Drawing.Point(435, 285);
            this.hide_password.Name = "hide_password";
            this.hide_password.Size = new System.Drawing.Size(20, 21);
            this.hide_password.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.hide_password.TabIndex = 15;
            this.hide_password.TabStop = false;
            // 
            // logo
            // 
            this.logo.BorderColor = System.Drawing.Color.Black;
            this.logo.BorderThickness = 1;
            this.logo.Image = global::CNPM_UI_QUANG.Properties.Resources.Green_Sol;
            this.logo.ImageRotate = 0F;
            this.logo.Location = new System.Drawing.Point(285, 42);
            this.logo.Name = "logo";
            this.logo.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.logo.Size = new System.Drawing.Size(82, 78);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logo.TabIndex = 12;
            this.logo.TabStop = false;
            // 
            // anh_minhhoa
            // 
            this.anh_minhhoa.BackColor = System.Drawing.Color.Transparent;
            this.anh_minhhoa.FillColor = System.Drawing.Color.Transparent;
            this.anh_minhhoa.Image = global::CNPM_UI_QUANG.Properties.Resources.Untitled_image;
            this.anh_minhhoa.ImageRotate = 0F;
            this.anh_minhhoa.Location = new System.Drawing.Point(582, 3);
            this.anh_minhhoa.Name = "anh_minhhoa";
            this.anh_minhhoa.Size = new System.Drawing.Size(573, 467);
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
            this.btn_config.Image = global::CNPM_UI_QUANG.Properties.Resources.cog_wheel;
            this.btn_config.Location = new System.Drawing.Point(132, 21);
            this.btn_config.Name = "btn_config";
            this.btn_config.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btn_config.Size = new System.Drawing.Size(38, 31);
            this.btn_config.TabIndex = 4;
            this.btn_config.Click += new System.EventHandler(this.guna2CircleButton1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 463);
            this.Controls.Add(this.view_password);
            this.Controls.Add(this.hide_password);
            this.Controls.Add(this.captcha);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logo);
            this.Controls.Add(this.anh_minhhoa);
            this.Controls.Add(this.nhap_matkhau);
            this.Controls.Add(this.text_chaomung);
            this.Controls.Add(this.nhap_hoten_email);
            this.Controls.Add(this.btn_dang_nhap);
            this.Controls.Add(this.btn_config);
            this.Controls.Add(this.phienban);
            this.Controls.Add(this.matkhau);
            this.Controls.Add(this.hoten_email);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.view_password)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hide_password)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
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
        private CirclePictureBoxWithBorder logo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.WebBrowser captcha;
        private Guna.UI2.WinForms.Guna2PictureBox hide_password;
        private Guna.UI2.WinForms.Guna2PictureBox view_password;
    }
}

