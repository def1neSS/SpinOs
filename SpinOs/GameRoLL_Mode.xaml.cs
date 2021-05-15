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
        public List<TextBlock> info_tables = new List<TextBlock>(); // список текстблоков слотов

        public GameRoLL_Mode()
        {

            InitializeComponent();

            GameRoLLBackground.ImageSource = new BitmapImage(new Uri("../../Data/wall.png", UriKind.Relative)); //фоновая картинка
            InitListsFromData();
        }

        public void InitListsFromData() //загрузка из файлов и инициализация данных
        {
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

        private void roll_fun(object sender, RoutedEventArgs e) //собственно, сам процесс ролла
        {
            if (complexity_menu.Text != "")
            {
                int indexer = detect_difficulty();
                CalcRoLL cl = new CalcRoLL();

                cl.modeRoLL(slots, indexer);
                cl.resultType(slots, indexer);
                if (ISPercentage.IsChecked.Value)
                {
                    cl.percantage(slots, indexer);
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
        //читаются данные с контрола, а нужно из списка результатов
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
                Console.WriteLine(slots[1].Text);
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
                MainWindow.additionGameRoLL = 1;
            }
            else if (complexity_menu.Text == diff_listnew[1].Name)
            {
                MainWindow.additionGameRoLL = 1.5;
            }
            else if (complexity_menu.Text == diff_listnew[2].Name)
            {
                MainWindow.additionGameRoLL = 2;
            }
            else { MainWindow.additionGameRoLL = 3; }
        }

        private async void alertDiffChoose()
        {
            ProgLog.Text = " Выберите сложность ! ";
            ProgLog.Background = Brushes.Red;
            await Task.Delay(100);
            ProgLog.Background = Brushes.PapayaWhip;
        }


    }
}