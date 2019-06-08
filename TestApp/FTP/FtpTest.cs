using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Limilabs.FTP.Client;

namespace TestApp.FTP
{
  public  class FtpTest
    {
        public void Upload()
        {
            using (Ftp ftp = new Ftp())
            {
                ftp.Connect("ftp.ciotems.com");
                ftp.Login("collector", "Cete@712");
                ftp.ChangeFolder("logs");
                ftp.Upload("logs.txt", @"D:\桌面\新建文件夹\log.txt.2");
                ftp.Close();
            }
        }
    }
}
