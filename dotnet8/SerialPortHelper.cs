using System;
using System.IO.Ports;



namespace EnergyCollector.AllThreading.helper
{
    /// <summary>
    /// 自定义串口帮助类
    /// </summary>
    public class SerialPortHelper
    {
        /// <summary>
        /// 串口资源
        /// </summary>
        public SerialPort serialPort;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="meterCommParamsTmp"></param>
        public SerialPortHelper(string port, string baudRate, string dataBit, string stopBit, string parityBit)
        {
            //根据表计通信参数的配置创建一个SerialPort对象,并进行初始化
            serialPort = new SerialPort
            {
                //端口号
                PortName = port,
                //波特率
                BaudRate = int.Parse(baudRate),
                //数据位
                DataBits = int.Parse(dataBit)
            };

            //停止位
            switch (stopBit)
            {
                case "NONE":
                    //serialPort.StopBits = null;
                    break;

                case "TWO":
                    serialPort.StopBits = StopBits.Two;
                    break;

                case "ONE":
                default:
                    serialPort.StopBits = StopBits.One;
                    break;
            }

            //校验位
            switch (parityBit)
            {
                case "EVEN":
                    serialPort.Parity = Parity.Even;
                    break;

                case "ODD":
                    serialPort.Parity = Parity.Odd;
                    break;

                case "NONE":
                default:
                    serialPort.Parity = Parity.None;
                    break;
            }

            //启用RTS（重要!不启用时可能数据发不出去）
            serialPort.RtsEnable = true;
            serialPort.WriteTimeout = 5000;
            serialPort.ReadTimeout = 5000;
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        public bool OpenPort()
        {
            try
            {
                if (serialPort != null)
                {
                    if (serialPort.IsOpen)
                    {
                        ClosePort();
                    }
                    serialPort.Open();
                    //清空buffer
                    serialPort.DiscardInBuffer();
                    serialPort.DiscardOutBuffer();
                    return serialPort.IsOpen;
                }
            }
            catch (Exception ex)
            {
                //Log4NetHelper.Error(typeof(SerialPortHelper), ex, "串口打开异常:" + serialPort.PortName);
            }
            return false;
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void ClosePort()
        {
            try
            {
                if (serialPort != null && serialPort.IsOpen)
                {
                    serialPort.Close();
                }
            }
            catch (Exception ex)
            {
                //Log4NetHelper.Error(typeof(SerialPortHelper), ex, "串口关闭异常:" + serialPort.PortName);
            }
        }
    }
}