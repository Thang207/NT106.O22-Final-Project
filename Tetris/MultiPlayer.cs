using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    public partial class MultiPlayer : Form
    {
        public GameTetris p1Game;
        public GameTetris p2Game;

        public MultiPlayer()
        {
            InitializeComponent();

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

            p1Game.StartGame += GameTetris_StartGame;

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

            pn_p2.Enabled = false;
            gb_p2.Enabled = false;

            this.Size = new Size(gb_p1.Width * 2 + 10, 760);
        }

        // Chỉ cho panel 1 có thể nhấn phím
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Space
                || keyData == Keys.A || keyData == Keys.S || keyData == Keys.W || keyData == Keys.D)
            {
                KeyEventArgs e = new KeyEventArgs(keyData);
                p1Game.MainWindow_KeyDown(this, e);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
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
        private void GameTetris_StartGame(object sender, EventArgs e)
        {
            p2Game.btnPlay_Click(this, e);
        }
    }
}
