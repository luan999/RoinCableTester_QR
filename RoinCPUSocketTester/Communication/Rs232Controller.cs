using RoinCableTester.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;

namespace RoinCableTester.Communication {
    public class Rs232Controller {

        public const string             W_RESET                 = "#REST";
        public const string             W_LOGO                  = "#LOGO";
        public const string             W_NETLIST               = "T NETLIST";
        public const string             W_DISK                  = "#DISK";
        public const string             W_DIRA                  = "#DIRA";
        public const string             W_LOAD                  = "#LOAD:";
        public const string             W_SEARCH                = "K_SRCH";
        public const string             W_ESCAPE                = "K_ESC.";
        public const string             W_P_BEGIN               = "P BEGIN";
        public const string             W_P_END                 = "P END.";
        public const string             W_MODE_TEST             = "#?";
        public const string             W_READ                  = "K_READ";
        public const string             W_TEST                  = "K_TEST";
        public const string             W_ESC                   = "K_ESC.";
        public const string             W_READ_NET              = "#RNET";
        public const string             W_REMPTY                = "REMPTY";
        public const string             W_CEMPTY                = "CEMPTY";
        public const string             W_DEMPTY                = "DEMPTY";

        public const string             R_BOOTING               = "$Booting";
        public const string             R_SELF_TEST_OK          = "$ ** Self Test OK";
        public const string             R_RESET                 = "$#RESET";
        public const string             R_LOGO                  = "$LOGO";
        public const string             R_WLOGO                 = "$#LOGO";
        public const string             R_ID                    = "$ID:";
        public const string             R_RIGHT                 = "$KEY_RIGHT";
        public const string             R_TEST                  = "$KEY_TEST";
        public const string             R_SEARCH                = "$KEY_SRCH";
        public const string             R_ESCAPE                = "$KEY_ESC";
        public const string             R_TEST_MODE             = "$TEST";
        public const string             R_READ                  = "$KEY_READ";
        public const string             R_READ_MODE             = "$READ";
        public const string             R_READ_OK               = "$Read Ok";
        public const string             R_READ_EMPTY            = "$Empty Read";
        public const string             R_ESC                   = "$KEY_ESC";
        public const string             R_9001                  = "$  RT-9001";
        public const string             R_DATALIST              = "$*** "; //"$*** Read Data Listin";
        public const string             R_END_NETLIST           = "$End Netlist";
        public const string             R_NO_DISK               = "$No Disk";
        public const string             R_NO_FILE               = "$No File";
        public const string             R_START_DIRA            = "$#DIR DISK A:";
        public const string             R_END_DIRA              = "$End Dir";
        public const string             R_LOAD                  = "$#LOAD:";
        public const string             R_FILE_NOT_FOUND        = "$File not found";
        public const string             R_FILE_ERROR            = "$Load file error";
        public const string             R_FILE_OK               = "$Load file Ok";

