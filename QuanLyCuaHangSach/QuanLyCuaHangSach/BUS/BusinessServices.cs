using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QuanLyCuaHangSach
{
    // BUS: nhận yêu cầu từ giao diện, gọi DAL (DB) và trả dữ liệu/kết quả cho GUI.
    public static class CatalogBUS
    {
        public static DataTable GetRows(string table, string key, IEnumerable<string> fields)
        {
            string columns = string.Join(", ", fields.Select(QuoteIdentifier));
            return DB.GetTable("SELECT " + QuoteIdentifier(key) + ", " + columns +
                " FROM " + QuoteIdentifier(table) + " ORDER BY " + QuoteIdentifier(key) + " DESC");
        }

        public static int Save(string table, string key, IEnumerable<string> fields,
            IEnumerable<SqlParameter> values, int? id)
        {
            var fieldList = fields.ToArray();
            var parameters = values.ToList();
            if (fieldList.Length != parameters.Count)
                throw new ArgumentException("Số trường và tham số không khớp.");

            if (id.HasValue)
            {
                var assignments = fieldList.Select((field, index) => QuoteIdentifier(field) + " = @p" + index);
                parameters.Add(new SqlParameter("@id", id.Value));
                return DB.Exec("UPDATE " + QuoteIdentifier(table) + " SET " + string.Join(", ", assignments) +
                    " WHERE " + QuoteIdentifier(key) + " = @id", parameters.ToArray());
            }

            string names = string.Join(", ", fieldList.Select(QuoteIdentifier));
            string args = string.Join(", ", fieldList.Select((field, index) => "@p" + index));
            return DB.Exec("INSERT INTO " + QuoteIdentifier(table) + " (" + names + ") VALUES (" + args + ")", parameters.ToArray());
        }

        public static int Delete(string table, string key, int id)
        {
            return DB.Exec("DELETE FROM " + QuoteIdentifier(table) + " WHERE " + QuoteIdentifier(key) + " = @id",
                new SqlParameter("@id", id));
        }

        private static string QuoteIdentifier(string value)
        {
            if (String.IsNullOrWhiteSpace(value) || value.Any(c => !(Char.IsLetterOrDigit(c) || c == '_')))
                throw new ArgumentException("Tên bảng/cột không hợp lệ.");
            return "[" + value + "]";
        }
    }

    public static class AuthenticationBUS
    {
        public static DataTable FindEmployee(string username)
        {
            return DB.GetTable(@"SELECT nv.MaNV, nv.TenNV, nv.MatKhau, cv.TenCV
FROM NhanVien nv JOIN CongViec cv ON cv.MaCV=nv.MaCV
WHERE nv.TenDangNhap=@TenDangNhap",
                new SqlParameter("@TenDangNhap", SqlDbType.VarChar, 50) { Value = username });
        }
    }

    public sealed class DashboardSummary
    {
        public decimal Revenue { get; set; }
        public int InvoiceCount { get; set; }
        public DataTable LowStockBooks { get; set; }
    }

    public static class DashboardBUS
    {
        public static DashboardSummary GetTodaySummary()
        {
            var sales = DB.GetTable(@"SELECT ISNULL(SUM(TongTien), 0) AS DoanhThu, COUNT(*) AS SoHoaDon
FROM HoaDonBan WHERE NgayBan=CONVERT(date, GETDATE())");
            var lowStock = DB.GetTable(@"SELECT s.MaSach, s.TenSach, k.SoLuong
FROM KhoSach k JOIN Sach s ON s.MaSach=k.MaSach
WHERE k.SoLuong <= 5 ORDER BY k.SoLuong, s.TenSach");
            return new DashboardSummary
            {
                Revenue = Convert.ToDecimal(sales.Rows[0]["DoanhThu"]),
                InvoiceCount = Convert.ToInt32(sales.Rows[0]["SoHoaDon"]),
                LowStockBooks = lowStock
            };
        }
    }

    public static class SearchBUS
    {
        public static DataTable GetBookTypes() { return DB.GetTable("SELECT MaLoai AS Id, TenLoai AS Ten FROM LoaiSach ORDER BY TenLoai"); }
        public static DataTable GetPublishers() { return DB.GetTable("SELECT MaNXB AS Id, TenNXB AS Ten FROM NXB ORDER BY TenNXB"); }

        public static DataTable SearchBooks(string name, object typeId, object publisherId)
        {
            return DB.GetTableSP("sp_TimSach",
                new SqlParameter("@TenSach", SqlDbType.NVarChar, 200) { Value = String.IsNullOrWhiteSpace(name) ? (object)DBNull.Value : name.Trim() },
                new SqlParameter("@MaLoai", SqlDbType.Int) { Value = typeId ?? DBNull.Value },
                new SqlParameter("@MaNXB", SqlDbType.Int) { Value = publisherId ?? DBNull.Value },
                new SqlParameter("@MaLinhVuc", SqlDbType.Int) { Value = DBNull.Value },
                new SqlParameter("@MaNgonNgu", SqlDbType.Int) { Value = DBNull.Value });
        }

        public static DataTable SearchPurchases(object bookId, object fromDate, object toDate)
        {
            return DB.GetTableSP("sp_TimHoaDonNhap",
                new SqlParameter("@MaSach", SqlDbType.Int) { Value = bookId ?? DBNull.Value },
                new SqlParameter("@TuNgay", SqlDbType.Date) { Value = fromDate ?? DBNull.Value },
                new SqlParameter("@DenNgay", SqlDbType.Date) { Value = toDate ?? DBNull.Value });
        }
    }

    public static class ReportBUS
    {
        public static DataTable GetProcedureReport(string procedure, params SqlParameter[] parameters)
        {
            return DB.GetTableSP(procedure, parameters);
        }

        public static DataSet GetSalesByQuarter(int quarter, int year)
        {
            var data = new DataSet();
            using (var connection = new SqlConnection(DB.ConnStr))
            using (var adapter = new SqlDataAdapter("sp_HoaDonBanTheoQuy", connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.Add("@Quy", SqlDbType.Int).Value = quarter;
                adapter.SelectCommand.Parameters.Add("@Nam", SqlDbType.Int).Value = year;
                adapter.Fill(data);
            }
            return data;
        }
    }
}
