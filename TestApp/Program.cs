using System;
using System.Reflection;
using System.Windows.Forms;

using Microsoft.Win32;

using TestApp.Common;

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

            //new TestClass().TestBcd();

            //new TestModbus().TestEasyModbus();

            //var s = new TestClass().GetMathResult("10*10.101");




            //string path = Assembly.GetEntryAssembly().Location.Replace(".dll", ".exe");
            //Console.WriteLine($"Path using Assembly.GetEntryAssembly().Location: {path}");

            //// 尝试打开 HKEY_CURRENT_USER 下的一个键，以可写模式,开机启动？
            //string key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

            //using (RegistryKey? userKey = Registry.CurrentUser.OpenSubKey(key, true))
            //{
            //    if (userKey == null)
            //    {
            //        // 如果键不存在，创建它
            //        Registry.CurrentUser.CreateSubKey(key);
            //    }
            //    if (userKey == null)
            //    {
            //        return;
            //    }
            //    RegistryKey uk = (RegistryKey)userKey;
            //    // 写入一个值
            //    uk.SetValue("ectest", path);
            //}
            //// To customize application configuration such as set high DPI settings or default font,
            //// see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
            Console.WriteLine("------over");
            Console.ReadLine();
        }
    }
}