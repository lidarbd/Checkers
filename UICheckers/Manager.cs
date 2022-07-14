using System;
using System.Windows.Forms;
using System.Drawing;
using CheckersLogic;

namespace UICheckers
{
    internal class Manager
    {
        private readonly CheckersApps r_Game;
        private readonly FormCheckersGame r_FormGame;
        private int m_PlayerNumber;

        public Manager()
        {
            r_FormGame = new FormCheckersGame();
            r_Game = new CheckersApps(r_FormGame.GameSettings.GetBoardSize(), r_FormGame.GameSettings.Player1Name, r_FormGame.GameSettings.GetPlayer2Name(), getPlayer2Type());
            m_PlayerNumber = 1;
            r_FormGame.ClickOccured += new MoveEventHandler(formGame_ClickOccured);
            r_Game.MovementOccured += new MoveEventHandler(formGame_MovementOccured);
            r_Game.EatenOccured += new MovementEffectEventHandler(formGame_EatenOccured);
            r_Game.BecameKingOccured += new MovementEffectEventHandler(formGame_BecameKingOccured);
            r_FormGame.ShowDialog();
        }

        private ePlayerType getPlayer2Type()
        {
            ePlayerType player2Type;

            if(r_FormGame.GameSettings.IsComputerOpponent)
            {
                player2Type = ePlayerType.Computer;
            }
            else
            {
                player2Type = ePlayerType.Human;
            }

            return player2Type;
        }

        private void formGame_BecameKingOccured(ButtonPanelPositionEventArgs e)
        {
            if(r_Game.CurrentPlayer.Sign == eSign.X)
            {
                r_FormGame.UpdateBoardPanelsAfterBecameKing(e.PanelPosition, "X");
            }
            else
            {
                r_FormGame.UpdateBoardPanelsAfterBecameKing(e.PanelPosition, "O");
            }
        }

        private void formGame_EatenOccured(ButtonPanelPositionEventArgs e)
        {
            r_FormGame.UpdateBoardPanelsAfterEatenTool(e.PanelPosition);
        }

        private void formGame_MovementOccured(MovePositionEventArgs e)
        {
            r_FormGame.UpdateBoardPanelsAfterMovement(e.FromWhere, e.ToWhere);
        }

        private void formGame_ClickOccured(MovePositionEventArgs e)
        {
            bool isCurrentPlayersToolGame = checkIfChosenButtonIsUserGameTool();

            if (r_FormGame.IsFirstButtonClicked || CheckIfTheSameButtonClicked(e))
            {
                r_FormGame.ChangeButtonsBackground(isCurrentPlayersToolGame);
            }
            else
            {
                if (!r_Game.CheckIfMoveAppearsInToolLegalMovements(e.FromWhere, e.ToWhere))
                {
                    r_FormGame.HandleInvalidMove();
                }
                else
                {
                    implementMovementInFormGame(e);
                }
            }
        }

        private void implementMovementInFormGame(MovePositionEventArgs e)
        {
            implementPlayerMove(e);
            r_Game.ChangePlayer(ref m_PlayerNumber);
            while (!r_Game.IsRoundOver && r_Game.CurrentPlayer.Type == ePlayerType.Computer)
            {
                implementComputerMove();
            }

            if (r_Game.IsRoundOver)
            {
                getRoundOverData();
            }
        }

        private void getRoundOverData()
        {
            bool isWon;
            Player winnerPlayer, loserPlayer;

            winnerPlayer = null;
            if (r_Game.CheckIfThereIsTie())
            {
                isWon = false;
            }
            else
            {
                r_Game.GetWinnerPlayer(out winnerPlayer, out loserPlayer);
                r_Game.CalculateWinnersScore(winnerPlayer, loserPlayer);
                isWon = true;
            }

            r_FormGame.CheckIfUserWantsAnotherRound(winnerPlayer.Name, isWon);
            initializeGame();
        }

        private void initializeGame()
        {
            r_Game.InitializeRound();
            r_Game.PlayerOne.InitializeNumberOfTools(r_Game.GameBoard.Cols);
            r_Game.PlayerTwo.InitializeNumberOfTools(r_Game.GameBoard.Cols);
            r_Game.PlayerOne.ToolsValue = r_Game.PlayerTwo.ToolsValue = 0;
            r_Game.InitializePlayersStatus();
            r_Game.InitializeGameToolsPositionsForPlayer(r_Game.CurrentPlayer);
            r_Game.InitializeGameToolsPositionsForPlayer(r_Game.OpponentPlayer);
            resetFormGame();
        }

        private bool checkIfChosenButtonIsUserGameTool()
        {
            return (r_Game.CurrentPlayer.Sign == eSign.O &&
                (r_FormGame.CurrentClickedButton.AccessibleName == "O" || r_FormGame.CurrentClickedButton.AccessibleName == "U")) ||
                (r_Game.CurrentPlayer.Sign == eSign.X &&
                (r_FormGame.CurrentClickedButton.AccessibleName == "X" || r_FormGame.CurrentClickedButton.AccessibleName == "K"));
        }

        private void implementPlayerMove(MovePositionEventArgs e)
        {
            r_Game.UpdateDataForPlayerAfterMovement(e.FromWhere, e.ToWhere);
            r_FormGame.ChangeButtonsBackgroundToOriginal(e.FromWhere);
        }

        private void implementComputerMove()
        {
            Point fromWhere, toWhere;

            r_Game.GetMoveFromComputer(out fromWhere, out toWhere);
            implementPlayerMove(new MovePositionEventArgs(fromWhere, toWhere));
            r_Game.ChangePlayer(ref m_PlayerNumber);
        }

        private bool CheckIfTheSameButtonClicked(MovePositionEventArgs e)
        {
            return e.FromWhere == e.ToWhere;
        }

        private void resetFormGame()
        {
            int boardSize = r_FormGame.GameSettings.GetBoardSize();

            r_FormGame.UpdateLabelsPlayerScore(r_Game.PlayerOne.Score, r_Game.PlayerTwo.Score);
            for (int i = 0; i < boardSize; ++i)
            {
                for (int j = 0; j < boardSize; ++j)
                {
                    switch (r_Game.GameBoard.GameBoard[i, j].CellContent)
                    {
                        case eSign.X:
                            r_FormGame.GameBoard[i, j].AccessibleName = "X";
                            r_FormGame.GameBoard[i, j].Image = global::UICheckers.Properties.Resources.PlayerX;
                            break;
                        case eSign.O:
                            r_FormGame.GameBoard[i, j].AccessibleName = "O";
                            r_FormGame.GameBoard[i, j].Image = global::UICheckers.Properties.Resources.PlayerO;
                            break;
                        default:
                            r_FormGame.GameBoard[i, j].AccessibleName = string.Empty;
                            r_FormGame.GameBoard[i, j].Image = null;
                            break;
                    }
                }
            }
        }
    }
}
