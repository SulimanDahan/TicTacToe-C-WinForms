using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TicTacToi
{
    public partial class MainWindow : Form
    {
        private Button[,] btns;
        private bool isX;       // A flag to check if it's the time for player x to play or not
        private int XWin = 0;
        private int OWin = 0;

        public MainWindow()
        {
            InitializeComponent();

            ReloadWindow();
        }

        #region ReloadWindow
        // A method to lunch the window
        private void ReloadWindow()
        {
            this.Controls.Clear();
            this.AutoSize = true;

            isX = true;

            CreateButtons();
        }
        #endregion

        #region CreateButtons
        // Create an array of buttons and add them to the main window
        private void CreateButtons()
        {
            int number = 0;
            btns = new Button[3,3];
            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    btns[i, j] = new Button
                    {
                        Font = new Font("", 46, FontStyle.Bold),
                        Location = new Point(i * 110, j * 110),
                        Size = new Size(100, 100),
                        Text = (++number).ToString(),
                        ForeColor = SystemColors.ControlLight
                    };
                    this.btns[i, j].Click += new EventHandler(ButtonClick);
                    this.Controls.Add(btns[i, j]);
                }
            }
        }
        #endregion

        #region ButtonClick
        // Buttons click event
        private void ButtonClick(object sender, EventArgs es)
        {
            Button TempBtn = sender as Button;

            if (isX)
            {
                TempBtn.Text = "X";
                TempBtn.BackColor = Color.Red;
                isX = false;
            }
            else
            {
                TempBtn.Text = "O";
                TempBtn.BackColor = Color.Blue;
                isX = true;
            }

            TempBtn.ForeColor = Color.White;

            CheckState();
        }
        #endregion

        #region CheckState
        // Check if there is a winner and color the buttons
        private void CheckState()
        {
            char winner = ' '; // A char variable to store the winner

            // Check rows and columns
            for (int i = 0; i < 3; i++)
            {
                // check if buttons on the same row have the same character
                if (btns[i, 0].Text == btns[i, 1].Text && btns[i, 0].Text == btns[i, 2].Text)
                {
                    btns[i, 0].BackColor = Color.Green;
                    btns[i, 1].BackColor = Color.Green;
                    btns[i, 2].BackColor = Color.Green;

                    // Check who wins the game
                    if (btns[i, 0].Text == "X") winner = 'X';
                    else winner = 'O';

                    break;
                }

                // check if buttons on the same column have the same character
                if (btns[0, i].Text == btns[1, i].Text && btns[0, i].Text == btns[2, i].Text)
                {
                    btns[0, i].BackColor = Color.Green;
                    btns[1, i].BackColor = Color.Green;
                    btns[2, i].BackColor = Color.Green;

                    if (btns[0, i].Text == "X") winner = 'X';
                    else winner = 'O';

                    break;
                }    
            }

            if (winner != ' ')
                goto End;

            // check if main diameter's buttons have the same character
            if (btns[0, 0].Text == btns[1, 1].Text && btns[1, 1].Text == btns[2, 2].Text)
            {
                btns[0, 0].BackColor = Color.Green;
                btns[1, 1].BackColor = Color.Green;
                btns[2, 2].BackColor = Color.Green;

                if (btns[1, 1].Text == "X") winner = 'X';
                else winner = 'O';
            }

            // check if secondary diameter's buttons have the same character
            if (btns[0, 2].Text == btns[1, 1].Text && btns[1, 1].Text == btns[2, 0].Text)
            {
                btns[0, 2].BackColor = Color.Green;
                btns[1, 1].BackColor = Color.Green;
                btns[2, 0].BackColor = Color.Green;

                if (btns[1, 1].Text == "X") winner = 'X';
                else winner = 'O';
            }

            End:;

            if (winner != ' ') Winner(winner);
        }
        #endregion

        #region Winner method
        // Display winner and get result
        private void Winner(Char winner)
        {
            int WinScore = 0;

            if (winner == 'X') WinScore = ++XWin;
            else WinScore = ++OWin;

            //Show a message tells the players how win and ask them if they want to play again
            if(MessageBox.Show($"Player {winner} Wins {WinScore} times\n\nDo you want to play again?", "Winner", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                ReloadWindow();
            }
        }
        #endregion
    }
}
