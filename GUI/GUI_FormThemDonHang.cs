using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using Guna.UI2.WinForms;

namespace GUI
{
    public partial class GUI_FormThemDonHang : Form
    {
        private string _donHangID;

        public GUI_FormThemDonHang()
        {
            InitializeComponent();


        }
        public string SavedDonHangID { get; private set; }
        public GUI_FormThemDonHang(string donHangID = null)
        {
            InitializeComponent();
            _donHangID = donHangID;
        }







        private void guna2ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

       private void guna2Button5_Click(object sender, EventArgs e)
{
    try
    {
        string donhangid   = guna2TextBox2.Text.Trim();
        string maDonHang   = (guna2TextBox1.Text ?? "").Trim();
        string hopDongID   = guna2ComboBox4.SelectedValue?.ToString();
        string trangThaiID = guna2ComboBox5.SelectedValue?.ToString();
        string ghiChu      = (guna2TextBox4.Text ?? "").Trim();
        string khachhang   = guna2ComboBox1.SelectedValue?.ToString();

        // >>>>> NEW: tự sinh maDonHang nếu bỏ trống
        if (string.IsNullOrWhiteSpace(maDonHang))
        {
            maDonHang = GenerateDefaultMaDonHang();
            guna2TextBox1.Text = maDonHang; // hiển thị lại cho user
        }

        // Validate sau khi đã auto-generate
        if (string.IsNullOrEmpty(hopDongID) || string.IsNullOrEmpty(trangThaiID))
        {
            MessageBox.Show("Vui lòng chọn Hợp đồng và Trạng thái.");
            return;
        }

        if (string.IsNullOrEmpty(_donHangID))
        {
            // THÊM
            if (string.IsNullOrEmpty(donhangid))
            {
                MessageBox.Show("Vui lòng nhập DonHangID.");
                return;
            }
            BLL_DonHang.ThemDonHang(donhangid, maDonHang, hopDongID, trangThaiID, ghiChu, khachhang);
            SavedDonHangID = donhangid;
            MessageBox.Show("Thêm đơn hàng thành công!");
        }
        else
        {
            // SỬA
            BLL_DonHang.CapNhatDonHang(_donHangID, donhangid, maDonHang, hopDongID, trangThaiID, ghiChu, khachhang);
            SavedDonHangID = donhangid; // hoặc _donHangID theo bạn
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




        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void GUI_FormThemDonHang_Load(object sender, EventArgs e)
{
    // KH: hiển thị tên – lấy ID
    guna2ComboBox1.DataSource = BLL_KhachHang.GetAllKhachHang();
    guna2ComboBox1.DisplayMember = "TenCongTy";
    guna2ComboBox1.ValueMember   = "KhachHangID";

    // HĐ: hiển thị MaHopDong – lấy HopDongID (đã fix DAL trả đúng 2 cột)
    guna2ComboBox4.DataSource = BLL_DonHang.GetAllHopDong();
    guna2ComboBox4.DisplayMember = "MaHopDong";
    guna2ComboBox4.ValueMember   = "HopDongID";

    // Trạng thái
    guna2ComboBox5.DataSource   = BLL_DonHang.GetAllTrangThaiDonHang();
    guna2ComboBox5.DisplayMember= "TenTrangThai";
    guna2ComboBox5.ValueMember  = "TrangThaiID";

    if (string.IsNullOrEmpty(_donHangID))
    {
        guna2TextBox2.Text = BLL_DonHang.SinhMaDonHang();                          // DonHangID do bạn nhập/điền
    }
    else
    {
        var dt = BLL_DonHang.GetDonHangByID(_donHangID);
        if (dt.Rows.Count > 0)
        {
            var row = dt.Rows[0];
            guna2TextBox2.Text = row["DonHangID"].ToString();
            guna2TextBox1.Text = row["MaDonHang"].ToString();
            guna2ComboBox4.SelectedValue = row["HopDongID"].ToString();
            guna2ComboBox5.SelectedValue = row["TrangThaiID"].ToString();
            guna2TextBox4.Text           = row["GhiChu"]?.ToString();
            guna2ComboBox1.SelectedValue = row["IDKhachHang"]?.ToString();
        }
        // Không cho sửa mã đơn hàng
        
    }
}

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private string GenerateDefaultMaDonHang()
{
    // Ví dụ: DH-2025-0607-214530  (yyyy-MMdd-HHmmss)
    // Gọn hơn: DH-2025-214530 (yyyy-HHmmss)
    // Tuỳ bạn chọn format; mình dùng bản gọn mà vẫn unique:
    return "DH-" + DateTime.Now.ToString("yyyy-HHmmss");
}
    }
}
