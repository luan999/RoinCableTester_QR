#define CHT

using ClosedXML.Excel;
using RoinCableTester.Communication;
using RoinCableTester.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace RoinCableTester.Dialog {
    public partial class ReportDialog : Form {
        Database _db = new Database();
        NetListInfo _uploadData = null;
        MainForm _form = null;
        DataTable _data = null;
        DataTable _table = null;
        List<string> _netlistPins;

        public ReportDialog(NetListInfo uploadData, MainForm form) {
            _uploadData = uploadData;
            _form = form;
            InitializeComponent();

            lblProductName.Text = IniFile.IniReadValue("ReportDialog", "ProductName") + "<font color='red'>＊</font>";
            //lblCustomerName.Text = IniFile.IniReadValue("ReportDialog", "CustomerName") + "<font color='red'>＊</font>";
            txtProductName.Text = Util.GetProperty("ProductName");
            //txtCustomerName.Text = Util.GetProperty("CustomerName");

            btnCancel.Text = IniFile.IniReadValue("Button", "Exit");
            btnExport.Text = IniFile.IniReadValue("Button", "Export");
            btnSearch.Text = IniFile.IniReadValue("Button", "Search");

            ListReport();
        }

        private void btnSearch_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(txtProductName.Text)) {
                MessageBox.Show(IniFile.IniReadValue("Message", "MustRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            } else {
                ListReport();
            }
            this.DialogResult = DialogResult.None;
        }

        private void ListReport() {
            string sql = "select * from TestResult where 1=1";
            if (!string.IsNullOrEmpty(txtProductName.Text)) {
                sql += string.Format(" and ProductName = '{0}'", txtProductName.Text);
            }
            //Util.TraceInfo("Report SQL : " + sql);

            try {
                BindingSource bs = new BindingSource();
                _data = _db.GetDataTable(txtProductName.Text, sql);
                _table = new DataTable();
                _table.Columns.Add(IniFile.IniReadValue("ReportDialog", "ProductName"), typeof(string));
                _table.Columns.Add(IniFile.IniReadValue("ReportDialog", "OrderNo"), typeof(string));
                _table.Columns.Add(IniFile.IniReadValue("ReportDialog", "LabelSerialNo"), typeof(string));
                _table.Columns.Add(IniFile.IniReadValue("ReportDialog", "InsulationValue"), typeof(string));
                _table.Columns.Add(IniFile.IniReadValue("ReportDialog", "TestResult"), typeof(string));
                _table.Columns.Add(IniFile.IniReadValue("ReportDialog", "TestTime"), typeof(string));
                _table.Columns.Add(IniFile.IniReadValue("ReportDialog", "ResultDesc"), typeof(string));
                _table.Columns.Add(IniFile.IniReadValue("ReportDialog", "Operator"), typeof(string));


                /*
                if (_uploadData != null) { // 啟動後尚未取得NETLIST不允顯示
                    foreach (string netListPin in _uploadData.netListPinOrg) {
                        _table.Columns.Add(netListPin, typeof(string));
                    }
                }
                */

                _netlistPins = new List<string>();
                // 抓資料表第一行標示netlist
                string netlist;
                if (_data.Rows.Count > 0) {
                    foreach (DataRow r in _data.Rows) {
                        netlist = r["NETList"].ToString();
                        if (!string.IsNullOrEmpty(netlist) && netlist != "[]") {
                            List<TestDetailData> nets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TestDetailData>>(netlist);
                            if (nets.Count > 0) {
                                foreach (TestDetailData net in nets) {
                                    _table.Columns.Add(net.Name, typeof(string));
                                    _netlistPins.Add(net.Name);
                                }
                                break;
                            }
                        }
                    }
                }

                foreach (DataRow r in _data.Rows) {
                    List<object> paramList = new List<object>(new object[] { r["ProductName"], r["OrderNo"], r["LabelSerialNo"], r["InsulationValue"], r["TestResult"].ToString(), r["TestTime"], r["ResultDesc"], r["Operator"] });

                    //if (_uploadData != null) { // 啟動後尚未取得NETLIST不允顯示
                        netlist = r["NETList"].ToString();
                        if (!string.IsNullOrEmpty(netlist) && netlist != "[]") {
                            List<TestDetailData> detail = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TestDetailData>>(netlist);
                            if (detail.Count > 0) {
                                foreach (string netListPin in _netlistPins) {
                                    TestDetailData detailData = detail.SingleOrDefault(x => netListPin == x.Name);
                                    paramList.Add(detailData == null ? "" : detailData.Data);
                                }
                            }
                        }
                    //}
                    _table.Rows.Add(paramList.ToArray<object>());
                }

                bs.DataSource = _table;
                gvReport.DataSource = bs;
                bindingNavigatorEx1.BindingSource = bs;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e) {
#if ENG
            string templateFile = "ROIN_RT_TESTER_REPORT_Eng.xlsx";
#elif CHS
            string templateFile = "ROIN_RT_TESTER_REPORT_Chs.xlsx";
#else
            string templateFile = "ROIN_RT_TESTER_REPORT_Cht.xlsx";
#endif
            Util.WriteExcel(templateFile, this.txtProductName.Text + ".xlsx", (workbook) => {
            ClosedXML.Excel.IXLWorksheet workSheet = null;
                try {
                    workSheet = workbook.Worksheet(2);

                    workSheet.Cell(2, 2).Value = this.txtProductName.Text;
                    workSheet.Cell(3, 2).Value = _form.lblFileName.Text;
                    workSheet.Cell(4, 2).Value = _data.Rows[0]["TestTime"];
                    workSheet.Cell(5, 2).Value = _data.Rows[_data.Rows.Count - 1]["TestTime"];
                    workSheet.Cell(6, 2).Value = Util.GetProperty("TestMachine");

                    workSheet.Cell(9, 2).Value = Util.GetProperty("LabelTitle");
                    workSheet.Cell(10, 2).Value = Util.GetProperty("LabelProduct");
                    workSheet.Cell(11, 2).Value = Util.GetProperty("LabelRevision");
                    workSheet.Cell(12, 2).Value = Util.GetProperty("LabelPart");
                    workSheet.Cell(13, 2).Value = Util.GetProperty("LabelPlantVendor");
                    workSheet.Cell(14, 2).Value = Util.GetProperty("LabelTestedBy");

                    workSheet.Cell(2, 5).Value = _form.lblCondData.Text;
                    workSheet.Cell(2, 7).Value = _form.lblACHipotData.Text;
                    workSheet.Cell(2, 9).Value = _form.lblDCHipotData.Text;
                    workSheet.Cell(3, 9).Value = _form.lblDCHipotTimeData.Text;
                    workSheet.Cell(3, 5).Value = _form.lblInterTime2Data.Text;
                    workSheet.Cell(3, 7).Value = _form.lblACHipotTimeData.Text;
                    workSheet.Cell(4, 9).Value = _form.lblInstData.Text;
                    workSheet.Cell(4, 7).Value = _form.lblDCILeakageData.Text;

                    workSheet = workbook.Worksheet(3);

                    // 加入所有 DataTables 資料到 worksheet
                    workSheet.Cell("A18").InsertTable(_table);

                    //var header = workSheet.Range("A18:" + Convert.ToChar(_table.Columns.Count - 1 + Convert.ToInt32('A')) + "18");
                    //header.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                    workSheet.Tables.First().ShowAutoFilter = false;

                    IEnumerable<DataRow> dataRow = _data.AsEnumerable();
                    for (int y = 0; y < dataRow.Count(); y++) {
                        if (workSheet.Cell(19 + y, 5).Value.ToString() != "PASS") {
                            workSheet.Cell(19 + y, 5).Style.Font.FontColor = XLColor.Red;
                        }
                        for (int x = 0; x < _netlistPins.Count(); x++) {
                            string sVal = workSheet.Cell(19 + y, 9 + x).Value.ToString();
                            double iVal = 0;
                            // 僅驗證導通值是否大於 10, 規避元件判斷
                            if (_netlistPins[x].IndexOf(':') == -1 && double.TryParse(sVal, out iVal) && iVal > 10) {
                                workSheet.Cell(19 + y, 9 + x).Style.Font.FontColor = XLColor.Red;
                            }
                        }
                    }

                    workSheet.Row(3).Cell(1).Value = _form.lblCustomerName.Text; //_data.Rows[0]["ProductName"];
                    workSheet.Row(3).Cell(2).Value = dataRow.Count();
                    workSheet.Row(3).Cell(3).Value = dataRow.Count(row => row.Field<String>("TestResult") == "PASS");
                    workSheet.Row(3).Cell(7).Value = dataRow.Count(row => row.Field<String>("TestResult") == "Open" && row.Field<String>("ResultDesc").IndexOf("Component Fail") == -1);
                    workSheet.Row(3).Cell(8).Value = dataRow.Count(row => row.Field<String>("TestResult") == "Short" && row.Field<String>("ResultDesc").IndexOf("Component Fail") == -1);
                    workSheet.Row(3).Cell(9).Value = dataRow.Count(row => row.Field<String>("TestResult") == "Cond");
                    workSheet.Row(3).Cell(10).Value = dataRow.Count(row => row.Field<String>("TestResult") == "Leak" || row.Field<String>("TestResult") == "Ovac");
                    workSheet.Row(3).Cell(11).Value = dataRow.Count(row => row.Field<String>("TestResult") == "Ins" || row.Field<String>("TestResult") == "Ovv" || row.Field<String>("TestResult") == "Ovc" || row.Field<String>("TestResult") == "Arc" || row.Field<String>("TestResult") == "DcOvv" || row.Field<String>("TestResult") == "DcOvc" || row.Field<String>("TestResult") == "DcArc" || row.Field<String>("TestResult") == "Dci");
                    workSheet.Row(3).Cell(12).Value = dataRow.Count(row => row.Field<String>("TestResult") == "Inter");
                    workSheet.Row(3).Cell(13).Value = dataRow.Count(row => row.Field<String>("ResultDesc").IndexOf("Component Fail") > -1);

                } catch (Exception ee) {
                    Util.TraceInfo("Report Error: " + ee.Message);
                }
            });

            this.DialogResult = DialogResult.None;
        }
    }
}
