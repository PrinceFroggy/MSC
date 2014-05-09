using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;

namespace sorprendente
{
    /// <summary>
    /// The hotkey logic.
    /// </summary>
    class globalHotkey
    {
        #region Imports

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        #endregion

        #region Function

        public static void HotKeys(bool hotkeys, Form form)
        {
            try
            {
                if (hotkeys)
                {
                    RegisterHotKey(form.Handle, 1, 0, Convert.ToUInt32(Keys.F1));
                    RegisterHotKey(form.Handle, 2, 0, Convert.ToUInt32(Keys.F2));
                    RegisterHotKey(form.Handle, 3, 0, Convert.ToUInt32(Keys.F3));
                    RegisterHotKey(form.Handle, 4, 0, Convert.ToUInt32(Keys.F4));
                    RegisterHotKey(form.Handle, 5, 0, Convert.ToUInt32(Keys.F5));
                    RegisterHotKey(form.Handle, 6, 0, Convert.ToUInt32(Keys.F6));
                    RegisterHotKey(form.Handle, 7, 0, Convert.ToUInt32(Keys.F7));
                    RegisterHotKey(form.Handle, 8, 0, Convert.ToUInt32(Keys.F8));
                }
                else
                {
                    UnregisterHotKey(form.Handle, 1);
                    UnregisterHotKey(form.Handle, 2);
                    UnregisterHotKey(form.Handle, 3);
                    UnregisterHotKey(form.Handle, 4);
                    UnregisterHotKey(form.Handle, 5);
                    UnregisterHotKey(form.Handle, 6);
                    UnregisterHotKey(form.Handle, 7);
                    UnregisterHotKey(form.Handle, 8);
                }
            }
            catch
            {

            }
        }

        #endregion
    }

    /// <summary>
    /// The custom controls logic.
    /// </summary>
    public class CueTextBox : TextBox
    {
        #region PInvoke Helpers

        private static uint ECM_FIRST = 0x1500;
        private static uint EM_SETCUEBANNER = ECM_FIRST + 1;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, String lParam);

        #endregion PInvoke Helpers

        #region CueText

        private string _cueText = String.Empty;

