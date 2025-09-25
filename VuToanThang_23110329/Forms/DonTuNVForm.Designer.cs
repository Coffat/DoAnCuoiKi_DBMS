using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class DonTuNVForm
    {
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private DataGridView dgvDonTu;
        private ComboBox cmbLoai, cmbTrangThai;
        private DateTimePicker dtpTuLuc, dtpDenLuc;
        private TextBox txtLyDo, txtSoGio;
        private Button btnThem, btnSua, btnXoa, btnLuu, btnHuy, btnLamMoi;
        private Panel pnlThongTin;
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
            this.Size = new Size(800, 550);
            this.Text = "Quản lý đơn từ - Nhân viên";
            this.Padding = new Padding(20);
            this.MinimumSize = new Size(800, 550);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.StartPosition = FormStartPosition.CenterParent;
            
            // Add preview controls for Designer
            var lblPreview = new Label
            {
                Text = "QUẢN LÝ ĐỚN TỪ - NHÂN VIÊN\n(Designer Preview)",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(50, 50),
                AutoSize = true
            };
            
            var pnlActions = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(700, 80),
                Location = new Point(50, 120)
            };
            
            var lblActions = new Label
            {
                Text = "📝 Buttons: Tạo đơn nghỉ | Tạo đơn OT | Xem chi tiết | Hủy đơn",
                ForeColor = Color.White,
                Location = new Point(20, 30),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            pnlActions.Controls.Add(lblActions);
            
            var pnlContent = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(700, 280),
                Location = new Point(50, 220)
            };
            
            var lblContent = new Label
            {
                Text = "📋 DANH SÁCH ĐƠN TỪ CỦA TÔI\n\nDataGridView: Đơn từ đã tạo\nColumns: Loại đơn | Từ lúc | Đến lúc | Số giờ | Lý do | Trạng thái\n\nInfo Panel: Chi tiết đơn từ được chọn\nTrạng thái: Chờ duyệt | Đã duyệt | Từ chối",
                ForeColor = Color.White,
                Location = new Point(20, 50),
                Size = new Size(660, 180),
                Font = new Font("Segoe UI", 11)
            };
            pnlContent.Controls.Add(lblContent);
            
            this.Controls.AddRange(new Control[] { lblPreview, pnlActions, pnlContent });
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
