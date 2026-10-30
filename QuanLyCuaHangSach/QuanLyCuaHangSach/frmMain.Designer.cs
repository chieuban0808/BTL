namespace QuanLyCuaHangSach
{
    partial class frmMain
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flowMenu;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnSach;
        private System.Windows.Forms.Button btnNhapHang;
        private System.Windows.Forms.Button btnMatSach;
        private System.Windows.Forms.Button btnLoaiSach;
        private System.Windows.Forms.Button btnTacGia;
        private System.Windows.Forms.Button btnNXB;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.flowMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnSach = new System.Windows.Forms.Button();
            this.btnNhapHang = new System.Windows.Forms.Button();
            this.btnMatSach = new System.Windows.Forms.Button();
            this.btnLoaiSach = new System.Windows.Forms.Button();
            this.btnTacGia = new System.Windows.Forms.Button();
            this.btnNXB = new System.Windows.Forms.Button();
            this.flowMenu.SuspendLayout();
            this.SuspendLayout();
            //
            // flowMenu
            //
            this.flowMenu.AutoScroll = true;
            this.flowMenu.Controls.Add(this.btnSach);
            this.flowMenu.Controls.Add(this.btnNhapHang);
            this.flowMenu.Controls.Add(this.btnMatSach);
            this.flowMenu.Controls.Add(this.btnLoaiSach);
            this.flowMenu.Controls.Add(this.btnTacGia);
            this.flowMenu.Controls.Add(this.btnNXB);
            this.flowMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowMenu.Location = new System.Drawing.Point(0, 76);
            this.flowMenu.Name = "flowMenu";
            this.flowMenu.Padding = new System.Windows.Forms.Padding(34, 22, 34, 22);
            this.flowMenu.Size = new System.Drawing.Size(900, 544);
            this.flowMenu.TabIndex = 1;
            //
            // lblTitle
            //
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(35, 64, 102);
            this.lblTitle.Height = 76;
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "QUẢN LÝ CỬA HÀNG SÁCH";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // Menu buttons
            //
            this.btnSach.BackColor = System.Drawing.Color.FromArgb(235, 242, 250);
            this.btnSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSach.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSach.Margin = new System.Windows.Forms.Padding(12);
            this.btnSach.Name = "btnSach";
            this.btnSach.Size = new System.Drawing.Size(300, 112);
            this.btnSach.Text = "Sách và kho sách";
            this.btnSach.UseVisualStyleBackColor = false;
            this.btnSach.Click += new System.EventHandler(this.btnSach_Click);

            this.btnNhapHang.BackColor = System.Drawing.Color.FromArgb(235, 242, 250);
            this.btnNhapHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhapHang.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnNhapHang.Margin = new System.Windows.Forms.Padding(12);
            this.btnNhapHang.Name = "btnNhapHang";
            this.btnNhapHang.Size = new System.Drawing.Size(300, 112);
            this.btnNhapHang.Text = "Nhập hàng";
            this.btnNhapHang.UseVisualStyleBackColor = false;
            this.btnNhapHang.Click += new System.EventHandler(this.btnNhapHang_Click);

            this.btnMatSach.BackColor = System.Drawing.Color.FromArgb(235, 242, 250);
            this.btnMatSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMatSach.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnMatSach.Margin = new System.Windows.Forms.Padding(12);
            this.btnMatSach.Name = "btnMatSach";
            this.btnMatSach.Size = new System.Drawing.Size(300, 112);
            this.btnMatSach.Text = "Ghi nhận sách mất";
            this.btnMatSach.UseVisualStyleBackColor = false;
            this.btnMatSach.Click += new System.EventHandler(this.btnMatSach_Click);

            this.btnLoaiSach.BackColor = System.Drawing.Color.FromArgb(235, 242, 250);
            this.btnLoaiSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoaiSach.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLoaiSach.Margin = new System.Windows.Forms.Padding(12);
            this.btnLoaiSach.Name = "btnLoaiSach";
            this.btnLoaiSach.Size = new System.Drawing.Size(300, 112);
            this.btnLoaiSach.Text = "Loại sách";
            this.btnLoaiSach.UseVisualStyleBackColor = false;
            this.btnLoaiSach.Click += new System.EventHandler(this.btnLoaiSach_Click);

            this.btnTacGia.BackColor = System.Drawing.Color.FromArgb(235, 242, 250);
            this.btnTacGia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTacGia.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnTacGia.Margin = new System.Windows.Forms.Padding(12);
            this.btnTacGia.Name = "btnTacGia";
            this.btnTacGia.Size = new System.Drawing.Size(300, 112);
            this.btnTacGia.Text = "Tác giả";
            this.btnTacGia.UseVisualStyleBackColor = false;
            this.btnTacGia.Click += new System.EventHandler(this.btnTacGia_Click);

            this.btnNXB.BackColor = System.Drawing.Color.FromArgb(235, 242, 250);
            this.btnNXB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNXB.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnNXB.Margin = new System.Windows.Forms.Padding(12);
            this.btnNXB.Name = "btnNXB";
            this.btnNXB.Size = new System.Drawing.Size(300, 112);
            this.btnNXB.Text = "Nhà xuất bản";
            this.btnNXB.UseVisualStyleBackColor = false;
            this.btnNXB.Click += new System.EventHandler(this.btnNXB_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 620);
            this.Controls.Add(this.flowMenu);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(920, 660);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý cửa hàng sách";
            this.flowMenu.ResumeLayout(false);
            this.ResumeLayout(false);
        }

    }
}
