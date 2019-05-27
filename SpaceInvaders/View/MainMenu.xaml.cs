using System;
using System.Collections.Generic;
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
    /// Interaktionslogik für MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        
        public MainMenu()
        {
            InitializeComponent();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow game = new MainWindow();
            this.Visibility=Visibility.Hidden;
            game.Visibility = Visibility.Visible;
        }

        private void HighscoreButton_Click(object sender, RoutedEventArgs e)
        {
            HighscoreList highscores = new HighscoreList();
            this.Visibility = Visibility.Hidden;
            highscores.Visibility = Visibility.Visible;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
