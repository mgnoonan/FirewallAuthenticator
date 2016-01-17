using System;
using System.Windows.Forms;
using WebBrowserControlDialogs;

namespace FirewallAuthenticator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Tell the WidowsInterop to Hook messages
            WindowsInterop.Hook();

            Application.Run(new frmMain());

            // Tell the WidowsInterop to Unhook
            WindowsInterop.Unhook();
        }
    }
}
