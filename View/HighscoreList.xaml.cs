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
    /// Interaktionslogik für HighscoreList.xaml
    /// </summary>
    public partial class HighscoreList : Window
    {
        private List<Highscore>  listHighscores= new List<Highscore>();
        public HighscoreList()
        {
            InitializeComponent();
            readDB();
            Highscores.DataContext = listHighscores;
        }


        private void readDB()
        {
            OleDbConnection con = new OleDbConnection(Properties.Settings.Default.DbCon);
            con.Open();
            OleDbCommand com = con.CreateCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = ("Select * From Highscores");
            OleDbDataReader reader = com.ExecuteReader();
            while(reader.Read()==true)
            {
                Highscore h = new Highscore();
                h.Initials = reader["initials"].ToString();
                h.Points = Convert.ToInt32(reader["points"].ToString());
                listHighscores.Add(h);
            }


           listHighscores.OrderBy(x => x.Points);

            con.Close();
        }
    }
}
