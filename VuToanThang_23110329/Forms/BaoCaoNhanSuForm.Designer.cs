using System;
using System.Drawing;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class BaoCaoNhanSuForm
    {
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private TabControl tabControl;
        private DataGridView dgvTongQuan, dgvChamCong, dgvDonTu;
        private ComboBox cmbThang, cmbNam, cmbPhongBan, cmbTrangThai;
        private DateTimePicker dtpTuNgay, dtpDenNgay;
        private Button btnXuatBaoCao, btnLamMoi;
        private Panel pnlThongKe;
        private Label lblTongNV, lblDangLam, lblNghi, lblTongCong, lblTongDonTu, lblTitle;

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
            this.Text = "Báo cáo nhân sự";
            this.Padding = new Padding(20);
            this.MinimumSize = new Size(900, 650);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.StartPosition = FormStartPosition.CenterParent;
            
            // Add preview controls for Designer
            var lblPreview = new Label
            {
                Text = "BÁO CÁO NHÂN SỰ\n(Designer Preview)",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(50, 50),
                AutoSize = true
            };
            
            var pnlStats = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(800, 100),
                Location = new Point(50, 120)
            };
            
            var lblStats = new Label
            {
                Text = "📊 Thống kê: Tổng NV | Đang làm | Nghỉ việc | Nam | Nữ",
                ForeColor = Color.White,
                Location = new Point(20, 40),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            pnlStats.Controls.Add(lblStats);
            
            var pnlTabs = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(800, 400),
                Location = new Point(50, 240)
            };
            
            var lblTabs = new Label
            {
                Text = "📋 TABCONTROL BÁO CÁO\n\nTab 1: Danh sách nhân viên\n- DataGridView với tất cả thông tin nhân viên\n- Filter theo phòng ban, chức danh, trạng thái\n\nTab 2: Thống kê theo phòng ban\n- Biểu đồ phân bố nhân sự\n- Báo cáo chi tiết theo từng phòng ban\n\nButtons: Xuất Excel | In báo cáo | Làm mới",
                ForeColor = Color.White,
                Location = new Point(20, 50),
                Size = new Size(760, 300),
                Font = new Font("Segoe UI", 11)
            };
            pnlTabs.Controls.Add(lblTabs);
            
            this.Controls.AddRange(new Control[] { lblPreview, pnlStats, pnlTabs });
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
