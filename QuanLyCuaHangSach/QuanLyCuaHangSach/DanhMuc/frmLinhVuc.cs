namespace QuanLyCuaHangSach
{
    public partial class frmLinhVuc : CatalogFormBase
    {
        public frmLinhVuc() : base("Danh mục lĩnh vực", "LinhVuc", "MaLinhVuc",
            new CatalogField("TenLinhVuc", "Tên lĩnh vực", CatalogValueKind.Text, true, 100)) { }
    }
}
