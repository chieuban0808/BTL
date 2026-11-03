namespace QuanLyCuaHangSach
{
    partial class frmMatSach
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel layoutRoot;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.TableLayoutPanel layoutFields;
        private System.Windows.Forms.Label lblSach;
        private System.Windows.Forms.ComboBox cboSach;
        private System.Windows.Forms.Label lblNgayMat;
        private System.Windows.Forms.DateTimePicker dtpNgayMat;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.NumericUpDown nudSoLuong;
        private System.Windows.Forms.Label lblLyDo;
        private System.Windows.Forms.TextBox txtLyDo;
        private System.Windows.Forms.FlowLayoutPanel layoutActions;
        private System.Windows.Forms.Button btnGhiNhan;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnTaiLai;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaLanMat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenSach;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayMat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSLMat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLyDo;

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
            this.lblSach = new System.Windows.Forms.Label();
            this.cboSach = new System.Windows.Forms.ComboBox();
            this.lblNgayMat = new System.Windows.Forms.Label();
            this.dtpNgayMat = new System.Windows.Forms.DateTimePicker();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.nudSoLuong = new System.Windows.Forms.NumericUpDown();
            this.lblLyDo = new System.Windows.Forms.Label();
            this.txtLyDo = new System.Windows.Forms.TextBox();
            this.layoutActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGhiNhan = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnTaiLai = new System.Windows.Forms.Button();
            this.grid = new System.Windows.Forms.DataGridView();
            this.layoutRoot.SuspendLayout();
            this.layoutFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).BeginInit();
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
            this.layoutRoot.Padding = new System.Windows.Forms.Padding(14);
            this.layoutRoot.RowCount = 4;
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 146F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutRoot.Size = new System.Drawing.Size(1020, 670);
            this.layoutRoot.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(64)))), ((int)(((byte)(102)))));
            this.lblTieuDe.Location = new System.Drawing.Point(17, 14);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(986, 42);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "GHI NHẬN SÁCH MẤT";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // layoutFields
            // 
            this.layoutFields.ColumnCount = 4;
            this.layoutFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.layoutFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.layoutFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.layoutFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.layoutFields.Controls.Add(this.lblSach, 0, 0);
            this.layoutFields.Controls.Add(this.cboSach, 1, 0);
            this.layoutFields.Controls.Add(this.lblNgayMat, 2, 0);
            this.layoutFields.Controls.Add(this.dtpNgayMat, 3, 0);
            this.layoutFields.Controls.Add(this.lblSoLuong, 0, 1);
            this.layoutFields.Controls.Add(this.nudSoLuong, 1, 1);
            this.layoutFields.Controls.Add(this.lblLyDo, 0, 2);
            this.layoutFields.Controls.Add(this.txtLyDo, 1, 2);
            this.layoutFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutFields.Location = new System.Drawing.Point(17, 59);
            this.layoutFields.Name = "layoutFields";
            this.layoutFields.Padding = new System.Windows.Forms.Padding(0, 6, 0, 8);
            this.layoutFields.RowCount = 3;
            this.layoutFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.layoutFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.layoutFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutFields.Size = new System.Drawing.Size(986, 140);
            this.layoutFields.TabIndex = 1;
            // 
            // lblSach
            // 
            this.lblSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSach.Location = new System.Drawing.Point(3, 6);
            this.lblSach.Name = "lblSach";
            this.lblSach.Size = new System.Drawing.Size(99, 38);
            this.lblSach.TabIndex = 0;
            this.lblSach.Text = "Sách *";
            this.lblSach.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboSach
            // 
            this.cboSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboSach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSach.Location = new System.Drawing.Point(108, 9);
            this.cboSach.Name = "cboSach";
            this.cboSach.Size = new System.Drawing.Size(465, 28);
            this.cboSach.TabIndex = 1;
            // 
            // lblNgayMat
            // 
            this.lblNgayMat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNgayMat.Location = new System.Drawing.Point(579, 6);
            this.lblNgayMat.Name = "lblNgayMat";
            this.lblNgayMat.Size = new System.Drawing.Size(89, 38);
            this.lblNgayMat.TabIndex = 2;
            this.lblNgayMat.Text = "Ngày mất";
            this.lblNgayMat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpNgayMat
            // 
            this.dtpNgayMat.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtpNgayMat.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayMat.Location = new System.Drawing.Point(674, 9);
            this.dtpNgayMat.Name = "dtpNgayMat";
            this.dtpNgayMat.Size = new System.Drawing.Size(200, 27);
            this.dtpNgayMat.TabIndex = 3;
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSoLuong.Location = new System.Drawing.Point(3, 44);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(99, 38);
            this.lblSoLuong.TabIndex = 4;
            this.lblSoLuong.Text = "Số lượng *";
            this.lblSoLuong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudSoLuong
            // 
            this.nudSoLuong.Dock = System.Windows.Forms.DockStyle.Left;
            this.nudSoLuong.Location = new System.Drawing.Point(108, 47);
            this.nudSoLuong.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudSoLuong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSoLuong.Name = "nudSoLuong";
            this.nudSoLuong.Size = new System.Drawing.Size(120, 27);
            this.nudSoLuong.TabIndex = 5;
            this.nudSoLuong.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblLyDo
            // 
            this.lblLyDo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLyDo.Location = new System.Drawing.Point(3, 82);
            this.lblLyDo.Name = "lblLyDo";
            this.lblLyDo.Size = new System.Drawing.Size(99, 50);
            this.lblLyDo.TabIndex = 6;
            this.lblLyDo.Text = "Lý do";
            this.lblLyDo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLyDo
            // 
            this.layoutFields.SetColumnSpan(this.txtLyDo, 3);
            this.txtLyDo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLyDo.Location = new System.Drawing.Point(108, 85);
            this.txtLyDo.MaxLength = 200;
            this.txtLyDo.Multiline = true;
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.Size = new System.Drawing.Size(875, 44);
            this.txtLyDo.TabIndex = 7;
            // 
            // layoutActions
            // 
            this.layoutActions.Controls.Add(this.btnGhiNhan);
            this.layoutActions.Controls.Add(this.btnLamMoi);
            this.layoutActions.Controls.Add(this.btnXoa);
            this.layoutActions.Controls.Add(this.btnTaiLai);
            this.layoutActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutActions.Location = new System.Drawing.Point(17, 205);
            this.layoutActions.Name = "layoutActions";
            this.layoutActions.Size = new System.Drawing.Size(986, 38);
            this.layoutActions.TabIndex = 2;
            // 
            // btnGhiNhan
            // 
            this.btnGhiNhan.AutoSize = true;
            this.btnGhiNhan.Location = new System.Drawing.Point(3, 3);
            this.btnGhiNhan.Name = "btnGhiNhan";
            this.btnGhiNhan.Size = new System.Drawing.Size(107, 34);
            this.btnGhiNhan.TabIndex = 0;
            this.btnGhiNhan.Text = "Ghi nhận mất";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.AutoSize = true;
            this.btnLamMoi.Location = new System.Drawing.Point(116, 3);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(77, 34);
            this.btnLamMoi.TabIndex = 1;
            this.btnLamMoi.Text = "Làm mới";
            // 
            // btnXoa
            // 
            this.btnXoa.AutoSize = true;
            this.btnXoa.Location = new System.Drawing.Point(199, 3);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(173, 34);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa bản ghi đang chọn";
            // 
            // btnTaiLai
            // 
            this.btnTaiLai.AutoSize = true;
            this.btnTaiLai.Location = new System.Drawing.Point(378, 3);
            this.btnTaiLai.Name = "btnTaiLai";
            this.btnTaiLai.Size = new System.Drawing.Size(75, 34);
            this.btnTaiLai.TabIndex = 3;
            this.btnTaiLai.Text = "Tải lại";
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.BackgroundColor = System.Drawing.Color.White;
            this.grid.ColumnHeadersHeight = 29;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(17, 249);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersVisible = false;
            this.grid.RowHeadersWidth = 51;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(986, 404);
            this.grid.TabIndex = 3;
            // 
            // frmMatSach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 670);
            this.Controls.Add(this.layoutRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(850, 560);
            this.Name = "frmMatSach";
            this.Text = "Ghi nhận sách mất";
            this.layoutRoot.ResumeLayout(false);
            this.layoutFields.ResumeLayout(false);
            this.layoutFields.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).EndInit();
            this.layoutActions.ResumeLayout(false);
            this.layoutActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
