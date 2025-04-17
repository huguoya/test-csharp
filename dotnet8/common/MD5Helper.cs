using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace dotnet8.common
{
    public class MD5Helper
    {
        public static string Md5Encrypt(string str)
        {
            // Updated to use MD5.Create()
            byte[] data = MD5.HashData(Encoding.UTF8.GetBytes(str));
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// 对文件流进行MD5加密
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <example></example>
        public static string MD5Stream(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (MD5 md5 = MD5.Create()) // Updated to use MD5.Create()
            {
                md5.ComputeHash(fs);

                byte[] b = md5.Hash;

                StringBuilder sb = new StringBuilder(32);
                for (int i = 0; i < b.Length; i++)
                {
                    sb.Append(b[i].ToString("X2"));
                }

                Console.WriteLine(sb.ToString());
                Console.ReadLine();

                return sb.ToString();
            }
        }

        /// <summary>
        /// 对文件进行MD5加密
        /// </summary>
        /// <param name="filePath"></param>
        public static string MD5File(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (MD5 md5 = MD5.Create()) // Updated to use MD5.Create()
            {
                int bufferSize = 1048576; // 缓冲区大小，1MB
                byte[] buff = new byte[bufferSize];

                md5.Initialize();

                long offset = 0;
                while (offset < fs.Length)
                {
                    long readSize = bufferSize;
                    if (offset + readSize > fs.Length)
                    {
                        readSize = fs.Length - offset;
                    }

                    fs.Read(buff, 0, Convert.ToInt32(readSize)); // 读取一段数据到缓冲区

                    if (offset + readSize < fs.Length) // 不是最后一块
                    {
                        md5.TransformBlock(buff, 0, Convert.ToInt32(readSize), buff, 0);
                    }
                    else // 最后一块
                    {
                        md5.TransformFinalBlock(buff, 0, Convert.ToInt32(readSize));
                    }

                    offset += bufferSize;
                }

                byte[] result = md5.Hash;

                StringBuilder sb = new StringBuilder(32);
                for (int i = 0; i < result.Length; i++)
                {
                    sb.Append(result[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}