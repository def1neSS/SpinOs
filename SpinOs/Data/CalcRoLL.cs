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
using System.Windows.Threading;
using static SpinOs.Data.GameRoLL_InitData;

namespace SpinOs.Data
{
    class CalcRoLL
    {
        public int mstime_lap = 50; //milliseconds
        public int laps = 10;

        public void modeRoLL(List<TextBlock> slots, int indexer)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                int d = 0;
                Random rnd = new Random();
                int per = rnd.Next(0, 100);
                List<int> rangediff = new List<int>();

                for(int i = 0; i < mods.Count; i++) //делает список сумм 1 12 123 1234 12345
                {
                    rangediff.Add(diff_listnew[indexer].Rating_complexity[i]);
                    if(i!=0) rangediff[i] += rangediff[i-1];
                    Console.WriteLine(rangediff[i]);
                }

                
                foreach(int s in rangediff) //вычисление выпавшего значения
                {
                    if (per < s)
                    {
                        break;
                    }
                    else
                    {
                        d++;
                    }
                }
                Console.WriteLine(d);
                Console.WriteLine(per);
                waiting(slots, 0, mods, d);
            });

        }

        public void resultType(List<TextBlock> slots, int indexer)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                int index_of_result_type_list;
                Random rnd = new Random();
                int per = rnd.Next(0, 100);

                if (per > 80) { index_of_result_type_list = 0; } // 20 %  -  FC
                else { index_of_result_type_list = 1; }         // 80 %  -  PASS

                waiting(slots, 1, result_type, index_of_result_type_list);
            });
        }

        public void percantage(List<TextBlock> slots, int indexer)
        {
            string temp_text;
            Random rnd = new Random();
            int temp_num = 0;
            
            temp_text = temp_num.ToString() + " - " + (temp_num + diff_listnew[indexer].Percentage_complexity).ToString() + " %";

            //waiting(slots, 2, temp_text, null);
        }

        public async void waiting(List<TextBlock> slots, int index, List<string> list, int finalize_value)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() => { slots[index].Background = Brushes.Blue; });

            Random rnd = new Random();
            int rnd_number;
            for (int i = 0; i < laps; i++)
            {
                rnd_number = rnd.Next(0, list.Count);
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    slots[index].Text = list[i%list.Count];
                });
                await Task.Delay(mstime_lap);
            }
            rnd_number = rnd.Next(0, list.Count);
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                slots[index].Text = list[finalize_value];
                slots[index].Background = Brushes.CornflowerBlue;
            });

        }
    }
}
