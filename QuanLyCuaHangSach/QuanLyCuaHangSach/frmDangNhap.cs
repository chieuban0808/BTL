using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
            btnDangNhap.Click += delegate { Login(); };
            btnThoat.Click += delegate { DialogResult = DialogResult.Cancel; Close(); };
            txtMatKhau.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; Login(); }
            };
        }

        private void Login()
        {
            try
            {
                string username = txtTenDangNhap.Text.Trim();
                if (username.Length == 0 || txtMatKhau.Text.Length == 0)
                    throw new InvalidOperationException("Nhập tên đăng nhập và mật khẩu.");

                var rowset = DB.GetTable(@"SELECT nv.MaNV, nv.TenNV, nv.MatKhau, cv.TenCV
FROM NhanVien nv JOIN CongViec cv ON cv.MaCV=nv.MaCV
WHERE nv.TenDangNhap=@TenDangNhap",
                    new SqlParameter("@TenDangNhap", SqlDbType.VarChar, 50) { Value = username });
                if (rowset.Rows.Count == 0 || !VerifyPassword(txtMatKhau.Text, Convert.ToString(rowset.Rows[0]["MatKhau"])))
                    throw new InvalidOperationException("Tên đăng nhập hoặc mật khẩu không đúng.");

                var employee = rowset.Rows[0];
                string job = Convert.ToString(employee["TenCV"]);
                AppRole role;
                if (job.IndexOf("quản lý", StringComparison.OrdinalIgnoreCase) >= 0) role = AppRole.Manager;
                else if (job.IndexOf("bán hàng", StringComparison.OrdinalIgnoreCase) >= 0) role = AppRole.Sales;
                else if (job.IndexOf("thủ kho", StringComparison.OrdinalIgnoreCase) >= 0 || job.IndexOf("kho", StringComparison.OrdinalIgnoreCase) >= 0) role = AppRole.Stock;
                else throw new InvalidOperationException("Chức vụ '" + job + "' chưa được gán quyền trong menu.");

                AppSession.EmployeeId = Convert.ToInt32(employee["MaNV"]);
                AppSession.EmployeeName = Convert.ToString(employee["TenNV"]);
                AppSession.JobName = job;
                AppSession.Role = role;
                AppSession.IsAuthenticated = true;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static bool VerifyPassword(string password, string storedHash)
        {
            using (var sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                var value = new StringBuilder(bytes.Length * 2);
                foreach (byte b in bytes) value.Append(b.ToString("X2"));
                return String.Equals(value.ToString(), storedHash, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}
