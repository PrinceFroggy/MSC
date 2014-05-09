using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sorprendente
{
    class WRegistry
    {
        #region Variable

        private static RegistryKey Mroot;

        #endregion

        #region Functions

        public static void SetReg()
        {
            if (System.Environment.Is64BitOperatingSystem)
            {
                Mroot = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            }
            else
            {
                Mroot = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            }

            Mroot = Mroot.OpenSubKey(@"SOFTWARE\Wow6432Node\MSC", true);
        }

        public static void SetLine(string s, int n)
        {
            switch (n)
            {
                case 0:
                    Mroot.SetValue("ra", s);
                    break;

                case 1:
                    Mroot.SetValue("hv", s);
                    break;

                case 2:
                    Mroot.SetValue("mv", s);
                    break;
            }
        }

        public static void SetKey(object o, int n)
        {
            switch (n)
            {
                case 0:
                    Mroot.SetValue("ak", o);
                    break;

                case 1:
                    Mroot.SetValue("lk", o);
                    break;

                case 2:
                    Mroot.SetValue("jk", o);
                    break;

                case 3:
                    Mroot.SetValue("hk", o);
                    break;

                case 4:
                    Mroot.SetValue("mk", o);
                    break;
            }
        }

        public static void SetText(string s)
        {
            Mroot.SetValue("ru", s);
        }

        public static void CloseReg()
        {
            Mroot.Close();
        }

        #endregion
    }
}
