namespace GUI
{
    partial class UC_TrangChu
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
            Guna.UI2.WinForms.Guna2HtmlLabel slogan4;
            Guna.UI2.WinForms.Guna2HtmlLabel slogan3;
            Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
            Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
            this.gradient_box_message = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            slogan4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            slogan3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.SuspendLayout();
            // 
            // slogan4
            // 
            slogan4.AutoSize = false;
            slogan4.BackColor = System.Drawing.Color.Transparent;
            slogan4.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            slogan4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(220)))), ((int)(((byte)(89)))));
            slogan4.Location = new System.Drawing.Point(589, 107);
            slogan4.Name = "slogan4";
            slogan4.Size = new System.Drawing.Size(221, 65);
            slogan4.TabIndex = 19;
            slogan4.TabStop = false;
            slogan4.Text = "thông minh!";
            // 
            // slogan3
            // 
            slogan3.AutoSize = false;
            slogan3.BackColor = System.Drawing.Color.Transparent;
            slogan3.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            slogan3.ForeColor = System.Drawing.Color.White;
            slogan3.Location = new System.Drawing.Point(394, 107);
            slogan3.Name = "slogan3";
            slogan3.Size = new System.Drawing.Size(293, 72);
            slogan3.TabIndex = 18;
            slogan3.TabStop = false;
            slogan3.Text = "Giải pháp...";
            slogan3.Click += new System.EventHandler(this.slogan3_Click);
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.AutoSize = false;
            guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            guna2HtmlLabel2.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            guna2HtmlLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(109)))), ((int)(((byte)(77)))));
            guna2HtmlLabel2.Location = new System.Drawing.Point(240, 32);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new System.Drawing.Size(236, 69);
            guna2HtmlLabel2.TabIndex = 17;
            guna2HtmlLabel2.TabStop = false;
            guna2HtmlLabel2.Text = "vững vàng,";
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.AutoSize = false;
            guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent;
            guna2HtmlLabel3.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            guna2HtmlLabel3.ForeColor = System.Drawing.Color.White;
            guna2HtmlLabel3.Location = new System.Drawing.Point(98, 32);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new System.Drawing.Size(215, 70);
            guna2HtmlLabel3.TabIndex = 16;
            guna2HtmlLabel3.TabStop = false;
            guna2HtmlLabel3.Text = "Gốc rễ...";
            // 
            // gradient_box_message
            // 
            this.gradient_box_message.AutoScroll = true;
            this.gradient_box_message.BackColor = System.Drawing.Color.Transparent;
            this.gradient_box_message.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.gradient_box_message.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gradient_box_message.FillColor = System.Drawing.Color.Black;
            this.gradient_box_message.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.gradient_box_message.FillColor3 = System.Drawing.Color.Transparent;
            this.gradient_box_message.FillColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.gradient_box_message.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gradient_box_message.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gradient_box_message.Location = new System.Drawing.Point(0, 188);
            this.gradient_box_message.Name = "gradient_box_message";
            this.gradient_box_message.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.gradient_box_message.Size = new System.Drawing.Size(890, 327);
            this.gradient_box_message.TabIndex = 20;
            this.gradient_box_message.Paint += new System.Windows.Forms.PaintEventHandler(this.gradient_box_message_Paint);
            // 
            // UC_TrangChu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.gradient_box_message);
            this.Controls.Add(slogan4);
            this.Controls.Add(slogan3);
            this.Controls.Add(guna2HtmlLabel2);
            this.Controls.Add(guna2HtmlLabel3);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UC_TrangChu";
            this.Size = new System.Drawing.Size(890, 515);
            this.Load += new System.EventHandler(this.UC_TrangChu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2CustomGradientPanel gradient_box_message;
    }
}
