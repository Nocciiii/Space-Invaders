using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Model
{
    public class Player
    {
        private int life = 3;
        private Uri look = new Uri("Laser_Cannon.png", UriKind.Relative);
        private Double xpos;
        private Double ypos;
        private Boolean hitted = false;

        public Player(Double xpos, Double ypos)
        {
            this.Xpos = xpos;
            this.Ypos = ypos;
        }

        public void Hit()
        {
            life = life - 1;
            Hitted = true;
            Thread t = new Thread(() => playerGotHitted());
            t.Start();
        }

        private void playerGotHitted()
        {
            Thread.Sleep(100);
            Hitted = false;
        }

        public int Life { get => life; set => life = life-1; }
        public Uri Look { get => look; set => look = value; }
        public Double Xpos { get => xpos; set => xpos = value; }
        public Double Ypos { get => ypos; set => ypos = value; }
        public bool Hitted { get => hitted; set => hitted = value; }
    }
}
