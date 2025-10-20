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
        "giúp bạn tiết kiệm thời gian, nâng cao độ tin cậy và đảm bảo tuân thủ quy định pháp luật.<br><br>" +

        "Sứ mệnh của <b>GreenSol</b> là xây dựng một hệ sinh thái quản lý hiện đại, minh bạch và thân thiện, góp phần hỗ trợ doanh nghiệp " +
        "hoạt động hiệu quả hơn, đồng thời hướng tới một hành tinh sạch và bền vững. Trong tương lai, <b>GreenSol</b> sẽ không ngừng mở rộng, " +
        "tích hợp thêm nhiều tính năng thông minh, để trở thành người bạn đồng hành tin cậy của bạn trên hành trình bảo vệ môi trường.<br><br>" +

        "Khi bắt đầu sử dụng <b>GreenSol</b>, bạn không chỉ đang quản lý công việc hiệu quả hơn, mà còn cùng chúng tôi đặt một viên gạch vững chắc " +
        "cho tương lai xanh – nơi dữ liệu và công nghệ phục vụ cho sự phát triển bền vững.";

    // Tạo label HTML và chèn nội dung
    var html = new Guna.UI2.WinForms.Guna2HtmlLabel
    {
        Name = "lblIntro",
        BackColor = Color.Transparent,
        AutoSize = false,
        Dock = DockStyle.Fill,
        Padding = new Padding(2, 0, 2, 4),
        // Nếu muốn cỡ chữ lớn hơn, tăng size dưới đây
        Font = new Font("Segoe UI", 10f),
        Text = introHtml,
        ForeColor = Color.White
    };

    gradient_box_message.Controls.Add(html);

    // Khi thay đổi kích thước, vẽ lại khung gradient cho nét
    gradient_box_message.Resize += (s, _e) => gradient_box_message.Invalidate();
}


    }
}
