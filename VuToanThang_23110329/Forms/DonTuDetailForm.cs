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
    public partial class DonTuDetailForm : Form
    {
        private readonly DonTuRepository _donTuRepository;
        private int _maDon;
        private bool _isReadOnly;

        public DonTuDetailForm(int maDon, bool isReadOnly = false)
        {
            InitializeComponent();
            _donTuRepository = new DonTuRepository();
            _maDon = maDon;
            _isReadOnly = isReadOnly;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var donTu = _donTuRepository.GetById(_maDon);
                if (donTu != null)
                {
                    // Display request details
                    if (_isReadOnly)
                    {
                        // Disable editing controls
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_isReadOnly) return;

            try
            {
                // Save request details
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
