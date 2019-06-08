using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestApp.Common;
using TestApp.WmOcr;

namespace TestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string httpPrefix = "http://";

        private string loginIndexUrl = "www.95598.cn/member/login.shtml";

        private string imgVcodeUrl = "www.95598.cn/95598/imageCode/getImgCode";

        private string datailUrl = "www.95598.cn/gov/as/ecq/quePowerInfo.jsp?partNo=GM03001002";

        private int submit = 0;

        private bool nextMonth = false;

        private List<string> monthList = new List<string>();

        private string lastUrl = string.Empty;

        private void Form1_Load(object sender, EventArgs e)
        {
            submit = 0;
            webBrowser1.ScriptErrorsSuppressed = true;
            GetALL12();
            Login();
        }

        private void btnReLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void btnRefByDate_Click(object sender, EventArgs e)
        {
            nextMonth = false;
            var month = DateTime.Now.ToString("yyyy.MM");
            webBrowser1.Document.InvokeScript("getBillList", new object[] { month });
        }

        private void btnGet12_Click(object sender, EventArgs e)
        {
            GetALL12();
        }

        private void GetALL12()
        {
            nextMonth = false;
            if (nextMonth)
            {
                monthList = new List<string>();
                for (int i = 0; i < 12; i++)
                {
                    monthList.Add(DateTime.Now.AddMonths(-i).ToString("yyyy.MM"));
                }
            }
        }

        private void Login()
        {
            webBrowser1.Navigate(httpPrefix + loginIndexUrl);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                if (webBrowser1.ReadyState < WebBrowserReadyState.Complete)
                {
                    return;
                }
                //if (webBrowser1.ReadyState < WebBrowserReadyState.Complete || webBrowser1.Url.ToString() == LastUrl)

                lastUrl = webBrowser1.Url.ToString();
                lbUrl.Text = webBrowser1.Url.ToString();
                if (lastUrl.Contains(loginIndexUrl))
                {
                    //株洲日望精工有限公司
                    //4340295598
                    webBrowser1.Document.GetElementById("loginName").SetAttribute("value", "miarat");
                    webBrowser1.Document.GetElementById("pwd").SetAttribute("value", "1234568o");
                    webBrowser1.Document.GetElementById("code").SetAttribute("value", OcrImgVcode(httpPrefix + imgVcodeUrl));
                    webBrowser1.Document.InvokeScript("submitLoginForm");
                    lbLogin.Text = $"第{submit++}次登录;";

                    StartDelayTask(5,
                        () =>
                        {
                            var dataStr = webBrowser1.Document.GetElementById("loginMsg")?.InnerText;
                            if (dataStr == "验证码错误，请重新输入!")
                            {
                                Login();
                            }
                        }, this);
                }
                else if (lastUrl.Contains(datailUrl))
                {
                    StartDelayTask(3,
                        () =>
                        {
                            var dataStr = webBrowser1.Document.GetElementById("elecTitle")?.InnerText;
                            txtMsg.Text += dataStr + "\r\n";
                            if (!string.IsNullOrEmpty(dataStr))
                            {
                                WriteData(dataStr);
                                var month = DateTimeHelper.ConvertStrToDatetime(dataStr.Substring(dataStr.IndexOf("年") - 4, 8), "yyyy年MM月").ToString("yyyy.MM");
                                monthList = monthList.Where(q => q != month).ToList();

                                if (nextMonth && timer1.Interval <= 3000)
                                {
                                    timer1.Interval = 5 * 1000;
                                    timer1.Start();
                                }
                            }
                        }, this);
                }
                else
                {
                    lbLogin.Text += "登录成功";
                    webBrowser1.Navigate(httpPrefix + datailUrl);
                }
            }
            catch (Exception)
            {
                webBrowser1.Navigate(httpPrefix + loginIndexUrl);
            }
        }

        private string OcrImgVcode(string url)
        {
            if (WmCode.LoadWmFromFile(@"D:\wwwroot\完美验证码识别系统V3.1\gwyzm\gwyzm.dat", "yzhn6666"))
            {
                WmCode.SetWmOption(1, 313);
                //下载图片
                string ImgPath = System.Environment.CurrentDirectory + "\\temp.tmp";
                WmCode.URLDownloadToFile(0, url, ImgPath, 0, 0);
                //读取图片
                FileStream fsMyfile = File.OpenRead(ImgPath);
                int FileLen = (int)fsMyfile.Length;
                byte[] Buffer = new byte[FileLen];
                fsMyfile.Read(Buffer, 0, FileLen);
                fsMyfile.Close();
                //识别图片
                StringBuilder sb = new StringBuilder();
                if (WmCode.GetImageFromBuffer(Buffer, FileLen, sb))
                {
                    var vCode = sb.ToString();
                    return vCode;
                }
            }
            return "失败";
        }

        /// <summary>
        /// 开始一个延时任务
        /// </summary>
        /// <param name="DelayTime">延时时长（秒）</param>
        /// <param name="taskEndAction">延时时间完毕之后执行的委托（会跳转回UI线程）</param>
        /// <param name="control">UI线程的控件</param>
        public void StartDelayTask(int DelayTime, Action taskEndAction, Control control)
        {
            if (control == null)
            {
                return;
            }

            Task task = new Task(() =>
            {
                try
                {
                    Thread.Sleep(DelayTime * 1000);

                    //返回UI线程
                    control.Invoke(new Action(() =>
                    {
                        taskEndAction();
                    }));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            });

            task.Start();
        }

        public void WriteData(string data)
        {
            var dataFilePath = @"D:\wwwroot\data\data.txt";
            if (!File.Exists(dataFilePath))
            {
                File.Create(dataFilePath);
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(dataFilePath, true))
            {
                file.WriteLine(data);// 直接追加文件末尾，换行
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (monthList.Count > 0)
            {
                var monthNext = monthList.FirstOrDefault();
                webBrowser1.Document.InvokeScript("getBillList", new object[] { monthNext });
            }
            else
            {
                txtMsg.Text += "获取结束" + "\r\n";
                timer1.Interval = 1000;
                timer1.Stop();
            }
        }

        private void btnGetCookie_Click(object sender, EventArgs e)
        {
            string pCookie = FullWebBrowserCookie.GetCookieInternal(webBrowser1.Url, false);
            txtMsg.Text += pCookie + "\r\n";
            PostRequest("2018-08", pCookie);
        }

        public string PostRequest(string month, string pCookie)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(httpPrefix + datailUrl);

            request.Method = "POST";
            request.Accept = "*/*";
            request.Headers.Add("Accept-Language", "zh-Hans-CN,zh-Hans;q=0.8,en-US;q=0.5,en;q=0.3");
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.2; WOW64; Trident/7.0; .NET4.0C; .NET4.0E; .NET CLR 2.0.50727; .NET CLR 3.0.30729; .NET CLR 3.5.30729; BRI/2)";
            request.Referer = "http://www.95598.cn/gov/as/ecq/quePowerInfo.jsp?partNo=GM03001002";
            request.Headers.Add("Cookie", pCookie);

            byte[] data = new ASCIIEncoding().GetBytes("amtYm=" + month);
            request.ContentLength = data.Length;
            Stream newStream = request.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress), Encoding.UTF8);
            var jData = sr.ReadToEnd();
            sr.Close();
            return jData;
        }
    }
}