namespace QuanLyCuaHangSach
{
    public partial class frmNhaCungCap : CatalogFormBase
    {
        public frmNhaCungCap() : base("Danh mục nhà cung cấp", "NhaCungCap", "MaNCC",
            new CatalogField("TenNCC", "Tên nhà cung cấp", CatalogValueKind.Text, true, 150),
            new CatalogField("DiaChi", "Địa chỉ", CatalogValueKind.Text, false, 200),
            new CatalogField("DienThoai", "Điện thoại", CatalogValueKind.Text, false, 15)) { }
    }
}
