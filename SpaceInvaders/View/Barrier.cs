using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Model
{
    class Barrier
    {
        private int life = 13;
        private static int currentId = 0;
        private int id;
        private Double xpos;
        private Double ypos;
        private Boolean dead = false;
        private Boolean hitted = false;
        private Uri look = new Uri("barrier.png", UriKind.Relative);

        public Barrier()
        {
            this.Id = CurrentId;
            CurrentId += 1;
        }

        public void hit()
        {
            life -= 1;
            if(life <= 0)
            {
                Dead = true;
            }
            Hitted = true;
            Thread t = new Thread(() => barrierGotHitted());
            t.Start();
        }

        private void barrierGotHitted()
        {
            Thread.Sleep(50);
            Hitted = false;
        }

        public int Life { get => life; set => life = value; }
        public static int CurrentId { get => currentId; set => currentId = value; }
        public int Id { get => id; set => id = value; }
        public double Xpos { get => xpos; set => xpos = value; }
        public double Ypos { get => ypos; set => ypos = value; }
        public bool Dead { get => dead; set => dead = value; }
        public bool Hitted { get => hitted; set => hitted = value; }
        public Uri Look { get => look; set => look = value; }
    }
}
