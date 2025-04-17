using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MQTTnet;
using MQTTnet.Formatter;


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
        public double GetMathResult(string expression)
        {
            return Convert.ToDouble(new DataTable().Compute(expression, ""));
        }

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
            ////用户code码
            //var entCode = "085458";
            ////用户密码
            //var secret = "2E1B811F701F3C7CEFD031689FE82924";
            ////物联卡基本信息（可用）
            //var API_IotCardInfoApi = "http://wl.51liubei.com/dhApi/iotCardInfoApi.api";

            //var iccid = "8986031746202530209Y";
            //RestSharp.Http http = new RestSharp.Http();
            //StringBuilder url = new StringBuilder(API_IotCardInfoApi);
            //url.Append("entCode=").Append(entCode).Append("&secret=").Append(secret)
            //         .Append("&iccid=").Append(iccid).Append("&access_token=");
            ////.Append(MD5.getAccessToken(Constants.entCode, Constants.secret));
            ////http.Url = new Uri();
            //http.Get();
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
            //JsonReader reader = new JsonTextReader(new StringReader(jsonText));
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
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue(
                            "Basic",
                            Convert.ToBase64String(Encoding.ASCII.GetBytes("admin:admin"))
                        );

                    HttpResponseMessage response = client.GetAsync("http://192.168.10.1").Result;
                    response.EnsureSuccessStatusCode();

                    string content = response.Content.ReadAsStringAsync().Result;
                    var textSubStart = content.Substring(content.IndexOf("signalstrength") + "signalstrength".Length + 2);
                    var sig = textSubStart.Substring(0, textSubStart.IndexOf('"'));
                    return sig;
                }
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
            }
            return string.Empty;
        }

        public string GetSig4ZlwlR20E1lt()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue(
                            "Basic",
                            Convert.ToBase64String(Encoding.ASCII.GetBytes("admin:admin"))
                        );

                    HttpResponseMessage response = client.GetAsync("http://192.168.1.1").Result;
                    response.EnsureSuccessStatusCode();

                    string content = response.Content.ReadAsStringAsync().Result;
                    var textSubStart = content.Substring(content.IndexOf("信号强度") + "信号强度".Length);
                    // Process the signal strength as needed
                }
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
            }
            return string.Empty;
        }

        public string GetHtml()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue(
                            "Basic",
                            Convert.ToBase64String(Encoding.ASCII.GetBytes("admin:admin"))
                        );

                    HttpResponseMessage response = client.GetAsync("http://192.168.1.1").Result;
                    response.EnsureSuccessStatusCode();

                    string content = response.Content.ReadAsStringAsync().Result;

                    int i = content.IndexOf("status-data-sys.jsx");
                    string str = "http://192.168.10.1/" + content.Substring(i, 1000);

                    HttpResponseMessage response1 = client.GetAsync(str).Result;
                    response1.EnsureSuccessStatusCode();

                    string content2 = response1.Content.ReadAsStringAsync().Result;

                    // Process content2 as needed
                }
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
            }
            return string.Empty;
        }

        public async void TestMqtt()
        {
            var _clientSender = new MqttClientFactory().CreateMqttClient();

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

                    // 判定是否连接
                    if (_clientSender.IsConnected == false)
                    {
                        var op = new MqttClientOptionsBuilder()
                              .WithTcpServer(host)
                              .WithCredentials(username, password)
                              .WithClientId(Guid.NewGuid().ToString())
                              .WithProtocolVersion(MqttProtocolVersion.V311)
                              .WithTimeout(new TimeSpan(0, 0, 90))
                              .Build();
                        _clientSender.ApplicationMessageReceivedAsync += ClientSender_MqttMsgPublishReceived;
                        await _clientSender.ConnectAsync(op);

                        if (_clientSender.IsConnected == false)
                        {
                            Thread.Sleep(5 * 1000);
                            Console.WriteLine("配置站点连接断开;重连失败");
                            continue;
                        }
                    }
                    if (_isReConnect)
                    {
                        await _clientSender.SubscribeAsync("v1.0/download");
                    }
                    byte[] data = System.Text.Encoding.UTF8.GetBytes("连接正常");
                    await _clientSender.PublishAsync(new MqttApplicationMessageBuilder().WithTopic("v1.0/upload").WithPayload(data).Build());
                    Console.WriteLine("连接正常");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                Thread.Sleep(30 * 1000);
            }
        }

        private Task ClientSender_MqttMsgPublishReceived(MqttApplicationMessageReceivedEventArgs e)
        {
            Console.WriteLine("消息主题：" + e.ApplicationMessage.Topic);
            var msg = System.Text.Encoding.UTF8.GetString(e.ApplicationMessage.Payload).Replace("\0", "").Replace("\n", "");
            Console.WriteLine("消息内容：" + msg);
            return Task.CompletedTask;
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