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
using System.Windows.Shapes;

namespace SpinOs
{
    /// <summary>
    /// Interaction logic for StartAlert.xaml
    /// </summary>
    public partial class StartAlert : Window
    {
        public StartAlert()
        {
            InitializeComponent();
        }

        private void yes_startalert_fun(object sender, RoutedEventArgs e)
        {
            result("yes");
        }

        private void no_startalert_fun(object sender, RoutedEventArgs e)
        {
            result("no");
        }

        private void result(string res)
        {
            switch (res)
            {
                case "yes": MainWindow.resultStartAlert = true; break;
                case "no": break;
            }
            Close();
        }
    }
}
