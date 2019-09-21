using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Common
{
    public class TestModbus
    {
        public void TestEasyModbus()
        {

            EasyModbus.ModbusClient mc = new EasyModbus.ModbusClient()
            {
                IPAddress = "127.0.0.1",
                Port = 502,
                ConnectionTimeout = 5 * 1000,
            };
            mc.Connect();
            if (mc.Connected)
            {
                var data = mc.ReadHoldingRegisters(0, 10);
                Console.WriteLine(string.Join(" ", data));
            }
        }
    }
}
