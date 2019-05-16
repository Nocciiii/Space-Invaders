		private void WriteHighscore(String initialen, int points)
        {
            OleDbConnection con = new OleDbConnection(Properties.Settings.Default.DbCon);
            con.Open();
            OleDbCommand com = con.CreateCommand();
            com.CommandType = CommandType.Text;
            com.CommandText=("INSERT INTO Highscores(Initials,Points)" + "VALUES (?,?)");
            com.Parameters.AddWithValue(initialen, points);
            com.ExecuteNonQuery();

            con.Close();
        }