using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace sorprendente
{
    public partial class Form6 : Form
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

        ListViewItem item0;
        ListViewItem item1;
        ListViewItem item2;
        ListViewItem item3;
        ListViewItem item4;

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
        }

        #endregion

        #region Functions

        void AddItems()
        {
            item0 = new ListViewItem();
            item0.Text = "";
            item0.SubItems.Add("");
            item0.SubItems.Add("");
            item0.SubItems.Add("");

            item1 = new ListViewItem();
            item1.Text = "";
            item1.SubItems.Add("");
            item1.SubItems.Add("");
            item1.SubItems.Add("");

            item2 = new ListViewItem();
            item2.Text = "";
            item2.SubItems.Add("");
            item2.SubItems.Add("");
            item2.SubItems.Add("");

            item3 = new ListViewItem();
            item3.Text = "";
            item3.SubItems.Add("");
            item3.SubItems.Add("");
            item3.SubItems.Add("");

            item4 = new ListViewItem();
            item4.Text = "";
            item4.SubItems.Add("");
            item4.SubItems.Add("");
            item4.SubItems.Add("");

            listView1.Items.AddRange(new ListViewItem[] { item0, item1, item2, item3, item4 });
        }

        void RDebug_OnOutputDebugString(int pid, string text)
        {
            AppendText(pid, text);
        }

        void AppendText(int pid, string text)
        {
            listView1.BeginUpdate();

            try
            {
                if (text.EndsWith(Environment.NewLine))
                    text = text.Substring(0, text.Length - Environment.NewLine.Length);

                text = text.Replace("\r\n", "\n");

                foreach (string line in text.Split('\n'))
                {
                    if (line.IndexOf("=", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        string[] tempString = line.Split('=');

                        switch (tempString[0])
                        {
                            case "map":
                                listView1.Items[0].SubItems[1].Text = tempString[0];
                                listView1.Items[0].SubItems[2].Text = tempString[1];
                                return;

                            case "charX":
                                listView1.Items[1].SubItems[1].Text = tempString[0];
                                listView1.Items[1].SubItems[2].Text = tempString[1];
                                return;

                            case "charY":
                                listView1.Items[2].SubItems[1].Text = tempString[0];
                                listView1.Items[2].SubItems[2].Text = tempString[1];
                                return;

                            case "hp":
                                listView1.Items[3].SubItems[1].Text = tempString[0];
                                listView1.Items[3].SubItems[2].Text = tempString[1];
                                return;

                            case "mp":
                                listView1.Items[4].SubItems[1].Text = tempString[0];
                                listView1.Items[4].SubItems[2].Text = tempString[1];
                                return;
                        }
                    }
                }
            }
            finally
            {
                listView1.EndUpdate();
                listView1.Refresh();
                Application.DoEvents();
            }
        }

        string GetProcessName(int pid)
        {
            if (pid == -1)
                return Process.GetCurrentProcess().ProcessName;
            try
            {
                return Process.GetProcessById(pid).ProcessName;
            }
            catch
            {
                return "<exited>";
            }
        }

        #endregion

        #region Form functions

        #region Form6 function

        public Form6()
        {
            InitializeComponent();

            AddItems();

            ListViewHelper.EnableDoubleBuffer(listView1);

            RDebug.OnOutputDebugString += new OnOutputDebugStringHandler(RDebug_OnOutputDebugString);
            RDebug.Start();
        }

        #endregion

        #region listView1 ColumnWidthChanging function

        private void listView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
        }

        #endregion

        #endregion
    }
}
