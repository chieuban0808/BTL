using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class frmTimKiem : Form
    {
        public frmTimKiem()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            StartPosition = FormStartPosition.CenterParent;
            btnTimSach.Click += delegate { SearchBooks(); };
            btnTimNhap.Click += delegate { SearchPurchases(); };
            txtTenSach.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; SearchBooks(); }
            };
            txtMaSach.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; SearchPurchases(); }
            };
            Load += delegate { LoadLookups(); };
        }

        private void LoadLookups()
        {
            try
            {
                BindOptional(cboLoai, "SELECT MaLoai AS Id, TenLoai AS Ten FROM LoaiSach ORDER BY TenLoai");
                BindOptional(cboNXB, "SELECT MaNXB AS Id, TenNXB AS Ten FROM NXB ORDER BY TenNXB");
                SearchBooks();
                SearchPurchases();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private static void BindOptional(ComboBox combo, string sql)
        {
            var data = DB.GetTable(sql);
            var all = data.NewRow();
            all["Id"] = DBNull.Value;
            all["Ten"] = "(Tất cả)";
            data.Rows.InsertAt(all, 0);
            combo.DataSource = data;
            combo.ValueMember = "Id";
            combo.DisplayMember = "Ten";
            combo.SelectedIndex = 0;
        }

        private void SearchBooks()
        {
            try
            {
                gridSach.DataSource = DB.GetTableSP("sp_TimSach",
                    new SqlParameter("@TenSach", SqlDbType.NVarChar, 200) { Value = string.IsNullOrWhiteSpace(txtTenSach.Text) ? (object)DBNull.Value : txtTenSach.Text.Trim() },
                    new SqlParameter("@MaLoai", SqlDbType.Int) { Value = ComboValue(cboLoai) },
                    new SqlParameter("@MaNXB", SqlDbType.Int) { Value = ComboValue(cboNXB) },
                    new SqlParameter("@MaLinhVuc", SqlDbType.Int) { Value = DBNull.Value },
                    new SqlParameter("@MaNgonNgu", SqlDbType.Int) { Value = DBNull.Value });
                SetHeader(gridSach, "MaSach", "Mã sách");
                SetHeader(gridSach, "TenSach", "Tên sách");
                SetHeader(gridSach, "TenLoai", "Loại sách");
                SetHeader(gridSach, "TenTG", "Tác giả");
                SetHeader(gridSach, "TenNXB", "Nhà xuất bản");
                SetHeader(gridSach, "TenLinhVuc", "Lĩnh vực");
                SetHeader(gridSach, "TenNgonNgu", "Ngôn ngữ");
                SetHeader(gridSach, "SoTrang", "Số trang");
                SetHeader(gridSach, "SoLuong", "Tồn kho");
                SetHeader(gridSach, "DonGiaNhap", "Giá nhập");
                SetHeader(gridSach, "DonGiaBan", "Giá bán");
                HideBookIdColumns();
                FormatMoney(gridSach, "DonGiaNhap");
                FormatMoney(gridSach, "DonGiaBan");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void HideBookIdColumns()
        {
            foreach (string name in new[] { "MaLoai", "MaTG", "MaNXB", "MaLinhVuc", "MaNgonNgu", "Anh" })
                if (gridSach.Columns.Contains(name)) gridSach.Columns[name].Visible = false;
        }

        private void SearchPurchases()
        {
            try
            {
                object bookId = DBNull.Value;
                if (!string.IsNullOrWhiteSpace(txtMaSach.Text))
                {
                    int id;
                    if (!int.TryParse(txtMaSach.Text.Trim(), out id) || id <= 0)
                        throw new InvalidOperationException("Mã sách phải là số nguyên dương.");
                    bookId = id;
                }
                if (dtpTuNgay.Checked && dtpDenNgay.Checked && dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
                    throw new InvalidOperationException("Ngày bắt đầu phải trước hoặc bằng ngày kết thúc.");

                gridNhap.DataSource = DB.GetTableSP("sp_TimHoaDonNhap",
                    new SqlParameter("@MaSach", SqlDbType.Int) { Value = bookId },
                    new SqlParameter("@TuNgay", SqlDbType.Date) { Value = dtpTuNgay.Checked ? (object)dtpTuNgay.Value.Date : DBNull.Value },
                    new SqlParameter("@DenNgay", SqlDbType.Date) { Value = dtpDenNgay.Checked ? (object)dtpDenNgay.Value.Date : DBNull.Value });
                SetHeader(gridNhap, "SoHDN", "Số hóa đơn nhập");
                SetHeader(gridNhap, "NgayNhap", "Ngày nhập");
                SetHeader(gridNhap, "TenNCC", "Nhà cung cấp");
                SetHeader(gridNhap, "TenNV", "Nhân viên");
                SetHeader(gridNhap, "TongTien", "Tổng tiền");
                if (gridNhap.Columns.Contains("NgayNhap")) gridNhap.Columns["NgayNhap"].DefaultCellStyle.Format = "d";
                FormatMoney(gridNhap, "TongTien");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private static object ComboValue(ComboBox combo)
        {
            if (combo.SelectedValue == null || combo.SelectedValue == DBNull.Value) return DBNull.Value;
            return Convert.ToInt32(combo.SelectedValue);
        }

        private static void SetHeader(DataGridView grid, string column, string caption)
        {
            if (grid.Columns.Contains(column)) grid.Columns[column].HeaderText = caption;
        }

        private static void FormatMoney(DataGridView grid, string column)
        {
            if (grid.Columns.Contains(column)) grid.Columns[column].DefaultCellStyle.Format = "N0";
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Không thể tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
