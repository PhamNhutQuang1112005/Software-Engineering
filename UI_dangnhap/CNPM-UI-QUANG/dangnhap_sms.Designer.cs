namespace CNPM_UI_QUANG
{
    partial class dangnhap_sms
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
            this.sdt_email = new System.Windows.Forms.Label();
            this.phienban = new System.Windows.Forms.Label();
            this.btn_dang_nhap = new Guna.UI2.WinForms.Guna2Button();
            this.nhap_hoten_email = new Guna.UI2.WinForms.Guna2TextBox();
            this.text_chaomung = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.captcha = new System.Windows.Forms.WebBrowser();
            this.logo = new CNPM_UI_QUANG.CirclePictureBoxWithBorder();
            this.anh_minhhoa = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btn_config = new Guna.UI2.WinForms.Guna2CircleButton();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.anh_minhhoa)).BeginInit();
            this.SuspendLayout();
            // 
            // sdt_email
            // 
            this.sdt_email.AutoSize = true;
            this.sdt_email.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sdt_email.Location = new System.Drawing.Point(188, 205);
            this.sdt_email.Name = "sdt_email";
            this.sdt_email.Size = new System.Drawing.Size(123, 13);
            this.sdt_email.TabIndex = 1;
            this.sdt_email.Text = "Số điện thoại/Email:";
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
            this.btn_dang_nhap.Location = new System.Drawing.Point(257, 351);
            this.btn_dang_nhap.Margin = new System.Windows.Forms.Padding(2);
            this.btn_dang_nhap.Name = "btn_dang_nhap";
            this.btn_dang_nhap.Size = new System.Drawing.Size(135, 37);
            this.btn_dang_nhap.TabIndex = 5;
            this.btn_dang_nhap.Text = "Gửi SMS";
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
            this.nhap_hoten_email.Location = new System.Drawing.Point(191, 230);
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
            // captcha
            // 
            this.captcha.Location = new System.Drawing.Point(235, 298);
            this.captcha.MinimumSize = new System.Drawing.Size(20, 20);
            this.captcha.Name = "captcha";
            this.captcha.Size = new System.Drawing.Size(178, 36);
            this.captcha.TabIndex = 14;
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
            // 
            // dangnhap_sms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 463);
            this.Controls.Add(this.captcha);
            this.Controls.Add(this.logo);
            this.Controls.Add(this.anh_minhhoa);
            this.Controls.Add(this.text_chaomung);
            this.Controls.Add(this.nhap_hoten_email);
            this.Controls.Add(this.btn_dang_nhap);
            this.Controls.Add(this.btn_config);
            this.Controls.Add(this.phienban);
            this.Controls.Add(this.sdt_email);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "dangnhap_sms";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.anh_minhhoa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label sdt_email;
        private System.Windows.Forms.Label phienban;
        private Guna.UI2.WinForms.Guna2CircleButton btn_config;
        private Guna.UI2.WinForms.Guna2Button btn_dang_nhap;
        private Guna.UI2.WinForms.Guna2TextBox nhap_hoten_email;
        private Guna.UI2.WinForms.Guna2HtmlLabel text_chaomung;
        private Guna.UI2.WinForms.Guna2PictureBox anh_minhhoa;
        private CirclePictureBoxWithBorder logo;
        private System.Windows.Forms.WebBrowser captcha;
    }
}