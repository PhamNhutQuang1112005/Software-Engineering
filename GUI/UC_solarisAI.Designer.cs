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
            this.anhsolarisAI = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.ohoithoai = new Guna.UI2.WinForms.Guna2TextBox();
            this.AI_respond = new Guna.UI2.WinForms.Guna2HtmlLabel();
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
            this.ohoithoai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ohoithoai.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ohoithoai.Location = new System.Drawing.Point(195, 364);
            this.ohoithoai.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ohoithoai.Name = "ohoithoai";
            this.ohoithoai.PlaceholderForeColor = System.Drawing.Color.Black;
            this.ohoithoai.PlaceholderText = "Bạn cần gì? Hãy hỏi Solaris nhé!";
            this.ohoithoai.SelectedText = "";
            this.ohoithoai.Size = new System.Drawing.Size(605, 56);
            this.ohoithoai.TabIndex = 40;
            // 
            // AI_respond
            // 
            this.AI_respond.BackColor = System.Drawing.Color.Transparent;
            this.AI_respond.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.AI_respond.ForeColor = System.Drawing.Color.White;
            this.AI_respond.Location = new System.Drawing.Point(195, 84);
            this.AI_respond.Name = "AI_respond";
            this.AI_respond.Size = new System.Drawing.Size(408, 25);
            this.AI_respond.TabIndex = 41;
            this.AI_respond.Text = "Xin chào! Tôi là Solaris, tôi có thể giúp gì cho bạn?";
            // 
            // UC_solarisAI
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.AI_respond);
            this.Controls.Add(this.ohoithoai);
            this.Controls.Add(this.anhsolarisAI);
            this.Name = "UC_solarisAI";
            this.Size = new System.Drawing.Size(800, 450);
            ((System.ComponentModel.ISupportInitialize)(this.anhsolarisAI)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2CirclePictureBox anhsolarisAI;
        private Guna.UI2.WinForms.Guna2TextBox ohoithoai;
        private Guna.UI2.WinForms.Guna2HtmlLabel AI_respond;
    }
}
