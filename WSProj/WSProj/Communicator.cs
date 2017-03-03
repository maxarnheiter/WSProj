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

        List<Message> _sent = new List<Message>();

        List<Message> _received = new List<Message>();

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

        //////////////////////////////////////////////////////////////////////        THREAD LOOOP          //////////////////////////////////////////////////////////////////

        public void Update()
        {
            while(_run)
            {
                if (_serialPort == null || _serialPort.IsOpen == false)
                    continue;

                string output = Read();

                if(output != null || output != "")
                {
                    OnMessageReceived(output);
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

        //////////////////////////////////////////////////////////////////////        EVENTS         ////////////////////////////////////////////////////////////////////

        void OnMessageReceived(string message)
        {

            Message received = new Message(message);


            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                Debug.Log(message);
                Debug.Log(received.ToString());
            }));
        }

        //////////////////////////////////////////////////////////////////////        INCOMING         //////////////////////////////////////////////////////////////////

        public void SendData(string address, CommandType command, RegisterType register, string parameters)
        {
            Message newMessage = new Message(address, command, register, parameters);

            Debug.Log("Sending command: " + newMessage.ToString());

            _sent.Add(newMessage);

            _serialPort.WriteLine(newMessage.ToString());
        }

        public void End()
        {
            _run = false;
        }

    }
}
