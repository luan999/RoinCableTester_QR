using System;
using System.Security.Cryptography;
using System.Text;

namespace RoinCableTester.Utils {
    public class Simple3Des {

        TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider();

        public Simple3Des(string key) {
            tripleDes.Key = TruncateHash(key, tripleDes.KeySize / 8);
            tripleDes.IV = TruncateHash("", tripleDes.BlockSize / 8);
        }

        private byte[] TruncateHash(string key, int length) {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] keyBytes = Encoding.Unicode.GetBytes(key);
            byte[] hash = sha1.ComputeHash(keyBytes);
            Array.Resize(ref hash, length);
            return hash;
        }

        public string EncryptData(string plainText) {
            byte[] plainTextBytes = Encoding.Unicode.GetBytes(plainText);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream encStream = new CryptoStream(ms, tripleDes.CreateEncryptor(), CryptoStreamMode.Write);

            encStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            encStream.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }

        public string DecryptData(string encryptedText) {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream decStream = new CryptoStream(ms, tripleDes.CreateDecryptor(), CryptoStreamMode.Write);

            decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            decStream.FlushFinalBlock();

            return Encoding.Unicode.GetString(ms.ToArray());
        }
    }
}
