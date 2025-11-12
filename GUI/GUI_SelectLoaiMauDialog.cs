using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using BLL;
using Guna.UI2.WinForms;

namespace GUI
{
    public class GUI_SelectLoaiMauDialog : Form
    {
        private readonly string _donHangID;
        private readonly BLL_ThongSoQuanTrac _bll = new BLL_ThongSoQuanTrac();

        // Designer controls declared in InitializeComponent
        private Common.SmoothFlowLayoutPanel hienthiloaimau;
        private Guna2Button xacnhan;
        private Guna2Button huythaotac;

        // Store chips by id
        private readonly Dictionary<string, (string Ten, Guna2Button Chip)> _chips =
            new Dictionary<string, (string Ten, Guna2Button Chip)>(StringComparer.OrdinalIgnoreCase);

        // Backward-compat for single select callers
        public string SelectedLoaiViTriID { get; private set; }
        public string SelectedTenLoai { get; private set; }

        // Multi-select result
        public IReadOnlyList<(string Id, string Ten)> SelectedLoaiMauMulti { get; private set; }

        public GUI_SelectLoaiMauDialog(string donHangID)
        {
            _donHangID = donHangID;
            InitializeComponent();
            this.Load += OnDialogLoad;
            if (xacnhan != null) xacnhan.Click += OnConfirm;
            if (huythaotac != null) huythaotac.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; Close(); };
        }

        private void OnDialogLoad(object sender, EventArgs e)
        {
            try
            {
                if (hienthiloaimau != null)
                {
                    hienthiloaimau.WrapContents = true;
                    hienthiloaimau.AutoScroll = true;
                }

                var dtViTri = _bll.GetViTriByDonHang(_donHangID);
                if (dtViTri == null) return;

                var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (DataRow vt in dtViTri.Rows)
                {
                    string viTriID = Convert.ToString(vt["ViTriID"]);
                    if (string.IsNullOrEmpty(viTriID)) continue;
                    var dtLoai = _bll.GetLoaiViTriByViTri(viTriID);
                    if (dtLoai == null) continue;

                    foreach (DataRow lv in dtLoai.Rows)
                    {
                        string id = Convert.ToString(lv["LoaiViTriID"]);
                        if (string.IsNullOrEmpty(id)) continue;
                        string ten = dtLoai.Columns.Contains("TenLoai") ? Convert.ToString(lv["TenLoai"]) : id;
                        if (!dict.ContainsKey(id)) dict[id] = ten;
                    }
                }

                foreach (var kv in dict.OrderBy(x => x.Value))
                {
                    var chip = CreateLoaiChip(kv.Key, kv.Value);
                    _chips[kv.Key] = (kv.Value, chip);
                    hienthiloaimau.Controls.Add(chip);
                }
            }
            catch { }
        }

        private Guna2Button CreateLoaiChip(string id, string ten)
        {
            var btn = new Guna2Button
            {
                Text = ten,
                Tag = id,
                AutoRoundedCorners = true,
                BorderRadius = 16,
                ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.ToogleButton,
                Height = 34,
                Width = Math.Max(120, TextRenderer.MeasureText(ten, new Font("Segoe UI", 10, FontStyle.Bold)).Width + 28),
                Margin = new Padding(8),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),

                BorderThickness = 1,
                BorderColor = Color.FromArgb(120, 195, 170),
                FillColor = Color.Transparent,
                ForeColor = Color.White
            };

            btn.HoverState.FillColor = Color.FromArgb(24, 120, 195, 170);
            btn.HoverState.BorderColor = Color.FromArgb(120, 195, 170);

            btn.CheckedState.FillColor = Color.FromArgb(60, 255, 255, 255);
            btn.CheckedState.BorderColor = Color.FromArgb(180, 120, 195, 170);
            btn.CheckedState.CustomBorderColor = btn.CheckedState.BorderColor;
            btn.CheckedState.ForeColor = Color.White;

            return btn;
        }

        private void OnConfirm(object sender, EventArgs e)
        {
            var selected = new List<(string Id, string Ten)>();
            foreach (var kv in _chips)
            {
                if (kv.Value.Chip.Checked)
                    selected.Add((kv.Key, kv.Value.Ten));
            }

            if (selected.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một loại mẫu.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SelectedLoaiMauMulti = selected;

            // Backward compat single
            SelectedLoaiViTriID = selected[0].Id;
            SelectedTenLoai     = selected[0].Ten;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI_SelectLoaiMauDialog));
            this.hienthiloaimau = new GUI.Common.SmoothFlowLayoutPanel();
            this.xacnhan = new Guna.UI2.WinForms.Guna2Button();
            this.huythaotac = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // hienthiloaimau
            // 
            this.hienthiloaimau.AutoScroll = true;
            this.hienthiloaimau.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(130)))), ((int)(((byte)(90)))));
            this.hienthiloaimau.Location = new System.Drawing.Point(12, 12);
            this.hienthiloaimau.Name = "hienthiloaimau";
            this.hienthiloaimau.Size = new System.Drawing.Size(415, 210);
            this.hienthiloaimau.TabIndex = 0;
            this.hienthiloaimau.Paint += new System.Windows.Forms.PaintEventHandler(this.hienthiloaimau_Paint);
            // 
            // xacnhan
            // 
            this.xacnhan.AutoRoundedCorners = true;
            this.xacnhan.BackColor = System.Drawing.Color.Transparent;
            this.xacnhan.BorderColor = System.Drawing.Color.White;
            this.xacnhan.BorderRadius = 21;
            this.xacnhan.BorderThickness = 2;
            this.xacnhan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.xacnhan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.xacnhan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.xacnhan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.xacnhan.FillColor = System.Drawing.Color.Green;
            this.xacnhan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.xacnhan.ForeColor = System.Drawing.Color.White;
            this.xacnhan.Location = new System.Drawing.Point(86, 235);
            this.xacnhan.Name = "xacnhan";
            this.xacnhan.Size = new System.Drawing.Size(119, 45);
            this.xacnhan.TabIndex = 36;
            this.xacnhan.Text = "Xác nhận";
            // 
            // huythaotac
            // 
            this.huythaotac.AutoRoundedCorners = true;
            this.huythaotac.BackColor = System.Drawing.Color.Transparent;
            this.huythaotac.BorderColor = System.Drawing.Color.White;
            this.huythaotac.BorderRadius = 21;
            this.huythaotac.BorderThickness = 2;
            this.huythaotac.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.huythaotac.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.huythaotac.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.huythaotac.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.huythaotac.FillColor = System.Drawing.Color.Green;
            this.huythaotac.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.huythaotac.ForeColor = System.Drawing.Color.White;
            this.huythaotac.Location = new System.Drawing.Point(236, 235);
            this.huythaotac.Name = "huythaotac";
            this.huythaotac.Size = new System.Drawing.Size(119, 45);
            this.huythaotac.TabIndex = 37;
            this.huythaotac.Text = "Hủy";
            // 
            // GUI_SelectLoaiMauDialog
            // 
            this.BackColor = System.Drawing.Color.SeaGreen;
            this.ClientSize = new System.Drawing.Size(439, 296);
            this.Controls.Add(this.huythaotac);
            this.Controls.Add(this.xacnhan);
            this.Controls.Add(this.hienthiloaimau);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUI_SelectLoaiMauDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chọn loại mẫu";
            this.ResumeLayout(false);

        }

        private void hienthiloaimau_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
