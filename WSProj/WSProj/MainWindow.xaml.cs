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
        }

        //////////////////////////////////////////////////////////////////////        INITIALIZE UI           //////////////////////////////////////////////////////////////////////

        void InitializeUI()
        {
            InitializeComPorts();

            InitializeBaudRateComboBox();

            InitializeParityComboBox();



            ListenToUIEvents();
        }

        void ListenToUIEvents()
        {
            ComPortComboBox.SelectionChanged += ComPortComboBox_SelectionChanged;

            BaudRateComboBox.SelectionChanged += BaudRateComboBox_SelectionChanged;

            ParityComboBox.SelectionChanged += ParityComboBox_SelectionChanged;
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

            BaudRateComboBox.SelectedIndex = 0;
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

        void DebugLog(string text)
        {
            DebuggingTextBox.Text += text + System.Environment.NewLine;
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
    }
}
