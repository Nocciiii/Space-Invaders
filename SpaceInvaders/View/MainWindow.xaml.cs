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
        private List<Alien> alienObject = new List<Alien>();
        private Double direction = 5;
        private List<Thread> threads = new List<Thread>();
        private int row = 1;
        private int maxRow = 10;
        private int rowDown = 5;
        private Double shotDirection;
        private int rowDifference = 30;
        private int highscore = 0;
        private Boolean end = false;

        public MainWindow()
        {
            InitializeComponent();
            shotDirection = playground.Height / 100 * 5;
            gameStart();
        }

        private void gameStart()
        {
            //Create Player
            Double playerY = playground.Height / 100 * 90;
            this.img = new Image();
            img.Height = 20;
            img.Width = 35;
            double left = playground.Width / 2;
            this.player = new Player(left, playerY);

            playground.Children.Add(img);
            img.Source = new BitmapImage(player.Look);


            Canvas.SetLeft(img, left);
            Canvas.SetTop(img, playerY);
            img.Visibility = Visibility.Visible;

            //Create 3 rows of enemys at the start of game
            while (row <= 3)
            {
                for (int j = 10; j >= 1; j--)
                {
                    
                    Image imga = new Image();
                    imga.Height = 20;
                    imga.Width = 35;
                    Alien a = new Alien((playground.Width - img.Width) / 10 * j - imga.Width, row * rowDifference, row);
                    imga.Source = new BitmapImage(a.Look);
                    playground.Children.Add(imga);
                    Canvas.SetLeft(imga, a.Xpos);
                    Canvas.SetTop(imga, a.Ypos);
                    imga.Visibility = Visibility.Visible;
                    aliens.Add(imga);
                    alienObject.Add(a);
                }
                row++;
            }
            
        }

        private void alienMove(MainWindow main)
        {
            int j = 0;
            int rowMovement = 0;

            while (player.Life != 0 || aliens != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    j = 0;
                    foreach (Image imgl in aliens)
                    {
                        Alien alien = alienObject.ElementAt(j);
                        alien.Xpos = alien.Xpos + direction;
                        Dispatcher.BeginInvoke(new Action(() => changeAlienPos(alien)));
                        j++;
                    }
                   Thread.Sleep(100);
                }
                rowMovement += rowDown;
                j = 0;

                

                direction = -direction;
                foreach (Image imgl in aliens)
                {
                    Alien alien = alienObject.ElementAt(j);
                    alien.Ypos = alien.Ypos + rowDown;
                    Dispatcher.BeginInvoke(new Action(() => changeAlienPos(alien)));
                    Dispatcher.BeginInvoke(new Action(() => playerHealth(alien, imgl)));

                    j++;
                    
                }
                if (row <= maxRow && rowMovement == rowDifference && direction > 0)
                {
                    Dispatcher.BeginInvoke(new Action(() => createRow()));
                    rowMovement = 0;
                }

            }
        }

        private void playerHealth(Alien alien, Image imgl)
        {
            if (alien.Ypos >= player.Ypos - img.Height && alien.Ypos <= player.Ypos - img.Height + rowDown)
            {
                if (alien.Xpos + imgl.Width >= player.Xpos && alien.Xpos  <= player.Xpos + img.Width)
                {
                    if (alien.Dead == false)
                    {
                        player.Hit();
                    }
                }
                playground.Children.Remove(imgl);
                if (player.Life <= 0 && end == false)
                {
                    end = true;
                    playground.Children.Remove(img);
                    gameover();
                }
            }
        }

        private void createRow()
        {
            for (int j = 1; j <= 10; j++)
            {
                Image imga = new Image();
                imga.Height = 20;
                imga.Width = 35;
                Alien a = new Alien((playground.Width - img.Width) / 10 * j - imga.Width + direction, rowDifference, row);
                imga.Source = new BitmapImage(a.Look);
                playground.Children.Add(imga);
                Canvas.SetLeft(imga, a.Xpos);
                Canvas.SetTop(imga, a.Ypos);
                imga.Visibility = Visibility.Visible;
                aliens.Add(imga);
                alienObject.Add(a);
            }
            row++;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(this);
            Canvas.SetLeft(img, p.X - img.Width / 2);
            player.Xpos = p.X - img.Width / 2;
        }

        public void changeAlienPos(Alien alien)
        {
            Image imga = aliens.ElementAt(alien.Id);
            Canvas.SetLeft(imga, alien.Xpos);
            Canvas.SetTop(imga, alien.Ypos);
        }

        private void playground_Loaded(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(() => alienMove(this));
            t.Start();
            threads.Add(t);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            /*foreach(Thread t in threads)
            {
                t.Interrupt();
            }*/
        }
        private void gameover()
        {
            bool sieg = true;
            if (player.Life == 0)
            {
                sieg = false;
            }
            GameOverScreen gameover = new GameOverScreen(sieg, highscore);
            this.Visibility = Visibility.Hidden;
            gameover.Visibility = Visibility.Visible;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image imgs = new Image();
            imgs.Height = 10;
            imgs.Width = 10;
            Shot shot = new Shot(player.Xpos + img.Width / 2 - imgs.Width / 2, player.Ypos - img.Height, true);
            imgs.Source = new BitmapImage(shot.Look);
            playground.Children.Add(imgs);
            Canvas.SetLeft(imgs, shot.Xpos);
            Canvas.SetTop(imgs, shot.Ypos);
            imgs.Visibility = Visibility.Visible;

            Thread t = new Thread(() => shotMove(shot, imgs));
            t.Start();
            threads.Add(t);
        }

        private void shotMove(Shot shot, Image imgs)
        {
            Double shotMove = shotDirection;
            if(shot.IsPlayer == true)
            {
                shotMove = -shotMove;
            }
            while(shot.Alive == true)
            {
                shot.Ypos = shot.Ypos + shotMove;
                Dispatcher.BeginInvoke(new Action(() => changeShotPos(shot, imgs)));
                Thread.Sleep(100);
            }
        }

        private void changeShotPos(Shot shot, Image imgs)
        {
            Canvas.SetLeft(imgs, shot.Xpos);
            Canvas.SetTop(imgs, shot.Ypos);

            if (shot.Ypos <= 0)
            {
                shot.Alive = false;
                playground.Children.Remove(imgs);   
            }
            int i = 0;
            foreach (Alien alien in alienObject)
            {
                Image imga = aliens.ElementAt(i);
                if (shot.IsPlayer == true)
                {
                    if (shot.Xpos + imgs.Width >= alien.Xpos && shot.Xpos <= alien.Xpos + imga.Width && shot.Ypos - imgs.Height >= alien.Ypos - shotDirection && shot.Ypos <= alien.Ypos - imgs.Height + shotDirection)
                    {
                        if (alien.Dead == false)
                        {
                            shot.Alive = false;
                            playground.Children.Remove(imgs);

                            playground.Children.Remove(imga);
                            alien.Dead = true;
                        }
                    }
                }

                i++;
            }
        }
    }
    
}
