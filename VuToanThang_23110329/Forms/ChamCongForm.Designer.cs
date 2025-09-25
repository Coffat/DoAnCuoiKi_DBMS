using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class ChamCongForm
    {
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private DataGridView dgvChamCong, dgvLichChamCong;
        private DateTimePicker dtpTuNgay, dtpDenNgay, dtpGioVao, dtpGioRa;
        private ComboBox cmbNhanVien, cmbThangKhoa, cmbNamKhoa;
        private TextBox txtGhiChu;
        private Button btnTimKiem, btnCapNhat, btnKhoaCong, btnMoKhoaCong, btnLamMoi, btnXuatBaoCao;
        private Button btnCheckIn, btnCheckOut, btnRefreshStatus;
        private Panel pnlFilter, pnlThongTin, pnlCheckInOut;
        private TabControl tabControl;
        private Label lblTongGioCong, lblDiTre, lblVeSom;
        private Label lblTrangThaiHienTai, lblThongTinCa, lblThongTinChamCong;
        private Label lblTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            if (disposing && _refreshTimer != null)
            {
                _refreshTimer.Stop();
                _refreshTimer.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SuspendLayout();

            // Form properties
            this.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.Size = new System.Drawing.Size(900, 650);
            this.Text = "Quản lý Chấm công";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.MinimumSize = new System.Drawing.Size(900, 650);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            // Add preview title for Designer
            var lblPreview = new System.Windows.Forms.Label
            {
                Text = "QUẢN LÝ CHẤM CÔNG\n(Designer Preview)",
                Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(124, 77, 255),
                Location = new System.Drawing.Point(50, 20),
                AutoSize = true
            };

            // Check In/Out Panel Preview
            var pnlCheckInOutPreview = new System.Windows.Forms.Panel
            {
                BackColor = System.Drawing.Color.FromArgb(60, 60, 60),
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Size = new System.Drawing.Size(300, 120),
                Location = new System.Drawing.Point(50, 80)
            };

            var lblCheckInOutPreview = new System.Windows.Forms.Label
            {
                Text = "🕐 CHECK IN/OUT PANEL\n\n" +
                       "• Trạng thái: Chưa vào ca / Đang làm việc / Đã hoàn thành\n" +
                       "• Thông tin ca: Ca Sáng (06:00-14:00)\n" +
                       "• Chấm công: Vào: 06:00 | Ra: 14:00 | Công: 8h\n" +
                       "• Buttons: [CHECK IN] [CHECK OUT] [Làm mới]",
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(20, 15),
                Size = new System.Drawing.Size(260, 90),
                Font = new System.Drawing.Font("Segoe UI", 9)
            };
            pnlCheckInOutPreview.Controls.Add(lblCheckInOutPreview);

            // Filter Panel Preview
            var pnlFilterPreview = new System.Windows.Forms.Panel
            {
                BackColor = System.Drawing.Color.FromArgb(60, 60, 60),
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Size = new System.Drawing.Size(500, 120),
                Location = new System.Drawing.Point(370, 80)
            };

            var lblFilterPreview = new System.Windows.Forms.Label
            {
                Text = "🔍 BỘ LỌC CHẤM CÔNG\n\n" +
                       "• Từ ngày: [DatePicker] | Đến ngày: [DatePicker]\n" +
                       "• Nhân viên: [ComboBox] | Buttons: [Tìm kiếm] [Làm mới]\n" +
                       "• DataGridView: Danh sách chấm công với filter\n" +
                       "• Info Panel: Chi tiết chấm công được chọn",
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(20, 15),
                Size = new System.Drawing.Size(460, 90),
                Font = new System.Drawing.Font("Segoe UI", 9)
            };
            pnlFilterPreview.Controls.Add(lblFilterPreview);

            // Tab Control Preview
            var pnlTabPreview = new System.Windows.Forms.Panel
            {
                BackColor = System.Drawing.Color.FromArgb(60, 60, 60),
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Size = new System.Drawing.Size(800, 200),
                Location = new System.Drawing.Point(50, 220)
            };

            var lblTabPreview = new System.Windows.Forms.Label
            {
                Text = "📑 TAB CONTROL - 4 TABS\n\n" +
                       "📅 Chấm công hàng ngày: Filter + DataGridView + Info Panel\n" +
                       "📊 Lịch & chấm công: DataGridView tổng hợp\n" +
                       "🕐 Check In/Out: Real-time check in/out cho nhân viên\n" +
                       "🔒 Khóa công kỳ: Khóa dữ liệu chấm công theo tháng/năm\n\n" +
                       "Responsive layout: Vertical/Compact/Full based on width",
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(20, 15),
                Size = new System.Drawing.Size(760, 170),
                Font = new System.Drawing.Font("Segoe UI", 9)
            };
            pnlTabPreview.Controls.Add(lblTabPreview);

            // DataGridView Preview
            var pnlDataGridPreview = new System.Windows.Forms.Panel
            {
                BackColor = System.Drawing.Color.FromArgb(60, 60, 60),
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Size = new System.Drawing.Size(800, 150),
                Location = new System.Drawing.Point(50, 440)
            };

            var lblDataGridPreview = new System.Windows.Forms.Label
            {
                Text = "📋 DANH SÁCH CHẤM CÔNG\n\n" +
                       "┌─────────────────────────────────────────────────────────────────────────────┐\n" +
                       "│ STT │   Mã NV   │    Họ tên     │   Ngày     │  Giờ vào  │  Giờ ra   │  Giờ công  │  Trạng thái │\n" +
                       "├─────────────────────────────────────────────────────────────────────────────┤\n" +
                       "│  1  │  NV001    │ Nguyễn Văn A  │ 2024-01-15 │  06:00    │  14:00    │    8h     │   Đúng giờ  │\n" +
                       "│  2  │  NV002    │ Trần Thị B    │ 2024-01-15 │  06:15    │  13:45    │   7.5h    │   Đi trễ   │\n" +
                       "│  3  │  NV003    │ Lê Văn C      │ 2024-01-15 │  06:00    │  14:30    │    8h     │   Về sớm   │\n" +
                       "└─────────────────────────────────────────────────────────────────────────────┘",
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(760, 110),
                Font = new System.Drawing.Font("Consolas", 8)
            };
            pnlDataGridPreview.Controls.Add(lblDataGridPreview);

            this.Controls.AddRange(new Control[] { lblPreview, pnlCheckInOutPreview, pnlFilterPreview, pnlTabPreview, pnlDataGridPreview });

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
