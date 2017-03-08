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

namespace WSProj
{
    public static class Debug
    {

        public static TextBox TextBox;

        public static void Log(string text)
        {
            TextBox.AppendText(System.Environment.NewLine + text);
            TextBox.ScrollToEnd();
        }

        public static void LogAsync(string text)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                TextBox.AppendText(System.Environment.NewLine + text);
                TextBox.ScrollToEnd();
            }));
        }
    }
}
