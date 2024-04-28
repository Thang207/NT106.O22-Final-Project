﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Server : Form
    {
        // MAX number of user
        private int maxUser;
        // MAX number of game Tables
        private int maxTables;
        private GameTable[] gameTable;
        // List of user connected to the server
        List<User> userList= new List<User>();
        // Local IP
        IPAddress localAddress;
        // Liston Port
        private int port = 8888;
        private TcpListener tcpListener;
        private Service service;
        public Server()
        {
            InitializeComponent();
            service = new Service(ServerLog_lb); 
        }
        // Loading Form Event
        private void Server_Load(object sender, EventArgs e)
        {
            ServerLog_lb.HorizontalScrollbar = true;
            localAddress = IPAddress.Parse("127.0.0.1");
            Disconnect_btn.Enabled = false;
        }
        // Connect Button Event
        private void Connect_btn_Click(object sender, EventArgs e)
        {
            if (int.TryParse(MaxUser_tb.Text, out maxUser) == false
               || int.TryParse(MaxTable_tb.Text, out maxTables) == false)
            {
                MessageBox.Show("Please enter a positive integer in the range!!!");
                return;
            }
            if (maxUser < 1 || maxUser > 300)
            {
                MessageBox.Show("The number of people allowed is 1-300!!!");
                return;
            }
            if (maxTables < 1 || maxTables > 100)
            {
                MessageBox.Show("The number of tables allowed is 1-100!!!");
                return;
            }
            MaxUser_tb.Enabled = false;
            MaxTable_tb.Enabled = false;
            // Create game table array
            gameTable = new GameTable[maxTables]; // Create [maxTabe] tables
            for (int i = 0; i < maxTables; i++)
            {
                gameTable[i] = new GameTable(ServerLog_lb);
            }
            // Monitor
            tcpListener = new TcpListener(localAddress, port);
            tcpListener.Start();
            service.AddItem(string.Format("Start listening for client connections at {0}:{1}", localAddress, port));
            //Create a thread to listen for client connections request
            Thread myThread = new Thread(new ThreadStart(ListenClientConnect));
            myThread.Start();
            Connect_btn.Enabled = false;
            Disconnect_btn.Enabled = true;
        }
        // Disconnect Button Event
        private void Disconnect_btn_Click(object sender, EventArgs e)
        {
            service.AddItem(string.Format("Number of currently connected users:{0}", userList.Count));
            service.AddItem(string.Format("Stop the service immediately, the user will exit in sequence"));
            for (int i = 0; i < userList.Count; i++)
            {
                userList[i].client.Close();
            }
            // exit the listener thread
            tcpListener.Stop();
            Connect_btn.Enabled = true;
            Disconnect_btn.Enabled = false;
            MaxUser_tb.Enabled = true;
            MaxTable_tb.Enabled = true;
        }
        //Receive client connection
        private void ListenClientConnect()
        {
            while (true)
            {
                TcpClient newClient = null;
                try
                {
                    newClient = tcpListener.AcceptTcpClient();
                }
                catch
                {
                    break;
                }
                // create a thread for each client
                Thread threadReceive = new Thread(ReceiveData);
                User user = new User(newClient);
                threadReceive.Start(user);
                userList.Add(user);
                service.AddItem(string.Format("{0}Enter", newClient.Client.RemoteEndPoint));
                service.AddItem(string.Format("Number of currently connected users: {0}", userList.Count));
            }
        }
        //Receive client information
        private void ReceiveData(object obj)
        {
            User user = (User)obj;
            TcpClient client = user.client;
            //Whether to exit the receiving thread normally
            bool normalExit = false;
            //Used to control whether to exit the loop
            bool exitWhile = false;
            while (exitWhile == false)
            {
                string receiveString = null;
                try
                {
                    receiveString = user.sr.ReadLine();
                }
                catch
                {
                    service.AddItem("Failed to receive data");
                }
                //If the TcpClient object is closed and the underlying socket is not closed, no exception is generated, but the read result is null
                if (receiveString == null)
                {
                    if (normalExit == false)
                    {
                        if (client.Connected == true)
                        {
                            service.AddItem(string.Format("lost contact with {0}, has terminated receiving the user information", client.Client.RemoteEndPoint));
                        }
                        RemoveClientfromPlayer(user);
                    }
                    break;
                }
                service.AddItem(string.Format("from {0}:{1}", user.userName, receiveString));
                string[] splitString = receiveString.Split(',');
                string sendString;
                string command = splitString[0].ToLower();
                switch (command)
                {
                    case "login":
                        if (userList.Count > maxUser)
                        {
                            sendString = "Sorry";
                            service.SendToOne(user, sendString);
                            service.AddItem("The number of people is full, refuse" + splitString[1] + "Enter the game room");
                            exitWhile = true;
                        }
                        else
                        {
                            //Save the user's nickname to the user list
                            user.userName = string.Format("[{0}]", splitString[1]);
                            //Send the status of whether there are people at each table to the user
                            sendString = "Tables," + this.GetOnlineString();
                            service.SendToOne(user, sendString);

                        }
                        break;

                }
            }
            userList.Remove(user);
            client.Close();
            service.AddItem(string.Format("There is one exit, remaining connected users: {0}", userList.Count));
        }
        //Detect if the user is sitting on the game table, if so, remove it and terminate the table game
        private void RemoveClientfromPlayer(User user)
        {
            for (int i = 0; i < gameTable.Length; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (gameTable[i].gamePlayer[j].user != null)
                    {
                        if (gameTable[i].gamePlayer[j].user == user)
                        {
                            StopPlayer(i, j);
                            return;
                        }
                    }
                }
            }
        }
        //stop the game at table i
        private void StopPlayer(int i, int j)
        {
            gameTable[i].gamePlayer[j].someone = false;
            gameTable[i].gamePlayer[j].started = false;
            int otherSide = (j + 1) % 2;
            if (gameTable[i].gamePlayer[otherSide].started == true)
            {
                gameTable[i].gamePlayer[otherSide].started = false;
                if (gameTable[i].gamePlayer[otherSide].user.client.Connected == true)
                {
                    service.SendToOne(gameTable[i].gamePlayer[otherSide].user,
                                        string.Format("Lost,{0},{1}",
                                        j, gameTable[i].gamePlayer[j].user.userName));
                }
            }
        }
        //Get the string of whether there is someone at each table, 0 means there is someone, 1 means no one
        private string GetOnlineString()
        {
            string str = "";
            for (int i = 0; i < gameTable.Length; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    str += gameTable[i].gamePlayer[j].someone == true ? "1" : "0";
                }
            }
            return str;
        }
        // Form Closing Event
        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tcpListener != null)
            {
                Disconnect_btn_Click(null, null);
            }
        }
    }
}