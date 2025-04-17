// See https://aka.ms/new-console-template for more information


using System;
using System.Runtime.Versioning;

using TestApp.Common;
using TestApp.FTP;

namespace dotnet8;



[SupportedOSPlatform("windows6.1")]
class Program
{

    [MTAThread]
    private static void Main()
    {
        //Test104.TestMain();
        //FtpTest.Upload();
        TestModbus.TestFlunetModbusTcp();
        System.Console.ReadLine();
    }
}
