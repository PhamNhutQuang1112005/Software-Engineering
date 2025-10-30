using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using BLL;

namespace GUI
{
    public partial class GUI_FormThemThongSoV2 : Form
    {
        private BLL_ThongSoMoiTruong bll = new BLL_ThongSoMoiTruong();
        private bool daThemMoi = false;
        public GUI_FormThemThongSoV2()
        {
            InitializeComponent();
            // ⚙️ Sau khi Initialize mới set cái này
          

            // ⚙️ Chèn dữ liệu thủ công
  

            // ⚙️ Xóa hàng trống cuối (nếu có)

            




        }

        private void frmThongSoMoiTruong_Load(object sender, EventArgs e)
        {
            dgvStation.DataSource = bll.GetThongSoMoiTruongView();

        }
        private void LoadThongSoMoiTruong()
        {
            dgvStation.AllowUserToAddRows = false;
        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           
        }

        private void GUI_FormThemThongSoV2_Load(object sender, EventArgs e)
        {

            dgvStation.DataSource = bll.GetThongSoMoiTruongView();
            dgvStation.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
        }


        private void guna2Button2_Click(object sender, EventArgs e)
        {
            GUI_FormThemDonHang gUI_FormThemDonHang = new GUI_FormThemDonHang();
            gUI_FormThemDonHang.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            dgvStation.AllowUserToAddRows = true;
            dgvStation.EditMode = DataGridViewEditMode.EditOnEnter;
            daThemMoi = true; // ✅ đánh dấu là đã thêm dòng mới





        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (!daThemMoi)
            {
                MessageBox.Show("⚠️ Bạn chưa thêm dữ liệu mới để lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dgvStation.Rows.Count > 1) // phải có ít nhất 1 dòng dữ liệu thật
            {
                // Dòng mới thêm là dòng kế cuối (dòng cuối thường là dòng trống để nhập mới)
                var row = dgvStation.Rows[dgvStation.Rows.Count - 2];

                string tenThongSo = row.Cells["Tên thông số"].Value?.ToString()?.Trim();
                string donVi = row.Cells["Đơn vị"].Value?.ToString()?.Trim();
                string giaTri = row.Cells["Vị trí đo 1"].Value?.ToString()?.Trim();
                string giaTriQuyChuan = row.Cells["Vị trí đo 2"].Value?.ToString()?.Trim();
                string ketLuan = row.Cells["Vị trí đo 3"].Value?.ToString()?.Trim();
                string phongban = row.Cells["Phòng phụ trách"].Value?.ToString()?.Trim();
                int giaTriSo = int.TryParse(row.Cells["Giá trị chuẩn"].Value?.ToString(), out int val) ? val : 0;

                // 🔍 Kiểm tra xem người dùng có thực sự nhập dữ liệu hay không
                if (string.IsNullOrEmpty(tenThongSo) &&
                    string.IsNullOrEmpty(donVi) &&
                    string.IsNullOrEmpty(giaTri) &&
                    string.IsNullOrEmpty(giaTriQuyChuan) &&
                    string.IsNullOrEmpty(ketLuan) &&
                    giaTriSo == 0 &&
                    string.IsNullOrEmpty(phongban))
                {
                    MessageBox.Show("⚠️ Bạn chưa nhập dữ liệu mới để lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Nếu có dữ liệu thì mới lưu
                if (bll.LuuThongSoMoiTruong(tenThongSo, donVi, giaTri, giaTriQuyChuan, ketLuan, giaTriSo, phongban))
                {
                    MessageBox.Show("✅ Lưu thông số thành công!");
                    GUI_FormThemThongSoV2_Load(sender, e);
                    daThemMoi = false; // ✅ đánh dấu là đã thêm dòng mới
                }
                else
                {
                    MessageBox.Show("❌ Lưu thất bại!");
                }
            }
            else
            {
                MessageBox.Show("⚠️ Không có dữ liệu để lưu!");
            }
            dgvStation.AllowUserToAddRows = false;
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            if (dgvStation.CurrentRow != null)
            {
                string tenThongSo = dgvStation.CurrentRow.Cells["Tên Thông Số"].Value.ToString();

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa thông số '{tenThongSo}' không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    BLL_ThongSoMoiTruong bll = new BLL_ThongSoMoiTruong();
                    bool success = bll.XoaThongSo(tenThongSo);

                    if (success)
                    {
                        MessageBox.Show("Xóa thành công!");
                        GUI_FormThemThongSoV2_Load(sender, e); 


                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại hoặc thông số không tồn tại!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!");
            }

        }
        }
    }

