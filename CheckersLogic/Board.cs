using System;
using System.Text;
using System.Drawing;

namespace CheckersLogic
{
    public class Board
    {
        private int m_Rows;
        private int m_Cols;
        private Cell[,] m_GameBoard;

        public Board(int i_BoardSize)
        {
            m_Rows = m_Cols = i_BoardSize;
            m_GameBoard = new Cell[i_BoardSize, i_BoardSize];
            InitializeBoard();
        }

        public Cell[,] GameBoard
        {
            get
            {
                return m_GameBoard;
            }
        }

        public int Rows
        {
            get
            {
                return m_Rows;
            }

            set
            {
                m_Rows = value;
            }
        }

        public int Cols
        {
            get
            {
                return m_Cols;
            }

            set
            {
                m_Cols = value;
            }
        }

        public void InitializeBoard()
        {
            for (int i = 0; i < m_Rows; ++i)
            {
                for (int j = 0; j < m_Cols; ++j)
                {
                    m_GameBoard[i, j] = new Cell();
                    if (i < (m_Rows / 2) - 1)
                    {
                        if ((i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0))
                        {
                            m_GameBoard[i, j].CellContent = eSign.Empty;
                        }
                        else
                        {
                            m_GameBoard[i, j].CellContent = eSign.O;
                        }
                    }
                    else if (i > m_Rows / 2)
                    {
                        if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                        {
                            m_GameBoard[i, j].CellContent = eSign.X;
                        }
                        else
                        {
                            m_GameBoard[i, j].CellContent = eSign.Empty;
                        }
                    }
                    else
                    {
                        m_GameBoard[i, j].CellContent = eSign.Empty;
                    }
                }
            }
        }

        public bool CheckIfOutOfBoardBoundaries(Point i_CurrentPosition)
        {
            return i_CurrentPosition.X < 0 || i_CurrentPosition.X > m_Rows - 1 ||
                i_CurrentPosition.Y < 0 || i_CurrentPosition.Y > m_Cols - 1;
        }

        internal bool CheckIfCellEmpty(Point i_Position)
        {
            return m_GameBoard[i_Position.X, i_Position.Y].CellContent == eSign.Empty;
        }

        internal bool CheckIfKingInCell(Point i_Position)
        {
            return m_GameBoard[i_Position.X, i_Position.Y].CellContent == eSign.K ||
                m_GameBoard[i_Position.X, i_Position.Y].CellContent == eSign.U;
        }
    }
}
