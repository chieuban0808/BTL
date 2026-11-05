using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace QuanLyCuaHangSach
{
    internal static class ExcelReportExporter
    {
        public static void Export(DataTable rows, string title, string[] headers, string[] columns,
            string invoiceIdColumn, string invoiceTotalColumn, string dateColumn, string path)
        {
            if (Path.GetExtension(path).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                WriteCsv(rows, headers, columns, invoiceIdColumn, invoiceTotalColumn, dateColumn, path);
            else WriteXlsx(rows, title, headers, columns, invoiceIdColumn, invoiceTotalColumn, dateColumn, path);
        }

        private static void WriteXlsx(DataTable rows, string title, string[] headers, string[] columns,
            string invoiceIdColumn, string invoiceTotalColumn, string dateColumn, string path)
        {
            using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            using (ZipArchive archive = new ZipArchive(file, ZipArchiveMode.Create))
            {
                AddText(archive, "[Content_Types].xml", "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Types xmlns=\"http://schemas.openxmlformats.org/package/2006/content-types\"><Default Extension=\"rels\" ContentType=\"application/vnd.openxmlformats-package.relationships+xml\"/><Default Extension=\"xml\" ContentType=\"application/xml\"/><Override PartName=\"/xl/workbook.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml\"/><Override PartName=\"/xl/worksheets/sheet1.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml\"/><Override PartName=\"/xl/styles.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml\"/></Types>");
                AddText(archive, "_rels/.rels", "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\"><Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument\" Target=\"xl/workbook.xml\"/></Relationships>");
                AddText(archive, "xl/workbook.xml", "<?xml version=\"1.0\" encoding=\"UTF-8\"?><workbook xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\" xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\"><sheets><sheet name=\"BaoCao\" sheetId=\"1\" r:id=\"rId1\"/></sheets></workbook>");
                AddText(archive, "xl/_rels/workbook.xml.rels", "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\"><Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet\" Target=\"worksheets/sheet1.xml\"/><Relationship Id=\"rId2\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles\" Target=\"styles.xml\"/></Relationships>");
                AddText(archive, "xl/styles.xml", "<?xml version=\"1.0\" encoding=\"UTF-8\"?><styleSheet xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\"><numFmts count=\"3\"><numFmt numFmtId=\"164\" formatCode=\"#,##0\"/><numFmt numFmtId=\"165\" formatCode=\"dd/mm/yyyy\"/><numFmt numFmtId=\"166\" formatCode=\"0.00\"/></numFmts><fonts count=\"3\"><font><sz val=\"11\"/><name val=\"Calibri\"/></font><font><b/><sz val=\"16\"/><color rgb=\"FF234066\"/><name val=\"Calibri\"/></font><font><b/><sz val=\"11\"/><color rgb=\"FFFFFFFF\"/><name val=\"Calibri\"/></font></fonts><fills count=\"3\"><fill><patternFill patternType=\"none\"/></fill><fill><patternFill patternType=\"gray125\"/></fill><fill><patternFill patternType=\"solid\"><fgColor rgb=\"FF234066\"/><bgColor indexed=\"64\"/></patternFill></fill></fills><borders count=\"1\"><border><left/><right/><top/><bottom/><diagonal/></border></borders><cellStyleXfs count=\"1\"><xf numFmtId=\"0\" fontId=\"0\" fillId=\"0\" borderId=\"0\"/></cellStyleXfs><cellXfs count=\"7\"><xf numFmtId=\"0\" fontId=\"0\" fillId=\"0\" borderId=\"0\" xfId=\"0\"/><xf numFmtId=\"165\" fontId=\"0\" fillId=\"0\" borderId=\"0\" xfId=\"0\" applyNumberFormat=\"1\"/><xf numFmtId=\"164\" fontId=\"0\" fillId=\"0\" borderId=\"0\" xfId=\"0\" applyNumberFormat=\"1\"/><xf numFmtId=\"166\" fontId=\"0\" fillId=\"0\" borderId=\"0\" xfId=\"0\" applyNumberFormat=\"1\"/><xf numFmtId=\"0\" fontId=\"2\" fillId=\"2\" borderId=\"0\" xfId=\"0\" applyFill=\"1\" applyFont=\"1\"/><xf numFmtId=\"0\" fontId=\"1\" fillId=\"0\" borderId=\"0\" xfId=\"0\" applyFont=\"1\"/><xf numFmtId=\"164\" fontId=\"2\" fillId=\"2\" borderId=\"0\" xfId=\"0\" applyFill=\"1\" applyFont=\"1\" applyNumberFormat=\"1\"/></cellXfs></styleSheet>");
                AddBytes(archive, "xl/worksheets/sheet1.xml", BuildSheet(rows, title, headers, columns, invoiceIdColumn, invoiceTotalColumn, dateColumn));
            }
        }

        private static byte[] BuildSheet(DataTable rows, string title, string[] headers, string[] columns,
            string invoiceIdColumn, string invoiceTotalColumn, string dateColumn)
        {
            using (MemoryStream stream = new MemoryStream())
            using (XmlWriter writer = XmlWriter.Create(stream, new XmlWriterSettings { Encoding = new UTF8Encoding(false), Indent = true }))
            {
                const string ns = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
                writer.WriteStartDocument(); writer.WriteStartElement("worksheet", ns); writer.WriteStartElement("cols", ns);
                for (int i = 0; i < headers.Length; i++) { writer.WriteStartElement("col", ns); writer.WriteAttributeString("min", (i + 1).ToString()); writer.WriteAttributeString("max", (i + 1).ToString()); writer.WriteAttributeString("width", (i == 2 || i == 3 ? "28" : i == 5 ? "38" : "16")); writer.WriteAttributeString("customWidth", "1"); writer.WriteEndElement(); }
                writer.WriteEndElement(); writer.WriteStartElement("sheetData", ns);
                writer.WriteStartElement("row", ns); writer.WriteAttributeString("r", "1"); Cell(writer, ns, "A1", title, 5); writer.WriteEndElement();
                writer.WriteStartElement("row", ns); writer.WriteAttributeString("r", "2"); Cell(writer, ns, "A2", "Ngày xuất: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture), 0); writer.WriteEndElement();
                writer.WriteStartElement("row", ns); writer.WriteAttributeString("r", "4"); for (int i = 0; i < headers.Length; i++) Cell(writer, ns, Col(i + 1) + "4", headers[i], 4); writer.WriteEndElement();
                int rowNum = 5; object previousInvoice = null; decimal sum = 0m;
                foreach (DataRow row in rows.Rows)
                {
                    object invoice = row[invoiceIdColumn]; bool first = previousInvoice == null || !Object.Equals(previousInvoice, invoice);
                    if (first) { previousInvoice = invoice; if (!row.IsNull(invoiceTotalColumn)) sum += Convert.ToDecimal(row[invoiceTotalColumn]); }
                    writer.WriteStartElement("row", ns); writer.WriteAttributeString("r", rowNum.ToString(CultureInfo.InvariantCulture));
                    for (int i = 0; i < columns.Length; i++)
                    {
                        string col = columns[i], cell = Col(i + 1) + rowNum.ToString(CultureInfo.InvariantCulture);
                        if (col == invoiceTotalColumn && !first || row.IsNull(col)) { Cell(writer, ns, cell, "", 0); continue; }
                        object value = row[col];
                        if (col == dateColumn) Cell(writer, ns, cell, Convert.ToDateTime(value).ToOADate().ToString(CultureInfo.InvariantCulture), 1, true);
                        else if (rows.Columns[col].DataType == typeof(string)) Cell(writer, ns, cell, Convert.ToString(value), 0);
                        else Cell(writer, ns, cell, Convert.ToDecimal(value).ToString(CultureInfo.InvariantCulture), col.IndexOf("KhuyenMai", StringComparison.OrdinalIgnoreCase) >= 0 ? 3 : (col.StartsWith("SL", StringComparison.OrdinalIgnoreCase) || col.StartsWith("Ma", StringComparison.OrdinalIgnoreCase) || col.StartsWith("SoHD", StringComparison.OrdinalIgnoreCase) ? 0 : 2), true);
                    }
                    writer.WriteEndElement(); rowNum++;
                }
                writer.WriteStartElement("row", ns); writer.WriteAttributeString("r", rowNum.ToString(CultureInfo.InvariantCulture));
                Cell(writer, ns, Col(Math.Max(1, headers.Length - 1)) + rowNum, "TỔNG CỘNG", 4); Cell(writer, ns, Col(headers.Length) + rowNum, sum.ToString(CultureInfo.InvariantCulture), 6, true); writer.WriteEndElement();
                writer.WriteEndElement(); writer.WriteStartElement("mergeCells", ns); writer.WriteAttributeString("count", "2");
                writer.WriteStartElement("mergeCell", ns); writer.WriteAttributeString("ref", "A1:" + Col(headers.Length) + "1"); writer.WriteEndElement(); writer.WriteStartElement("mergeCell", ns); writer.WriteAttributeString("ref", "A2:" + Col(headers.Length) + "2"); writer.WriteEndElement(); writer.WriteEndElement();
                writer.WriteEndElement(); writer.WriteEndDocument(); writer.Flush(); return stream.ToArray();
            }
        }

        private static void Cell(XmlWriter w, string ns, string reference, string value, int style, bool numeric = false)
        {
            w.WriteStartElement("c", ns); w.WriteAttributeString("r", reference); if (style != 0) w.WriteAttributeString("s", style.ToString(CultureInfo.InvariantCulture));
            if (numeric) { w.WriteStartElement("v", ns); w.WriteString(value); w.WriteEndElement(); }
            else { w.WriteAttributeString("t", "inlineStr"); w.WriteStartElement("is", ns); w.WriteStartElement("t", ns); w.WriteAttributeString("xml", "space", "http://www.w3.org/XML/1998/namespace", "preserve"); w.WriteString(value ?? ""); w.WriteEndElement(); w.WriteEndElement(); }
            w.WriteEndElement();
        }
        private static string Col(int n) { string result = ""; while (n > 0) { result = (char)('A' + (n - 1) % 26) + result; n = (n - 1) / 26; } return result; }
        private static void AddText(ZipArchive z, string name, string value) { AddBytes(z, name, new UTF8Encoding(false).GetBytes(value)); }
        private static void AddBytes(ZipArchive z, string name, byte[] bytes) { var e = z.CreateEntry(name, CompressionLevel.Optimal); using (var s = e.Open()) s.Write(bytes, 0, bytes.Length); }

        private static void WriteCsv(DataTable rows, string[] headers, string[] columns, string invoiceIdColumn, string invoiceTotalColumn, string dateColumn, string path)
        {
            using (StreamWriter writer = new StreamWriter(path, false, new UTF8Encoding(true)))
            {
                writer.WriteLine(string.Join(",", Array.ConvertAll(headers, Escape))); object previous = null; decimal sum = 0m;
                foreach (DataRow row in rows.Rows)
                {
                    bool first = previous == null || !Object.Equals(previous, row[invoiceIdColumn]);
                    if (first) { previous = row[invoiceIdColumn]; if (!row.IsNull(invoiceTotalColumn)) sum += Convert.ToDecimal(row[invoiceTotalColumn]); }
                    string[] values = new string[columns.Length];
                    for (int i = 0; i < columns.Length; i++)
                    { string col = columns[i]; values[i] = col == invoiceTotalColumn && !first ? "" : row.IsNull(col) ? "" : col == dateColumn ? Convert.ToDateTime(row[col]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : Convert.ToString(row[col], CultureInfo.InvariantCulture); }
                    writer.WriteLine(string.Join(",", Array.ConvertAll(values, Escape)));
                }
                string[] total = new string[headers.Length]; total[headers.Length - 2] = "TỔNG CỘNG"; total[headers.Length - 1] = sum.ToString(CultureInfo.InvariantCulture); writer.WriteLine(string.Join(",", Array.ConvertAll(total, Escape)));
            }
        }
        private static string Escape(string value) { return "\"" + (value ?? "").Replace("\"", "\"\"") + "\""; }
    }
}
