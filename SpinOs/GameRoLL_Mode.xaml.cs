using System.Collections.Generic;
using System;
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
using System.Threading;
using System.Windows.Forms;
using System.IO;
using SpinOs.Data;
using static SpinOs.Data.GameRoLL_InitData;


namespace SpinOs
{
    public partial class GameRoLL_Mode : Window
    {
        public List<TextBlock> slots = new List<TextBlock>(); // список текстблоков слотов

        public GameRoLL_Mode()
        {
            InitializeComponent();
            InitListsFromData();
        }

        public void InitListsFromData() //загрузка из файлов и инициализация данных
        {
            GameRoLL_InitData.InitDataFiles();

            slots = slots_init(roll_slot_1, roll_slot_2, roll_slot_3);
            rule1.Text = rules[0];
            rule2.Text = rules[1];
            rule3.Text = rules[2];
        }

        private int detect_difficulty() //определяет выставленную сложность из выпадающего списка
        {
            int indexer = 0;
            foreach (Difficulty d in diff_listnew)
            {
                if (d.Name == complexity_menu.Text)
                {
                    break;
                }
                else
                {
                    indexer++;
                }
            }
            return indexer;
        }

        private async void roll_fun(object sender, RoutedEventArgs e) //собственно, сам процесс ролла
        {
            if (complexity_menu.Text != "")
            {
                int indexer = detect_difficulty(); // получение индекса нужных элементов
                CalcRoLL cl = new CalcRoLL();

                await Task.Run(() => cl.modeRoLL(slots, indexer)); //ролл мода
                await Task.Delay(cl.laps * cl.mstime_lap + 2 * cl.mstime_lap); //ожидание потока// кол-во кругов * времся круга + на всякий случай время двух кругов
                await Task.Run(() => cl.resultType(slots, indexer)); //ролл типа результата
                await Task.Delay(cl.laps * cl.mstime_lap + 2 * cl.mstime_lap); // -//-

                if (ISPercentage.IsChecked.Value) //проверка на галочку процентов
                {
                    await Task.Run(() => cl.percantage(slots, indexer));
                }
                else
                {
                    slots[2].Text = "0 - 100 %";
                }
            }
            else
            {
                alertDiffChoose();
            }
        }

        private void full_random_button_fun(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            int index_diff = rnd.Next(0, diff_listnew.Count);
            complexity_menu.Text = diff_listnew[index_diff].Name;
            roll_fun(null, null);
        }

        private void fc_button_fun(object sender, RoutedEventArgs e)
        {
            if (complexity_menu.Text != "")
            {
                calc_result_score();
                switch (slots[1].Text)
                {
                    case "FC": break;
                    case "Pass": MainWindow.additionGameRoLL *= 1.25; break;
                    default: MainWindow.additionGameRoLL = 0; break;
                }
                Console.WriteLine(MainWindow.additionGameRoLL);
                Close();
            }
            else
            {
                alertDiffChoose();
            }
        }

        private void pass_button_fun(object sender, RoutedEventArgs e)
        {
            if (complexity_menu.Text != "")
            {
                calc_result_score();
                switch (slots[1].Text)
                {
                    case "FC": MainWindow.additionGameRoLL *= 0.25; break;
                    case "Pass": break;
                    default: MainWindow.additionGameRoLL = 0; break;
                }

                Close();
            }
            else
            {
                alertDiffChoose();
            }
        }

        private void fail_button_fun(object sender, RoutedEventArgs e)
        {
            if (complexity_menu.Text != "")
            {
                MainWindow.additionGameRoLL = 0;
                Close();
            }
            else
            {
                alertDiffChoose();
            }
        }

        private void calc_result_score()
        {
            if (complexity_menu.Text == diff_listnew[0].Name)
            {
                MainWindow.additionGameRoLL = 4;
            }
            else if (complexity_menu.Text == diff_listnew[1].Name)
            {
                MainWindow.additionGameRoLL = 6;
            }
            else if (complexity_menu.Text == diff_listnew[2].Name)
            {
                MainWindow.additionGameRoLL = 8;
            }
            else { MainWindow.additionGameRoLL = 12; }
        }

        private async void alertDiffChoose()
        {
            Game_RoLL_infobar.Text = " Выберите сложность ! ";
            Game_RoLL_infobar.Background = Brushes.Red;
            await Task.Delay(100);
            Game_RoLL_infobar.Background = Brushes.PapayaWhip;
        }


    }
}