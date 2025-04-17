using System;
using System.Runtime.Versioning;
using System.Windows.Forms;

using WinFormsApp1;

namespace TestApp
{
    internal class Program
    {
        [STAThread]
        [SupportedOSPlatform("windows6.1")]
        private static void Main()
        {
            Application.Run(new Form1());
        }
    }
}