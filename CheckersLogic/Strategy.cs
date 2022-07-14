using System.Collections.Generic;
using System.Drawing;

namespace CheckersLogic
{
    public class Strategy
    {
        private Point m_CurrentPosition;
        private List<PossiblePositions> m_ToolPossibleMovements;

        public Strategy(Point i_CurrentPosition)
        {
            m_CurrentPosition = i_CurrentPosition;
            m_ToolPossibleMovements = new List<PossiblePositions>();
        }

        public Point CurrentPosition
        {
            get
            {
                return m_CurrentPosition;
            }

            set
            {
                m_CurrentPosition = value;
            }
        }

        public List<PossiblePositions> ToolPossibleMovements
        {
            get
            {
                return m_ToolPossibleMovements;
            }

            set
            {
                m_ToolPossibleMovements = value;
            }
        }
    }
}
