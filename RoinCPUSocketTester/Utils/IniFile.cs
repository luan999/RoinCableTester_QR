using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace RoinCableTester.Utils {
    public class IniFile {
        private static string _path;
        private static string _charSet = "UTF-8";

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, byte[] val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);

        public static void SetIniPath(string IniPath) {
            _path = IniPath;
        }

        public static void IniWriteValue(string Section, string Key, string Value) {
            WritePrivateProfileString(Section, Key, Encoding.GetEncoding(_charSet).GetBytes(Value), _path);
        }

        public static string IniReadValue(string Section, string Key) {
            byte[] temp = new byte[500];
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, _path);
            return Encoding.GetEncoding(_charSet).GetString(temp, 0, i);
        }
    }
}
