using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    class QuickMaths
    {
        private Double shotDirection;
        private Double rowDifference;
        private Double rowDown;
        private Double direction;
        private Double playerHeight;
        private Double playerWidth;
        private Double alienHeight;
        private Double alienWidth;
        private Double shotHeight;
        private Double shotWidth;

        public QuickMaths(MainWindow main)
        {
            this.Direction = main.playground.Height / 100 * 2;
            this.RowDown = main.playground.Height / 100 * 1.5;
            this.ShotDirection = main.playground.Height / 100 * 3;
            this.RowDifference = main.playground.Height / 100 * 9;
            this.PlayerHeight = main.playground.Height / 100 * 9;
            this.PlayerWidth = main.playground.Height / 100 * 13;
            this.AlienHeight = main.playground.Height / 100 * 9;
            this.AlienWidth = main.playground.Height / 100 * 12;
            this.ShotHeight = main.playground.Height / 100 * 3;
            this.ShotWidth = main.playground.Height / 100 * 3;
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
    }
}
