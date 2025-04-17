using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TestApp.Common
{
    public class Encryption
    {
        private static string Vector = "ciotems";

        public static string Encrypt(string data, string key)
        {
            var dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] bKey = new byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(key.PadRight(bKey.Length)), bKey, bKey.Length);
            byte[] bVector = new byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(Vector.PadRight(bVector.Length)), bVector, bVector.Length);
            var encDataBytes = AESEncrypt(dataBytes, bKey, bVector);
            var encDataStr = Convert.ToBase64String(encDataBytes);
            return encDataStr;
        }

        public static string Decrypt(string data, string key)
        {
            var dataBytes = Convert.FromBase64String(data);
            byte[] bKey = new byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(key.PadRight(bKey.Length)), bKey, bKey.Length);
            byte[] bVector = new byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(Vector.PadRight(bVector.Length)), bVector, bVector.Length);
            var decDataBytes = AESDecrypt(dataBytes, bKey, bVector);
            var str = Encoding.UTF8.GetString(decDataBytes, 0, decDataBytes.Length);
            return str;
        }

        public static byte[] Encrypt(byte[] dataBytes, string key)
        {
            byte[] bKey = new byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(key.PadRight(bKey.Length)), bKey, bKey.Length);
            byte[] bVector = new byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(Vector.PadRight(bVector.Length)), bVector, bVector.Length);
            var encDataBytes = AESEncrypt(dataBytes, bKey, bVector);
            return encDataBytes;
        }

        public static byte[] Decrypt(byte[] dataBytes, string key)
        {
            byte[] bKey = new byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(key.PadRight(bKey.Length)), bKey, bKey.Length);
            byte[] bVector = new byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(Vector.PadRight(bVector.Length)), bVector, bVector.Length);
            var decDataBytes = AESDecrypt(dataBytes, bKey, bVector);
            return decDataBytes;
        }

        public static byte[] AESEncrypt(byte[] toEncrypt, byte[] key, byte[] ivBytes)
        {
            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = ivBytes;
            using var cryptoTransform = aes.CreateEncryptor();
            return cryptoTransform.TransformFinalBlock(toEncrypt, 0, toEncrypt.Length);
        }

        private static byte[] AESDecrypt(byte[] toDecrypt, byte[] key, byte[] ivBytes)
        {
            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = ivBytes;
            using var cryptoTransform = aes.CreateDecryptor();
            return cryptoTransform.TransformFinalBlock(toDecrypt, 0, toDecrypt.Length);
        }

        private static byte[] AESEncrypt2(byte[] plainBytes, byte[] bKey, byte[] bVector)
        {
            try
            {
                using var memory = new MemoryStream();
                using var aes = Aes.Create();
                using var encryptor = new CryptoStream(memory, aes.CreateEncryptor(bKey, bVector), CryptoStreamMode.Write);
                encryptor.Write(plainBytes, 0, plainBytes.Length);
                encryptor.FlushFinalBlock();
                return memory.ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static byte[] AESDecrypt2(byte[] encryptedBytes, byte[] bKey, byte[] bVector)
        {
            try
            {
                using var memory = new MemoryStream(encryptedBytes);
                using var aes = Aes.Create();
                using var decryptor = new CryptoStream(memory, aes.CreateDecryptor(bKey, bVector), CryptoStreamMode.Read);
                using var originalMemory = new MemoryStream();
                byte[] buffer = new byte[1024];
                int readBytes;
                while ((readBytes = decryptor.Read(buffer, 0, buffer.Length)) > 0)
                {
                    originalMemory.Write(buffer, 0, readBytes);
                }
                return originalMemory.ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static string AESEncrypt2(string data, string key)
        {
            using var mStream = new MemoryStream();
            using var aes = Aes.Create();
            byte[] plainBytes = Encoding.UTF8.GetBytes(data);
            byte[] bKey = new byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(key.PadRight(bKey.Length)), bKey, bKey.Length);

            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 128;
            aes.Key = bKey;

            using var cryptoStream = new CryptoStream(mStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
            cryptoStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        private static string AESDecrypt2(string data, string key)
        {
            byte[] encryptedBytes = Convert.FromBase64String(data);
            byte[] bKey = new byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(key.PadRight(bKey.Length)), bKey, bKey.Length);

            using var mStream = new MemoryStream(encryptedBytes);
            using var aes = Aes.Create();
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 128;
            aes.Key = bKey;

            using var cryptoStream = new CryptoStream(mStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
            byte[] tmp = new byte[encryptedBytes.Length + 32];
            int len = cryptoStream.Read(tmp, 0, encryptedBytes.Length + 32);
            byte[] ret = new byte[len];
            Array.Copy(tmp, 0, ret, 0, len);
            return Encoding.UTF8.GetString(ret, 0, ret.Length);
        }
    }
}