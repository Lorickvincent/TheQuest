using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfApplication1
{
    public class Mob
    {


        public string Name { get; set; }
        public Uri Sprite { get; set; }
        public Position Position { get; set; }

        public Mob()
        {
            Position = new Position();
        }
    }
}
