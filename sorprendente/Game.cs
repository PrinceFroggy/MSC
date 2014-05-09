using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace sorprendente
{
    /// <summary>
    /// The creation logic.
    /// </summary>
    class Game
    {
        #region Structures

        public struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public uint dwProcessId;
            public uint dwThreadId;
        }

        public struct STARTUPINFO
        {
            public uint cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public uint dwX;
            public uint dwY;
            public uint dwXSize;
            public uint dwYSize;
            public uint dwXCountChars;
            public uint dwYCountChars;
            public uint dwFillAttribute;
            public uint dwFlags;
            public short wShowWindow;
            public short cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        #endregion

        #region Imports

        [DllImport("kernel32.dll")]
        static extern bool CreateProcess(string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);

        [DllImport("kernel32.dll")]
        static extern uint ResumeThread(IntPtr hThread);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

        #endregion

        #region Variables

        private static STARTUPINFO StartupInfo;
        private static PROCESS_INFORMATION ProcessInfo;

        private static RegistryKey MSroot = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wizet\\").OpenSubKey("MapleStory");
        private static RegistryKey MSCroot = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\MSC");

        private static string MSpath;
        private static string DIRname = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + "MSC" + "\\";
        private static string DLLname;

        public static string ERRORname;

        #endregion

        #region Functions

        public static bool Launch()
        {
            try
            {
                ERRORname = "InstanceAlive";
                if (!Instance.Alive())
                {
                    ERRORname = "CreateRegistry";
                    if (CreateRD())
                    {
                        ERRORname = "CreatePath";
                        if (PathGame())
                        {
                            ERRORname = "StartProcess";
                            if (StartProcess())
                            {
                                ERRORname = "CreateDLL";
                                if (CreateDLL())
                                {
                                    ERRORname = "InjectDLL";
                                    if (InjectDLL())
                                    {
                                        Instance.Check();

                                        return true;
                                    }
                                }
                            }
                        }
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        static bool CreateRD()
        {
            try
            {
                if (MSCroot == null)
                {
                    MSCroot = Registry.LocalMachine.CreateSubKey("Software\\Wow6432Node\\MSC");

                    MSCroot.SetValue("ru", "");
                    MSCroot.SetValue("ra", "");
                    MSCroot.SetValue("ak", "");
                    MSCroot.SetValue("lk", "");
                    MSCroot.SetValue("jk", "");
                    MSCroot.SetValue("hv", "");
                    MSCroot.SetValue("hk", "");
                    MSCroot.SetValue("mv", "");
                    MSCroot.SetValue("mk", "");

                    MSCroot.Close();
                }

                if (!Directory.Exists(DIRname))
                {
                    DirectoryInfo folder = Directory.CreateDirectory(DIRname);
                    folder.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        static bool PathGame()
        {
            try
            {
                if (File.Exists(MSroot.GetValue("RootPath").ToString() + @"\MapleStory.exe"))
                {
                    MSpath = MSroot.GetValue("RootPath").ToString() + @"\MapleStory.exe";

                    MSroot.Close();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        static bool StartProcess()
        {
            try
            {
                StartupInfo = new STARTUPINFO();

                ProcessInfo = new PROCESS_INFORMATION();

                if (CreateProcess(MSpath, " GameLaunching", IntPtr.Zero, IntPtr.Zero, false, 4, IntPtr.Zero, null, ref StartupInfo, out ProcessInfo))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        static bool CreateDLL()
        {
            try
            {
                Array.ForEach(Directory.GetFiles(DIRname, "*.dll"), File.Delete);
                DLLname = DIRname + ProcessInfo.dwProcessId.ToString() + ".dll";
                File.WriteAllBytes(DLLname, Properties.Resources.bello);

                return true;
            }
            catch 
            {
                TerminateProcess(ProcessInfo.hProcess, 0);

                return false;
            }
        }

        static bool InjectDLL()
        {
            try
            {
                int ProcessID = Convert.ToInt32(ProcessInfo.dwProcessId);

                IntPtr hProcess = (IntPtr)Injector.OpenProcess(0x1F0FFF, 1, ProcessID);

                if (hProcess != null)
                {
                    Injector.InjectDLL(hProcess, DLLname);

                    ResumeThread(ProcessInfo.hThread);

                    return true;
                }
                else
                {
                    TerminateProcess(ProcessInfo.hProcess, 0);

                    return false;
                }
            }
            catch
            {
                TerminateProcess(ProcessInfo.hProcess, 0);

                return false;
            }
        }

        #endregion
    }

    /// <summary>
    /// The injection logic.
    /// </summary>
    class Injector
    {
        #region Imports

        [DllImport("kernel32")]
        public static extern IntPtr CreateRemoteThread(
          IntPtr hProcess,
          IntPtr lpThreadAttributes,
          uint dwStackSize,
          UIntPtr lpStartAddress,
          IntPtr lpParameter,
          uint dwCreationFlags,
          out IntPtr lpThreadId
        );

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(
            UInt32 dwDesiredAccess,
            Int32 bInheritHandle,
            Int32 dwProcessId
            );

        [DllImport("kernel32.dll")]
        public static extern Int32 CloseHandle(
        IntPtr hObject
        );

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool VirtualFreeEx(
            IntPtr hProcess,
            IntPtr lpAddress,
            UIntPtr dwSize,
            uint dwFreeType
            );

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern UIntPtr GetProcAddress(
            IntPtr hModule,
            string procName
            );

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(
            IntPtr hProcess,
            IntPtr lpAddress,
            uint dwSize,
            uint flAllocationType,
            uint flProtect
            );

        [DllImport("kernel32.dll")]
        static extern bool WriteProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            string lpBuffer,
            UIntPtr nSize,
            out IntPtr lpNumberOfBytesWritten
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(
            string lpModuleName
            );

        [DllImport("kernel32", SetLastError = true, ExactSpelling = true)]
        internal static extern Int32 WaitForSingleObject(
            IntPtr handle,
            Int32 milliseconds
            );

        #endregion

        #region Functions

        public static Int32 GetProcessId(String proc)
        {
            Process[] ProcList;
            ProcList = Process.GetProcessesByName(proc);
            return ProcList[0].Id;
        }

        private static byte[] CalcBytes(string sToConvert)
        {
            byte[] bRet = System.Text.Encoding.ASCII.GetBytes(sToConvert);
            return bRet;
        }

        public static void InjectDLL(IntPtr hProcess, String strDLLName)
        {
            IntPtr bytesout;
            Int32 LenWrite = strDLLName.Length + 1;
            IntPtr AllocMem = (IntPtr)VirtualAllocEx(hProcess, (IntPtr)null, (uint)LenWrite, 0x1000, 0x40);
            WriteProcessMemory(hProcess, AllocMem, strDLLName, (UIntPtr)LenWrite, out bytesout);
            UIntPtr Injector = (UIntPtr)GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
            IntPtr hThread = (IntPtr)CreateRemoteThread(hProcess, (IntPtr)null, 0, Injector, AllocMem, 0, out bytesout);
            int Result = WaitForSingleObject(hThread, 1000);
            Thread.Sleep(1000);
            VirtualFreeEx(hProcess, AllocMem, (UIntPtr)0, 0x8000);
            if (hThread != null)
            {
                CloseHandle(hThread);
            }
            return;
        }

        #endregion 
    }

    /// <summary>
    /// The multiple & maplestory instances logic.
    /// </summary>
    class Instance
    {
        #region Imports

        [DllImport("psapi.dll")]
        public static extern int EmptyWorkingSet(IntPtr hwProc);

        #endregion

        #region Variable

        private static Thread isRunning;

        #endregion

        #region Function

        public static bool Alive()
        {
            var sorprendente = Process.GetCurrentProcess();
            var Processes = Process.GetProcessesByName(sorprendente.ProcessName);

            for (var i = 0; i < Processes.Length; i++)
            {
                if (Processes[i].Id != sorprendente.Id)
                {
                    return true;
                }
            }
            return false;
        }

        public static void Check()
        {
            isRunning = new Thread(new ThreadStart(Running));
            isRunning.IsBackground = true;
            isRunning.Start();
        }

        static void Running()
        {
            while (true)
            {
                Process[] MSprocess = Process.GetProcessesByName("MapleStory");

                int i = MSprocess.Length;

                while (i > 0)
                {
                    try
                    {
                        EmptyWorkingSet(Process.GetProcessById(MSprocess[i - 1].Id).Handle);
                        EmptyWorkingSet(Process.GetCurrentProcess().Handle);
                    }
                    catch
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }

        #endregion
    }
}