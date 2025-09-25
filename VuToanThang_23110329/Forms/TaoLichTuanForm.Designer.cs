using System;
using System.Drawing;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class TaoLichTuanForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form properties
            this.Text = "Tạo lịch tuần";
            this.Size = new Size(1000, 700);
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimumSize = new Size(1000, 700);
            
            // Add preview controls for Designer
            var lblPreview = new Label
            {
                Text = "TẠO LỊCH TUẦN\n(Designer Preview)",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(50, 50),
                AutoSize = true
            };
            
            var pnlWeekSelector = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(900, 80),
                Location = new Point(50, 120)
            };
            
            var lblWeekSelector = new Label
            {
                Text = "📅 Chọn tuần: Từ ngày | Đến ngày | Buttons: Tạo lịch | Lưu | Hủy",
                ForeColor = Color.White,
                Location = new Point(20, 30),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            pnlWeekSelector.Controls.Add(lblWeekSelector);
            
            var pnlWeekGrid = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(900, 450),
                Location = new Point(50, 220)
            };
            
            var lblWeekGrid = new Label
            {
                Text = "📋 LỊCH TUẦN (DataGridView)\n\nColumns: Nhân viên | Thứ 2 | Thứ 3 | Thứ 4 | Thứ 5 | Thứ 6 | Thứ 7 | Chủ nhật\nRows: Danh sách nhân viên với dropdown chọn ca cho từng ngày\n\nTính năng: Tự động phân ca, Kiểm tra xung đột, Lưu lịch tuần",
                ForeColor = Color.White,
                Location = new Point(20, 50),
                Size = new Size(860, 350),
                Font = new Font("Segoe UI", 11)
            };
            pnlWeekGrid.Controls.Add(lblWeekGrid);
            
            this.Controls.AddRange(new Control[] { lblPreview, pnlWeekSelector, pnlWeekGrid });
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
    }
}
