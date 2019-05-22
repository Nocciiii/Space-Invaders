using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
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

        private void WriteHighscore(String initialen, int points)
        {
            OleDbConnection con = new OleDbConnection(Properties.Settings.Default.DbCon);
            con.Open();
            OleDbCommand com = con.CreateCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = ("INSERT INTO Highscores(Initials,Points)" + "VALUES (?,?)");
            com.Parameters.AddWithValue(initialen, points);
            com.ExecuteNonQuery();

            con.Close();
            ToHighscorelist();
        }
        private void ToHighscorelist()
        {
            HighscoreList highscorelist = new HighscoreList();
            this.Visibility = Visibility.Hidden;
            highscorelist.Visibility = Visibility.Visible;
        }
    }
}
