using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace sorprendente
{
    public partial class Form4 : Form
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

        private Form1 _form1 = null;

        Map rdic = new Map();

        long? key = null;
        int tempKey;

        bool ssb = false;

        #endregion

        #region Event Hanlder

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST)
            {
                switch ((int)m.Result)
                {
                    case HTBOTTOM:
                    case HTLEFT:
                    case HTRIGHT:
                    case HTTOP:
                    case HTTOPLEFT:
                    case HTTOPRIGHT:
                    case HTBOTTOMLEFT:
                    case HTBOTTOMRIGHT:
                        m.Result = (IntPtr)HTCLIENT;
                        break;
                }
            }
        }

        #endregion

        #region Functions

        void SearchMD()
        {
            if (int.TryParse(cueTextBox1.Text, out tempKey) && rdic.Keys.Contains(tempKey))
            {
                key = tempKey;
            }
            else if (rdic.Values.Contains(cueTextBox1.Text))
            {
                key = rdic.First(kvp => kvp.Value == cueTextBox1.Text).Key;
            }
            else
            {
                key = null;
            }
        }

        void WriteRG()
        {
            WRegistry.SetReg();

            WRegistry.SetText(key.ToString());

            WRegistry.CloseReg();
        }
        
        public void FUpdate(int i)
        {
            switch (i)
            {
                case 2:
                    CAC("Arrived at destination!");
                    break;

                case 3:
                    CAC("Starting destination is desired destination!");
                    break;

                case 4:
                    CAC("Invalid starting or ending destination!");
                    break;

                case 5:
                    CAC("Cannot determine path!");
                    break;
            }
        }

        void CAC(string s)
        {
            if (this.InvokeRequired)
            {
                Invoke(new MethodInvoker(() =>
                {
                    PInteraction.sendRusher(false);

                    toolTip1.SetToolTip(cueTextBox1, s);
                    toolTip1.Show(s, cueTextBox1, 3000);

                    _form1.ChangeSetting(true);

                    cueTextBox1.Enabled = true;
                    cueTextBox1.Text = null;

                    groupBox1.Focus();

                    button1.Text = "→";
                    ssb = false;
                }));
            }
            else
            {
                PInteraction.sendRusher(false);

                toolTip1.SetToolTip(cueTextBox1, s);
                toolTip1.Show(s, cueTextBox1, 3000);

                _form1.ChangeSetting(true);

                cueTextBox1.Enabled = true;
                cueTextBox1.Text = null;

                groupBox1.Focus();

                button1.Text = "→";
                ssb = false; 
            }
        }

        public void ToggleButton(bool b)
        {
            if (b)
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
        }

        #endregion 

        #region Form functions

        #region Form4 functions

        public Form4(Form1 form1)
        {
            InitializeComponent();
            PInteraction.sendForm4(this);

            _form1 = form1;

            var autoCompleteSuggestions = new AutoCompleteStringCollection();

            autoCompleteSuggestions.AddRange(rdic.Keys.Select(k => k.ToString()).Union(rdic.Values).ToArray());
            cueTextBox1.AutoCompleteCustomSource = autoCompleteSuggestions;
        }

        private void Form4_Click(object sender, EventArgs e)
        {
            groupBox1.Focus();
        }

        #endregion

        #region Button function

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Focus();

            if (!ssb)
            {
                if (cueTextBox1.Text.Trim().Length != 0)
                {
                    SearchMD();

                    WriteRG();

                    PInteraction.sendRusher(true);

                    _form1.ChangeSetting(false);

                    cueTextBox1.Enabled = false;

                    button1.Text = "x";

                    ssb = true;
                }
                else
                {
                    toolTip1.SetToolTip(cueTextBox1, "No destination selected!");
                    toolTip1.Show("No destination selected!", cueTextBox1, 3000);

                    cueTextBox1.Text = null;

                    groupBox1.Focus();
                }
            }
            else
            {
                CAC("Cancelled rush!");
            }
        }

        #endregion

        #region cueTextBox MouseHover

        private void cueTextBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip
                (cueTextBox1,
                @"3rd 
CTF_GL 
newNLC_GL 
Episode1GL
Gacha_GL 
HalloweenGL 
MasteriaGL 
SG 
etc 
global 
event 
event_5th 
event_6th 
jp 
maple 
ossyria 
singapore 
thai 
grandis 
aquaroad 
masteria 
boost 
victoria 
weddingGL 
Pink ZakumGL 
Back to the old maple 
chinese 
HalloweenGL2012 
presElect 
Snakwell 
taiwan 
dawnveil");
            toolTip1.Show(@"3rd 
CTF_GL 
newNLC_GL 
Episode1GL
Gacha_GL 
HalloweenGL 
MasteriaGL 
SG 
etc 
global 
event 
event_5th 
event_6th 
jp 
maple 
ossyria 
singapore 
thai 
grandis 
aquaroad 
masteria 
boost 
victoria 
weddingGL 
Pink ZakumGL 
Back to the old maple 
chinese 
HalloweenGL2012 
presElect 
Snakwell 
taiwan 
dawnveil", cueTextBox1);
        }

        #endregion

        #endregion
    }
}
