using System;
using System.Drawing;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class BangLuongForm
    {
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private DataGridView dgvBangLuong;
        private ComboBox cmbThang, cmbNam, cmbTrangThai;
        private Button btnTimKiem, btnXuatExcel, btnInBangLuong, btnLamMoi;
        private Panel pnlFilter, pnlSummary;
        private Label lblTongNhanVien, lblTongLuong, lblLuongTB, lblTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.Text = "Bảng lương";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.MinimumSize = new System.Drawing.Size(900, 650);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            // Add preview title for Designer
            var lblPreview = new System.Windows.Forms.Label
            {
                Text = "BẢNG LƯƠNG NHÂN VIÊN\n(Designer Preview)",
                Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(124, 77, 255),
                Location = new System.Drawing.Point(50, 20),
                AutoSize = true
            };

            // Filter Panel Preview
            var pnlFilterPreview = new System.Windows.Forms.Panel
            {
                BackColor = System.Drawing.Color.FromArgb(60, 60, 60),
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Size = new System.Drawing.Size(800, 80),
                Location = new System.Drawing.Point(50, 80)
            };

            var lblFilterPreview = new System.Windows.Forms.Label
            {
                Text = "🔍 BỘ LỌC TÌM KIẾM\n\nTháng: [1-12] | Năm: [2023-2025] | Trạng thái: [Tất cả/Mở/Đóng]\n\nButtons: [Tìm kiếm] [Xuất Excel] [In bảng lương] [Làm mới]",
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(20, 15),
                Size = new System.Drawing.Size(760, 50),
                Font = new System.Drawing.Font("Segoe UI", 9)
            };
            pnlFilterPreview.Controls.Add(lblFilterPreview);

            // Summary Panel Preview
            var pnlSummaryPreview = new System.Windows.Forms.Panel
            {
                BackColor = System.Drawing.Color.FromArgb(60, 60, 60),
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Size = new System.Drawing.Size(800, 60),
                Location = new System.Drawing.Point(50, 180)
            };

            var lblSummaryPreview = new System.Windows.Forms.Label
            {
                Text = "📊 THỐNG KÊ TỔNG HỢP\n\n👥 Tổng nhân viên: 150 | 💰 Tổng chi lương: 1,500,000,000 VNĐ | 📈 Lương TB: 10,000,000 VNĐ",
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(20, 15),
                Size = new System.Drawing.Size(760, 30),
                Font = new System.Drawing.Font("Segoe UI", 9)
            };
            pnlSummaryPreview.Controls.Add(lblSummaryPreview);

            // DataGridView Preview
            var pnlDataGridPreview = new System.Windows.Forms.Panel
            {
                BackColor = System.Drawing.Color.FromArgb(60, 60, 60),
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Size = new System.Drawing.Size(800, 300),
                Location = new System.Drawing.Point(50, 260)
            };

            var lblDataGridPreview = new System.Windows.Forms.Label
            {
                Text = "📋 DANH SÁCH BẢNG LƯƠNG\n\n" +
                       "┌─────────────────────────────────────────────────────────────────────────────┐\n" +
                       "│ STT │   Mã NV   │    Họ tên     │  Lương CB  │  Phụ cấp  │  Khấu trừ  │  Thuế BH  │  Thực lãnh  │\n" +
                       "├─────────────────────────────────────────────────────────────────────────────┤\n" +
                       "│  1  │  NV001    │ Nguyễn Văn A  │ 8,000,000 │  500,000  │  200,000  │  400,000  │ 8,900,000  │\n" +
                       "│  2  │  NV002    │ Trần Thị B    │ 7,500,000 │  300,000  │  150,000  │  375,000  │ 7,650,000  │\n" +
                       "│  3  │  NV003    │ Lê Văn C      │ 9,000,000 │  800,000  │  250,000  │  450,000  │ 10,100,000 │\n" +
                       "└─────────────────────────────────────────────────────────────────────────────┘\n\n" +
                       "Double-click để chỉnh sửa chi tiết",
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(760, 260),
                Font = new System.Drawing.Font("Consolas", 8)
            };
            pnlDataGridPreview.Controls.Add(lblDataGridPreview);

            // Action Panel Preview
            var pnlActionPreview = new System.Windows.Forms.Panel
            {
                BackColor = System.Drawing.Color.FromArgb(70, 70, 70),
                Size = new System.Drawing.Size(800, 60),
                Location = new System.Drawing.Point(50, 580)
            };

            var lblActionPreview = new System.Windows.Forms.Label
            {
                Text = "⚡ CHỨC NĂNG\n\n• Tìm kiếm: Lọc theo tháng/năm/trạng thái\n• Xuất Excel: Export ra file Excel\n• In bảng lương: In báo cáo chi tiết\n• Làm mới: Reset bộ lọc\n• Double-click row: Chỉnh sửa chi tiết",
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(20, 10),
                Size = new System.Drawing.Size(760, 40),
                Font = new System.Drawing.Font("Segoe UI", 9)
            };
            pnlActionPreview.Controls.Add(lblActionPreview);

            this.Controls.AddRange(new Control[] { lblPreview, pnlFilterPreview, pnlSummaryPreview, pnlDataGridPreview, pnlActionPreview });

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
