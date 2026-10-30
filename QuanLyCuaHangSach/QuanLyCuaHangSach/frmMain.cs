using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            Text = "Quản lý cửa hàng sách";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(760, 430);

            var title = new Label
            {
                Text = "QUẢN LÝ CỬA HÀNG SÁCH",
                Dock = DockStyle.Top,
                Height = 80,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 64, 102)
            };
            var menu = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(40),
                WrapContents = true,
                AutoScroll = true
            };

            menu.Controls.Add(CreateMenuButton("Sách và kho sách", delegate { OpenForm(new frmSach()); }));
            menu.Controls.Add(CreateMenuButton("Nhập hàng", delegate { OpenForm(new frmNhapHang()); }));
            menu.Controls.Add(CreateMenuButton("Ghi nhận sách mất", delegate { OpenForm(new frmMatSach()); }));
            menu.Controls.Add(CreateMenuButton("Loại sách", delegate { OpenForm(new frmLoaiSach()); }));
            menu.Controls.Add(CreateMenuButton("Tác giả", delegate { OpenForm(new frmTacGia()); }));
            menu.Controls.Add(CreateMenuButton("Nhà xuất bản", delegate { OpenForm(new frmNXB()); }));
            Controls.Add(menu);
            Controls.Add(title);
        }

        private static Button CreateMenuButton(string text, EventHandler click)
        {
            var button = new Button
            {
                Text = text,
                Width = 290,
                Height = 110,
                Margin = new Padding(15),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(235, 242, 250),
                FlatStyle = FlatStyle.Flat
            };
            button.Click += click;
            return button;
        }

        private void OpenForm(Form form)
        {
            using (form) form.ShowDialog(this);
        }
    }
}
