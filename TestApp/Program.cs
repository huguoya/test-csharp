using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using ciotems.open;
using TestApp.Common;
using TestApp.FileManage;
using TestApp.FTP;

namespace TestApp
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            //var s = DateTime.Now.Minute % 15;
            //snew DeleteRepeatFile().TestDelete();
            //TestClass testClass = new TestClass();
            //testClass.Sort();

            //Console.WriteLine(Computer.Instance().CpuID);
            //Console.WriteLine(Computer.Instance().CpuID);
            //Console.WriteLine(Computer.Instance().CpuID);
            //Console.WriteLine(Computer.Instance().CpuID);
            //Console.WriteLine(Computer.Instance().CpuID);

            //new FileRename().Rename4();

            //new TestClass().GetHtmlBySocket();

            //new FileRename().RenameDir();
            // 国网数据获取
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            //var data = "胡国亚";
            //var key = "huguoya";
            //var encStr = Encryption.Encrypt(data, key);
            //Console.WriteLine("加密后：" + encStr);

            //var decStr = Encryption.Decrypt(encStr, key);
            //Console.WriteLine("解密后：" + decStr);

            //TestMQTT

            //new TestClass().GetFileVersion();

            new TestClass().TestBcd();
            Console.WriteLine("------over");
            Console.ReadLine();
        }
    }
}