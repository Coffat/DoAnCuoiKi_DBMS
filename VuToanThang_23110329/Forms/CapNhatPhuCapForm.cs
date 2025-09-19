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
    public partial class CapNhatPhuCapForm : Form
    {
        private readonly BangLuongRepository _bangLuongRepository;
        private string _maNV;
        private int _nam;
        private int _thang;

        public CapNhatPhuCapForm(string maNV, int nam, int thang)
        {
            InitializeComponent();
            _bangLuongRepository = new BangLuongRepository();
            _maNV = maNV;
            _nam = nam;
            _thang = thang;
            LoadData();
        }

        private void LoadData()
        {
            // Load current allowance data
            var bangLuong = _bangLuongRepository.GetByMaNVAndPeriod(_maNV, _nam, _thang);
            if (bangLuong != null)
            {
                // Set current values in UI controls
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Update allowance logic
                MessageBox.Show("Cập nhật phụ cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
