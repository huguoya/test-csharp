using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestApp.Common
{
    public class TxtStream
    {
        public void Test_SaveBytes()
        {
            List<byte> data = new List<byte>();
            data.Add(0x68);
            data.Add(0x00);
            data.Add(0x16);
            SaveBytes(data.ToArray(), "test");
            Console.WriteLine("finish");
        }

        private void SaveBytes(byte[] data, string type)
        {
            string dirPath = @"D:\Message\";
            string filePath = @"D:\Message\" + DateTime.Now.ToString("yyyy-MM-dd HH：mm：ss：ffff") + "_" + type + ".txt";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                for (int i = 0; i < data.Length; i++)
                {
                    //二进制
                    string bin = Convert.ToString(data[i], 2);
                    //补齐二进制
                    string binFull = bin.PadLeft(8, '0');
                    sw.Write(binFull);
                    sw.Write("    ");
                    sw.Write(string.Format("{0:x}", Convert.ToInt32(binFull, 2)));
                    sw.WriteLine();
                }
            }
        }

        public void Test_BCD2H()
        {
            string filePath = @"D:\Working\Message\2015-01-13 11：43：09：2438_receive.txt";
            BCD2H(filePath);
            Console.ReadLine();
        }

        public void BCD2H(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.Write(line);
                    for (int i = 0; i < 16 - line.Length; i++)
                    {
                        Console.Write(" ");
                    }
                    for (int i = 0; i < line.Length / 4; i++)
                    {
                        Console.Write(string.Format("{0:x}", Convert.ToInt32(line.Substring(i * 4, 4), 2)));
                    }
                    Console.WriteLine();
                }
            }
        }

        public void ShortBytes()
        {
            ushort s = 1000;
            while (true)
            {
                s += 100;
                Console.Write(s);
                Console.Write("\t");

                byte[] bs = BitConverter.GetBytes(s);
                for (int i = bs.Length - 1; i >= 0; i--)
                {
                    string binFull = (Convert.ToString(bs[i], 2)).PadLeft(8, '0');
                    Console.Write(binFull);
                }
                Console.WriteLine();
                Thread.Sleep(100);
            }
        }

        public void Test_GetCRC()
        {
            string filePath = @"D:\Message\2015-01-13 16：10：27：6970_send.txt";
            GetCRC(filePath);

            Console.ReadLine();
        }

        public void GetCRC(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line = string.Empty;
                List<byte> byteList = new List<byte>();
                short s = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    byteList.Add(Convert.ToByte(line.Substring(0, 8), 2));
                }
                byte[] bs = byteList.ToArray();
                for (int i = 1; i < bs.Length - 3; i++)
                {
                    s += bs[i];
                }
                byte[] bsCRC = BitConverter.GetBytes(s);
                for (int j = bsCRC.Length - 1; j >= 0; j--)
                {
                    string binFull = (Convert.ToString(bsCRC[j], 2)).PadLeft(8, '0');
                    Console.Write(binFull);
                    Console.Write("\t");
                    Console.Write(string.Format("{0:x}", Convert.ToInt32(binFull, 2)));
                    Console.WriteLine();
                }
            }
        }
    }
}