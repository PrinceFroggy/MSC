using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Windows.Forms;

namespace sorprendente
{
    /// <summary>
    /// The pipe logic.
    /// </summary>
    class Pipe
    {
        #region Variable

        static NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "negro", PipeDirection.InOut);

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
    class Interaction
    {
        #region Variables

        static bool PipeConnected = false;
        static bool ActivePipe = false;

        const byte ERROR_CODE = 40;

        #endregion

        #region Functions

        public static void InitializePipe()
        {
            if (Pipe.Initialize())
            {
                PipeConnected = true;
                Thread checkPipe = new Thread(ReadPipe);
                checkPipe.Start();
                Thread.Sleep(2000);
                Thread checkDisconnect = new Thread(CheckPipeDisconnect);
                checkDisconnect.Start();
            }
            else
                MessageBox.Show("Pipe failed to start", "Pipe Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void CheckPipeDisconnect()
        {
            while (ActivePipe)
            {
                if (!PipeConnected)
                {
                    Pipe.Reconnect();
                }
                Thread.Sleep(300);
            }
        }

        public static void ReadPipe()
        {
            byte[] data = { 0, 0, 0, 0, 0 };
            while (Pipe.isConnected())
            {
                do
                {
                    data = Pipe.ReceieveBytes(5);
                }
                while (data[0] == 0 && Pipe.isConnected());

                switch (data[0])
                {
                    case ERROR_CODE:
                        Pipe.Disconnect();
                        PipeConnected = false;
                        break;
                }
                data[0] = 0;
            }
        }

        void SendBytesThread(byte[] data, int count)
        {
            Pipe.SendBytes(data, count);
        }

        void SendBytes(byte[] data, int count)
        {
            Thread SendThread = new Thread(delegate()
            {
                SendBytesThread(data, 2);
            });
            SendThread.Start();
        }

        #endregion
    }
}
