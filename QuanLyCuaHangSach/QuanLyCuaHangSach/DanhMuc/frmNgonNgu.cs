namespace QuanLyCuaHangSach
{
    public partial class frmNgonNgu : CatalogFormBase
    {
        public frmNgonNgu() : base("Danh mục ngôn ngữ", "NgonNgu", "MaNgonNgu",
            new CatalogField("TenNgonNgu", "Tên ngôn ngữ", CatalogValueKind.Text, true, 50)) { }
    }
}
