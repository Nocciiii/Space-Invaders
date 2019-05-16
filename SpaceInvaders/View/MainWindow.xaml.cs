using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private Spieler spieler;
        private Image img;
        private List<Image> aliens = new List<Image>();
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        Double richtung = 5;

        public MainWindow()
        {
            InitializeComponent();
            erstelleSpieler();
            timer.Interval = TimeSpan.FromSeconds(0.05);
            timer.Tick += ticker;
            timer.IsEnabled = true;
        }

        private void erstelleSpieler()
        {
            this.spieler = new Spieler();
            this.img = new Image();
            spielfeld.Children.Add(img);
            img.Source = new BitmapImage(spieler.Aussehen);
            double left = spielfeld.ActualWidth/2;
            img.Height = 20;
            img.Width = 35;
            Canvas.SetLeft(img, 50);
            Canvas.SetBottom(img, 15);
            img.Visibility = Visibility.Visible;
        }

        private void ticker(object sender, EventArgs e)
        {
            Double posx;
            Double posy;
            int reihen = 0;

            erstelleReihe();
            while (spieler.Leben != 0)
            {
                foreach (Image img in aliens)
                {
                    posx = Canvas.GetLeft(img);
                    posx = posx + richtung;
                }
            }
            richtung = -richtung;
            foreach (Image img in aliens)
            {
                posy = Canvas.GetTop(img);
                posy = posy + 5;
                if (posy >= Canvas.GetTop(img))
                {
                    spieler.Treffer();
                }
                if (reihen != 5)
                {
                    erstelleReihe();
                }
                reihen++;
            }
        }
        private void erstelleReihe()
        {
            for (int j = 0; j < 15; j++)
            {
                Alien a = new Alien();
                Image imga = new Image();
                imga.Source = new BitmapImage(a.Aussehen);
                spielfeld.Children.Add(imga);
                if (richtung == 5)
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

        private void spielfeld_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(this);
            Canvas.SetLeft(img, p.X);
        }
    }
}
