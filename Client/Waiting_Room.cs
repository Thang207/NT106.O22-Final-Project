using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Tetris;

namespace Client
{
    public partial class Waiting_Room : Form
    {
        #region Global
        private int maxPlayingTables;
        private string username = "Player";

        private CheckBox[,] checkBoxGameTables;
        private Playing_Room room;

        private NetworkStream netStream = null;
        private IPEndPoint ipEP = null;
        private Socket client = null;

        private bool isConnected = false;
        // The seat number of the game table you are sitting on, -1 means not seated
        private int side = -1;
        #endregion

        public Waiting_Room(string name)
        {
            InitializeComponent();
            this.username = name;
            lbUserName.Text += name;
        }
            
        #region Client
        // Form load and start connecting to server
        private void Client_Load(object sender, EventArgs e)
        {
            btnConnect.PerformClick();
            btnConnect.Visible = false;
            maxPlayingTables = 0;
        }
        // Close form and close connect 
        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null && netStream != null)
            {
                SendToServer("Leave");
                this.Invoke((MethodInvoker)delegate
                {
                    client.Close();
                    netStream.Close();
                });
            }
        }
        // Return button out connect 
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Button Connect to server
        private void btnConnect_Click(object sender, EventArgs e)
        {
            ipEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                try
                {
                    client.Connect(ipEP);
                    isConnected = true;
                }
                catch 
                {
                    Console.WriteLine($"Failed to connect to the server.");
                    return;
                }

                netStream = new NetworkStream(client);
                SendToServer("Join," + this.username);

                Thread threadReceive = new Thread(new ThreadStart(ReceiveData));
                threadReceive.IsBackground = true;
                threadReceive.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to connect to the server: {ex.Message}");
            }
        }
        // Send data to Server
        private void SendToServer(string str)
        {
            if (!isConnected)
            {
                return;
            }
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(str);
                netStream.Write(data, 0, data.Length);
                netStream.Flush();
            }
            catch
            {
                Console.WriteLine("Failed to send data");
            }
        }
        // Receive command data from server
        private void ReceiveData()
        {
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024];
                    string receiveString = null;

                    int numBytesRead = client.Receive(data);

                    if (numBytesRead == 0)
                    {
                        client.Close();
                        return;
                    }

                    receiveString = Encoding.UTF8.GetString(data, 0, numBytesRead);

                    if(receiveString == null)
                    {
                        side = -1;
                        break;
                    }

                    string[] splitString = receiveString.Split(',');
                    string command = splitString[0].ToLower();

                    // Receive table, format: tables, side, seat string
                    if (command == "tables")
                    {
                        receiveTable = true;
                        string seatString = splitString[1];
                        if (maxPlayingTables == 0) // First time receive table
                        {
                            // 2 bit is one table, ex: 101010 means 3 table
                            maxPlayingTables = seatString.Length / 2;
                            checkBoxGameTables = new CheckBox[maxPlayingTables, 2];
                            for (int i = 0; i < maxPlayingTables; i++)
                            {
                                AddCheckBoxToPanel(seatString, i);
                            }
                        }
                        else 
                        {
                            for (int i = 0; i < maxPlayingTables; i++)
                            {
                                for (int j = 0; j < 2; j++)
                                {
                                    if (seatString[2 * i + j] == '0')
                                    {
                                        UpdateCheckBox(checkBoxGameTables[i, j], false);
                                    }
                                    else
                                    {
                                        UpdateCheckBox(checkBoxGameTables[i, j], true);
                                    }
                                }
                            }
                        }
                        receiveTable = false;
                    }
                    // Receive sitdown info, format: sitdown, other side, other name 
                    else if (command == "sitdown")
                    {
                        int side = int.Parse(splitString[1]);
                        string name = splitString[2];

                        while (!Complete_create_game_room)
                        {
                            Thread.Sleep(100); // Chờ 0.1 giây
                        }

                        room.SetName(side, name);
                        room.AddMessage($"{name} enter room at position {side + 1}");
                        room.ClearGrid();
                    }
                    // Receive full client, format: fullclient
                    else if (command == "fullclient")
                    {
                        MessageBox.Show("Server is full");
                    }
                    // Receive 2 side are ready signal, format: allready, global seed game
                    else if (command == "allready")
                    {
                        room.Invoke((MethodInvoker)delegate
                        {
                            int Globalseed = int.Parse(splitString[1]);
                            room.GameTetris_StartGame(Globalseed);
                            room.AddMessage("Both side is ready, let's start the game!!!");
                            room.Focus();
                        });
                    }
                    // Receive leave seat, format: getup, side, username, is playing
                    else if (command == "getup")
                    {
                        if (side == int.Parse(splitString[1]))
                        {
                            side = -1;
                            Complete_create_game_room = false;
                            this.Invoke((MethodInvoker)delegate
                            {
                                btnQuickPlay.Enabled = true;
                            });
                        }
                        else
                        {
                            int getup_side = int.Parse(splitString[1]);
                            string userName = splitString[2];
                            int isplaying = int.Parse(splitString[3]);

                            if (isplaying == 1)
                            {
                                room.Invoke((MethodInvoker)delegate
                                {
                                    while (!Complete_create_game_room)
                                    {
                                        Thread.Sleep(100); // Chờ 0.1 giây
                                    }
                                    room.SetName(getup_side, "");
                                    room.AddMessage("Enemy escape, You Win!!!");
                                    room.annouceMgs("You Win!!!");
                                    room.p1Game.StopGame();
                                    room.p2Game.StopGame();
                                    room.p1Game.Enable_PlayButton();
                                    room.p2Game.Enable_PlayButton();
                                });
                            }
                            else
                            {
                                room.Invoke((MethodInvoker)delegate
                                {
                                    while (!Complete_create_game_room)
                                    {
                                        Thread.Sleep(100); // Chờ 0.1 giây
                                    }
                                    room.SetName(getup_side, "");
                                    room.AddMessage("Enemy escape!!!");
                                });
                            }
                        }
                    }
                    // Receive key data, format: key, key data
                    else if (command == "key")
                    {
                        try
                        {
                            Keys keyData = (Keys)Enum.Parse(typeof(Keys), splitString[1]);

                            room?.ProcessReceivedKey(keyData);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error parsing key data: " + ex.Message);
                        }
                    }
                    // Receive chat from server, format: message, message
                    else if (command == "message")
                    {
                        room.Invoke((MethodInvoker)delegate
                        {
                            room.AddMessage(splitString[1]);
                        });
                    }
                    // Receive winner, format: winner, side, username, winner's score
                    else if (command == "winner")
                    {
                        int sideWinner = int.Parse(splitString[1]);
                        string nameWinner = splitString[2];
                        int score = int.Parse(splitString[3]);

                        room.Invoke((MethodInvoker)delegate
                        {
                            if (sideWinner == 0 || sideWinner == 1)
                            {
                                room.annouceMgs(nameWinner + " WIN!!!" + Environment.NewLine + $"Score: {score}");
                                room.AddMessage(nameWinner + $" win with {score} points.");
                            }
                            else if (sideWinner == 2)
                            {
                                room.annouceMgs("Draw!!!");
                                room.AddMessage("Both player have the same score so Draw!!!");
                            }
                            else
                            {
                                room.annouceMgs("I DON'T KNOW WHO WIN :>");
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion

        #region Create CheckBox
        // Add a game table and seat
        delegate void Paneldelegate(string s, int i);
        private void AddCheckBoxToPanel(string s, int i)
        {
            if (panel1.InvokeRequired == true)
            {
                Paneldelegate d = AddCheckBoxToPanel;
                this.Invoke(d, s, i);
            }
            else
            {
                Label label = new Label();
                label.Location = new Point(10, 15 + i * 50);
                label.Text = string.Format("Table {0}: ", i + 1);
                label.Width = 90;
                label.Font = new Font("Arial", 12, FontStyle.Bold);

                this.panel1.Controls.Add(label);
                CreateCheckBox(i, 0, s, "P1");
                CreateCheckBox(i, 1, s, "P2");
            }
        }
        private bool receiveTable = false;
        delegate void CheckBoxDelegate(CheckBox checkbox, bool isChecked);
        // Modify the selection state
        private void UpdateCheckBox(CheckBox checkbox, bool isChecked)
        {
            if (checkbox.InvokeRequired == true)
            {
                CheckBoxDelegate d = UpdateCheckBox;
                this.Invoke(d, checkbox, isChecked);
            }
            else
            {
                if (side == -1)
                {
                    if (isChecked == true)
                    {
                        checkbox.Checked = true;
                        checkbox.Enabled = false;
                    }
                    else
                    {
                        checkbox.Checked = false;
                        checkbox.Enabled = true;
                    }
                }
                else
                {
                    //Already seated, no other tables are allowed
                    checkbox.Enabled = false;
                    if (isChecked == true)
                        checkbox.Checked = true;
                    else
                        checkbox.Checked = false;
                }
            }
        }
        // Option to add game table seats
        private void CreateCheckBox(int i, int j, string s, string text)
        {
            int x = j == 0 ? 100 : 200;
            checkBoxGameTables[i, j] = new CheckBox();
            checkBoxGameTables[i, j].Name = string.Format("check{0:0000}{1:0000}", i, j);
            checkBoxGameTables[i, j].Width = 60;
            checkBoxGameTables[i, j].Location = new Point(x + 10, 10 + i * 50);
            checkBoxGameTables[i, j].Text = text;
            checkBoxGameTables[i, j].TextAlign = ContentAlignment.MiddleCenter;
            checkBoxGameTables[i, j].FlatAppearance.BorderSize = 1;
            checkBoxGameTables[i, j].BackColor = SystemColors.Control;
            checkBoxGameTables[i, j].FlatAppearance.CheckedBackColor = Color.FromArgb(150, 147, 0, 255);
            checkBoxGameTables[i, j].FlatStyle = FlatStyle.Flat;
            checkBoxGameTables[i, j].Appearance = Appearance.Button;
            checkBoxGameTables[i, j].Font = new Font("Arial", 11, FontStyle.Bold);

            if (s[2 * i + j] == '1')
            {
                //1 means someone
                checkBoxGameTables[i, j].Enabled = false;
                checkBoxGameTables[i, j].Checked = true;
            }
            else
            {
                //0 means no one
                checkBoxGameTables[i, j].Enabled = true;
                checkBoxGameTables[i, j].Checked = false;
            }
            this.panel1.Controls.Add(checkBoxGameTables[i, j]);
            checkBoxGameTables[i, j].CheckedChanged += new EventHandler(checkBox_CheckedChanged);
        }

        public bool Complete_create_game_room = false;
        // Modify the title of the room to the table number
        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            // Whether to update the table status for the server
            if (receiveTable == true)
            {
                return;
            }
            CheckBox checkbox = (CheckBox)sender;
            // If Checked is true, it means that the player sits on the jth table at the ith table
            if (checkbox.Checked == true)
            {
                if (receiveTable == false)
                {
                    int i = int.Parse(checkbox.Name.Substring(5, 4)); // TabeIndex
                    int j = int.Parse(checkbox.Name.Substring(9, 4)); // side
                    side = j;
                    // Format: SitDown, Nickname, Table Number, Side,
                    SendToServer(string.Format("SitDown,{0},{1}", i, j));
                    room = new Playing_Room(i, j, netStream);

                    checkbox.ForeColor = Color.Black;

                    btnQuickPlay.Enabled = false;
                    Complete_create_game_room = true;
                    Hide();
                    room.Show();
                    room.Text = "Table " + (i + 1);
                    room.FormClosed += (s, args) => Show();
                }
            }
        }
        #endregion

        // Quickly choose seat, priority to table has someone
        private void btnQuickPlay_Click(object sender, EventArgs e)
        {
            // Compare 2 seats, if they are different, 1 is empty and the other is filled
            for (int i = 0; i < maxPlayingTables; i++)
            {
                if (checkBoxGameTables[i, 0].Checked != checkBoxGameTables[i, 1].Checked)
                {
                    // Choose enable seat
                    checkBoxGameTables[i, checkBoxGameTables[i, 0].Checked ? 1 : 0].Checked = true;
                    btnQuickPlay.Enabled = false;
                    return;
                }
            }
            // If there is no one, then sit down at first seat (not on top)
            for (int i = 0; i < maxPlayingTables; i++)
            {
                if (!checkBoxGameTables[i, 0].Checked || !checkBoxGameTables[i, 1].Checked)
                {
                    checkBoxGameTables[i, checkBoxGameTables[i, 0].Checked ? 1 : 0].Checked = true;
                    btnQuickPlay.Enabled = false;
                    return;
                }
            }
            // if full, leave a messagebox
            MessageBox.Show("Không tìm được bàn phù hợp", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
