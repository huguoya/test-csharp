using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

using lib60870.CS101;
using lib60870.CS104;


namespace dotnet8;

internal class Test104
{

    static Dictionary<int, float> dv = new Dictionary<int, float>();
    public static void TestMain()
    {

        Connection con = new Connection("127.0.0.1", 2404);

        con.SetASDUReceivedHandler(asduReceivedHandler, null);
        while (true)
        {
            try
            {
                if (!con.IsRunning)
                {
                    Console.WriteLine("尝试重连...");
                    con.Connect();
                    con.SendInterrogationCommand(CauseOfTransmission.ACTIVATION, 1, QualifierOfInterrogation.STATION);
                }
                //ASDU asdu = new ASDU(con.GetApplicationLayerParameters(), CauseOfTransmission.ACTIVATION, false, false, 0, 1, false);
                //asdu.AddInformationObject(new SetpointCommandShort(16400, (float)(new Random().NextDouble() * 100), new SetpointCommandQualifier(true, 0)));
                //con.SendASDU(asdu);

                //foreach (var item in dv)
                //{
                //    Console.WriteLine($"{DateTime.Now} ; {item.Key}:{item.Value}");
                //}
                //Console.WriteLine("-------------------");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {

                Thread.Sleep(10_000);
            }
        }
    }

    private static void ConnectionHandler(object parameter, ConnectionEvent connectionEvent)
    {
        switch (connectionEvent)
        {
            case ConnectionEvent.OPENED:
                Console.WriteLine("Connected");
                break;
            case ConnectionEvent.CLOSED:
                Console.WriteLine("Connection closed");
                break;
            case ConnectionEvent.STARTDT_CON_RECEIVED:
                Console.WriteLine("STARTDT CON received");
                break;
            case ConnectionEvent.STOPDT_CON_RECEIVED:
                Console.WriteLine("STOPDT CON received");
                break;
        }
    }

    private static bool asduReceivedHandler(object parameter, ASDU asdu)
    {
        //Console.WriteLine(asdu.ToString());

        switch (asdu.TypeId)
        {
            case TypeID.M_ME_NC_1:
                {
                    //13 短测量值（FLOAT32） 监视 遥测（带品质描述的浮点值，每个遥测值占5个字节）
                    for (int i = 0; i < asdu.NumberOfElements; i++)
                    {
                        var mfv = (MeasuredValueShort)asdu.GetElement(i);

                        //Console.WriteLine("  IOA: " + mfv.ObjectAddress + " float value: " + mfv.Value);
                        //Console.WriteLine("   " + mfv.Quality.ToString());
                        dv[mfv.ObjectAddress] = mfv.Value;
                    }

                    break;
                }

            case TypeID.C_SE_NC_1:
                {
                    //50 设定值命令，短值（FLOAT32） 控制 遥调
                    for (int i = 0; i < asdu.NumberOfElements; i++)
                    {
                        var mfv = (SetpointCommandShort)asdu.GetElement(i);
                        dv[mfv.ObjectAddress] = mfv.Value;

                        //Console.WriteLine("  IOA: " + mfv.ObjectAddress + " float value: " + mfv.Value);
                    }

                    break;
                }

            case TypeID.C_IC_NA_1:
                //C_IC_NA_1(100) 询问命令 控制
                //​功能：总召唤命令
                //​触发：主站请求从站上传全站数据（遥测、遥信等）。
                //if (asdu.Cot == CauseOfTransmission.ACTIVATION_CON)
                //{
                //    //是否阴性
                //    Console.WriteLine((asdu.IsNegative ? "Negative" : "Positive") + "confirmation for interrogation command");
                //}
                //else if (asdu.Cot == CauseOfTransmission.ACTIVATION_TERMINATION)
                //{
                //    Console.WriteLine("Interrogation command terminated");
                //}
                break;

            default:
                Console.WriteLine("Unknown message type!");
                break;
        }
        return true;
    }
}
