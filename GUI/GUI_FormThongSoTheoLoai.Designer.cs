// GUI/GUI_FormThongSoTheoLoai.Designer.cs
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace GUI
{
    partial class GUI_FormThongSoTheoLoai
    {
        private System.ComponentModel.IContainer components = null;

        // Header
        private Guna2GradientPanel header;
        private Label lblDonHang;
        private Label lblDiaChi;

        // Card bộ lọc
        private Guna2ShadowPanel cardFilter;
        private Label lblChiTieu;
        private Label lblNguoi;
        private Guna2ComboBox cboNguoiPhuTrach;
        private Label lblThauPhu;
        private Guna2ComboBox cboThauPhu;
        private Guna2Button btnThem;

        // Lưới + thanh dưới
        private Guna2DataGridView dgv;
        private Guna2Panel bottomBar;
        private Guna2Button btnLuu;
        private Guna2Button btnXoa;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.header = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.lblDonHang = new System.Windows.Forms.Label();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.cardFilter = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.cboLoaiChiTieu = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblChiTieu = new System.Windows.Forms.Label();
            this.lblNguoi = new System.Windows.Forms.Label();
            this.cboNguoiPhuTrach = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblThauPhu = new System.Windows.Forms.Label();
            this.cboThauPhu = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.dgv = new Guna.UI2.WinForms.Guna2DataGridView();
            this.bottomBar = new Guna.UI2.WinForms.Guna2Panel();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.header.SuspendLayout();
            this.cardFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.bottomBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // header
            // 
            this.header.Controls.Add(this.lblDonHang);
            this.header.Controls.Add(this.lblDiaChi);
            this.header.Dock = System.Windows.Forms.DockStyle.Top;
            this.header.FillColor = System.Drawing.Color.SeaGreen;
            this.header.FillColor2 = System.Drawing.Color.ForestGreen;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.header.Size = new System.Drawing.Size(920, 78);
            this.header.TabIndex = 3;
            // 
            // lblDonHang
            // 
            this.lblDonHang.AutoSize = true;
            this.lblDonHang.BackColor = System.Drawing.Color.Transparent;
            this.lblDonHang.Font = new System.Drawing.Font("Segoe UI Semibold", 15F);
            this.lblDonHang.ForeColor = System.Drawing.Color.White;
            this.lblDonHang.Location = new System.Drawing.Point(18, 10);
            this.lblDonHang.Name = "lblDonHang";
            this.lblDonHang.Size = new System.Drawing.Size(102, 28);
            this.lblDonHang.TabIndex = 0;
            this.lblDonHang.Text = "Đơn hàng";
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.BackColor = System.Drawing.Color.Transparent;
            this.lblDiaChi.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblDiaChi.ForeColor = System.Drawing.Color.White;
            this.lblDiaChi.Location = new System.Drawing.Point(20, 44);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(67, 19);
            this.lblDiaChi.TabIndex = 1;
            this.lblDiaChi.Text = "Địa chỉ: …";
            // 
            // cardFilter
            // 
            this.cardFilter.BackColor = System.Drawing.Color.Transparent;
            this.cardFilter.Controls.Add(this.cboLoaiChiTieu);
            this.cardFilter.Controls.Add(this.lblChiTieu);
            this.cardFilter.Controls.Add(this.lblNguoi);
            this.cardFilter.Controls.Add(this.cboNguoiPhuTrach);
            this.cardFilter.Controls.Add(this.lblThauPhu);
            this.cardFilter.Controls.Add(this.cboThauPhu);
            this.cardFilter.Controls.Add(this.btnThem);
            this.cardFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.cardFilter.FillColor = System.Drawing.Color.White;
            this.cardFilter.Location = new System.Drawing.Point(0, 78);
            this.cardFilter.Name = "cardFilter";
            this.cardFilter.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.cardFilter.Radius = 12;
            this.cardFilter.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cardFilter.ShadowStyle = Guna.UI2.WinForms.Guna2ShadowPanel.ShadowMode.Dropped;
            this.cardFilter.Size = new System.Drawing.Size(920, 96);
            this.cardFilter.TabIndex = 2;
            // 
            // cboLoaiChiTieu
            // 
            this.cboLoaiChiTieu.AutoRoundedCorners = true;
            this.cboLoaiChiTieu.BackColor = System.Drawing.Color.Transparent;
            this.cboLoaiChiTieu.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiChiTieu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiChiTieu.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboLoaiChiTieu.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboLoaiChiTieu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboLoaiChiTieu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboLoaiChiTieu.IntegralHeight = false;
            this.cboLoaiChiTieu.ItemHeight = 28;
            this.cboLoaiChiTieu.Location = new System.Drawing.Point(12, 36);
            this.cboLoaiChiTieu.Name = "cboLoaiChiTieu";
            this.cboLoaiChiTieu.Size = new System.Drawing.Size(218, 34);
            this.cboLoaiChiTieu.TabIndex = 9;
            // 
            // lblChiTieu
            // 
            this.lblChiTieu.AutoSize = true;
            this.lblChiTieu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblChiTieu.Location = new System.Drawing.Point(84, 16);
            this.lblChiTieu.Name = "lblChiTieu";
            this.lblChiTieu.Size = new System.Drawing.Size(48, 15);
            this.lblChiTieu.TabIndex = 0;
            this.lblChiTieu.Text = "Chỉ tiêu";
            // 
            // lblNguoi
            // 
            this.lblNguoi.AutoSize = true;
            this.lblNguoi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNguoi.Location = new System.Drawing.Point(379, 16);
            this.lblNguoi.Name = "lblNguoi";
            this.lblNguoi.Size = new System.Drawing.Size(94, 15);
            this.lblNguoi.TabIndex = 4;
            this.lblNguoi.Text = "Người phụ trách";
            this.lblNguoi.Click += new System.EventHandler(this.lblNguoi_Click);
            // 
            // cboNguoiPhuTrach
            // 
            this.cboNguoiPhuTrach.AutoRoundedCorners = true;
            this.cboNguoiPhuTrach.BackColor = System.Drawing.Color.Transparent;
            this.cboNguoiPhuTrach.BorderRadius = 16;
            this.cboNguoiPhuTrach.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNguoiPhuTrach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNguoiPhuTrach.FocusedColor = System.Drawing.Color.SeaGreen;
            this.cboNguoiPhuTrach.FocusedState.BorderColor = System.Drawing.Color.SeaGreen;
            this.cboNguoiPhuTrach.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboNguoiPhuTrach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboNguoiPhuTrach.ItemHeight = 28;
            this.cboNguoiPhuTrach.Location = new System.Drawing.Point(314, 36);
            this.cboNguoiPhuTrach.Name = "cboNguoiPhuTrach";
            this.cboNguoiPhuTrach.Size = new System.Drawing.Size(218, 34);
            this.cboNguoiPhuTrach.TabIndex = 5;
            // 
            // lblThauPhu
            // 
            this.lblThauPhu.AutoSize = true;
            this.lblThauPhu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblThauPhu.Location = new System.Drawing.Point(702, 16);
            this.lblThauPhu.Name = "lblThauPhu";
            this.lblThauPhu.Size = new System.Drawing.Size(58, 15);
            this.lblThauPhu.TabIndex = 6;
            this.lblThauPhu.Text = "Thầu phụ";
            // 
            // cboThauPhu
            // 
            this.cboThauPhu.AutoRoundedCorners = true;
            this.cboThauPhu.BackColor = System.Drawing.Color.Transparent;
            this.cboThauPhu.BorderRadius = 16;
            this.cboThauPhu.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboThauPhu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboThauPhu.FocusedColor = System.Drawing.Color.SeaGreen;
            this.cboThauPhu.FocusedState.BorderColor = System.Drawing.Color.SeaGreen;
            this.cboThauPhu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboThauPhu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboThauPhu.ItemHeight = 28;
            this.cboThauPhu.Location = new System.Drawing.Point(616, 36);
            this.cboThauPhu.Name = "cboThauPhu";
            this.cboThauPhu.Size = new System.Drawing.Size(218, 34);
            this.cboThauPhu.TabIndex = 7;
            // 
            // btnThem
            // 
            this.btnThem.AutoRoundedCorners = true;
            this.btnThem.BorderRadius = 14;
            this.btnThem.FillColor = System.Drawing.Color.SeaGreen;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(842, 36);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(64, 30);
            this.btnThem.TabIndex = 8;
            this.btnThem.Text = "Thêm";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(252)))), ((int)(((byte)(249)))));
            this.dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.SeaGreen;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv.ColumnHeadersHeight = 36;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgv.Location = new System.Drawing.Point(0, 174);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidth = 51;
            this.dgv.Size = new System.Drawing.Size(920, 157);
            this.dgv.TabIndex = 0;
            this.dgv.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(252)))), ((int)(((byte)(249)))));
            this.dgv.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgv.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgv.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgv.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgv.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgv.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgv.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.SeaGreen;
            this.dgv.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F);
            this.dgv.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgv.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv.ThemeStyle.HeaderStyle.Height = 36;
            this.dgv.ThemeStyle.ReadOnly = false;
            this.dgv.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgv.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgv.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgv.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgv.ThemeStyle.RowsStyle.Height = 22;
            this.dgv.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.dgv.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            // 
            // bottomBar
            // 
            this.bottomBar.Controls.Add(this.btnLuu);
            this.bottomBar.Controls.Add(this.btnXoa);
            this.bottomBar.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bottomBar.CustomBorderThickness = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.bottomBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomBar.FillColor = System.Drawing.Color.White;
            this.bottomBar.Location = new System.Drawing.Point(0, 331);
            this.bottomBar.Name = "bottomBar";
            this.bottomBar.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.bottomBar.Size = new System.Drawing.Size(920, 52);
            this.bottomBar.TabIndex = 1;
            // 
            // btnLuu
            // 
            this.btnLuu.AutoRoundedCorners = true;
            this.btnLuu.BorderRadius = 17;
            this.btnLuu.FillColor = System.Drawing.Color.SeaGreen;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(12, 8);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(120, 36);
            this.btnLuu.TabIndex = 0;
            this.btnLuu.Text = "Lưu thay đổi";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.AutoRoundedCorners = true;
            this.btnXoa.BorderRadius = 17;
            this.btnXoa.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(138, 8);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(84, 36);
            this.btnXoa.TabIndex = 1;
            this.btnXoa.Text = "Xoá";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // GUI_FormThongSoTheoLoai
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(920, 383);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.bottomBar);
            this.Controls.Add(this.cardFilter);
            this.Controls.Add(this.header);
            this.Name = "GUI_FormThongSoTheoLoai";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thông số theo loại vị trí";
            this.Load += new System.EventHandler(this.GUI_FormThongSoTheoLoai_Load);
            this.header.ResumeLayout(false);
            this.header.PerformLayout();
            this.cardFilter.ResumeLayout(false);
            this.cardFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.bottomBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Guna2ComboBox cboLoaiChiTieu;
    }
}
