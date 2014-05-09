using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Management;

namespace sorprendente
{
    public partial class Form1 : Form
    {
        #region Variables

        Form2 form2;
        Form3 form3;
        Form4 form4;
        Form5 form5;
        Form6 form6;
        Form7 form7;

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

        bool bb = false;

        #endregion

        #region Event handler

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
            else if (m.Msg == 0x312)
            {
                int hotKeyId = m.WParam.ToInt32();

                switch (hotKeyId)
                {
                    case 1:
                        HotkeyF1();
                        break;

                    case 2:
                        HotkeyF2();
                        break;

                    case 3:
                        HotkeyF3();
                        break;

                    case 4:
                        HotkeyF4();
                        break;

                    case 5:
                        HotkeyF5();
                        break;

                    case 6:
                        HotkeyF6();
                        break;

                    case 7:
                        HotKeyF7();
                        break;

                    case 8:
                        HotKeyF8();
                        break;
                }
            }
        }

        #endregion

        #region Functions

        #region Visible function hotkeys

        void HotkeyF1()
        {
            label1.Focus();

            if (this.Visible)
            {
                this.Hide();
            }
            else
            {
                this.Show();
            }
        }

        void HotkeyF2()
        {
            label1.Focus();

            if (!form2.Visible)
            {
                form2.Show();
            }
            else
            {
                form2.Hide();
            }
        }

        void HotkeyF3()
        {
            checkBox1.Checked = !checkBox1.Checked;
        }

        void HotkeyF4()
        {
            label1.Focus();

            if (!form3.Visible)
            {
                form3.ClientSize = new System.Drawing.Size(154, 301);
                form3.Show();
            }
            else
            {
                form3.Hide();
            }
        }

        #endregion

        #region Invisible function hotkeys

        void HotkeyF5()
        {
            label1.Focus();

            if (!form7.Visible)
            {
                form7.ClientSize = new System.Drawing.Size(154, 189);
                form7.Show();
            }
            else
            {
                form7.Hide();
            }
        }

        void HotkeyF6()
        {
            label1.Focus();

            if (!form4.Visible)
            {
                form4.ClientSize = new System.Drawing.Size(444, 81);
                form4.Show();
            }
            else
            {
                form4.Hide();
            }
        }

        void HotKeyF7()
        {
            label1.Focus();

            if (!form5.Visible)
            {
                form5.ClientSize = new System.Drawing.Size(324, 301);
                form5.Show();
            }
            else
            {
                form5.Hide();
            }
        }

        void HotKeyF8()
        {
            label1.Focus();

            if (!form6.Visible)
            {
                form6.ClientSize = new System.Drawing.Size(214, 113);
                form6.Show();
            }
            else
            {
                form6.Hide();
            }
        }

        #endregion

        #region Change function

        public void ChangeSetting(bool b)
        {
            if (this.InvokeRequired)
            {
                Invoke(new MethodInvoker(() =>
                {
                    checkBox1.Enabled = b;
                }));
            }
            else
            {
                checkBox1.Enabled = b;
            }
        }

        #endregion

        #endregion

        #region Form functions

        #region Form1 functions

        public Form1()
        {
            InitializeComponent();

            this.ClientSize = new System.Drawing.Size(99, 138);

            form2 = new Form2();
            form3 = new Form3();
            form4 = new Form4(this);
            form5 = new Form5();
            form6 = new Form6();
            form7 = new Form7();

            globalHotkey.HotKeys(true, this);
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            label1.Focus();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            globalHotkey.HotKeys(false, this);
        }

        #endregion

        #region Checkbox CheckedChanged function

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            label1.Focus();

            if (!bb)
            {
                this.ClientSize = new System.Drawing.Size(99, 138);

                label1.Focus();

                form4.ToggleButton(true);

                bb = true;
            }
            else
            {
                form4.ToggleButton(false);

                bb = false;
            }
        }

        #endregion

        #region Button Click functions

        private void button1_Click(object sender, EventArgs e)
        {
            HotkeyF2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HotkeyF4();
        }

        #endregion

        #endregion
    }
}
