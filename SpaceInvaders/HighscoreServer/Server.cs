﻿using System;
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
        byte[] bytes = new byte[1024];
        Socket handler;
        Socket listener;

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
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);
            Console.Write("Server gestartet");
            listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(100);
            while (true)
            {
                handler=listener.Accept();
                Thread t = new Thread(() => HandleRequestFromClient(handler));
                t.Start();
            }
        }

        private void readDB()
        {
            OleDbConnection con = new OleDbConnection(Settings.Default.DbCon);
            con.Open();
            OleDbCommand com = con.CreateCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = ("Select * From Highscores");
            OleDbDataReader reader = com.ExecuteReader();
            while (reader.Read() == true)
            {
                Highscore h = new Highscore();
                h.Initials = reader["Initials"].ToString();
                h.Points = Convert.ToInt32(reader["Points"].ToString());
                highscores.Add(h);
            }
            highscores.OrderBy(x => x.Points);
            con.Close();
        }
        private void Send(Socket handler)
        {
            readDB();
            byte[] msg = null;
            foreach (Highscore h in highscores)
            {
                    String obj = h.Points+";"+h.Initials;
                    msg = Encoding.ASCII.GetBytes(obj);
                    handler.BeginSend(msg, 0, msg.Length, 0, new AsyncCallback(SendCallback), handler);
                    Thread.Sleep(100);
            }
            handler.Close();
        }
        public void HandleRequestFromClient(Socket handler)
        {
            int bytestream=handler.Receive(bytes);
            String decoded = Encoding.ASCII.GetString(bytes, 0, bytestream);
            int task = Convert.ToInt32(decoded);
            if(task==1)
            {
                Send(handler);
            }
            else
            {
                ReceiveData(handler);
            }
        }
        public void ReceiveData(Socket handler)
        {
            int bytesRec = handler.Receive(bytes);
            String obj = Encoding.ASCII.GetString(bytes, 0, bytesRec);
            WriteHighscore(obj);
        }
        private void WriteHighscore(String o)
        {
            String sendHighscore = o;
            String[]splitHighscore=sendHighscore.Split(';');
            Highscore h = new Highscore();
            h.Points = Convert.ToInt32(splitHighscore[0]);
            h.Initials = splitHighscore[1];

            OleDbConnection con = new OleDbConnection(Settings.Default.DbCon);
            con.Open();
            OleDbCommand com = con.CreateCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = ("INSERT INTO Highscores(Initials,Points)" + "VALUES (?,?)");
            com.Parameters.AddWithValue(h.Initials, h.Points);
            com.ExecuteNonQuery();

            handler.Close();
            con.Close();
        }
        private static void SendCallback(IAsyncResult ar)
        {
            Socket send = (Socket)ar.AsyncState;
            send.EndSend(ar);
        }
    }
}
