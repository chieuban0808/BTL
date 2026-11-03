namespace QuanLyCuaHangSach
{
    public enum AppRole { Manager, Sales, Stock }

    public static class AppSession
    {
        public static int EmployeeId { get; set; }
        public static string EmployeeName { get; set; }
        public static string JobName { get; set; }
        public static AppRole Role { get; set; }
        public static bool IsAuthenticated { get; set; }

        public static void Clear()
        {
            EmployeeId = 0;
            EmployeeName = null;
            JobName = null;
            IsAuthenticated = false;
        }
    }
}
