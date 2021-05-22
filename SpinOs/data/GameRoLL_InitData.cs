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
using SpinOs;


namespace SpinOs.Data
{
    public static class GameRoLL_InitData
    {

        public static List<Difficulty> diff_listnew = new List<Difficulty>(); // список классов сложностей
        public static List<string> mods = new List<string>(); // список модов, которые будут прокручены в слоте
        public static List<string> result_type = new List<string>(); // список результатов ( FC , Pass , etc. )
        public static List<string> rules = new List<string>() { "","","" };

        //static string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        static string modspath = @"..\..\data\mods.txt";
        //static string diffpath = @"..\..\data\difficulty.txt";
        static string diffnewpath = @"..\..\data\difficulties.txt";
        static string rulepath = @"..\..\data\rule.txt";

        public static List<TextBlock> slots_init(TextBlock x1, TextBlock x2, TextBlock x3)
        {
            List<TextBlock> slots = new List<TextBlock>();
            slots.Add(x1);
            slots.Add(x2);
            slots.Add(x3);

            return slots;
        }

        public static void InitDataFiles()
        {
            diff_listnew.Clear();
            mods.Clear();
            result_type.Clear();

            // ИнфоСписок
            if (rules[0] == "")
            {
                try
                {
                    using (StreamReader sr = new StreamReader(rulepath, Encoding.UTF8))
                    {
                        string line;
                        int counter = 0;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (counter % 5 == 0) { rules[0] += line + "\n\n\n\n"; rules[1] += "\n"; rules[2] += "\n"; counter++; }
                            else if (counter == 4 || counter == 9 || counter == 14 || counter == 19) { rules[2] += line + "\n\n\n"; counter++; } //!!!!
                            else { rules[1] += line + "\n"; counter++; }
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Reading Error! RULES");
                }
            }
            //Новый список сложностей
            try
            {
                using (StreamReader sr = new StreamReader(diffnewpath, Encoding.Default))
                {
                    string line;
                    int counter = 0;
                    string name = "";
                    int[] diff = new int[5];
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (counter == 0)
                        {
                            name = line;
                            counter++;
                        }
                        else if (counter < 6)
                        {
                            diff[counter - 1] = Int32.Parse(line);
                            counter++;
                        }
                        else if (counter == 6)
                        {
                            List<int> temp_diff_list = diff.Cast<int>().ToList();
                            diff_listnew.Add(new Difficulty { Name = name, Rating_complexity = temp_diff_list, Percentage_complexity = 0 });
                            counter = 0;
                        }
                    }
                    
                    foreach (Difficulty d in diff_listnew)
                    {
                        Console.WriteLine(d.Name);
                        for (int i = 0; i < d.Rating_complexity.Count; i++)
                        {
                            Console.WriteLine(d.Rating_complexity[i]);
                        }
                    }
                    Console.WriteLine("next");
                    
                }
            }
            catch
            {
                Console.WriteLine("Reading Error! DIFFICULTIES");
            }
                
            // Список модов
            try
            {
                using (StreamReader sr = new StreamReader(modspath, Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        mods.Add(line);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Reading Error! MODS");
            }

            result_type.Add("FC");
            result_type.Add("Pass");

        }

    }

}
