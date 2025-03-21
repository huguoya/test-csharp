// See https://aka.ms/new-console-template for more information


using System.Runtime.Versioning;

using Microsoft.Win32;


[SupportedOSPlatform("windows6.1")]
class Program
{
    [MTAThread]
    private static void Main(string[] args)
    {
        System.Console.WriteLine("hello world");
        System.Console.ReadLine();
    }
}
