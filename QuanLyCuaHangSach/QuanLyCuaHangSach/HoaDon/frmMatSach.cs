using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public sealed class frmMatSach : Form
    {
        private readonly ComboBox cboSach = new ComboBox();
        private readonly DateTimePicker dtpNgayMat = new DateTimePicker();
        private readonly NumericUpDown nudSoLuong = new NumericUpDown();
        private readonly TextBox txtLyDo = new TextBox();
        private readonly DataGridView grid = new DataGridView();

        public frmMatSach()
        {
            Text = "Ghi nhận sách mất";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(850, 560);
            Size = new Size(1020, 670);
            Font = new Font("Segoe UI", 9F);

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(14),
                ColumnCount = 1,
                RowCount = 4
            };
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var title = new Label
            {
                Text = "GHI NHẬN SÁCH MẤT",
                Dock = DockStyle.Top,
                Height = 40,
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 64, 102),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var fields = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                ColumnCount = 4,
                RowCount = 3,
                Padding = new Padding(0, 6, 0, 8)
            };
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105));
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95));
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            fields.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
            fields.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
            fields.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));

            cboSach.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSach.Dock = DockStyle.Fill;
            cboSach.Margin = new Padding(3, 5, 10, 5);
            cboSach.SelectedIndexChanged += delegate { UpdateStockLimit(); };
            dtpNgayMat.Format = DateTimePickerFormat.Short;
            dtpNgayMat.Value = DateTime.Today;
            dtpNgayMat.Dock = DockStyle.Left;
            nudSoLuong.Minimum = 1;
            nudSoLuong.Maximum = int.MaxValue;
            nudSoLuong.Value = 1;
            nudSoLuong.Width = 120;
            nudSoLuong.Margin = new Padding(3, 5, 10, 5);
            txtLyDo.Dock = DockStyle.Fill;
            txtLyDo.MaxLength = 200;
            txtLyDo.Multiline = true;

            fields.Controls.Add(MakeLabel("Sách *"), 0, 0);
            fields.Controls.Add(cboSach, 1, 0);
            fields.Controls.Add(MakeLabel("Ngày mất"), 2, 0);
            fields.Controls.Add(dtpNgayMat, 3, 0);
            fields.Controls.Add(MakeLabel("Số lượng *"), 0, 1);
            fields.Controls.Add(nudSoLuong, 1, 1);
            fields.Controls.Add(MakeLabel("Lý do"), 0, 2);
            fields.Controls.Add(txtLyDo, 1, 2);
            fields.SetColumnSpan(txtLyDo, 3);

            var actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Padding = new Padding(0, 0, 0, 6)
            };
            actions.Controls.Add(MakeButton("Ghi nhận mất", delegate { SaveLoss(); }));
            actions.Controls.Add(MakeButton("Làm mới", delegate { ClearEntry(); }));
            actions.Controls.Add(MakeButton("Xóa bản ghi đang chọn", delegate { DeleteSelectedLoss(); }));
            actions.Controls.Add(MakeButton("Tải lại", delegate { LoadData(); }));

            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.MultiSelect = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.BackgroundColor = Color.White;

            root.Controls.Add(title, 0, 0);
            root.Controls.Add(fields, 0, 1);
            root.Controls.Add(actions, 0, 2);
            root.Controls.Add(grid, 0, 3);
            Controls.Add(root);

            Load += delegate { LoadData(); };
        }

        private static Label MakeLabel(string text)
        {
            return new Label { Text = text, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        }

        private static Button MakeButton(string text, EventHandler click)
        {
            var button = new Button { Text = text, AutoSize = true, Height = 34, Margin = new Padding(4) };
            button.Click += click;
            return button;
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
