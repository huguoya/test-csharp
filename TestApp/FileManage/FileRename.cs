using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestApp.FileManage
{
    public class FileRename
    {
        public void Rename()
        {
            DirectoryInfo di = new DirectoryInfo(@"C:\Users\HuGuoya\Desktop\yg");
            var files = di.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.Contains("下载"))
                {
                    var newname = files[i].Name.Substring(0, files[i].Name.Length - 3);
                    files[i].CopyTo(newname);
                }
            }
        }

        public void Rename2()
        {
            DirectoryInfo di = new DirectoryInfo(@"E:\files\Video\A");
            var files = di.GetFiles("*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.StartsWith("B "))
                {
                    try
                    {
                        var newname = files[i].Name.Substring(2);
                        var newFullName = files[i].DirectoryName + "\\" + newname;
                        files[i].MoveTo(newFullName);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public void Rename3()
        {
            DirectoryInfo di = new DirectoryInfo(@"E:\files\Video\A");
            var files = di.GetFiles("*", SearchOption.AllDirectories);
            var keyName = "无码";
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.Contains(keyName))
                {
                    try
                    {
                        var strs = files[i].Name.Split(' ').ToList().Where(q => !string.IsNullOrWhiteSpace(q)).ToArray();
                        if (strs.Length >= 3 && strs[1] == keyName)
                        {
                            strs[1] = strs[2];
                            strs[2] = keyName;

                            var newname = string.Join(" ", strs);
                            var newFullName = files[i].DirectoryName + "\\" + newname;
                            files[i].MoveTo(newFullName);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }


        public void Rename4()
        {
            DirectoryInfo di = new DirectoryInfo(@"E:\v_cache\picture_t");
            var files = di.GetFiles("[XINGYAN星颜社]*.rar", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    var index1 = files[i].Name.IndexOf("VOL.") + 4;
                    var newname = "XY" + files[i].Name.Substring(index1);
                    var newFullName = files[i].DirectoryName + "\\" + newname;
                    files[i].MoveTo(newFullName);

                }
                catch (Exception)
                {
                }
            }
        }


        public void Rename5()
        {
            var dir = @"C:\Users\huguo\OneDrive - business\A工作文件\";
            DirectoryInfo di = new DirectoryInfo(dir);
            var dis = di.GetDirectories();
            foreach (var item in dis)
            {
                if (item.Name.StartsWith("2018") && !item.Name.StartsWith("2018-"))
                {
                    var newName = dir + item.Name.Substring(0, 4) + "-" + item.Name.Substring(4, 2) + "-" + item.Name.Substring(6);
                    item.MoveTo(newName);
                }
            }
        }

        public void Rename6()
        {
        }
        public void Rename7()
        {
        }

    }
}