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
    public partial class PrintPayslipForm : Form
    {
        private readonly BangLuongRepository _bangLuongRepository;
        private readonly NhanVienRepository _nhanVienRepository;
        private string _maNV;
        private int _nam;
        private int _thang;

        public PrintPayslipForm(string maNV, int nam, int thang)
        {
            InitializeComponent();
            _bangLuongRepository = new BangLuongRepository();
            _nhanVienRepository = new NhanVienRepository();
            _maNV = maNV;
            _nam = nam;
            _thang = thang;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var nhanVien = _nhanVienRepository.GetByMaNV(_maNV);
                var bangLuong = _bangLuongRepository.GetByMaNVAndPeriod(_maNV, _nam, _thang);
                
                if (nhanVien != null && bangLuong != null)
                {
                    // Display payslip data
                }
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
                // Print payslip logic
                MessageBox.Show("In phiếu lương thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
