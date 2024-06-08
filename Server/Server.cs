using System;
using System.Collections.Generic;
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
        #region Global
        // MAX number of user
        private int maxUser;
        // MAX number of game Tables
        private int maxTables;

        // List of user connected to the server
        private List<User> clientList = new List<User>();
        private GameTable[] gameTable;

        // Basic info for connection
        private IPAddress IPServer = IPAddress.Any;
        private int port = 8888;
        private IPEndPoint endPoint;

        private Socket socketListener;
        private Service service;
        #endregion

        public Server()
        {
            InitializeComponent();
            service = new Service(ServerLog_lb);
        }

        #region Server Method
        // Connect Button Event
        private void btnOpenServer_Click(object sender, EventArgs e)
        {
            if (int.TryParse(MaxUser_tb.Text, out maxUser) == false || int.TryParse(MaxTable_tb.Text, out maxTables) == false)
            {
                MessageBox.Show("PLease type integer > 0!!!");
                return;
            }
            if (maxUser < 1 || maxUser > 300)
            {
                MessageBox.Show("The number of people allowed is 1-300!!!");
                return;
            }
            if (maxTables < 1 || maxTables > 150)
            {
                MessageBox.Show("The number of tables allowed is 1-150!!!");
                return;
            }

            MaxUser_tb.Enabled = false;
            MaxTable_tb.Enabled = false;
            // Create game table array
            gameTable = new GameTable[maxTables]; // Create [maxTabe] tables
            for (int i = 0; i < maxTables; i++)
            {
                gameTable[i] = new GameTable();
            }

            // Initializes the server and starts listening for client connections.
            endPoint = new IPEndPoint(IPServer, port);
            socketListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socketListener.Bind(endPoint);
            socketListener.Listen(10);

            // Create a thread waiting for client connections request
            Thread myThread = new Thread(new ThreadStart(ListenClientConnect));
            myThread.Start();
            btnOpenServer.Enabled = false;
            btnCloseSever.Enabled = true;

            service.AddItem($"Open server at {IPServer}:{port}");
        }

        // Disconnect Button Event
        private void btnCloseServer_Click(object sender, EventArgs e)
        {
            service.AddItem(string.Format("Currently players:{0}", clientList.Count));
            service.AddItem(string.Format("Stop the service, the user will exit in sequence"));

            for (int i = 0; i < clientList.Count; i++)
            {
                try
                {
                    if (clientList[i].client.Connected)
                    {
                        clientList[i].client.Shutdown(SocketShutdown.Both);
                        clientList[i].client.Close();
                    }
                }
                catch (Exception ex)
                {
                    service.AddItem($"Error closing client connection: {ex.Message}");
                }
            }

            // exit the listener thread
            socketListener.Close();

            clientList.Clear();

            btnOpenServer.Enabled = true;
            btnCloseSever.Enabled = false;
            MaxUser_tb.Enabled = true;
            MaxTable_tb.Enabled = true;
        }

        // Form Closing Event - Close connect from client
        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (socketListener != null)
            {
                btnCloseServer_Click(null, null);
            }
        }

        // Receive client connection
        private void ListenClientConnect()
        {
            try
            {
                while (true)
                {
                    Socket newClientSocket = socketListener.Accept();

                    // Create a thread for each client
                    User user = new User(newClientSocket);
                    Thread threadReceive = new Thread(() => ReceiveData(user));
                    threadReceive.Start();

                    clientList.Add(user);

                    service.AddItem(string.Format("{0} connected to server", newClientSocket.RemoteEndPoint));
                    service.AddItem(string.Format("Currently connected users: {0}", clientList.Count));
                }
            }
            catch
            {
                endPoint = new IPEndPoint(IPServer, port);
                socketListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
        }

        // Receive client information
        private void ReceiveData(User user)
        {
            Socket client = user.client;
            try
            {
                while (client.Connected)
                {
                    byte[] data = new byte[1024];
                    int bytesRead = client.Receive(data);

                    if (bytesRead == 0)
                    {
                        clientList.Remove(user);
                        client.Close();
                        service.AddItem($"User {user.userName} disconnected. Remaining connected users: {clientList.Count}");
                        break;
                    }

                    if (!client.Connected)
                    {
                        break; 
                    }

                    string receiveString = Encoding.UTF8.GetString(data, 0, bytesRead).Replace("\n", "").Replace("\r", "");

                    if (string.IsNullOrEmpty(receiveString))
                    {
                        continue;
                    }

                    service.AddItem($"Received from [{user.userName}]: {receiveString}");
                    _ = ProcessMessageAsync(user, receiveString); // Process message asynchronously
                }
            } 
            catch (SocketException ex)
            {
                service.AddItem($"Error receiving data from {user.userName}: {ex.Message}");
            }
            finally
            {
                clientList.Remove(user);
                client.Close();
                service.AddItem($"User {user.userName} disconnected. Remaining connected users: {clientList.Count}");
            }
        }

        private async Task ProcessMessageAsync(User user, string receiveString)
        {
            string[] splitString = receiveString.Split(',');
            string sendString;
            string command = splitString[0].ToLower();

            int tableIndex;
            int side;
            int anotherSide;

            switch (command)
            {
                // Connect, format: join, userName
                case "join":
                    if (clientList.Count > maxUser)
                    {
                        sendString = "fullclient";
                        service.SendToOne(user, sendString);
                        service.AddItem("The number of connections is full, refuse " + splitString[1] + " enter the game room");
                    }
                    else
                    {
                        //Save the user's nickname to the user list
                        user.userName = string.Format("{0}", splitString[1]);
                        //Send the status of whether there are people at each table to the user
                        sendString = "Tables," + this.SeatString();
                        service.SendToOne(user, sendString);
                    }
                    break;
                //Exit, format: leave
                case "leave":
                    service.AddItem(string.Format("{0} exit the game room", user.userName));
                    //normalExit = true;
                    //exitWhile = true;
                    break;
                //Sit down, format: SitDown, table number, seat number
                case "sitdown":
                    tableIndex = int.Parse(splitString[1]); // i
                    side = int.Parse(splitString[2]);       // j

                    gameTable[tableIndex].gamePlayer[side].user = user;
                    gameTable[tableIndex].gamePlayer[side].someone = true;
                    service.AddItem(string.Format("{0} is seated at table {1}, seat {2}", user.userName, tableIndex + 1, side + 1));
                    //Get the seat number of the other party
                    anotherSide = (side + 1) % 2;
                    // Determine if the other party is someone
                    if (gameTable[tableIndex].gamePlayer[anotherSide].someone == true)
                    {
                        // Tell the user that the other party is seated
                        // Format: SitDown, seat number, username
                        sendString = string.Format("SitDown,{0},{1}", anotherSide, gameTable[tableIndex].gamePlayer[anotherSide].user.userName);
                        service.SendToOne(user, sendString);
                    }
                    await Task.Delay(100);
                    //Tell both users that the user is seated
                    //Format: SitDown, seat number, username
                    sendString = string.Format("SitDown,{0},{1}", side, user.userName);
                    service.SendToBoth(gameTable[tableIndex], sendString);
                    await Task.Delay(1000);

                    //Send the status of each table in the game room to all users
                    service.SendToAll(clientList, string.Format("Tables,{0}", SeatString()));

                    service.AddItem("------------------------------------");
                    break;
                //Leave seat, format: GetUp, table number, seat number
                case "getup":
                    tableIndex = int.Parse(splitString[1]);
                    side = int.Parse(splitString[2]);
                    anotherSide = (side + 1) % 2;

                    service.AddItem(string.Format("{0} leave seat and return to the game room", user.userName));

                    bool player1Started = gameTable[tableIndex].gamePlayer[0].started;
                    bool player2Started = gameTable[tableIndex].gamePlayer[1].started;

                    //Send the departure information to two users in the format: GetUp, seat number, user name
                    if (player1Started != player2Started || !player1Started || !player2Started)
                    {
                        service.SendToBoth(gameTable[tableIndex], $"GetUp,{side},{user.userName},0");
                    }
                    // both player is started playing
                    if (player1Started && player2Started)
                    {
                        service.SendToBoth(gameTable[tableIndex], $"GetUp,{side},{user.userName},1");
                        gameTable[tableIndex].gamePlayer[anotherSide].started = false;
                    }
                    gameTable[tableIndex].gamePlayer[side].someone = false;
                    gameTable[tableIndex].gamePlayer[side].started = false;

                    //Send the status of each table in the game room to all users
                    service.SendToAll(clientList, "Tables," + this.SeatString());
                    break;
                // Prepare, format: Start, table number, seat number
                case "ready":
                    tableIndex = int.Parse(splitString[1]);
                    side = int.Parse(splitString[2]);
                    gameTable[tableIndex].gamePlayer[side].started = true;
                    anotherSide = (side + 1) % 2;

                    gameTable[tableIndex].gamePlayer[side].score = 0;

                    sendString = $"Message,{user.userName} is ready";
                    service.SendToBoth(gameTable[tableIndex], sendString);

                    await Task.Delay(100);

                    if (gameTable[tableIndex].gamePlayer[anotherSide].started == true)
                    {
                        int Globalseed = new Random().Next(1000);
                        sendString = string.Format("allready, {0}", Globalseed);
                        service.SendToBoth(gameTable[tableIndex], sendString);
                    }
                    break;
                //Receive key signal, format: key, table index, side, key
                case "key":
                    tableIndex = int.Parse(splitString[1]);
                    side = int.Parse(splitString[2]);

                    anotherSide = (side + 1) % 2;
                    sendString = string.Format("Key,{0}", splitString[3]);
                    service.SendToOne(gameTable[tableIndex].gamePlayer[anotherSide].user, sendString);
                    break;
                // Receive lose signal from player then return win signal to another side
                // Receive lose signal, format: lose, table number, seat number
                case "lose":
                    tableIndex = int.Parse(splitString[1]);
                    side = int.Parse(splitString[2]);
                    int score = int.Parse(splitString[3]);
                    anotherSide = (side + 1) % 2;

                    gameTable[tableIndex].gamePlayer[side].score = score;
                    gameTable[tableIndex].gamePlayer[side].started = false;

                    if (gameTable[tableIndex].gamePlayer[anotherSide].started == false)
                    {
                        int scoreP1 = gameTable[tableIndex].gamePlayer[0].score;
                        int scoreP2 = gameTable[tableIndex].gamePlayer[1].score;

                        string nameP1 = gameTable[tableIndex].gamePlayer[0].user.userName;
                        string nameP2 = gameTable[tableIndex].gamePlayer[1].user.userName;

                        if (scoreP1 > scoreP2) sendString = string.Format("winner,0,{0},{1}", nameP1, scoreP1);
                        else if (scoreP1 < scoreP2) sendString = string.Format("winner,1,{0},{1}", nameP2, scoreP2);
                        else sendString = string.Format("winner,2,null,0");

                        service.SendToBoth(gameTable[tableIndex], sendString);
                    } else
                    {
                        sendString = string.Format("message,{0} lose the game. Wait you finish the game!!!", user.userName);
                        service.SendToOne(gameTable[tableIndex].gamePlayer[anotherSide].user, sendString);
                    }
                    break;
                // Receive message from client to client in table, format: message, table index, side, real message
                case "message":
                    tableIndex = int.Parse(splitString[1]);
                    side = int.Parse(splitString[2]);
                    anotherSide = (side + 1) % 2;
                    string mgs = splitString[3];

                    if (gameTable[tableIndex].gamePlayer[anotherSide].someone == true)
                    {
                        sendString = string.Format("message,{0}: {1}", user.userName, mgs);
                        service.SendToOne(gameTable[tableIndex].gamePlayer[anotherSide].user, sendString);
                    }
                    break;
            }
        }
        #endregion

        //Get the string of bit 01 , 0 means there is someone, 1 means no one
        private string SeatString()
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

        // Clear status log
        private void btnClear_Click(object sender, EventArgs e)
        {
            ServerLog_lb.Items.Clear();
        }
    }
}
