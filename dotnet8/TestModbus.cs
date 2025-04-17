using System;

namespace TestApp.Common
{
    public class TestModbus
    {
        //public void TestEasyModbus()
        //{

        //    EasyModbus.ModbusClient mc = new EasyModbus.ModbusClient()
        //    {
        //        IPAddress = "127.0.0.1",
        //        Port = 502,
        //        ConnectionTimeout = 5 * 1000,
        //    };
        //    mc.Connect();
        //    if (mc.Connected)
        //    {
        //        var data = mc.ReadHoldingRegisters(0, 10);
        //        Console.WriteLine(string.Join(" ", data));
        //    }
        //}


        public static void TestFlunetModbusTcp()
        {
            var mc = new FluentModbus.ModbusTcpClient();
            mc.Connect("127.0.0.1:502");
            if (mc.IsConnected)
            {
                var data = mc.ReadHoldingRegisters(1, 1, 10);
                Console.WriteLine();
            }
        }
        public static void TestFlunetModbusRtu()
        {
            var mc = new FluentModbus.ModbusRtuClient()
            {
                BaudRate = 9600,
                Parity = System.IO.Ports.Parity.Even,
                StopBits = System.IO.Ports.StopBits.One,
            };
            mc.Connect("COM1");
            if (mc.IsConnected)
            {
                var data = mc.ReadHoldingRegisters(1, 1, 10);
                Console.WriteLine();
            }
        }
    }
}
