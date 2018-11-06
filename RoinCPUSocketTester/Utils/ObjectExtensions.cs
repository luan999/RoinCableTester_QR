using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace RoinCableTester.Utils {

    public static class ObjectExtensions {
        
        public static T ToObject<T>(this IDictionary<string, object> source) where T : class, new() {
            T someObject = new T();
            Type someObjectType = someObject.GetType();

            foreach (KeyValuePair<string, object> item in source) {
                someObjectType.GetProperty(item.Key).SetValue(someObject, item.Value, null);
            }
            return someObject;
        }

        public static List<object> AsDictionaryList<T>(this ICollection<T> source) {
            List<object> values = new List<object>();
            foreach (object obj in (HashSet<T>)source) {
                values.Add(obj.AsDictionary());
            }
            return values;
        }

        public static Dictionary<string, object> AsDictionary(this object source) {
            var dictionary = new Dictionary<string, object>();
            if (source != null) {
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source)) {
                    object value = property.GetValue(source);
                    if (value != null && property.PropertyType.FullName.IndexOf("DateTime") != -1) {
                        value = ((DateTime)value).AsDateString();
                    }
                    dictionary.Add(property.Name, value);
                }
            }
            return dictionary;
        }

        public static T DeepClone<T>(this T source) {
            using (MemoryStream stream = new MemoryStream()) {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, source);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }

        public static string AsDateString(this DateTime source) {
            return source.ToString("yyyy/MM/dd");
        }

        public static string AsDateTimeString(this DateTime source) {
            return source.ToString("yyyy/MM/dd HH:mm");
        }

        public static string ToHexString(this string source) {
            string hexOutput = "";
            foreach (char c in source.ToCharArray()) {
                hexOutput += String.Format("{0:X2}", Convert.ToInt32(c));
            }
            return hexOutput;
        }

        public static string ToHexString(this byte[] source) {
            StringBuilder hex = new StringBuilder(source.Length * 2);
            foreach (byte b in source) {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        public static string ToMD5(this string source) {
            return MD5.Create().ComputeHash(Encoding.Default.GetBytes(source)).ToHexString();
        }

        public static string ToEncodeBase64(this string source) {
            return source == null ? "" : Convert.ToBase64String(Encoding.UTF8.GetBytes(source));
        }

        public static string ToDecodeBase64(this string source) {
            return source == null ? "" : Encoding.UTF8.GetString(Convert.FromBase64String(source));
        }

        public static string ToCapitalize(this string source) {
            return string.IsNullOrEmpty(source) ? string.Empty : char.ToUpper(source[0]) + source.Substring(1);
        }

        static private Regex findParameters = new Regex("\\{(?<param>.*?)\\}", RegexOptions.Compiled | RegexOptions.Singleline);

        public static string FormatNamed(this string format, Dictionary<string, object> args) {
            if (string.IsNullOrEmpty(format)) {
                return "";
            }
            return findParameters.Replace(format, delegate (Match match) {
                string[] param = match.Groups["param"].Value.Split(new char[] { ':' }, 2);

                object value;
                if (param[0].IndexOf('.') != -1) {
                    var param2 = param[0].Split('.');
                    if (!args.TryGetValue(param2[0], out value)) {
                        value = match.Value;
                    } else {
                        if (!((Dictionary<string, object>)value).TryGetValue(param2[1], out value)) {
                            value = match.Value;
                        }
                    }
                } else if (!args.TryGetValue(param[0], out value)) {
                    value = match.Value;
                }
                if ((param.Length == 2) && (param[1].Length != 0)) {
                    return string.Format(CultureInfo.CurrentCulture, "{0:" + param[1] + "}", value);
                } else {
                    return value.ToString();
                }
            });
        }

        public static string CHTPadRight(this string word, int number) {
            return word + string.Empty.PadRight(number - Encoding.Default.GetByteCount(word));
        }

        public static DataTable ToDataTable<T>(this IList<T> data) {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties) {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            foreach (T item in data) {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties) {
                    row[prop.Name] = prop.GetValue(item) == null ? "" : prop.GetValue(item).ToString();
                }
                table.Rows.Add(row);
            }
            return table;
        }

        public static List<string> ToList(this byte[] content) {
            string s = Encoding.Default.GetString(content, 0, content.Length - 1);
            return new List<string>(s.Split(new[] { Environment.NewLine }, StringSplitOptions.None));
        }

        public static List<string> ToDotList(this string content) {
            return content.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).Select(s => s.Trim()).ToList();
        }

        private static void ThrowExceptionWhenSourceArgumentIsNull() {
            throw new ArgumentNullException("source", "Unable to convert object to a dictionary. The source object is null.");
        }
    }
}