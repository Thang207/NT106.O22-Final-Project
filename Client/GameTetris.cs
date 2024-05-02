﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Tetris
{
    public partial class GameTetris : Form
    {
        #region Global 
        // Initialize global variables
        Control[] activePiece = { null, null, null, null };
        Control[] activePiece2 = { null, null, null, null };
        Control[] nextPiece = { null, null, null, null };
        Control[] savedPiece = { null, null, null, null };
        Control[] Ghost = { null, null, null, null };
        List<int> PieceSequence = new List<int>();
        Color pieceColor = Color.White;
        Color savedPieceColor = Color.White;
        int timeElapsed = 0;
        int currentPiece;
        int nextPieceInt;
        int savedPieceInt = -1;
        int rotations = 0;
        int combo = 0;
        int score = 0;
        int clears = 0;
        int level = 0;
        bool gameOver = false;
        bool isPaused = false;
        int PieceSequenceIteration = 0;
        bool isPlayable = false;

        readonly Color[] colorList =
        {
            Color.FromArgb(1, 237, 250),     // I piece - cyan
            Color.FromArgb(255, 200, 46),   // L piece - orange
            Color.FromArgb(0, 119, 211),     // J piece - blue
            Color.FromArgb(83, 218, 63),    // S piece - green
            Color.FromArgb(234, 20, 28),      // Z piece - red
            Color.FromArgb(255, 213, 0),   // O piece - yellow 
            Color.FromArgb(221, 10, 178)   // T piece - purple
        };
        #endregion

        public event EventHandler StartGame;
        public event EventHandler GameOver;
        public event EventHandler RestartGame;


        // Load main window
        public GameTetris()
        {
            InitializeComponent();
            ScoreUpdateLabel.Text = "";
            // Initialize/reset ghost piece
            // box1 through box4 are invisible
            activePiece2[0] = box1;
            activePiece2[1] = box2;
            activePiece2[2] = box3;
            activePiece2[3] = box4;
        }

        private void GameTetris_Load(object sender, EventArgs e)
        {
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


                        // Xử lý chơi game mới
                        gameOver = false;
                        isPaused = false;
                        btnPlay.Enabled = true;
                        RestartGame?.Invoke(this, EventArgs.Empty);
                    


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

                TimeLabel.Text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
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
            ClearsLabel.Text = "Clears: " + clears;

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
            // 1-3 line clear is worth 100 per line
            // Quad line clear (no combo) is worth 800
            // 2 or more quad line clears in a row is worth 1200 

            bool skipComboReset = false;

            // Single clear
            if (combo == 0)
            {
                score += 100;
                ScoreUpdateLabel.Text = "+100";
            }

            // Double clear
            else if (combo == 1)
            {
                score += 100;
                ScoreUpdateLabel.Text = "+200";
            }

            // Triple clear
            else if (combo == 2)
            {
                score += 100;
                ScoreUpdateLabel.Text = "+300";
            }

            // Quad clear, start combo
            else if (combo == 3)
            {
                score += 500;
                ScoreUpdateLabel.Text = "+800";
                skipComboReset = true;
            }

            // Single clear, broken combo
            else if (combo > 3 && combo % 4 == 0)
            {
                score += 100;
                ScoreUpdateLabel.Text = "+100";
            }

            // Double clear, broken combo
            else if (combo > 3 && ((combo - 1) % 4 == 0))
            {
                score += 100;
                ScoreUpdateLabel.Text = "+200";
            }

            // Triple clear, broken combo
            else if (combo > 3 && ((combo - 2) % 4 == 0))
            {
                score += 100;
                ScoreUpdateLabel.Text = "+300";
            }

            // Quad clear, continue combo
            else if (combo > 3 && ((combo - 3) % 4 == 0))
            {
                score += 900;
                ScoreUpdateLabel.Text = "+1200";
                skipComboReset = true;
            }

            if (CheckForCompleteRows() == -1 && skipComboReset == false)
            {
                // 1-3 line clear
                combo = 0;
            }
            else
            {
                // Quad clear
                combo++;
            }

            ScoreLabel.Text = "Score: " + score.ToString();
            ScoreUpdateTimer.Start();
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
            LevelLabel.Text = "Level: " + level.ToString();

            // Milliseconds per square fall
            // Level 1 = 800 ms per square, level 2 = 716 ms per square, etc.
            int[] levelSpeed =
            {
                800, 716, 633, 555, 466, 383, 300, 216, 133, 100, 083, 083, 083, 066, 066,
                066, 050, 050, 050, 033, 033, 033, 033, 033, 033, 033, 033, 033, 033, 016
            };

            // Speed does not change after level 29
            if (level <= 29)
            {
                SpeedTimer.Interval = levelSpeed[level];
            }
        }

        // Game ends if a piece is in the top row when the next piece is dropped
        private bool CheckGameOver()
        {
            Control[] topRow = { box1, box2, box3, box4, box5, box6, box7, box8, box9, box10 };

            foreach (Control box in topRow)
            {
                if ((box.BackColor != Color.White & box.BackColor != Color.LightGray) & !activePiece.Contains(box))
                {
                    //Game over!
                    return true;
                }
            }

            if (gameOver == true)
            {
                return true;
            }

            return false;
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
                    return true; // Đã xảy ra va chạm với khối Ghost
                }
            }
            return false;
        }

        public static List<int> GenerateTetrisSequence(int length)
        {
            List<int> sequence = new List<int>();
            for (int i = 0; i < length; i++)
            {
                sequence.Add(TetrisRoom.random.Next(7));  // Assuming there are 7 different Tetris blocks
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
        }

        #endregion

        public void btnPlay_Click(object sender, EventArgs e)
        {
            //List<int> sequence = GameTetris.GenerateTetrisSequence(10);
            //StartNewGame(sequence);
            StartGame?.Invoke(this, EventArgs.Empty);
            btnPlay.Enabled = false;
        }

        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    if (keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Space
        //        || keyData == Keys.A || keyData == Keys.S || keyData == Keys.W || keyData == Keys.D)
        //    {
        //        KeyEventArgs e = new KeyEventArgs(keyData);
        //        this.MainWindow_KeyDown(this, e);
        //        return true;
        //    }
        //    return base.ProcessCmdKey(ref msg, keyData);
        //}

        public void AddMessage(string str)
        {
            lvStatus.Items.Add(str);
        }

        public void HideListView()
        {
            lvStatus.Visible = false;
            btnPlay.Visible = false;
        }

        public void Enable_Play()
        {
            this.Invoke(new MethodInvoker(delegate
            {
                btnPlay.Enabled = true;
            }));
        }
    }
}