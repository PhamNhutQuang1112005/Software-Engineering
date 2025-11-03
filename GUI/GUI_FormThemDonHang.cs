using System;
using System.Data;
using System.Windows.Forms;
using BLL;
using DTO;                         // <- dùng DTO_DonHang
using Guna.UI2.WinForms;

namespace GUI
{
    public partial class GUI_FormThemDonHang : Form
    {
        private string _donHangID;
        public string SavedDonHangID { get; private set; }

        public GUI_FormThemDonHang()
        {
            InitializeComponent();
        }

        public GUI_FormThemDonHang(string donHangID = null)
        {
            InitializeComponent();
            _donHangID = donHangID;
        }

        // ====== LOAD FORM ======
        private void GUI_FormThemDonHang_Load(object sender, EventArgs e)
        {
            // Nguồn dữ liệu combobox (giữ như cũ)
            guna2ComboBox1.DataSource   = BLL_KhachHang.GetAllKhachHang();
            guna2ComboBox1.DisplayMember = "TenCongTy";
            guna2ComboBox1.ValueMember   = "KhachHangID";

            guna2ComboBox4.DataSource    = BLL_DonHang.GetAllHopDong();
            guna2ComboBox4.DisplayMember = "MaHopDong";
            guna2ComboBox4.ValueMember   = "HopDongID";

            guna2ComboBox5.DataSource    = BLL_DonHang.GetAllTrangThaiDonHang();
            guna2ComboBox5.DisplayMember = "TenTrangThai";
            guna2ComboBox5.ValueMember   = "TrangThaiID";

            // "Ngày dự kiến" chỉ hiển thị, không cho sửa tay
            Ngay_Du_kien.Enabled = false;

            if (string.IsNullOrEmpty(_donHangID))
            {
                // ====== TẠO MỚI ======
                // ID để bạn điền; nếu muốn auto, dùng SinhMaDonHang của bạn
                guna2TextBox2.Text = BLL_DonHang.SinhMaDonHang();

                // Ngày mặc định
                Ngay_LayMau.Checked = true;
                Ngay_LayMau.Value   = DateTime.Today;
                UpdateNgayDuKien();

                // "Ngày trả thực tế" chỉ nhập khi SỬA
                Ngay_Thuc_te.Enabled = false;

                // Địa chỉ trống
                Dia_Chi_text.Text = string.Empty;
            }
            else
            {
                // ====== SỬA ======
                var dt = BLL_DonHang.GetDonHangByID(_donHangID);
                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];

                    // ID/Mã + các combobox + mô tả (giữ logic cũ)
                    guna2TextBox2.Text          = Convert.ToString(row["DonHangID"]);
                    guna2TextBox1.Text          = Convert.ToString(row["MaDonHang"]);
                    guna2ComboBox4.SelectedValue = Convert.ToString(row["HopDongID"]);
                    guna2ComboBox5.SelectedValue = Convert.ToString(row["TrangThaiID"]);
                    guna2TextBox4.Text          = Convert.ToString(row["GhiChu"]);
                    if (row.Table.Columns.Contains("IDKhachHang"))
                        guna2ComboBox1.SelectedValue = Convert.ToString(row["IDKhachHang"]);

                    // Địa chỉ
                    if (row.Table.Columns.Contains("DiaChi"))
                        Dia_Chi_text.Text = row["DiaChi"] == DBNull.Value ? "" : Convert.ToString(row["DiaChi"]);

                    // Ngày
                    if (row.Table.Columns.Contains("NgayLayMau") && row["NgayLayMau"] != DBNull.Value)
                    {
                        Ngay_LayMau.Checked = true;
                        Ngay_LayMau.Value   = Convert.ToDateTime(row["NgayLayMau"]);
                    }
                    else
                    {
                        Ngay_LayMau.Checked = true;
                        Ngay_LayMau.Value   = DateTime.Today;
                    }

                    // Tính dự kiến trước
                    UpdateNgayDuKien();

                    // Nếu DB đã có ngày dự kiến thì hiển thị theo DB
                    if (row.Table.Columns.Contains("NgayDuKienTraKetQua") && row["NgayDuKienTraKetQua"] != DBNull.Value)
                        Ngay_Du_kien.Value = Convert.ToDateTime(row["NgayDuKienTraKetQua"]);

                    // Cho phép nhập "Ngày trả thực tế" khi SỬA
                    Ngay_Thuc_te.Enabled = true;
                    if (row.Table.Columns.Contains("NgayTraThucTe") && row["NgayTraThucTe"] != DBNull.Value)
                        Ngay_Thuc_te.Value = Convert.ToDateTime(row["NgayTraThucTe"]);
                }
            }
        }

        // ====== CỘNG +15 KHI THAY "NGÀY LẤY MẪU" ======
        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            UpdateNgayDuKien();
        }

        private void UpdateNgayDuKien()
        {
            var baseDate = Ngay_LayMau.Checked ? Ngay_LayMau.Value.Date : DateTime.Today;
            Ngay_Du_kien.Value = baseDate.AddDays(15);
        }

        // ====== LƯU ======
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            try
            {
                string donhangid   = (guna2TextBox2.Text ?? "").Trim();  // DonHangID
                string maDonHang   = (guna2TextBox1.Text ?? "").Trim();  // Mã đơn hàng
                string hopDongID   = guna2ComboBox4.SelectedValue?.ToString();
                string trangThaiID = guna2ComboBox5.SelectedValue?.ToString();
                string ghiChu      = (guna2TextBox4.Text ?? "").Trim();
                string khachhang   = guna2ComboBox1.SelectedValue?.ToString();
                string diaChi      = (Dia_Chi_text.Text ?? "").Trim();

                // Auto-gen mã nếu bỏ trống (giữ thói quen cũ)
                if (string.IsNullOrWhiteSpace(maDonHang))
                {
                    maDonHang = GenerateDefaultMaDonHang();
                    guna2TextBox1.Text = maDonHang;
                }

                // Validate cơ bản
                if (string.IsNullOrWhiteSpace(donhangid))
                {
                    MessageBox.Show("Vui lòng nhập DonHangID.");
                    return;
                }
                if (string.IsNullOrEmpty(hopDongID) || string.IsNullOrEmpty(trangThaiID))
                {
                    MessageBox.Show("Vui lòng chọn Hợp đồng và Trạng thái.");
                    return;
                }

                // Ngày
                DateTime? ngayLayMau = Ngay_LayMau.Value.Date;
                DateTime? ngayDuKien = Ngay_Du_kien.Value.Date;
                DateTime? ngayThucTe = string.IsNullOrEmpty(_donHangID) ? (DateTime?)null : Ngay_Thuc_te.Value.Date;

                // Tạo DTO đầy đủ
                var dh = new DTO_DonHang
                {
                    DonHangID   = donhangid,
                    MaDonHang   = maDonHang,
                    HopDongID   = hopDongID,
                    TrangThaiID = trangThaiID,
                    GhiChu      = ghiChu,
                    IDKhachHang = khachhang,
                    DiaChi      = diaChi,
                    NgayLayMau  = ngayLayMau,
                    NgayDuKienTraKetQua = ngayDuKien,    // đã +15 tự động
                    NgayTraThucTe       = ngayThucTe     // chỉ có khi SỬA
                };

                if (string.IsNullOrEmpty(_donHangID))
                {
                    // THÊM: không set Ngày trả thực tế
                    BLL_DonHang.ThemDonHang(dh);
                    SavedDonHangID = donhangid;
                    MessageBox.Show("Thêm đơn hàng thành công!");
                }
                else
                {
                    // SỬA
                    BLL_DonHang.CapNhatDonHang(_donHangID, dh);
                    SavedDonHangID = donhangid;
                    MessageBox.Show("Cập nhật đơn hàng thành công!");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // ====== CÁC HÀM/EVT KHÁC GIỮ NGUYÊN ======
        private void guna2ComboBox4_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void guna2TextBox4_TextChanged(object sender, EventArgs e) { }
        private void guna2TextBox2_TextChanged(object sender, EventArgs e) { }
        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void guna2ComboBox5_SelectedIndexChanged(object sender, EventArgs e) { }
        private void guna2TextBox3_TextChanged(object sender, EventArgs e) { }
        private void guna2ComboBox1_SelectedIndexChanged_1(object sender, EventArgs e) { }
        private void guna2Button6_Click(object sender, EventArgs e) { this.DialogResult = DialogResult.Cancel; this.Close(); }
        private void label5_Click(object sender, EventArgs e) { }

        private string GenerateDefaultMaDonHang()
        {
            return "DH-" + DateTime.Now.ToString("yyyy-HHmmss");
        }

        private void Dia_Chi_text_TextChanged(object sender, EventArgs e) { }
    }
}
