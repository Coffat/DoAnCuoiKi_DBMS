using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class DonTuHRForm
    {
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private DataGridView dgvDonTu;
        private ComboBox cmbTrangThai, cmbLoai, cmbPhongBan;
        private DateTimePicker dtpTuNgay, dtpDenNgay;
        private Button btnTimKiem, btnDuyet, btnTuChoi, btnLamMoi, btnXuatBaoCao;
        private Panel pnlFilter, pnlThongKe, pnlThongTin;
        private Label lblTongSo, lblChoDuyet, lblDaDuyet, lblTuChoi, lblTitle;
        private TextBox txtGhiChuDuyet;

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
            this.SuspendLayout();
            
            // Form properties
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.Size = new Size(800, 550);
            this.Text = "Quản lý đơn từ - HR";
            this.Padding = new Padding(20);
            this.MinimumSize = new Size(800, 550);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.StartPosition = FormStartPosition.CenterParent;
            
            // Add preview controls for Designer
            var lblPreview = new Label
            {
                Text = "QUẢN LÝ ĐỚN TỪ - HR\n(Designer Preview)",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(50, 50),
                AutoSize = true
            };
            
            var pnlStats = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(700, 80),
                Location = new Point(50, 120)
            };
            
            var lblStats = new Label
            {
                Text = "📊 Thống kê: Chờ duyệt | Đã duyệt | Từ chối | Tổng đơn",
                ForeColor = Color.White,
                Location = new Point(20, 30),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            pnlStats.Controls.Add(lblStats);
            
            var pnlFilter = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(700, 100),
                Location = new Point(50, 220)
            };
            
            var lblFilter = new Label
            {
                Text = "🔍 Bộ lọc: Loại đơn | Trạng thái | Từ ngày | Đến ngày | Nhân viên\nButtons: Duyệt | Từ chối | Xem chi tiết",
                ForeColor = Color.White,
                Location = new Point(20, 30),
                Size = new Size(660, 40),
                Font = new Font("Segoe UI", 10)
            };
            pnlFilter.Controls.Add(lblFilter);
            
            var pnlContent = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(700, 150),
                Location = new Point(50, 340)
            };
            
            var lblContent = new Label
            {
                Text = "📋 DataGridView: Danh sách đơn từ cần xử lý\nInfo Panel: Chi tiết đơn từ được chọn",
                ForeColor = Color.White,
                Location = new Point(20, 60),
                AutoSize = true,
                Font = new Font("Segoe UI", 12)
            };
            pnlContent.Controls.Add(lblContent);
            
            this.Controls.AddRange(new Control[] { lblPreview, pnlStats, pnlFilter, pnlContent });

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
