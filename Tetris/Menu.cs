using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btnSolo_Click(object sender, EventArgs e)
        {
            GameTetris game = new GameTetris();
            if (txtName.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên", "Thông báo");
                txtName.Focus();
                return;
            }
            game.PlayerName = txtName.Text;
            game.Show();
        }

        private void btnMulti_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập tên", "Thông báo");
                    txtName.Focus();
                    return;
                }
                if (txtRoom.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập số phòng", "Thông báo");
                    txtRoom.Focus();
                    return;
                }
                MultiPlayer gameCaro = new MultiPlayer();
                gameCaro.p1Game.PlayerName = txtName.Text.Trim();
                gameCaro.Show();
            }
            catch
            {
                MessageBox.Show("Bạn nhập sai số phòng", "Thông báo");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát chương trình không?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
