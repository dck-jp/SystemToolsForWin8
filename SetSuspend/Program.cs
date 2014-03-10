using System.Windows.Forms;

namespace Suspend
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.SetSuspendState(PowerState.Suspend, false, false);
        }
    }
}
