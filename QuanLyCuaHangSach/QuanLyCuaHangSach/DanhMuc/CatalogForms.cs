using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public enum CatalogValueKind { Text, Integer, Date }

    public sealed class CatalogField
    {
        public string Name;
        public string Caption;
        public CatalogValueKind Kind;
        public bool Required;
        public int MaxLength;

        public CatalogField(string name, string caption, CatalogValueKind kind, bool required, int maxLength)
        {
            Name = name;
            Caption = caption;
            Kind = kind;
            Required = required;
            MaxLength = maxLength;
        }
    }

    public abstract class CatalogFormBase : Form
    {
        private readonly string tableName;
        private readonly string keyName;
        private readonly CatalogField[] fields;
        private readonly Dictionary<string, TextBox> editors = new Dictionary<string, TextBox>();
        private readonly DataGridView grid = new DataGridView();
        private int? selectedId;

        protected CatalogFormBase(string title, string table, string key, params CatalogField[] catalogFields)
        {
            tableName = table;
            keyName = key;
            fields = catalogFields;
            Text = title;
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(760, 500);
            Size = new Size(900, 620);
            Font = new Font("Segoe UI", 9F);

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(14),
                ColumnCount = 1,
                RowCount = 3
            };
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var editorPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                WrapContents = true,
                Padding = new Padding(0, 0, 0, 8)
            };
            foreach (var field in fields)
            {
                var block = new Panel { Width = 390, Height = 60, Margin = new Padding(4) };
                var label = new Label
                {
                    Text = field.Caption + (field.Required ? " *" : ""),
                    Left = 0,
                    Top = 2,
                    Width = 380,
                    Height = 20
                };
                var editor = new TextBox
                {
                    Left = 0,
                    Top = 24,
                    Width = 380,
                    MaxLength = field.MaxLength > 0 ? field.MaxLength : 32767
                };
                editors.Add(field.Name, editor);
                block.Controls.Add(label);
                block.Controls.Add(editor);
                editorPanel.Controls.Add(block);
            }

            var actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Padding = new Padding(0, 0, 0, 8)
            };
            actions.Controls.Add(MakeButton("Thêm mới", delegate { ClearEditors(); }));
            actions.Controls.Add(MakeButton("Lưu", delegate { SaveRecord(); }));
            actions.Controls.Add(MakeButton("Xóa", delegate { DeleteRecord(); }));
            actions.Controls.Add(MakeButton("Tải lại", delegate { LoadRows(); ClearEditors(); }));

            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.MultiSelect = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.BackgroundColor = Color.White;
            grid.CellClick += Grid_CellClick;

            layout.Controls.Add(editorPanel, 0, 0);
            layout.Controls.Add(actions, 0, 1);
            layout.Controls.Add(grid, 0, 2);
            Controls.Add(layout);
            Load += delegate { LoadRows(); };
        }

        private static Button MakeButton(string text, EventHandler click)
        {
            var button = new Button { Text = text, AutoSize = true, Height = 34, Margin = new Padding(4) };
            button.Click += click;
            return button;
        }

        private void LoadRows()
        {
            try
            {
                string columns = string.Join(", ", fields.Select(f => "[" + f.Name + "]"));
                grid.DataSource = DB.GetTable("SELECT [" + keyName + "], " + columns + " FROM [" + tableName + "] ORDER BY [" + keyName + "] DESC");
                if (grid.Columns.Contains(keyName)) grid.Columns[keyName].HeaderText = "Mã";
                foreach (var field in fields)
                    if (grid.Columns.Contains(field.Name)) grid.Columns[field.Name].HeaderText = field.Caption;
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = grid.Rows[e.RowIndex];
            selectedId = Convert.ToInt32(row.Cells[keyName].Value);
            foreach (var field in fields)
            {
                object value = row.Cells[field.Name].Value;
                editors[field.Name].Text = value == null || value == DBNull.Value ? "" : Convert.ToString(value);
            }
        }

        private void SaveRecord()
        {
            try
            {
                var values = new List<object>();
                foreach (var field in fields)
                {
                    string raw = editors[field.Name].Text.Trim();
                    if (field.Required && raw.Length == 0)
                        throw new InvalidOperationException("Vui lòng nhập " + field.Caption + ".");
                    if (raw.Length == 0) { values.Add(DBNull.Value); continue; }

                    if (field.Kind == CatalogValueKind.Integer)
                    {
                        int value;
                        if (!int.TryParse(raw, out value)) throw new InvalidOperationException(field.Caption + " phải là số nguyên.");
                        values.Add(value);
                    }
                    else if (field.Kind == CatalogValueKind.Date)
                    {
                        DateTime value;
                        if (!DateTime.TryParse(raw, out value)) throw new InvalidOperationException(field.Caption + " không đúng định dạng ngày.");
                        values.Add(value.Date);
                    }
                    else values.Add(raw);
                }

                var parameters = new List<SqlParameter>();
                for (int i = 0; i < fields.Length; i++)
                    parameters.Add(CreateParameter("@p" + i, fields[i], values[i]));

                if (selectedId.HasValue)
                {
                    var assignments = fields.Select((f, i) => "[" + f.Name + "] = @p" + i);
                    parameters.Add(new SqlParameter("@id", selectedId.Value));
                    DB.Exec("UPDATE [" + tableName + "] SET " + string.Join(", ", assignments) + " WHERE [" + keyName + "] = @id", parameters.ToArray());
                }
                else
                {
                    string names = string.Join(", ", fields.Select(f => "[" + f.Name + "]"));
                    string args = string.Join(", ", fields.Select((f, i) => "@p" + i));
                    DB.Exec("INSERT INTO [" + tableName + "] (" + names + ") VALUES (" + args + ")", parameters.ToArray());
                }

                LoadRows();
                ClearEditors();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private static SqlParameter CreateParameter(string name, CatalogField field, object value)
        {
            SqlParameter parameter;
            if (field.Kind == CatalogValueKind.Integer) parameter = new SqlParameter(name, SqlDbType.Int);
            else if (field.Kind == CatalogValueKind.Date) parameter = new SqlParameter(name, SqlDbType.Date);
            else parameter = new SqlParameter(name, SqlDbType.NVarChar, field.MaxLength > 0 ? field.MaxLength : 4000);
            parameter.Value = value;
            return parameter;
        }

        private void DeleteRecord()
        {
            if (!selectedId.HasValue)
            {
                MessageBox.Show(this, "Chọn một dòng cần xóa.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show(this, "Xóa bản ghi đang chọn?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                DB.Exec("DELETE FROM [" + tableName + "] WHERE [" + keyName + "] = @id", new SqlParameter("@id", selectedId.Value));
                LoadRows();
                ClearEditors();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void ClearEditors()
        {
            selectedId = null;
            foreach (var editor in editors.Values) editor.Clear();
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Không thể thực hiện", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public sealed class frmLoaiSach : CatalogFormBase
    {
        public frmLoaiSach() : base("Danh mục loại sách", "LoaiSach", "MaLoai",
            new CatalogField("TenLoai", "Tên loại sách", CatalogValueKind.Text, true, 100)) { }
    }

    public sealed class frmTacGia : CatalogFormBase
    {
        public frmTacGia() : base("Danh mục tác giả", "TacGia", "MaTG",
            new CatalogField("TenTG", "Tên tác giả", CatalogValueKind.Text, true, 100),
            new CatalogField("NgaySinh", "Ngày sinh (ví dụ 15/08/1980)", CatalogValueKind.Date, false, 0),
            new CatalogField("GioiTinh", "Giới tính", CatalogValueKind.Text, false, 5),
            new CatalogField("DiaChi", "Địa chỉ", CatalogValueKind.Text, false, 200)) { }
    }

    public sealed class frmNXB : CatalogFormBase
    {
        public frmNXB() : base("Danh mục nhà xuất bản", "NXB", "MaNXB",
            new CatalogField("TenNXB", "Tên nhà xuất bản", CatalogValueKind.Text, true, 150),
            new CatalogField("DiaChi", "Địa chỉ", CatalogValueKind.Text, false, 200),
            new CatalogField("DienThoai", "Điện thoại", CatalogValueKind.Text, false, 15)) { }
    }
}
