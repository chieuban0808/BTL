namespace QuanLyCuaHangSach
{
    public partial class frmTacGia : CatalogFormBase
    {
        public frmTacGia() : base("Danh mục tác giả", "TacGia", "MaTG",
            new CatalogField("TenTG", "Tên tác giả", CatalogValueKind.Text, true, 100),
            new CatalogField("NgaySinh", "Ngày sinh (ví dụ 15/08/1980)", CatalogValueKind.Date, false, 0),
            new CatalogField("GioiTinh", "Giới tính", CatalogValueKind.Text, false, 5),
            new CatalogField("DiaChi", "Địa chỉ", CatalogValueKind.Text, false, 200)) { }
    }
}
