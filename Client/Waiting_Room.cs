using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using Tetris;
using System.Threading;
using System.Linq;

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
            UserName_tb.ReadOnly = true;
        }
        // process the received data
        private async void ReceiveData()
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
                        MessageBox.Show("Khong the ket noi toi server");
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
                        receiveTable = true;
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
                        receiveTable = false;
                        break;
                    case "sitdown": //sitdown,side, user name
                        int Receive_side_need_update_name = int.Parse(splitString[1]);
                        string name = splitString[2];
                        while (!Complete_create_game_room)
                        {
                            Thread.Sleep(100); // Chờ 0.1 giây
                        }
                        room.SetName(Receive_side_need_update_name, name);
                        break;
                    case "allready":
                        room.Invoke((MethodInvoker)delegate
                        {
                            int Globalseed = int.Parse(splitString[1]);
                            room.AddMessage("Both sides are ready, the game starts!");
                            room.GameTetris_StartGame(Globalseed);
                            room.Focus();
                        });
                        break;
                    // leave seat
                    case "getup":
                        if (side == int.Parse(splitString[1]))
                        {
                            side = -1;
                            Complete_create_game_room = false;
                            this.Invoke((MethodInvoker)delegate
                            {
                                button_play.Enabled = true;
                            });
                        }
                        else
                        {
                            room.Invoke((MethodInvoker)delegate
                            {
                                room.annouceWin("You Win!!!");
                                room.AddMessage("Enemy escape, You Win!!!");
                                room.p1Game.StopGame();
                                room.p2Game.StopGame();
                            });
                        }
                        break;
                    case "key":
                        Keys keyData = (Keys)Enum.Parse(typeof(Keys), splitString[1]);
                        if (room != null)
                        {
                            room.ProcessReceivedKey(keyData);
                        }
                        break;
                    case "message":
                        room.Invoke((MethodInvoker)delegate
                        {
                            room.AddMessage(splitString[1]);
                        });
                        break;
                    case "win":
                        room.Invoke((MethodInvoker)delegate
                        {
                            room.annouceWin("You Win!!!");
                            room.AddMessage("You Win!!!");
                            if (side == 0)
                            {
                                room.p1Game.StopGame();
                            }
                            else if (side == 1)
                            {
                                room.p2Game.StopGame();
                            }
                        });
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

        private bool receiveTable = false;
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
            checkBoxGameTables[i, j].CheckedChanged += new EventHandler(checkBox_CheckedChanged);
        }
        //Triggered when the Checked property of the CheckBox changes
        public bool Complete_create_game_room = false;
        // Modify the title of the room to the table number
        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            // Whether to update the table status for the server
            if (isReceiveCommand == true)
            {
                return;
            }
            CheckBox checkbox = (CheckBox)sender;
            // If Checked is true, it means that the player sits on the jth table at the ith table
            if (checkbox.Checked == true)
            {
                int i = int.Parse(checkbox.Name.Substring(5, 4)); // TabeIndex
                int j = int.Parse(checkbox.Name.Substring(9, 4)); // side
                side = j;

                if (receiveTable == false)
                {
                    // Format: SitDown, Nickname, Table Number, Side,
                    service.SendToServer(string.Format("SitDown,{0},{1}", i, j));
                    room = new TetrisRoom(i, j, sw);
                    room.Text = "Table " + (i + 1);
                    room.Show();
                    Complete_create_game_room = true;
                }

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
        // tìm phòng trong textbox làm hiển thị trong panel
        private void button_find_Click(object sender, EventArgs e)
        {
            try
            {
                int tableIndex = int.Parse(textbox_tableindex.Text);
                if (tableIndex >= 1 && tableIndex <= maxPlayingTables)
                {
                    //table index tính từ 0 nên -1
                    tableIndex -= 1;
                    CheckBox targetCheckBox = checkBoxGameTables[tableIndex, 0];
                    panel1.ScrollControlIntoView(targetCheckBox);
                }
                else
                {
                    MessageBox.Show("Không có bàn này", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Vui lòng nhập một số hợp lệ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_play_Click(object sender, EventArgs e)
        {
            // tìm bàn đầu tiên có chỗ trống
            // so sánh 2 checkbox nếu khác nhau thì có nghĩa là 1 trống 1 đầy.
            for (int i = 0; i < maxPlayingTables; i++)
            {
                if (checkBoxGameTables[i, 0].Checked != checkBoxGameTables[i, 1].Checked)
                {
                    //ngồi tai vị trí hợp lệ
                    checkBoxGameTables[i, checkBoxGameTables[i, 0].Checked ? 1 : 0].Checked = true;
                    button_play.Enabled = false;
                    return;
                }
            }
            //nếu không có bàn nào có 1 người ngồi thì vào vị trí hợp lệ đầu tiên (không ngồi đè)
            for (int i = 0; i < maxPlayingTables; i++)
            {
                if (!checkBoxGameTables[i, 0].Checked || !checkBoxGameTables[i, 1].Checked)
                {
                    checkBoxGameTables[i, checkBoxGameTables[i, 0].Checked ? 1 : 0].Checked = true;
                    button_play.Enabled = false;
                    return;
                }
            }
            // nếu đầy hết thì thông báo
            MessageBox.Show("Không tìm được bàn phù hợp", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



    }
}
