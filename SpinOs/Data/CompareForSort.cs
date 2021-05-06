using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinOs.Data
{
    class CompareForSort : IComparer<Player> //класс с функцией для сортировки игроков в таблице
    {
            public int Compare(Player p1, Player p2)
            {
                if (p1.Score > p2.Score)
                {
                    return -1;
                }
                else if (p1.Score < p2.Score)
                {
                    return 1;
                }

                return 0;
            }
    }
}
