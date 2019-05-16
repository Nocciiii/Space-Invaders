using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Model
{
    public class Player
    {
        private int life = 3;
        private Uri look = new Uri("Laser_Cannon.png", UriKind.Relative);

        public int Life { get => life; set => life = life-1; }
        public Uri Look { get => look; set => look = value; }
        public void Hit()
        {
            life = life - 1;
        }
    }
}
