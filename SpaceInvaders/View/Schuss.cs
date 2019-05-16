using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Schuss
    {
        Double posx;
        Double posy;

        public Boolean alive;
        public Schuss()
        {
            while(alive==true)
            {
                MoveUP();
            }
        }

        public void feuer(object obj)
        {
            throw new NotImplementedException();
        }

        public void MoveUP()
        {
           
        }
    }
}
