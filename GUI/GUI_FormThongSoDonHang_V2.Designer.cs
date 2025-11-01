namespace GUI
{
    partial class GUI_FormThongSoDonHang_V2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Guna.UI2.WinForms.Guna2CustomGradientPanel headerPanel;
        private System.Windows.Forms.Label lblTieuDe;
        private Guna.UI2.WinForms.Guna2Button btnThemChiTieu;

        private Guna.UI2.WinForms.Guna2Panel filterPanel;
        private System.Windows.Forms.Label lblLCT;
        private System.Windows.Forms.Label lblDV;
        private System.Windows.Forms.Label lblLPT;
        private System.Windows.Forms.Label lblNPT;

        private Guna.UI2.WinForms.Guna2ComboBox cboLoaiChiTieu;
        private Guna.UI2.WinForms.Guna2ComboBox cboDonVi;
        private Guna.UI2.WinForms.Guna2ComboBox cboLoaiPhanTich;
        private Guna.UI2.WinForms.Guna2ComboBox cboNguoiPhuTrach;

        private Guna.UI2.WinForms.Guna2Panel cardPanel;
        private System.Windows.Forms.Label sectionTitle;
        private System.Windows.Forms.TabControl tabViTri;

        private Guna.UI2.WinForms.Guna2Button btnLuuThayDoi;
        private Guna.UI2.WinForms.Guna2Button btnXoa;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.headerPanel = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.btnThemChiTieu = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.btnLuuThayDoi = new Guna.UI2.WinForms.Guna2Button();
            this.filterPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.lblLCT = new System.Windows.Forms.Label();
            this.lblDV = new System.Windows.Forms.Label();
            this.lblLPT = new System.Windows.Forms.Label();
            this.lblNPT = new System.Windows.Forms.Label();
            this.cboLoaiChiTieu = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cboDonVi = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cboLoaiPhanTich = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cboNguoiPhuTrach = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cardPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.sectionTitle = new System.Windows.Forms.Label();
            this.tabViTri = new System.Windows.Forms.TabControl();
            this.headerPanel.SuspendLayout();
            this.filterPanel.SuspendLayout();
            this.cardPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.lblTieuDe);
            this.headerPanel.Controls.Add(this.btnThemChiTieu);
            this.headerPanel.Controls.Add(this.btnXoa);
            this.headerPanel.Controls.Add(this.btnLuuThayDoi);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.FillColor = System.Drawing.Color.SeaGreen;
            this.headerPanel.FillColor2 = System.Drawing.Color.ForestGreen;
            this.headerPanel.FillColor3 = System.Drawing.Color.SeaGreen;
            this.headerPanel.FillColor4 = System.Drawing.Color.ForestGreen;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Padding = new System.Windows.Forms.Padding(18, 14, 18, 14);
            this.headerPanel.ShadowDecoration.BorderRadius = 0;
            this.headerPanel.ShadowDecoration.Depth = 8;
            this.headerPanel.ShadowDecoration.Enabled = true;
            this.headerPanel.Size = new System.Drawing.Size(1100, 68);
            this.headerPanel.TabIndex = 2;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.BackColor = System.Drawing.Color.Transparent;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.White;
            this.lblTieuDe.Location = new System.Drawing.Point(22, 18);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(244, 37);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Chi tiết Đơn Hàng:";
            // 
            // btnThemChiTieu
            // 
            this.btnThemChiTieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemChiTieu.Animated = true;
            this.btnThemChiTieu.BackColor = System.Drawing.Color.Transparent;
            this.btnThemChiTieu.BorderRadius = 18;
            this.btnThemChiTieu.FillColor = System.Drawing.Color.MediumSeaGreen;
            this.btnThemChiTieu.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnThemChiTieu.ForeColor = System.Drawing.Color.White;
            this.btnThemChiTieu.Location = new System.Drawing.Point(928, 15);
            this.btnThemChiTieu.Name = "btnThemChiTieu";
            this.btnThemChiTieu.Size = new System.Drawing.Size(150, 38);
            this.btnThemChiTieu.TabIndex = 1;
            this.btnThemChiTieu.Text = "Thêm chỉ tiêu";
            this.btnThemChiTieu.Click += new System.EventHandler(this.btnThemChiTieu_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnXoa.Animated = true;
            this.btnXoa.BackColor = System.Drawing.Color.Transparent;
            this.btnXoa.BorderRadius = 18;
            this.btnXoa.FillColor = System.Drawing.Color.IndianRed;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(655, 15);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(120, 38);
            this.btnXoa.TabIndex = 3;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLuuThayDoi
            // 
            this.btnLuuThayDoi.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnLuuThayDoi.Animated = true;
            this.btnLuuThayDoi.BackColor = System.Drawing.Color.Transparent;
            this.btnLuuThayDoi.BorderRadius = 18;
            this.btnLuuThayDoi.FillColor = System.Drawing.Color.SeaGreen;
            this.btnLuuThayDoi.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuuThayDoi.ForeColor = System.Drawing.Color.White;
            this.btnLuuThayDoi.Location = new System.Drawing.Point(772, 15);
            this.btnLuuThayDoi.Name = "btnLuuThayDoi";
            this.btnLuuThayDoi.Size = new System.Drawing.Size(150, 38);
            this.btnLuuThayDoi.TabIndex = 2;
            this.btnLuuThayDoi.Text = "Lưu thay đổi";
            this.btnLuuThayDoi.Click += new System.EventHandler(this.btnLuuThayDoi_Click);
            // 
            // filterPanel
            // 
            this.filterPanel.BackColor = System.Drawing.Color.SeaGreen;
            this.filterPanel.Controls.Add(this.lblLCT);
            this.filterPanel.Controls.Add(this.lblDV);
            this.filterPanel.Controls.Add(this.lblLPT);
            this.filterPanel.Controls.Add(this.lblNPT);
            this.filterPanel.Controls.Add(this.cboLoaiChiTieu);
            this.filterPanel.Controls.Add(this.cboDonVi);
            this.filterPanel.Controls.Add(this.cboLoaiPhanTich);
            this.filterPanel.Controls.Add(this.cboNguoiPhuTrach);
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.filterPanel.FillColor = System.Drawing.Color.SeaGreen;
            this.filterPanel.Location = new System.Drawing.Point(0, 68);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Padding = new System.Windows.Forms.Padding(18, 12, 18, 12);
            this.filterPanel.Size = new System.Drawing.Size(1100, 84);
            this.filterPanel.TabIndex = 1;
            // 
            // lblLCT
            // 
            this.lblLCT.AutoSize = true;
            this.lblLCT.BackColor = System.Drawing.Color.Transparent;
            this.lblLCT.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblLCT.Location = new System.Drawing.Point(30, 14);
            this.lblLCT.Name = "lblLCT";
            this.lblLCT.Size = new System.Drawing.Size(63, 21);
            this.lblLCT.TabIndex = 0;
            this.lblLCT.Text = "Chỉ tiêu";
            // 
            // lblDV
            // 
            this.lblDV.AutoSize = true;
            this.lblDV.BackColor = System.Drawing.Color.Transparent;
            this.lblDV.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDV.Location = new System.Drawing.Point(300, 14);
            this.lblDV.Name = "lblDV";
            this.lblDV.Size = new System.Drawing.Size(56, 21);
            this.lblDV.TabIndex = 1;
            this.lblDV.Text = "Đơn vị";
            // 
            // lblLPT
            // 
            this.lblLPT.AutoSize = true;
            this.lblLPT.BackColor = System.Drawing.Color.Transparent;
            this.lblLPT.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblLPT.Location = new System.Drawing.Point(570, 14);
            this.lblLPT.Name = "lblLPT";
            this.lblLPT.Size = new System.Drawing.Size(123, 21);
            this.lblLPT.TabIndex = 2;
            this.lblLPT.Text = "Phòng phân tích";
            // 
            // lblNPT
            // 
            this.lblNPT.AutoSize = true;
            this.lblNPT.BackColor = System.Drawing.Color.Transparent;
            this.lblNPT.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNPT.Location = new System.Drawing.Point(840, 14);
            this.lblNPT.Name = "lblNPT";
            this.lblNPT.Size = new System.Drawing.Size(124, 21);
            this.lblNPT.TabIndex = 3;
            this.lblNPT.Text = "Người phụ trách";
            // 
            // cboLoaiChiTieu
            // 
            this.cboLoaiChiTieu.AutoRoundedCorners = true;
            this.cboLoaiChiTieu.BackColor = System.Drawing.Color.Transparent;
            this.cboLoaiChiTieu.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiChiTieu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiChiTieu.FocusedColor = System.Drawing.Color.Empty;
            this.cboLoaiChiTieu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboLoaiChiTieu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboLoaiChiTieu.ItemHeight = 30;
            this.cboLoaiChiTieu.Location = new System.Drawing.Point(30, 38);
            this.cboLoaiChiTieu.Name = "cboLoaiChiTieu";
            this.cboLoaiChiTieu.Size = new System.Drawing.Size(230, 36);
            this.cboLoaiChiTieu.TabIndex = 4;
            // 
            // cboDonVi
            // 
            this.cboDonVi.AutoRoundedCorners = true;
            this.cboDonVi.BackColor = System.Drawing.Color.Transparent;
            this.cboDonVi.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDonVi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDonVi.FocusedColor = System.Drawing.Color.Empty;
            this.cboDonVi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboDonVi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboDonVi.ItemHeight = 30;
            this.cboDonVi.Location = new System.Drawing.Point(300, 38);
            this.cboDonVi.Name = "cboDonVi";
            this.cboDonVi.Size = new System.Drawing.Size(230, 36);
            this.cboDonVi.TabIndex = 5;
            // 
            // cboLoaiPhanTich
            // 
            this.cboLoaiPhanTich.AutoRoundedCorners = true;
            this.cboLoaiPhanTich.BackColor = System.Drawing.Color.Transparent;
            this.cboLoaiPhanTich.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiPhanTich.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiPhanTich.FocusedColor = System.Drawing.Color.Empty;
            this.cboLoaiPhanTich.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboLoaiPhanTich.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboLoaiPhanTich.ItemHeight = 30;
            this.cboLoaiPhanTich.Location = new System.Drawing.Point(570, 38);
            this.cboLoaiPhanTich.Name = "cboLoaiPhanTich";
            this.cboLoaiPhanTich.Size = new System.Drawing.Size(230, 36);
            this.cboLoaiPhanTich.TabIndex = 6;
            // 
            // cboNguoiPhuTrach
            // 
            this.cboNguoiPhuTrach.AutoRoundedCorners = true;
            this.cboNguoiPhuTrach.BackColor = System.Drawing.Color.Transparent;
            this.cboNguoiPhuTrach.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNguoiPhuTrach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNguoiPhuTrach.FocusedColor = System.Drawing.Color.Empty;
            this.cboNguoiPhuTrach.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboNguoiPhuTrach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboNguoiPhuTrach.ItemHeight = 30;
            this.cboNguoiPhuTrach.Location = new System.Drawing.Point(840, 38);
            this.cboNguoiPhuTrach.Name = "cboNguoiPhuTrach";
            this.cboNguoiPhuTrach.Size = new System.Drawing.Size(230, 36);
            this.cboNguoiPhuTrach.TabIndex = 7;
            // 
            // cardPanel
            // 
            this.cardPanel.BorderColor = System.Drawing.Color.SeaGreen;
            this.cardPanel.BorderRadius = 18;
            this.cardPanel.BorderThickness = 3;
            this.cardPanel.Controls.Add(this.sectionTitle);
            this.cardPanel.Controls.Add(this.tabViTri);
            this.cardPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardPanel.FillColor = System.Drawing.Color.White;
            this.cardPanel.Location = new System.Drawing.Point(0, 152);
            this.cardPanel.Name = "cardPanel";
            this.cardPanel.Padding = new System.Windows.Forms.Padding(18, 16, 18, 16);
            this.cardPanel.Size = new System.Drawing.Size(1100, 528);
            this.cardPanel.TabIndex = 0;
            // 
            // sectionTitle
            // 
            this.sectionTitle.BackColor = System.Drawing.Color.Transparent;
            this.sectionTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12.5F, System.Drawing.FontStyle.Bold);
            this.sectionTitle.ForeColor = System.Drawing.Color.SeaGreen;
            this.sectionTitle.Location = new System.Drawing.Point(24, 10);
            this.sectionTitle.Name = "sectionTitle";
            this.sectionTitle.Size = new System.Drawing.Size(300, 28);
            this.sectionTitle.TabIndex = 0;
            this.sectionTitle.Text = "Thông số môi trường";
            // 
            // tabViTri
            // 
            this.tabViTri.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabViTri.ItemSize = new System.Drawing.Size(120, 30);
            this.tabViTri.Location = new System.Drawing.Point(12, 41);
            this.tabViTri.Name = "tabViTri";
            this.tabViTri.Padding = new System.Drawing.Point(16, 6);
            this.tabViTri.SelectedIndex = 0;
            this.tabViTri.Size = new System.Drawing.Size(1076, 487);
            this.tabViTri.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabViTri.TabIndex = 1;
            // 
            // GUI_FormThongSoDonHang_V2
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.cardPanel);
            this.Controls.Add(this.filterPanel);
            this.Controls.Add(this.headerPanel);
            this.Name = "GUI_FormThongSoDonHang_V2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thông số môi trường";
            this.Load += new System.EventHandler(this.GUI_FormThongSoDonHang_V2_Load);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.filterPanel.ResumeLayout(false);
            this.filterPanel.PerformLayout();
            this.cardPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
