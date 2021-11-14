using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AdventOfCode2021
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            AttachConsole(-1);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        
        [DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int dwProcessId);
    }
}