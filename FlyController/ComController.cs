using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
namespace FlyController
{
    static class ComController
    {
        public delegate void _receiveMessageDelegate(string message);
        public static event _receiveMessageDelegate receiveMessage;
        public static event _receiveMessageDelegate writeMessage;
        static SerialPort _serialPort;
        static string name;
        public static string[] GetOpenPorts()
        {
            return SerialPort.GetPortNames(); 
        }

        public static void Init(string Name)
        {
            if (_serialPort != null)
                if (_serialPort.IsOpen)
                    Close();
            _serialPort = new SerialPort(Name,9600);
            _serialPort.Handshake= Handshake.None;
            _serialPort.DataReceived += ReceiveMessage;
            name = Name;
        }

        public static void Open()
        {
            _serialPort.Open();
        }

        private static void ReceiveMessage(object sender, SerialDataReceivedEventArgs e)
        {
            string data = _serialPort.ReadLine();
            receiveMessage?.Invoke(data);
        }

        public static void Write(string str)
        {
            try
            {
                _serialPort.Write(str);
                writeMessage?.Invoke(str);
            }
            catch
            {

            }
        }

        public static void Close()
        {
            _serialPort.Close();
        }

        public static string GetInfo()
        {
            return name;
        }
    }
}
