using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;
using System.Windows;

namespace WSProj
{
    public class Communicator
    {
        SerialPort _serialPort;

        bool _run = true;

        public Communicator(SerialPort serialPort)
        {
            _serialPort = serialPort;

            Debug.Log("Communicator initialized");

            //ListenToSerialPortEvents();
        }

        void ListenToSerialPortEvents()
        {
            _serialPort.DataReceived += _serialPort_DataReceived;
            _serialPort.Disposed += _serialPort_Disposed;
            _serialPort.ErrorReceived += _serialPort_ErrorReceived;
            _serialPort.PinChanged += _serialPort_PinChanged;

            Debug.Log("Listening to serial port events");
        }

        //////////////////////////////////////////////////////////////////////        SERIAL PORT EVENTS          //////////////////////////////////////////////////////////////////

        private void _serialPort_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                Debug.Log("Serial Port Pin Changed: " + sender + " " + e);
            }));
        }

        private void _serialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                Debug.Log("Serial Port Error Received: " + sender + " " + e);
            }));
        }

        private void _serialPort_Disposed(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                Debug.Log("Serial Port Disposed: " + sender + " " + e);
            }));
        }

        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                Debug.Log("Serial Port Data Received: " + sender + " " + e);
                Debug.Log(_serialPort.ReadExisting());
            }));
        }

        public void Update()
        {
            while(_run)
            {
                if (_serialPort == null || _serialPort.IsOpen == false)
                    continue;

                string output = Read();

                if(output != null || output != "")
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        Debug.Log(output);
                    }));
                }
            }
        }

        string Read()
        {
            string output = null;

            try
            {
                output = _serialPort.ReadLine();
            }
            catch (TimeoutException) { }

            return output;
        }
        
        public void SendData(string address, CommandType commandType, RegisterType registerType, string optionalParameters)
        {
            string command = address + GetCommandValue(commandType) + GetRegisterValue(registerType) + ":" + optionalParameters;

            Debug.Log("Sending command: " + command);

            _serialPort.WriteLine(command);
        }

        string GetCommandValue(CommandType commandType)
        {
            switch (commandType)
            {
                case CommandType.ReadLiteralValue:
                    return "05";
                case CommandType.ReadFinalValue:
                    return "11";
                case CommandType.WriteFinalValue:
                    return "12";
            }

            return "";
        }

        string GetRegisterValue(RegisterType registerType)
        {
            switch (registerType)
            {
                case RegisterType.GrossWeight:
                    return "0026";
                case RegisterType.NetWeight:
                    return "0027";
                case RegisterType.SetpointType:
                    return "0170";
                case RegisterType.SetpointSource:
                    return "0171";
                case RegisterType.SetpointTarget:
                    return "0172";
                case RegisterType.KeyPress:
                    return "0008";
                case RegisterType.SystemStatus:
                    return "0021";
                case RegisterType.SystemError:
                    return "0022";
            }

            return "";
        }
    }
}
