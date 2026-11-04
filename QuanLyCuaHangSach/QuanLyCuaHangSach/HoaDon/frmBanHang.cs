using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class frmBanHang : Form
    {
        private DataTable books;
        private int? lastSavedInvoiceId;
        private DataTable invoiceToPrint;
        private readonly PrintDocument printDocument = new PrintDocument();
        private int printRowIndex;

        public frmBanHang()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            components.Add(printDocument);
            StartPosition = FormStartPosition.CenterParent;
            KeyPreview = true;
            txtTimSach.TextChanged += delegate { FilterBooks(); };
            txtTimSach.KeyDown += Search_KeyDown;
            gridKetQua.CellDoubleClick += delegate { AddSelectedBook(); };
            btnThemSach.Click += delegate { AddSelectedBook(); };
            btnThemKhach.Click += delegate { AddCustomerQuickly(); };
            btnLuu.Click += delegate { SaveInvoice(); };
            btnLamMoi.Click += delegate { CancelInvoice(); };
            btnIn.Click += delegate { PrintLastInvoice(); };
            btnTang.Click += delegate { ChangeSelectedQuantity(1); };
            btnGiam.Click += delegate { ChangeSelectedQuantity(-1); };
            btnXoaDong.Click += delegate { RemoveSelectedBook(); };
            grid.CellValidating += ValidateCartCell;
            grid.CellValueChanged += delegate { UpdateCartTotals(); };
            grid.CellEndEdit += delegate(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex >= 0)
                {
                    grid.Rows[e.RowIndex].ErrorText = String.Empty;
                    grid.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = String.Empty;
                }
                UpdateCartTotals();
            };
            grid.SelectionChanged += delegate { UpdateQuantityButtons(); };
            printDocument.PrintPage += PrintDocument_PrintPage;
            Load += delegate { LoadLookups(); };
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F2) { txtTimSach.Focus(); txtTimSach.SelectAll(); return true; }
            if (keyData == Keys.F9) { SaveInvoice(); return true; }
            if (keyData == Keys.Escape) { CancelInvoice(); return true; }
            if (keyData == Keys.Delete && grid.ContainsFocus && !grid.IsCurrentCellInEditMode) { RemoveSelectedBook(); return true; }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void LoadLookups()
        {
            try
            {
                BindEmployees();
                EnsureWalkInCustomer();
                BindCustomers(null);
                books = DB.GetTable(@"SELECT s.MaSach, s.TenSach, ISNULL(k.SoLuong, 0) AS TonKho,
    ISNULL(k.DonGiaBan, 0) AS DonGiaBan
FROM Sach s LEFT JOIN KhoSach k ON k.MaSach=s.MaSach ORDER BY s.TenSach");
                FilterBooks();
                dtpNgayBan.Value = DateTime.Today;
                UpdateQuantityButtons();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void BindEmployees()
        {
            var data = DB.GetTable("SELECT MaNV, TenNV FROM NhanVien ORDER BY TenNV");
            cboNhanVien.DataSource = data;
            cboNhanVien.ValueMember = "MaNV";
            cboNhanVien.DisplayMember = "TenNV";
            cboNhanVien.SelectedIndex = -1;
            if (AppSession.IsAuthenticated)
                cboNhanVien.SelectedValue = AppSession.EmployeeId;
            if (cboNhanVien.SelectedIndex < 0 && data.Rows.Count > 0) cboNhanVien.SelectedIndex = 0;
            cboNhanVien.Enabled = AppSession.Role == AppRole.Manager;
        }

        private void EnsureWalkInCustomer()
        {
            DB.Exec(@"IF NOT EXISTS (SELECT 1 FROM dbo.KhachHang WHERE TenKhach=N'Khách lẻ')
INSERT INTO dbo.KhachHang (TenKhach, DienThoai, DiaChi) VALUES (N'Khách lẻ', NULL, NULL)");
        }

        private void BindCustomers(int? preferredId)
        {
            var data = DB.GetTable("SELECT MaKhach, TenKhach FROM KhachHang ORDER BY TenKhach");
            cboKhachHang.DataSource = data;
            cboKhachHang.ValueMember = "MaKhach";
            cboKhachHang.DisplayMember = "TenKhach";
            cboKhachHang.SelectedIndex = -1;
            if (preferredId.HasValue) cboKhachHang.SelectedValue = preferredId.Value;
            if (cboKhachHang.SelectedIndex < 0)
                foreach (DataRow row in data.Rows)
                    if (String.Equals(Convert.ToString(row["TenKhach"]), "Khách lẻ", StringComparison.OrdinalIgnoreCase))
                    { cboKhachHang.SelectedValue = Convert.ToInt32(row["MaKhach"]); break; }
            if (cboKhachHang.SelectedIndex < 0 && data.Rows.Count > 0) cboKhachHang.SelectedIndex = 0;
        }

        private void FilterBooks()
        {
            var results = books == null ? null : books.Clone();
            string query = txtTimSach.Text.Trim();
            if (books != null && query.Length > 0)
            {
                int id;
                foreach (DataRow row in books.Rows)
                    if (Convert.ToString(row["TenSach"]).IndexOf(query, StringComparison.CurrentCultureIgnoreCase) >= 0
                        || (Int32.TryParse(query, out id) && Convert.ToInt32(row["MaSach"]) == id))
                        results.ImportRow(row);
            }
            gridKetQua.DataSource = results;
            SetSearchGridHeaders();
            lblTimHint.Text = query.Length == 0 ? "Gõ tên sách hoặc mã sách để lọc; nhấn Enter để thêm." :
                (results.Rows.Count == 0 ? "Không tìm thấy sách phù hợp." : results.Rows.Count + " kết quả · Enter để thêm sách đầu tiên.");
        }

        private void SetSearchGridHeaders()
        {
            SetHeader(gridKetQua, "MaSach", "Mã");
            SetHeader(gridKetQua, "TenSach", "Sách");
            SetHeader(gridKetQua, "DonGiaBan", "Giá bán");
            SetHeader(gridKetQua, "TonKho", "Tồn");
            if (gridKetQua.Columns.Contains("DonGiaBan")) gridKetQua.Columns["DonGiaBan"].DefaultCellStyle.Format = "N0";
        }

        private void Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                AddSelectedBook();
            }
            else if (e.KeyCode == Keys.Down && gridKetQua.Rows.Count > 0)
            {
                gridKetQua.Focus();
                gridKetQua.CurrentCell = gridKetQua.Rows[0].Cells["TenSach"];
                e.SuppressKeyPress = true;
            }
        }

        private void AddSelectedBook()
        {
            if (gridKetQua.CurrentRow == null || gridKetQua.CurrentRow.IsNewRow)
            {
                if (gridKetQua.Rows.Count == 0) return;
                gridKetQua.CurrentCell = gridKetQua.Rows[0].Cells["TenSach"];
            }
            int id = Convert.ToInt32(gridKetQua.CurrentRow.Cells["MaSach"].Value);
            int stock = Convert.ToInt32(gridKetQua.CurrentRow.Cells["TonKho"].Value);
            var existing = FindCartRow(id);
            int current = existing == null ? 0 : Convert.ToInt32(existing.Cells["SLBan"].Value);
            if (stock <= 0)
            {
                ShowStockWarning("Sách này hiện hết hàng.");
                return;
            }
            if (current + 1 > stock)
            {
                ShowStockWarning("Tồn kho chỉ còn " + stock + " cuốn; không thể tăng thêm.");
                return;
            }

            if (existing != null)
                existing.Cells["SLBan"].Value = current + 1;
            else
            {
                int rowIndex = grid.Rows.Add();
                var row = grid.Rows[rowIndex];
                row.Cells["MaSach"].Value = id;
                row.Cells["TenSach"].Value = Convert.ToString(gridKetQua.CurrentRow.Cells["TenSach"].Value);
                row.Cells["SLBan"].Value = 1;
                row.Cells["TonKho"].Value = stock;
                row.Cells["DonGiaBan"].Value = Convert.ToDecimal(gridKetQua.CurrentRow.Cells["DonGiaBan"].Value);
                row.Cells["KhuyenMai"].Value = 0m;
            }
            UpdateCartTotals();
            lblCanhBao.Text = "";
            txtTimSach.Clear();
            txtTimSach.Focus();
        }

        private DataGridViewRow FindCartRow(int maSach)
        {
            foreach (DataGridViewRow row in grid.Rows)
                if (!row.IsNewRow && row.Cells["MaSach"].Value != null && Convert.ToInt32(row.Cells["MaSach"].Value) == maSach) return row;
            return null;
        }

        private void ValidateCartCell(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = grid.Rows[e.RowIndex];
            if (row.IsNewRow) return;
            string column = grid.Columns[e.ColumnIndex].Name;
            decimal value;
            if (column == "SLBan")
            {
                int quantity;
                if (!Int32.TryParse(Convert.ToString(e.FormattedValue), out quantity) || quantity < 1)
                {
                    RejectCell(row, e.ColumnIndex, e, "Số lượng phải là số nguyên dương.");
                    return;
                }
                int stock = Convert.ToInt32(row.Cells["TonKho"].Value);
                if (quantity > stock)
                {
                    RejectCell(row, e.ColumnIndex, e, "Tồn kho chỉ còn " + stock + " cuốn.");
                    return;
                }
            }
            else if (column == "KhuyenMai")
            {
                if (!ReadDecimal(e.FormattedValue, out value) || value < 0 || value > 100)
                {
                    RejectCell(row, e.ColumnIndex, e, "Mức giảm phải nằm trong khoảng 0–100%.");
                    return;
                }
            }
            row.Cells[e.ColumnIndex].ErrorText = String.Empty;
            row.ErrorText = String.Empty;
            lblCanhBao.Text = "";
        }

        private void RejectCell(DataGridViewRow row, int columnIndex, DataGridViewCellValidatingEventArgs e, string message)
        {
            e.Cancel = true;
            row.Cells[columnIndex].ErrorText = message;
            row.ErrorText = message;
            ShowStockWarning(message);
        }

        private void UpdateCartTotals()
        {
            decimal total = 0m;
            int bookCount = 0;
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;
                decimal quantity, price, discount;
                if (ReadDecimal(row.Cells["SLBan"].Value, out quantity)
                    && ReadDecimal(row.Cells["DonGiaBan"].Value, out price)
                    && ReadDecimal(row.Cells["KhuyenMai"].Value, out discount)
                    && quantity > 0 && price >= 0 && discount >= 0 && discount <= 100)
                {
                    decimal amount = Math.Round(quantity * price * (100m - discount) / 100m, 0, MidpointRounding.AwayFromZero);
                    row.Cells["ThanhTien"].Value = amount;
                    total += amount;
                    bookCount += (int)quantity;
                    int stock = Convert.ToInt32(row.Cells["TonKho"].Value);
                    if (quantity > stock) row.DefaultCellStyle.BackColor = Color.MistyRose;
                    else row.DefaultCellStyle.BackColor = Color.White;
                }
                else row.Cells["ThanhTien"].Value = null;
            }
            lblTongTien.Text = "Tổng cộng (" + bookCount + " cuốn)  " + total.ToString("N0") + " đ";
            UpdateQuantityButtons();
        }

        private void ChangeSelectedQuantity(int delta)
        {
            var row = grid.CurrentRow;
            if (row == null || row.IsNewRow) return;
            int quantity = Convert.ToInt32(row.Cells["SLBan"].Value);
            int newQuantity = quantity + delta;
            int stock = Convert.ToInt32(row.Cells["TonKho"].Value);
            if (newQuantity < 1) return;
            if (newQuantity > stock)
            {
                ShowStockWarning("Tồn kho chỉ còn " + stock + " cuốn.");
                return;
            }
            row.Cells["SLBan"].Value = newQuantity;
            lblCanhBao.Text = "";
        }

        private void RemoveSelectedBook()
        {
            if (grid.CurrentRow == null || grid.CurrentRow.IsNewRow) return;
            grid.Rows.Remove(grid.CurrentRow);
            UpdateCartTotals();
        }

        private void UpdateQuantityButtons()
        {
            bool hasRow = grid.CurrentRow != null && !grid.CurrentRow.IsNewRow;
            btnGiam.Enabled = hasRow;
            btnTang.Enabled = hasRow;
            btnXoaDong.Enabled = hasRow;
        }

        private void ShowStockWarning(string message)
        {
            lblCanhBao.Text = message;
            lblCanhBao.ForeColor = Color.Firebrick;
        }

        private void SaveInvoice()
        {
            try
            {
                if (grid.IsCurrentCellInEditMode && !grid.EndEdit()) return;
                if (!String.IsNullOrEmpty(lblCanhBao.Text)) throw new InvalidOperationException(lblCanhBao.Text);
                if (cboNhanVien.SelectedValue == null || cboKhachHang.SelectedValue == null)
                    throw new InvalidOperationException("Cần có nhân viên và khách hàng trước khi lập hóa đơn.");
                var lines = new System.Collections.Generic.List<SaleLine>();
                foreach (DataGridViewRow row in grid.Rows)
                {
                    if (row.IsNewRow) continue;
                    int id = Convert.ToInt32(row.Cells["MaSach"].Value);
                    int quantity = Convert.ToInt32(row.Cells["SLBan"].Value);
                    int stock = Convert.ToInt32(row.Cells["TonKho"].Value);
                    decimal discount = Convert.ToDecimal(row.Cells["KhuyenMai"].Value);
                    if (quantity < 1 || quantity > stock) throw new InvalidOperationException("Tồn kho không đủ cho sách " + row.Cells["TenSach"].Value + ".");
                    if (discount < 0 || discount > 100) throw new InvalidOperationException("Mức giảm phải nằm trong khoảng 0–100%.");
                    lines.Add(new SaleLine { MaSach = id, SoLuong = quantity, KhuyenMai = discount });
                }
                if (lines.Count == 0) throw new InvalidOperationException("Hãy thêm ít nhất một cuốn sách vào giỏ.");
                int soHDB = SaveSaleTransaction(lines);
                lastSavedInvoiceId = soHDB;
                btnIn.Enabled = true;
                MessageBox.Show(this, "Đã lưu hóa đơn bán số " + soHDB + ". Tổng tiền do cơ sở dữ liệu tự tính.", Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearCart();
                LoadLookups();
                btnIn.Focus();
            }
            catch (SqlException ex) when (ex.Message.IndexOf("Số lượng tồn kho không đủ", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                ShowStockWarning("Số lượng tồn kho không đủ; tồn kho có thể vừa thay đổi. Cập nhật lại số lượng rồi thử lại.");
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

        private void CancelInvoice()
        {
            if (grid.Rows.Count > 0 && MessageBox.Show(this, "Hủy hóa đơn đang nhập và xóa giỏ hàng?", Text,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            ClearCart();
            txtTimSach.Clear();
            dtpNgayBan.Value = DateTime.Today;
            BindEmployees();
            BindCustomers(null);
            lblCanhBao.Text = "";
        }

        private void ClearCart()
        {
            grid.Rows.Clear();
            UpdateCartTotals();
        }

        private void AddCustomerQuickly()
        {
            using (var dialog = new Form { Text = "Thêm nhanh khách hàng", ClientSize = new Size(430, 270), StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false, MinimizeBox = false, Font = new Font("Segoe UI", 9F) })
            {
                var lblName = new Label { Text = "Tên khách hàng *", Left = 22, Top = 22, Width = 360 };
                var txtName = new TextBox { Left = 22, Top = 46, Width = 380, MaxLength = 100 };
                var lblPhone = new Label { Text = "Điện thoại", Left = 22, Top = 84, Width = 360 };
                var txtPhone = new TextBox { Left = 22, Top = 108, Width = 380, MaxLength = 15 };
                var lblAddress = new Label { Text = "Địa chỉ", Left = 22, Top = 146, Width = 360 };
                var txtAddress = new TextBox { Left = 22, Top = 170, Width = 380, MaxLength = 200 };
                var btnSave = new Button { Text = "Thêm khách", Left = 214, Top = 218, Width = 100, DialogResult = DialogResult.None };
                var btnCancel = new Button { Text = "Hủy", Left = 324, Top = 218, Width = 78, DialogResult = DialogResult.Cancel };
                dialog.Controls.AddRange(new Control[] { lblName, txtName, lblPhone, txtPhone, lblAddress, txtAddress, btnSave, btnCancel });
                dialog.AcceptButton = btnSave;
                dialog.CancelButton = btnCancel;
                int? createdId = null;
                btnSave.Click += delegate
                {
                    if (String.IsNullOrWhiteSpace(txtName.Text))
                    {
                        MessageBox.Show(dialog, "Nhập tên khách hàng.", dialog.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtName.Focus();
                        return;
                    }
                    try
                    {
                        using (var connection = new SqlConnection(DB.ConnStr))
                        using (var command = new SqlCommand(@"INSERT INTO KhachHang (TenKhach, DienThoai, DiaChi)
OUTPUT INSERTED.MaKhach VALUES (@TenKhach, @DienThoai, @DiaChi)", connection))
                        {
                            command.Parameters.Add("@TenKhach", SqlDbType.NVarChar, 100).Value = txtName.Text.Trim();
                            command.Parameters.Add("@DienThoai", SqlDbType.VarChar, 15).Value = String.IsNullOrWhiteSpace(txtPhone.Text) ? (object)DBNull.Value : txtPhone.Text.Trim();
                            command.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 200).Value = String.IsNullOrWhiteSpace(txtAddress.Text) ? (object)DBNull.Value : txtAddress.Text.Trim();
                            connection.Open();
                            createdId = Convert.ToInt32(command.ExecuteScalar());
                        }
                        dialog.DialogResult = DialogResult.OK;
                        dialog.Close();
                    }
                    catch (Exception ex) { MessageBox.Show(dialog, ex.Message, "Không thể thêm khách hàng", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                };
                if (dialog.ShowDialog(this) == DialogResult.OK && createdId.HasValue) BindCustomers(createdId.Value);
            }
        }

        private void PrintLastInvoice()
        {
            if (!lastSavedInvoiceId.HasValue) return;
            try
            {
                invoiceToPrint = DB.GetTable(@"SELECT h.SoHDB, h.NgayBan, h.TongTien, kh.TenKhach, nv.TenNV,
    s.TenSach, c.SLBan, c.DonGiaBan, c.KhuyenMai, c.ThanhTien
FROM HoaDonBan h JOIN KhachHang kh ON kh.MaKhach=h.MaKhach
JOIN NhanVien nv ON nv.MaNV=h.MaNV
JOIN ChiTietHDB c ON c.SoHDB=h.SoHDB JOIN Sach s ON s.MaSach=c.MaSach
WHERE h.SoHDB=@SoHDB ORDER BY s.TenSach",
                    new SqlParameter("@SoHDB", SqlDbType.Int) { Value = lastSavedInvoiceId.Value });
                if (invoiceToPrint.Rows.Count == 0) throw new InvalidOperationException("Không tìm thấy chi tiết hóa đơn để in.");
                printRowIndex = 0;
                using (var preview = new PrintPreviewDialog { Document = printDocument, Width = 1000, Height = 750, UseAntiAlias = true })
                    preview.ShowDialog(this);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (invoiceToPrint == null || invoiceToPrint.Rows.Count == 0) { e.HasMorePages = false; return; }
            DataRow first = invoiceToPrint.Rows[0];
            using (var titleFont = new Font("Segoe UI", 17F, FontStyle.Bold))
            using (var bodyFont = new Font("Segoe UI", 10F))
            using (var boldFont = new Font("Segoe UI", 10F, FontStyle.Bold))
            using (var brush = new SolidBrush(Color.Black))
            {
                float y = e.MarginBounds.Top;
                e.Graphics.DrawString("HÓA ĐƠN BÁN HÀNG", titleFont, brush, e.MarginBounds.Left, y); y += 38;
                e.Graphics.DrawString("Số: " + first["SoHDB"] + "     Ngày: " + Convert.ToDateTime(first["NgayBan"]).ToString("dd/MM/yyyy"), bodyFont, brush, e.MarginBounds.Left, y); y += 24;
                e.Graphics.DrawString("Khách hàng: " + first["TenKhach"], bodyFont, brush, e.MarginBounds.Left, y); y += 22;
                e.Graphics.DrawString("Thu ngân: " + first["TenNV"], bodyFont, brush, e.MarginBounds.Left, y); y += 32;
                e.Graphics.DrawString("Sách", boldFont, brush, e.MarginBounds.Left, y);
                e.Graphics.DrawString("SL", boldFont, brush, e.MarginBounds.Left + 310, y);
                e.Graphics.DrawString("Đơn giá", boldFont, brush, e.MarginBounds.Left + 365, y);
                e.Graphics.DrawString("Thành tiền", boldFont, brush, e.MarginBounds.Left + 480, y); y += 24;
                while (printRowIndex < invoiceToPrint.Rows.Count && y < e.MarginBounds.Bottom - 65)
                {
                    DataRow row = invoiceToPrint.Rows[printRowIndex++];
                    string name = Convert.ToString(row["TenSach"]);
                    if (name.Length > 42) name = name.Substring(0, 39) + "...";
                    e.Graphics.DrawString(name, bodyFont, brush, e.MarginBounds.Left, y);
                    e.Graphics.DrawString(Convert.ToString(row["SLBan"]), bodyFont, brush, e.MarginBounds.Left + 310, y);
                    e.Graphics.DrawString(Convert.ToDecimal(row["DonGiaBan"]).ToString("N0"), bodyFont, brush, e.MarginBounds.Left + 365, y);
                    e.Graphics.DrawString(Convert.ToDecimal(row["ThanhTien"]).ToString("N0"), bodyFont, brush, e.MarginBounds.Left + 480, y);
                    y += 22;
                }
                if (printRowIndex < invoiceToPrint.Rows.Count) { e.HasMorePages = true; return; }
                y += 12;
                e.Graphics.DrawString("TỔNG TIỀN: " + Convert.ToDecimal(first["TongTien"]).ToString("N0") + " đ", boldFont, brush, e.MarginBounds.Left + 350, y);
                y += 30;
                e.Graphics.DrawString("Cảm ơn quý khách!", bodyFont, brush, e.MarginBounds.Left, y);
                e.HasMorePages = false;
            }
        }

        private static void SetHeader(DataGridView view, string column, string caption)
        {
            if (view.Columns.Contains(column)) view.Columns[column].HeaderText = caption;
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
