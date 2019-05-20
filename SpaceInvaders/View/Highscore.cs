using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    class Highscore
    {
        private int points;
        private String initials;

        public int Points { get => points; set => points = value; }
        public string Initials { get => initials; set => initials = value; }
    }
}
