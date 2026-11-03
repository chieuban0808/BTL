using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class frmNhanVien : System.Windows.Forms.Form
    {
        private int? selectedId;

        public frmNhanVien()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            StartPosition = FormStartPosition.CenterParent;
            grid.CellClick += Grid_CellClick;
            btnThem.Click += delegate { ClearEditors(); };
            btnLuu.Click += delegate { SaveRecord(); };
            btnXoa.Click += delegate { DeleteRecord(); };
            btnTaiLai.Click += delegate { LoadRows(); ClearEditors(); };
            Load += delegate
            {
                try { LoadJobList(); LoadRows(); }
                catch (Exception ex) { ShowError(ex); }
            };
        }

        private void LoadJobList()
        {
            var data = DB.GetTable("SELECT MaCV, TenCV FROM CongViec ORDER BY TenCV");
            cboCongViec.DataSource = data;
            cboCongViec.ValueMember = "MaCV";
            cboCongViec.DisplayMember = "TenCV";
            cboCongViec.SelectedIndex = data.Rows.Count > 0 ? 0 : -1;
        }

        private void LoadRows()
        {
            grid.DataSource = DB.GetTable(@"SELECT nv.MaNV, nv.MaCV, nv.TenNV, cv.TenCV, nv.DienThoai, nv.DiaChi, nv.TenDangNhap
FROM NhanVien nv JOIN CongViec cv ON cv.MaCV = nv.MaCV
ORDER BY nv.MaNV DESC");
            SetHeader("MaNV", "Mã nhân viên");
            if (grid.Columns.Contains("MaCV")) grid.Columns["MaCV"].Visible = false;
            SetHeader("TenNV", "Họ tên");
            SetHeader("TenCV", "Công việc");
            SetHeader("DienThoai", "Điện thoại");
            SetHeader("DiaChi", "Địa chỉ");
            SetHeader("TenDangNhap", "Tên đăng nhập");
        }

        private void SetHeader(string column, string header)
        {
            if (grid.Columns.Contains(column)) grid.Columns[column].HeaderText = header;
        }

        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = grid.Rows[e.RowIndex];
            selectedId = Convert.ToInt32(row.Cells["MaNV"].Value);
            txtTen.Text = Convert.ToString(row.Cells["TenNV"].Value);
            txtDienThoai.Text = row.Cells["DienThoai"].Value == DBNull.Value ? "" : Convert.ToString(row.Cells["DienThoai"].Value);
            txtDiaChi.Text = row.Cells["DiaChi"].Value == DBNull.Value ? "" : Convert.ToString(row.Cells["DiaChi"].Value);
            txtTenDangNhap.Text = Convert.ToString(row.Cells["TenDangNhap"].Value);
            txtMatKhau.Clear();
            cboCongViec.SelectedValue = row.Cells["MaCV"].Value;
        }

        private void SaveRecord()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTen.Text)) throw new InvalidOperationException("Vui lòng nhập họ tên.");
                if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text)) throw new InvalidOperationException("Vui lòng nhập tên đăng nhập.");
                if (cboCongViec.SelectedValue == null) throw new InvalidOperationException("Hãy tạo và chọn công việc cho nhân viên.");
                if (!selectedId.HasValue && string.IsNullOrEmpty(txtMatKhau.Text))
                    throw new InvalidOperationException("Nhân viên mới cần có mật khẩu.");

                object hash = string.IsNullOrEmpty(txtMatKhau.Text) ? (object)DBNull.Value : HashPassword(txtMatKhau.Text);
                var parameters = new[]
                {
                    new SqlParameter("@TenNV", SqlDbType.NVarChar, 100) { Value = txtTen.Text.Trim() },
                    new SqlParameter("@DienThoai", SqlDbType.VarChar, 15) { Value = string.IsNullOrWhiteSpace(txtDienThoai.Text) ? (object)DBNull.Value : txtDienThoai.Text.Trim() },
                    new SqlParameter("@DiaChi", SqlDbType.NVarChar, 200) { Value = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? (object)DBNull.Value : txtDiaChi.Text.Trim() },
                    new SqlParameter("@MaCV", SqlDbType.Int) { Value = Convert.ToInt32(cboCongViec.SelectedValue) },
                    new SqlParameter("@TenDangNhap", SqlDbType.VarChar, 50) { Value = txtTenDangNhap.Text.Trim() },
                    new SqlParameter("@MatKhau", SqlDbType.VarChar, 64) { Value = hash }
                };

                if (selectedId.HasValue)
                {
                    var all = new SqlParameter[parameters.Length + 1];
                    Array.Copy(parameters, all, parameters.Length);
                    all[all.Length - 1] = new SqlParameter("@MaNV", SqlDbType.Int) { Value = selectedId.Value };
                    DB.Exec(@"UPDATE NhanVien SET TenNV=@TenNV, DienThoai=@DienThoai, DiaChi=@DiaChi,
MaCV=@MaCV, TenDangNhap=@TenDangNhap,
MatKhau=CASE WHEN @MatKhau IS NULL THEN MatKhau ELSE @MatKhau END
WHERE MaNV=@MaNV", all);
                }
                else
                {
                    DB.Exec(@"INSERT INTO NhanVien (TenNV, DienThoai, DiaChi, MaCV, TenDangNhap, MatKhau)
VALUES (@TenNV, @DienThoai, @DiaChi, @MaCV, @TenDangNhap, @MatKhau)", parameters);
                }
                LoadRows();
                ClearEditors();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private static string HashPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                var result = new StringBuilder(bytes.Length * 2);
                foreach (byte value in bytes) result.Append(value.ToString("X2"));
                return result.ToString();
            }
        }

        private void DeleteRecord()
        {
            if (!selectedId.HasValue)
            {
                MessageBox.Show(this, "Chọn nhân viên cần xóa.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show(this, "Xóa nhân viên đang chọn?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                DB.Exec("DELETE FROM NhanVien WHERE MaNV=@MaNV", new SqlParameter("@MaNV", SqlDbType.Int) { Value = selectedId.Value });
                LoadRows();
                ClearEditors();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void ClearEditors()
        {
            selectedId = null;
            txtTen.Clear();
            txtDienThoai.Clear();
            txtDiaChi.Clear();
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            if (cboCongViec.Items.Count > 0) cboCongViec.SelectedIndex = 0;
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Không thể thực hiện", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
