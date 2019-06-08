using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography;

namespace TestApp
{
    public class Untility
    {
        public static string WebResponseGet(string apiStr)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(apiStr);

            StreamReader responseReader = null;

            string responseData = "";
            try
            {
                HttpWebResponse apiRespone = (HttpWebResponse)webRequest.GetResponse();
                if (apiRespone != null && apiRespone.StatusCode == HttpStatusCode.OK)
                {
                    using (responseReader = new StreamReader(apiRespone.GetResponseStream()))
                    {
                        responseData = responseReader.ReadToEnd();
                    }
                }

                if (webRequest != null)
                {
                    webRequest.Abort();
                }
            }
            catch (Exception)
            {
                webRequest.Abort();
                responseReader = null;
                return "";
            }
            finally
            {
                webRequest.ServicePoint.Expect100Continue = false;
                responseReader = null;
            }
            return responseData;
        }

        public static string WebResponsePost(string url, string postData, string encodeType, out string err)
        {
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.GetEncoding(encodeType);
            byte[] data = encoding.GetBytes(postData);

            try
            {
                request = WebRequest.Create(url) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                response = request.GetResponse() as HttpWebResponse;
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                string content = sr.ReadToEnd();
                err = string.Empty;
                return content;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return string.Empty;
            }
        }
    }

    public sealed class DES
    {
        private readonly string _key;

        public DES(string key)
        {
            this._key = key;
        }

        public string Decrypt(string cyphertext)
        {
            if (string.IsNullOrEmpty(cyphertext))
            {
                return string.Empty;
            }
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] buffer = new byte[cyphertext.Length / 2];
            for (int i = 0; i < (cyphertext.Length / 2); i++)
            {
                int num2 = Convert.ToInt32(cyphertext.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte)num2;
            }
            provider.Key = Encoding.ASCII.GetBytes(this._key);
            provider.IV = Encoding.ASCII.GetBytes(this._key);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            StringBuilder builder = new StringBuilder();
            return Encoding.GetEncoding("UTF-8").GetString(stream.ToArray());
        }

        public string Encrypt(string text)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.GetEncoding("UTF-8").GetBytes(text);
            byte[] buffer2 = Encoding.ASCII.GetBytes(this._key);
            provider.Key = Encoding.ASCII.GetBytes(this._key);
            provider.IV = Encoding.ASCII.GetBytes(this._key);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            StringBuilder builder = new StringBuilder();
            foreach (byte num in stream.ToArray())
            {
                builder.AppendFormat("{0:X2}", num);
            }
            return builder.ToString();
        }
    }

    public class FileOperate
    {
        //保存Html文件
        public static void SaveToFile(string filePath, Encoding code, string content = "")
        {
            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    System.IO.File.Create(filePath).Close();
                }

                if (content != "")
                    System.IO.File.WriteAllText(filePath, content, code);
                else
                    throw new Exception(filePath + "文件未写入,原因：将要写入的内容为空");
            }
            catch (Exception)
            {
                string msg = DateTime.Now + "获取文件内容写入失败。\r\n";
            }
        }
    }
}