        /// <summary>
        /// Gets or sets the text the <see cref="TextBox"/> will display as a cue to the user.
        /// </summary>
        [Description("The text value to be displayed as a cue to the user.")]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string CueText
        {
            get { return _cueText; }
            set
            {
                if (value == null)
                {
                    value = String.Empty;
                }

                if (!_cueText.Equals(value, StringComparison.CurrentCulture))
                {
                    _cueText = value;
                    UpdateCue();
                    OnCueTextChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Occurs when the <see cref="CueText"/> property value changes.
        /// </summary>
        public event EventHandler CueTextChanged;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnCueTextChanged(EventArgs e)
        {
            EventHandler handler = CueTextChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion CueText

        #region ShowCueTextOnFocus

        private bool _showCueTextWithFocus = false;

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="TextBox"/> will display the <see cref="CueText"/>
        /// even when the control has focus.
        /// </summary>
        [Description("Indicates whether the CueText will be displayed even when the control has focus.")]
        [Category("Appearance")]
        [DefaultValue(false)]
        [Localizable(true)]
        public bool ShowCueTextWithFocus
        {
            get { return _showCueTextWithFocus; }
            set
            {
                if (_showCueTextWithFocus != value)
                {
                    _showCueTextWithFocus = value;
                    UpdateCue();
                    OnShowCueTextWithFocusChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Occurs when the <see cref="ShowCueTextWithFocus"/> property value changes.
        /// </summary>
        public event EventHandler ShowCueTextWithFocusChanged;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnShowCueTextWithFocusChanged(EventArgs e)
        {
            EventHandler handler = ShowCueTextWithFocusChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion ShowCueTextOnFocus

        #region Overrides

        protected override void OnHandleCreated(EventArgs e)
        {
            UpdateCue();

            base.OnHandleCreated(e);
        }

        #endregion Overrides

        private void UpdateCue()
        {
            if (this.IsHandleCreated)
            {
                SendMessage(new HandleRef(this, this.Handle), EM_SETCUEBANNER, (_showCueTextWithFocus) ? new IntPtr(1) : IntPtr.Zero, _cueText);
            }
        }
    }

    public class CueComboBox : ComboBox
    {
        #region PInvoke Helpers

        private static uint CB_SETCUEBANNER = 0x1703;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, String lParam);

        #endregion PInvoke Helpers

        #region CueText

        private const Keys PasteKeys = Keys.Control | Keys.V;
        private string _cueText = String.Empty;

        public CueComboBox(): base()
        {
            //disable right click menu
            ContextMenu = new ContextMenu();
        }

        /// <summary>
        /// Gets or sets the text the <see cref="ComboBox"/> will display as a cue to the user.
        /// </summary>
        [Description("The text value to be displayed as a cue to the user.")]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string CueText
        {
            get { return _cueText; }
            set
            {
                if (value == null)
                {
                    value = String.Empty;
                }

                if (!_cueText.Equals(value, StringComparison.CurrentCulture))
                {
                    _cueText = value;
                    UpdateCue();
                    OnCueTextChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Occurs when the <see cref="CueText"/> property value changes.
        /// </summary>
        public event EventHandler CueTextChanged;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnCueTextChanged(EventArgs e)
        {
            EventHandler handler = CueTextChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion CueText

        #region Overrides

        protected override void OnHandleCreated(EventArgs e)
        {
            UpdateCue();

            base.OnHandleCreated(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == PasteKeys)
            {
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        #endregion Overrides

        private void UpdateCue()
        {
            if (this.IsHandleCreated)
            {
                SendMessage(new HandleRef(this, this.Handle), CB_SETCUEBANNER, IntPtr.Zero, _cueText);
            }
        }
    }

    public class CueToolStripTextBox : ToolStripTextBox
    {
        public CueToolStripTextBox()
            : base()
        {
            if (this.Control != null)
            {
                this.Control.HandleCreated += new EventHandler(OnControlHandleCreated);
            }
        }

        public CueToolStripTextBox(string name)
            : base(name)
        {
            if (this.Control != null)
            {
                this.Control.HandleCreated += new EventHandler(OnControlHandleCreated);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Control != null)
                {
                    this.Control.HandleCreated -= new EventHandler(OnControlHandleCreated);
                }
            }

            base.Dispose(disposing);
        }

        void OnControlHandleCreated(object sender, EventArgs e)
        {
            UpdateCue();
        }

        #region PInvoke Helpers

        private static uint ECM_FIRST = 0x1500;
        private static uint EM_SETCUEBANNER = ECM_FIRST + 1;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, String lParam);

        #endregion PInvoke Helpers

        #region CueText

        private string _cueText = String.Empty;

        /// <summary>
        /// Gets or sets the text the <see cref="TextBox"/> will display as a cue to the user.
        /// </summary>
        [Description("The text value to be displayed as a cue to the user.")]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string CueText
        {
            get { return _cueText; }
            set
            {
                if (value == null)
                {
                    value = String.Empty;
                }

                if (!_cueText.Equals(value, StringComparison.CurrentCulture))
                {
                    _cueText = value;
                    UpdateCue();
                    OnCueTextChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Occurs when the <see cref="CueText"/> property value changes.
        /// </summary>
        public event EventHandler CueTextChanged;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnCueTextChanged(EventArgs e)
        {
            EventHandler handler = CueTextChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion CueText

        #region ShowCueTextOnFocus

        private bool _showCueTextWithFocus = false;

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="TextBox"/> will display the <see cref="CueText"/>
        /// even when the control has focus.
        /// </summary>
        [Description("Indicates whether the CueText will be displayed even when the control has focus.")]
        [Category("Appearance")]
        [DefaultValue(false)]
        [Localizable(true)]
        public bool ShowCueTextWithFocus
        {
            get { return _showCueTextWithFocus; }
            set
            {
                if (_showCueTextWithFocus != value)
                {
                    _showCueTextWithFocus = value;
                    UpdateCue();
                    OnShowCueTextWithFocusChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Occurs when the <see cref="ShowCueTextWithFocus"/> property value changes.
        /// </summary>
        public event EventHandler ShowCueTextWithFocusChanged;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnShowCueTextWithFocusChanged(EventArgs e)
        {
            EventHandler handler = ShowCueTextWithFocusChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion ShowCueTextOnFocus

        private void UpdateCue()
        {
            if ((this.Control != null) && (this.Control.IsHandleCreated))
            {
                SendMessage(new HandleRef(this.Control, this.Control.Handle), EM_SETCUEBANNER, (_showCueTextWithFocus) ? new IntPtr(1) : IntPtr.Zero, _cueText);
            }
        }
    }

    /// <summary>
    /// A transparent control.
    /// </summary>
    public class TransparentPanel : Panel
    {
        public TransparentPanel()
        {
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                return createParams;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Do not paint background.
        }
    }

    /// <summary>
    /// Contains helper methods to change extended styles on ListView, including enabling double buffering.
    /// Based on Giovanni Montrone's article on <see cref="http://www.codeproject.com/KB/list/listviewxp.aspx"/>
    /// </summary>
    /// 
    public enum ListViewExtendedStyles
    {
        /// <summary>
        /// LVS_EX_GRIDLINES
        /// </summary>
        GridLines = 0x00000001,
        /// <summary>
        /// LVS_EX_SUBITEMIMAGES
        /// </summary>
        SubItemImages = 0x00000002,
        /// <summary>
        /// LVS_EX_CHECKBOXES
        /// </summary>
        CheckBoxes = 0x00000004,
        /// <summary>
        /// LVS_EX_TRACKSELECT
        /// </summary>
        TrackSelect = 0x00000008,
        /// <summary>
        /// LVS_EX_HEADERDRAGDROP
        /// </summary>
        HeaderDragDrop = 0x00000010,
        /// <summary>
        /// LVS_EX_FULLROWSELECT
        /// </summary>
        FullRowSelect = 0x00000020,
        /// <summary>
        /// LVS_EX_ONECLICKACTIVATE
        /// </summary>
        OneClickActivate = 0x00000040,
        /// <summary>
        /// LVS_EX_TWOCLICKACTIVATE
        /// </summary>
        TwoClickActivate = 0x00000080,
        /// <summary>
        /// LVS_EX_FLATSB
        /// </summary>
        FlatsB = 0x00000100,
        /// <summary>
        /// LVS_EX_REGIONAL
        /// </summary>
        Regional = 0x00000200,
        /// <summary>
        /// LVS_EX_INFOTIP
        /// </summary>
        InfoTip = 0x00000400,
        /// <summary>
        /// LVS_EX_UNDERLINEHOT
        /// </summary>
        UnderlineHot = 0x00000800,
        /// <summary>
        /// LVS_EX_UNDERLINECOLD
        /// </summary>
        UnderlineCold = 0x00001000,
        /// <summary>
        /// LVS_EX_MULTIWORKAREAS
        /// </summary>
        MultilWorkAreas = 0x00002000,
        /// <summary>
        /// LVS_EX_LABELTIP
        /// </summary>
        LabelTip = 0x00004000,
        /// <summary>
        /// LVS_EX_BORDERSELECT
        /// </summary>
        BorderSelect = 0x00008000,
        /// <summary>
        /// LVS_EX_DOUBLEBUFFER
        /// </summary>
        DoubleBuffer = 0x00010000,
        /// <summary>
        /// LVS_EX_HIDELABELS
        /// </summary>
        HideLabels = 0x00020000,
        /// <summary>
        /// LVS_EX_SINGLEROW
        /// </summary>
        SingleRow = 0x00040000,
        /// <summary>
        /// LVS_EX_SNAPTOGRID
        /// </summary>
        SnapToGrid = 0x00080000,
        /// <summary>
        /// LVS_EX_SIMPLESELECT
        /// </summary>
        SimpleSelect = 0x00100000
    }

    public enum ListViewMessages
    {
        First = 0x1000,
        SetExtendedStyle = (First + 54),
        GetExtendedStyle = (First + 55),
    }

    public class ListViewHelper
    {
        private ListViewHelper()
        {
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr handle, int messg, int wparam, int lparam);

        public static void SetExtendedStyle(Control control, ListViewExtendedStyles exStyle)
        {
            ListViewExtendedStyles styles;
            styles = (ListViewExtendedStyles)SendMessage(control.Handle, (int)ListViewMessages.GetExtendedStyle, 0, 0);
            styles |= exStyle;
            SendMessage(control.Handle, (int)ListViewMessages.SetExtendedStyle, 0, (int)styles);
        }

        public static void EnableDoubleBuffer(Control control)
        {
            ListViewExtendedStyles styles;
            // read current style
            styles = (ListViewExtendedStyles)SendMessage(control.Handle, (int)ListViewMessages.GetExtendedStyle, 0, 0);
            // enable double buffer and border select
            styles |= ListViewExtendedStyles.DoubleBuffer | ListViewExtendedStyles.BorderSelect;
            // write new style
            SendMessage(control.Handle, (int)ListViewMessages.SetExtendedStyle, 0, (int)styles);
        }
        public static void DisableDoubleBuffer(Control control)
        {
            ListViewExtendedStyles styles;
            // read current style
            styles = (ListViewExtendedStyles)SendMessage(control.Handle, (int)ListViewMessages.GetExtendedStyle, 0, 0);
            // disable double buffer and border select
            styles -= styles & ListViewExtendedStyles.DoubleBuffer;
            styles -= styles & ListViewExtendedStyles.BorderSelect;
            // write new style
            SendMessage(control.Handle, (int)ListViewMessages.SetExtendedStyle, 0, (int)styles);
        }
    }
}
