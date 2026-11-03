namespace QuanLyCuaHangSach
{
    public partial class frmLoaiSach : CatalogFormBase
    {
        public frmLoaiSach() : base("Danh mục loại sách", "LoaiSach", "MaLoai",
            new CatalogField("TenLoai", "Tên loại sách", CatalogValueKind.Text, true, 100)) { }
    }
}
