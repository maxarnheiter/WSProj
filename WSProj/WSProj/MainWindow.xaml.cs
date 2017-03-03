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
using System.Threading;


namespace WSProj
{
   

    public partial class MainWindow : Window
    {

        string[] _portNames;

        SerialPort _serialPort = new SerialPort();

        Communicator _communicator;
        Thread _communicatorThread;

        public MainWindow()
        {
            InitializeComponent();

            Debug.TextBox = DebuggingTextBox;

            InitializeUI();

            InitializeCommunicator();
        }

        void InitializeCommunicator()
        {
            _communicator = new Communicator(_serialPort);

            ThreadStart threadStart = new ThreadStart(_communicator.Update);

            _communicatorThread = new Thread(threadStart);

            _communicatorThread.Start();
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

            Debug.Log("UI initialized");
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
                Debug.Log("ERROR: No Com Ports found");
                return;
            }

            Debug.Log("Com Ports Found: ");

            foreach (var port in _portNames)
            {
                Debug.Log("Found: " + port);
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

        //////////////////////////////////////////////////////////////////////        REFRESH UI         //////////////////////////////////////////////////////////////////////

        void RefreshConnectButton()
        {
            if (_serialPort.IsOpen)
                ConnectButton.IsEnabled = false;
            else
                ConnectButton.IsEnabled = true;

        }

        void SetWeight(float weight, bool isPounds)
        {
            WeightTextBox.Text = weight.ToString("0.00") + (isPounds ? " LB" : " KG");
        }

        //////////////////////////////////////////////////////////////////////        UI EVENTS          //////////////////////////////////////////////////////////////////////


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_communicator != null)
                _communicator.End();
        }

        private void ComPortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _serialPort.PortName = ComPortComboBox.Items[ComPortComboBox.SelectedIndex].ToString();

            Debug.Log("Serial Port Changed to: " + _serialPort.PortName);
        }

        private void BaudRateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int baudRate = 0;

            int.TryParse(BaudRateComboBox.Items[BaudRateComboBox.SelectedIndex].ToString(), out baudRate);

            _serialPort.BaudRate = baudRate;

            Debug.Log("Baud Rate Changed to: " + _serialPort.BaudRate);
        }

        private void ParityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Parity parity = Parity.None;

            Enum.TryParse(ParityComboBox.Items[ParityComboBox.SelectedIndex].ToString(), out parity);

            _serialPort.Parity = parity;

            Debug.Log("Parity Bits Changed to: " + _serialPort.Parity);
        }

        private void StopBitsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StopBits stopBits = StopBits.One;

            Enum.TryParse(StopBitsComboBox.Items[StopBitsComboBox.SelectedIndex].ToString(), out stopBits);

            _serialPort.StopBits = stopBits;

            Debug.Log("Stop Bits Changed to: " + _serialPort.StopBits);

        }

        private void DataBitsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int dataBits = 0;

            int.TryParse(DataBitsComboBox.Items[DataBitsComboBox.SelectedIndex].ToString(), out dataBits);

            _serialPort.DataBits = dataBits;

            Debug.Log("Data Bits Changed to: " + _serialPort.DataBits);
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            Test();
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            Connect();
        }

        //////////////////////////////////////////////////////////////////////        Serial Port Logic         //////////////////////////////////////////////////////////////////////

        void Connect()
        {
            if (_serialPort.IsOpen == false)
            {
                _serialPort.Open();
                Debug.Log("Connected");
            }

            RefreshConnectButton();
        }

        void Test()
        {

            _communicator.SendData("20", CommandType.ReadLiteralValue, RegisterType.GrossWeight, "");

            //_communicator.SendData("20", CommandType.WriteFinalValue, RegisterType.SetpointSource, "1F4");

            //SetWeight(100, false);
        }

    }
}
