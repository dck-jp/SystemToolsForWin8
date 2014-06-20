using HybridShutdown;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Shutdown
{
    class Program
    {
        static void Main(string[] args)
        {
            Util.AdjustToken();
            Util.WriteDebugFile(Marshal.GetLastWin32Error());

            Win32API.ExitWindowsEx(Win32API.ExitWindows.EWX_POWEROFF, 0);
            Util.WriteDebugFile(Marshal.GetLastWin32Error());
        }
    }
}
