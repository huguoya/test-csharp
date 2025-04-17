using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace dotnet8.FileManage
{
    public class DownPic
    {
        private readonly string dirPath = @"E:\MyFile\new\p\";

        public void TestMain()
        {
            Console.WriteLine("TestMain");
            string url = "http://720.kaixin.ru/xz/cn/";
            string html = DownHtml(url);
            List<string> urlList = GetUrlList(html);
            List<string> jpgUrlList = GetJpgUrlList(urlList);
            List<string> domUrlList = GetUrlList(urlList);
            WriteUrlList(domUrlList);
        }

        public void TestMain3()
        {
            Console.WriteLine("TestMain3");
            foreach (var fileDir in Directory.GetFiles(dirPath))
            {
                Console.WriteLine("anlysis:" + fileDir);
                string html = File.ReadAllText(fileDir);
                List<string> urlList = GetUrlList(html);
                string fileName = fileDir.Substring(0, fileDir.LastIndexOf('\\'));
                WriteUrlList(urlList, dirPath + fileName + "\\" + fileName + ".txt");
                Console.WriteLine("---------------");
            }
        }
        private static readonly HttpClient httpClient = new(); // Use a single instance of HttpClient

        public string DownHtml(string url)
        {
            try
            {
                return httpClient.GetStringAsync(url).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return string.Empty;
            }
        }

        public List<string> GetUrlList(string html)
        {
            string[] strs = html.Split('=');
            List<string> urlList = new List<string>();
            for (int i = 0; i < strs.Length; i++)
            {
                if (strs[i].Contains("http"))
                {
                    urlList.Add(strs[i]);
                }
            }
            return urlList;
        }

        public List<string> GetUrlList(List<string> urlList)
        {
            List<string> jpgUrlList = new List<string>();
            foreach (string url in urlList)
            {
                if (!url.Contains("jpg") && (url.Contains("tn") || url.Contains("zz") || url.Contains("xz")))
                {
                    int start = url.IndexOf("http");
                    int end = url.IndexOf("\"", 4);
                    string urlNew = url.Substring(start, end - start);
                    jpgUrlList.Add(urlNew);
                }
            }
            return jpgUrlList;
        }

        public List<string> GetJpgUrlList(List<string> urlList)
        {
            List<string> jpgUrlList = new List<string>();
            foreach (string url in urlList)
            {
                if (url.Contains("http") && url.Contains("jpg"))
                {
                    int start = url.IndexOf("http");
                    int end = url.IndexOf(".jpg");
                    string jpgUrl = url.Substring(start, end - start + 4);
                    jpgUrlList.Add(jpgUrl);
                }
            }
            return jpgUrlList;
        }

        public List<string> GetJpgUrlList(string html, string url)
        {
            string[] strs = html.Split('=');
            List<string> urlList = new List<string>();
            for (int i = 0; i < strs.Length; i++)
            {
                if (strs[i].Contains(".jpg"))
                {
                    int start = strs[i].IndexOf("images");
                    int end = strs[i].IndexOf(".jpg");
                    string urlnew = url + strs[i].Substring(start, end - start + 4);
                    urlList.Add(urlnew);
                }
            }
            return urlList;
        }

        public void WriteUrlList(List<string> urlList)
        {
            foreach (string url in urlList)
            {
                string html = DownHtml(url);
                string filename = url.Substring(url.IndexOf(".com") + 5).Substring(0, url.Substring(url.IndexOf(".com") + 5).Length - 1);
                List<string> url2List = GetJpgUrlList(html, url);
                Console.WriteLine("start:" + url);
                string fileDirPath = dirPath + filename + @"\";
                if (!Directory.Exists(fileDirPath))
                {
                    Directory.CreateDirectory(fileDirPath);
                }
                string fileFullPath = fileDirPath + filename + ".txt";
                using (StreamWriter sw = new StreamWriter(fileFullPath, true))
                {
                    foreach (string url2 in url2List)
                    {
                        sw.WriteLine(url2);
                    }
                }
                Console.WriteLine("end:" + url);
            }
        }

        public void WriteUrlList(List<string> urlList, string fileName)
        {
            string fileDirPath = fileName.Substring(0, fileName.LastIndexOf('\\'));
            if (!Directory.Exists(fileDirPath))
            {
                Directory.CreateDirectory(fileDirPath);
            }
            using (StreamWriter sw = new StreamWriter(fileDirPath, true))
            {
                foreach (string url in urlList)
                {
                    sw.WriteLine(url);
                }
            }
            Console.WriteLine("end:" + fileName);
        }

        public void DownPicByUrlList(string url)
        {
            try
            {
                string filePath = dirPath + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".jpg";
                using (var response = httpClient.GetAsync(url).Result)
                {
                    response.EnsureSuccessStatusCode();
                    var fileBytes = response.Content.ReadAsByteArrayAsync().Result;
                    File.WriteAllBytes(filePath, fileBytes);
                }

                Console.WriteLine("down:" + filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void DownWithTask(List<string> urlList)
        {
            int itsk = 0;
            Task[] tsks = new Task[urlList.Count];
            foreach (string url in urlList)
            {
                var tsk = Task.Factory.StartNew(() => DownPicByUrlList(url));
                tsks[itsk] = tsk;
                itsk++;
            }
            Task.WaitAll(tsks);
        }
    }
}