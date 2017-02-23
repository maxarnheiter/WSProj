using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.IO.Ports;


namespace WSProj
{
   

    public partial class MainWindow : Window
    {

        string[] _portNames;

        SerialPort _serialPort = new SerialPort();


        public MainWindow()
        {
            InitializeComponent();

            InitializeUI();

            ListenToSerialPortEvents();
        }

        void ListenToSerialPortEvents()
        {
            _serialPort.DataReceived += _serialPort_DataReceived;
            _serialPort.Disposed += _serialPort_Disposed;
            _serialPort.ErrorReceived += _serialPort_ErrorReceived;
            _serialPort.PinChanged += _serialPort_PinChanged;
        }

        //////////////////////////////////////////////////////////////////////        SERIAL PORT EVENTS          //////////////////////////////////////////////////////////////////

        private void _serialPort_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            Console.WriteLine("Serial Port Pin Changed: " + sender + " " + e);
        }

        private void _serialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            Console.WriteLine("Serial Port Error Received: " + sender + " " + e);
        }

        private void _serialPort_Disposed(object sender, EventArgs e)
        {
            Console.WriteLine("Serial Port Disposed: " + sender + " " + e);
        }

        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Console.WriteLine("Serial Port Data Received: " + sender + " " + e);

            Console.WriteLine(_serialPort.ReadExisting());
        }


        //////////////////////////////////////////////////////////////////////        INITIALIZE UI           //////////////////////////////////////////////////////////////////////

        void InitializeUI()
        {
            ListenToUIEvents();

            InitializeComPorts();

            InitializeBaudRateComboBox();

            InitializeParityComboBox();

            InitializeDataBitsComboBox();

            InitializeStopBitsComboBox();
        }

        void ListenToUIEvents()
        {
            ComPortComboBox.SelectionChanged += ComPortComboBox_SelectionChanged;

            BaudRateComboBox.SelectionChanged += BaudRateComboBox_SelectionChanged;

            ParityComboBox.SelectionChanged += ParityComboBox_SelectionChanged;

            DataBitsComboBox.SelectionChanged += DataBitsComboBox_SelectionChanged;

            StopBitsComboBox.SelectionChanged += StopBitsComboBox_SelectionChanged;
        }

        void InitializeComPorts()
        {
            _portNames = SerialPort.GetPortNames();

            if (_portNames == null)
            {
                DebugLog("ERROR: No Com Ports found.");
                return;
            }

            DebugLog("Com Ports Found: ");

            foreach (var port in _portNames)
            {
                DebugLog("Found: " + port);
                ComPortComboBox.Items.Add(port);
            }

            ComPortComboBox.SelectedIndex = 0;
        }

        void InitializeBaudRateComboBox()
        {
            BaudRateComboBox.Items.Add(2400);
            BaudRateComboBox.Items.Add(4800);
            BaudRateComboBox.Items.Add(9600);
            BaudRateComboBox.Items.Add(19200);
            BaudRateComboBox.Items.Add(38400);

            BaudRateComboBox.SelectedIndex = 2;
        }

        void InitializeParityComboBox()
        {
            ParityComboBox.Items.Add(Parity.None);
            ParityComboBox.Items.Add(Parity.Odd);
            ParityComboBox.Items.Add(Parity.Even);
            ParityComboBox.Items.Add(Parity.Mark);
            ParityComboBox.Items.Add(Parity.Space);

            ParityComboBox.SelectedIndex = 0;
        }

        void InitializeDataBitsComboBox()
        {
            DataBitsComboBox.Items.Add(7);
            DataBitsComboBox.Items.Add(8);

            DataBitsComboBox.SelectedIndex = 1;

        }

        void InitializeStopBitsComboBox()
        {
            StopBitsComboBox.Items.Add(StopBits.One);
            StopBitsComboBox.Items.Add(StopBits.Two);

            StopBitsComboBox.SelectedIndex = 0;
        }

        //////////////////////////////////////////////////////////////////////        DEBUGGING          //////////////////////////////////////////////////////////////////////

        void DebugLog(string text)
        {
            DebuggingTextBox.Text += text + System.Environment.NewLine;
        }

        //////////////////////////////////////////////////////////////////////        REFRESH UI         //////////////////////////////////////////////////////////////////////

        void RefreshConnectButton()
        {
            if (_serialPort.IsOpen)
                ConnectButton.IsEnabled = false;
            else
                ConnectButton.IsEnabled = true;

        }

        //////////////////////////////////////////////////////////////////////        UI EVENTS          //////////////////////////////////////////////////////////////////////


        private void ComPortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _serialPort.PortName = ComPortComboBox.Items[ComPortComboBox.SelectedIndex].ToString();

            DebugLog("Serial Port Changed to: " + _serialPort.PortName);
        }

        private void BaudRateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int baudRate = 0;

            int.TryParse(BaudRateComboBox.Items[BaudRateComboBox.SelectedIndex].ToString(), out baudRate);

            _serialPort.BaudRate = baudRate;

            DebugLog("Baud Rate Changed to: " + _serialPort.BaudRate);
        }

        private void ParityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Parity parity = Parity.None;

            Enum.TryParse(ParityComboBox.Items[ParityComboBox.SelectedIndex].ToString(), out parity);

            _serialPort.Parity = parity;

            DebugLog("Parity Bits Changed to: " + _serialPort.Parity);
        }

        private void StopBitsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StopBits stopBits = StopBits.One;

            Enum.TryParse(StopBitsComboBox.Items[StopBitsComboBox.SelectedIndex].ToString(), out stopBits);

            _serialPort.StopBits = stopBits;

            DebugLog("Stop Bits Changed to: " + _serialPort.StopBits);

        }

        private void DataBitsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int dataBits = 0;

            int.TryParse(DataBitsComboBox.Items[DataBitsComboBox.SelectedIndex].ToString(), out dataBits);

            _serialPort.DataBits = dataBits;

            DebugLog("Data Bits Changed to: " + _serialPort.DataBits);
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            Connect();
        }

        //////////////////////////////////////////////////////////////////////        Serial Port Logic         //////////////////////////////////////////////////////////////////////

        void Connect()
        {
            if(_serialPort.IsOpen == false)
                _serialPort.Open();

            string command = "20050026:";

            _serialPort.WriteLine(command);

            RefreshConnectButton();
        }


    }
}
