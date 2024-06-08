using System;
using System.Drawing;
using System.Windows.Forms;
using Tetris;
using System.Media;

namespace Client
{
    public partial class Menu : Form
    {
        SoundPlayer music;
        public Menu()
        {
            InitializeComponent();
            music = new SoundPlayer("tetris.wav");
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            txtUserName.BackColor = Color.White;
            this.BackColor = Color.Green;
            music.Load();
            music.PlayLooping();
        }

        private void btnSolo_Click(object sender, EventArgs e)
        {
            string name = txtUserName.Text;
            GameTetris gameTetris = new GameTetris(1,name);
            Hide();
            gameTetris.ShowDialog();
            Show();
        }

        private void btnMulti_Click(object sender, EventArgs e)
        {
            string name = txtUserName.Text;
            Waiting_Room gameTetris = new Waiting_Room(name);
            Hide();
            gameTetris.Show();
            gameTetris.FormClosed += (s, args) => Show();
        }

        // button turn off and turn on music 
        private void btnSound_Click(object sender, EventArgs e)
        {
            if (btnSound.Text == "🔊")
            {
                music.Stop();
                btnSound.Text = "🔈";
            } else if (btnSound.Text == "🔈")
            {
                music.Load();
                music.PlayLooping();
                btnSound.Text = "🔊";
            }
        }
    }
}
