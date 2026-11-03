namespace QuanLyCuaHangSach
{
    partial class frmTimKiem
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabSach;
        private System.Windows.Forms.TabPage tabNhap;
        private System.Windows.Forms.TableLayoutPanel layoutSach;
        private System.Windows.Forms.TableLayoutPanel layoutNhap;
        private System.Windows.Forms.FlowLayoutPanel filtersSach;
        private System.Windows.Forms.FlowLayoutPanel filtersNhap;
        private System.Windows.Forms.Label lblTenSach;
        private System.Windows.Forms.TextBox txtTenSach;
        private System.Windows.Forms.Label lblLoai;
        private System.Windows.Forms.ComboBox cboLoai;
        private System.Windows.Forms.Label lblNXB;
        private System.Windows.Forms.ComboBox cboNXB;
        private System.Windows.Forms.Button btnTimSach;
        private System.Windows.Forms.DataGridView gridSach;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaSach;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenSach;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenLoai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenTacGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenNXB;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenLinhVuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenNgonNgu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoTrang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaBan;
        private System.Windows.Forms.Label lblMaSach;
        private System.Windows.Forms.TextBox txtMaSach;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Button btnTimNhap;
        private System.Windows.Forms.DataGridView gridNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoHDN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenNCC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTongTien;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabSach = new System.Windows.Forms.TabPage();
            this.layoutSach = new System.Windows.Forms.TableLayoutPanel();
            this.filtersSach = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTenSach = new System.Windows.Forms.Label();
            this.txtTenSach = new System.Windows.Forms.TextBox();
            this.lblLoai = new System.Windows.Forms.Label();
            this.cboLoai = new System.Windows.Forms.ComboBox();
            this.lblNXB = new System.Windows.Forms.Label();
            this.cboNXB = new System.Windows.Forms.ComboBox();
            this.btnTimSach = new System.Windows.Forms.Button();
            this.gridSach = new System.Windows.Forms.DataGridView();
            this.MaSach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenSach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenLoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenTG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenNXB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenLinhVuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenNgonNgu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoTrang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DonGiaNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DonGiaBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabNhap = new System.Windows.Forms.TabPage();
            this.layoutNhap = new System.Windows.Forms.TableLayoutPanel();
            this.filtersNhap = new System.Windows.Forms.FlowLayoutPanel();
            this.lblMaSach = new System.Windows.Forms.Label();
            this.txtMaSach = new System.Windows.Forms.TextBox();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.btnTimNhap = new System.Windows.Forms.Button();
            this.gridNhap = new System.Windows.Forms.DataGridView();
            this.SoHDN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenNCC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TongTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabs.SuspendLayout();
            this.tabSach.SuspendLayout();
            this.layoutSach.SuspendLayout();
            this.filtersSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSach)).BeginInit();
            this.tabNhap.SuspendLayout();
            this.layoutNhap.SuspendLayout();
            this.filtersNhap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridNhap)).BeginInit();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabSach);
            this.tabs.Controls.Add(this.tabNhap);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(1180, 730);
            this.tabs.TabIndex = 0;
            // 
            // tabSach
            // 
            this.tabSach.Controls.Add(this.layoutSach);
            this.tabSach.Location = new System.Drawing.Point(4, 29);
            this.tabSach.Name = "tabSach";
            this.tabSach.Size = new System.Drawing.Size(1172, 697);
            this.tabSach.TabIndex = 0;
            this.tabSach.Text = "Tìm sách";
            // 
            // layoutSach
            // 
            this.layoutSach.ColumnCount = 1;
            this.layoutSach.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutSach.Controls.Add(this.filtersSach, 0, 0);
            this.layoutSach.Controls.Add(this.gridSach, 0, 1);
            this.layoutSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutSach.Location = new System.Drawing.Point(0, 0);
            this.layoutSach.Name = "layoutSach";
            this.layoutSach.Padding = new System.Windows.Forms.Padding(12);
            this.layoutSach.RowCount = 2;
            this.layoutSach.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.layoutSach.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutSach.Size = new System.Drawing.Size(1172, 697);
            this.layoutSach.TabIndex = 0;
            // 
            // filtersSach
            // 
            this.filtersSach.Controls.Add(this.lblTenSach);
            this.filtersSach.Controls.Add(this.txtTenSach);
            this.filtersSach.Controls.Add(this.lblLoai);
            this.filtersSach.Controls.Add(this.cboLoai);
            this.filtersSach.Controls.Add(this.lblNXB);
            this.filtersSach.Controls.Add(this.cboNXB);
            this.filtersSach.Controls.Add(this.btnTimSach);
            this.filtersSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filtersSach.Location = new System.Drawing.Point(15, 15);
            this.filtersSach.Name = "filtersSach";
            this.filtersSach.Size = new System.Drawing.Size(1142, 48);
            this.filtersSach.TabIndex = 0;
            // 
            // lblTenSach
            // 
            this.lblTenSach.AutoSize = true;
            this.lblTenSach.Location = new System.Drawing.Point(6, 10);
            this.lblTenSach.Margin = new System.Windows.Forms.Padding(6, 10, 3, 0);
            this.lblTenSach.Name = "lblTenSach";
            this.lblTenSach.Size = new System.Drawing.Size(65, 20);
            this.lblTenSach.TabIndex = 0;
            this.lblTenSach.Text = "Tên sách";
            // 
            // txtTenSach
            // 
            this.txtTenSach.Location = new System.Drawing.Point(77, 3);
            this.txtTenSach.Name = "txtTenSach";
            this.txtTenSach.Size = new System.Drawing.Size(230, 27);
            this.txtTenSach.TabIndex = 1;
            // 
            // lblLoai
            // 
            this.lblLoai.AutoSize = true;
            this.lblLoai.Location = new System.Drawing.Point(316, 10);
            this.lblLoai.Margin = new System.Windows.Forms.Padding(6, 10, 3, 0);
            this.lblLoai.Name = "lblLoai";
            this.lblLoai.Size = new System.Drawing.Size(70, 20);
            this.lblLoai.TabIndex = 2;
            this.lblLoai.Text = "Loại sách";
            // 
            // cboLoai
            // 
            this.cboLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoai.Location = new System.Drawing.Point(392, 3);
            this.cboLoai.Name = "cboLoai";
            this.cboLoai.Size = new System.Drawing.Size(190, 28);
            this.cboLoai.TabIndex = 3;
            // 
            // lblNXB
            // 
            this.lblNXB.AutoSize = true;
            this.lblNXB.Location = new System.Drawing.Point(591, 10);
            this.lblNXB.Margin = new System.Windows.Forms.Padding(6, 10, 3, 0);
            this.lblNXB.Name = "lblNXB";
            this.lblNXB.Size = new System.Drawing.Size(97, 20);
            this.lblNXB.TabIndex = 4;
            this.lblNXB.Text = "Nhà xuất bản";
            // 
            // cboNXB
            // 
            this.cboNXB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNXB.Location = new System.Drawing.Point(694, 3);
            this.cboNXB.Name = "cboNXB";
            this.cboNXB.Size = new System.Drawing.Size(210, 28);
            this.cboNXB.TabIndex = 5;
            // 
            // btnTimSach
            // 
            this.btnTimSach.AutoSize = true;
            this.btnTimSach.Location = new System.Drawing.Point(910, 3);
            this.btnTimSach.Name = "btnTimSach";
            this.btnTimSach.Size = new System.Drawing.Size(77, 32);
            this.btnTimSach.TabIndex = 6;
            this.btnTimSach.Text = "Tìm sách";
            // 
            // gridSach
            // 
            this.gridSach.AllowUserToAddRows = false;
            this.gridSach.AllowUserToDeleteRows = false;
            this.gridSach.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.gridSach.BackgroundColor = System.Drawing.Color.White;
            this.gridSach.ColumnHeadersHeight = 29;
            this.gridSach.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaSach,
            this.TenSach,
            this.TenLoai,
            this.TenTG,
            this.TenNXB,
            this.TenLinhVuc,
            this.TenNgonNgu,
            this.SoTrang,
            this.SoLuong,
            this.DonGiaNhap,
            this.DonGiaBan});
            this.gridSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSach.Location = new System.Drawing.Point(15, 69);
            this.gridSach.Name = "gridSach";
            this.gridSach.ReadOnly = true;
            this.gridSach.RowHeadersVisible = false;
            this.gridSach.RowHeadersWidth = 51;
            this.gridSach.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridSach.Size = new System.Drawing.Size(1142, 613);
            this.gridSach.TabIndex = 1;
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
            // tabNhap
            // 
            this.tabNhap.Controls.Add(this.layoutNhap);
            this.tabNhap.Location = new System.Drawing.Point(4, 29);
            this.tabNhap.Name = "tabNhap";
            this.tabNhap.Size = new System.Drawing.Size(192, 67);
            this.tabNhap.TabIndex = 1;
            this.tabNhap.Text = "Tìm hóa đơn nhập";
            // 
            // layoutNhap
            // 
            this.layoutNhap.ColumnCount = 1;
            this.layoutNhap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutNhap.Controls.Add(this.filtersNhap, 0, 0);
            this.layoutNhap.Controls.Add(this.gridNhap, 0, 1);
            this.layoutNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutNhap.Location = new System.Drawing.Point(0, 0);
            this.layoutNhap.Name = "layoutNhap";
            this.layoutNhap.Padding = new System.Windows.Forms.Padding(12);
            this.layoutNhap.RowCount = 2;
            this.layoutNhap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.layoutNhap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutNhap.Size = new System.Drawing.Size(192, 67);
            this.layoutNhap.TabIndex = 0;
            // 
            // filtersNhap
            // 
            this.filtersNhap.Controls.Add(this.lblMaSach);
            this.filtersNhap.Controls.Add(this.txtMaSach);
            this.filtersNhap.Controls.Add(this.lblTuNgay);
            this.filtersNhap.Controls.Add(this.dtpTuNgay);
            this.filtersNhap.Controls.Add(this.lblDenNgay);
            this.filtersNhap.Controls.Add(this.dtpDenNgay);
            this.filtersNhap.Controls.Add(this.btnTimNhap);
            this.filtersNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filtersNhap.Location = new System.Drawing.Point(15, 15);
            this.filtersNhap.Name = "filtersNhap";
            this.filtersNhap.Size = new System.Drawing.Size(162, 52);
            this.filtersNhap.TabIndex = 0;
            // 
            // lblMaSach
            // 
            this.lblMaSach.AutoSize = true;
            this.lblMaSach.Location = new System.Drawing.Point(6, 10);
            this.lblMaSach.Margin = new System.Windows.Forms.Padding(6, 10, 3, 0);
            this.lblMaSach.Name = "lblMaSach";
            this.lblMaSach.Size = new System.Drawing.Size(63, 20);
            this.lblMaSach.TabIndex = 0;
            this.lblMaSach.Text = "Mã sách";
            // 
            // txtMaSach
            // 
            this.txtMaSach.Location = new System.Drawing.Point(3, 33);
            this.txtMaSach.MaxLength = 10;
            this.txtMaSach.Name = "txtMaSach";
            this.txtMaSach.Size = new System.Drawing.Size(100, 27);
            this.txtMaSach.TabIndex = 1;
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Location = new System.Drawing.Point(6, 73);
            this.lblTuNgay.Margin = new System.Windows.Forms.Padding(6, 10, 3, 0);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(62, 20);
            this.lblTuNgay.TabIndex = 2;
            this.lblTuNgay.Text = "Từ ngày";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Checked = false;
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(3, 96);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.ShowCheckBox = true;
            this.dtpTuNgay.Size = new System.Drawing.Size(200, 27);
            this.dtpTuNgay.TabIndex = 3;
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Location = new System.Drawing.Point(6, 136);
            this.lblDenNgay.Margin = new System.Windows.Forms.Padding(6, 10, 3, 0);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(72, 20);
            this.lblDenNgay.TabIndex = 4;
            this.lblDenNgay.Text = "Đến ngày";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Checked = false;
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(3, 159);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.ShowCheckBox = true;
            this.dtpDenNgay.Size = new System.Drawing.Size(200, 27);
            this.dtpDenNgay.TabIndex = 5;
            // 
            // btnTimNhap
            // 
            this.btnTimNhap.AutoSize = true;
            this.btnTimNhap.Location = new System.Drawing.Point(3, 192);
            this.btnTimNhap.Name = "btnTimNhap";
            this.btnTimNhap.Size = new System.Drawing.Size(140, 32);
            this.btnTimNhap.TabIndex = 6;
            this.btnTimNhap.Text = "Tìm hóa đơn nhập";
            // 
            // gridNhap
            // 
            this.gridNhap.AllowUserToAddRows = false;
            this.gridNhap.AllowUserToDeleteRows = false;
            this.gridNhap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridNhap.BackgroundColor = System.Drawing.Color.White;
            this.gridNhap.ColumnHeadersHeight = 29;
            this.gridNhap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SoHDN,
            this.NgayNhap,
            this.TenNCC,
            this.TenNV,
            this.TongTien});
            this.gridNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridNhap.Location = new System.Drawing.Point(15, 73);
            this.gridNhap.Name = "gridNhap";
            this.gridNhap.ReadOnly = true;
            this.gridNhap.RowHeadersVisible = false;
            this.gridNhap.RowHeadersWidth = 51;
            this.gridNhap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridNhap.Size = new System.Drawing.Size(162, 1);
            this.gridNhap.TabIndex = 1;
            // 
            // SoHDN
            // 
            this.SoHDN.DataPropertyName = "SoHDN";
            this.SoHDN.HeaderText = "Số hóa đơn nhập";
            this.SoHDN.MinimumWidth = 6;
            this.SoHDN.Name = "SoHDN";
            this.SoHDN.ReadOnly = true;
            // 
            // NgayNhap
            // 
            this.NgayNhap.DataPropertyName = "NgayNhap";
            this.NgayNhap.HeaderText = "Ngày nhập";
            this.NgayNhap.MinimumWidth = 6;
            this.NgayNhap.Name = "NgayNhap";
            this.NgayNhap.ReadOnly = true;
            // 
            // TenNCC
            // 
            this.TenNCC.DataPropertyName = "TenNCC";
            this.TenNCC.HeaderText = "Nhà cung cấp";
            this.TenNCC.MinimumWidth = 6;
            this.TenNCC.Name = "TenNCC";
            this.TenNCC.ReadOnly = true;
            // 
            // TenNV
            // 
            this.TenNV.DataPropertyName = "TenNV";
            this.TenNV.HeaderText = "Nhân viên";
            this.TenNV.MinimumWidth = 6;
            this.TenNV.Name = "TenNV";
            this.TenNV.ReadOnly = true;
            // 
            // TongTien
            // 
            this.TongTien.DataPropertyName = "TongTien";
            this.TongTien.HeaderText = "Tổng tiền";
            this.TongTien.MinimumWidth = 6;
            this.TongTien.Name = "TongTien";
            this.TongTien.ReadOnly = true;
            // 
            // frmTimKiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 730);
            this.Controls.Add(this.tabs);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(950, 600);
            this.Name = "frmTimKiem";
            this.Text = "Tìm kiếm";
            this.tabs.ResumeLayout(false);
            this.tabSach.ResumeLayout(false);
            this.layoutSach.ResumeLayout(false);
            this.filtersSach.ResumeLayout(false);
            this.filtersSach.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSach)).EndInit();
            this.tabNhap.ResumeLayout(false);
            this.layoutNhap.ResumeLayout(false);
            this.filtersNhap.ResumeLayout(false);
            this.filtersNhap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridNhap)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.DataGridViewTextBoxColumn MaSach;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenSach;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenLoai;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenTG;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenNXB;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenLinhVuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenNgonNgu;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoTrang;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn DonGiaNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn DonGiaBan;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoHDN;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenNCC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn TongTien;
    }
}
