using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HighscoreServer
{
    class Server
    {
        private List<Highscore> highscores = new List<Highscore>();
        private int port = 8008;

        static void Main(string[] args)
        {
            new Server();
        }
        public Server()
        {
            Running();
        }
        public void Running()
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

            while (true)
            {
                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(localEndPoint);
                listener.Listen(1);
                Socket handler=listener.Accept();
                Thread t = new Thread(() => send(handler));
                t.Start();
            }
        }

        private void readDB()
        {
            OleDbConnection con = new OleDbConnection(Properties.Settings.Default.DbCon);
            con.Open();
            OleDbCommand com = con.CreateCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = ("Select * From Highscores");
            OleDbDataReader reader = com.ExecuteReader();
            while (reader.Read() == true)
            {
                Highscore h = new Highscore();
                h.Initials = reader["initials"].ToString();
                h.Points = Convert.ToInt32(reader["points"].ToString());
                highscores.Add(h);
            }
            highscores.OrderBy(x => x.Points);

            con.Close();
        }
        private void send(Socket handler)
        {
            readDB();
            byte[] msg = null;
            XmlSerializer ser = new XmlSerializer(typeof(Highscore));
            foreach (Highscore h in highscores)
            {
                using (StringWriter textWriter = new StringWriter())
                {
                    ser.Serialize(textWriter, h);
                    String obj = textWriter.ToString();
                    msg = Encoding.ASCII.GetBytes(obj);
                    handler.Send(msg);

                }
            }
        }
    }
}
