
using Limilabs.FTP.Client;

namespace TestApp.FTP
{
    public class TestFtp
    {
        public static void Upload()
        {
            //using (FluentFTP.FtpClient ftp = new("ftp.ciotems.com", "collector", "Cete@712"))
            //{
            //    ftp.Connect();
            //    if (ftp.DirectoryExists("logs"))
            //    {
            //        ftp.CreateDirectory("logs");
            //    }
            //    ftp.SetWorkingDirectory("logs");
            //    ftp.UploadFile(@"D:\迅雷下载\c0011003201806150011_20250409_log.log", "logs.txt");
            //}

            using (Ftp ftp = new Ftp())
            {
                ftp.Connect("ftp.ciotems.com");
                ftp.Login("collector", "Cete@712");
                ftp.ChangeFolder("logs");
                ftp.Upload("logs.txt", @"D:\迅雷下载\c0011003201806150011_20250409_log.log");
                ftp.Close();
            }
        }
    }
}
