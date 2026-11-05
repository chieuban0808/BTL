namespace QuanLyCuaHangSach
{
    partial class frmLichSuBanHang
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel layoutRoot;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.SplitContainer splitHistory;
        private System.Windows.Forms.DataGridView gridHoaDon;
        private System.Windows.Forms.TableLayoutPanel detailLayout;
        private System.Windows.Forms.Label lblInvoiceDetail;
        private System.Windows.Forms.DataGridView gridChiTiet;
        private System.Windows.Forms.FlowLayoutPanel actions;
        private System.Windows.Forms.Button btnTaiLai;
        private System.Windows.Forms.Button btnXoaChiTiet;
        private System.Windows.Forms.Button btnXuatExcel;
        protected override void Dispose(bool disposing)
        { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.layoutRoot = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.splitHistory = new System.Windows.Forms.SplitContainer();
            this.gridHoaDon = new System.Windows.Forms.DataGridView();
            this.detailLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblInvoiceDetail = new System.Windows.Forms.Label();
            this.gridChiTiet = new System.Windows.Forms.DataGridView();
            this.actions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnTaiLai = new System.Windows.Forms.Button();
            this.btnXoaChiTiet = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.layoutRoot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitHistory)).BeginInit();
            this.splitHistory.Panel1.SuspendLayout(); this.splitHistory.Panel2.SuspendLayout(); this.splitHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridHoaDon)).BeginInit();
            this.detailLayout.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)(this.gridChiTiet)).BeginInit();
            this.actions.SuspendLayout(); this.SuspendLayout();
            this.layoutRoot.ColumnCount = 1;
            this.layoutRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutRoot.Controls.Add(this.lblTitle, 0, 0); this.layoutRoot.Controls.Add(this.splitHistory, 0, 1);
            this.layoutRoot.Dock = System.Windows.Forms.DockStyle.Fill; this.layoutRoot.Padding = new System.Windows.Forms.Padding(16);
            this.layoutRoot.RowCount = 2; this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F)); this.layoutRoot.Name = "layoutRoot";
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill; this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(35, 64, 102); this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "LỊCH SỬ BÁN HÀNG"; this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.splitHistory.Dock = System.Windows.Forms.DockStyle.Fill; this.splitHistory.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitHistory.Name = "splitHistory"; this.splitHistory.Size = new System.Drawing.Size(1168, 660); this.splitHistory.SplitterDistance = 520;
            this.splitHistory.Panel1.Controls.Add(this.gridHoaDon); this.splitHistory.Panel2.Controls.Add(this.detailLayout);
            this.gridHoaDon.AllowUserToAddRows = false; this.gridHoaDon.AllowUserToDeleteRows = false;
            this.gridHoaDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridHoaDon.BackgroundColor = System.Drawing.Color.White; this.gridHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridHoaDon.MultiSelect = false; this.gridHoaDon.Name = "gridHoaDon"; this.gridHoaDon.ReadOnly = true;
            this.gridHoaDon.RowHeadersVisible = false; this.gridHoaDon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.detailLayout.ColumnCount = 1; this.detailLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.detailLayout.Controls.Add(this.lblInvoiceDetail, 0, 0); this.detailLayout.Controls.Add(this.gridChiTiet, 0, 1); this.detailLayout.Controls.Add(this.actions, 0, 2);
            this.detailLayout.Dock = System.Windows.Forms.DockStyle.Fill; this.detailLayout.Name = "detailLayout"; this.detailLayout.RowCount = 3;
            this.detailLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.detailLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.detailLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.lblInvoiceDetail.Dock = System.Windows.Forms.DockStyle.Fill; this.lblInvoiceDetail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblInvoiceDetail.Name = "lblInvoiceDetail"; this.lblInvoiceDetail.Text = "Chọn hóa đơn để xem chi tiết.";
            this.lblInvoiceDetail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.gridChiTiet.AllowUserToAddRows = false; this.gridChiTiet.AllowUserToDeleteRows = false;
            this.gridChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridChiTiet.BackgroundColor = System.Drawing.Color.White; this.gridChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridChiTiet.MultiSelect = false; this.gridChiTiet.Name = "gridChiTiet"; this.gridChiTiet.ReadOnly = true;
            this.gridChiTiet.RowHeadersVisible = false; this.gridChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.actions.Controls.Add(this.btnTaiLai); this.actions.Controls.Add(this.btnXoaChiTiet); this.actions.Controls.Add(this.btnXuatExcel);
            this.actions.Dock = System.Windows.Forms.DockStyle.Fill; this.actions.Name = "actions";
            this.btnTaiLai.AutoSize = true; this.btnTaiLai.Name = "btnTaiLai"; this.btnTaiLai.Text = "Tải lại";
            this.btnXoaChiTiet.AutoSize = true; this.btnXoaChiTiet.Name = "btnXoaChiTiet"; this.btnXoaChiTiet.Text = "Xóa chi tiết đang chọn";
            this.btnXuatExcel.AutoSize = true; this.btnXuatExcel.Name = "btnXuatExcel"; this.btnXuatExcel.Text = "Xuất Excel";
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 740); this.Controls.Add(this.layoutRoot); this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmLichSuBanHang"; this.Text = "Lịch sử bán hàng";
            this.layoutRoot.ResumeLayout(false); this.splitHistory.Panel1.ResumeLayout(false); this.splitHistory.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitHistory)).EndInit(); this.splitHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridHoaDon)).EndInit(); this.detailLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridChiTiet)).EndInit(); this.actions.ResumeLayout(false); this.actions.PerformLayout(); this.ResumeLayout(false);
        }
    }
}
