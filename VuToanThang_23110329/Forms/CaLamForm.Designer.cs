using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class CaLamForm
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
            this.Dock = DockStyle.Fill;
            this.Text = "Quản lý ca làm";
            this.Padding = new Padding(20);
            this.MinimumSize = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            
            // Add preview controls for Designer
            var lblPreview = new Label
            {
                Text = "QUẢN LÝ CA LÀM\n(Designer Preview)",
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
                Text = "🔍 Tìm kiếm | Buttons: Thêm | Sửa | Xóa | Lưu | Hủy | Làm mới",
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
                Size = new Size(700, 400),
                Location = new Point(50, 220)
            };
            
            var lblContent = new Label
            {
                Text = "⏰ QUẢN LÝ CA LÀM\n\nDataGridView: Danh sách ca làm việc\nColumns: Tên ca | Giờ bắt đầu | Giờ kết thúc | Hệ số ca | Mô tả | Kích hoạt\n\nForm nhập liệu: Tên ca, Thời gian, Hệ số, Mô tả, Trạng thái\nHỗ trợ: Ca qua đêm, Validation thời gian, Kiểm tra trùng lặp",
                ForeColor = Color.White,
                Location = new Point(20, 50),
                Size = new Size(660, 300),
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
