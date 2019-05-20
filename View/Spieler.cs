using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Model
{
    public class Spieler
    {
        int leben = 3;
        Uri aussehen = new Uri("Laser_Cannon.png", UriKind.Relative);

        public int Leben { get => leben; set => leben = leben-1; }
        public Uri Aussehen { get => aussehen; set => aussehen = value; }
        public void Treffer()
        {
            leben = leben - 1;
        }
    }
}
