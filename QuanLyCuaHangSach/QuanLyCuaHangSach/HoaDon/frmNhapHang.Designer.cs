namespace QuanLyCuaHangSach
{
    partial class frmNhapHang
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel layoutRoot;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.TableLayoutPanel layoutHeader;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.ComboBox cboNhanVien;
        private System.Windows.Forms.Label lblNhaCungCap;
        private System.Windows.Forms.ComboBox cboNhaCungCap;
        private System.Windows.Forms.Label lblNgayNhap;
        private System.Windows.Forms.DateTimePicker dtpNgayNhap;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.FlowLayoutPanel layoutFooter;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnThemNCC;
        private System.Windows.Forms.Button btnThemSach;
        private System.Windows.Forms.Button btnLichSu;

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
            this.layoutRoot = new System.Windows.Forms.TableLayoutPanel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.layoutHeader = new System.Windows.Forms.TableLayoutPanel();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.cboNhanVien = new System.Windows.Forms.ComboBox();
            this.lblNhaCungCap = new System.Windows.Forms.Label();
            this.cboNhaCungCap = new System.Windows.Forms.ComboBox();
            this.btnThemNCC = new System.Windows.Forms.Button();
            this.lblNgayNhap = new System.Windows.Forms.Label();
            this.dtpNgayNhap = new System.Windows.Forms.DateTimePicker();
            this.grid = new System.Windows.Forms.DataGridView();
            this.MaSach = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.SLNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DonGiaNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KhuyenMai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.layoutFooter = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnThemSach = new System.Windows.Forms.Button();
            this.btnLichSu = new System.Windows.Forms.Button();
            this.layoutRoot.SuspendLayout();
            this.layoutHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.layoutFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutRoot
            // 
            this.layoutRoot.ColumnCount = 1;
            this.layoutRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutRoot.Controls.Add(this.lblTieuDe, 0, 0);
            this.layoutRoot.Controls.Add(this.layoutHeader, 0, 1);
            this.layoutRoot.Controls.Add(this.grid, 0, 2);
            this.layoutRoot.Controls.Add(this.layoutFooter, 0, 3);
            this.layoutRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutRoot.Location = new System.Drawing.Point(0, 0);
            this.layoutRoot.Name = "layoutRoot";
            this.layoutRoot.Padding = new System.Windows.Forms.Padding(14);
            this.layoutRoot.RowCount = 4;
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.layoutRoot.Size = new System.Drawing.Size(1120, 700);
            this.layoutRoot.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(64)))), ((int)(((byte)(102)))));
            this.lblTieuDe.Location = new System.Drawing.Point(17, 14);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(1086, 44);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "HÓA ĐƠN NHẬP HÀNG";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // layoutHeader
            // 
            this.layoutHeader.ColumnCount = 5;
            this.layoutHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.layoutHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.layoutHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.layoutHeader.Controls.Add(this.lblNhanVien, 0, 0);
            this.layoutHeader.Controls.Add(this.cboNhanVien, 1, 0);
            this.layoutHeader.Controls.Add(this.lblNhaCungCap, 2, 0);
            this.layoutHeader.Controls.Add(this.cboNhaCungCap, 3, 0);
            this.layoutHeader.Controls.Add(this.btnThemNCC, 4, 0);
            this.layoutHeader.Controls.Add(this.lblNgayNhap, 0, 1);
            this.layoutHeader.Controls.Add(this.dtpNgayNhap, 1, 1);
            this.layoutHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutHeader.Location = new System.Drawing.Point(17, 61);
            this.layoutHeader.Name = "layoutHeader";
            this.layoutHeader.Padding = new System.Windows.Forms.Padding(0, 4, 0, 8);
            this.layoutHeader.RowCount = 2;
            this.layoutHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutHeader.Size = new System.Drawing.Size(1086, 78);
            this.layoutHeader.TabIndex = 1;
            // 
            // lblNhanVien
            // 
            this.lblNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNhanVien.Location = new System.Drawing.Point(3, 4);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Size = new System.Drawing.Size(94, 33);
            this.lblNhanVien.TabIndex = 0;
            this.lblNhanVien.Text = "Nhân viên *";
            this.lblNhanVien.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboNhanVien
            // 
            this.cboNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVien.Location = new System.Drawing.Point(103, 7);
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.Size = new System.Drawing.Size(432, 28);
            this.cboNhanVien.TabIndex = 1;
            // 
            // lblNhaCungCap
            // 
            this.lblNhaCungCap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNhaCungCap.Location = new System.Drawing.Point(541, 4);
            this.lblNhaCungCap.Name = "lblNhaCungCap";
            this.lblNhaCungCap.Size = new System.Drawing.Size(104, 33);
            this.lblNhaCungCap.TabIndex = 2;
            this.lblNhaCungCap.Text = "Nhà cung cấp *";
            this.lblNhaCungCap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboNhaCungCap
            // 
            this.cboNhaCungCap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboNhaCungCap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhaCungCap.Location = new System.Drawing.Point(651, 7);
            this.cboNhaCungCap.Name = "cboNhaCungCap";
            this.cboNhaCungCap.Size = new System.Drawing.Size(432, 28);
            this.cboNhaCungCap.TabIndex = 3;
            //
            // btnThemNCC
            //
            this.btnThemNCC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnThemNCC.Location = new System.Drawing.Point(1083, 7);
            this.btnThemNCC.Name = "btnThemNCC";
            this.btnThemNCC.Size = new System.Drawing.Size(1, 27);
            this.btnThemNCC.TabIndex = 6;
            this.btnThemNCC.Text = "+";
            this.btnThemNCC.UseVisualStyleBackColor = true;
            // 
            // lblNgayNhap
            // 
            this.lblNgayNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNgayNhap.Location = new System.Drawing.Point(3, 37);
            this.lblNgayNhap.Name = "lblNgayNhap";
            this.lblNgayNhap.Size = new System.Drawing.Size(94, 33);
            this.lblNgayNhap.TabIndex = 4;
            this.lblNgayNhap.Text = "Ngày nhập";
            this.lblNgayNhap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpNgayNhap
            // 
            this.dtpNgayNhap.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtpNgayNhap.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayNhap.Location = new System.Drawing.Point(103, 40);
            this.dtpNgayNhap.Name = "dtpNgayNhap";
            this.dtpNgayNhap.Size = new System.Drawing.Size(200, 27);
            this.dtpNgayNhap.TabIndex = 5;
            // 
            // grid
            // 
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.BackgroundColor = System.Drawing.Color.White;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaSach,
            this.SLNhap,
            this.DonGiaNhap,
            this.KhuyenMai,
            this.ThanhTien});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(17, 145);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.RowHeadersVisible = false;
            this.grid.RowHeadersWidth = 51;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(1086, 486);
            this.grid.TabIndex = 2;
            // 
            // MaSach
            // 
            this.MaSach.FillWeight = 42F;
            this.MaSach.HeaderText = "Sách";
            this.MaSach.MinimumWidth = 6;
            this.MaSach.Name = "MaSach";
            // 
            // SLNhap
            // 
            this.SLNhap.FillWeight = 12F;
            this.SLNhap.HeaderText = "Số lượng";
            this.SLNhap.MinimumWidth = 6;
            this.SLNhap.Name = "SLNhap";
            // 
            // DonGiaNhap
            // 
            dataGridViewCellStyle1.Format = "N0";
            this.DonGiaNhap.DefaultCellStyle = dataGridViewCellStyle1;
            this.DonGiaNhap.FillWeight = 16F;
            this.DonGiaNhap.HeaderText = "Giá nhập";
            this.DonGiaNhap.MinimumWidth = 6;
            this.DonGiaNhap.Name = "DonGiaNhap";
            // 
            // KhuyenMai
            // 
            dataGridViewCellStyle2.Format = "N2";
            this.KhuyenMai.DefaultCellStyle = dataGridViewCellStyle2;
            this.KhuyenMai.FillWeight = 14F;
            this.KhuyenMai.HeaderText = "Chiết khấu (%)";
            this.KhuyenMai.MinimumWidth = 6;
            this.KhuyenMai.Name = "KhuyenMai";
            // 
            // ThanhTien
            // 
            dataGridViewCellStyle3.Format = "N0";
            this.ThanhTien.DefaultCellStyle = dataGridViewCellStyle3;
            this.ThanhTien.FillWeight = 18F;
            this.ThanhTien.HeaderText = "Thành tiền";
            this.ThanhTien.MinimumWidth = 6;
            this.ThanhTien.Name = "ThanhTien";
            this.ThanhTien.ReadOnly = true;
            // 
            // layoutFooter
            // 
            this.layoutFooter.Controls.Add(this.lblTongTien);
            this.layoutFooter.Controls.Add(this.btnLuu);
            this.layoutFooter.Controls.Add(this.btnLamMoi);
            this.layoutFooter.Controls.Add(this.btnThemSach);
            this.layoutFooter.Controls.Add(this.btnLichSu);
            this.layoutFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutFooter.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.layoutFooter.Location = new System.Drawing.Point(17, 637);
            this.layoutFooter.Name = "layoutFooter";
            this.layoutFooter.Size = new System.Drawing.Size(1086, 46);
            this.layoutFooter.TabIndex = 3;
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(64)))), ((int)(((byte)(102)))));
            this.lblTongTien.Location = new System.Drawing.Point(902, 0);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Padding = new System.Windows.Forms.Padding(12, 8, 12, 0);
            this.lblTongTien.Size = new System.Drawing.Size(181, 38);
            this.lblTongTien.TabIndex = 0;
            this.lblTongTien.Text = "Tổng tiền: 0 đ";
            // 
            // btnLuu
            // 
            this.btnLuu.AutoSize = true;
            this.btnLuu.Location = new System.Drawing.Point(757, 3);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(139, 38);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "Lưu hóa đơn nhập";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.AutoSize = true;
            this.btnLamMoi.Location = new System.Drawing.Point(674, 3);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(77, 38);
            this.btnLamMoi.TabIndex = 2;
            this.btnLamMoi.Text = "Làm mới";
            this.btnThemSach.AutoSize = true;
            this.btnThemSach.Location = new System.Drawing.Point(560, 3);
            this.btnThemSach.Name = "btnThemSach";
            this.btnThemSach.Size = new System.Drawing.Size(110, 38);
            this.btnThemSach.TabIndex = 3;
            this.btnThemSach.Text = "+ Sách mới";
            this.btnLichSu.AutoSize = true;
            this.btnLichSu.Location = new System.Drawing.Point(430, 3);
            this.btnLichSu.Name = "btnLichSu";
            this.btnLichSu.Size = new System.Drawing.Size(124, 38);
            this.btnLichSu.TabIndex = 4;
            this.btnLichSu.Text = "Lịch sử nhập";
            // 
            // frmNhapHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 700);
            this.Controls.Add(this.layoutRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frmNhapHang";
            this.Text = "Lập hóa đơn nhập hàng";
            this.layoutRoot.ResumeLayout(false);
            this.layoutHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.layoutFooter.ResumeLayout(false);
            this.layoutFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.DataGridViewComboBoxColumn MaSach;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn DonGiaNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn KhuyenMai;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThanhTien;
    }
}
