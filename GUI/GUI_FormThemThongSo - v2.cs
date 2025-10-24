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
        public GUI_FormThemThongSoV2()
        {
            InitializeComponent();
            // ⚙️ Sau khi Initialize mới set cái này
          

            // ⚙️ Chèn dữ liệu thủ công
            dgvStation.Rows.Add("Amoni(NH₄⁺ tính theo N) 2024", "mg/L", "hcm", "hcm", "hcm", 5);
            dgvStation.Rows.Add("Độ màu 2024-HT", "Pt-Co", "hcm", "hcm", "hcm", 50);
            dgvStation.Rows.Add("Nhiệt độ 2024-HT", "°C", "hcm", "hcm", "hcm", 40);
            dgvStation.Rows.Add("Lưu lượng 2024-HT", "m³/h", "hcm", "hcm", "hcm", 100);
            dgvStation.Rows.Add("Nhu cầu oxy hóa học (COD) 2024", "mg/L", "hcm", "hcm", "hcm", 75);
            dgvStation.Rows.Add("pH2024-HT", "mg/L", "hcm", "hcm", "hcm", "6 - 9");
            dgvStation.Rows.Add("Tổng chất rắn lơ lửng (TSS) 2024", "mg/L", "hcm", "hcm", "hcm", 50);
            dgvStation.Rows.Add("Tổng dầu mỡ khoáng 2024", "mg/L", "hcm", "hcm", "hcm", 5);
            dgvStation.Rows.Add("Tổng N 2024", "mg/L", "hcm", "hcm", "hcm", 20);
            dgvStation.Rows.Add("Tổng P 2024", "mg/L", "hcm", "hcm", "hcm", 4);

            // ⚙️ Xóa hàng trống cuối (nếu có)

            




        }

        private void frmThongSoMoiTruong_Load(object sender, EventArgs e)
        {
           
            
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
            dgvStation.AllowUserToAddRows = false;
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
            if (dgvStation.Rows.Count > 0)
            {
                var lastRow = dgvStation.Rows[dgvStation.Rows.Count - 1];
                bool isEmpty = true;

                foreach (DataGridViewCell cell in lastRow.Cells)
                {
                    if (cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString()))
                    {
                        isEmpty = false;
                        break;
                    }
                }

                if (isEmpty)
                {
                    MessageBox.Show("Bạn đang có 1 hàng trống để nhập, vui lòng điền trước khi thêm hàng mới!");
                    return;
                }
            }

            // Thêm 1 hàng trống để người dùng nhập
            dgvStation.Rows.Add();
            int lastIndex = dgvStation.Rows.Count - 1;
            dgvStation.CurrentCell = dgvStation.Rows[lastIndex].Cells[0]; // chọn ô đầu tiên để nhập
            dgvStation.BeginEdit(true);


        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvStation.Rows)
            {
                if (row.IsNewRow) continue; // bỏ qua hàng trống cuối

                string tenThongSo = row.Cells[0].Value?.ToString() ?? "";
                string donVi = row.Cells[1].Value?.ToString() ?? "";
                string muc1 = row.Cells[2].Value?.ToString() ?? "";
                string muc2 = row.Cells[3].Value?.ToString() ?? "";
                string muc3 = row.Cells[4].Value?.ToString() ?? "";
                string mucMax = row.Cells[5].Value?.ToString() ?? "";

              
                if (string.IsNullOrWhiteSpace(tenThongSo)) continue;

             
               
            }

            MessageBox.Show("Lưu dữ liệu thành công!");
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            if (dgvStation.CurrentRow != null && !dgvStation.CurrentRow.IsNewRow)
            {
                // Xóa khỏi DataGridView
                dgvStation.Rows.Remove(dgvStation.CurrentRow);

                // Nếu bạn đang lưu tạm trong BLL, cũng xóa khỏi danh sách
                // Giả sử dựa vào cột 0 (Tên Thông Số) để tìm
                string ten = dgvStation.CurrentRow.Cells[0].Value?.ToString() ?? "";

            }
        }
    }
}
