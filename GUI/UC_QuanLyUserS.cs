using BLL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class UC_QuanLyUserS : UserControl
    {
        private readonly BLL_TaiKhoan bllNguoiDung = new BLL_TaiKhoan();
        private readonly Btnbeautifull _theme = new Btnbeautifull()
{
    Text = Color.White,
    Outline = Color.FromArgb(120, 195, 170),
    SearchFill = Color.Azure,
    SearchText = Color.Black,
    SearchPlaceholder = Color.Black
};
        public UC_QuanLyUserS()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void UC_QuanLyUserS_Load(object sender, EventArgs e)
        {
            HienThiDanhSachNguoiDung();
            PillStyler.Button(themuser,_theme);
            PillStyler.Button(guna2Button2,_theme);
            PillStyler.Button(guna2Button3,_theme);
            PillStyler.SearchBox(txtTimKiem,_theme, "Tìm kiếm người dùng");
        }

        private void HienThiDanhSachNguoiDung()
        {
            try
            {
                DataTable dt = bllNguoiDung.LayTatCaNguoiDung();
                dataGridView1.DataSource = dt;
                 dataGridView1.AllowUserToAddRows = false;
                dataGridView1.RowHeadersVisible = false; 
                // Tùy chỉnh cột cho đẹp
                dataGridView1.Columns["TenDangNhap"].HeaderText = "Tên đăng nhập";
                dataGridView1.Columns["HoVaTen"].HeaderText = "Họ và tên";
                dataGridView1.Columns["DienThoai"].HeaderText = "Điện thoại";
                dataGridView1.Columns["Email"].HeaderText = "Email";
                dataGridView1.Columns["TenVaiTro"].HeaderText = "Vai trò";
                dataGridView1.Columns["TenPhongBan"].HeaderText = "Phòng ban";
                dataGridView1.Columns["IsActive"].HeaderText = "Trạng thái";
                dataGridView1.Columns["VaiTroID"].Visible = false;
                dataGridView1.Columns["PhongBanID"].Visible = false;
                dataGridView1.Columns["NguoiDungID"].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu người dùng:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            GUI_FormThemNguoiDung frm = new GUI_FormThemNguoiDung();
    if (frm.ShowDialog() == DialogResult.OK)
        HienThiDanhSachNguoiDung();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
    {
        MessageBox.Show("Vui lòng chọn người dùng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
    }

    DataRowView drv = (DataRowView)dataGridView1.CurrentRow.DataBoundItem;
    DataRow row = drv.Row;

    GUI_FormThemNguoiDung frm = new GUI_FormThemNguoiDung(row);
    if (frm.ShowDialog() == DialogResult.OK)
        HienThiDanhSachNguoiDung();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
    {
        MessageBox.Show("Vui lòng chọn người dùng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
    }

    string id = dataGridView1.CurrentRow.Cells["NguoiDungID"].Value.ToString();
    if (MessageBox.Show("Bạn có chắc chắn muốn xóa người dùng này?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
    {
        BLL_TaiKhoan bll = new BLL_TaiKhoan();
        bll.XoaNguoiDung(id);
        HienThiDanhSachNguoiDung();
    }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
    DataTable dt = bllNguoiDung.TimKiemNguoiDung(keyword);
    dataGridView1.DataSource = dt;
        }
    }
}
