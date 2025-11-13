// GUI/GUI_FormThemLoaiChiTieu.cs
using System;
using System.Data;
using System.Windows.Forms;
using BLL;
using DTO;

namespace GUI
{
    public partial class GUI_FormThemLoaiChiTieu : Form
    {
        private readonly BLL_ThongSoQuanTrac _bllThongSo = new BLL_ThongSoQuanTrac();
        private readonly BLL_PhongBan _bllPhongBan = new BLL_PhongBan();

        public bool Saved { get; private set; }
        public DTO_LoaiChiTieu NewLoaiChiTieu { get; private set; }

        public GUI_FormThemLoaiChiTieu()
        {
            InitializeComponent();
        }

        private void GUI_FormThemLoaiChiTieu_Load(object sender, EventArgs e)
        {
            LoadCombos();
        }

        private void LoadCombos()
        {
            try
            {
                // Đơn vị
                var dtDonVi = _bllThongSo.GetAllDonVi(); // cần cột DonViID, TenDonVi
                if (dtDonVi != null)
                {
                    cboDonVi.DataSource = dtDonVi;
                    cboDonVi.DisplayMember = dtDonVi.Columns.Contains("TenDonVi") ? "TenDonVi" : "DonVi";
                    cboDonVi.ValueMember   = "DonViID";
                    cboDonVi.SelectedIndex = -1;
                }

                // Phòng ban
                var dtPhongBan = BLL_PhongBan.GetAllPhongBan(); // static
                if (dtPhongBan != null)
                {
                    cboPhongBan.DataSource = dtPhongBan;
                    cboPhongBan.DisplayMember = dtPhongBan.Columns.Contains("TenPhongBan") ? "TenPhongBan" : "PhongBan";
                    cboPhongBan.ValueMember   = "PhongBanID";
                    cboPhongBan.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load danh mục: " + ex.Message);
            }
        }

        private string GenerateNewLoaiChiTieuID()
        {
            int max = 0;
            try
            {
                DataTable dt = _bllThongSo.GetAllLoaiChiTieu();
                if (dt != null && dt.Columns.Contains("LoaiChiTieuID"))
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        var raw = Convert.ToString(r["LoaiChiTieuID"]);
                        if (string.IsNullOrWhiteSpace(raw)) continue;

                        if (raw.StartsWith("LCT", StringComparison.OrdinalIgnoreCase))
                        {
                            string numPart = raw.Substring(3).Trim();
                            if (int.TryParse(numPart, out int n))
                            {
                                if (n > max) max = n;
                            }
                        }
                    }
                }
            }
            catch
            {
                // nếu lỗi thì cứ coi như max = 0 -> LCT001
            }

            return "LCT" + (max + 1).ToString("000");
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string ten = txtTenChiTieu.Text.Trim();
            string donViId = cboDonVi.SelectedValue as string;
            string phongBanId = cboPhongBan.SelectedValue as string;
            string giaTriChuan = txtGiaTriChuan.Text.Trim();

            if (string.IsNullOrWhiteSpace(ten))
            {
                MessageBox.Show("Vui lòng nhập Tên chỉ tiêu.");
                txtTenChiTieu.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(donViId))
            {
                MessageBox.Show("Vui lòng chọn Đơn vị.");
                cboDonVi.DroppedDown = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(phongBanId))
            {
                MessageBox.Show("Vui lòng chọn Phòng ban.");
                cboPhongBan.DroppedDown = true;
                return;
            }

            string newId = GenerateNewLoaiChiTieuID();

            var dto = new DTO_LoaiChiTieu
            {
                LoaiChiTieuID = newId,
                TenChiTieu    = ten,
                DonViID       = donViId,
                PhongBan      = phongBanId,
                GiaTriChuan   = giaTriChuan
            };

            try
            {
                // TODO: bảo đảm bạn đã thêm hàm InsertLoaiChiTieu trong BLL + DAL
                bool ok = InsertLoaiChiTieu(dto);
                if (!ok)
                {
                    MessageBox.Show("Không lưu được Loại chỉ tiêu mới.");
                    return;
                }

                Saved = true;
                NewLoaiChiTieu = dto;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu Loại chỉ tiêu: " + ex.Message);
            }
        }

        /// <summary>
        /// Hàm wrapper tạm gọi xuống BLL. Bạn có thể thay bằng _bllThongSo.InsertLoaiChiTieu(dto)
        /// nếu đã thêm chính thức trong BLL_ThongSoQuanTrac.
        /// </summary>
        private bool InsertLoaiChiTieu(DTO_LoaiChiTieu dto)
        {
            return _bllThongSo.InsertLoaiChiTieu(dto);
            
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
