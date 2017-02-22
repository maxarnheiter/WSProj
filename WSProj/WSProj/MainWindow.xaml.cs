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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string[] _portNames;


        public MainWindow()
        {
            InitializeComponent();

            InitializeComPorts();
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


        void DebugLog(string text)
        {
            DebuggingTextBox.Text += text + System.Environment.NewLine;
        }
    }
}
