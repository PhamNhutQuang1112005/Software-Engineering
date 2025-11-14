using System.Windows.Forms;

namespace GUI
{
    partial class UC_solarisAI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_solarisAI));
            this.anhsolarisAI = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.ohoithoai = new Guna.UI2.WinForms.Guna2TextBox();
            this.AI_respond = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2TextBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.anhsolarisAI)).BeginInit();
            this.SuspendLayout();
            // 
            // anhsolarisAI
            // 
            this.anhsolarisAI.Image = global::GUI.Properties.Resources.sun_15710;
            this.anhsolarisAI.ImageRotate = 0F;
            this.anhsolarisAI.Location = new System.Drawing.Point(111, 61);
            this.anhsolarisAI.Name = "anhsolarisAI";
            this.anhsolarisAI.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.anhsolarisAI.Size = new System.Drawing.Size(78, 74);
            this.anhsolarisAI.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.anhsolarisAI.TabIndex = 0;
            this.anhsolarisAI.TabStop = false;
            // 
            // ohoithoai
            // 
            this.ohoithoai.BackColor = System.Drawing.Color.Transparent;
            this.ohoithoai.BorderColor = System.Drawing.Color.Black;
            this.ohoithoai.BorderRadius = 15;
            this.ohoithoai.BorderThickness = 2;
            this.ohoithoai.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ohoithoai.DefaultText = "";
            this.ohoithoai.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.ohoithoai.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ohoithoai.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ohoithoai.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ohoithoai.FillColor = System.Drawing.Color.WhiteSmoke;
            this.ohoithoai.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ohoithoai.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ohoithoai.ForeColor = System.Drawing.Color.Black;
            this.ohoithoai.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ohoithoai.Location = new System.Drawing.Point(179, 373);
            this.ohoithoai.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ohoithoai.Name = "ohoithoai";
            this.ohoithoai.PlaceholderForeColor = System.Drawing.Color.Black;
            this.ohoithoai.PlaceholderText = "Bạn cần gì? Hãy hỏi Solaris nhé!";
            this.ohoithoai.SelectedText = "";
            this.ohoithoai.Size = new System.Drawing.Size(605, 72);
            this.ohoithoai.TabIndex = 40;
            // 
            // AI_respond
            // 
            this.AI_respond.BackColor = System.Drawing.Color.Transparent;
            this.AI_respond.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AI_respond.ForeColor = System.Drawing.Color.White;
            this.AI_respond.Location = new System.Drawing.Point(195, 84);
            this.AI_respond.Name = "AI_respond";
            this.AI_respond.Size = new System.Drawing.Size(592, 37);
            this.AI_respond.TabIndex = 41;
            this.AI_respond.Text = "Xin chào! Tôi là Solaris, tôi có thể giúp gì cho bạn?";
            // 
            // guna2TextBox1
            // 
            this.guna2TextBox1.BorderRadius = 20;
            this.guna2TextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox1.DefaultText = "";
            this.guna2TextBox1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2TextBox1.ForeColor = System.Drawing.Color.Black;
            this.guna2TextBox1.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox1.Location = new System.Drawing.Point(179, 137);
            this.guna2TextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2TextBox1.Multiline = true;
            this.guna2TextBox1.Name = "guna2TextBox1";
            this.guna2TextBox1.PlaceholderText = "";
            this.guna2TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.guna2TextBox1.SelectedText = "";
            this.guna2TextBox1.Size = new System.Drawing.Size(605, 201);
            this.guna2TextBox1.TabIndex = 47;
            // 
            // guna2Button1
            // 
            this.guna2Button1.BackColor = System.Drawing.Color.White;
            this.guna2Button1.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.guna2Button1.BorderRadius = 25;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.White;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button1.Image")));
            this.guna2Button1.ImageSize = new System.Drawing.Size(50, 50);
            this.guna2Button1.Location = new System.Drawing.Point(714, 382);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(49, 48);
            this.guna2Button1.TabIndex = 48;
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // UC_solarisAI
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.guna2TextBox1);
            this.Controls.Add(this.AI_respond);
            this.Controls.Add(this.ohoithoai);
            this.Controls.Add(this.anhsolarisAI);
            this.Name = "UC_solarisAI";
            this.Size = new System.Drawing.Size(822, 460);
            ((System.ComponentModel.ISupportInitialize)(this.anhsolarisAI)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2CirclePictureBox anhsolarisAI;
        private Guna.UI2.WinForms.Guna2TextBox ohoithoai;
        private Guna.UI2.WinForms.Guna2HtmlLabel AI_respond;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox1;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
    }
}
