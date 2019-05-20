﻿using Model;
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
        private Thread t;
        private int row = 1;
        private int maxRow = 10;

        public MainWindow()
        {
            InitializeComponent();
            gameStart();
        }

        private void gameStart()
        {
            //Create Player
            this.img = new Image();
            img.Height = 20;
            img.Width = 35;
            double left = playground.Width / 2;
            this.player = new Player(left, 15);

            playground.Children.Add(img);
            img.Source = new BitmapImage(player.Look);


            Canvas.SetLeft(img, left);
            Canvas.SetBottom(img, 15);
            img.Visibility = Visibility.Visible;

            //Create 3 rows of enemys at the start of game
            while (row <= 3)
            {
                for (int j = 1; j <= 9; j++)
                {
                    Image imga = new Image();
                    imga.Height = 20;
                    imga.Width = 35;
                    Alien a = new Alien((playground.Width - img.Width) / 10 * j - imga.Width, row * 30);
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

                j = 0;

                direction = -direction;
                foreach (Image imga in aliens)
                {
                    Alien alien = alienObject.ElementAt(j);
                    alien.Ypos = alien.Ypos + 5;
                    Dispatcher.BeginInvoke(new Action(() => changeAlienPos(alien)));
                    if (alien.Ypos >= player.Ypos)
                    {
                        player.Hit();
                    }
                    if (row <= maxRow)
                    {
                        //createRow();
                    }
                    row++;
                    j++;
                }
            }

            if (player.Life <= 0)
            {
                //Am Ende eine eigene Seite für Game Over und Abgeschlossen
            }
        }

        private void createRow()
        {
            for (int j = 1; j < 16; j++)
            {
                Alien a = new Alien(0, 0);
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
            t = new Thread(() => alienMove(this));
            t.Start();
        }
    }
}
