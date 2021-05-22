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
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;

namespace SpinOs
{
    /// <summary>
    /// Interaction logic for funRoLL_Mode.xaml
    /// </summary>
    public partial class funRoLL_Mode : Window
    {
        //можно и один несколько мерный лист создать, но пока не буду
        public string challenges_path = @"..\..\data\funRoLL\funRoLL_challenges.txt";
        public string challenges_true_fun_path = @"..\..\data\funRoLL\funRoLL_truefun_challenges.txt";
        public string challenges_additional_first = @"..\..\data\funRoLL\funRoLL_additional_first.txt";
        public string challenges_additional_second = @"..\..\data\funRoLL\funRoLL_additional_second.txt";
        public List<string> challenges_list = new List<string>();
        public List<string> challenges_true_fun_list = new List<string>();
        public List<string> challenges_additional_first_list = new List<string>();
        public List<string> challenges_additional_second_list = new List<string>();

        public int roll_laps = 2;
        public int time_await = 60;

        public funRoLL_Mode()
        {
            InitializeComponent();
            InitChallenges();
        }

        private async void roll_button_fun(object sender, RoutedEventArgs e)
        {
            var rnd = new Random();
            bool flag_on_true_fun = false;
            if (rnd.Next(0, 2) == 0)
            {
                await Task.Run(() => rolling(challenges_list, rolling_window_tb));
            }
            else
            {
                await Task.Run(() => rolling(challenges_true_fun_list, rolling_window_tb));
                flag_on_true_fun = true;
            }

            if (!flag_on_true_fun)
            {
                await Task.Delay(time_await * roll_laps * challenges_list.Count + 20);
                await Task.Run(() => rolling(challenges_additional_first_list, rolling_additional_first));
                await Task.Delay(time_await * roll_laps * challenges_additional_first_list.Count + 20);
                await Task.Run(() => rolling(challenges_additional_second_list, rolling_additional_second));
            }
            else
            {
                rolling_additional_first.Text = "И так хватит";
                rolling_additional_second.Text = "Удачи";
            }

        }

        public async void rolling(List<string> list, TextBlock tb)
        {
            for (int i = 0; i < roll_laps ; i++)
            {
                foreach (string s in list)
                {
                    Dispatcher.Invoke(()=> { 
                        tb.Text = s; 
                    });
                    await Task.Delay(time_await);
                    
                }
            }
            Random rnd = new Random();
            int rnd_number = rnd.Next(0,list.Count);
            Dispatcher.Invoke(() =>
            {
                tb.Text = list[rnd_number];
            });
        }

        public void InitChallenges()
        {
            string line;
            using (StreamReader sr = new StreamReader(challenges_path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    challenges_list.Add(line);
                }
            }
            using (StreamReader sr = new StreamReader(challenges_true_fun_path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    challenges_true_fun_list.Add(line);
                }
            }
            using (StreamReader sr = new StreamReader(challenges_additional_first))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    challenges_additional_first_list.Add(line);
                }
            }
            using (StreamReader sr = new StreamReader(challenges_additional_second))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    challenges_additional_second_list.Add(line);
                }
            }
        }

        private void submit_buton(object sender, RoutedEventArgs e)
        {
            if (rolling_window_tb.Text != "")
            {
                if (check_condition_1.IsChecked.Value) { MainWindow.additionGameRoLL += 8; }
                if (check_condition_2.IsChecked.Value) { MainWindow.additionGameRoLL += 4; }
                if (check_condition_3.IsChecked.Value) { MainWindow.additionGameRoLL += 5; }
                Close();
            }
            else
            {
                alertDiffChoose();
            }

        }
        private async void alertDiffChoose()
        {
            Fun_RoLL_infobar.Text = " Заролльте ! ";
            Fun_RoLL_infobar.Background = Brushes.Red;
            await Task.Delay(100);
            Fun_RoLL_infobar.Background = Brushes.PapayaWhip;
        }
    }
}
