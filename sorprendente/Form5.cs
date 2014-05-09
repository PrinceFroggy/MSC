using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sorprendente
{
    public partial class Form5 : Form
    {
        #region Variables

        const int WM_NCHITTEST = 0x0084;
        const int HTBOTTOM = 15;
        const int HTBOTTOMLEFT = 16;
        const int HTBOTTOMRIGHT = 17;
        const int HTLEFT = 10;
        const int HTRIGHT = 11;
        const int HTTOPLEFT = 13;
        const int HTTOPRIGHT = 14;
        const int HTTOP = 12;
        const int HTCLIENT = 1;

        #endregion

        #region Event Hanlder

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST)
            {
                switch ((int)m.Result)
                {
                    case HTLEFT:
                    case HTTOP:
                    case HTTOPLEFT:
                    case HTBOTTOM:
                    case HTRIGHT:
                    case HTTOPRIGHT:
                    case HTBOTTOMLEFT:
                        m.Result = (IntPtr)HTCLIENT;
                        break;
                }
            }
        }

        #endregion

        #region Form functions

        #region Form5 function

        public Form5()
        {
            InitializeComponent();
        }

        #endregion

        #endregion
    }
}
