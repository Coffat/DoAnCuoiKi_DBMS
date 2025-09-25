using System;
using System.Drawing;
using System.Windows.Forms;
using VuToanThang_23110329.Controls;

namespace VuToanThang_23110329.Forms
{
    partial class NhanVienForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.Size = new Size(1400, 900);
            this.Text = "Quản lý nhân viên";
            this.Padding = new Padding(20);
            this.StartPosition = FormStartPosition.CenterParent;
            
            // Add preview controls for Designer
            var lblPreview = new Label
            {
                Text = "QUẢN LÝ NHÂN VIÊN\n(Designer Preview)",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(50, 50),
                AutoSize = true
            };
            
            var pnlSearch = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(1300, 80),
                Location = new Point(50, 120)
            };
            
            var lblSearch = new Label
            {
                Text = "🔍 Tìm kiếm: TextBox | Filter trạng thái | Buttons: Thêm | Sửa | Xóa | Khôi phục | Làm mới",
                ForeColor = Color.White,
                Location = new Point(20, 30),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            pnlSearch.Controls.Add(lblSearch);
            
            var pnlDataGrid = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(1300, 600),
                Location = new Point(50, 220)
            };
            
            var lblDataGrid = new Label
            {
                Text = "👥 DANH SÁCH NHÂN VIÊN (DataGridView)\n\nColumns: Mã NV | Họ tên | Ngày sinh | Giới tính | Điện thoại | Email\n         Địa chỉ | Ngày vào làm | Trạng thái | Phòng ban | Chức danh | Lương CB\n\nTính năng: Double-click để xem chi tiết, Context menu, Export Excel",
                ForeColor = Color.White,
                Location = new Point(20, 200),
                Size = new Size(1260, 200),
                Font = new Font("Segoe UI", 11)
            };
            pnlDataGrid.Controls.Add(lblDataGrid);
            
            this.Controls.AddRange(new Control[] { lblPreview, pnlSearch, pnlDataGrid });
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
