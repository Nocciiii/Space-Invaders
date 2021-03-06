﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    public class Highscore
    {
        private int points;
        private String initials;

        public int Points
        {
            get
            {
                return points;
            }

            set
            {
                points = value;
            }
        }

        public string Initials
        {
            get
            {
                return initials;
            }

            set
            {
                initials = value;
            }
        }

        public override string ToString()
        {
            return Initials+Points.ToString();
        }
    }
}
