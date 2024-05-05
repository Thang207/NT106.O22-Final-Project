using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Client
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            txtUserName.BackColor = Color.White;
            this.BackColor = Color.Green;
            //this.Padding = new Padding(5);
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
    }
}
