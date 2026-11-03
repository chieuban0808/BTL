using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class frmNhapHang : System.Windows.Forms.Form
    {
        private DataTable sachData;
        private bool recalculating;

        public frmNhapHang()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            StartPosition = FormStartPosition.CenterParent;
            ConfigureGrid();
            btnLuu.Click += delegate { SaveInvoice(); };
            btnLamMoi.Click += delegate { ClearInvoice(); };

            Load += delegate { LoadLookups(); };
        }

        private void ConfigureGrid()
        {
            grid.DefaultValuesNeeded += delegate(object sender, DataGridViewRowEventArgs e)
            {
                e.Row.Cells["SLNhap"].Value = 1;
                e.Row.Cells["DonGiaNhap"].Value = 0m;
                e.Row.Cells["KhuyenMai"].Value = 0m;
            };
            grid.CurrentCellDirtyStateChanged += delegate
            {
                if (grid.IsCurrentCellDirty) grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
            grid.CellValueChanged += delegate { RecalculateTotals(); };
            grid.CellEndEdit += delegate { RecalculateTotals(); };
            grid.RowsRemoved += delegate { RecalculateTotals(); };
            grid.DataError += delegate(object sender, DataGridViewDataErrorEventArgs e)
            {
                e.ThrowException = false;
                e.Cancel = true;
            };
        }

        private void LoadLookups()
        {
            try
            {
                var employees = DB.GetTable("SELECT MaNV, TenNV FROM NhanVien ORDER BY TenNV");
                cboNhanVien.DataSource = employees;
                cboNhanVien.ValueMember = "MaNV";
                cboNhanVien.DisplayMember = "TenNV";
                cboNhanVien.SelectedIndex = employees.Rows.Count > 0 ? 0 : -1;

                var suppliers = DB.GetTable("SELECT MaNCC, TenNCC FROM NhaCungCap ORDER BY TenNCC");
                cboNhaCungCap.DataSource = suppliers;
                cboNhaCungCap.ValueMember = "MaNCC";
                cboNhaCungCap.DisplayMember = "TenNCC";
                cboNhaCungCap.SelectedIndex = suppliers.Rows.Count > 0 ? 0 : -1;

                sachData = DB.GetTable(@"SELECT s.MaSach,
    s.TenSach + N' (tồn: ' + CONVERT(NVARCHAR(20), ISNULL(k.SoLuong, 0)) + N')' AS TenSach
FROM Sach s LEFT JOIN KhoSach k ON k.MaSach = s.MaSach
ORDER BY s.TenSach");
                var bookColumn = (DataGridViewComboBoxColumn)grid.Columns["MaSach"];
                bookColumn.DataSource = sachData;
                bookColumn.ValueMember = "MaSach";
                bookColumn.DisplayMember = "TenSach";
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void RecalculateTotals()
        {
            if (recalculating) return;
            recalculating = true;
            try
            {
            decimal total = 0m;
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;
                decimal amount;
                if (TryCalculateLine(row, out amount))
                {
                    row.Cells["ThanhTien"].Value = amount;
                    total += amount;
                }
                else row.Cells["ThanhTien"].Value = DBNull.Value;
            }
            lblTongTien.Text = "Tổng tiền: " + total.ToString("N0") + " đ";
            }
            finally { recalculating = false; }
        }

        private static bool TryCalculateLine(DataGridViewRow row, out decimal amount)
        {
            amount = 0m;
            decimal quantity, price, discount;
            return TryReadDecimal(row.Cells["SLNhap"].Value, out quantity)
                && TryReadDecimal(row.Cells["DonGiaNhap"].Value, out price)
                && TryReadDecimal(row.Cells["KhuyenMai"].Value, out discount)
                && quantity > 0 && price >= 0 && discount >= 0 && discount <= 100
                && (amount = Math.Round(quantity * price * (100m - discount) / 100m, 0, MidpointRounding.AwayFromZero)) >= 0;
        }

        private static bool TryReadDecimal(object value, out decimal result)
        {
            if (value == null || value == DBNull.Value)
            {
                result = 0m;
                return false;
            }
            return decimal.TryParse(Convert.ToString(value), NumberStyles.Number, CultureInfo.CurrentCulture, out result);
        }

        private void SaveInvoice()
        {
            try
            {
                grid.EndEdit();
                RecalculateTotals();
                if (cboNhanVien.SelectedValue == null || cboNhaCungCap.SelectedValue == null)
                    throw new InvalidOperationException("Cần có nhân viên và nhà cung cấp trước khi lập hóa đơn.");

                var lines = new List<NhapHangLine>();
                var books = new HashSet<int>();
                foreach (DataGridViewRow row in grid.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.Cells["MaSach"].Value == null || row.Cells["MaSach"].Value == DBNull.Value)
                        throw new InvalidOperationException("Hãy chọn sách cho từng dòng chi tiết.");

                    int maSach = Convert.ToInt32(row.Cells["MaSach"].Value);
                    if (!books.Add(maSach)) throw new InvalidOperationException("Mỗi sách chỉ được xuất hiện một lần trong hóa đơn.");

                    decimal quantity, price, discount, amount;
                    if (!TryReadDecimal(row.Cells["SLNhap"].Value, out quantity) || quantity <= 0 || quantity != decimal.Truncate(quantity))
                        throw new InvalidOperationException("Số lượng nhập phải là số nguyên lớn hơn 0.");
                    if (!TryReadDecimal(row.Cells["DonGiaNhap"].Value, out price) || price < 0 || price != decimal.Truncate(price))
                        throw new InvalidOperationException("Giá nhập phải là số nguyên không âm.");
                    if (!TryReadDecimal(row.Cells["KhuyenMai"].Value, out discount) || discount < 0 || discount > 100)
                        throw new InvalidOperationException("Chiết khấu phải từ 0 đến 100%.");
                    TryCalculateLine(row, out amount);
                    lines.Add(new NhapHangLine { MaSach = maSach, SoLuong = (int)quantity, DonGia = price, KhuyenMai = discount });
                }
                if (lines.Count == 0) throw new InvalidOperationException("Hãy nhập ít nhất một dòng chi tiết.");

                int soHDN = SaveInvoiceTransaction(lines);
                decimal displayTotal = lines.Sum(line => Math.Round(line.SoLuong * line.DonGia * (100m - line.KhuyenMai) / 100m, 0, MidpointRounding.AwayFromZero));
                MessageBox.Show(this, "Đã lưu hóa đơn nhập số " + soHDN + ".\r\nTổng tiền: " + displayTotal.ToString("N0") + " đ", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInvoice();
                LoadLookups();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private int SaveInvoiceTransaction(List<NhapHangLine> lines)
        {
            using (var connection = new SqlConnection(DB.ConnStr))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int soHDN;
                        using (var header = new SqlCommand(@"INSERT INTO HoaDonNhap (MaNV, MaNCC, NgayNhap)
OUTPUT INSERTED.SoHDN
VALUES (@MaNV, @MaNCC, @NgayNhap);", connection, transaction))
                        {
                            header.Parameters.Add("@MaNV", SqlDbType.Int).Value = Convert.ToInt32(cboNhanVien.SelectedValue);
                            header.Parameters.Add("@MaNCC", SqlDbType.Int).Value = Convert.ToInt32(cboNhaCungCap.SelectedValue);
                            header.Parameters.Add("@NgayNhap", SqlDbType.Date).Value = dtpNgayNhap.Value.Date;
                            soHDN = Convert.ToInt32(header.ExecuteScalar());
                        }

                        var sql = new StringBuilder("INSERT INTO ChiTietHDN (SoHDN, MaSach, SLNhap, DonGiaNhap, KhuyenMai) VALUES ");
                        using (var details = new SqlCommand())
                        {
                            details.Connection = connection;
                            details.Transaction = transaction;
                            details.Parameters.Add("@SoHDN", SqlDbType.Int).Value = soHDN;
                            for (int i = 0; i < lines.Count; i++)
                            {
                                if (i > 0) sql.Append(", ");
                                sql.Append("(@SoHDN, @MaSach").Append(i).Append(", @SLNhap").Append(i).Append(", @DonGiaNhap").Append(i).Append(", @KhuyenMai").Append(i).Append(")");
                                details.Parameters.Add("@MaSach" + i, SqlDbType.Int).Value = lines[i].MaSach;
                                details.Parameters.Add("@SLNhap" + i, SqlDbType.Int).Value = lines[i].SoLuong;
                                details.Parameters.Add("@DonGiaNhap" + i, SqlDbType.Decimal).Value = lines[i].DonGia;
                                details.Parameters["@DonGiaNhap" + i].Precision = 18;
                                details.Parameters["@DonGiaNhap" + i].Scale = 0;
                                details.Parameters.Add("@KhuyenMai" + i, SqlDbType.Decimal).Value = lines[i].KhuyenMai;
                                details.Parameters["@KhuyenMai" + i].Precision = 5;
                                details.Parameters["@KhuyenMai" + i].Scale = 2;
                            }
                            details.CommandText = sql.ToString();
                            details.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        return soHDN;
                    }
                    catch
                    {
                        try { transaction.Rollback(); } catch { }
                        throw;
                    }
                }
            }
        }

        private void ClearInvoice()
        {
            for (int i = grid.Rows.Count - 1; i >= 0; i--)
                if (!grid.Rows[i].IsNewRow) grid.Rows.RemoveAt(i);
            if (cboNhanVien.Items.Count > 0) cboNhanVien.SelectedIndex = 0;
            if (cboNhaCungCap.Items.Count > 0) cboNhaCungCap.SelectedIndex = 0;
            dtpNgayNhap.Value = DateTime.Today;
            RecalculateTotals();
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Không thể lưu hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private sealed class NhapHangLine
        {
            public int MaSach;
            public int SoLuong;
            public decimal DonGia;
            public decimal KhuyenMai;
        }
    }
}
