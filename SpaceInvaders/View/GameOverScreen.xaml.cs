using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace View
{
    /// <summary>
    /// Interaktionslogik für GameOverScreen.xaml
    /// </summary>
    public partial class GameOverScreen : Window
    {
        private int highscore;
        public GameOverScreen(bool sieg, int highscore)
        {
            InitializeComponent();

            this.highscore = highscore;
            if (sieg == true)
            {
                GameOverMessage.Text = "Glückwunsch, Sie haben gewonnen";
            }
            else
            {
                GameOverMessage.Text = "Game Over, Sie haben versagt";
            }
            Points.Text = Convert.ToString(highscore);
        }

        private void HighscoreSave_Click(object sender, RoutedEventArgs e)
        {
            WriteHighscore(Initials.Text, highscore);
        }

        private void HighscoreDrop_Click(object sender, RoutedEventArgs e)
        {
            ToHighscorelist();
        }

        private void WriteHighscore(String initials, int points)
        {
            Highscore newHighscore = new Highscore();
            newHighscore.Points = points;
            newHighscore.Initials = initials;
            SendHighscore(newHighscore);
            ToHighscorelist();
        }
        private void SendHighscore(Highscore newHighscore)
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 8008);

            Socket sender = new Socket(ipAddress.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);
            try
            {
                sender.Connect(remoteEP);
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
            }
            byte[] msg = null;
            //msg for protocoll
            msg = Encoding.ASCII.GetBytes("2");
            sender.BeginSend(msg, 0, msg.Length, 0, new AsyncCallback(SendCallback), sender);
            //actual message
            String obj = newHighscore.Points + '~' + newHighscore.Initials;
            msg = Encoding.ASCII.GetBytes(obj);
            sender.BeginSend(msg, 0, msg.Length, 0, new AsyncCallback(SendCallback), sender);

            sender.Close();
        }
        private void ToHighscorelist()
        {
            HighscoreList highscorelist = new HighscoreList();
            this.Visibility = Visibility.Hidden;
            highscorelist.Visibility = Visibility.Visible;
            this.Close();
        }

        private static void SendCallback(IAsyncResult ar)
        {
            Socket send = (Socket)ar.AsyncState;
            send.EndSend(ar);
        }
    }
}
