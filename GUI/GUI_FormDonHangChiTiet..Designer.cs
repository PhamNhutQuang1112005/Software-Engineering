// GUI/GUI_FormDonHangChiTiet.Designer.cs
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace GUI
{
    partial class GUI_FormDonHangChiTiet
    {
        private System.ComponentModel.IContainer components = null;

        private Guna2GradientPanel header;
        private Label lblDonHang;
        private Label lblDiaChi;

        private SplitContainer splitMain;
        private Panel panelLeft;
        private Label lblLeft;
        private FlowLayoutPanel pnlViTri;

        private Panel panelRight;
        private Panel topRight;
        private Guna2Button btnThemLoai;
        private Guna2Button btnXoaLoai;
        private Guna2Button btnOpenThongSo;
        private DataGridView dgvLoaiViTri;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI_FormDonHangChiTiet));
            this.header = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.lblDonHang = new System.Windows.Forms.Label();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.pnlViTri = new System.Windows.Forms.FlowLayoutPanel();
            this.lblLeft = new System.Windows.Forms.Label();
            this.panelRight = new System.Windows.Forms.Panel();
            this.dgvLoaiViTri = new System.Windows.Forms.DataGridView();
            this.topRight = new System.Windows.Forms.Panel();
            this.btnThemLoai = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoaLoai = new Guna.UI2.WinForms.Guna2Button();
            this.btnOpenThongSo = new Guna.UI2.WinForms.Guna2Button();
            this.header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoaiViTri)).BeginInit();
            this.topRight.SuspendLayout();
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
            this.header.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            this.header.Size = new System.Drawing.Size(1062, 74);
            this.header.TabIndex = 1;
            // 
            // lblDonHang
            // 
            this.lblDonHang.AutoSize = true;
            this.lblDonHang.BackColor = System.Drawing.Color.Transparent;
            this.lblDonHang.Font = new System.Drawing.Font("Segoe UI Semibold", 13F);
            this.lblDonHang.ForeColor = System.Drawing.Color.White;
            this.lblDonHang.Location = new System.Drawing.Point(20, 8);
            this.lblDonHang.Name = "lblDonHang";
            this.lblDonHang.Size = new System.Drawing.Size(111, 30);
            this.lblDonHang.TabIndex = 0;
            this.lblDonHang.Text = "Đơn hàng";
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.BackColor = System.Drawing.Color.Transparent;
            this.lblDiaChi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDiaChi.ForeColor = System.Drawing.Color.White;
            this.lblDiaChi.Location = new System.Drawing.Point(21, 33);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(83, 23);
            this.lblDiaChi.TabIndex = 1;
            this.lblDiaChi.Text = "Địa chỉ: …";
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 74);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.panelLeft);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.panelRight);
            this.splitMain.Size = new System.Drawing.Size(1062, 325);
            this.splitMain.SplitterDistance = 706;
            this.splitMain.TabIndex = 0;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.pnlViTri);
            this.panelLeft.Controls.Add(this.lblLeft);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(706, 325);
            this.panelLeft.TabIndex = 0;
            // 
            // pnlViTri
            // 
            this.pnlViTri.AutoScroll = true;
            this.pnlViTri.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlViTri.BackgroundImage = global::GUI.Properties.Resources.Sườn_UI__light_1;
            this.pnlViTri.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlViTri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlViTri.Location = new System.Drawing.Point(0, 28);
            this.pnlViTri.Name = "pnlViTri";
            this.pnlViTri.Padding = new System.Windows.Forms.Padding(10);
            this.pnlViTri.Size = new System.Drawing.Size(706, 297);
            this.pnlViTri.TabIndex = 0;
            // 
            // lblLeft
            // 
            this.lblLeft.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLeft.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.lblLeft.Location = new System.Drawing.Point(0, 0);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblLeft.Size = new System.Drawing.Size(706, 28);
            this.lblLeft.TabIndex = 1;
            this.lblLeft.Text = "Vị trí\r\n";
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.dgvLoaiViTri);
            this.panelRight.Controls.Add(this.topRight);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(0, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(352, 325);
            this.panelRight.TabIndex = 0;
            // 
            // dgvLoaiViTri
            // 
            this.dgvLoaiViTri.AllowUserToAddRows = false;
            this.dgvLoaiViTri.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLoaiViTri.BackgroundColor = System.Drawing.Color.White;
            this.dgvLoaiViTri.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLoaiViTri.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvLoaiViTri.ColumnHeadersHeight = 36;
            this.dgvLoaiViTri.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLoaiViTri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLoaiViTri.Location = new System.Drawing.Point(0, 46);
            this.dgvLoaiViTri.MultiSelect = false;
            this.dgvLoaiViTri.Name = "dgvLoaiViTri";
            this.dgvLoaiViTri.ReadOnly = true;
            this.dgvLoaiViTri.RowHeadersVisible = false;
            this.dgvLoaiViTri.RowHeadersWidth = 51;
            this.dgvLoaiViTri.RowTemplate.Height = 28;
            this.dgvLoaiViTri.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLoaiViTri.Size = new System.Drawing.Size(352, 279);
            this.dgvLoaiViTri.TabIndex = 0;
            this.dgvLoaiViTri.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLoaiViTri_CellDoubleClick);
            // 
            // topRight
            // 
            this.topRight.Controls.Add(this.btnThemLoai);
            this.topRight.Controls.Add(this.btnXoaLoai);
            this.topRight.Controls.Add(this.btnOpenThongSo);
            this.topRight.Dock = System.Windows.Forms.DockStyle.Top;
            this.topRight.Location = new System.Drawing.Point(0, 0);
            this.topRight.Name = "topRight";
            this.topRight.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.topRight.Size = new System.Drawing.Size(352, 46);
            this.topRight.TabIndex = 1;
            // 
            // btnThemLoai
            // 
            this.btnThemLoai.AutoRoundedCorners = true;
            this.btnThemLoai.BorderRadius = 15;
            this.btnThemLoai.FillColor = System.Drawing.Color.SeaGreen;
            this.btnThemLoai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThemLoai.ForeColor = System.Drawing.Color.White;
            this.btnThemLoai.Location = new System.Drawing.Point(8, 7);
            this.btnThemLoai.Name = "btnThemLoai";
            this.btnThemLoai.Size = new System.Drawing.Size(84, 32);
            this.btnThemLoai.TabIndex = 0;
            this.btnThemLoai.Text = "+ Loại";
            this.btnThemLoai.Click += new System.EventHandler(this.btnThemLoai_Click);
            // 
            // btnXoaLoai
            // 
            this.btnXoaLoai.AutoRoundedCorners = true;
            this.btnXoaLoai.BorderRadius = 15;
            this.btnXoaLoai.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnXoaLoai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXoaLoai.ForeColor = System.Drawing.Color.White;
            this.btnXoaLoai.Location = new System.Drawing.Point(98, 7);
            this.btnXoaLoai.Name = "btnXoaLoai";
            this.btnXoaLoai.Size = new System.Drawing.Size(60, 32);
            this.btnXoaLoai.TabIndex = 1;
            this.btnXoaLoai.Text = "Xóa";
            this.btnXoaLoai.Click += new System.EventHandler(this.btnXoaLoai_Click);
            // 
            // btnOpenThongSo
            // 
            this.btnOpenThongSo.AutoRoundedCorners = true;
            this.btnOpenThongSo.BorderRadius = 15;
            this.btnOpenThongSo.FillColor = System.Drawing.Color.ForestGreen;
            this.btnOpenThongSo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnOpenThongSo.ForeColor = System.Drawing.Color.White;
            this.btnOpenThongSo.Location = new System.Drawing.Point(164, 7);
            this.btnOpenThongSo.Name = "btnOpenThongSo";
            this.btnOpenThongSo.Size = new System.Drawing.Size(112, 32);
            this.btnOpenThongSo.TabIndex = 2;
            this.btnOpenThongSo.Text = "Thông số…";
            this.btnOpenThongSo.Click += new System.EventHandler(this.btnOpenThongSo_Click);
            // 
            // GUI_FormDonHangChiTiet
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1062, 399);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.header);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUI_FormDonHangChiTiet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đơn hàng – chi tiết";
            this.Load += new System.EventHandler(this.GUI_FormDonHangChiTiet_Load);
            this.header.ResumeLayout(false);
            this.header.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoaiViTri)).EndInit();
            this.topRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
