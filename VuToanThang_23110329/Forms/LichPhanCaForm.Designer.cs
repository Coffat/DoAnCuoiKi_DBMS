using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class LichPhanCaForm
    {
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private DataGridView dgvLichPhanCa;
        private DateTimePicker dtpTuNgay, dtpDenNgay, dtpNgayLam;
        private ComboBox cmbNhanVien, cmbCaLam, cmbTrangThai;
        private Button btnThem, btnSua, btnXoa, btnLuu, btnHuy, btnLamMoi, btnTimKiem, btnTaoLichTuan, btnXoaTimKiem;
        private Panel pnlThongTin, pnlFilter;
        private TextBox txtTimKiem;
        private Label lblTitle;

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
            this.Size = new Size(900, 650);
            this.Text = "Lịch phân ca";
            this.Padding = new Padding(20);
            this.MinimumSize = new Size(900, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            
            // Add preview controls for Designer
            var lblPreview = new Label
            {
                Text = "LỊCH PHÂN CA\n(Designer Preview)",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(50, 50),
                AutoSize = true
            };
            
            var pnlFilter = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(800, 80),
                Location = new Point(50, 120)
            };
            
            var lblFilter = new Label
            {
                Text = "Bộ lọc: Tuần | Tháng | Năm | Nhân viên | Ca làm | Trạng thái",
                ForeColor = Color.White,
                Location = new Point(20, 30),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            pnlFilter.Controls.Add(lblFilter);
            
            var pnlButtons = new Panel
            {
                BackColor = Color.FromArgb(70, 70, 70),
                Size = new Size(800, 50),
                Location = new Point(50, 220)
            };
            
            var lblButtons = new Label
            {
                Text = "Buttons: Thêm | Sửa | Xóa | Lưu | Hủy | Làm mới | Tạo lịch tuần",
                ForeColor = Color.White,
                Location = new Point(20, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            pnlButtons.Controls.Add(lblButtons);
            
            var pnlContent = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(800, 300),
                Location = new Point(50, 290)
            };
            
            var lblContent = new Label
            {
                Text = "DataGridView: Danh sách lịch phân ca\nInfo Panel: Thông tin chi tiết lịch được chọn",
                ForeColor = Color.White,
                Location = new Point(20, 130),
                AutoSize = true,
                Font = new Font("Segoe UI", 12)
            };
            pnlContent.Controls.Add(lblContent);
            
            this.Controls.AddRange(new Control[] { lblPreview, pnlFilter, pnlButtons, pnlContent });

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
