using System;
using System.Drawing;
using System.Windows.Forms;
using CheckersLogic;

namespace UICheckers
{
    public partial class FormCheckersGame : Form
    {
        public event MoveEventHandler ClickOccured;

        private const int k_BoardPanelSize = 50;
        private const int k_FormWidthExtension = 30;
        private const int k_FormHeightExtension = 90;
        private readonly FormGameSettings r_GameSettings = new FormGameSettings();
        private Button[,] buttonsGameBoard;
        private bool m_IsFirstButtonClicked;
        private Point m_CurrentFromWherePosition;
        private Button m_CurrentClickedButton;

        public FormGameSettings GameSettings
        {
            get { return r_GameSettings; }
        }

        public Button[,] GameBoard
        {
            get { return buttonsGameBoard; }
        }

        public Button CurrentClickedButton
        {
            get { return m_CurrentClickedButton; }
        }

        public bool IsFirstButtonClicked
        {
            get { return m_IsFirstButtonClicked; }
            set { m_IsFirstButtonClicked = value; }
        }

        public FormCheckersGame()
        {
            r_GameSettings.ShowDialog();
            if (r_GameSettings.DialogResult == DialogResult.Cancel)
            {
                this.Close();
            }
            else
            {
                InitializeComponent();
                initializeGameSettings();
                initializeGameBoard();
                m_IsFirstButtonClicked = true;
            }
        }

        private void initializeGameSettings()
        {
            labelPlayer1.Text = string.Format("{0}:", r_GameSettings.Player1Name);
            labelPlayer1Result.Left = labelPlayer1.Right;
            labelPlayer2.Text = string.Format("{0}:", r_GameSettings.GetPlayer2Name());
            labelPlayer2Result.Left = labelPlayer2.Right;
        }

        private void initializeGameBoard()
        {
            int boardSize = r_GameSettings.GetBoardSize();

            Size = new Size((k_BoardPanelSize * boardSize) + k_FormWidthExtension, (k_BoardPanelSize * boardSize) + k_FormHeightExtension);
            buttonsGameBoard = new Button[boardSize, boardSize];
            for (int i = 0; i < boardSize; ++i)
            {
                for(int j = 0; j < boardSize; ++j)
                {
                    buttonsGameBoard[i, j] = new Button();
                    buttonsGameBoard[i, j].Click += new EventHandler(Button_Click);
                    buttonsGameBoard[i, j].Size = new Size(k_BoardPanelSize, k_BoardPanelSize);
                    buttonsGameBoard[i, j].Top = (labelPlayer1.Bottom + 12) + (i * buttonsGameBoard[i, j].Height);
                    buttonsGameBoard[i, j].Left = 12 + (j * buttonsGameBoard[i, j].Width);

                    if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                    {
                        buttonsGameBoard[i, j].BackgroundImage = global::UICheckers.Properties.Resources.whiteBackground;
                        buttonsGameBoard[i, j].BackColor = Color.White;
                    }
                    else
                    {
                        buttonsGameBoard[i, j].BackgroundImage = global::UICheckers.Properties.Resources.blackBackground;
                        buttonsGameBoard[i, j].Enabled = false;
                    }

                    if (i < (boardSize / 2) - 1)
                    {
                        if ((i % 2 == 0 || j % 2 == 0) && (i % 2 != 0 || j % 2 != 0))
                        {
                            buttonsGameBoard[i, j].AccessibleName = "O";
                            buttonsGameBoard[i, j].Image = global::UICheckers.Properties.Resources.PlayerO;
                        }
                    }
                    else if (i > boardSize / 2)
                    {
                        if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                        {
                            buttonsGameBoard[i, j].AccessibleName = "X";
                            buttonsGameBoard[i, j].Image = global::UICheckers.Properties.Resources.PlayerX;
                        }
                    }

                    this.Controls.Add(buttonsGameBoard[i, j]);
                }
            }
        }

        protected virtual void OnClick(Point i_FromWhere, Point i_ToWhere)
        {
            MovePositionEventArgs e = new MovePositionEventArgs(i_FromWhere, i_ToWhere);

            if (ClickOccured != null)
            {
                ClickOccured(e);
            }
        }

