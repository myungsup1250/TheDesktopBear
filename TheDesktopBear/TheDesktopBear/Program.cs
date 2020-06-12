using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheDesktopBear
{
    static class Program
    {
        public static Thread t;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            t = new Thread(new ThreadStart(FindFriends.WaitPing));
            t.Start();
            Application.Run(new Bear());

            t.Abort();

            Thread t = new Thread(new ThreadStart(FindFriends.WaitPing));
            t.Start();

            Thread receive = new Thread(new ThreadStart(Bear.receive));
            receive.Start();
            
            Application.Run(new Bear());
        }
    }
}
