using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Security.Policy;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Tetris
{
    public partial class GameTetris : Form
    {
        #region Global 
        // Initialize global variables
        Control[] activePiece = { null, null, null, null };
        Control[] activePiece2 = { null, null, null, null };
        Control[] nextPiece = { null, null, null, null };
        Control[] Ghost = { null, null, null, null };
        List<int> PieceSequence = new List<int>();
        int timeElapsed = 0;
        int currentPiece;
        int nextPieceInt;
        int rotations = 0;
        int combo = 0;
        int score = 0;
        int clears = 0;
        int level = 0;
        bool gameOver = false;
        bool isPaused = false;
        int PieceSequenceIteration = 0;

        public bool isTexting = false;
        public bool isPlayable = false;

        private delegate void AddMessageDelegate(string str);
        private AddMessageDelegate addMessageDelegate;

        readonly Color[] colorList =
        {
            Color.FromArgb(0, 236, 250),     // I piece - cyan
            Color.FromArgb(255, 151, 28),    // L piece - orange
            Color.FromArgb(3, 65, 174),     // J piece - blue
            Color.FromArgb(114, 203, 59),     // S piece - green
            Color.FromArgb(255, 50, 19),     // Z piece - red
            Color.FromArgb(255, 213, 0),     // O piece - yellow 
            Color.FromArgb(221, 10, 178)     // T piece - purple
        };
        #endregion

        private int mode;

        public event EventHandler StartGame;
        public event EventHandler GameOver;
        public event EventHandler RestartGame;
        public event EventHandler SendMessage;


        // Load main window
        public GameTetris(int mode = 2, string username = null)
        {
            InitializeComponent();
            this.mode = mode;
            lbUserName.Text = username;
            ScoreUpdateLabel.Text = "";
            // Initialize/reset ghost piece
            // box1 through box4 are invisible
            activePiece2[0] = box1;
            activePiece2[1] = box2;
            activePiece2[2] = box3;
            activePiece2[3] = box4;
            grid.Focus();

            addMessageDelegate = new AddMessageDelegate(AddMessage);

            if (this.mode == 1)
            {
                rtbChatZone.Visible = false;
                btnPlay.Text = "Play";
                btnSendMessage.Visible = false;
                txtChat.Visible = false;
            }
        }

        #region Methods
        public void DropNewPiece()
        {
            // Reset number of times current piece has been rotated
            rotations = 0;

            // Move next piece to current piece
            currentPiece = nextPieceInt;

            // If last piece of PieceSequence, reset PieceSequenceIteration
            if (PieceSequenceIteration == PieceSequence.Count)
            {
                PieceSequenceIteration = 0;
            }

            // Select next piece from PieceSequence
            nextPieceInt = PieceSequence[PieceSequenceIteration];
            PieceSequenceIteration++;


            // If not first move, clear next piece panel
            if (nextPiece.Contains(null) == false)
            {
                foreach (Control x in nextPiece)
                {
                    x.BackColor = Color.White;
                }
            }

            // Layout options for next piece
            Control[,] nextPieceArray =
            {
                { box203, box207, box211, box215 }, // I piece
                { box202, box206, box210, box211 }, // L piece
                { box203, box207, box211, box210 }, // J piece
                { box206, box207, box203, box204 }, // S piece
                { box202, box203, box207, box208 }, // Z piece
                { box206, box207, box210, box211 }, // O piece
                { box207, box210, box211, box212 }  // T piece
            };

            // Retrieve layout for next piece
            for (int x = 0; x < 4; x++)
            {
                nextPiece[x] = nextPieceArray[nextPieceInt, x];
            }

            // Populate next piece panel with correct color
            foreach (Control square in nextPiece)
            {
                square.BackColor = colorList[nextPieceInt];
            }

            // Layout options for falling piece
            Control[,] activePieceArray =
            {
                { box6, box16, box26, box36 }, // I piece
                { box4, box14, box24, box25 }, // L piece
                { box5, box15, box25, box24 }, // J piece
                { box14, box15, box5, box6 },  // S piece
                { box5, box6, box16, box17 },  // Z piece
                { box5, box6, box15, box16 },  // O piece
                { box6, box15, box16, box17 }  // T piece
            };

            // Select falling piece
            for (int x = 0; x < 4; x++)
            {
                activePiece[x] = activePieceArray[currentPiece, x];
            }

            // This is needed for DrawGhost()
            for (int x = 0; x < 4; x++)
            {
                activePiece2[x] = activePieceArray[currentPiece, x];
            }

            // Check for game over
            foreach (Control box in activePiece)
            {
                if (box.BackColor != Color.White && box.BackColor != Color.LightGray)
                {
                    //Game over!
                    SpeedTimer.Stop();
                    GameTimer.Stop();
                    gameOver = true;
                    GameOver?.Invoke(this, EventArgs.Empty);

                    // Handle to restart new game
                    gameOver = false;
                    isPaused = false;
                    btnPlay.Enabled = true;
                    RestartGame?.Invoke(this, EventArgs.Empty);

                    if (this.mode == 1)
                    {
                        lbInfo.BringToFront();
                        lbInfo.Visible = true;
                        lbInfo.Text = "YOU LOSE!!!" + Environment.NewLine + $"Score: {Get_Score()}";
                        isPlayable = false;
                    }

                    return;
                }
            }

            // Draw ghost piece
            DrawGhost();

            // Populate falling piece squares with correct color
            foreach (Control square in activePiece)
            {
                square.BackColor = colorList[currentPiece];
            }
        }

        // Test if a potential move (left/right/down) would be outside the grid or overlap another piece
        public bool TestMove(string direction)
        {
            if (!isPlayable) return false;

            int currentHighRow = 21;
            int currentLowRow = 0;
            int currentLeftCol = 9;
            int currentRightCol = 0;

            int nextSquare = 0;

            Control newSquare = new Control();

            // Determine highest, lowest, left, and right rows of potential move
            foreach (Control square in activePiece)
            {
                if (grid.GetRow(square) < currentHighRow)
                {
                    currentHighRow = grid.GetRow(square);
                }
                if (grid.GetRow(square) > currentLowRow)
                {
                    currentLowRow = grid.GetRow(square);
                }
                if (grid.GetColumn(square) < currentLeftCol)
                {
                    currentLeftCol = grid.GetColumn(square);
                }
                if (grid.GetColumn(square) > currentRightCol)
                {
                    currentRightCol = grid.GetColumn(square);
                }
            }

            // Test if any squares would be outside of grid
            foreach (Control square in activePiece)
            {
                int squareRow = grid.GetRow(square);
                int squareCol = grid.GetColumn(square);

                // Left
                if (direction == "left" & squareCol > 0)
                {
                    newSquare = grid.GetControlFromPosition(squareCol - 1, squareRow);
                    nextSquare = currentLeftCol;
                }
                else if (direction == "left" & squareCol == 0)
                {
                    // Move would be outside of grid, left
                    return false;
                }

                // Right
                else if (direction == "right" & squareCol < 9)
                {
                    newSquare = grid.GetControlFromPosition(squareCol + 1, squareRow);
                    nextSquare = currentRightCol;
                }
                else if (direction == "right" & squareCol == 9)
                {
                    // Move would be outside of grid, right
                    return false;
                }

                // Down
                else if (direction == "down" & squareRow < 21)
                {
                    newSquare = grid.GetControlFromPosition(squareCol, squareRow + 1);
                    nextSquare = currentLowRow;
                }
                else if (direction == "down" & squareRow == 21)
                {
                    return false;
                    // Move would be below grid
                }

                // Test if potential move would overlap another piece
                if ((newSquare.BackColor != Color.White & newSquare.BackColor != Color.LightGray) & activePiece.Contains(newSquare) == false & nextSquare > 0)
                {
                    return false;
                }

            }

            // All tests passed
            return true;
        }

        public void MovePiece(string direction)
        {
            // Erase old position of piece
            // and determine new position based on input direction
            int x = 0;
            foreach (PictureBox square in activePiece)
            {
                square.BackColor = Color.White;
                int squareRow = grid.GetRow(square);
                int squareCol = grid.GetColumn(square);
                int newSquareRow = 0;
                int newSquareCol = 0;
                if (direction == "left")
                {
                    newSquareCol = squareCol - 1;
                    newSquareRow = squareRow;
                }
                else if (direction == "right")
                {
                    newSquareCol = squareCol + 1;
                    newSquareRow = squareRow;
                }
                else if (direction == "down")
                {
                    newSquareCol = squareCol;
                    newSquareRow = squareRow + 1;
                }

                activePiece2[x] = grid.GetControlFromPosition(newSquareCol, newSquareRow);
                x++;
            }

            // Copy activePiece2 to activePiece
            x = 0;
            foreach (PictureBox square in activePiece2)
            {
                activePiece[x] = square;
                x++;
            }

            // Draw ghost piece (must be between erasing old position and drawing new position)
            DrawGhost();

            // Draw piece in new position
            x = 0;
            foreach (PictureBox square in activePiece2)
            {
                square.BackColor = colorList[currentPiece];
                x++;
            }
        }

        // Test if a potential rotation would overlap another piece
        private bool TestOverlap()
        {
            foreach (PictureBox square in activePiece2)
            {
                if ((square.BackColor != Color.White && square.BackColor != Color.LightGray) & activePiece.Contains(square) == false)
                {
                    return false;
                }
            }
            return true;
        }

        // Timer for piece movement speed - increases with game level
        // Speed is controlled by LevelUp() method
        private void SpeedTimer_Tick(object sender, EventArgs e)
        {
            if (CheckGameOver() == true)
            {
                SpeedTimer.Stop();
                GameTimer.Stop();
                GameOver?.Invoke(this, EventArgs.Empty);

                if (this.mode == 1)
                {
                    lbInfo.BringToFront();
                    lbInfo.Visible = true;
                    lbInfo.Text = "YOU LOSE!!!" + Environment.NewLine + $"Score: {Get_Score()}";
                    isPlayable = false;
                }

                // Xử lý chơi game mới
                gameOver = false;
                isPaused = false;
                btnPlay.Enabled = true;
            }
            else
            {
                //Move piece down, or drop new piece if it can't move
                if (TestMove("down") == true)
                {
                    MovePiece("down");
                }
                else
                {
                    if (CheckGameOver() == true)
                    {
                        SpeedTimer.Stop();
                        GameTimer.Stop();
                        GameOver?.Invoke(this, EventArgs.Empty);

                        if (this.mode == 1)
                        {
                            lbInfo.BringToFront();
                            lbInfo.Visible = true;
                            lbInfo.Text = "YOU LOSE!!!" + Environment.NewLine + $"Score: {Get_Score()}";
                            isPlayable = false;
                        }

                        isPlayable = false;
                        
                        // Xử lý chơi game mới
                        gameOver = false;
                        isPaused = false;
                        btnPlay.Enabled = true;

                    }
                    if (CheckForCompleteRows() > -1)
                    {
                        ClearFullRow();
                    }
                    DropNewPiece();
                }
            }
        }

        // Game time (seconds elapsed)
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (!isPaused)
            {
                timeElapsed++;
                int minutes = timeElapsed / 60; // Số phút
                int seconds = timeElapsed % 60; // Số giây

                TimeLabel.Text = "TIME: " + minutes.ToString("00") + ":" + seconds.ToString("00");
            }
        }

        // Clear lowest full row
        private void ClearFullRow()
        {
            int completedRow = CheckForCompleteRows();

            //Turn that row white
            for (int x = 0; x <= 9; x++)
            {
                Control z = grid.GetControlFromPosition(x, completedRow);
                z.BackColor = Color.White;
            }

            //Move all other squares down
            for (int x = completedRow - 1; x >= 0; x--) //For each row above cleared row
            {
                //For each square in row
                for (int y = 0; y <= 9; y++)
                {
                    //the square
                    Control z = grid.GetControlFromPosition(y, x);

                    //the square below it
                    Control zz = grid.GetControlFromPosition(y, x + 1);

                    zz.BackColor = z.BackColor;
                    z.BackColor = Color.White;
                }
            }

            UpdateScore();

            clears++;

            if (clears % 10 == 0)
            {
                LevelUp();
            }

            if (CheckForCompleteRows() > -1)
            {
                ClearFullRow();
            }
        }

        private void UpdateScore()
        {
            bool skipComboReset = false;
            int bonus = 100 * Math.Min(combo + 1, 3); // Base score for 1-3 line clears

            if (combo == 3)
            { // Quad clear start
                bonus = 800;
                skipComboReset = true;
            }
            else if (combo > 3 && (combo - 3) % 4 == 0)
            { // Quad clear continue
                bonus = 1200;
                skipComboReset = true;
            }

            score += bonus;

            if (CheckForCompleteRows() == -1 && !skipComboReset)
            {
                combo = 0; // Reset combo for non-quad clears
            }
            else
            {
                combo++;
            }

            this.Invoke(new MethodInvoker(delegate
            {
                ScoreUpdateLabel.Text = "+" + bonus;
                ScoreLabel.Text = "SCORE: \n" + score;
                ScoreUpdateTimer.Start();
            }));
        }

        // Return row number of lowest full row
        // If no full rows, return -1
        private int CheckForCompleteRows()
        {
            // For each row
            for (int x = 21; x >= 2; x--)
            {
                // For each square in row
                for (int y = 0; y <= 9; y++)
                {
                    Control z = grid.GetControlFromPosition(y, x);
                    if (z.BackColor == Color.White)
                    {
                        break;
                    }
                    if (y == 9)
                    {
                        // Return full row number
                        return x;
                    }
                }
            }
            return -1; // "null"
        }

        // Increase fall speed
        private void LevelUp()
        {
            level++;

            int speed = Math.Max(800 - (level * 84), 16); // Example formula, adjust as needed
            SpeedTimer.Interval = speed;
        }

        // Game ends if a piece is in the top row when the next piece is dropped
        private bool CheckGameOver()
        {
            Control[] topRow = { box1, box2, box3, box4, box5, box6, box7, box8, box9, box10 };

            foreach (Control box in topRow)
            {
                if ((box.BackColor != Color.White && box.BackColor != Color.LightGray) && !activePiece.Contains(box))
                {
                    return true; // Game Over
                }
            }

            return false; // Game Not Over
        }

        // Clear score update notification every 2 seconds
        private void ScoreUpdateTimer_Tick(object sender, EventArgs e)
        {
            ScoreUpdateLabel.Text = "";
            ScoreUpdateTimer.Stop();
        }

        // Check if a piece collise with ghost
        private bool CheckCollisionWithGhost()
        {
            foreach (Control square in activePiece)
            {
                if (square.BackColor != Color.White && square.BackColor != Color.LightGray && Ghost.Contains(square))
                {
                    return true;
                }
            }
            return false;
        }

        public static List<int> GenerateTetrisSequence(int length)
        {
            List<int> sequence = new List<int>();
            for (int i = 0; i < length; i++)
            {
                sequence.Add(Playing_Room.random.Next(7));  // Assuming there are 7 different Tetris blocks
            }
            return sequence;
        }

        public void StartNewGame(List<int> sequence)
        {
            timeElapsed = 0;
            rotations = 0;
            combo = 0;
            score = 0;
            clears = 0;
            level = 0;
            gameOver = false;
            isPaused = false;
            PieceSequenceIteration = 0;
            isPlayable = true;
            lbInfo.Visible = false;
            lbInfo.Text = "YOU LOSE!!!";
            this.ActiveControl = this.grid;

            foreach (Control control in grid.Controls)
            {
                control.BackColor = Color.White;
            }

            // Use the provided sequence
            PieceSequence = sequence;

            SpeedTimer.Start();
            GameTimer.Start();

            // Select first piece
            nextPieceInt = PieceSequence[0];
            PieceSequenceIteration++;

            DropNewPiece();
            isPlayable = true;
            btnPlay.Enabled = false;
        }

        public void StopGame()
        {
            SpeedTimer.Stop();
            GameTimer.Stop();
            gameOver = true;
            isPaused = true;
            isPlayable = false;
        }

        #endregion

        public void btnPlay_Click(object sender, EventArgs e)
        {
            if (this.mode == 1)
            {
                int GlobalSeed = new Random().Next(1000);
                Playing_Room.random = new System.Random(GlobalSeed);
                List<int> sequence = GameTetris.GenerateTetrisSequence(1000);
                StartNewGame(sequence);
                return;
            }

            StartGame?.Invoke(this, EventArgs.Empty);
            btnPlay.Enabled = false;
        }


        public void HideControls()
        {
            rtbChatZone.Visible = false;
            btnSendMessage.Visible = false;
            txtChat.Visible = false;
            btnPlay.Visible = false;
            ScoreLabel.Visible = false;
            ScoreUpdateLabel.Visible = false;
            TimeLabel.Visible = false;
        }

        public void Enable_PlayButton()
        {
            this.Invoke(new MethodInvoker(delegate
            {
                btnPlay.Enabled = true;
            }));
        }

        public int Get_Score()
        {
            return score;
        }

        public void SetName(string name)
        {
            if (this.lbUserName.InvokeRequired)
            {
                this.lbUserName.Invoke(new Action<string>(SetName), name);
            }
            else
            {
                lbUserName.Text = name;
            }
        }

        public void ClearGrid()
        {
            foreach (Control control in grid.Controls)
            {
                control.BackColor = Color.White;
            }
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                control.BackColor = Color.White;
            }
        }

        #region chat zone UI and method
        public void AddMessage(string str)
        {
            if (rtbChatZone.InvokeRequired)
            {
                rtbChatZone.Invoke(addMessageDelegate, str);
            }
            else
            {
                rtbChatZone.AppendText(str + Environment.NewLine);
                rtbChatZone.SelectionStart = rtbChatZone.Text.Length;
                rtbChatZone.ScrollToCaret();
            }
        }


        public string ReadMessage()
        {
            string mgs = txtChat.Text.Trim();
            if (string.IsNullOrEmpty(mgs))
            {
                return "";
            }
            return mgs;
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            string mgs = txtChat.Text.Trim();
            if (string.IsNullOrEmpty(mgs))
            {
                return;
            }

            SendMessage?.Invoke(this, EventArgs.Empty);

            AddMessage("Me: " + mgs);
            txtChat.Clear();
        }

        private void txtChat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSendMessage.PerformClick();
            }
        }

        private void txtChat_Enter(object sender, EventArgs e)
        {
            isTexting = true;
        }

        private void GameTetris_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtChat.ContainsFocus)
            {
                isTexting = false;
                grid.Focus();
            }
        }

        private void GameTetris_Load(object sender, EventArgs e)
        {
            this.ActiveControl = grid;
        }

        #endregion

        public void AnnounceLabel(string str)
        {
            lbInfo.Text = str;
        }

        public void ChangeVisible(bool need)
        {
            lbInfo.Visible = need;
        }
    }
}