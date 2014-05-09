using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace sorprendente
{
    /// <summary>
    /// The hotkey logic.
    /// </summary>
    class Hotkey
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
            if (hotkeys)
            {
                RegisterHotKey(form.Handle, 1, 0, Convert.ToUInt32(Keys.F1));
                RegisterHotKey(form.Handle, 2, 0, Convert.ToUInt32(Keys.F2));
                RegisterHotKey(form.Handle, 3, 0, Convert.ToUInt32(Keys.F3));
                RegisterHotKey(form.Handle, 4, 0, Convert.ToUInt32(Keys.F4));
            }
            else
            {
                UnregisterHotKey(form.Handle, 1);
                UnregisterHotKey(form.Handle, 2);
                UnregisterHotKey(form.Handle, 3);
                UnregisterHotKey(form.Handle, 4);
            }
        }

        #endregion
    }
}
