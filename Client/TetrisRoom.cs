using Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Tetris
{
    public partial class TetrisRoom : Form
    {
        public static int GlobalSeed { get; set; }
        public static System.Random random = new System.Random(GlobalSeed);
        private int TableIndex;
        private int side = 0;

        public GameTetris p1Game;
        public GameTetris p2Game;
        public GameTetris main;
        private Service service;

        public TetrisRoom(int tableIndex, int side, StreamWriter sw)
        {
            InitializeComponent();
            TableIndex = tableIndex;
            this.side = side;
            DrawGameRoom();
            service = new Service(null, sw);
        }

        private void DrawGameRoom()
        {
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Normal;

            p1Game = new GameTetris();
            p2Game = new GameTetris();

            p1Game.TopLevel = false;
            p2Game.TopLevel = false;

            p1Game.FormBorderStyle = FormBorderStyle.None;
            p2Game.FormBorderStyle = FormBorderStyle.None;

            p1Game.GameOver += PlayerWindow_GameOver;
            p2Game.GameOver += PlayerWindow_GameOver;

            p1Game.StartGame += Player_Ready;
            p2Game.StartGame += Player_Ready;

            p1Game.RestartGame += Player_Restart;
            p2Game.RestartGame += Player_Restart;


            pn_p1.Size = new Size(p1Game.Width, p1Game.Height);
            pn_p2.Size = new Size(p2Game.Width, p2Game.Height);

            pn_p1.Controls.Add(p1Game);
            pn_p2.Controls.Add(p2Game);

            gb_p1.Size = new Size(pn_p1.Width, pn_p2.Height);
            gb_p2.Size = new Size(pn_p2.Width, pn_p2.Height);

            pn_p1.Dock = DockStyle.Fill;
            pn_p2.Dock = DockStyle.Fill;

            gb_p1.Dock = DockStyle.Left;
            gb_p2.Dock = DockStyle.Right;

            p1Game.Show();
            p2Game.Show();

            if (side == 0)
            {
                pn_p2.Enabled = false;
                gb_p2.Enabled = false;
                p2Game.HideListView();
                p1Game.Focus();
            } else if (side == 1)
            {
                pn_p1.Enabled = false;
                gb_p1.Enabled = false;
                p1Game.HideListView();
                p2Game.Focus();
            }

            this.Size = new Size(gb_p1.Width * 2 + 10, 760);
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

        // Chỉ cho panel 1 có thể nhấn phím
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (side == 0)
            {
                if (keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Space
                    || keyData == Keys.A || keyData == Keys.S || keyData == Keys.W || keyData == Keys.D || keyData == Keys.Shift)
                {
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    p1Game.MainWindow_KeyDown(this, e);
                    service.SendToServer(string.Format("Key,{0},{1},{2}", TableIndex, side, keyData));
                    return true;
                }
            }
            else if (side == 1)
            {
                if (keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Space
                    || keyData == Keys.A || keyData == Keys.S || keyData == Keys.W || keyData == Keys.D || keyData == Keys.Shift)
                {
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    p2Game.MainWindow_KeyDown(this, e);
                    service.SendToServer(string.Format("Key,{0},{1},{2}", TableIndex, side, keyData));
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

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

        private void Player_Restart(object sender, EventArgs e)
        {
            GameTetris senderWindow = sender as GameTetris;
            p1Game.Enable_Play();
            p2Game.Enable_Play();
        }

        private void Player_Ready(object sender, EventArgs e)
        {
             GameTetris senderWindow = sender as GameTetris;

            if (senderWindow == p1Game)
            {
                service.SendToServer(string.Format("Start,{0},{1}", TableIndex, side));
            }
            else
            {
                service.SendToServer(string.Format("Start,{0},{1}", TableIndex, side));
            }
        }

        private void PlayerWindow_GameOver(object sender, EventArgs e)
        {
            GameTetris senderWindow = sender as GameTetris;
            p2Game.StopGame();
            p1Game.StopGame();

            if (senderWindow == p1Game)
            {
                service.SendToServer(string.Format("Win,{0},{1}", TableIndex, side));
            }
            else
            {
                service.SendToServer(string.Format("Win,{0},{1}", TableIndex, side));
            }
        }
        public void GameTetris_StartGame()
        {
            TetrisRoom.random = new System.Random(GlobalSeed);
            List<int> sequence = GameTetris.GenerateTetrisSequence(1000);  
            p1Game.StartNewGame(sequence);
            p2Game.StartNewGame(sequence);
            this.Focus();
        }


        private void TetrisRoom_Load(object sender, EventArgs e)
        {

        }

        private void TetrisRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            service.SendToServer(string.Format("GetUp,{0},{1}", TableIndex, side));
        }
    }
}
