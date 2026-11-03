using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public sealed partial class frmBaoCao : Form
    {
        public frmBaoCao()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            StartPosition = FormStartPosition.CenterParent;
            ConfigurePeriod(nudQuyKhongBan, 1, 4, (DateTime.Today.Month - 1) / 3 + 1);
            ConfigurePeriod(nudNamKhongBan, 2000, 2100, DateTime.Today.Year);
            ConfigurePeriod(nudNamTopNhap, 2000, 2100, DateTime.Today.Year);
            ConfigurePeriod(nudQuyBan, 1, 4, (DateTime.Today.Month - 1) / 3 + 1);
            ConfigurePeriod(nudNamBan, 2000, 2100, DateTime.Today.Year);
            ConfigurePeriod(nudThangKhach, 1, 12, DateTime.Today.Month);
            ConfigurePeriod(nudNamKhach, 2000, 2100, DateTime.Today.Year);
            btnKhongBan.Click += delegate { RunReport(gridKhongBan, "sp_SachKhongBanTheoQuy", new SqlParameter("@Quy", (int)nudQuyKhongBan.Value), new SqlParameter("@Nam", (int)nudNamKhongBan.Value)); };
            btnTopNhap.Click += delegate { RunReport(gridTopNhap, "sp_Top5HoaDonNhap", new SqlParameter("@Nam", (int)nudNamTopNhap.Value)); };
            btnBanQuy.Click += delegate { RunSalesQuarterReport(); };
            btnKhach.Click += delegate { RunReport(gridKhach, "sp_KhachKhongMuaTheoThang", new SqlParameter("@Thang", (int)nudThangKhach.Value), new SqlParameter("@Nam", (int)nudNamKhach.Value)); };
        }

        private static void ConfigurePeriod(NumericUpDown control, int min, int max, int value)
        {
            control.Minimum = min;
            control.Maximum = max;
            control.Value = Math.Max(min, Math.Min(max, value));
            control.Width = 90;
            control.Margin = new Padding(3, 5, 8, 4);
        }

        private static void RunReport(DataGridView grid, string procedure, params SqlParameter[] parameters)
        {
            try
            {
                grid.DataSource = DB.GetTableSP(procedure, parameters);
                FormatReportGrid(grid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Không thể tải báo cáo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RunSalesQuarterReport()
        {
            try
            {
                var data = new DataSet();
                using (var connection = new SqlConnection(DB.ConnStr))
                using (var adapter = new SqlDataAdapter("sp_HoaDonBanTheoQuy", connection))
                {
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add("@Quy", SqlDbType.Int).Value = (int)nudQuyBan.Value;
                    adapter.SelectCommand.Parameters.Add("@Nam", SqlDbType.Int).Value = (int)nudNamBan.Value;
                    adapter.Fill(data);
                }

                gridBanQuy.DataSource = data.Tables.Count > 0 ? data.Tables[0] : null;
                FormatReportGrid(gridBanQuy);
                decimal total = data.Tables.Count > 1 && data.Tables[1].Rows.Count > 0
                    ? Convert.ToDecimal(data.Tables[1].Rows[0]["TongTienQuy"])
                    : 0m;
                lblTongBanQuy.Text = "Tổng tiền quý: " + total.ToString("N0") + " đ";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Không thể tải báo cáo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void FormatReportGrid(DataGridView grid)
        {
            var captions = new System.Collections.Generic.Dictionary<string, string>
            {
                { "MaSach", "Mã sách" }, { "TenSach", "Tên sách" }, { "TonKho", "Tồn kho" },
                { "SoHDN", "Số hóa đơn nhập" }, { "NgayNhap", "Ngày nhập" }, { "TenNCC", "Nhà cung cấp" },
                { "TongTien", "Tổng tiền" }, { "SoHDB", "Số hóa đơn bán" }, { "NgayBan", "Ngày bán" },
                { "TenKhach", "Khách hàng" }, { "TenNV", "Nhân viên" }, { "DienThoai", "Điện thoại" }
            };
            foreach (var pair in captions)
                if (grid.Columns.Contains(pair.Key)) grid.Columns[pair.Key].HeaderText = pair.Value;
            foreach (string column in new[] { "NgayNhap", "NgayBan" })
                if (grid.Columns.Contains(column)) grid.Columns[column].DefaultCellStyle.Format = "d";
            foreach (string column in new[] { "TongTien" })
                if (grid.Columns.Contains(column)) grid.Columns[column].DefaultCellStyle.Format = "N0";
        }
    }
}
