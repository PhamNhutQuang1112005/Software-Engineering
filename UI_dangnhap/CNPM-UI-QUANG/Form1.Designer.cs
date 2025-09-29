namespace CNPM_UI_QUANG

{
    partial class Form1
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
            this.btndangnhap = new System.Windows.Forms.Button();
            this.hoten_email = new System.Windows.Forms.Label();
            this.matkhau = new System.Windows.Forms.Label();
            this.phienban = new System.Windows.Forms.Label();
            this.guna2CircleButton1 = new Guna.UI2.WinForms.Guna2CircleButton();
            this.SuspendLayout();
            // 
            // btndangnhap
            // 
            this.btndangnhap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(220)))), ((int)(((byte)(89)))));
            this.btndangnhap.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btndangnhap.FlatAppearance.BorderSize = 50;
            this.btndangnhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndangnhap.Location = new System.Drawing.Point(152, 346);
            this.btndangnhap.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btndangnhap.Name = "btndangnhap";
            this.btndangnhap.Size = new System.Drawing.Size(110, 39);
            this.btndangnhap.TabIndex = 0;
            this.btndangnhap.Text = "Đăng nhập";
            this.btndangnhap.UseVisualStyleBackColor = false;
            this.btndangnhap.Click += new System.EventHandler(this.button1_Click);
            this.btndangnhap.Paint += new System.Windows.Forms.PaintEventHandler(this.button1_Paint);
            // 
            // hoten_email
            // 
            this.hoten_email.AutoSize = true;
            this.hoten_email.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hoten_email.Location = new System.Drawing.Point(177, 147);
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
            this.matkhau.Location = new System.Drawing.Point(181, 256);
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
            // guna2CircleButton1
            // 
            this.guna2CircleButton1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2CircleButton1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2CircleButton1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2CircleButton1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2CircleButton1.FillColor = System.Drawing.Color.Transparent;
            this.guna2CircleButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2CircleButton1.ForeColor = System.Drawing.Color.White;
            this.guna2CircleButton1.Image = global::CNPM_UI_QUANG.Properties.Resources.cog_wheel;
            this.guna2CircleButton1.ImageSize = new System.Drawing.Size(50, 50);
            this.guna2CircleButton1.Location = new System.Drawing.Point(132, 1);
            this.guna2CircleButton1.Name = "guna2CircleButton1";
            this.guna2CircleButton1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CircleButton1.Size = new System.Drawing.Size(82, 60);
            this.guna2CircleButton1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 463);
            this.Controls.Add(this.guna2CircleButton1);
            this.Controls.Add(this.phienban);
            this.Controls.Add(this.matkhau);
            this.Controls.Add(this.hoten_email);
            this.Controls.Add(this.btndangnhap);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btndangnhap;
        private System.Windows.Forms.Label hoten_email;
        private System.Windows.Forms.Label matkhau;
        private System.Windows.Forms.Label phienban;
        private Guna.UI2.WinForms.Guna2CircleButton guna2CircleButton1;
    }
}

