﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Alien
    {
        private Uri look = new Uri("Alien1.png", UriKind.Relative);


        public Uri Look { get => look; set => look = value; }
    }
}
