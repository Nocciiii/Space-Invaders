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
    /// Interaktionslogik für HighscoreList.xaml
    /// </summary>
    public partial class HighscoreList : Window
    {
        private List<Highscore>  listHighscores= new List<Highscore>();
        Socket sender;
        byte[] bytes = new byte[1024];
        public HighscoreList()
        {
            InitializeComponent();
            ConnectToServer();
            Highscores.DataContext = listHighscores;

        }


        public void ConnectToServer()
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 8008);
 
            sender = new Socket(ipAddress.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);
            try
            {
                sender.Connect(remoteEP);
                Thread t = new Thread(() => readDB());
                t.Start();
                System.Threading.Thread.Sleep(100);
                listHighscores.Sort((x,y) => x.Points.CompareTo(y.Points));
            }
            catch(Exception e)
            {

            }
        }
        public void readDB()
        {
            byte[] msg = null;
            //msg for protocoll
            msg = Encoding.ASCII.GetBytes("1");
            sender.Send(msg);
            Highscore type = new Highscore();
            XmlSerializer ser = new XmlSerializer(type.GetType());
            while (true)
            {
                int  bytesRec = sender.Receive(bytes);
                String obj=Encoding.ASCII.GetString(bytes, 0, bytesRec);
                using (StringReader textreader = new StringReader(obj))
                {
                    Highscore h = (Highscore)ser.Deserialize(textreader);
                    listHighscores.Add(h);
                }
            }
        }
    }
}
