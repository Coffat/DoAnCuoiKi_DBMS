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
    public partial class TaoLichTuanForm : Form
    {
        private readonly LichPhanCaRepository _lichPhanCaRepository;
        private readonly NhanVienRepository _nhanVienRepository;
        private readonly CaLamRepository _caLamRepository;
        private DateTime _startDate;
        private List<NhanVien> _employees;
        private List<CaLam> _shifts;
        private DataTable _scheduleTable;

        public TaoLichTuanForm(DateTime startDate)
        {
            InitializeComponent();
            _lichPhanCaRepository = new LichPhanCaRepository();
            _nhanVienRepository = new NhanVienRepository();
            _caLamRepository = new CaLamRepository();
            _startDate = startDate;
            
            // Set date range for the week
            dtpTuNgay.Value = _startDate;
            dtpDenNgay.Value = _startDate.AddDays(6); // 7 days total
            
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Load employees and shifts
                _employees = VuToanThang_23110329.Data.CurrentUser.IsHR ? 
                    _nhanVienRepository.GetAll() : 
                    _nhanVienRepository.GetByRLS();
                _shifts = _caLamRepository.GetAll();
                
                SetupWeeklyGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupWeeklyGrid()
        {
            _scheduleTable = new DataTable();
            
            // Add employee column
            _scheduleTable.Columns.Add("Nhân viên", typeof(string));
            _scheduleTable.Columns.Add("MaNV", typeof(int));
            
            // Add columns for each day of the week
            var startDate = dtpTuNgay.Value.Date;
            for (int i = 0; i < 7; i++)
            {
                var date = startDate.AddDays(i);
                var dayName = GetVietnameseDayName(date.DayOfWeek);
                var columnName = $"{dayName}\n{date:dd/MM}";
                _scheduleTable.Columns.Add(columnName, typeof(string));
            }
            
            // Add rows for each employee
            if (_employees != null)
            {
                foreach (var emp in _employees)
                {
                    var row = _scheduleTable.NewRow();
                    row["Nhân viên"] = emp.HoTen;
                    row["MaNV"] = emp.MaNV;
                    
                    // Initialize all days with empty values
                    for (int i = 0; i < 7; i++)
                    {
                        var date = startDate.AddDays(i);
                        var dayName = GetVietnameseDayName(date.DayOfWeek);
                        var columnName = $"{dayName}\n{date:dd/MM}";
                        row[columnName] = "";
                    }
                    
                    _scheduleTable.Rows.Add(row);
                }
            }
            
            dgvLichTuan.DataSource = _scheduleTable;
            ConfigureGrid();
        }

        private void ConfigureGrid()
        {
            // Hide MaNV column
            if (dgvLichTuan.Columns["MaNV"] != null)
                dgvLichTuan.Columns["MaNV"].Visible = false;
            
            // Set employee column width
            if (dgvLichTuan.Columns["Nhân viên"] != null)
            {
                dgvLichTuan.Columns["Nhân viên"].Width = 200;
                dgvLichTuan.Columns["Nhân viên"].ReadOnly = true;
            }
            
            // Configure day columns as ComboBox
            for (int i = 2; i < dgvLichTuan.Columns.Count; i++)
            {
                var column = dgvLichTuan.Columns[i];
                
                // Create ComboBox column for shifts
                var comboColumn = new DataGridViewComboBoxColumn();
                comboColumn.HeaderText = column.HeaderText;
                comboColumn.Name = column.Name;
                comboColumn.DataSource = GetShiftOptions();
                comboColumn.DisplayMember = "Display";
                comboColumn.ValueMember = "Value";
                comboColumn.FlatStyle = FlatStyle.Flat;
                
                // Replace the column
                dgvLichTuan.Columns.RemoveAt(i);
                dgvLichTuan.Columns.Insert(i, comboColumn);
            }
            
            dgvLichTuan.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        private List<object> GetShiftOptions()
        {
            var options = new List<object>();
            options.Add(new { Display = "", Value = "" }); // Empty option
            
            if (_shifts != null)
            {
                foreach (var shift in _shifts)
                {
                    options.Add(new { 
                        Display = shift.DisplayText, 
                        Value = shift.MaCa.ToString() 
                    });
                }
            }
            
            return options;
        }

        private string GetVietnameseDayName(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday: return "Thứ 2";
                case DayOfWeek.Tuesday: return "Thứ 3";
                case DayOfWeek.Wednesday: return "Thứ 4";
                case DayOfWeek.Thursday: return "Thứ 5";
                case DayOfWeek.Friday: return "Thứ 6";
                case DayOfWeek.Saturday: return "Thứ 7";
                case DayOfWeek.Sunday: return "CN";
                default: return dayOfWeek.ToString();
            }
        }

        private void BtnLoadEmployees_Click(object sender, EventArgs e)
        {
            LoadData();
            MessageBox.Show("Đã tải lại danh sách nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var scheduleList = new List<LichPhanCa>();
                var startDate = dtpTuNgay.Value.Date;
                
                foreach (DataGridViewRow row in dgvLichTuan.Rows)
                {
                    if (row.IsNewRow) continue;
                    
                    var maNV = Convert.ToInt32(row.Cells["MaNV"].Value);
                    
                    // Check each day column
                    for (int i = 2; i < dgvLichTuan.Columns.Count; i++)
                    {
                        var cellValue = row.Cells[i].Value?.ToString();
                        if (!string.IsNullOrEmpty(cellValue))
                        {
                            var dayIndex = i - 2; // Subtract 2 for employee name and MaNV columns
                            var workDate = startDate.AddDays(dayIndex);
                            
                            if (int.TryParse(cellValue, out int maCa))
                            {
                                var lichPhanCa = new LichPhanCa
                                {
                                    MaNV = maNV,
                                    MaCa = maCa,
                                    NgayLam = workDate,
                                    TrangThai = "Mo",
                                    GhiChu = "Tạo từ lịch tuần"
                                };
                                
                                scheduleList.Add(lichPhanCa);
                            }
                        }
                    }
                }
                
                if (scheduleList.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một ca làm cho nhân viên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // Save all schedules
                int successCount = 0;
                int errorCount = 0;
                var errors = new List<string>();
                
                foreach (var schedule in scheduleList)
                {
                    try
                    {
                        var result = _lichPhanCaRepository.Insert(schedule);
                        if (result.Success)
                        {
                            successCount++;
                        }
                        else
                        {
                            errorCount++;
                            errors.Add($"Ngày {schedule.NgayLam:dd/MM}: {result.Message}");
                        }
                    }
                    catch (Exception ex)
                    {
                        errorCount++;
                        errors.Add($"Ngày {schedule.NgayLam:dd/MM}: {ex.Message}");
                    }
                }
                
                // Show result summary
                var message = $"Đã tạo thành công {successCount} lịch phân ca.";
                if (errorCount > 0)
                {
                    message += $"\n{errorCount} lịch bị lỗi:\n" + string.Join("\n", errors.Take(5));
                    if (errors.Count > 5)
                        message += $"\n... và {errors.Count - 5} lỗi khác.";
                }
                
                MessageBox.Show(message, successCount > 0 ? "Thành công" : "Lỗi", 
                    MessageBoxButtons.OK, successCount > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                
                if (successCount > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu lịch tuần: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
