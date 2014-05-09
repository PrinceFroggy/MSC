using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace sorprendente
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Game.Launch())
            {
                PInteraction.ThreadPipe();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {
                DialogResult errorBox = MessageBox.Show("Error arised at: " + Game.ERRORname, "error", MessageBoxButtons.OK);

                if (errorBox == DialogResult.OK)
                    return;
            }
        }
    }
}
