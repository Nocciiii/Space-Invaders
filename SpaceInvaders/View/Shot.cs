using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Shot
    {
        private Double xpos;
        private Double ypos;
        private Boolean isPlayer;
        private Boolean alive = true;
        private Uri look = new Uri("shot.jpg", UriKind.Relative);

        public Shot(Double xpos, Double ypos, Boolean isPlayer)
        {
            this.Xpos = xpos;
            this.Ypos = ypos;
            this.IsPlayer = isPlayer;
        }

        public void feuer(object obj)
        {
            throw new NotImplementedException();
        }

        public void move()
        {

        }

        public double Xpos { get => xpos; set => xpos = value; }
        public double Ypos { get => ypos; set => ypos = value; }
        public bool IsPlayer { get => isPlayer; set => isPlayer = value; }
        public Uri Look { get => look; set => look = value; }
        public bool Alive { get => alive; set => alive = value; }
    }
}
