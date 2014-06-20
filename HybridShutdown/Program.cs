using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace HybridShutdown
{
    class Program
    {
        static void Main(string[] args)
        {
            Util.AdjustToken();
            Util.WriteDebugFile(Marshal.GetLastWin32Error());

            Win32API.ExitWindowsEx(Win32API.ExitWindows.EWX_HYBRID_SHUTDOWN | Win32API.ExitWindows.EWX_SHUTDOWN, 0);
            Util.WriteDebugFile(Marshal.GetLastWin32Error());
        }
    }
}
