using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HybridShutdown
{
    static class Win32API
    {
        public enum ExitWindows : uint
        {
            EWX_HYBRID_SHUTDOWN = 0x00400000,
            EWX_LOGOFF = 0x00,
            EWX_SHUTDOWN = 0x01,
            EWX_REBOOT = 0x02,
            EWX_POWEROFF = 0x08,
            EWX_RESTARTAPPS = 0x40,

            EWX_FORCE = 0x04,
            EWX_FORCEIFHUNG = 0x10,
        }


        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ExitWindowsEx(ExitWindows uFlags, int dwReason);

        [DllImport("kernel32.dll")]
        public static extern uint FormatMessage(
          uint dwFlags, IntPtr lpSource,
          uint dwMessageId, uint dwLanguageId,
          StringBuilder lpBuffer, int nSize,
          IntPtr Arguments);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool OpenProcessToken(IntPtr ProcessHandle,
            uint DesiredAccess,
            out IntPtr TokenHandle);

        [DllImport("advapi32.dll", SetLastError = true,
            CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool LookupPrivilegeValue(string lpSystemName,
            string lpName,
            out long lpLuid);

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TOKEN_PRIVILEGES
        {
            public int PrivilegeCount;
            public long Luid;
            public int Attributes;
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool AdjustTokenPrivileges(IntPtr TokenHandle,
            bool DisableAllPrivileges,
            ref TOKEN_PRIVILEGES NewState,
            int BufferLength,
            IntPtr PreviousState,
            IntPtr ReturnLength);
    }
}
