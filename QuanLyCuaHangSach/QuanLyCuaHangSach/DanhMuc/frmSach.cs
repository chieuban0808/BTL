using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class frmSach : Form
    {
        private int? selectedId;

        public frmSach()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            StartPosition = FormStartPosition.CenterParent;
            txtTim.KeyDown += delegate(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; LoadBooks(); } };
            grid.CellClick += Grid_CellClick;
            btnThem.Click += delegate { ClearEditors(); };
            btnLuu.Click += delegate { SaveRecord(); };
            btnXoa.Click += delegate { DeleteRecord(); };
            btnTim.Click += delegate { LoadBooks(); };
            btnTatCa.Click += delegate { txtTim.Clear(); LoadBooks(); };

            Load += delegate
            {
                try
                {
                    LoadLookups();
                    LoadBooks();
                }
                catch (Exception ex) { ShowError(ex); }
            };
        }

        private void LoadLookups()
        {
            BindLookup(cboLoai, "SELECT MaLoai, TenLoai FROM LoaiSach ORDER BY TenLoai", "MaLoai", "TenLoai");
            BindLookup(cboTacGia, "SELECT MaTG, TenTG FROM TacGia ORDER BY TenTG", "MaTG", "TenTG", true);
            BindLookup(cboNXB, "SELECT MaNXB, TenNXB FROM NXB ORDER BY TenNXB", "MaNXB", "TenNXB", true);
            BindLookup(cboLinhVuc, "SELECT MaLinhVuc, TenLinhVuc FROM LinhVuc ORDER BY TenLinhVuc", "MaLinhVuc", "TenLinhVuc", true);
            BindLookup(cboNgonNgu, "SELECT MaNgonNgu, TenNgonNgu FROM NgonNgu ORDER BY TenNgonNgu", "MaNgonNgu", "TenNgonNgu", true);
        }

        private static void BindLookup(ComboBox combo, string sql, string idColumn, string nameColumn, bool allowEmpty = false)
        {
            var data = DB.GetTable(sql);
            if (allowEmpty)
            {
                var empty = data.NewRow();
                empty[idColumn] = DBNull.Value;
                empty[nameColumn] = "(Không chọn)";
                data.Rows.InsertAt(empty, 0);
            }
            combo.DataSource = data;
            combo.ValueMember = idColumn;
            combo.DisplayMember = nameColumn;
            combo.SelectedIndex = data.Rows.Count > 0 ? 0 : -1;
        }

        private void LoadBooks()
        {
            try
            {
                var search = new SqlParameter("@TenSach", SqlDbType.NVarChar, 200)
                {
                    Value = string.IsNullOrWhiteSpace(txtTim.Text) ? (object)DBNull.Value : txtTim.Text.Trim()
                };
                grid.DataSource = DB.GetTableSP("sp_TimSach",
                    search,
                    new SqlParameter("@MaLoai", SqlDbType.Int) { Value = DBNull.Value },
                    new SqlParameter("@MaNXB", SqlDbType.Int) { Value = DBNull.Value },
                    new SqlParameter("@MaLinhVuc", SqlDbType.Int) { Value = DBNull.Value },
                    new SqlParameter("@MaNgonNgu", SqlDbType.Int) { Value = DBNull.Value });

                SetHeader("MaSach", "Mã sách");
                SetHeader("TenSach", "Tên sách");
                SetHeader("TenLoai", "Loại sách");
                SetHeader("TenTG", "Tác giả");
                SetHeader("TenNXB", "Nhà xuất bản");
                SetHeader("TenLinhVuc", "Lĩnh vực");
                SetHeader("TenNgonNgu", "Ngôn ngữ");
                SetHeader("SoTrang", "Số trang");
                SetHeader("SoLuong", "Tồn kho");
                SetHeader("DonGiaNhap", "Giá nhập");
                SetHeader("DonGiaBan", "Giá bán");
                HideColumn("MaLoai");
                HideColumn("MaTG");
                HideColumn("MaNXB");
                HideColumn("MaLinhVuc");
                HideColumn("MaNgonNgu");
                HideColumn("Anh");
                FormatMoney("DonGiaNhap");
                FormatMoney("DonGiaBan");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void SetHeader(string column, string header)
        {
            if (grid.Columns.Contains(column)) grid.Columns[column].HeaderText = header;
        }

        private void HideColumn(string column)
        {
            if (grid.Columns.Contains(column)) grid.Columns[column].Visible = false;
        }

        private void FormatMoney(string column)
        {
            if (grid.Columns.Contains(column)) grid.Columns[column].DefaultCellStyle.Format = "N0";
        }

        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = grid.Rows[e.RowIndex];
            selectedId = Convert.ToInt32(row.Cells["MaSach"].Value);
            // sp_TimSach chỉ trả về các cột phục vụ hiển thị, không nhất thiết có
            // cột Anh hoặc các khóa ngoại. Đọc bản ghi đầy đủ từ bảng Sach để sửa.
            try
            {
                var detail = DB.GetTable(
                    "SELECT TenSach, MaLoai, MaTG, MaNXB, MaLinhVuc, MaNgonNgu, SoTrang, Anh FROM Sach WHERE MaSach=@MaSach",
                    new SqlParameter("@MaSach", SqlDbType.Int) { Value = selectedId.Value });
                if (detail.Rows.Count == 0)
                {
                    ClearEditors();
                    LoadBooks();
                    return;
                }

                var book = detail.Rows[0];
                txtTenSach.Text = Convert.ToString(book["TenSach"]);
                txtSoTrang.Text = book["SoTrang"] == DBNull.Value ? "" : Convert.ToString(book["SoTrang"]);
                txtAnh.Text = book["Anh"] == DBNull.Value ? "" : Convert.ToString(book["Anh"]);
                SelectLookup(cboLoai, book["MaLoai"]);
                SelectLookup(cboTacGia, book["MaTG"]);
                SelectLookup(cboNXB, book["MaNXB"]);
                SelectLookup(cboLinhVuc, book["MaLinhVuc"]);
                SelectLookup(cboNgonNgu, book["MaNgonNgu"]);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private static void SelectLookup(ComboBox combo, object value)
        {
            combo.SelectedValue = value == null ? DBNull.Value : value;
            if (combo.SelectedIndex < 0 && combo.Items.Count > 0) combo.SelectedIndex = 0;
        }

        private void SaveRecord()
        {
            try
            {
                string title = txtTenSach.Text.Trim();
                if (title.Length == 0) throw new InvalidOperationException("Vui lòng nhập tên sách.");
                if (cboLoai.SelectedValue == null || cboLoai.SelectedValue == DBNull.Value)
                    throw new InvalidOperationException("Vui lòng tạo và chọn loại sách.");

                object pages = DBNull.Value;
                if (txtSoTrang.Text.Trim().Length > 0)
                {
                    int parsedPages;
                    if (!int.TryParse(txtSoTrang.Text.Trim(), out parsedPages) || parsedPages <= 0)
                        throw new InvalidOperationException("Số trang phải là số nguyên lớn hơn 0.");
                    pages = parsedPages;
                }

                var parameters = new[]
                {
                    new SqlParameter("@TenSach", SqlDbType.NVarChar, 200) { Value = title },
                    new SqlParameter("@MaLoai", SqlDbType.Int) { Value = Convert.ToInt32(cboLoai.SelectedValue) },
                    NullableIntParameter("@MaTG", cboTacGia),
                    NullableIntParameter("@MaNXB", cboNXB),
                    NullableIntParameter("@MaLinhVuc", cboLinhVuc),
                    NullableIntParameter("@MaNgonNgu", cboNgonNgu),
                    new SqlParameter("@SoTrang", SqlDbType.Int) { Value = pages },
                    new SqlParameter("@Anh", SqlDbType.NVarChar, 260) { Value = string.IsNullOrWhiteSpace(txtAnh.Text) ? (object)DBNull.Value : txtAnh.Text.Trim() }
                };

                if (selectedId.HasValue)
                {
                    var all = new SqlParameter[parameters.Length + 1];
                    Array.Copy(parameters, all, parameters.Length);
                    all[all.Length - 1] = new SqlParameter("@MaSach", SqlDbType.Int) { Value = selectedId.Value };
                    DB.Exec("UPDATE Sach SET TenSach=@TenSach, MaLoai=@MaLoai, MaTG=@MaTG, MaNXB=@MaNXB, MaLinhVuc=@MaLinhVuc, MaNgonNgu=@MaNgonNgu, SoTrang=@SoTrang, Anh=@Anh WHERE MaSach=@MaSach", all);
                }
                else
                {
                    DB.Exec("INSERT INTO Sach (TenSach, MaLoai, MaTG, MaNXB, MaLinhVuc, MaNgonNgu, SoTrang, Anh) VALUES (@TenSach, @MaLoai, @MaTG, @MaNXB, @MaLinhVuc, @MaNgonNgu, @SoTrang, @Anh)", parameters);
                }
                ClearEditors();
                LoadBooks();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private static SqlParameter NullableIntParameter(string name, ComboBox combo)
        {
            object value = combo.SelectedIndex <= 0 || combo.SelectedValue == null || combo.SelectedValue == DBNull.Value
                ? (object)DBNull.Value
                : Convert.ToInt32(combo.SelectedValue);
            return new SqlParameter(name, SqlDbType.Int) { Value = value };
        }

        private void DeleteRecord()
        {
            if (!selectedId.HasValue)
            {
                MessageBox.Show(this, "Chọn một sách cần xóa.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show(this, "Xóa sách đang chọn? Sách đã có giao dịch sẽ không thể xóa.", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                var stock = DB.GetTable("SELECT COUNT(*) AS SoDong FROM KhoSach WHERE MaSach=@MaSach",
                    new SqlParameter("@MaSach", SqlDbType.Int) { Value = selectedId.Value });
                if (Convert.ToInt32(stock.Rows[0]["SoDong"]) > 0)
                {
                    MessageBox.Show(this,
                        "Không thể xóa sách vì sách đã có dòng tồn kho. Hãy giữ sách trong danh mục để bảo toàn dữ liệu kho và lịch sử giao dịch.",
                        Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DB.Exec("DELETE FROM Sach WHERE MaSach=@MaSach", new SqlParameter("@MaSach", SqlDbType.Int) { Value = selectedId.Value });
                ClearEditors();
                LoadBooks();
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                MessageBox.Show(this,
                    "Không thể xóa sách vì sách đang được tham chiếu bởi dữ liệu kho hoặc hóa đơn. Hãy giữ sách trong danh mục để bảo toàn lịch sử.",
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void ClearEditors()
        {
            selectedId = null;
            txtTenSach.Clear();
            txtSoTrang.Clear();
            txtAnh.Clear();
            if (cboLoai.Items.Count > 0) cboLoai.SelectedIndex = 0;
            foreach (var combo in new[] { cboTacGia, cboNXB, cboLinhVuc, cboNgonNgu })
                if (combo.Items.Count > 0) combo.SelectedIndex = 0;
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Không thể thực hiện", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
