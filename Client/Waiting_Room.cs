using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using Tetris;

namespace Client
{
    public partial class Waiting_Room : Form
    {
        private int maxPlayingTables;
        private CheckBox[,] checkBoxGameTables;
        private TcpClient client = null;
        private StreamWriter sw;
        private StreamReader sr;
        private Service service;
        // Form game ở đây
        private TetrisRoom room;

        //Whether to exit the receiving thread normally
        private bool normalExit = false;
        // whether the command is from the server
        private bool isReceiveCommand = false;
        //The seat number of the game table you are sitting on, -1 means not seated, 0 means black, 1 means red
        private int side = -1;
        public Waiting_Room()
        {
            InitializeComponent();
        }

        private void Client_Load(object sender, EventArgs e)
        {
            Random r = new Random((int)DateTime.Now.Ticks);
            UserName_tb.Text = "Player" + r.Next(1, 100);
            maxPlayingTables = 0;
            Local_tb.ReadOnly = true;
            Server_tb.ReadOnly = true;
        }

        private void Connect_btn_Click(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient("127.0.0.1", 8888);
            }
            catch
            {
                MessageBox.Show("Failed to connect to the server", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Local_tb.Text = client.Client.LocalEndPoint.ToString();
            Server_tb.Text = client.Client.RemoteEndPoint.ToString();
            Connect_btn.Enabled = false;
            // get network stream
            NetworkStream netStream = client.GetStream();
            sr = new StreamReader(netStream, System.Text.Encoding.UTF8);
            sw = new StreamWriter(netStream, System.Text.Encoding.UTF8);
            //Get the server table information
            service = new Service(listBox1, sw);
            //Format: Login, nickname
            service.SendToServer("Login," + UserName_tb.Text);
            Thread threadReceive = new Thread(new ThreadStart(ReceiveData));
            threadReceive.Start();
        }
        // process the received data
        private void ReceiveData()
        {
            bool exitWhile = false;
            while (exitWhile == false)
            {
                string receiveString = null;
                try
                {
                    receiveString = sr.ReadLine();
                }
                catch
                {
                    service.AddItemToListBox("Failed to receive data");
                }
                if (receiveString == null)
                {
                    if (normalExit == false)
                    {
                        MessageBox.Show("Lost contact with server, game cannot continue!");
                    }
                    if (side != 1)
                    {
                        //ExitFormPlaying();
                    }
                    side = -1;
                    normalExit = true;
                    break;
                }
                service.AddItemToListBox("Received:" + receiveString);
                string[] splitString = receiveString.Split(',');
                string command = splitString[0].ToLower();
                switch (command)
                {
                    case "tables":
                        string s = splitString[1];
                        //If maxPlayingTables is 0, it means checkBoxGameTables has not been created
                        if (maxPlayingTables == 0)
                        {
                            // count the number of tables
                            maxPlayingTables = s.Length / 2;
                            checkBoxGameTables = new CheckBox[maxPlayingTables, 2];
                            isReceiveCommand = true;
                            //Add the CheckBox object to the array
                            for (int i = 0; i < maxPlayingTables; i++)
                            {
                                AddCheckBoxToPanel(s, i);
                            }
                            isReceiveCommand = false;
                        }
                        else
                        {
                            isReceiveCommand = true;
                            for (int i = 0; i < maxPlayingTables; i++)
                            {
                                for (int j = 0; j < 2; j++)
                                {
                                    if (s[2 * i + j] == '0')
                                    {
                                        UpdateCheckBox(checkBoxGameTables[i, j], false);
                                    }
                                    else
                                    {
                                        UpdateCheckBox(checkBoxGameTables[i, j], true);
                                    }
                                }
                                isReceiveCommand = false;
                            }
                        }
                        break;
                    case "allready":
                        MessageBox.Show("Both sides are ready, the game starts!");
                        room.Invoke((MethodInvoker)delegate
                        {
                            room.GameTetris_StartGame();
                            room.Focus();
                        });
                        break;
                    case "key":
                        Keys keyData = (Keys)Enum.Parse(typeof(Keys), splitString[1]);
                        //MessageBox.Show("nhan tu server");
                        if (room != null)
                        {
                            room.ProcessReceivedKey(keyData);
                        }
                        break;
                }
            }
            Application.Exit();
        }
        delegate void ExitFormPlayingDelegate();
        //exit the game
        delegate void Paneldelegate(string s, int i);
        //Add a game table
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
                label.Location = new Point(10, 15 + i * 30);
                label.Text = string.Format("Table {0}: ", i + 1);
                label.Width = 70;
                this.panel1.Controls.Add(label);
                CreateCheckBox(i, 1, s, "Red");
                CreateCheckBox(i, 0, s, "Black");
            }
        }

        delegate void CheckBoxDelegate(CheckBox checkbox, bool isChecked);
        //Modify the selection state
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
                    checkbox.Enabled = !isChecked;
                }
                else
                {
                    //Already seated, no other tables are allowed
                    checkbox.Enabled = false;
                }
                checkbox.Checked = isChecked;
            }
        }
        // Option to add game table seats
        private void CreateCheckBox(int i, int j, string s, string text)
        {
            int x = j == 0 ? 100 : 200;
            checkBoxGameTables[i, j] = new CheckBox();
            checkBoxGameTables[i, j].Name = string.Format("check{0:0000}{1:0000}", i, j);
            checkBoxGameTables[i, j].Width = 60;
            checkBoxGameTables[i, j].Location = new Point(x, 10 + i * 30);
            checkBoxGameTables[i, j].Text = text;
            checkBoxGameTables[i, j].TextAlign = ContentAlignment.MiddleLeft;
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
            checkBoxGameTables[i, j].CheckedChanged +=
                 new EventHandler(checkBox_CheckedChanged);
        }
        //Triggered when the Checked property of the CheckBox changes
        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            //Whether to update the table status for the server
            if (isReceiveCommand == true)
            {
                return;
            }
            CheckBox checkbox = (CheckBox)sender;
            //If Checked is true, it means that the player sits on the jth table at the ith table
            if (checkbox.Checked == true)
            {
                int i = int.Parse(checkbox.Name.Substring(5, 4));
                int j = int.Parse(checkbox.Name.Substring(9, 4));
                side = j;
                //MessageBox.Show(side.ToString());
                //Format: SitDown, Nickname, Table Number, Seat Number
                service.SendToServer(string.Format("SitDown,{0},{1}", i, j));
                room = new TetrisRoom(i, j, sw);
                room.Show();
                //formPlaying.RePaint();
            }
        }
        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null)
            {
                // Do not allow the player to exit the entire program directly from the game table
                //Only allowed to return to the game room from the game table, and then exit from the game room
                if (side != -1)
                {
                    MessageBox.Show("Please stand up from the game table, return to the game room, and then exit");
                    e.Cancel = true;
                }
                else
                {
                    //When the server stops the service, normalExit is true, otherwise it is false
                    if (normalExit == false)
                    {
                        normalExit = true;
                        service.SendToServer("Logout");
                    }
                    client.Close();
                }
            }
        }
    }
}
