﻿using Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Playing_Room : Form
    {
        private int TableIndex;
        private int side = 0;
        private NetworkStream netStream = null;
        public GameTetris p1Game;
        public GameTetris p2Game;
        public GameTetris main;
        public static Random random;
        public Playing_Room(int tableIndex, int side, NetworkStream netStream)
        {
            InitializeComponent();
            TableIndex = tableIndex;
            this.side = side;
            DrawGameRoom();
            this.netStream = netStream;
        }
        // Draw game room
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

            p1Game.StartGame += Player_Ready;
            p2Game.StartGame += Player_Ready;

            p1Game.RestartGame += Player_Restart;
            p2Game.RestartGame += Player_Restart;


            pn_p1.Size = new Size(p1Game.Width, p1Game.Height);
            pn_p2.Size = new Size(p2Game.Width, p2Game.Height);

            pn_p1.Controls.Add(p1Game);
            pn_p2.Controls.Add(p2Game);

            pn_p1.Dock = DockStyle.Left;
            pn_p2.Dock = DockStyle.Right;

            p1Game.Show();
            p2Game.Show();

            if (side == 0)
            {
                announcementLabel.Location = new Point(pn_p1.Width / 2 - announcementLabel.Width / 2, pn_p1.Height / 2 - announcementLabel.Height / 2);
                pn_p1.Controls.Add(announcementLabel);
                announcementLabel.BringToFront();
                pn_p2.Enabled = false;
                p2Game.HideControls();
                p1Game.Focus();
                p1Game.GameOver += PlayerWindow_GameOver;
            }
            else if (side == 1)
            {
                announcementLabel.Location = new Point(pn_p1.Width / 2 - announcementLabel.Width / 2, pn_p1.Height / 2 - announcementLabel.Height / 2);
                pn_p2.Controls.Add(announcementLabel);
                announcementLabel.BringToFront();
                pn_p1.Enabled = false;
                p1Game.HideControls();
                p2Game.Focus();
                p2Game.GameOver += PlayerWindow_GameOver;
            }

            this.Size = new Size(pn_p1.Width * 2 + 10, 760);
        }
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

        // Only let 1 panel to control the keys
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (p1Game.isPlayable == false || p2Game.isPlayable == false)
            {
                return false;
            }
            if (side == 0)
            {
                if (keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Space
                    || keyData == Keys.A || keyData == Keys.S || keyData == Keys.W || keyData == Keys.D)
                {
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    p1Game.MainWindow_KeyDown(this, e);
                    SendToServer(string.Format("Key,{0},{1},{2}", TableIndex, side, keyData));
                    return true;
                }
            }
            else if (side == 1)
            {
                if (keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Space
                    || keyData == Keys.A || keyData == Keys.S || keyData == Keys.W || keyData == Keys.D)
                {
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    p2Game.MainWindow_KeyDown(this, e);
                    SendToServer(string.Format("Key,{0},{1},{2}", TableIndex, side, keyData));
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
            p1Game.Enable_Play();
            p2Game.Enable_Play();
        }

        private void Player_Ready(object sender, EventArgs e)
        {
            GameTetris senderWindow = sender as GameTetris;

            if (senderWindow == p1Game)
            {
                SendToServer(string.Format("Ready,{0},{1}", TableIndex, side));
            }
            else
            {
                SendToServer(string.Format("Ready,{0},{1}", TableIndex, side));
            }
        }

        private void PlayerWindow_GameOver(object sender, EventArgs e)
        {
            announcementLabel.Text = "You Lose!!!";
            AddMessage("You Lose!!!");
            announcementLabel.Visible = true;
            announcementTimer.Start();
            GameTetris senderWindow = sender as GameTetris;

            p2Game.StopGame();
            p1Game.StopGame();

            if (senderWindow == p1Game)
            {
                SendToServer(string.Format("lose,{0},{1}", TableIndex, side));
            }
            else
            {
                SendToServer(string.Format("lose,{0},{1}", TableIndex, side));
            }
        }

        public void GameTetris_StartGame(int GlobalSeed)
        {
            Playing_Room.random = new System.Random(GlobalSeed);
            List<int> sequence = GameTetris.GenerateTetrisSequence(1000);
            p1Game.StartNewGame(sequence);
            p2Game.StartNewGame(sequence);
            this.Focus();
        }
        public void annouceWin(string message)
        {
            announcementLabel.Text = message;
            announcementLabel.Visible = true;
            announcementTimer.Start();
        }
        private void announcement_Tick(object sender, EventArgs e)
        {
            announcementLabel.Text = "";
            announcementLabel.Visible = false;
            announcementTimer.Stop();
        }
        private void TetrisRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            SendToServer(string.Format("GetUp,{0},{1}", TableIndex, side));
        }
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

        public void ClearGrid()
        {
            p1Game.ClearGrid();
            p2Game.ClearGrid();
        }
    }
}