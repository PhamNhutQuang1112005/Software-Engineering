using System;
using System.Data;
using System.Windows.Forms;

namespace GUI
{
    public class GUI_FormPreviewExport : Form
    {
        private readonly DataTable _data;
        private readonly string _donHangID;
        private readonly string _tenLoaiMau;

        // Designer controls
        private Guna.UI2.WinForms.Guna2DataGridView thongsogridview;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2Button xuatraPDF;
        private Label label2;
        private Label lbldonhang;
        private Label madonhang;
        private Label loaimau;
        private Label lblLoaimau;

        public Func<DataTable, bool> OnExport; // callback thực sự để xuất PDF

        // NEW constructor: nhận DonHangID + TenLoaiMau + DataTable đã build
        public GUI_FormPreviewExport(string donHangID, string tenLoaiMau, DataTable dt)
        {
            _donHangID  = donHangID;
            _tenLoaiMau = tenLoaiMau;
            _data       = dt ?? new DataTable();
            InitializeComponent();
            RuntimeBind();
        }

        // Bind dữ liệu và thiết lập label, header cột
        private void RuntimeBind()
        {
            if (madonhang != null) madonhang.Text = _donHangID ?? "-";
            if (loaimau   != null) loaimau.Text   = _tenLoaiMau ?? "-";

            if (thongsogridview != null)
            {
                thongsogridview.DataSource = _data;
                thongsogridview.ReadOnly = true;
                thongsogridview.AllowUserToAddRows = false;
                thongsogridview.AllowUserToDeleteRows = false;
                thongsogridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                RenameColumn("STT", "STT");
                RenameColumn("ThongSo", "Thông số");
                RenameColumn("DonVi", "Đơn vị");
                RenameColumn("GiaTriChuan", "Giá trị chuẩn");
                RenameColumn("ViTri1", "Vị trí 1");
                RenameColumn("ViTri2", "Vị trí 2");
                RenameColumn("ViTri3", "Vị trí 3");

                TrySetFill("STT", 40);
                TrySetFill("ThongSo", 200);
                TrySetFill("DonVi", 80);
                TrySetFill("GiaTriChuan", 120);
                TrySetFill("ViTri1", 100);
                TrySetFill("ViTri2", 100);
                TrySetFill("ViTri3", 100);
            }

            if (xuatraPDF != null)
            {
                xuatraPDF.Click += (s, e) =>
                {
                    if (OnExport == null)
                    {
                        MessageBox.Show("Chưa cấu hình hàm xuất PDF.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (!xuatraPDF.Enabled) return; // guard
                    xuatraPDF.Enabled = false;
                    try
                    {
                        var ok = OnExport(_data);
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xuất: " + ex.Message);
                    }
                    finally
                    {
                        xuatraPDF.Enabled = true;
                    }
                };
            }
        }

        private void TrySetFill(string col, float weight)
        {
            if (thongsogridview.Columns.Contains(col))
            {
                var c = thongsogridview.Columns[col];
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                c.FillWeight = weight;
                c.MinimumWidth = 40;
            }
        }

        private void RenameColumn(string name, string header)
        {
            if (thongsogridview.Columns.Contains(name))
                thongsogridview.Columns[name].HeaderText = header;
        }

        private void GUI_FormPreviewExport_Load(object sender, EventArgs e) { }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI_FormPreviewExport));
            this.thongsogridview = new Guna.UI2.WinForms.Guna2DataGridView();
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.xuatraPDF = new Guna.UI2.WinForms.Guna2Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lbldonhang = new System.Windows.Forms.Label();
            this.madonhang = new System.Windows.Forms.Label();
            this.loaimau = new System.Windows.Forms.Label();
            this.lblLoaimau = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.thongsogridview)).BeginInit();
            this.SuspendLayout();
            // 
            // thongsogridview
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.thongsogridview.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SeaGreen;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.MediumSeaGreen;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.thongsogridview.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.thongsogridview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Honeydew;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.thongsogridview.DefaultCellStyle = dataGridViewCellStyle3;
            this.thongsogridview.GridColor = System.Drawing.Color.Gainsboro;
            this.thongsogridview.Location = new System.Drawing.Point(12, 148);
            this.thongsogridview.Name = "thongsogridview";
            this.thongsogridview.RowHeadersVisible = false;
            this.thongsogridview.RowHeadersWidth = 51;
            this.thongsogridview.Size = new System.Drawing.Size(697, 348);
            this.thongsogridview.TabIndex = 0;
            this.thongsogridview.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.thongsogridview.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.thongsogridview.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.thongsogridview.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.thongsogridview.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.thongsogridview.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.thongsogridview.ThemeStyle.GridColor = System.Drawing.Color.Gainsboro;
            this.thongsogridview.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.thongsogridview.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.thongsogridview.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thongsogridview.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.thongsogridview.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.thongsogridview.ThemeStyle.HeaderStyle.Height = 4;
            this.thongsogridview.ThemeStyle.ReadOnly = false;
            this.thongsogridview.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.thongsogridview.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.thongsogridview.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thongsogridview.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.thongsogridview.ThemeStyle.RowsStyle.Height = 22;
            this.thongsogridview.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.thongsogridview.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(216)))), ((int)(((byte)(112)))));
            this.guna2GradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.guna2GradientPanel1.Location = new System.Drawing.Point(-72, 110);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(798, 8);
            this.guna2GradientPanel1.TabIndex = 51;
            // 
            // xuatraPDF
            // 
            this.xuatraPDF.AutoRoundedCorners = true;
            this.xuatraPDF.BackColor = System.Drawing.Color.Transparent;
            this.xuatraPDF.BorderColor = System.Drawing.Color.White;
            this.xuatraPDF.BorderRadius = 21;
            this.xuatraPDF.BorderThickness = 2;
            this.xuatraPDF.FillColor = System.Drawing.Color.ForestGreen;
            this.xuatraPDF.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.xuatraPDF.ForeColor = System.Drawing.Color.White;
            this.xuatraPDF.Location = new System.Drawing.Point(262, 527);
            this.xuatraPDF.Name = "xuatraPDF";
            this.xuatraPDF.Size = new System.Drawing.Size(175, 45);
            this.xuatraPDF.TabIndex = 53;
            this.xuatraPDF.Text = "Xuất ra PDF";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(373, 35);
            this.label2.TabIndex = 52;
            this.label2.Text = "Xem trước dữ liệu sẽ xuất PDF";
            // 
            // lbldonhang
            // 
            this.lbldonhang.AutoSize = true;
            this.lbldonhang.BackColor = System.Drawing.Color.Transparent;
            this.lbldonhang.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lbldonhang.ForeColor = System.Drawing.Color.White;
            this.lbldonhang.Location = new System.Drawing.Point(12, 57);
            this.lbldonhang.Name = "lbldonhang";
            this.lbldonhang.Size = new System.Drawing.Size(110, 28);
            this.lbldonhang.TabIndex = 54;
            this.lbldonhang.Text = "Đơn hàng:";
            // 
            // madonhang
            // 
            this.madonhang.AutoSize = true;
            this.madonhang.BackColor = System.Drawing.Color.Transparent;
            this.madonhang.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.madonhang.ForeColor = System.Drawing.Color.White;
            this.madonhang.Location = new System.Drawing.Point(128, 57);
            this.madonhang.Name = "madonhang";
            this.madonhang.Size = new System.Drawing.Size(20, 28);
            this.madonhang.TabIndex = 55;
            this.madonhang.Text = "-";
            // 
            // loaimau
            // 
            this.loaimau.AutoSize = true;
            this.loaimau.BackColor = System.Drawing.Color.Transparent;
            this.loaimau.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.loaimau.ForeColor = System.Drawing.Color.White;
            this.loaimau.Location = new System.Drawing.Point(516, 57);
            this.loaimau.Name = "loaimau";
            this.loaimau.Size = new System.Drawing.Size(20, 28);
            this.loaimau.TabIndex = 57;
            this.loaimau.Text = "-";
            // 
            // lblLoaimau
            // 
            this.lblLoaimau.AutoSize = true;
            this.lblLoaimau.BackColor = System.Drawing.Color.Transparent;
            this.lblLoaimau.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblLoaimau.ForeColor = System.Drawing.Color.White;
            this.lblLoaimau.Location = new System.Drawing.Point(400, 57);
            this.lblLoaimau.Name = "lblLoaimau";
            this.lblLoaimau.Size = new System.Drawing.Size(103, 28);
            this.lblLoaimau.TabIndex = 56;
            this.lblLoaimau.Text = "Loại mẫu:";
            // 
            // GUI_FormPreviewExport
            // 
            this.BackColor = System.Drawing.Color.SeaGreen;
            this.ClientSize = new System.Drawing.Size(721, 595);
            this.Controls.Add(this.loaimau);
            this.Controls.Add(this.lblLoaimau);
            this.Controls.Add(this.madonhang);
            this.Controls.Add(this.lbldonhang);
            this.Controls.Add(this.xuatraPDF);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.guna2GradientPanel1);
            this.Controls.Add(this.thongsogridview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUI_FormPreviewExport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preview Export";
            this.Load += new System.EventHandler(this.GUI_FormPreviewExport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.thongsogridview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
        