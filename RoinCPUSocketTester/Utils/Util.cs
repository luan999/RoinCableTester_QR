using DevComponents.DotNetBar.Controls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace RoinCableTester.Utils {
    public static class Util {
        private static string _appPath = Path.GetDirectoryName(Application.ExecutablePath);

        // Configuration Variables
        private static Configuration _configManager = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
        private static KeyValueConfigurationCollection _configSetting = _configManager.AppSettings.Settings;

        public static string GetProperty(string propName) {
            try {
                return _configSetting[propName].Value.ToString();
            } catch (Exception e) {
                TraceInfo(e.Message);
                return "";
            }
        }

        public static void SetProperty(string propName, string propValue) {
            _configSetting[propName].Value = propValue;
            _configManager.Save(ConfigurationSaveMode.Minimal);
            ConfigurationManager.RefreshSection(_configManager.AppSettings.SectionInformation.Name);
        }

        public static void TraceInfo(string message) {
            string tracemessage = string.Format("{0}\t{1}", DateTime.Now.ToString("MM/dd/yy HH:mm:ss"), message);
            Trace.WriteLine(tracemessage);
            //Trace.Flush();
        }

        public static string GetAppPath() {
            return _appPath;
        }

        public delegate void ExcelDelegate(ClosedXML.Excel.XLWorkbook workbook);

        public static void WriteExcel(string templateName, string fileName, ExcelDelegate xlsd) {

            string templatePath = Path.Combine(Util.GetAppPath(), templateName);
            ClosedXML.Excel.XLWorkbook workbook = new ClosedXML.Excel.XLWorkbook(templatePath);

            xlsd(workbook);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.InitialDirectory = "%USERPROFILE%";
            saveFileDialog.Filter = "Excel|*.xlsx";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.ShowHelp = true;
            saveFileDialog.FileName = fileName;
            if (saveFileDialog.ShowDialog().Equals(DialogResult.OK)) {
                workbook.SaveAs(saveFileDialog.FileName);
            }
            workbook.Dispose();
        }

        public delegate string CsvDelegate(System.Data.DataTable dt);

        public static void WriteCsv<T>(List<T> list, string fileName, CsvDelegate csvd) {
            if (list.Count > 0) {
                // 加入所有 DataTables 資料到 worksheets
                System.Data.DataTable dt = list.ToDataTable<T>();
                string header = csvd(dt);

                using (StreamWriter csvWriter = new StreamWriter(fileName)) {
                    csvWriter.WriteLine(header);

                    foreach (DataRow row in dt.Rows) {
                        csvWriter.WriteLine(string.Join(",", dt.Columns.Cast<DataColumn>().Select(x => row[x].ToString().Trim())));
                    }
                    csvWriter.Flush();
                }
            }
        }

        public static void WriteFile(List<string> list, string fileName) {
            if (File.Exists(fileName)) {
                File.Delete(fileName);
            }
            using (StreamWriter writer = new StreamWriter(File.Open(fileName, FileMode.CreateNew), Encoding.UTF8)) {
                foreach (string line in list) {
                    writer.WriteLine(line);
                }
                writer.Flush();
            }
        }

        public static void AppendText(this RichTextBoxEx box, string text, Color color) {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}
