using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32;

namespace dotnet8;

class TestRegedit
{
    private static void test1()
    {
        // 尝试打开 HKEY_LOCAL_MACHINE 下的一个键，以只读模式
        //using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", false))
        //{
        //    if (key != null)
        //    {
        //        // 读取键的值
        //        object value = key.GetValue("SomeValue");
        //        if (value != null)
        //        {
        //            Console.WriteLine($"Value: {value}");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Failed to open the key in HKEY_LOCAL_MACHINE.");
        //    }
        //}

        // 尝试打开 HKEY_CURRENT_USER 下的一个键，以可写模式
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
        //    uk.SetValue("ectest", "MyData");
        //}
    }
}
