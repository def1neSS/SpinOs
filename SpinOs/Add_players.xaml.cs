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
using System.IO;

namespace SpinOs
{

    public partial class Add_players : Window
    {
        string playerspath = @"..\..\Data\players.txt"; // путь брать из главного окна !
        public Add_players()
        {
            InitializeComponent();
        }

        private void add_players_button_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(playerspath,true))
            {
                sw.Write(add_players_box.Text);
                sw.Write("\n");
            }
            Close();
        }
    }
}
