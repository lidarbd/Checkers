using System;
using System.Collections.Generic;

namespace CheckersLogic
{
    public class Player
    {
        private const int k_MaxNameLen = 20;
        private readonly string r_Name;
        private readonly eSign r_Sign;
        private readonly ePlayerType m_Type;
        private int m_Score;
        private int m_ToolsValue;
        private List<Strategy> m_CurrentGameToolsPositions;
        private int m_NumberOfTools;
        private bool m_HasNoOptionToSkip;

        public Player(eSign i_PlayerSign, ePlayerType i_PlayerType, string i_PlayerName)
        {
            r_Sign = i_PlayerSign;
            m_Type = i_PlayerType;
            r_Name = i_PlayerName;
            m_Score = 0;
            m_NumberOfTools = 0;
            m_HasNoOptionToSkip = true;
            m_CurrentGameToolsPositions = new List<Strategy>();
        }

        public int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }

        public int ToolsValue
        {
            get
            {
                return m_ToolsValue;
            }

            set
            {
                m_ToolsValue = value;
            }
        }

        public eSign Sign
        {
            get
            {
                return r_Sign;
            }
        }

        public ePlayerType Type
        {
            get
            {
                return m_Type;
            }
        }

        public int NumberOfTools
        {
            get
            {
                return m_NumberOfTools;
            }

            set
            {
                m_NumberOfTools = value;
            }
        }

        public bool IsHasNoOptionToSkip
        {
            get
            {
                return m_HasNoOptionToSkip;
            }

            set
            {
                m_HasNoOptionToSkip = value;
            }
        }

        public List<Strategy> CurrentGameToolsPositions
        {
            get
            {
                return m_CurrentGameToolsPositions;
            }

            set
            {
                m_CurrentGameToolsPositions = value;
            }
        }

        public string Name
        {
            get
            {
                return r_Name;
            }
        }

        public void InitializeNumberOfTools(int i_GameBoardCols)
        {
            m_NumberOfTools = (i_GameBoardCols / 2) * ((i_GameBoardCols / 2) - 1);
        }

        internal bool CheckIfRoundOver()
        {
            return m_NumberOfTools == 0 || CheckIfThereIsNoLegalMovementsLeft();
        }

        internal bool CheckIfThereIsNoLegalMovementsLeft()
        {
            bool isNoLegalMovementsLeft = true;

            for (int i = 0; i < m_NumberOfTools; ++i)
            {
                if (m_CurrentGameToolsPositions[i].ToolPossibleMovements.Count > 0)
                {
                    isNoLegalMovementsLeft = false;
                }
            }

            return isNoLegalMovementsLeft;
        }
    }
}
