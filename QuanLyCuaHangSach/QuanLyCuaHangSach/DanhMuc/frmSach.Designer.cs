namespace QuanLyCuaHangSach
{
    partial class frmSach
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel layoutRoot;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.TableLayoutPanel layoutFields;
        private System.Windows.Forms.Label lblTenSach;
        private System.Windows.Forms.Label lblLoai;
        private System.Windows.Forms.Label lblTacGia;
        private System.Windows.Forms.Label lblNXB;
        private System.Windows.Forms.Label lblLinhVuc;
        private System.Windows.Forms.Label lblNgonNgu;
        private System.Windows.Forms.Label lblSoTrang;
        private System.Windows.Forms.Label lblAnh;
        private System.Windows.Forms.TextBox txtTenSach;
        private System.Windows.Forms.TextBox txtSoTrang;
        private System.Windows.Forms.TextBox txtAnh;
        private System.Windows.Forms.TextBox txtTim;
        private System.Windows.Forms.ComboBox cboLoai;
        private System.Windows.Forms.ComboBox cboTacGia;
        private System.Windows.Forms.ComboBox cboNXB;
        private System.Windows.Forms.ComboBox cboLinhVuc;
        private System.Windows.Forms.ComboBox cboNgonNgu;
        private System.Windows.Forms.FlowLayoutPanel layoutActions;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.Button btnTatCa;
        private System.Windows.Forms.Label lblTim;
        private System.Windows.Forms.DataGridView grid;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.layoutRoot = new System.Windows.Forms.TableLayoutPanel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.layoutFields = new System.Windows.Forms.TableLayoutPanel();
            this.lblTenSach = new System.Windows.Forms.Label();
            this.txtTenSach = new System.Windows.Forms.TextBox();
            this.lblLoai = new System.Windows.Forms.Label();
            this.cboLoai = new System.Windows.Forms.ComboBox();
            this.lblTacGia = new System.Windows.Forms.Label();
            this.cboTacGia = new System.Windows.Forms.ComboBox();
            this.lblNXB = new System.Windows.Forms.Label();
            this.cboNXB = new System.Windows.Forms.ComboBox();
            this.lblLinhVuc = new System.Windows.Forms.Label();
            this.cboLinhVuc = new System.Windows.Forms.ComboBox();
            this.lblNgonNgu = new System.Windows.Forms.Label();
            this.cboNgonNgu = new System.Windows.Forms.ComboBox();
            this.lblSoTrang = new System.Windows.Forms.Label();
            this.txtSoTrang = new System.Windows.Forms.TextBox();
            this.lblAnh = new System.Windows.Forms.Label();
            this.txtAnh = new System.Windows.Forms.TextBox();
            this.layoutActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.lblTim = new System.Windows.Forms.Label();
            this.txtTim = new System.Windows.Forms.TextBox();
            this.btnTim = new System.Windows.Forms.Button();
            this.btnTatCa = new System.Windows.Forms.Button();
            this.grid = new System.Windows.Forms.DataGridView();
            this.MaSach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenSach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DonGiaNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DonGiaBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenLoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenTG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenNXB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenLinhVuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenNgonNgu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoTrang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.layoutRoot.SuspendLayout();
            this.layoutFields.SuspendLayout();
            this.layoutActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutRoot
            // 
            this.layoutRoot.ColumnCount = 1;
            this.layoutRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutRoot.Controls.Add(this.lblTieuDe, 0, 0);
            this.layoutRoot.Controls.Add(this.layoutFields, 0, 1);
            this.layoutRoot.Controls.Add(this.layoutActions, 0, 2);
            this.layoutRoot.Controls.Add(this.grid, 0, 3);
            this.layoutRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutRoot.Location = new System.Drawing.Point(0, 0);
            this.layoutRoot.Name = "layoutRoot";
            this.layoutRoot.Padding = new System.Windows.Forms.Padding(12);
            this.layoutRoot.RowCount = 4;
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 164F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutRoot.Size = new System.Drawing.Size(1120, 720);
            this.layoutRoot.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(64)))), ((int)(((byte)(102)))));
            this.lblTieuDe.Location = new System.Drawing.Point(15, 12);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(1090, 42);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "SÁCH VÀ KHO SÁCH";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // layoutFields
            // 
            this.layoutFields.ColumnCount = 4;
            this.layoutFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.layoutFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.layoutFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutFields.Controls.Add(this.lblTenSach, 0, 0);
            this.layoutFields.Controls.Add(this.txtTenSach, 1, 0);
            this.layoutFields.Controls.Add(this.lblLoai, 2, 0);
            this.layoutFields.Controls.Add(this.cboLoai, 3, 0);
            this.layoutFields.Controls.Add(this.lblTacGia, 0, 1);
            this.layoutFields.Controls.Add(this.cboTacGia, 1, 1);
            this.layoutFields.Controls.Add(this.lblNXB, 2, 1);
            this.layoutFields.Controls.Add(this.cboNXB, 3, 1);
            this.layoutFields.Controls.Add(this.lblLinhVuc, 0, 2);
            this.layoutFields.Controls.Add(this.cboLinhVuc, 1, 2);
            this.layoutFields.Controls.Add(this.lblNgonNgu, 2, 2);
            this.layoutFields.Controls.Add(this.cboNgonNgu, 3, 2);
            this.layoutFields.Controls.Add(this.lblSoTrang, 0, 3);
            this.layoutFields.Controls.Add(this.txtSoTrang, 1, 3);
            this.layoutFields.Controls.Add(this.lblAnh, 2, 3);
            this.layoutFields.Controls.Add(this.txtAnh, 3, 3);
            this.layoutFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutFields.Location = new System.Drawing.Point(15, 57);
            this.layoutFields.Name = "layoutFields";
            this.layoutFields.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.layoutFields.RowCount = 4;
            this.layoutFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutFields.Size = new System.Drawing.Size(1090, 158);
            this.layoutFields.TabIndex = 1;
            // 
            // lblTenSach
            // 
            this.lblTenSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTenSach.Location = new System.Drawing.Point(3, 4);
            this.lblTenSach.Name = "lblTenSach";
            this.lblTenSach.Size = new System.Drawing.Size(109, 37);
            this.lblTenSach.TabIndex = 0;
            this.lblTenSach.Text = "Tên sách *";
            this.lblTenSach.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTenSach
            // 
            this.txtTenSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTenSach.Location = new System.Drawing.Point(118, 7);
            this.txtTenSach.MaxLength = 200;
            this.txtTenSach.Name = "txtTenSach";
            this.txtTenSach.Size = new System.Drawing.Size(424, 27);
            this.txtTenSach.TabIndex = 1;
            // 
            // lblLoai
            // 
            this.lblLoai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLoai.Location = new System.Drawing.Point(548, 4);
            this.lblLoai.Name = "lblLoai";
            this.lblLoai.Size = new System.Drawing.Size(109, 37);
            this.lblLoai.TabIndex = 2;
            this.lblLoai.Text = "Loại sách *";
            this.lblLoai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboLoai
            // 
            this.cboLoai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoai.Location = new System.Drawing.Point(663, 7);
            this.cboLoai.Name = "cboLoai";
            this.cboLoai.Size = new System.Drawing.Size(424, 28);
            this.cboLoai.TabIndex = 3;
            // 
            // lblTacGia
            // 
            this.lblTacGia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTacGia.Location = new System.Drawing.Point(3, 41);
            this.lblTacGia.Name = "lblTacGia";
            this.lblTacGia.Size = new System.Drawing.Size(109, 37);
            this.lblTacGia.TabIndex = 4;
            this.lblTacGia.Text = "Tác giả";
            this.lblTacGia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboTacGia
            // 
            this.cboTacGia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboTacGia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTacGia.Location = new System.Drawing.Point(118, 44);
            this.cboTacGia.Name = "cboTacGia";
            this.cboTacGia.Size = new System.Drawing.Size(424, 28);
            this.cboTacGia.TabIndex = 5;
            // 
            // lblNXB
            // 
            this.lblNXB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNXB.Location = new System.Drawing.Point(548, 41);
            this.lblNXB.Name = "lblNXB";
            this.lblNXB.Size = new System.Drawing.Size(109, 37);
            this.lblNXB.TabIndex = 6;
            this.lblNXB.Text = "Nhà xuất bản";
            this.lblNXB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboNXB
            // 
            this.cboNXB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboNXB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNXB.Location = new System.Drawing.Point(663, 44);
            this.cboNXB.Name = "cboNXB";
            this.cboNXB.Size = new System.Drawing.Size(424, 28);
            this.cboNXB.TabIndex = 7;
            // 
            // lblLinhVuc
            // 
            this.lblLinhVuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLinhVuc.Location = new System.Drawing.Point(3, 78);
            this.lblLinhVuc.Name = "lblLinhVuc";
            this.lblLinhVuc.Size = new System.Drawing.Size(109, 37);
            this.lblLinhVuc.TabIndex = 8;
            this.lblLinhVuc.Text = "Lĩnh vực";
            this.lblLinhVuc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboLinhVuc
            // 
            this.cboLinhVuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboLinhVuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLinhVuc.Location = new System.Drawing.Point(118, 81);
            this.cboLinhVuc.Name = "cboLinhVuc";
            this.cboLinhVuc.Size = new System.Drawing.Size(424, 28);
            this.cboLinhVuc.TabIndex = 9;
            // 
            // lblNgonNgu
            // 
            this.lblNgonNgu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNgonNgu.Location = new System.Drawing.Point(548, 78);
            this.lblNgonNgu.Name = "lblNgonNgu";
            this.lblNgonNgu.Size = new System.Drawing.Size(109, 37);
            this.lblNgonNgu.TabIndex = 10;
            this.lblNgonNgu.Text = "Ngôn ngữ";
            this.lblNgonNgu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboNgonNgu
            // 
            this.cboNgonNgu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboNgonNgu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNgonNgu.Location = new System.Drawing.Point(663, 81);
            this.cboNgonNgu.Name = "cboNgonNgu";
            this.cboNgonNgu.Size = new System.Drawing.Size(424, 28);
            this.cboNgonNgu.TabIndex = 11;
            // 
            // lblSoTrang
            // 
            this.lblSoTrang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSoTrang.Location = new System.Drawing.Point(3, 115);
            this.lblSoTrang.Name = "lblSoTrang";
            this.lblSoTrang.Size = new System.Drawing.Size(109, 39);
            this.lblSoTrang.TabIndex = 12;
            this.lblSoTrang.Text = "Số trang";
            this.lblSoTrang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSoTrang
            // 
            this.txtSoTrang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSoTrang.Location = new System.Drawing.Point(118, 118);
            this.txtSoTrang.Name = "txtSoTrang";
            this.txtSoTrang.Size = new System.Drawing.Size(424, 27);
            this.txtSoTrang.TabIndex = 13;
            // 
            // lblAnh
            // 
            this.lblAnh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAnh.Location = new System.Drawing.Point(548, 115);
            this.lblAnh.Name = "lblAnh";
            this.lblAnh.Size = new System.Drawing.Size(109, 39);
            this.lblAnh.TabIndex = 14;
            this.lblAnh.Text = "Ảnh bìa";
            this.lblAnh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAnh
            // 
            this.txtAnh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAnh.Location = new System.Drawing.Point(663, 118);
            this.txtAnh.MaxLength = 260;
            this.txtAnh.Name = "txtAnh";
            this.txtAnh.Size = new System.Drawing.Size(424, 27);
            this.txtAnh.TabIndex = 15;
            // 
            // layoutActions
            // 
            this.layoutActions.Controls.Add(this.btnThem);
            this.layoutActions.Controls.Add(this.btnLuu);
            this.layoutActions.Controls.Add(this.btnXoa);
            this.layoutActions.Controls.Add(this.lblTim);
            this.layoutActions.Controls.Add(this.txtTim);
            this.layoutActions.Controls.Add(this.btnTim);
            this.layoutActions.Controls.Add(this.btnTatCa);
            this.layoutActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutActions.Location = new System.Drawing.Point(15, 221);
            this.layoutActions.Name = "layoutActions";
            this.layoutActions.Size = new System.Drawing.Size(1090, 38);
            this.layoutActions.TabIndex = 2;
            // 
            // btnThem
            // 
            this.btnThem.AutoSize = true;
            this.btnThem.Location = new System.Drawing.Point(3, 3);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(86, 34);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm mới";
            // 
            // btnLuu
            // 
            this.btnLuu.AutoSize = true;
            this.btnLuu.Location = new System.Drawing.Point(95, 3);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 34);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "Lưu";
            // 
            // btnXoa
            // 
            this.btnXoa.AutoSize = true;
            this.btnXoa.Location = new System.Drawing.Point(176, 3);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 34);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            // 
            // lblTim
            // 
            this.lblTim.AutoSize = true;
            this.lblTim.Location = new System.Drawing.Point(257, 0);
            this.lblTim.Name = "lblTim";
            this.lblTim.Padding = new System.Windows.Forms.Padding(10, 9, 0, 0);
            this.lblTim.Size = new System.Drawing.Size(105, 29);
            this.lblTim.TabIndex = 3;
            this.lblTim.Text = "Tìm tên sách:";
            // 
            // txtTim
            // 
            this.txtTim.Location = new System.Drawing.Point(368, 3);
            this.txtTim.Name = "txtTim";
            this.txtTim.Size = new System.Drawing.Size(240, 27);
            this.txtTim.TabIndex = 4;
            // 
            // btnTim
            // 
            this.btnTim.AutoSize = true;
            this.btnTim.Location = new System.Drawing.Point(614, 3);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(75, 34);
            this.btnTim.TabIndex = 5;
            this.btnTim.Text = "Tìm";
            // 
            // btnTatCa
            // 
            this.btnTatCa.AutoSize = true;
            this.btnTatCa.Location = new System.Drawing.Point(695, 3);
            this.btnTatCa.Name = "btnTatCa";
            this.btnTatCa.Size = new System.Drawing.Size(75, 34);
            this.btnTatCa.TabIndex = 6;
            this.btnTatCa.Text = "Tất cả";
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grid.BackgroundColor = System.Drawing.Color.White;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaSach,
            this.TenSach,
            this.SoLuong,
            this.DonGiaNhap,
            this.DonGiaBan,
            this.TenLoai,
            this.TenTG,
            this.TenNXB,
            this.TenLinhVuc,
            this.TenNgonNgu,
            this.SoTrang});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(15, 265);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersVisible = false;
            this.grid.RowHeadersWidth = 51;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(1090, 440);
            this.grid.TabIndex = 3;
            // 
            // MaSach
            // 
            this.MaSach.DataPropertyName = "MaSach";
            this.MaSach.HeaderText = "Mã sách";
            this.MaSach.MinimumWidth = 6;
            this.MaSach.Name = "MaSach";
            this.MaSach.ReadOnly = true;
            this.MaSach.Width = 92;
            // 
            // TenSach
            // 
            this.TenSach.DataPropertyName = "TenSach";
            this.TenSach.HeaderText = "Tên sách";
            this.TenSach.MinimumWidth = 6;
            this.TenSach.Name = "TenSach";
            this.TenSach.ReadOnly = true;
            this.TenSach.Width = 94;
            // 
            // SoLuong
            // 
            this.SoLuong.DataPropertyName = "SoLuong";
            this.SoLuong.HeaderText = "Tồn kho";
            this.SoLuong.MinimumWidth = 6;
            this.SoLuong.Name = "SoLuong";
            this.SoLuong.ReadOnly = true;
            this.SoLuong.Width = 91;
            // 
            // DonGiaNhap
            // 
            this.DonGiaNhap.DataPropertyName = "DonGiaNhap";
            this.DonGiaNhap.HeaderText = "Giá nhập";
            this.DonGiaNhap.MinimumWidth = 6;
            this.DonGiaNhap.Name = "DonGiaNhap";
            this.DonGiaNhap.ReadOnly = true;
            this.DonGiaNhap.Width = 97;
            // 
            // DonGiaBan
            // 
            this.DonGiaBan.DataPropertyName = "DonGiaBan";
            this.DonGiaBan.HeaderText = "Giá bán";
            this.DonGiaBan.MinimumWidth = 6;
            this.DonGiaBan.Name = "DonGiaBan";
            this.DonGiaBan.ReadOnly = true;
            this.DonGiaBan.Width = 89;
            // 
            // TenLoai
            // 
            this.TenLoai.DataPropertyName = "TenLoai";
            this.TenLoai.HeaderText = "Loại sách";
            this.TenLoai.MinimumWidth = 6;
            this.TenLoai.Name = "TenLoai";
            this.TenLoai.ReadOnly = true;
            this.TenLoai.Width = 99;
            // 
            // TenTG
            // 
            this.TenTG.DataPropertyName = "TenTG";
            this.TenTG.HeaderText = "Tác giả";
            this.TenTG.MinimumWidth = 6;
            this.TenTG.Name = "TenTG";
            this.TenTG.ReadOnly = true;
            this.TenTG.Width = 84;
            // 
            // TenNXB
            // 
            this.TenNXB.DataPropertyName = "TenNXB";
            this.TenNXB.HeaderText = "Nhà xuất bản";
            this.TenNXB.MinimumWidth = 6;
            this.TenNXB.Name = "TenNXB";
            this.TenNXB.ReadOnly = true;
            this.TenNXB.Width = 126;
            // 
            // TenLinhVuc
            // 
            this.TenLinhVuc.DataPropertyName = "TenLinhVuc";
            this.TenLinhVuc.HeaderText = "Lĩnh vực";
            this.TenLinhVuc.MinimumWidth = 6;
            this.TenLinhVuc.Name = "TenLinhVuc";
            this.TenLinhVuc.ReadOnly = true;
            this.TenLinhVuc.Width = 92;
            // 
            // TenNgonNgu
            // 
            this.TenNgonNgu.DataPropertyName = "TenNgonNgu";
            this.TenNgonNgu.HeaderText = "Ngôn ngữ";
            this.TenNgonNgu.MinimumWidth = 6;
            this.TenNgonNgu.Name = "TenNgonNgu";
            this.TenNgonNgu.ReadOnly = true;
            this.TenNgonNgu.Width = 105;
            // 
            // SoTrang
            // 
            this.SoTrang.DataPropertyName = "SoTrang";
            this.SoTrang.HeaderText = "Số trang";
            this.SoTrang.MinimumWidth = 6;
            this.SoTrang.Name = "SoTrang";
            this.SoTrang.ReadOnly = true;
            this.SoTrang.Width = 94;
            // 
            // frmSach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 720);
            this.Controls.Add(this.layoutRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1020, 650);
            this.Name = "frmSach";
            this.Text = "Danh mục sách và kho sách";
            this.layoutRoot.ResumeLayout(false);
            this.layoutFields.ResumeLayout(false);
            this.layoutFields.PerformLayout();
            this.layoutActions.ResumeLayout(false);
            this.layoutActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.DataGridViewTextBoxColumn MaSach;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenSach;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn DonGiaNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn DonGiaBan;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenLoai;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenTG;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenNXB;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenLinhVuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenNgonNgu;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoTrang;
    }
}
