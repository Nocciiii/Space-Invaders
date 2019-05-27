using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HighscoreServer
{
    class Server
    {
        private int port = 8008;
        private String host = "localhost";
        Socket socket = null;

        static void Main(string[] args)
        {
            new Server();
        }
        public Server()
        {
            running();
        }
        public void running()
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(host);
            IPAddress[] ipAddresses = hostEntry.AddressList;

            Console.WriteLine(host + " is mapped to the IP-Address(es): ");

            // Ausgabe der zugeordneten IP-Adressen
            foreach (IPAddress ipAddress in ipAddresses)
            {
                Console.Write(ipAddress.ToString());
            }

            // Instanziere einen Endpunkt mit der ersten IP-Adresse
            IPEndPoint ipEo = new IPEndPoint(ipAddresses[0], port);

            while (true)
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipEo);

                socket.Close();
            }
        }
    }
}
