using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    public partial class CatalogFormBase : Form
    {
        private readonly string tableName;
        private readonly string keyName;
        private readonly CatalogField[] fields;
        private readonly Dictionary<string, TextBox> editors = new Dictionary<string, TextBox>();
        private int? selectedId;

        // Constructor không tham số để WinForms Designer có thể nạp lớp cơ sở.
        // Các form danh mục thật dùng constructor có metadata bên dưới.
        public CatalogFormBase()
        {
            tableName = string.Empty;
            keyName = string.Empty;
            fields = new CatalogField[0];
            InitializeComponent();
            WireUiEvents();
        }

        protected CatalogFormBase(string title, string table, string key, params CatalogField[] catalogFields)
        {
            tableName = table;
            keyName = key;
            fields = catalogFields;
            InitializeComponent();
            Text = title;
            lblTitle.Text = title.ToUpperInvariant();
            BuildFieldEditors();
            WireUiEvents();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(760, 500);
            Size = new Size(900, 620);
            Font = new Font("Segoe UI", 9F);
            Load += delegate { LoadRows(); };
        }

        private void BuildFieldEditors()
        {
            editorFields.Controls.Clear();
            editors.Clear();
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
                editorFields.Controls.Add(block);
            }
        }

        private void WireUiEvents()
        {
            btnThem.Click += delegate { ClearEditors(); };
            btnLuu.Click += delegate { SaveRecord(); };
            btnXoa.Click += delegate { DeleteRecord(); };
            btnTaiLai.Click += delegate { if (fields.Length > 0) LoadRows(); ClearEditors(); };
            grid.CellClick += Grid_CellClick;
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

}
