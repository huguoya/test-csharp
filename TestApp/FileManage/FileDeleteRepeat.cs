using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Common;

namespace TestApp.FileManage
{
    public class FileDeleteRepeat
    {
        public List<FileAtrr> FilesList = new List<FileAtrr>();

        public void TestDelete()
        {
            Console.WriteLine("请输入筛选路径:");
            //string dirPath = Console.ReadLine();
            string dirPath = @"D:\A我的文件\我的图片";
            //Delete(dirPath);
            RenameFile(dirPath);
            Console.ReadLine();
        }

        private void Delete(string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                List<string> filePathList = Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories).ToList();
                int delNum = 0;
                int searchNum = 0;
                foreach (var filePath in filePathList)
                {
                    searchNum++;
                    if (searchNum % 10 == 0)
                    {
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine($"第{searchNum};{Math.Round(searchNum * 100.0 / filePathList.Count, 2)}%");
                    }

                    FileInfo fi = new FileInfo(filePath);
                    long len = fi.Length;
                    if (len > 0)
                    {
                        string md5 = string.Empty;
                        if (len > 20 * 1024 * 1024)
                        {
                            md5 = MD5Helper.Md5Encrypt(len.ToString());
                        }
                        else
                        {
                            md5 = MD5Helper.MD5File(filePath);
                        }

                        if (FilesList.Any(q => q.Md5 == md5))
                        {
                            File.Delete(filePath);
                            delNum++;
                            Console.WriteLine("--------------------------------------------------");
                            Console.WriteLine(delNum);
                            Console.WriteLine(FilesList.Where(q => q.Md5 == md5).FirstOrDefault().FilePath);
                            Console.WriteLine(filePath);
                            Console.WriteLine("delete {0}", filePath);
                        }
                        else
                        {
                            FilesList.Add(new FileAtrr() { FilePath = filePath, Md5 = md5 });
                        }
                    }
                    else
                    {
                        Console.WriteLine("--------------------------------------------------");
                        File.Delete(filePath);
                        Console.WriteLine("0MB");
                        Console.WriteLine(filePath);
                        delNum++;
                    }
                }
                //Console.WriteLine(filesPath.Count);
                //Console.WriteLine(delNum);
            }
            Console.WriteLine("over");
        }

        private void DeleteBig(string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                List<string> filesPath = Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories).ToList();
                int delNum = 0;
                int searchNum = 0;
                foreach (var filePath in filesPath)
                {
                    searchNum++;
                    if (searchNum % 100 == 0)
                    {
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine("第" + searchNum);
                    }

                    FileInfo fi = new FileInfo(filePath);
                    long len = fi.Length;
                    if (len > 0)
                    {
                        List<FileAtrr> fl = FilesList.Where(q => q.FileLong == len).ToList();
                        if (fl.Count() > 0)
                        {
                            File.Delete(filePath);
                            delNum++;
                            Console.WriteLine("--------------------------------------------------");
                            Console.WriteLine(delNum);
                            Console.WriteLine(FilesList.Where(q => q.FileLong == len).FirstOrDefault().FilePath);
                            Console.WriteLine(filePath);
                            Console.WriteLine("delete {0}", filePath);
                        }
                        else
                        {
                            FilesList.Add(new FileAtrr() { FilePath = filePath, FileLong = len });
                        }
                    }
                    else
                    {
                        Console.WriteLine("--------------------------------------------------");
                        File.Delete(filePath);
                        Console.WriteLine("0MB");
                        Console.WriteLine(filePath);
                        delNum++;
                    }
                }
                //Console.WriteLine(filesPath.Count);
                //Console.WriteLine(delNum);
            }
            Console.WriteLine("over");
            Console.ReadLine();
        }

        private void RenameFile(string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                List<string> filePathList = Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories).ToList();
                int searchNum = 0;
                foreach (var filePath in filePathList)
                {
                    searchNum++;
                    if (searchNum % 10 == 0)
                    {
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine($"第{searchNum};{Math.Round(searchNum * 100.0 / filePathList.Count, 2)}%");
                    }

                    FileInfo fi = new FileInfo(filePath);
                    if (fi.FullName.Contains(@"(1)"))
                    {
                        string newPath = fi.FullName.Replace(@"(1)", "");
                        fi.MoveTo(Path.Combine(newPath));
                    }
                }
                //Console.WriteLine(filesPath.Count);
                //Console.WriteLine(delNum);
            }
            Console.WriteLine("over");
        }
    }

    public class FileAtrr
    {
        public string FilePath { get; set; }
        public string Md5 { get; set; }
        public long FileLong { get; set; }
    }
}