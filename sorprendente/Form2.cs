using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Threading;
using sorprendente;

namespace sorprendente
{
    public partial class Form2 : Form
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

        private Map mdic = new Map();

        private Image player;
        private Image portal;

        private string id;
        private string name;

        private int map;
        private int oldmap;

        private int characterX;
        private int characterY;

        private int mwidth;
        private int mheight;
        private int mcenterX;
        private int mcenterY;

        private int relativeX;
        private int relativeY;
        private int imageposX;
        private int imageposY;

        private int pblock;

        private string uri;
        private bool _isExecutedOnce = false;
        private XElement xelement;

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

        void RGraphics()
        {
            player = Properties.Resources.player;
            portal = Properties.Resources.portal;
        }

        public void MUpdate(int i)
        {
            map = i;

            try
            {
                if (map != 0)
                {
                    if (map != oldmap && mdic.TryGetValue(map, out name))
                    {
                        pblock = 0;

                        FID();
                        GMD();
                        SMI();

                        oldmap = map;
                        _isExecutedOnce = false;
                    }
                }
                else
                {
                    if (!_isExecutedOnce)
                    {
                        RTP();

                        _isExecutedOnce = true;
                    }
                }
            }
            catch
            {
                RTP();
            }
        }

        void RTP()
        {
            pblock = 0;

            oldmap = 0;

            if (this.InvokeRequired)
            {
                Invoke(new MethodInvoker(() => 
                { 
                    toolTip1.SetToolTip(pictureBox1, null); 
                    
                    pictureBox1.Image = pictureBox1.ErrorImage; 
                    
                    this.Size = new Size(162, 120); 
                    this.Refresh(); 
                }));
            }
            else
            {
                toolTip1.SetToolTip(pictureBox1, null); 
                
                pictureBox1.Image = pictureBox1.ErrorImage; 
                
                this.Size = new Size(162, 120); 
                this.Refresh();
            }
        }

        void FID()
        {
            if (map <= 90000)
            {
                id = "0000" + map.ToString();
            }
            else
            {
                if (map <= 900000)
                {
                    id = "000" + map.ToString();
                }
                else
                {
                    if (map <= 9000000)
                    {
                        if (!NRoute(map))
                            id = "00" + map.ToString();
                    }
                    else
                    {
                        id = map.ToString();
                    }
                }
            }
        }

        bool NRoute(int i)
        {
            if (i == 004000013)
            {
                id = "000040000";
                return true;
            }
            else if (i == 004000020)
            {
                id = "001000000";
                return true;
            }
            else if (i == 004000021)
            {
                id = "001010000";
                return true;
            }
            else if (i == 004000026)
            {
                id = "001020000";
                return true;
            }
            else if (i == 004000030)
            {
                id = "002000000";
                return true;
            }
            else
            {
                return false;
            }
        }

        void GMD()
        {
            uri = "http://randomspam.co/XML/" + id + ".img.xml";
            xelement = XElement.Load(uri);

            mwidth = MDimensions.GetInt(xelement, "width");
            mheight = MDimensions.GetInt(xelement, "height");
            mcenterX = MDimensions.GetInt(xelement, "centerX");
            mcenterY = MDimensions.GetInt(xelement, "centerY");
        }

        void SMI()
        {
            if (this.InvokeRequired)
            {
                Invoke(new MethodInvoker(() =>
                {
                    toolTip1.SetToolTip(pictureBox1, String.Join(": ", name.Split(new[]{": "}, StringSplitOptions.None).Skip(1).ToArray()));
                    pictureBox1.ImageLocation = "http://randomspam.co/CANVAS/" + id + ".img/miniMap.canvas.png";
                }));
            }
            else
            {
                toolTip1.SetToolTip(pictureBox1, String.Join(": ", name.Split(new[] { ": " }, StringSplitOptions.None).Skip(1).ToArray()));
                pictureBox1.ImageLocation = "http://randomspam.co/CANVAS/" + id + ".img/miniMap.canvas.png";
            }
        }

        public void CPUpdate(int x, int y)
        {
            characterX = x;
            characterY = y;

            if (map != 0 && pblock != 0)
            {
                if (this.InvokeRequired)
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        relativeX = mwidth / pictureBox1.Image.Width;
                        relativeY = mheight / pictureBox1.Image.Height;
                        imageposX = (mcenterX / relativeX) + (characterX / relativeX);
                        imageposY = (mcenterY / relativeY) + (characterY / relativeY);
                        pictureBox1.Refresh();
                    }));
                }
                else
                {
                    relativeX = mwidth / pictureBox1.Image.Width;
                    relativeY = mheight / pictureBox1.Image.Height;
                    imageposX = (mcenterX / relativeX) + (characterX / relativeX);
                    imageposY = (mcenterY / relativeY) + (characterY / relativeY);
                    pictureBox1.Refresh();
                }
            }
        }

        #endregion

        #region Form functions

        #region Form2 function

        public Form2()
        {
            InitializeComponent();
            RGraphics();

            PInteraction.sendForm2(this);
        }

        private void Form2_Click(object sender, EventArgs e)
        {
            pictureBox1.Focus();
        }

        #endregion

        #region Picturebox functions

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (pblock != 0)
            {
                e.Graphics.CompositingMode = CompositingMode.SourceOver;

                MDraw.DrawPortal(xelement, e.Graphics, portal, mcenterX, mcenterY, relativeX, relativeY);
                MDraw.DrawPlayer(e.Graphics, player, imageposX, imageposY);

                e.Graphics.Flush();
            }
        }

        private void pictureBox1_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                RTP();
            }
            else
            {
                this.Size = new Size(pictureBox1.Width + 16, pictureBox1.Height + 34);
                this.Refresh();

                pblock = 1;
            }
        }

        #endregion

        #endregion
    }
}