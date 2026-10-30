using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyCuaHangSach
{
    public static class DB
    {
        public static string ConnStr =
            ConfigurationManager.ConnectionStrings["QLSach"].ConnectionString;

        // Tạo tham số nhanh: DB.P("@ten", "abc")
        public static SqlParameter P(string name, object value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }

        // SELECT -> DataTable
        public static DataTable GetTable(string sql, params SqlParameter[] ps)
        {
            using (var con = new SqlConnection(ConnStr))
            using (var da = new SqlDataAdapter(sql, con))
            {
                if (ps != null) da.SelectCommand.Parameters.AddRange(ps);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Gọi stored procedure -> DataTable
        public static DataTable GetTableSP(string spName, params SqlParameter[] ps)
        {
            using (var con = new SqlConnection(ConnStr))
            using (var da = new SqlDataAdapter(spName, con))
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (ps != null) da.SelectCommand.Parameters.AddRange(ps);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // INSERT / UPDATE / DELETE -> số dòng bị ảnh hưởng
        public static int Exec(string sql, params SqlParameter[] ps)
        {
            using (var con = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(sql, con))
            {
                if (ps != null) cmd.Parameters.AddRange(ps);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // Lấy 1 giá trị (COUNT, SCOPE_IDENTITY...)
        public static object Scalar(string sql, params SqlParameter[] ps)
        {
            using (var con = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(sql, con))
            {
                if (ps != null) cmd.Parameters.AddRange(ps);
                con.Open();
                return cmd.ExecuteScalar();
            }
        }
    }
}
