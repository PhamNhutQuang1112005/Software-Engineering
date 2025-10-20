using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class UC_TrangChu : UserControl
    {
        public UC_TrangChu()
        {
            InitializeComponent();
        }

        private void UC_TrangChu_Load(object sender, EventArgs e)
{
    // Khung chứa văn bản (panel) – nhớ đúng tên control của bạn
    gradient_box_message.Padding = new Padding(16);
    gradient_box_message.BackColor = Color.Transparent;
    gradient_box_message.Controls.Clear();

    // Nội dung văn bản (HTML đơn giản: <b>, <br>)
    string introHtml =
        "<b>GreenSol</b> ra đời với một mục tiêu rõ ràng: mang đến những giải pháp thông minh và bền vững cho công tác quan trắc môi trường. " +
        "Chúng tôi tin rằng một tương lai xanh hơn bắt đầu từ việc quản lý minh bạch, dữ liệu chính xác và những công cụ giúp doanh nghiệp " +
        "cũng như cơ quan quản lý dễ dàng theo dõi, đánh giá chất lượng môi trường xung quanh.<br><br>" +

        "<b>GreenSol</b> được thành lập để giải quyết những khó khăn thường gặp trong việc quản lý hợp đồng, đơn hàng và lịch trình quan trắc. " +
        "Thay vì phụ thuộc vào giấy tờ phức tạp hay bảng tính thủ công dễ sai sót, chúng tôi mang đến một nền tảng số hóa toàn diện – " +
        "giúp bạn tiết kiệm thời gian, nâng cao độ tin cậy và đảm bảo tuân thủ quy định pháp luật.<br><br>";

    // Tạo label HTML và chèn nội dung
    var html = new Guna.UI2.WinForms.Guna2HtmlLabel
    {
        Name = "lblIntro",
        BackColor = Color.Transparent,
        AutoSize = false,
        Dock = DockStyle.Fill,
        Padding = new Padding(2, 0, 2, 4),
        // Nếu muốn cỡ chữ lớn hơn, tăng size dưới đây
        Font = new Font("Segoe UI", 16f),
        Text = introHtml,
        ForeColor = Color.White
    };

    gradient_box_message.Controls.Add(html);

    // Khi thay đổi kích thước, vẽ lại khung gradient cho nét
    gradient_box_message.Resize += (s, _e) => gradient_box_message.Invalidate();
}

        private void slogan3_Click(object sender, EventArgs e)
        {

        }
    }
}
