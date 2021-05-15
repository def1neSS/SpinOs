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
using static SpinOs.Data.GameRoLL_InitData;

namespace SpinOs.Data
{
    class CalcRoLL
    {
        public void modeRoLL(List<TextBlock> slots, int indexer)
        {
            string temp_text = "";
            Random rnd = new Random();
            int per = rnd.Next(0, 100);
            List<int> rangediff = new List<int>();
            Console.WriteLine(per);
            for(int i = 0; i < mods.Count; i++) //делает список сумм 1 12 123 1234 12345
            {
                rangediff.Add(diff_listnew[indexer].Rating_complexity[i]);
                //Console.WriteLine(rangediff[i]);
                if(i!=0) rangediff[i] += rangediff[i-1];
            }

            int d = 0;
            foreach(int s in rangediff)
            {
                if (per < s)
                {
                    temp_text = mods[d];
                }
                else
                {
                    d++;
                }
            }
            slots[0].Text = temp_text;
            //waiting(slots, 0, temp_text);
        }

        public void resultType(List<TextBlock> slots, int indexer)
        {
            int per;
            string temp_text;
            Random rnd = new Random();
            slots[1].Background = Brushes.Blue;
            
            per = rnd.Next(0, 100);
            if (per > 80) { /*slots[1].Text*/ temp_text = result_type[0].ToString(); } // 20 %  -  FC
            else { /*slots[1].Text*/temp_text = result_type[1].ToString(); }         // 80 %  -  PASS
            slots[1].Background = Brushes.CornflowerBlue;

            waiting(slots, 1, temp_text);
        }

        public void percantage(List<TextBlock> slots, int indexer)
        {
            string temp_text;
            Random rnd = new Random();
            slots[2].Background = Brushes.Blue;
            int temp_num = 0;
            
            /*slots[2].Text*/ temp_text = temp_num.ToString() + " - " + (temp_num + diff_listnew[indexer].Percentage_complexity).ToString() + " %";
            slots[2].Background = Brushes.CornflowerBlue;

            waiting(slots, 2, temp_text);
        }

        public void waiting(List<TextBlock> slots, int index, string value)
        {
            for (int i = 0; i < 10; i++)
            {
                slots[index].Text = "O";
                Task.Delay(50);
                slots[index].Text = "o'";
            }
            slots[index].Text = value;
        }
    }
}
