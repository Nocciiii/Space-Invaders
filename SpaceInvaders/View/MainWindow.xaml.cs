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
        private List<Thread> threads = new List<Thread>();
        private List<Image> kingdomHearts = new List<Image>();
        private int row = 1;
        private int maxRow = 10;
        private int highscore = 0;
        private Boolean end = false;
        private QuickMaths quickMaths;

        public MainWindow()
        {
            InitializeComponent();
            this.quickMaths = new QuickMaths(this);
            gameStart();
        }

        private void gameStart()
        {
            //Create Player
            this.img = new Image();
            img.Height = quickMaths.PlayerHeight;
            img.Width = quickMaths.PlayerWidth;
            this.player = new Player(quickMaths.getPlayerXpos(this), quickMaths.getPlayerYpos(this));

            playground.Children.Add(img);
            img.Source = new BitmapImage(player.Look);


            Canvas.SetLeft(img, quickMaths.getPlayerXpos(this));
            Canvas.SetTop(img, quickMaths.getPlayerYpos(this));
            img.Visibility = Visibility.Visible;

            //Create 3 rows of enemys at the start of game
            while (row <= 3)
            {
                for (int j = 8; j >= 1; j--)
                {
                    
                    Image imga = new Image();
                    imga.Height = quickMaths.AlienHeight;
                    imga.Width = quickMaths.AlienWidth;
                    Alien a = new Alien((playground.Width - img.Width) / 8 * j - imga.Width, row * quickMaths.RowDifference, row);
                    imga.Source = new BitmapImage(a.Look);
                    playground.Children.Add(imga);
                    Canvas.SetLeft(imga, a.Xpos);
                    Canvas.SetTop(imga, a.Ypos);
                    imga.Visibility = Visibility.Visible;
                    aliens.Add(imga);
                    alienObject.Add(a);
                    if (a.Level == 2)
                    {
                        Thread t = new Thread(() => battleStrats(a, imga));
                        t.SetApartmentState(ApartmentState.STA);
                        t.Start();
                    }
                }
                row++;
            }
            
            for(int i = 0; i < 3; i++)
            {
                Image imgk = new Image();
                imgk.Height = quickMaths.HeartHeight;
                imgk.Width = quickMaths.HeartWidth;
                Uri look = new Uri("KingdomHearts.png", UriKind.Relative);
                Canvas.SetLeft(imgk, quickMaths.getHeartXpos(this, i));
                Canvas.SetTop(imgk, quickMaths.getHeartYpos(this));
                imgk.Source = new BitmapImage(look);
                imgk.Visibility = Visibility.Visible;
                playground.Children.Add(imgk);
                kingdomHearts.Add(imgk);
            }
        }

        private void alienMove(MainWindow main)
        {
            int j = 0;
            Double rowMovement = 0;

            while (player.Life != 0 || aliens != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    j = 0;
                    foreach (Image imgl in aliens)
                    {
                        Alien alien = alienObject.ElementAt(j);
                        alien.Xpos = alien.Xpos + quickMaths.Direction;
                        Dispatcher.BeginInvoke(new Action(() => changeAlienPos(alien)));
                        j++;
                    }
                    Thread.Sleep(50);
                }
                rowMovement += quickMaths.RowDown;
                j = 0;

                

                quickMaths.Direction = -quickMaths.Direction;
                foreach (Image imgl in aliens)
                {
                    Alien alien = alienObject.ElementAt(j);
                    alien.Ypos = alien.Ypos + quickMaths.RowDown;
                    Dispatcher.BeginInvoke(new Action(() => changeAlienPos(alien)));
                    Dispatcher.BeginInvoke(new Action(() => playerHealth(alien, imgl)));
                    j++;
                }
                if(player.Hitted == true)
                {
                    player.Hitted = false;
                }
                if (row <= maxRow && rowMovement >= quickMaths.RowDifference && quickMaths.Direction > 0)
                {
                    Dispatcher.BeginInvoke(new Action(() => createRow()));
                    rowMovement = 0;
                }

            }
        }

        private void playerHealth(Alien alien, Image imgl)
        {
            if (alien.Ypos >= player.Ypos - img.Height && alien.Ypos <= player.Ypos - img.Height + quickMaths.RowDown)
            {
                
                if (alien.Dead == false && player.Hitted == false)
                {
                    player.Hit();
                    player.Hitted = true;
                    Image imgk = kingdomHearts.Last();
                    playground.Children.Remove(imgk);
                    kingdomHearts.Remove(imgk);
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
            for (int j = 1; j <= 8; j++)
            {
                Image imga = new Image();
                imga.Height = quickMaths.AlienHeight;
                imga.Width = quickMaths.AlienWidth;
                Alien a = new Alien((playground.Width - img.Width) / 8 * j - imga.Width + quickMaths.Direction, quickMaths.RowDifference, row);
                imga.Source = new BitmapImage(a.Look);
                playground.Children.Add(imga);
                Canvas.SetLeft(imga, a.Xpos);
                Canvas.SetTop(imga, a.Ypos);
                imga.Visibility = Visibility.Visible;
                aliens.Add(imga);
                alienObject.Add(a);
                if (a.Level == 2)
                {
                    Thread t = new Thread(() => battleStrats(a, imga));
                    t.SetApartmentState(ApartmentState.STA);
                    t.Start();
                }
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
            isPlayerAlive();
            GameOverScreen gameover = new GameOverScreen(sieg, highscore);
            this.Visibility = Visibility.Hidden;
            gameover.Visibility = Visibility.Visible;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
                Thread t = new Thread(() => Dispatcher.BeginInvoke(new Action(() => createShot(player.Xpos, player.Ypos, true, img))));
                t.Start();
                threads.Add(t);
        }

        public void createShot(double posX, double posY, bool direction, Image imgSender)
        {
            Shot shot;
            Image imgs = new Image();
            imgs.Height = quickMaths.ShotHeight;
            imgs.Width = quickMaths.ShotWidth;
            shot = new Shot(posX + imgSender.Width / 2 - imgs.Width / 2, posY - imgSender.Height, direction);
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
            Double shotMove = quickMaths.ShotDirection;
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
                    if (shot.Xpos + imgs.Width >= alien.Xpos && shot.Xpos <= alien.Xpos + imga.Width 
                        && shot.Ypos <= alien.Ypos + imga.Height
                        && shot.Ypos + imgs.Height >= alien.Ypos)
                    {
                        if (alien.Dead == false && shot.Hitted == false)
                        {
                            shot.Hitted = true;
                            shot.Alive = false;
                            playground.Children.Remove(imgs);

                            playground.Children.Remove(imga);
                            alien.Dead = true;
                        }
                    }
                }
                else
                {
                    if (shot.Xpos + imgs.Width >= player.Xpos && shot.Xpos <= player.Xpos + img.Width
                        && shot.Ypos <= player.Ypos + img.Height
                        && shot.Ypos + imgs.Height >= player.Ypos)
                    {
                        if (shot.Alive == true && shot.Hitted == false)
                        {
                            shot.Hitted = true;
                            player.Hit();
                            shot.Alive = false;
                            isPlayerAlive();
                            playground.Children.Remove(imgs);

                            Image imgk = kingdomHearts.Last();
                            playground.Children.Remove(imgk);
                            kingdomHearts.Remove(imgk);
                        }
                    }
                }

                i++;
            }
        }

        private void playground_MouseMove(object sender, MouseEventArgs e)
        {
            //Mouse.OverrideCursor = Cursors.None;
        }

        private void battleStrats(Alien a, Image imga)
        {
            while (a.Dead == false)
            {
                Dispatcher.BeginInvoke(new Action(() => createShot(a.Xpos, a.Ypos, false, imga)));
                Thread.Sleep(7000);
            }
        }

        private void isPlayerAlive()
        {
            if (player.Life <= 0 && end == false)
            {
                end = true;
                playground.Children.Remove(img);
                gameover();
            }
        }
    }
    
}
