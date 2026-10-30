using System;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnSach_Click(object sender, EventArgs e) { OpenForm(new frmSach()); }
        private void btnNhapHang_Click(object sender, EventArgs e) { OpenForm(new frmNhapHang()); }
        private void btnMatSach_Click(object sender, EventArgs e) { OpenForm(new frmMatSach()); }
        private void btnLoaiSach_Click(object sender, EventArgs e) { OpenForm(new frmLoaiSach()); }
        private void btnTacGia_Click(object sender, EventArgs e) { OpenForm(new frmTacGia()); }
        private void btnNXB_Click(object sender, EventArgs e) { OpenForm(new frmNXB()); }

        private void OpenForm(Form form)
        {
            using (form) form.ShowDialog(this);
        }
    }
}
