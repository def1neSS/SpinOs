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
using System.Threading;
using System.Windows.Forms;
using System.IO;
using SpinOs.Data;
using static SpinOs.Data.InitData; // для InitDataFiles();

namespace SpinOs
{

    public partial class MainWindow : Window
    {
        public string playerspath = @"..\..\Data\players.txt"; //txt файл игроков
        public int indexer; // нужна для индексации списка игроков
        List<Player> players_list = new List<Player>(); //список игроков
        bool startflag = false; // для определения окончательной загрузки параметров
        public static double additionGameRoLL; // Переменная содержащая полученное кол-во очков с геймролла, к ней обращаются другие файлы // Потом исправлю код и уберу её
        public static bool resultStartAlert; // нужна для подтверждения новой игры
        public static int lap = 1; //круг игры

        public MainWindow()
        {
            InitializeComponent();
            SpinOsMainBackground.ImageSource = new BitmapImage(new Uri("../../Data/wall_main.jpg", UriKind.Relative)); //фоновая картинка;
            InitDataFiles();
            indexer = 0;
        }

        private void add_players_button(object sender, RoutedEventArgs e) // Функция добавления игроков
        {
            Add_players AP = new Add_players();
            AP.Owner = this;
            
            AP.Show();
            UpdateTables();
        }

        private void load_players_button(object sender, RoutedEventArgs e) // Функция загрузки сохраненных игроков из файла // По сути только ссылается на другую функцию // Потом исправлю, мхе
        {
            UpdateTables();
        }

        private void clear_players_button(object sender, RoutedEventArgs e) //Функция удаления списка игроков из таблицы и файла
        {
            using (StreamWriter sw = new StreamWriter(playerspath, false))
            {
                sw.Write("");
            }
            table_players.Items.Clear();
        }

        public void UpdateTables() // Функция обновления таблицы из файла
        {
            table_players.Items.Clear();
            players_list.Clear();
            string line;
            using (StreamReader sr = new StreamReader(playerspath,true))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    table_players.Items.Add( new Player { Name = line, Score = 0 } );

                    players_list.Add(new Player { Name = line, Score = 0 } );
                }
            }
        }

        private async void GameRoLL(object sender, RoutedEventArgs e) // Функция запуска окна с геймроллом
        {
            if(players_list.Count > 0)
            {
                GameRoLL gr = new GameRoLL();
                gr.Owner = this;
                gr.ShowDialog();
                if (additionGameRoLL == 0)
                {
                    down_panel.Text = "Игрок " + players_list[indexer % (players_list.Count)].Name + " не получает очков ";
                }
                else
                {
                    string temp_score;
                    switch (additionGameRoLL)
                    {
                        case 1.0: temp_score = "очко"; break;
                        default: temp_score = "очка"; break;
                    }
                    down_panel.Text = "Игрок " + players_list[indexer % (players_list.Count)].Name + " получает " + additionGameRoLL + " " + temp_score;
                }
                players_list[indexer % (players_list.Count)].Score += additionGameRoLL; //присваивание очков игроку //мод здесь для "кольцевой очереди" или типо того, нз как это называется 
                indexer++;
                current_player.Text = players_list[indexer % (players_list.Count)].Name; //Переход к след.игроку // тоже мод
                update_table(); // обновление таблицы после присваивания очков

                additionGameRoLL = 0;
            }
            else 
            {
                down_panel.Text = " Добавьте игроков! ";
                down_panel.Background = Brushes.Red;
                await Task.Delay(100);
                down_panel.Background = Brushes.LightGray;
            }
            
        }

        public void scoring()
        {
            foreach(Player pl in table_players.Items)
            {
                Console.WriteLine(pl.Name);
                Console.WriteLine(pl.Score);
            }
        }

        private async void Start(object sender, RoutedEventArgs e) //Загрузка игроков, таблицы и еще пары параметров
        {
            if (table_players.Items.Count==0)
            {
                lap_game.Text = "1";
                resultStartAlert = false; //для проверки новой игры
                startflag = true;
                indexer = 0;
                UpdateTables();
                if (players_list.Count > 0)
                {
                    current_player.Text = players_list[indexer % (players_list.Count)].Name;
                }
                else
                {
                    down_panel.Text = " Добавьте игроков! ";
                    down_panel.Background = Brushes.Red;
                    await Task.Delay(100);
                    down_panel.Background = Brushes.LightGray;
                }
            }
            else
            {
                
                StartAlert sa = new StartAlert();
                sa.Owner = this;
                sa.ShowDialog();
                if(resultStartAlert) table_players.Items.Clear();
            }
        }

        public void update_table()
        {
            CompareForSort cfs = new CompareForSort();
            players_list.Sort(cfs);

            table_players.Items.Clear();
            foreach (Player pl in players_list)
            {
                table_players.Items.Add(pl);
            }
            if (indexer % players_list.Count == 0) update_lap();
        }
                                                                                //ПОД ОДНУ ФУНКЦИЮ!!!!!!!!! // Тоже потом исправлю
                                                                                //все таски с делеями нужны для визуальной составляющей, но пока беды с асинхроном, потоками и тп
                                                                                //некоторые задачи выполняются до других и искажают данные 
        private void plus0fun(object sender, RoutedEventArgs e)
        {
            if (startflag)
            {
                down_panel.Text = "Игрок " + players_list[indexer % (players_list.Count)].Name + " получает 0 очков ";
                indexer++;
                current_player.Text = players_list[indexer % (players_list.Count)].Name;
            }
            else
            {
                down_panel.Text = " Нажмите кнопку Start ";
                /*
                down_panel.Background = Brushes.Red;
                await Task.Delay(100);
                down_panel.Background = Brushes.LightGray;
                */
            }
        }

        private void plus05fun(object sender, RoutedEventArgs e)
        {
            if (startflag)
            {
                down_panel.Text = "Игрок " + players_list[indexer % (players_list.Count)].Name + " получает 0.5 очков ";
                players_list[indexer % (players_list.Count)].Score += 0.5;
                indexer++;
                current_player.Text = players_list[indexer % (players_list.Count)].Name;
                update_table();
            }
            else
            {
                down_panel.Text = " Нажмите кнопку Start ";
                /*
                down_panel.Background = Brushes.Red;
                await Task.Delay(100);
                down_panel.Background = Brushes.LightGray;
                */
            }
        }

        private void plus1fun(object sender, RoutedEventArgs e)
        {
            if (startflag)
            {
                down_panel.Text = "Игрок " + players_list[indexer % (players_list.Count)].Name + " получает 1 очко ";
                players_list[indexer % (players_list.Count)].Score += 1;
                indexer++;
                current_player.Text = players_list[indexer % (players_list.Count)].Name;
                update_table();
            }
            else
            {
                down_panel.Text = " Нажмите кнопку Start ";
                /*
                down_panel.Background = Brushes.Red;
                await Task.Delay(100);
                down_panel.Background = Brushes.LightGray;
                */
            }
        }

        public void update_lap()
        {
            lap++;
            lap_game.Text = lap.ToString();
        }
        private void plus()
        {

        }

    }
}