        public const string             R_INFO_COND             = "$ Cond. o";
        public const string             R_INFO_INS              = "$ Ins.  o";
        public const string             R_INFO_DCHIPOT          = "$ Hipot V";
        public const string             R_INFO_DCTIME           = "$ HV Time";
        public const string             R_INFO_ACHIPOT          = "$ AC Hipot V";
        public const string             R_INFO_ACTIME           = "$ ACV Time";
        public const string             R_INFO_LEAKAGE          = "$ Leakage I";
        public const string             R_INFO_DCI_LEAKAGE      = "$ DCI Leakage";
        public const string             R_INFO_DCI_TIME         = "$ DCI Time";
        public const string             R_INFO_INTERMITTENT     = "$ Intermittent";
        public const string             R_INFO_INTER_TEST1      = "$ Inter. Test1";
        public const string             R_INFO_INTER_TEST2      = "$ Inter. Test2";
        public const string             R_INFO_WIRE_MODE        = "$ 4-Wire Mode";
        public const string             R_P_BEGIN               = "$P BEGIN";
        public const string             R_P_END                 = "$P END";
        public const string             R_EMPTY                 = "$EMPTY";
        public const string             R_START                 = "$START";
        public const string             R_END                   = "$END";
        public const string             R_COND_ERROR            = "$COND ERROR";
        public const string             R_OPEN_ERROR            = "$OPEN ERROR";
        public const string             R_SHORT_ERROR           = "$SHORT ERROR";
        public const string             R_OSC_PASS              = "$O/S/C PASS";
        public const string             R_AC_HV_TESTING         = "$AC HV TESTING";
        public const string             R_HV_TESTING            = "$HV TESTING";
        public const string             R_DC_LEAK_TESTING       = "$DC LEAK. TESTING";
        public const string             R_INS_TESTING           = "$INS TESTING";
        public const string             R_INTER1_TESTING        = "$INTER. 1 TESTING";
        public const string             R_INTER2_TESTING        = "$INTER. 2 TESTING";
        public const string             R_INS_FAIL              = "$INS FAIL";
        public const string             R_DC_LEAK_FAIL          = "$DC LEAK FAIL";
        public const string             R_LEAK_FAIL             = "$LEAK FAIL";
        public const string             R_HIPOT_TEST_FAIL       = "$ Hipot Test Fail !!";
        public const string             R_INS_MULTI_TEST_FAIL   = "$Ins Multi Test Fail.";
        public const string             R_INTER1_FAIL           = "$INTER 1 FAIL";
        public const string             R_INTER2_FAIL           = "$INTER 2 FAIL";
        public const string             R_COMP_FAIL             = "$COMP FAIL";
        public const string             R_PASS                  = "$PASS";
        public const string             R_SHORT                 = "$SHORT";
        public const string             R_SHORT_POINT           = "$S:";
        public const string             R_OPEN_POINT            = "$O:";
        public const string             R_OPEN_INTER_POINT      = "$o:";
        public const string             R_MISWIRE_POINT         = "$M:";
        public const string             R_INS_POINT             = "$I:";
        public const string             R_ARC_POINT             = "$Arc:";
        public const string             R_OVC_POINT             = "$Ovc:";
        public const string             R_OVV_POINT             = "$Ovv:";
        public const string             R_LEAK_POINT            = "$Leak:";
        public const string             R_OVAC_POINT            = "$Ovac:";
        public const string             R_DCI_POINT             = "$Dci:";
        public const string             R_DCARC_POINT           = "$DcArc:";
        public const string             R_DCOVC_POINT           = "$DcOvc:";
        public const string             R_DCOVV_POINT           = "$DcOvv:";
        public const string             R_COND_POINT            = "$c:";
        public const string             R_SINK_POINT            = "$Sink:";
        public static Regex             R_RESISTANCE_POINT      = new Regex(@"^\$R[1-9]\d?:", RegexOptions.Compiled);
        public static Regex             R_CAPACITY_POINT        = new Regex(@"^\$C[1-9]\d?:", RegexOptions.Compiled);
        public static Regex             R_DIODE_POINT           = new Regex(@"^\$D[1-9]\d?:", RegexOptions.Compiled);
        public const string             R_POINT_SEARCH          = "$Point Search";
        public const string             R_POINT_END             = "$Point End";
        public const string             R_NETLIST               = "$#NET LIST";
        public const string             R_CONT                  = "$CONT";
        public const string             R_COND                  = "$COND";
        public const string             R_OPEN                  = "$OPEN";
        public const string             R_ENDLIST               = "$FF";

        public const string             R_REMPTY                = "$REMPTY";
        public const string             R_CEMPTY                = "$CEMPTY";
        public const string             R_DEMPTY                = "$DEMPTY";
        public const string             R_RESTBL                = "$RES TBL";
        public const string             R_CAPTBL                = "$CAP TBL";
        public const string             R_DIODETBL              = "$DIODE TBL";
        public const string             R_ENDTBL                = "$END TBL";

        private SerialPort              _serialPort             = null;
        private string[]                _serialPortNames        = null;

        private bool                    _deviceActivate         = false;
        private string                  _sndCommand             = null;
        private StringBuilder           _recCommand             = null;
        
        private int                     _retryCount             = 0;
        private Timer                   _retryTimer             = null;

        public static int               retryTimePeriod         = 1000;
        public static int               commandWaitTime         = 100;

        private static BlockingCollection<string> _commandQueue = new BlockingCollection<string>();
        public delegate void CommandProcessDeligate(string command);
        private static CommandProcessDeligate _commandProcess = null;

        public Rs232Controller(CommandProcessDeligate commandProcess) {
            InitializeRs232();
            InitializeRetryTimer();
            _commandProcess = commandProcess;
            Task.Factory.StartNew(() => CommandQueueConsumer());
        }

        public string[] GetPortNames() {
            return _serialPortNames;
        }

        public bool Open(string portName) {
            if (portName == IniFile.IniReadValue("Message", "None")) {
                return true;
            }
            _serialPort.PortName = portName;
            _serialPort.Open();
            if (_serialPort.IsOpen) {
                _serialPort.DataReceived += CommandReceivedHandler;
                _deviceActivate = true;
                _retryTimer.Start();
                return true;
            }
            return false;
        }

        public bool Close() {
            if (_serialPort.IsOpen) {
                _serialPort.Close();
                _serialPort.DataReceived -= CommandReceivedHandler;
                _retryTimer.Stop();
                _deviceActivate = false;
            }
            return !_serialPort.IsOpen;
        }

        public bool IsOpen() {
            return _serialPort.IsOpen;
        }

        public void WriteCommand(string command) {
            if (_serialPort.IsOpen) {
                Util.TraceInfo(string.Format("Data Write   : {0}", command));
                _sndCommand = command;
                _serialPort.Write(command + "\0");
                _retryCount = 0;
            }
        }

        public void WriteUploadCommand(string command) {
            if (_serialPort.IsOpen) {
                Util.TraceInfo(string.Format("Data Write   : {0}", command));
                _sndCommand = command;
                _serialPort.Write(command + CalcUploadCheckSum(command) + "\0");
                _retryCount = 0;
            }
        }

