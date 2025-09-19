using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VuToanThang_23110329.Models;
using VuToanThang_23110329.Repositories;

namespace VuToanThang_23110329.Forms
{
    public partial class PrintBangLuongForm : Form
    {
        private readonly BangLuongRepository _bangLuongRepository;
        private int _nam;
        private int _thang;

        public PrintBangLuongForm(int nam, int thang)
        {
            InitializeComponent();
            _bangLuongRepository = new BangLuongRepository();
            _nam = nam;
            _thang = thang;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var bangLuongList = _bangLuongRepository.GetByPeriod(_nam, _thang);
                // Display payroll data for printing
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                // Print payroll logic
                MessageBox.Show("In bảng lương thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi in: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
