#define CHT

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using DevComponents.DotNetBar;
using RoinCableTester.Models;
using RoinCableTester.Communication;
using RoinCableTester.Utils;
using RoinCableTester.Dialog;
using System.Messaging;
using Microsoft.Win32;
using System.Globalization;
using Utils.LabelPrinting;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Text;
using QRCoder;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using DataMatrix.net;
using System.Drawing.Drawing2D;
using Utils;

namespace RoinCableTester {

    public partial class MainForm : Form {
        public const string                     S_EMPTY             = "";
        public const string                     S_BEGIN_BOOTING     = "BOOTING";
        public const string                     S_MODE_TEST         = "MODE_TEST";
        public const string                     S_BEGIN_UPLOAD      = "UPLOAD";
        public const string                     S_BEGIN_DOWNLOAD    = "DOWNLOAD";
        public const string                     S_BEGIN_NETLIST     = "NETLIST";
        public const string                     S_BEGIN_READ        = "READ";
        public const string                     S_BEGIN_TEST1       = "TEST1";
        public const string                     S_BEGIN_TEST2       = "TEST2";
        public const string                     S_SHORT_TEST        = "SHORT";
        public const string                     S_POINT_SEARCH      = "POINT_SEARCH";

        public const string                     S_BEGIN_DISK        = "DISK";
        public const string                     S_NETLIST_CONT      = "NETLIST_CONT";
        public const string                     S_NETLIST_COND      = "NETLIST_COND";
        public const string                     S_NETLIST_RES       = "NETLIST_RES";
        public const string                     S_NETLIST_CAP       = "NETLIST_CAP";
        public const string                     S_NETLIST_DIODE     = "NETLIST_DIODE";
        public const string                     S_LOAD_FILE         = "LOAD_FILE";

        public const string                     S_UPLOAD_NETLIST    = "UPLOAD_NETLIST";
        public const string                     S_UPLOAD_RESISTANCE = "UPLOAD_RESISTANCE";
        public const string                     S_UPLOAD_CAPACITY   = "UPLOAD_CAPACITY";
        public const string                     S_UPLOAD_DIODE      = "UPLOAD_DIODE";
        public const string                     S_UPLOAD_TEST_COND  = "UPLOAD_TEST_COND";

        // Rs232 Variables
        private Rs232Controller                 _rs232Controller    = null;
        private string                          _commandState       = null;
        private string                          _commandStateInner  = null;
        // Settings
        private Simple3Des                      _des                = new Simple3Des("roin");
        private List<string>                    _deviceId           = null;
        private string                          _afterCommand       = null;
        private bool                            _isR9001            = false;
        private string                          _expiredDate        = "";
        // Test Info
        private NetListInfo                     _uploadData           = null;
        private int                             _uploadCount          = 0;
        private bool                            _bPassChecked         = false;
        private List<string[]>                  _shortList            = null;
        private List<string>                    _pointList            = null;
        private int                             _successCount         = 0;
        private int                             _failCount            = 0;
        private int                             _testStepCount        = 0;
        private string                          _uploadType           = "";
        private string                          _uploadTestCondition  = null;
        private List<string>                    _paramList            = null;
        private List<string>                    _testErrorList        = null;
        private bool                            _fTestingFinish       = false; // 單一測試結束 (顯示 OK 字樣)
        private bool                            _fTestingStart        = false; // 單一測試啟動 (顯示 TESTING 字樣)
        private List<TestDetailData>            _testDetailList       = null;
        private string                          _testReportResultText = "";
        private bool                            _bUploadInitialized   = false;
        private bool                            _fTestingPass         = false; // 確認測試PASS (偵測PASS字樣，如尚未取得PASS則等待EMPTY)
        private string                          _testLastReceived     = ""; // 最後測試訊息資料 (判斷END後是否為EMPTY)
        // Database
        private Database                        _db                 = new Database();
        // Read Disk
        private List<string>                    _diskName           = new List<string>();
        private int                             _diskNameIndex      = 99;
        private string                          _netString          = "";
        private List<string>                    _net4save           = new List<string>();
        // Label
        private string                          _labelSerialNo      = "";

        private Regex regexR = new Regex(@"^R\d[\d.] [A-Zαβπδθψ]([0-5]\d|6[0-4])-[A-Zαβπδθψ]([0-5]\d|6[0-4]) (\d{4}|(?=\d+\.\d+)[\d.]{4})[oKM][\/\+-]\d{2}%$", RegexOptions.Compiled);
        private Regex regexC = new Regex(@"^C\d[\d.] [A-Zαβπδθψ]([0-5]\d|6[0-4])[-\]][A-Zαβπδθψ]([0-5]\d|6[0-4]) (\d{4}|(?=\d+\.\d+)[\d.]{4})[upn][\/\+-]\d{2}%$", RegexOptions.Compiled);
        private Regex regexD = new Regex(@"^D\d[\d.] [A-Zαβπδθψ]([0-5]\d|6[0-4])[-\+\=\]][A-Zαβπδθψ]([0-5]\d|6[0-4]) (\d{4}|(?=\d+\.\d+)[\d.]{4})V[\/\+-]\d{2}%$", RegexOptions.Compiled);

        public MainForm() {
            InitializeComponent();
            InitializeApplication();
            InitializeRs232();
        }

        private void InitializeApplication() {
            if (File.Exists(@"trace.log")) {
                File.Delete(@"trace.log");
            }
#if ENG
            IniFile.SetIniPath(Path.Combine(Util.GetAppPath(), "RoinLocaleEng.ini"));
#elif CHS
            IniFile.SetIniPath(Path.Combine(Util.GetAppPath(), "RoinLocaleChs.ini"));
#else
            IniFile.SetIniPath(Path.Combine(Util.GetAppPath(), "RoinLocaleCht.ini"));
#endif
            ShowMessage(IniFile.IniReadValue("Message", "RS232ConnectFirst"), true);

            _shortList = new List<string[]>();
            _pointList = new List<string>();

            DisableAllButton();

            this.Shown += this.MainForm_Shown;

            KeyDown += new KeyEventHandler(MainForm_KeyDown);
            SizeChanged += new EventHandler(MainForm_SizeChanged);

            Util.SetProperty("DtpDate", DateTime.Now.ToString("yyyy/MM/dd"));
        }

        void MainForm_KeyDown(object sender, KeyEventArgs e) {
            //System.Diagnostics.Debug.Write(e.KeyCode);
        }

