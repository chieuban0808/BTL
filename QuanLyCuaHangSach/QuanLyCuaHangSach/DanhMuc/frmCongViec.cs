namespace QuanLyCuaHangSach
{
    public partial class frmCongViec : CatalogFormBase
    {
        public frmCongViec() : base("Danh mục công việc", "CongViec", "MaCV",
            new CatalogField("TenCV", "Tên công việc", CatalogValueKind.Text, true, 100)) { }
    }
}
