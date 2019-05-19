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
        private Double direction = 5;
        private Thread t;
        private int row = 1;
        private int maxRow = 10;

        public MainWindow()
        {
            InitializeComponent();
            gameStart();
            this.t = new Thread(alienMove);
            t.Start();
        }

        private void gameStart()
        {
            //Create Player
            this.player = new Player();
            this.img = new Image();
            playground.Children.Add(img);
            img.Source = new BitmapImage(player.Look);
            double left = playground.Width / 2;
            img.Height = 20;
            img.Width = 35;
            Canvas.SetLeft(img, left);
            Canvas.SetBottom(img, 15);
            img.Visibility = Visibility.Visible;

            //Create 3 rows of enemys at the start of game
            while (row <= 3)
            {
                for (int j = 1; j <= 9; j++)
                {
                    Alien a = new Alien();
                    Image imga = new Image();
                    imga.Source = new BitmapImage(a.Look);
                    playground.Children.Add(imga);
                    imga.Height = 20;
                    imga.Width = 35;
                    Canvas.SetLeft(imga, (playground.Width - img.Width)/10*j - imga.Width);
                    Canvas.SetTop(imga, row * 30);
                    imga.Visibility = Visibility.Visible;
                    aliens.Add(imga);
                }
                row++;
            }
        }
        private void alienMove()
        {
            Double posx;
            Double posy;

            while (player.Life != 0 || aliens != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    foreach (Image imgl in aliens)
                    {
                        posx = Canvas.GetLeft(imgl);
                        posx = posx + direction;
                        Canvas.SetLeft(imgl, posx);
                    }
                }
            
                direction = -direction;
                foreach (Image imga in aliens)
                {
                    posy = Canvas.GetTop(imga);
                    posy = posy + 5;
		    Canvas.SetTop(imga,posy);
                    if (posy >= Canvas.GetTop(img))
                    {
                        player.Hit();
                    }
                    if (row <= maxRow)
                    {
                        createRow();
                    }
                    row++;
                }
            }

            if(player.Life <= 0)
            {
                //Am Ende eine eigene Seite für Game Over und Abgeschlossen
            }
        }

        private void createRow()
        {
            for (int j = 1; j < 16; j++)
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
                img.Visibility = Visibility.Visible;
                aliens.Add(imga);
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(this);
            Canvas.SetLeft(img, p.X - img.Width / 2);
        }
    }
}
