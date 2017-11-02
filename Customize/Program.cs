using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Customize
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ロック画面を表示しないようにします。");
            Console.WriteLine("「サインイン時または画面上のすべてのアプリを終了したときに、スタート画面ではなくデスクトップに移動する」をONにします。");
            Console.WriteLine("Press Any Key..");
            Console.Read();

            using (var reg = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Personalization"))
            {
                reg.SetValue("NoLockScreen", 1);                
            }

            using (var reg = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\StartPage"))
            {
                reg.SetValue("OpenAtLogon", 0);

            }

            Console.Read();
        }
    }
}
