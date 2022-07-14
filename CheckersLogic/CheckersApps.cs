using System.Collections.Generic;
using System.Drawing;
using System;
using System.Text;

namespace CheckersLogic
{
    public class CheckersApps
    {
        public event MoveEventHandler MovementOccured;

        public event MovementEffectEventHandler EatenOccured;

        public event MovementEffectEventHandler BecameKingOccured;

        private readonly Random r_RandomIndex;
        public const int k_PlayerOne = 1;
        public const int k_PlayerTwo = 2;
        internal const int k_NotFound = -1;
        private Player m_PlayerOne, m_PlayerTwo, m_CurrentPlayer, m_OpponentPlayer;
        private Board m_GameBoard;
        private bool m_IsCurrentPlayerSkipped;
        private bool m_IsRoundOver;

        public CheckersApps(int i_BoardSize, string i_Player1Name, string i_Player2Name, ePlayerType i_Player2Type)
        {
            r_RandomIndex = new Random();
            m_PlayerOne = m_CurrentPlayer = new Player(eSign.X, ePlayerType.Human, i_Player1Name);
            m_PlayerTwo = m_OpponentPlayer = new Player(eSign.O, i_Player2Type, i_Player2Name);
            m_GameBoard = new Board(i_BoardSize);
            initializeGame();
        }

        private void initializeGame()
        {
            m_IsCurrentPlayerSkipped = m_IsRoundOver = false;
            m_PlayerOne.InitializeNumberOfTools(m_GameBoard.Cols);
            m_PlayerTwo.InitializeNumberOfTools(m_GameBoard.Cols);
            m_PlayerOne.ToolsValue = m_PlayerTwo.ToolsValue = 0;
            InitializePlayersStatus();
            InitializeGameToolsPositionsForPlayer(m_CurrentPlayer);
            InitializeGameToolsPositionsForPlayer(m_OpponentPlayer);
        }

        public Player CurrentPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }

