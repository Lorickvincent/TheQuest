using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class Door
    {
        public bool IsOpen { get; set; }
        public Room LeadsTo { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