        public void WriteUploadList(List<string> list) {
            if (_serialPort.IsOpen) {
                list = ConvertUploadPinNumber(list);
                string command = "P " + string.Join("=", list);
                Util.TraceInfo(string.Format("Data Write   : {0}", command));
                _sndCommand = command;
                _serialPort.Write(command + CalcUploadCheckSum(command) + "\0");
                _retryCount = 0;
            }
        }

        public void StopRetry() {
            _retryCount = -1;
        }

        public void DeactiveDevice() {
            _deviceActivate = false;
        }

        private void InitializeRs232() {
            SerialPort serialPort = new SerialPort();
            serialPort.BaudRate = 19200;
            serialPort.Parity = Parity.None;
            serialPort.StopBits = StopBits.One;
            serialPort.DataBits = 8;
            serialPort.Handshake = Handshake.None;
            serialPort.DtrEnable = true;
            serialPort.RtsEnable = true;
            serialPort.ReadBufferSize = 16384;
            serialPort.WriteBufferSize = 1024;

            _serialPort = serialPort;
            _deviceActivate = false;
            _sndCommand = "";
            _recCommand = new StringBuilder();

            _serialPortNames = SerialPort.GetPortNames();
            Array.Sort(_serialPortNames);
        }

        private void InitializeRetryTimer() {
            Timer retryTimer = new Timer();
            retryTimer.Elapsed += OnRetryTimerEvent;
            retryTimer.Interval = retryTimePeriod;

            _retryTimer = retryTimer;
            _retryCount = -1;
        }

        private void OnRetryTimerEvent(object sender, ElapsedEventArgs e) {
            Timer retryTimer = (Timer)sender;
            if (_retryCount == -1) {
                return;
            }
            _retryCount++;
            if (_retryCount == 1) {
                return;
            }
            if (!_serialPort.IsOpen) {
                _retryCount = -1;
                return;
            }
            int n;
            if ((_sndCommand.StartsWith("P ") && !_sndCommand.StartsWith("P END")) || _sndCommand.StartsWith("@") || _sndCommand.StartsWith("#")
                || (_sndCommand.StartsWith("R") && _sndCommand.Length > 1 && int.TryParse(_sndCommand[1].ToString(), out n))
                || (_sndCommand.StartsWith("C") && _sndCommand.Length > 1 && int.TryParse(_sndCommand[1].ToString(), out n))
                || (_sndCommand.StartsWith("D") && _sndCommand.Length > 1 && int.TryParse(_sndCommand[1].ToString(), out n))) {
                Util.TraceInfo(string.Format("Data Retry   : {0}", "00,00,00,00"));
                System.Threading.Thread.Sleep(100);
                _serialPort.Write("\0\0\0\0");
                System.Threading.Thread.Sleep(100);
            }
            Util.TraceInfo(string.Format("Data Retry   : {0}", _sndCommand));
            _serialPort.Write(_sndCommand + "\0");
        }

        private void CommandReceivedHandler(Object sender, SerialDataReceivedEventArgs e) {
            SerialPort serialPort = (SerialPort)sender;
            int count = serialPort.BytesToRead;
            try {
                for (int i = 0; i < count; i++) {
                    int data = serialPort.ReadByte();
                    if (data == 0) {
                        string command = _recCommand.ToString();
                        TraceInfo(string.Format("Data Received: {0}", command));
                        if (_deviceActivate) {
                            _commandQueue.Add(command);
                        }
                        _recCommand.Clear();
                    } else {
                        _recCommand.Append(char.ConvertFromUtf32(data));
                    }
                }
            } catch (Exception ex) {
                TraceInfo("Command Recevied Error: " + ex.Message);
            }
        }

        private static void CommandQueueConsumer() {
            foreach (string command in _commandQueue.GetConsumingEnumerable()) {
                _commandProcess(command);
            }
        }

        private static int[] specialPoints = new int[] { Convert.ToInt32("E0", 16), Convert.ToInt32("E2", 16), Convert.ToInt32("F7", 16), Convert.ToInt32("E5", 16), Convert.ToInt32("F2", 16), Convert.ToInt32("EC", 16) };

        private List<string> ConvertUploadPinNumber(List<string> points) {
            string[] specialString = {"α", "β", "π", "δ", "θ", "ψ"};
            List<string> list = new List<string>();
            foreach (string point in points) {
                int lead = point[0];
                int specialIndex = Array.IndexOf(specialString, point.Substring(0, 1));
                string num = "????";
                if (specialIndex > -1) {
                    num = ((specialIndex + 26) * 64 + Convert.ToInt32(point.Substring(1, 2)) - 1).ToString("D4");
                } else if (lead >= (int)'A' && lead <= (int)'Z') {
                    num = ((lead - (int)'A') * 64 + Convert.ToInt32(point.Substring(1, 2)) - 1).ToString("D4");
                }
                list.Add(num);
            }
            return list;
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

        public static void TraceInfo(string message) {
            string tracemessage = string.Format("{0}\t{1}", DateTime.Now.ToString("MM/dd/yy HH:mm:ss"), message);
            Trace.WriteLine(tracemessage);
            //Trace.Flush();
        }
    }
}
