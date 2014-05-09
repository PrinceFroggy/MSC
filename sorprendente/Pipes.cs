using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace sorprendente
{
    /// <summary>
    /// The pipe logic.
    /// </summary>
    class Pipes
    {
        #region Variable

        private static NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "MSC", PipeDirection.InOut);

        #endregion

        #region Functions

        public static bool Initialize()
        {
            pipeClient.Connect();
            pipeClient.ReadMode = PipeTransmissionMode.Byte;

            if (pipeClient.IsConnected)
                return true;
            else
                return false;
        }

        public static void SendBytes(byte[] data, int bytes)
        {
            try
            {
                pipeClient.Write(data, 0, bytes);
                pipeClient.Flush();
            }
            catch
            {

            }
        }

        public static byte[] ReceieveBytes(int bytes)
        {
            byte[] data = new byte[bytes];

            pipeClient.Read(data, 0, bytes);

            return data;
        }

        public static byte booltobyte(bool b)
        {
            if (b) return 1;
            return 0;
        }

        public static bool isConnected()
        {
            return pipeClient.IsConnected;
        }

        public static void Disconnect()
        {
            if (pipeClient.IsConnected)
                pipeClient.Close();
        }

        public static bool Reconnect()
        {
            int i = 0;
            do
            {
                Initialize();
                Thread.Sleep(i * 1000);
                i++;
            } while (!pipeClient.IsConnected && i < 3);
            if (pipeClient.IsConnected)
                return true;
            else
                return false;
        }

        #endregion
    }

    /// <summary>
    /// The pipe interaction logic.
    /// </summary>
    class PInteraction
    {
        #region Variables

        private static bool PipeConnected = false;
        private static bool ActivePipe = false;

        const byte BOT = 1;
        const byte CONFIG = 2;
        const byte RUSH = 3;
        const byte ERROR_CODE = 40;

        private enum extras
        {
            rmessage_val = 4, characterx_val, charactery_val, map_val, monstercount_val,
        }

        private static int rmessage = 0;

        private static int characterx = 0;
        private static int charactery = 0;

        private static int map = 0;

        private static int monstercount = 0;

        private static Form2 _form2;
        private static Form4 _form4;

        #endregion

        #region Pipe functions

        public static void ThreadPipe()
        {
            Thread pipeStart = new Thread(InitializePipe);
            pipeStart.Start();

            ActivePipe = true;
        }

        static void InitializePipe()
        {
            if (Pipes.Initialize())
            {
                PipeConnected = true;
                Thread checkPipe = new Thread(ReadPipe);
                checkPipe.Start();
                Thread.Sleep(2000);
                Thread checkDisconnect = new Thread(CheckPipeDisconnect);
                checkDisconnect.Start();
            }
            else
            {
                MessageBox.Show("Pipe failed to start", "Pipe Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        static void CheckPipeDisconnect()
        {
            while (ActivePipe)
            {
                if (!PipeConnected)
                {
                    Pipes.Reconnect();
                }
                Thread.Sleep(300);
            }
        }

        static void ReadPipe()
        {
            byte[] data = { 0, 0, 0, 0, 0 };

            while (Pipes.isConnected())
            {
                do
                {
                    data = Pipes.ReceieveBytes(5);
                }
                while (data[0] == 0 && Pipes.isConnected());

                switch (data[0])
                {
                    case (int)extras.rmessage_val:
                        rmessage = BitConverter.ToInt32(data, 1);
                        if (_form4 != null)
                            _form4.FUpdate(rmessage);
                        break;

                    case (int)extras.characterx_val:
                        characterx = BitConverter.ToInt32(data, 1);
                        break;

                    case (int)extras.charactery_val:
                        charactery = BitConverter.ToInt32(data, 1);
                        break;

                    case (int)extras.map_val:
                        map = BitConverter.ToInt32(data, 1);
                        if (_form2 != null)
                            _form2.MUpdate(map);
                        break;

                    case (int)extras.monstercount_val:
                        monstercount = BitConverter.ToInt32(data, 1);
                        break;

                    case ERROR_CODE:
                        Pipes.Disconnect();
                        PipeConnected = false;
                        break;
                }

                if (_form2 != null)
                    _form2.CPUpdate(characterx, charactery);

                data[0] = 0;
            }
        }

        static void SendBytesThread(byte[] data, int count)
        {
            Pipes.SendBytes(data, count);
        }

        static void SendBytes(byte[] data, int count)
        {
            Thread SendThread = new Thread(delegate()
            {
                SendBytesThread(data, 2);
            });
            SendThread.Start();
        }

        #endregion

        #region Send functions

        public static void sendForm2(Form2 form2)
        {
            _form2 = form2;
        }

        public static void sendForm4(Form4 form4)
        {
            _form4 = form4;
        }

        public static void sendConfig()
        {
            byte[] data = { CONFIG, 1 };
            SendBytes(data, 2);
        }

        public static void sendRusher(bool b)
        {
            if (b)
            {
                byte[] data = { RUSH, 1 };
                SendBytes(data, 2);
            }
            else
            {
                byte[] data = { RUSH, 0 };
                SendBytes(data, 2);
            }
        }

        #endregion
    }
}