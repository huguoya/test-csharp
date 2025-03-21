using System.Reflection;

using Microsoft.Win32;

namespace WinFormsApp1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {


            string path = Assembly.GetEntryAssembly().Location.Replace(".dll",".exe");
            Console.WriteLine($"Path using Assembly.GetEntryAssembly().Location: {path}");

            // 尝试打开 HKEY_CURRENT_USER 下的一个键，以可写模式,开机启动？
            string key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

            using (RegistryKey? userKey = Registry.CurrentUser.OpenSubKey(key, true))
            {
                if (userKey == null)
                {
                    // 如果键不存在，创建它
                    Registry.CurrentUser.CreateSubKey(key);
                }
                if (userKey == null)
                {
                    return;
                }
                RegistryKey uk = (RegistryKey)userKey;
                // 写入一个值
                uk.SetValue("ectest", path);
            }
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());

        }
    }
}