using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinOs.Data
{
    public class Difficulty
    {
        public string Name { get; set; }
        public List<int> Rating_complexity { get; set; }
        public int Percentage_complexity { get; set; }
    }
}
