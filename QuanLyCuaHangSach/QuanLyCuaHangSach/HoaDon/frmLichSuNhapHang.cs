using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public sealed class frmLichSuNhapHang : Form
    {
        private readonly DateTimePicker fromDate = new DateTimePicker { Format = DateTimePickerFormat.Short };
        private readonly DateTimePicker toDate = new DateTimePicker { Format = DateTimePickerFormat.Short };
        private readonly ComboBox supplier = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly DataGridView invoices = MakeGrid();
        private readonly DataGridView details = MakeGrid();
        private readonly Label detailTitle = new Label { Text = "Chọn hóa đơn để xem chi tiết", Dock = DockStyle.Fill, TextAlign = System.Drawing.ContentAlignment.MiddleLeft, Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold) };
        private int? selectedInvoice;

        public frmLichSuNhapHang()
        {
            Text = "Lịch sử nhập hàng"; StartPosition = FormStartPosition.CenterParent; MinimumSize = new System.Drawing.Size(1000, 650); Size = new System.Drawing.Size(1250, 780); Font = new System.Drawing.Font("Segoe UI", 9F);
            var root = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(14), ColumnCount = 1, RowCount = 3 };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 46)); root.RowStyles.Add(new RowStyle(SizeType.Absolute, 48)); root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            var heading = new Label { Text = "LỊCH SỬ NHẬP HÀNG", Dock = DockStyle.Fill, Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold), ForeColor = System.Drawing.Color.FromArgb(35, 64, 102), TextAlign = System.Drawing.ContentAlignment.MiddleLeft };
            var filters = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, Padding = new Padding(0, 5, 0, 0) };
            filters.Controls.Add(new Label { Text = "Từ ngày", AutoSize = true, Padding = new Padding(0, 7, 0, 0) }); fromDate.Value = DateTime.Today.AddMonths(-1); filters.Controls.Add(fromDate);
            filters.Controls.Add(new Label { Text = "Đến ngày", AutoSize = true, Padding = new Padding(10, 7, 0, 0) }); toDate.Value = DateTime.Today; filters.Controls.Add(toDate);
            filters.Controls.Add(new Label { Text = "Nhà cung cấp", AutoSize = true, Padding = new Padding(10, 7, 0, 0) }); supplier.Width = 220; filters.Controls.Add(supplier);
            var search = new Button { Text = "Lọc", AutoSize = true }; search.Click += delegate { LoadInvoices(); }; filters.Controls.Add(search);
            var export = new Button { Text = "Xuất Excel", AutoSize = true }; export.Click += delegate { Export(); }; filters.Controls.Add(export);
            var split = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, SplitterDistance = 270 };
            split.Panel1.Controls.Add(invoices);
            var lower = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
            lower.RowStyles.Add(new RowStyle(SizeType.Absolute, 34)); lower.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); lower.Controls.Add(detailTitle, 0, 0); lower.Controls.Add(details, 0, 1); split.Panel2.Controls.Add(lower);
            root.Controls.Add(heading, 0, 0); root.Controls.Add(filters, 0, 1); root.Controls.Add(split, 0, 2); Controls.Add(root);
            invoices.CellClick += delegate(object sender, DataGridViewCellEventArgs e) { if (e.RowIndex < 0) return; selectedInvoice = Convert.ToInt32(invoices.Rows[e.RowIndex].Cells["SoHDN"].Value); LoadDetails(); };
            Load += delegate { LoadSuppliers(); LoadInvoices(); };
        }

        private static DataGridView MakeGrid()
        { return new DataGridView { Dock = DockStyle.Fill, BackgroundColor = System.Drawing.Color.White, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false, RowHeadersVisible = false, MultiSelect = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect }; }

        private void LoadSuppliers()
        {
            DataTable data = DB.GetTable("SELECT MaNCC, TenNCC FROM NhaCungCap ORDER BY TenNCC");
            DataRow all = data.NewRow(); all["MaNCC"] = DBNull.Value; all["TenNCC"] = "Tất cả nhà cung cấp"; data.Rows.InsertAt(all, 0);
            supplier.DataSource = data; supplier.ValueMember = "MaNCC"; supplier.DisplayMember = "TenNCC"; supplier.SelectedIndex = 0;
        }

        private void LoadInvoices()
        {
            try
            {
                if (fromDate.Value.Date > toDate.Value.Date) throw new InvalidOperationException("Ngày bắt đầu không được sau ngày kết thúc.");
                DataTable data = DB.GetTable(@"SELECT h.SoHDN, h.NgayNhap, n.TenNCC, v.TenNV, h.TongTien
FROM HoaDonNhap h JOIN NhaCungCap n ON n.MaNCC=h.MaNCC JOIN NhanVien v ON v.MaNV=h.MaNV
WHERE h.NgayNhap>=@TuNgay AND h.NgayNhap<=@DenNgay AND (@MaNCC IS NULL OR h.MaNCC=@MaNCC)
ORDER BY h.NgayNhap DESC, h.SoHDN DESC",
                    new SqlParameter("@TuNgay", SqlDbType.Date) { Value = fromDate.Value.Date }, new SqlParameter("@DenNgay", SqlDbType.Date) { Value = toDate.Value.Date },
                    new SqlParameter("@MaNCC", SqlDbType.Int) { Value = supplier.SelectedValue ?? DBNull.Value });
                invoices.DataSource = data; Header(invoices, "SoHDN", "Số hóa đơn"); Header(invoices, "NgayNhap", "Ngày nhập"); Header(invoices, "TenNCC", "Nhà cung cấp"); Header(invoices, "TenNV", "Nhân viên"); Header(invoices, "TongTien", "Tổng tiền");
                if (invoices.Columns.Contains("NgayNhap")) invoices.Columns["NgayNhap"].DefaultCellStyle.Format = "d"; if (invoices.Columns.Contains("TongTien")) invoices.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                if (data.Rows.Count == 0) { selectedInvoice = null; details.DataSource = null; detailTitle.Text = "Không có hóa đơn nhập trong bộ lọc."; return; }
                int index = 0; if (selectedInvoice.HasValue) { for (int i = 0; i < data.Rows.Count; i++) if (Convert.ToInt32(data.Rows[i]["SoHDN"]) == selectedInvoice.Value) { index = i; break; } }
                selectedInvoice = Convert.ToInt32(data.Rows[index]["SoHDN"]); invoices.CurrentCell = invoices.Rows[index].Cells["SoHDN"]; LoadDetails();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void LoadDetails()
        {
            if (!selectedInvoice.HasValue) return;
            try
            {
                details.DataSource = DB.GetTable(@"SELECT c.SoHDN, c.MaSach, s.TenSach, c.SLNhap, c.DonGiaNhap, c.KhuyenMai, c.ThanhTien
FROM ChiTietHDN c JOIN Sach s ON s.MaSach=c.MaSach WHERE c.SoHDN=@SoHDN ORDER BY s.TenSach",
                    new SqlParameter("@SoHDN", SqlDbType.Int) { Value = selectedInvoice.Value });
                if (details.Columns.Contains("SoHDN")) details.Columns["SoHDN"].Visible = false; if (details.Columns.Contains("MaSach")) details.Columns["MaSach"].Visible = false;
                Header(details, "TenSach", "Sách"); Header(details, "SLNhap", "Số lượng"); Header(details, "DonGiaNhap", "Giá nhập"); Header(details, "KhuyenMai", "Chiết khấu (%)"); Header(details, "ThanhTien", "Thành tiền");
                foreach (string c in new[] { "DonGiaNhap", "ThanhTien" }) if (details.Columns.Contains(c)) details.Columns[c].DefaultCellStyle.Format = "N0";
                if (details.Columns.Contains("KhuyenMai")) details.Columns["KhuyenMai"].DefaultCellStyle.Format = "N2";
                detailTitle.Text = "Chi tiết hóa đơn nhập số " + selectedInvoice.Value;
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void Export()
        {
            if (fromDate.Value.Date > toDate.Value.Date) { MessageBox.Show(this, "Ngày bắt đầu không được sau ngày kết thúc.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            using (SaveFileDialog dialog = new SaveFileDialog { Title = "Xuất lịch sử nhập hàng", Filter = "Excel Workbook (*.xlsx)|*.xlsx|Tệp CSV mở bằng Excel (*.csv)|*.csv", DefaultExt = "xlsx", FileName = "LichSuNhapHang_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xlsx" })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    DataTable data = DB.GetTable(@"SELECT h.SoHDN, h.NgayNhap, n.TenNCC, v.TenNV, s.MaSach, s.TenSach, c.SLNhap, c.DonGiaNhap, c.KhuyenMai, c.ThanhTien, h.TongTien
FROM HoaDonNhap h JOIN NhaCungCap n ON n.MaNCC=h.MaNCC JOIN NhanVien v ON v.MaNV=h.MaNV
JOIN ChiTietHDN c ON c.SoHDN=h.SoHDN JOIN Sach s ON s.MaSach=c.MaSach
WHERE h.NgayNhap>=@TuNgay AND h.NgayNhap<=@DenNgay AND (@MaNCC IS NULL OR h.MaNCC=@MaNCC)
ORDER BY h.NgayNhap DESC, h.SoHDN DESC, s.TenSach",
                        new SqlParameter("@TuNgay", SqlDbType.Date) { Value = fromDate.Value.Date }, new SqlParameter("@DenNgay", SqlDbType.Date) { Value = toDate.Value.Date },
                        new SqlParameter("@MaNCC", SqlDbType.Int) { Value = supplier.SelectedValue ?? DBNull.Value });
                    ExcelReportExporter.Export(data, "LỊCH SỬ NHẬP HÀNG",
                        new[] { "Số hóa đơn", "Ngày nhập", "Nhà cung cấp", "Nhân viên", "Mã sách", "Tên sách", "Số lượng", "Giá nhập", "Chiết khấu (%)", "Thành tiền", "Tổng hóa đơn" },
                        new[] { "SoHDN", "NgayNhap", "TenNCC", "TenNV", "MaSach", "TenSach", "SLNhap", "DonGiaNhap", "KhuyenMai", "ThanhTien", "TongTien" },
                        "SoHDN", "TongTien", "NgayNhap", dialog.FileName);
                    MessageBox.Show(this, "Đã xuất " + data.Rows.Count + " dòng chi tiết nhập hàng.", "Xuất Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { MessageBox.Show(this, ex.Message, "Xuất Excel thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private static void Header(DataGridView grid, string column, string text) { if (grid.Columns.Contains(column)) grid.Columns[column].HeaderText = text; }
        private void ShowError(Exception ex) { MessageBox.Show(this, ex.Message, "Không thể tải lịch sử nhập", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }
}