            set
            {
                m_CurrentPlayer = value;
            }
        }

        public Player OpponentPlayer
        {
            get
            {
                return m_OpponentPlayer;
            }

            set
            {
                m_OpponentPlayer = value;
            }
        }

        public Player PlayerOne
        {
            get
            {
                return m_PlayerOne;
            }

            set
            {
                m_PlayerOne = value;
            }
        }

        public Player PlayerTwo
        {
            get
            {
                return m_PlayerTwo;
            }

            set
            {
                m_PlayerTwo = value;
            }
        }

        public Board GameBoard
        {
            get
            {
                return m_GameBoard;
            }

            set
            {
                m_GameBoard = value;
            }
        }

        public bool IsRoundOver
        {
            get
            {
                return m_IsRoundOver;
            }

            set
            {
                m_IsRoundOver = value;
            }
        }

        public void InitializePlayersStatus()
        {
            updatePlayersStatus(k_PlayerOne);
        }

        private void updatePlayersStatus(int i_PlayerNumber)
        {
            m_IsCurrentPlayerSkipped = false;
            if (i_PlayerNumber == k_PlayerOne)
            {
                m_CurrentPlayer = m_PlayerOne;
                m_OpponentPlayer = m_PlayerTwo;
            }
            else
            {
                m_CurrentPlayer = m_PlayerTwo;
                m_OpponentPlayer = m_PlayerOne;
            }
        }

        private void updatePlayerGameToolPositions(Player i_Player)
        {
            i_Player.CurrentGameToolsPositions.Clear();
            InitializeGameToolsPositionsForPlayer(i_Player);
        }

        public void InitializeGameToolsPositionsForPlayer(Player i_Player)
        {
            int currentGameTool = 0;
            Point currentPosition;

            i_Player.IsHasNoOptionToSkip = true;
            for (int i = 0; i < m_GameBoard.Rows; ++i)
            {
                for (int j = 0; j < m_GameBoard.Cols; ++j)
                {
                    if (checkIfCellContentMatchesToPlayerSigns(i_Player, i, j))
                    {
                        currentPosition = new Point(i, j);
                        i_Player.CurrentGameToolsPositions.Add(new Strategy(currentPosition));
                        i_Player.CurrentGameToolsPositions[currentGameTool].ToolPossibleMovements = findToolPossibleMovements(i_Player, currentPosition, currentGameTool);
                        currentGameTool++;
                    }
                }
            }
        }

        private bool checkIfCellContentMatchesToPlayerSigns(Player i_Player, int i_Row, int i_Col)
        {
            bool isCellContentMatchesToPlayerSigns = true;

            if (i_Player.Sign == eSign.X)
            {
                isCellContentMatchesToPlayerSigns = m_GameBoard.GameBoard[i_Row, i_Col].CellContent == eSign.X
                    || m_GameBoard.GameBoard[i_Row, i_Col].CellContent == eSign.K;
            }
            else if (i_Player.Sign == eSign.O)
            {
                isCellContentMatchesToPlayerSigns = m_GameBoard.GameBoard[i_Row, i_Col].CellContent == eSign.O
                    || m_GameBoard.GameBoard[i_Row, i_Col].CellContent == eSign.U;
            }

            return isCellContentMatchesToPlayerSigns;
        }

        private List<PossiblePositions> findToolPossibleMovements(Player i_Player, Point i_CurrentPosition, int i_CurrentGameTool)
        {
            List<PossiblePositions> toolPossibleMovements = new List<PossiblePositions>();

            if (checkIfSkipMove(i_Player, i_CurrentPosition, toolPossibleMovements) && i_Player.IsHasNoOptionToSkip)
            {
                i_Player.IsHasNoOptionToSkip = false;
                for (int i = 0; i < i_CurrentGameTool; ++i)
                {
                    i_Player.CurrentGameToolsPositions[i].ToolPossibleMovements.Clear();
                }
            }
            else if (!checkIfSkipMove(i_Player, i_CurrentPosition, toolPossibleMovements) && i_Player.IsHasNoOptionToSkip)
            {
                updateToolPossibleRegularMovementsList(i_Player, i_CurrentPosition, toolPossibleMovements);
            }

            return toolPossibleMovements;
        }

        private void updateToolPossibleRegularMovementsList(Player i_Player, Point i_CurrentPosition,
            List<PossiblePositions> i_ToolPossibleMovements)
        {
            if (m_GameBoard.CheckIfKingInCell(i_CurrentPosition))
            {
                updateListPossiblePositionByMovementDirection(i_CurrentPosition, eDirections.Up, eDirections.Left, i_ToolPossibleMovements);
                updateListPossiblePositionByMovementDirection(i_CurrentPosition, eDirections.Up, eDirections.Right, i_ToolPossibleMovements);
                updateListPossiblePositionByMovementDirection(i_CurrentPosition, eDirections.Down, eDirections.Left, i_ToolPossibleMovements);
                updateListPossiblePositionByMovementDirection(i_CurrentPosition, eDirections.Down, eDirections.Right, i_ToolPossibleMovements);
            }
            else if (i_Player.Sign == eSign.O)
            {
                updateListPossiblePositionByMovementDirection(i_CurrentPosition, eDirections.Down, eDirections.Left, i_ToolPossibleMovements);
                updateListPossiblePositionByMovementDirection(i_CurrentPosition, eDirections.Down, eDirections.Right, i_ToolPossibleMovements);
            }
            else if (i_Player.Sign == eSign.X)
            {
                updateListPossiblePositionByMovementDirection(i_CurrentPosition, eDirections.Up, eDirections.Left, i_ToolPossibleMovements);
                updateListPossiblePositionByMovementDirection(i_CurrentPosition, eDirections.Up, eDirections.Right, i_ToolPossibleMovements);
            }
        }

        private void updateListPossiblePositionByMovementDirection(Point i_CurrentPosition, eDirections i_DirectionX,
            eDirections i_DirectionY, List<PossiblePositions> i_ToolPossibleMovements)
        {
            PossiblePositions movement = new PossiblePositions();
            Point nextPosition = new Point(i_CurrentPosition.X + (int)i_DirectionX, i_CurrentPosition.Y + (int)i_DirectionY);

            if (!m_GameBoard.CheckIfOutOfBoardBoundaries(nextPosition) && m_GameBoard.CheckIfCellEmpty(nextPosition))
            {
                movement.PlayerNextPosition = nextPosition;
                i_ToolPossibleMovements.Add(movement);
            }
        }
         
        private bool checkIfSkipMove(Player i_Player, Point i_CurrentPosition, List<PossiblePositions> i_ToolPossibleMovements)
        {
            bool isSkipMove = false;

            if (m_GameBoard.CheckIfKingInCell(i_CurrentPosition))
            {
                isSkipMove = checkIfSkipMoveForKing(i_Player, i_CurrentPosition, i_ToolPossibleMovements);
            }
            else if (i_Player.Sign == eSign.O)
            {
                isSkipMove = checkIfDownSkipMove(i_Player, i_CurrentPosition, i_ToolPossibleMovements);
            }
            else if (i_Player.Sign == eSign.X)
            {
                isSkipMove = checkIfUpSkipMove(i_Player, i_CurrentPosition, i_ToolPossibleMovements);
            }

            return isSkipMove;
        }

        private bool checkIfSkipMoveForKing(Player i_Player, Point i_CurrentPosition, List<PossiblePositions> i_ToolPossibleMovements)
        {
            return checkIfUpSkipMove(i_Player, i_CurrentPosition, i_ToolPossibleMovements) ||
                checkIfDownSkipMove(i_Player, i_CurrentPosition, i_ToolPossibleMovements);
        }

        private bool checkIfUpSkipMove(Player i_Player, Point i_CurrentPosition, List<PossiblePositions> i_ToolPossibleMovements)
        {
            bool isLeftSkipMove, isRightSkipMove;

            isLeftSkipMove = updateToolPossibleMovementsListIfSkipMove(i_Player, i_CurrentPosition, eDirections.Up, eDirections.Left, i_ToolPossibleMovements);
            isRightSkipMove = updateToolPossibleMovementsListIfSkipMove(i_Player, i_CurrentPosition, eDirections.Up, eDirections.Right, i_ToolPossibleMovements);

            return isLeftSkipMove || isRightSkipMove;
        }

        private bool checkIfDownSkipMove(Player i_Player, Point i_CurrentPosition, List<PossiblePositions> i_ToolPossibleMovements)
        {
            bool isLeftSkipMove, isRightSkipMove;

            isLeftSkipMove = updateToolPossibleMovementsListIfSkipMove(i_Player, i_CurrentPosition, eDirections.Down, eDirections.Left, i_ToolPossibleMovements);
            isRightSkipMove = updateToolPossibleMovementsListIfSkipMove(i_Player, i_CurrentPosition, eDirections.Down, eDirections.Right, i_ToolPossibleMovements);

            return isLeftSkipMove || isRightSkipMove;
        }

        private bool updateToolPossibleMovementsListIfSkipMove(Player i_Player, Point i_CurrentPosition, eDirections i_DirectionX,
            eDirections i_DirectionY, List<PossiblePositions> i_ToolPossibleMovements)
        {
            bool isSkipMove;
            Point nextPosition, skippedToolPosition;
            PossiblePositions movements = new PossiblePositions();

            skippedToolPosition = new Point(i_CurrentPosition.X + (int)i_DirectionX, i_CurrentPosition.Y + (int)i_DirectionY);
            if (!m_GameBoard.CheckIfOutOfBoardBoundaries(skippedToolPosition) && checkIfOpponentInCell(i_Player, skippedToolPosition))
            {
                nextPosition = new Point(i_CurrentPosition.X + ((int)i_DirectionX * 2), i_CurrentPosition.Y + ((int)i_DirectionY * 2));
                isSkipMove = (!m_GameBoard.CheckIfOutOfBoardBoundaries(nextPosition)) && m_GameBoard.CheckIfCellEmpty(nextPosition);
                if (isSkipMove)
                {
                    movements.SkippedToolPosition = skippedToolPosition;
                    movements.PlayerNextPosition = nextPosition;
                    i_ToolPossibleMovements.Add(movements);
                }
            }
            else
            {
                isSkipMove = false;
            }

            return isSkipMove;
        }

        private bool checkIfOpponentInCell(Player i_Player, Point i_Position)
        {
            bool isOpponentInCell = true;

            if (i_Player.Sign == eSign.O)
            {
                isOpponentInCell = m_GameBoard.GameBoard[i_Position.X, i_Position.Y].CellContent == eSign.X ||
                    m_GameBoard.GameBoard[i_Position.X, i_Position.Y].CellContent == eSign.K;
            }
            else if (i_Player.Sign == eSign.X)
            {
                isOpponentInCell = m_GameBoard.GameBoard[i_Position.X, i_Position.Y].CellContent == eSign.O ||
                    m_GameBoard.GameBoard[i_Position.X, i_Position.Y].CellContent == eSign.U;
            }

            return isOpponentInCell;
        }

        public bool CheckIfMoveAppearsInToolLegalMovements(Point i_FromWhere, Point i_ToWhere)
        {
            int numberOfAllPosiblePositions;
            bool isLegalMove, isCurrentMoveExists;

            isLegalMove = isCurrentMoveExists = false;
            for (int i = 0; i < m_CurrentPlayer.NumberOfTools && !isCurrentMoveExists; ++i)
            {
                if (i_FromWhere == m_CurrentPlayer.CurrentGameToolsPositions[i].CurrentPosition)
                {
                    isLegalMove = isCurrentMoveExists = false;
                    numberOfAllPosiblePositions = m_CurrentPlayer.CurrentGameToolsPositions[i].ToolPossibleMovements.Count;
                    for (int j = 0; j < numberOfAllPosiblePositions && !isLegalMove; ++j)
                    {
                        isLegalMove = m_CurrentPlayer.CurrentGameToolsPositions[i].ToolPossibleMovements[j].PlayerNextPosition == i_ToWhere;
                        if (isLegalMove && !m_CurrentPlayer.IsHasNoOptionToSkip)
                        {
                            removeOpponentFromGame(m_CurrentPlayer.CurrentGameToolsPositions[i].ToolPossibleMovements[j].SkippedToolPosition);
                        }
                    }
                }
            }

            return !isCurrentMoveExists && isLegalMove;
        }

        private void updateBoardContentAfterMovement(Point i_FromWhere, Point i_ToWhere)
        {
            if (m_GameBoard.CheckIfKingInCell(i_FromWhere) || checkIfBecomingKing(i_ToWhere))
            {
                updateToolSignInGameBoardIfKing(i_ToWhere);
            }
            else
            {
                m_GameBoard.GameBoard[i_ToWhere.X, i_ToWhere.Y].CellContent = m_CurrentPlayer.Sign;
            }

            m_GameBoard.GameBoard[i_FromWhere.X, i_FromWhere.Y].CellContent = eSign.Empty;
        }

        private bool checkIfBecomingKing(Point i_ToWhere)
        {
            int backRowPlayerO = 0;
            int backRowPlayerX = m_GameBoard.Rows - 1;

            return (m_CurrentPlayer.Sign == eSign.X && i_ToWhere.X == backRowPlayerO) ||
                (m_CurrentPlayer.Sign == eSign.O && i_ToWhere.X == backRowPlayerX);
        }

        private void removeOpponentFromGame(Point i_OpponentCurrentPosition)
        {
            m_OpponentPlayer.NumberOfTools--;
            m_GameBoard.GameBoard[i_OpponentCurrentPosition.X, i_OpponentCurrentPosition.Y].CellContent = eSign.Empty;
            m_IsCurrentPlayerSkipped = true;
            OnEaten(new ButtonPanelPositionEventArgs(i_OpponentCurrentPosition));
        }

        private void calculatePlayersToolsValue(Player i_Player)
        {
            for (int i = 0; i < i_Player.NumberOfTools; ++i)
            {
                if (m_GameBoard.CheckIfKingInCell(i_Player.CurrentGameToolsPositions[i].CurrentPosition))
                {
                    i_Player.ToolsValue += 4;
                }
                else
                {
                    i_Player.ToolsValue++;
                }
            }
        }

        public void CalculateWinnersScore(Player i_Winner, Player i_Loser)
        {
            calculatePlayersToolsValue(i_Winner);
            calculatePlayersToolsValue(i_Loser);
            i_Winner.Score += Math.Abs(i_Winner.ToolsValue - i_Loser.ToolsValue);
        }

        private void updateToolSignInGameBoardIfKing(Point i_ToWhere)
        {
            if (m_CurrentPlayer.Sign == eSign.X)
            {
                m_GameBoard.GameBoard[i_ToWhere.X, i_ToWhere.Y].CellContent = eSign.K;
            }
            else if (m_CurrentPlayer.Sign == eSign.O)
            {
                m_GameBoard.GameBoard[i_ToWhere.X, i_ToWhere.Y].CellContent = eSign.U;
            }
        }

        public void UpdateDataForPlayerAfterMovement(Point i_FromWhere, Point i_ToWhere)
        {
            updateBoardContentAfterMovement(i_FromWhere, i_ToWhere);
            updatePlayerGameToolPositions(CurrentPlayer);
            updatePlayerGameToolPositions(OpponentPlayer);
            updateIfRoundOver();
            OnMovement(new MovePositionEventArgs(i_FromWhere, i_ToWhere));
            if(checkIfBecomingKing(i_ToWhere))
            {
                OnBecameKing(new ButtonPanelPositionEventArgs(i_ToWhere));
            }
        }

        protected virtual void OnMovement(MovePositionEventArgs e)
        {
            if (MovementOccured != null)
            {
                MovementOccured(e);
            }
        }

        protected virtual void OnEaten(ButtonPanelPositionEventArgs e)
        {
            if (EatenOccured != null)
            {
                EatenOccured(e);
            }
        }

        protected virtual void OnBecameKing(ButtonPanelPositionEventArgs e)
        {
            if (BecameKingOccured != null)
            {
                BecameKingOccured(e);
            }
        }

        public void GetMoveFromComputer(out Point o_FromWhere, out Point o_ToWhere)
        {
            int currentGameTool = chooseRandomGameTool();
            int currentGameToolMovement;

            o_FromWhere = m_CurrentPlayer.CurrentGameToolsPositions[currentGameTool].CurrentPosition;
            currentGameToolMovement = r_RandomIndex.Next(m_CurrentPlayer.CurrentGameToolsPositions[currentGameTool].ToolPossibleMovements.Count);
            o_ToWhere = m_CurrentPlayer.CurrentGameToolsPositions[currentGameTool].ToolPossibleMovements[currentGameToolMovement].PlayerNextPosition;
            if (!m_CurrentPlayer.IsHasNoOptionToSkip)
            {
                removeOpponentFromGame(m_CurrentPlayer.CurrentGameToolsPositions[currentGameTool].ToolPossibleMovements[currentGameToolMovement].SkippedToolPosition);
            }
        }

        private int chooseRandomGameTool()
        {
            int currentGameTool;

            currentGameTool = r_RandomIndex.Next(m_CurrentPlayer.CurrentGameToolsPositions.Count);
            while (m_CurrentPlayer.CurrentGameToolsPositions[currentGameTool].ToolPossibleMovements.Count == 0)
            {
                currentGameTool = r_RandomIndex.Next(m_CurrentPlayer.CurrentGameToolsPositions.Count);
            }

            return currentGameTool;
        }

        private void updateIfRoundOver()
        {
            if (m_CurrentPlayer.CheckIfRoundOver() || m_OpponentPlayer.CheckIfRoundOver())
            {
                m_IsRoundOver = true;
            }
        }

        public void GetWinnerPlayer(out Player o_WinnerPlayer, out Player o_LoserPlayer)
        {
            o_WinnerPlayer = o_LoserPlayer = new Player(eSign.Empty, ePlayerType.Human, null);

            if (m_PlayerTwo.CheckIfRoundOver())
            {
                o_WinnerPlayer = m_PlayerOne;
                o_LoserPlayer = m_PlayerTwo;
            }
            else if (m_PlayerOne.CheckIfRoundOver())
            {
                o_WinnerPlayer = m_PlayerTwo;
                o_LoserPlayer = m_PlayerOne;
            }
        }

        public bool CheckIfThereIsTie()
        {
            return m_CurrentPlayer.CheckIfThereIsNoLegalMovementsLeft() && m_OpponentPlayer.CheckIfThereIsNoLegalMovementsLeft();
        }

        public void ChangePlayer(ref int i_PlayerNumber)
        {
            if (!m_IsCurrentPlayerSkipped || m_CurrentPlayer.IsHasNoOptionToSkip)
            {
                updatePlayerNumber(ref i_PlayerNumber);
                updatePlayersStatus(i_PlayerNumber);
            }
        }

        private void updatePlayerNumber(ref int i_PlayerNumber)
        {
            if (i_PlayerNumber == k_PlayerOne)
            {
                i_PlayerNumber = k_PlayerTwo;
            }
            else if (i_PlayerNumber == k_PlayerTwo)
            {
                i_PlayerNumber = k_PlayerOne;
            }
        }

        public void InitializeRound()
        {
            m_IsRoundOver = false;
            m_IsCurrentPlayerSkipped = false;
            m_GameBoard.InitializeBoard();
            PlayerOne.CurrentGameToolsPositions.Clear();
            PlayerTwo.CurrentGameToolsPositions.Clear();
        }
    }
}
