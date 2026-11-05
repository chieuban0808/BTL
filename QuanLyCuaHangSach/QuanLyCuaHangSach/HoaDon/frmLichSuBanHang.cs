using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class frmLichSuBanHang : Form
    {
        private int? selectedInvoiceId;
        public frmLichSuBanHang()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            btnTaiLai.Click += delegate { LoadInvoices(); };
            btnXoaChiTiet.Click += delegate { DeleteSelectedDetail(); };
            btnXuatExcel.Click += delegate { ExportToExcel(); };
            gridHoaDon.CellClick += delegate(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex < 0) return;
                selectedInvoiceId = Convert.ToInt32(gridHoaDon.Rows[e.RowIndex].Cells["SoHDB"].Value);
                LoadDetails();
            };
            Load += delegate { LoadInvoices(); };
        }
        private void LoadInvoices()
        {
            try
            {
                gridHoaDon.DataSource = DB.GetTable(@"SELECT h.SoHDB, h.NgayBan, kh.TenKhach, nv.TenNV, h.TongTien
FROM HoaDonBan h JOIN KhachHang kh ON kh.MaKhach=h.MaKhach JOIN NhanVien nv ON nv.MaNV=h.MaNV ORDER BY h.SoHDB DESC");
                SetHeader(gridHoaDon, "SoHDB", "Số hóa đơn"); SetHeader(gridHoaDon, "NgayBan", "Ngày bán");
                SetHeader(gridHoaDon, "TenKhach", "Khách hàng"); SetHeader(gridHoaDon, "TenNV", "Thu ngân"); SetHeader(gridHoaDon, "TongTien", "Tổng tiền");
                if (gridHoaDon.Columns.Contains("NgayBan")) gridHoaDon.Columns["NgayBan"].DefaultCellStyle.Format = "d";
                if (gridHoaDon.Columns.Contains("TongTien")) gridHoaDon.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                if (gridHoaDon.Rows.Count == 0) { selectedInvoiceId = null; gridChiTiet.DataSource = null; return; }
                int rowIndex = 0;
                if (selectedInvoiceId.HasValue)
                    for (int i = 0; i < gridHoaDon.Rows.Count; i++)
                        if (Convert.ToInt32(gridHoaDon.Rows[i].Cells["SoHDB"].Value) == selectedInvoiceId.Value) { rowIndex = i; break; }
                gridHoaDon.CurrentCell = gridHoaDon.Rows[rowIndex].Cells["SoHDB"];
                selectedInvoiceId = Convert.ToInt32(gridHoaDon.Rows[rowIndex].Cells["SoHDB"].Value);
                LoadDetails();
            }
            catch (Exception ex) { ShowError(ex); }
        }
        private void LoadDetails()
        {
            if (!selectedInvoiceId.HasValue) return;
            try
            {
                gridChiTiet.DataSource = DB.GetTable(@"SELECT c.SoHDB, c.MaSach, s.TenSach, c.SLBan, c.DonGiaBan, c.KhuyenMai, c.ThanhTien
FROM ChiTietHDB c JOIN Sach s ON s.MaSach=c.MaSach WHERE c.SoHDB=@SoHDB ORDER BY s.TenSach",
                    new SqlParameter("@SoHDB", SqlDbType.Int) { Value = selectedInvoiceId.Value });
                if (gridChiTiet.Columns.Contains("SoHDB")) gridChiTiet.Columns["SoHDB"].Visible = false;
                if (gridChiTiet.Columns.Contains("MaSach")) gridChiTiet.Columns["MaSach"].Visible = false;
                SetHeader(gridChiTiet, "TenSach", "Sách"); SetHeader(gridChiTiet, "SLBan", "Số lượng");
                SetHeader(gridChiTiet, "DonGiaBan", "Đơn giá"); SetHeader(gridChiTiet, "KhuyenMai", "Giảm (%)"); SetHeader(gridChiTiet, "ThanhTien", "Thành tiền");
                foreach (string name in new[] { "DonGiaBan", "ThanhTien" })
                    if (gridChiTiet.Columns.Contains(name)) gridChiTiet.Columns[name].DefaultCellStyle.Format = "N0";
                lblInvoiceDetail.Text = "Chi tiết hóa đơn số " + selectedInvoiceId.Value + " — xóa dòng sẽ hoàn tồn kho.";
            }
            catch (Exception ex) { ShowError(ex); }
        }
        private void DeleteSelectedDetail()
        {
            if (!selectedInvoiceId.HasValue || gridChiTiet.CurrentRow == null) return;
            if (MessageBox.Show(this, "Xóa sách khỏi hóa đơn số " + selectedInvoiceId.Value + "? Trigger sẽ tự hoàn tồn kho.",
                Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                DB.Exec("DELETE FROM ChiTietHDB WHERE SoHDB=@SoHDB AND MaSach=@MaSach",
                    new SqlParameter("@SoHDB", SqlDbType.Int) { Value = selectedInvoiceId.Value },
                    new SqlParameter("@MaSach", SqlDbType.Int) { Value = Convert.ToInt32(gridChiTiet.CurrentRow.Cells["MaSach"].Value) });
                LoadDetails(); LoadInvoices();
            }
            catch (Exception ex) { ShowError(ex); }
        }
        private void ExportToExcel()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Xuất lịch sử bán hàng";
                dialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx|Tệp CSV mở bằng Excel (*.csv)|*.csv";
                dialog.FileName = "LichSuBanHang_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xlsx";
                dialog.DefaultExt = "xlsx";
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    DataTable rows = DB.GetTable(@"SELECT h.SoHDB, h.NgayBan, kh.TenKhach, nv.TenNV,
 s.MaSach, s.TenSach, c.SLBan, c.DonGiaBan, c.KhuyenMai, c.ThanhTien, h.TongTien
FROM HoaDonBan h JOIN KhachHang kh ON kh.MaKhach=h.MaKhach
JOIN NhanVien nv ON nv.MaNV=h.MaNV
JOIN ChiTietHDB c ON c.SoHDB=h.SoHDB JOIN Sach s ON s.MaSach=c.MaSach
ORDER BY h.NgayBan DESC, h.SoHDB DESC, s.TenSach");
                    ExcelReportExporter.Export(rows, "LỊCH SỬ BÁN HÀNG",
                        new[] { "Số hóa đơn", "Ngày bán", "Khách hàng", "Thu ngân", "Mã sách", "Tên sách", "Số lượng", "Đơn giá", "Giảm (%)", "Thành tiền", "Tổng hóa đơn" },
                        new[] { "SoHDB", "NgayBan", "TenKhach", "TenNV", "MaSach", "TenSach", "SLBan", "DonGiaBan", "KhuyenMai", "ThanhTien", "TongTien" },
                        "SoHDB", "TongTien", "NgayBan", dialog.FileName);
                    MessageBox.Show(this, "Đã xuất " + rows.Rows.Count + " dòng chi tiết hóa đơn.", "Xuất Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { MessageBox.Show(this, ex.Message, "Xuất Excel thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
        private static void SetHeader(DataGridView grid, string column, string header)
        { if (grid.Columns.Contains(column)) grid.Columns[column].HeaderText = header; }
        private void ShowError(Exception ex)
        { MessageBox.Show(this, ex.Message, "Không thể tải lịch sử bán", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }
}
