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

        bool _autoWeigh;

        public delegate void WeighEvent(float weight, bool isPounds);
        public event WeighEvent WeightRecorded;

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
            Debug.LogAsync("Serial Port Pin Changed: " + sender + " " + e);
        }

        private void _serialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            Debug.LogAsync("Serial Port Error Received: " + sender + " " + e);
        }

        private void _serialPort_Disposed(object sender, EventArgs e)
        {
             Debug.LogAsync("Serial Port Disposed: " + sender + " " + e);
        }

        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Debug.LogAsync("Serial Port Data Received: " + sender + " " + e);
            Debug.LogAsync(_serialPort.ReadExisting());
        }

        //////////////////////////////////////////////////////////////////////        THREAD LOOOP          //////////////////////////////////////////////////////////////////

        public void Update()
        {
            while(_run)
            {
                if (_serialPort == null || _serialPort.IsOpen == false)
                    continue;

                if (_autoWeigh)
                    SendData("20", CommandType.ReadLiteralValue, RegisterType.GrossWeight, "");
            
                string output = Read();

                if (output != null || output != "")
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

            if(received.CommandType == CommandType.ReadLiteralValue && received.RegisterType == RegisterType.GrossWeight)
            {
                string text = received.ToString();

                int colonIndex = text.IndexOf(':');

                text = text.Remove(0, colonIndex + 1);

                text = text.Replace("G", "");

                bool isPounds = text.Contains("lb");

                if (isPounds)
                    text = text.Replace("lb", "");
                else
                    text = text.Replace("kg", "");

                text = text.Replace(" ", "");

                float weight = 0f;

                float.TryParse(text, out weight);

                OnWeightRecordedAsync(weight, isPounds);
            }
        }

        void OnWeightRecordedAsync(float weight, bool isPounds)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                WeighEvent handler = WeightRecorded;
                if (handler != null)
                    handler(weight, isPounds);
            }));
        }

        //////////////////////////////////////////////////////////////////////        REQUESTS         //////////////////////////////////////////////////////////////////

        public void SendData(string address, CommandType command, RegisterType register, string parameters)
        {
            Message newMessage = new Message(address, command, register, parameters);

            _serialPort.WriteLine(newMessage.ToString());
        }

        void RequestWeight()
        {
            Message newMessage = new Message("20", CommandType.ReadLiteralValue, RegisterType.GrossWeight, "");

            _serialPort.WriteLine(newMessage.ToString());
        }

        //////////////////////////////////////////////////////////////////////        INCOMING         //////////////////////////////////////////////////////////////////

        public void SetAutoWeigh(bool enabled)
        {
            _autoWeigh = enabled;
        }

        public void End()
        {
            _run = false;
        }

    }
}
