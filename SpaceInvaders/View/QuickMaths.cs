using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    class QuickMaths
    {
        //Variablen für Bewegungen
        private Double shotDirection;
        private Double rowDifference;
        private Double rowDown;
        private Double direction;

        //Variablen für Spieler
        private Double playerHeight;
        private Double playerWidth;

        //Variablen für Aliens
        private Double alienHeight;
        private Double alienWidth;

        //Variablen für Schüsse
        private Double shotHeight;
        private Double shotWidth;

        //Variablen für die Herzen
        private Double heartHeight;
        private Double heartWidth;

        //Variablen für die Barrieren
        private Double barrierHeight;
        private Double barrierWidth;

        //Variablen für die Lebensanzeige der Barriere
        private Double barrierHealthHeight;
        private Double barrierHealthWidth;
        private Double barrierHealthFilling;

        public QuickMaths(MainWindow main)
        {

            //Ausrechnen von Bewegungen
            this.Direction = main.playground.Width / 100 * 2;
            this.RowDown = main.playground.Height / 100 * 1.5;
            this.ShotDirection = main.playground.Height / 100 * 3;
            this.RowDifference = main.playground.Height / 100 * 9;

            //Ausrechnen für Spieler
            this.PlayerHeight = main.playground.Height / 100 * 9;
            this.PlayerWidth = main.playground.Width / 100 * 13;

            //Ausrechnen für Alien
            this.AlienHeight = main.playground.Height / 100 * 9;
            this.AlienWidth = main.playground.Width / 100 * 12;

            //Ausrechnen für Schuss
            this.ShotHeight = main.playground.Height / 100 * 3;
            this.ShotWidth = main.playground.Width / 100 * 3;

            //Ausrechnen für Herzen
            this.HeartHeight = main.playground.Height / 100 * 5;
            this.HeartWidth = main.playground.Width / 100 * 5;

            //Ausrechnen für Barrieren 
            this.BarrierHeight = main.playground.Height / 100 * 15;
            this.BarrierWidth = main.playground.Width / 100 * 15;

            //Ausrechnen für Lebensbalken für Barrieren
            this.BarrierHealthHeight = BarrierHeight / 100 * 10;
            this.BarrierHealthWidth = BarrierWidth;
            this.BarrierHealthFilling = BarrierWidth;
        }

        //Position des Spielers
        public Double getPlayerXpos(MainWindow main)
        {
            Double xpos = main.playground.Width / 2;
            return xpos;
        }

        public Double getPlayerYpos(MainWindow main)
        {
            Double ypos = main.playground.Height / 100 * 95 - PlayerHeight;
            return ypos;
        }

        //Position des Aliens
        public Double getAlienXpos(MainWindow main, Double shotmiddle, Double alienmiddle, int j,  Boolean firstRows)
        {
            Double xpos;
            if (firstRows == true)
            {
                xpos = (main.playground.Width - shotmiddle) / 8 * j - alienmiddle;
            }
            else
            {
                xpos = (main.playground.Width - shotmiddle) / 8 * j - alienmiddle + Direction;
            }
            return xpos;
        }

        public Double getAlienYpos(MainWindow main, int row, Boolean firstRows)
        {
            Double ypos;
            if (firstRows == true)
            {
                ypos = row * RowDifference;
            }
            else
            {
                ypos = RowDifference;
            }
            return ypos;
        }

        //Position des Schusses
        public Double getShotXpos(MainWindow main, Double pos, Double gunmiddle, Double shotmiddle)
        {
            Double xpos = pos + gunmiddle / 2 - shotmiddle / 2;
            return xpos;
        }

        public Double getShotYpos(MainWindow main, Double pos, Double gunmiddle)
        {
            Double ypos = pos - gunmiddle;
            return ypos;
        }

        //Position der Herzen
        public Double getHeartXpos(MainWindow main, int row)
        {
            Double xpos = main.playground.Width / 100 * 5 + row * main.playground.Width / 100 * 5;
            return xpos;
        }

        public Double getHeartYpos(MainWindow main)
        {
            Double ypos = main.playground.Height / 100 * 5;
            return ypos;
        }

        //Position der Barrieren
        public Double getBarrierXpos(MainWindow main, int i)
        {
            Double xpos = main.playground.Width / 7 * (1 + 2 * i);
            return xpos;
        }

        public Double getBarrierYpos(MainWindow main, Player play)
        {
            Double ypos = main.playground.Height / 100 * 70;
            return ypos;
        }

        //Position der Lebensbalken für die Balken
        public Double getHealthBarXpos(Barrier barrier)
        {
            Double xpos = barrier.Xpos;
            return xpos;
        }

        public Double getHealthBarYpos(Barrier barrier)
        {
            Double ypos = barrier.Ypos + barrierHeight / 100 * 110;
            return ypos;
        }

        public Double getHealthBarFilling(Barrier barrier)
        {
            Double filling = BarrierHealthWidth / 13 * barrier.Life;
            return filling;
        }

        public double ShotDirection { get => shotDirection; set => shotDirection = value; }
        public double RowDifference { get => rowDifference; set => rowDifference = value; }
        public double RowDown { get => rowDown; set => rowDown = value; }
        public double Direction { get => direction; set => direction = value; }
        public double PlayerHeight { get => playerHeight; set => playerHeight = value; }
        public double PlayerWidth { get => playerWidth; set => playerWidth = value; }
        public double AlienHeight { get => alienHeight; set => alienHeight = value; }
        public double AlienWidth { get => alienWidth; set => alienWidth = value; }
        public double ShotHeight { get => shotHeight; set => shotHeight = value; }
        public double ShotWidth { get => shotWidth; set => shotWidth = value; }
        public double HeartHeight { get => heartHeight; set => heartHeight = value; }
        public double HeartWidth { get => heartWidth; set => heartWidth = value; }
        public double BarrierHeight { get => barrierHeight; set => barrierHeight = value; }
        public double BarrierWidth { get => barrierWidth; set => barrierWidth = value; }
        public double BarrierHealthHeight { get => barrierHealthHeight; set => barrierHealthHeight = value; }
        public double BarrierHealthWidth { get => barrierHealthWidth; set => barrierHealthWidth = value; }
        public double BarrierHealthFilling { get => barrierHealthFilling; set => barrierHealthFilling = value; }
    }
}
