namespace QuanLyCuaHangSach
{
    public partial class frmNXB : CatalogFormBase
    {
        public frmNXB() : base("Danh mục nhà xuất bản", "NXB", "MaNXB",
            new CatalogField("TenNXB", "Tên nhà xuất bản", CatalogValueKind.Text, true, 150),
            new CatalogField("DiaChi", "Địa chỉ", CatalogValueKind.Text, false, 200),
            new CatalogField("DienThoai", "Điện thoại", CatalogValueKind.Text, false, 15)) { }
    }
}
