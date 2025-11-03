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
    // Bố cục vùng hiển thị
    gradient_box_message.Padding = new Padding(16);
    gradient_box_message.BackColor = Color.Transparent;
    gradient_box_message.Controls.Clear();
    gradient_box_message.RightToLeft = RightToLeft.No; // đảm bảo thanh cuộn ở bên phải

    // Panel cuộn chứa nội dung
    var scrollHost = new Panel
    {
        Name = "scrollHost",
        Dock = DockStyle.Fill,
        AutoScroll = false, // nếu cần cuộn thì đặt true
        BackColor = Color.Transparent,
        RightToLeft = RightToLeft.No
    };
    gradient_box_message.Controls.Add(scrollHost);

    // Panel nội dung tự giãn theo chiều cao
    var contentPanel = new Panel
    {
        Name = "contentPanel",
        AutoSize = true,
        AutoSizeMode = AutoSizeMode.GrowAndShrink,
        Dock = DockStyle.Top,
        BackColor = Color.Transparent,
        RightToLeft = RightToLeft.No,
        Padding = new Padding(35, 0, 0, 0) // dịch nhẹ sang phải/trái (hiệu quả thật sự)
    };
    scrollHost.Controls.Add(contentPanel);

    // Nội dung HTML có justify
    string introHtml = @"
<div style='text-align: justify; text-justify: inter-word; line-height: 1.5;'>
    <p><b>GreenSol</b> ra đời với một mục tiêu rõ ràng: mang đến những giải pháp thông minh và bền vững cho công tác quan trắc môi trường. 
    Chúng tôi tin rằng một tương lai xanh hơn bắt đầu từ việc quản lý minh bạch, dữ liệu chính xác và những công cụ giúp doanh nghiệp 
    cũng như cơ quan quản lý dễ dàng theo dõi, đánh giá chất lượng môi trường xung quanh.</p>

    <p><b>GreenSol</b> được thành lập để giải quyết những khó khăn thường gặp trong việc quản lý hợp đồng, đơn hàng và lịch trình quan trắc. 
    Thay vì phụ thuộc vào giấy tờ phức tạp hay bảng tính thủ công dễ sai sót, chúng tôi mang đến một nền tảng số hóa toàn diện – 
    giúp bạn tiết kiệm thời gian và nguồn lực.</p>
</div>";

    var html = new Guna.UI2.WinForms.Guna2HtmlLabel
    {
        Name = "lblIntro",
        BackColor = Color.Transparent,
        AutoSize = true,               // tự giãn theo chiều cao
                        
        Font = new Font("Segoe UI", 13.5f),
        Text = introHtml,
        ForeColor = Color.White,
        RightToLeft = RightToLeft.No,
        Location = new Point(contentPanel.Padding.Left, 0) // vị trí ban đầu
    };

    contentPanel.Controls.Add(html);

    // Tùy chọn căn giữa nội dung trong container (không phải căn giữa chữ)
    bool centerInContainer = false; // bật = căn giữa; tắt = dùng padding để dịch nhẹ sang phải

    void UpdateTextWidthAndPosition()
    {
        int scrollBarWidth = (scrollHost.VerticalScroll.Visible ? SystemInformation.VerticalScrollBarWidth : 0);
        int availableWidth = Math.Max(
            100,
            scrollHost.ClientSize.Width
            - scrollBarWidth
            - gradient_box_message.Padding.Horizontal
            - contentPanel.Padding.Horizontal
        );

        // Giới hạn bề rộng để bọc dòng đúng
        html.MaximumSize = new Size(availableWidth, 0);

        // Cập nhật kích thước ưa thích để tính vị trí
        var preferred = html.PreferredSize;
        int left = contentPanel.Padding.Left;

        if (centerInContainer)
        {
            // căn giữa control trong vùng khả dụng, không ảnh hưởng text-align: justify
            left = contentPanel.Padding.Left + Math.Max(0, (availableWidth - preferred.Width) / 2);
        }

        html.Location = new Point(left, 0);
    }

    // Gọi lần đầu và gắn các sự kiện để cập nhật động
    UpdateTextWidthAndPosition();
    gradient_box_message.Resize += (s, _e) => { UpdateTextWidthAndPosition(); gradient_box_message.Invalidate(); };
    scrollHost.SizeChanged += (s, _e) => UpdateTextWidthAndPosition();
    scrollHost.Layout += (s, _e) => UpdateTextWidthAndPosition();
}

        private void slogan3_Click(object sender, EventArgs e)
        {

        }

        private void gradient_box_message_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
