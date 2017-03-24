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
using System.Collections.ObjectModel;


namespace WSProj
{
   

    public partial class MainWindow : Window
    {

        string[] _portNames;

        SerialPort _serialPort = new SerialPort();

        Communicator _communicator;
        Thread _communicatorThread;

        ObservableCollection<WeighingRecord> _records = new ObservableCollection<WeighingRecord>();

        float _currentWeight;
        string _tempSerialNumber;
        float _tempStartingWeight;
        float _tempEndingWeight;

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

            _communicator.WeightRecorded += OnWeightRecordReceived;

            ThreadStart threadStart = new ThreadStart(_communicator.Update);

            _communicatorThread = new Thread(threadStart);

            //If we don't set IsBackground to true, it doesn't end the thread when we close the window.
            _communicatorThread.IsBackground = true;

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

            UpdateRecordPreviewTextBox();

            WeightRecordDataGrid.ItemsSource = _records;

            HideAdvancedConnectionOptions();

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

        void RefreshConnectionButtons()
        {
            if (_serialPort.IsOpen)
            {
                ConnectButton.IsEnabled = false;
                DisconnectButton.IsEnabled = true;
            }
            else
            {
                ConnectButton.IsEnabled = true;
                DisconnectButton.IsEnabled = false;
            }

        }

        void SetWeight(float weight, bool isPounds)
        {
            WeightTextBox.Text = weight.ToString("0.00") + (isPounds ? " LB" : " KG");
        }

        void UpdateRecordPreviewTextBox()
        {
            RecordPreviewTextBox.Text = "SN: " +  _tempSerialNumber + "   Starting Weight: [" + _tempStartingWeight + "]    Ending Weight: [" + _tempEndingWeight + "]";
        }

        void HideAdvancedConnectionOptions()
        {
            DataBitsComboBox.Visibility = Visibility.Hidden;
            DataBitsLabel.Visibility = Visibility.Hidden;

            BaudRateComboBox.Visibility = Visibility.Hidden;
            BaudRateLabel.Visibility = Visibility.Hidden;

            ParityComboBox.Visibility = Visibility.Hidden;
            ParityLabel.Visibility = Visibility.Hidden;

            StopBitsComboBox.Visibility = Visibility.Hidden;
            StopBitsLabel.Visibility = Visibility.Hidden;
        }

        void ShowAdvancedConnectionOptions()
        {
            DataBitsComboBox.Visibility = Visibility.Visible;
            DataBitsLabel.Visibility = Visibility.Visible;

            BaudRateComboBox.Visibility = Visibility.Visible;
            BaudRateLabel.Visibility = Visibility.Visible;

            ParityComboBox.Visibility = Visibility.Visible;
            ParityLabel.Visibility = Visibility.Visible;

            StopBitsComboBox.Visibility = Visibility.Visible;
            StopBitsLabel.Visibility = Visibility.Visible;
        }

        //////////////////////////////////////////////////////////////////////        UI EVENTS          //////////////////////////////////////////////////////////////////////


        private void AutoWeighCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            lock (_communicator)
            {
                _communicator.SetAutoWeigh(false);
            }
        }

        private void AutoWeighCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            lock (_communicator)
            {
                _communicator.SetAutoWeigh(true);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DebuggingTextBox.ScrollToEnd();
        }

        private void SerialNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _tempSerialNumber = SerialNumberTextBox.Text;

            UpdateRecordPreviewTextBox();
        }

        private void AddRecordButton_Click(object sender, RoutedEventArgs e)
        {
            _records.Add(new WeighingRecord(_tempSerialNumber, _tempStartingWeight, _tempEndingWeight));

            WeightRecordDataGrid.Items.Refresh();

            _tempSerialNumber = "";
            _tempStartingWeight = 0;
            _tempEndingWeight = 0;

            SerialNumberTextBox.Text = "";

            UpdateRecordPreviewTextBox();
        }

        private void RemoveRecordButton_Click(object sender, RoutedEventArgs e)
        {
            List<WeighingRecord> weighingRecordsToRemove = new List<WeighingRecord>();

            foreach (var item in WeightRecordDataGrid.SelectedItems)
                weighingRecordsToRemove.Add(item as WeighingRecord);

            foreach (var weighingRecord in weighingRecordsToRemove)
            {
                _records.Remove(weighingRecord);
            }
        }

        private void StartingWeightButton_Click(object sender, RoutedEventArgs e)
        {
            _tempStartingWeight = _currentWeight;

            UpdateRecordPreviewTextBox();
        }

        private void EndingWeightButton_Click(object sender, RoutedEventArgs e)
        {
            _tempEndingWeight = _currentWeight;

            UpdateRecordPreviewTextBox();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_communicator != null)
                _communicator.Stop();
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

        private void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            Disconnect();
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AdvancedCommunicationCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            ShowAdvancedConnectionOptions();
        }

        private void AdvancedCommunicationCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            HideAdvancedConnectionOptions();
        }

        //////////////////////////////////////////////////////////////////////        Serial Port Logic         //////////////////////////////////////////////////////////////////////

        void Connect()
        {
            if (_serialPort.IsOpen == false)
            {
                _serialPort.Open();
                Debug.Log("Connected");
            }

            RefreshConnectionButtons();
        }

        void Disconnect()
        {
            if(_serialPort.IsOpen == true)
            {
                _serialPort.Close();
                Debug.Log("Disconnected");
            }

            RefreshConnectionButtons();
        }

        //////////////////////////////////////////////////////////////////////        COMMUNICATOR EVENTS        //////////////////////////////////////////////////////////////////////

        void OnWeightRecordReceived(float weight, bool isPounds)
        {
            _currentWeight = weight;

            SetWeight(weight, isPounds);
        }

        //////////////////////////////////////////////////////////////////////         MISC         //////////////////////////////////////////////////////////////////////

        void Test()
        {
           // DebuggingTextBox.ScrollToEnd();
            // _records.Add(new WeighingRecord("G34A43001", 50, 100));
            // _records.Add(new WeighingRecord("C45345CX", 20, 10));

            _communicator.SendData("20", CommandType.ReadLiteralValue, RegisterType.GrossWeight, "");

            //_communicator.SendData("20", CommandType.WriteFinalValue, RegisterType.SetpointSource, "1F4");

            //SetWeight(100, false);
        }

    }
}
