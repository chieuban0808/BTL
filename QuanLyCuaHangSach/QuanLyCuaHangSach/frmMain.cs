using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class frmMain : Form
    {
        private readonly List<Form> embeddedForms = new List<Form>();
        private string currentTarget = "home";

        public frmMain()
        {
            InitializeComponent();
            BuildNavigation();
            UpdateSessionLabels();
            ShowDashboard();
            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
                LoadDashboard();
        }

        private AppRole CurrentRole
        {
            get { return AppSession.IsAuthenticated ? AppSession.Role : AppRole.Manager; }
        }

        private bool IsManager { get { return CurrentRole == AppRole.Manager; } }
        private bool CanUseSales { get { return IsManager || CurrentRole == AppRole.Sales; } }
        private bool CanUseStock { get { return IsManager || CurrentRole == AppRole.Stock; } }

        private void BuildNavigation()
        {
            flowNav.SuspendLayout();
            flowNav.Controls.Clear();
            AddNavButton("Trang chủ", "home", true);

            if (CanUseSales)
            {
                AddSectionLabel("BÁN HÀNG");
                AddNavButton("Bán hàng và khách hàng", "sales", false);
            }
            if (CanUseStock)
            {
                AddSectionLabel("KHO HÀNG");
                AddNavButton("Kho sách, nhập và mất", "stock", false);
                AddSectionLabel("DANH MỤC");
                AddNavButton("Danh mục cửa hàng", "catalog", false);
            }
            AddSectionLabel("TRA CỨU");
            AddNavButton(IsManager ? "Tìm kiếm và báo cáo" : "Tìm kiếm", "lookup", false);
            if (IsManager)
            {
                AddSectionLabel("HỆ THỐNG");
                AddNavButton("Nhân viên và công việc", "system", false);
            }
            flowNav.ResumeLayout(true);
        }

        private void AddSectionLabel(string text)
        {
            var label = new Label
            {
                AutoSize = false,
                Width = 226,
                Height = 29,
                Text = text,
                ForeColor = Color.FromArgb(156, 176, 201),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                TextAlign = ContentAlignment.BottomLeft,
                Margin = new Padding(12, 14, 8, 2),
                Padding = new Padding(4, 0, 0, 2)
            };
            flowNav.Controls.Add(label);
        }

        private void AddNavButton(string text, string target, bool isHome)
        {
            var button = new Button
            {
                AutoSize = false,
                Width = 244,
                Height = 42,
                Text = "   " + text,
                Tag = target,
                TextAlign = ContentAlignment.MiddleLeft,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = target == currentTarget ? Color.FromArgb(52, 78, 112) : Color.FromArgb(24, 42, 67),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                Margin = new Padding(5, 2, 5, 2),
                Cursor = Cursors.Hand
            };
            button.Click += delegate { Navigate(target); };
            flowNav.Controls.Add(button);
        }

        private void Navigate(string target, int selectedTab = 0)
        {
            currentTarget = target;
            foreach (Control control in flowNav.Controls)
            {
                var button = control as Button;
                if (button != null)
                    button.BackColor = (string)button.Tag == target ? Color.FromArgb(52, 78, 112) : Color.FromArgb(24, 42, 67);
            }
            switch (target)
            {
                case "home": ShowDashboard(); LoadDashboard(); break;
                case "sales": ShowTabs("Bán hàng", new[] { "Lập hóa đơn bán", "Khách hàng" }, new Form[] { new frmBanHang(), new frmKhachHang() }); break;
                case "stock": ShowTabs("Kho hàng", new[] { "Sách và tồn kho", "Nhập hàng", "Ghi nhận sách mất" }, new Form[] { new frmSach(), new frmNhapHang(), new frmMatSach() }, selectedTab); break;
                case "catalog": ShowTabs("Danh mục cửa hàng", new[] { "Loại sách", "Lĩnh vực", "Ngôn ngữ", "Tác giả", "Nhà xuất bản", "Nhà cung cấp" }, new Form[] { new frmLoaiSach(), new frmLinhVuc(), new frmNgonNgu(), new frmTacGia(), new frmNXB(), new frmNhaCungCap() }); break;
                case "lookup":
                    if (IsManager) ShowTabs("Tra cứu và báo cáo", new[] { "Tìm kiếm", "Báo cáo" }, new Form[] { new frmTimKiem(), new frmBaoCao() });
                    else ShowTabs("Tìm kiếm", new[] { "Tìm kiếm" }, new Form[] { new frmTimKiem() });
                    break;
                case "system": ShowTabs("Hệ thống", new[] { "Nhân viên", "Công việc" }, new Form[] { new frmNhanVien(), new frmCongViec() }); break;
            }
        }

        private void ShowTabs(string title, string[] captions, Form[] forms, int selectedTab = 0)
        {
            ReleaseEmbeddedForms();
            panelHome.Visible = false;
            for (int i = panelHost.Controls.Count - 1; i >= 0; i--)
            {
                var oldPage = panelHost.Controls[i];
                if (oldPage == panelHome) continue;
                panelHost.Controls.RemoveAt(i);
                oldPage.Dispose();
            }
            panelHost.Controls.Clear();
            lblPageTitle.Text = title;
            var tabs = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9F), Name = "tabsPage" };
            panelHost.Controls.Add(tabs);
            for (int i = 0; i < forms.Length; i++)
            {
                var page = new TabPage(captions[i]) { BackColor = Color.White, Padding = new Padding(0) };
                tabs.TabPages.Add(page);
                var child = forms[i];
                child.TopLevel = false;
                child.FormBorderStyle = FormBorderStyle.None;
                child.Dock = DockStyle.Fill;
                child.Visible = false;
                page.Controls.Add(child);
                child.Show();
                embeddedForms.Add(child);
            }
            if (forms.Length > 0) tabs.SelectedIndex = Math.Max(0, Math.Min(selectedTab, tabs.TabPages.Count - 1));
            if (forms.Length > 0) lblPageSubtitle.Text = "Chuyển tab để mở chức năng trong cùng cửa sổ.";
        }

        private void ReleaseEmbeddedForms()
        {
            foreach (var form in embeddedForms)
            {
                try { form.Close(); form.Dispose(); } catch { }
            }
            embeddedForms.Clear();
        }

        private void ShowDashboard()
        {
            ReleaseEmbeddedForms();
            panelHost.Controls.Clear();
            lblPageTitle.Text = "Tổng quan cửa hàng";
            lblPageSubtitle.Text = "Hoạt động hôm nay và các việc cần xử lý.";
            panelHome.Visible = true;
            panelHome.Dock = DockStyle.Fill;
            panelHost.Controls.Add(panelHome);
            panelHome.BringToFront();
            btnQuickBan.Visible = CanUseSales;
            btnQuickNhap.Visible = CanUseStock;
            btnQuickSach.Visible = CanUseStock;
        }

        private void LoadDashboard()
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            try
            {
                var sales = DB.GetTable(@"SELECT ISNULL(SUM(TongTien), 0) AS DoanhThu, COUNT(*) AS SoHoaDon
FROM HoaDonBan WHERE NgayBan=CONVERT(date, GETDATE())");
                lblRevenueValue.Text = Convert.ToDecimal(sales.Rows[0]["DoanhThu"]).ToString("N0") + " đ";
                lblInvoiceValue.Text = Convert.ToString(sales.Rows[0]["SoHoaDon"]);
                var lowStock = DB.GetTable(@"SELECT s.MaSach, s.TenSach, k.SoLuong
FROM KhoSach k JOIN Sach s ON s.MaSach=k.MaSach
WHERE k.SoLuong <= 5 ORDER BY k.SoLuong, s.TenSach");
                lblLowStockValue.Text = lowStock.Rows.Count.ToString();
                gridLowStock.DataSource = lowStock;
                SetGridHeader("MaSach", "Mã sách");
                SetGridHeader("TenSach", "Tên sách");
                SetGridHeader("SoLuong", "Tồn kho");
                if (gridLowStock.Columns.Contains("SoLuong"))
                    gridLowStock.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            catch (Exception ex)
            {
                lblRevenueValue.Text = "—";
                lblInvoiceValue.Text = "—";
                lblLowStockValue.Text = "—";
                MessageBox.Show(this, "Không tải được số liệu tổng quan: " + ex.Message, "Tổng quan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SetGridHeader(string column, string header)
        {
            if (gridLowStock.Columns.Contains(column)) gridLowStock.Columns[column].HeaderText = header;
        }

        private void UpdateSessionLabels()
        {
            lblUserName.Text = String.IsNullOrWhiteSpace(AppSession.EmployeeName) ? "Bản xem thiết kế" : AppSession.EmployeeName;
            lblUserRole.Text = String.IsNullOrWhiteSpace(AppSession.JobName) ? "Quản lý" : AppSession.JobName;
        }

        private void btnRefresh_Click(object sender, EventArgs e) { LoadDashboard(); }
        private void btnQuickBan_Click(object sender, EventArgs e) { Navigate("sales"); }
        private void btnQuickNhap_Click(object sender, EventArgs e) { Navigate("stock", 1); }
        private void btnQuickSach_Click(object sender, EventArgs e) { Navigate("stock"); }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            AppSession.Clear();
            Hide();
            using (var login = new frmDangNhap())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    BuildNavigation();
                    UpdateSessionLabels();
                    ShowDashboard();
                    LoadDashboard();
                    Show();
                    return;
                }
            }
            Close();
        }
    }
}
