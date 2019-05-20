﻿using System;
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
        private Double xpos;
        private Double ypos;

        public Player(Double xpos, Double ypos)
        {
            this.Xpos = xpos;
            this.Ypos = ypos;
        }

        public void Hit()
        {
            life = life - 1;
        }

        public int Life { get => life; set => life = life-1; }
        public Uri Look { get => look; set => look = value; }
        public Double Xpos { get => xpos; set => xpos = value; }
        public Double Ypos { get => ypos; set => ypos = value; }
    }
}
