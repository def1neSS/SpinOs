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
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;


namespace SpinOs.Data
{
    public static class InitData
    {

        public static List<Difficulty> diff_listnew = new List<Difficulty>(); // список классов сложностей
        //public static List<Difficulty> diffnew_list = new List<Difficulty>(); //новый список классов сложностей
        public static List<string> mods = new List<string>(); // список модов, которые будут прокручены в слоте
        public static List<string> result_type = new List<string>(); // список результатов ( FC , Pass , etc. )
        public static List<string> rules = new List<string>();

        static string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        static string modspath = @"..\..\Data\mods.txt";
        //static string diffpath = @"..\..\Data\difficulty.txt";
        static string diffnewpath = @"..\..\Data\difficulties_new.txt";
        static string rulepath = @"..\..\Data\rule.txt";

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

            string rule1 = "";
            string rule2 = "";
            string rule3 = "";



            // Список правил
            try
            {
                using (StreamReader sr = new StreamReader(rulepath, Encoding.UTF8))
                {
                    string line;
                    int counter = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (counter % 5 == 0) { rule1 += line + "\n\n\n\n\n"; rule2 += "\n"; rule3 += "\n"; counter++; }
                        else if (counter == 4 || counter == 9 || counter == 14 || counter == 19) { rule3 += line + "\n\n\n\n"; counter++; } //!!!!
                        else { rule2 += line + "\n"; counter++; }
                    }
                }
                rules.Add(rule1);
                rules.Add(rule2);
                rules.Add(rule3);
            }
            catch
            {
                Console.WriteLine("Reading Error! RULES");
            }

            //Новый список сложностей

                using (StreamReader sr = new StreamReader(diffnewpath, Encoding.Default))
                {
                    string line;
                    int counter = 0;
                    string name = "";
                    //List<int> diff = new List<int>();
                    int[] diff = new int[5];
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (counter == 0)
                        {
                            name = line;
                            counter++;
                            //Console.WriteLine(line);
                        }
                        else if (counter < 6)
                        {
                            //diff.Add(Int32.Parse(line));
                            diff[counter - 1] = Int32.Parse(line);
                            counter++;
                            //Console.WriteLine(line);
                        }
                        else if (counter == 6)
                        {
                            List<int> temp_diff_list = diff.Cast<int>().ToList();
                            diff_listnew.Add(new Difficulty { Name = name, Rating_complexity = temp_diff_list, Percentage_complexity = 0 });
                            counter = 0;
                            //diff = new List<int>();


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
            try
            {
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
