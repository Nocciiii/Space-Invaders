using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace Model
{
    public class Alien : INotifyPropertyChanged
    {
        private Uri look;
        private static int currentId = 0;
        private int id;
        private int currentRow;
        private Double xpos;
        private Double ypos;
        private Boolean dead = false;
        private int level;
        public event PropertyChangedEventHandler PropertyChanged;
        public Alien(Double xpos, Double ypos, int currentRow)
        {
            this.Xpos = xpos;
            this.Ypos = ypos;
            this.CurrentRow = currentRow;
            this.id = currentId;
            currentId++;
            Random random = new Random();
            level = random.Next(50);
            if (level <= 30)
            {
                level = 2;
                look = new Uri("Alien2.jpg", UriKind.Relative);
            }
            else
            {
                level = 1;
                look = new Uri("Alien1.png", UriKind.Relative);
            }
        }

        protected void OnPropertyChanged(String name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public Uri Look { get => look; set => look = value; }
        public double Xpos { get => xpos; set { xpos = value; OnPropertyChanged("Xpos"); } }
        public double Ypos { get => ypos; set { ypos = value; OnPropertyChanged("Ypos"); } }
        public int Id { get => id; }
        public int CurrentRow { get => currentRow; set => currentRow = value; }
        public bool Dead { get => dead; set => dead = value; }
        public int Level { get => level; set => level = value; }
    }
}
