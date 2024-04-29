﻿using Client;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Tetris
{
    public partial class TetrisRoom : Form
    {
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
            } else if (side == 1)
            {
                pn_p1.Enabled = false;
                gb_p1.Enabled = false;
            }

            this.Size = new Size(gb_p1.Width * 2 + 10, 760);
        }

        // Chỉ cho panel 1 có thể nhấn phím
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (side == 0)
            {
                if (keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Space
                    || keyData == Keys.A || keyData == Keys.S || keyData == Keys.W || keyData == Keys.D)
                {
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    p1Game.MainWindow_KeyDown(this, e);
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
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
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
                MessageBox.Show("Player 2 wins!");
            }
            else
            {
                MessageBox.Show("Player 1 wins!");
            }
        }

        public void GameTetris_StartGame()
        {
            p1Game.StartNewGame();
            p2Game.StartNewGame();
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
