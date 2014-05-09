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
    public partial class Form7 : Form
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

        public Form7()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(groupBox1, "Cleared!");
            toolTip1.Show("Cleared!", groupBox1, 3000);

            groupBox1.Focus();

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

            cueTextBox1.Text = null;
            cueTextBox2.Text = null;
            cueTextBox3.Text = null;

            this.ClientSize = new System.Drawing.Size(154, 189);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cueTextBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(cueTextBox1, cueTextBox1.Text);
            toolTip1.Show(cueTextBox1.Text, cueTextBox1);
        }

        private void cueTextBox2_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(cueTextBox2, cueTextBox2.Text);
            toolTip1.Show(cueTextBox2.Text, cueTextBox2);
        }

        private void cueTextBox3_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(cueTextBox3, cueTextBox3.Text);
            toolTip1.Show(cueTextBox3.Text, cueTextBox3);
        }

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

        private void cueComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
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

        private void cueComboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Form7_Click(object sender, EventArgs e)
        {
            groupBox1.Focus();
        }

        private void cueComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cueComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
