using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace ciotems.open
{
    public class Encryption
    {
        private static string Vector = "ciotems";

        public static string Encrypt(string data, string key)
        {
            var dataBytes = System.Text.Encoding.UTF8.GetBytes(data);
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
            var decDataStr = Convert.ToBase64String(decDataBytes);
            var str = System.Text.Encoding.UTF8.GetString(decDataBytes, 0, decDataBytes.Length);
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

        /// 加密
        /// </summary>
        /// <param name="toEncrypt">明文</param>
        /// <param name="key">秘钥</param>
        /// <param name="ivBytes">向量</param>
        /// <returns>密文</returns>
        public static byte[] AESEncrypt(byte[] toEncrypt, byte[] key, byte[] ivBytes)
        {
            var rijndael = new RijndaelManaged();
            rijndael.Key = key;
            rijndael.IV = ivBytes;
            ICryptoTransform cryptoTransform = rijndael.CreateEncryptor();
            byte[] resultBytes = cryptoTransform.TransformFinalBlock(toEncrypt, 0, toEncrypt.Length);
            return resultBytes;
        }

        /// 解密
        /// </summary>
        /// <param name="toDecrypt">密文</param>
        /// <param name="key">秘钥</param>
        /// <param name="ivBytes">向量</param>
        /// <returns>明文</returns>
        private static byte[] AESDecrypt(byte[] toDecrypt, byte[] key, byte[] ivBytes)
        {
            var rijndael = new RijndaelManaged();
            rijndael.Key = key;
            rijndael.IV = ivBytes;
            ICryptoTransform cryptoTransform = rijndael.CreateDecryptor();
            byte[] resultArray = cryptoTransform.TransformFinalBlock(toDecrypt, 0, toDecrypt.Length);
            return resultArray;
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="Data">被加密的明文</param>
        /// <param name="Key">密钥</param>
        /// <param name="Vector">向量</param>
        /// <returns>密文</returns>
        private static byte[] AESEncrypt2(byte[] plainBytes, byte[] bKey, byte[] bVector)
        {
            byte[] cryptograph = null; // 加密后的密文

            try
            {
                // 开辟一块内存流
                using (MemoryStream Memory = new MemoryStream())
                {
                    Rijndael aes = Rijndael.Create();
                    // 把内存流对象包装成加密流对象
                    using (CryptoStream Encryptor = new CryptoStream(Memory,
                     aes.CreateEncryptor(bKey, bVector),
                     CryptoStreamMode.Write))
                    {
                        // 明文数据写入加密流
                        Encryptor.Write(plainBytes, 0, plainBytes.Length);
                        Encryptor.FlushFinalBlock();

                        cryptograph = Memory.ToArray();
                    }
                }
            }
            catch (Exception)
            {
                cryptograph = null;
            }

            return cryptograph;
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="Data">被解密的密文</param>
        /// <param name="Key">密钥</param>
        /// <param name="Vector">向量</param>
        /// <returns>明文</returns>
        private static byte[] AESDecrypt2(byte[] encryptedBytes, byte[] bKey, byte[] bVector)
        {
            byte[] original = null; // 解密后的明文

            try
            {
                // 开辟一块内存流，存储密文
                using (MemoryStream Memory = new MemoryStream(encryptedBytes))
                {
                    Rijndael Aes = Rijndael.Create();

                    // 把内存流对象包装成加密流对象
                    using (CryptoStream Decryptor = new CryptoStream(Memory,
                    Aes.CreateDecryptor(bKey, bVector),
                    CryptoStreamMode.Read))
                    {
                        // 明文存储区
                        using (MemoryStream originalMemory = new MemoryStream())
                        {
                            byte[] Buffer = new byte[1024];
                            Int32 readBytes = 0;
                            while ((readBytes = Decryptor.Read(Buffer, 0, Buffer.Length)) > 0)
                            {
                                originalMemory.Write(Buffer, 0, readBytes);
                            }

                            original = originalMemory.ToArray();
                        }
                    }
                }
            }
            catch (Exception)
            {
                original = null;
            }
            return original;
        }

        /// <summary>
        /// AES加密(无向量)
        /// </summary>
        /// <param name="plainBytes">被加密的明文</param>
        /// <param name="key">密钥</param>
        /// <returns>密文</returns>
        private static string AESEncrypt2(String Data, String Key)
        {
            MemoryStream mStream = new MemoryStream();
            RijndaelManaged aes = new RijndaelManaged();

            byte[] plainBytes = Encoding.UTF8.GetBytes(Data);
            byte[] bKey = new byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);

            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 128;
            //aes.Key = _key;
            aes.Key = bKey;
            //aes.IV = _iV;
            CryptoStream cryptoStream = new CryptoStream(mStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            try
            {
                cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                cryptoStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            finally
            {
                cryptoStream.Close();
                mStream.Close();
                aes.Clear();
            }
        }

        /// <summary>
        /// AES解密(无向量)
        /// </summary>
        /// <param name="encryptedBytes">被加密的明文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        private static string AESDecrypt2(String Data, String Key)
        {
            byte[] encryptedBytes = Convert.FromBase64String(Data);
            byte[] bKey = new byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);

            MemoryStream mStream = new MemoryStream(encryptedBytes);
            //mStream.Write( encryptedBytes, 0, encryptedBytes.Length );
            //mStream.Seek( 0, SeekOrigin.Begin );
            RijndaelManaged aes = new RijndaelManaged();
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 128;
            aes.Key = bKey;
            //aes.IV = _iV;
            CryptoStream cryptoStream = new CryptoStream(mStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
            try
            {
                byte[] tmp = new byte[encryptedBytes.Length + 32];
                int len = cryptoStream.Read(tmp, 0, encryptedBytes.Length + 32);
                byte[] ret = new byte[len];
                Array.Copy(tmp, 0, ret, 0, len);
                return Encoding.UTF8.GetString(ret, 0, ret.Length);
            }
            finally
            {
                cryptoStream.Close();
                mStream.Close();
                aes.Clear();
            }
        }
    }
}