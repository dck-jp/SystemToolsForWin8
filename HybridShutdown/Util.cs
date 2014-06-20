using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HybridShutdown
{
    static class Util
    {
        public static void WriteDebugFile(int errorNumber)
        {
            if (errorNumber == 0) return;

            const uint FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;
            StringBuilder message = new StringBuilder(255);
            Win32API.FormatMessage(
                                    FORMAT_MESSAGE_FROM_SYSTEM,
                                    IntPtr.Zero,
                                    (uint)errorNumber,
                                    0,
                                    message,
                                    message.Capacity,
                                    IntPtr.Zero);
            var path = Environment.SpecialFolder.Desktop + @"\debug.log";
            File.AppendAllLines(path, new[] { message.ToString() });
        }

        public static void AdjustToken()
        {
            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                return;

            const uint TOKEN_ADJUST_PRIVILEGES = 0x20;
            const uint TOKEN_QUERY = 0x8;
            const int SE_PRIVILEGE_ENABLED = 0x2;
            const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

            //トークンを取得する
            IntPtr tokenHandle;
            var procHandle = Win32API.GetCurrentProcess();
            Win32API.OpenProcessToken(procHandle, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out tokenHandle);
            //LUIDを取得する
            var tp = new Win32API.TOKEN_PRIVILEGES();
            tp.Attributes = SE_PRIVILEGE_ENABLED;
            tp.PrivilegeCount = 1;
            Win32API.LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, out tp.Luid);
            //特権を有効にする
            Win32API.AdjustTokenPrivileges(tokenHandle, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
        }
        
    }
}
