using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class frmMatSach : Form
    {
        public frmMatSach()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            StartPosition = FormStartPosition.CenterParent;
            cboSach.SelectedIndexChanged += delegate { UpdateStockLimit(); };
            btnGhiNhan.Click += delegate { SaveLoss(); };
            btnLamMoi.Click += delegate { ClearEntry(); };
            btnXoa.Click += delegate { DeleteSelectedLoss(); };
            btnTaiLai.Click += delegate { LoadData(); };

            Load += delegate { LoadData(); };
        }

        private void LoadData()
        {
            try
            {
                var books = DB.GetTable(@"SELECT s.MaSach,
    s.TenSach + N' (tồn: ' + CONVERT(NVARCHAR(20), ISNULL(k.SoLuong, 0)) + N')' AS TenSach,
    ISNULL(k.SoLuong, 0) AS TonKho
FROM Sach s LEFT JOIN KhoSach k ON k.MaSach = s.MaSach
ORDER BY s.TenSach");
                cboSach.DataSource = books;
                cboSach.ValueMember = "MaSach";
                cboSach.DisplayMember = "TenSach";
                cboSach.SelectedIndex = books.Rows.Count > 0 ? 0 : -1;

                grid.DataSource = DB.GetTable(@"SELECT m.MaLanMat, s.TenSach, m.NgayMat, m.SLMat, m.LyDo
FROM MatSach m JOIN Sach s ON s.MaSach = m.MaSach
ORDER BY m.NgayMat DESC, m.MaLanMat DESC");
                SetHeader("MaLanMat", "Mã lần mất");
                SetHeader("TenSach", "Tên sách");
                SetHeader("NgayMat", "Ngày mất");
                SetHeader("SLMat", "Số lượng mất");
                SetHeader("LyDo", "Lý do");
                if (grid.Columns.Contains("NgayMat")) grid.Columns["NgayMat"].DefaultCellStyle.Format = "d";
                UpdateStockLimit();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void SetHeader(string columnName, string header)
        {
            if (grid.Columns.Contains(columnName)) grid.Columns[columnName].HeaderText = header;
        }

        private void UpdateStockLimit()
        {
            if (cboSach.SelectedItem == null) return;
            var row = (cboSach.SelectedItem as DataRowView);
            if (row == null) return;
            int stock = Convert.ToInt32(row["TonKho"]);
            nudSoLuong.Maximum = Math.Max(1, stock);
            if (nudSoLuong.Value > nudSoLuong.Maximum) nudSoLuong.Value = nudSoLuong.Maximum;
        }

        private void SaveLoss()
        {
            try
            {
                if (cboSach.SelectedValue == null || cboSach.SelectedValue == DBNull.Value)
                    throw new InvalidOperationException("Chưa có sách để ghi nhận mất.");

                var selectedBook = cboSach.SelectedItem as DataRowView;
                int stock = selectedBook == null ? 0 : Convert.ToInt32(selectedBook["TonKho"]);
                int quantity = Convert.ToInt32(nudSoLuong.Value);
                if (stock < quantity)
                    throw new InvalidOperationException("Số lượng tồn kho không đủ để ghi nhận sách mất.");

                using (var connection = new SqlConnection(DB.ConnStr))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (var command = new SqlCommand(@"INSERT INTO MatSach (MaSach, NgayMat, SLMat, LyDo)
VALUES (@MaSach, @NgayMat, @SLMat, @LyDo);", connection, transaction))
                            {
                                command.Parameters.Add("@MaSach", SqlDbType.Int).Value = Convert.ToInt32(cboSach.SelectedValue);
                                command.Parameters.Add("@NgayMat", SqlDbType.Date).Value = dtpNgayMat.Value.Date;
                                command.Parameters.Add("@SLMat", SqlDbType.Int).Value = quantity;
                                command.Parameters.Add("@LyDo", SqlDbType.NVarChar, 200).Value = string.IsNullOrWhiteSpace(txtLyDo.Text)
                                    ? (object)DBNull.Value
                                    : txtLyDo.Text.Trim();
                                command.ExecuteNonQuery();
                            }
                            transaction.Commit();
                        }
                        catch
                        {
                            try { transaction.Rollback(); } catch { }
                            throw;
                        }
                    }
                }

                MessageBox.Show(this, "Đã ghi nhận sách mất và cập nhật tồn kho.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearEntry();
                LoadData();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void DeleteSelectedLoss()
        {
            if (grid.CurrentRow == null || grid.CurrentRow.IsNewRow)
            {
                MessageBox.Show(this, "Chọn bản ghi mất cần xóa.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int id = Convert.ToInt32(grid.CurrentRow.Cells["MaLanMat"].Value);
            if (MessageBox.Show(this, "Xóa bản ghi này? Số lượng sách sẽ được hoàn lại kho.", Text,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                using (var connection = new SqlConnection(DB.ConnStr))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (var command = new SqlCommand("DELETE FROM MatSach WHERE MaLanMat=@MaLanMat", connection, transaction))
                            {
                                command.Parameters.Add("@MaLanMat", SqlDbType.Int).Value = id;
                                command.ExecuteNonQuery();
                            }
                            transaction.Commit();
                        }
                        catch
                        {
                            try { transaction.Rollback(); } catch { }
                            throw;
                        }
                    }
                }
                LoadData();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void ClearEntry()
        {
            dtpNgayMat.Value = DateTime.Today;
            nudSoLuong.Value = 1;
            txtLyDo.Clear();
            if (cboSach.Items.Count > 0) cboSach.SelectedIndex = 0;
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Không thể thực hiện", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
