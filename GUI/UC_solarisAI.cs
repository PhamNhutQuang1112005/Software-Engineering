using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL; // Giả sử bạn đã tạo BLL_Gemini
using DTO;
namespace GUI
{
    public partial class UC_solarisAI : UserControl
    {
        // 🔑 Hardcode API key Gemini
        private readonly string apiKey = "AIzaSyDKSsf8kBDh-aAwW1fp9Y69qIdVTArJKCY";

        private void AddHistoryBubble(string text, bool isUser)
        {
            // Panel chứa cả dòng tin nhắn
            Panel row = new Panel();
            row.Width = flowLayoutPanel1.ClientSize.Width - 10;
            row.AutoSize = true;
            row.Margin = new Padding(0, 5, 0, 5);

            // Bong bóng chat
            Label bubble = new Label();
            bubble.AutoSize = true;

            bubble.Padding = new Padding(10);
            bubble.Font = new Font("Segoe UI", 12);
            bubble.Text = text;
            bubble.BorderStyle = BorderStyle.FixedSingle;

            // User bên trái
            if (isUser)
            {
                bubble.MaximumSize = new Size(250, 0);
                bubble.BackColor = Color.FromArgb(173, 216, 230);
                row.Controls.Add(bubble);

                bubble.Location = new Point(10, 0);  // TRÁI
            }
            else // AI bên phải
            {
                bubble.MaximumSize = new Size(500, 0);
                bubble.BackColor = Color.LightGray;
                row.Controls.Add(bubble);

                bubble.Location = new Point(
                    row.Width - bubble.PreferredWidth - 10,   // PHẢI
                    0
                );
            }

            flowLayoutPanel1.Controls.Add(row);
            flowLayoutPanel1.ScrollControlIntoView(row);
        }



        public UC_solarisAI()
        {
            InitializeComponent();


        }

        // TextBox nhập prompt
        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        // Nút gửi prompt tới Gemini


        // Nếu muốn vẫn giữ API key TextBox (guna2TextBox1) nhưng chỉ để hiển thị
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            // Luôn hiển thị key
        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            string prompt = ohoithoai.Text.Trim();

            if (string.IsNullOrEmpty(prompt))
            {
                MessageBox.Show("Vui lòng nhập câu hỏi!");
                return;
            }