        void MainForm_SizeChanged(object sender, EventArgs e) {
            if (this.WindowState == FormWindowState.Maximized) {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void MainForm_Shown(Object sender, EventArgs e) {
            var expiredDate = Util.GetProperty("ExpiredDate");
            if (string.IsNullOrEmpty(expiredDate)) {
                AlertMessage("Expired Date not found ?");
                Environment.Exit(Environment.ExitCode);
            }
            try {
                var date = new ClsCrypto("roin").Decrypt(expiredDate);
                _expiredDate = date + (date.Length == 8 ? "235959" : "");
            } catch (Exception ex) {
                AlertMessage("Wrong Expired Date ? " + ex.Message);
                Environment.Exit(Environment.ExitCode);
            }
            CheckDateLimit();

            this.roinMetroShell.TitleText = IniFile.IniReadValue("TitleInfo", "Title");
            this.btnPortSelection.Text = IniFile.IniReadValue("TitleInfo", "PortSelectionText");
            this.rs232PortsItem.Caption = IniFile.IniReadValue("TitleInfo", "PortSelectionCaption");
            this.lblTester.Text = IniFile.IniReadValue("TitleInfo", "TesterName");
            this.lblTestMachine.Text = IniFile.IniReadValue("TitleInfo", "TestMachineName");
            this.btnPasswordChange.Text = IniFile.IniReadValue("TitleInfo", "PasswordChange");
            this.btnTestSet.Text = IniFile.IniReadValue("TitleInfo", "TestSet");
            this.btnLabelSet.Text = IniFile.IniReadValue("TitleInfo", "LabelSet");
            this.btnAbout.Text = IniFile.IniReadValue("TitleInfo", "About");

            this.btnRs232Switch.Text = IniFile.IniReadValue("Button", "RS232Switch");
            this.btnClearCounter.Text = IniFile.IniReadValue("Button", "ClearCounter");
            this.btnStartSearchPoint.Text = IniFile.IniReadValue("Button", "StartSearchPoint");
            this.btnStopSearchPoint.Text = IniFile.IniReadValue("Button", "StopSearchPoint");
            this.btnTest.Text = IniFile.IniReadValue("Button", "Test");
            this.btnEscape.Text = IniFile.IniReadValue("Button", "Escape");
            this.btnUpload.Text = IniFile.IniReadValue("Button", "Upload");
            this.btnEditor.Text = IniFile.IniReadValue("Button", "Editor");
            this.btnReport.Text = IniFile.IniReadValue("Button", "Report");
            this.btnSingleStep.Text = IniFile.IniReadValue("Button", "SingleStep");

            this.lblStatus.Text = IniFile.IniReadValue("Label", "Status");
            this.lblFile.Text = IniFile.IniReadValue("Label", "FileName");
            this.lblNetList.Text = IniFile.IniReadValue("Label", "NetList");
            this.lblNetPair.Text = IniFile.IniReadValue("Label", "NetPair");
            this.lblTestResult.Text = IniFile.IniReadValue("Label", "TestResult");
            this.lblSearchPoint.Text = IniFile.IniReadValue("Label", "SearchPoint");
            this.lblTesterInfo.Text = IniFile.IniReadValue("Label", "TesterInfo");
            this.lblSuccess.Text = IniFile.IniReadValue("Label", "SuccessCount");
            this.lblFail.Text = IniFile.IniReadValue("Label", "FailCount");
            this.lblTotal.Text = IniFile.IniReadValue("Label", "TotalCount");
            this.lblPointNo.Text = IniFile.IniReadValue("Label", "PointNo");
            this.lblPointMap.Text = IniFile.IniReadValue("Label", "PointMap");
            this.lblOrderNo.Text = IniFile.IniReadValue("Label", "SerialNo");

            this.lblCond.Text = IniFile.IniReadValue("TesterInfo", "Conduction");
            this.lblDCHipot.Text = IniFile.IniReadValue("TesterInfo", "DCHipot");
            this.lblACHipot.Text = IniFile.IniReadValue("TesterInfo", "ACHipot");
            this.lblInst.Text = IniFile.IniReadValue("TesterInfo", "Insulation");
            this.lblDCILeakage.Text = IniFile.IniReadValue("TesterInfo", "DCILeakage");
            //this.lblDCITime.Text = IniFile.IniReadValue("TesterInfo", "DCITime");
            this.lblInterTest.Text = IniFile.IniReadValue("TesterInfo", "IntermittentTest");
            this.lblInterTime2.Text = IniFile.IniReadValue("TesterInfo", "IntermittentTime2");
            this.lblInterTime.Text = IniFile.IniReadValue("TesterInfo", "IntermittentTime");
            this.lblDCHipotTime.Text = IniFile.IniReadValue("TesterInfo", "DCHipotTime");
            this.lblACHipotTime.Text = IniFile.IniReadValue("TesterInfo", "ACHipotTime");
            this.lblWireMode.Text = IniFile.IniReadValue("TesterInfo", "WireMode");

            this.btnLabelSet.Enabled = false;
            // 是否顯示設定畫面
            //if (String.IsNullOrWhiteSpace(Util.GetProperty("TestMachine")) || String.IsNullOrWhiteSpace(Util.GetProperty("ProductName")) ||
            //    String.IsNullOrWhiteSpace(Util.GetProperty("CustomerName")) || String.IsNullOrWhiteSpace(Util.GetProperty("TestTotal"))) {
            SetTestSet();
            //}

            DisplayTestInfo();

            CreateDatabase();

            lblCustomerName.Text = Util.GetProperty("CustomerName");
            lblProductName.Text = Util.GetProperty("ProductName");
            txtOrderNo.Text = Util.GetProperty("OrderNo");

            //_failCount = Convert.ToInt32(Util.GetProperty("TestFailCount"));
            //_successCount = Convert.ToInt32(Util.GetProperty("TestSuccessCount"));
            RefreshCounter();

            this.btnRs232Switch.Focus();
        }

        private void CreateDatabase() {
            if (!File.Exists(Path.Combine(Util.GetAppPath(), Util.GetProperty("ProductName") + ".db"))) {
                try {
                    _db.CreateSQLiteDatabase(Util.GetProperty("ProductName"));
                    string createtablestring = "create table TestResult (Id INTEGER PRIMARY KEY, ProductName TEXT, OrderNo TEXT, LabelSerialNo TEXT, InsulationValue TEXT, TestResult TEXT, TestTime TEXT, ResultDesc TEXT, Operator TEXT, NETList TEXT);";
                    _db.CreateSQLiteTable(Util.GetProperty("ProductName"), createtablestring);
                } catch (Exception ex) {
                    Util.TraceInfo(ex.Message);
                }
            }
        }

        private void CheckDateLimit() {
            RegistryKey key = null;
            try {
                key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ROIN\\TIME", true);
                if (key == null) {
                    key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ROIN\\TIME");
                    key.SetValue("StartDate", DateTime.Now.ToString("yyyyMMdd") + "000000", RegistryValueKind.String);
                    //key.SetValue("EndDate", DateTime.Now.AddMonths(2).ToString("yyyyMMdd")+"235959", RegistryValueKind.String);
                    key.SetValue("EndDate", _expiredDate, RegistryValueKind.String);
                    key.SetValue("WorkDate", DateTime.Now.ToString("yyyyMMddHHmmss"), RegistryValueKind.String);
                } else {
                    key.SetValue("EndDate", _expiredDate, RegistryValueKind.String);

                    DateTime startDate = DateTime.ParseExact(key.GetValue("StartDate") as String, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                    DateTime endDate = DateTime.ParseExact(key.GetValue("EndDate") as String, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                    DateTime workDate = DateTime.ParseExact(key.GetValue("WorkDate") as String, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                    DateTime today = DateTime.Now;
                    if (today < startDate || today > endDate) {
                        MessageBox.Show(IniFile.IniReadValue("Message", "DateOverdue"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        Environment.Exit(Environment.ExitCode);
                    }
                    if (today < workDate) {
                        MessageBox.Show(IniFile.IniReadValue("Message", "DateError"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        Environment.Exit(Environment.ExitCode);
                    }
                    key.SetValue("WorkDate", DateTime.Now.ToString("yyyyMMddHHmmss"), RegistryValueKind.String);
                }
            } catch (Exception e) {
                Util.TraceInfo(string.Format("ERROR: {0}", e.Message));
                Environment.Exit(Environment.ExitCode);
            } finally {
                if (key != null) {
                    key.Close();
                }
            }
        }

        private void DisableAllButton() {
            SetButtonControl(new ButtonX[] { btnReport }, true);
            SetControlEnabled(btnSingleStep, false);
        }

        private void DisplayTestInfo() {
            this.lblTestMachineNo.Text = Util.GetProperty("TestMachine");
            this.lblOperator.Text = Util.GetProperty("Operator");
            this.lblCustomerName.Text = Util.GetProperty("CustomerName");
            this.lblProductName.Text = Util.GetProperty("ProductName");
            this.txtOrderNo.Text = Util.GetProperty("OrderNo");
        }

        private delegate void SetRs232SwitchSafe(bool isOpen);

        private void SetRs232Switch(bool isOpen) {
            if (this.InvokeRequired) {
                this.Invoke(new SetRs232SwitchSafe(SetRs232Switch), new object[] { isOpen });
            } else {
                if (isOpen) {
                    btnRs232Switch.Text = IniFile.IniReadValue("Message", "RS232CloseConnection");
                    btnRs232Switch.HoverImage = Properties.Resources.KeyStop;
                    btnRs232Switch.Image = Properties.Resources.KeyStart;
                    rs232PortsItem.Enabled = false;
                } else {
                    btnRs232Switch.Text = IniFile.IniReadValue("Message", "RS232StartConnection");
                    btnRs232Switch.HoverImage = Properties.Resources.KeyStart;
                    btnRs232Switch.Image = Properties.Resources.KeyStop;
                    rs232PortsItem.Enabled = true;
                    DisableAllButton();
                }
            }
        }

        private void InitializeRs232() {
            Rs232Controller.retryTimePeriod = Convert.ToInt32(Util.GetProperty("RetryTimePeriod"));
            Rs232Controller.commandWaitTime = Convert.ToInt32(Util.GetProperty("CommandWaitTime"));
            var deligate = new Rs232Controller.CommandProcessDeligate(CommandProcess);
            _rs232Controller = new Rs232Controller(deligate);

            List<string> nameList = new List<string>(new string[] { IniFile.IniReadValue("Message", "None") });
            nameList.AddRange(_rs232Controller.GetPortNames());
            rs232PortsItem.Items.Clear();
            rs232PortsItem.Items.AddRange(nameList.ToArray());
            int p1 = nameList.IndexOf(GetDefaultPort());
            if (p1 > -1) {
                rs232PortsItem.SelectedIndex = p1;
            }
        }

        private string GetPassword() {
            if (string.IsNullOrWhiteSpace(Util.GetProperty("Password"))) {
                SetPassword("12345678");
            }
            return _des.DecryptData(Util.GetProperty("Password"));
        }

        private void SetPassword(string password) {
            Util.SetProperty("Password", _des.EncryptData(password));
        }

        private string GetDefaultPort() {
            string port = Util.GetProperty("Rs232Port");
            if (string.IsNullOrWhiteSpace(port)) {
                string[] rs232Ports = _rs232Controller.GetPortNames();
                Util.SetProperty("Rs232Port", rs232Ports.Count() > 0 ? rs232Ports[0] : IniFile.IniReadValue("Message", "None"));
            }
            return Util.GetProperty("Rs232Port");
        }

        private void StartRs232() {
            if (ReadLicense() == null) {
                ShowMessage(IniFile.IniReadValue("Message", "LicenseNotExist"), true);
                return;
            }
            if (rs232PortsItem.Text == IniFile.IniReadValue("Message", "None")) {
                MessageBox.Show(IniFile.IniReadValue("Message", "RS232PortNotSelect"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            try {
                _pointList.Clear();
                if (_rs232Controller.Open(rs232PortsItem.Text)) {

                    SetRs232Switch(true);
                    //WriteCommandAfterLogo(Rs232Controller.W_NETLIST);
                    if (Convert.ToBoolean(Util.GetProperty("LoadFileSwitch"))) {
                        WriteCommandAfterLogo(Rs232Controller.W_DIRA);
                    } else {
                        _rs232Controller.WriteCommand(Rs232Controller.W_LOGO);
                    }
                    ShowMessage(IniFile.IniReadValue("Message", "RS232Connecting"), false);

                    _commandState = S_EMPTY;
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void CloseRs232() {
            try {
                _pointList.Clear();
                if (_rs232Controller.Close()) {
                    ShowMessage(IniFile.IniReadValue("Message", "RS232ConnectFirst"), true);
                    SetRs232Switch(false);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void Rs232PortsItem_SelectedIndexChanged(object sender, EventArgs e) {
            Util.SetProperty("Rs232Port", ((ComboBoxItem)sender).SelectedItem.ToString());
        }

        private void BtnRs232Switch_Click(object sender, EventArgs e) {
            if (_rs232Controller.IsOpen()) {
                CloseRs232();
            } else {
                _bUploadInitialized = false;
                StartRs232();
            }
            PointSearchClear();
        }

        private List<string> ReadLicense() {
            if (_deviceId == null) {
                string filePath = Path.Combine(Application.StartupPath, "license");
                if (File.Exists(filePath)) {
                    using (var fileReader = new StreamReader(filePath, true)) {
                        string[] license = _des.DecryptData(fileReader.ReadLine()).Split(',');
                        if (license.Length == 2) {
                            _deviceId = new List<string>(license);
                        }
                    }
                }
            }
            return _deviceId;
        }

        private void WriteCommandAfterLogo(string command) {
            _afterCommand = command;
            _rs232Controller.WriteCommand(Rs232Controller.W_LOGO);
        }

        private void WriteCommandAfterEscape(string command) {
            _afterCommand = command;
            _rs232Controller.WriteCommand(Rs232Controller.W_ESCAPE);
        }

        private int testDetailNo = 0;

        public void CommandProcess(string command) {
            if (command.StartsWith(Rs232Controller.R_BOOTING)) {
                _rs232Controller.StopRetry();
                _commandState = S_BEGIN_BOOTING;
            } else if (command.StartsWith(Rs232Controller.R_RESET)) {
                _rs232Controller.StopRetry();
                _commandState = S_BEGIN_BOOTING;
            } else if (command.StartsWith(Rs232Controller.R_ESCAPE)) {
                _rs232Controller.StopRetry();
                if (_commandState == S_UPLOAD_TEST_COND) {
                    UploadParameter();
                } else if (String.IsNullOrEmpty(_afterCommand)) {
                    if (_commandState == S_POINT_SEARCH) {
                        ShowMessage(IniFile.IniReadValue("Message", "StopPointSearch"), false);
                    } else if (_commandState == S_BEGIN_TEST1 || _commandState == S_BEGIN_TEST2) {
                        ShortTestClear();
                        ShowMessage(IniFile.IniReadValue("Message", "StopShortTest"), false);
                    }
                    _commandState = S_EMPTY;
                    SetButtonControl(new ButtonX[] { btnStopSearchPoint, btnEscape }, false);
                } else {
                    _rs232Controller.WriteCommand(_afterCommand);
                    _afterCommand = null;
                }
            } else if (command.StartsWith(Rs232Controller.R_WLOGO)) {
                //Util.TraceInfo("R_WLOGO=" + _commandState + " COMMAND=" + command);
                _rs232Controller.StopRetry();
                if (String.IsNullOrEmpty(_afterCommand)) {
                    if (!_bUploadInitialized) {
                        bool bFileReaded = StartUpload();
                        if (!bFileReaded || String.IsNullOrEmpty(Util.GetProperty("NetListFile"))) {
                            SetButtonControl(new ButtonX[] { btnUpload }, true);
                            ShowMessage(IniFile.IniReadValue("Message", "ReadNetListFirst"), true);
                        } else {
                            SetButtonControl(new ButtonX[] { btnUpload, btnStartSearchPoint, btnTest, btnEditor, btnReport }, true);
                            ShowMessage(IniFile.IniReadValue("Message", "InitFinished"), false);
                        }
                        _bUploadInitialized = true;
                    }
                } else {
                    _rs232Controller.WriteCommand(_afterCommand);
                    _afterCommand = null;
                }
            /// Save File for DISK Content -----------
            } else if (command.StartsWith(Rs232Controller.R_START_DIRA)) {
                _rs232Controller.StopRetry();
                _diskName = new List<string>();
                _diskNameIndex = 0;
                _commandState = S_BEGIN_DISK;
                ShowMessage(IniFile.IniReadValue("Message", "LoadFile"), false);
            } else if (_commandState == S_BEGIN_DISK) {
                _rs232Controller.StopRetry();
                if (command.StartsWith(Rs232Controller.R_LOAD)) {
                    _diskNameIndex++;
                } else if (command.StartsWith(Rs232Controller.R_END_DIRA)) {
                    if (_diskName.Count() > 0) {
                        _rs232Controller.WriteCommand(Rs232Controller.W_LOAD + _diskName[_diskNameIndex]);
                    } else {
                        _commandState = S_EMPTY;
                        _rs232Controller.WriteCommand(Rs232Controller.W_LOGO);
                    }
                } else if (command.StartsWith(Rs232Controller.R_NO_DISK) || command.StartsWith(Rs232Controller.R_NO_FILE) || command.StartsWith(Rs232Controller.R_FILE_ERROR) || command.StartsWith(Rs232Controller.R_FILE_NOT_FOUND)) {
                    // Do File load
                    if (_diskNameIndex < _diskName.Count()) {
                        _rs232Controller.WriteCommand(Rs232Controller.W_LOAD + _diskName[_diskNameIndex]);
                    } else {
                        _commandState = S_EMPTY;
                        _rs232Controller.WriteCommand(Rs232Controller.W_LOGO);
                    }
                } else if (command.StartsWith(Rs232Controller.R_FILE_OK)) {
                    // Do File load
                    _commandState = S_EMPTY;
                    _commandStateInner = S_LOAD_FILE;
                    _rs232Controller.WriteCommand(Rs232Controller.W_NETLIST);
                } else if (command.Length >= 8) {
                    _diskName.Add(command.Substring(5, command.Length - 2 - 5).PadRight(11));
                }
            } else if (command.StartsWith(Rs232Controller.R_NO_DISK) || command.StartsWith(Rs232Controller.R_NO_FILE)) {
                _rs232Controller.StopRetry();
                _commandState = S_EMPTY;
                _rs232Controller.WriteCommand(Rs232Controller.W_LOGO);
            /// ----------------------------------------
            } else if (command.StartsWith(Rs232Controller.R_READ_EMPTY)) { // T NETLIST return empty <- no read data error
                //Util.TraceInfo("R_READ_EMPTY=" + _commandState + " COMMAND=" + command);
                _rs232Controller.StopRetry();
                ShowMessage(IniFile.IniReadValue("Message", "ReadNetListFirst"), true);
            } else if (command.StartsWith(Rs232Controller.R_TEST)) {
                _rs232Controller.StopRetry();
                //Util.TraceInfo("R_TEST=" + _commandState + " COMMAND=" + command);
                if (_commandState == S_BEGIN_TEST1) {
                    _commandState = S_BEGIN_TEST2;
                    ShowMessage(IniFile.IniReadValue("Message", "StartShortTest2"), false);
                } else if (_commandState != S_BEGIN_TEST2) {
                    _commandState = S_BEGIN_TEST1;
                    ShowMessage(IniFile.IniReadValue("Message", "StartShortTest1"), false);
                    _isR9001 = false;
                }
                SetButtonControl(new ButtonX[] { btnEscape, btnTest }, true);
            } else if (_commandState == S_BEGIN_TEST2) {
                if (_testLastReceived.StartsWith(Rs232Controller.R_END) && !command.StartsWith(Rs232Controller.R_EMPTY)) {
                    _fTestingStart = false;
                    TestStartInit();
                }
                //Util.TraceInfo("S_BEGIN_TEST2=" + _commandState + " COMMAND=" + command);
                if (command.StartsWith(Rs232Controller.R_EMPTY)) {
                    _rs232Controller.StopRetry();
                    if (!_fTestingPass) {
                        // 如尚未取得 PASS 則結束測試顯示錯誤訊息
                        ShortTestResult();
                        _commandState = S_BEGIN_TEST1;
                        _fTestingStart = false;
                    }
                } else if (command.StartsWith(Rs232Controller.R_START)) {
                    _fTestingPass = false;
                    TestStartInit();
                } else if (command.StartsWith(Rs232Controller.R_END)) {
                    if (_fTestingPass) {
                        ShortTestResult();
                        _commandState = S_BEGIN_TEST1;
                        _fTestingStart = false;
                    }
                } else if (command.StartsWith(Rs232Controller.R_OPEN_ERROR)) {
                    //_testErrorList.Add(Rs232Controller.R_OPEN_ERROR);
                    TestStartInit();
                } else if (command.StartsWith(Rs232Controller.R_SHORT_ERROR)) {
                    //_testErrorList.Add(Rs232Controller.R_SHORT_ERROR);
                    TestStartInit();
                } else if (command.StartsWith(Rs232Controller.R_COND_ERROR)) {
                    TestStartInit();
                    _testErrorList.Add(Rs232Controller.R_COND_ERROR);
                } else if (command.StartsWith(Rs232Controller.R_PASS)) {
                    _fTestingPass = true;
                    ShowConsole("OK");
                } else if (command.StartsWith(Rs232Controller.R_OSC_PASS)) {
                    //PointSearchResult();
                    TestStartInit();
                } else if (command.StartsWith(Rs232Controller.R_HV_TESTING)) {
                    ShowConsole("OK");
                    ShowConsole("HV Testing");
                } else if (command.StartsWith(Rs232Controller.R_AC_HV_TESTING)) {
                    ShowConsole("OK");
                    ShowConsole("AC HV Testing");
                } else if (command.StartsWith(Rs232Controller.R_DC_LEAK_TESTING)) {
                    ShowConsole("OK");
                    ShowConsole("DC LEAK. Testing");
                } else if (command.StartsWith(Rs232Controller.R_INS_TESTING)) {
                    ShowConsole("OK");
                    ShowConsole("INS Testing");
                } else if (command.StartsWith(Rs232Controller.R_INTER1_TESTING)) {
                    ShowConsole("OK");
                    ShowConsole("INTER. 1 Testing");
                } else if (command.StartsWith(Rs232Controller.R_INTER2_TESTING)) {
                    ShowConsole("OK");
                    ShowConsole("INTER. 2 Testing");
                } else if (command.StartsWith(Rs232Controller.R_INS_FAIL)) {
                    _testErrorList.Add(Rs232Controller.R_INS_FAIL);
                } else if (command.StartsWith(Rs232Controller.R_DC_LEAK_FAIL)) {
                    _testErrorList.Add(Rs232Controller.R_DC_LEAK_FAIL);
                } else if (command.StartsWith(Rs232Controller.R_LEAK_FAIL)) {
                    _testErrorList.Add(Rs232Controller.R_LEAK_FAIL);
                } else if (command.StartsWith(Rs232Controller.R_HIPOT_TEST_FAIL)) {
                    _testErrorList.Add(Rs232Controller.R_HIPOT_TEST_FAIL);
                } else if (command.StartsWith(Rs232Controller.R_INS_MULTI_TEST_FAIL)) {
                    _testErrorList.Add(Rs232Controller.R_INS_MULTI_TEST_FAIL);
                } else if (command.StartsWith(Rs232Controller.R_INTER1_FAIL)) {
                    _testErrorList.Add(Rs232Controller.R_INTER1_FAIL);
                } else if (command.StartsWith(Rs232Controller.R_INTER2_FAIL)) {
                    _testErrorList.Add(Rs232Controller.R_INTER2_FAIL);
                } else if (command.StartsWith(Rs232Controller.R_COMP_FAIL)) {
                    ShowConsole("OK");
                    ShowConsole("Component Testing");
                    _testErrorList.Add(Rs232Controller.R_COMP_FAIL);
                } else if (command.StartsWith(Rs232Controller.R_SHORT_POINT) ||
                    command.StartsWith(Rs232Controller.R_OPEN_POINT) ||
                    command.StartsWith(Rs232Controller.R_OPEN_INTER_POINT) ||
                    command.StartsWith(Rs232Controller.R_MISWIRE_POINT) ||
                    command.StartsWith(Rs232Controller.R_INS_POINT) ||
                    command.StartsWith(Rs232Controller.R_ARC_POINT) ||
                    command.StartsWith(Rs232Controller.R_OVC_POINT) ||
                    command.StartsWith(Rs232Controller.R_OVV_POINT) ||
                    command.StartsWith(Rs232Controller.R_LEAK_POINT) ||
                    command.StartsWith(Rs232Controller.R_OVAC_POINT) ||
                    command.StartsWith(Rs232Controller.R_DCI_POINT) ||
                    command.StartsWith(Rs232Controller.R_DCARC_POINT) ||
                    command.StartsWith(Rs232Controller.R_DCOVC_POINT) ||
                    command.StartsWith(Rs232Controller.R_DCOVV_POINT) ||
                    command.StartsWith(Rs232Controller.R_COND_POINT) || 
                    command.StartsWith(Rs232Controller.R_SINK_POINT) ||
                    Rs232Controller.R_RESISTANCE_POINT.IsMatch(command) ||
                    Rs232Controller.R_CAPACITY_POINT.IsMatch(command) ||
                    Rs232Controller.R_DIODE_POINT.IsMatch(command)
                    ) {
                    _pointList.Add(command.Substring(1));
                } else if (command.StartsWith(Rs232Controller.R_ID)) {
                    string deviceId = command.Substring(Rs232Controller.R_ID.Length, 11);
                    //Util.TraceInfo("R_ID=" + _commandState + " COMMAND=" + command + " License=" + ReadLicense());
                    if (!ReadLicense().Contains("99999999999") && !ReadLicense().Contains(deviceId)) {
                        CloseRs232();
                        ShowMessage(IniFile.IniReadValue("Message", "LicenseNotExist"), true);
                        _commandState = S_EMPTY;
                    }
                } else if (command.Length > 5 && int.TryParse(command.Substring(1, 3), out testDetailNo)) {
                    // test detail number
                    string[] detail = command.Split(' ');
                    if (detail.Length > 2) {
                        _testDetailList.Add(new TestDetailData { No = testDetailNo, Name = detail[1], Data = detail[2].Substring(0, detail[2].Length - 2).Replace("o", "") });
                    }
                }
            } else if (command.StartsWith(Rs232Controller.R_SEARCH) && _commandState == S_EMPTY) {
                //Util.TraceInfo("S_EMPTY=" + _commandState + " COMMAND=" + command);
                _rs232Controller.StopRetry();
                _commandState = S_POINT_SEARCH;
                ShowMessage(IniFile.IniReadValue("Message", "StartPointSearch"), false);
                SetButtonControl(new ButtonX[] { btnStopSearchPoint }, true);
            } else if (_commandState == S_POINT_SEARCH) {
                //Util.TraceInfo("S_POINT_SEARCH=" + _commandState + " COMMAND=" + command);
                if (command.StartsWith(Rs232Controller.R_POINT_SEARCH)) {
                    PointSearchClear();
                } else if (command.StartsWith(Rs232Controller.R_POINT_END)) {
                    PointSearchResult();
                } else {
                    _pointList.Add(command);
                }
            } else if (command.StartsWith(Rs232Controller.R_DATALIST)) {
                _rs232Controller.StopRetry();
                _commandState = S_BEGIN_NETLIST;
                if (_commandStateInner == S_LOAD_FILE) {
                    _net4save.Clear();
                    _net4save.Add("---TestCondition--------");
                    _net4save.Add("");
                }
            } else if (_commandState == S_BEGIN_NETLIST) {
                if (_commandStateInner == S_NETLIST_CONT) {
                    _netString += command.Substring(1, command.Length - 3);
                    if (_netString.EndsWith("FF")) {
                        _commandStateInner = S_LOAD_FILE;

                        //var oss = _netString.Substring(0, _netString.Length - 2).Split(new string[] { "FE" }, StringSplitOptions.None);
                        //foreach (string os in oss) {
                        //    if (os != "") {
                        //        _net4save.Add(string.Join("-", Enumerable.Range(0, os.Length / 3).Select(i => os.Substring(i * 3, 3))));
                        //    }
                        //}
                        _netString = "";
                    }
                } else if (_commandStateInner == S_NETLIST_COND) {
                    _netString = command.Substring(1, command.Length - 3);
                    if (_netString.EndsWith("FF")) {
                        _commandStateInner = S_LOAD_FILE;
                        _netString = "";
                    } else {
                        _net4save.Add(_netString.Substring(0, 3) + "-" + _netString.Substring(3, 3));
                    }
                } else if (_commandStateInner == S_NETLIST_RES) {
                    if (command.StartsWith(Rs232Controller.R_ENDTBL)) {
                        _commandStateInner = S_LOAD_FILE;
                    } else {
                        _net4save.Add(command.Substring(1, command.Length - 3));
                    }
                } else if (_commandStateInner == S_NETLIST_CAP) {
                    if (command.StartsWith(Rs232Controller.R_ENDTBL)) {
                        _commandStateInner = S_LOAD_FILE;
                    } else {
                        _net4save.Add(command.Substring(1, command.Length - 3));
                    }
                } else if (_commandStateInner == S_NETLIST_DIODE) {
                    if (command.StartsWith(Rs232Controller.R_ENDTBL)) {
                        _commandStateInner = S_LOAD_FILE;
                    } else {
                        _net4save.Add(command.Substring(1, command.Length - 3));
                    }
                } else {
                    if (command.StartsWith(Rs232Controller.R_INFO_COND)) {
                        var net = SetTestInfoData(Rs232Controller.R_INFO_COND, command);
                        if (_commandStateInner == S_LOAD_FILE) {
                            _net4save.Add(IniFile.IniReadValue("NETLIST", "NET1") + " =" + net.Split('Ω')[0]);
                        }
                    } else if (command.StartsWith(Rs232Controller.R_INFO_INS)) {
                        var net = SetTestInfoData(Rs232Controller.R_INFO_INS, command);
                        if (_commandStateInner == S_LOAD_FILE) {
                            _net4save.Add(IniFile.IniReadValue("NETLIST", "NET3") + " =" + net.Split('Ω')[0]);
                        }
                    } else if (command.StartsWith(Rs232Controller.R_INFO_DCHIPOT)) {
                        var net = SetTestInfoData(Rs232Controller.R_INFO_DCHIPOT, command);
                        if (_commandStateInner == S_LOAD_FILE) {
                            _net4save.Add(IniFile.IniReadValue("NETLIST", "NET2") + " =" + net);
                        }
                    } else if (command.StartsWith(Rs232Controller.R_INFO_DCTIME)) {
                        var net = SetTestInfoData(Rs232Controller.R_INFO_DCTIME, command);
                        if (_commandStateInner == S_LOAD_FILE) {
                            _net4save.Add(IniFile.IniReadValue("NETLIST", "NET4") + " =" + net);
                        }
                    } else if (command.StartsWith(Rs232Controller.R_INFO_ACHIPOT)) {
                        var net = SetTestInfoData(Rs232Controller.R_INFO_ACHIPOT, command);
                        if (_commandStateInner == S_LOAD_FILE) {
                            _net4save.Add(IniFile.IniReadValue("NETLIST", "NET5") + " =" + net);
                        }
                    } else if (command.StartsWith(Rs232Controller.R_INFO_ACTIME)) {
                        var net = SetTestInfoData(Rs232Controller.R_INFO_ACTIME, command);
                        if (_commandStateInner == S_LOAD_FILE) {
                            _net4save.Add(IniFile.IniReadValue("NETLIST", "NET7") + " =" + net);
                        }
                    } else if (command.StartsWith(Rs232Controller.R_INFO_LEAKAGE)) {
                        var net = SetTestInfoData(Rs232Controller.R_INFO_LEAKAGE, command);
                        if (_commandStateInner == S_LOAD_FILE) {
                            _net4save.Add(IniFile.IniReadValue("NETLIST", "NET6") + " =" + net);
                        }
                    } else if (command.StartsWith(Rs232Controller.R_INFO_DCI_LEAKAGE)) {
                        SetTestInfoData(Rs232Controller.R_INFO_DCI_LEAKAGE, command);
                    } else if (command.StartsWith(Rs232Controller.R_INFO_DCI_TIME)) {
                        SetTestInfoData(Rs232Controller.R_INFO_DCI_TIME, command);
                    } else if (command.StartsWith(Rs232Controller.R_INFO_INTERMITTENT)) {
                        var net = SetTestInfoData(Rs232Controller.R_INFO_INTERMITTENT, command);
                        if (_commandStateInner == S_LOAD_FILE) {
                            _net4save.Add(IniFile.IniReadValue("NETLIST", "NET8") + " =" + net);
                        }
                    } else if (command.StartsWith(Rs232Controller.R_INFO_INTER_TEST1)) {
                        SetTestInfoData(Rs232Controller.R_INFO_INTER_TEST1, command);
                    } else if (command.StartsWith(Rs232Controller.R_INFO_INTER_TEST2)) {
                        var net = SetTestInfoData(Rs232Controller.R_INFO_INTER_TEST2, command);
                        if (_commandStateInner == S_LOAD_FILE) {
                            _net4save.Add(IniFile.IniReadValue("NETLIST", "NET9") + " =" + net);
                        }
                    } else if (command.StartsWith(Rs232Controller.R_INFO_WIRE_MODE)) {
                        SetTestInfoData(Rs232Controller.R_INFO_WIRE_MODE, command);
                    } else if (_commandStateInner == S_LOAD_FILE && command.StartsWith(Rs232Controller.R_CONT)) {
                        _commandStateInner = S_NETLIST_CONT;
                        _net4save.Add("");
                        _net4save.Add("---NetList--------------");
                        _net4save.Add("");
                        _netString = "";
                    } else if (_commandStateInner == S_LOAD_FILE && command.StartsWith(Rs232Controller.R_COND)) {
                        _commandStateInner = S_NETLIST_COND;
                    } else if (_commandStateInner == S_LOAD_FILE && command.StartsWith(Rs232Controller.R_RESTBL)) {
                        _commandStateInner = S_NETLIST_RES;
                        if (_netString == "") {
                            _net4save.Add("");
                            _net4save.Add("---Component------------");
                            _net4save.Add("");
                            _netString = "RES";
                        }
                    } else if (_commandStateInner == S_LOAD_FILE && command.StartsWith(Rs232Controller.R_CAPTBL)) {
                        _commandStateInner = S_NETLIST_CAP;
                        if (_netString == "") {
                            _net4save.Add("");
                            _net4save.Add("---Component------------");
                            _net4save.Add("");
                            _netString = "CAP";
                        }
                    } else if (_commandStateInner == S_LOAD_FILE && command.StartsWith(Rs232Controller.R_DIODETBL)) {
                        _commandStateInner = S_NETLIST_DIODE;
                        if (_netString == "") {
                            _net4save.Add("");
                            _net4save.Add("---Component------------");
                            _net4save.Add("");
                            _netString = "DIODE";
                        }
                    } else if (command.StartsWith(Rs232Controller.R_END_NETLIST)) {
                        if (_net4save.Count() > 0) {
                            try {
                                Util.WriteFile(_net4save, System.IO.Path.Combine(Application.StartupPath, _diskName[_diskNameIndex - 1].Trim() + ".txt"));
                            } catch (Exception e) {
                                Util.TraceInfo("Load File Error: " + e.Message);
                            }
                        }
                        if (_commandStateInner != null) {
                            _commandStateInner = null;
                            if (_diskNameIndex < _diskName.Count()) {
                                //Logger.Info(_diskNameIndex + " " + _diskName.Count() + " " + _commandState + " COMMAND=" + command);
                                _commandState = S_BEGIN_DISK;
                                _rs232Controller.WriteCommand(Rs232Controller.W_LOAD + _diskName[_diskNameIndex]);
                            } else {
                                _commandState = S_EMPTY;
                                _rs232Controller.WriteCommand(Rs232Controller.W_LOGO);
                            }
                        }
                    }
                }
            } else if (_commandState == S_BEGIN_TEST1) {
                //Util.TraceInfo("S_BEGIN_TEST1=" + _commandState + " COMMAND="+command);
                
                    /*
                    } else if (command.StartsWith(Rs232Controller.R_EMPTY)) {
                        _rs232Controller.StopRetry();
                        ShortTestClear();
                        ShowMessage(IniFile.IniReadValue("Message", "StartShortTest2"), false);
                        await Task.Delay(300);
                        _rs232Controller.WriteCommand(Rs232Controller.W_TEST);
                    }
                    */
                //} else 
                if (command.StartsWith(Rs232Controller.R_START)) {
                    _commandState = S_BEGIN_TEST2;
                    _rs232Controller.StopRetry();
                    ShortTestClear();
                    ShowMessage(IniFile.IniReadValue("Message", "StartShortTest2"), false);
                } else if (_testLastReceived.StartsWith(Rs232Controller.R_TEST) && command.StartsWith(Rs232Controller.R_EMPTY)) {
                    // 顯示空測訊息
                    ShortTestClear();
                    _testReportResultText = "Empty";
                    ShortTestResult();
                }
            } else if (command.StartsWith(Rs232Controller.R_P_BEGIN)) {
                //Util.TraceInfo("R_P_BEGIN=" + _commandState + " COMMAND=" + command);
                UploadData();
            } else if (command.StartsWith(Rs232Controller.R_P_END)) {
                //Util.TraceInfo("R_P_END=" + _commandState + " COMMAND=" + command);
                SetButtonControl(new ButtonX[] { btnUpload, btnStartSearchPoint, btnTest, btnEditor, btnReport }, true);
                _rs232Controller.WriteCommand(Rs232Controller.W_ESC);
                Util.SetProperty("NetListFile", _uploadData.fileName);
                
                _paramList = _uploadData.paramMap.Keys.ToList();
                if (_paramList.Count > 0) {
                    _commandState = S_UPLOAD_TEST_COND; // set the flag for setting parameter
                } else {
                    _commandState = S_EMPTY;
                    ShowMessage(IniFile.IniReadValue("Message", "UploadNetListFinished"), false);
                }
            } else if (_commandState == S_UPLOAD_NETLIST && command.StartsWith("$P")) {
                //Util.TraceInfo("$P=" + _commandState + " COMMAND=" + command);
                if (!UploadData()) {
                    _rs232Controller.StopRetry();
                    ShowMessage(IniFile.IniReadValue("Message", "UploadNetListFinished"), false);
                }
            } else if (command.StartsWith(Rs232Controller.R_ID)) {
                //Util.TraceInfo("R_ID=" + _commandState + " COMMAND=" + command);
                string deviceId = command.Substring(Rs232Controller.R_ID.Length, 11);
                if (deviceId != "99999999999" && !ReadLicense().Contains(deviceId)) {
                    _rs232Controller.DeactiveDevice();
                    ShowMessage(IniFile.IniReadValue("Message", "LicenseNotExist"), true);
                }
            } else if (!string.IsNullOrEmpty(_uploadTestCondition) && command.StartsWith("$"+_uploadTestCondition)) {
                _rs232Controller.StopRetry();
                _paramList.Remove(_uploadTestCondition.Substring(1).Split(':')[0].TrimEnd());
                _uploadTestCondition = null;
                UploadParameter();
            } else if (command.StartsWith(Rs232Controller.R_REMPTY) || command.StartsWith(Rs232Controller.R_CEMPTY) || command.StartsWith(Rs232Controller.R_DEMPTY)) {
                if (!UploadData()) {
                    _rs232Controller.StopRetry();
                    ShowMessage(IniFile.IniReadValue("Message", "UploadNetListFinished"), false);
                }
            } else if (_commandState == S_UPLOAD_NETLIST || _commandState == S_UPLOAD_RESISTANCE || _commandState == S_UPLOAD_CAPACITY || _commandState == S_UPLOAD_DIODE) {
                if (command.StartsWith("$P") || command.StartsWith("$R") || command.StartsWith("$C") || command.StartsWith("$D")) {
                    if (!UploadData()) {
                        _rs232Controller.StopRetry();
                        ShowMessage(IniFile.IniReadValue("Message", "UploadNetListFinished"), false);
                    }
                }
            }
            _testLastReceived = command; // 暫存TESTING最後的接收COMMAND (為了判斷EMPTY前是否為END)
        }

        private void TestStartInit() {
            if (!_fTestingStart) {
                _rs232Controller.StopRetry();
                ShortTestClear();
                ShowMessage(IniFile.IniReadValue("Message", "StartShortTest2"), false);
                ShowConsole("O/S/C Testing");
                _fTestingStart = true;
                _fTestingPass = false;
            }
        }

        private string GetInfoData(string command) {
            string data = command.Split(':')[1];
            return data.Substring(0, data.Length - 2);
        }

        private delegate string SetTestInfoDataSafe(string command, string data);

        private string SetTestInfoData(string command, string data) {
            string ret = null;
            if (this.InvokeRequired) {
                return (string) this.Invoke(new SetTestInfoDataSafe(SetTestInfoData), new object[] { command, data });
            } else {
                switch (command) {
                    case Rs232Controller.R_INFO_COND:
                        ret = lblCondData.Text = GetInfoData(data).Replace('o', 'Ω');
                        break;
                    case Rs232Controller.R_INFO_INS:
                        ret = lblInstData.Text = GetInfoData(data).Replace('o', 'Ω');
                        break;
                    case Rs232Controller.R_INFO_DCHIPOT:
                        ret = lblDCHipotData.Text = GetInfoData(data);
                        break;
                    case Rs232Controller.R_INFO_DCTIME:
                        ret = lblDCHipotTimeData.Text = _isR9001 ? "" : GetInfoData(data);
                        break;
                    case Rs232Controller.R_INFO_ACHIPOT:
                        ret = lblACHipotData.Text = GetInfoData(data);
                        break;
                    case Rs232Controller.R_INFO_ACTIME:
                        ret = lblACHipotTimeData.Text = GetInfoData(data);
                        break;
                    case Rs232Controller.R_INFO_LEAKAGE:
                        ret = lblDCILeakageData.Text = GetInfoData(data);
                        break;
                    //case Rs232Controller.R_INFO_DCI_LEAKAGE:
                        //lblDCILeakageData.Text = GetInfoData(data);
                        //break;
                    //case Rs232Controller.R_INFO_DCI_TIME:
                        //lblDCITimeData.Text = GetInfoData(data);
                        //break;
                    case Rs232Controller.R_INFO_INTERMITTENT:
                        ret = lblInterTestData.Text = GetInfoData(data);
                        break;
                    case Rs232Controller.R_INFO_INTER_TEST1:
                        ret = lblInterTimeData.Text = GetInfoData(data);
                        break;
                    case Rs232Controller.R_INFO_INTER_TEST2:
                        ret = lblInterTime2Data.Text = GetInfoData(data);
                        break;
                    case Rs232Controller.R_INFO_WIRE_MODE:
                        ret = lblWireModeData.Text = GetInfoData(data);
                        break;
                }
            }
            return ret;
        }

        private delegate void PointSearchClearSafe();

        private void PointSearchClear() {
            if (this.InvokeRequired) {
                this.Invoke(new PointSearchClearSafe(PointSearchClear), new object[] { });
            } else {
                _pointList.Clear();
                txtPointSearch1.Text = txtPointSearch2.Text = "";
            }
        }

        private delegate void PointSearchResultSafe();

        private void PointSearchResult() {
            if (this.InvokeRequired) {
                this.Invoke(new PointSearchResultSafe(PointSearchResult), new object[] { });
            } else {
                List<string> pinList = new List<string>();
                List<string> mapList = new List<string>();
                foreach (string point in _pointList) {
                    string pinNumber = ConvertPinNumber(point.Substring(1, 3));
                    string pinMap = "";
                    pinList.Add((Convert.ToInt32(pinNumber) + 1).ToString());
                    if (_uploadData.netMap.ContainsKey(pinNumber)) {
                        pinMap = _uploadData.netMap[pinNumber];
                    } else {
                        pinMap = IniFile.IniReadValue("Message", "Unknow");
                    }
                    mapList.Add(pinMap);
                }
                txtPointSearch1.Text = String.Join("-", mapList);
                txtPointSearch2.Text = String.Join("-", pinList);
            }
        }

        private delegate void ShortTestClearSafe();

        private void ShortTestClear() {
            if (this.InvokeRequired) {
                this.Invoke(new ShortTestClearSafe(ShortTestClear), new object[] { });
            } else {
                _pointList.Clear();
                _shortList.Clear();
                SetControlEnabled(btnSingleStep, false);
                btnSingleStep.Text = IniFile.IniReadValue("Button", "SingleStep");
                txtTestResult.Text = lblMapResult1.Text = lblMapResult2.Text = lblTestResult1.Text = lblTestResult2.Text = "";
                txtConsole.Clear();
                _testErrorList = new List<string>();
                _fTestingFinish = false;
                _testDetailList = new List<TestDetailData>();
                _testReportResultText = "";
            }
        }

        private delegate void ShortTestResultSafe();

        private void ShortTestResult() {
            if (this.InvokeRequired) {
                this.Invoke(new ShortTestResultSafe(ShortTestResult), new object[] { });
            } else {
                if (_testReportResultText == "Empty") {
                    txtTestResult.ForeColor = Color.Orange;
                    txtTestResult.Text = IniFile.IniReadValue("Message", "Empty");
                    return;
                }
                Util.TraceInfo(string.Format("ShortTestCount = {0}", _pointList.Count()));
                string resultDesc = "", reportResultDesc = "";
                bool bPrinting = false;

                if (_pointList.Count() == 0 && _testErrorList.Count() == 0) {
                    _successCount++;
                    _testReportResultText = "PASS";
                    txtTestResult.ForeColor = Color.Green;
                    txtTestResult.Text = IniFile.IniReadValue("Message", "Pass");
                    ShowMessage(IniFile.IniReadValue("Message", "WaitShortTest"), false);
                    ShowConsole("OK");
                    // 印表
                    if (Util.GetProperty("PrinterActive") == "1") {
                        bPrinting = true;
                    }
                } else {
                    _failCount++;
                    int shortCount = 0, openCount = 0, miswireCount = 0;
                    Dictionary<string, List<string>> osInfo = new Dictionary<string, List<string>>();
                    //List<string> shortList = new List<string>();
                    foreach (string point in _pointList) {
                        string[] pInfo = point.Split(':');
                        string[] points, comps;
                        switch (point[0]) {
                            case 'R':
                                // Resistance Error connector
                                comps = pInfo[1].Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                                if (comps[1].IndexOf('*') != -1) {
                                    pInfo[0] = "RO"; // 電阻開路
                                } else if (comps[1].IndexOf('ê') != -1 || comps[1].IndexOf('K') != -1 || comps[1].IndexOf('M') != -1) {
                                    pInfo[0] = "RF"; // 阻值錯誤
                                } else {
                                    continue;
                                }
                                points = comps[0].Split('-');
                                _shortList.Add(new string[] { points[0], points.Length > 1 ? points[1] : "", pInfo[0] });
                                break;
                            case 'C':
                                // Capacity Error connector
                                comps = pInfo[1].Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                                if (comps[1].StartsWith("Short")) {
                                    pInfo[0] = "CS"; // 電容短路
                                } else if (comps[1].IndexOf("Err") > -1) {
                                    pInfo[0] = "CR"; // 電容反向
                                } else if (comps[1].IndexOf("F") > -1 || comps[1].IndexOf('u') != -1 || comps[1].IndexOf('n') != -1 || comps[1].IndexOf('p') != -1) {
                                    pInfo[0] = "CF"; // 電容錯誤
                                } else {
                                    continue;
                                }
                                points = comps[0].Split('-');
                                _shortList.Add(new string[] { points[0], points.Length > 1 ? points[1] : "", pInfo[0] });
                                break;
                            case 'D':
                                // Diode Error connector
                                comps = pInfo[1].Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                                if (comps[1].StartsWith("Open")) {
                                    pInfo[0] = "DO"; // 二極體開路
                                } else if (comps[1].StartsWith("Reversed")) {
                                    pInfo[0] = "DR"; // 二極體反向
                                } else if (comps[1].IndexOf("Sht") > -1) {
                                    pInfo[0] = "DS"; // 二極體短路
                                } else if (comps[1].IndexOf('V') != -1) {
                                    pInfo[0] = "DF"; // 二極體電壓錯誤
                                } else {
                                    continue;
                                }
                                points = comps[0].Split('-');
                                _shortList.Add(new string[] { points[0], points.Length > 1 ? points[1] : "", pInfo[0] });
                                break;
                            default:
                                // O/S/C Error connector
                                string pInfoContent = pInfo[1].Replace("   ****", "");
                                pInfoContent = pInfoContent.Substring(0, pInfoContent.Length - 2).ToUpper();
                                points = pInfoContent.Split('-');
                                _shortList.Add(new string[] { points[0], points.Length > 1 ? points[1] : "", pInfo[0] });

                                // O/S Test Count
                                if (pInfo[0] == "O") { openCount++; } else if (pInfo[0] == "M") { miswireCount++; } else if (pInfo[0] == "S") { shortCount++; }

                                // 記錄資料庫報表顯示之OS錯誤訊息點號
                                if (!osInfo.ContainsKey(pInfo[0])) {
                                    osInfo[pInfo[0]] = new List<string>();
                                }
                                // 限制顯示五個點號
                                if (osInfo[pInfo[0]].Count() < 5) {
                                    osInfo[pInfo[0]].Add(pInfoContent);
                                }
                                break;
                        }
                    }

                    DisplayTestResult(_testStepCount = 0);
                    SetControlEnabled(btnSingleStep, true);

                    if (_testErrorList.Count > 0) {
                        reportResultDesc = resultDesc = string.Format(IniFile.IniReadValue("Message",
                            _testErrorList.Contains(Rs232Controller.R_COND_ERROR) ? "TestResult1" : (
                            _testErrorList.Contains(Rs232Controller.R_INS_FAIL) ? "TestResult2" : (
                            _testErrorList.Contains(Rs232Controller.R_DC_LEAK_FAIL) ? "TestResult3" : (
                            _testErrorList.Contains(Rs232Controller.R_LEAK_FAIL) ? "TestResult4" : (
                            _testErrorList.Contains(Rs232Controller.R_HIPOT_TEST_FAIL) ? "TestResult5" : (
                            _testErrorList.Contains(Rs232Controller.R_INS_MULTI_TEST_FAIL) ? "TestResult6" : (
                            (_testErrorList.Contains(Rs232Controller.R_INTER1_FAIL) || _testErrorList.Contains(Rs232Controller.R_INTER2_FAIL)) ? "TestResult7" : 
                            _testErrorList.Contains(Rs232Controller.R_COMP_FAIL) ? "TestResult8" : "Fail"))))))));
                        _testReportResultText = _testErrorList.Contains(Rs232Controller.R_COND_ERROR) ? "Cond" : (
                            (_testErrorList.Contains(Rs232Controller.R_INTER1_FAIL) || _testErrorList.Contains(Rs232Controller.R_INTER2_FAIL)) ? "Inter" : _testReportResultText);

                        if (_shortList.Count() == 0 && _testErrorList.Contains(Rs232Controller.R_COND_ERROR)) {
                            // 導通不良顯示中間錯誤訊息
                            txtTestResult.ForeColor = Color.Red;
                            txtTestResult.Text = IniFile.IniReadValue("Message", "Cond");
                        }
                    } else {
                        // O/S Test Result
                        if (shortCount > 0 || openCount > 0 || miswireCount > 0) {
                            resultDesc = string.Format(IniFile.IniReadValue("Message", "TestResult"), shortCount, openCount, miswireCount);
                            // 報表顯示使用
                            List<string> osResult = new List<string>();
                            osResult.Add(shortCount > 0 ? string.Format(IniFile.IniReadValue("Message", "OSResult1"), shortCount, string.Join(",", osInfo["S"])) : "");
                            osResult.Add(openCount > 0 ? string.Format(IniFile.IniReadValue("Message", "OSResult2"), openCount, string.Join(",", osInfo["O"])) : "");
                            osResult.Add(miswireCount > 0 ? string.Format(IniFile.IniReadValue("Message", "OSResult3"), miswireCount, string.Join(",", osInfo["M"])) : "");
                            reportResultDesc = string.Join(", ", osResult.Where(x => !string.IsNullOrEmpty(x)));
                        }
                    }
                    ShowMessage(resultDesc, true);
                    ShowConsole("Fail");
                }

                // 新增或變更資料庫測試資料

                DateTime dateTime = DateTime.Now;
                string date = dateTime.ToString("yyyy/MM/dd");
                string time = dateTime.ToString("HH:mm:ss");
                string netListJson = JsonConvert.SerializeObject(_testDetailList);
                string orderNo = Util.GetProperty("OrderNo");
                _labelSerialNo = bPrinting ? GetLabelSerialNo() : "";

                string countSql = string.Format("select count(*) from TestResult where ProductName='{0}' and OrderNo='{1}'", lblProductName.Text, orderNo);
                string count = _db.SQLiteExecuteScalar(Util.GetProperty("ProductName"), countSql);
                if (count == "0") {
                    string insertSql = string.Format("insert into TestResult (ProductName, OrderNo, LabelSerialNo, InsulationValue, TestResult, TestTime, ResultDesc, Operator, NETList) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}');", lblProductName.Text, orderNo, _labelSerialNo, lblInstData.Text, _testReportResultText, date + " " + time, reportResultDesc, Util.GetProperty("Operator"), netListJson);
                    _db.SQLiteExecuteNonQuery(Util.GetProperty("ProductName"), insertSql);
                } else {
                    string updateSql = string.Format("update TestResult set InsulationValue='{0}', TestResult='{1}', TestTime='{2}', ResultDesc='{3}', Operator='{4}', NETList='{5}', LabelSerialNo='{6}' where ProductName='{7}' and OrderNo='{8}'", lblInstData.Text, _testReportResultText, date + " " + time, reportResultDesc, Util.GetProperty("Operator"), netListJson, _labelSerialNo, lblProductName.Text, orderNo);
                    _db.SQLiteExecuteNonQuery(Util.GetProperty("ProductName"), updateSql);
                }

                // 印表
                if (bPrinting) {
                    PrintLabel();
                }

                this.txtOrderNo.Text = orderNo = (Convert.ToInt32(orderNo) + 1).ToString();
                Util.SetProperty("OrderNo", orderNo);

                RefreshCounter();

                OutputTestInfo(reportResultDesc);
            }
        }

        // Write Test Output Info to CSV
        void OutputTestInfo(string resultDesc) {
            var barcode = "P" + Util.GetProperty("LabelProduct") + ":S" + GetLabelSerialNo();

            var fileName = barcode.Replace(":", "") + "T" + DateTime.Now.ToString("yyyyMMddHHmm") + ".txt";
            var path = Path.Combine(Util.GetProperty("FileOutputDir"), fileName);
            Directory.CreateDirectory(Util.GetProperty("FileOutputDir"));

            var ls = new List<string>();
            ls.Add("Barcode: " + barcode); // 產品上線前臨時條碼，工單號+流水號（可重複使用的條碼）
            ls.Add("DateTime: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")); // 測試時間
            ls.Add("WorkOrder: " + Util.GetProperty("ProductName")); // 生產訂單號
            ls.Add("Equipment: " + Util.GetProperty("TestMachine")); // 電測機編號
            ls.Add("Cond: " + lblCondData.Text);
            ls.Add("Hipod V: " + lblDCHipotData.Text);
            ls.Add("Ins: " + lblInstData.Text);
            ls.Add("HV Time: " + lblDCHipotTimeData.Text);
            ls.Add("Result: " + (string.IsNullOrEmpty(resultDesc) ? "PASS" : "Fail")); // PASS or Fail
            ls.Add("Fail reason: " + resultDesc); // 點位，不良原因
            try {
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None)) {
                    using (StreamWriter srOutFile = new StreamWriter(fs, Encoding.Default)) {
                        foreach (string s in ls) {
                            srOutFile.WriteLine(s);
                            srOutFile.Flush();
                        }
                        srOutFile.Close();
                    }
                }
            } catch (Exception ex) {
                Util.TraceInfo(ex.Message);
            }
        }

        private Dictionary<string, string> testPointName = new Dictionary<string, string> {
            { "S", "Short" }, { "O", "Open" }, { "M", "Miswire" }, { "I", "Ins" }, { "Arc" , "Arc" }, { "Ovc", "Ovc" }, { "Ovv", "Ovv" }, { "c", "Cond" },
            { "Leak", "Leak" }, { "Ovac", "Ovac" }, { "Dci", "Dci" }, { "DcArc", "DcArc" }, { "DcOvc", "DcOvc" }, { "DcOvv", "DcOvv" }, { "o", "OpenInter" },
            { "Sink", "Sink" }, { "RO", "Open" }, { "RF", "ResistanceFail" }, { "CF", "CapacityFail" }, { "CS", "Short" }, { "CR", "Reversed" },
            { "DF", "VoltageFail" }, { "DR", "Reversed" }, { "DO", "Open" }, { "DS", "Short" }
        };

        private void DisplayTestResult(int index) {
            if (_shortList.Count() <= index) {
                return;
            }
            string[] testList = new string[] { ConvertPinNumber(_shortList[index][0]), ConvertPinNumber(_shortList[index][1]), _shortList[index][2] };
            string[] mapList = new string[] {
                _uploadData.netMap.ContainsKey(testList[0]) ? _uploadData.netMap[testList[0]] : IniFile.IniReadValue("Message", "Unknow"),
                _uploadData.netMap.ContainsKey(testList[1]) ? _uploadData.netMap[testList[1]] : IniFile.IniReadValue("Message", "Unknow")
            };
            _testReportResultText = testPointName[testList[2]];
            txtTestResult.ForeColor = Color.Red;
            txtTestResult.Text = IniFile.IniReadValue("Message", _testReportResultText);
            lblMapResult1.Text = mapList[0];
            lblMapResult2.Text = mapList[1];
            lblTestResult1.Text = (Convert.ToInt32(testList[0]) + 1).ToString();
            lblTestResult2.Text = string.IsNullOrEmpty(testList[1]) ? "" : (Convert.ToInt32(testList[1]) + 1).ToString();
            btnSingleStep.Text = IniFile.IniReadValue("Button", "SingleStep") + "(" + (index + 1) + " / " + _shortList.Count() + ")";
        }

        private int[] specialPoints = new int[] { Convert.ToInt32("E0", 16), Convert.ToInt32("E2", 16), Convert.ToInt32("F7", 16), Convert.ToInt32("E5", 16), Convert.ToInt32("F2", 16), Convert.ToInt32("EC", 16) };
        private string[] specialString = { "α", "β", "π", "δ", "θ", "ψ" };

        private string ConvertPinNumber(string point) {
            if (String.IsNullOrEmpty(point)) {
                return "";
            }
            point = point.ToUpper();
            char lead = point[0];
            int specialIndex = Array.IndexOf(specialPoints, lead);
            if (specialIndex > -1) {
                point = ((specialIndex + 26) * 64 + Convert.ToInt32(point.Substring(1, 2)) - 1).ToString();
            } else if (lead >= 'A' && lead <= 'Z') {
                point = (((int)lead - (int)'A') * 64 + Convert.ToInt32(point.Substring(1, 2)) - 1).ToString();
            }
            return point;
        }

        private string ConvertNumberPin(string number) {
            if (String.IsNullOrEmpty(number)) {
                return "";
            }
            int num = Convert.ToInt32(number);
            int headNum = num / 64;
            string headChar = headNum >= 26 ? specialString[headNum - 26] : Convert.ToChar(headNum + Convert.ToInt32('A')).ToString();
            string point = headChar + ((num % 64) + 1).ToString("D2");
            
            return point;
        }

        private string ConvertNumberPinList(List<string> numbers) {
            List<string> pinList = new List<string>();
            foreach (string number in numbers) {
                pinList.Add(ConvertNumberPin(number));
            }
            return string.Join("-", pinList);
        }

        public delegate void ControlEnabled(Control control, bool enabled);

        public void SetControlEnabled(Control control, bool enabled) {
            if (control.InvokeRequired) {
                control.Invoke(new ControlEnabled(SetControlEnabled), new object[] { control, enabled });
            } else {
                control.Enabled = enabled;
            }
        }

        public delegate void ShowMessageSafe(string message, bool errorFlag);

        public void ShowMessage(string message, bool errorFlag) {
            if (TxtMessageBox.InvokeRequired) {
                TxtMessageBox.Invoke(new ShowMessageSafe(ShowMessage), new object[] { message, errorFlag });
            } else {
                TxtMessageBox.Text = message;
                TxtMessageBox.SelectAll();
                TxtMessageBox.SelectionAlignment = HorizontalAlignment.Center;
                TxtMessageBox.SelectionColor = errorFlag ? Color.Red : Color.ForestGreen;
                int index = message.IndexOf("RESET按鍵", 0);
                if (index != -1) {
                    TxtMessageBox.SelectionStart = index;
                    TxtMessageBox.SelectionLength = "RESET按鍵".Length;
                    TxtMessageBox.SelectionColor = Color.Red;
                }
            }
        }

        public delegate void ShowConsoleSafe(string message);

        public void ShowConsole(string message) {
            if (txtConsole.InvokeRequired) {
                txtConsole.Invoke(new ShowConsoleSafe(ShowConsole), new object[] { message });
            } else {
                if (message.EndsWith("Testing")) {
                    message += new string(' ', 25 - message.Length) + "..... ";
                    txtConsole.AppendText(message);
                    _fTestingFinish = false;
                } else if (!_fTestingFinish && message == "OK") {
                    message += Environment.NewLine;
                    txtConsole.AppendText(message, Color.Green);
                    _fTestingFinish = true;
                } else if (!_fTestingFinish && message == "Fail") {
                    message += Environment.NewLine;
                    txtConsole.AppendText(message, Color.Red);
                    _fTestingFinish = true;
                }
            }
        }

        private void BtnAbout_Click(object sender, EventArgs e) {
            Process.Start("http://www.roin.com.tw/");
        }

        private void BtnClearCounter_Click(object sender, EventArgs e) {
            var confirmResult = MessageBox.Show(string.Format("是否確認刪除 派工單號 {0} 之測試資料 ??", lblProductName.Text),
                                     "確認刪除", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes) {
                //_successCount = _failCount = 0;
                string sql = string.Format("delete from TestResult where ProductName = '{0}'", lblProductName.Text);
                _db.SQLiteExecuteNonQuery(Util.GetProperty("ProductName"), sql);
                RefreshCounter();
                Util.SetProperty("OrderNo", (this.txtOrderNo.Text = "1"));
            }
        }

        private void RefreshCounter() {
            string sql = string.Format("select count(*) from TestResult where ProductName = '{0}'", lblProductName.Text);
            int total = Convert.ToInt32(_db.SQLiteExecuteScalar(Util.GetProperty("ProductName"), sql));
            sql = string.Format("select count(*) from TestResult where ProductName = '{0}' and TestResult = 'PASS'", lblProductName.Text);
            int success = Convert.ToInt32(_db.SQLiteExecuteScalar(Util.GetProperty("ProductName"), sql));

            //string success = _successCount.ToString();
            //string fail = _failCount.ToString();
            lblSuccessData.Text = (_successCount = success).ToString();
            lblFailData.Text = (_failCount = total - success).ToString();
            lblTotalData.Text = (total).ToString();
            Util.SetProperty("TestFailCount", lblFailData.Text);
            Util.SetProperty("TestSuccessCount", lblSuccessData.Text);
        }

        static string[] labelX = { "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "R", "T", "U", "V", "W", "Y" };
        static Font font = new Font("Consolas", 8, FontStyle.Regular, GraphicsUnit.Point);

        private void btnPasswordChange_Click(object sender, EventArgs e) {
            PasswordChangeDialog dialog = new PasswordChangeDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                if (GetPassword() != dialog.TextOldPassword.Text) {
                    ShowMessage(IniFile.IniReadValue("Message", "InputOldPasswordError"), true);
                    return;
                }
                SetPassword(dialog.TextNewPassword.Text);
                _bPassChecked = false;
            }
        }

        private bool CheckPassword() {
            if (_bPassChecked) {
                return true;
            }
            PasswordDialog dialog = new PasswordDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                if (GetPassword() == dialog.TextPassword.Text) {
                    _bPassChecked = true;
                    return true;
                }
            }
            return false;
        }

        public delegate void SetButtonControlSafe(ButtonX[] btns, bool doEnable);

        public void SetButtonControl(ButtonX[] btns, bool doEnable) {
            if (this.InvokeRequired) {
                this.Invoke(new SetButtonControlSafe(SetButtonControl), new object[] { btns, doEnable });
            } else {
                List<ButtonX> others = new ButtonX[] { btnStartSearchPoint, btnStopSearchPoint, btnTest, btnEscape, btnUpload, btnEditor, btnReport }.Except(btns).ToList();
                btns.ToList().ForEach(x => SetControlEnabled(x, doEnable));
                others.ForEach(x => SetControlEnabled(x, !doEnable));
            }
        }

        private void btnTestSet_Click(object sender, EventArgs e) {
            SetTestSet();
        }

        private void SetTestSet() {
            TestInfoDialog dialog = new TestInfoDialog();
            foreach (string printerName in PrinterSettings.InstalledPrinters) {
                dialog.cbPrinterName.Items.Add(printerName);
            }
            string oldProductName = Util.GetProperty("ProductName");
            string oldCustomerName = Util.GetProperty("CustomerName");
            string oldOrderNo = Util.GetProperty("OrderNo");

            dialog.txtTestMachine.Text = Util.GetProperty("TestMachine");
            dialog.txtProductName.Text = Util.GetProperty("ProductName");
            dialog.txtCustomerName.Text = Util.GetProperty("CustomerName");
            dialog.txtTestTotal.Text = Util.GetProperty("TestTotal");
            dialog.txtOrderNo.Text = Util.GetProperty("OrderNo");
            dialog.txtOperator.Text = Util.GetProperty("Operator");
            dialog.txtValidator.Text = Util.GetProperty("Validator");
            dialog.txtProductRev.Text = Util.GetProperty("ProductRev");
            dialog.cbPrinterName.SelectedIndex = dialog.cbPrinterName.FindStringExact(Util.GetProperty("PrinterName"));
            dialog.dtDtpDate.Value = Util.GetProperty("DtpDate") == "" ? DateTime.Now : DateTime.Parse(Util.GetProperty("DtpDate"));
            dialog.radioButton1.Checked = Util.GetProperty("PrinterActive") == "1";
            dialog.radioButton2.Checked = Util.GetProperty("PrinterActive") == "0";

            if (dialog.ShowDialog() == DialogResult.OK) {
                Util.SetProperty("TestMachine", dialog.txtTestMachine.Text);
                Util.SetProperty("ProductName", dialog.txtProductName.Text);
                Util.SetProperty("CustomerName", dialog.txtCustomerName.Text);
                Util.SetProperty("TestTotal", dialog.txtTestTotal.Text);
                Util.SetProperty("OrderNo", dialog.txtOrderNo.Text);
                Util.SetProperty("Operator", dialog.txtOperator.Text);
                Util.SetProperty("Validator", dialog.txtValidator.Text);
                Util.SetProperty("ProductRev", dialog.txtProductRev.Text);
                Util.SetProperty("PrinterName", dialog.cbPrinterName.Text);
                Util.SetProperty("DtpDate", dialog.dtDtpDate.Value.ToString("yyyy/MM/dd"));
                Util.SetProperty("PrinterActive", dialog.radioButton1.Checked ? "1" : "0");
            }
            if (String.IsNullOrWhiteSpace(Util.GetProperty("ProductName")) || String.IsNullOrWhiteSpace(Util.GetProperty("CustomerName"))) {
                SetControlEnabled(btnRs232Switch, false);
                ShowMessage(IniFile.IniReadValue("Message", "TestSetFirst"), true);
            } else {
                DisplayTestInfo();
                SetControlEnabled(btnRs232Switch, true);
                if (_rs232Controller != null && !_rs232Controller.IsOpen()) {
                    ShowMessage(IniFile.IniReadValue("Message", "RS232ConnectFirst"), true);
                }
                //if (Util.GetProperty("ProductName") != oldProductName) {
                    CreateDatabase();
                //}
                //if (Util.GetProperty("ProductName") != oldProductName) {
                string sql = string.Format("select count(*) from TestResult where ProductName = '{0}'", Util.GetProperty("ProductName"));
                string count = _db.SQLiteExecuteScalar(Util.GetProperty("ProductName"), sql);
                string orderNo = null;
                if (Convert.ToInt32(count) > 0) {
                    sql = string.Format("select max(CAST(OrderNo as INTEGER)) from TestResult where ProductName = '{0}'", Util.GetProperty("ProductName"));
                    orderNo = _db.SQLiteExecuteScalar(Util.GetProperty("ProductName"), sql);
                }
                this.txtOrderNo.Text = orderNo = string.IsNullOrEmpty(orderNo) ? "1" : (Convert.ToInt32(orderNo) + 1).ToString();
                Util.SetProperty("OrderNo", orderNo);
                RefreshCounter();
                //}

                // 是否顯示標籤設定
                this.btnLabelSet.Enabled = dialog.radioButton1.Checked;
            }
        }

        private void btnLabelSet_Click(object sender, EventArgs e) {
            SetLabelSet();
        }

        private void SetLabelSet() {
            LabelInfoDialog dialog = new LabelInfoDialog();
            dialog.txtTitle.Text = Util.GetProperty("LabelTitle");
            //dialog.txtProduct.Text = Util.GetProperty("LabelProduct");
            dialog.txtProduct.Text = Util.GetProperty("CustomerName");
            dialog.txtRevision.Text = Util.GetProperty("LabelRevision");
            dialog.txtManufacturerCode.Text = Util.GetProperty("LabelManufacturerCode");
            var plantCode = Util.GetProperty("LabelPlantCode");
            dialog.cbPlantCode.SelectedIndex = string.IsNullOrEmpty(plantCode) ? 0 : Convert.ToInt16(plantCode);
            dialog.txtPart.Text = Util.GetProperty("LabelPart");
            dialog.txtPlantVendor.Text = Util.GetProperty("LabelPlantVendor");
            dialog.txtTestedBy.Text = Util.GetProperty("LabelTestedBy");
            var qrCodeSize = Util.GetProperty("LabelQRCodeSize");
            dialog.cbQRCodeSize.SelectedIndex = string.IsNullOrEmpty(qrCodeSize) ? 0 : Convert.ToInt16(qrCodeSize);
            
            dialog.cbSwitch.SelectedIndex = Convert.ToInt16(Util.GetProperty("LabelSwitch"));
            dialog.txtFont1.Text = Util.GetProperty("PrinterPaperFont1");
            dialog.txtFont2.Text = Util.GetProperty("PrinterPaperFont2");
            dialog.txtFont3.Text = Util.GetProperty("PrinterPaperFont3");
            dialog.txtLabelWidth.Text = Util.GetProperty("PrinterPaperWidth");
            dialog.txtLabelHeight.Text = Util.GetProperty("PrinterPaperHeight");
            dialog.radioButton1.Checked = Util.GetProperty("QRCodeActive") == "1";
            dialog.radioButton2.Checked = Util.GetProperty("QRCodeActive") == "0";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                Util.SetProperty("LabelTitle", dialog.txtTitle.Text);
                Util.SetProperty("LabelProduct", dialog.txtProduct.Text);
                Util.SetProperty("LabelRevision", dialog.txtRevision.Text);
                Util.SetProperty("LabelManufacturerCode", dialog.txtManufacturerCode.Text);
                Util.SetProperty("LabelPlantCode", dialog.cbPlantCode.SelectedIndex.ToString());
                Util.SetProperty("LabelPart", dialog.txtPart.Text);
                Util.SetProperty("LabelPlantVendor", dialog.txtPlantVendor.Text);
                Util.SetProperty("LabelTestedBy", dialog.txtTestedBy.Text);

                Util.SetProperty("PrinterPaperWidth", dialog.txtLabelWidth.Text);
                Util.SetProperty("PrinterPaperHeight", dialog.txtLabelHeight.Text);
                Util.SetProperty("QRCodeActive", dialog.radioButton1.Checked ? "1" : "0");
                Util.SetProperty("LabelQRCodeSize", dialog.cbQRCodeSize.SelectedIndex.ToString());
                Util.SetProperty("PrinterPaperFont1", dialog.txtFont1.Text);
                Util.SetProperty("PrinterPaperFont2", dialog.txtFont2.Text);
                Util.SetProperty("PrinterPaperFont3", dialog.txtFont3.Text);

                Util.SetProperty("LabelSwitch", dialog.cbSwitch.SelectedIndex.ToString());
            }
        }

        private void btnReport_Click(object sender, EventArgs e) {
            ReportDialog dialog = new ReportDialog(_uploadData, this);
            dialog.ShowDialog();
        }

        private void btnSingleStep_Click(object sender, EventArgs e) {
            int index = ++_testStepCount;
            if (index == _shortList.Count()) {
                index = _testStepCount = 0;
            }
            DisplayTestResult(index);
        }

        private void btnStartSearchPoint_Click(object sender, EventArgs e) {
            PointSearchClear();
            _commandState = S_EMPTY;
            _rs232Controller.WriteCommand(Rs232Controller.W_SEARCH);
        }

        private void btnStopSearchPoint_Click(object sender, EventArgs e) {
            PointSearchClear();
            _rs232Controller.WriteCommand(Rs232Controller.W_ESCAPE);
        }

        private void btnStartTest_Click(object sender, EventArgs e) {
            PointSearchClear();
            _rs232Controller.WriteCommand(Rs232Controller.W_TEST);
        }

        private void btnEscape_Click(object sender, EventArgs e) {
            PointSearchClear();
            _rs232Controller.WriteCommand(Rs232Controller.W_ESCAPE);
        }

        private void btnEditor_Click(object sender, EventArgs e) {
            string netListFile = Util.GetProperty("NetListFile");
            if (!String.IsNullOrEmpty(netListFile)) {
                Process.Start(@"notepad.exe", netListFile);
            } else {
                ShowMessage(IniFile.IniReadValue("Message", "NotFoundForEdit"), true);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e) {
            if (CheckPassword()) {
                NetListInfo info = LoadNetList();
                if (info != null) {
                    _uploadData = info;
                    DoUpload();
                }
            } else {
                MessageBox.Show(IniFile.IniReadValue("Message", "PasswordError"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public delegate bool StartUploadSafe();

        public bool StartUpload() {
            if (this.InvokeRequired) {
                this.Invoke(new StartUploadSafe(StartUpload), new object[] { });
            } else {
                string netListFile = Util.GetProperty("NetListFile");
                if (!String.IsNullOrEmpty(netListFile)) {
                    NetListInfo info = LoadNetList(netListFile);
                    if (info != null) {
                        _uploadData = info;
                        DoUpload();
                        return true;
                    }
                }
            }
            return false;
        }

        private void DoUpload() {
            if (_uploadData != null && _uploadData.error.Count() > 0) {
                MessageBox.Show(string.Join(Environment.NewLine, _uploadData.error), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            int comCount = _uploadData.resistance.Count() + _uploadData.capacity.Count() + _uploadData.diode.Count();
            int count = _uploadData == null ? 0 : (_uploadData.netList.Count() + comCount);

            txtNetList.Clear();
            txtNetMap.Clear();
            lblFileName.Text = "";
            if (count == 0) {
                ShowMessage(IniFile.IniReadValue("Message", "UploadNoData"), true);
            } else {
                _uploadCount = 0;
                _commandState = S_UPLOAD_RESISTANCE;
                WriteCommandAfterEscape(Rs232Controller.W_REMPTY + CalcUploadCheckSum(Rs232Controller.W_REMPTY));

                SetControlEnabled(btnUpload, false);
                ShowMessage(IniFile.IniReadValue("Message", "Upload"), false);

                txtNetList.Text = String.Join(Environment.NewLine, _uploadData.netListOrg);
                txtNetMap.Text = String.Join(Environment.NewLine, _uploadData.netMapOrg);
                lblFileName.Text = Path.GetFileName(_uploadData.fileName);
            }
        }

        private string CalcCheckSum(string data) {
            char[] chArray = data.ToCharArray();
            int checkSum = 0;
            foreach (char ch in chArray) {
                checkSum += Convert.ToInt32(ch);
            }
            string str = checkSum.ToString("X2");
            return str.Substring(Math.Max(0, str.Length - 2));
        }

        private string CalcUploadCheckSum(string data) {
            char[] chArray = data.ToCharArray();
            int checkSum = 0;
            int power = 1;
            foreach (char ch in chArray) {
                checkSum += Convert.ToInt32(ch) * power;
                power = power < 128 ? power << 1 : 1;
            }
            string str = checkSum.ToString("X4");
            return str.Substring(Math.Max(0, str.Length - 4));
        }

        private bool UploadData() {
            if (_commandState == S_UPLOAD_NETLIST) {
                if (_uploadData.netList.Count() == _uploadCount) {
                    _rs232Controller.WriteCommand(Rs232Controller.W_P_END + CalcUploadCheckSum(Rs232Controller.W_P_END));
                    _commandState = S_EMPTY;
                    _uploadCount = 0;
                    return true;
                }
                List<string> points = _uploadData.netList[_uploadCount];
                string data = "P " + string.Join("=", points);
                ShowMessage(IniFile.IniReadValue("Message", "Upload") + data, false);
                _rs232Controller.WriteCommand(data + CalcUploadCheckSum(data));
                _uploadCount++;
                return true;
            } else if (_commandState == S_UPLOAD_RESISTANCE) {
                if (_uploadData.resistance.Count() == _uploadCount) {
                    if (_uploadData.resistance.Count() > 0) {
                        ShowMessage(IniFile.IniReadValue("Message", "UploadResistanceFinished"), false);
                    }
                    _commandState = S_UPLOAD_CAPACITY;
                    _uploadCount = 0;
                    _rs232Controller.WriteCommand(Rs232Controller.W_CEMPTY + CalcUploadCheckSum(Rs232Controller.W_CEMPTY));
                    return true;
                }
                string data = _uploadData.resistance[_uploadCount];
                ShowMessage(IniFile.IniReadValue("Message", "Upload") + data, false);
                _rs232Controller.WriteCommand(data + CalcUploadCheckSum(data));
                _uploadCount++;
                return true;
            } else if (_commandState == S_UPLOAD_CAPACITY) {
                if (_uploadData.capacity.Count() == _uploadCount) {
                    if (_uploadData.capacity.Count() > 0) {
                        ShowMessage(IniFile.IniReadValue("Message", "UploadCapacityFinished"), false);
                    }
                    _commandState = S_UPLOAD_DIODE;
                    _uploadCount = 0;
                    _rs232Controller.WriteCommand(Rs232Controller.W_DEMPTY + CalcUploadCheckSum(Rs232Controller.W_DEMPTY));
                    return true;
                }
                string data = _uploadData.capacity[_uploadCount];
                ShowMessage(IniFile.IniReadValue("Message", "Upload") + data, false);
                _rs232Controller.WriteCommand(data + CalcUploadCheckSum(data));
                _uploadCount++;
                return true;
            } else if (_commandState == S_UPLOAD_DIODE) {
                if (_uploadData.diode.Count() == _uploadCount) {
                    if (_uploadData.diode.Count() > 0) {
                        ShowMessage(IniFile.IniReadValue("Message", "UploadDiodeFinished"), false);
                    }
                    _commandState = S_UPLOAD_NETLIST;
                    _uploadCount = 0;
                    _rs232Controller.WriteCommand(Rs232Controller.W_P_BEGIN + CalcUploadCheckSum(Rs232Controller.W_P_BEGIN));
                    return true;
                }
                string data = _uploadData.diode[_uploadCount];
                ShowMessage(IniFile.IniReadValue("Message", "Upload") + data, false);
                _rs232Controller.WriteCommand(data + CalcUploadCheckSum(data));
                _uploadCount++;
                return true;
            }
            return false;
        }

        private List<string> tno = new List<string>(new string[] { "0.01s", "0.02s", "0.05s", "0.1s", "0.2s", "0.5s", "1s", "2s", "3s", "4s", "5s", "6s", "7s", "8s", "9s", "10s", "20s", "30s", "60s" });
        private List<string> holdMode = new List<string>(new string[] { "Off", "O/S", "C/S", "Cond", "O/S+O", "O+O/S", "C/S+O", "O+C/S", "Comp" });
        private List<string> psbx_tm = new List<string>(new string[] { "Off", "0.1s", "0.2s", "0.5s", "1s", "2s", "3s", "4s", "5s", "6s", "7s", "8s", "9s", "10s", "Max", "M.1s", "M.2s", "M.5s", "M1s", "M2s", "M3s", "M4s", "M5s" });

        private void UploadParameter() {
            string paramCommand = _paramList.FirstOrDefault();
            if (!string.IsNullOrEmpty(paramCommand)) {
                string value = _uploadData.paramMap[paramCommand];
                int indexing = 999;
                switch (paramCommand) {
                    case "COND IMPNO": // 6
                        value = value.EndsWith("K") ? (value.Substring(0, value.Length - 1) + new string(' ', 6 - value.Length) + "K") : (value + new string(' ', 6 - value.Length));
                        break;
                    case "HIPOT VNO": // 5
                    case "DCMA HIPOT":
                    case "AC HV VNO":
                        value = value.ToUpper();
                        value += new string(' ', 5 - value.Length);
                        break;
                    case "INS IMPNO": // 4
                        value = value.ToLower() == "off" ? "Off" : value;
                        value += new string(' ', 4 - value.Length);
                        break;
                    case "LEAKAGE": // 6
                        value = value.ToLower() == "off" ? "Off" : value;
                        value += new string(' ', 6 - value.Length);
                        break;
                    case "HIPOT TNO": // 2
                    case "AC HV TNO": // 2
                    case "DCMA HVTM": // 2 
                        value = value.EndsWith("S") ? value.Substring(0, value.Length - 1) + "s" : value;
                        indexing = tno.IndexOf(value);
                        if (indexing == -1) {
                            BackgroundMessageBox(this, string.Format(IniFile.IniReadValue("Message", "UploadError"), _uploadData.paramName[paramCommand]));
                        } else {
                            value = indexing.ToString("D2");
                        }
                        break;
                    case "HOLD MODE": // 1
                        value = value.ToLower() == "off" ? "Off" : value;
                        indexing = holdMode.IndexOf(value);
                        if (indexing == -1) {
                            BackgroundMessageBox(this, string.Format(IniFile.IniReadValue("Message", "UploadError"), _uploadData.paramName[paramCommand]));
                        } else {
                            value = indexing.ToString();
                        }
                        break;
                    case "PSBX TM1": // 2
                    case "PSBX TM2":
                        value = value.ToLower() == "off" ? "Off" : (value.EndsWith("S") ? value.Substring(0, value.Length - 1) + "s" : value);
                        indexing = psbx_tm.IndexOf(value);
                        if (indexing == -1) {
                            BackgroundMessageBox(this, string.Format(IniFile.IniReadValue("Message", "UploadError"), _uploadData.paramName[paramCommand]));
                        } else {
                            value = indexing.ToString("D2");
                        }
                        break;
                }
                if (indexing != -1) {
                    WriteRTParameter(paramCommand, value);
                }
            } else {
                ShowMessage(IniFile.IniReadValue("Message", "UploadNetListFinished"), false);
                _rs232Controller.WriteCommand(Rs232Controller.W_NETLIST);
                _commandState = S_EMPTY;
            }
        }

        private DialogResult BackgroundMessageBox(IWin32Window owner, string text) {
            if (this.InvokeRequired) {
                return (DialogResult)this.Invoke(new Func<DialogResult>(() => { return MessageBox.Show(owner, text); }));
            } else {
                return MessageBox.Show(owner, text);
            }
        }

        private void WriteRTParameter(string command, string value) {
            _uploadTestCondition = "@" + command + new string(' ', 11 - command.Length) + ":" + value;
            _rs232Controller.WriteCommand(_uploadTestCondition);
            _uploadTestCondition = _uploadTestCondition.TrimEnd();
            ShowMessage(IniFile.IniReadValue("Message", "Upload") + _uploadTestCondition, false);
        }

        public delegate NetListInfo LoadNetListSafe();

        public NetListInfo LoadNetList() {
            if (this.InvokeRequired) {
                return (NetListInfo)this.Invoke(new LoadNetListSafe(LoadNetList), new object[] { });
            } else {
                Stream ostr = null;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                //openFileDialog.InitialDirectory = "%USERPROFILE%";
                openFileDialog.InitialDirectory = Util.GetAppPath();
                openFileDialog.Filter = "Net List|*.net;*.txt|All Files|*.*";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.ShowHelp = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    try {
                        if ((ostr = openFileDialog.OpenFile()) != null) {
                            NetListInfo netListInfo = null;
                            using (ostr) {
                                netListInfo = LoadNetListStream(new StreamReader(ostr, Encoding.UTF8));
                            }
                            netListInfo.fileName = openFileDialog.FileName;
                            return netListInfo;
                        }
                    } catch (IOException ex) {
                        MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                return null;
            }
        }

        public NetListInfo LoadNetList(string fileName) {
            try {
                _uploadType = "";
                NetListInfo netListInfo = LoadNetListStream(new StreamReader(fileName, Encoding.UTF8));
                netListInfo.fileName = fileName;
                return netListInfo;
            } catch (Exception e) {
                ShowMessage(e.Message, true);
            }
            return null;
        }

        private NetListInfo LoadNetListStream(StreamReader fr) {
            NetListInfo netListInfo = new NetListInfo();
            _uploadType = "";
            using (fr) {
                while (!fr.EndOfStream) {
                    string line = fr.ReadLine().Trim();
                    if (line.Length == 0 || line[0] == '#') {
                        continue;
                    }
                    if (line.StartsWith("---")) {
                        _uploadType = line.Trim().Replace("-", "").ToLower();
                        continue;
                    }
                    switch (_uploadType) {
                        case "testcondition":
                            line = line.Split(new char[] { '�', '@', '\t' }, StringSplitOptions.RemoveEmptyEntries)[0];
                            string[] paramList = line.Split('=');
                            if (paramList.Length > 1) {
                                string paramCommand = Regex.Replace(paramList[0], @"^.*?\(([^)]*)\).*$", "$1");
                                netListInfo.paramMap.Add(paramCommand, paramList[1]);
                                netListInfo.paramName.Add(paramCommand, paramList[0].Substring(0, paramList[0].IndexOf('(')));
                            }
                            break;
                        case "netlist":
                            char type = String.IsNullOrEmpty(line) ? '_' : (line.IndexOf("=") > 0 ? 'X' : 'P');
                            switch (type) {
                                case 'P':
                                    line = line.Split(new char[] { '�', '@', '　', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)[0];
                                    string[] pointList = line.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (pointList.Length > 1) {
                                        List<string> pinNumberList = new List<string>();
                                        foreach (string point in pointList) {
                                            var p = char.IsLetter(point[0]) ? (Convert.ToInt32(ConvertPinNumber(point)) + 1).ToString() : point;
                                            pinNumberList.Add((Convert.ToInt32(p) - 1).ToString("D4"));
                                        }
                                        for (var i = 0; i < pinNumberList.Count - 1; i++) {
                                            netListInfo.netList.Add(pinNumberList.Skip(i).Take(2).ToList());
                                        }
                                        netListInfo.netListOrg.Add(line);
                                        netListInfo.netListPinOrg.Add(ConvertNumberPinList(pinNumberList));
                                    }
                                    break;
                                case 'X':
                                    string[] pair = line.Split(new char[] { '=', ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                                    if (pair.Length > 1) {
                                        // Connect Pair
                                        var p = char.IsLetter(pair[0][0]) ? (Convert.ToInt32(ConvertPinNumber(pair[0])) + 1).ToString() : pair[0];
                                        string point = (Convert.ToInt32(p) - 1).ToString();
                                        netListInfo.netMap.Add(point, pair[1]);
                                        netListInfo.netMapOrg.Add(pair[0] + "=" + pair[1]);
                                    }
                                    break;
                            }
                            break;
                        case "component":
                            switch (line[0]) {
                                case 'R':
                                    if (regexR.IsMatch(line)) {
                                        netListInfo.resistance.Add(line);
                                    } else {
                                        netListInfo.error.Add(line + " 格式錯誤");
                                    }
                                    break;
                                case 'C':
                                    if (regexC.IsMatch(line)) {
                                        if (line[16] == 'u' && Convert.ToDouble(line.Substring(12, 4)) > 2000) {
                                            netListInfo.error.Add(line + " 超過最大值2000uF");
                                        } else {
                                            netListInfo.capacity.Add(line);
                                        }
                                    } else {
                                        netListInfo.error.Add(line + " 格式錯誤");
                                    }
                                    break;
                                case 'D':
                                    if (regexD.IsMatch(line)) {
                                        if (Convert.ToDouble(line.Substring(12, 4)) > 4.52) {
                                            netListInfo.error.Add(line + " 超過最大值4.52");
                                        } else {
                                            netListInfo.diode.Add(line);
                                        }
                                    } else {
                                        netListInfo.error.Add(line + " 格式錯誤");
                                    }
                                    break;
                            }
                            break;
                    }
                }
                if (netListInfo.netMap.Count() > 0) {
                    netListInfo.netMap.Add("", "");
                }
                netListInfo.netListPinOrg.Sort();
            }
             return netListInfo;
        }

        private void PrintLabel() {
            AFCSPrinter afcs = new AFCSPrinter();
            afcs.PrinterName = Util.GetProperty("PrinterName");
            afcs.DocumentName = "LabelPrint";
            afcs.UseDefaultPaper = false;
            /*
            string margin = Util.GetProperty("PrinterPaperMargins");
            if (!string.IsNullOrEmpty(margin)) {
                string[] margins = margin.Split(',');
                afcs.MarginsLeft = Convert.ToInt32(margins[0]);
                afcs.MarginsRight = Convert.ToInt32(margins[1]);
                afcs.MarginsTop = Convert.ToInt32(margins[2]);
                afcs.MarginsBottom = Convert.ToInt32(margins[3]);
            }
            */
            afcs.PaperWidth = Convert.ToSingle(Util.GetProperty("PrinterPaperWidth"));
            afcs.PaperHeight = Convert.ToSingle(Util.GetProperty("PrinterPaperHeight"));
            //MessageBox.Show(afcs.PrinterName);
            if (Util.GetProperty("LabelSwitch") == "0") {
                afcs.Print(new AFCSPrinter.DoPrintDelegate(labelPrintQRCode));
            } else {
                afcs.Print(new AFCSPrinter.DoPrintDelegate(labelPrintBarcode));
            }
        }

        private FontStyle GetFontStyle(string type) {
            switch (type) {
                default:
                case "Regular":
                    return FontStyle.Regular;
                case "Bold":
                    return FontStyle.Bold;
                case "Underline":
                    return FontStyle.Underline;
                case "Italic":
                    return FontStyle.Italic;
                case "Strikeout":
                    return FontStyle.Strikeout;
            }
        }

        void labelPrintQRCode(Graphics g, ref bool hasMorePage) {
            Graphics graphics = g;
            //Font font = new Font("Courier New", 6);
            
            string[] font1Info = Util.GetProperty("PrinterPaperFont1").Split(',');
            string[] font2Info = Util.GetProperty("PrinterPaperFont2").Split(',');
            string[] font3Info = (Util.GetProperty("PrinterPaperFont3") ?? "Arial Narrow,5,Regular").Split(',');
            Font font1 = new Font(font1Info[0], Convert.ToSingle(font1Info[1]), GetFontStyle(font1Info[2]));
            Font font2 = new Font(font2Info[0], Convert.ToSingle(font2Info[1]), GetFontStyle(font2Info[2]));
            Font font3 = new Font(font2Info[0], Convert.ToInt32(font2Info[1]), GetFontStyle(font2Info[2]) | GetFontStyle("Underline"));
            Font font4 = new Font(font3Info[0], Convert.ToInt32(font3Info[1]), GetFontStyle(font3Info[2]) | GetFontStyle("Underline"));
            SolidBrush brush = new SolidBrush(Color.Black);
            int startX = Convert.ToInt32(Util.GetProperty("PrinterPaperStartX"));
            int startY = Convert.ToInt32(Util.GetProperty("PrinterPaperStartY"));
            int lineShift = (int)Math.Round(Convert.ToSingle(Util.GetProperty("PrinterPaperHeight")) / 25.4f * 100f / 6); //Convert.ToInt32(Util.GetProperty("PrinterPaperLineShift"));
            int offset = 0;
            SizeF fSize;
            String label = "";
            graphics.DrawString(Util.GetProperty("LabelTitle"), font1, brush, startX, startY + offset);
            offset = offset + lineShift;
            label = "P/N:";
            graphics.DrawString("P/N:", font2, brush, startX, startY + offset);
            fSize = graphics.MeasureString(label, font2);
            graphics.DrawString(Util.GetProperty("LabelProduct"), font3, brush, startX + fSize.Width, startY + offset);
            label += Util.GetProperty("LabelProduct") + "_";
            fSize = graphics.MeasureString(label, font2);
            graphics.DrawString("Rev:", font2, brush, startX + fSize.Width, startY + offset);
            label += "Rev:";
            fSize = graphics.MeasureString(label, font2);
            graphics.DrawString(Util.GetProperty("LabelRevision"), font3, brush, startX + fSize.Width, startY + offset);
            offset = offset + lineShift;
            graphics.DrawString("S/N:", font2, brush, startX, startY + offset);
            fSize = graphics.MeasureString("S/N:", font2);
            graphics.DrawString(_labelSerialNo, font3, brush, startX + fSize.Width, startY + offset);
            offset = offset + lineShift;
            graphics.DrawString("Part:", font2, brush, startX, startY + offset);
            fSize = graphics.MeasureString("Part:", font2);
            var pSize = graphics.MeasureString(Util.GetProperty("LabelPart"), font4);
            graphics.DrawString(Util.GetProperty("LabelPart"), font4, brush, startX + fSize.Width, startY + offset + (fSize.Height - pSize.Height) / 2);
            offset = offset + lineShift;
            graphics.DrawString("Plant/Vendor:", font2, brush, startX, startY + offset);
            fSize = graphics.MeasureString("Plant/Vendor:", font2);
            graphics.DrawString(Util.GetProperty("LabelPlantVendor"), font3, brush, startX + fSize.Width, startY + offset);
            offset = offset + lineShift;
            graphics.DrawString("Tested By:", font2, brush, startX, startY + offset);
            fSize = graphics.MeasureString("Tested By:", font2);
            graphics.DrawString(Util.GetProperty("LabelTestedBy"), font3, brush, startX + fSize.Width, startY + offset);

            if (Util.GetProperty("QRCodeActive") == "1") {
                var qrCodeSize = Util.GetProperty("LabelQRCodeSize");
                string qrInfo = Util.GetProperty("QRCodeInfo" + (string.IsNullOrEmpty(qrCodeSize) ? "0" : qrCodeSize));
                Rectangle qrRect = new Rectangle(0, 0, 0, 0);
                if (!string.IsNullOrEmpty(qrInfo)) {
                    string[] qrInfos = qrInfo.Split(',');
                    qrRect.X = Convert.ToInt32(qrInfos[1]);
                    qrRect.Y = Convert.ToInt32(qrInfos[2]);
                    qrRect.Width = Convert.ToInt32(qrInfos[3]);
                    qrRect.Height = Convert.ToInt32(qrInfos[3]);
                }
                // Draw QRCODE
                using (Bitmap qrBmp = GetQRCode("P" + Util.GetProperty("LabelProduct") + ":S" + _labelSerialNo, Math.Min(qrRect.Width, qrRect.Height))) {
                    graphics.DrawImage(qrBmp, qrRect.X, qrRect.Y, qrRect.Width, qrRect.Height);
                }
            }
        }

        void labelPrintBarcode(Graphics g, ref bool hasMorePage) {
            Graphics graphics = g;

            string[] font1Info = Util.GetProperty("PrinterPaperFont1").Split(',');
            string[] font2Info = Util.GetProperty("PrinterPaperFont2").Split(',');
            Font font1 = new Font(font1Info[0], Convert.ToSingle(font1Info[1]), GetFontStyle(font1Info[2]));
            Font font2 = new Font(font2Info[0], Convert.ToSingle(font2Info[1]), GetFontStyle(font2Info[2]));
            SolidBrush brush = new SolidBrush(Color.Black);
            int[] wordPos = Util.GetProperty("RightDataMatrixWordInfo2").Split(',').Select(Int32.Parse).ToArray();
            int[] shift = Util.GetProperty("RightDataMatrixWordShift").Split(',').Select(Int32.Parse).ToArray();

            int offset = 0;
            graphics.DrawString("Part Number(P):", font1, brush, wordPos[0], wordPos[1]);
            offset = offset + shift[0];
            graphics.DrawString(Util.GetProperty("LabelProduct"), font2, brush, wordPos[0], wordPos[1] + offset);
            offset = offset + shift[1];
            graphics.DrawString("Serial Number(S):", font1, brush, wordPos[0], wordPos[1] + offset);
            offset = offset + shift[0];
            graphics.DrawString(_labelSerialNo, font2, brush, wordPos[0], wordPos[1] + offset);
            offset = offset + shift[1];
            graphics.DrawString("Part Description:", font1, brush, wordPos[0], wordPos[1] + offset);
            offset = offset + shift[0];
            graphics.DrawString(Util.GetProperty("LabelPart"), font1, brush, new RectangleF(wordPos[0], wordPos[1] + offset, wordPos[2]-wordPos[0], wordPos[3]-offset));

            int[] serialNoPos = Util.GetProperty("LeftDataMatrixWordSerialNo").Split(',').Select(Int32.Parse).ToArray();
            graphics.DrawString(_labelSerialNo, font2, brush, serialNoPos[0], serialNoPos[1]);

            int[] bottomPos = Util.GetProperty("LeftDataMatrixWordBottom").Split(',').Select(Int32.Parse).ToArray();
            DrawRotatedTextAt(graphics, 90, "BOTTOM", bottomPos[0], bottomPos[1], font2, brush);

            using (Bitmap dmBmp = GetBarcode("P" + Util.GetProperty("LabelProduct") + ":S" + _labelSerialNo, 2)) {
                using (Bitmap bmp = Sharpen(dmBmp, whichMatrix.Mean3x3, 1)) {
                    // Draw BARCODE Left
                    int[] left = Util.GetProperty("LeftDataMatrixCodeInfo2").Split(',').Skip(1).Select(Int32.Parse).ToArray();
                    graphics.DrawImage(bmp, left[0], left[1], left[2], left[2]);
                    // Draw BARCODE Right
                    int[] right = Util.GetProperty("RightDataMatrixCodeInfo2").Split(',').Skip(1).Select(Int32.Parse).ToArray();
                    graphics.DrawImage(bmp, right[0], right[1], right[2], right[2]);
                }
            }
        }

        public Bitmap Sharpen(Image image, whichMatrix welcheMatrix, double strength) {
            double FaktorKorrekturWert = 0;
            switch (welcheMatrix) {
                case whichMatrix.Gaussian3x3:
                    strength = (strength * -1) / 10;
                    FaktorKorrekturWert = 16;
                    break;
                case whichMatrix.Mean3x3:
                    strength = strength * -9 / 100;
                    FaktorKorrekturWert = 10;
                    break;
                case whichMatrix.Gaussian5x5Type1:
                    strength = strength * 2.5 / 100;
                    FaktorKorrekturWert = 12;
                    break;
                default:
                    break;
            }
            using (var bitmap = image as Bitmap) {
                if (bitmap != null) {
                    var sharpenImage = bitmap.Clone() as Bitmap;

                    int width = image.Width;
                    int height = image.Height;

                    var filter = Matrix(welcheMatrix);
                    int filterSize = filter.GetLength(0);
                    double bias = 1.0 - strength;
                    double factor = strength / FaktorKorrekturWert;
                    int s = filterSize / 2; 
                    var result = new Color[image.Width, image.Height];

                    if (sharpenImage != null) {
                        BitmapData pbits = sharpenImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                        int bytes = pbits.Stride * height;
                        var rgbValues = new byte[bytes];
                        Marshal.Copy(pbits.Scan0, rgbValues, 0, bytes);

                        int rgb;
                        for (int x = s; x < width - s; x++) {
                            for (int y = s; y < height - s; y++) {
                                double red = 0.0, green = 0.0, blue = 0.0;
                                for (int filterX = 0; filterX < filterSize; filterX++) {
                                    for (int filterY = 0; filterY < filterSize; filterY++) {
                                        int imageX = (x - s + filterX + width) % width;
                                        int imageY = (y - s + filterY + height) % height;

                                        rgb = imageY * pbits.Stride + 3 * imageX;

                                        red += rgbValues[rgb + 2] * filter[filterX, filterY];
                                        green += rgbValues[rgb + 1] * filter[filterX, filterY];
                                        blue += rgbValues[rgb + 0] * filter[filterX, filterY];
                                    }
                                    rgb = y * pbits.Stride + 3 * x;
                                    int r = Math.Min(Math.Max((int)(factor * red + (bias * rgbValues[rgb + 2])), 0), 255);
                                    int g = Math.Min(Math.Max((int)(factor * green + (bias * rgbValues[rgb + 1])), 0), 255);
                                    int b = Math.Min(Math.Max((int)(factor * blue + (bias * rgbValues[rgb + 0])), 0), 255);
                                    result[x, y] = System.Drawing.Color.FromArgb(r, g, b);
                                }
                            }
                        }
                        for (int x = s; x < width - s; x++) {
                            for (int y = s; y < height - s; y++) {
                                rgb = y * pbits.Stride + 3 * x;

                                rgbValues[rgb + 2] = result[x, y].R;
                                rgbValues[rgb + 1] = result[x, y].G;
                                rgbValues[rgb + 0] = result[x, y].B;
                            }
                        }
                        Marshal.Copy(rgbValues, 0, pbits.Scan0, bytes);
                        sharpenImage.UnlockBits(pbits);
                    }
                    return sharpenImage;
                }
            }
            return null;
        }

        public enum whichMatrix {
            Gaussian3x3,
            Mean3x3,
            Gaussian5x5Type1
        }

        private double[,] Matrix(whichMatrix welcheMatrix) {
            double[,] selectedMatrix = null;

            switch (welcheMatrix) {
                case whichMatrix.Gaussian3x3:
                    selectedMatrix = new double[,]
                    {
                    { 1, 2, 1, },
                    { 2, 4, 2, },
                    { 1, 2, 1, },
                    };
                    break;

                case whichMatrix.Gaussian5x5Type1:
                    selectedMatrix = new double[,]
                    {
                    {-1, -1, -1, -1, -1},
                    {-1,  2,  2,  2, -1},
                    {-1,  2,  16, 2, -1},
                    {-1,  2, -1,  2, -1},
                    {-1, -1, -1, -1, -1}
                    };
                    break;

                case whichMatrix.Mean3x3:
                    selectedMatrix = new double[,]
                    {
                    { 1, 1, 1, },
                    { 1, 1, 1, },
                    { 1, 1, 1, },
                    };
                    break;
            }
            return selectedMatrix;
        }

        private void DrawRotatedTextAt(Graphics gr, float angle, string txt, int x, int y, Font font, Brush brush) {
            GraphicsState state = gr.Save();
            gr.ResetTransform();
            gr.RotateTransform(angle);
            gr.TranslateTransform(x, y, MatrixOrder.Append);
            gr.DrawString(txt, font, brush, 0, 0);
            gr.Restore(state);
        }

        private string GetLabelSerialNo() {
            var date = Util.GetProperty("LabelSwitch") == "0" ? GetSNDate() : GetDMDate();
            string lead = Util.GetProperty("LabelManufacturerCode") + date + Util.GetProperty("LabelPlantCode");
            string sql = string.Format("select max(LabelSerialNo) from TestResult where ProductName = '{0}' and LabelSerialNo like '{1}%'", Util.GetProperty("ProductName"), lead);
            string serialNo = _db.SQLiteExecuteScalar(Util.GetProperty("ProductName"), sql);
            int no = string.IsNullOrEmpty(serialNo) ? 0 : Convert.ToInt32(Util.GetProperty("LabelSwitch") == "0" ? serialNo.Substring(serialNo.Length - 6) : serialNo.Substring(serialNo.Length - 6, 5));
            return lead + (Util.GetProperty("LabelSwitch") == "0" ? (no + 1).ToString("D6") : ((no + 1).ToString("D5") + "0"));
        }

        private string GetSNDate() {
            DateTime now = DateTime.Now;
            int y = now.Year, m = now.Month, d = now.Day;
            return (y % 100).ToString() + (m > 9 ? Convert.ToChar(m - 10 + ((int)'A')).ToString() : m.ToString()) + (d > 9 ? Convert.ToChar(d - 10 + ((int)'A')).ToString() : d.ToString());
        }

        private string GetDMDate() {
            DateTime now = DateTime.Now;
            return (now.Year % 100).ToString() + now.DayOfYear.ToString().PadLeft(3, '0');
        }

        private Bitmap GetQRCode(string text, int pixels) {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            return qrCode.GetGraphic(pixels);
        }

        private Bitmap GetBarcode(string text, int module) {
            DmtxImageEncoder barcode = new DmtxImageEncoder();
            DmtxImageEncoderOptions options = new DmtxImageEncoderOptions();
            options.SizeIdx = DmtxSymbolSize.DmtxSymbol20x20;
            options.Scheme = DmtxScheme.DmtxSchemeAscii;
            options.MarginSize = 0;
            options.ModuleSize = module;
            return barcode.EncodeImage(text, options);
        }

        private void txtOrderNo_KeyPressed(object sender, KeyPressEventArgs e) {
            // 避免輸入非數字Character
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtOrderNo_LostFocus(object sender, EventArgs e) {
            // 確認變更 orderNo
            Util.SetProperty("OrderNo", txtOrderNo.Text);
        }

        public delegate void AlertMessageSafe(string message);

        public void AlertMessage(string message) {
            if (this.InvokeRequired) {
                this.Invoke(new AlertMessageSafe(AlertMessage), new object[] { message });
            } else {
                MessageBox.Show(message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
    }

    public class NetListInfo {
        public string fileName                      = null;
        public List<List<string>> netList           = new List<List<string>>();
        public List<string> netListOrg              = new List<string>();
        public List<string> netListPinOrg           = new List<string>();
        public Dictionary<string, string> netMap    = new Dictionary<string, string>();
        public List<string> netMapOrg               = new List<string>();
        public List<string> resistance              = new List<string>();
        public List<string> capacity                = new List<string>();
        public List<string> diode                   = new List<string>();
        public List<string> error                   = new List<string>();
        public Dictionary<string, string> paramMap  = new Dictionary<string, string>();
        public Dictionary<string, string> paramName = new Dictionary<string, string>();
    }

    public class TestDetailData {
        public int No { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
    }
}
