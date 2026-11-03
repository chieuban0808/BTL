using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class frmBanHang : Form
    {
        private DataTable books;
        private bool updating;

        public frmBanHang()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            StartPosition = FormStartPosition.CenterParent;
            btnLuu.Click += delegate { SaveInvoice(); };
            btnLamMoi.Click += delegate { ClearInvoice(); };
            btnTaiLai.Click += delegate { LoadLookups(); LoadInvoices(); };
            btnXoaChiTiet.Click += delegate { DeleteSelectedDetail(); };
            cboHoaDon.SelectedIndexChanged += delegate { LoadInvoiceDetails(); };
            grid.DefaultValuesNeeded += delegate(object sender, DataGridViewRowEventArgs e)
            {
                e.Row.Cells["SLBan"].Value = 1;
                e.Row.Cells["KhuyenMai"].Value = 0m;
            };
            grid.CellValueChanged += delegate { UpdateLineTotal(); };
            grid.CellEndEdit += delegate { UpdateLineTotal(); };
            grid.CurrentCellDirtyStateChanged += delegate
            {
                if (grid.IsCurrentCellDirty) grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
            grid.RowsRemoved += delegate { UpdateTotal(); };
            grid.DataError += delegate(object sender, DataGridViewDataErrorEventArgs e) { e.ThrowException = false; e.Cancel = true; };
            Load += delegate { LoadLookups(); LoadInvoices(); };
        }

        private void LoadLookups()
        {
            try
            {
                BindCombo(cboNhanVien, "SELECT MaNV, TenNV FROM NhanVien ORDER BY TenNV", "MaNV", "TenNV");
                BindCombo(cboKhachHang, "SELECT MaKhach, TenKhach FROM KhachHang ORDER BY TenKhach", "MaKhach", "TenKhach");
                books = DB.GetTable(@"SELECT s.MaSach,
    s.TenSach + N' (tồn: ' + CONVERT(NVARCHAR(20), ISNULL(k.SoLuong, 0)) + N')' AS TenSach,
    ISNULL(k.SoLuong, 0) AS TonKho, ISNULL(k.DonGiaBan, 0) AS DonGiaBan
FROM Sach s LEFT JOIN KhoSach k ON k.MaSach=s.MaSach ORDER BY s.TenSach");
                colMaSach.DataSource = books;
                colMaSach.ValueMember = "MaSach";
                colMaSach.DisplayMember = "TenSach";
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private static void BindCombo(ComboBox combo, string sql, string value, string display)
        {
            var data = DB.GetTable(sql);
            combo.DataSource = data;
            combo.ValueMember = value;
            combo.DisplayMember = display;
            combo.SelectedIndex = data.Rows.Count == 0 ? -1 : 0;
        }

        private void UpdateLineTotal()
        {
            if (updating) return;
            updating = true;
            try
            {
                foreach (DataGridViewRow row in grid.Rows)
                {
                    if (row.IsNewRow) continue;
                    DataRow selected = null;
                    if (books != null && row.Cells["MaSach"].Value != null && row.Cells["MaSach"].Value != DBNull.Value)
                        foreach (DataRow book in books.Rows)
                            if (Convert.ToInt32(book["MaSach"]) == Convert.ToInt32(row.Cells["MaSach"].Value)) { selected = book; break; }
                    if (selected != null) row.Cells["DonGiaBan"].Value = selected["DonGiaBan"];
                    decimal qty, price, discount;
                    if (ReadDecimal(row.Cells["SLBan"].Value, out qty) && ReadDecimal(row.Cells["DonGiaBan"].Value, out price)
                        && ReadDecimal(row.Cells["KhuyenMai"].Value, out discount) && qty > 0 && price >= 0 && discount >= 0 && discount <= 100)
                        row.Cells["ThanhTien"].Value = Math.Round(qty * price * (100m - discount) / 100m, 0, MidpointRounding.AwayFromZero);
                    else row.Cells["ThanhTien"].Value = null;
                }
                UpdateTotal();
            }
            finally { updating = false; }
        }

        private void UpdateTotal()
        {
            decimal total = 0m;
            foreach (DataGridViewRow row in grid.Rows)
            {
                decimal amount;
                if (!row.IsNewRow && ReadDecimal(row.Cells["ThanhTien"].Value, out amount)) total += amount;
            }
            lblTongTien.Text = "Tổng tiền dự kiến: " + total.ToString("N0") + " đ";
        }

        private void SaveInvoice()
        {
            try
            {
                grid.EndEdit();
                UpdateLineTotal();
                if (cboNhanVien.SelectedValue == null || cboKhachHang.SelectedValue == null)
                    throw new InvalidOperationException("Cần có nhân viên và khách hàng trước khi lập hóa đơn.");
                var lines = new System.Collections.Generic.List<SaleLine>();
                var uniqueBooks = new System.Collections.Generic.HashSet<int>();
                foreach (DataGridViewRow row in grid.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.Cells["MaSach"].Value == null || row.Cells["MaSach"].Value == DBNull.Value)
                        throw new InvalidOperationException("Hãy chọn sách cho từng dòng chi tiết.");
                    int id = Convert.ToInt32(row.Cells["MaSach"].Value);
                    if (!uniqueBooks.Add(id)) throw new InvalidOperationException("Mỗi sách chỉ được xuất hiện một lần trong hóa đơn.");
                    decimal qty, discount;
                    if (!ReadDecimal(row.Cells["SLBan"].Value, out qty) || qty <= 0 || qty != decimal.Truncate(qty))
                        throw new InvalidOperationException("Số lượng bán phải là số nguyên lớn hơn 0.");
                    if (!ReadDecimal(row.Cells["KhuyenMai"].Value, out discount) || discount < 0 || discount > 100)
                        throw new InvalidOperationException("Chiết khấu phải từ 0 đến 100%.");
                    lines.Add(new SaleLine { MaSach = id, SoLuong = (int)qty, KhuyenMai = discount });
                }
                if (lines.Count == 0) throw new InvalidOperationException("Hãy nhập ít nhất một dòng chi tiết.");
                int soHDB = SaveSaleTransaction(lines);
                MessageBox.Show(this, "Đã lưu hóa đơn bán số " + soHDB + ". Tổng tiền được cơ sở dữ liệu tự cập nhật.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInvoice();
                LoadLookups();
                LoadInvoices();
                tabs.SelectedTab = tabLichSu;
            }
            catch (SqlException ex) when (ex.Message.IndexOf("Số lượng tồn kho không đủ", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                MessageBox.Show(this, "Số lượng tồn kho không đủ để bán sách đã chọn.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private int SaveSaleTransaction(System.Collections.Generic.List<SaleLine> lines)
        {
            using (var connection = new SqlConnection(DB.ConnStr))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int invoiceId;
                        using (var header = new SqlCommand(@"INSERT INTO HoaDonBan (MaNV, MaKhach, NgayBan)
OUTPUT INSERTED.SoHDB VALUES (@MaNV, @MaKhach, @NgayBan);", connection, transaction))
                        {
                            header.Parameters.Add("@MaNV", SqlDbType.Int).Value = Convert.ToInt32(cboNhanVien.SelectedValue);
                            header.Parameters.Add("@MaKhach", SqlDbType.Int).Value = Convert.ToInt32(cboKhachHang.SelectedValue);
                            header.Parameters.Add("@NgayBan", SqlDbType.Date).Value = dtpNgayBan.Value.Date;
                            invoiceId = Convert.ToInt32(header.ExecuteScalar());
                        }
                        var sql = new StringBuilder("INSERT INTO ChiTietHDB (SoHDB, MaSach, SLBan, KhuyenMai) VALUES ");
                        using (var detail = new SqlCommand())
                        {
                            detail.Connection = connection;
                            detail.Transaction = transaction;
                            detail.Parameters.Add("@SoHDB", SqlDbType.Int).Value = invoiceId;
                            for (int i = 0; i < lines.Count; i++)
                            {
                                if (i > 0) sql.Append(", ");
                                sql.Append("(@SoHDB, @MaSach").Append(i).Append(", @SLBan").Append(i).Append(", @KhuyenMai").Append(i).Append(")");
                                detail.Parameters.Add("@MaSach" + i, SqlDbType.Int).Value = lines[i].MaSach;
                                detail.Parameters.Add("@SLBan" + i, SqlDbType.Int).Value = lines[i].SoLuong;
                                detail.Parameters.Add("@KhuyenMai" + i, SqlDbType.Decimal).Value = lines[i].KhuyenMai;
                                detail.Parameters["@KhuyenMai" + i].Precision = 5;
                                detail.Parameters["@KhuyenMai" + i].Scale = 2;
                            }
                            detail.CommandText = sql.ToString();
                            detail.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        return invoiceId;
                    }
                    catch { try { transaction.Rollback(); } catch { } throw; }
                }
            }
        }

        private void LoadInvoices()
        {
            try
            {
                var data = DB.GetTable(@"SELECT h.SoHDB,
    CONVERT(NVARCHAR(20), h.SoHDB) + N' - ' + CONVERT(NVARCHAR(10), h.NgayBan, 103) + N' - ' + kh.TenKhach AS MoTa
FROM HoaDonBan h JOIN KhachHang kh ON kh.MaKhach=h.MaKhach ORDER BY h.SoHDB DESC");
                cboHoaDon.DataSource = data;
                cboHoaDon.ValueMember = "SoHDB";
                cboHoaDon.DisplayMember = "MoTa";
                cboHoaDon.SelectedIndex = data.Rows.Count == 0 ? -1 : 0;
                LoadInvoiceDetails();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void LoadInvoiceDetails()
        {
            try
            {
                if (cboHoaDon.SelectedValue == null || cboHoaDon.SelectedValue is DataRowView) { gridChiTiet.DataSource = null; return; }
                gridChiTiet.DataSource = DB.GetTable(@"SELECT c.SoHDB, c.MaSach, s.TenSach, c.SLBan, c.DonGiaBan, c.KhuyenMai, c.ThanhTien
FROM ChiTietHDB c JOIN Sach s ON s.MaSach=c.MaSach WHERE c.SoHDB=@SoHDB ORDER BY s.TenSach",
                    new SqlParameter("@SoHDB", SqlDbType.Int) { Value = Convert.ToInt32(cboHoaDon.SelectedValue) });
                if (gridChiTiet.Columns.Contains("SoHDB")) gridChiTiet.Columns["SoHDB"].Visible = false;
                if (gridChiTiet.Columns.Contains("MaSach")) gridChiTiet.Columns["MaSach"].Visible = false;
                if (gridChiTiet.Columns.Contains("TenSach")) gridChiTiet.Columns["TenSach"].HeaderText = "Sách";
                if (gridChiTiet.Columns.Contains("SLBan")) gridChiTiet.Columns["SLBan"].HeaderText = "Số lượng";
                if (gridChiTiet.Columns.Contains("DonGiaBan")) { gridChiTiet.Columns["DonGiaBan"].HeaderText = "Đơn giá"; gridChiTiet.Columns["DonGiaBan"].DefaultCellStyle.Format = "N0"; }
                if (gridChiTiet.Columns.Contains("KhuyenMai")) gridChiTiet.Columns["KhuyenMai"].HeaderText = "Giảm (%)";
                if (gridChiTiet.Columns.Contains("ThanhTien")) { gridChiTiet.Columns["ThanhTien"].HeaderText = "Thành tiền"; gridChiTiet.Columns["ThanhTien"].DefaultCellStyle.Format = "N0"; }
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void DeleteSelectedDetail()
        {
            if (gridChiTiet.CurrentRow == null || cboHoaDon.SelectedValue == null || cboHoaDon.SelectedValue is DataRowView) return;
            if (MessageBox.Show(this, "Xóa sách khỏi hóa đơn này? Trigger cơ sở dữ liệu sẽ hoàn tồn kho.", Text,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                using (var connection = new SqlConnection(DB.ConnStr))
                using (var command = new SqlCommand("DELETE FROM ChiTietHDB WHERE SoHDB=@SoHDB AND MaSach=@MaSach", connection))
                {
                    command.Parameters.Add("@SoHDB", SqlDbType.Int).Value = Convert.ToInt32(cboHoaDon.SelectedValue);
                    command.Parameters.Add("@MaSach", SqlDbType.Int).Value = Convert.ToInt32(gridChiTiet.CurrentRow.Cells["MaSach"].Value);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                LoadInvoiceDetails();
                LoadLookups();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void ClearInvoice()
        {
            for (int i = grid.Rows.Count - 1; i >= 0; i--) if (!grid.Rows[i].IsNewRow) grid.Rows.RemoveAt(i);
            if (cboNhanVien.Items.Count > 0) cboNhanVien.SelectedIndex = 0;
            if (cboKhachHang.Items.Count > 0) cboKhachHang.SelectedIndex = 0;
            dtpNgayBan.Value = DateTime.Today;
            UpdateTotal();
        }

        private static bool ReadDecimal(object value, out decimal result)
        {
            if (value == null || value == DBNull.Value) { result = 0m; return false; }
            return decimal.TryParse(Convert.ToString(value), NumberStyles.Number, CultureInfo.CurrentCulture, out result);
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Không thể thực hiện", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private sealed class SaleLine { public int MaSach; public int SoLuong; public decimal KhuyenMai; }
    }
}
