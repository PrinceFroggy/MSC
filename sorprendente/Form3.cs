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
    public partial class Form3 : Form
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

        string[] Line = new string[3];
        string[] PrevLine = new string[3];
        object[] TempKey = new object[5];
        object[] PrevTempKey = new object[5];

        #endregion

        #region Event Handler

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST)
            {
                switch ((int)m.Result)
                {
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

        #region Function

        bool Availability(object Selection)
        {
            for (int i = 0; i < 5; i++)
            {
                if (Selection == TempKey[i])
                    return false;
            }
            return true;
        }

        void CreateData()
        {
            WRegistry.SetReg();

            if (Line != null)
            {
                for (int i = 0; i < 3; ++i)
                {
                    if (Line[i] != null && Line[i] != PrevLine[i])
                    {
                        PrevLine[i] = Line[i];
                        WRegistry.SetLine(Line[i], i);
                    }
                }
            }

            if (TempKey != null)
            {
                for (int i = 0; i < 5; ++i)
                {
                    if (TempKey[i] != null && TempKey[i] != PrevTempKey[i])
                    {
                        PrevTempKey[i] = TempKey[i];
                        WRegistry.SetKey(TempKey[i], i);
                    }
                }
            }

            WRegistry.CloseReg();
        }

        #endregion

        #region Form functions

        #region Form3 functions

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Click(object sender, EventArgs e)
        {
            groupBox1.Focus();
        }

        #endregion

        #region cueComboBox Draw functions

        private void cueComboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cbx = sender as ComboBox;
            if (cbx != null)
            {
                e.DrawBackground();

                if (e.Index >= 0)
                {
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;

                    Brush brush = new SolidBrush(cbx.ForeColor);

                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                        brush = SystemBrushes.HighlightText;

                    e.Graphics.DrawString(cbx.Items[e.Index].ToString(), cbx.Font, brush, e.Bounds, sf);
                }
            }
        }

        private void cueComboBox2_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cbx = sender as ComboBox;
            if (cbx != null)
            {
                e.DrawBackground();

                if (e.Index >= 0)
                {
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;

                    Brush brush = new SolidBrush(cbx.ForeColor);

                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                        brush = SystemBrushes.HighlightText;

                    e.Graphics.DrawString(cbx.Items[e.Index].ToString(), cbx.Font, brush, e.Bounds, sf);
                }
            }
        }

        private void cueComboBox3_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cbx = sender as ComboBox;
            if (cbx != null)
            {
                e.DrawBackground();

                if (e.Index >= 0)
                {
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;

                    Brush brush = new SolidBrush(cbx.ForeColor);

                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                        brush = SystemBrushes.HighlightText;

                    e.Graphics.DrawString(cbx.Items[e.Index].ToString(), cbx.Font, brush, e.Bounds, sf);
                }
            }
        }

        private void cueComboBox4_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cbx = sender as ComboBox;
            if (cbx != null)
            {
                e.DrawBackground();

                if (e.Index >= 0)
                {
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;

                    Brush brush = new SolidBrush(cbx.ForeColor);

                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                        brush = SystemBrushes.HighlightText;

                    e.Graphics.DrawString(cbx.Items[e.Index].ToString(), cbx.Font, brush, e.Bounds, sf);
                }
            }
        }

        private void cueComboBox5_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cbx = sender as ComboBox;
            if (cbx != null)
            {
                e.DrawBackground();

                if (e.Index >= 0)
                {
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;

                    Brush brush = new SolidBrush(cbx.ForeColor);

                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                        brush = SystemBrushes.HighlightText;

                    e.Graphics.DrawString(cbx.Items[e.Index].ToString(), cbx.Font, brush, e.Bounds, sf);
                }
            }
        }

        private void cueComboBox6_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cbx = sender as ComboBox;
            if (cbx != null)
            {
                e.DrawBackground();

                if (e.Index >= 0)
                {
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;

                    Brush brush = new SolidBrush(cbx.ForeColor);

                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                        brush = SystemBrushes.HighlightText;

                    e.Graphics.DrawString(cbx.Items[e.Index].ToString(), cbx.Font, brush, e.Bounds, sf);
                }
            }
        }

        #endregion

        #region cueComboBox SelectedIndex functions

        private void cueComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox1.Focus();

            if (cueComboBox1.SelectedItem != null)
            {
                Line[0] = cueComboBox1.SelectedItem.ToString();
            }
            else
            {
                toolTip1.SetToolTip(cueComboBox1, "The selection is empty!");
                toolTip1.Show("The selection is empty!", cueComboBox1, 3000);

                Line[0] = string.Empty;

                cueComboBox1.SelectedIndexChanged -= new System.EventHandler(cueComboBox1_SelectedIndexChanged);
                cueComboBox1.SelectedIndex = -1;
                cueComboBox1.ResetText();
                cueComboBox1.SelectedItem = null;
                cueComboBox1.SelectedIndexChanged += new System.EventHandler(cueComboBox1_SelectedIndexChanged);
            }
        }

        private void cueComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox1.Focus();

            if (TempKey[0] != cueComboBox2.SelectedItem)
            {
                TempKey[0] = null;

                if (Availability(cueComboBox2.SelectedItem))
                {
                    if (cueComboBox2.SelectedItem != null)
                    {
                        TempKey[0] = cueComboBox2.SelectedItem;
                    }
                    else
                    {
                        toolTip1.SetToolTip(cueComboBox2, "The selection is empty!");
                        toolTip1.Show("The selection is empty!", cueComboBox2, 3000);

                        TempKey[0] = null;

                        cueComboBox2.SelectedIndexChanged -= new System.EventHandler(cueComboBox2_SelectedIndexChanged);
                        cueComboBox2.SelectedIndex = -1;
                        cueComboBox2.ResetText();
                        cueComboBox2.SelectedItem = null;
                        cueComboBox2.SelectedIndexChanged += new System.EventHandler(cueComboBox2_SelectedIndexChanged);
                    }
                }
                else
                {
                    toolTip1.SetToolTip(cueComboBox2, "The key is already assigned!");
                    toolTip1.Show("The key is already assigned!", cueComboBox2, 3000);

                    TempKey[0] = null;

                    cueComboBox2.SelectedIndexChanged -= new System.EventHandler(cueComboBox2_SelectedIndexChanged);
                    cueComboBox2.SelectedIndex = -1;
                    cueComboBox2.ResetText();
                    cueComboBox2.SelectedItem = null;
                    cueComboBox2.SelectedIndexChanged += new System.EventHandler(cueComboBox2_SelectedIndexChanged);
                }
            }
        }

        private void cueComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox1.Focus();

            if (TempKey[1] != cueComboBox3.SelectedItem)
            {
                TempKey[1] = null;

                if (Availability(cueComboBox3.SelectedItem))
                {
                    if (cueComboBox3.SelectedItem != null)
                    {
                        TempKey[1] = cueComboBox3.SelectedItem;
                    }
                    else
                    {
                        toolTip1.SetToolTip(cueComboBox3, "The selection is empty!");
                        toolTip1.Show("The selection is empty!", cueComboBox3, 3000);

                        TempKey[1] = null;

                        cueComboBox3.SelectedIndexChanged -= new System.EventHandler(cueComboBox3_SelectedIndexChanged);
                        cueComboBox3.SelectedIndex = -1;
                        cueComboBox3.ResetText();
                        cueComboBox3.SelectedItem = null;
                        cueComboBox3.SelectedIndexChanged += new System.EventHandler(cueComboBox3_SelectedIndexChanged);
                    }
                }
                else
                {
                    toolTip1.SetToolTip(cueComboBox3, "The key is already assigned!");
                    toolTip1.Show("The key is already assigned!", cueComboBox3, 3000);

                    TempKey[1] = null;

                    cueComboBox3.SelectedIndexChanged -= new System.EventHandler(cueComboBox3_SelectedIndexChanged);
                    cueComboBox3.SelectedIndex = -1;
                    cueComboBox3.ResetText();
                    cueComboBox3.SelectedItem = null;
                    cueComboBox3.SelectedIndexChanged += new System.EventHandler(cueComboBox3_SelectedIndexChanged);
                }
            }
        }

        private void cueComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox1.Focus();

            if (TempKey[2] != cueComboBox4.SelectedItem)
            {
                TempKey[2] = null;

                if (Availability(cueComboBox4.SelectedItem))
                {
                    if (cueComboBox4.SelectedItem != null)
                    {
                        TempKey[2] = cueComboBox4.SelectedItem;
                    }
                    else
                    {
                        toolTip1.SetToolTip(cueComboBox4, "The selection is empty!");
                        toolTip1.Show("The selection is empty!", cueComboBox4, 3000);

                        TempKey[2] = null;

                        cueComboBox4.SelectedIndexChanged -= new System.EventHandler(cueComboBox4_SelectedIndexChanged);
                        cueComboBox4.SelectedIndex = -1;
                        cueComboBox4.ResetText();
                        cueComboBox4.SelectedItem = null;
                        cueComboBox4.SelectedIndexChanged += new System.EventHandler(cueComboBox4_SelectedIndexChanged);
                    }
                }
                else
                {
                    toolTip1.SetToolTip(cueComboBox4, "The key is already assigned!");
                    toolTip1.Show("The key is already assigned!", cueComboBox4, 3000);

                    TempKey[2] = null;

                    cueComboBox4.SelectedIndexChanged -= new System.EventHandler(cueComboBox4_SelectedIndexChanged);
                    cueComboBox4.SelectedIndex = -1;
                    cueComboBox4.ResetText();
                    cueComboBox4.SelectedItem = null;
                    cueComboBox4.SelectedIndexChanged += new System.EventHandler(cueComboBox4_SelectedIndexChanged);
                }
            }
        }

        private void cueComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox1.Focus();

            if (!String.IsNullOrEmpty(cueTextBox1.Text))
            {
                if (TempKey[3] != cueComboBox5.SelectedItem)
                {
                    TempKey[3] = null;

                    if (Availability(cueComboBox5.SelectedItem))
                    {
                        if (cueComboBox5.SelectedItem != null)
                        {
                            TempKey[3] = cueComboBox5.SelectedItem;
                        }
                        else
                        {
                            toolTip1.SetToolTip(cueComboBox5, "The selection is empty!");
                            toolTip1.Show("The selection is empty!", cueComboBox5, 3000);

                            TempKey[3] = null;
                            Line[1] = string.Empty;

                            cueComboBox5.SelectedIndexChanged -= new System.EventHandler(cueComboBox5_SelectedIndexChanged);
                            cueComboBox5.SelectedIndex = -1;
                            cueComboBox5.ResetText();
                            cueComboBox5.SelectedItem = null;
                            cueComboBox5.SelectedIndexChanged += new System.EventHandler(cueComboBox5_SelectedIndexChanged);
                        }
                    }
                    else
                    {
                        toolTip1.SetToolTip(cueComboBox5, "The key is already assigned!");
                        toolTip1.Show("The key is already assigned!", cueComboBox5, 3000);

                        TempKey[3] = null;
                        Line[1] = string.Empty;

                        cueComboBox5.SelectedIndexChanged -= new System.EventHandler(cueComboBox5_SelectedIndexChanged);
                        cueComboBox5.SelectedIndex = -1;
                        cueComboBox5.ResetText();
                        cueComboBox5.SelectedItem = null;
                        cueComboBox5.SelectedIndexChanged += new System.EventHandler(cueComboBox5_SelectedIndexChanged);
                    }
                }
            }
            else
            {
                toolTip1.SetToolTip(cueComboBox5, "The value is not assigned!");
                toolTip1.Show("The value is not assigned!", cueComboBox5, 3000);

                TempKey[3] = null;
                Line[1] = string.Empty;

                cueComboBox5.SelectedIndexChanged -= new System.EventHandler(cueComboBox5_SelectedIndexChanged);
                cueComboBox5.SelectedIndex = -1;
                cueComboBox5.ResetText();
                cueComboBox5.SelectedItem = null;
                cueComboBox5.SelectedIndexChanged += new System.EventHandler(cueComboBox5_SelectedIndexChanged);
            }
        }

        private void cueComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox1.Focus();

            if (!String.IsNullOrEmpty(cueTextBox2.Text))
            {
                if (TempKey[4] != cueComboBox6.SelectedItem)
                {
                    TempKey[4] = null;

                    if (Availability(cueComboBox6.SelectedItem))
                    {
                        if (cueComboBox6.SelectedItem != null)
                        {
                            TempKey[4] = cueComboBox6.SelectedItem;
                        }
                        else
                        {
                            toolTip1.SetToolTip(cueComboBox6, "The selection is empty!");
                            toolTip1.Show("The selection is empty!", cueComboBox6, 3000);

                            TempKey[4] = null;
                            Line[2] = string.Empty;

                            cueComboBox6.SelectedIndexChanged -= new System.EventHandler(cueComboBox6_SelectedIndexChanged);
                            cueComboBox6.SelectedIndex = -1;
                            cueComboBox6.ResetText();
                            cueComboBox6.SelectedItem = null;
                            cueComboBox6.SelectedIndexChanged += new System.EventHandler(cueComboBox6_SelectedIndexChanged);
                        }
                    }
                    else
                    {
                        toolTip1.SetToolTip(cueComboBox6, "The key is already assigned!");
                        toolTip1.Show("The key is already assigned!", cueComboBox6, 3000);

                        TempKey[4] = null;
                        Line[2] = string.Empty;

                        cueComboBox6.SelectedIndexChanged -= new System.EventHandler(cueComboBox6_SelectedIndexChanged);
                        cueComboBox6.SelectedIndex = -1;
                        cueComboBox6.ResetText();
                        cueComboBox6.SelectedItem = null;
                        cueComboBox6.SelectedIndexChanged += new System.EventHandler(cueComboBox6_SelectedIndexChanged);
                    }
                }
            }
            else
            {
                toolTip1.SetToolTip(cueComboBox6, "The value is not assigned!");
                toolTip1.Show("The value is not assigned!", cueComboBox6, 3000);

                TempKey[4] = null;
                Line[2] = string.Empty;

                cueComboBox6.SelectedIndexChanged -= new System.EventHandler(cueComboBox6_SelectedIndexChanged);
                cueComboBox6.SelectedIndex = -1;
                cueComboBox6.ResetText();
                cueComboBox6.SelectedItem = null;
                cueComboBox6.SelectedIndexChanged += new System.EventHandler(cueComboBox6_SelectedIndexChanged);
            }
        }

        #endregion

        #region cueComboBox MouseHover functions

        private void cueComboBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(cueComboBox1, "type");
            toolTip1.Show("type", cueComboBox1);
        }

        private void cueComboBox2_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(cueComboBox2, "key");
            toolTip1.Show("key", cueComboBox2);
        }

        private void cueComboBox3_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(cueComboBox3, "key");
            toolTip1.Show("key", cueComboBox3);
        }

        private void cueComboBox4_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(cueComboBox4, "key");
            toolTip1.Show("key", cueComboBox4);
        }

        private void cueComboBox5_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(cueComboBox5, "key");
            toolTip1.Show("key", cueComboBox5);
        }

        private void cueComboBox6_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(cueComboBox6, "key");
            toolTip1.Show("key", cueComboBox6);
        }

        #endregion

        #region cueComboBox KeyPress functions

        private void cueComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cueComboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cueComboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cueComboBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cueComboBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cueComboBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        #endregion

        #region cueTextBox KeyPress functions

        private void cueTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            const char Delete = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete;
        }

        private void cueTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            const char Delete = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete;
        }

        #endregion

        #region cueTextBox MouseHover functions

        private void cueTextBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(cueTextBox1, "value");
            toolTip1.Show("value", cueTextBox1);
        }

        private void cueTextBox2_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(cueTextBox2, "value");
            toolTip1.Show("value", cueTextBox2);
        }

        #endregion

        #region Button Click functions

        private void button1_Click(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(groupBox1, "Cleared!");
            toolTip1.Show("Cleared!", groupBox1, 3000);

            groupBox1.Focus();

            Line[0] = string.Empty;
            Line[1] = string.Empty;
            Line[2] = string.Empty;

            PrevLine[0] = string.Empty;
            PrevLine[1] = string.Empty;
            PrevLine[2] = string.Empty;

            TempKey[0] = null;
            TempKey[1] = null;
            TempKey[2] = null;
            TempKey[3] = null;
            TempKey[4] = null;

            PrevTempKey[0] = null;
            PrevTempKey[1] = null;
            PrevTempKey[2] = null;
            PrevTempKey[3] = null;
            PrevTempKey[4] = null;

            cueComboBox1.SelectedIndexChanged -= new System.EventHandler(cueComboBox1_SelectedIndexChanged);
            cueComboBox1.SelectedIndex = -1;
            cueComboBox1.ResetText();
            cueComboBox1.SelectedItem = null;
            cueComboBox1.SelectedIndexChanged += new System.EventHandler(cueComboBox1_SelectedIndexChanged);

            cueComboBox2.SelectedIndexChanged -= new System.EventHandler(cueComboBox2_SelectedIndexChanged);
            cueComboBox2.SelectedIndex = -1;
            cueComboBox2.ResetText();
            cueComboBox2.SelectedItem = null;
            cueComboBox2.SelectedIndexChanged += new System.EventHandler(cueComboBox2_SelectedIndexChanged);

            cueComboBox3.SelectedIndexChanged -= new System.EventHandler(cueComboBox3_SelectedIndexChanged);
            cueComboBox3.SelectedIndex = -1;
            cueComboBox3.ResetText();
            cueComboBox3.SelectedItem = null;
            cueComboBox3.SelectedIndexChanged += new System.EventHandler(cueComboBox3_SelectedIndexChanged);

            cueComboBox4.SelectedIndexChanged -= new System.EventHandler(cueComboBox4_SelectedIndexChanged);
            cueComboBox4.SelectedIndex = -1;
            cueComboBox4.ResetText();
            cueComboBox4.SelectedItem = null;
            cueComboBox4.SelectedIndexChanged += new System.EventHandler(cueComboBox4_SelectedIndexChanged);

            cueTextBox1.Text = null;

            cueComboBox5.SelectedIndexChanged -= new System.EventHandler(cueComboBox5_SelectedIndexChanged);
            cueComboBox5.SelectedIndex = -1;
            cueComboBox5.ResetText();
            cueComboBox5.SelectedItem = null;
            cueComboBox5.SelectedIndexChanged += new System.EventHandler(cueComboBox5_SelectedIndexChanged);

            cueTextBox2.Text = null;

            cueComboBox6.SelectedIndexChanged -= new System.EventHandler(cueComboBox6_SelectedIndexChanged);
            cueComboBox6.SelectedIndex = -1;
            cueComboBox6.ResetText();
            cueComboBox6.SelectedItem = null;
            cueComboBox6.SelectedIndexChanged += new System.EventHandler(cueComboBox6_SelectedIndexChanged);

            this.ClientSize = new System.Drawing.Size(154, 301);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox1.Focus();

            Line[1] = cueTextBox1.Text;
            Line[2] = cueTextBox2.Text;

            CreateData();

            PInteraction.sendConfig();

            toolTip1.SetToolTip(groupBox1, "Set!");
            toolTip1.Show("Set!", groupBox1, 3000);

            this.ClientSize = new System.Drawing.Size(154, 301);
        }

        #endregion

        #endregion
    }
}