            try
            {
                // 👉 Hiển thị trạng thái chờ
                ohoithoai.Enabled = false;
                guna2Button1.Enabled = false;
                string oldText = ohoithoai.Text;
                ohoithoai.Text = "⏳ Đang gửi yêu cầu...";

                await Task.Yield(); // nhường luồng UI

                // 👉 Lấy dữ liệu từ BLL
                // 👉 Lấy dữ liệu từ BLL
                BLL_ThongSoQuanTrac bll = new BLL_ThongSoQuanTrac();
                DataTable dt = bll.GetAllThongSoMoiTruong();
                DataTable dt0 = bll.GetAllLoaiChiTieu();
                DataTable dt1 = bll.GetAllViTriLayMau();
                List<DTO_DonVi> listDonVi = bll.GetAllDonViDTO();
                DataTable dt2 = BLL_DonHang.GetAllDonHang();         // mapping DonHangID → MaDonHang (tên đơn hàng)
                Dictionary<string, int> soLuongDonTheoKhach = dt2.AsEnumerable()
    .GroupBy(r => r["IDKhachHang"].ToString())
    .ToDictionary(g => g.Key, g => g.Count());
                DataTable dtKH = BLL_KhachHang.GetAllKhachHang();



                // 👉 Tạo mô tả cho Gemini
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Dưới đây là dữ liệu các chỉ tiêu môi trường hiện tại (từ cơ sở dữ liệu):");
                sb.AppendLine("--------------------------------------------------");

                foreach (DataRow row in dt.Rows)
                {
                    string loaiChiTieuID = row["LoaiChiTieuID"].ToString();
                    string viTriID = row["ViTriID"].ToString();  // ⚠️ dt phải có cột này

                    // 👉 Lấy thông tin Loại Chỉ Tiêu
                    DataRow[] matchLoai = dt0.Select($"LoaiChiTieuID = '{loaiChiTieuID}'");
                    string tenChiTieu = matchLoai.Length > 0
                        ? matchLoai[0]["TenChiTieu"].ToString()
                        : "(Không rõ chỉ tiêu)";

                    // 👉 Lấy thông tin vị trí để biết DonHangID
                    DataRow[] matchViTri = dt1.Select($"ViTriID = '{viTriID}'");
                    string donHangID = matchViTri.Length > 0
                        ? matchViTri[0]["DonHangID"].ToString()
                        : null;

                    // 👉 Từ DonHangID → lấy tên đơn hàng (MaDonHang)
                    string tenDonHang = "(Không rõ đơn hàng)";
                    if (!string.IsNullOrEmpty(donHangID))
                    {
                        DataRow[] matchDonHang = dt2.Select($"DonHangID = '{donHangID}'");
                        if (matchDonHang.Length > 0)
                        {
                            tenDonHang = matchDonHang[0]["MaDonHang"].ToString(); // chính là tên đơn hàng
                        }
                    }
                    // 👉 Lấy KhachHangID từ đơn hàng
                    string khachHangID = "(Không rõ)";
                    int soDon = 0;
                    string khaNangTaiKy = "Không xác định";
                    string tenCongTy = "(Không rõ công ty)";
                    if (!string.IsNullOrEmpty(donHangID))
                    {
                        DataRow[] matchDonHang = dt2.Select($"DonHangID = '{donHangID}'");
                        if (matchDonHang.Length > 0)
                        {
                            khachHangID = matchDonHang[0]["IDKhachHang"].ToString();
                            DataRow[] matchKH = dtKH.Select($"KhachHangID = '{khachHangID}'");
                            if (matchKH.Length > 0)
                            {
                                tenCongTy = matchKH[0]["TenCongTy"].ToString();
                            }

                            // Lấy số lượng đơn hàng của khách
                            soDon = soLuongDonTheoKhach.ContainsKey(khachHangID)
                                ? soLuongDonTheoKhach[khachHangID]
                                : 0;

                            // Dự đoán tái ký
                            khaNangTaiKy = DuDoanTaiKy(soDon);
                        }
                    }



                    string donViID = row["DonViID"].ToString();
                    string tenDonVi = "(Không rõ đơn vị)";

                    var matchDonVi = listDonVi.FirstOrDefault(d => d.DonViID == donViID);
                    if (matchDonVi != null)
                    {
                        tenDonVi = matchDonVi.TenDonVi;
                    }

                    double.TryParse(row["GiaTri"].ToString(), out double thucTe);
                    double.TryParse(row["GiaTriQuyChuan"].ToString(), out double quyChuan);

                    double ratio = thucTe / quyChuan;
                    string ketLuan = "";

                    // Đánh giá theo mức độ vượt ngưỡng
                    if (ratio <= 1)
                    {
                        ketLuan = "Đạt chuẩn";
                    }
                    else if (ratio <= 1.2)
                    {
                        ketLuan = "Vượt nhẹ";
                    }
                    else if (ratio <= 2)
                    {
                        ketLuan = "Vượt trung bình – Có dấu hiệu ô nhiễm";
                    }
                    else if (ratio <= 5)
                    {
                        ketLuan = "Vượt cao – Nguy cơ ô nhiễm rõ rệt";
                    }
                    else
                    {
                        ketLuan = "Vượt cực cao – Ô nhiễm nghiêm trọng";
                    }

                    sb.AppendLine(
     $"Đơn hàng: {tenDonHang} | Khách hàng: {tenCongTy} | Số đơn: {soDon} → {khaNangTaiKy} | " +
     $"Chỉ tiêu: {tenChiTieu} → {thucTe} {tenDonVi} (Chuẩn {quyChuan}) → {ketLuan}"
 );




                }
                sb.AppendLine();
                sb.AppendLine("--------------------------------------------------");
                sb.AppendLine("THỐNG KÊ DỰ ĐOÁN TÁI KÝ THEO KHÁCH HÀNG:");
                sb.AppendLine();

                foreach (var kv in soLuongDonTheoKhach)
                {
                    string khID = kv.Key;
                    int count = kv.Value;

                    // Lấy tên công ty
                    string tenCongTyTK = "(Không rõ công ty)";
                    DataRow[] kh = dtKH.Select($"KhachHangID = '{khID}'");
                    if (kh.Length > 0)
                        tenCongTyTK = kh[0]["TenCongTy"].ToString();

                    // Dự đoán tái ký
                    string duDoan = DuDoanTaiKy(count);

                    sb.AppendLine($"{tenCongTyTK}: {count} đơn → Khả năng tái ký: {duDoan}");
                }



                // 👉 Ghép prompt người dùng
                string fullPrompt = $@"
Bạn là chuyên gia phân tích môi trường.
Dưới đây là dữ liệu từ hệ thống quan trắc:

{sb.ToString()}

Người dùng hỏi: {prompt}

Yêu cầu:
- Chỉ trả lời dựa vào dữ liệu trên.
- Nếu câu hỏi không liên quan, trả lời ngắn gọn: 'Tôi chỉ trả lời về dữ liệu quan trắc môi trường.'
";

                // 👉 Gửi tới Gemini
                BLL_Gemini gemini = new BLL_Gemini(apiKey);
                string result = await gemini.TaoNoiDungAsync(fullPrompt);

                // 👉 Hiển thị kết quả

                // Lưu vào lịch sử Messenger-style
                AddHistoryBubble("Bạn: " + prompt, true);
                AddHistoryBubble("AI: " + result, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi: " + ex.Message);
            }
            finally
            {
                // 👉 Khôi phục UI
                ohoithoai.Enabled = true;
                guna2Button1.Enabled = true;
                ohoithoai.Text = "";
            }
        }

        private string DuDoanTaiKy(int soDon)
        {
            if (soDon >= 5)
                return "Có khả năng tái ký cao";
            else if (soDon >= 4)
                return "Có khả năng tái ký";
            else if (soDon >= 2)
                return "Có khả năng tái ký nhưng thấp";
            else
                return "Không có khả năng tái ký";
        }

        private void ohoithoai_TextChanged(object sender, EventArgs e)
        {

        }

        private void UC_solarisAI_Load(object sender, EventArgs e)
        {

        }
    }
}
