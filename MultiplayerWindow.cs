using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    public partial class MultiplayerWindow : Form
    {
        private MainWindow player1Window;
        private MainWindow player2Window;
        private Panel panel1;
        private Panel panel2;

        public MultiplayerWindow()
        {
            InitializeComponent();

            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Normal;
            this.Size = new Size(1100,700 ); // Adjust for window borders and title bar

            player1Window = new MainWindow(1);
            player2Window = new MainWindow(2);

            // Set TopLevel to false to allow adding to a Panel
            player1Window.TopLevel = false;
            player2Window.TopLevel = false;

            // Remove the title bar
            player1Window.FormBorderStyle = FormBorderStyle.None;
            player2Window.FormBorderStyle = FormBorderStyle.None;

            // Add the windows to the panels
            panel1.Controls.Add(player1Window);
            panel2.Controls.Add(player2Window);

            // Show the MainWindow
            player1Window.Show();
            player2Window.Show();
        }
    }
}