        private Point getButtonsPositionInBoard(Button i_Button)
        {
            int x, y;

            x = (i_Button.Top - labelPlayer1.Bottom - 12) / i_Button.Height;
            y = (i_Button.Left - 12) / i_Button.Width;

            return new Point(x, y);
        }

        internal void Button_Click(object sender, EventArgs e)
        {
            Point currentButtonPosition;

            m_CurrentClickedButton = sender as Button;
            currentButtonPosition = getButtonsPositionInBoard(m_CurrentClickedButton);
            if (m_IsFirstButtonClicked)
            {
                m_CurrentFromWherePosition = currentButtonPosition;
            }
           
            OnClick(m_CurrentFromWherePosition, currentButtonPosition);
        }

        internal void ChangeButtonsBackground(bool i_IsValidGameTool)
        {
            if (i_IsValidGameTool)
            {
                if (m_CurrentClickedButton.BackColor == Color.Blue)
                {
                    m_CurrentClickedButton.BackgroundImage = global::UICheckers.Properties.Resources.whiteBackground;
                    m_CurrentClickedButton.BackColor = Color.White;
                    m_IsFirstButtonClicked = true;
                }
                else
                {
                    m_CurrentClickedButton.BackgroundImage = global::UICheckers.Properties.Resources.blueBackground;
                    m_CurrentClickedButton.BackColor = Color.Blue;
                    m_IsFirstButtonClicked = false;
                }
            }
            else
            {
                MessageBox.Show("Please choose your tool game!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        internal void ChangeButtonsBackgroundToOriginal(Point i_FromWhere)
        {
            buttonsGameBoard[i_FromWhere.X, i_FromWhere.Y].BackColor = Color.White;
            buttonsGameBoard[i_FromWhere.X, i_FromWhere.Y].BackgroundImage = global::UICheckers.Properties.Resources.whiteBackground;
            m_IsFirstButtonClicked = true;
        }

        internal void HandleInvalidMove()
        {
            MessageBox.Show("Invalid move! Try again.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        internal void UpdateBoardPanelsAfterMovement(Point i_FromWhere, Point i_ToWhere)
        {
            buttonsGameBoard[i_ToWhere.X, i_ToWhere.Y].AccessibleName = buttonsGameBoard[i_FromWhere.X, i_FromWhere.Y].AccessibleName;
            buttonsGameBoard[i_FromWhere.X, i_FromWhere.Y].AccessibleName = string.Empty;
            buttonsGameBoard[i_ToWhere.X, i_ToWhere.Y].Image = buttonsGameBoard[i_FromWhere.X, i_FromWhere.Y].Image;
            buttonsGameBoard[i_FromWhere.X, i_FromWhere.Y].Image = null;
        }

        internal void UpdateBoardPanelsAfterBecameKing(Point i_Position, string i_Sign)
        {
            buttonsGameBoard[i_Position.X, i_Position.Y].AccessibleName = i_Sign;
            updateKingImage(i_Position, i_Sign);
        }

        private void updateKingImage(Point i_Position, string i_Sign)
        {
            if (i_Sign == "X")
            {
                buttonsGameBoard[i_Position.X, i_Position.Y].Image = global::UICheckers.Properties.Resources.CrownX;
            }
            else
            {
                buttonsGameBoard[i_Position.X, i_Position.Y].Image = global::UICheckers.Properties.Resources.CrownO;
            }
        }

        internal void UpdateBoardPanelsAfterEatenTool(Point i_Position)
        {
            buttonsGameBoard[i_Position.X, i_Position.Y].AccessibleName = string.Empty;
            buttonsGameBoard[i_Position.X, i_Position.Y].Image = null;
        }

        internal void CheckIfUserWantsAnotherRound(string i_WinnersName, bool i_IsWon)
        {
            string output;
            DialogResult result;

            if (i_IsWon)
            {
                output = string.Format("{0} Won!{1}Another Round?", i_WinnersName, Environment.NewLine);
            }
            else
            {
                output = string.Format("Tie!{0}Another Round?", Environment.NewLine);
            }

            result = MessageBox.Show(output, "Damka", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.No)
            {
                this.Close();
            }
        }

        internal void UpdateLabelsPlayerScore(int i_Player1Score, int i_Player2Score)
        {
            labelPlayer1Result.Text = i_Player1Score.ToString();
            labelPlayer2Result.Text = i_Player2Score.ToString();
        }
    }
}
