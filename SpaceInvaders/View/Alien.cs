using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Alien
    {
        private Uri aussehen = new Uri("Alien1.png", UriKind.Relative);


        public Uri Aussehen { get => aussehen; set => aussehen = value; }
    }
}
