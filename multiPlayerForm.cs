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
    public partial class multiPlayerForm : Form
    {
        // Declare the MainWindow variables
        private MainWindow player1Window;
        private MainWindow player2Window;

        public multiPlayerForm()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Normal;
            this.Size = new Size(1100, 700); // Adjust for window borders and title bar

            player1Window = new MainWindow(1);
            player2Window = new MainWindow(2);

            // Set TopLevel to false to allow adding to a Panel
            player1Window.TopLevel = false;
            player2Window.TopLevel = false;

            // Remove the title bar
            player1Window.FormBorderStyle = FormBorderStyle.None;
            player2Window.FormBorderStyle = FormBorderStyle.None;

            // Set the location of player2Window
            player2Window.Location = new Point(560, 0);

            // Add the windows to the panels
            this.Controls.Add(player1Window);
            this.Controls.Add(player2Window);

            // Show the MainWindow
            player1Window.Show();
            player2Window.Show();

            player1Window.Activate();

        }
    }
}
