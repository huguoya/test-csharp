using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace TestApp.Common
{
    [DataContract]
    public class Project
    {
        [DataMember]
        public string Input { get; set; }

        [DataMember]
        public string Output { get; set; }
    }

    public class TestClass
    {
        public void Sort()
        {
            var str = "123 23423 34 348 2345 3234 951 2344 444 4344";
            var strArray = new string[str.Length];
            var index = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ')
                {
                    index++;
                }
                else
                {
                    strArray[index] += str[i];
                }
            }
            var strArray2 = new string[index + 1];
            for (int i = 0; i <= index; i++)
            {
                strArray2[i] = strArray[i];
            }
            for (int i = 0; i < strArray2.Length; i++)
            {
                for (int j = 0; j < strArray2.Length - i - 1; j++)
                {
                    var valueLeft = GetValueLast3(strArray2[j]);
                    var valueRight = GetValueLast3(strArray2[j + 1]);
                    if (valueLeft > valueRight)
                    {
                        var tmp = strArray2[j + 1];
                        strArray2[j + 1] = strArray2[j];
                        strArray2[j] = tmp;
                    }
                }
            }
            for (int i = 0; i < strArray2.Length; i++)
            {
                Console.WriteLine(strArray2[i].PadLeft(10));
            }
        }

        public int GetValueLast3(string strTmp)
        {
            return Convert.ToInt32(strTmp.Length > 3 ? strTmp.Substring(strTmp.Length - 3) : strTmp);
        }

        public void GetIotCard()
        {
            //用户code码
            var entCode = "085458";
            //用户密码
            var secret = "2E1B811F701F3C7CEFD031689FE82924";
            //物联卡基本信息（可用）
            var API_IotCardInfoApi = "http://wl.51liubei.com/dhApi/iotCardInfoApi.api";

            var iccid = "8986031746202530209Y";
            RestSharp.Http http = new RestSharp.Http();
            StringBuilder url = new StringBuilder(API_IotCardInfoApi);
            url.Append("entCode=").Append(entCode).Append("&secret=").Append(secret)
                     .Append("&iccid=").Append(iccid).Append("&access_token=");
            //.Append(MD5.getAccessToken(Constants.entCode, Constants.secret));
            //http.Url = new Uri();
            http.Get();
        }

        private void TestReadXmlConfig(string[] args)
        {
            string filePath = @"D:\Projects\EnergyCollector\EnergyCollector\EnergyCollector.Main\bin\Debug\ConfigFiles\globalUrl.config";
            string xmlName = "MetersTpye";
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains(xmlName) && line.Contains("value"))
                    {
                        string[] lineStrs = line.Split('\"');
                        if (lineStrs.Length == 5)
                        {
                            for (int i = 0; i < lineStrs.Length; i++)
                            {
                                Console.WriteLine(i + ":" + lineStrs[i]);
                            }
                            break;
                        }
                    }
                }
            }
            Console.ReadLine();
        }

        public void TestJson(string[] args)
        {
            string jsonText = @"{""input"" : ""value"", ""output"" : ""result""}";
            ////1
            JsonReader reader = new JsonTextReader(new StringReader(jsonText));
            //while (reader.Read())
            //{
            //    Console.WriteLine(reader.TokenType + "\t\t" + reader.ValueType + "\t\t" + reader.Value);
            //}

            ////2
            //StringWriter sw = new StringWriter();
            //JsonWriter writer = new JsonTextWriter(sw);
            //writer.WriteStartObject();
            //writer.WritePropertyName("input");
            //writer.WriteValue("value");
            //writer.WritePropertyName("output");
            //writer.WriteValue("result");
            //writer.WriteEndObject();
            //writer.Flush();
            //string jsonText = sw.GetStringBuilder().ToString();
            //Console.WriteLine(jsonText);

            ////3
            //JObject jo = JObject.Parse(jsonText);
            //string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();
            //foreach (var item in values)
            //{
            //    Console.WriteLine(item);
            //}

            ////4
            //Project p = new Project() { Input = "stone", Output = "gold" };
            //JsonSerializer serializer = new JsonSerializer();
            //StringWriter sw = new StringWriter();
            //serializer.Serialize(new JsonTextWriter(sw), p);
            //Console.WriteLine(sw.GetStringBuilder().ToString());

            //StringReader sr = new StringReader(@"{""Input"":""stone"", ""Output"":""gold""}");
            //Project p1 = (Project)serializer.Deserialize(new JsonTextReader(sr), typeof(Project));
            //Console.WriteLine(p1.Input + "=>" + p1.Output);

            ////5
            //Project p = new Project() { Input = "stone", Output = "gold" };
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //var json = serializer.Serialize(p);
            //Console.WriteLine(json);

            //var p1 = serializer.Deserialize<Project>(json);
            //Console.WriteLine(p1.Input + "=>" + p1.Output);
            //Console.WriteLine(ReferenceEquals(p, p1));

            //6
            Project p = new Project() { Input = "stone", Output = "gold" };
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(p.GetType());

            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, p);
                jsonText = Encoding.UTF8.GetString(stream.ToArray());
                Console.WriteLine(jsonText);
            }

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonText)))
            {
                DataContractJsonSerializer serializer1 = new DataContractJsonSerializer(typeof(Project));
                Project p1 = (Project)serializer1.ReadObject(ms);
                Console.WriteLine(p1.Input + "=>" + p1.Output);
            }

            Console.ReadLine();
        }

        public string GetSig4Lbt5350()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.10.1");

                request.Credentials = new NetworkCredential("admin", "admin");
                //得到返回
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //得到流
                Stream recStream = response.GetResponseStream();

                //编码方式
                Encoding gb2312 = Encoding.GetEncoding("UTF-8");
                StreamReader sr = new StreamReader(recStream, gb2312);

                //以字符串方式得到网页内容
                string content = sr.ReadToEnd();
                recStream.Close();
                var textSubStart = content.Substring(content.IndexOf("signalstrength") + "signalstrength".Length + 2);
                var sig = textSubStart.Substring(0, textSubStart.IndexOf('"'));
                return sig;
            }
            catch (Exception)
            {
                //Log4NetHelper.Error(typeof(Signal4gService), ex);
            }
            return string.Empty;
        }

        public string GetSig4ZlwlR20E1lt()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.1.1");
                request.Credentials = new NetworkCredential("admin", "admin");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream recStream = response.GetResponseStream();
                //编码方式
                Encoding gb2312 = Encoding.GetEncoding("UTF-8");
                StreamReader sr = new StreamReader(recStream, gb2312);
                //以字符串方式得到网页内容
                string content = sr.ReadToEnd();
                recStream.Close();
                var textSubStart = content.Substring(content.IndexOf("信号强度") + "信号强度".Length);
                //var sig = textSubStart.Substring(0, textSubStart.IndexOf('"'));
                //return sig;
            }
            catch (Exception)
            {
                //Log4NetHelper.Error(typeof(Signal4gService), ex);
            }
            return string.Empty;
        }

        public string GetHtml()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.1.1");

                request.Credentials = new NetworkCredential("admin", "admin");
                //得到返回
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //得到流
                Stream recStream = response.GetResponseStream();

                //编码方式
                Encoding gb2312 = Encoding.GetEncoding("UTF-8");

                //指定转换为gb2312编码
                StreamReader sr = new StreamReader(recStream, gb2312);

                //以字符串方式得到网页内容
                string content = sr.ReadToEnd();
                recStream.Close();

                int i = content.IndexOf("status-data-sys.jsx");
                string str = "http://192.168.10.1/" + content.Substring(i, 1000);
                HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(str);

                request1.Credentials = new NetworkCredential("admin", "admin");
                //得到返回
                HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();

                //得到流
                Stream recStream1 = response1.GetResponseStream();

                //指定转换为gb2312编码
                StreamReader sr1 = new StreamReader(recStream1, gb2312);

                //以字符串方式得到网页内容
                string content2 = sr1.ReadToEnd();
                recStream1.Close();

                //将网页内容显示在TextBox中
                //mac地址

                //int i1 = content2.IndexOf("'wan_hwaddr': '");
                //text.text1 = content2.Substring(i1 + 15, 17);

                ////连接类型

                //int i2 = content2.IndexOf("'modem_type'");
                //text.text2 = "3G-" + content2.Substring(i2 + 15, 23);

                ////IMEI

                //int i3 = content2.IndexOf("'modem_imei'");
                //if (content2.Substring(i3 + 15, 1) == "0")
                //{
                //    text.text3 = "0";
                //}
                //else
                //{
                //    text.text3 = content2.Substring(i3 + 15, 8);
                //}

                ////Modem状态

                //int i4 = content2.IndexOf("'modem_state'");
                //if (content2.Substring(i4 + 16, 1) == "1")
                //{
                //    text.text4 = "正常";
                //}
                //else
                //{
                //    text.text4 = "异常";
                //}

                ////当前网络

                //int i5 = content2.IndexOf("'cell_network'");
                //if (content2.Substring(i5 + 17, 1) == "'")
                //{
                //    text.text5 = "";
                //}
                //else
                //{
                //    text.text5 = content2.Substring(i5 + 17, 4);
                //}

                ////USIM状态

                //int i6 = content2.IndexOf("'sim_state'");
                //if (content2.Substring(i6 + 14, 1) == "1")
                //{
                //    text.text6 = "正常";
                //}
                //else
                //{
                //    text.text6 = "失败";
                //}

                ////信号强度

                //int i7 = content2.IndexOf("'csq'");
                //if (content2.Substring(i7 + 8, 1) == "")
                //{
                //    text.text7 = "0";
                //}
                //else
                //{
                //    text.text7 = content2.Substring(i7 + 8, 2);
                //}

                ////IP地址

                //int i81 = content2.IndexOf("stats.wanup = ");
                //int i8 = content2.IndexOf("'wan_ipaddr'");
                //if (content2.Substring(i8 + 15, 1) == "0")
                //{
                //    text.text8 = "0.0.0.0";
                //}
                //else
                //{
                //    text.text8 = content2.Substring(i8 + 15, 15).Replace("'", "").Replace(";", "").Replace(",", "");
                //}

                ////子网掩码

                //int i9 = content2.IndexOf("'wan_netmask'");
                //if (content2.Substring(i8 + 15, 1) == "0")
                //{
                //    text.text9 = "0.0.0.0";
                //}
                //else
                //{
                //    text.text9 = content2.Substring(i9 + 16, 15).Replace("'", "").Replace(";", "").Replace(",", "");
                //}

                ////网关
                //int i10 = content2.IndexOf("'wan_gateway_get'");
                //if (content2.Substring(i8 + 15, 1) == "0")
                //{
                //    text.text10 = "0.0.0.0";
                //}
                //else
                //{
                //    text.text10 = content2.Substring(i10 + 20, 15).Replace("'", "").Replace(";", "").Replace(",", "");
                //}

                ////DNS
                //int i11 = content2.IndexOf("dns = ");
                //if (content2.Substring(i11 + 7, 1) == "]")
                //{
                //    text.text11 = "";
                //}
                //else
                //{
                //    text.text11 = content2.Substring(i11 + 7, 40).Replace("'", "").Replace(";", "").Replace("]", "");
                //}

                ////MTU

                //int i12 = content2.IndexOf("'wan_run_mtu'");
                //text.text12 = content2.Substring(i12 + 16, 4);

                ////连接状态

                //int i13 = content2.IndexOf("stats.wanstatus =");
                //text.text13 = content2.Substring(i13 + 19, 12).Replace("'", "").Replace(";", "");

                ////已连接时间

                //int i14 = content2.IndexOf("stats.wanuptime =");
                //if (content2.Substring(i14 + 19, 1) == "-")
                //{
                //    text.text14 = "-";
                //}
                //else
                //{
                //    text.text14 = content2.Substring(i14 + 19, 8);
                //}
                //this.Invalidate();
            }
            catch (Exception)
            {
                //text.text13 = "连接失败";
                //this.Invalidate();
            }
            return string.Empty;
        }

        public string GetHtmlBySocket()
        {
            try
            {
                var url = "http://192.168.10.1/";
                HttpWebSocket httpWebSocket = new HttpWebSocket();
                var content = httpWebSocket.GetHtmlUseSocket(url);
                var textSubStart = content.Substring(content.IndexOf("signalstrength") + "signalstrength".Length + 2);
                var sig = textSubStart.Substring(0, textSubStart.IndexOf('"'));
                return sig;
            }
            catch (Exception)
            {
            }
            return string.Empty;
        }

        public void TestMqtt()
        {
            MqttClient _clientSender = null;

            // 是否为重连,需要重新订阅相关召测主题
            bool _isReConnect = true;

            var host = "ciotems_mq.mqtt.iot.bj.baidubce.com";
            var username = "ciotems_mq/collector";
            var password = "k+pwiu5Fe18E29LC49UBLPJ5ijEWqnE00HOSwPBPhYg=";

            //var host = "47.97.183.74";
            //var username = "abcdefgh";
            //var password = "abcdefgh";
            while (true)
            {
                try
                {
                    if (_clientSender == null)
                    {
                        var addr = GetIpAddr(host);
                        if (addr != null)
                        {
                            _clientSender = new MqttClient(host);
                        }
                    }
                    // 判定是否连接
                    if (_clientSender.IsConnected == false)
                    {
                        _clientSender.MqttMsgPublishReceived += new MqttClient.MqttMsgPublishEventHandler(ClientSender_MqttMsgPublishReceived);
                        string clientId = Guid.NewGuid().ToString();
                        string willMessage = "{\"Guid\":\"" + "c0011011201811290000" + "\"}";
                        _clientSender.Connect(clientId, username, password, true, MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true, "v1.0/offLine/collector", willMessage, true, 90);
                        //_clientSender.Connect(clientId);

                        if (_clientSender.IsConnected == false)
                        {
                            Thread.Sleep(5 * 1000);
                            Console.WriteLine("配置站点连接断开;重连失败");
                            continue;
                        }
                    }
                    if (_isReConnect)
                    {
                        _clientSender.Subscribe(new string[] { "v1.0/download" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                    }
                    byte[] data = System.Text.Encoding.UTF8.GetBytes("连接正常");
                    _clientSender.Publish("v1.0/upload", data, MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
                    Console.WriteLine("连接正常");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                Thread.Sleep(30 * 1000);
            }
        }

        private void ClientSender_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Console.WriteLine("消息主题：" + e.Topic);
            var msg = System.Text.Encoding.UTF8.GetString(e.Message, 0, e.Message.Length).Replace("\0", "").Replace("\n", "");
            Console.WriteLine("消息内容：" + msg);
        }

        public static IPAddress GetIpAddr(string hostName)
        {
            var addrList = Dns.GetHostEntry(hostName).AddressList;
            if (addrList != null && addrList.Length > 0)
            {
                foreach (var addr in addrList)
                {
                    if (addr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return addr;
                    }
                }
            }
            return null;
        }

        public void GetFileVersion()
        {
            var path = @"D:\临时文档\采集程序\采集程序\1.0.6906.20627\main\EnergyCollector.Main.exe";
            System.Diagnostics.FileVersionInfo info = System.Diagnostics.FileVersionInfo.GetVersionInfo(path);
            Console.WriteLine(info.FileVersion);
        }

        public void TestBcd()
        {
            var dataArray = new byte[] { 0xfb, 0xf1 };
            //for (int i = 0; i < dataArray.Length; i++)
            //{
            //    byte b1 = (byte)((dataArray[i] >> 4) & 0x0F);
            //    //低四位
            //    byte b2 = (byte)(dataArray[i] & 0x0F);

            //}
            var d1 = 0xA1;
            d1 = (byte)((byte)(d1 << 1) >> 1);
        }
    }
}