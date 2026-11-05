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
        private bool isSaving;
        private bool loadingLookups;

        public frmNhapHang()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            StartPosition = FormStartPosition.CenterParent;
            ConfigureGrid();
            btnLuu.Click += delegate { SaveInvoice(); };
            btnLamMoi.Click += delegate { ClearInvoice(); };
            btnThemNCC.Click += delegate { AddSupplierQuick(); };
            btnThemSach.Click += delegate { AddBookQuick(); };
            btnLichSu.Click += delegate { using (var form = new frmLichSuNhapHang()) form.ShowDialog(this); };
            cboNhaCungCap.SelectedValueChanged += delegate { if (!loadingLookups) LoadBookSuggestions(); };

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
            loadingLookups = true;
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

            }
            catch (Exception ex) { ShowError(ex); }
            finally { loadingLookups = false; }
            LoadBookSuggestions();
        }

        private void LoadBookSuggestions(int? additionalBookId = null)
        {
            try
            {
                DataTable allBooks = DB.GetTable(@"SELECT s.MaSach,
    s.TenSach + N' (tồn: ' + CONVERT(NVARCHAR(20), ISNULL(k.SoLuong, 0)) + N')' AS TenSach
FROM Sach s LEFT JOIN KhoSach k ON k.MaSach = s.MaSach ORDER BY s.TenSach");
                DataTable suggested = allBooks;
                int supplierId;
                if (cboNhaCungCap.SelectedValue != null && cboNhaCungCap.SelectedValue != DBNull.Value &&
                    Int32.TryParse(Convert.ToString(cboNhaCungCap.SelectedValue), out supplierId))
                {
                    DataTable fromSupplier = DB.GetTable(@"SELECT DISTINCT s.MaSach,
    s.TenSach + N' (tồn: ' + CONVERT(NVARCHAR(20), ISNULL(k.SoLuong, 0)) + N')' AS TenSach
FROM Sach s LEFT JOIN KhoSach k ON k.MaSach=s.MaSach
JOIN ChiTietHDN c ON c.MaSach=s.MaSach JOIN HoaDonNhap h ON h.SoHDN=c.SoHDN
WHERE h.MaNCC=@MaNCC ORDER BY TenSach", new SqlParameter("@MaNCC", SqlDbType.Int) { Value = supplierId });
                    if (fromSupplier.Rows.Count > 0)
                    {
                        suggested = fromSupplier;
                        var included = new HashSet<int>();
                        foreach (DataRow row in suggested.Rows) included.Add(Convert.ToInt32(row["MaSach"]));
                        foreach (DataGridViewRow row in grid.Rows)
                        {
                            if (row.IsNewRow || row.Cells["MaSach"].Value == null || row.Cells["MaSach"].Value == DBNull.Value) continue;
                            int currentId = Convert.ToInt32(row.Cells["MaSach"].Value);
                            if (included.Add(currentId)) ImportBook(allBooks, suggested, currentId);
                        }
                    }
                }
                if (additionalBookId.HasValue && !ContainsBook(suggested, additionalBookId.Value)) ImportBook(allBooks, suggested, additionalBookId.Value);
                var bookColumn = (DataGridViewComboBoxColumn)grid.Columns["MaSach"];
                bookColumn.DataSource = suggested;
                bookColumn.ValueMember = "MaSach";
                bookColumn.DisplayMember = "TenSach";
                sachData = suggested;
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private static bool ContainsBook(DataTable table, int id)
        { foreach (DataRow row in table.Rows) if (Convert.ToInt32(row["MaSach"]) == id) return true; return false; }

        private static void ImportBook(DataTable source, DataTable destination, int id)
        {
            foreach (DataRow row in source.Rows)
                if (Convert.ToInt32(row["MaSach"]) == id) { destination.ImportRow(row); return; }
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
            if (isSaving) return;
            isSaving = true;
            btnLuu.Enabled = false;
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
                grid.Focus();
                if (grid.Rows.Count > 0) grid.CurrentCell = grid.Rows[grid.Rows.Count - 1].Cells["MaSach"];
            }
            catch (Exception ex) { ShowError(ex); }
            finally { isSaving = false; btnLuu.Enabled = true; }
        }

        private void AddSupplierQuick()
        {
            using (var dialog = new QuickEntryDialog("Thêm nhà cung cấp", "Tên nhà cung cấp", "Địa chỉ", "Điện thoại"))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    int id = Convert.ToInt32(DB.Scalar(@"INSERT INTO NhaCungCap (TenNCC, DiaChi, DienThoai)
OUTPUT INSERTED.MaNCC VALUES (@Ten, @DiaChi, @DienThoai)",
                        new SqlParameter("@Ten", SqlDbType.NVarChar, 150) { Value = dialog.Value1 },
                        new SqlParameter("@DiaChi", SqlDbType.NVarChar, 200) { Value = DbText(dialog.Value2) },
                        new SqlParameter("@DienThoai", SqlDbType.VarChar, 15) { Value = DbText(dialog.Value3) }));
                    LoadSupplierLookup(id);
                }
                catch (Exception ex) { ShowError(ex); }
            }
        }

        private void LoadSupplierLookup(int selectedId)
        {
            var suppliers = DB.GetTable("SELECT MaNCC, TenNCC FROM NhaCungCap ORDER BY TenNCC");
            loadingLookups = true;
            try { cboNhaCungCap.DataSource = suppliers; cboNhaCungCap.ValueMember = "MaNCC"; cboNhaCungCap.DisplayMember = "TenNCC"; cboNhaCungCap.SelectedValue = selectedId; }
            finally { loadingLookups = false; }
            LoadBookSuggestions();
        }

        private void AddBookQuick()
        {
            var categories = DB.GetTable("SELECT MaLoai, TenLoai FROM LoaiSach ORDER BY TenLoai");
            if (categories.Rows.Count == 0) { MessageBox.Show(this, "Hãy thêm ít nhất một loại sách trước.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            using (var dialog = new QuickBookDialog(categories))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    int id = Convert.ToInt32(DB.Scalar(@"INSERT INTO Sach (TenSach, MaLoai) OUTPUT INSERTED.MaSach VALUES (@TenSach, @MaLoai)",
                        new SqlParameter("@TenSach", SqlDbType.NVarChar, 200) { Value = dialog.BookTitle },
                        new SqlParameter("@MaLoai", SqlDbType.Int) { Value = dialog.CategoryId }));
                    LoadBookSuggestions(id);
                    int rowIndex = grid.Rows.Add();
                    grid.Rows[rowIndex].Cells["MaSach"].Value = id;
                    grid.Rows[rowIndex].Cells["SLNhap"].Value = 1;
                    grid.Rows[rowIndex].Cells["DonGiaNhap"].Value = 0m;
                    grid.Rows[rowIndex].Cells["KhuyenMai"].Value = 0m;
                    grid.CurrentCell = grid.Rows[rowIndex].Cells["SLNhap"];
                    grid.Focus();
                }
                catch (Exception ex) { ShowError(ex); }
            }
        }

        private static object DbText(string value) { return string.IsNullOrWhiteSpace(value) ? (object)DBNull.Value : value.Trim(); }

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
            MessageBox.Show(this, ex.Message, "Lỗi nhập hàng", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private sealed class NhapHangLine
        {
            public int MaSach;
            public int SoLuong;
            public decimal DonGia;
            public decimal KhuyenMai;
        }

        private sealed class QuickEntryDialog : Form
        {
            private readonly TextBox name = new TextBox(); private readonly TextBox address = new TextBox(); private readonly TextBox phone = new TextBox();
            public string Value1 { get { return name.Text.Trim(); } } public string Value2 { get { return address.Text.Trim(); } } public string Value3 { get { return phone.Text.Trim(); } }
            public QuickEntryDialog(string title, string nameLabel, string addressLabel, string phoneLabel)
            {
                Text = title; Width = 460; Height = 300; StartPosition = FormStartPosition.CenterParent; FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false; MinimizeBox = false; AutoScaleMode = AutoScaleMode.Font;
                var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(14), ColumnCount = 2, RowCount = 4 };
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120)); layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48)); layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48)); layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48)); layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                AddField(layout, nameLabel, name, 0); AddField(layout, addressLabel, address, 1); AddField(layout, phoneLabel, phone, 2);
                var actions = new FlowLayoutPanel { FlowDirection = FlowDirection.RightToLeft, Dock = DockStyle.Fill, WrapContents = false, Padding = new Padding(0, 6, 0, 0) };
                var save = new Button { Text = "Thêm", DialogResult = DialogResult.None, Size = new System.Drawing.Size(90, 34), Margin = new Padding(6, 0, 0, 0) };
                var cancel = new Button { Text = "Hủy", DialogResult = DialogResult.Cancel, Size = new System.Drawing.Size(90, 34), Margin = new Padding(6, 0, 0, 0) };
                save.Click += delegate { if (Value1.Length == 0) MessageBox.Show(this, "Vui lòng nhập " + nameLabel.ToLower() + ".", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning); else { DialogResult = DialogResult.OK; Close(); } };
                actions.Controls.Add(save); actions.Controls.Add(cancel); layout.Controls.Add(actions, 1, 3); Controls.Add(layout); AcceptButton = save; CancelButton = cancel;
            }
            private static void AddField(TableLayoutPanel layout, string label, Control input, int row)
            { layout.Controls.Add(new Label { Text = label, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, Margin = new Padding(3, 0, 8, 0) }, 0, row); input.Dock = DockStyle.Fill; input.Margin = new Padding(3, 7, 3, 7); layout.Controls.Add(input, 1, row); }
        }

        private sealed class QuickBookDialog : Form
        {
            private readonly TextBox titleBox = new TextBox(); private readonly ComboBox category = new ComboBox();
            public string BookTitle { get { return titleBox.Text.Trim(); } } public int CategoryId { get { return Convert.ToInt32(category.SelectedValue); } }
            public QuickBookDialog(DataTable categories)
            {
                Text = "Thêm sách mới"; Width = 460; Height = 250; StartPosition = FormStartPosition.CenterParent; FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false; MinimizeBox = false; AutoScaleMode = AutoScaleMode.Font;
                var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(12), ColumnCount = 2, RowCount = 3 };
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100)); layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 54)); layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 54)); layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                layout.Controls.Add(new Label { Text = "Tên sách *", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 0, 0); titleBox.Dock = DockStyle.Fill; layout.Controls.Add(titleBox, 1, 0);
                layout.Controls.Add(new Label { Text = "Loại sách *", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 0, 1);
                category.DataSource = categories; category.ValueMember = "MaLoai"; category.DisplayMember = "TenLoai"; category.DropDownStyle = ComboBoxStyle.DropDownList; category.Dock = DockStyle.Fill; layout.Controls.Add(category, 1, 1);
                var actions = new FlowLayoutPanel { FlowDirection = FlowDirection.RightToLeft, Dock = DockStyle.Fill, WrapContents = false, Padding = new Padding(0, 6, 0, 0) };
                var save = new Button { Text = "Thêm", Size = new System.Drawing.Size(90, 34), Margin = new Padding(6, 0, 0, 0) }; var cancel = new Button { Text = "Hủy", DialogResult = DialogResult.Cancel, Size = new System.Drawing.Size(90, 34), Margin = new Padding(6, 0, 0, 0) };
                save.Click += delegate { if (BookTitle.Length == 0) MessageBox.Show(this, "Vui lòng nhập tên sách.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning); else { DialogResult = DialogResult.OK; Close(); } };
                actions.Controls.Add(save); actions.Controls.Add(cancel); layout.Controls.Add(actions, 1, 2); Controls.Add(layout); AcceptButton = save; CancelButton = cancel;
            }
        }
    }
}
