using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Playing_Room : Form
    {
        #region Global
        private int TableIndex;
        private int side = 0;
        private NetworkStream netStream = null;
        public GameTetris p1Game;
        public GameTetris p2Game;
        public static Random random;
        #endregion

        public Playing_Room(int tableIndex, int side, NetworkStream netStream)
        {
            InitializeComponent();
            this.TableIndex = tableIndex;
            this.side = side;
            this.netStream = netStream;
            DrawGameRoom();
        }

        // Leave Playing Game Form
        private void TetrisRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            SendToServer(string.Format("GetUp,{0},{1}", TableIndex, side));
        }

        #region Draw Game Form
        // Draw each player plays in own panel side
        private void DrawGameRoom()
        {
            // Set up Multiple Document Interface (MDI) container and window state
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Normal;

            // Set up instance for each side
            p1Game = new GameTetris();
            p2Game = new GameTetris();

            p1Game.TopLevel = false;
            p1Game.FormBorderStyle = FormBorderStyle.None;
            p1Game.StartGame += Player_Ready;
            p1Game.RestartGame += Player_Restart;
            p1Game.GameOver += Player_GameOver;
            p1Game.SendMessage += Player_SendMessage;

            p2Game.TopLevel = false;
            p2Game.FormBorderStyle = FormBorderStyle.None;
            p2Game.StartGame += Player_Ready;
            p2Game.RestartGame += Player_Restart;
            p2Game.GameOver += Player_GameOver;
            p2Game.SendMessage += Player_SendMessage;

            // Configure game windows
            SetupGameWindow(p1Game, pn_p1);
            SetupGameWindow(p2Game, pn_p2);

            // Show game windows
            p1Game.Show();
            p2Game.Show();

            pn_p1.Dock = DockStyle.Left;
            pn_p2.Dock = DockStyle.Right;

            // Determine player layout based on 'side' variable
            if (side == 0) SetupPlayerLayout(p1Game, pn_p1, p2Game, pn_p2);
            else if (side == 1) SetupPlayerLayout(p2Game, pn_p2, p1Game, pn_p1);

            // Set the form size
            this.Size = new Size(pn_p1.Width * 2 + 10, 760);
        }
        // Fit the game in panel
        private void SetupGameWindow(GameTetris game, Panel panel)
        {
            panel.Size = new Size(game.Width, game.Height);
            panel.Controls.Add(game);
            game.Dock = DockStyle.Fill;
        }
        // Setup layout for each side
        private void SetupPlayerLayout(GameTetris activeGame, Panel activePanel, GameTetris inactiveGame, Panel inactivePanel)
        {
            int centerX = (inactivePanel.Left + announcementLabel.Height);
            int centerY = (activePanel.Top + activePanel.Bottom);
            announcementLabel.Location = new Point(centerX, centerY);

            //announcementLabel.Visible = true;

            activePanel.Controls.Add(announcementLabel);
            announcementLabel.BringToFront();
            inactivePanel.Enabled = false;
            inactiveGame.HideControls();
            activeGame.Focus();
        }
        // Only let 1 panel can control the input keys
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (p1Game.isPlayable == false || p2Game.isPlayable == false)
            {
                return false;
            }
            if (side == 0 && p1Game.isTexting == false)
            {
                if (keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Space
                    || keyData == Keys.A || keyData == Keys.S || keyData == Keys.W || keyData == Keys.D)
                {
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    this.Invoke((MethodInvoker)delegate
                    {
                        p1Game.MainWindow_KeyDown(this, e);
                    });
                    SendToServer(string.Format("Key,{0},{1},{2}", TableIndex, side, keyData));
                    return true;
                }
            }
            else if (side == 1 && p2Game.isTexting == false)
            {
                if (keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Space
                    || keyData == Keys.A || keyData == Keys.S || keyData == Keys.W || keyData == Keys.D)
                {
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    this.Invoke((MethodInvoker)delegate
                    {
                        p2Game.MainWindow_KeyDown(this, e);
                    });
                    SendToServer(string.Format("Key,{0},{1},{2}", TableIndex, side, keyData));
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region EventHandler
        // Handle the recevied key from another side
        public void ProcessReceivedKey(Keys keyData)
        {
            if (side == 0)
            {
                // Process key for player 1
                KeyEventArgs e = new KeyEventArgs(keyData);
                p2Game.MainWindow_KeyDown(this, e);
            }
            else if (side == 1)
            {
                // Process key for player 2
                KeyEventArgs e = new KeyEventArgs(keyData);
                p1Game.MainWindow_KeyDown(this, e);
            }
        }
        // Event send message chat
        private string ReadMessage()
        {
            if (side == 0) return p1Game.ReadMessage();
            if (side == 1) return p2Game.ReadMessage();
            return "";
        }
        public void Player_SendMessage(object sender, EventArgs e)
        {
            string mgs = ReadMessage();
            if (mgs == "") return;
            SendToServer(string.Format("Message,{0},{1},{2}",TableIndex,side,mgs));
        }
        // Event enable playbutton
        private void Player_Restart(object sender, EventArgs e)
        {
            p1Game.Enable_PlayButton();
            p2Game.Enable_PlayButton();
        }
        // Send ready signal to server
        private void Player_Ready(object sender, EventArgs e)
        {
            SendToServer(string.Format("Ready,{0},{1}", TableIndex, side));
        }
        // Event when one player game over and send score to server
        public int Get_Score()
        {
            if (side == 0) return p1Game.Get_Score();
            else return p2Game.Get_Score();
        }
        private void Player_GameOver(object sender, EventArgs e)
        {
            GameTetris senderWindow = sender as GameTetris;

            if (senderWindow == p1Game && side == 0)
            {
                p1Game.StopGame();
                SendToServer(string.Format("lose,{0},{1},{2}", TableIndex, side, Get_Score()));
                annouceMgs("YOU LOSE", 0);
            }
            else if (senderWindow == p2Game && side == 1)
            {
                p2Game.StopGame(); 
                SendToServer(string.Format("lose,{0},{1},{2}", TableIndex, side, Get_Score()));
                annouceMgs("YOU LOSE", 1);
            }
        }
        // Begin the game with global seed received from server
        public void GameTetris_StartGame(int GlobalSeed)
        {
            Playing_Room.random = new System.Random(GlobalSeed);
            List<int> sequence = GameTetris.GenerateTetrisSequence(1000);
            p1Game.StartNewGame(sequence);
            p2Game.StartNewGame(sequence);
            this.Focus();
        }
        #endregion

        public void SetName(int side, string name)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate {
                    if (side == 0)
                    {
                        p1Game.SetName(name);
                    }
                    else if (side == 1)
                    {
                        p2Game.SetName(name);
                    }
                });
            }
            else
            {
                if (side == 0)
                {
                    p1Game.SetName(name);
                }
                else if (side == 1)
                {
                    p2Game.SetName(name);
                }
            }
        }
        public void AddMessage(string message)
        {
            if (side == 0)
            {
                p1Game.AddMessage(message);
            }
            else if (side == 1)
            {
                p2Game.AddMessage(message);
            }
        }

        // Annouce lable display for 4 seconds
        public void annouceMgs(string message, int game = 2)
        {
            if (side == 0 || side == 0)
            {
                p1Game.AnnounceLabel(message);
                p1Game.ChangeVisible(true);
            }
            else if (side == 1 || side == 1)
            {
                p2Game.AnnounceLabel(message);
                p2Game.ChangeVisible(true);
            }
            else return;

            if(game == 2)
            announcementTimer.Start();
        }
        private void announcement_Tick(object sender, EventArgs e)
        {
            p1Game.ChangeVisible(false);
            p2Game.ChangeVisible(false);

            announcementTimer.Stop();
        }

        // Send data to server
        public void SendToServer(string str)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(str + "\n");
                netStream.Write(data, 0, data.Length);
                netStream.Flush();
            }
            catch
            {
                Console.WriteLine("Failed to send data");
            }
        }

        // Clear all game panel 
        public void ClearGrid()
        {
            p1Game.ClearGrid();
            p2Game.ClearGrid();
        }
    }
}