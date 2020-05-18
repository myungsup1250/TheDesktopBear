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
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Thread t = new Thread(new ThreadStart(FindFriends.WaitPing));
            t.Start();

            Thread receive = new Thread(new ThreadStart(Bear.receive));
            receive.Start();
            Application.Run(new Bear());
        }
    }
}
