using System;
using System.Drawing;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class BaoCaoLuongForm
    {
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private TabControl tabControl;
        private DataGridView dgvBaoCaoThang, dgvBaoCaoNam, dgvSoSanh;
        private ComboBox cmbThang, cmbNam, cmbNamSoSanh, cmbPhongBan;
        private Button btnXemBaoCao, btnXuatExcel, btnLamMoi;
        private Panel pnlThongKe;
        private Label lblTongLuong, lblLuongTB, lblCaoNhat, lblThapNhat, lblTongNV, lblTitle;

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
            this.Text = "Báo cáo lương";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.MinimumSize = new System.Drawing.Size(900, 650);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            // Add preview title for Designer
            var lblPreview = new System.Windows.Forms.Label
            {
                Text = "BÁO CÁO LƯƠNG NHÂN VIÊN\n(Designer Preview)",
                Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(124, 77, 255),
                Location = new System.Drawing.Point(50, 20),
                AutoSize = true
            };

            // Statistics Panel Preview
            var pnlStatsPreview = new System.Windows.Forms.Panel
            {
                BackColor = System.Drawing.Color.FromArgb(60, 60, 60),
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Size = new System.Drawing.Size(800, 100),
                Location = new System.Drawing.Point(50, 80)
            };

            var lblStatsPreview = new System.Windows.Forms.Label
            {
                Text = "📊 THỐNG KÊ LƯƠNG\n\n" +
                       "👥 Tổng NV: 150 | 💰 Tổng lương: 1,500,000,000 VNĐ | 📈 Lương TB: 10,000,000 VNĐ\n" +
                       "🏆 Cao nhất: 25,000,000 VNĐ | 🏅 Thấp nhất: 5,000,000 VNĐ",
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(20, 15),
                Size = new System.Drawing.Size(760, 70),
                Font = new System.Drawing.Font("Segoe UI", 9)
            };
            pnlStatsPreview.Controls.Add(lblStatsPreview);

            // Tab Control Preview
            var pnlTabPreview = new System.Windows.Forms.Panel
            {
                BackColor = System.Drawing.Color.FromArgb(60, 60, 60),
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Size = new System.Drawing.Size(800, 120),
                Location = new System.Drawing.Point(50, 200)
            };

            var lblTabPreview = new System.Windows.Forms.Label
            {
                Text = "📑 TAB CONTROL - 3 TABS\n\n" +
                       "📅 Báo cáo theo tháng: Tháng/Năm filter + DataGridView\n" +
                       "📊 Báo cáo theo năm: Năm filter + DataGridView\n" +
                       "⚖️ So sánh lương: 2 năm comparison + DataGridView",
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(20, 15),
                Size = new System.Drawing.Size(760, 90),
                Font = new System.Drawing.Font("Segoe UI", 9)
            };
            pnlTabPreview.Controls.Add(lblTabPreview);

            // DataGridView Preview
            var pnlDataGridPreview = new System.Windows.Forms.Panel
            {
                BackColor = System.Drawing.Color.FromArgb(60, 60, 60),
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Size = new System.Drawing.Size(800, 200),
                Location = new System.Drawing.Point(50, 340)
            };

            var lblDataGridPreview = new System.Windows.Forms.Label
            {
                Text = "📋 DANH SÁCH BÁO CÁO LƯƠNG\n\n" +
                       "┌─────────────────────────────────────────────────────────────────────────────┐\n" +
                       "│ STT │   Mã NV   │    Họ tên     │  Lương CB  │  Phụ cấp  │  Khấu trừ  │  Thuế BH  │  Thực lãnh  │\n" +
                       "├─────────────────────────────────────────────────────────────────────────────┤\n" +
                       "│  1  │  NV001    │ Nguyễn Văn A  │ 8,000,000 │  500,000  │  200,000  │  400,000  │ 8,900,000  │\n" +
                       "│  2  │  NV002    │ Trần Thị B    │ 7,500,000 │  300,000  │  150,000  │  375,000  │ 7,650,000  │\n" +
                       "│  3  │  NV003    │ Lê Văn C      │12,000,000 │1,000,000  │  300,000  │  600,000  │13,100,000 │\n" +
                       "└─────────────────────────────────────────────────────────────────────────────┘",
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(760, 160),
                Font = new System.Drawing.Font("Consolas", 8)
            };
            pnlDataGridPreview.Controls.Add(lblDataGridPreview);

            // Action Buttons Preview
            var pnlActionPreview = new System.Windows.Forms.Panel
            {
                BackColor = System.Drawing.Color.FromArgb(70, 70, 70),
                Size = new System.Drawing.Size(800, 60),
                Location = new System.Drawing.Point(50, 560)
            };

            var lblActionPreview = new System.Windows.Forms.Label
            {
                Text = "⚡ CHỨC NĂNG\n\n• Xem báo cáo: Load dữ liệu theo filter\n• Xuất Excel: Export ra file Excel\n• Làm mới: Reset tất cả bộ lọc\n• Tab switching: Chuyển đổi giữa các loại báo cáo",
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(20, 10),
                Size = new System.Drawing.Size(760, 40),
                Font = new System.Drawing.Font("Segoe UI", 9)
            };
            pnlActionPreview.Controls.Add(lblActionPreview);

            this.Controls.AddRange(new Control[] { lblPreview, pnlStatsPreview, pnlTabPreview, pnlDataGridPreview, pnlActionPreview });

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
