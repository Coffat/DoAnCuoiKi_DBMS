using System;
using System.Drawing;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class DonTuHRForm
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
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.Size = new Size(800, 550);
            this.Text = "Quản lý đơn từ - HR";
            this.Padding = new Padding(20);
            this.MinimumSize = new Size(800, 550);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.SuspendLayout();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
