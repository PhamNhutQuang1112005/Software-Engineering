namespace GUI
{
    partial class UC_PopupThongBao
    {
        private System.ComponentModel.IContainer components = null;
        private Guna.UI2.WinForms.Guna2Button btnSapHetHan;
        private Guna.UI2.WinForms.Guna2Button btnQuaHan;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region InitializeComponent
        private void InitializeComponent()
        {
            this.btnSapHetHan = new Guna.UI2.WinForms.Guna2Button();
            this.btnQuaHan = new Guna.UI2.WinForms.Guna2Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ThanhTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.SuspendLayout();
            // 
            // btnSapHetHan
            // 
            this.btnSapHetHan.BorderRadius = 10;
            this.btnSapHetHan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(130)))), ((int)(((byte)(90)))));
            this.btnSapHetHan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSapHetHan.ForeColor = System.Drawing.Color.White;
            this.btnSapHetHan.Location = new System.Drawing.Point(10, 10);
            this.btnSapHetHan.Name = "btnSapHetHan";
            this.btnSapHetHan.Size = new System.Drawing.Size(211, 40);
            this.btnSapHetHan.TabIndex = 0;
            this.btnSapHetHan.Text = "Đơn hàng sắp hết hạn ";
            this.btnSapHetHan.Click += new System.EventHandler(this.btnSapHetHan_Click);
            // 
            // btnQuaHan
            // 
            this.btnQuaHan.BorderRadius = 10;
            this.btnQuaHan.FillColor = System.Drawing.Color.Transparent;
            this.btnQuaHan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnQuaHan.ForeColor = System.Drawing.Color.White;
            this.btnQuaHan.Location = new System.Drawing.Point(227, 10);
            this.btnQuaHan.Name = "btnQuaHan";
            this.btnQuaHan.Size = new System.Drawing.Size(199, 40);
            this.btnQuaHan.TabIndex = 1;
            this.btnQuaHan.Text = "Đơn hàng quá hạn";
            this.btnQuaHan.Click += new System.EventHandler(this.btnQuaHan_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(10, 60);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(416, 241);
            this.flowLayoutPanel1.TabIndex = 2;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
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
            this.ThanhTimKiem.Location = new System.Drawing.Point(50, 322);
            this.ThanhTimKiem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ThanhTimKiem.Name = "ThanhTimKiem";
            this.ThanhTimKiem.PlaceholderForeColor = System.Drawing.Color.Black;
            this.ThanhTimKiem.PlaceholderText = "Tìm kiếm theo tên đơn hàng...";
            this.ThanhTimKiem.SelectedText = "";
            this.ThanhTimKiem.Size = new System.Drawing.Size(327, 36);
            this.ThanhTimKiem.TabIndex = 46;
            this.ThanhTimKiem.TextChanged += new System.EventHandler(this.ThanhTimKiem_TextChanged);
            // 
            // UC_PopupThongBao
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.ThanhTimKiem);
            this.Controls.Add(this.btnSapHetHan);
            this.Controls.Add(this.btnQuaHan);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "UC_PopupThongBao";
            this.Size = new System.Drawing.Size(436, 386);
            this.Load += new System.EventHandler(this.UC_PopupThongBao_Load);
            this.ResumeLayout(false);

        }
        #endregion

        private Guna.UI2.WinForms.Guna2TextBox ThanhTimKiem;
    }
}
