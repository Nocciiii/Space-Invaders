using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace View
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>5
    public partial class MainWindow : Window
    {
        private Player player;
        private Image img;
        private List<Image> aliens = new List<Image>();
        private System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        private Double direction = 5;
        private Thread t;

        public MainWindow()
        {
            InitializeComponent();
            erstelleplayer();
        }

        private void erstelleplayer()
        {
            this.player = new Player();
            this.img = new Image();
            playground.Children.Add(img);
            img.Source = new BitmapImage(player.Look);
            double left = playground.ActualWidth/2;
            img.Height = 20;
            img.Width = 35;
            Canvas.SetLeft(img, 50);
            Canvas.SetBottom(img, 15);
            img.Visibility = Visibility.Visible;
        }

        private void alienMove()
        {
            Double posx;
            Double posy;
            int reihen = 0;

            createRow();
            while (player.Life != 0)
            {
                foreach (Image img in aliens)
                {
                    posx = Canvas.GetLeft(img);
                    posx = posx + direction;
                }
            }
            direction = -direction;
            foreach (Image img in aliens)
            {
                posy = Canvas.GetTop(img);
                posy = posy + 5;
                if (posy >= Canvas.GetTop(img))
                {
                    player.Hit();
                }
                if (reihen != 5)
                {
                    createRow();
                }
                reihen++;
            }
        }
        private void createRow()
        {
            for (int j = 0; j < 15; j++)
            {
                Alien a = new Alien();
                Image imga = new Image();
                imga.Source = new BitmapImage(a.Look);
                playground.Children.Add(imga);
                if (direction == 5)
                {
                    Canvas.SetLeft(imga, 0 + j * 5);
                }
                else
                {
                    Canvas.SetRight(imga, 0 + j * 5);
                }
                Canvas.SetTop(imga, 0);
                imga.Height = 5;
                imga.Width = 5;
                aliens.Add(imga);

            }
        }

        private void playground_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(this);
            Canvas.SetLeft(img, p.X);
        }
    }
}
