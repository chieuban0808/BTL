namespace QuanLyCuaHangSach
{
    partial class frmDangNhap
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelCard;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblTenDangNhap;
        private System.Windows.Forms.TextBox txtTenDangNhap;
        private System.Windows.Forms.Label lblMatKhau;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.Button btnThoat;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelCard = new System.Windows.Forms.Panel();
            this.lblBrand = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTenDangNhap = new System.Windows.Forms.Label();
            this.txtTenDangNhap = new System.Windows.Forms.TextBox();
            this.lblMatKhau = new System.Windows.Forms.Label();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.btnDangNhap = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.panelCard.SuspendLayout();
            this.SuspendLayout();
            this.panelCard.BackColor = System.Drawing.Color.White;
            this.panelCard.Controls.Add(this.lblBrand);
            this.panelCard.Controls.Add(this.lblSubtitle);
            this.panelCard.Controls.Add(this.lblTenDangNhap);
            this.panelCard.Controls.Add(this.txtTenDangNhap);
            this.panelCard.Controls.Add(this.lblMatKhau);
            this.panelCard.Controls.Add(this.txtMatKhau);
            this.panelCard.Controls.Add(this.btnDangNhap);
            this.panelCard.Controls.Add(this.btnThoat);
            this.panelCard.Location = new System.Drawing.Point(74, 38);
            this.panelCard.Name = "panelCard";
            this.panelCard.Size = new System.Drawing.Size(420, 390);
            this.lblBrand.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblBrand.ForeColor = System.Drawing.Color.FromArgb(35, 64, 102);
            this.lblBrand.Location = new System.Drawing.Point(34, 32);
            this.lblBrand.Size = new System.Drawing.Size(350, 42);
            this.lblBrand.Text = "CỬA HÀNG SÁCH";
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblSubtitle.Location = new System.Drawing.Point(38, 78);
            this.lblSubtitle.Size = new System.Drawing.Size(340, 26);
            this.lblSubtitle.Text = "Đăng nhập bằng tài khoản nhân viên";
            this.lblTenDangNhap.Location = new System.Drawing.Point(38, 130);
            this.lblTenDangNhap.Size = new System.Drawing.Size(340, 22);
            this.lblTenDangNhap.Text = "Tên đăng nhập";
            this.txtTenDangNhap.Location = new System.Drawing.Point(38, 155);
            this.txtTenDangNhap.Size = new System.Drawing.Size(340, 27);
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.lblMatKhau.Location = new System.Drawing.Point(38, 196);
            this.lblMatKhau.Size = new System.Drawing.Size(340, 22);
            this.lblMatKhau.Text = "Mật khẩu";
            this.txtMatKhau.Location = new System.Drawing.Point(38, 221);
            this.txtMatKhau.Size = new System.Drawing.Size(340, 27);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.UseSystemPasswordChar = true;
            this.btnDangNhap.BackColor = System.Drawing.Color.FromArgb(35, 64, 102);
            this.btnDangNhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangNhap.ForeColor = System.Drawing.Color.White;
            this.btnDangNhap.Location = new System.Drawing.Point(38, 278);
            this.btnDangNhap.Size = new System.Drawing.Size(210, 42);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Text = "Đăng nhập";
            this.btnDangNhap.UseVisualStyleBackColor = false;
            this.btnThoat.Location = new System.Drawing.Point(264, 278);
            this.btnThoat.Size = new System.Drawing.Size(114, 42);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Text = "Thoát";
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.ClientSize = new System.Drawing.Size(568, 466);
            this.Controls.Add(this.panelCard);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";
            this.panelCard.ResumeLayout(false);
            this.panelCard.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
