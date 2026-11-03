namespace QuanLyCuaHangSach
{
    public partial class frmKhachHang : CatalogFormBase
    {
        public frmKhachHang() : base("Danh mục khách hàng", "KhachHang", "MaKhach",
            new CatalogField("TenKhach", "Tên khách hàng", CatalogValueKind.Text, true, 100),
            new CatalogField("DienThoai", "Điện thoại", CatalogValueKind.Text, false, 15),
            new CatalogField("DiaChi", "Địa chỉ", CatalogValueKind.Text, false, 200)) { }
    }
}
