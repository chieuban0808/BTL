namespace QuanLyCuaHangSach
{
    partial class frmBanHang
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabLap;
        private System.Windows.Forms.TabPage tabLichSu;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel layoutHeader;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.ComboBox cboNhanVien;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.ComboBox cboKhachHang;
        private System.Windows.Forms.Label lblNgayBan;
        private System.Windows.Forms.DateTimePicker dtpNgayBan;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewComboBoxColumn colMaSach;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSLBan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGiaBan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKhuyenMai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
        private System.Windows.Forms.FlowLayoutPanel bottomPanel;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.TableLayoutPanel historyLayout;
        private System.Windows.Forms.FlowLayoutPanel historyActions;
        private System.Windows.Forms.ComboBox cboHoaDon;
        private System.Windows.Forms.Button btnTaiLai;
        private System.Windows.Forms.Button btnXoaChiTiet;
        private System.Windows.Forms.DataGridView gridChiTiet;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabLap = new System.Windows.Forms.TabPage();
            this.grid = new System.Windows.Forms.DataGridView();
            this.MaSach = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.SLBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DonGiaBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KhuyenMai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bottomPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.layoutHeader = new System.Windows.Forms.TableLayoutPanel();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.cboNhanVien = new System.Windows.Forms.ComboBox();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.cboKhachHang = new System.Windows.Forms.ComboBox();
            this.lblNgayBan = new System.Windows.Forms.Label();
            this.dtpNgayBan = new System.Windows.Forms.DateTimePicker();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabLichSu = new System.Windows.Forms.TabPage();
            this.historyLayout = new System.Windows.Forms.TableLayoutPanel();
            this.historyActions = new System.Windows.Forms.FlowLayoutPanel();
            this.cboHoaDon = new System.Windows.Forms.ComboBox();
            this.btnTaiLai = new System.Windows.Forms.Button();
            this.btnXoaChiTiet = new System.Windows.Forms.Button();
            this.gridChiTiet = new System.Windows.Forms.DataGridView();
            this.tabs.SuspendLayout();
            this.tabLap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.bottomPanel.SuspendLayout();
            this.layoutHeader.SuspendLayout();
            this.tabLichSu.SuspendLayout();
            this.historyLayout.SuspendLayout();
            this.historyActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabLap);
            this.tabs.Controls.Add(this.tabLichSu);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(1120, 700);
            this.tabs.TabIndex = 0;
            // 
            // tabLap
            // 
            this.tabLap.Controls.Add(this.grid);
            this.tabLap.Controls.Add(this.bottomPanel);
            this.tabLap.Controls.Add(this.layoutHeader);
            this.tabLap.Controls.Add(this.lblTitle);
            this.tabLap.Location = new System.Drawing.Point(4, 29);
            this.tabLap.Name = "tabLap";
            this.tabLap.Padding = new System.Windows.Forms.Padding(14);
            this.tabLap.Size = new System.Drawing.Size(1112, 667);
            this.tabLap.TabIndex = 0;
            this.tabLap.Text = "Lập hóa đơn";
            // 
            // grid
            // 
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.BackgroundColor = System.Drawing.Color.White;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaSach,
            this.SLBan,
            this.DonGiaBan,
            this.KhuyenMai,
            this.ThanhTien});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(14, 138);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.RowHeadersVisible = false;
            this.grid.RowHeadersWidth = 51;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(1084, 467);
            this.grid.TabIndex = 0;
            // 
            // MaSach
            // 
            this.MaSach.DataPropertyName = "MaSach";
            this.MaSach.FillWeight = 220F;
            this.MaSach.HeaderText = "Sách (tồn kho)";
            this.MaSach.MinimumWidth = 6;
            this.MaSach.Name = "MaSach";
            // 
            // SLBan
            // 
            dataGridViewCellStyle1.Format = "N0";
            this.SLBan.DefaultCellStyle = dataGridViewCellStyle1;
            this.SLBan.HeaderText = "Số lượng";
            this.SLBan.MinimumWidth = 6;
            this.SLBan.Name = "SLBan";
            // 
            // DonGiaBan
            // 
            dataGridViewCellStyle2.Format = "N0";
            this.DonGiaBan.DefaultCellStyle = dataGridViewCellStyle2;
            this.DonGiaBan.HeaderText = "Đơn giá bán";
            this.DonGiaBan.MinimumWidth = 6;
            this.DonGiaBan.Name = "DonGiaBan";
            this.DonGiaBan.ReadOnly = true;
            // 
            // KhuyenMai
            // 
            dataGridViewCellStyle3.Format = "N2";
            this.KhuyenMai.DefaultCellStyle = dataGridViewCellStyle3;
            this.KhuyenMai.HeaderText = "Giảm (%)";
            this.KhuyenMai.MinimumWidth = 6;
            this.KhuyenMai.Name = "KhuyenMai";
            // 
            // ThanhTien
            // 
            dataGridViewCellStyle4.Format = "N0";
            this.ThanhTien.DefaultCellStyle = dataGridViewCellStyle4;
            this.ThanhTien.HeaderText = "Thành tiền";
            this.ThanhTien.MinimumWidth = 6;
            this.ThanhTien.Name = "ThanhTien";
            this.ThanhTien.ReadOnly = true;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.lblTongTien);
            this.bottomPanel.Controls.Add(this.btnLuu);
            this.bottomPanel.Controls.Add(this.btnLamMoi);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(14, 605);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(1084, 48);
            this.bottomPanel.TabIndex = 1;
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.Location = new System.Drawing.Point(3, 8);
            this.lblTongTien.Margin = new System.Windows.Forms.Padding(3, 8, 24, 3);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(209, 25);
            this.lblTongTien.TabIndex = 0;
            this.lblTongTien.Text = "Tổng tiền dự kiến: 0 đ";
            // 
            // btnLuu
            // 
            this.btnLuu.AutoSize = true;
            this.btnLuu.Location = new System.Drawing.Point(239, 3);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(102, 30);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "Lưu hóa đơn";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.AutoSize = true;
            this.btnLamMoi.Location = new System.Drawing.Point(347, 3);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(77, 30);
            this.btnLamMoi.TabIndex = 2;
            this.btnLamMoi.Text = "Làm mới";
            // 
            // layoutHeader
            // 
            this.layoutHeader.ColumnCount = 4;
            this.layoutHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.layoutHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.layoutHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutHeader.Controls.Add(this.lblNhanVien, 0, 0);
            this.layoutHeader.Controls.Add(this.cboNhanVien, 1, 0);
            this.layoutHeader.Controls.Add(this.lblKhachHang, 2, 0);
            this.layoutHeader.Controls.Add(this.cboKhachHang, 3, 0);
            this.layoutHeader.Controls.Add(this.lblNgayBan, 0, 1);
            this.layoutHeader.Controls.Add(this.dtpNgayBan, 1, 1);
            this.layoutHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutHeader.Location = new System.Drawing.Point(14, 60);
            this.layoutHeader.Name = "layoutHeader";
            this.layoutHeader.RowCount = 2;
            this.layoutHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutHeader.Size = new System.Drawing.Size(1084, 78);
            this.layoutHeader.TabIndex = 2;
            // 
            // lblNhanVien
            // 
            this.lblNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNhanVien.Location = new System.Drawing.Point(3, 0);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Size = new System.Drawing.Size(94, 39);
            this.lblNhanVien.TabIndex = 0;
            this.lblNhanVien.Text = "Nhân viên *";
            this.lblNhanVien.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboNhanVien
            // 
            this.cboNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVien.Location = new System.Drawing.Point(103, 3);
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.Size = new System.Drawing.Size(433, 28);
            this.cboNhanVien.TabIndex = 1;
            // 
            // lblKhachHang
            // 
            this.lblKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKhachHang.Location = new System.Drawing.Point(542, 0);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(99, 39);
            this.lblKhachHang.TabIndex = 2;
            this.lblKhachHang.Text = "Khách hàng *";
            this.lblKhachHang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboKhachHang
            // 
            this.cboKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboKhachHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhachHang.Location = new System.Drawing.Point(647, 3);
            this.cboKhachHang.Name = "cboKhachHang";
            this.cboKhachHang.Size = new System.Drawing.Size(434, 28);
            this.cboKhachHang.TabIndex = 3;
            // 
            // lblNgayBan
            // 
            this.lblNgayBan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNgayBan.Location = new System.Drawing.Point(3, 39);
            this.lblNgayBan.Name = "lblNgayBan";
            this.lblNgayBan.Size = new System.Drawing.Size(94, 39);
            this.lblNgayBan.TabIndex = 4;
            this.lblNgayBan.Text = "Ngày bán";
            this.lblNgayBan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpNgayBan
            // 
            this.dtpNgayBan.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtpNgayBan.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayBan.Location = new System.Drawing.Point(103, 42);
            this.dtpNgayBan.Name = "dtpNgayBan";
            this.dtpNgayBan.Size = new System.Drawing.Size(200, 27);
            this.dtpNgayBan.TabIndex = 5;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(64)))), ((int)(((byte)(102)))));
            this.lblTitle.Location = new System.Drawing.Point(14, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1084, 46);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "LẬP HÓA ĐƠN BÁN";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabLichSu
            // 
            this.tabLichSu.Controls.Add(this.historyLayout);
            this.tabLichSu.Location = new System.Drawing.Point(4, 29);
            this.tabLichSu.Name = "tabLichSu";
            this.tabLichSu.Padding = new System.Windows.Forms.Padding(14);
            this.tabLichSu.Size = new System.Drawing.Size(192, 67);
            this.tabLichSu.TabIndex = 1;
            this.tabLichSu.Text = "Chi tiết hóa đơn bán";
            // 
            // historyLayout
            // 
            this.historyLayout.ColumnCount = 1;
            this.historyLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.historyLayout.Controls.Add(this.historyActions, 0, 0);
            this.historyLayout.Controls.Add(this.gridChiTiet, 0, 1);
            this.historyLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.historyLayout.Location = new System.Drawing.Point(14, 14);
            this.historyLayout.Name = "historyLayout";
            this.historyLayout.RowCount = 2;
            this.historyLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.historyLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.historyLayout.Size = new System.Drawing.Size(164, 39);
            this.historyLayout.TabIndex = 0;
            // 
            // historyActions
            // 
            this.historyActions.Controls.Add(this.cboHoaDon);
            this.historyActions.Controls.Add(this.btnTaiLai);
            this.historyActions.Controls.Add(this.btnXoaChiTiet);
            this.historyActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.historyActions.Location = new System.Drawing.Point(3, 3);
            this.historyActions.Name = "historyActions";
            this.historyActions.Size = new System.Drawing.Size(158, 42);
            this.historyActions.TabIndex = 0;
            // 
            // cboHoaDon
            // 
            this.cboHoaDon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHoaDon.Location = new System.Drawing.Point(3, 3);
            this.cboHoaDon.Name = "cboHoaDon";
            this.cboHoaDon.Size = new System.Drawing.Size(390, 28);
            this.cboHoaDon.TabIndex = 0;
            // 
            // btnTaiLai
            // 
            this.btnTaiLai.AutoSize = true;
            this.btnTaiLai.Location = new System.Drawing.Point(3, 33);
            this.btnTaiLai.Name = "btnTaiLai";
            this.btnTaiLai.Size = new System.Drawing.Size(75, 30);
            this.btnTaiLai.TabIndex = 1;
            this.btnTaiLai.Text = "Tải lại";
            // 
            // btnXoaChiTiet
            // 
            this.btnXoaChiTiet.AutoSize = true;
            this.btnXoaChiTiet.Location = new System.Drawing.Point(3, 69);
            this.btnXoaChiTiet.Name = "btnXoaChiTiet";
            this.btnXoaChiTiet.Size = new System.Drawing.Size(168, 30);
            this.btnXoaChiTiet.TabIndex = 2;
            this.btnXoaChiTiet.Text = "Xóa chi tiết đang chọn";
            // 
            // gridChiTiet
            // 
            this.gridChiTiet.AllowUserToAddRows = false;
            this.gridChiTiet.AllowUserToDeleteRows = false;
            this.gridChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.gridChiTiet.ColumnHeadersHeight = 29;
            this.gridChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridChiTiet.Location = new System.Drawing.Point(3, 51);
            this.gridChiTiet.MultiSelect = false;
            this.gridChiTiet.Name = "gridChiTiet";
            this.gridChiTiet.ReadOnly = true;
            this.gridChiTiet.RowHeadersVisible = false;
            this.gridChiTiet.RowHeadersWidth = 51;
            this.gridChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridChiTiet.Size = new System.Drawing.Size(158, 1);
            this.gridChiTiet.TabIndex = 1;
            // 
            // frmBanHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 700);
            this.Controls.Add(this.tabs);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frmBanHang";
            this.Text = "Bán hàng";
            this.tabs.ResumeLayout(false);
            this.tabLap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.layoutHeader.ResumeLayout(false);
            this.tabLichSu.ResumeLayout(false);
            this.historyLayout.ResumeLayout(false);
            this.historyActions.ResumeLayout(false);
            this.historyActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridChiTiet)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.DataGridViewComboBoxColumn MaSach;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLBan;
        private System.Windows.Forms.DataGridViewTextBoxColumn DonGiaBan;
        private System.Windows.Forms.DataGridViewTextBoxColumn KhuyenMai;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThanhTien;
    }
}
